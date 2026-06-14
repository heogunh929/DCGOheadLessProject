using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3HolyFlameScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-13",
        "ST3_13",
        CardEffectPortingStatus.Implemented,
        "Main option owner Digimon DP +3000 selection; security applies player-wide field DP +5000 and Security Digimon DP +5000 until turn end, then moves this card from Executing to Hand through ZoneMover.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-13:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-13:option:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-13:security:dp-plus-5000-and-add-to-hand",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-13:option:dp-selection")
        {
            throw new DomainException("ST3-13 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-13:security:dp-plus-5000-and-add-to-hand")
        {
            throw new DomainException($"ST3-13 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporaryPlayerDPModifier(
                state,
                controller,
                amount: 5000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-13 all owner Digimon DP +5000 until turn end");
            primitives.AddTemporarySecurityDigimonDPModifier(
                state,
                controller,
                amount: 5000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-13 owner Security Digimon DP +5000 until turn end");

            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-13 security effect requires a source card.");
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
                amount: 3000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-13 DP +3000 until turn end");
        }
    }
}
