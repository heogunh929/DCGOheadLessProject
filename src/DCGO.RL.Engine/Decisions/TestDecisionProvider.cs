using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed class TestDecisionProvider : IDecisionProvider
{
    private readonly Queue<string> _legalActionIds = new();
    private readonly Queue<SelectionResult> _selectionResults = new();

    public void EnqueueActionId(string actionId) => _legalActionIds.Enqueue(actionId);

    public void EnqueueSelectionResult(SelectionResult result) => _selectionResults.Enqueue(result);

    public GameAction ChooseAction(DecisionPoint decisionPoint)
    {
        if (decisionPoint.Kind != DecisionKind.LegalAction)
        {
            throw new DomainException("DecisionPoint does not contain legal actions.");
        }

        if (decisionPoint.LegalActions.Count == 0)
        {
            throw new DomainException("DecisionPoint has no legal actions.");
        }

        if (_legalActionIds.Count == 0)
        {
            if (decisionPoint.LegalActions.Count == 1)
            {
                return decisionPoint.LegalActions[0].Action;
            }

            throw new DomainException("TestDecisionProvider requires an action id when multiple actions are legal.");
        }

        var actionId = _legalActionIds.Dequeue();
        var legalAction = decisionPoint.LegalActions.FirstOrDefault(action => string.Equals(action.Id, actionId, StringComparison.Ordinal));
        if (legalAction is null)
        {
            throw new DomainException($"Legal action '{actionId}' is not available.");
        }

        return legalAction.Action;
    }

    public SelectionResult ChooseSelection(SelectionRequest request)
    {
        if (_selectionResults.Count == 0)
        {
            throw new DomainException("TestDecisionProvider requires a queued SelectionResult.");
        }

        var result = _selectionResults.Dequeue();
        SelectionValidator.Validate(request, result);
        return result;
    }
}
