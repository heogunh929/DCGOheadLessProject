// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_086.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_086Script : ICardScript
{
    private const string StartTurnStableId = "BT20-086:on-start-turn:set-memory-to-three";
    private const string SecurityStableId = "BT20-086:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-086",
        "BT20_086",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnStartTurn set-memory-to-3 and SecuritySkill play-self Tamer are implemented. StartMainPhase hand/trash Cyborg/Machine source placement plus opponent security flip remains blocked by trait metadata, hand/trash-to-source placement, source ordering, security face-up flip, and optional cost continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt20ScriptSupport.CreateSetMemoryTo3TamerDescriptor(
                StartTurnStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            Bt20ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
                SecurityStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == StartTurnStableId)
        {
            context.WithState((state, _) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt20ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt20ScriptSupport.SetMemoryTo3IfLow(state, controller);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt20ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt20ScriptSupport.PlaySelfTamerFromExecuting(
                    state,
                    primitives,
                    controller,
                    context.Resolution.SourceCard);
            });
            return;
        }

        throw new DomainException($"BT20-086 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
