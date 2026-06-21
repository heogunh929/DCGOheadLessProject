// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_093.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_093Script : ICardScript
{
    private const string StartMainMemoryStableId = "BT22-093:on-start-main:memory-plus-one";
    private const string SecurityStableId = "BT22-093:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-093",
        "BT22_093",
        CardEffectPortingStatus.PartiallyImplemented,
        "StartMainPhase memory +1 while opponent has a Digimon and SecuritySkill play-self Tamer are implemented. Your-turn suspend-cost hand digivolution into CS Digimon remains blocked by CS trait metadata, digivolution payload with same-level source predicate, suspend-cost continuation, and free hand digivolution support.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateStartMainMemoryDescriptor(context.SourceCard, context.SourcePermanent, controller),
            Bt22ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
                SecurityStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == StartMainMemoryStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt22ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                primitives.ModifyMemory(state, controller, 1);
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

        throw new DomainException($"BT22-093 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }

    private static EffectDescriptor CreateStartMainMemoryDescriptor(
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller) =>
        new(
            StartMainMemoryStableId,
            EffectTiming.OnStartMainPhase,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            CanTrigger: effectContext =>
                effectContext.State.TurnPlayerId == controller
                && Bt22ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && Bt22ScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0);
}
