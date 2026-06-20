using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed record RuleStabilizationEvent(
    EffectTiming Timing,
    PlayerId Player,
    IReadOnlyDictionary<string, object?> Values);

public sealed record RuleStabilizationResult(
    int ChangesApplied,
    IReadOnlyList<RuleStabilizationEvent> Events)
{
    public static RuleStabilizationResult Empty { get; } =
        new(0, Array.Empty<RuleStabilizationEvent>());
}
