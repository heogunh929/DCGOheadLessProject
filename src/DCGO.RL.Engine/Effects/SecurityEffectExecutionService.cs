using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Effects;

public sealed record SecurityEffectExecutionResult(
    CardInstanceId SecurityCard,
    IReadOnlyList<EffectResolution> SecurityResolutions,
    IReadOnlyList<EffectResolution> ActivatedMainOptionResolutions,
    IReadOnlyList<SelectionResultApplicationResult> SelectionApplications)
{
    public bool ExecutedAny =>
        SecurityResolutions.Count > 0
        || ActivatedMainOptionResolutions.Count > 0
        || SelectionApplications.Count > 0;
}

public sealed class SecurityEffectExecutionService
{
    private readonly ICardScriptRegistry _cardScriptRegistry;
    private readonly IDecisionProvider? _decisionProvider;
    private readonly SelectionResultApplicator _selectionApplicator;
    private readonly Tier1PrimitiveService _primitives;

    public SecurityEffectExecutionService(
        ICardScriptRegistry cardScriptRegistry,
        IDecisionProvider? decisionProvider = null,
        SelectionResultApplicator? selectionApplicator = null,
        Tier1PrimitiveService? primitives = null)
    {
        _cardScriptRegistry = cardScriptRegistry ?? throw new ArgumentNullException(nameof(cardScriptRegistry));
        _decisionProvider = decisionProvider;
        _selectionApplicator = selectionApplicator ?? new SelectionResultApplicator();
        _primitives = primitives ?? new Tier1PrimitiveService();
    }

    public SecurityEffectExecutionResult ExecuteSecurityEffects(
        GameState state,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        _ = state.GetPlayer(defender);

        if (!state.Cards.TryGetValue(securityCard, out var instance))
        {
            throw new DomainException($"Security card '{securityCard}' does not exist.");
        }

        if (instance.Owner != defender)
        {
            throw new DomainException(
                $"Security card '{securityCard}' is owned by '{instance.Owner}', not defender '{defender}'.");
        }

        if (instance.CurrentZone is not (Zone.Security or Zone.Executing))
        {
            throw new DomainException(
                $"Security card '{securityCard}' must be in Security or Executing, actual '{instance.CurrentZone}'.");
        }

        var definition = state.CardDefinitions.TryGetValue(instance.DefinitionId, out var cardDefinition)
            ? cardDefinition
            : throw new DomainException($"Card definition '{instance.DefinitionId}' does not exist.");
        var script = _cardScriptRegistry.GetScript(definition);
        var descriptors = script.CreateEffectDescriptors(new CardScriptContext(
            state,
            securityCard,
            SourcePermanent: null,
            Controller: instance.Owner));

        var securityContext = new EffectContext(
            state,
            EffectTiming.SecuritySkill,
            instance.Owner,
            securityCard,
            Values: new Dictionary<string, object?>
            {
                ["Card"] = securityCard,
                ["IsFaceDown"] = wasFaceDown,
                ["SourceZone"] = instance.CurrentZone,
            });
        var collected = new TriggerCollector().Collect(securityContext, descriptors);

        var activatedMainOptions = new List<EffectResolution>();
        var selectionApplications = new List<SelectionResultApplicationResult>();
        foreach (var resolution in collected.QueuedEffects)
        {
            if (resolution.SecurityExecutionMode == SecurityEffectExecutionMode.ActivateMainOption)
            {
                ActivateMainOption(
                    state,
                    script,
                    descriptors,
                    securityCard,
                    instance.Owner,
                    trace,
                    activatedMainOptions,
                    selectionApplications);
                continue;
            }

            ExecuteDirectSecurityResolution(state, script, resolution, trace, selectionApplications);
        }

        return new SecurityEffectExecutionResult(
            securityCard,
            collected.QueuedEffects,
            activatedMainOptions,
            selectionApplications);
    }

    private void ActivateMainOption(
        GameState state,
        ICardScript script,
        IReadOnlyList<EffectDescriptor> descriptors,
        CardInstanceId sourceCard,
        PlayerId controller,
        GameTrace? trace,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var optionContext = new EffectContext(
            state,
            EffectTiming.OptionSkill,
            controller,
            sourceCard,
            Values: new Dictionary<string, object?>
            {
                ["Card"] = sourceCard,
                ["ActivatedFromSecurity"] = true,
                ["PayCost"] = false,
                ["SourceZone"] = state.Cards[sourceCard].CurrentZone,
            });
        var collected = new TriggerCollector().Collect(optionContext, descriptors);

        if (collected.QueuedEffects.Count == 0)
        {
            throw new DomainException(
                $"Security activation for card '{sourceCard}' requires at least one runnable OptionSkill main effect.");
        }

        foreach (var optionResolution in collected.QueuedEffects)
        {
            activatedMainOptions.Add(optionResolution);
            ExecuteOptionMainResolution(
                state,
                script,
                optionResolution,
                trace,
                activatedMainOptions,
                selectionApplications);
        }
    }

    private void ExecuteDirectSecurityResolution(
        GameState state,
        ICardScript script,
        EffectResolution resolution,
        GameTrace? trace,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (resolution.PendingSelectionRequest is not null)
        {
            ApplyRequiredSelection(state, script, resolution, trace, null, selectionApplications);
            return;
        }

        script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
    }

    private void ExecuteOptionMainResolution(
        GameState state,
        ICardScript script,
        EffectResolution resolution,
        GameTrace? trace,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (resolution.PendingSelectionRequest is not null)
        {
            ApplyRequiredSelection(state, script, resolution, trace, activatedMainOptions, selectionApplications);
            return;
        }

        script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
    }

    private void ApplyRequiredSelection(
        GameState state,
        ICardScript script,
        EffectResolution resolution,
        GameTrace? trace,
        List<EffectResolution>? activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (_decisionProvider is null)
        {
            throw new DomainException(
                $"Effect resolution '{resolution.StableId}' requires a SelectionResult, but no decision provider was supplied.");
        }

        var request = resolution.PendingSelectionRequest
            ?? throw new DomainException($"Effect resolution '{resolution.StableId}' has no selection request.");
        var result = _decisionProvider.ChooseSelection(request);
        var application = _selectionApplicator.Apply(state, resolution, result, _primitives, trace);
        selectionApplications.Add(application);

        if (application.NextResolution is null)
        {
            return;
        }

        activatedMainOptions?.Add(application.NextResolution);
        ExecuteOptionMainResolution(
            state,
            script,
            application.NextResolution,
            trace,
            activatedMainOptions ?? new List<EffectResolution>(),
            selectionApplications);
    }
}
