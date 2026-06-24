using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record PhaseExecutionResult(
    EffectTiming Timing,
    TriggerPipelineResult? TriggerResult,
    PhaseExecutionContinuation? Continuation = null)
{
    public bool HasPendingSelection => TriggerResult?.HasPendingSelection == true;

    public SelectionRequest? PendingSelectionRequest => TriggerResult?.PendingSelectionRequest;

    public EffectResolution? PendingResolution => TriggerResult?.PendingResolution;

    public TriggerPipelineContinuation? PendingContinuation => TriggerResult?.PendingContinuation;
}

public enum PhaseExecutionContinuationKind
{
    CompleteEndCurrentTurn,
}

public sealed record PhaseExecutionContinuation(
    PhaseExecutionContinuationKind Kind,
    int MemoryForNextPlayer);

public sealed class PhaseRunner
{
    private readonly DrawService _drawService;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly Tier1PrimitiveService _primitives;

    public PhaseRunner(
        DrawService? drawService = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        TriggerPipelineService? triggerPipelineService = null,
        Tier1PrimitiveService? primitiveService = null)
    {
        _drawService = drawService ?? new DrawService();
        _keywordService = keywordService ?? BattleKeywordService.Default;
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _triggerPipelineService = triggerPipelineService;
        _primitives = primitiveService ?? new Tier1PrimitiveService();
    }

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public void RunActivePhase(GameState state)
    {
        var result = RunActivePhaseWithResult(state);
        ThrowIfPendingSelection(result);
    }

    public PhaseExecutionResult RunActivePhaseWithResult(GameState state, GameTrace? trace = null)
    {
        state.TurnCount++;
        state.Phase = Phase.Active;

        foreach (var permanent in state.GetPlayer(state.TurnPlayerId).FieldPermanents)
        {
            _primitives.Unsuspend(state, permanent.Id);
        }

        foreach (var permanent in state.GetPlayer(state.NonTurnPlayerId).FieldPermanents)
        {
            if (_keywordService.HasKeyword(state, permanent, BattleKeyword.Reboot))
            {
                _primitives.Unsuspend(state, permanent.Id);
            }
        }

        _durationCleanupService.CleanupOwnerActivePhaseEnd(state, state.TurnPlayerId);
        var triggerResult = RunTriggerPipelineWithResult(state, EffectTiming.OnStartTurn, state.TurnPlayerId, trace);
        return new PhaseExecutionResult(EffectTiming.OnStartTurn, triggerResult);
    }

    public DrawPhaseResult RunDrawPhase(GameState state) => _drawService.ExecuteDrawPhase(state);

    public void RunBreedingPhase(GameState state) => state.Phase = Phase.Breeding;

    public void RunMainPhase(GameState state)
    {
        var result = RunMainPhaseWithResult(state);
        ThrowIfPendingSelection(result);
    }

    public PhaseExecutionResult RunMainPhaseWithResult(GameState state, GameTrace? trace = null)
    {
        state.Phase = Phase.Main;
        var triggerResult = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnStartMainPhase,
            state.TurnPlayerId,
            trace);
        return new PhaseExecutionResult(EffectTiming.OnStartMainPhase, triggerResult);
    }

    public void EndCurrentTurn(GameState state, int memoryForNextPlayer)
    {
        var result = EndCurrentTurnWithResult(state, memoryForNextPlayer);
        ThrowIfPendingSelection(result);
    }

    public PhaseExecutionResult EndCurrentTurnWithResult(
        GameState state,
        int memoryForNextPlayer,
        GameTrace? trace = null)
    {
        state.Phase = Phase.End;
        var triggerResult = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnEndTurn,
            state.TurnPlayerId,
            trace);
        if (triggerResult?.HasPendingSelection == true)
        {
            return new PhaseExecutionResult(
                EffectTiming.OnEndTurn,
                triggerResult,
                new PhaseExecutionContinuation(
                    PhaseExecutionContinuationKind.CompleteEndCurrentTurn,
                    memoryForNextPlayer));
        }

        return CompleteEndCurrentTurnAfterEndTriggerWithResult(state, memoryForNextPlayer, trace);
    }

    public PhaseExecutionResult CompleteEndCurrentTurnAfterEndTriggerWithResult(
        GameState state,
        int memoryForNextPlayer,
        GameTrace? trace = null)
    {
        _durationCleanupService.CleanupTurnEnd(state, state.TurnPlayerId);
        state.TurnPlayerId = state.NonTurnPlayerId;
        state.Memory = Math.Max(0, memoryForNextPlayer);
        return RunActivePhaseWithResult(state, trace);
    }

    private void RunTriggerPipeline(GameState state, EffectTiming timing, PlayerId player)
    {
        var result = RunTriggerPipelineWithResult(state, timing, player);
        if (result is not null)
        {
            ThrowIfPendingSelection(result);
        }
    }

    private TriggerPipelineResult? RunTriggerPipelineWithResult(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        GameTrace? trace = null)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(state, timing, player, trace: trace);
    }

    private static void ThrowIfPendingSelection(TriggerPipelineResult result)
    {
        if (result.PendingSelectionRequest is null)
        {
            return;
        }

        throw new DomainException(
            $"Trigger timing '{result.Context.Timing}' requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
    }

    private static void ThrowIfPendingSelection(PhaseExecutionResult result)
    {
        if (result.TriggerResult is null)
        {
            return;
        }

        ThrowIfPendingSelection(result.TriggerResult);
    }
}
