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

public sealed record RunnerSessionHandle(Guid RunnerId, Guid SessionId);

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
    string? PendingStableContinuationId = null,
    RunnerSessionHandle? PendingRunnerSession = null)
{
    public GameState FinalState { get; init; } = FinalState.Clone();

    public GameTrace Trace { get; init; } = new(Trace.Events.ToArray());

    public IReadOnlyList<EngineInvariantReport> InvariantReports { get; init; } = InvariantReports.ToArray();

    public EngineInvariantReport InvariantReport => InvariantReports.Count == 0
        ? new EngineInvariantReport(Array.Empty<EngineInvariantViolation>())
        : InvariantReports[^1];
}

public sealed class ScriptedScenarioRunnerSession
{
    internal ScriptedScenarioRunnerSession(
        Guid ownerRunnerId,
        ScriptedScenario scenario,
        GameState state,
        GameTrace trace,
        EngineSession engineSession)
    {
        OwnerRunnerId = ownerRunnerId;
        SessionId = Guid.NewGuid();
        Scenario = scenario;
        State = state;
        Trace = trace;
        EngineSession = engineSession;
    }

    public Guid SessionId { get; }

    public string ScenarioName => Scenario.Name;

    public int NextStepIndex { get; internal set; }

    public ScenarioResult Result { get; internal set; } = null!;

    public bool IsCompleted { get; internal set; }

    internal Guid OwnerRunnerId { get; }

    internal ScriptedScenario Scenario { get; }

    internal GameState State { get; }

    internal GameTrace Trace { get; }

    internal EngineSession EngineSession { get; }

    internal List<EngineInvariantReport> InvariantReports { get; } = new();

    internal int? PendingStepIndex { get; set; }

    internal EngineStepResult? PendingStepResult { get; set; }
}

public sealed class ScriptedScenarioRunner
{
    private readonly Guid _runnerId = Guid.NewGuid();
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
        var session = StartSession(scenario, requireProviderless: false);
        if (session.Result.Status == ScenarioRunStatus.PausedForDecision
            && _services?.HasRuntimeDecisionProvider != true)
        {
            throw new DomainException(
                "ScriptedScenarioRunner.Run cannot return a providerless paused decision because the resumable session would be lost; use StartSession(...) and Resume(..., DecisionResult) for external decisions.");
        }

        return session.Result;
    }

    public ScriptedScenarioRunnerSession StartSession(ScriptedScenario scenario)
    {
        return StartSession(scenario, requireProviderless: true);
    }

    private ScriptedScenarioRunnerSession StartSession(ScriptedScenario scenario, bool requireProviderless)
    {
        var state = scenario.InitialState.Clone();
        var trace = new GameTrace();
        var session = new ScriptedScenarioRunnerSession(
            _runnerId,
            scenario,
            state,
            trace,
            RequiredServices(requireProviderless).CreateSession(state, trace));

        trace.AddStateSnapshot($"scenario:{scenario.Name}:initial", state);
        AddInvariantReport(state, session.InvariantReports);
        session.Result = Continue(session);
        return session;
    }

    public ScenarioResult Resume(ScriptedScenarioRunnerSession session, DecisionResult result)
    {
        ValidateSession(session);
        if (session.IsCompleted)
        {
            throw new DomainException($"Scripted runner session '{session.SessionId}' is already completed.");
        }

        if (session.PendingStepResult is null || session.PendingStepIndex is null)
        {
            throw new DomainException($"Scripted runner session '{session.SessionId}' has no pending decision.");
        }

        var stepResult = session.EngineSession.Resume(result);
        AddInvariantReport(session.State, session.InvariantReports);
        if (stepResult.IsPaused)
        {
            return Pause(session, session.PendingStepIndex.Value, stepResult);
        }

        session.NextStepIndex = session.PendingStepIndex.Value + 1;
        session.PendingStepIndex = null;
        session.PendingStepResult = null;
        session.Result = Continue(session);
        return session.Result;
    }

    private ScenarioResult Continue(ScriptedScenarioRunnerSession session)
    {
        var scenario = session.Scenario;
        var state = session.State;
        var trace = session.Trace;

        for (var i = session.NextStepIndex; i < scenario.Steps.Count; i++)
        {
            if (state.IsGameOver)
            {
                break;
            }

            var stepResult = ExecuteStep(scenario.Name, i, scenario.Steps[i], state, trace, session.EngineSession);
            AddInvariantReport(state, session.InvariantReports);
            if (stepResult?.IsPaused == true)
            {
                return Pause(session, i, stepResult);
            }

            session.NextStepIndex = i + 1;
        }

        trace.AddStateSnapshot($"scenario:{scenario.Name}:final", state);
        session.IsCompleted = true;
        session.Result = new ScenarioResult(
            scenario.Name,
            state.IsGameOver ? ScenarioRunStatus.GameOver : ScenarioRunStatus.Completed,
            state,
            trace,
            session.InvariantReports,
            state.ComputeStateHash());
        return session.Result;
    }

    private ScenarioResult Pause(
        ScriptedScenarioRunnerSession session,
        int stepIndex,
        EngineStepResult stepResult)
    {
        session.PendingStepIndex = stepIndex;
        session.PendingStepResult = stepResult;
        session.Trace.AddStateSnapshot($"scenario:{session.Scenario.Name}:paused:{stepIndex}", session.State);
        session.Result = new ScenarioResult(
            session.Scenario.Name,
            ScenarioRunStatus.PausedForDecision,
            session.State,
            session.Trace,
            session.InvariantReports,
            session.State.ComputeStateHash(),
            PendingDecisionPoint: stepResult.PendingDecisionPoint,
            PendingDecisionToken: stepResult.PendingDecisionToken,
            PendingStableContinuationId: stepResult.PendingStableContinuationId,
            PendingRunnerSession: new RunnerSessionHandle(_runnerId, session.SessionId));
        return session.Result;
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

    private BattleEngineServices RequiredServices(bool requireProviderless)
    {
        var services = _services ?? throw new DomainException(
            "ScriptedScenarioRunner resumable sessions require a complete BattleEngineServices graph.");
        return requireProviderless ? RequireProviderlessServices(services) : services;
    }

    private static BattleEngineServices RequireProviderlessServices(BattleEngineServices services)
    {
        if (services.HasRuntimeDecisionProvider)
        {
            throw new DomainException(
                "ScriptedScenarioRunner resumable sessions require a providerless BattleEngineServices graph; submit selections through Resume(DecisionResult).");
        }

        return services;
    }

    private void ValidateSession(ScriptedScenarioRunnerSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (session.OwnerRunnerId != _runnerId)
        {
            throw new DomainException(
                $"Scripted runner session '{session.SessionId}' does not belong to this runner.");
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
