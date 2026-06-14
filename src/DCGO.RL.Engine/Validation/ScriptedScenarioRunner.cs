using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
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
}

public sealed record MaxTurnAbortResult(int MaxActions, int ActionsExecuted, string FinalStateHash);

public sealed record ScenarioResult(
    string ScenarioName,
    ScenarioRunStatus Status,
    GameState FinalState,
    GameTrace Trace,
    IReadOnlyList<EngineInvariantReport> InvariantReports,
    string FinalStateHash,
    MaxTurnAbortResult? MaxTurnAbort = null)
{
    public EngineInvariantReport InvariantReport => InvariantReports.Count == 0
        ? new EngineInvariantReport(Array.Empty<EngineInvariantViolation>())
        : InvariantReports[^1];
}

public sealed class ScriptedScenarioRunner
{
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly ActionExecutor _actionExecutor;
    private readonly PhaseRunner _phaseRunner;
    private readonly TurnRunner _turnRunner;

    public ScriptedScenarioRunner(
        EngineInvariantChecker? invariantChecker = null,
        ActionExecutor? actionExecutor = null,
        PhaseRunner? phaseRunner = null,
        TurnRunner? turnRunner = null)
    {
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _actionExecutor = actionExecutor ?? new ActionExecutor();
        _phaseRunner = phaseRunner ?? new PhaseRunner();
        _turnRunner = turnRunner ?? new TurnRunner(_phaseRunner);
    }

    public ScenarioResult Run(ScriptedScenario scenario)
    {
        var state = scenario.InitialState.Clone();
        var trace = new GameTrace();
        var invariantReports = new List<EngineInvariantReport>();

        trace.AddStateSnapshot($"scenario:{scenario.Name}:initial", state);
        AddInvariantReport(state, invariantReports);

        for (var i = 0; i < scenario.Steps.Count; i++)
        {
            if (state.IsGameOver)
            {
                break;
            }

            ExecuteStep(scenario.Name, i, scenario.Steps[i], state, trace);
            AddInvariantReport(state, invariantReports);
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

    private void ExecuteStep(string scenarioName, int stepIndex, ScriptedScenarioStep step, GameState state, GameTrace trace)
    {
        switch (step)
        {
            case ActionScenarioStep actionStep:
                _actionExecutor.Execute(state, actionStep.Action, trace);
                break;

            case DrawPhaseScenarioStep:
                _phaseRunner.RunDrawPhase(state);
                trace.AddStateSnapshot($"scenario:{scenarioName}:draw-phase:{stepIndex}", state);
                break;

            case RunToMainPhaseScenarioStep:
                _turnRunner.RunToMainPhase(state);
                trace.AddStateSnapshot($"scenario:{scenarioName}:main-phase:{stepIndex}", state);
                break;

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
}
