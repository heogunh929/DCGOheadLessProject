using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public enum EngineStepStatus
{
    Completed,
    PausedForDecision,
    GameOver,
}

public sealed record DecisionResult(PlayerId Player, DecisionToken Token, SelectionResult SelectionResult);

public sealed record EngineStepResult(
    EngineStepStatus Status,
    GameAction? CompletedAction,
    EffectTiming? CompletedTiming,
    DecisionPoint? PendingDecisionPoint,
    DecisionToken? PendingDecisionToken,
    string? PendingStableContinuationId,
    bool RulesProcessed,
    bool IsTerminal,
    IReadOnlyList<TraceEvent> TraceDelta)
{
    public bool IsPaused => Status == EngineStepStatus.PausedForDecision;
}

public sealed class EngineSession
{
    private readonly BattleEngineServices _services;
    private PendingEngineInteraction? _pending;
    private long _nextDecisionToken = 1;

    public EngineSession(BattleEngineServices services, GameState state, GameTrace? trace = null)
    {
        _services = services ?? throw new ArgumentNullException(nameof(services));
        State = state ?? throw new ArgumentNullException(nameof(state));
        Trace = trace ?? new GameTrace();
    }

    public GameState State { get; }

    public GameTrace Trace { get; }

    public DecisionPoint? PendingDecisionPoint => _pending?.DecisionPoint;

    public bool HasPendingDecision => _pending is not null;

    public EngineStepResult RunToMainPhase()
    {
        if (_pending is not null)
        {
            throw new DomainException(
                $"Cannot advance to main phase while SelectionRequest '{_pending.Request.Id}' is pending.");
        }

        var traceStart = Trace.Events.Count;
        var beforePhase = State.Clone();

        if (State.Phase is Phase.None or Phase.End)
        {
            var activeResult = _services.PhaseRunner.RunActivePhaseWithResult(State, Trace);
            if (activeResult.HasPendingSelection)
            {
                Trace.AddPhase("phase:RunToMainPhase", beforePhase, State, "RunToMainPhase");
                _pending = PendingEngineInteraction.FromPhaseResult(
                    State,
                    activeResult,
                    NextDecisionToken(),
                    continueToMainPhaseAfterResume: true);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(action: null, activeResult.PendingResolution?.Timing, traceStart);
            }
        }

        return CompleteRunToMainPhase(traceStart, beforePhase);
    }

    public EngineStepResult RunMainPhase()
    {
        if (_pending is not null)
        {
            throw new DomainException(
                $"Cannot advance to main phase while SelectionRequest '{_pending.Request.Id}' is pending.");
        }

        var traceStart = Trace.Events.Count;
        var beforePhase = State.Clone();
        var result = _services.PhaseRunner.RunMainPhaseWithResult(State, Trace);
        if (result.HasPendingSelection)
        {
            Trace.AddPhase("phase:RunMainPhase", beforePhase, State, "RunMainPhase");
            _pending = PendingEngineInteraction.FromPhaseResult(State, result, NextDecisionToken());
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(action: null, result.PendingResolution?.Timing, traceStart);
        }

        Trace.AddPhase("phase:RunMainPhase", beforePhase, State, "RunMainPhase");
        var rulesResult = _services.RuleProcessor.ProcessAfterActionWithResult(State, Trace);
        if (rulesResult.HasPendingSelection)
        {
            _pending = PendingEngineInteraction.FromRuleResult(State, rulesResult, NextDecisionToken());
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(action: null, rulesResult.PendingResolution?.Timing, traceStart);
        }

        return CompletedResult(action: null, result.Timing, rulesProcessed: true, traceStart);
    }

    public EngineStepResult Step(GameAction action)
    {
        ArgumentNullException.ThrowIfNull(action);
        if (_pending is not null)
        {
            throw new DomainException(
                $"Cannot execute a new action while SelectionRequest '{_pending.Request.Id}' is pending.");
        }

        var traceStart = Trace.Events.Count;
        var result = _services.ActionExecutor.Execute(State, action, Trace);
        if (result.HasPendingSelection)
        {
            _pending = PendingEngineInteraction.FromActionResult(State, result, NextDecisionToken());
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(action, result.PendingResolution?.Timing, traceStart);
        }

        return CompletedResult(action, result.PhaseExecution?.Timing, result.RulesProcessed, traceStart);
    }

    public EngineStepResult Resume(DecisionResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        if (_pending is null)
        {
            throw new DomainException("Cannot resume because no engine decision is pending.");
        }

        var pending = _pending;
        if (result.Player != pending.Request.Player)
        {
            throw new DomainException(
                $"SelectionRequest '{pending.Request.Id}' is for player '{pending.Request.Player}', not '{result.Player}'.");
        }

        if (result.Token != pending.DecisionToken)
        {
            throw new DomainException(
                $"DecisionToken '{result.Token}' does not match pending token '{pending.DecisionToken}' for request '{pending.Request.Id}'.");
        }

        if (!string.Equals(result.SelectionResult.RequestId, pending.Request.Id, StringComparison.Ordinal))
        {
            throw new DomainException(
                $"SelectionResult request id '{result.SelectionResult.RequestId}' does not match pending request '{pending.Request.Id}'.");
        }

        var traceStart = Trace.Events.Count;
        var beforeSelection = State.Clone();
        if (pending.Continuation is null)
        {
            if (pending.AttackContinuation is null)
            {
                throw new DomainException("Pending engine interaction has no resumable effect continuation.");
            }

            var attackSelectionResult = _services.AttackService.ResumeAttackContinuationWithResult(
                State,
                pending.AttackContinuation,
                result.SelectionResult,
                Trace);
            if (attackSelectionResult.HasPendingSelection)
            {
                _pending = pending.NextAttack(State, attackSelectionResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, attackSelectionResult.PendingResolution?.Timing, traceStart);
            }

            _pending = null;
            var securityRulesResult = _services.RuleProcessor.ProcessAfterActionWithResult(State, Trace);
            if (securityRulesResult.HasPendingSelection)
            {
                _pending = pending.NextRule(State, securityRulesResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, securityRulesResult.PendingResolution?.Timing, traceStart);
            }

            Trace.AddSelection(
                $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                beforeSelection,
                State,
                result);
            return CompletedResult(pending.Action, pending.Resolution.Timing, rulesProcessed: true, traceStart);
        }

        var pipelineResult = _services.TriggerPipelineService.Resume(
            State,
            pending.Continuation,
            result.SelectionResult,
            Trace);

        if (pipelineResult.HasPendingSelection)
        {
            _pending = pending.Next(State, pipelineResult, NextDecisionToken());
            Trace.AddSelection(
                $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                beforeSelection,
                State,
                result);
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(pending.Action, pipelineResult.PendingResolution?.Timing, traceStart);
        }

        if (pending.OptionCard is { } optionCard)
        {
            _services.PlayCardService.CompleteOptionPipelineResult(State, optionCard, pipelineResult, Trace);
        }

        if (pending.AttackContinuation is not null)
        {
            var attackTailResult = _services.AttackService.CompleteAttackContinuationWithResult(
                State,
                pending.AttackContinuation,
                Trace);
            if (attackTailResult.HasPendingSelection)
            {
                _pending = pending.NextAttack(State, attackTailResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, attackTailResult.PendingResolution?.Timing, traceStart);
            }
        }

        var rulesAlreadyProcessed = pending.Resolution.Timing == EffectTiming.RulesTiming;

        if (pending.PhaseContinuation is not null)
        {
            var phaseTailResult = CompletePhaseContinuation(pending.PhaseContinuation);
            if (phaseTailResult.HasPendingSelection)
            {
                _pending = pending.NextPhase(State, phaseTailResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, phaseTailResult.PendingResolution?.Timing, traceStart);
            }
        }

        if (pending.RuleContinuation is not null)
        {
            var ruleTailResult = _services.RuleProcessor.CompleteRuleContinuationWithResult(
                State,
                pending.RuleContinuation,
                Trace);
            if (ruleTailResult.HasPendingSelection)
            {
                _pending = pending.NextRule(State, ruleTailResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, ruleTailResult.PendingResolution?.Timing, traceStart);
            }

            rulesAlreadyProcessed = true;
        }

        if (pending.ContinueToMainPhaseAfterResume)
        {
            return ContinueRunToMainPhaseAfterSelection(pending, beforeSelection, result, traceStart);
        }

        _pending = null;
        if (!rulesAlreadyProcessed)
        {
            var rulesResult = _services.RuleProcessor.ProcessAfterActionWithResult(State, Trace);
            if (rulesResult.HasPendingSelection)
            {
                _pending = pending.NextRule(State, rulesResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(pending.Action, rulesResult.PendingResolution?.Timing, traceStart);
            }
        }

        Trace.AddSelection(
            $"selection:{pending.StableContinuationId}:{pending.Request.Id}",
            beforeSelection,
            State,
            result);
        return CompletedResult(pending.Action, pending.Resolution.Timing, rulesProcessed: true, traceStart);
    }

    private DecisionToken NextDecisionToken() => new(_nextDecisionToken++);

    private EngineStepResult CompleteRunToMainPhase(int traceStart, GameState beforePhase)
    {
        if (State.Phase == Phase.Active)
        {
            _services.PhaseRunner.RunDrawPhase(State);
        }

        if (State.IsGameOver)
        {
            Trace.AddPhase("phase:RunToMainPhase", beforePhase, State, "RunToMainPhase");
            return CompletedResult(action: null, completedTiming: null, rulesProcessed: false, traceStart);
        }

        if (State.Phase == Phase.Draw)
        {
            _services.PhaseRunner.RunBreedingPhase(State);
        }

        if (State.Phase == Phase.Breeding)
        {
            var mainResult = _services.PhaseRunner.RunMainPhaseWithResult(State, Trace);
            if (mainResult.HasPendingSelection)
            {
                Trace.AddPhase("phase:RunToMainPhase", beforePhase, State, "RunToMainPhase");
                _pending = PendingEngineInteraction.FromPhaseResult(State, mainResult, NextDecisionToken());
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(action: null, mainResult.PendingResolution?.Timing, traceStart);
            }
        }

        Trace.AddPhase("phase:RunToMainPhase", beforePhase, State, "RunToMainPhase");
        var rulesResult = _services.RuleProcessor.ProcessAfterActionWithResult(State, Trace);
        if (rulesResult.HasPendingSelection)
        {
            _pending = PendingEngineInteraction.FromRuleResult(State, rulesResult, NextDecisionToken());
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(action: null, rulesResult.PendingResolution?.Timing, traceStart);
        }

        return CompletedResult(action: null, EffectTiming.OnStartMainPhase, rulesProcessed: true, traceStart);
    }

    private EngineStepResult ContinueRunToMainPhaseAfterSelection(
        PendingEngineInteraction completedPending,
        GameState beforeSelection,
        DecisionResult result,
        int traceStart)
    {
        _pending = null;

        if (State.Phase == Phase.Active)
        {
            _services.PhaseRunner.RunDrawPhase(State);
        }

        if (!State.IsGameOver && State.Phase == Phase.Draw)
        {
            _services.PhaseRunner.RunBreedingPhase(State);
        }

        if (!State.IsGameOver && State.Phase == Phase.Breeding)
        {
            var mainResult = _services.PhaseRunner.RunMainPhaseWithResult(State, Trace);
            if (mainResult.HasPendingSelection)
            {
                _pending = PendingEngineInteraction.FromPhaseResult(State, mainResult, NextDecisionToken());
                Trace.AddSelection(
                    $"selection:{completedPending.StableContinuationId}:{completedPending.Request.Id}",
                    beforeSelection,
                    State,
                    result);
                Trace.AddDecision(
                    $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                    State,
                    _pending.DecisionPoint);
                return PausedResult(action: null, mainResult.PendingResolution?.Timing, traceStart);
            }
        }

        var rulesResult = _services.RuleProcessor.ProcessAfterActionWithResult(State, Trace);
        if (rulesResult.HasPendingSelection)
        {
            _pending = PendingEngineInteraction.FromRuleResult(State, rulesResult, NextDecisionToken());
            Trace.AddSelection(
                $"selection:{completedPending.StableContinuationId}:{completedPending.Request.Id}",
                beforeSelection,
                State,
                result);
            Trace.AddDecision(
                $"decision:{_pending.StableContinuationId}:{_pending.Request.Id}",
                State,
                _pending.DecisionPoint);
            return PausedResult(action: null, rulesResult.PendingResolution?.Timing, traceStart);
        }

        Trace.AddSelection(
            $"selection:{completedPending.StableContinuationId}:{completedPending.Request.Id}",
            beforeSelection,
            State,
            result);
        return CompletedResult(action: null, EffectTiming.OnStartMainPhase, rulesProcessed: true, traceStart);
    }

    private EngineStepResult PausedResult(GameAction? action, EffectTiming? timing, int traceStart) =>
        new(
            EngineStepStatus.PausedForDecision,
            action,
            timing,
            _pending?.DecisionPoint,
            _pending?.DecisionToken,
            _pending?.StableContinuationId,
            RulesProcessed: false,
            State.IsGameOver,
            Trace.Events.Skip(traceStart).ToArray());

    private EngineStepResult CompletedResult(GameAction? action, EffectTiming? completedTiming, bool rulesProcessed, int traceStart) =>
        new(
            State.IsGameOver ? EngineStepStatus.GameOver : EngineStepStatus.Completed,
            action,
            completedTiming,
            PendingDecisionPoint: null,
            PendingDecisionToken: null,
            PendingStableContinuationId: null,
            rulesProcessed,
            State.IsGameOver,
            Trace.Events.Skip(traceStart).ToArray());

    private PhaseExecutionResult CompletePhaseContinuation(PhaseExecutionContinuation continuation) =>
        continuation.Kind switch
        {
            PhaseExecutionContinuationKind.CompleteEndCurrentTurn =>
                _services.PhaseRunner.CompleteEndCurrentTurnAfterEndTriggerWithResult(
                    State,
                    continuation.MemoryForNextPlayer,
                    Trace),
            _ => throw new UnsupportedMechanicException($"Phase continuation '{continuation.Kind}'"),
        };

    private sealed record PendingEngineInteraction(
        GameAction? Action,
        TriggerPipelineContinuation? Continuation,
        CardInstanceId? OptionCard,
        PhaseExecutionContinuation? PhaseContinuation,
        AttackExecutionContinuation? AttackContinuation,
        RuleProcessorContinuation? RuleContinuation,
        bool ContinueToMainPhaseAfterResume,
        EffectResolution Resolution,
        SelectionRequest Request,
        DecisionToken DecisionToken,
        DecisionPoint DecisionPoint)
    {
        public string StableContinuationId =>
            Continuation?.StableContinuationId
            ?? AttackContinuation?.SecurityCheckContinuation?.StableContinuationId
            ?? AttackContinuation?.Context?.State.ToString()
            ?? Resolution.StableId;

        public static PendingEngineInteraction FromActionResult(
            GameState state,
            ActionExecutionResult result,
            DecisionToken token)
        {
            if (result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending action result requires a resolution and selection request.");
            }

            if (result.PendingContinuation is null
                && result.AttackExecution?.Continuation is null)
            {
                throw new DomainException("Pending action result requires a trigger or attack continuation.");
            }

            return new PendingEngineInteraction(
                result.Action,
                result.PendingContinuation,
                result.OptionPlay?.OptionCard,
                result.PhaseExecution?.Continuation,
                result.AttackExecution?.Continuation,
                result.RuleProcessing?.Continuation,
                ContinueToMainPhaseAfterResume: false,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                result.PendingDecisionPoint
                    is null
                    ? DecisionPoint.ForSelection(
                        result.PendingSelectionRequest.Player,
                        state.Phase,
                        "engine-action-pending-selection",
                        result.PendingSelectionRequest,
                        token)
                    : result.PendingDecisionPoint with { Token = token });
        }

        public static PendingEngineInteraction FromRuleResult(
            GameState state,
            RuleProcessorExecutionResult result,
            DecisionToken token)
        {
            if (result.PendingContinuation is null || result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending rule result requires an effect continuation, resolution, and selection request.");
            }

            return new PendingEngineInteraction(
                null,
                result.PendingContinuation,
                null,
                result.PhaseExecution?.Continuation,
                null,
                result.Continuation,
                ContinueToMainPhaseAfterResume: false,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-rule-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }

        public static PendingEngineInteraction FromPhaseResult(
            GameState state,
            PhaseExecutionResult result,
            DecisionToken token,
            bool continueToMainPhaseAfterResume = false)
        {
            if (result.PendingContinuation is null || result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending phase result requires an effect continuation, resolution, and selection request.");
            }

            return new PendingEngineInteraction(
                null,
                result.PendingContinuation,
                null,
                result.Continuation,
                null,
                null,
                continueToMainPhaseAfterResume,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-phase-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }

        public PendingEngineInteraction Next(
            GameState state,
            TriggerPipelineResult result,
            DecisionToken token)
        {
            if (result.PendingContinuation is null || result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending pipeline result requires an effect continuation, resolution, and selection request.");
            }

            return new PendingEngineInteraction(
                Action,
                result.PendingContinuation,
                OptionCard,
                PhaseContinuation,
                AttackContinuation,
                RuleContinuation,
                ContinueToMainPhaseAfterResume,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-resume-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }

        public PendingEngineInteraction NextAttack(
            GameState state,
            AttackExecutionResult result,
            DecisionToken token)
        {
            if (result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending attack tail result requires a resolution and selection request.");
            }

            if (result.PendingContinuation is null
                && result.Continuation is null)
            {
                throw new DomainException("Pending attack tail result requires a trigger or attack continuation.");
            }

            return new PendingEngineInteraction(
                Action,
                result.PendingContinuation,
                null,
                null,
                result.Continuation,
                null,
                ContinueToMainPhaseAfterResume,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-attack-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }

        public PendingEngineInteraction NextPhase(
            GameState state,
            PhaseExecutionResult result,
            DecisionToken token)
        {
            if (result.PendingContinuation is null || result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending phase tail result requires an effect continuation, resolution, and selection request.");
            }

            return new PendingEngineInteraction(
                Action,
                result.PendingContinuation,
                null,
                result.Continuation,
                null,
                null,
                ContinueToMainPhaseAfterResume,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-phase-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }

        public PendingEngineInteraction NextRule(
            GameState state,
            RuleProcessorExecutionResult result,
            DecisionToken token)
        {
            if (result.PendingContinuation is null || result.PendingResolution is null || result.PendingSelectionRequest is null)
            {
                throw new DomainException("Pending rule tail result requires an effect continuation, resolution, and selection request.");
            }

            return new PendingEngineInteraction(
                Action,
                result.PendingContinuation,
                null,
                result.PhaseExecution?.Continuation,
                null,
                result.Continuation,
                ContinueToMainPhaseAfterResume: false,
                result.PendingResolution,
                result.PendingSelectionRequest,
                token,
                DecisionPoint.ForSelection(
                    result.PendingSelectionRequest.Player,
                    state.Phase,
                    "engine-rule-pending-selection",
                    result.PendingSelectionRequest,
                    token));
        }
    }
}
