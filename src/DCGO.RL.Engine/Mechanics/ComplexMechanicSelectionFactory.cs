using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Mechanics;

public sealed class ComplexMechanicSelectionFactory
{
    private readonly ComplexMechanicMatcher _matcher;

    public ComplexMechanicSelectionFactory(ComplexMechanicMatcher? matcher = null)
    {
        _matcher = matcher ?? new ComplexMechanicMatcher();
    }

    public SelectionRequest CreateJogressSourceSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<PermanentState> candidates) =>
        CreatePermanentSelection(state, player, $"jogress:{card.Value}", candidates, 2, "Select jogress source permanents.");

    public SelectionRequest CreateBurstTamerSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<PermanentState> candidates) =>
        CreatePermanentSelection(state, player, $"burst:{card.Value}", candidates, 1, "Select burst tamer.");

    public SelectionRequest CreateAppFusionLinkCardSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<CardInstanceId> linkedCards) =>
        CreateCardSelection(state, player, $"app-fusion:{card.Value}", linkedCards, 1, "Select app fusion link card.");

    public SelectionRequest CreateDigiXrosMaterialSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<MaterialCandidate> candidates, int maxCount) =>
        CreateMaterialSelection(state, player, $"digixros:{card.Value}", candidates, 0, maxCount, canSkip: true, "Select DigiXros materials.");

    public SelectionRequest CreateAssemblyMaterialSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<MaterialCandidate> candidates, int count) =>
        CreateMaterialSelection(state, player, $"assembly:{card.Value}", candidates, count, count, canSkip: false, "Select Assembly materials.");

    public SelectionRequest CreateLinkSelection(GameState state, PlayerId player, CardInstanceId card, IReadOnlyList<PermanentState> candidates) =>
        CreatePermanentSelection(state, player, $"link:{card.Value}", candidates, 1, "Select link target.");

    private static SelectionRequest CreatePermanentSelection(
        GameState state,
        PlayerId player,
        string id,
        IReadOnlyList<PermanentState> candidates,
        int count,
        string prompt) =>
        new(
            id,
            player,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            count,
            count,
            canSkip: false,
            canEndNotMax: false,
            candidates.Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: Battle.BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea)).ToArray(),
            prompt);

    private static SelectionRequest CreateCardSelection(
        GameState state,
        PlayerId player,
        string id,
        IReadOnlyList<CardInstanceId> cards,
        int count,
        string prompt) =>
        new(
            id,
            player,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            count,
            count,
            canSkip: false,
            canEndNotMax: false,
            cards.Select(card => new SelectableTarget(
                SelectionTargetKind.Card,
                $"card:{card.Value}",
                state.Cards[card].Owner,
                Card: card,
                Label: Battle.BattleRules.Definition(state, card).CardId,
                Zone: state.Cards[card].CurrentZone)).ToArray(),
            prompt);

    private static SelectionRequest CreateMaterialSelection(
        GameState state,
        PlayerId player,
        string id,
        IReadOnlyList<MaterialCandidate> candidates,
        int minCount,
        int maxCount,
        bool canSkip,
        string prompt) =>
        new(
            id,
            player,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            minCount,
            maxCount,
            canSkip,
            canEndNotMax: canSkip,
            candidates.Select(candidate => new SelectableTarget(
                SelectionTargetKind.Card,
                $"card:{candidate.Card.Value}",
                candidate.Owner,
                Card: candidate.Card,
                Permanent: candidate.Permanent,
                Label: $"{candidate.Label} [{candidate.Zone}]",
                Zone: candidate.Zone)).ToArray(),
            prompt);
}
