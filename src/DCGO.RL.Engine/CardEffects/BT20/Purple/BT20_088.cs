// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_088.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_088Script : ICardScript
{
    private const string StartMainMemoryStableId = "BT20-088:on-start-main:memory-plus-one";
    private const string SecurityStableId = "BT20-088:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-088",
        "BT20_088",
        CardEffectPortingStatus.PartiallyImplemented,
        "StartMainPhase memory +1 while opponent has a Digimon and SecuritySkill play-self Tamer are implemented. Owner-turn OnDeletion suspend-cost Ghost hand digivolution remains blocked by trait metadata, suspend-cost continuation, hand digivolve cost reduction, and deleted-source trigger payload coverage.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                StartMainMemoryStableId,
                EffectTiming.OnStartMainPhase,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.State.TurnPlayerId == controller
                    && Bt20ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0),
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
                    ?? Bt20ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                primitives.ModifyMemory(state, controller, 1);
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

        throw new DomainException($"BT20-088 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
