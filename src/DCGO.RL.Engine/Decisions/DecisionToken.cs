namespace DCGO.RL.Engine.Decisions;

public readonly record struct DecisionToken(long Value)
{
    public override string ToString() => Value.ToString();
}
