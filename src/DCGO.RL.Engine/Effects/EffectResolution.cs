using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

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
    string? OncePerTurnKey = null)
{
    public SelectionRequest? PendingSelectionRequest => SelectionRequest ?? OptionalSelectionRequest;
}
