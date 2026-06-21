// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_092.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_092Script : ICardScript
{
    private const string StartTurnStableId = "BT20-092:on-start-turn:set-memory-to-three";
    private const string SecurityStableId = "BT20-092:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-092",
        "BT20_092",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnStartTurn set-memory-to-3 and SecuritySkill play-self Tamer are implemented. OnPlay level-3 hand card to this Tamer's bottom source plus draw, and StartMainPhase play source card then delete this Tamer, remain blocked by hand-to-source placement, source selection/play continuation, ETB continuation, and self-delete sequencing.");

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

        throw new DomainException($"BT20-092 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
