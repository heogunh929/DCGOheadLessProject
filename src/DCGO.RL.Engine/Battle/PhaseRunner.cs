using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;

namespace DCGO.RL.Engine.Battle;

public sealed class PhaseRunner
{
    private readonly DrawService _drawService;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly TriggerPipelineService? _triggerPipelineService;

    public PhaseRunner(
        DrawService? drawService = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        TriggerPipelineService? triggerPipelineService = null)
    {
        _drawService = drawService ?? new DrawService();
        _keywordService = keywordService ?? BattleKeywordService.Default;
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _triggerPipelineService = triggerPipelineService;
    }

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public void RunActivePhase(GameState state)
    {
        state.TurnCount++;
        state.Phase = Phase.Active;

        foreach (var permanent in state.GetPlayer(state.TurnPlayerId).FieldPermanents)
        {
            permanent.IsSuspended = false;
        }

        foreach (var permanent in state.GetPlayer(state.NonTurnPlayerId).FieldPermanents)
        {
            if (_keywordService.HasKeyword(state, permanent, BattleKeyword.Reboot))
            {
                permanent.IsSuspended = false;
            }
        }

        _durationCleanupService.CleanupOwnerActivePhaseEnd(state, state.TurnPlayerId);
        RunTriggerPipeline(state, EffectTiming.OnStartTurn, state.TurnPlayerId);
    }

    public DrawPhaseResult RunDrawPhase(GameState state) => _drawService.ExecuteDrawPhase(state);

    public void RunBreedingPhase(GameState state) => state.Phase = Phase.Breeding;

    public void RunMainPhase(GameState state)
    {
        state.Phase = Phase.Main;
        RunTriggerPipeline(state, EffectTiming.OnStartMainPhase, state.TurnPlayerId);
    }

    public void EndCurrentTurn(GameState state, int memoryForNextPlayer)
    {
        state.Phase = Phase.End;
        RunTriggerPipeline(state, EffectTiming.OnEndTurn, state.TurnPlayerId);
        _durationCleanupService.CleanupTurnEnd(state, state.TurnPlayerId);
        state.TurnPlayerId = state.NonTurnPlayerId;
        state.Memory = Math.Max(0, memoryForNextPlayer);
        RunActivePhase(state);
    }

    private void RunTriggerPipeline(GameState state, EffectTiming timing, PlayerId player)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(state, timing, player);
        ThrowIfPendingSelection(result);
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
}
