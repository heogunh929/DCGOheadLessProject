using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed record EffectContext(
    GameState State,
    EffectTiming Timing,
    PlayerId? Player = null,
    CardInstanceId? SourceCard = null,
    PermanentId? SourcePermanent = null,
    IReadOnlyDictionary<string, object?>? Values = null)
{
    public IReadOnlyDictionary<string, object?> Payload { get; } =
        Values is null
            ? new Dictionary<string, object?>(StringComparer.Ordinal)
            : new Dictionary<string, object?>(Values, StringComparer.Ordinal);

    public object? GetValueOrDefault(string key) =>
        Payload.TryGetValue(key, out var value) ? value : null;
}
