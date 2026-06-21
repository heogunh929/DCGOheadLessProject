// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_049.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_049Script : ICardScript
{
    private const string InheritedDestroyedStableId = "BT1-049:inherited:on-dp-zero-delete:draw-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-049",
        "BT1_049",
        CardEffectPortingStatus.Implemented,
        "Inherited owner-turn OnDestroyedAnyone draw 1 for opponent Digimon deleted by DP 0 is implemented from RuleProcessor destruction payload.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                InheritedDestroyedStableId,
                EffectTiming.OnDestroyedAnyone,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt1ScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.TurnPlayerId == controller
                    && effectContext.GetValueOrDefault("DestroyedController") is PlayerId destroyedController
                    && destroyedController != controller
                    && effectContext.GetValueOrDefault("DestroyedByDpZero") is true
                    && effectContext.State.GetPlayer(controller).Deck.Count >= 1),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != InheritedDestroyedStableId)
        {
            throw new DomainException($"BT1-049 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.Draw(state, controller, 1, context.Trace);
        });
    }
}
