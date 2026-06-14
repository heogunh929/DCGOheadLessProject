namespace DCGO.RL.Engine.Domain;

public sealed record PermanentZoneMoveCommand(
    PermanentId Permanent,
    Zone SourceZone,
    Zone DestinationZone,
    MoveReason Reason,
    int? DestinationFrameIndex = null);
