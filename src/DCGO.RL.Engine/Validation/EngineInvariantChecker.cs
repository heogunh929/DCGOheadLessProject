using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed class EngineInvariantChecker
{
    public EngineInvariantReport Check(GameState state)
    {
        var violations = new List<EngineInvariantViolation>();
        var memberships = CollectMemberships(state, violations);
        CheckCardMemberships(state, memberships, violations);
        CheckPermanents(state, memberships, violations);
        CheckTemporaryModifiers(state, violations);
        return new EngineInvariantReport(violations);
    }

    public void ThrowIfInvalid(GameState state) => Check(state).ThrowIfInvalid();

    private static List<CardMembership> CollectMemberships(GameState state, List<EngineInvariantViolation> violations)
    {
        var memberships = new List<CardMembership>();

        foreach (var player in state.Players)
        {
            AddPlayerZoneMemberships(state, player, Zone.Deck, player.Deck, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.DigiEggDeck, player.DigiEggDeck, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Hand, player.Hand, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Trash, player.Trash, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Lost, player.Lost, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Security, player.Security, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Executing, player.Executing, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.Revealed, player.Revealed, memberships, violations);
            AddPlayerZoneMemberships(state, player, Zone.OutsideGame, player.OutsideGame, memberships, violations);

            foreach (var permanent in player.FieldPermanents)
            {
                AddPermanentMembership(state, permanent.TopCardId, permanent, permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea, memberships, violations);

                foreach (var source in permanent.SourceCardIds)
                {
                    AddPermanentMembership(state, source, permanent, Zone.EvolutionSources, memberships, violations);
                }

                foreach (var linked in permanent.LinkedCards)
                {
                    AddPermanentMembership(state, linked, permanent, Zone.LinkedCards, memberships, violations);
                }
            }
        }

        return memberships;
    }

    private static void AddPlayerZoneMemberships(
        GameState state,
        PlayerState player,
        Zone zone,
        IEnumerable<CardInstanceId> cards,
        List<CardMembership> memberships,
        List<EngineInvariantViolation> violations)
    {
        foreach (var card in cards)
        {
            if (!state.Cards.TryGetValue(card, out var instance))
            {
                violations.Add(new EngineInvariantViolation(
                    "card-missing",
                    $"Card '{card}' exists in player '{player.Id}' zone '{zone}' but not in GameState.Cards.",
                    card));
                continue;
            }

            if (instance.Owner != player.Id)
            {
                violations.Add(new EngineInvariantViolation(
                    "owner-mismatch",
                    $"Card '{card}' owner is '{instance.Owner}' but it is stored in player '{player.Id}' zone '{zone}'.",
                    card));
            }

            memberships.Add(new CardMembership(card, zone, player.Id, null));
        }
    }

    private static void AddPermanentMembership(
        GameState state,
        CardInstanceId card,
        PermanentState permanent,
        Zone zone,
        List<CardMembership> memberships,
        List<EngineInvariantViolation> violations)
    {
        if (!state.Cards.ContainsKey(card))
        {
            violations.Add(new EngineInvariantViolation(
                "card-missing",
                $"Card '{card}' exists in permanent '{permanent.Id}' zone '{zone}' but not in GameState.Cards.",
                card,
                permanent.Id));
            return;
        }

        memberships.Add(new CardMembership(card, zone, permanent.ControllerPlayerId, permanent.Id));
    }

    private static void CheckCardMemberships(
        GameState state,
        IReadOnlyList<CardMembership> memberships,
        List<EngineInvariantViolation> violations)
    {
        foreach (var cardPair in state.Cards.OrderBy(pair => pair.Key.Value))
        {
            var cardMemberships = memberships.Where(membership => membership.Card == cardPair.Key).ToArray();
            if (cardMemberships.Length != 1)
            {
                violations.Add(new EngineInvariantViolation(
                    "card-membership-count",
                    $"Card '{cardPair.Key}' must exist in exactly one zone, actual memberships: {cardMemberships.Length}.",
                    cardPair.Key));
                continue;
            }

            var membership = cardMemberships[0];
            if (cardPair.Value.CurrentZone != membership.Zone)
            {
                violations.Add(new EngineInvariantViolation(
                    "current-zone-mismatch",
                    $"Card '{cardPair.Key}' CurrentZone is '{cardPair.Value.CurrentZone}', actual zone is '{membership.Zone}'.",
                    cardPair.Key,
                    membership.Permanent));
            }

            if (membership.Permanent is null)
            {
                if (cardPair.Value.PermanentId is not null)
                {
                    violations.Add(new EngineInvariantViolation(
                        "permanent-link-mismatch",
                        $"Card '{cardPair.Key}' is in public zone '{membership.Zone}' but PermanentId is '{cardPair.Value.PermanentId}'.",
                        cardPair.Key));
                }
            }
            else if (cardPair.Value.PermanentId != membership.Permanent.Value)
            {
                violations.Add(new EngineInvariantViolation(
                    "permanent-link-mismatch",
                    $"Card '{cardPair.Key}' PermanentId is '{cardPair.Value.PermanentId}', actual permanent is '{membership.Permanent}'.",
                    cardPair.Key,
                    membership.Permanent));
            }
        }

        foreach (var duplicateGroup in memberships.GroupBy(membership => membership.Card).Where(group => group.Count() > 1))
        {
            violations.Add(new EngineInvariantViolation(
                "duplicate-zone-membership",
                $"Card '{duplicateGroup.Key}' exists in multiple zone containers: {string.Join(",", duplicateGroup.Select(item => item.Zone))}.",
                duplicateGroup.Key));
        }
    }

    private static void CheckPermanents(
        GameState state,
        IReadOnlyList<CardMembership> memberships,
        List<EngineInvariantViolation> violations)
    {
        foreach (var permanent in state.Players.SelectMany(player => player.FieldPermanents))
        {
            var stackCards = new[] { permanent.TopCardId }
                .Concat(permanent.SourceCardIds)
                .Concat(permanent.LinkedCards)
                .ToArray();

            foreach (var duplicate in stackCards.GroupBy(card => card).Where(group => group.Count() > 1))
            {
                violations.Add(new EngineInvariantViolation(
                    "permanent-stack-duplicate",
                    $"Permanent '{permanent.Id}' contains card '{duplicate.Key}' more than once across top/source/link.",
                    duplicate.Key,
                    permanent.Id));
            }

            if (!state.Cards.TryGetValue(permanent.TopCardId, out var topCard))
            {
                continue;
            }

            var expectedTopZone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
            var topMembership = memberships.FirstOrDefault(membership => membership.Card == permanent.TopCardId && membership.Permanent == permanent.Id);
            if (topMembership is not null && topMembership.Zone != expectedTopZone)
            {
                violations.Add(new EngineInvariantViolation(
                    "permanent-top-zone-mismatch",
                    $"Permanent '{permanent.Id}' top card '{permanent.TopCardId}' actual zone is '{topMembership.Zone}', expected '{expectedTopZone}'.",
                    permanent.TopCardId,
                    permanent.Id));
            }

            if (topCard.Owner != permanent.OwnerPlayerId)
            {
                violations.Add(new EngineInvariantViolation(
                    "permanent-owner-mismatch",
                    $"Permanent '{permanent.Id}' owner is '{permanent.OwnerPlayerId}' but top card owner is '{topCard.Owner}'.",
                    permanent.TopCardId,
                    permanent.Id));
            }
        }
    }

    private static void CheckTemporaryModifiers(GameState state, List<EngineInvariantViolation> violations)
    {
        foreach (var duplicateGroup in state.TemporaryModifiers
            .GroupBy(modifier => modifier.StableId, StringComparer.Ordinal)
            .Where(group => group.Count() > 1))
        {
            violations.Add(new EngineInvariantViolation(
                "temporary-modifier-duplicate",
                $"Temporary modifier stable id '{duplicateGroup.Key}' appears more than once."));
        }

        foreach (var modifier in state.TemporaryModifiers)
        {
            if (string.IsNullOrWhiteSpace(modifier.StableId))
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-id-missing",
                    "Temporary modifier stable id must not be empty."));
            }

            var hasPermanentTarget = modifier.TargetPermanentId is not null;
            var hasPlayerTarget = modifier.TargetPlayerId is not null;
            if (hasPermanentTarget == hasPlayerTarget)
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-target-invalid",
                    $"Temporary modifier '{modifier.StableId}' must target exactly one permanent or player.",
                    Permanent: modifier.TargetPermanentId));
                continue;
            }

            if (modifier.SourceCardId is not null && !state.Cards.ContainsKey(modifier.SourceCardId.Value))
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-source-card-missing",
                    $"Temporary modifier '{modifier.StableId}' source card '{modifier.SourceCardId}' does not exist.",
                    modifier.SourceCardId.Value,
                    modifier.TargetPermanentId));
            }

            if (hasPermanentTarget && FindPermanent(state, modifier.TargetPermanentId!.Value) is null)
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-target-stale",
                    $"Temporary modifier '{modifier.StableId}' target permanent '{modifier.TargetPermanentId}' does not exist.",
                    Permanent: modifier.TargetPermanentId));
            }

            if (hasPlayerTarget && state.Players.All(player => player.Id != modifier.TargetPlayerId!.Value))
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-player-missing",
                    $"Temporary modifier '{modifier.StableId}' target player '{modifier.TargetPlayerId}' does not exist."));
            }

            if (modifier.ModifierKind == TemporaryModifierKind.SecurityDigimonDP && !hasPlayerTarget)
            {
                violations.Add(new EngineInvariantViolation(
                    "temporary-modifier-target-invalid",
                    $"Security Digimon DP modifier '{modifier.StableId}' must target a player.",
                    Permanent: modifier.TargetPermanentId));
            }
        }
    }

    private static PermanentState? FindPermanent(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId);

    private sealed record CardMembership(CardInstanceId Card, Zone Zone, PlayerId Owner, PermanentId? Permanent);
}
