using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Effects;

public sealed record SecurityEffectExecutionContinuation(
    CardInstanceId SecurityCard,
    PlayerId Defender,
    bool WasFaceDown,
    EffectResolution PendingResolution,
    SelectionRequest PendingSelectionRequest,
    EffectDecisionStage PendingStage,
    IReadOnlyList<EffectResolution> SecurityResolutions,
    IReadOnlyList<EffectResolution> ActivatedMainOptionResolutions,
    IReadOnlyList<SelectionResultApplicationResult> SelectionApplications,
    IReadOnlyList<EffectResolution> RemainingSecurityResolutions,
    IReadOnlyList<EffectResolution> RemainingOptionResolutions,
    bool IsResolvingActivatedMainOption)
{
    public string StableContinuationId => PendingResolution.StableId;
}

public sealed record SecurityEffectExecutionResult(
    CardInstanceId SecurityCard,
    IReadOnlyList<EffectResolution> SecurityResolutions,
    IReadOnlyList<EffectResolution> ActivatedMainOptionResolutions,
    IReadOnlyList<SelectionResultApplicationResult> SelectionApplications,
    EffectResolution? PendingResolution = null,
    SelectionRequest? PendingSelectionRequest = null,
    SecurityEffectExecutionContinuation? Continuation = null)
{
    public bool ExecutedAny =>
        SecurityResolutions.Count > 0
        || ActivatedMainOptionResolutions.Count > 0
        || SelectionApplications.Count > 0;

    public bool HasPendingSelection => Continuation is not null || PendingSelectionRequest is not null;
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

    internal Tier1PrimitiveService RuntimePrimitiveService => _primitives;

    public SecurityEffectExecutionResult ExecuteSecurityEffects(
        GameState state,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        GameTrace? trace = null,
        IReadOnlyDictionary<string, object?>? values = null)
    {
        var result = ExecuteSecurityEffectsWithResult(state, securityCard, defender, wasFaceDown, trace, values);
        if (result.HasPendingSelection)
        {
            throw new DomainException(
                $"Security effect resolution '{result.PendingResolution!.StableId}' requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
        }

        return result;
    }

    public SecurityEffectExecutionResult ExecuteSecurityEffectsWithResult(
        GameState state,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        GameTrace? trace = null,
        IReadOnlyDictionary<string, object?>? values = null)
    {
        var script = GetSecurityScript(state, securityCard, defender);
        var descriptors = script.CreateEffectDescriptors(new CardScriptContext(
            state,
            securityCard,
            SourcePermanent: null,
            Controller: defender));

        var securityValues = values is null
            ? new Dictionary<string, object?>(StringComparer.Ordinal)
            : new Dictionary<string, object?>(values, StringComparer.Ordinal);
        securityValues["Card"] = securityCard;
        securityValues["SecurityCard"] = securityCard;
        securityValues["Defender"] = defender;
        securityValues["IsFaceDown"] = wasFaceDown;
        securityValues["SourceZone"] = state.Cards[securityCard].CurrentZone;

        var securityContext = new EffectContext(
            state,
            EffectTiming.SecuritySkill,
            defender,
            securityCard,
            Values: securityValues);
        var collected = new TriggerCollector().Collect(securityContext, descriptors);

        return ContinueSecurityResolutions(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            collected.QueuedEffects,
            collected.QueuedEffects,
            new List<EffectResolution>(),
            new List<SelectionResultApplicationResult>(),
            trace);
    }

    public SecurityEffectExecutionResult ResumeSecurityEffects(
        GameState state,
        SecurityEffectExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);
        ArgumentNullException.ThrowIfNull(selectionResult);

        var script = GetSecurityScript(state, continuation.SecurityCard, continuation.Defender);
        var activatedMainOptions = continuation.ActivatedMainOptionResolutions.ToList();
        var selectionApplications = continuation.SelectionApplications.ToList();

        return ContinuePendingResolutionAfterSelection(
            state,
            script,
            continuation.SecurityCard,
            continuation.Defender,
            continuation.WasFaceDown,
            continuation.SecurityResolutions,
            activatedMainOptions,
            selectionApplications,
            continuation.PendingResolution,
            continuation.PendingSelectionRequest,
            continuation.PendingStage,
            selectionResult,
            continuation.RemainingSecurityResolutions,
            continuation.RemainingOptionResolutions,
            continuation.IsResolvingActivatedMainOption,
            trace);
    }

    private ICardScript GetSecurityScript(GameState state, CardInstanceId securityCard, PlayerId defender)
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

        return _cardScriptRegistry.GetScript(definition);
    }

    private SecurityEffectExecutionResult ContinueSecurityResolutions(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        GameTrace? trace)
    {
        if (remainingSecurityResolutions.Count == 0)
        {
            return Completed(securityCard, allSecurityResolutions, activatedMainOptions, selectionApplications);
        }

        var resolution = remainingSecurityResolutions[0];
        var nextSecurityResolutions = remainingSecurityResolutions.Skip(1).ToArray();
        if (resolution.SecurityExecutionMode == SecurityEffectExecutionMode.ActivateMainOption)
        {
            var optionResolutions = CollectOptionResolutions(state, script, securityCard, defender).ToArray();
            return ContinueOptionResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                nextSecurityResolutions,
                optionResolutions,
                activatedMainOptions,
                selectionApplications,
                trace);
        }

        return ContinueDirectSecurityResolution(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            allSecurityResolutions,
            resolution,
            nextSecurityResolutions,
            activatedMainOptions,
            selectionApplications,
            trace);
    }

    private SecurityEffectExecutionResult ContinueDirectSecurityResolution(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        EffectResolution resolution,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        GameTrace? trace)
    {
        if (resolution.PendingSelectionRequest is not null)
        {
            return ContinuePendingResolution(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                resolution,
                activatedMainOptions,
                selectionApplications,
                remainingSecurityResolutions,
                Array.Empty<EffectResolution>(),
                isResolvingActivatedMainOption: false,
                trace);
        }

        script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
        return ContinueSecurityResolutions(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            allSecurityResolutions,
            remainingSecurityResolutions,
            activatedMainOptions,
            selectionApplications,
            trace);
    }

    private SecurityEffectExecutionResult ContinueOptionResolutions(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        GameTrace? trace)
    {
        if (remainingOptionResolutions.Count == 0)
        {
            return ContinueSecurityResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                remainingSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                trace);
        }

        var resolution = remainingOptionResolutions[0];
        var nextOptionResolutions = remainingOptionResolutions.Skip(1).ToArray();
        activatedMainOptions.Add(resolution);

        if (resolution.PendingSelectionRequest is not null)
        {
            return ContinuePendingResolution(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                resolution,
                activatedMainOptions,
                selectionApplications,
                remainingSecurityResolutions,
                nextOptionResolutions,
                isResolvingActivatedMainOption: true,
                trace);
        }

        script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
        return ContinueOptionResolutions(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            allSecurityResolutions,
            remainingSecurityResolutions,
            nextOptionResolutions,
            activatedMainOptions,
            selectionApplications,
            trace);
    }

    private SecurityEffectExecutionResult ContinueAfterSelectionApplication(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        SelectionResultApplicationResult application,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        bool isResolvingActivatedMainOption,
        GameTrace? trace)
    {
        if (application.NextResolution is not null)
        {
            if (isResolvingActivatedMainOption)
            {
                return ContinueOptionResolutions(
                    state,
                    script,
                    securityCard,
                    defender,
                    wasFaceDown,
                    allSecurityResolutions,
                    remainingSecurityResolutions,
                    new[] { application.NextResolution }.Concat(remainingOptionResolutions).ToArray(),
                    activatedMainOptions,
                    selectionApplications,
                    trace);
            }

            return ContinueDirectSecurityResolution(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                application.NextResolution,
                remainingSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                trace);
        }

        return isResolvingActivatedMainOption
            ? ContinueOptionResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                remainingSecurityResolutions,
                remainingOptionResolutions,
                activatedMainOptions,
                selectionApplications,
                trace)
            : ContinueSecurityResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                remainingSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                trace);
    }

    private SecurityEffectExecutionResult ContinuePendingResolution(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        EffectResolution resolution,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        bool isResolvingActivatedMainOption,
        GameTrace? trace)
    {
        var stage = resolution.OptionalSelectionRequest is not null
            ? EffectDecisionStage.Optional
            : EffectDecisionStage.Selection;
        var request = stage == EffectDecisionStage.Optional
            ? resolution.OptionalSelectionRequest!
            : resolution.SelectionRequest!;

        if (_decisionProvider is null)
        {
            return Pending(
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                resolution,
                request,
                stage,
                remainingSecurityResolutions,
                remainingOptionResolutions,
                isResolvingActivatedMainOption);
        }

        var selection = _decisionProvider.ChooseSelection(request);
        return ContinuePendingResolutionAfterSelection(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            allSecurityResolutions,
            activatedMainOptions,
            selectionApplications,
            resolution,
            request,
            stage,
            selection,
            remainingSecurityResolutions,
            remainingOptionResolutions,
            isResolvingActivatedMainOption,
            trace);
    }

    private SecurityEffectExecutionResult ContinuePendingResolutionAfterSelection(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        EffectResolution resolution,
        SelectionRequest request,
        EffectDecisionStage stage,
        SelectionResult selectionResult,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        bool isResolvingActivatedMainOption,
        GameTrace? trace)
    {
        if (!string.Equals(selectionResult.RequestId, request.Id, StringComparison.Ordinal))
        {
            throw new DomainException(
                $"SelectionResult request id '{selectionResult.RequestId}' does not match pending request '{request.Id}'.");
        }

        if (stage == EffectDecisionStage.Optional)
        {
            SelectionValidator.Validate(request, selectionResult);
            if (selectionResult.SelectedBoolean == false)
            {
                return ContinueAfterSkippedOptional(
                    state,
                    script,
                    securityCard,
                    defender,
                    wasFaceDown,
                    allSecurityResolutions,
                    activatedMainOptions,
                    selectionApplications,
                    remainingSecurityResolutions,
                    remainingOptionResolutions,
                    isResolvingActivatedMainOption,
                    trace);
            }

            if (resolution.SelectionRequest is not null)
            {
                return Pending(
                    securityCard,
                    defender,
                    wasFaceDown,
                    allSecurityResolutions,
                    activatedMainOptions,
                    selectionApplications,
                    resolution,
                    resolution.SelectionRequest,
                    EffectDecisionStage.Selection,
                    remainingSecurityResolutions,
                    remainingOptionResolutions,
                    isResolvingActivatedMainOption);
            }

            script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
            return ContinueAfterSkippedOptional(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                remainingSecurityResolutions,
                remainingOptionResolutions,
                isResolvingActivatedMainOption,
                trace);
        }

        if (stage != EffectDecisionStage.Selection)
        {
            throw new DomainException($"Unsupported security decision stage '{stage}' for '{resolution.StableId}'.");
        }

        var application = _selectionApplicator.Apply(state, resolution, request, selectionResult, _primitives, trace);
        selectionApplications.Add(application);
        return ContinueAfterSelectionApplication(
            state,
            script,
            securityCard,
            defender,
            wasFaceDown,
            allSecurityResolutions,
            activatedMainOptions,
            selectionApplications,
            application,
            remainingSecurityResolutions,
            remainingOptionResolutions,
            isResolvingActivatedMainOption,
            trace);
    }

    private SecurityEffectExecutionResult ContinueAfterSkippedOptional(
        GameState state,
        ICardScript script,
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> allSecurityResolutions,
        List<EffectResolution> activatedMainOptions,
        List<SelectionResultApplicationResult> selectionApplications,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        bool isResolvingActivatedMainOption,
        GameTrace? trace) =>
        isResolvingActivatedMainOption
            ? ContinueOptionResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                remainingSecurityResolutions,
                remainingOptionResolutions,
                activatedMainOptions,
                selectionApplications,
                trace)
            : ContinueSecurityResolutions(
                state,
                script,
                securityCard,
                defender,
                wasFaceDown,
                allSecurityResolutions,
                remainingSecurityResolutions,
                activatedMainOptions,
                selectionApplications,
                trace);

    private IReadOnlyList<EffectResolution> CollectOptionResolutions(
        GameState state,
        ICardScript script,
        CardInstanceId sourceCard,
        PlayerId controller)
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
        var descriptors = script.CreateEffectDescriptors(new CardScriptContext(
            state,
            sourceCard,
            SourcePermanent: null,
            Controller: controller));
        var collected = new TriggerCollector().Collect(optionContext, descriptors);

        if (collected.QueuedEffects.Count == 0)
        {
            throw new DomainException(
                $"Security activation for card '{sourceCard}' requires at least one runnable OptionSkill main effect.");
        }

        return collected.QueuedEffects;
    }

    private static SecurityEffectExecutionResult Completed(
        CardInstanceId securityCard,
        IReadOnlyList<EffectResolution> securityResolutions,
        IReadOnlyList<EffectResolution> activatedMainOptions,
        IReadOnlyList<SelectionResultApplicationResult> selectionApplications) =>
        new(securityCard, securityResolutions, activatedMainOptions.ToArray(), selectionApplications.ToArray());

    private static SecurityEffectExecutionResult Pending(
        CardInstanceId securityCard,
        PlayerId defender,
        bool wasFaceDown,
        IReadOnlyList<EffectResolution> securityResolutions,
        IReadOnlyList<EffectResolution> activatedMainOptions,
        IReadOnlyList<SelectionResultApplicationResult> selectionApplications,
        EffectResolution pendingResolution,
        SelectionRequest pendingRequest,
        EffectDecisionStage pendingStage,
        IReadOnlyList<EffectResolution> remainingSecurityResolutions,
        IReadOnlyList<EffectResolution> remainingOptionResolutions,
        bool isResolvingActivatedMainOption)
    {
        return new SecurityEffectExecutionResult(
            securityCard,
            securityResolutions,
            activatedMainOptions.ToArray(),
            selectionApplications.ToArray(),
            pendingResolution,
            pendingRequest,
            new SecurityEffectExecutionContinuation(
                securityCard,
                defender,
                wasFaceDown,
                pendingResolution,
                pendingRequest,
                pendingStage,
                securityResolutions.ToArray(),
                activatedMainOptions.ToArray(),
                selectionApplications.ToArray(),
                remainingSecurityResolutions.ToArray(),
                remainingOptionResolutions.ToArray(),
                isResolvingActivatedMainOption));
    }
}
