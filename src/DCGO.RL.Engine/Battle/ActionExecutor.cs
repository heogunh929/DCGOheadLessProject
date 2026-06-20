using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Mechanics;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record ActionExecutionResult(
    GameAction Action,
    PermanentState? PlayedPermanent = null,
    OptionPlayResult? OptionPlay = null,
    PhaseExecutionResult? PhaseExecution = null,
    AttackExecutionResult? AttackExecution = null,
    RuleProcessorExecutionResult? RuleProcessing = null,
    SelectionRequest? PendingSelectionRequest = null,
    EffectResolution? PendingResolution = null,
    TriggerPipelineContinuation? PendingContinuation = null,
    DecisionPoint? PendingDecisionPoint = null,
    bool RulesProcessed = false)
{
    public bool HasPendingSelection => PendingSelectionRequest is not null;

    public static ActionExecutionResult Completed(GameAction action, PermanentState? playedPermanent = null) =>
        new(action, playedPermanent);

    public static ActionExecutionResult FromPlay(GameState state, PlayCardAction action, PlayCardResult result)
    {
        var pendingRequest = result.PendingSelectionRequest;
        return new ActionExecutionResult(
            Action: action,
            PlayedPermanent: result.Permanent,
            OptionPlay: result.OptionPlay,
            PhaseExecution: null,
            AttackExecution: null,
            RuleProcessing: null,
            PendingSelectionRequest: pendingRequest,
            PendingResolution: result.PendingResolution,
            PendingContinuation: result.PendingContinuation,
            PendingDecisionPoint: pendingRequest is null
                ? null
                : DecisionPoint.ForSelection(
                    pendingRequest.Player,
                    state.Phase,
                    "action-pending-selection",
                    pendingRequest));
    }

    public static ActionExecutionResult FromDigivolve(GameState state, DigivolveAction action, DigivolveResult result)
    {
        var pendingRequest = result.PendingSelectionRequest;
        return new ActionExecutionResult(
            Action: action,
            PlayedPermanent: result.Permanent,
            OptionPlay: null,
            PhaseExecution: null,
            AttackExecution: null,
            RuleProcessing: null,
            PendingSelectionRequest: pendingRequest,
            PendingResolution: result.PendingResolution,
            PendingContinuation: result.PendingContinuation,
            PendingDecisionPoint: pendingRequest is null
                ? null
                : DecisionPoint.ForSelection(
                    pendingRequest.Player,
                    state.Phase,
                    "action-pending-selection",
                    pendingRequest));
    }

    public static ActionExecutionResult FromPhase(GameState state, GameAction action, PhaseExecutionResult result)
    {
        var pendingRequest = result.PendingSelectionRequest;
        return new ActionExecutionResult(
            Action: action,
            PlayedPermanent: null,
            OptionPlay: null,
            PhaseExecution: result,
            AttackExecution: null,
            RuleProcessing: null,
            PendingSelectionRequest: pendingRequest,
            PendingResolution: result.PendingResolution,
            PendingContinuation: result.PendingContinuation,
            PendingDecisionPoint: pendingRequest is null
                ? null
                : DecisionPoint.ForSelection(
                    pendingRequest.Player,
                    state.Phase,
                    "action-phase-pending-selection",
                    pendingRequest));
    }

    public static ActionExecutionResult FromAttack(GameState state, AttackAction action, AttackExecutionResult result)
    {
        var pendingRequest = result.PendingSelectionRequest;
        return new ActionExecutionResult(
            Action: action,
            PlayedPermanent: null,
            OptionPlay: null,
            PhaseExecution: null,
            AttackExecution: result,
            RuleProcessing: null,
            PendingSelectionRequest: pendingRequest,
            PendingResolution: result.PendingResolution,
            PendingContinuation: result.PendingContinuation,
            PendingDecisionPoint: pendingRequest is null
                ? null
                : DecisionPoint.ForSelection(
                    pendingRequest.Player,
                    state.Phase,
                    "action-attack-pending-selection",
                    pendingRequest));
    }

    public ActionExecutionResult WithRuleProcessing(GameState state, RuleProcessorExecutionResult result)
    {
        var pendingRequest = result.PendingSelectionRequest;
        return this with
        {
            PhaseExecution = result.PhaseExecution ?? PhaseExecution,
            RuleProcessing = result,
            PendingSelectionRequest = pendingRequest,
            PendingResolution = result.PendingResolution,
            PendingContinuation = result.PendingContinuation,
            PendingDecisionPoint = pendingRequest is null
                ? null
                : DecisionPoint.ForSelection(
                    pendingRequest.Player,
                    state.Phase,
                    "action-rules-pending-selection",
                    pendingRequest),
        };
    }

    public ActionExecutionResult MarkRulesProcessed() => this with { RulesProcessed = true };
}

public sealed class ActionExecutor
{
    private readonly HatchService _hatchService;
    private readonly MoveFromBreedingService _moveFromBreedingService;
    private readonly PlayCardService _playCardService;
    private readonly DigivolveService _digivolveService;
    private readonly AttackService _attackService;
    private readonly ComplexMechanicService _complexMechanicService;
    private readonly PhaseRunner _phaseRunner;
    private readonly RuleProcessor _ruleProcessor;

    public ActionExecutor(
        HatchService? hatchService = null,
        MoveFromBreedingService? moveFromBreedingService = null,
        PlayCardService? playCardService = null,
        DigivolveService? digivolveService = null,
        AttackService? attackService = null,
        ComplexMechanicService? complexMechanicService = null,
        PhaseRunner? phaseRunner = null,
        RuleProcessor? ruleProcessor = null)
    {
        if (HasMissingDependency(
            hatchService,
            moveFromBreedingService,
            playCardService,
            digivolveService,
            attackService,
            complexMechanicService,
            phaseRunner,
            ruleProcessor))
        {
            throw new DomainException(
                "ActionExecutor requires a complete service graph from BattleEngineServices. Partial dependency injection is not allowed.");
        }

        _hatchService = hatchService!;
        _moveFromBreedingService = moveFromBreedingService!;
        _playCardService = playCardService!;
        _digivolveService = digivolveService!;
        _attackService = attackService!;
        _complexMechanicService = complexMechanicService!;
        _phaseRunner = phaseRunner!;
        _ruleProcessor = ruleProcessor!;
    }

    internal PlayCardService RuntimePlayCardService => _playCardService;

    internal DigivolveService RuntimeDigivolveService => _digivolveService;

    internal AttackService RuntimeAttackService => _attackService;

    internal PhaseRunner RuntimePhaseRunner => _phaseRunner;

    internal RuleProcessor RuntimeRuleProcessor => _ruleProcessor;

    public ActionExecutionResult Execute(GameState state, GameAction action, GameTrace? trace = null)
    {
        if (state.IsGameOver)
        {
            throw new DomainException("Cannot execute actions after game over.");
        }

        var before = trace is null ? null : state.Clone();
        var result = ActionExecutionResult.Completed(action);

        switch (action)
        {
            case HatchAction hatch:
                _hatchService.Hatch(state, hatch.Actor);
                break;

            case MoveFromBreedingAction move:
                _moveFromBreedingService.Move(state, move.Actor, move.Permanent, move.TargetFrameIndex);
                break;

            case PlayCardAction play:
                result = ActionExecutionResult.FromPlay(
                    state,
                    play,
                    _playCardService.PlayWithResult(state, play, trace));
                break;

            case DigivolveAction digivolve:
                result = ActionExecutionResult.FromDigivolve(
                    state,
                    digivolve,
                    _digivolveService.DigivolveWithResult(state, digivolve, trace));
                break;

            case JogressAction jogress:
                _complexMechanicService.ExecuteJogress(state, jogress, trace);
                break;

            case BurstDigivolveAction burst:
                _complexMechanicService.ExecuteBurstDigivolve(state, burst, trace);
                break;

            case AppFusionAction appFusion:
                _complexMechanicService.ExecuteAppFusion(state, appFusion, trace);
                break;

            case DigiXrosPlayAction digiXros:
                _complexMechanicService.ExecuteDigiXrosPlay(state, digiXros);
                break;

            case AssemblyPlayAction assembly:
                _complexMechanicService.ExecuteAssemblyPlay(state, assembly);
                break;

            case LinkAction link:
                _complexMechanicService.ExecuteLink(state, link);
                break;

            case DelayOptionPlayAction delay:
                _complexMechanicService.ExecuteDelayOptionPlay(state, delay);
                break;

            case AttackAction attack:
                result = ActionExecutionResult.FromAttack(
                    state,
                    attack,
                    _attackService.AttackWithResult(state, attack, trace));
                break;

            case PassAction pass:
                if (pass.Actor != state.TurnPlayerId)
                {
                    throw new DomainException($"Only turn player '{state.TurnPlayerId}' can pass.");
                }

                result = ActionExecutionResult.FromPhase(
                    state,
                    pass,
                    _phaseRunner.EndCurrentTurnWithResult(state, 3, trace));
                break;

            default:
                throw new UnsupportedMechanicException($"Executing action '{action.GetType().Name}'");
        }

        trace?.AddAction($"action:{action.GetType().Name}", before!, state, action);
        if (result.HasPendingSelection)
        {
            return result;
        }

        var rulesResult = _ruleProcessor.ProcessAfterActionWithResult(state, trace);
        if (rulesResult.HasPendingSelection)
        {
            return result.WithRuleProcessing(state, rulesResult);
        }

        return result.MarkRulesProcessed();
    }

    private static bool HasMissingDependency(
        HatchService? hatchService,
        MoveFromBreedingService? moveFromBreedingService,
        PlayCardService? playCardService,
        DigivolveService? digivolveService,
        AttackService? attackService,
        ComplexMechanicService? complexMechanicService,
        PhaseRunner? phaseRunner,
        RuleProcessor? ruleProcessor) =>
        hatchService is null
        || moveFromBreedingService is null
        || playCardService is null
        || digivolveService is null
        || attackService is null
        || complexMechanicService is null
        || phaseRunner is null
        || ruleProcessor is null;
}
