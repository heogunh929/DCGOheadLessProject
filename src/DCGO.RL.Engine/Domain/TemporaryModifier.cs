namespace DCGO.RL.Engine.Domain;

public enum DurationScope
{
    UntilTurnEnd,
    UntilOwnerTurnEnd,
    UntilOpponentTurnEnd,
    UntilBattleEnd,
    UntilAttackEnd,
    UntilSecurityCheckEnd,
    UntilOwnerActivePhaseEnd,
}

public enum TemporaryModifierKind
{
    DP,
    SecurityAttack,
    SecurityDigimonDP,
    CannotAttack,
    CannotBlock,
    CannotSwitchAttackTarget,
}

public sealed record TemporaryModifier(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    PermanentId? TargetPermanentId,
    PlayerId? TargetPlayerId,
    TemporaryModifierKind ModifierKind,
    int Amount,
    DurationScope DurationScope,
    int CreatedTurnCount,
    Phase CreatedPhase,
    PlayerId? ExpiresAtTurnPlayerId,
    string DebugLabel);
