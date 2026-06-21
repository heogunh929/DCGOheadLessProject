namespace DCGO.RL.Engine.Domain;

public sealed class RuntimeRuleState
{
    private readonly List<OncePerTurnUse> _oncePerTurnUses = new();
    private AttackRuntimeContext? _attack;

    public IReadOnlyList<OncePerTurnUse> OncePerTurnUses => _oncePerTurnUses;

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
        clone._attack = _attack;
        return clone;
    }

    internal void RestoreFrom(RuntimeRuleState snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        _oncePerTurnUses.Clear();
        _oncePerTurnUses.AddRange(snapshot._oncePerTurnUses);
        _attack = snapshot._attack;
    }
}

public readonly record struct OncePerTurnUse(
    int TurnCount,
    PlayerId Player,
    string EffectStableId,
    CardInstanceId? SourceCard);
