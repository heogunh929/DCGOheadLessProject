// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_080.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_080Script : ICardScript
{
    private const string OnPlayStableId = "BT18-080:on-play:delete-digimon-then-tamer";
    private const string WhenDigivolvingStableId = "BT18-080:when-digivolving:delete-digimon-then-tamer";
    private const string DeleteDigimonStage = "delete-digimon";
    private const string DeleteTamerStage = "delete-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT18-080",
        "BT18_080",
        CardEffectPortingStatus.Implemented,
        "OnPlay and WhenDigivolving delete one level 4 or lower red/green/purple/white opponent Digimon, then one blue/yellow/black/white opponent Tamer with play cost 3 or less. Inherited Retaliation is represented by CardDefinition.BattleKeywords.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt18ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            CreateDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT18-080 delete body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateDescriptor(
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
                Bt18ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && (DigimonCandidates(effectContext.State, controller).Count > 0
                    || TamerCandidates(effectContext.State, controller).Count > 0),
            CreateSelectionRequest: effectContext => CreateFirstRequest(
                stableId,
                effectContext.State,
                controller),
            SelectionContinuation: ApplySelection);

    private static SelectionRequest CreateFirstRequest(string stableId, GameState state, PlayerId controller)
    {
        var digimonCandidates = DigimonCandidates(state, controller);
        if (digimonCandidates.Count > 0)
        {
            return Bt18ScriptSupport.CreatePermanentSelectionRequest(
                RequestId(stableId, DeleteDigimonStage),
                controller,
                digimonCandidates,
                "Delete 1 opponent Digimon level 4 or lower that is red, green, purple, or white.");
        }

        return Bt18ScriptSupport.CreatePermanentSelectionRequest(
            RequestId(stableId, DeleteTamerStage),
            controller,
            TamerCandidates(state, controller),
            "Delete 1 opponent Tamer with play cost 3 or less that is blue, yellow, black, or white.");
    }

    private static IReadOnlyList<SelectableTarget> DigimonCandidates(GameState state, PlayerId controller) =>
        Bt18ScriptSupport.OpponentBattleAreaPermanentCandidates(
            state,
            controller,
            definition =>
                definition.CardKinds.Contains(CardKind.Digimon)
                && definition.Level <= 4
                && Bt18ScriptSupport.HasAnyColor(
                    definition,
                    CardColor.Red,
                    CardColor.Green,
                    CardColor.Purple,
                    CardColor.White));

    private static IReadOnlyList<SelectableTarget> TamerCandidates(GameState state, PlayerId controller) =>
        Bt18ScriptSupport.OpponentBattleAreaPermanentCandidates(
            state,
            controller,
            definition =>
                definition.CardKinds.Contains(CardKind.Tamer)
                && definition.PlayCost <= 3
                && Bt18ScriptSupport.HasAnyColor(
                    definition,
                    CardColor.Blue,
                    CardColor.Yellow,
                    CardColor.Black,
                    CardColor.White));

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }

        if (context.Request.Id.EndsWith($":{DeleteDigimonStage}", StringComparison.Ordinal))
        {
            var controller = context.Resolution.Controller
                ?? Bt18ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
            var tamerCandidates = TamerCandidates(context.State, controller);
            if (tamerCandidates.Count == 0)
            {
                return;
            }

            var request = Bt18ScriptSupport.CreatePermanentSelectionRequest(
                RequestId(context.Resolution.StableId, DeleteTamerStage),
                controller,
                tamerCandidates,
                "Delete 1 opponent Tamer with play cost 3 or less that is blue, yellow, black, or white.");

            context.SetNextResolution(context.Resolution with
            {
                StableId = $"{context.Resolution.StableId}:then-tamer",
                SelectionRequest = request,
                SelectionContinuation = ApplySelection,
            });
        }
    }

    private static string RequestId(string stableId, string stage) => $"{stableId}:{stage}";
}
