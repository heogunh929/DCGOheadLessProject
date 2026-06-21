// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_084.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_084Script : ICardScript
{
    private const string StartMainMemoryStableId = "BT19-084:on-start-main:memory-plus-one";
    private const string SecurityPlaySelfStableId = "BT19-084:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-084",
        "BT19_084",
        CardEffectPortingStatus.PartiallyImplemented,
        "StartMainPhase memory +1 while owner has face-up security and SecuritySkill play-self Tamer are implemented. Main suspend-cost face-up security digivolution and optional Royal Base hand-to-security placement remain blocked by suspend-cost digivolve, trait metadata, and face-up security placement layers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt19ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

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
                    && Bt19ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && HasFaceUpSecurity(effectContext.State, controller)),
            new EffectDescriptor(
                SecurityPlaySelfStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                    effectContext.State,
                    context.SourceCard,
                    controller)),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == StartMainMemoryStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt19ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                primitives.ModifyMemory(state, controller, 1);
            });
            return;
        }

        if (context.Resolution.StableId == SecurityPlaySelfStableId)
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? Bt19ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                var sourceCard = context.Resolution.SourceCard
                    ?? throw new DomainException("BT19-084 security play-self requires a source card.");
                var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
                primitives.PlayWithoutPayingCost(
                    state,
                    controller,
                    sourceCard,
                    Zone.Executing,
                    frame,
                    suspended: false);
            });
            return;
        }

        throw new DomainException($"BT19-084 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }

    private static bool HasFaceUpSecurity(GameState state, PlayerId controller) =>
        state.GetPlayer(controller)
            .Security
            .Any(card => state.Cards.TryGetValue(card, out var instance) && instance.IsFaceUp);
}
