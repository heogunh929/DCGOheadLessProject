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

public enum SecurityCheckContinuationStage
{
    None,
    AfterSecuritySkillAutoProcess,
    OnSecurityCheck,
    OnLoseSecurity,
    AfterSecurityTimingAutoProcess,
    AfterCleanupAutoProcess,
}

public sealed record SecurityCheckContinuation(
    PermanentId Attacker,
    PlayerId Defender,
    int ChecksCompleted,
    CardInstanceId CurrentSecurityCard,
    IReadOnlyList<CardInstanceId> CheckedCards,
    IReadOnlyList<BattleResolutionResult> BattleResults,
    IReadOnlyList<SecurityEffectExecutionResult> SecurityEffectResults,
    IReadOnlyList<TriggerPipelineResult> SecurityTimingResults,
    PreparedTriggerGroup? PreparedOnSecurityCheck = null,
    PreparedTriggerGroup? PreparedOnLoseSecurity = null,
    SecurityEffectExecutionContinuation? EffectContinuation = null,
    TriggerPipelineContinuation? TriggerContinuation = null,
    SecurityCheckContinuationStage TriggerStage = SecurityCheckContinuationStage.None)
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
    private static readonly TriggerPipelineOptions SecurityTimingOptions = new(
        TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
        | TriggerSourceZone.Hand
        | TriggerSourceZone.Trash
        | TriggerSourceZone.FaceUpSecurity,
        ResolveAfterEffectsActivate: true);

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
        _ = state.GetPlayer(defenderId);
        return ContinueChecksWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted: 0,
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
            return ResumeSecurityTimingWithResult(
                state,
                continuation,
                selectionResult,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                trace);
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
            return PendingEffect(
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                continuation.PreparedOnSecurityCheck,
                continuation.PreparedOnLoseSecurity,
                securityEffect);
        }

        securityEffectResults.Add(securityEffect);
        return ContinueAfterSecuritySkillWithResult(
            state,
            continuation.Attacker,
            continuation.Defender,
            continuation.ChecksCompleted,
            continuation.CurrentSecurityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            RequirePrepared(continuation.PreparedOnSecurityCheck, EffectTiming.OnSecurityCheck),
            RequirePrepared(continuation.PreparedOnLoseSecurity, EffectTiming.OnLoseSecurity),
            trace);
    }

    private SecurityCheckExecutionResult ContinueChecksWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        if (!CanStartNextCheck(state, attackerId, defenderId, checksCompleted))
        {
            return new SecurityCheckExecutionResult(BuildResult(
                defenderId,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults));
        }

        return StartNextSecurityCheckWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            trace);
    }

    private SecurityCheckExecutionResult StartNextSecurityCheckWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        var defender = state.GetPlayer(defenderId);
        var securityCard = defender.Security[0];
        var wasFaceDown = !state.Cards[securityCard].IsFaceUp;
        var preparedOnSecurityCheck = PrepareSecurityTiming(
            state,
            EffectTiming.OnSecurityCheck,
            attackerId,
            defenderId,
            securityCard);

        MoveSecurityToExecuting(state, defenderId, securityCard, trace);

        var preparedOnLoseSecurity = PrepareSecurityTiming(
            state,
            EffectTiming.OnLoseSecurity,
            attackerId,
            defenderId,
            securityCard);

        checkedCards.Add(securityCard);
        var nextChecksCompleted = checksCompleted + 1;

        if (_securityEffectExecutionService is not null)
        {
            var securityEffect = _securityEffectExecutionService.ExecuteSecurityEffectsWithResult(
                state,
                securityCard,
                defenderId,
                wasFaceDown,
                trace,
                SecurityEffectValues(attackerId, defenderId, securityCard));
            if (securityEffect.HasPendingSelection)
            {
                return PendingEffect(
                    attackerId,
                    defenderId,
                    nextChecksCompleted,
                    securityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    preparedOnSecurityCheck,
                    preparedOnLoseSecurity,
                    securityEffect);
            }

            securityEffectResults.Add(securityEffect);
        }

        return ContinueAfterSecuritySkillWithResult(
            state,
            attackerId,
            defenderId,
            nextChecksCompleted,
            securityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            preparedOnSecurityCheck,
            preparedOnLoseSecurity,
            trace);
    }

    private SecurityCheckExecutionResult ContinueAfterSecuritySkillWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        PreparedTriggerGroup? preparedOnSecurityCheck,
        PreparedTriggerGroup? preparedOnLoseSecurity,
        GameTrace? trace)
    {
        var autoProcess = RunSecurityAutoProcessWithResult(state, trace);
        if (autoProcess?.HasPendingSelection == true)
        {
            return PendingTrigger(
                attackerId,
                defenderId,
                checksCompleted,
                securityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                preparedOnSecurityCheck,
                preparedOnLoseSecurity,
                autoProcess,
                SecurityCheckContinuationStage.AfterSecuritySkillAutoProcess);
        }

        if (autoProcess is not null && HasTriggerData(autoProcess))
        {
            securityTimingResults.Add(autoProcess);
        }

        return ContinueAfterSecuritySkillAutoProcessWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            securityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            preparedOnSecurityCheck,
            preparedOnLoseSecurity,
            trace);
    }

    private SecurityCheckExecutionResult ContinueAfterSecuritySkillAutoProcessWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        PreparedTriggerGroup? preparedOnSecurityCheck,
        PreparedTriggerGroup? preparedOnLoseSecurity,
        GameTrace? trace)
    {
        if (preparedOnSecurityCheck is not null)
        {
            var onSecurityCheck = RunPreparedSecurityTimingWithResult(state, preparedOnSecurityCheck, trace);
            if (onSecurityCheck.HasPendingSelection)
            {
                return PendingTrigger(
                    attackerId,
                    defenderId,
                    checksCompleted,
                    securityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    preparedOnSecurityCheck,
                    preparedOnLoseSecurity,
                    onSecurityCheck,
                    SecurityCheckContinuationStage.OnSecurityCheck);
            }

            securityTimingResults.Add(onSecurityCheck);
        }

        return ContinueAfterOnSecurityCheckWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            securityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
                    securityTimingResults,
                    preparedOnLoseSecurity,
                    trace);
    }

    private SecurityCheckExecutionResult ContinueAfterOnSecurityCheckWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        PreparedTriggerGroup? preparedOnLoseSecurity,
        GameTrace? trace)
    {
        if (preparedOnLoseSecurity is not null)
        {
            var onLoseSecurity = RunPreparedSecurityTimingWithResult(state, preparedOnLoseSecurity, trace);
            if (onLoseSecurity.HasPendingSelection)
            {
                return PendingTrigger(
                    attackerId,
                    defenderId,
                    checksCompleted,
                    securityCard,
                    checkedCards,
                    battleResults,
                    securityEffectResults,
                    securityTimingResults,
                    preparedOnSecurityCheck: null,
                    preparedOnLoseSecurity,
                    onLoseSecurity,
                    SecurityCheckContinuationStage.OnLoseSecurity);
            }

            securityTimingResults.Add(onLoseSecurity);
        }

        return ContinueAfterSecurityTimingWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            securityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            trace);
    }

    private SecurityCheckExecutionResult ContinueAfterSecurityTimingWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
        CardInstanceId securityCard,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        var autoProcess = RunSecurityAutoProcessWithResult(state, trace);
        if (autoProcess?.HasPendingSelection == true)
        {
            return PendingTrigger(
                attackerId,
                defenderId,
                checksCompleted,
                securityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                preparedOnSecurityCheck: null,
                preparedOnLoseSecurity: null,
                autoProcess,
                SecurityCheckContinuationStage.AfterSecurityTimingAutoProcess);
        }

        if (autoProcess is not null && HasTriggerData(autoProcess))
        {
            securityTimingResults.Add(autoProcess);
        }

        return FinishCurrentSecurityCardWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            securityCard,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            trace);
    }

    private SecurityCheckExecutionResult ResumeSecurityTimingWithResult(
        GameState state,
        SecurityCheckContinuation continuation,
        SelectionResult selectionResult,
        List<CardInstanceId> checkedCards,
        List<BattleResolutionResult> battleResults,
        List<SecurityEffectExecutionResult> securityEffectResults,
        List<TriggerPipelineResult> securityTimingResults,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            throw new DomainException("Security timing continuation requires TriggerPipelineService.");
        }

        var triggerResult = _triggerPipelineService.Resume(
            state,
            continuation.TriggerContinuation!,
            selectionResult,
            trace);
        if (triggerResult.HasPendingSelection)
        {
            return PendingTrigger(
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                continuation.PreparedOnSecurityCheck,
                continuation.PreparedOnLoseSecurity,
                triggerResult,
                continuation.TriggerStage);
        }

        securityTimingResults.Add(triggerResult);
        return continuation.TriggerStage switch
        {
            SecurityCheckContinuationStage.AfterSecuritySkillAutoProcess => ContinueAfterSecuritySkillAutoProcessWithResult(
                state,
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                continuation.PreparedOnSecurityCheck,
                continuation.PreparedOnLoseSecurity,
                trace),

            SecurityCheckContinuationStage.OnSecurityCheck => ContinueAfterOnSecurityCheckWithResult(
                state,
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                RequirePrepared(continuation.PreparedOnLoseSecurity, EffectTiming.OnLoseSecurity),
                trace),

            SecurityCheckContinuationStage.OnLoseSecurity => ContinueAfterSecurityTimingWithResult(
                state,
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                trace),

            SecurityCheckContinuationStage.AfterSecurityTimingAutoProcess => FinishCurrentSecurityCardWithResult(
                state,
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                continuation.CurrentSecurityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                trace),

            SecurityCheckContinuationStage.AfterCleanupAutoProcess => ContinueChecksWithResult(
                state,
                continuation.Attacker,
                continuation.Defender,
                continuation.ChecksCompleted,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                trace),

            var stage => throw new DomainException(
                $"Security timing continuation cannot resume stage '{stage}'."),
        };
    }

    private SecurityCheckExecutionResult FinishCurrentSecurityCardWithResult(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted,
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
            _durationCleanupService.CleanupBattleEnd(state);
        }

        _durationCleanupService.CleanupSecurityCheckEnd(state);
        var autoProcess = RunSecurityAutoProcessWithResult(state, trace);
        if (autoProcess?.HasPendingSelection == true)
        {
            return PendingTrigger(
                attackerId,
                defenderId,
                checksCompleted,
                securityCard,
                checkedCards,
                battleResults,
                securityEffectResults,
                securityTimingResults,
                preparedOnSecurityCheck: null,
                preparedOnLoseSecurity: null,
                autoProcess,
                SecurityCheckContinuationStage.AfterCleanupAutoProcess);
        }

        if (autoProcess is not null && HasTriggerData(autoProcess))
        {
            securityTimingResults.Add(autoProcess);
        }

        return ContinueChecksWithResult(
            state,
            attackerId,
            defenderId,
            checksCompleted,
            checkedCards,
            battleResults,
            securityEffectResults,
            securityTimingResults,
            trace);
    }

    private PreparedTriggerGroup? PrepareSecurityTiming(
        GameState state,
        EffectTiming timing,
        PermanentId attackerId,
        PlayerId defenderId,
        CardInstanceId securityCard)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Prepare(
            state,
            timing,
            defenderId,
            values: SecurityTimingValues(state, attackerId, defenderId, securityCard),
            options: SecurityTimingOptions);
    }

    private TriggerPipelineResult RunPreparedSecurityTimingWithResult(
        GameState state,
        PreparedTriggerGroup prepared,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            throw new DomainException("Prepared security timing requires TriggerPipelineService.");
        }

        return _triggerPipelineService.RunPrepared(state, prepared, trace);
    }

    private TriggerPipelineResult? RunSecurityAutoProcessWithResult(GameState state, GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.RunAutoProcess(
            state,
            state.TurnPlayerId,
            options: SecurityTimingOptions,
            trace: trace);
    }

    private static bool HasTriggerData(TriggerPipelineResult result) =>
        result.QueuedEffects.Count > 0
        || result.BackgroundEffects.Count > 0
        || result.ExecutedEffects.Count > 0
        || result.SkippedOptionalEffects.Count > 0
        || result.SelectionApplications.Count > 0
        || result.HasPendingSelection;

    private static IReadOnlyDictionary<string, object?> SecurityTimingValues(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        CardInstanceId securityCard) =>
        new Dictionary<string, object?>
        {
            ["AttackingPermanent"] = attackerId,
            ["Attacker"] = attackerId,
            ["Card"] = securityCard,
            ["SecurityCard"] = securityCard,
            ["Player"] = defenderId,
            ["Defender"] = defenderId,
            ["CardEffect"] = null,
            ["SourceZone"] = state.Cards[securityCard].CurrentZone,
        };

    private static IReadOnlyDictionary<string, object?> SecurityEffectValues(
        PermanentId attackerId,
        PlayerId defenderId,
        CardInstanceId securityCard) =>
        new Dictionary<string, object?>
        {
            ["AttackingPermanent"] = attackerId,
            ["Attacker"] = attackerId,
            ["Card"] = securityCard,
            ["SecurityCard"] = securityCard,
            ["Player"] = defenderId,
            ["Defender"] = defenderId,
        };

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

    private bool CanStartNextCheck(
        GameState state,
        PermanentId attackerId,
        PlayerId defenderId,
        int checksCompleted)
    {
        if (state.GetPlayer(defenderId).Security.Count == 0)
        {
            return false;
        }

        var attacker = FindAttacker(state, attackerId);
        return AttackerCanContinue(state, attacker)
            && _keywordService.SecurityAttackCount(state, attacker!) > checksCompleted;
    }

    private bool AttackerCanContinue(GameState state, PermanentId attackerId) =>
        AttackerCanContinue(state, FindAttacker(state, attackerId));

    private bool AttackerCanContinue(GameState state, PermanentState? attacker) =>
        attacker is not null
        && !attacker.IsBreedingArea
        && BattleRules.IsDigimon(state, attacker.TopCardId)
        && _effectiveStats.Dp(state, attacker) > 0;

    private static PermanentState? FindAttacker(GameState state, PermanentId attackerId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == attackerId);

    private static PreparedTriggerGroup RequirePrepared(PreparedTriggerGroup? prepared, EffectTiming timing) =>
        prepared ?? throw new DomainException($"Security check continuation is missing prepared '{timing}' trigger group.");

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

    private static SecurityCheckExecutionResult PendingEffect(
        PermanentId attacker,
        PlayerId defender,
        int checksCompleted,
        CardInstanceId currentSecurityCard,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
        IReadOnlyList<TriggerPipelineResult> securityTimingResults,
        PreparedTriggerGroup? preparedOnSecurityCheck,
        PreparedTriggerGroup? preparedOnLoseSecurity,
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
                checksCompleted,
                currentSecurityCard,
                checkedCards.ToArray(),
                battleResults.ToArray(),
                securityEffectResults.ToArray(),
                securityTimingResults.ToArray(),
                preparedOnSecurityCheck,
                preparedOnLoseSecurity,
                securityEffect.Continuation,
                TriggerContinuation: null));
    }

    private static SecurityCheckExecutionResult PendingTrigger(
        PermanentId attacker,
        PlayerId defender,
        int checksCompleted,
        CardInstanceId currentSecurityCard,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
        IReadOnlyList<TriggerPipelineResult> securityTimingResults,
        PreparedTriggerGroup? preparedOnSecurityCheck,
        PreparedTriggerGroup? preparedOnLoseSecurity,
        TriggerPipelineResult triggerResult,
        SecurityCheckContinuationStage triggerStage)
    {
        if (triggerResult.PendingContinuation is null)
        {
            throw new DomainException("Pending security timing result requires a continuation.");
        }

        return new SecurityCheckExecutionResult(
            Continuation: new SecurityCheckContinuation(
                attacker,
                defender,
                checksCompleted,
                currentSecurityCard,
                checkedCards.ToArray(),
                battleResults.ToArray(),
                securityEffectResults.ToArray(),
                securityTimingResults.ToArray(),
                preparedOnSecurityCheck,
                preparedOnLoseSecurity,
                EffectContinuation: null,
                triggerResult.PendingContinuation,
                triggerStage));
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
