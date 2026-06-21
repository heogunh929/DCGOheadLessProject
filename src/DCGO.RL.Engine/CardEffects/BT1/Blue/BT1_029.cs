// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_029.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_029Script : ICardScript
{
    private const string OnPlayStableId = "BT1-029:on-play:draw-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-029",
        "BT1_029",
        CardEffectPortingStatus.Implemented,
        "OnPlay Draw 1 is implemented through the draw primitive and L0006 OnAddHand/OnDraw event boundary.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt1ScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Deck.Count >= 1),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != OnPlayStableId)
        {
            throw new DomainException($"BT1-029 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.Draw(state, controller, 1, context.Trace);
        });
    }
}
