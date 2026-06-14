namespace DCGO.RL.Engine.Domain;

public sealed record GameConfig
{
    public ulong Seed { get; init; } = 1;
    public int InitialHandSize { get; init; } = 5;
    public int InitialSecurityCount { get; init; } = 5;
    public int FieldSlotCount { get; init; } = 16;
}
