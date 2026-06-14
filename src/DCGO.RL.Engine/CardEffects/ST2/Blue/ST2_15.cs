using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2WereGarurumonScript : ICardScript
{
    private const string SelectedSourcePermanentKey = "SelectedSourcePermanent";

    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-15",
        "ST2_15",
        CardEffectPortingStatus.Implemented,
        "Main/security choose one of your battle area Digimon, then choose one playable Digimon digivolution source under it and play it as a new Digimon without paying cost through chained SelectionResultApplication and ZoneMover-backed primitive.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-15:option:select-source-host",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOwnerBattleAreaDigimonWithPlayableEvolutionSourceSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-15:option:source-host",
                    "Select 1 of your Digimon that has a playable Digimon digivolution source."),
                SelectionContinuation: ContinueWithSourceCardSelection),
            new EffectDescriptor(
                "ST2-15:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
                        effectContext.State,
                        controller).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST2-15:security:main-option-activation")
        {
            throw new DomainException("ST2-15 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST2-15 two-stage source play must be resolved through SelectionResultApplicator.");
    }

    private static void ContinueWithSourceCardSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        var selectedPermanent = context.SelectedPermanentIds.SingleOrDefault();
        if (selectedPermanent == default)
        {
            throw new DomainException("ST2-15 requires exactly one selected source host Digimon.");
        }

        if (!StarterScriptSupport.HasPlayableEvolutionSource(context.State, controller, selectedPermanent))
        {
            throw new DomainException("ST2-15 selected Digimon no longer has a playable Digimon digivolution source.");
        }

        var request = StarterScriptSupport.CreatePlayableEvolutionSourceCardSelectionRequest(
            context.State,
            controller,
            selectedPermanent,
            $"ST2-15:option:source-card:{selectedPermanent.Value}",
            "Select 1 Digimon digivolution source to play without paying its cost.");

        var values = new Dictionary<string, object?>(context.Resolution.Context.Payload, StringComparer.Ordinal)
        {
            [SelectedSourcePermanentKey] = selectedPermanent,
        };
        var nextContext = new EffectContext(
            context.State,
            context.Resolution.Timing,
            controller,
            context.Resolution.SourceCard,
            context.Resolution.SourcePermanent,
            values);

        context.SetNextResolution(new EffectResolution(
            $"ST2-15:option:play-source-card:{selectedPermanent.Value}",
            context.Resolution.Timing,
            context.Resolution.SourceCard,
            context.Resolution.SourcePermanent,
            controller,
            IsBackground: false,
            IsOptional: false,
            nextContext,
            SelectionRequest: request,
            SelectionContinuation: PlaySelectedSourceCard));
    }

    private static void PlaySelectedSourceCard(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        if (context.Resolution.Context.GetValueOrDefault(SelectedSourcePermanentKey) is not PermanentId sourcePermanent)
        {
            throw new DomainException("ST2-15 source-card selection requires the selected host permanent context.");
        }

        foreach (var selectedCard in context.SelectedCardIds)
        {
            if (!StarterScriptSupport.IsPlayableEvolutionSource(context.State, controller, sourcePermanent, selectedCard))
            {
                throw new DomainException("ST2-15 selected card is no longer a playable Digimon digivolution source.");
            }

            var targetFrame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(context.State, controller);
            context.Primitives.PlayEvolutionSourceAsNewPermanent(
                context.State,
                controller,
                selectedCard,
                sourcePermanent,
                targetFrame,
                suspended: false);
        }
    }
}
