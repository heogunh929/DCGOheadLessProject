using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed class OncePerTurnTracker
{
    private readonly HashSet<OncePerTurnKey> _used = new();

    public bool CanUse(GameState state, string effectStableId, PlayerId player, CardInstanceId? sourceCard = null) =>
        !_used.Contains(new OncePerTurnKey(state.TurnCount, player, effectStableId, sourceCard));

    public void RegisterUse(GameState state, string effectStableId, PlayerId player, CardInstanceId? sourceCard = null)
    {
        if (!CanUse(state, effectStableId, player, sourceCard))
        {
            throw new DomainException($"Effect '{effectStableId}' has already been used this turn by player '{player}'.");
        }

        _used.Add(new OncePerTurnKey(state.TurnCount, player, effectStableId, sourceCard));
    }

    public void ClearBeforeTurn(int turnCount) =>
        _used.RemoveWhere(key => key.TurnCount < turnCount);

    private sealed record OncePerTurnKey(
        int TurnCount,
        PlayerId Player,
        string EffectStableId,
        CardInstanceId? SourceCard);
}
