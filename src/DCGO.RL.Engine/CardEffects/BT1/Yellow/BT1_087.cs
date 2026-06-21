// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_087.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_087Script : ICardScript
{
    private const string StartTurnStableId = "BT1-087:on-start-turn:set-memory-to-three";
    private const string SecurityStableId = "BT1-087:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-087",
        "BT1_087",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnStartTurn set-memory-to-3 and SecuritySkill play-self Tamer are implemented. OnPlay look at security, reveal/add one to hand, conditional Recovery, and shuffle security require security-stack selection/reveal/shuffle continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt1ScriptSupport.CreateSetMemoryTo3TamerDescriptor(
                StartTurnStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            Bt1ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
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
                    ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt1ScriptSupport.SetMemoryTo3IfLow(state, controller);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt1ScriptSupport.PlaySelfTamerFromExecuting(
                    state,
                    primitives,
                    controller,
                    context.Resolution.SourceCard);
            });
            return;
        }

        throw new DomainException($"BT1-087 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
