// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_09.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3AngewomonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-09",
        "ST3_09",
        CardEffectPortingStatus.Implemented,
        "Original OnEnterFieldAnyone plus CanTriggerWhenDigivolving maps to WhenDigivolving; security 3 or fewer condition recovers 1 from deck through Tier1PrimitiveService.RecoverFromDeck.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-09:when-digivolving:recovery-plus-1-deck",
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Security.Count <= 3
                    && effectContext.State.GetPlayer(controller).Deck.Count > 0),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            if (context.Resolution.StableId != "ST3-09:when-digivolving:recovery-plus-1-deck")
            {
                throw new DomainException($"ST3-09 cannot resolve unknown effect '{context.Resolution.StableId}'.");
            }

            if (!StarterScriptSupport.IsTopSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST3-09 recovery requires this card as the top card in battle area.");
            }

            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.GetPlayer(controller).Security.Count > 3)
            {
                throw new DomainException("ST3-09 recovery requires 3 or fewer security cards.");
            }

            primitives.RecoverFromDeck(state, controller, count: 1, context.Trace);
        });
}
