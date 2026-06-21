using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record RuleVisibleSnapshot(
    string Phase,
    int TurnPlayer,
    int TurnCount,
    int Memory,
    IReadOnlyList<RuleVisiblePlayerSnapshot> Players,
    IReadOnlyList<RuleVisibleTemporaryModifierSnapshot> TemporaryModifiers,
    RuleVisibleGameResultSnapshot GameResult,
    string CanonicalStateHash)
{
    public static RuleVisibleSnapshot Capture(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        return new RuleVisibleSnapshot(
            state.Phase.ToString(),
            state.TurnPlayerId.Value,
            state.TurnCount,
            state.Memory,
            state.Players
                .OrderBy(player => player.Id.Value)
                .Select(player => RuleVisiblePlayerSnapshot.Capture(state, player))
                .ToArray(),
            state.TemporaryModifiers
                .OrderBy(modifier => modifier.StableId, StringComparer.Ordinal)
                .ThenBy(modifier => modifier.SourceCardId?.Value ?? -1)
                .ThenBy(modifier => modifier.TargetPermanentId?.Value ?? -1)
                .Select(RuleVisibleTemporaryModifierSnapshot.Capture)
                .ToArray(),
            new RuleVisibleGameResultSnapshot(
                state.Result.Kind.ToString(),
                state.Result.Winner?.Value,
                state.Result.Reason),
            state.ComputeStateHash());
    }
}

public sealed record RuleVisiblePlayerSnapshot(
    int Player,
    IReadOnlyList<RuleVisibleZoneCount> ZoneCounts,
    IReadOnlyList<RuleVisibleZoneCards> PublicZones,
    IReadOnlyList<RuleVisiblePermanentSnapshot> Permanents)
{
    public static RuleVisiblePlayerSnapshot Capture(GameState state, PlayerState player) =>
        new(
            player.Id.Value,
            ZoneCountsFor(player).ToArray(),
            PublicZonesFor(state, player).ToArray(),
            player.FieldPermanents
                .OrderBy(permanent => permanent.IsBreedingArea ? 1 : 0)
                .ThenBy(permanent => permanent.FrameIndex)
                .ThenBy(permanent => permanent.Id.Value)
                .Select(permanent => RuleVisiblePermanentSnapshot.Capture(state, permanent))
                .ToArray());

    private static IEnumerable<RuleVisibleZoneCount> ZoneCountsFor(PlayerState player)
    {
        yield return new RuleVisibleZoneCount(Zone.Deck.ToString(), player.Deck.Count);
        yield return new RuleVisibleZoneCount(Zone.DigiEggDeck.ToString(), player.DigiEggDeck.Count);
        yield return new RuleVisibleZoneCount(Zone.Hand.ToString(), player.Hand.Count);
        yield return new RuleVisibleZoneCount(Zone.Security.ToString(), player.Security.Count);
        yield return new RuleVisibleZoneCount(Zone.Trash.ToString(), player.Trash.Count);
        yield return new RuleVisibleZoneCount(Zone.Lost.ToString(), player.Lost.Count);
        yield return new RuleVisibleZoneCount(Zone.Executing.ToString(), player.Executing.Count);
        yield return new RuleVisibleZoneCount(Zone.Revealed.ToString(), player.Revealed.Count);
        yield return new RuleVisibleZoneCount(Zone.OutsideGame.ToString(), player.OutsideGame.Count);
        yield return new RuleVisibleZoneCount(Zone.BattleArea.ToString(), player.BattleAreaPermanents.Count());
        yield return new RuleVisibleZoneCount(Zone.BreedingArea.ToString(), player.BreedingAreaPermanent is null ? 0 : 1);
        yield return new RuleVisibleZoneCount(Zone.EvolutionSources.ToString(), player.FieldPermanents.Sum(permanent => permanent.SourceCardIds.Count));
        yield return new RuleVisibleZoneCount(Zone.LinkedCards.ToString(), player.FieldPermanents.Sum(permanent => permanent.LinkedCards.Count));
    }

    private static IEnumerable<RuleVisibleZoneCards> PublicZonesFor(GameState state, PlayerState player)
    {
        yield return CardsInZone(state, Zone.Trash, player.Trash);
        yield return CardsInZone(state, Zone.Lost, player.Lost);
        yield return CardsInZone(state, Zone.Executing, player.Executing);
        yield return CardsInZone(state, Zone.Revealed, player.Revealed);
        yield return CardsInZone(state, Zone.OutsideGame, player.OutsideGame);
        yield return CardsInZone(
            state,
            Zone.Security,
            player.Security.Where(card => state.Cards[card].IsFaceUp));
    }

    private static RuleVisibleZoneCards CardsInZone(
        GameState state,
        Zone zone,
        IEnumerable<CardInstanceId> cards) =>
        new(
            zone.ToString(),
            cards.Select(card => RuleVisibleCardIdentity.Capture(state, card)).ToArray());
}

public sealed record RuleVisibleZoneCount(string Zone, int Count);

public sealed record RuleVisibleZoneCards(string Zone, IReadOnlyList<RuleVisibleCardIdentity> Cards);

public sealed record RuleVisibleCardIdentity(
    int InstanceId,
    string DefinitionId,
    int Owner,
    string Zone,
    bool IsFaceUp,
    int? PermanentId)
{
    public static RuleVisibleCardIdentity Capture(GameState state, CardInstanceId cardId)
    {
        var card = state.Cards[cardId];
        return new RuleVisibleCardIdentity(
            card.Id.Value,
            card.DefinitionId,
            card.Owner.Value,
            card.CurrentZone.ToString(),
            card.IsFaceUp,
            card.PermanentId?.Value);
    }
}

public sealed record RuleVisiblePermanentSnapshot(
    int PermanentId,
    int Owner,
    int Controller,
    string Zone,
    int FrameIndex,
    bool Suspended,
    RuleVisibleCardIdentity Top,
    IReadOnlyList<RuleVisibleCardIdentity> Sources,
    IReadOnlyList<RuleVisibleCardIdentity> Links,
    IReadOnlyList<string> BattleKeywords)
{
    public static RuleVisiblePermanentSnapshot Capture(GameState state, PermanentState permanent) =>
        new(
            permanent.Id.Value,
            permanent.OwnerPlayerId.Value,
            permanent.ControllerPlayerId.Value,
            permanent.IsBreedingArea
                ? Domain.Zone.BreedingArea.ToString()
                : Domain.Zone.BattleArea.ToString(),
            permanent.FrameIndex,
            permanent.IsSuspended,
            RuleVisibleCardIdentity.Capture(state, permanent.TopCardId),
            permanent.SourceCardIds
                .Select(card => RuleVisibleCardIdentity.Capture(state, card))
                .ToArray(),
            permanent.LinkedCards
                .Select(card => RuleVisibleCardIdentity.Capture(state, card))
                .ToArray(),
            permanent.BattleKeywords.Select(keyword => keyword.ToString()).Order(StringComparer.Ordinal).ToArray());
}

public sealed record RuleVisibleTemporaryModifierSnapshot(
    string StableId,
    int? SourceCard,
    int? SourcePermanent,
    int Controller,
    int? TargetPermanent,
    int? TargetPlayer,
    string Kind,
    int Amount,
    string DurationScope,
    int CreatedTurnCount,
    string CreatedPhase,
    int? ExpiresAtTurnPlayer,
    string DebugLabel)
{
    public static RuleVisibleTemporaryModifierSnapshot Capture(TemporaryModifier modifier) =>
        new(
            modifier.StableId,
            modifier.SourceCardId?.Value,
            modifier.SourcePermanentId?.Value,
            modifier.ControllerPlayerId.Value,
            modifier.TargetPermanentId?.Value,
            modifier.TargetPlayerId?.Value,
            modifier.ModifierKind.ToString(),
            modifier.Amount,
            modifier.DurationScope.ToString(),
            modifier.CreatedTurnCount,
            modifier.CreatedPhase.ToString(),
            modifier.ExpiresAtTurnPlayerId?.Value,
            modifier.DebugLabel);
}

public sealed record RuleVisibleGameResultSnapshot(
    string Kind,
    int? Winner,
    string Reason);
