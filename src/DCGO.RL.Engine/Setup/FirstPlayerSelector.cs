using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Infrastructure;

namespace DCGO.RL.Engine.Setup;

public sealed class FirstPlayerSelector
{
    public PlayerId SelectFirstPlayer(GameState state, IDeterministicRng rng, PlayerId? forcedFirstPlayer = null)
    {
        if (forcedFirstPlayer is not null)
        {
            _ = state.GetPlayer(forcedFirstPlayer.Value);
            return forcedFirstPlayer.Value;
        }

        return rng.NextInt(state.Players.Count) == 0 ? PlayerId.Player0 : PlayerId.Player1;
    }
}
