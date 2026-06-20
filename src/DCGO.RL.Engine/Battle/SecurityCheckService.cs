using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record SecurityCheckResult(
    PlayerId Defender,
    CardInstanceId? CheckedCard,
    BattleResolutionResult? BattleResult)
{
    public IReadOnlyList<CardInstanceId> CheckedCards { get; init; } =
        CheckedCard is null ? Array.Empty<CardInstanceId>() : new[] { CheckedCard.Value };

    public IReadOnlyList<BattleResolutionResult> BattleResults { get; init; } =
        BattleResult is null ? Array.Empty<BattleResolutionResult>() : new[] { BattleResult };

    public IReadOnlyList<SecurityEffectExecutionResult> SecurityEffectResults { get; init; } =
        Array.Empty<SecurityEffectExecutionResult>();

    public IReadOnlyList<TriggerPipelineResult> SecurityTimingResults { get; init; } =
        Array.Empty<TriggerPipelineResult>();
}

public sealed record SecurityCheckContinuation(
    PermanentId Attacker,
    PlayerId Defender,
    int RemainingChecks,
    CardInstanceId CurrentSecurityCard,
    IReadOnlyList<CardInstanceId> CheckedCards,
    IReadOnlyList<BattleResolutionResult> BattleResults,
    IReadOnlyList<SecurityEffectExecutionResult> SecurityEffectResults,
    IReadOnlyList<TriggerPipelineResult> SecurityTimingResults,
    SecurityEffectExecutionContinuation? EffectContinuation = null,
    TriggerPipelineContinuation? TriggerContinuation = null)
{
    public EffectResolution PendingResolution =>
        EffectContinuation?.PendingResolution
        ?? TriggerContinuation?.PendingResolution
        ?? throw new DomainException("Security check continuation has no pending resolution.");

    public SelectionRequest PendingSelectionRequest =>
        EffectContinuation?.PendingSelectionRequest
        ?? TriggerContinuation?.PendingSelectionRequest
        ?? throw new DomainException("Security check continuation has no pending selection request.");

    public string StableContinuationId =>
        EffectContinuation?.StableContinuationId
        ?? TriggerContinuation?.StableContinuationId
        ?? throw new DomainException("Security check continuation has no stable continuation id.");
}

public sealed record SecurityCheckExecutionResult(
    SecurityCheckResult? Result = null,
    SecurityCheckContinuation? Continuation = null)
{
    public bool HasPendingSelection => Continuation is not null;

    public SelectionRequest? PendingSelectionRequest => Continuation?.PendingSelectionRequest;

    public EffectResolution? PendingResolution => Continuation?.PendingResolution;
}

public sealed class SecurityCheckService
{
    private readonly IZoneMover _zoneMover;
    private readonly BattleResolver _battleResolver;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly SecurityEffectExecutionService? _securityEffectExecutionService;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly EffectiveStatService _effectiveStats;

    public SecurityCheckService(
        IZoneMover? zoneMover = null,
        BattleResolver? battleResolver = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        SecurityEffectExecutionService? securityEffectExecutionService = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _battleResolver = battleResolver ?? new BattleResolver(_zoneMover, _keywordService, _effectiveStats);
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _securityEffectExecutionService = securityEffectExecutionService;
        _triggerPipelineService = triggerPipelineService;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal SecurityEffectExecutionService? RuntimeSecurityEffectExecutionService => _securityEffectExecutionService;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public SecurityCheckResult CheckSecurity(GameState state, PermanentId attackerId, PlayerId defenderId, GameTrace? trace = null)
    {
        var snapshot = state.Clone();
        var traceCount = trace?.Events.Count ?? 0;
        var result = CheckSecurityWithResult(state, attackerId, defenderId, trace);
        if (result.HasPendingSelection)
        {
            RestoreAfterPendingSynchronousCall(state, snapshot, trace, traceCount);
            throw new DomainException(
                $"Security check requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
        }

        return result.Result
            ?? throw new DomainException("Completed security check result is missing.");
    }

    public SecurityCheckExecutionResult CheckSecurityWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        GameTrace? trace = null)
    {
        var defender = state.GetPlayer(defenderId);
        if (defender.Security.Count == 0)
        {
            _durationCleanupService.CleanupSecurityCheckEnd(state);
            return new SecurityCheckExecutionResult(new SecurityCheckResult(defenderId, null, null));
        }

        var attacker = BattleRules.Permanent(state, attackerId);
        var checkCount = _keywordService.SecurityAttackCount(state, attacker);
        return ContinueChecksWithResult(
            state,
            attackerId,
            defenderId,
            checkCount,
            new List<CardInstanceId>(),
            new List<BattleResolutionResult>(),
            new List<SecurityEffectExecutionResult>(),
            new List<TriggerPipelineResult>(),
            trace);
    }

    public SecurityCheckExecutionResult CompleteSecurityContinuationWithResult(
        GameState state,
        SecurityCheckContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);
        ArgumentNullException.ThrowIfNull(selectionResult);

        var checkedCards = continuation.CheckedCards.ToList();
        var battleResults = continuation.BattleResults.ToList();
        var securityEffectResults = continuation.SecurityEffectResults.ToList();
        var securityTimingResults = continuation.SecurityTimingResults.ToList();

        if (continuation.TriggerContinuation is not null)
        {
            if (_triggerPipelineService is null)
            {
                throw new DomainException("Security timing continuation requires TriggerPipelineService.");
            }

            var triggerResult = _triggerPipelineService.Resume(
                state,
                continuation.TriggerContinuation,
                selectionResult,
                trace);
            if (triggerResult.HasPendingSelection)
            {
                return PendingTrigger(
                    continuation.Attacker,
                    continuation.Defender,
                    continuation.RemainingChecks,
                    continuation.CurrentSecurityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    triggerResult);
            }

            securityTimingResults.Add(triggerResult);
            return continuation.TriggerContinuation.Context.Timing switch
            {
                EffectTiming.OnSecurityCheck => ContinueSecurityTimingAndBattleWithResult(
                    state,
                    continuation.Attacker,
                    continuation.Defender,
                    continuation.RemainingChecks,
                    continuation.CurrentSecurityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    EffectTiming.OnLoseSecurity,
                    trace),

                EffectTiming.OnLoseSecurity => ContinueAfterSecurityTimingWithResult(
                    state,
                    continuation.Attacker,
                    continuation.Defender,
                    continuation.RemainingChecks,
                    continuation.CurrentSecurityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    trace),

                var timing => throw new DomainException(
                    $"Security timing continuation cannot resume timing '{timing}'."),
            };
        }

        if (continuation.EffectContinuation is null)
        {
            throw new DomainException("Security effect continuation is missing.");
        }

        if (_securityEffectExecutionService is null)
        {
            throw new DomainException("Security effect continuation requires SecurityEffectExecutionService.");
        }

        var securityEffect = _securityEffectExecutionService.ResumeSecurityEffects(
            state,
            continuation.EffectContinuation,
            selectionResult,
            trace);

        if (securityEffect.HasPendingSelection)
        {
            return Pending(
                continuation.Attacker,
                continuation.Defender,
                continuation.RemainingChecks,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                securityEffect);
        }

        securityEffectResults.Add(securityEffect);
        return ContinueSecurityTimingAndBattleWithResult(
            state,
            continuation.Attacker,
            continuation.Defender,
            continuation.RemainingChecks,
            continuation.CurrentSecurityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            EffectTiming.OnSecurityCheck,
            trace);
    }

    private SecurityCheckExecutionResult ContinueChecksWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int remainingChecks,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        var defender = state.GetPlayer(defenderId);
        for (var i = 0; i < remainingChecks && defender.Security.Count > 0; i++)
        {
            if (!AttackerCanContinue(state, attackerId))
            {
                break;
            }

            var securityCard = defender.Security[0];
            var wasFaceDown = !state.Cards[securityCard].IsFaceUp;
            checkedCards.Add(securityCard);

            MoveSecurityToExecuting(state, defenderId, securityCard, trace);

            if (_securityEffectExecutionService is not null)
            {
                var securityEffect = _securityEffectExecutionService.ExecuteSecurityEffectsWithResult(
                    state,
                    securityCard,
                    defenderId,
                    wasFaceDown,
                    trace);
                if (securityEffect.HasPendingSelection)
                {
                    return Pending(
                        attackerId,
                        defenderId,
                        remainingChecks - i - 1,
                        securityCard,
                        checkedCards,
                        battleResults,
                        securityEffectResults,
                        securityTimingResults,
                        securityEffect);
                }

                securityEffectResults.Add(securityEffect);
            }

            var timingResult = ContinueSecurityTimingAndBattleWithResult(
                state,
                attackerId,
                defenderId,
                remainingChecks - i - 1,
                securityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                EffectTiming.OnSecurityCheck,
                trace);
            if (timingResult.HasPendingSelection)
            {
                return timingResult;
            }
        }

        _durationCleanupService.CleanupSecurityCheckEnd(state);
        return new SecurityCheckExecutionResult(BuildResult(defenderId, checkedCards, battleResults, securityEffectResults, securityTimingResults));
    }

    private SecurityCheckExecutionResult ContinueSecurityTimingAndBattleWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int remainingChecks,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        EffectTiming nextTiming,
        GameTrace? trace)
    {
        if (nextTiming == EffectTiming.OnSecurityCheck)
        {
            var onSecurityCheck = RunSecurityTimingWithResult(
                state,
                EffectTiming.OnSecurityCheck,
                attackerId,
                defenderId,
                securityCard,
                trace);
            if (onSecurityCheck?.HasPendingSelection == true)
            {
                return PendingTrigger(
                    attackerId,
                    defenderId,
                    remainingChecks,
                    securityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    onSecurityCheck);
            }

            if (onSecurityCheck is not null)
            {
                securityTimingResults.Add(onSecurityCheck);
            }

            nextTiming = EffectTiming.OnLoseSecurity;
        }

        if (nextTiming == EffectTiming.OnLoseSecurity)
        {
            var onLoseSecurity = RunSecurityTimingWithResult(
                state,
                EffectTiming.OnLoseSecurity,
                attackerId,
                defenderId,
                securityCard,
                trace);
            if (onLoseSecurity?.HasPendingSelection == true)
            {
                return PendingTrigger(
                    attackerId,
                    defenderId,
                    remainingChecks,
                    securityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    onLoseSecurity);
            }

            if (onLoseSecurity is not null)
            {
                securityTimingResults.Add(onLoseSecurity);
            }

            return ContinueAfterSecurityTimingWithResult(
                state,
                attackerId,
                defenderId,
                remainingChecks,
                securityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                trace);
        }

        throw new DomainException($"Unsupported security timing continuation '{nextTiming}'.");
    }

    private SecurityCheckExecutionResult ContinueAfterSecurityTimingWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int remainingChecks,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        var battle = ResolveSecurityBattleAndTrashIfNeeded(state, attackerId, defenderId, securityCard, trace);
        if (battle is not null)
        {
            battleResults.Add(battle);
        }

        return ContinueChecksWithResult(
            state,
            attackerId,
            defenderId,
            remainingChecks,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            trace);
    }

    private TriggerPipelineResult? RunSecurityTimingWithResult(
        GameState state,
        EffectTiming timing,
        PermanentId attackerId,
        PlayerId defenderId,
        CardInstanceId securityCard,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(
            state,
            timing,
            defenderId,
            values: new Dictionary<string, object?>
            {
                ["AttackingPermanent"] = attackerId,
                ["Attacker"] = attackerId,
                ["Card"] = securityCard,
                ["SecurityCard"] = securityCard,
                ["Player"] = defenderId,
                ["Defender"] = defenderId,
                ["CardEffect"] = null,
            },
            options: new TriggerPipelineOptions(
                TriggerSourceZone.FieldTop
                | TriggerSourceZone.Inherited
                | TriggerSourceZone.Hand
                | TriggerSourceZone.Trash
                | TriggerSourceZone.FaceUpSecurity),
            trace: trace);
    }

    private BattleResolutionResult? ResolveSecurityBattleAndTrashIfNeeded(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        CardInstanceId securityCard,
        GameTrace? trace)
    {
        BattleResolutionResult? battle = null;
        if (AttackerCanContinue(state, attackerId)
            && state.Cards.TryGetValue(securityCard, out var checkedCard)
            && checkedCard.CurrentZone == Zone.Executing
            && BattleRules.IsDigimon(state, securityCard)
            && _effectiveStats.SecurityDp(state, securityCard) > 0)
        {
            battle = _battleResolver.ResolveSecurityBattle(state, attackerId, securityCard, trace);
        }

        TrashIfStillExecuting(state, defenderId, securityCard, trace);
        return battle;
    }

    private void MoveSecurityToExecuting(
        GameState state,
        PlayerId defenderId,
        CardInstanceId securityCard,
        GameTrace? trace)
    {
        var command = new MoveCardCommand(securityCard, Zone.Security, Zone.Executing, MoveReason.Battle, FaceUp: true);
        var before = trace is null ? null : state.Clone();
        var result = _zoneMover.MoveCard(state, command);
        trace?.AddMove($"security-check-execute:{defenderId.Value}:{securityCard.Value}", before!, state, command, result);
    }

    private void TrashIfStillExecuting(
        GameState state,
        PlayerId defenderId,
        CardInstanceId securityCard,
        GameTrace? trace)
    {
        if (!state.Cards.TryGetValue(securityCard, out var instance) || instance.CurrentZone != Zone.Executing)
        {
            return;
        }

        var command = new MoveCardCommand(securityCard, Zone.Executing, Zone.Trash, MoveReason.Battle, FaceUp: true);
        var before = trace is null ? null : state.Clone();
        var result = _zoneMover.MoveCard(state, command);
        trace?.AddMove($"security-check-trash:{defenderId.Value}:{securityCard.Value}", before!, state, command, result);
    }

    private bool AttackerCanContinue(GameState state, PermanentId attackerId)
    {
        var attacker = state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == attackerId);

        return attacker is not null
            && !attacker.IsBreedingArea
            && BattleRules.IsDigimon(state, attacker.TopCardId)
            && _effectiveStats.Dp(state, attacker) > 0;
    }

    private static SecurityCheckResult BuildResult(
        PlayerId defenderId,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
        IReadOnlyList<TriggerPipelineResult> securityTimingResults) =>
        new(
            defenderId,
            checkedCards.Count == 0 ? null : checkedCards[0],
            battleResults.Count == 0 ? null : battleResults[0])
        {
            CheckedCards = checkedCards.ToArray(),
            BattleResults = battleResults.ToArray(),
            SecurityEffectResults = securityEffectResults.ToArray(),
            SecurityTimingResults = securityTimingResults.ToArray(),
        };

    private static SecurityCheckExecutionResult Pending(
        PermanentId attacker,
        PlayerId defender,
        int remainingChecks,
        CardInstanceId currentSecurityCard,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
        IReadOnlyList<TriggerPipelineResult> securityTimingResults,
        SecurityEffectExecutionResult securityEffect)
    {
        if (securityEffect.Continuation is null)
        {
            throw new DomainException("Pending security effect result requires a continuation.");
        }

        return new SecurityCheckExecutionResult(
            Continuation: new SecurityCheckContinuation(
                attacker,
                defender,
                remainingChecks,
                currentSecurityCard,
                checkedCards.ToArray(),
                battleResults.ToArray(),
                securityEffectResults.ToArray(),
                securityTimingResults.ToArray(),
                securityEffect.Continuation,
                TriggerContinuation: null));
    }

    private static SecurityCheckExecutionResult PendingTrigger(
        PermanentId attacker,
        PlayerId defender,
        int remainingChecks,
        CardInstanceId currentSecurityCard,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
        IReadOnlyList<TriggerPipelineResult> securityTimingResults,
        TriggerPipelineResult triggerResult)
    {
        if (triggerResult.PendingContinuation is null)
        {
            throw new DomainException("Pending security timing result requires a continuation.");
        }

        return new SecurityCheckExecutionResult(
            Continuation: new SecurityCheckContinuation(
                attacker,
                defender,
                remainingChecks,
                currentSecurityCard,
                checkedCards.ToArray(),
                battleResults.ToArray(),
                securityEffectResults.ToArray(),
                securityTimingResults.ToArray(),
                EffectContinuation: null,
                triggerResult.PendingContinuation));
    }

    private static void RestoreAfterPendingSynchronousCall(
        GameState state,
        GameState snapshot,
        GameTrace? trace,
        int traceCount)
    {
        state.RestoreFrom(snapshot);
        trace?.Truncate(traceCount);
    }
}
