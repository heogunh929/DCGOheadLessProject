using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Domain;

public sealed record TemporaryGrantedEffect(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    PermanentId? TargetPermanentId,
    PlayerId? TargetPlayerId,
    EffectTiming Timing,
    string GrantedEffectKey,
    DurationScope DurationScope,
    int CreatedTurnCount,
    Phase CreatedPhase,
    PlayerId? ExpiresAtTurnPlayerId,
    string DebugLabel,
    CardMetadataCriteria? TargetMetadataCriteria = null);
