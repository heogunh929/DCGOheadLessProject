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
        var defender = state.GetPlayer(defenderId);
        var checkedCards = new List<CardInstanceId>();
        var battleResults = new List<BattleResolutionResult>();
        var securityEffectResults = new List<SecurityEffectExecutionResult>();
        CardInstanceId? firstCheckedCard = null;
        BattleResolutionResult? firstBattleResult = null;

        if (defender.Security.Count == 0)
        {
            _durationCleanupService.CleanupSecurityCheckEnd(state);
            return new SecurityCheckResult(defenderId, null, null);
        }

        var attacker = BattleRules.Permanent(state, attackerId);
        var checkCount = _keywordService.SecurityAttackCount(state, attacker);
        for (var i = 0; i < checkCount && defender.Security.Count > 0; i++)
        {
            if (!AttackerCanContinue(state, attackerId))
            {
                break;
            }

            var securityCard = defender.Security[0];
            var wasFaceDown = !state.Cards[securityCard].IsFaceUp;
            checkedCards.Add(securityCard);
            firstCheckedCard ??= securityCard;

            MoveSecurityToExecuting(state, defenderId, securityCard, trace);

            if (_securityEffectExecutionService is not null)
            {
                var securityEffect = _securityEffectExecutionService.ExecuteSecurityEffects(
                    state,
                    securityCard,
                    defenderId,
                    wasFaceDown,
                    trace);
                securityEffectResults.Add(securityEffect);
            }

            if (AttackerCanContinue(state, attackerId)
                && state.Cards.TryGetValue(securityCard, out var checkedCard)
                && checkedCard.CurrentZone == Zone.Executing
                && BattleRules.IsDigimon(state, securityCard)
                && _effectiveStats.SecurityDp(state, securityCard) > 0)
            {
                var battle = _battleResolver.ResolveSecurityBattle(state, attackerId, securityCard, trace);
                battleResults.Add(battle);
                firstBattleResult ??= battle;
            }

            TrashIfStillExecuting(state, defenderId, securityCard, trace);
        }

        _durationCleanupService.CleanupSecurityCheckEnd(state);
        return new SecurityCheckResult(defenderId, firstCheckedCard, firstBattleResult)
        {
            CheckedCards = checkedCards,
            BattleResults = battleResults,
            SecurityEffectResults = securityEffectResults,
        };
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
}
