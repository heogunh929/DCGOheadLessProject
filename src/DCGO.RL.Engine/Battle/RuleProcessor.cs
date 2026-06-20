using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record RuleProcessorOptions(int MaxIterations = 32);

public sealed record RuleProcessorResult(int Iterations, int ChangesApplied);

public enum RuleProcessorContinuationKind
{
    ContinueAfterRulesTiming,
    ContinueAfterOnDestroyedAnyone,
}

public sealed record RuleProcessorContinuation(RuleProcessorContinuationKind Kind);

public sealed record RuleProcessorExecutionResult(
    RuleProcessorResult? Result = null,
    PhaseExecutionResult? PhaseExecution = null,
    TriggerPipelineResult? TriggerResult = null,
    RuleProcessorContinuation? Continuation = null)
{
    public bool HasPendingSelection =>
        TriggerResult?.HasPendingSelection == true
        || PhaseExecution?.HasPendingSelection == true;

    public SelectionRequest? PendingSelectionRequest =>
        TriggerResult?.PendingSelectionRequest
        ?? PhaseExecution?.PendingSelectionRequest;

    public EffectResolution? PendingResolution =>
        TriggerResult?.PendingResolution
        ?? PhaseExecution?.PendingResolution;

    public TriggerPipelineContinuation? PendingContinuation =>
        TriggerResult?.PendingContinuation
        ?? PhaseExecution?.PendingContinuation;
}

public sealed class RuleProcessor
{
    private readonly IZoneMover _zoneMover;
    private readonly PhaseRunner _phaseRunner;
    private readonly BattleResolver _battleResolver;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly RuleProcessorOptions _options;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly EffectiveStatService _effectiveStats;

    public RuleProcessor(
        IZoneMover? zoneMover = null,
        PhaseRunner? phaseRunner = null,
        BattleResolver? battleResolver = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        EngineInvariantChecker? invariantChecker = null,
        RuleProcessorOptions? options = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _zoneMover = zoneMover ?? throw new ArgumentNullException(nameof(zoneMover));
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _phaseRunner = phaseRunner ?? new PhaseRunner(keywordService: _keywordService, triggerPipelineService: triggerPipelineService);
        _battleResolver = battleResolver ?? new BattleResolver(keywordService: _keywordService, effectiveStats: _effectiveStats);
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _options = options ?? new RuleProcessorOptions();
        _triggerPipelineService = triggerPipelineService;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal PhaseRunner RuntimePhaseRunner => _phaseRunner;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public void ProcessAfterAction(GameState state)
    {
        var result = ProcessAfterActionWithResult(state);
        ThrowIfPending(result, "ProcessAfterAction");
    }

    public RuleProcessorExecutionResult ProcessAfterActionWithResult(GameState state, GameTrace? trace = null) =>
        ProcessAfterActionWithResult(state, trace, skipInitialRulesTiming: false);

    public RuleProcessorExecutionResult CompleteRuleContinuationWithResult(
        GameState state,
        RuleProcessorContinuation continuation,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);

        return continuation.Kind switch
        {
            RuleProcessorContinuationKind.ContinueAfterRulesTiming
                or RuleProcessorContinuationKind.ContinueAfterOnDestroyedAnyone =>
                ProcessAfterActionWithResult(state, trace, skipInitialRulesTiming: true),

            _ => throw new UnsupportedMechanicException($"RuleProcessor continuation '{continuation.Kind}'"),
        };
    }

    private RuleProcessorExecutionResult ProcessAfterActionWithResult(
        GameState state,
        GameTrace? trace,
        bool skipInitialRulesTiming)
    {
        var result = ProcessUntilStableWithResult(state, trace, skipInitialRulesTiming);
        if (result.HasPendingSelection || state.IsGameOver)
        {
            return result;
        }

        if (state.IsGameOver)
        {
            return result;
        }

        if (state.Phase == Phase.Main && state.Memory < 0)
        {
            var phaseResult = _phaseRunner.EndCurrentTurnWithResult(state, Math.Abs(state.Memory), trace);
            if (phaseResult.HasPendingSelection)
            {
                return new RuleProcessorExecutionResult(PhaseExecution: phaseResult);
            }

            return ProcessUntilStableWithResult(state, trace, skipInitialRulesTiming: false);
        }

        return result;
    }

    public RuleProcessorResult ProcessUntilStable(GameState state)
    {
        var result = ProcessUntilStableWithResult(state);
        ThrowIfPending(result, "ProcessUntilStable");

        return result.Result
            ?? throw new DomainException("Completed RuleProcessor result is missing.");
    }

    public RuleProcessorExecutionResult ProcessUntilStableWithResult(
        GameState state,
        GameTrace? trace = null,
        bool skipInitialRulesTiming = false)
    {
        var totalChanges = 0;

        for (var iteration = 1; iteration <= _options.MaxIterations; iteration++)
        {
            var staleCleanupChanges = _durationCleanupService.CleanupStaleTargets(state).RemovedModifierStableIds.Count;
            _invariantChecker.ThrowIfInvalid(state);

            if (state.IsGameOver)
            {
                return new RuleProcessorExecutionResult(new RuleProcessorResult(iteration, totalChanges));
            }

            var passResult = ProcessSinglePassWithResult(
                state,
                trace,
                skipRulesTiming: skipInitialRulesTiming && iteration == 1);
            if (passResult.HasPendingSelection)
            {
                return new RuleProcessorExecutionResult(
                    new RuleProcessorResult(iteration, totalChanges + staleCleanupChanges + passResult.ChangesApplied),
                    TriggerResult: passResult.TriggerResult,
                    Continuation: passResult.Continuation);
            }

            var changes = staleCleanupChanges + passResult.ChangesApplied;
            totalChanges += changes;
            _invariantChecker.ThrowIfInvalid(state);

            if (changes == 0)
            {
                return new RuleProcessorExecutionResult(new RuleProcessorResult(iteration, totalChanges));
            }
        }

        throw new UnsupportedMechanicException($"RuleProcessor exceeded max iteration guard '{_options.MaxIterations}'.");
    }

    private RuleProcessorSinglePassResult ProcessSinglePassWithResult(
        GameState state,
        GameTrace? trace,
        bool skipRulesTiming)
    {
        if (!skipRulesTiming)
        {
            var rulesTiming = RunRulesTimingWithResult(state, trace);
            if (rulesTiming?.HasPendingSelection == true)
            {
                return RuleProcessorSinglePassResult.Pending(
                    rulesTiming,
                    new RuleProcessorContinuation(RuleProcessorContinuationKind.ContinueAfterRulesTiming));
            }
        }

        _keywordService.EnsureSupportedKeywords(state);

        var invalidOrFaceDown = state.Players
            .SelectMany(player => player.FieldPermanents)
            .Where(permanent =>
                IsInvalidBreedingPermanent(state, permanent)
                || IsFaceDownPermanent(state, permanent))
            .Select(permanent => permanent.Id)
            .Distinct()
            .ToArray();

        var dpZero = state.Players
            .SelectMany(player => player.FieldPermanents)
            .Where(permanent => IsDpZeroOrBelowPermanent(state, permanent))
            .Select(permanent => permanent.Id)
            .Distinct()
            .Except(invalidOrFaceDown)
            .ToArray();

        foreach (var permanentId in invalidOrFaceDown)
        {
            if (TryFindPermanent(state, permanentId, out var permanent))
            {
                _battleResolver.DestroyPermanent(state, permanent);
            }
        }

        foreach (var permanentId in dpZero)
        {
            if (TryFindPermanent(state, permanentId, out var permanent))
            {
                var controller = permanent.ControllerPlayerId;
                var topCard = permanent.TopCardId;
                _battleResolver.DestroyPermanent(state, permanent);
                var destroyedTrigger = RunOnDestroyedAnyoneWithResult(
                    state,
                    permanentId,
                    controller,
                    topCard,
                    destroyedByDpZero: true,
                    trace);
                if (destroyedTrigger?.HasPendingSelection == true)
                {
                    return RuleProcessorSinglePassResult.Pending(
                        destroyedTrigger,
                        new RuleProcessorContinuation(RuleProcessorContinuationKind.ContinueAfterOnDestroyedAnyone),
                        invalidOrFaceDown.Length + 1);
                }
            }
        }

        return RuleProcessorSinglePassResult.Completed(
            invalidOrFaceDown.Length + dpZero.Length + TrimExcessLinkedCards(state));
    }

    private TriggerPipelineResult? RunRulesTimingWithResult(GameState state, GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(state, EffectTiming.RulesTiming, state.TurnPlayerId, trace: trace);
    }

    private TriggerPipelineResult? RunOnDestroyedAnyoneWithResult(
        GameState state,
        PermanentId destroyedPermanent,
        PlayerId destroyedController,
        CardInstanceId destroyedTopCard,
        bool destroyedByDpZero,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(
            state,
            EffectTiming.OnDestroyedAnyone,
            state.TurnPlayerId,
            values: new Dictionary<string, object?>
            {
                ["DestroyedPermanent"] = destroyedPermanent,
                ["DestroyedController"] = destroyedController,
                ["DestroyedTopCard"] = destroyedTopCard,
                ["DestroyedByDpZero"] = destroyedByDpZero,
            },
            trace: trace);
    }

    private static void ThrowIfPending(RuleProcessorExecutionResult result, string operation)
    {
        if (!result.HasPendingSelection)
        {
            return;
        }

        throw new DomainException(
            $"{operation} requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
    }

    private static bool IsInvalidBreedingPermanent(GameState state, PermanentState permanent) =>
        permanent.IsBreedingArea && !BattleRules.IsDigimon(state, permanent.TopCardId);

    private bool IsDpZeroOrBelowPermanent(GameState state, PermanentState permanent) =>
        BattleRules.IsDigimon(state, permanent.TopCardId) && _effectiveStats.Dp(state, permanent) <= 0;

    private static bool IsFaceDownPermanent(GameState state, PermanentState permanent) =>
        state.Cards.TryGetValue(permanent.TopCardId, out var topCard) && !topCard.IsFaceUp;

    private int TrimExcessLinkedCards(GameState state)
    {
        var changes = 0;
        foreach (var permanent in state.Players.SelectMany(player => player.FieldPermanents).ToArray())
        {
            var max = Math.Max(0, BattleRules.Definition(state, permanent.TopCardId).LinkedMax);
            while (permanent.LinkedCards.Count > max)
            {
                var linked = permanent.LinkedCards[^1];
                _zoneMover.MoveCard(
                    state,
                    new MoveCardCommand(
                        linked,
                        Zone.LinkedCards,
                        Zone.Trash,
                        MoveReason.Trash,
                        SourcePermanent: permanent.Id));
                changes++;
            }
        }

        return changes;
    }

    private static bool TryFindPermanent(GameState state, PermanentId permanentId, out PermanentState permanent)
    {
        foreach (var candidate in state.Players.SelectMany(player => player.FieldPermanents))
        {
            if (candidate.Id == permanentId)
            {
                permanent = candidate;
                return true;
            }
        }

        permanent = null!;
        return false;
    }

    private sealed record RuleProcessorSinglePassResult(
        int ChangesApplied,
        TriggerPipelineResult? TriggerResult = null,
        RuleProcessorContinuation? Continuation = null)
    {
        public bool HasPendingSelection => TriggerResult?.HasPendingSelection == true;

        public static RuleProcessorSinglePassResult Completed(int changesApplied) =>
            new(changesApplied);

        public static RuleProcessorSinglePassResult Pending(
            TriggerPipelineResult result,
            RuleProcessorContinuation continuation,
            int changesApplied = 0) =>
            new(changesApplied, result, continuation);
    }
}
