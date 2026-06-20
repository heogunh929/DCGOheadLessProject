using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record RuleProcessorOptions(int MaxIterations = 32);

public sealed record RuleProcessorResult(int Iterations, int ChangesApplied);

public sealed class RuleProcessor
{
    private readonly PhaseRunner _phaseRunner;
    private readonly BattleResolver _battleResolver;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly RuleProcessorOptions _options;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly EffectiveStatService _effectiveStats;

    public RuleProcessor(
        PhaseRunner? phaseRunner = null,
        BattleResolver? battleResolver = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        EngineInvariantChecker? invariantChecker = null,
        RuleProcessorOptions? options = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _phaseRunner = phaseRunner ?? new PhaseRunner(keywordService: _keywordService, triggerPipelineService: triggerPipelineService);
        _battleResolver = battleResolver ?? new BattleResolver(keywordService: _keywordService, effectiveStats: _effectiveStats);
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _options = options ?? new RuleProcessorOptions();
        _triggerPipelineService = triggerPipelineService;
    }

    internal PhaseRunner RuntimePhaseRunner => _phaseRunner;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public void ProcessAfterAction(GameState state)
    {
        ProcessUntilStable(state);
        if (state.IsGameOver)
        {
            return;
        }

        if (state.Phase == Phase.Main && state.Memory < 0)
        {
            _phaseRunner.EndCurrentTurn(state, Math.Abs(state.Memory));
            ProcessUntilStable(state);
        }
    }

    public RuleProcessorResult ProcessUntilStable(GameState state)
    {
        var totalChanges = 0;

        for (var iteration = 1; iteration <= _options.MaxIterations; iteration++)
        {
            var staleCleanupChanges = _durationCleanupService.CleanupStaleTargets(state).RemovedModifierStableIds.Count;
            _invariantChecker.ThrowIfInvalid(state);

            if (state.IsGameOver)
            {
                return new RuleProcessorResult(iteration, totalChanges);
            }

            var changes = staleCleanupChanges + ProcessSinglePass(state);
            totalChanges += changes;
            _invariantChecker.ThrowIfInvalid(state);

            if (changes == 0)
            {
                return new RuleProcessorResult(iteration, totalChanges);
            }
        }

        throw new UnsupportedMechanicException($"RuleProcessor exceeded max iteration guard '{_options.MaxIterations}'.");
    }

    private int ProcessSinglePass(GameState state)
    {
        RunRulesTiming(state);
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
                RunOnDestroyedAnyone(state, permanentId, controller, topCard, destroyedByDpZero: true);
            }
        }

        return invalidOrFaceDown.Length + dpZero.Length + TrimExcessLinkedCards(state);
    }

    private void RunRulesTiming(GameState state)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(state, EffectTiming.RulesTiming, state.TurnPlayerId);
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"RulesTiming requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }

    private void RunOnDestroyedAnyone(
        GameState state,
        PermanentId destroyedPermanent,
        PlayerId destroyedController,
        CardInstanceId destroyedTopCard,
        bool destroyedByDpZero)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(
            state,
            EffectTiming.OnDestroyedAnyone,
            state.TurnPlayerId,
            values: new Dictionary<string, object?>
            {
                ["DestroyedPermanent"] = destroyedPermanent,
                ["DestroyedController"] = destroyedController,
                ["DestroyedTopCard"] = destroyedTopCard,
                ["DestroyedByDpZero"] = destroyedByDpZero,
            });
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"OnDestroyedAnyone requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
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
                new ZoneMover().MoveCard(
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
}
