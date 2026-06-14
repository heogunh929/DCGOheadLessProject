namespace DCGO.RL.Engine.Domain;

public sealed record DigivolveCardResult(
    CardInstanceId Card,
    CardInstanceId PreviousTopCard,
    PermanentId Permanent,
    ZoneMoveTrace Trace);
