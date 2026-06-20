using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed class OncePerTurnTracker
{
    public bool CanUse(GameState state, string effectStableId, PlayerId player, CardInstanceId? sourceCard = null) =>
        state.RuntimeRules.CanUseOncePerTurn(state.TurnCount, player, effectStableId, sourceCard);

    public void RegisterUse(GameState state, string effectStableId, PlayerId player, CardInstanceId? sourceCard = null)
    {
        state.RuntimeRules.RegisterOncePerTurnUse(state.TurnCount, player, effectStableId, sourceCard);
    }

    public void ClearBeforeTurn(GameState state, int turnCount) =>
        state.RuntimeRules.ClearOncePerTurnBefore(turnCount);
}
