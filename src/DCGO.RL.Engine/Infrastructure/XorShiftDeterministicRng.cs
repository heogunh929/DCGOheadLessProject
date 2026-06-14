namespace DCGO.RL.Engine.Infrastructure;

public sealed class XorShiftDeterministicRng : IDeterministicRng
{
    private ulong _state;

    public XorShiftDeterministicRng(ulong seed)
    {
        Seed = seed;
        _state = seed == 0 ? 0x9E3779B97F4A7C15UL : seed;
    }

    private XorShiftDeterministicRng(ulong seed, ulong state)
    {
        Seed = seed;
        _state = state;
    }

    public ulong Seed { get; }

    public int NextInt(int exclusiveMax)
    {
        if (exclusiveMax <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(exclusiveMax));
        }

        return (int)(NextUInt64() % (uint)exclusiveMax);
    }

    public int NextInt(int inclusiveMin, int exclusiveMax)
    {
        if (exclusiveMax <= inclusiveMin)
        {
            throw new ArgumentOutOfRangeException(nameof(exclusiveMax));
        }

        return inclusiveMin + NextInt(exclusiveMax - inclusiveMin);
    }

    public double NextDouble()
    {
        const double denominator = 1.0 / (1UL << 53);
        return (NextUInt64() >> 11) * denominator;
    }

    public IDeterministicRng Clone() => new XorShiftDeterministicRng(Seed, _state);

    private ulong NextUInt64()
    {
        var x = _state;
        x ^= x << 13;
        x ^= x >> 7;
        x ^= x << 17;
        _state = x;
        return x;
    }
}
