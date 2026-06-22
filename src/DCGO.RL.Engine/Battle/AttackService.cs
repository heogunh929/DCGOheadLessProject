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
    ContinueAfterAttackTargetChangedToCounterNonCounter,
    ContinueAfterAttackTargetChangedToCounterCounter,
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

internal sealed record CounterCandidate(EffectResolution Resolution, string StableId);

public sealed class AttackService
{
    private static readonly TriggerPipelineOptions CounterTimingOptions = new(
        TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
        | TriggerSourceZone.Hand
        | TriggerSourceZone.Trash
        | TriggerSourceZone.FaceUpSecurity,
        ResolveAfterEffectsActivate: true);

    private static readonly TriggerPipelineOptions CounterCutInSourceOptions = new(
        TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
        | TriggerSourceZone.Hand
        | TriggerSourceZone.Trash
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
    private readonly StaticEffectService? _staticEffects;

    public AttackService(
        BattleResolver? battleResolver = null,
        SecurityCheckService? securityCheckService = null,
        WinConditionChecker? winConditionChecker = null,
        BattleKeywordService? keywordService = null,
        DurationCleanupService? durationCleanupService = null,
        TriggerPipelineService? triggerPipelineService = null,
        EffectiveStatService? effectiveStats = null,
        Tier1PrimitiveService? primitiveService = null,
        StaticEffectService? staticEffects = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _staticEffects = staticEffects;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats, _staticEffects);
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

    internal StaticEffectService? RuntimeStaticEffectService => _staticEffects;

    public SelectionRequest? CreateBlockerSelectionRequest(GameState state, AttackAction action)
    {
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can attack.");
        }

        var attacker = BattleRules.Permanent(state, action.Attacker);
        if (!BattleRules.CanAttack(state, attacker, _keywordService, _effectiveStats, _staticEffects))
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

        if (!BattleRules.CanAttack(state, attacker, _keywordService, _effectiveStats, _staticEffects))
        {
            throw new DomainException($"Permanent '{action.Attacker}' cannot attack.");
        }

        if (action.Defender is not null)
        {
            RequireValidAttackTarget(state, action, action.Defender.Value);
        }

        var context = new AttackRuntimeContext(
            action.Attacker,
            action.Defender,
            AttackRuntimeState.CounterNonCounter,
            AttackerTopCardWhenDeclared: attacker.TopCardId,
            CounterSourceSnapshot: PermanentCards(attacker));
        state.RuntimeRules.StartAttack(context);

        if (!SuspendForAttackDeclaration(state, action.Attacker))
        {
            state.RuntimeRules.ClearAttack();
            throw new DomainException($"Permanent '{action.Attacker}' could not be suspended for attack declaration.");
        }

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
        GameTrace? trace = null,
        TriggerPipelineResult? completedPipelineResult = null)
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
                CompleteCounterEffectWithResult(
                    state,
                    continuation.Action,
                    RequireContext(state, continuation),
                    completedPipelineResult,
                    trace),

            AttackExecutionContinuationKind.ResolveCounterSelection =>
                throw new DomainException("Counter selection attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.ResolveBlockerSelection =>
                throw new DomainException("Blocker selection attack continuation requires a SelectionResult."),

            AttackExecutionContinuationKind.ContinueAfterBlockTrigger =>
                ContinueAfterBlockTriggerWithResult(
                    state,
                    continuation.Action,
                    trace),

            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterNonCounter =>
                ContinueAfterAttackTargetChangedWithResult(
                    state,
                    continuation.Action,
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterNonCounter,
                    trace),

            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterCounter =>
                ContinueAfterAttackTargetChangedWithResult(
                    state,
                    continuation.Action,
                    AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterCounter,
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
                AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterNonCounter,
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
                descriptor => !descriptor.IsCounterEffect,
                CounterCutInSourceOptions);
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
                AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterCounter,
                trace,
                () => RunCounterCounterWithResult(state, action, context.WithState(AttackRuntimeState.CounterCounter), trace)));

    private AttackExecutionResult RunCounterCounterWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        state.RuntimeRules.SetAttack(context with
        {
            State = AttackRuntimeState.CounterCounter,
            CounterGroup = CounterWindowGroup.TurnPlayer,
            CounterUsed = false,
            AttemptedCounterCandidateIds = null,
        });
        return ValidateStageThen(state, action, AttackRuntimeState.CounterCounter, trace, () =>
            RunNextCounterGroupWithResult(state, action, state.RuntimeRules.RequireAttack(), trace));
    }

    private AttackExecutionResult RunNextCounterGroupWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        GameTrace? trace)
    {
        if (context.CounterUsed || context.CounterGroup == CounterWindowGroup.Done)
        {
            return ContinueAfterCounterCounterWithResult(
                state,
                action,
                context.WithState(AttackRuntimeState.BlockDesignation),
                trace);
        }

        var prepared = PrepareCounterEffects(state, action, context);
        var candidates = SelectCounterChoiceGroup(state, prepared, context);
        if (candidates.Count == 0)
        {
            var advanced = context.AdvanceCounterGroup();
            state.RuntimeRules.SetAttack(advanced);
            return RunNextCounterGroupWithResult(state, action, advanced, trace);
        }

        if (candidates.Count == 1)
        {
            return RunSelectedCounterCandidateWithResult(
                state,
                action,
                context.WithState(AttackRuntimeState.CounterCounter),
                prepared,
                candidates[0],
                consumeOptionalSelection: false,
                trace);
        }

        var selectionContext = context.WithState(AttackRuntimeState.CounterSelection);
        state.RuntimeRules.SetAttack(selectionContext);
        var request = CreateCounterSelectionRequest(state, selectionContext, candidates);
        var resolution = CreateAttackDecisionResolution(
            state,
            selectionContext,
            EffectTiming.OnCounterTiming,
            $"counter-selection:{selectionContext.CounterGroup}");
        return new AttackExecutionResult(
            Attack: null,
            Continuation: new AttackExecutionContinuation(
                AttackExecutionContinuationKind.ResolveCounterSelection,
                action,
                selectionContext),
            DirectSelectionRequest: request,
            DirectPendingResolution: resolution);
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
        var candidates = SelectCounterChoiceGroup(state, prepared, context);
        var request = CreateCounterSelectionRequest(state, context, candidates);
        SelectionValidator.Validate(request, selectionResult);
        if (selectionResult.SelectedTargets.Count == 0 && string.IsNullOrWhiteSpace(selectionResult.SelectedOption))
        {
            var advanced = context.AdvanceCounterGroup();
            state.RuntimeRules.SetAttack(advanced);
            return RunNextCounterGroupWithResult(state, continuation.Action, advanced, trace);
        }

        var selectedId = selectionResult.SelectedOption
            ?? selectionResult.SelectedTargets.SingleOrDefault()?.StableId
            ?? throw new DomainException("Counter selection requires a selected effect.");
        var selected = FindCounterCandidateBySelectionId(candidates, selectedId)
            ?? throw new DomainException($"Counter effect '{selectedId}' is no longer a legal candidate.");

        return RunSelectedCounterCandidateWithResult(
            state,
            continuation.Action,
            context,
            prepared,
            selected,
            consumeOptionalSelection: selected.Resolution.CounterSelectionConsumesOptional,
            trace);
    }

    private AttackExecutionResult RunSelectedCounterCandidateWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        PreparedTriggerGroup? prepared,
        CounterCandidate candidate,
        bool consumeOptionalSelection,
        GameTrace? trace)
    {
        var selected = candidate.Resolution;
        if (consumeOptionalSelection)
        {
            selected = selected with
            {
                IsOptional = false,
                OptionalSelectionRequest = null,
            };
        }

        var attempted = context
            .WithState(AttackRuntimeState.CounterCounter)
            .MarkCounterCandidateAttempted(candidate.StableId);
        state.RuntimeRules.SetAttack(attempted);

        var selectedPrepared = new PreparedTriggerGroup(
            prepared?.Context ?? selected.Context,
            new[] { selected },
            Array.Empty<EffectResolution>(),
            prepared?.Options ?? CounterTimingOptions);
        var counterTrigger = _triggerPipelineService!.RunPrepared(state, selectedPrepared, trace);
        PersistCounterExecutionIfAny(state, counterTrigger);
        if (counterTrigger.HasPendingSelection)
        {
            return PendingTrigger(
                counterTrigger,
                new AttackExecutionContinuation(
                    AttackExecutionContinuationKind.ContinueAfterCounterCounter,
                    action,
                    state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterCounter)));
        }

        return CompleteCounterEffectWithResult(
            state,
            action,
            state.RuntimeRules.RequireAttack(),
            counterTrigger,
            trace);
    }

    private AttackExecutionResult CompleteCounterEffectWithResult(
        GameState state,
        AttackAction action,
        AttackRuntimeContext context,
        TriggerPipelineResult? completedPipelineResult,
        GameTrace? trace)
    {
        var counterUsed = PersistCounterExecutionIfAny(state, completedPipelineResult)
            || state.RuntimeRules.RequireAttack().CounterUsed;
        if (counterUsed)
        {
            return ContinueAfterCounterCounterWithResult(
                state,
                action,
                state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.BlockDesignation),
                trace);
        }

        return RunNextCounterGroupWithResult(
            state,
            action,
            state.RuntimeRules.RequireAttack().WithState(AttackRuntimeState.CounterCounter),
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

        return ContinueAfterBlockTriggerWithResult(
            state,
            action,
            trace);
    }

    private AttackExecutionResult ContinueAfterBlockTriggerWithResult(
        GameState state,
        AttackAction action,
        GameTrace? trace)
    {
        var context = state.RuntimeRules.RequireAttack();
        if (!AttackerExists(state, context.Attacker))
        {
            return RunEndAttackWithResult(state, action, context, null, null, false, trace);
        }

        if (context.Blocker is { } blocker && !IsValidBattleDefender(state, action, blocker))
        {
            var cleared = context.SetBlocking(false).WithState(AttackRuntimeState.Battle);
            state.RuntimeRules.SetAttack(cleared);
            _ = state.RuntimeRules.ConsumePendingAttackTargetSwitch();
            return ResolveBattleWithResult(state, action, state.RuntimeRules.RequireAttack(), trace);
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
        if (HasPendingTargetSwitches(state))
        {
            return RunAttackTargetChangedIfNeededWithResult(state, action, nextKind, trace);
        }

        var context = state.RuntimeRules.RequireAttack();
        return nextKind switch
        {
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterNonCounter =>
                RunCounterNonCounterWithResult(state, action, context.WithState(AttackRuntimeState.CounterNonCounter), trace),
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToCounterCounter =>
                RunCounterCounterWithResult(state, action, context.WithState(AttackRuntimeState.CounterCounter), trace),
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBlockDesignation =>
                RunBlockDesignationWithResult(state, action, context.WithState(AttackRuntimeState.BlockDesignation), trace),
            AttackExecutionContinuationKind.ContinueAfterAttackTargetChangedToBattle =>
                ResolveBattleWithResult(state, action, context.WithState(AttackRuntimeState.Battle), trace),
            _ => throw new DomainException($"Unsupported attack target changed continuation '{nextKind}'."),
        };
    }

    private static bool PersistCounterExecutionIfAny(GameState state, TriggerPipelineResult? result)
    {
        if (result?.ExecutedEffects.Any(effect => effect.IsCounterEffect) != true)
        {
            return false;
        }

        var context = state.RuntimeRules.RequireAttack();
        if (!context.CounterUsed)
        {
            state.RuntimeRules.SetAttack(context.MarkCounterUsed());
        }

        return true;
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
        Func<EffectDescriptor, bool>? descriptorFilter = null,
        TriggerPipelineOptions? options = null)
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
            options: options,
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

        return _triggerPipelineService.Prepare(
            state,
            EffectTiming.OnCounterTiming,
            action.Actor,
            sourcePermanent: context.Attacker,
            values: AttackValues(context),
            options: CounterCutInSourceOptions,
            descriptorFilter: descriptor => descriptor.IsCounterEffect);
    }

    private static IReadOnlyList<CounterCandidate> SelectCounterChoiceGroup(
        GameState state,
        PreparedTriggerGroup? prepared,
        AttackRuntimeContext context)
    {
        if (prepared is null)
        {
            return Array.Empty<CounterCandidate>();
        }

        var candidates = prepared.QueuedEffects
            .Where(candidate => candidate.CanActivate?.Invoke(candidate.Context) ?? true)
            .ToArray();
        if (candidates.Length == 0)
        {
            return Array.Empty<CounterCandidate>();
        }

        var player = context.CounterGroup switch
        {
            CounterWindowGroup.TurnPlayer => state.TurnPlayerId,
            CounterWindowGroup.NonTurnPlayer => state.NonTurnPlayerId,
            _ => (PlayerId?)null,
        };
        return player is null
            ? Array.Empty<CounterCandidate>()
            : candidates
                .Where(candidate => (candidate.Controller ?? candidate.Context.Player) == player)
                .Select((candidate, index) => new CounterCandidate(
                    candidate,
                    CreateCounterCandidateSelectionId(candidate, index)))
                .Where(candidate => !context.AttemptedCounterCandidates.Contains(candidate.StableId, StringComparer.Ordinal))
                .ToArray();
    }

    private static SelectionRequest CreateCounterSelectionRequest(
        GameState state,
        AttackRuntimeContext context,
        IReadOnlyList<CounterCandidate> candidates)
    {
        var player = candidates.Count > 0
            ? CounterController(candidates[0].Resolution) ?? state.NonTurnPlayerId
            : state.NonTurnPlayerId;
        var canSkip = candidates.Count > 0 && candidates.All(candidate => candidate.Resolution.IsSkippable);
        var targets = candidates
            .Select(candidate => new SelectableTarget(
                SelectionTargetKind.Option,
                candidate.StableId,
                CounterController(candidate.Resolution),
                candidate.Resolution.SourceCard,
                candidate.Resolution.SourcePermanent,
                Label: candidate.Resolution.StableId,
                OptionValue: candidate.StableId))
            .ToArray();
        return new SelectionRequest(
            $"attack:{context.Attacker.Value}:counter-selection:{context.CounterGroup}",
            player,
            SelectionKind.ChooseAction,
            SelectionTargetKind.Option,
            minCount: 1,
            maxCount: 1,
            canSkip,
            canEndNotMax: canSkip,
            targets,
            "Select one Counter effect to use.");
    }

    private static PlayerId? CounterController(EffectResolution candidate) =>
        candidate.Controller ?? candidate.Context.Player;

    private static string CreateCounterCandidateSelectionId(EffectResolution candidate, int candidateIndex)
    {
        var sourceCard = candidate.SourceCard?.Value.ToString() ?? "-";
        var sourcePermanent = candidate.SourcePermanent?.Value.ToString() ?? "-";
        return $"counter:{candidateIndex}:card:{sourceCard}:perm:{sourcePermanent}:effect:{candidate.StableId}";
    }

    private static CounterCandidate? FindCounterCandidateBySelectionId(
        IReadOnlyList<CounterCandidate> candidates,
        string selectedId)
    {
        return candidates.FirstOrDefault(candidate => string.Equals(candidate.StableId, selectedId, StringComparison.Ordinal));
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

        if (RequiresLiveDefender(stage)
            && context.Defender is { } defender
            && !IsValidBattleDefender(state, action, defender))
        {
            return RunEndAttackWithResult(state, action, context.EndAttack(), null, null, false, trace);
        }

        if (RequiresLiveBlocker(stage)
            && context.Blocker is { } blocker
            && !IsValidBattleDefender(state, action, blocker))
        {
            state.RuntimeRules.SetAttack(context.SetBlocking(false).EndAttack());
            return RunEndAttackWithResult(state, action, state.RuntimeRules.RequireAttack(), null, null, false, trace);
        }

        return next();
    }

    private static bool RequiresLiveDefender(AttackRuntimeState stage) =>
        stage == AttackRuntimeState.Battle;

    private static bool RequiresLiveBlocker(AttackRuntimeState stage) =>
        stage == AttackRuntimeState.AttackTargetChanged
        || stage == AttackRuntimeState.Battle;

    private static bool HasPendingTargetSwitches(GameState state) =>
        state.RuntimeRules.Attack?.TargetSwitchQueue.Count > 0;

    private AttackExecutionResult RunPendingSwitchThenNext(
        GameState state,
        AttackAction action,
        AttackExecutionContinuationKind targetChangedNext,
        GameTrace? trace,
        Func<AttackExecutionResult> next)
    {
        if (!HasPendingTargetSwitches(state))
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

    private bool SuspendForAttackDeclaration(GameState state, PermanentId attacker) =>
        _primitives.Suspend(state, attacker);

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
        AttackTargetSwitch? targetSwitch = null)
    {
        var defender = targetSwitch?.NewDefender ?? context.Defender;
        var blocker = targetSwitch is not null ? targetSwitch.Blocker : context.Blocker;
        var isBlocking = targetSwitch?.IsBlocking ?? context.IsBlocking;
        return new()
        {
            ["Attacker"] = context.Attacker,
            ["Defender"] = defender,
            ["Blocker"] = blocker,
            ["IsBlocking"] = isBlocking,
            ["PreviousDefender"] = targetSwitch?.OldDefender,
            ["NewDefender"] = targetSwitch?.NewDefender,
            ["IsBlock"] = isBlocking,
            ["SourceEffectStableId"] = targetSwitch?.SourceEffectStableId,
            ["AttackState"] = context.State,
            ["AttackerTopCardWhenDeclared"] = context.AttackerTopCardWhenDeclared,
            ["CounterSourceSnapshot"] = context.CounterSources.ToArray(),
        };
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
