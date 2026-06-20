using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed record DecisionPoint(
    DecisionKind Kind,
    PlayerId Player,
    Phase Phase,
    string Reason,
    IReadOnlyList<LegalAction> LegalActions,
    SelectionRequest? SelectionRequest,
    DecisionToken? Token = null)
{
    public static DecisionPoint ForLegalActions(
        PlayerId player,
        Phase phase,
        string reason,
        IReadOnlyList<LegalAction> legalActions,
        DecisionToken? token = null) =>
        new(DecisionKind.LegalAction, player, phase, reason, legalActions, null, token);

    public static DecisionPoint ForSelection(
        PlayerId player,
        Phase phase,
        string reason,
        SelectionRequest selectionRequest,
        DecisionToken? token = null) =>
        new(DecisionKind.Selection, player, phase, reason, Array.Empty<LegalAction>(), selectionRequest, token);
}
