using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;
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
    ContinueAfterCounterNonCounter,
    ResolveCounterSelection,
    ContinueAfterCounterCounter,
    ResolveBlockerSelection,
    ContinueAfterBlockTrigger,
    ContinueAfterAttackTargetChangedToBlockDesignation,
    ContinueAfterAttackTargetChangedToBattle,
    ContinueAfterSecurityCheck,
    CompleteAfterEndAttack,
}

public sealed record AttackExecutionContinuation(
    AttackExecutionContinuationKind Kind,
    AttackAction Action,
    AttackRuntimeContext? Context = null,
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
    private static readonly TriggerPipelineOptions CounterTimingOptions = new(
        TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
        | TriggerSourceZone.Hand
        | TriggerSourceZone.Trash
        | TriggerSourceZone.Executing
        | TriggerSourceZone.FaceUpSecurity,
        ResolveAfterEffectsActivate: true);

    private readonly BattleResolver _battleResolver;
    private readonly SecurityCheckService _securityCheckService;
    private readonly WinConditionChecker _winConditionChecker;
    private readonly BattleKeywordService _keywordService;
    private readonly DurationCleanupService _durationCleanupService;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly EffectiveStatService _effectiveStats;
    private readonly Tier1PrimitiveService _primitives;

    public AttackService(
        BattleResolver? battleResolver = null,
        SecurityCheckService? securityCheckService = null,
        WinConditionChecker? winConditionChecker = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null,
        Tier1PrimitiveService? primitiveService = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
        _battleResolver = battleResolver ?? new BattleResolver(null, _keywordService, _effectiveStats);
        _securityCheckService = securityCheckService ?? new SecurityCheckService(null, _battleResolver, _keywordService, effectiveStats: _effectiveStats);
        _winConditionChecker = winConditionChecker ?? new WinConditionChecker();
        _durationCleanupService = durationCleanupService ?? new DurationCleanupService();
        _triggerPipelineService = triggerPipelineService;
        _primitives = primitiveService ?? new Tier1PrimitiveService(battleResolver: _battleResolver, securityCheckService: _securityCheckService);
    }

    internal SecurityCheckService RuntimeSecurityCheckService => _securityCheckService;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    internal Tier1PrimitiveService RuntimePrimitiveService => _primitives;

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

        if (!_primitives.Suspend(state, action.Attacker))
        {
            throw new DomainException($"Permanent '{action.Attacker}' could not be suspended for attack declaration.");
        }

        var context = new AttackRuntimeContext(
            action.Attacker,
            action.Defender,
            AttackRuntimeState.CounterNonCounter,
            AttackerTopCardWhenDeclared: attacker.TopCardId,
            CounterSourceSnapshot: PermanentCards(attacker));
        state.RuntimeRules.StartAttack(context);

        var allyAttackTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnAllyAttack,
            action.Actor,
            context.Attacker,
            AttackValues(context),
            trace);
        if (allyAttackTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                allyAttackTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterAllyAttack,
                    action,
                    context));
        }

        return ContinueAfterAllyAttackWithResult(state, action, context, trace);
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
                ContinueAfterAllyAttackWithResult(state, continuation.Action, RequireContext(state, continuation), trace),

            AttackExecutionContinuationKind.ContinueAfterCounterNonCounter =>
                ContinueAfterCounterNonCounterWithResult(state, continuation.Action, RequireContext(state, continuation), trace),

            AttackExecutionContinuationKind.ContinueAfterCounterCounter =>
                ContinueAfterCounterCounterWithResult(state, continuation.Action, RequireContext(state, continuation), trace),

            AttackExecutionContinuationKind.ResolveCounterSelection =>
                throw new DomainException("Counter selection attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.ResolveBlockerSelection =>
                throw new DomainException("Blocker selection attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.ContinueAfterBlockTrigger =>
                RunAttackTargetChangedIfNeededWithResult(
                    state,
                    continuation.Action,
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle,
                    trace),

            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation =>
                ContinueAfterAttackTargetChangedWithResult(
                    state,
                    continuation.Action,
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation,
                    trace),

            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle =>
                ContinueAfterAttackTargetChangedWithResult(
                    state,
                    continuation.Action,
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle,
                    trace),

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
            AttackExecutionContinuationKind.ResolveCounterSelection =>
                ResumeCounterSelectionWithResult(state, continuation, selectionResult, trace),

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
        var context = state.RuntimeRules.Attack
            ?? new AttackRuntimeContext(attacker, blocker, AttackRuntimeState.BlockTrigger, IsBlocking: true, Blocker: blocker);
        var result = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnBlockAnyone,
            blockerPermanent.ControllerPlayerId,
            attacker,
            AttackValues(context),
            trace)
            ?? EmptyTriggerResult(state, EffectTiming.OnBlockAnyone, blockerPermanent.ControllerPlayerId, context);

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
        AttackRuntimeContext context,
        GameTrace? trace) =>
        ValidateStageThen(state, action, AttackRuntimeState.CounterNonCounter, trace, () =>
            RunPendingSwitchThenNext(
                state,
                action,
                AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation,
                trace,
                () => RunCounterNonCounterWithResult(state, action, context.WithState(AttackRuntimeState.CounterNonCounter), trace)));

    private AttackExecutionResult RunCounterNonCounterWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        state.RuntimeRules.SetAttack(context.WithState(AttackRuntimeState.CounterNonCounter));
        return ValidateStageThen(state, action, AttackRuntimeState.CounterNonCounter, trace, () =>
        {
            var counterTrigger = RunTriggerPipelineWithResult(
                state,
                EffectTiming.OnCounterTiming,
                action.Actor,
                context.Attacker,
                AttackValues(state.RuntimeRules.RequireAttack()),
                trace,
                descriptor => !descriptor.IsCounterEffect);
            if (counterTrigger?.HasPendingSelection == true)
            {
                return PendingTrigger(
                    counterTrigger,
                    new AttackExecutionContinuation(
                        AttackExecutionContinuationKind.ContinueAfterCounterNonCounter,
                        action,
                        state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterCounter)));
            }

            return ContinueAfterCounterNonCounterWithResult(
                state,
                action,
                state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterCounter),
                trace);
        });
    }

    private AttackExecutionResult ContinueAfterCounterNonCounterWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace) =>
        ValidateStageThen(state, action, AttackRuntimeState.CounterCounter, trace, () =>
            RunPendingSwitchThenNext(
                state,
                action,
                AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation,
                trace,
                () => RunCounterCounterWithResult(state, action, context.WithState(AttackRuntimeState.CounterCounter), trace)));

    private AttackExecutionResult RunCounterCounterWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        state.RuntimeRules.SetAttack(context.WithState(AttackRuntimeState.CounterCounter));
        return ValidateStageThen(state, action, AttackRuntimeState.CounterCounter, trace, () =>
        {
            var prepared = PrepareCounterEffects(state, action, state.RuntimeRules.RequireAttack());
            var candidates = SelectCounterChoiceGroup(state, prepared);
            if (candidates.Count == 0)
            {
                return ContinueAfterCounterCounterWithResult(
                    state,
                    action,
                    state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.BlockDesignation),
                    trace);
            }

            var request = CreateCounterSelectionRequest(state, state.RuntimeRules.RequireAttack(), candidates);
            var resolution = CreateAttackDecisionResolution(
                state,
                state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterSelection),
                EffectTiming.OnCounterTiming,
                "counter-selection");
            return new AttackExecutionResult(
                Attack: null,
                Continuation: new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ResolveCounterSelection,
                    action,
                    state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterSelection)),
                DirectSelectionRequest: request,
                DirectPendingResolution: resolution);
        });
    }

    private AttackExecutionResult ResumeCounterSelectionWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace)
    {
        var context = RequireContext(state, continuation);
        if (!AttackerExists(state, context.Attacker))
        {
            return RunEndAttackWithResult(state, continuation.Action, context, null, null, false, trace);
        }

        var prepared = PrepareCounterEffects(state, continuation.Action, context);
        var candidates = SelectCounterChoiceGroup(state, prepared);
        var request = CreateCounterSelectionRequest(state, context, candidates);
        SelectionValidator.Validate(request, selectionResult);
        if (selectionResult.SelectedTargets.Count == 0 && string.IsNullOrWhiteSpace(selectionResult.SelectedOption))
        {
            return ContinueAfterCounterCounterWithResult(
                state,
                continuation.Action,
                context.WithState(AttackRuntimeState.BlockDesignation),
                trace);
        }

        var selectedId = selectionResult.SelectedOption
            ?? selectionResult.SelectedTargets.SingleOrDefault()?.StableId
            ?? throw new DomainException("Counter selection requires a selected effect.");
        var selected = candidates.FirstOrDefault(candidate =>
                string.Equals(candidate.StableId, selectedId, StringComparison.Ordinal))
            ?? throw new DomainException($"Counter effect '{selectedId}' is no longer a legal candidate.");

        var selectedPrepared = new PreparedTriggerGroup(
            prepared?.Context ?? selected.Context,
            new[] { selected },
            Array.Empty<EffectResolution>(),
            prepared?.Options ?? CounterTimingOptions);
        var counterTrigger = _triggerPipelineService!.RunPrepared(state, selectedPrepared, trace);
        if (counterTrigger.HasPendingSelection)
        {
            return PendingTrigger(
                counterTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterCounterCounter,
                    continuation.Action,
                    state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.BlockDesignation)));
        }

        return ContinueAfterCounterCounterWithResult(
            state,
            continuation.Action,
            state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.BlockDesignation),
            trace);
    }

    private AttackExecutionResult ContinueAfterCounterCounterWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace) =>
        ValidateStageThen(state, action, AttackRuntimeState.BlockDesignation, trace, () =>
            RunPendingSwitchThenNext(
                state,
                action,
                AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation,
                trace,
                () => RunBlockDesignationWithResult(state, action, context.WithState(AttackRuntimeState.BlockDesignation), trace)));

    private AttackExecutionResult RunBlockDesignationWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        state.RuntimeRules.SetAttack(context.WithState(AttackRuntimeState.BlockDesignation));
        return ValidateStageThen(state, action, AttackRuntimeState.BlockDesignation, trace, () =>
        {
            var current = state.RuntimeRules.RequireAttack();
            var request = _keywordService.CreateBlockerSelectionRequest(state, current.Attacker, current.Defender);
            if (request is null)
            {
                return ResolveBattleWithResult(state, action, current.WithState(AttackRuntimeState.Battle), trace);
            }

            var resolution = CreateAttackDecisionResolution(
                state,
                current,
                EffectTiming.OnBlockAnyone,
                "blocker-selection");
            return new AttackExecutionResult(
                Attack: null,
                Continuation: new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ResolveBlockerSelection,
                    action,
                    current),
                DirectSelectionRequest: request,
                DirectPendingResolution: resolution);
        });
    }

    private AttackExecutionResult ResumeBlockerSelectionWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace)
    {
        var context = RequireContext(state, continuation);
        if (!AttackerExists(state, context.Attacker))
        {
            return RunEndAttackWithResult(state, continuation.Action, context, null, null, false, trace);
        }

        var request = _keywordService.CreateBlockerSelectionRequest(state, context.Attacker, context.Defender)
            ?? throw new DomainException("Pending blocker selection no longer has legal blocker candidates.");
        SelectionValidator.Validate(request, selectionResult);
        if (selectionResult.SelectedTargets.Count == 0)
        {
            return ResolveBattleWithResult(state, continuation.Action, context.WithState(AttackRuntimeState.Battle), trace);
        }

        var blocker = selectionResult.SelectedTargets.Single().Permanent
            ?? throw new DomainException("Blocker selection must select a permanent target.");
        return ApplyBlockerSelectionWithResult(state, continuation.Action, context, blocker, trace);
    }

    private AttackExecutionResult ApplyBlockerSelectionWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        PermanentId blocker,
        GameTrace? trace)
    {
        var attacker = BattleRules.Permanent(state, context.Attacker);
        var blockerPermanent = BattleRules.Permanent(state, blocker);
        if (!_keywordService.CanBlock(state, blockerPermanent, attacker))
        {
            throw new DomainException($"Permanent '{blocker}' is no longer a valid blocker.");
        }

        if (context.Defender == blocker)
        {
            throw new DomainException($"Permanent '{blocker}' is already the current attack target.");
        }

        if (!_primitives.Suspend(state, blocker))
        {
            return ResolveBattleWithResult(state, action, context.WithState(AttackRuntimeState.Battle), trace);
        }

        if (!AttackRuntimeOperations.SwitchDefender(
            state,
            blocker,
            isBlock: true,
            blocker,
            sourceEffectStableId: "attack:blocker-selection"))
        {
            return ResolveBattleWithResult(state, action, context.WithState(AttackRuntimeState.Battle), trace);
        }

        var blockContext = state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.BlockTrigger);
        state.RuntimeRules.SetAttack(blockContext);
        var blockTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnBlockAnyone,
            blockerPermanent.ControllerPlayerId,
            blockContext.Attacker,
            AttackValues(blockContext),
            trace);
        if (blockTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                blockTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterBlockTrigger,
                    action,
                    blockContext.WithState(AttackRuntimeState.AttackTargetChanged)));
        }

        return RunAttackTargetChangedIfNeededWithResult(
            state,
            action,
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle,
            trace);
    }

    private AttackExecutionResult RunAttackTargetChangedIfNeededWithResult(
        GameState state,
        AttackAction action,
        AttackExecutionContinuationKind nextKind,
        GameTrace? trace)
    {
        var targetSwitch = state.RuntimeRules.ConsumePendingAttackTargetSwitch();
        if (targetSwitch is null || targetSwitch.OldDefender == targetSwitch.NewDefender)
        {
            return ContinueAfterAttackTargetChangedWithResult(state, action, nextKind, trace);
        }

        var context = state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.AttackTargetChanged);
        state.RuntimeRules.SetAttack(context);
        var targetChangedTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnAttackTargetChanged,
            action.Actor,
            context.Attacker,
            AttackValues(context, targetSwitch),
            trace);
        if (targetChangedTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                targetChangedTrigger,
                new AttackExecutionContinuation(
                    nextKind,
                    action,
                    context));
        }

        return ContinueAfterAttackTargetChangedWithResult(state, action, nextKind, trace);
    }

    private AttackExecutionResult ContinueAfterAttackTargetChangedWithResult(
        GameState state,
        AttackAction action,
        AttackExecutionContinuationKind nextKind,
        GameTrace? trace)
    {
        if (state.RuntimeRules.Attack?.PendingTargetSwitch is not null)
        {
            return RunAttackTargetChangedIfNeededWithResult(state, action, nextKind, trace);
        }

        var context = state.RuntimeRules.RequireAttack();
        return nextKind switch
        {
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation =>
                RunBlockDesignationWithResult(state, action, context.WithState(AttackRuntimeState.BlockDesignation), trace),
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle =>
                ResolveBattleWithResult(state, action, context.WithState(AttackRuntimeState.Battle), trace),
            _ => throw new DomainException($"Unsupported attack target changed continuation '{nextKind}'."),
        };
    }

    private AttackExecutionResult ResolveBattleWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        state.RuntimeRules.SetAttack(context.WithState(AttackRuntimeState.Battle));
        var current = state.RuntimeRules.RequireAttack();
        if (!AttackerExists(state, current.Attacker))
        {
            return RunEndAttackWithResult(state, action, current, null, null, false, trace);
        }

        if (current.Defender is { } defenderId)
        {
            if (!IsValidBattleDefender(state, action, defenderId))
            {
                return RunEndAttackWithResult(state, action, current.EndAttack(), null, null, false, trace);
            }

            var battle = _battleResolver.ResolvePermanentBattle(state, current.Attacker, defenderId, trace);
            _durationCleanupService.CleanupBattleEnd(state);

            SecurityCheckResult? piercingCheck = null;
            if (TryFindPermanent(state, current.Attacker, out var attackerAfterBattle)
                && _keywordService.HasKeyword(state, attackerAfterBattle, BattleKeyword.Piercing)
                && battle.DestroyedPermanents.Contains(defenderId)
                && !battle.DestroyedPermanents.Contains(current.Attacker)
                && OpponentHasSecurity(state, action.Actor))
            {
                var piercingCheckResult = _securityCheckService.CheckSecurityWithResult(
                    state,
                    current.Attacker,
                    state.NonTurnPlayerId,
                    trace);
                if (piercingCheckResult.HasPendingSelection)
                {
                    return PendingSecurityCheck(
                        action,
                        current,
                        piercingCheckResult,
                        battle);
                }

                piercingCheck = piercingCheckResult.Result
                    ?? throw new DomainException("Completed piercing security check result is missing.");
            }

            return RunEndAttackWithResult(state, action, current, battle, piercingCheck, false, trace);
        }

        var opponent = state.Players.First(player => player.Id != action.Actor);
        if (opponent.Security.Count == 0
            && TryFindPermanent(state, current.Attacker, out var directAttacker)
            && _keywordService.SecurityAttackCount(state, directAttacker) >= 1)
        {
            _winConditionChecker.ApplyDirectAttackWin(state, action.Actor);
            return RunEndAttackWithResult(state, action, current, null, null, true, trace);
        }

        var securityCheckResult = _securityCheckService.CheckSecurityWithResult(state, current.Attacker, opponent.Id, trace);
        if (securityCheckResult.HasPendingSelection)
        {
            return PendingSecurityCheck(action, current, securityCheckResult, BattleResult: null);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        return RunEndAttackWithResult(state, action, current, securityCheck.BattleResult, securityCheck, false, trace);
    }

    private AttackExecutionResult ResumeSecurityCheckWithResult(
        GameState state,
        AttackExecutionContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace)
    {
        var context = RequireContext(state, continuation);
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
                context,
                securityCheckResult,
                continuation.BattleResult);
        }

        var securityCheck = securityCheckResult.Result
            ?? throw new DomainException("Completed security check result is missing.");
        return ContinueAfterSecurityCheckWithResult(
            state,
            continuation.Action,
            context,
            securityCheck,
            continuation.BattleResult,
            trace);
    }

    private AttackExecutionResult ContinueAfterSecurityCheckWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        SecurityCheckResult securityCheck,
        BattleResolutionResult? battleResult,
        GameTrace? trace) =>
        RunEndAttackWithResult(state, action, context, battleResult ?? securityCheck.BattleResult, securityCheck, false, trace);

    private AttackExecutionResult RunEndAttackWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        BattleResolutionResult? battle,
        SecurityCheckResult? securityCheck,
        bool directWin,
        GameTrace? trace)
    {
        var endContext = state.RuntimeRules.Attack is null
            ? context.EndAttack()
            : state.RuntimeRules.EndAttack();
        endContext = endContext.WithState(AttackRuntimeState.EndAttack);

        var attackResult = new AttackResult(
            endContext.Attacker,
            endContext.Defender,
            directWin,
            securityCheck,
            battle);

        if (!ShouldRunOnEndAttack(state, endContext, directWin))
        {
            CleanupAttackRuntime(state);
            return new AttackExecutionResult(Attack: attackResult);
        }

        var values = AttackValues(endContext);
        values["DirectWin"] = directWin;
        values["Battle"] = battle;
        values["SecurityCheck"] = securityCheck;

        var endAttackTrigger = RunTriggerPipelineWithResult(
            state,
            EffectTiming.OnEndAttack,
            action.Actor,
            endContext.Attacker,
            values,
            trace);
        if (endAttackTrigger?.HasPendingSelection == true)
        {
            return PendingTrigger(
                endAttackTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.CompleteAfterEndAttack,
                    action,
                    endContext.WithState(AttackRuntimeState.Cleanup),
                    AttackResult: attackResult));
        }

        CleanupAttackRuntime(state);
        return new AttackExecutionResult(Attack: attackResult, TriggerResult: endAttackTrigger);
    }

    private AttackExecutionResult CompleteAfterEndAttack(GameState state, AttackResult? attackResult)
    {
        if (attackResult is null)
        {
            throw new DomainException("Completing OnEndAttack requires the resolved attack result.");
        }

        CleanupAttackRuntime(state);
        return new AttackExecutionResult(Attack: attackResult);
    }

    private static AttackExecutionResult PendingSecurityCheck(
        AttackAction action,
        AttackRuntimeContext context,
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
                context,
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

    private PreparedTriggerGroup? PrepareCounterEffects(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        var queued = new List<EffectResolution>();
        var background = new List<EffectResolution>();
        var values = AttackValues(context);
        values["AttackerTopCardWhenDeclared"] = context.AttackerTopCardWhenDeclared;
        values["CounterSourceSnapshot"] = context.CounterSources.ToArray();
        foreach (var source in context.CounterSources)
        {
            if (!state.Cards.ContainsKey(source))
            {
                continue;
            }

            var prepared = _triggerPipelineService.Prepare(
                state,
                EffectTiming.OnCounterTiming,
                action.Actor,
                sourceCard: source,
                values: values,
                options: CounterTimingOptions,
                descriptorFilter: descriptor => descriptor.IsCounterEffect);
            queued.AddRange(prepared.QueuedEffects);
            background.AddRange(prepared.BackgroundEffects);
        }

        var effectContext = new EffectContext(
            state,
            EffectTiming.OnCounterTiming,
            action.Actor,
            SourcePermanent: context.Attacker,
            Values: values);
        return new PreparedTriggerGroup(effectContext, queued, background, CounterTimingOptions);
    }

    private static IReadOnlyList<EffectResolution> SelectCounterChoiceGroup(
        GameState state,
        PreparedTriggerGroup? prepared)
    {
        if (prepared is null)
        {
            return Array.Empty<EffectResolution>();
        }

        var candidates = prepared.QueuedEffects.Concat(prepared.BackgroundEffects).ToArray();
        if (candidates.Length == 0)
        {
            return candidates;
        }

        var turnPlayerGroup = candidates
            .Where(candidate => (candidate.Controller ?? candidate.Context.Player) == state.TurnPlayerId)
            .ToArray();
        return turnPlayerGroup.Length > 0
            ? turnPlayerGroup
            : candidates
                .Where(candidate => (candidate.Controller ?? candidate.Context.Player) == state.NonTurnPlayerId)
                .ToArray();
    }

    private static SelectionRequest CreateCounterSelectionRequest(
        GameState state,
        AttackRuntimeContext context,
        IReadOnlyList<EffectResolution> candidates)
    {
        var player = candidates
            .Select(candidate => candidate.Controller ?? candidate.Context.Player)
            .FirstOrDefault(player => player is not null)
            ?? state.NonTurnPlayerId;
        var targets = candidates
            .Select((candidate, index) => new SelectableTarget(
                SelectionTargetKind.Option,
                candidate.StableId,
                candidate.Controller,
                candidate.SourceCard,
                candidate.SourcePermanent,
                Label: candidate.StableId,
                OptionValue: candidate.StableId))
            .ToArray();
        return new SelectionRequest(
            $"attack:{context.Attacker.Value}:counter-selection",
            player,
            SelectionKind.ChooseAction,
            SelectionTargetKind.Option,
            minCount: 0,
            maxCount: 1,
            canSkip: true,
            canEndNotMax: true,
            targets,
            "Select one Counter effect to use.");
    }

    private AttackExecutionResult ValidateStageThen(
        GameState state,
        AttackAction action,
        AttackRuntimeState stage,
        GameTrace? trace,
        Func<AttackExecutionResult> next)
    {
        var context = state.RuntimeRules.Attack;
        if (context is null)
        {
            return next();
        }

        state.RuntimeRules.SetAttack(context.WithState(stage));
        context = state.RuntimeRules.RequireAttack();
        if (context.IsEndAttack || !AttackerExists(state, context.Attacker))
        {
            return RunEndAttackWithResult(state, action, context, null, null, false, trace);
        }

        if (context.Defender is { } defender && !IsValidBattleDefender(state, action, defender))
        {
            return RunEndAttackWithResult(state, action, context.EndAttack(), null, null, false, trace);
        }

        if (context.Blocker is { } blocker && !IsValidBattleDefender(state, action, blocker))
        {
            state.RuntimeRules.SetAttack(context.SetBlocking(false).EndAttack());
            return RunEndAttackWithResult(state, action, state.RuntimeRules.RequireAttack(), null, null, false, trace);
        }

        return next();
    }

    private AttackExecutionResult RunPendingSwitchThenNext(
        GameState state,
        AttackAction action,
        AttackExecutionContinuationKind targetChangedNext,
        GameTrace? trace,
        Func<AttackExecutionResult> next)
    {
        if (state.RuntimeRules.Attack?.PendingTargetSwitch is null)
        {
            return next();
        }

        return RunAttackTargetChangedIfNeededWithResult(state, action, targetChangedNext, trace);
    }

    private void CleanupAttackRuntime(GameState state)
    {
        _durationCleanupService.CleanupAttackEnd(state);
        state.RuntimeRules.ClearAttack();
    }

    private static EffectResolution CreateAttackDecisionResolution(
        GameState state,
        AttackRuntimeContext context,
        EffectTiming timing,
        string stableIdSuffix)
    {
        var sourceCard = TryFindPermanent(state, context.Attacker, out var attacker)
            ? attacker.TopCardId
            : context.AttackerTopCardWhenDeclared;
        var controller = attacker?.ControllerPlayerId;
        var effectContext = new EffectContext(
            state,
            timing,
            controller,
            sourceCard,
            context.Attacker,
            AttackValues(context));

        return new EffectResolution(
            $"attack:{context.Attacker.Value}:{stableIdSuffix}",
            timing,
            sourceCard,
            context.Attacker,
            controller,
            IsBackground: false,
            IsOptional: false,
            effectContext);
    }

    private static TriggerPipelineResult EmptyTriggerResult(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        AttackRuntimeContext context) =>
        new(
            new EffectContext(state, timing, player, SourcePermanent: context.Attacker, Values: AttackValues(context)),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            Array.Empty<SelectionResultApplicationResult>());

    private static AttackRuntimeContext RequireContext(GameState state, AttackExecutionContinuation continuation)
    {
        var context = state.RuntimeRules.Attack ?? continuation.Context;
        if (context is null)
        {
            throw new DomainException($"Attack continuation '{continuation.Kind}' requires attack runtime context.");
        }

        if (state.RuntimeRules.Attack is null)
        {
            state.RuntimeRules.StartAttack(context);
        }

        return state.RuntimeRules.RequireAttack();
    }

    private static void RequireValidAttackTarget(GameState state, AttackAction action, PermanentId defenderId)
    {
        var defender = BattleRules.Permanent(state, defenderId);
        if (defender.ControllerPlayerId == action.Actor || defender.IsBreedingArea || !BattleRules.IsDigimon(state, defender.TopCardId))
        {
            throw new DomainException($"Permanent '{defenderId}' is not a valid attack target.");
        }
    }

    private bool IsValidBattleDefender(GameState state, AttackAction action, PermanentId defenderId)
    {
        if (!TryFindPermanent(state, defenderId, out var defender))
        {
            return false;
        }

        return defender.ControllerPlayerId != action.Actor
            && !defender.IsBreedingArea
            && BattleRules.IsDigimon(state, defender.TopCardId)
            && _effectiveStats.Dp(state, defender) > 0;
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

    private static bool ShouldRunOnEndAttack(GameState state, AttackRuntimeContext context, bool directWin) =>
        !directWin
        && !state.IsGameOver
        && TryFindPermanent(state, context.Attacker, out var attacker)
        && BattleRules.IsDigimon(state, attacker.TopCardId);

    private static IReadOnlyList<CardInstanceId> PermanentCards(PermanentState permanent) =>
        new[] { permanent.TopCardId }
            .Concat(permanent.SourceCardIds)
            .Concat(permanent.LinkedCards)
            .ToArray();

    private static Dictionary<string, object?> AttackValues(
        AttackRuntimeContext context,
        AttackTargetSwitch? targetSwitch = null) =>
        new()
        {
            ["Attacker"] = context.Attacker,
            ["Defender"] = context.Defender,
            ["Blocker"] = context.Blocker,
            ["IsBlocking"] = context.IsBlocking,
            ["PreviousDefender"] = targetSwitch?.OldDefender,
            ["NewDefender"] = targetSwitch?.NewDefender,
            ["IsBlock"] = targetSwitch?.IsBlock ?? context.IsBlocking,
            ["SourceEffectStableId"] = targetSwitch?.SourceEffectStableId,
            ["AttackState"] = context.State,
            ["AttackerTopCardWhenDeclared"] = context.AttackerTopCardWhenDeclared,
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
