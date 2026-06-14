using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Setup;

public sealed record SecuritySetupResult(
    PlayerId Player,
    IReadOnlyList<CardInstanceId> AddedCards,
    bool RequestedMoreThanAvailable);

public sealed class SecuritySetupService
{
    private readonly IZoneMover _zoneMover;

    public SecuritySetupService(IZoneMover? zoneMover = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
    }

    public SecuritySetupResult AddSecurityFromDeck(GameState state, PlayerId playerId, int count, GameTrace? trace = null)
    {
        if (count < 0)
        {
            throw new DomainException("Security setup count must not be negative.");
        }

        var player = state.GetPlayer(playerId);
        var addedCards = new List<CardInstanceId>();

        for (var i = 0; i < count; i++)
        {
            if (player.Deck.Count == 0)
            {
                return new SecuritySetupResult(playerId, addedCards, RequestedMoreThanAvailable: true);
            }

            var card = player.Deck[0];
            var command = new MoveCardCommand(card, Zone.Deck, Zone.Security, MoveReason.SecuritySetup, ToTop: true, FaceUp: false);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"security-setup:{playerId.Value}:{addedCards.Count}", before!, state, command, result);
            addedCards.Add(card);
        }

        return new SecuritySetupResult(playerId, addedCards, RequestedMoreThanAvailable: false);
    }
}
