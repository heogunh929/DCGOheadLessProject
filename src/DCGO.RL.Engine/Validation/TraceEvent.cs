using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public enum TraceEventKind
{
    StateSnapshot,
    Action,
    MoveCard,
    Decision,
    Selection,
    UnsupportedMechanic,
}

public sealed record TraceEvent(
    int Index,
    TraceEventKind Kind,
    string Label,
    string StateHashBefore,
    string StateHashAfter,
    GameAction? Action = null,
    MoveCardCommand? MoveCommand = null,
    MoveCardResult? MoveResult = null,
    DecisionPoint? DecisionPoint = null,
    SelectionResult? SelectionResult = null,
    string Details = "");
