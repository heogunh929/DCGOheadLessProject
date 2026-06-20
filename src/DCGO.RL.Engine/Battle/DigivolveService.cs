using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed class DigivolveService
{
    private readonly IZoneMover _zoneMover;
    private readonly DrawService _drawService;
    private readonly TriggerPipelineService? _triggerPipelineService;

    public DigivolveService(
        IZoneMover? zoneMover = null,
        DrawService? drawService = null,
        TriggerPipelineService? triggerPipelineService = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _drawService = drawService ?? new DrawService(_zoneMover);
        _triggerPipelineService = triggerPipelineService;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    public PermanentState Digivolve(GameState state, DigivolveAction action, GameTrace? trace = null)
    {
        var player = state.GetPlayer(action.Actor);
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can digivolve.");
        }

        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        var permanent = BattleRules.Permanent(state, action.TargetPermanent);
        if (permanent.ControllerPlayerId != action.Actor)
        {
            throw new DomainException($"Permanent '{action.TargetPermanent}' is not controlled by player '{action.Actor}'.");
        }

        if (!BattleRules.CanDigivolve(state, action.Card, permanent, out var cost))
        {
            throw new DomainException($"Card '{action.Card}' cannot digivolve onto permanent '{action.TargetPermanent}'.");
        }

        BattleRules.PayMemory(state, action.Actor, cost);
        _zoneMover.DigivolveCard(state, new DigivolveCardCommand(action.Card, Zone.Hand, action.TargetPermanent));
        _drawService.DrawCards(state, action.Actor, 1, trace);
        RunTriggerPipeline(state, action.Actor, action.Card, action.TargetPermanent, trace);
        return permanent;
    }

    private void RunTriggerPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId sourceCard,
        PermanentId sourcePermanent,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(
            state,
            EffectTiming.WhenDigivolving,
            player,
            sourceCard,
            sourcePermanent,
            new Dictionary<string, object?>
            {
                ["Card"] = sourceCard,
                ["Permanent"] = sourcePermanent,
            },
            trace: trace);
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"WhenDigivolving requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }
}
