// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_015.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_015Script : ICardScript, IContinuousCardScript
{
    private const string OnPlayDeleteStableId = "BT21-015:on-play:delete-dp-4000";
    private const string WhenDigivolvingDeleteStableId = "BT21-015:when-digivolving:delete-dp-4000";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-015",
        "BT21_015",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay/WhenDigivolving delete one opponent Digimon with 4000 DP or less and inherited owner-turn DP +2000 are implemented. SecuritySkill play-self Digimon after battle remains blocked by source-aligned security after-battle play continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateDeleteDescriptor(
                OnPlayDeleteStableId,
                EffectTiming.OnPlay,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            CreateDeleteDescriptor(
                WhenDigivolvingDeleteStableId,
                EffectTiming.WhenDigivolving,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { } target
            && target.Id == context.SourcePermanent
            && target.SourceCardIds.Contains(context.SourceCard)
            && evaluation.State.TurnPlayerId == context.Controller;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT21-015:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT21-015 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-015 delete target body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateDeleteDescriptor(
        string stableId,
        EffectTiming timing,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller) =>
        new(
            stableId,
            timing,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            CanTrigger: effectContext =>
                Bt21ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && DeleteCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                DeleteCandidates(effectContext.State, controller),
                "Delete 1 opponent Digimon with 4000 DP or less."),
            SelectionContinuation: ApplyDeleteSelection);

    private static IReadOnlyList<SelectableTarget> DeleteCandidates(GameState state, PlayerId controller) =>
        Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (permanent, _) => BattleRules.Dp(state, permanent) <= 4000);

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
