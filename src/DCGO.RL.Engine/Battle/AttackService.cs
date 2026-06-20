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

public enum AttackExecutionContinuationKind
{
    ContinueAfterAllyAttack,
    ContinueAfterSecurityCheck,
    CompleteAfterEndAttack,
}

public sealed record AttackExecutionContinuation(
    AttackExecutionContinuationKind Kind,
    AttackAction Action,
    AttackResult? AttackResult = null,
    BattleResolutionResult? BattleResult = null,
    SecurityCheckContinuation? SecurityCheckContinuation = null);

public sealed record AttackExecutionResult(
    AttackResult? Attack,
    TriggerPipelineResult? TriggerResult = null,
    SecurityCheckExecutionResult? SecurityCheckExecution = null,
    AttackExecutionContinuation? Continuation = null)
{
    public bool HasPendingSelection =>
        TriggerResult?.HasPendingSelection == true
        || SecurityCheckExecution?.HasPendingSelection == true;

    public SelectionRequest? PendingSelectionRequest =>
        TriggerResult?.PendingSelectionRequest
        ?? SecurityCheckExecution?.PendingSelectionRequest;

    public EffectResolution? PendingResolution =>
        TriggerResult?.PendingResolution
        ?? SecurityCheckExecution?.PendingResolution;

    public TriggerPipelineContinuation? PendingContinuation => TriggerResult?.PendingContinuation;
}

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

    internal SecurityCheckService RuntimeSecurityCheckService => _securityCheckService;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

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
        var snapshot = state.Clone();
        var traceCount = trace?.Events.Count ?? 0;
        var result = AttackWithResult(state, action, trace);
        if (result.HasPendingSelection)
        {
            RestoreAfterPendingSynchronousCall(state, snapshot, trace, traceCount);
            throw new DomainException(
                $"Attack timing requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
        }

        return result.Attack
            ?? throw new DomainException("Completed attack result is missing.");
    }

    public AttackExecutionResult AttackWithResult(GameState state, AttackAction action, GameTrace? trace = null)
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
        var allyAttackTrigger = RunTriggerPipelineWithResult(
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
        if (allyAttackTrigger?.HasPendingSelection == true)
        {
            return new AttackExecutionResult(
                Attack: null,
                TriggerResult: allyAttackTrigger,
                Continuation: new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterAllyAttack,
                    action));
        }

        return ContinueAfterAllyAttackWithResult(state, action, trace);
    }

    public AttackExecutionResult CompleteAttackContinuationWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);

        return continuation.Kind switch
        {
            AttackExecutionContinuationKind.ContinueAfterAllyAttack =>
                ContinueAfterAllyAttackWithResult(state, continuation.Action, trace),

            AttackExecutionContinuationKind.ContinueAfterSecurityCheck =>
                throw new DomainException("Security check attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.CompleteAfterEndAttack =>
                CompleteAfterEndAttack(state, continuation.AttackResult),

            _ => throw new UnsupportedMechanicException($"Attack continuation '{continuation.Kind}'"),
        };
    }

    public AttackExecutionResult ResumeAttackContinuationWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);
        ArgumentNullException.ThrowIfNull(selectionResult);

        if (continuation.Kind != AttackExecutionContinuationKind.ContinueAfterSecurityCheck)
        {
            throw new DomainException($"Attack continuation '{continuation.Kind}' does not accept a SelectionResult.");
        }

        var securityContinuation = continuation.SecurityCheckContinuation
            ?? throw new DomainException("Security check attack continuation is missing security state.");
        var securityCheckResult = _securityCheckService.CompleteSecurityContinuationWithResult(
            state,
            securityContinuation,
            selectionResult,
            trace);
        if (securityCheckResult.HasPendingSelection)
        {
            return PendingSecurityCheck(continuation.Action, securityCheckResult, continuation.BattleResult);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        return ContinueAfterSecurityCheckWithResult(
            state,
            continuation.Action,
            securityCheck,
            continuation.BattleResult,
            trace);
    }

    private AttackExecutionResult ContinueAfterAllyAttackWithResult(
        GameState state,
        AttackAction action,
        GameTrace? trace)
    {
        var attacker = BattleRules.Permanent(state, action.Attacker);
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
                var piercingCheckResult = _securityCheckService.CheckSecurityWithResult(
                    state,
                    action.Attacker,
                    state.NonTurnPlayerId,
                    trace);
                if (piercingCheckResult.HasPendingSelection)
                {
                    return PendingSecurityCheck(action, piercingCheckResult, battle);
                }

                piercingCheck = piercingCheckResult.Result
                    ?? throw new DomainException("Completed piercing security check result is missing.");
            }

            var attackResult = new AttackResult(action.Attacker, action.Defender, DirectWin: false, SecurityCheck: piercingCheck, Battle: battle);
            var endAttackTrigger = RunTriggerPipelineWithResult(
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
            if (endAttackTrigger?.HasPendingSelection == true)
            {
                return new AttackExecutionResult(
                    Attack: attackResult,
                    TriggerResult: endAttackTrigger,
                    Continuation: new AttackExecutionContinuation(
                        AttackExecutionContinuationKind.CompleteAfterEndAttack,
                        action,
                        attackResult));
            }

            _durationCleanupService.CleanupBattleEnd(state);
            return new AttackExecutionResult(Attack: attackResult, TriggerResult: endAttackTrigger);
        }

        var opponent = state.Players.First(player => player.Id != action.Actor);
        if (opponent.Security.Count == 0 && _keywordService.SecurityAttackCount(state, attacker) >= 1)
        {
            _winConditionChecker.ApplyDirectAttackWin(state, action.Actor);
            var attackResult = new AttackResult(action.Attacker, null, DirectWin: true, SecurityCheck: null, Battle: null);
            var endAttackTrigger = RunTriggerPipelineWithResult(
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
            if (endAttackTrigger?.HasPendingSelection == true)
            {
                return new AttackExecutionResult(
                    Attack: attackResult,
                    TriggerResult: endAttackTrigger,
                    Continuation: new AttackExecutionContinuation(
                        AttackExecutionContinuationKind.CompleteAfterEndAttack,
                        action,
                        attackResult));
            }

            _durationCleanupService.CleanupBattleEnd(state);
            return new AttackExecutionResult(Attack: attackResult, TriggerResult: endAttackTrigger);
        }

        var securityCheckResult = _securityCheckService.CheckSecurityWithResult(state, action.Attacker, opponent.Id, trace);
        if (securityCheckResult.HasPendingSelection)
        {
            return PendingSecurityCheck(action, securityCheckResult, BattleResult: null);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        var securityAttackResult = new AttackResult(action.Attacker, null, DirectWin: false, SecurityCheck: securityCheck, Battle: securityCheck.BattleResult);
        var securityEndAttackTrigger = RunTriggerPipelineWithResult(
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
        if (securityEndAttackTrigger?.HasPendingSelection == true)
        {
            return new AttackExecutionResult(
                Attack: securityAttackResult,
                TriggerResult: securityEndAttackTrigger,
                Continuation: new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.CompleteAfterEndAttack,
                    action,
                    securityAttackResult));
        }

        _durationCleanupService.CleanupBattleEnd(state);
        return new AttackExecutionResult(Attack: securityAttackResult, TriggerResult: securityEndAttackTrigger);

        static bool opponentHasSecurity(GameState state, PlayerId actor) =>
            state.Players.First(player => player.Id != actor).Security.Count > 0;
    }

    private AttackExecutionResult CompleteAfterEndAttack(GameState state, AttackResult? attackResult)
    {
        if (attackResult is null)
        {
            throw new DomainException("Completing OnEndAttack requires the resolved attack result.");
        }

        _durationCleanupService.CleanupBattleEnd(state);
        return new AttackExecutionResult(Attack: attackResult);
    }

    private AttackExecutionResult ContinueAfterSecurityCheckWithResult(
        GameState state,
        AttackAction action,
        SecurityCheckResult securityCheck,
        BattleResolutionResult? battleResult,
        GameTrace? trace)
    {
        var attackResult = battleResult is null
            ? new AttackResult(action.Attacker, null, DirectWin: false, SecurityCheck: securityCheck, Battle: securityCheck.BattleResult)
            : new AttackResult(action.Attacker, action.Defender, DirectWin: false, SecurityCheck: securityCheck, Battle: battleResult);
        var values = new Dictionary<string, object?>
        {
            ["Attacker"] = action.Attacker,
            ["SecurityCheck"] = securityCheck,
        };
        if (action.Defender is not null)
        {
            values["Defender"] = action.Defender;
        }

        if (battleResult is not null)
        {
            values["Battle"] = battleResult;
        }

        var endAttackTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnEndAttack,
            action.Actor,
            action.Attacker,
            values,
            trace);
        if (endAttackTrigger?.HasPendingSelection == true)
        {
            return new AttackExecutionResult(
                Attack: attackResult,
                TriggerResult: endAttackTrigger,
                Continuation: new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.CompleteAfterEndAttack,
                    action,
                    attackResult));
        }

        _durationCleanupService.CleanupBattleEnd(state);
        return new AttackExecutionResult(Attack: attackResult, TriggerResult: endAttackTrigger);
    }

    private static AttackExecutionResult PendingSecurityCheck(
        AttackAction action,
        SecurityCheckExecutionResult securityCheckResult,
        BattleResolutionResult? BattleResult)
    {
        if (securityCheckResult.Continuation is null)
        {
            throw new DomainException("Pending security check result requires a continuation.");
        }

        return new AttackExecutionResult(
            Attack: null,
            SecurityCheckExecution: securityCheckResult,
            Continuation: new AttackExecutionContinuation(
                AttackExecutionContinuationKind.ContinueAfterSecurityCheck,
                action,
                BattleResult: BattleResult,
                SecurityCheckContinuation: securityCheckResult.Continuation));
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
        var result = RunTriggerPipelineWithResult(state, timing, player, sourcePermanent, values, trace);
        if (result?.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"Trigger timing '{timing}' requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }

    private TriggerPipelineResult? RunTriggerPipelineWithResult(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        PermanentId sourcePermanent,
        IReadOnlyDictionary<string, object?> values,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(
            state,
            timing,
            player,
            sourcePermanent: sourcePermanent,
            values: values,
            trace: trace);
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
