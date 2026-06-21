namespace DCGO.RL.Engine.Domain;

public enum AttackRuntimeState
{
    CounterNonCounter,
    CounterCounter,
    CounterSelection,
    BlockDesignation,
    BlockTrigger,
    AttackTargetChanged,
    Battle,
    EndAttack,
    Cleanup,
}

public sealed record AttackTargetSwitch(
    PermanentId? OldDefender,
    PermanentId? NewDefender,
    bool IsBlock,
    PermanentId? Blocker,
    string? SourceEffectStableId);

public sealed record AttackRuntimeContext(
    PermanentId Attacker,
    PermanentId? Defender,
    AttackRuntimeState State,
    bool IsBlocking = false,
    PermanentId? Blocker = null,
    bool IsEndAttack = false,
    CardInstanceId? AttackerTopCardWhenDeclared = null,
    IReadOnlyList<CardInstanceId>? CounterSourceSnapshot = null,
    AttackTargetSwitch? PendingTargetSwitch = null)
{
    public IReadOnlyList<CardInstanceId> CounterSources { get; } =
        CounterSourceSnapshot?.ToArray() ?? Array.Empty<CardInstanceId>();

    public AttackRuntimeContext WithState(AttackRuntimeState state) =>
        this with { State = state };

    public AttackRuntimeContext EndAttack() =>
        this with
        {
            State = AttackRuntimeState.EndAttack,
            IsEndAttack = true,
            IsBlocking = false,
            Blocker = null,
        };

    public AttackRuntimeContext SwitchDefender(
        PermanentId? newDefender,
        bool isBlock,
        PermanentId? blocker = null,
        string? sourceEffectStableId = null)
    {
        var changed = Defender != newDefender;
        return this with
        {
            Defender = newDefender,
            IsBlocking = isBlock,
            Blocker = isBlock ? blocker ?? newDefender : null,
            PendingTargetSwitch = changed
                ? new AttackTargetSwitch(Defender, newDefender, isBlock, blocker ?? newDefender, sourceEffectStableId)
                : PendingTargetSwitch,
        };
    }

    public AttackRuntimeContext SwitchToSecurityTarget(string? sourceEffectStableId = null) =>
        SwitchDefender(null, isBlock: false, blocker: null, sourceEffectStableId);

    public AttackRuntimeContext SetBlocking(bool isBlocking, PermanentId? blocker = null) =>
        this with
        {
            IsBlocking = isBlocking,
            Blocker = isBlocking ? blocker ?? Blocker : null,
        };

    public AttackRuntimeContext ConsumePendingTargetSwitch() =>
        this with { PendingTargetSwitch = null };
}
