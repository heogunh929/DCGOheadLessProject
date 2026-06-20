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
}

public sealed record SecurityCheckContinuation(
    PermanentId Attacker,
    PlayerId Defender,
    int RemainingChecks,
    CardInstanceId CurrentSecurityCard,
    IReadOnlyList<CardInstanceId> CheckedCards,
    IReadOnlyList<BattleResolutionResult> BattleResults,
    IReadOnlyList<SecurityEffectExecutionResult> SecurityEffectResults,
    SecurityEffectExecutionContinuation EffectContinuation)
{
    public EffectResolution PendingResolution => EffectContinuation.PendingResolution;

    public SelectionRequest PendingSelectionRequest => EffectContinuation.PendingSelectionRequest;

    public string StableContinuationId => EffectContinuation.StableContinuationId;
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
    private readonly EffectiveStatService _effectiveStats;

    public SecurityCheckService(
        IZoneMover? zoneMover = null,
        BattleResolver? battleResolver = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        SecurityEffectExecutionService? securityEffectExecutionService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _battleResolver = battleResolver ?? new BattleResolver(_zoneMover, _keywordService, _effectiveStats);
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _securityEffectExecutionService = securityEffectExecutionService;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal SecurityEffectExecutionService? RuntimeSecurityEffectExecutionService => _securityEffectExecutionService;

    public SecurityCheckResult CheckSecurity(GameState state, PermanentId attackerId, PlayerId defenderId, GameTrace? trace = null)
    {
        var result = CheckSecurityWithResult(state, attackerId, defenderId, trace);
        if (result.HasPendingSelection)
        {
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

        if (_securityEffectExecutionService is null)
        {
            throw new DomainException("Security effect continuation requires SecurityEffectExecutionService.");
        }

        var checkedCards = continuation.CheckedCards.ToList();
        var battleResults = continuation.BattleResults.ToList();
        var securityEffectResults = continuation.SecurityEffectResults.ToList();
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
                securityEffect);
        }

        securityEffectResults.Add(securityEffect);
        var battle = ResolveSecurityBattleAndTrashIfNeeded(
            state,
            continuation.Attacker,
            continuation.Defender,
            continuation.CurrentSecurityCard,
            trace);
        if (battle is not null)
        {
            battleResults.Add(battle);
        }

        return ContinueChecksWithResult(
            state,
            continuation.Attacker,
            continuation.Defender,
            continuation.RemainingChecks,
            checkedCards,
            battleResults,
            securityEffectResults,
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
                        securityEffect);
                }

                securityEffectResults.Add(securityEffect);
            }

            var battle = ResolveSecurityBattleAndTrashIfNeeded(state, attackerId, defenderId, securityCard, trace);
            if (battle is not null)
            {
                battleResults.Add(battle);
            }
        }

        _durationCleanupService.CleanupSecurityCheckEnd(state);
        return new SecurityCheckExecutionResult(BuildResult(defenderId, checkedCards, battleResults, securityEffectResults));
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
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults) =>
        new(
            defenderId,
            checkedCards.Count == 0 ? null : checkedCards[0],
            battleResults.Count == 0 ? null : battleResults[0])
        {
            CheckedCards = checkedCards.ToArray(),
            BattleResults = battleResults.ToArray(),
            SecurityEffectResults = securityEffectResults.ToArray(),
        };

    private static SecurityCheckExecutionResult Pending(
        PermanentId attacker,
        PlayerId defender,
        int remainingChecks,
        CardInstanceId currentSecurityCard,
        IReadOnlyList<CardInstanceId> checkedCards,
        IReadOnlyList<BattleResolutionResult> battleResults,
        IReadOnlyList<SecurityEffectExecutionResult> securityEffectResults,
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
                securityEffect.Continuation));
    }
}
