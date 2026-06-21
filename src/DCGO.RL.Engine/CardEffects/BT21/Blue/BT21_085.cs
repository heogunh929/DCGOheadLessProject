// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_085.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_085Script : ICardScript
{
    private const string StartMainMemoryStableId = "BT21-085:on-start-main:memory-plus-one";
    private const string SecurityStableId = "BT21-085:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-085",
        "BT21_085",
        CardEffectPortingStatus.PartiallyImplemented,
        "StartMainPhase memory +1 while opponent has a Digimon and SecuritySkill play-self Tamer are implemented. Main suspend-cost, trash top stacked card from Armor Form Digimon, draw, and memory continuation remain blocked by suspend-cost continuation, trait metadata, top-stacked-card trash, and ETB/after-effect sequencing.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateStartMainMemoryDescriptor(context.SourceCard, context.SourcePermanent, controller),
            Bt20ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
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
                    ?? Bt21ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                primitives.ModifyMemory(state, controller, 1);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt21ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                Bt20ScriptSupport.PlaySelfTamerFromExecuting(
                    state,
                    primitives,
                    controller,
                    context.Resolution.SourceCard);
            });
            return;
        }

        throw new DomainException($"BT21-085 cannot resolve unknown effect '{context.Resolution.StableId}'.");
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
                && Bt21ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0);
}
