namespace DCGO.RL.Engine.Domain;

public sealed class ZoneMover : IZoneMover
{
    public MoveCardResult MoveCard(GameState state, MoveCardCommand command)
    {
        if (!state.Cards.TryGetValue(command.Card, out var card))
        {
            throw new DomainException($"Card instance '{command.Card}' does not exist.");
        }

        var sourceZone = NormalizeZone(command.SourceZone);
        var destinationZone = NormalizeZone(command.DestinationZone);
        if (sourceZone == Zone.None || destinationZone == Zone.None)
        {
            throw new DomainException("Source and destination zones must be explicit.");
        }

        if (card.CurrentZone != sourceZone)
        {
            throw new DomainException($"Card '{command.Card}' current zone is '{card.CurrentZone}', not '{sourceZone}'.");
        }

        if (!IsInZone(state, command.Card, sourceZone, command.SourcePermanent))
        {
            throw new DomainException($"Card '{command.Card}' does not exist in source zone '{sourceZone}'.");
        }

        EnsureSingleMembership(state, command.Card);

        var beforeHash = state.ComputeStateHash();
        var sourcePermanent = RemoveFromZone(state, card, sourceZone, command.SourcePermanent);
        var destinationPermanent = AddToZone(state, card, destinationZone, command);
        EnsureSingleMembership(state, command.Card);

        if (card.CurrentZone != destinationZone)
        {
            throw new DomainException($"Card '{command.Card}' failed to move to '{destinationZone}'.");
        }

        var afterHash = state.ComputeStateHash();
        var trace = new ZoneMoveTrace(
            command.Card,
            card.Owner,
            sourceZone,
            destinationZone,
            command.Reason,
            sourcePermanent,
            destinationPermanent,
            beforeHash,
            afterHash);

        return new MoveCardResult(command.Card, sourceZone, destinationZone, trace);
    }

    public PermanentZoneMoveResult MovePermanent(GameState state, PermanentZoneMoveCommand command)
    {
        var sourceZone = NormalizeZone(command.SourceZone);
        var destinationZone = NormalizeZone(command.DestinationZone);
        if (sourceZone is not (Zone.BattleArea or Zone.BreedingArea) || destinationZone is not (Zone.BattleArea or Zone.BreedingArea))
        {
            throw new DomainException($"Permanent zone move only supports BattleArea/BreedingArea, actual '{sourceZone}' -> '{destinationZone}'.");
        }

        if (sourceZone == destinationZone)
        {
            throw new DomainException("Permanent source and destination zones must differ.");
        }

        var owner = FindPermanentOwner(state, command.Permanent, out var permanent);
        if (PermanentZone(permanent) != sourceZone)
        {
            throw new DomainException($"Permanent '{command.Permanent}' current zone is '{PermanentZone(permanent)}', not '{sourceZone}'.");
        }

        if (destinationZone == Zone.BreedingArea && owner.BreedingAreaPermanent is not null)
        {
            throw new DomainException($"Player '{owner.Id}' already has a breeding area permanent.");
        }

        var destinationFrameIndex = command.DestinationFrameIndex ?? NextFrameIndex(owner);
        if (destinationZone == Zone.BattleArea && owner.BattleAreaPermanents.Any(candidate => candidate.FrameIndex == destinationFrameIndex))
        {
            throw new DomainException($"Player '{owner.Id}' battle frame '{destinationFrameIndex}' is occupied.");
        }

        var topCard = state.Cards[permanent.TopCardId];
        var beforeHash = state.ComputeStateHash();

        permanent.IsBreedingArea = destinationZone == Zone.BreedingArea;
        permanent.FrameIndex = destinationFrameIndex;
        topCard.CurrentZone = destinationZone;

        var afterHash = state.ComputeStateHash();
        var trace = new ZoneMoveTrace(
            permanent.TopCardId,
            topCard.Owner,
            sourceZone,
            destinationZone,
            command.Reason,
            permanent.Id,
            permanent.Id,
            beforeHash,
            afterHash);

        return new PermanentZoneMoveResult(permanent.Id, permanent.TopCardId, sourceZone, destinationZone, trace);
    }

    public DigivolveCardResult DigivolveCard(GameState state, DigivolveCardCommand command)
    {
        if (!state.Cards.TryGetValue(command.Card, out var card))
        {
            throw new DomainException($"Card instance '{command.Card}' does not exist.");
        }

        var sourceZone = NormalizeZone(command.SourceZone);
        if (sourceZone == Zone.None)
        {
            throw new DomainException("Digivolve source zone must be explicit.");
        }

        if (card.CurrentZone != sourceZone)
        {
            throw new DomainException($"Card '{command.Card}' current zone is '{card.CurrentZone}', not '{sourceZone}'.");
        }

        if (!IsInZone(state, command.Card, sourceZone, null))
        {
            throw new DomainException($"Card '{command.Card}' does not exist in source zone '{sourceZone}'.");
        }

        var permanent = RequireDestinationPermanent(state, command.TargetPermanent, Zone.BattleArea);
        if (permanent.OwnerPlayerId != card.Owner)
        {
            throw new DomainException($"Card '{command.Card}' owner '{card.Owner}' does not match permanent owner '{permanent.OwnerPlayerId}'.");
        }

        EnsureSingleMembership(state, command.Card);
        EnsureSingleMembership(state, permanent.TopCardId);

        var beforeHash = state.ComputeStateHash();
        var previousTop = permanent.TopCardId;
        RemoveFromZone(state, card, sourceZone, null);

        permanent.TopCardId = card.Id;
        AddToPermanentList(permanent.SourceCardIds, previousTop, command.SourceToTop);

        card.CurrentZone = PermanentZone(permanent);
        card.PermanentId = permanent.Id;
        card.IsFaceUp = true;
        state.Cards[previousTop].CurrentZone = Zone.EvolutionSources;
        state.Cards[previousTop].PermanentId = permanent.Id;
        state.Cards[previousTop].IsFaceUp = true;

        EnsureSingleMembership(state, command.Card);
        EnsureSingleMembership(state, previousTop);

        var afterHash = state.ComputeStateHash();
        var trace = new ZoneMoveTrace(
            command.Card,
            card.Owner,
            sourceZone,
            card.CurrentZone,
            command.Reason,
            null,
            permanent.Id,
            beforeHash,
            afterHash);

        return new DigivolveCardResult(command.Card, previousTop, permanent.Id, trace);
    }

    private static Zone NormalizeZone(Zone zone) =>
        zone == Zone.DigivolutionCards ? Zone.EvolutionSources : zone;

    private static PermanentId? RemoveFromZone(GameState state, CardInstance card, Zone sourceZone, PermanentId? sourcePermanent)
    {
        switch (sourceZone)
        {
            case Zone.Deck:
            case Zone.DigiEggDeck:
            case Zone.Hand:
            case Zone.Security:
            case Zone.Trash:
            case Zone.Lost:
            case Zone.Executing:
            case Zone.Revealed:
            case Zone.OutsideGame:
                RemoveSingle(state.GetPlayer(card.Owner).CardsIn(sourceZone), card.Id, sourceZone);
                card.PermanentId = null;
                return null;

            case Zone.BattleArea:
            case Zone.BreedingArea:
                return RemoveTopCardFromPermanent(state, card, sourceZone, sourcePermanent);

            case Zone.EvolutionSources:
                return RemoveEvolutionSourceFromPermanent(state, card, sourcePermanent);

            case Zone.LinkedCards:
                return RemoveLinkedCardFromPermanent(state, card, sourcePermanent);

            default:
                throw new DomainException($"Unsupported source zone '{sourceZone}'.");
        }
    }

    private static PermanentId? AddToZone(GameState state, CardInstance card, Zone destinationZone, MoveCardCommand command)
    {
        switch (destinationZone)
        {
            case Zone.Deck:
            case Zone.DigiEggDeck:
            case Zone.Hand:
            case Zone.Security:
            case Zone.Trash:
            case Zone.Lost:
            case Zone.Executing:
            case Zone.Revealed:
            case Zone.OutsideGame:
                AddToPlayerZone(state.GetPlayer(card.Owner).CardsIn(destinationZone), card.Id, destinationZone, command.ToTop);
                card.CurrentZone = destinationZone;
                card.PermanentId = null;
                card.IsFaceUp = command.FaceUp ?? destinationZone != Zone.Security;
                return null;

            case Zone.BattleArea:
            case Zone.BreedingArea:
                return AddAsPermanentTopCard(state, card, destinationZone, command);

            case Zone.EvolutionSources:
                return AddAsEvolutionSource(state, card, command);

            case Zone.LinkedCards:
                return AddAsLinkedCard(state, card, command);

            default:
                throw new DomainException($"Unsupported destination zone '{destinationZone}'.");
        }
    }

    private static PermanentId RemoveTopCardFromPermanent(GameState state, CardInstance card, Zone sourceZone, PermanentId? sourcePermanent)
    {
        var (owner, permanent) = FindPermanentForTopCard(state, card.Id, sourceZone, sourcePermanent);
        var removedPermanentId = permanent.Id;

        if (permanent.SourceCardIds.Count > 0)
        {
            var promotedTopCard = permanent.SourceCardIds[0];
            permanent.SourceCardIds.RemoveAt(0);
            permanent.TopCardId = promotedTopCard;
            state.Cards[promotedTopCard].CurrentZone = sourceZone;
            state.Cards[promotedTopCard].PermanentId = permanent.Id;
        }
        else
        {
            if (permanent.LinkedCards.Count > 0)
            {
                throw new UnsupportedMechanicException(
                    "Moving the only top card away from a permanent that still has linked cards is not supported by this primitive yet.");
            }

            owner.FieldPermanents.Remove(permanent);
            RemoveTemporaryModifiersForPermanent(state, permanent.Id);
        }

        card.PermanentId = null;
        return removedPermanentId;
    }

    private static void RemoveTemporaryModifiersForPermanent(GameState state, PermanentId permanentId)
    {
        state.TemporaryModifiers.RemoveAll(modifier => modifier.TargetPermanentId == permanentId);
    }

    private static PermanentId RemoveEvolutionSourceFromPermanent(GameState state, CardInstance card, PermanentId? sourcePermanent)
    {
        var permanent = FindPermanentContainingSource(state, card.Id, sourcePermanent);
        RemoveSingle(permanent.SourceCardIds, card.Id, Zone.EvolutionSources);
        card.PermanentId = null;
        return permanent.Id;
    }

    private static PermanentId RemoveLinkedCardFromPermanent(GameState state, CardInstance card, PermanentId? sourcePermanent)
    {
        var permanent = FindPermanentContainingLinkedCard(state, card.Id, sourcePermanent);
        RemoveSingle(permanent.LinkedCards, card.Id, Zone.LinkedCards);
        card.PermanentId = null;
        return permanent.Id;
    }

    private static PermanentId AddAsPermanentTopCard(GameState state, CardInstance card, Zone destinationZone, MoveCardCommand command)
    {
        if (command.DestinationPermanent is null)
        {
            throw new DomainException($"DestinationPermanent is required when moving to '{destinationZone}'.");
        }

        if (FindPermanentByIdOrDefault(state, command.DestinationPermanent.Value) is not null)
        {
            throw new DomainException($"Permanent '{command.DestinationPermanent}' already exists.");
        }

        var permanent = new PermanentState(command.DestinationPermanent.Value, card.Owner, card.Id)
        {
            ControllerPlayerId = card.Owner,
            FrameIndex = command.DestinationFrameIndex ?? NextFrameIndex(state.GetPlayer(card.Owner)),
            IsBreedingArea = destinationZone == Zone.BreedingArea,
        };

        state.GetPlayer(card.Owner).FieldPermanents.Add(permanent);
        card.CurrentZone = destinationZone;
        card.PermanentId = permanent.Id;
        card.IsFaceUp = command.FaceUp ?? true;
        return permanent.Id;
    }

    private static PermanentId AddAsEvolutionSource(GameState state, CardInstance card, MoveCardCommand command)
    {
        var permanent = RequireDestinationPermanent(state, command.DestinationPermanent, Zone.EvolutionSources);
        AddToPermanentList(permanent.SourceCardIds, card.Id, command.ToTop);
        card.CurrentZone = Zone.EvolutionSources;
        card.PermanentId = permanent.Id;
        card.IsFaceUp = command.FaceUp ?? true;
        return permanent.Id;
    }

    private static PermanentId AddAsLinkedCard(GameState state, CardInstance card, MoveCardCommand command)
    {
        var permanent = RequireDestinationPermanent(state, command.DestinationPermanent, Zone.LinkedCards);
        AddToPermanentList(permanent.LinkedCards, card.Id, command.ToTop);
        card.CurrentZone = Zone.LinkedCards;
        card.PermanentId = permanent.Id;
        card.IsFaceUp = command.FaceUp ?? true;
        return permanent.Id;
    }

    private static bool IsInZone(GameState state, CardInstanceId card, Zone zone, PermanentId? sourcePermanent)
    {
        var instance = state.Cards[card];
        return zone switch
        {
            Zone.Deck or Zone.DigiEggDeck or Zone.Hand or Zone.Security or Zone.Trash or Zone.Lost or Zone.Executing or Zone.Revealed or Zone.OutsideGame =>
                state.GetPlayer(instance.Owner).CardsIn(zone).Contains(card),
            Zone.BattleArea or Zone.BreedingArea =>
                TryFindPermanentForTopCard(state, card, zone, sourcePermanent, out _, out _),
            Zone.EvolutionSources =>
                TryFindPermanentContainingSource(state, card, sourcePermanent, out _),
            Zone.LinkedCards =>
                TryFindPermanentContainingLinkedCard(state, card, sourcePermanent, out _),
            _ => false,
        };
    }

    private static void AddToPlayerZone(List<CardInstanceId> zoneCards, CardInstanceId card, Zone zone, bool toTop)
    {
        if (zoneCards.Contains(card))
        {
            throw new DomainException($"Card '{card}' is already in zone '{zone}'.");
        }

        if (toTop && InsertsAtTop(zone))
        {
            zoneCards.Insert(0, card);
        }
        else
        {
            zoneCards.Add(card);
        }
    }

    private static bool InsertsAtTop(Zone zone) =>
        zone is Zone.Deck or Zone.Security or Zone.Trash or Zone.Executing or Zone.Revealed;

    private static void AddToPermanentList(List<CardInstanceId> cards, CardInstanceId card, bool toTop)
    {
        if (cards.Contains(card))
        {
            throw new DomainException($"Card '{card}' is already in permanent stack.");
        }

        if (toTop)
        {
            cards.Insert(0, card);
        }
        else
        {
            cards.Add(card);
        }
    }

    private static void RemoveSingle(List<CardInstanceId> cards, CardInstanceId card, Zone zone)
    {
        if (!cards.Remove(card))
        {
            throw new DomainException($"Card '{card}' does not exist in zone '{zone}'.");
        }

        if (cards.Contains(card))
        {
            throw new DomainException($"Card '{card}' had duplicate entries in zone '{zone}'.");
        }
    }

    private static (PlayerState Owner, PermanentState Permanent) FindPermanentForTopCard(
        GameState state,
        CardInstanceId card,
        Zone zone,
        PermanentId? sourcePermanent)
    {
        if (!TryFindPermanentForTopCard(state, card, zone, sourcePermanent, out var owner, out var permanent))
        {
            throw new DomainException($"Card '{card}' is not the top card of a '{zone}' permanent.");
        }

        return (owner!, permanent!);
    }

    private static bool TryFindPermanentForTopCard(
        GameState state,
        CardInstanceId card,
        Zone zone,
        PermanentId? sourcePermanent,
        out PlayerState? owner,
        out PermanentState? permanent)
    {
        foreach (var player in state.Players)
        {
            foreach (var candidate in player.FieldPermanents)
            {
                if (sourcePermanent is not null && candidate.Id != sourcePermanent.Value)
                {
                    continue;
                }

                if (candidate.TopCardId == card && PermanentZone(candidate) == zone)
                {
                    owner = player;
                    permanent = candidate;
                    return true;
                }
            }
        }

        owner = null;
        permanent = null;
        return false;
    }

    private static PermanentState FindPermanentContainingSource(GameState state, CardInstanceId card, PermanentId? sourcePermanent)
    {
        if (TryFindPermanentContainingSource(state, card, sourcePermanent, out var permanent))
        {
            return permanent!;
        }

        throw new DomainException($"Card '{card}' is not an evolution source.");
    }

    private static bool TryFindPermanentContainingSource(
        GameState state,
        CardInstanceId card,
        PermanentId? sourcePermanent,
        out PermanentState? permanent)
    {
        permanent = state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(candidate =>
                (sourcePermanent is null || candidate.Id == sourcePermanent.Value)
                && candidate.SourceCardIds.Contains(card));

        return permanent is not null;
    }

    private static PermanentState FindPermanentContainingLinkedCard(GameState state, CardInstanceId card, PermanentId? sourcePermanent)
    {
        if (TryFindPermanentContainingLinkedCard(state, card, sourcePermanent, out var permanent))
        {
            return permanent!;
        }

        throw new DomainException($"Card '{card}' is not a linked card.");
    }

    private static bool TryFindPermanentContainingLinkedCard(
        GameState state,
        CardInstanceId card,
        PermanentId? sourcePermanent,
        out PermanentState? permanent)
    {
        permanent = state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(candidate =>
                (sourcePermanent is null || candidate.Id == sourcePermanent.Value)
                && candidate.LinkedCards.Contains(card));

        return permanent is not null;
    }

    private static PermanentState RequireDestinationPermanent(GameState state, PermanentId? permanentId, Zone zone)
    {
        if (permanentId is null)
        {
            throw new DomainException($"DestinationPermanent is required when moving to '{zone}'.");
        }

        return FindPermanentByIdOrDefault(state, permanentId.Value)
            ?? throw new DomainException($"Permanent '{permanentId}' does not exist.");
    }

    private static PermanentState? FindPermanentByIdOrDefault(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId);

    private static PlayerState FindPermanentOwner(GameState state, PermanentId permanentId, out PermanentState permanent)
    {
        foreach (var player in state.Players)
        {
            var candidate = player.FieldPermanents.FirstOrDefault(permanent => permanent.Id == permanentId);
            if (candidate is not null)
            {
                permanent = candidate;
                return player;
            }
        }

        throw new DomainException($"Permanent '{permanentId}' does not exist.");
    }

    private static Zone PermanentZone(PermanentState permanent) =>
        permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;

    private static int NextFrameIndex(PlayerState player) =>
        player.FieldPermanents.Count == 0 ? 0 : player.FieldPermanents.Max(permanent => permanent.FrameIndex) + 1;

    private static void EnsureSingleMembership(GameState state, CardInstanceId card)
    {
        var count = CountMemberships(state, card);
        if (count != 1)
        {
            throw new DomainException($"Card '{card}' must exist in exactly one zone, actual memberships: {count}.");
        }
    }

    private static int CountMemberships(GameState state, CardInstanceId card)
    {
        var count = 0;
        foreach (var player in state.Players)
        {
            count += CountIn(player.Deck, card);
            count += CountIn(player.DigiEggDeck, card);
            count += CountIn(player.Hand, card);
            count += CountIn(player.Security, card);
            count += CountIn(player.Trash, card);
            count += CountIn(player.Lost, card);
            count += CountIn(player.Executing, card);
            count += CountIn(player.Revealed, card);
            count += CountIn(player.OutsideGame, card);

            foreach (var permanent in player.FieldPermanents)
            {
                if (permanent.TopCardId == card)
                {
                    count++;
                }

                count += CountIn(permanent.SourceCardIds, card);
                count += CountIn(permanent.LinkedCards, card);
            }
        }

        return count;
    }

    private static int CountIn(IEnumerable<CardInstanceId> cards, CardInstanceId card) =>
        cards.Count(candidate => candidate == card);
}
