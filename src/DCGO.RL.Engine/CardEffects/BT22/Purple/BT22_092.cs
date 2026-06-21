// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_092.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_092Script : ICardScript
{
    private const string StartTurnStableId = "BT22-092:on-start-turn:set-memory-to-three";
    private const string SecurityStableId = "BT22-092:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-092",
        "BT22_092",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnStartTurn set-memory-to-3 and SecuritySkill play-self Tamer are implemented. Your-turn suspend-cost activation of a triggered Digimon's Main effect and memory +1 remain blocked by Flame/CS trait metadata, OnEnterFieldAnyone played/digivolved payload, suspend-cost continuation, nested Main effect selection/execution, and AddUse propagation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt22ScriptSupport.CreateSetMemoryTo3TamerDescriptor(
                StartTurnStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            Bt22ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
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
                    ?? Bt22ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt22ScriptSupport.SetMemoryTo3IfLow(state, controller);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt22ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt22ScriptSupport.PlaySelfTamerFromExecuting(
                    state,
                    primitives,
                    controller,
                    context.Resolution.SourceCard);
            });
            return;
        }

        throw new DomainException($"BT22-092 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
