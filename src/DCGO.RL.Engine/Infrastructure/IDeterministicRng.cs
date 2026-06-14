namespace DCGO.RL.Engine.Infrastructure;

public interface IDeterministicRng
{
    ulong Seed { get; }
    int NextInt(int exclusiveMax);
    int NextInt(int inclusiveMin, int exclusiveMax);
    double NextDouble();
    IDeterministicRng Clone();
}
