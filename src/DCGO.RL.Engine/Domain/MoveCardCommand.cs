namespace DCGO.RL.Engine.Domain;

public sealed record MoveCardCommand(
    CardInstanceId Card,
    Zone SourceZone,
    Zone DestinationZone,
    MoveReason Reason,
    PermanentId? SourcePermanent = null,
    PermanentId? DestinationPermanent = null,
    bool ToTop = true,
    int? DestinationFrameIndex = null,
    bool? FaceUp = null);
