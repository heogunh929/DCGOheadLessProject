using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Decisions;

namespace DCGO.RL.Engine.Effects;

public enum SecurityEffectExecutionMode
{
    Direct,
    ActivateMainOption,
}

public sealed record EffectDescriptor(
    string StableId,
    EffectTiming Timing,
    CardInstanceId? SourceCard = null,
    PermanentId? SourcePermanent = null,
    PlayerId? Controller = null,
    bool IsBackground = false,
    bool IsOptional = false,
    Func<EffectContext, bool>? CanTrigger = null,
    Func<EffectContext, SelectionRequest>? CreateSelectionRequest = null,
    SelectionResultContinuation? SelectionContinuation = null,
    SecurityEffectExecutionMode SecurityExecutionMode = SecurityEffectExecutionMode.Direct,
    bool IsOncePerTurn = false,
    string? OncePerTurnKey = null,
    Func<EffectContext, bool>? CanActivate = null,
    TriggerSourceSnapshot? SourceSnapshot = null)
{
    public bool Matches(EffectContext context) =>
        Timing == context.Timing && (CanTrigger?.Invoke(context) ?? true);
}
