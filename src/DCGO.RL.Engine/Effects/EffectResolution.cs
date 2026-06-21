using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public enum EffectDecisionStage
{
    Optional,
    Selection,
    Ordering,
}

public sealed record EffectResolution(
    string StableId,
    EffectTiming Timing,
    CardInstanceId? SourceCard,
    PermanentId? SourcePermanent,
    PlayerId? Controller,
    bool IsBackground,
    bool IsOptional,
    EffectContext Context,
    SelectionRequest? OptionalSelectionRequest = null,
    SelectionRequest? SelectionRequest = null,
    SelectionResultContinuation? SelectionContinuation = null,
    SecurityEffectExecutionMode SecurityExecutionMode = SecurityEffectExecutionMode.Direct,
    bool IsOncePerTurn = false,
    string? OncePerTurnKey = null,
    Func<EffectContext, bool>? CanActivate = null,
    TriggerSourceSnapshot? SourceSnapshot = null,
    TriggerSourcePersistencePolicy SourcePersistencePolicy = TriggerSourcePersistencePolicy.RequireSameRole,
    bool IsCounterEffect = false,
    bool IsSkippable = false,
    bool CounterSelectionConsumesOptional = false)
{
    public SelectionRequest? PendingSelectionRequest => OptionalSelectionRequest ?? SelectionRequest;
}
