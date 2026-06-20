namespace DCGO.RL.Engine.Domain;

public sealed class RuntimeRuleState
{
    private readonly List<OncePerTurnUse> _oncePerTurnUses = new();

    public IReadOnlyList<OncePerTurnUse> OncePerTurnUses => _oncePerTurnUses;

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

    public RuntimeRuleState Clone()
    {
        var clone = new RuntimeRuleState();
        clone._oncePerTurnUses.AddRange(_oncePerTurnUses);
        return clone;
    }

    internal void RestoreFrom(RuntimeRuleState snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        _oncePerTurnUses.Clear();
        _oncePerTurnUses.AddRange(snapshot._oncePerTurnUses);
    }
}

public readonly record struct OncePerTurnUse(
    int TurnCount,
    PlayerId Player,
    string EffectStableId,
    CardInstanceId? SourceCard);
