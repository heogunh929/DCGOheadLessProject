using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Mechanics;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

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
        _hatchService = hatchService ?? new HatchService();
        _moveFromBreedingService = moveFromBreedingService ?? new MoveFromBreedingService();
        _playCardService = playCardService ?? new PlayCardService();
        _digivolveService = digivolveService ?? new DigivolveService();
        _attackService = attackService ?? new AttackService();
        _complexMechanicService = complexMechanicService ?? new ComplexMechanicService();
        _phaseRunner = phaseRunner ?? new PhaseRunner();
        _ruleProcessor = ruleProcessor ?? new RuleProcessor(_phaseRunner);
    }

    public void Execute(GameState state, GameAction action, GameTrace? trace = null)
    {
        if (state.IsGameOver)
        {
            throw new DomainException("Cannot execute actions after game over.");
        }

        var before = trace is null ? null : state.Clone();

        switch (action)
        {
            case HatchAction hatch:
                _hatchService.Hatch(state, hatch.Actor);
                break;

            case MoveFromBreedingAction move:
                _moveFromBreedingService.Move(state, move.Actor, move.Permanent, move.TargetFrameIndex);
                break;

            case PlayCardAction play:
                _playCardService.Play(state, play, trace);
                break;

            case DigivolveAction digivolve:
                _digivolveService.Digivolve(state, digivolve, trace);
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
                _attackService.Attack(state, attack, trace);
                break;

            case PassAction pass:
                if (pass.Actor != state.TurnPlayerId)
                {
                    throw new DomainException($"Only turn player '{state.TurnPlayerId}' can pass.");
                }

                _phaseRunner.EndCurrentTurn(state, 3);
                break;

            default:
                throw new UnsupportedMechanicException($"Executing action '{action.GetType().Name}'");
        }

        trace?.AddAction($"action:{action.GetType().Name}", before!, state, action);
        _ruleProcessor.ProcessAfterAction(state);
    }
}
