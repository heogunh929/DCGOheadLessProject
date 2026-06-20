using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public abstract record ScriptedScenarioStep;

public sealed record ActionScenarioStep(GameAction Action) : ScriptedScenarioStep;

public sealed record DrawPhaseScenarioStep : ScriptedScenarioStep;

public sealed record RunToMainPhaseScenarioStep : ScriptedScenarioStep;

public sealed record ScriptedScenario
{
    public ScriptedScenario(string name, GameState initialState, IReadOnlyList<GameAction> actions)
        : this(name, initialState, actions.Select(action => new ActionScenarioStep(action)).Cast<ScriptedScenarioStep>().ToArray())
    {
    }

    public ScriptedScenario(string name, GameState initialState, IReadOnlyList<ScriptedScenarioStep> steps)
    {
        Name = name;
        InitialState = initialState;
        Steps = steps.ToArray();
    }

    public string Name { get; }
    public GameState InitialState { get; }
    public IReadOnlyList<ScriptedScenarioStep> Steps { get; }
}

public enum ScenarioRunStatus
{
    Completed,
    GameOver,
    MaxTurnAbort,
    PausedForDecision,
}

public sealed record MaxTurnAbortResult(int MaxActions, int ActionsExecuted, string FinalStateHash);

public sealed record ScenarioResult(
    string ScenarioName,
    ScenarioRunStatus Status,
    GameState FinalState,
    GameTrace Trace,
    IReadOnlyList<EngineInvariantReport> InvariantReports,
    string FinalStateHash,
    MaxTurnAbortResult? MaxTurnAbort = null,
    DecisionPoint? PendingDecisionPoint = null,
    DecisionToken? PendingDecisionToken = null,
    string? PendingStableContinuationId = null)
{
    public EngineInvariantReport InvariantReport => InvariantReports.Count == 0
        ? new EngineInvariantReport(Array.Empty<EngineInvariantViolation>())
        : InvariantReports[^1];
}

public sealed class ScriptedScenarioRunner
{
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly BattleEngineServices? _services;
    private readonly ActionExecutor _actionExecutor;
    private readonly PhaseRunner _phaseRunner;
    private readonly TurnRunner _turnRunner;

    public ScriptedScenarioRunner(
        BattleEngineServices? services = null,
        EngineInvariantChecker? invariantChecker = null,
        ActionExecutor? actionExecutor = null,
        PhaseRunner? phaseRunner = null,
        TurnRunner? turnRunner = null)
    {
        _services = services;
        _invariantChecker = invariantChecker ?? services?.InvariantChecker ?? new EngineInvariantChecker();
        _actionExecutor = actionExecutor
            ?? services?.ActionExecutor
            ?? throw new DomainException("ScriptedScenarioRunner requires an ActionExecutor from BattleEngineServices.");
        _phaseRunner = phaseRunner
            ?? services?.PhaseRunner
            ?? throw new DomainException("ScriptedScenarioRunner requires a PhaseRunner from BattleEngineServices.");
        _turnRunner = turnRunner
            ?? services?.TurnRunner
            ?? throw new DomainException("ScriptedScenarioRunner requires a TurnRunner from BattleEngineServices.");
    }

    public ScenarioResult Run(ScriptedScenario scenario)
    {
        var state = scenario.InitialState.Clone();
        var trace = new GameTrace();
        var session = _services?.CreateSession(state, trace);
        var invariantReports = new List<EngineInvariantReport>();

        trace.AddStateSnapshot($"scenario:{scenario.Name}:initial", state);
        AddInvariantReport(state, invariantReports);

        for (var i = 0; i < scenario.Steps.Count; i++)
        {
            if (state.IsGameOver)
            {
                break;
            }

            var stepResult = ExecuteStep(scenario.Name, i, scenario.Steps[i], state, trace, session);
            AddInvariantReport(state, invariantReports);
            if (stepResult?.IsPaused == true)
            {
                trace.AddStateSnapshot($"scenario:{scenario.Name}:paused:{i}", state);
                return new ScenarioResult(
                    scenario.Name,
                    ScenarioRunStatus.PausedForDecision,
                    state,
                    trace,
                    invariantReports,
                    state.ComputeStateHash(),
                    PendingDecisionPoint: stepResult.PendingDecisionPoint,
                    PendingDecisionToken: stepResult.PendingDecisionToken,
                    PendingStableContinuationId: stepResult.PendingStableContinuationId);
            }
        }

        trace.AddStateSnapshot($"scenario:{scenario.Name}:final", state);
        return new ScenarioResult(
            scenario.Name,
            state.IsGameOver ? ScenarioRunStatus.GameOver : ScenarioRunStatus.Completed,
            state,
            trace,
            invariantReports,
            state.ComputeStateHash());
    }

    private EngineStepResult? ExecuteStep(
        string scenarioName,
        int stepIndex,
        ScriptedScenarioStep step,
        GameState state,
        GameTrace trace,
        EngineSession? session)
    {
        switch (step)
        {
            case ActionScenarioStep actionStep:
                if (session is not null)
                {
                    return session.Step(actionStep.Action);
                }

                ThrowIfPendingSelection(
                    _actionExecutor.Execute(state, actionStep.Action, trace),
                    $"scenario:{scenarioName}:action:{stepIndex}");
                return null;

            case DrawPhaseScenarioStep:
                _phaseRunner.RunDrawPhase(state);
                trace.AddStateSnapshot($"scenario:{scenarioName}:draw-phase:{stepIndex}", state);
                return null;

            case RunToMainPhaseScenarioStep:
                if (session is not null)
                {
                    return session.RunToMainPhase();
                }

                _turnRunner.RunToMainPhase(state);
                trace.AddStateSnapshot($"scenario:{scenarioName}:main-phase:{stepIndex}", state);
                return null;

            default:
                throw new UnsupportedMechanicException($"Scripted scenario step '{step.GetType().Name}'");
        }
    }

    private void AddInvariantReport(GameState state, List<EngineInvariantReport> invariantReports)
    {
        var report = _invariantChecker.Check(state);
        report.ThrowIfInvalid();
        invariantReports.Add(report);
    }

    private static void ThrowIfPendingSelection(ActionExecutionResult result, string context)
    {
        if (!result.HasPendingSelection)
        {
            return;
        }

        throw new DomainException(
            $"ScriptedScenarioRunner cannot ignore pending SelectionRequest '{result.PendingSelectionRequest!.Id}' at {context}.");
    }
}
