using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Infrastructure;

namespace DCGO.RL.Engine.Validation;

public sealed record RandomLegalActionRunRequest(
    string Name,
    GameState InitialState,
    int MaxActions,
    ulong Seed);

public enum RandomLegalActionPauseLocation
{
    None,
    PhaseNormalization,
    Action,
    AfterBreedingActionMainPhase,
}

public sealed class RandomLegalActionRunnerSession
{
    internal RandomLegalActionRunnerSession(
        Guid ownerRunnerId,
        RandomLegalActionRunRequest request,
        GameState state,
        GameTrace trace,
        EngineSession engineSession,
        XorShiftDeterministicRng rng)
    {
        OwnerRunnerId = ownerRunnerId;
        SessionId = Guid.NewGuid();
        Request = request;
        State = state;
        Trace = trace;
        EngineSession = engineSession;
        Rng = rng;
    }

    public Guid SessionId { get; }

    public string Name => Request.Name;

    public int ActionsExecuted { get; internal set; }

    public ScenarioResult Result { get; internal set; } = null!;

    public bool IsCompleted { get; internal set; }

    internal Guid OwnerRunnerId { get; }

    internal RandomLegalActionRunRequest Request { get; }

    internal GameState State { get; }

    internal GameTrace Trace { get; }

    internal EngineSession EngineSession { get; }

    internal XorShiftDeterministicRng Rng { get; }

    internal List<EngineInvariantReport> InvariantReports { get; } = new();

    internal RandomLegalActionPauseLocation PauseLocation { get; set; }

    internal EngineStepResult? PendingStepResult { get; set; }
}

public sealed class RandomLegalActionRunner
{
    private readonly Guid _runnerId = Guid.NewGuid();
    private readonly LegalActionGenerator _legalActionGenerator;
    private readonly BattleEngineServices? _services;
    private readonly ActionExecutor _actionExecutor;
    private readonly PhaseRunner _phaseRunner;
    private readonly TurnRunner _turnRunner;
    private readonly EngineInvariantChecker _invariantChecker;

    public RandomLegalActionRunner(
        BattleEngineServices? services = null,
        LegalActionGenerator? legalActionGenerator = null,
        ActionExecutor? actionExecutor = null,
        PhaseRunner? phaseRunner = null,
        TurnRunner? turnRunner = null,
        EngineInvariantChecker? invariantChecker = null)
    {
        _services = services;
        _legalActionGenerator = legalActionGenerator ?? services?.LegalActionGenerator ?? new LegalActionGenerator();
        _actionExecutor = actionExecutor
            ?? services?.ActionExecutor
            ?? throw new DomainException("RandomLegalActionRunner requires an ActionExecutor from BattleEngineServices.");
        _phaseRunner = phaseRunner
            ?? services?.PhaseRunner
            ?? throw new DomainException("RandomLegalActionRunner requires a PhaseRunner from BattleEngineServices.");
        _turnRunner = turnRunner
            ?? services?.TurnRunner
            ?? throw new DomainException("RandomLegalActionRunner requires a TurnRunner from BattleEngineServices.");
        _invariantChecker = invariantChecker ?? services?.InvariantChecker ?? new EngineInvariantChecker();
    }

    public ScenarioResult Run(RandomLegalActionRunRequest request)
    {
        var session = StartSession(request, requireProviderless: false);
        if (session.Result.Status == ScenarioRunStatus.PausedForDecision)
        {
            throw new DomainException(
                "RandomLegalActionRunner.Run cannot return a paused decision because the resumable session would be lost; use StartSession(...) and Resume(..., DecisionResult) for external decisions.");
        }

        return session.Result;
    }

    public RandomLegalActionRunnerSession StartSession(RandomLegalActionRunRequest request)
    {
        return StartSession(request, requireProviderless: true);
    }

    private RandomLegalActionRunnerSession StartSession(RandomLegalActionRunRequest request, bool requireProviderless)
    {
        if (request.MaxActions < 1)
        {
            throw new DomainException("MaxActions must be positive.");
        }

        var state = request.InitialState.Clone();
        var trace = new GameTrace();
        var runnerSession = new RandomLegalActionRunnerSession(
            _runnerId,
            request,
            state,
            trace,
            RequiredServices(requireProviderless).CreateSession(state, trace),
            new XorShiftDeterministicRng(request.Seed));

        trace.AddStateSnapshot($"random:{request.Name}:initial", state);
        AddInvariantReport(state, runnerSession.InvariantReports);
        runnerSession.Result = Continue(runnerSession);
        return runnerSession;
    }

    public ScenarioResult Resume(RandomLegalActionRunnerSession session, DecisionResult result)
    {
        ValidateSession(session);
        if (session.IsCompleted)
        {
            throw new DomainException($"Random runner session '{session.SessionId}' is already completed.");
        }

        if (session.PendingStepResult is null || session.PauseLocation == RandomLegalActionPauseLocation.None)
        {
            throw new DomainException($"Random runner session '{session.SessionId}' has no pending decision.");
        }

        var pauseLocation = session.PauseLocation;
        var stepResult = session.EngineSession.Resume(result);
        AddInvariantReport(session.State, session.InvariantReports);
        if (stepResult.IsPaused)
        {
            return Pause(session, pauseLocation, stepResult);
        }

        session.PendingStepResult = null;
        session.PauseLocation = RandomLegalActionPauseLocation.None;
        if (pauseLocation == RandomLegalActionPauseLocation.Action)
        {
            session.ActionsExecuted++;
            if (session.State.Phase == Phase.Breeding && !session.State.IsGameOver)
            {
                var mainPhaseResult = session.EngineSession.RunMainPhase();
                if (mainPhaseResult.IsPaused)
                {
                    AddInvariantReport(session.State, session.InvariantReports);
                    return Pause(
                        session,
                        RandomLegalActionPauseLocation.AfterBreedingActionMainPhase,
                        mainPhaseResult);
                }

                AddInvariantReport(session.State, session.InvariantReports);
            }
        }

        session.Result = Continue(session);
        return session.Result;
    }

    private ScenarioResult Continue(RandomLegalActionRunnerSession session)
    {
        var state = session.State;
        var trace = session.Trace;
        var request = session.Request;

        while (!state.IsGameOver)
        {
            var normalizeResult = NormalizeToActionPhase(session);
            if (normalizeResult?.IsPaused == true)
            {
                return Pause(session, RandomLegalActionPauseLocation.PhaseNormalization, normalizeResult);
            }

            if (state.IsGameOver)
            {
                break;
            }

            var legalActions = _legalActionGenerator.Generate(state, state.TurnPlayerId);
            if (legalActions.Count == 0)
            {
                break;
            }

            if (session.ActionsExecuted >= request.MaxActions)
            {
                var abort = new MaxTurnAbortResult(
                    request.MaxActions,
                    session.ActionsExecuted,
                    state.ComputeStateHash());
                trace.AddStateSnapshot($"random:{request.Name}:max-action-abort", state);
                session.IsCompleted = true;
                session.Result = new ScenarioResult(
                    request.Name,
                    ScenarioRunStatus.MaxTurnAbort,
                    state,
                    trace,
                    session.InvariantReports,
                    state.ComputeStateHash(),
                    abort);
                return session.Result;
            }

            var action = legalActions[session.Rng.NextInt(legalActions.Count)].Action;
            var actionResult = session.EngineSession.Step(action);
            if (actionResult.IsPaused)
            {
                AddInvariantReport(state, session.InvariantReports);
                return Pause(session, RandomLegalActionPauseLocation.Action, actionResult);
            }

            session.ActionsExecuted++;
            AddInvariantReport(state, session.InvariantReports);

            if (state.Phase == Phase.Breeding && !state.IsGameOver)
            {
                var mainPhaseResult = session.EngineSession.RunMainPhase();
                if (mainPhaseResult.IsPaused)
                {
                    AddInvariantReport(state, session.InvariantReports);
                    return Pause(
                        session,
                        RandomLegalActionPauseLocation.AfterBreedingActionMainPhase,
                        mainPhaseResult);
                }

                AddInvariantReport(state, session.InvariantReports);
            }
        }

        trace.AddStateSnapshot($"random:{request.Name}:final", state);
        session.IsCompleted = true;
        session.Result = new ScenarioResult(
            request.Name,
            state.IsGameOver ? ScenarioRunStatus.GameOver : ScenarioRunStatus.Completed,
            state,
            trace,
            session.InvariantReports,
            state.ComputeStateHash());
        return session.Result;
    }

    private EngineStepResult? NormalizeToActionPhase(
        RandomLegalActionRunnerSession session)
    {
        var state = session.State;
        var trace = session.Trace;
        while (!state.IsGameOver)
        {
            switch (state.Phase)
            {
                case Phase.None:
                case Phase.End:
                    var runToMainResult = session.EngineSession.RunToMainPhase();
                    if (runToMainResult.IsPaused)
                    {
                        AddInvariantReport(state, session.InvariantReports);
                        return runToMainResult;
                    }

                    AddInvariantReport(state, session.InvariantReports);
                    continue;

                case Phase.Active:
                    _phaseRunner.RunDrawPhase(state);
                    if (!state.IsGameOver)
                    {
                        _phaseRunner.RunBreedingPhase(state);
                    }

                    trace.AddStateSnapshot("random:active-to-breeding", state);
                    AddInvariantReport(state, session.InvariantReports);
                    continue;

                case Phase.Draw:
                    _phaseRunner.RunBreedingPhase(state);
                    trace.AddStateSnapshot("random:draw-to-breeding", state);
                    AddInvariantReport(state, session.InvariantReports);
                    continue;

                case Phase.Breeding:
                    if (_legalActionGenerator.Generate(state, state.TurnPlayerId).Count == 0)
                    {
                        var mainPhaseResult = session.EngineSession.RunMainPhase();
                        if (mainPhaseResult.IsPaused)
                        {
                            AddInvariantReport(state, session.InvariantReports);
                            return mainPhaseResult;
                        }

                        AddInvariantReport(state, session.InvariantReports);
                        continue;
                    }

                    return null;

                case Phase.Main:
                    return null;

                default:
                    throw new UnsupportedMechanicException($"Random runner phase '{state.Phase}'");
            }
        }

        return null;
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
            $"RandomLegalActionRunner cannot ignore pending SelectionRequest '{result.PendingSelectionRequest!.Id}' at {context}.");
    }

    private ScenarioResult Pause(
        RandomLegalActionRunnerSession session,
        RandomLegalActionPauseLocation location,
        EngineStepResult stepResult)
    {
        session.PauseLocation = location;
        session.PendingStepResult = stepResult;
        session.Trace.AddStateSnapshot($"random:{session.Request.Name}:paused", session.State);
        session.Result = new ScenarioResult(
            session.Request.Name,
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

    private BattleEngineServices RequiredServices(bool requireProviderless)
    {
        var services = _services ?? throw new DomainException(
            "RandomLegalActionRunner resumable sessions require a complete BattleEngineServices graph.");
        return requireProviderless ? RequireProviderlessServices(services) : services;
    }

    private static BattleEngineServices RequireProviderlessServices(BattleEngineServices services)
    {
        if (services.HasRuntimeDecisionProvider)
        {
            throw new DomainException(
                "RandomLegalActionRunner resumable sessions require a providerless BattleEngineServices graph; submit selections through Resume(DecisionResult).");
        }

        return services;
    }

    private void ValidateSession(RandomLegalActionRunnerSession session)
    {
        ArgumentNullException.ThrowIfNull(session);
        if (session.OwnerRunnerId != _runnerId)
        {
            throw new DomainException(
                $"Random runner session '{session.SessionId}' does not belong to this runner.");
        }
    }
}
