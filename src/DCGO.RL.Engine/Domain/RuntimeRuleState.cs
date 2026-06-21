using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Domain;

public sealed class RuntimeRuleState
{
    private readonly List<OncePerTurnUse> _oncePerTurnUses = new();
    private readonly List<PendingRuleEvent> _pendingRuleEvents = new();
    private AttackRuntimeContext? _attack;

    public IReadOnlyList<OncePerTurnUse> OncePerTurnUses => _oncePerTurnUses;

    public IReadOnlyList<PendingRuleEvent> PendingRuleEvents => _pendingRuleEvents;

    public AttackRuntimeContext? Attack => _attack;

    public bool CanUseOncePerTurn(
        int turnCount,
        PlayerId player,
        string effectStableId,
        CardInstanceId? sourceCard = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(effectStableId);
        ClearOncePerTurnBefore(turnCount);
        return !_oncePerTurnUses.Contains(new OncePerTurnUse(turnCount, player, effectStableId, sourceCard));
    }

    public void RegisterOncePerTurnUse(
        int turnCount,
        PlayerId player,
        string effectStableId,
        CardInstanceId? sourceCard = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(effectStableId);
        ClearOncePerTurnBefore(turnCount);
        var use = new OncePerTurnUse(turnCount, player, effectStableId, sourceCard);
        if (_oncePerTurnUses.Contains(use))
        {
            throw new DomainException($"Effect '{effectStableId}' has already been used this turn by player '{player}'.");
        }

        _oncePerTurnUses.Add(use);
    }

    public void ClearOncePerTurnBefore(int turnCount) =>
        _oncePerTurnUses.RemoveAll(use => use.TurnCount < turnCount);

    public void EnqueueRuleEvent(
        EffectTiming timing,
        PlayerId player,
        IReadOnlyDictionary<string, object?>? values = null)
    {
        if (timing is EffectTiming.None)
        {
            throw new DomainException("Pending rule event timing must be explicit.");
        }

        var payload = values is null
            ? new Dictionary<string, object?>(StringComparer.Ordinal)
            : new Dictionary<string, object?>(values, StringComparer.Ordinal);
        _pendingRuleEvents.Add(new PendingRuleEvent(timing, player, payload));
    }

    public IReadOnlyList<PendingRuleEvent> ConsumePendingRuleEvents()
    {
        if (_pendingRuleEvents.Count == 0)
        {
            return Array.Empty<PendingRuleEvent>();
        }

        var events = _pendingRuleEvents.ToArray();
        _pendingRuleEvents.Clear();
        return events;
    }

    public bool TryDequeuePendingRuleEvent(out PendingRuleEvent ruleEvent)
    {
        if (_pendingRuleEvents.Count == 0)
        {
            ruleEvent = null!;
            return false;
        }

        ruleEvent = _pendingRuleEvents[0];
        _pendingRuleEvents.RemoveAt(0);
        return true;
    }

    public void StartAttack(AttackRuntimeContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (_attack is not null)
        {
            throw new DomainException("An attack runtime context is already active.");
        }

        _attack = context;
    }

    public void SetAttack(AttackRuntimeContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        if (_attack is null)
        {
            throw new DomainException("Cannot update attack runtime context because no attack is active.");
        }

        _attack = context;
    }

    public AttackRuntimeContext RequireAttack() =>
        _attack ?? throw new DomainException("Attack runtime context is not active.");

    public AttackRuntimeContext EndAttack()
    {
        var context = RequireAttack().EndAttack();
        _attack = context;
        return context;
    }

    public AttackRuntimeContext SwitchDefender(
        PermanentId? defender,
        bool isBlock,
        PermanentId? blocker = null,
        string? sourceEffectStableId = null)
    {
        var context = RequireAttack().SwitchDefender(defender, isBlock, blocker, sourceEffectStableId);
        _attack = context;
        return context;
    }

    public AttackRuntimeContext SwitchToSecurityTarget(string? sourceEffectStableId = null)
    {
        var context = RequireAttack().SwitchToSecurityTarget(sourceEffectStableId);
        _attack = context;
        return context;
    }

    public AttackRuntimeContext SetBlocking(bool isBlocking, PermanentId? blocker = null)
    {
        var context = RequireAttack().SetBlocking(isBlocking, blocker);
        _attack = context;
        return context;
    }

    public AttackTargetSwitch? ConsumePendingAttackTargetSwitch()
    {
        if (_attack is null || _attack.TargetSwitchQueue.Count == 0)
        {
            return null;
        }

        var pending = _attack.TargetSwitchQueue[0];
        _attack = _attack.ConsumePendingTargetSwitch();
        return pending;
    }

    public void ClearAttack() => _attack = null;

    public RuntimeRuleState Clone()
    {
        var clone = new RuntimeRuleState();
        clone._oncePerTurnUses.AddRange(_oncePerTurnUses);
        clone._pendingRuleEvents.AddRange(_pendingRuleEvents.Select(ruleEvent => ruleEvent.Copy()));
        clone._attack = _attack;
        return clone;
    }

    internal void RestoreFrom(RuntimeRuleState snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        _oncePerTurnUses.Clear();
        _oncePerTurnUses.AddRange(snapshot._oncePerTurnUses);
        _pendingRuleEvents.Clear();
        _pendingRuleEvents.AddRange(snapshot._pendingRuleEvents.Select(ruleEvent => ruleEvent.Copy()));
        _attack = snapshot._attack;
    }
}

public readonly record struct OncePerTurnUse(
    int TurnCount,
    PlayerId Player,
    string EffectStableId,
    CardInstanceId? SourceCard);

public sealed record PendingRuleEvent(
    EffectTiming Timing,
    PlayerId Player,
    IReadOnlyDictionary<string, object?> Values)
{
    public PendingRuleEvent Copy() =>
        this with
        {
            Values = new Dictionary<string, object?>(Values, StringComparer.Ordinal),
        };
}
