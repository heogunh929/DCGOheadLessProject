using DCGO.RL.Engine.Actions;

namespace DCGO.RL.Engine.Decisions;

public interface IDecisionProvider
{
    GameAction ChooseAction(DecisionPoint decisionPoint);

    SelectionResult ChooseSelection(SelectionRequest request);
}
