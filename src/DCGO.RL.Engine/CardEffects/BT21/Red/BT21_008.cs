// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_008.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_008Script : ICardScript
{
    private const string InheritedLoseSecurityStableId = "BT21-008:inherited:on-lose-security:memory-plus-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-008",
        "BT21_008",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn once-per-turn memory +1 when opponent security is removed is implemented. OnPlay reveal top 3, select Reptile/Dragonkin and LIBERATOR, and bottom-deck rest remains blocked by trait metadata, multi-category reveal selection, and bottom-deck ordering.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                InheritedLoseSecurityStableId,
                EffectTiming.OnLoseSecurity,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.State.TurnPlayerId == controller
                    && IsOpponentSecurityLoss(effectContext, controller)
                    && Bt21ScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent),
                IsOncePerTurn: true,
                OncePerTurnKey: "BT21-008:inherited:on-lose-security:memory-plus-one"),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != InheritedLoseSecurityStableId)
        {
            throw new DomainException($"BT21-008 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt21ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.ModifyMemory(state, controller, 1);
        });
    }

    private static bool IsOpponentSecurityLoss(EffectContext context, PlayerId controller)
    {
        var opponent = Bt21ScriptSupport.Opponent(context.State, controller);
        return context.Player == opponent
            || context.GetValueOrDefault("Player") is PlayerId player && player == opponent
            || context.GetValueOrDefault("Defender") is PlayerId defender && defender == opponent;
    }
}
