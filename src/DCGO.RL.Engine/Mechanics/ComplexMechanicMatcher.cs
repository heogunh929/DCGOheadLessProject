using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Mechanics;

public sealed class ComplexMechanicMatcher
{
    public bool MatchesCard(GameState state, CardInstanceId card, MaterialRequirement? requirement)
    {
        if (requirement is null)
        {
            return true;
        }

        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Card '{card}' does not exist.");
        }

        var definition = Battle.BattleRules.Definition(state, card);
        if (requirement.DefinitionIds.Count > 0 && !requirement.DefinitionIds.Contains(definition.CardId, StringComparer.Ordinal))
        {
            return false;
        }

        if (requirement.CardKinds.Count > 0 && !requirement.CardKinds.Any(definition.CardKinds.Contains))
        {
            return false;
        }

        if (requirement.CardColors.Count > 0 && !requirement.CardColors.Any(definition.CardColors.Contains))
        {
            return false;
        }

        if (requirement.Levels.Count > 0 && !requirement.Levels.Contains(definition.Level))
        {
            return false;
        }

        if (requirement.Zones.Count > 0 && !requirement.Zones.Contains(instance.CurrentZone))
        {
            return false;
        }

        return true;
    }

    public bool MatchesPermanent(GameState state, PermanentState permanent, MaterialRequirement? requirement)
    {
        if (requirement is null)
        {
            return true;
        }

        var zone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
        if (requirement.Zones.Count > 0 && !requirement.Zones.Contains(zone))
        {
            return false;
        }

        return MatchesCard(state, permanent.TopCardId, requirement);
    }

    public IReadOnlyList<MaterialCandidate> FindMaterialCandidates(
        GameState state,
        PlayerId playerId,
        MaterialRequirement requirement)
    {
        var player = state.GetPlayer(playerId);
        var candidates = new List<MaterialCandidate>();

        if (requirement.AllowHand || requirement.Zones.Contains(Zone.Hand))
        {
            candidates.AddRange(player.Hand
                .Where(card => MatchesCard(state, card, requirement))
                .Select(card => ToCandidate(state, card, Zone.Hand, null)));
        }

        if (requirement.AllowTrash || requirement.Zones.Contains(Zone.Trash))
        {
            candidates.AddRange(player.Trash
                .Where(card => MatchesCard(state, card, requirement))
                .Select(card => ToCandidate(state, card, Zone.Trash, null)));
        }

        if (requirement.AllowBattleArea || requirement.Zones.Contains(Zone.BattleArea))
        {
            candidates.AddRange(player.BattleAreaPermanents
                .Where(permanent => MatchesPermanent(state, permanent, requirement))
                .Select(permanent => ToCandidate(state, permanent.TopCardId, Zone.BattleArea, permanent.Id)));
        }

        if (requirement.AllowEvolutionSources || requirement.Zones.Contains(Zone.EvolutionSources))
        {
            foreach (var permanent in player.FieldPermanents)
            {
                candidates.AddRange(permanent.SourceCardIds
                    .Where(card => MatchesCard(state, card, requirement))
                    .Select(card => ToCandidate(state, card, Zone.EvolutionSources, permanent.Id)));
            }
        }

        if (requirement.AllowLinkedCards || requirement.Zones.Contains(Zone.LinkedCards))
        {
            foreach (var permanent in player.FieldPermanents)
            {
                candidates.AddRange(permanent.LinkedCards
                    .Where(card => MatchesCard(state, card, requirement))
                    .Select(card => ToCandidate(state, card, Zone.LinkedCards, permanent.Id)));
            }
        }

        return candidates;
    }

    public MaterialCandidate ToCandidate(GameState state, CardInstanceId card, Zone zone, PermanentId? permanent)
    {
        var instance = state.Cards[card];
        var definition = Battle.BattleRules.Definition(state, card);
        return new MaterialCandidate(card, instance.Owner, zone, permanent, definition.CardId);
    }
}
