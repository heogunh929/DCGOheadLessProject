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

public enum CounterWindowGroup
{
    TurnPlayer,
    NonTurnPlayer,
    Done,
}

public sealed record AttackTargetSwitch(
    PermanentId? OldDefender,
    PermanentId? NewDefender,
    bool IsBlocking,
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
    IReadOnlyList<AttackTargetSwitch>? PendingTargetSwitches = null,
    CounterWindowGroup CounterGroup = CounterWindowGroup.TurnPlayer,
    bool CounterUsed = false,
    IReadOnlyList<string>? AttemptedCounterCandidateIds = null)
{
    public IReadOnlyList<CardInstanceId> CounterSources =>
        CounterSourceSnapshot?.ToArray() ?? Array.Empty<CardInstanceId>();

    public IReadOnlyList<AttackTargetSwitch> TargetSwitchQueue =>
        PendingTargetSwitches?.ToArray() ?? Array.Empty<AttackTargetSwitch>();

    public IReadOnlyList<string> AttemptedCounterCandidates =>
        AttemptedCounterCandidateIds?.ToArray() ?? Array.Empty<string>();

    public AttackRuntimeContext WithState(AttackRuntimeState state) =>
        this with { State = state };

    public AttackRuntimeContext EndAttack() =>
        this with
        {
            State = AttackRuntimeState.EndAttack,
            IsEndAttack = true,
        };

    public AttackRuntimeContext SwitchDefender(
        PermanentId? newDefender,
        bool isBlock,
        PermanentId? blocker = null,
        string? sourceEffectStableId = null)
    {
        var changed = Defender != newDefender;
        var switchQueue = TargetSwitchQueue.ToList();
        if (changed)
        {
            switchQueue.Add(new AttackTargetSwitch(
                Defender,
                newDefender,
                isBlock,
                isBlock ? blocker ?? newDefender : null,
                sourceEffectStableId));
        }

        return this with
        {
            Defender = newDefender,
            IsBlocking = isBlock,
            Blocker = isBlock ? blocker ?? newDefender : null,
            PendingTargetSwitches = switchQueue,
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

    public AttackRuntimeContext WithCounterGroup(CounterWindowGroup group) =>
        this with { CounterGroup = group };

    public AttackRuntimeContext MarkCounterUsed() =>
        this with
        {
            CounterUsed = true,
            CounterGroup = CounterWindowGroup.Done,
        };

    public AttackRuntimeContext MarkCounterCandidateAttempted(string candidateId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(candidateId);
        if (AttemptedCounterCandidates.Contains(candidateId, StringComparer.Ordinal))
        {
            return this;
        }

        return this with
        {
            AttemptedCounterCandidateIds = AttemptedCounterCandidates.Concat(new[] { candidateId }).ToArray(),
        };
    }

    public AttackRuntimeContext AdvanceCounterGroup() =>
        this with
        {
            CounterGroup = CounterGroup switch
            {
                CounterWindowGroup.TurnPlayer => CounterWindowGroup.NonTurnPlayer,
                _ => CounterWindowGroup.Done,
            },
        };

    public AttackRuntimeContext ConsumePendingTargetSwitch()
    {
        if (TargetSwitchQueue.Count == 0)
        {
            return this;
        }

        return this with { PendingTargetSwitches = TargetSwitchQueue.Skip(1).ToArray() };
    }
}
