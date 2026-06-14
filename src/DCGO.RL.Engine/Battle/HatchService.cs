using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Battle;

public sealed class HatchService
{
    private readonly IZoneMover _zoneMover;

    public HatchService(IZoneMover? zoneMover = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
    }

    public PermanentState Hatch(GameState state, PlayerId playerId)
    {
        var player = state.GetPlayer(playerId);
        if (playerId != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can hatch.");
        }

        if (player.DigiEggDeck.Count == 0)
        {
            throw new DomainException($"Player '{playerId}' has no digi-egg cards to hatch.");
        }

        if (player.BreedingAreaPermanent is not null)
        {
            throw new DomainException($"Player '{playerId}' already has a breeding area permanent.");
        }

        var card = player.DigiEggDeck[0];
        if (!BattleRules.HasKind(state, card, CardKind.DigiEgg))
        {
            throw new DomainException($"Card '{card}' is not a digi-egg card.");
        }

        var permanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(card, Zone.DigiEggDeck, Zone.BreedingArea, MoveReason.Hatch, DestinationPermanent: permanentId));

        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.EnterFieldTurnCount = -1;
        permanent.IsSuspended = false;
        return permanent;
    }
}
