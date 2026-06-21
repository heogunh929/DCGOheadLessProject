// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_096.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_096Script : ICardScript
{
    private const string OptionStableId = "BT20-096:option:discard-then-delete-level-four";
    private const string SecurityStableId = "BT20-096:security:delete-level-six";
    private const string OptionDiscardRequestId = "BT20-096:option:discard-hand";
    private const string OptionDeleteRequestId = "BT20-096:option:delete-level-four";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-096",
        "BT20_096",
        CardEffectPortingStatus.PartiallyImplemented,
        "OptionSkill hand discard then delete one opponent level 4-or-lower Digimon and SecuritySkill delete one opponent level 6-or-lower Digimon are implemented. Trash [Main] activation from trash with 6 memory payment, return-self-to-deck-bottom, and unsuspended target deletion remains blocked by OnDeclaration trash activation/cost and return-to-library-bottom continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OptionStableId,
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    HandDiscardCandidates(effectContext.State, controller).Count > 0
                    || LevelDeleteCandidates(effectContext.State, controller, maxLevel: 4).Count > 0,
                CreateSelectionRequest: effectContext => CreateOptionFirstRequest(effectContext.State, controller),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                SecurityStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => LevelDeleteCandidates(effectContext.State, controller, maxLevel: 6).Count > 0,
                CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                    $"{SecurityStableId}:target",
                    controller,
                    LevelDeleteCandidates(effectContext.State, controller, maxLevel: 6),
                    "Delete 1 opponent Digimon level 6 or lower."),
                SelectionContinuation: ApplyDeleteSelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-096 target body must be resolved through SelectionResultApplicator.");

    private static SelectionRequest CreateOptionFirstRequest(GameState state, PlayerId controller)
    {
        var hand = HandDiscardCandidates(state, controller);
        if (hand.Count > 0)
        {
            return Bt20ScriptSupport.CreateCardSelectionRequest(
                state,
                OptionDiscardRequestId,
                controller,
                SelectionTargetKind.Card,
                hand,
                Zone.Hand,
                canSkip: false,
                "Trash 1 card in your hand.");
        }

        return Bt20ScriptSupport.CreatePermanentSelectionRequest(
            OptionDeleteRequestId,
            controller,
            LevelDeleteCandidates(state, controller, maxLevel: 4),
            "Delete 1 opponent Digimon level 4 or lower.");
    }

    private static IReadOnlyList<CardInstanceId> HandDiscardCandidates(GameState state, PlayerId controller) =>
        state.GetPlayer(controller).Hand.ToArray();

    private static IReadOnlyList<SelectableTarget> LevelDeleteCandidates(
        GameState state,
        PlayerId controller,
        int maxLevel) =>
        Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (_, definition) => definition.Level > 0 && definition.Level <= maxLevel);

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);

        if (context.Request.Id == OptionDiscardRequestId)
        {
            context.Primitives.DiscardHandWithEvents(
                context.State,
                controller,
                context.SelectedCardIds,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                context.Trace);

            var deleteCandidates = LevelDeleteCandidates(context.State, controller, maxLevel: 4);
            if (deleteCandidates.Count == 0)
            {
                return;
            }

            var request = Bt20ScriptSupport.CreatePermanentSelectionRequest(
                OptionDeleteRequestId,
                controller,
                deleteCandidates,
                "Delete 1 opponent Digimon level 4 or lower.");
            context.SetNextResolution(context.Resolution with
            {
                StableId = $"{OptionStableId}:delete",
                SelectionRequest = request,
                SelectionContinuation = ApplyOptionSelection,
            });
            return;
        }

        ApplyDeleteSelection(context);
    }

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
