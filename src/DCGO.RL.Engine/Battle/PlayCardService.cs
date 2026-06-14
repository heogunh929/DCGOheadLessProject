using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Battle;

public sealed class PlayCardService
{
    private readonly IZoneMover _zoneMover;
    private readonly TriggerPipelineService? _triggerPipelineService;

    public PlayCardService(
        IZoneMover? zoneMover = null,
        TriggerPipelineService? triggerPipelineService = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _triggerPipelineService = triggerPipelineService;
    }

    public PermanentState? Play(GameState state, PlayCardAction action)
    {
        var player = state.GetPlayer(action.Actor);
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can play cards.");
        }

        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        var definition = BattleRules.Definition(state, action.Card);
        var cost = Math.Max(0, definition.PlayCost);
        BattleRules.PayMemory(state, action.Actor, cost);

        if (definition.CardKinds.Contains(CardKind.Option))
        {
            _zoneMover.MoveCard(state, new MoveCardCommand(action.Card, Zone.Hand, Zone.Trash, MoveReason.Play));
            RunTriggerPipeline(
                state,
                EffectTiming.OptionSkill,
                action.Actor,
                action.Card,
                sourcePermanent: null,
                new Dictionary<string, object?>
                {
                    ["Card"] = action.Card,
                    ["PayCost"] = true,
                    ["SourceZone"] = Zone.Trash,
                });
            return null;
        }

        if (!definition.CardKinds.Contains(CardKind.Digimon) && !definition.CardKinds.Contains(CardKind.Tamer))
        {
            throw new UnsupportedMechanicException($"Playing card kind '{string.Join(",", definition.CardKinds)}'");
        }

        if (!BattleRules.IsEmptyBattleFrame(player, action.TargetFrameIndex))
        {
            throw new DomainException($"Battle frame '{action.TargetFrameIndex}' is not empty.");
        }

        var permanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                action.Card,
                Zone.Hand,
                Zone.BattleArea,
                MoveReason.Play,
                DestinationPermanent: permanentId,
                DestinationFrameIndex: action.TargetFrameIndex));

        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.EnterFieldTurnCount = state.TurnCount;
        RunTriggerPipeline(
            state,
            EffectTiming.OnPlay,
            action.Actor,
            action.Card,
            permanent.Id,
            new Dictionary<string, object?>
            {
                ["Card"] = action.Card,
                ["Permanent"] = permanent.Id,
                ["Played"] = true,
            });
        return permanent;
    }

    private void RunTriggerPipeline(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        CardInstanceId sourceCard,
        PermanentId? sourcePermanent,
        IReadOnlyDictionary<string, object?> values)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(
            state,
            timing,
            player,
            sourceCard,
            sourcePermanent,
            values);
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"Trigger timing '{timing}' requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }
}
