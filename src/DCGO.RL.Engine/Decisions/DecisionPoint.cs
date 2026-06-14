using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed record DecisionPoint(
    DecisionKind Kind,
    PlayerId Player,
    Phase Phase,
    string Reason,
    IReadOnlyList<LegalAction> LegalActions,
    SelectionRequest? SelectionRequest)
{
    public static DecisionPoint ForLegalActions(
        PlayerId player,
        Phase phase,
        string reason,
        IReadOnlyList<LegalAction> legalActions) =>
        new(DecisionKind.LegalAction, player, phase, reason, legalActions, null);

    public static DecisionPoint ForSelection(
        PlayerId player,
        Phase phase,
        string reason,
        SelectionRequest selectionRequest) =>
        new(DecisionKind.Selection, player, phase, reason, Array.Empty<LegalAction>(), selectionRequest);
}
