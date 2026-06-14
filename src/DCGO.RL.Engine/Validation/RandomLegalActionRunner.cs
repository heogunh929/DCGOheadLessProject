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
    private readonly ActionExecutor _actionExecutor;
    private readonly PhaseRunner _phaseRunner;
    private readonly TurnRunner _turnRunner;
    private readonly EngineInvariantChecker _invariantChecker;

    public RandomLegalActionRunner(
        LegalActionGenerator? legalActionGenerator = null,
        ActionExecutor? actionExecutor = null,
        PhaseRunner? phaseRunner = null,
        TurnRunner? turnRunner = null,
        EngineInvariantChecker? invariantChecker = null)
    {
        _legalActionGenerator = legalActionGenerator ?? new LegalActionGenerator();
        _actionExecutor = actionExecutor ?? new ActionExecutor();
        _phaseRunner = phaseRunner ?? new PhaseRunner();
        _turnRunner = turnRunner ?? new TurnRunner(_phaseRunner);
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
    }

    public ScenarioResult Run(RandomLegalActionRunRequest request)
    {
        if (request.MaxActions < 1)
        {
            throw new DomainException("MaxActions must be positive.");
        }

        var state = request.InitialState.Clone();
        var trace = new GameTrace();
        var invariantReports = new List<EngineInvariantReport>();
        var rng = new XorShiftDeterministicRng(request.Seed);
        var actionsExecuted = 0;

        trace.AddStateSnapshot($"random:{request.Name}:initial", state);
        AddInvariantReport(state, invariantReports);

        while (!state.IsGameOver)
        {
            NormalizeToActionPhase(state, trace, invariantReports);
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
            _actionExecutor.Execute(state, action, trace);
            actionsExecuted++;
            AddInvariantReport(state, invariantReports);

            if (state.Phase == Phase.Breeding && !state.IsGameOver)
            {
                _phaseRunner.RunMainPhase(state);
                trace.AddStateSnapshot($"random:{request.Name}:after-breeding-action", state);
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

    private void NormalizeToActionPhase(GameState state, GameTrace trace, List<EngineInvariantReport> invariantReports)
    {
        while (!state.IsGameOver)
        {
            switch (state.Phase)
            {
                case Phase.None:
                case Phase.End:
                    _turnRunner.RunToMainPhase(state);
                    trace.AddStateSnapshot("random:run-to-main", state);
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
                        _phaseRunner.RunMainPhase(state);
                        trace.AddStateSnapshot("random:breeding-to-main", state);
                        AddInvariantReport(state, invariantReports);
                        continue;
                    }

                    return;

                case Phase.Main:
                    return;

                default:
                    throw new UnsupportedMechanicException($"Random runner phase '{state.Phase}'");
            }
        }
    }

    private void AddInvariantReport(GameState state, List<EngineInvariantReport> invariantReports)
    {
        var report = _invariantChecker.Check(state);
        report.ThrowIfInvalid();
        invariantReports.Add(report);
    }
}
