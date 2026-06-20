using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record ReplayResult(GameState FinalState, GameTrace Trace, EngineInvariantReport InvariantReport);

public sealed class ReplayRunner
{
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly ActionExecutor _actionExecutor;
    private readonly BattleEngineServices? _services;

    public ReplayRunner(
        EngineInvariantChecker? invariantChecker = null,
        ActionExecutor? actionExecutor = null,
        BattleEngineServices? services = null)
    {
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _services = services;
        _actionExecutor = actionExecutor
            ?? services?.ActionExecutor
            ?? throw new DomainException("ReplayRunner requires an ActionExecutor from BattleEngineServices.");
    }

    public ReplayResult Replay(GameState initialState, GameTrace trace)
    {
        var state = initialState.Clone();
        var report = _invariantChecker.Check(state);
        report.ThrowIfInvalid();
        var session = _services?.CreateSession(state, new GameTrace());

        foreach (var traceEvent in trace.Events)
        {
            if (traceEvent.Kind is not (TraceEventKind.Action or TraceEventKind.Selection or TraceEventKind.Phase))
            {
                continue;
            }

            var beforeHash = state.ComputeStateHash();
            if (!string.Equals(beforeHash, traceEvent.StateHashBefore, StringComparison.Ordinal))
            {
                throw new DomainException($"Trace replay hash mismatch before event '{traceEvent.Index}'.");
            }

            switch (traceEvent.Kind)
            {
                case TraceEventKind.Action:
                    if (traceEvent.Action is null)
                    {
                        throw new DomainException($"Trace action event '{traceEvent.Index}' has no action payload.");
                    }

                    if (session is not null)
                    {
                        session.Step(traceEvent.Action);
                    }
                    else
                    {
                        var result = _actionExecutor.Execute(state, traceEvent.Action);
                        if (result.HasPendingSelection)
                        {
                            throw new DomainException(
                                $"ReplayRunner cannot ignore pending SelectionRequest '{result.PendingSelectionRequest!.Id}' at trace event '{traceEvent.Index}'.");
                        }
                    }

                    break;

                case TraceEventKind.Selection:
                    if (session is null)
                    {
                        throw new DomainException("ReplayRunner requires BattleEngineServices to replay selection results.");
                    }

                    if (traceEvent.SelectionResult is null)
                    {
                        throw new DomainException($"Trace selection event '{traceEvent.Index}' has no selection payload.");
                    }

                    session.Resume(traceEvent.SelectionResult);
                    break;

                case TraceEventKind.Phase:
                    if (session is null)
                    {
                        throw new DomainException("ReplayRunner requires BattleEngineServices to replay phase events.");
                    }

                    if (!string.Equals(traceEvent.Details, "RunMainPhase", StringComparison.Ordinal))
                    {
                        throw new UnsupportedMechanicException($"Trace phase replay '{traceEvent.Details}'.");
                    }

                    session.RunMainPhase();
                    break;
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
