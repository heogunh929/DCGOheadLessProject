using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Domain;

public sealed class GameContext
{
    private readonly GameState _state;

    public GameContext(GameState state)
    {
        _state = state ?? throw new ArgumentNullException(nameof(state));
    }

    public int Memory => _state.Memory;
    public Phase TurnPhase => _state.Phase;
    public Player TurnPlayer => new(_state, _state.TurnPlayerId);
    public Player NonTurnPlayer => new(_state, _state.NonTurnPlayerId);
    public Player FirstPlayer => new(_state, _state.FirstPlayerId);
    public IReadOnlyList<Player> Players => _state.Players.Select(player => new Player(_state, player.Id)).ToArray();
    public IReadOnlyList<CardSource> ActiveCardList => _state.ActiveCardIds.Select(card => new CardSource(_state, card)).ToArray();

    public Player PlayerFromID(int playerId) => new(_state, new PlayerId(playerId));
    public CardSource CardSourceFromId(CardInstanceId card) => new(_state, card);
    public Permanent? PermanentOfCard(CardInstanceId card) => CardSourceFromId(card).PermanentOfThisCard();
}

public sealed class Player
{
    private readonly GameState _state;
    private readonly PlayerState _player;

    internal Player(GameState state, PlayerId playerId)
    {
        _state = state;
        _player = state.GetPlayer(playerId);
    }

    public PlayerId Id => _player.Id;
    public int PlayerID => _player.Id.Value;
    public Player Enemy => new(_state, OpponentOf(_state, _player.Id));
    public IReadOnlyList<CardSource> DeckCards => Cards(_player.Deck);
    public IReadOnlyList<CardSource> DigiEggDeckCards => Cards(_player.DigiEggDeck);
    public IReadOnlyList<CardSource> HandCards => Cards(_player.Hand);
    public IReadOnlyList<CardSource> TrashCards => Cards(_player.Trash);
    public IReadOnlyList<CardSource> SecurityCards => Cards(_player.Security);
    public IReadOnlyList<CardSource> ExecutingCards => Cards(_player.Executing);
    public IReadOnlyList<Permanent> FieldPermanents => _player.FieldPermanents.Select(permanent => new Permanent(_state, permanent.Id)).ToArray();

    public IReadOnlyList<Permanent> GetFieldPermanents() => FieldPermanents;
    public IReadOnlyList<Permanent> GetBattleAreaPermanents() =>
        _player.BattleAreaPermanents.Select(permanent => new Permanent(_state, permanent.Id)).ToArray();
    public IReadOnlyList<Permanent> GetBreedingAreaPermanents() =>
        _player.FieldPermanents
            .Where(permanent => permanent.IsBreedingArea)
            .Select(permanent => new Permanent(_state, permanent.Id))
            .ToArray();
    public IReadOnlyList<Permanent> GetBattleAreaDigimons() =>
        GetBattleAreaPermanents()
            .Where(permanent => permanent.IsDigimon)
            .ToArray();

    private IReadOnlyList<CardSource> Cards(IEnumerable<CardInstanceId> cards) =>
        cards.Select(card => new CardSource(_state, card)).ToArray();

    private static PlayerId OpponentOf(GameState state, PlayerId player) =>
        state.Players
            .Select(candidate => candidate.Id)
            .FirstOrDefault(candidate => candidate != player, new PlayerId(-1)) is { Value: >= 0 } opponent
            ? opponent
            : throw new DomainException($"Player '{player}' has no opponent.");
}

public sealed class CardSource
{
    private readonly GameState _state;
    private readonly CardInstance _card;

    internal CardSource(GameState state, CardInstanceId cardId)
    {
        _state = state;
        _card = state.Cards.TryGetValue(cardId, out var card)
            ? card
            : throw new DomainException($"Card '{cardId}' does not exist.");
    }

    public CardInstanceId Id => _card.Id;
    public string DefinitionId => _card.DefinitionId;
    public Player Owner => new(_state, _card.Owner);
    public Zone CurrentZone => _card.CurrentZone;
    public bool IsFaceUp => _card.IsFaceUp;
    public bool IsFlipped => _card.IsFaceUp;
    public CEntity_Base Entity => new(Definition);
    public CardDefinition Definition => _state.CardDefinitions.TryGetValue(_card.DefinitionId, out var definition)
        ? definition
        : throw new DomainException($"Card definition '{_card.DefinitionId}' does not exist.");
    internal GameState State => _state;

    public bool IsDigimon => Definition.CardKinds.Contains(CardKind.Digimon);
    public bool IsTamer => Definition.CardKinds.Contains(CardKind.Tamer);
    public bool IsOption => Definition.CardKinds.Contains(CardKind.Option);
    public int Level => Definition.Level;
    public IReadOnlyList<CardColor> CardColors => Definition.CardColors.Distinct().ToArray();
    public IReadOnlyList<string> CardNames => Names(Definition);
    public IReadOnlyList<string> CardTraits => Definition.CardTraits;

    public bool CanNotBeAffected(ICardEffect cardEffect) =>
        throw new UnsupportedMechanicException(
            "CardSource.CanNotBeAffected requires collected ICanNotAffectedEffect providers in the headless runtime.");

    public bool CanNotBeAffected(ICardEffect? cardEffect, IEnumerable<ICardEffect> candidateEffects)
    {
        if (cardEffect is null)
        {
            return false;
        }

        ArgumentNullException.ThrowIfNull(candidateEffects);

        return candidateEffects.Any(candidate =>
            candidate is ICanNotAffectedEffect immunityEffect
            && immunityEffect.CanNotAffect(this, cardEffect));
    }

    public Permanent? PermanentOfThisCard()
    {
        foreach (var player in _state.Players)
        {
            foreach (var permanent in player.FieldPermanents)
            {
                if (permanent.TopCardId == _card.Id
                    || permanent.SourceCardIds.Contains(_card.Id)
                    || permanent.LinkedCards.Contains(_card.Id))
                {
                    return new Permanent(_state, permanent.Id);
                }
            }
        }

        return null;
    }

    private static IReadOnlyList<string> Names(CardDefinition definition) =>
        new[] { definition.CardId, definition.CardNameEnglish, definition.CardNameJapanese }
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
}

public sealed class Permanent
{
    private readonly GameState _state;
    private readonly PermanentState _permanent;

    internal Permanent(GameState state, PermanentId permanentId)
    {
        _state = state;
        _permanent = state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId)
            ?? throw new DomainException($"Permanent '{permanentId}' does not exist.");
    }

    public PermanentId Id => _permanent.Id;
    public Player Owner => new(_state, _permanent.OwnerPlayerId);
    public Player Controller => new(_state, _permanent.ControllerPlayerId);
    public CardSource TopCard => new(_state, _permanent.TopCardId);
    public IReadOnlyList<CardSource> DigivolutionCards => _permanent.SourceCardIds.Select(card => new CardSource(_state, card)).ToArray();
    public IReadOnlyList<CardSource> LinkedCards => _permanent.LinkedCards.Select(card => new CardSource(_state, card)).ToArray();
    public bool IsBreedingArea => _permanent.IsBreedingArea;
    public bool IsSuspended => _permanent.IsSuspended;
    public bool HasNoDigivolutionCards => _permanent.SourceCardIds.Count == 0;
    public int DigivolutionCardCount => _permanent.SourceCardIds.Count;
    public int LinkedCardCount => _permanent.LinkedCards.Count;
    public bool IsDigimon => TopCard.IsDigimon;
    public bool IsTamer => TopCard.IsTamer;
    public bool IsOption => TopCard.IsOption;
}

public sealed class CEntity_Base
{
    internal CEntity_Base(CardDefinition definition)
    {
        Definition = definition;
    }

    public CardDefinition Definition { get; }
    public string CardID => Definition.CardId;
    public string CardEffectClassName => Definition.CardEffectClassName;
    public int Level => Definition.Level;
    public int DP => Definition.DP;
    public IReadOnlyList<CardKind> CardKind => Definition.CardKinds;
    public IReadOnlyList<CardColor> CardColor => Definition.CardColors;
    public IReadOnlyList<string> CardNames =>
        new[] { Definition.CardId, Definition.CardNameEnglish, Definition.CardNameJapanese }
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
    public IReadOnlyList<string> CardTraits => Definition.CardTraits;
}
