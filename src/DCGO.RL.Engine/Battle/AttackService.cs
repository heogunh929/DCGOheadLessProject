using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record AttackResult(
    PermanentId Attacker,
    PermanentId? Defender,
    bool DirectWin,
    SecurityCheckResult? SecurityCheck,
    BattleResolutionResult? Battle);

public sealed class AttackService
{
    private readonly BattleResolver _battleResolver;
    private readonly SecurityCheckService _securityCheckService;
    private readonly WinConditionChecker _winConditionChecker;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly EffectiveStatService _effectiveStats;

    public AttackService(
        BattleResolver? battleResolver = null,
        SecurityCheckService? securityCheckService = null,
        WinConditionChecker? winConditionChecker = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _battleResolver = battleResolver ?? new BattleResolver(null, _keywordService, _effectiveStats);
        _securityCheckService = securityCheckService ?? new SecurityCheckService(null, _battleResolver, _keywordService, effectiveStats: _effectiveStats);
        _winConditionChecker = winConditionChecker ?? new WinConditionChecker();
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _triggerPipelineService = triggerPipelineService;
    }

    public SelectionRequest? CreateBlockerSelectionRequest(GameState state, AttackAction action)
    {
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can attack.");
        }

        var attacker = BattleRules.Permanent(state, action.Attacker);
        if (!BattleRules.CanAttack(state, attacker, _keywordService, _effectiveStats))
        {
            throw new DomainException($"Permanent '{action.Attacker}' cannot attack.");
        }

        return _keywordService.CreateBlockerSelectionRequest(state, action.Attacker, action.Defender);
    }

    public AttackResult Attack(GameState state, AttackAction action, GameTrace? trace = null)
    {
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can attack.");
        }

        var attacker = BattleRules.Permanent(state, action.Attacker);
        if (attacker.ControllerPlayerId != action.Actor)
        {
            throw new DomainException($"Permanent '{action.Attacker}' is not controlled by player '{action.Actor}'.");
        }

        if (!BattleRules.CanAttack(state, attacker, _keywordService, _effectiveStats))
        {
            throw new DomainException($"Permanent '{action.Attacker}' cannot attack.");
        }

        attacker.IsSuspended = true;
        RunTriggerPipeline(
            state,
            EffectTiming.OnAllyAttack,
            action.Actor,
            action.Attacker,
            new Dictionary<string, object?>
            {
                ["Attacker"] = action.Attacker,
                ["Defender"] = action.Defender,
            },
            trace);

        if (action.Defender is not null)
        {
            var defender = BattleRules.Permanent(state, action.Defender.Value);
            if (defender.ControllerPlayerId == action.Actor || defender.IsBreedingArea || !BattleRules.IsDigimon(state, defender.TopCardId))
            {
                throw new DomainException($"Permanent '{action.Defender}' is not a valid attack target.");
            }

            var battle = _battleResolver.ResolvePermanentBattle(state, action.Attacker, action.Defender.Value, trace);
            SecurityCheckResult? piercingCheck = null;
            if (_keywordService.HasKeyword(state, attacker, BattleKeyword.Piercing)
                && battle.DestroyedPermanents.Contains(action.Defender.Value)
                && !battle.DestroyedPermanents.Contains(action.Attacker)
                && opponentHasSecurity(state, action.Actor))
            {
                piercingCheck = _securityCheckService.CheckSecurity(state, action.Attacker, state.NonTurnPlayerId, trace);
            }

            RunTriggerPipeline(
                state,
                EffectTiming.OnEndAttack,
                action.Actor,
                action.Attacker,
                new Dictionary<string, object?>
                {
                    ["Attacker"] = action.Attacker,
                    ["Defender"] = action.Defender,
                    ["Battle"] = battle,
                    ["SecurityCheck"] = piercingCheck,
                },
                trace);
            _durationCleanupService.CleanupBattleEnd(state);
            return new AttackResult(action.Attacker, action.Defender, DirectWin: false, SecurityCheck: piercingCheck, Battle: battle);
        }

        var opponent = state.Players.First(player => player.Id != action.Actor);
        if (opponent.Security.Count == 0 && _keywordService.SecurityAttackCount(state, attacker) >= 1)
        {
            _winConditionChecker.ApplyDirectAttackWin(state, action.Actor);
            RunTriggerPipeline(
                state,
                EffectTiming.OnEndAttack,
                action.Actor,
                action.Attacker,
                new Dictionary<string, object?>
                {
                    ["Attacker"] = action.Attacker,
                    ["DirectWin"] = true,
                },
                trace);
            _durationCleanupService.CleanupBattleEnd(state);
            return new AttackResult(action.Attacker, null, DirectWin: true, SecurityCheck: null, Battle: null);
        }

        var securityCheck = _securityCheckService.CheckSecurity(state, action.Attacker, opponent.Id, trace);
        RunTriggerPipeline(
            state,
            EffectTiming.OnEndAttack,
            action.Actor,
            action.Attacker,
            new Dictionary<string, object?>
            {
                ["Attacker"] = action.Attacker,
                ["SecurityCheck"] = securityCheck,
            },
            trace);
        _durationCleanupService.CleanupBattleEnd(state);
        return new AttackResult(action.Attacker, null, DirectWin: false, SecurityCheck: securityCheck, Battle: securityCheck.BattleResult);

        static bool opponentHasSecurity(GameState state, PlayerId actor) =>
            state.Players.First(player => player.Id != actor).Security.Count > 0;
    }

    public TriggerPipelineResult RunBlockTrigger(
        GameState state,
        PermanentId blocker,
        PermanentId attacker,
        GameTrace? trace = null)
    {
        var blockerPermanent = BattleRules.Permanent(state, blocker);
        var result = _triggerPipelineService?.Run(
            state,
            EffectTiming.OnBlockAnyone,
            blockerPermanent.ControllerPlayerId,
            sourcePermanent: blocker,
            values: new Dictionary<string, object?>
            {
                ["Blocker"] = blocker,
                ["Attacker"] = attacker,
            },
            trace: trace)
            ?? new TriggerPipelineResult(
                new EffectContext(state, EffectTiming.OnBlockAnyone, blockerPermanent.ControllerPlayerId, SourcePermanent: blocker),
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<SelectionResultApplicationResult>());

        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"OnBlockAnyone requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }

        return result;
    }

    private void RunTriggerPipeline(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        PermanentId sourcePermanent,
        IReadOnlyDictionary<string, object?> values,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(
            state,
            timing,
            player,
            sourcePermanent: sourcePermanent,
            values: values,
            trace: trace);
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"Trigger timing '{timing}' requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }
}
