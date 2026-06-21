using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Infrastructure;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Primitives;

public sealed record RevealResult(PlayerId Player, IReadOnlyList<CardInstanceId> RevealedCards);

public sealed record DeletePermanentResult(PermanentId Permanent, IReadOnlyList<CardInstanceId> TrashedCards);

public sealed record ReturnPermanentToHandResult(
    PermanentId Permanent,
    CardInstanceId ReturnedTopCard,
    IReadOnlyList<CardInstanceId> TrashedSourceCards,
    MoveCardResult TopCardMove,
    IReadOnlyList<MoveCardResult> SourceMoves);

public sealed record RecoverFromDeckResult(
    PlayerId Player,
    IReadOnlyList<CardInstanceId> AddedCards,
    IReadOnlyList<MoveCardResult> Moves,
    bool RequestedMoreThanAvailable);

public sealed class Tier1PrimitiveService
{
    private readonly IZoneMover _zoneMover;
    private readonly DrawService _drawService;
    private readonly BattleResolver _battleResolver;
    private SecurityCheckService _securityCheckService;
    private PlayCardService? _playCardService;
    private DigivolveService _digivolveService;

    public Tier1PrimitiveService(
        IZoneMover? zoneMover = null,
        DrawService? drawService = null,
        BattleResolver? battleResolver = null,
        SecurityCheckService? securityCheckService = null,
        PlayCardService? playCardService = null,
        DigivolveService? digivolveService = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _drawService = drawService ?? new DrawService(_zoneMover);
        _battleResolver = battleResolver ?? new BattleResolver(_zoneMover);
        _securityCheckService = securityCheckService ?? new SecurityCheckService(_zoneMover, _battleResolver);
        _playCardService = playCardService;
        _digivolveService = digivolveService ?? new DigivolveService(_zoneMover, _drawService);
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal SecurityCheckService RuntimeSecurityCheckService => _securityCheckService;

    internal PlayCardService? RuntimePlayCardService => _playCardService;

    internal DigivolveService RuntimeDigivolveService => _digivolveService;

    internal void AttachRuntimeServices(
        SecurityCheckService securityCheckService,
        PlayCardService playCardService,
        DigivolveService digivolveService)
    {
        _securityCheckService = securityCheckService ?? throw new ArgumentNullException(nameof(securityCheckService));
        _playCardService = playCardService ?? throw new ArgumentNullException(nameof(playCardService));
        _digivolveService = digivolveService ?? throw new ArgumentNullException(nameof(digivolveService));
    }

    public MoveCardResult MoveCard(GameState state, MoveCardCommand command) =>
        _zoneMover.MoveCard(state, command);

    public DrawResult Draw(GameState state, PlayerId player, int count, GameTrace? trace = null)
    {
        var result = _drawService.DrawCards(state, player, count, trace);
        QueueCardsAddedToHandEvents(
            state,
            player,
            result.DrawnCards,
            sourceZone: Zone.Deck,
            sourceCard: null,
            sourcePermanent: null,
            isDraw: true);
        return result;
    }

    public void Shuffle(GameState state, PlayerId player, Zone zone, IDeterministicRng rng)
    {
        var cards = state.GetPlayer(player).CardsIn(zone);
        for (var i = cards.Count - 1; i > 0; i--)
        {
            var j = rng.NextInt(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public RevealResult RevealFromDeckTop(GameState state, PlayerId playerId, int count, GameTrace? trace = null)
    {
        if (count < 0)
        {
            throw new DomainException("Reveal count must not be negative.");
        }

        var player = state.GetPlayer(playerId);
        var revealed = new List<CardInstanceId>();
        for (var i = 0; i < count && player.Deck.Count > 0; i++)
        {
            var card = player.Deck[0];
            var command = new MoveCardCommand(card, Zone.Deck, Zone.Revealed, MoveReason.Reveal, FaceUp: true);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"reveal:{playerId.Value}:{i}", before!, state, command, result);
            revealed.Add(card);
        }

        return new RevealResult(playerId, revealed);
    }

    public SelectionRequest CreateSearchRequest(
        GameState state,
        PlayerId player,
        string requestId,
        IEnumerable<CardInstanceId> candidates,
        int minCount,
        int maxCount,
        bool canSkip,
        string prompt)
    {
        var targets = candidates
            .Select(card => CreateCardTarget(state, card))
            .ToArray();

        return new SelectionRequest(
            requestId,
            player,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            minCount,
            maxCount,
            canSkip,
            canEndNotMax: canSkip,
            targets,
            prompt);
    }

    public IReadOnlyList<MoveCardResult> Trash(GameState state, IEnumerable<MoveCardCommand> commands)
    {
        var results = new List<MoveCardResult>();
        foreach (var command in commands)
        {
            if (command.DestinationZone != Zone.Trash)
            {
                throw new DomainException("Trash primitive requires DestinationZone Trash.");
            }

            results.Add(_zoneMover.MoveCard(state, command));
        }

        return results;
    }

    public IReadOnlyList<MoveCardResult> AddCardsToHandWithEvents(
        GameState state,
        PlayerId player,
        IEnumerable<CardInstanceId> cards,
        Zone sourceZone,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        bool isDraw = false,
        GameTrace? trace = null)
    {
        var cardArray = cards.ToArray();
        if (cardArray.Length == 0)
        {
            return Array.Empty<MoveCardResult>();
        }

        _ = state.GetPlayer(player);
        foreach (var card in cardArray)
        {
            if (state.Cards[card].Owner != player)
            {
                throw new DomainException($"Card '{card}' is owned by '{state.Cards[card].Owner}', not '{player}'.");
            }
        }

        if (sourceZone == Zone.Trash)
        {
            QueueRuleEvent(
                state,
                EffectTiming.OnReturnCardsToHandFromTrash,
                player,
                ZoneEventPayload(
                    cardArray,
                    sourceZone,
                    Zone.Hand,
                    sourceCard,
                    sourcePermanent,
                    extra: new Dictionary<string, object?>
                    {
                        ["CardSources"] = cardArray,
                    }));
        }

        var moves = new List<MoveCardResult>();
        foreach (var card in cardArray)
        {
            var command = new MoveCardCommand(card, sourceZone, Zone.Hand, MoveReason.Effect, FaceUp: true);
            var before = trace is null ? null : state.Clone();
            var move = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"add-hand:{player.Value}:{card.Value}", before!, state, command, move);
            moves.Add(move);
        }

        QueueCardsAddedToHandEvents(
            state,
            player,
            cardArray,
            sourceZone,
            sourceCard,
            sourcePermanent,
            isDraw);
        return moves;
    }

    public IReadOnlyList<MoveCardResult> DiscardHandWithEvents(
        GameState state,
        PlayerId player,
        IEnumerable<CardInstanceId> cards,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        GameTrace? trace = null)
    {
        var cardArray = cards.ToArray();
        if (cardArray.Length == 0)
        {
            return Array.Empty<MoveCardResult>();
        }

        var playerState = state.GetPlayer(player);
        foreach (var card in cardArray)
        {
            if (!playerState.Hand.Contains(card))
            {
                throw new DomainException($"Card '{card}' is not in player '{player}' hand.");
            }
        }

        var moves = new List<MoveCardResult>();
        foreach (var card in cardArray)
        {
            var command = new MoveCardCommand(card, Zone.Hand, Zone.Trash, MoveReason.Effect, FaceUp: true);
            var before = trace is null ? null : state.Clone();
            var move = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"discard-hand:{player.Value}:{card.Value}", before!, state, command, move);
            moves.Add(move);
        }

        QueueRuleEvent(
            state,
            EffectTiming.OnDiscardHand,
            player,
            ZoneEventPayload(
                cardArray,
                Zone.Hand,
                Zone.Trash,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["DiscardedCards"] = cardArray,
                    ["CardSources"] = cardArray,
                }));
        return moves;
    }

    public IReadOnlyList<MoveCardResult> ReturnTrashCardsToLibraryWithEvents(
        GameState state,
        PlayerId player,
        IEnumerable<CardInstanceId> cards,
        bool toTop,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        GameTrace? trace = null)
    {
        var cardArray = cards.ToArray();
        if (cardArray.Length == 0)
        {
            return Array.Empty<MoveCardResult>();
        }

        var playerState = state.GetPlayer(player);
        foreach (var card in cardArray)
        {
            if (!playerState.Trash.Contains(card))
            {
                throw new DomainException($"Card '{card}' is not in player '{player}' trash.");
            }
        }

        QueueRuleEvent(
            state,
            EffectTiming.OnReturnCardsToLibraryFromTrash,
            player,
            ZoneEventPayload(
                cardArray,
                Zone.Trash,
                Zone.Deck,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["CardSources"] = cardArray,
                    ["ToTop"] = toTop,
                }));

        var moves = new List<MoveCardResult>();
        foreach (var card in cardArray)
        {
            var command = new MoveCardCommand(card, Zone.Trash, Zone.Deck, MoveReason.Effect, ToTop: toTop, FaceUp: false);
            var before = trace is null ? null : state.Clone();
            var move = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"return-trash-library:{player.Value}:{card.Value}", before!, state, command, move);
            moves.Add(move);
        }

        return moves;
    }

    public MoveCardResult TrashCard(
        GameState state,
        CardInstanceId card,
        Zone sourceZone,
        PermanentId? sourcePermanent = null,
        MoveReason reason = MoveReason.Trash) =>
        _zoneMover.MoveCard(state, new MoveCardCommand(card, sourceZone, Zone.Trash, reason, SourcePermanent: sourcePermanent));

    public MoveCardResult AddSecurity(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        Zone sourceZone,
        bool toTop = true,
        bool faceUp = false)
    {
        if (state.Cards[card].Owner != player)
        {
            throw new UnsupportedMechanicException("Adding another player's card to security");
        }

        return _zoneMover.MoveCard(
            state,
            new MoveCardCommand(card, sourceZone, Zone.Security, MoveReason.Effect, ToTop: toTop, FaceUp: faceUp));
    }

    public RecoverFromDeckResult RecoverFromDeck(
        GameState state,
        PlayerId playerId,
        int count,
        GameTrace? trace = null)
    {
        if (count <= 0)
        {
            throw new DomainException("Recovery count must be positive.");
        }

        var player = state.GetPlayer(playerId);
        if (player.Deck.Count == 0)
        {
            throw new DomainException($"Player '{playerId}' cannot recover from an empty deck.");
        }

        var addedCards = new List<CardInstanceId>();
        var moves = new List<MoveCardResult>();
        for (var i = 0; i < count && player.Deck.Count > 0; i++)
        {
            var card = player.Deck[0];
            var command = new MoveCardCommand(
                card,
                Zone.Deck,
                Zone.Security,
                MoveReason.Effect,
                ToTop: true,
                FaceUp: false);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"recovery-from-deck:{playerId.Value}:{i}", before!, state, command, result);
            addedCards.Add(card);
            moves.Add(result);
        }

        return new RecoverFromDeckResult(
            playerId,
            addedCards,
            moves,
            RequestedMoreThanAvailable: addedCards.Count < count);
    }

    public MoveCardResult RemoveSecurity(
        GameState state,
        PlayerId player,
        CardInstanceId? card = null,
        Zone destinationZone = Zone.Trash)
    {
        var playerState = state.GetPlayer(player);
        if (playerState.Security.Count == 0)
        {
            throw new DomainException($"Player '{player}' has no security cards.");
        }

        var target = card ?? playerState.Security[0];
        if (!playerState.Security.Contains(target))
        {
            throw new DomainException($"Card '{target}' is not in player '{player}' security.");
        }

        return _zoneMover.MoveCard(
            state,
            new MoveCardCommand(target, Zone.Security, destinationZone, MoveReason.Effect, FaceUp: true));
    }

    public MoveCardResult AddExecutingSecurityEffectCardToHand(
        GameState state,
        CardInstanceId card,
        PlayerId owner)
    {
        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Security effect card '{card}' does not exist.");
        }

        if (instance.Owner != owner)
        {
            throw new DomainException($"Security effect card '{card}' is owned by '{instance.Owner}', not '{owner}'.");
        }

        if (instance.CurrentZone != Zone.Executing)
        {
            throw new DomainException($"Security effect card '{card}' must be in Executing, actual '{instance.CurrentZone}'.");
        }

        return _zoneMover.MoveCard(
            state,
            new MoveCardCommand(card, Zone.Executing, Zone.Hand, MoveReason.Effect, FaceUp: true));
    }

    public IReadOnlyList<MoveCardResult> TrashBottomDigivolutionSources(
        GameState state,
        PermanentId permanentId,
        int count)
    {
        if (count <= 0)
        {
            throw new DomainException("Digivolution source trash count must be positive.");
        }

        var permanent = BattleRules.Permanent(state, permanentId);
        if (permanent.IsBreedingArea)
        {
            throw new DomainException($"Digivolution source trash target permanent '{permanentId}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, permanent.TopCardId))
        {
            throw new DomainException($"Digivolution source trash target permanent '{permanentId}' is not a Digimon.");
        }

        if (permanent.SourceCardIds.Count == 0)
        {
            throw new DomainException($"Digivolution source trash target permanent '{permanentId}' has no digivolution sources.");
        }

        var results = new List<MoveCardResult>();
        var trashCount = Math.Min(count, permanent.SourceCardIds.Count);
        for (var i = 0; i < trashCount; i++)
        {
            var source = permanent.SourceCardIds[^1];
            results.Add(_zoneMover.MoveCard(
                state,
                new MoveCardCommand(
                    source,
                    Zone.EvolutionSources,
                    Zone.Trash,
                    MoveReason.Effect,
                    SourcePermanent: permanent.Id)));
        }

        return results;
    }

    public IReadOnlyList<MoveCardResult> ReturnDigivolutionSourcesToDeckBottomWithEvents(
        GameState state,
        PermanentId permanentId,
        IEnumerable<CardInstanceId> cards,
        CardInstanceId? sourceCard = null,
        GameTrace? trace = null)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        var cardArray = cards.ToArray();
        if (cardArray.Length == 0)
        {
            return Array.Empty<MoveCardResult>();
        }

        foreach (var card in cardArray)
        {
            if (!permanent.SourceCardIds.Contains(card))
            {
                throw new DomainException($"Card '{card}' is not a digivolution source under permanent '{permanentId}'.");
            }
        }

        QueueRuleEvent(
            state,
            EffectTiming.OnDigivolutionCardReturnToDeckBottom,
            permanent.ControllerPlayerId,
            ZoneEventPayload(
                cardArray,
                Zone.EvolutionSources,
                Zone.Deck,
                sourceCard,
                permanentId,
                extra: new Dictionary<string, object?>
                {
                    ["Permanent"] = permanentId,
                    ["DeckBottomCards"] = cardArray,
                    ["CardSources"] = cardArray,
                }));

        var moves = new List<MoveCardResult>();
        foreach (var card in cardArray)
        {
            var command = new MoveCardCommand(
                card,
                Zone.EvolutionSources,
                Zone.Deck,
                MoveReason.Effect,
                SourcePermanent: permanentId,
                ToTop: false,
                FaceUp: false);
            var before = trace is null ? null : state.Clone();
            var move = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"return-source-deck-bottom:{permanentId.Value}:{card.Value}", before!, state, command, move);
            moves.Add(move);
        }

        return moves;
    }

    public DeletePermanentResult DeletePermanent(GameState state, PermanentId permanent, GameTrace? trace = null) =>
        DestroyPermanent(state, permanent, trace);

    public ReturnPermanentToHandResult ReturnPermanentToHand(GameState state, PermanentId permanentId, GameTrace? trace = null)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        if (permanent.IsBreedingArea)
        {
            throw new DomainException($"Return-to-hand target permanent '{permanentId}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, permanent.TopCardId))
        {
            throw new DomainException($"Return-to-hand target permanent '{permanentId}' is not a Digimon.");
        }

        if (permanent.LinkedCards.Count > 0)
        {
            throw new UnsupportedMechanicException("Returning permanents with linked cards to hand");
        }

        var topCard = permanent.TopCardId;
        var sourceCards = permanent.SourceCardIds.ToArray();
        var sourceMoves = new List<MoveCardResult>();
        foreach (var source in sourceCards)
        {
            var command = new MoveCardCommand(
                source,
                Zone.EvolutionSources,
                Zone.Trash,
                MoveReason.Effect,
                SourcePermanent: permanentId);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"return-source-trash:{permanentId.Value}:{source.Value}", before!, state, command, result);
            sourceMoves.Add(result);
        }

        var topZone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
        var topCommand = new MoveCardCommand(
            topCard,
            topZone,
            Zone.Hand,
            MoveReason.Effect,
            SourcePermanent: permanentId,
            FaceUp: true);
        var topBefore = trace is null ? null : state.Clone();
        var topResult = _zoneMover.MoveCard(state, topCommand);
        trace?.AddMove($"return-top-to-hand:{permanentId.Value}:{topCard.Value}", topBefore!, state, topCommand, topResult);

        return new ReturnPermanentToHandResult(permanentId, topCard, sourceCards, topResult, sourceMoves);
    }

    public ReturnPermanentToHandResult ReturnPermanentToHandWithEvents(
        GameState state,
        PermanentId permanentId,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        GameTrace? trace = null)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        var topCard = permanent.TopCardId;
        var sourceCards = permanent.SourceCardIds.ToArray();
        var controller = permanent.ControllerPlayerId;

        QueueRuleEvent(
            state,
            EffectTiming.WhenReturntoHandAnyone,
            controller,
            ZoneEventPayload(
                new[] { topCard },
                permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea,
                Zone.Hand,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["Permanent"] = permanentId,
                    ["ReturnedTopCard"] = topCard,
                    ["CardSources"] = new[] { topCard },
                    ["WouldReturn"] = true,
                }));

        var result = ReturnPermanentToHand(state, permanentId, trace);

        var postPayload = ZoneEventPayload(
            new[] { result.ReturnedTopCard },
            permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea,
            Zone.Hand,
            sourceCard,
            sourcePermanent,
            extra: new Dictionary<string, object?>
            {
                ["Permanent"] = permanentId,
                ["ReturnedTopCard"] = result.ReturnedTopCard,
                ["TrashedSourceCards"] = sourceCards,
                ["CardSources"] = new[] { result.ReturnedTopCard },
            });
        QueueRuleEvent(state, EffectTiming.OnPermamemtReturnedToHand, controller, postPayload);
        QueueRuleEvent(state, EffectTiming.OnLeaveFieldAnyone, controller, postPayload);
        return result;
    }

    public MoveCardResult TrashTopCardWithEvents(
        GameState state,
        PermanentId permanentId,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        GameTrace? trace = null)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        var topCard = permanent.TopCardId;
        var zone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
        QueueRuleEvent(
            state,
            EffectTiming.WhenTopCardTrashed,
            permanent.ControllerPlayerId,
            ZoneEventPayload(
                new[] { topCard },
                zone,
                Zone.Trash,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["Permanent"] = permanentId,
                    ["CardSources"] = new[] { topCard },
                    ["TopCard"] = topCard,
                }));

        var command = new MoveCardCommand(
            topCard,
            zone,
            Zone.Trash,
            MoveReason.Effect,
            SourcePermanent: permanentId,
            FaceUp: true);
        var before = trace is null ? null : state.Clone();
        var result = _zoneMover.MoveCard(state, command);
        trace?.AddMove($"trash-top-card:{permanentId.Value}:{topCard.Value}", before!, state, command, result);
        return result;
    }

    public DeletePermanentResult DestroyPermanent(GameState state, PermanentId permanentId, GameTrace? trace = null)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        var stack = permanent.StackCardIds.Concat(permanent.LinkedCards).ToArray();
        _battleResolver.DestroyPermanent(state, permanent, trace);
        return new DeletePermanentResult(permanentId, stack);
    }

    public bool Suspend(GameState state, PermanentId permanentId)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        if (permanent.IsSuspended)
        {
            return false;
        }

        permanent.IsSuspended = true;
        return true;
    }

    public bool Unsuspend(GameState state, PermanentId permanentId)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        if (!permanent.IsSuspended)
        {
            return false;
        }

        permanent.IsSuspended = false;
        return true;
    }

    public int ModifyMemory(GameState state, PlayerId player, int amount)
    {
        _ = state.GetPlayer(player);
        var delta = player == state.TurnPlayerId ? amount : -amount;
        state.Memory = Math.Clamp(state.Memory + delta, -10, 10);
        return state.Memory;
    }

    public int ModifyDP(GameState state, PermanentId permanentId, int amount)
    {
        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.DpModifier += amount;
        return BattleRules.Dp(state, permanent);
    }

    public TemporaryModifier AddTemporaryDPModifier(
        GameState state,
        PermanentId targetPermanent,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null)
    {
        if (amount == 0)
        {
            throw new DomainException("Temporary DP modifier amount must not be zero.");
        }

        var permanent = BattleRules.Permanent(state, targetPermanent);
        if (permanent.IsBreedingArea)
        {
            throw new DomainException($"Temporary DP modifier target permanent '{targetPermanent}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, permanent.TopCardId))
        {
            throw new DomainException($"Temporary DP modifier target permanent '{targetPermanent}' is not a Digimon.");
        }

        return AddTemporaryModifier(
            state,
            TemporaryModifierKind.DP,
            amount,
            durationScope,
            controller,
            targetPermanent,
            targetPlayer: null,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"DP {amount:+#;-#;0} until {durationScope}");
    }

    public TemporaryModifier AddTemporaryPlayerDPModifier(
        GameState state,
        PlayerId targetPlayer,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null)
    {
        if (amount == 0)
        {
            throw new DomainException("Temporary player DP modifier amount must not be zero.");
        }

        _ = state.GetPlayer(targetPlayer);
        return AddTemporaryModifier(
            state,
            TemporaryModifierKind.DP,
            amount,
            durationScope,
            controller,
            targetPermanent: null,
            targetPlayer,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"Player DP {amount:+#;-#;0} until {durationScope}");
    }

    public TemporaryModifier AddTemporarySecurityAttackModifier(
        GameState state,
        PlayerId targetPlayer,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null)
    {
        if (amount == 0)
        {
            throw new DomainException("Temporary SecurityAttack modifier amount must not be zero.");
        }

        _ = state.GetPlayer(targetPlayer);
        return AddTemporaryModifier(
            state,
            TemporaryModifierKind.SecurityAttack,
            amount,
            durationScope,
            controller,
            targetPermanent: null,
            targetPlayer,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"SecurityAttack {amount:+#;-#;0} until {durationScope}");
    }

    public TemporaryModifier AddTemporarySecurityAttackModifier(
        GameState state,
        PermanentId targetPermanent,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null)
    {
        if (amount == 0)
        {
            throw new DomainException("Temporary SecurityAttack modifier amount must not be zero.");
        }

        var permanent = BattleRules.Permanent(state, targetPermanent);
        if (permanent.IsBreedingArea)
        {
            throw new DomainException($"Temporary SecurityAttack modifier target permanent '{targetPermanent}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, permanent.TopCardId))
        {
            throw new DomainException($"Temporary SecurityAttack modifier target permanent '{targetPermanent}' is not a Digimon.");
        }

        return AddTemporaryModifier(
            state,
            TemporaryModifierKind.SecurityAttack,
            amount,
            durationScope,
            controller,
            targetPermanent,
            targetPlayer: null,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"SecurityAttack {amount:+#;-#;0} until {durationScope}");
    }

    public TemporaryModifier AddTemporarySecurityDigimonDPModifier(
        GameState state,
        PlayerId targetPlayer,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null)
    {
        if (amount == 0)
        {
            throw new DomainException("Temporary Security Digimon DP modifier amount must not be zero.");
        }

        _ = state.GetPlayer(targetPlayer);
        return AddTemporaryModifier(
            state,
            TemporaryModifierKind.SecurityDigimonDP,
            amount,
            durationScope,
            controller,
            targetPermanent: null,
            targetPlayer,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"Security Digimon DP {amount:+#;-#;0} until {durationScope}");
    }

    public TemporaryModifier AddTemporaryAttackRestriction(
        GameState state,
        PermanentId targetPermanent,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null) =>
        AddTemporaryPermanentRestriction(
            state,
            targetPermanent,
            TemporaryModifierKind.CannotAttack,
            durationScope,
            controller,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"Cannot attack until {durationScope}");

    public TemporaryModifier AddTemporaryBlockRestriction(
        GameState state,
        PermanentId targetPermanent,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null) =>
        AddTemporaryPermanentRestriction(
            state,
            targetPermanent,
            TemporaryModifierKind.CannotBlock,
            durationScope,
            controller,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"Cannot block until {durationScope}");

    public TemporaryModifier AddTemporarySwitchAttackTargetRestriction(
        GameState state,
        PermanentId targetPermanent,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        string? stableId = null,
        string? debugLabel = null) =>
        AddTemporaryPermanentRestriction(
            state,
            targetPermanent,
            TemporaryModifierKind.CannotSwitchAttackTarget,
            durationScope,
            controller,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel ?? $"Cannot switch attack target until {durationScope}");

    private TemporaryModifier AddTemporaryPermanentRestriction(
        GameState state,
        PermanentId targetPermanent,
        TemporaryModifierKind restrictionKind,
        DurationScope durationScope,
        PlayerId controller,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        string? stableId,
        string debugLabel)
    {
        if (restrictionKind is not (
            TemporaryModifierKind.CannotAttack
            or TemporaryModifierKind.CannotBlock
            or TemporaryModifierKind.CannotSwitchAttackTarget))
        {
            throw new DomainException($"Temporary modifier kind '{restrictionKind}' is not a restriction.");
        }

        var permanent = BattleRules.Permanent(state, targetPermanent);
        if (permanent.IsBreedingArea)
        {
            throw new DomainException($"Restriction target permanent '{targetPermanent}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, permanent.TopCardId))
        {
            throw new DomainException($"Restriction target permanent '{targetPermanent}' is not a Digimon.");
        }

        return AddTemporaryModifier(
            state,
            restrictionKind,
            amount: 1,
            durationScope,
            controller,
            targetPermanent,
            targetPlayer: null,
            sourceCard,
            sourcePermanent,
            stableId,
            debugLabel);
    }

    public PermanentState? PlayCard(GameState state, PlayCardAction action) =>
        (_playCardService
            ?? throw new DomainException("Tier1PrimitiveService requires PlayCardService to execute PlayCard."))
        .Play(state, action);

    public PermanentState PlayWithoutPayingCost(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        Zone sourceZone,
        int targetFrameIndex,
        bool suspended = false)
    {
        _ = state.GetPlayer(player);
        if (state.Cards[card].Owner != player)
        {
            throw new DomainException($"Card '{card}' is not owned by player '{player}'.");
        }

        var definition = BattleRules.Definition(state, card);
        if (!definition.IsPermanent || definition.CardKinds.Contains(CardKind.Option))
        {
            throw new UnsupportedMechanicException($"PlayWithoutPayingCost for card kind '{string.Join(",", definition.CardKinds)}'");
        }

        var playerState = state.GetPlayer(player);
        if (!BattleRules.IsEmptyBattleFrame(playerState, targetFrameIndex))
        {
            throw new DomainException($"Battle frame '{targetFrameIndex}' is not empty.");
        }

        var permanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                card,
                sourceZone,
                Zone.BattleArea,
                MoveReason.Effect,
                DestinationPermanent: permanentId,
                DestinationFrameIndex: targetFrameIndex));

        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.EnterFieldTurnCount = state.TurnCount;
        permanent.IsSuspended = suspended;
        return permanent;
    }

    public PermanentState PlayEvolutionSourceAsNewPermanent(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        PermanentId sourcePermanent,
        int targetFrameIndex,
        bool suspended = false)
    {
        var playerState = state.GetPlayer(player);
        var host = BattleRules.Permanent(state, sourcePermanent);
        if (host.ControllerPlayerId != player)
        {
            throw new DomainException($"Evolution source host '{sourcePermanent}' is not controlled by player '{player}'.");
        }

        if (host.IsBreedingArea)
        {
            throw new DomainException($"Evolution source host '{sourcePermanent}' is not in battle area.");
        }

        if (!BattleRules.IsDigimon(state, host.TopCardId))
        {
            throw new DomainException($"Evolution source host '{sourcePermanent}' is not a Digimon.");
        }

        if (!host.SourceCardIds.Contains(card))
        {
            throw new DomainException($"Card '{card}' is not a digivolution source under permanent '{sourcePermanent}'.");
        }

        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Evolution source card '{card}' does not exist.");
        }

        if (instance.Owner != player)
        {
            throw new DomainException($"Evolution source card '{card}' is owned by '{instance.Owner}', not '{player}'.");
        }

        if (instance.CurrentZone != Zone.EvolutionSources || instance.PermanentId != sourcePermanent)
        {
            throw new DomainException(
                $"Evolution source card '{card}' is not linked to permanent '{sourcePermanent}' in EvolutionSources.");
        }

        var definition = BattleRules.Definition(state, card);
        if (!definition.CardKinds.Contains(CardKind.Digimon))
        {
            throw new DomainException($"Evolution source card '{card}' is not a Digimon.");
        }

        if (!definition.IsPermanent || definition.CardKinds.Contains(CardKind.Option))
        {
            throw new UnsupportedMechanicException($"Playing evolution source kind '{string.Join(",", definition.CardKinds)}'");
        }

        if (!BattleRules.IsEmptyBattleFrame(playerState, targetFrameIndex))
        {
            throw new DomainException($"Battle frame '{targetFrameIndex}' is not empty.");
        }

        return PlayWithoutPayingCost(
            state,
            player,
            card,
            Zone.EvolutionSources,
            targetFrameIndex,
            suspended);
    }

    public PermanentState Digivolve(GameState state, DigivolveAction action, GameTrace? trace = null) =>
        _digivolveService.Digivolve(state, action, trace);

    public PermanentState DigivolveByEffect(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        Zone sourceZone,
        PermanentId targetPermanent,
        bool drawForDigivolution = false,
        GameTrace? trace = null)
    {
        _ = state.GetPlayer(player);
        if (state.Cards[card].Owner != player)
        {
            throw new DomainException($"Card '{card}' is not owned by player '{player}'.");
        }

        var permanent = BattleRules.Permanent(state, targetPermanent);
        if (permanent.ControllerPlayerId != player)
        {
            throw new DomainException($"Permanent '{targetPermanent}' is not controlled by player '{player}'.");
        }

        if (!BattleRules.CanDigivolve(state, card, permanent, out _))
        {
            throw new DomainException($"Card '{card}' cannot digivolve onto permanent '{targetPermanent}'.");
        }

        _zoneMover.DigivolveCard(state, new DigivolveCardCommand(card, sourceZone, targetPermanent, MoveReason.Effect));
        if (drawForDigivolution)
        {
            _drawService.DrawCards(state, player, 1, trace);
        }

        return permanent;
    }

    public SecurityCheckResult SecurityCheck(GameState state, PermanentId attacker, PlayerId defender, GameTrace? trace = null) =>
        _securityCheckService.CheckSecurity(state, attacker, defender, trace);

    public BattleResolutionResult Battle(GameState state, PermanentId attacker, PermanentId defender, GameTrace? trace = null) =>
        _battleResolver.ResolvePermanentBattle(state, attacker, defender, trace);

    public PermanentState CreateToken(
        GameState state,
        PlayerId player,
        CardDefinition tokenDefinition,
        int targetFrameIndex,
        bool suspended = false)
    {
        if (!tokenDefinition.IsPermanent || tokenDefinition.CardKinds.Contains(CardKind.Option))
        {
            throw new UnsupportedMechanicException("CreateToken only supports permanent token cards.");
        }

        state.CardDefinitions.TryAdd(tokenDefinition.CardId, tokenDefinition);

        var tokenId = NextCardInstanceId(state);
        var token = new CardInstance(tokenId, tokenDefinition.CardId, player)
        {
            CurrentZone = Zone.OutsideGame,
            IsFaceUp = true,
        };

        state.Cards.Add(tokenId, token);
        state.GetPlayer(player).OutsideGame.Add(tokenId);

        return PlayWithoutPayingCost(state, player, tokenId, Zone.OutsideGame, targetFrameIndex, suspended);
    }

    private static CardInstanceId NextCardInstanceId(GameState state) =>
        new(state.Cards.Keys.Select(card => card.Value).DefaultIfEmpty(0).Max() + 1);

    private TemporaryModifier AddTemporaryModifier(
        GameState state,
        TemporaryModifierKind kind,
        int amount,
        DurationScope durationScope,
        PlayerId controller,
        PermanentId? targetPermanent,
        PlayerId? targetPlayer,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        string? stableId,
        string debugLabel)
    {
        _ = state.GetPlayer(controller);
        if (sourceCard is not null && !state.Cards.ContainsKey(sourceCard.Value))
        {
            throw new DomainException($"Temporary modifier source card '{sourceCard}' does not exist.");
        }

        if (sourcePermanent is not null)
        {
            _ = BattleRules.Permanent(state, sourcePermanent.Value);
        }

        if (targetPermanent is null && targetPlayer is null)
        {
            throw new DomainException("Temporary modifier requires a permanent or player target.");
        }

        if (targetPermanent is not null && targetPlayer is not null)
        {
            throw new DomainException("Temporary modifier cannot target both permanent and player.");
        }

        var id = stableId ?? NextTemporaryModifierStableId(state, kind, targetPermanent, targetPlayer);
        if (state.TemporaryModifiers.Any(modifier => string.Equals(modifier.StableId, id, StringComparison.Ordinal)))
        {
            throw new DomainException($"Temporary modifier stable id '{id}' already exists.");
        }

        var modifier = new TemporaryModifier(
            id,
            sourceCard,
            sourcePermanent,
            controller,
            targetPermanent,
            targetPlayer,
            kind,
            amount,
            durationScope,
            state.TurnCount,
            state.Phase,
            ResolveExpiresAtTurnPlayer(state, controller, durationScope),
            debugLabel);

        state.TemporaryModifiers.Add(modifier);
        return modifier;
    }

    private static string NextTemporaryModifierStableId(
        GameState state,
        TemporaryModifierKind kind,
        PermanentId? targetPermanent,
        PlayerId? targetPlayer)
    {
        var target = targetPermanent is not null
            ? $"permanent:{targetPermanent.Value.Value}"
            : $"player:{targetPlayer!.Value.Value}";
        return $"duration:{state.TurnCount}:{state.Phase}:{kind}:{target}:{state.TemporaryModifiers.Count + 1}";
    }

    private static PlayerId? ResolveExpiresAtTurnPlayer(GameState state, PlayerId controller, DurationScope durationScope) =>
        durationScope switch
        {
            DurationScope.UntilOwnerTurnEnd => controller,
            DurationScope.UntilOpponentTurnEnd => Opponent(state, controller),
            DurationScope.UntilOwnerActivePhaseEnd => controller,
            _ => null,
        };

    private static PlayerId Opponent(GameState state, PlayerId player)
    {
        foreach (var candidate in state.Players.Select(candidate => candidate.Id))
        {
            if (candidate != player)
            {
                return candidate;
            }
        }

        throw new DomainException($"Player '{player}' has no opponent.");
    }

    private static void QueueCardsAddedToHandEvents(
        GameState state,
        PlayerId player,
        IReadOnlyList<CardInstanceId> cards,
        Zone sourceZone,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        bool isDraw)
    {
        if (cards.Count == 0)
        {
            return;
        }

        QueueRuleEvent(
            state,
            EffectTiming.OnAddHand,
            player,
            ZoneEventPayload(
                cards,
                sourceZone,
                Zone.Hand,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["Players"] = new[] { player },
                    ["CardSources"] = cards.ToArray(),
                    ["IsDraw"] = isDraw,
                }));

        if (!isDraw)
        {
            return;
        }

        QueueRuleEvent(
            state,
            EffectTiming.OnDraw,
            player,
            ZoneEventPayload(
                cards,
                sourceZone,
                Zone.Hand,
                sourceCard,
                sourcePermanent,
                extra: new Dictionary<string, object?>
                {
                    ["DrawnCards"] = cards.ToArray(),
                    ["CardSources"] = cards.ToArray(),
                }));
    }

    private static void QueueRuleEvent(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        IReadOnlyDictionary<string, object?> payload) =>
        state.RuntimeRules.EnqueueRuleEvent(timing, player, payload);

    private static Dictionary<string, object?> ZoneEventPayload(
        IReadOnlyList<CardInstanceId> cards,
        Zone sourceZone,
        Zone destinationZone,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        IReadOnlyDictionary<string, object?>? extra = null)
    {
        var payload = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["Cards"] = cards.ToArray(),
            ["CardSources"] = cards.ToArray(),
            ["SourceZone"] = sourceZone,
            ["DestinationZone"] = destinationZone,
            ["CardEffect"] = sourceCard,
            ["SourceCard"] = sourceCard,
            ["SourcePermanent"] = sourcePermanent,
        };

        if (extra is not null)
        {
            foreach (var pair in extra)
            {
                payload[pair.Key] = pair.Value;
            }
        }

        return payload;
    }

    private static SelectableTarget CreateCardTarget(GameState state, CardInstanceId card)
    {
        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Card '{card}' does not exist.");
        }

        var label = state.CardDefinitions.TryGetValue(instance.DefinitionId, out var definition)
            ? definition.CardNameEnglish
            : instance.DefinitionId;

        return new SelectableTarget(
            SelectionTargetKind.Card,
            $"card:{card.Value}",
            instance.Owner,
            Card: card,
            Label: label,
            Zone: instance.CurrentZone);
    }
}
