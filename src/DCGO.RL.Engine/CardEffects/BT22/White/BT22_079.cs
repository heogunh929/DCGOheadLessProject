// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_079.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_079Script : ICardScript
{
    private const string OnPlayStableId = "BT22-079:on-play:draw-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-079",
        "BT22_079",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay Draw 1 is implemented through the draw primitive. Blocker metadata and inherited breeding-area owner-turn Eater play-cost reduction remain blocked by static keyword registry propagation, trait metadata, breeding-source trigger collection, BeforePayCost cost modifier, and once-per-turn cost-reduction decision support.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt22ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Deck.Count >= 1),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != OnPlayStableId)
        {
            throw new DomainException($"BT22-079 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt22ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.Draw(state, controller, 1, context.Trace);
        });
    }
}
