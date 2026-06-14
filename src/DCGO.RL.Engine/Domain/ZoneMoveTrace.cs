namespace DCGO.RL.Engine.Domain;

public sealed record ZoneMoveTrace(
    CardInstanceId Card,
    PlayerId Owner,
    Zone SourceZone,
    Zone DestinationZone,
    MoveReason Reason,
    PermanentId? SourcePermanent,
    PermanentId? DestinationPermanent,
    string StateHashBefore,
    string StateHashAfter);
