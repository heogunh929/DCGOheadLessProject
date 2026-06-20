using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed class GameTrace
{
    private readonly List<TraceEvent> _events = new();
    private readonly StateHasher _stateHasher;

    public GameTrace(StateHasher? stateHasher = null)
    {
        _stateHasher = stateHasher ?? new StateHasher();
    }

    public GameTrace(IEnumerable<TraceEvent> events, StateHasher? stateHasher = null)
        : this(stateHasher)
    {
        _events.AddRange(events.OrderBy(traceEvent => traceEvent.Index));
    }

    public IReadOnlyList<TraceEvent> Events => _events;

    public TraceEvent AddStateSnapshot(string label, GameState state) =>
        Add(TraceEventKind.StateSnapshot, label, state, state);

    public TraceEvent AddAction(string label, GameState before, GameState after, GameAction action) =>
        Add(TraceEventKind.Action, label, before, after, Action: action);

    public TraceEvent AddMove(string label, GameState before, GameState after, MoveCardCommand command, MoveCardResult result) =>
        Add(TraceEventKind.MoveCard, label, before, after, MoveCommand: command, MoveResult: result);

    public TraceEvent AddDecision(string label, GameState state, DecisionPoint decisionPoint) =>
        Add(TraceEventKind.Decision, label, state, state, DecisionPoint: decisionPoint);

    public TraceEvent AddSelection(string label, GameState state, SelectionResult selectionResult) =>
        Add(TraceEventKind.Selection, label, state, state, SelectionResult: selectionResult);

    public TraceEvent AddSelection(string label, GameState before, GameState after, SelectionResult selectionResult) =>
        Add(TraceEventKind.Selection, label, before, after, SelectionResult: selectionResult);

    public TraceEvent AddPhase(string label, GameState before, GameState after, string details) =>
        Add(TraceEventKind.Phase, label, before, after, Details: details);

    public TraceEvent AddUnsupportedMechanic(string label, GameState state, string details) =>
        Add(TraceEventKind.UnsupportedMechanic, label, state, state, Details: details);

    internal void Truncate(int count)
    {
        if (count < 0 || count > _events.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        _events.RemoveRange(count, _events.Count - count);
    }

    private TraceEvent Add(
        TraceEventKind kind,
        string label,
        GameState before,
        GameState after,
        GameAction? Action = null,
        MoveCardCommand? MoveCommand = null,
        MoveCardResult? MoveResult = null,
        DecisionPoint? DecisionPoint = null,
        SelectionResult? SelectionResult = null,
        string Details = "")
    {
        var traceEvent = new TraceEvent(
            Index: _events.Count,
            Kind: kind,
            Label: label,
            StateHashBefore: _stateHasher.ComputeHash(before),
            StateHashAfter: _stateHasher.ComputeHash(after),
            Action: Action,
            MoveCommand: MoveCommand,
            MoveResult: MoveResult,
            DecisionPoint: DecisionPoint,
            SelectionResult: SelectionResult,
            Details: Details);

        _events.Add(traceEvent);
        return traceEvent;
    }
}
