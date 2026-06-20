using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record ReplayResult(GameState FinalState, GameTrace Trace, EngineInvariantReport InvariantReport);

public sealed class ReplayRunner
{
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly ActionExecutor _actionExecutor;

    public ReplayRunner(EngineInvariantChecker? invariantChecker = null, ActionExecutor? actionExecutor = null)
    {
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _actionExecutor = actionExecutor
            ?? throw new DomainException("ReplayRunner requires an ActionExecutor from BattleEngineServices.");
    }

    public ReplayResult Replay(GameState initialState, GameTrace trace)
    {
        var state = initialState.Clone();
        var report = _invariantChecker.Check(state);
        report.ThrowIfInvalid();

        foreach (var traceEvent in trace.Events)
        {
            if (traceEvent.Kind != TraceEventKind.Action)
            {
                continue;
            }

            if (traceEvent.Action is null)
            {
                throw new DomainException($"Trace action event '{traceEvent.Index}' has no action payload.");
            }

            var beforeHash = state.ComputeStateHash();
            if (!string.Equals(beforeHash, traceEvent.StateHashBefore, StringComparison.Ordinal))
            {
                throw new DomainException($"Trace replay hash mismatch before event '{traceEvent.Index}'.");
            }

            var result = _actionExecutor.Execute(state, traceEvent.Action);
            if (result.HasPendingSelection)
            {
                throw new DomainException(
                    $"ReplayRunner cannot ignore pending SelectionRequest '{result.PendingSelectionRequest!.Id}' at trace event '{traceEvent.Index}'.");
            }

            var afterHash = state.ComputeStateHash();
            if (!string.Equals(afterHash, traceEvent.StateHashAfter, StringComparison.Ordinal))
            {
                throw new DomainException($"Trace replay hash mismatch after event '{traceEvent.Index}'.");
            }

            report = _invariantChecker.Check(state);
            report.ThrowIfInvalid();
        }

        return new ReplayResult(state, trace, report);
    }
}
