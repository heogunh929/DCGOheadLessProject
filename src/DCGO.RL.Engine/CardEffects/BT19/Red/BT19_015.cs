// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_015.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_015Script : ICardScript
{
    private const string WhenDigivolvingStableId = "BT19-015:when-digivolving:delete-dp-8000";
    private const string MemoryStableId = "BT19-015:on-destroyed-opponent:memory-plus-2";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-015",
        "BT19_015",
        CardEffectPortingStatus.PartiallyImplemented,
        "WhenDigivolving DP 8000-or-less deletion target and owner-turn once-per-turn memory +2 on opponent deletion are implemented. The fallback when deletion does not occur grants DP +3000 and Piercing until opponent turn end, but temporary keyword grants are not implemented yet.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt19ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt19ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && DeleteCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt19ScriptSupport.CreatePermanentSelectionRequest(
                    $"{WhenDigivolvingStableId}:target",
                    controller,
                    DeleteCandidates(effectContext.State, controller),
                    "Delete 1 opponent Digimon with 8000 DP or less."),
                SelectionContinuation: ApplyDeleteSelection),
            new EffectDescriptor(
                MemoryStableId,
                EffectTiming.OnDestroyedAnyone,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.State.TurnPlayerId == controller
                    && Bt19ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && Bt19ScriptSupport.IsOpponentDeletion(effectContext, controller),
                IsOncePerTurn: true,
                OncePerTurnKey: "BT19-015:on-destroyed-opponent:memory-plus-2"),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != MemoryStableId)
        {
            throw new DomainException("BT19-015 delete target body must be resolved through SelectionResultApplicator.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt19ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.ModifyMemory(state, controller, 2);
        });
    }

    private static IReadOnlyList<SelectableTarget> DeleteCandidates(GameState state, PlayerId controller) =>
        Bt19ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (permanent, _) => BattleRules.Dp(state, permanent) <= 8000);

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
