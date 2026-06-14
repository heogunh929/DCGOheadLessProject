namespace DCGO.RL.Engine.Domain;

public sealed record DigivolveCardCommand(
    CardInstanceId Card,
    Zone SourceZone,
    PermanentId TargetPermanent,
    MoveReason Reason = MoveReason.Digivolve,
    bool SourceToTop = true);
