using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3HeavensCharmScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-14",
        "ST3_14",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon DP -2000 selection; security moves this card from Executing to Hand through ZoneMover.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-14:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-14:option:dp",
                    "1 of your opponent's Digimon gets -2000 DP for the turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-14:security:add-to-hand",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-14:option:dp-selection")
        {
            throw new DomainException("ST3-14 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-14:security:add-to-hand")
        {
            throw new DomainException($"ST3-14 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-14 security effect requires a source card.");
            primitives.AddExecutingSecurityEffectCardToHand(state, sourceCard, controller);
        });
    }

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: -2000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-14 DP -2000 until turn end");
        }
    }
}
