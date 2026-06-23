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

    public IReadOnlyList<ICardEffect> EffectList(EffectTiming timing) =>
        throw new UnsupportedMechanicException(
            "Player.EffectList requires duration-scoped granted effect registry support in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        TemporaryGrantedEffectRegistry temporaryGrantedEffectRegistry)
    {
        ArgumentNullException.ThrowIfNull(temporaryGrantedEffectRegistry);

        var effects = new List<ICardEffect>();
        foreach (var grantedEffect in _state.TemporaryGrantedEffects
            .Where(effect => effect.Timing == timing
                && effect.TargetPlayerId is { } targetPlayer
                && targetPlayer == _player.Id)
            .OrderBy(effect => effect.StableId, StringComparer.Ordinal))
        {
            var sourceCard = SourceCardFacade(grantedEffect.SourceCardId);
            var sourcePermanent = SourcePermanentFacade(grantedEffect.SourcePermanentId);
            var descriptorContext = new TemporaryGrantedEffectDescriptorContext(
                _state,
                grantedEffect,
                sourceCard?.Id,
                sourcePermanent?.Id,
                grantedEffect.ControllerPlayerId,
                SourceSnapshot: null);

            if (!temporaryGrantedEffectRegistry.TryCreateDescriptor(
                grantedEffect,
                descriptorContext,
                out var descriptor))
            {
                throw TemporaryGrantedEffectRegistry.Unsupported(grantedEffect);
            }

            descriptor = descriptor with
            {
                SourceCard = descriptor.SourceCard ?? sourceCard?.Id,
                SourcePermanent = descriptor.SourcePermanent ?? sourcePermanent?.Id,
                Controller = descriptor.Controller ?? grantedEffect.ControllerPlayerId,
                TemporaryGrantedEffect = grantedEffect,
            };

            effects.Add(new DescriptorBackedCardEffect(
                descriptor,
                sourceCard,
                sourcePermanent,
                grantedEffect.DebugLabel));
        }

        return effects.ToArray();
    }

    private IReadOnlyList<CardSource> Cards(IEnumerable<CardInstanceId> cards) =>
        cards.Select(card => new CardSource(_state, card)).ToArray();

    private CardSource? SourceCardFacade(CardInstanceId? sourceCardId)
    {
        if (sourceCardId is null)
        {
            return null;
        }

        if (!_state.Cards.ContainsKey(sourceCardId.Value))
        {
            throw new DomainException($"Player effect source card '{sourceCardId}' does not exist.");
        }

        return new CardSource(_state, sourceCardId.Value);
    }

    private Permanent? SourcePermanentFacade(PermanentId? sourcePermanentId)
    {
        if (sourcePermanentId is null)
        {
            return null;
        }

        if (_state.Players
            .SelectMany(player => player.FieldPermanents)
            .All(permanent => permanent.Id != sourcePermanentId.Value))
        {
            throw new DomainException($"Player effect source permanent '{sourcePermanentId}' does not exist.");
        }

        return new Permanent(_state, sourcePermanentId.Value);
    }

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
    public bool IsFlipped => !_card.IsFaceUp;
    public CEntity_Base Entity => new(Definition);
    public CardDefinition Definition => _state.CardDefinitions.TryGetValue(_card.DefinitionId, out var definition)
        ? definition
        : throw new DomainException($"Card definition '{_card.DefinitionId}' does not exist.");
    internal GameState State => _state;

    public bool IsDigimon => Definition.CardKinds.Contains(CardKind.Digimon);
    public bool IsTamer => Definition.CardKinds.Contains(CardKind.Tamer);
    public bool IsOption => Definition.CardKinds.Contains(CardKind.Option);
    public bool IsLinked => CurrentZone == Zone.LinkedCards
        && PermanentOfThisCard()?.LinkedCards.Any(card => card.Id == Id) == true;
    public bool IsDigivolutionCard => CurrentZone == Zone.EvolutionSources
        && PermanentOfThisCard()?.DigivolutionCards.Any(card => card.Id == Id) == true;
    public int Level => Definition.Level;
    public IReadOnlyList<CardColor> CardColors => Definition.CardColors.Distinct().ToArray();
    public IReadOnlyList<string> CardNames => Names(Definition);
    public IReadOnlyList<string> CardTraits => Definition.CardTraits;

    public IReadOnlyList<ICardEffect> EffectList(EffectTiming timing) =>
        throw new UnsupportedMechanicException(
            "CardSource.EffectList requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        ICEntityEffectRegistry effectRegistry) =>
        EffectList_ForCard(timing, this, effectRegistry);

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories) =>
        EffectList(timing, new CEntityEffectRegistry(effectFactories));

    public IReadOnlyList<ICardEffect> EffectList_ForCard(EffectTiming timing, CardSource cardSource) =>
        throw new UnsupportedMechanicException(
            "CardSource.EffectList_ForCard requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList_ForCard(
        EffectTiming timing,
        CardSource cardSource,
        ICEntityEffectRegistry effectRegistry)
    {
        ArgumentNullException.ThrowIfNull(cardSource);
        ArgumentNullException.ThrowIfNull(effectRegistry);

        var effects = CreateEffectController(this, effectRegistry)
            .GetCardEffects(timing, cardSource)
            .Where(cardEffect => cardEffect is not null)
            .ToArray();

        foreach (var cardEffect in effects)
        {
            if (cardEffect.EffectSourceCard is null)
            {
                cardEffect.SetEffectSourceCard(this);
            }
        }

        return effects;
    }

    public IReadOnlyList<ICardEffect> EffectList_ForCard(
        EffectTiming timing,
        CardSource cardSource,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories)
    {
        ArgumentNullException.ThrowIfNull(effectFactories);
        return EffectList_ForCard(timing, cardSource, new CEntityEffectRegistry(effectFactories));
    }

    public IReadOnlyList<ICardEffect> EffectList_ExceptAddedEffects(EffectTiming timing) =>
        throw new UnsupportedMechanicException(
            "CardSource.EffectList_ExceptAddedEffects requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList_ExceptAddedEffects(
        EffectTiming timing,
        ICEntityEffectRegistry effectRegistry) =>
        EffectList_ForCard_ExceptAddedEffects(timing, this, effectRegistry);

    public IReadOnlyList<ICardEffect> EffectList_ExceptAddedEffects(
        EffectTiming timing,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories) =>
        EffectList_ExceptAddedEffects(timing, new CEntityEffectRegistry(effectFactories));

    public IReadOnlyList<ICardEffect> EffectList_ForCard_ExceptAddedEffects(EffectTiming timing, CardSource cardSource) =>
        throw new UnsupportedMechanicException(
            "CardSource.EffectList_ForCard_ExceptAddedEffects requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList_ForCard_ExceptAddedEffects(
        EffectTiming timing,
        CardSource cardSource,
        ICEntityEffectRegistry effectRegistry)
    {
        ArgumentNullException.ThrowIfNull(cardSource);
        ArgumentNullException.ThrowIfNull(effectRegistry);

        var effects = CreateEffectController(this, effectRegistry)
            .GetCardEffects_ExceptAddedEffects(timing, cardSource)
            .Where(cardEffect => cardEffect is not null)
            .ToArray();

        foreach (var cardEffect in effects)
        {
            if (cardEffect.EffectSourceCard is null)
            {
                cardEffect.SetEffectSourceCard(this);
            }
        }

        return effects;
    }

    public IReadOnlyList<ICardEffect> EffectList_ForCard_ExceptAddedEffects(
        EffectTiming timing,
        CardSource cardSource,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories)
    {
        ArgumentNullException.ThrowIfNull(effectFactories);
        return EffectList_ForCard_ExceptAddedEffects(timing, cardSource, new CEntityEffectRegistry(effectFactories));
    }

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

    private static CEntity_EffectController CreateEffectController(
        CardSource source,
        ICEntityEffectRegistry effectRegistry)
    {
        var controller = new CEntity_EffectController(effectRegistry: effectRegistry);
        controller.AddCardEffect(source.Entity.CardID, source.Entity.CardEffectClassName);
        return controller;
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

    public IReadOnlyList<ICardEffect> EffectList(EffectTiming timing) =>
        throw new UnsupportedMechanicException(
            "Permanent.EffectList requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        ICEntityEffectRegistry effectRegistry) =>
        EffectList_ForCard(timing, TopCard, effectRegistry, TemporaryGrantedEffectRegistry.Empty);

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        ICEntityEffectRegistry effectRegistry,
        TemporaryGrantedEffectRegistry temporaryGrantedEffectRegistry) =>
        EffectList_ForCard(timing, TopCard, effectRegistry, temporaryGrantedEffectRegistry);

    public IReadOnlyList<ICardEffect> EffectList(
        EffectTiming timing,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories) =>
        EffectList(timing, new CEntityEffectRegistry(effectFactories));

    public IReadOnlyList<ICardEffect> EffectList_ForCard(EffectTiming timing, CardSource cardSource) =>
        throw new UnsupportedMechanicException(
            "Permanent.EffectList_ForCard requires an explicit CEntity_Effect factory registry in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList_ForCard(
        EffectTiming timing,
        CardSource cardSource,
        ICEntityEffectRegistry effectRegistry) =>
        EffectList_ForCard(timing, cardSource, effectRegistry, TemporaryGrantedEffectRegistry.Empty);

    public IReadOnlyList<ICardEffect> EffectList_ForCard(
        EffectTiming timing,
        CardSource cardSource,
        ICEntityEffectRegistry effectRegistry,
        TemporaryGrantedEffectRegistry temporaryGrantedEffectRegistry)
    {
        ArgumentNullException.ThrowIfNull(cardSource);
        ArgumentNullException.ThrowIfNull(effectRegistry);
        ArgumentNullException.ThrowIfNull(temporaryGrantedEffectRegistry);

        var effects = new List<ICardEffect>();
        foreach (var source in EffectSourceCards())
        {
            if (source.IsFlipped)
            {
                continue;
            }

            var isTopCard = source.Id == TopCard.Id;
            if (!isTopCard && !IsDigimon)
            {
                continue;
            }

            foreach (var cardEffect in CreateEffectController(source, effectRegistry)
                .GetCardEffects(timing, source))
            {
                if (cardEffect.IsInheritedEffect && !isTopCard)
                {
                    AddPermanentEffect(effects, cardEffect);
                    continue;
                }

                if (cardEffect.IsLinkedEffect && source.IsLinked)
                {
                    AddPermanentEffect(effects, cardEffect);
                    continue;
                }

                if (isTopCard && !cardEffect.IsInheritedEffect && !cardEffect.IsLinkedEffect)
                {
                    AddPermanentEffect(effects, cardEffect);
                }
            }
        }

        foreach (var cardEffect in EffectList_Added(timing, temporaryGrantedEffectRegistry))
        {
            effects.Add(cardEffect);
        }

        foreach (var cardEffect in effects)
        {
            if (cardEffect.EffectSourceCard is null)
            {
                cardEffect.SetEffectSourceCard(cardSource);
            }
        }

        return effects.ToArray();
    }

    public IReadOnlyList<ICardEffect> EffectList_ForCard(
        EffectTiming timing,
        CardSource cardSource,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> effectFactories)
    {
        ArgumentNullException.ThrowIfNull(effectFactories);
        return EffectList_ForCard(timing, cardSource, new CEntityEffectRegistry(effectFactories));
    }

    public IReadOnlyList<ICardEffect> EffectList_Added(EffectTiming timing) =>
        throw new UnsupportedMechanicException(
            "Permanent.EffectList_Added requires duration-scoped granted effect registry support in the headless runtime.");

    public IReadOnlyList<ICardEffect> EffectList_Added(
        EffectTiming timing,
        TemporaryGrantedEffectRegistry temporaryGrantedEffectRegistry)
    {
        ArgumentNullException.ThrowIfNull(temporaryGrantedEffectRegistry);

        var topCard = TopCard;
        var controller = Controller.Id;
        var sourceSnapshot = new TriggerSourceSnapshot(
            TriggerSourceRole.FieldTop,
            topCard.CurrentZone,
            topCard.Id,
            Id,
            topCard.Id,
            topCard.Owner.Id,
            controller);
        var effects = new List<ICardEffect>();

        foreach (var grantedEffect in _state.TemporaryGrantedEffects
            .Where(effect => effect.Timing == timing
                && effect.TargetPermanentId is { } targetPermanent
                && targetPermanent == Id)
            .OrderBy(effect => effect.StableId, StringComparer.Ordinal))
        {
            var descriptorContext = new TemporaryGrantedEffectDescriptorContext(
                _state,
                grantedEffect,
                topCard.Id,
                Id,
                controller,
                sourceSnapshot);

            if (!descriptorContext.AppliesToPermanent(_permanent))
            {
                continue;
            }

            if (!temporaryGrantedEffectRegistry.TryCreateDescriptor(
                grantedEffect,
                descriptorContext,
                out var descriptor))
            {
                throw TemporaryGrantedEffectRegistry.Unsupported(grantedEffect);
            }

            descriptor = descriptor with
            {
                SourceCard = descriptor.SourceCard ?? topCard.Id,
                SourcePermanent = descriptor.SourcePermanent ?? Id,
                Controller = descriptor.Controller ?? controller,
                SourceSnapshot = descriptor.SourceSnapshot ?? sourceSnapshot,
                TemporaryGrantedEffect = grantedEffect,
            };

            var effect = new DescriptorBackedCardEffect(
                descriptor,
                topCard,
                this,
                grantedEffect.DebugLabel);
            effect.SetIsInheritedEffect(false);
            effects.Add(effect);
        }

        return effects.ToArray();
    }

    private IReadOnlyList<CardSource> EffectSourceCards() =>
        new[] { TopCard }
            .Concat(DigivolutionCards)
            .Concat(LinkedCards)
            .ToArray();

    private static CEntity_EffectController CreateEffectController(
        CardSource source,
        ICEntityEffectRegistry effectRegistry)
    {
        var controller = new CEntity_EffectController(effectRegistry: effectRegistry);
        controller.AddCardEffect(source.Entity.CardID, source.Entity.CardEffectClassName);
        return controller;
    }

    private void AddPermanentEffect(List<ICardEffect> effects, ICardEffect cardEffect)
    {
        if (cardEffect.EffectSourcePermanent is null)
        {
            cardEffect.SetEffectSourcePermanent(this);
        }

        effects.Add(cardEffect);
    }
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
