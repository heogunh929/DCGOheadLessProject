using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Infrastructure;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Setup;

public sealed class DeckInstantiationService
{
    public DeckValidationReport ValidateDecks(GameConfig config, IEnumerable<PlayerDeckList> playerDecks, ICardDatabase cardDatabase)
    {
        var errors = new List<string>();
        var decks = playerDecks.ToArray();
        var requiredSetupCards = config.InitialHandSize + config.InitialSecurityCount;

        foreach (var duplicate in decks.GroupBy(deck => deck.Player).Where(group => group.Count() > 1))
        {
            errors.Add($"Player '{duplicate.Key}' has multiple decklists.");
        }

        foreach (var player in new[] { PlayerId.Player0, PlayerId.Player1 })
        {
            if (!decks.Any(deck => deck.Player == player))
            {
                errors.Add($"Player '{player}' decklist is missing.");
            }
        }

        foreach (var deck in decks)
        {
            if (deck.MainDeck.Count < requiredSetupCards)
            {
                errors.Add($"Player '{deck.Player}' main deck has {deck.MainDeck.Count} cards, requires at least {requiredSetupCards} for setup.");
            }

            ValidateCards(deck.Player, Zone.Deck, deck.MainDeck, cardDatabase, errors);
            ValidateCards(deck.Player, Zone.DigiEggDeck, deck.DigiEggDeck, cardDatabase, errors);
        }

        return errors.Count == 0 ? DeckValidationReport.Valid : DeckValidationReport.Invalid(errors);
    }

    public DeckValidationReport InstantiateDecks(
        GameState state,
        IEnumerable<PlayerDeckList> playerDecks,
        ICardDatabase cardDatabase,
        CardInstanceFactory? cardInstanceFactory = null)
    {
        var decks = playerDecks.ToArray();
        var report = ValidateDecks(state.Config, decks, cardDatabase);
        if (!report.IsValid)
        {
            throw new DomainException($"Deck validation failed: {string.Join(" | ", report.Errors)}");
        }

        var factory = cardInstanceFactory ?? new CardInstanceFactory();

        foreach (var definition in cardDatabase.All)
        {
            state.CardDefinitions.TryAdd(definition.CardId, definition);
        }

        foreach (var deck in decks.OrderBy(deck => deck.Player.Value))
        {
            InstantiateCards(state, factory, cardDatabase, deck.Player, Zone.Deck, deck.MainDeck);
            InstantiateCards(state, factory, cardDatabase, deck.Player, Zone.DigiEggDeck, deck.DigiEggDeck);
        }

        return report;
    }

    public void ShuffleDecks(GameState state, IDeterministicRng rng)
    {
        foreach (var player in state.Players.OrderBy(player => player.Id.Value))
        {
            ShuffleInPlace(player.Deck, rng);
            ShuffleInPlace(player.DigiEggDeck, rng);
        }
    }

    private static void ValidateCards(
        PlayerId player,
        Zone zone,
        IEnumerable<string> cardIds,
        ICardDatabase cardDatabase,
        List<string> errors)
    {
        foreach (var cardId in cardIds)
        {
            if (string.IsNullOrWhiteSpace(cardId))
            {
                errors.Add($"Player '{player}' {zone} contains an empty card id.");
                continue;
            }

            if (!cardDatabase.TryGetByCardId(cardId, out var definition))
            {
                errors.Add($"Player '{player}' {zone} contains unknown card id '{cardId}'.");
                continue;
            }

            var isDigiEgg = definition.CardKinds.Contains(CardKind.DigiEgg);
            if (zone == Zone.Deck && isDigiEgg)
            {
                errors.Add($"Player '{player}' main deck contains digi-egg card '{cardId}'.");
            }
            else if (zone == Zone.DigiEggDeck && !isDigiEgg)
            {
                errors.Add($"Player '{player}' digi-egg deck contains non-digi-egg card '{cardId}'.");
            }
        }
    }

    private static void InstantiateCards(
        GameState state,
        CardInstanceFactory factory,
        ICardDatabase cardDatabase,
        PlayerId playerId,
        Zone zone,
        IEnumerable<string> cardIds)
    {
        var player = state.GetPlayer(playerId);
        var zoneCards = player.CardsIn(zone);

        foreach (var cardId in cardIds)
        {
            var instance = factory.Create(cardId, playerId, cardDatabase, zone, isFaceUp: zone != Zone.Security);
            state.Cards.Add(instance.Id, instance);
            zoneCards.Add(instance.Id);
        }
    }

    private static void ShuffleInPlace(List<CardInstanceId> cards, IDeterministicRng rng)
    {
        for (var i = cards.Count - 1; i > 0; i--)
        {
            var swapIndex = rng.NextInt(i + 1);
            (cards[i], cards[swapIndex]) = (cards[swapIndex], cards[i]);
        }
    }
}
