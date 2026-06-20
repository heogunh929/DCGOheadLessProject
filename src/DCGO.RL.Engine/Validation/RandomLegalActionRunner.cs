using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Infrastructure;

namespace DCGO.RL.Engine.Validation;

public sealed record RandomLegalActionRunRequest(
    string Name,
    GameState InitialState,
    int MaxActions,
    ulong Seed);

public sealed class RandomLegalActionRunner
{
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
        _legalActionGenerator = legalActionGenerator ?? new LegalActionGenerator();
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
        if (request.MaxActions < 1)
        {
            throw new DomainException("MaxActions must be positive.");
        }

        var state = request.InitialState.Clone();
        var trace = new GameTrace();
        var session = _services?.CreateSession(state, trace);
        var invariantReports = new List<EngineInvariantReport>();
        var rng = new XorShiftDeterministicRng(request.Seed);
        var actionsExecuted = 0;

        trace.AddStateSnapshot($"random:{request.Name}:initial", state);
        AddInvariantReport(state, invariantReports);

        while (!state.IsGameOver)
        {
            var normalizeResult = NormalizeToActionPhase(state, trace, invariantReports, session);
            if (normalizeResult?.IsPaused == true)
            {
                return PausedResult(request.Name, state, trace, invariantReports, normalizeResult);
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

            if (actionsExecuted >= request.MaxActions)
            {
                var abort = new MaxTurnAbortResult(request.MaxActions, actionsExecuted, state.ComputeStateHash());
                trace.AddStateSnapshot($"random:{request.Name}:max-action-abort", state);
                return new ScenarioResult(
                    request.Name,
                    ScenarioRunStatus.MaxTurnAbort,
                    state,
                    trace,
                    invariantReports,
                    state.ComputeStateHash(),
                    abort);
            }

            var action = legalActions[rng.NextInt(legalActions.Count)].Action;
            var actionResult = session?.Step(action);
            if (actionResult is not null)
            {
                if (actionResult.IsPaused)
                {
                    AddInvariantReport(state, invariantReports);
                    return PausedResult(request.Name, state, trace, invariantReports, actionResult);
                }
            }
            else
            {
                ThrowIfPendingSelection(
                    _actionExecutor.Execute(state, action, trace),
                    $"random:{request.Name}:{actionsExecuted}");
            }

            actionsExecuted++;
            AddInvariantReport(state, invariantReports);

            if (state.Phase == Phase.Breeding && !state.IsGameOver)
            {
                var mainPhaseResult = session?.RunMainPhase();
                if (mainPhaseResult is not null)
                {
                    if (mainPhaseResult.IsPaused)
                    {
                        AddInvariantReport(state, invariantReports);
                        return PausedResult(request.Name, state, trace, invariantReports, mainPhaseResult);
                    }
                }
                else
                {
                    _phaseRunner.RunMainPhase(state);
                    trace.AddStateSnapshot($"random:{request.Name}:after-breeding-action", state);
                }

                AddInvariantReport(state, invariantReports);
            }
        }

        trace.AddStateSnapshot($"random:{request.Name}:final", state);
        return new ScenarioResult(
            request.Name,
            state.IsGameOver ? ScenarioRunStatus.GameOver : ScenarioRunStatus.Completed,
            state,
            trace,
            invariantReports,
            state.ComputeStateHash());
    }

    private EngineStepResult? NormalizeToActionPhase(
        GameState state,
        GameTrace trace,
        List<EngineInvariantReport> invariantReports,
        EngineSession? session)
    {
        while (!state.IsGameOver)
        {
            switch (state.Phase)
            {
                case Phase.None:
                case Phase.End:
                    var runToMainResult = session?.RunToMainPhase();
                    if (runToMainResult is not null)
                    {
                        if (runToMainResult.IsPaused)
                        {
                            AddInvariantReport(state, invariantReports);
                            return runToMainResult;
                        }
                    }
                    else
                    {
                        _turnRunner.RunToMainPhase(state);
                        trace.AddStateSnapshot("random:run-to-main", state);
                    }

                    AddInvariantReport(state, invariantReports);
                    continue;

                case Phase.Active:
                    _phaseRunner.RunDrawPhase(state);
                    if (!state.IsGameOver)
                    {
                        _phaseRunner.RunBreedingPhase(state);
                    }

                    trace.AddStateSnapshot("random:active-to-breeding", state);
                    AddInvariantReport(state, invariantReports);
                    continue;

                case Phase.Draw:
                    _phaseRunner.RunBreedingPhase(state);
                    trace.AddStateSnapshot("random:draw-to-breeding", state);
                    AddInvariantReport(state, invariantReports);
                    continue;

                case Phase.Breeding:
                    if (_legalActionGenerator.Generate(state, state.TurnPlayerId).Count == 0)
                    {
                        var mainPhaseResult = session?.RunMainPhase();
                        if (mainPhaseResult is not null)
                        {
                            if (mainPhaseResult.IsPaused)
                            {
                                AddInvariantReport(state, invariantReports);
                                return mainPhaseResult;
                            }
                        }
                        else
                        {
                            _phaseRunner.RunMainPhase(state);
                            trace.AddStateSnapshot("random:breeding-to-main", state);
                        }

                        AddInvariantReport(state, invariantReports);
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

    private static ScenarioResult PausedResult(
        string name,
        GameState state,
        GameTrace trace,
        IReadOnlyList<EngineInvariantReport> invariantReports,
        EngineStepResult stepResult)
    {
        trace.AddStateSnapshot($"random:{name}:paused", state);
        return new ScenarioResult(
            name,
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
