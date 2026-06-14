using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed class StateHasher
{
    public string ComputeHash(GameState state) => state.ComputeStateHash();
}
