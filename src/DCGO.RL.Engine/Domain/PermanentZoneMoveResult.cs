namespace DCGO.RL.Engine.Domain;

public sealed record PermanentZoneMoveResult(
    PermanentId Permanent,
    CardInstanceId TopCard,
    Zone SourceZone,
    Zone DestinationZone,
    ZoneMoveTrace Trace);
