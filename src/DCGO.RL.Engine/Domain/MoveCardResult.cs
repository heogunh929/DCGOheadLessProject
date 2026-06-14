namespace DCGO.RL.Engine.Domain;

public sealed record MoveCardResult(
    CardInstanceId Card,
    Zone SourceZone,
    Zone DestinationZone,
    ZoneMoveTrace Trace);
