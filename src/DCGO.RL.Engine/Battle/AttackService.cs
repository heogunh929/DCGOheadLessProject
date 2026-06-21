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

public enum AttackRuntimeState
{
    CounterNonCounter,
    CounterCounter,
    BlockDesignation,
    BlockTrigger,
    AttackTargetChanged,
    EndBlockDesignation,
    Battle,
    EndAttack,
    Cleanup,
}

public sealed record AttackFrame(
    PermanentId Attacker,
    PermanentId? Defender,
    bool IsBlocking = false,
    PermanentId? Blocker = null,
    AttackRuntimeState State = AttackRuntimeState.CounterNonCounter);

public enum AttackExecutionContinuationKind
{
    ContinueAfterAllyAttack,
    ContinueAfterCounterNonCounter,
    ContinueAfterCounterCounter,
    ResolveBlockerSelection,
    ContinueAfterBlockTrigger,
    ContinueAfterAttackTargetChanged,
    ContinueAfterEndBlockDesignation,
    ContinueAfterSecurityCheck,
    CompleteAfterEndAttack,
}

public sealed record AttackExecutionContinuation(
    AttackExecutionContinuationKind Kind,
    AttackAction Action,
    AttackFrame? Frame = null,
    PermanentId? PreviousDefender = null,
    AttackResult? AttackResult = null,
    BattleResolutionResult? BattleResult = null,
    SecurityCheckContinuation? SecurityCheckContinuation = null);

public sealed record AttackExecutionResult(
    AttackResult? Attack,
    TriggerPipelineResult? TriggerResult = null,
    SecurityCheckExecutionResult? SecurityCheckExecution = null,
    AttackExecutionContinuation? Continuation = null,
    SelectionRequest? DirectSelectionRequest = null,
    EffectResolution? DirectPendingResolution = null)
{
    public bool HasPendingSelection =>
        TriggerResult?.HasPendingSelection == true
        || SecurityCheckExecution?.HasPendingSelection == true
        || DirectSelectionRequest is not null;

    public SelectionRequest? PendingSelectionRequest =>
        TriggerResult?.PendingSelectionRequest
        ?? SecurityCheckExecution?.PendingSelectionRequest
        ?? DirectSelectionRequest;

    public EffectResolution? PendingResolution =>
        TriggerResult?.PendingResolution
        ?? SecurityCheckExecution?.PendingResolution
        ?? DirectPendingResolution;

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

        if (action.Defender is not null)
        {
            RequireValidAttackTarget(state, action, action.Defender.Value);
        }

        attacker.IsSuspended = true;
        var frame = new AttackFrame(action.Attacker, action.Defender, State: AttackRuntimeState.CounterNonCounter);
        var allyAttackTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnAllyAttack,
            action.Actor,
            action.Attacker,
            AttackValues(frame),
            trace);
        if (allyAttackTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                allyAttackTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterAllyAttack,
                    action,
                    frame));
        }

        return ContinueAfterAllyAttackWithResult(state, action, frame, trace);
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
                ContinueAfterAllyAttackWithResult(state, continuation.Action, RequireFrame(continuation), trace),

            AttackExecutionContinuationKind.ContinueAfterCounterNonCounter =>
                RunCounterCounterWithResult(state, continuation.Action, RequireFrame(continuation), trace),

            AttackExecutionContinuationKind.ContinueAfterCounterCounter =>
                RunBlockDesignationWithResult(state, continuation.Action, RequireFrame(continuation), trace),

            AttackExecutionContinuationKind.ResolveBlockerSelection =>
                throw new DomainException("Blocker selection attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.ContinueAfterBlockTrigger =>
                RunAttackTargetChangedIfNeededWithResult(
                    state,
                    continuation.Action,
                    RequireFrame(continuation),
                    continuation.PreviousDefender,
                    trace),

            AttackExecutionContinuationKind.ContinueAfterAttackTargetChanged =>
                RunEndBlockDesignationWithResult(state, continuation.Action, RequireFrame(continuation), trace),

            AttackExecutionContinuationKind.ContinueAfterEndBlockDesignation =>
                ResolveBattleWithResult(state, continuation.Action, RequireFrame(continuation), trace),

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

        return continuation.Kind switch
        {
            AttackExecutionContinuationKind.ResolveBlockerSelection =>
                ResumeBlockerSelectionWithResult(state, continuation, selectionResult, trace),

            AttackExecutionContinuationKind.ContinueAfterSecurityCheck =>
                ResumeSecurityCheckWithResult(state, continuation, selectionResult, trace),

            _ => throw new DomainException($"Attack continuation '{continuation.Kind}' does not accept a SelectionResult."),
        };
    }

    public TriggerPipelineResult RunBlockTrigger(
        GameState state,
        PermanentId blocker,
        PermanentId attacker,
        GameTrace? trace = null)
    {
        var blockerPermanent = BattleRules.Permanent(state, blocker);
        var frame = new AttackFrame(attacker, blocker, IsBlocking: true, Blocker: blocker, State: AttackRuntimeState.BlockTrigger);
        var result = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnBlockAnyone,
            blockerPermanent.ControllerPlayerId,
            attacker,
            AttackValues(frame),
            trace)
            ?? EmptyTriggerResult(state, EffectTiming.OnBlockAnyone, blockerPermanent.ControllerPlayerId, frame);

        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"OnBlockAnyone requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }

        return result;
    }

    private AttackExecutionResult ContinueAfterAllyAttackWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace) =>
        RunCounterNonCounterWithResult(state, action, frame with { State = AttackRuntimeState.CounterNonCounter }, trace);

    private AttackExecutionResult RunCounterNonCounterWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace)
    {
        if (!AttackerExists(state, frame.Attacker))
        {
            return RunEndAttackWithResult(state, action, frame, null, null, false, trace);
        }

        var counterTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnCounterTiming,
            action.Actor,
            frame.Attacker,
            AttackValues(frame),
            trace,
            descriptor => !descriptor.IsCounterEffect);
        if (counterTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                counterTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterCounterNonCounter,
                    action,
                    frame with { State = AttackRuntimeState.CounterCounter }));
        }

        return RunCounterCounterWithResult(state, action, frame with { State = AttackRuntimeState.CounterCounter }, trace);
    }

    private AttackExecutionResult RunCounterCounterWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace)
    {
        if (!AttackerExists(state, frame.Attacker))
        {
            return RunEndAttackWithResult(state, action, frame, null, null, false, trace);
        }

        var counterTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnCounterTiming,
            action.Actor,
            frame.Attacker,
            AttackValues(frame),
            trace,
            descriptor => descriptor.IsCounterEffect);
        if (counterTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                counterTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterCounterCounter,
                    action,
                    frame with { State = AttackRuntimeState.BlockDesignation }));
        }

        return RunBlockDesignationWithResult(state, action, frame with { State = AttackRuntimeState.BlockDesignation }, trace);
    }

    private AttackExecutionResult RunBlockDesignationWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace)
    {
        if (!AttackerExists(state, frame.Attacker))
        {
            return RunEndAttackWithResult(state, action, frame, null, null, false, trace);
        }

        var request = _keywordService.CreateBlockerSelectionRequest(state, frame.Attacker, frame.Defender);
        if (request is null)
        {
            return RunEndBlockDesignationWithResult(state, action, frame, trace);
        }

        var resolution = CreateAttackDecisionResolution(state, frame, EffectTiming.OnEndBlockDesignation, "blocker-selection");
        return new AttackExecutionResult(
            Attack: null,
            Continuation: new AttackExecutionContinuation(
                AttackExecutionContinuationKind.ResolveBlockerSelection,
                action,
                frame),
            DirectSelectionRequest: request,
            DirectPendingResolution: resolution);
    }

    private AttackExecutionResult ResumeBlockerSelectionWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace)
    {
        var frame = RequireFrame(continuation);
        if (!AttackerExists(state, frame.Attacker))
        {
            return RunEndAttackWithResult(state, continuation.Action, frame, null, null, false, trace);
        }

        var request = _keywordService.CreateBlockerSelectionRequest(state, frame.Attacker, frame.Defender)
            ?? throw new DomainException("Pending blocker selection no longer has legal blocker candidates.");
        SelectionValidator.Validate(request, selectionResult);
        if (selectionResult.SelectedTargets.Count == 0)
        {
            return RunEndBlockDesignationWithResult(state, continuation.Action, frame, trace);
        }

        var blocker = selectionResult.SelectedTargets.Single().Permanent
            ?? throw new DomainException("Blocker selection must select a permanent target.");
        return ApplyBlockerSelectionWithResult(state, continuation.Action, frame, blocker, trace);
    }

    private AttackExecutionResult ApplyBlockerSelectionWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        PermanentId blocker,
        GameTrace? trace)
    {
        var attacker = BattleRules.Permanent(state, frame.Attacker);
        var blockerPermanent = BattleRules.Permanent(state, blocker);
        if (!_keywordService.CanBlock(state, blockerPermanent, attacker))
        {
            throw new DomainException($"Permanent '{blocker}' is no longer a valid blocker.");
        }

        if (frame.Defender == blocker)
        {
            throw new DomainException($"Permanent '{blocker}' is already the current attack target.");
        }

        var previousDefender = frame.Defender;
        blockerPermanent.IsSuspended = true;
        var blockFrame = frame with
        {
            Defender = blocker,
            IsBlocking = true,
            Blocker = blocker,
            State = AttackRuntimeState.BlockTrigger,
        };

        var blockTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnBlockAnyone,
            blockerPermanent.ControllerPlayerId,
            frame.Attacker,
            AttackValues(blockFrame, previousDefender),
            trace);
        if (blockTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                blockTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterBlockTrigger,
                    action,
                    blockFrame with { State = AttackRuntimeState.AttackTargetChanged },
                    PreviousDefender: previousDefender));
        }

        return RunAttackTargetChangedIfNeededWithResult(
            state,
            action,
            blockFrame with { State = AttackRuntimeState.AttackTargetChanged },
            previousDefender,
            trace);
    }

    private AttackExecutionResult RunAttackTargetChangedIfNeededWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        PermanentId? previousDefender,
        GameTrace? trace)
    {
        if (previousDefender == frame.Defender)
        {
            return RunEndBlockDesignationWithResult(state, action, frame, trace);
        }

        var targetChangedTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnAttackTargetChanged,
            action.Actor,
            frame.Attacker,
            AttackValues(frame, previousDefender),
            trace);
        if (targetChangedTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                targetChangedTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChanged,
                    action,
                    frame with { State = AttackRuntimeState.EndBlockDesignation }));
        }

        return RunEndBlockDesignationWithResult(state, action, frame with { State = AttackRuntimeState.EndBlockDesignation }, trace);
    }

    private AttackExecutionResult RunEndBlockDesignationWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace)
    {
        var endBlockTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnEndBlockDesignation,
            action.Actor,
            frame.Attacker,
            AttackValues(frame),
            trace);
        if (endBlockTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                endBlockTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterEndBlockDesignation,
                    action,
                    frame with { State = AttackRuntimeState.Battle }));
        }

        return ResolveBattleWithResult(state, action, frame with { State = AttackRuntimeState.Battle }, trace);
    }

    private AttackExecutionResult ResolveBattleWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        GameTrace? trace)
    {
        if (!AttackerExists(state, frame.Attacker))
        {
            return RunEndAttackWithResult(state, action, frame, null, null, false, trace);
        }

        if (frame.Defender is { } defenderId)
        {
            if (!TryFindPermanent(state, defenderId, out var defender)
                || defender.ControllerPlayerId == action.Actor
                || defender.IsBreedingArea
                || !BattleRules.IsDigimon(state, defender.TopCardId))
            {
                throw new DomainException($"Permanent '{defenderId}' is not a valid attack target.");
            }

            var battle = _battleResolver.ResolvePermanentBattle(state, frame.Attacker, defenderId, trace);
            SecurityCheckResult? piercingCheck = null;
            if (TryFindPermanent(state, frame.Attacker, out var attackerAfterBattle)
                && _keywordService.HasKeyword(state, attackerAfterBattle, BattleKeyword.Piercing)
                && battle.DestroyedPermanents.Contains(defenderId)
                && !battle.DestroyedPermanents.Contains(frame.Attacker)
                && OpponentHasSecurity(state, action.Actor))
            {
                var piercingCheckResult = _securityCheckService.CheckSecurityWithResult(
                    state,
                    frame.Attacker,
                    state.NonTurnPlayerId,
                    trace);
                if (piercingCheckResult.HasPendingSelection)
                {
                    return PendingSecurityCheck(
                        action,
                        frame,
                        piercingCheckResult,
                        battle);
                }

                piercingCheck = piercingCheckResult.Result
                    ?? throw new DomainException("Completed piercing security check result is missing.");
            }

            return RunEndAttackWithResult(state, action, frame, battle, piercingCheck, false, trace);
        }

        var opponent = state.Players.First(player => player.Id != action.Actor);
        if (opponent.Security.Count == 0
            && TryFindPermanent(state, frame.Attacker, out var directAttacker)
            && _keywordService.SecurityAttackCount(state, directAttacker) >= 1)
        {
            _winConditionChecker.ApplyDirectAttackWin(state, action.Actor);
            return RunEndAttackWithResult(state, action, frame, null, null, true, trace);
        }

        var securityCheckResult = _securityCheckService.CheckSecurityWithResult(state, frame.Attacker, opponent.Id, trace);
        if (securityCheckResult.HasPendingSelection)
        {
            return PendingSecurityCheck(action, frame, securityCheckResult, BattleResult: null);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        return RunEndAttackWithResult(state, action, frame, securityCheck.BattleResult, securityCheck, false, trace);
    }

    private AttackExecutionResult ResumeSecurityCheckWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace)
    {
        var securityContinuation = continuation.SecurityCheckContinuation
            ?? throw new DomainException("Security check attack continuation is missing security state.");
        var securityCheckResult = _securityCheckService.CompleteSecurityContinuationWithResult(
            state,
            securityContinuation,
            selectionResult,
            trace);
        if (securityCheckResult.HasPendingSelection)
        {
            return PendingSecurityCheck(
                continuation.Action,
                RequireFrame(continuation),
                securityCheckResult,
                continuation.BattleResult);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        return ContinueAfterSecurityCheckWithResult(
            state,
            continuation.Action,
            RequireFrame(continuation),
            securityCheck,
            continuation.BattleResult,
            trace);
    }

    private AttackExecutionResult ContinueAfterSecurityCheckWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        SecurityCheckResult securityCheck,
        BattleResolutionResult? battleResult,
        GameTrace? trace) =>
        RunEndAttackWithResult(state, action, frame, battleResult ?? securityCheck.BattleResult, securityCheck, false, trace);

    private AttackExecutionResult RunEndAttackWithResult(
        GameState state,
        AttackAction action,
        AttackFrame frame,
        BattleResolutionResult? battle,
        SecurityCheckResult? securityCheck,
        bool directWin,
        GameTrace? trace)
    {
        var attackResult = new AttackResult(
            frame.Attacker,
            frame.Defender,
            directWin,
            securityCheck,
            battle);

        var values = AttackValues(frame);
        values["DirectWin"] = directWin;
        values["Battle"] = battle;
        values["SecurityCheck"] = securityCheck;

        var endAttackTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnEndAttack,
            action.Actor,
            frame.Attacker,
            values,
            trace);
        if (endAttackTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                endAttackTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.CompleteAfterEndAttack,
                    action,
                    frame with { State = AttackRuntimeState.Cleanup },
                    AttackResult: attackResult));
        }

        _durationCleanupService.CleanupBattleEnd(state);
        return new AttackExecutionResult(Attack: attackResult, TriggerResult: endAttackTrigger);
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

    private static AttackExecutionResult PendingSecurityCheck(
        AttackAction action,
        AttackFrame frame,
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
                frame,
                BattleResult: BattleResult,
                SecurityCheckContinuation: securityCheckResult.Continuation));
    }

    private AttackExecutionResult PendingTrigger(
        TriggerPipelineResult result,
        AttackExecutionContinuation continuation) =>
        new(Attack: null, TriggerResult: result, Continuation: continuation);

    private TriggerPipelineResult? RunTriggerPipelineWithResult(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        PermanentId sourcePermanent,
        IReadOnlyDictionary<string, object?> values,
        GameTrace? trace,
        Func<EffectDescriptor, bool>? descriptorFilter = null)
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
            descriptorFilter: descriptorFilter,
            trace: trace);
    }

    private static EffectResolution CreateAttackDecisionResolution(
        GameState state,
        AttackFrame frame,
        EffectTiming timing,
        string stableIdSuffix)
    {
        var sourceCard = TryFindPermanent(state, frame.Attacker, out var attacker)
            ? attacker.TopCardId
            : (CardInstanceId?)null;
        var controller = attacker?.ControllerPlayerId;
        var context = new EffectContext(
            state,
            timing,
            controller,
            sourceCard,
            frame.Attacker,
            AttackValues(frame));

        return new EffectResolution(
            $"attack:{frame.Attacker.Value}:{stableIdSuffix}",
            timing,
            sourceCard,
            frame.Attacker,
            controller,
            IsBackground: false,
            IsOptional: false,
            context);
    }

    private static TriggerPipelineResult EmptyTriggerResult(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        AttackFrame frame) =>
        new(
            new EffectContext(state, timing, player, SourcePermanent: frame.Attacker, Values: AttackValues(frame)),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<SelectionResultApplicationResult>());

    private static AttackFrame RequireFrame(AttackExecutionContinuation continuation) =>
        continuation.Frame
        ?? throw new DomainException($"Attack continuation '{continuation.Kind}' requires attack frame state.");

    private static void RequireValidAttackTarget(GameState state, AttackAction action, PermanentId defenderId)
    {
        var defender = BattleRules.Permanent(state, defenderId);
        if (defender.ControllerPlayerId == action.Actor || defender.IsBreedingArea || !BattleRules.IsDigimon(state, defender.TopCardId))
        {
            throw new DomainException($"Permanent '{defenderId}' is not a valid attack target.");
        }
    }

    private static bool AttackerExists(GameState state, PermanentId attacker) =>
        TryFindPermanent(state, attacker, out var permanent)
        && !permanent.IsBreedingArea
        && BattleRules.IsDigimon(state, permanent.TopCardId);

    private static bool TryFindPermanent(GameState state, PermanentId id, out PermanentState permanent)
    {
        foreach (var player in state.Players)
        {
            var found = player.FieldPermanents.FirstOrDefault(permanent => permanent.Id == id);
            if (found is not null)
            {
                permanent = found;
                return true;
            }
        }

        permanent = null!;
        return false;
    }

    private static bool OpponentHasSecurity(GameState state, PlayerId actor) =>
        state.Players.First(player => player.Id != actor).Security.Count > 0;

    private static Dictionary<string, object?> AttackValues(
        AttackFrame frame,
        PermanentId? previousDefender = null) =>
        new()
        {
            ["Attacker"] = frame.Attacker,
            ["Defender"] = frame.Defender,
            ["Blocker"] = frame.Blocker,
            ["IsBlocking"] = frame.IsBlocking,
            ["PreviousDefender"] = previousDefender,
            ["AttackState"] = frame.State,
        };

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
