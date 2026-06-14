using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal static class StarterScriptSupport
{
    public static PlayerId Opponent(GameState state, PlayerId player)
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

    public static PlayerId? TrySourceCardOwner(GameState state, CardInstanceId? sourceCard)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return null;
        }

        return instance.Owner;
    }

    public static PlayerId RequireSourceCardOwner(GameState state, CardInstanceId? sourceCard) =>
        TrySourceCardOwner(state, sourceCard)
        ?? throw new DomainException("Card script resolution requires a source card owner.");

    public static bool IsTopSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        var permanent = FindPermanent(state, sourcePermanent, sourceCard.Value);
        return permanent is { IsBreedingArea: false } && permanent.TopCardId == sourceCard.Value;
    }

    public static bool HasOpponentBattleAreaDigimonWithoutSources(GameState state, PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Any(permanent =>
                permanent.ControllerPlayerId == opponent
                && BattleRules.IsDigimon(state, permanent.TopCardId)
                && permanent.SourceCardIds.Count == 0);
    }

    public static bool IsOpponentBattleAreaDigimonWithoutSources(
        GameState state,
        PlayerId controller,
        PermanentId permanentId)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Any(permanent =>
                permanent.Id == permanentId
                && permanent.ControllerPlayerId == opponent
                && BattleRules.IsDigimon(state, permanent.TopCardId)
                && permanent.SourceCardIds.Count == 0);
    }

    public static bool IsSourcePermanentAttacker(
        EffectContext context,
        PermanentId? sourcePermanent) =>
        sourcePermanent is not null
        && context.GetValueOrDefault("Attacker") is PermanentId attacker
        && attacker == sourcePermanent.Value;

    public static bool IsInheritedSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        var permanent = FindPermanent(state, sourcePermanent, sourceCard.Value);
        return permanent is { IsBreedingArea: false } && permanent.SourceCardIds.Contains(sourceCard.Value);
    }

    public static bool HasPlayableEvolutionSource(
        GameState state,
        PlayerId controller,
        PermanentId permanentId)
    {
        var permanent = state.GetPlayer(controller)
            .BattleAreaPermanents
            .FirstOrDefault(candidate =>
                candidate.Id == permanentId
                && candidate.ControllerPlayerId == controller
                && BattleRules.IsDigimon(state, candidate.TopCardId));
        return permanent is not null
            && state.GetPlayer(controller).BattleAreaPermanents.Count() < state.Config.FieldSlotCount
            && permanent.SourceCardIds.Any(source => IsPlayableEvolutionSource(state, controller, permanent.Id, source));
    }

    public static bool IsPlayableEvolutionSource(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent,
        CardInstanceId sourceCard)
    {
        var player = state.GetPlayer(controller);
        var permanent = player.BattleAreaPermanents.FirstOrDefault(candidate =>
            candidate.Id == sourcePermanent
            && candidate.ControllerPlayerId == controller
            && BattleRules.IsDigimon(state, candidate.TopCardId));
        if (permanent is null || !permanent.SourceCardIds.Contains(sourceCard))
        {
            return false;
        }

        if (player.BattleAreaPermanents.Count() >= state.Config.FieldSlotCount)
        {
            return false;
        }

        if (!state.Cards.TryGetValue(sourceCard, out var instance))
        {
            return false;
        }

        if (instance.Owner != controller
            || instance.CurrentZone != Zone.EvolutionSources
            || instance.PermanentId != sourcePermanent)
        {
            return false;
        }

        var definition = BattleRules.Definition(state, sourceCard);
        return definition.CardKinds.Contains(CardKind.Digimon)
            && definition.IsPermanent
            && !definition.CardKinds.Contains(CardKind.Option);
    }

    public static bool CanPlaySelfPermanentFromExecuting(
        GameState state,
        CardInstanceId? sourceCard,
        PlayerId controller)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return false;
        }

        if (instance.Owner != controller || instance.CurrentZone != Zone.Executing)
        {
            return false;
        }

        var definition = BattleRules.Definition(state, sourceCard.Value);
        if (!definition.IsPermanent || definition.CardKinds.Contains(CardKind.Option))
        {
            return false;
        }

        return state.GetPlayer(controller).BattleAreaPermanents.Count() < state.Config.FieldSlotCount;
    }

    public static int FirstEmptyBattleFrameForPlay(GameState state, PlayerId controller) =>
        BattleRules.FirstEmptyBattleFrame(state.GetPlayer(controller), state.Config.FieldSlotCount);

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonWithSourcesCandidates(
        GameState state,
        PlayerId controller,
        int? maxLevel = null)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => permanent.SourceCardIds.Count > 0)
            .Where(permanent => maxLevel is null || BattleRules.Definition(state, permanent.TopCardId).Level <= maxLevel.Value)
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonWithoutSourcesCandidates(
        GameState state,
        PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => permanent.SourceCardIds.Count == 0)
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
        GameState state,
        PlayerId controller) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => HasPlayableEvolutionSource(state, controller, permanent.Id))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();

    public static IReadOnlyList<SelectableTarget> PlayableEvolutionSourceCardCandidates(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent)
    {
        var permanent = BattleRules.Permanent(state, sourcePermanent);
        return permanent.SourceCardIds
            .Where(source => IsPlayableEvolutionSource(state, controller, sourcePermanent, source))
            .Select(source => new SelectableTarget(
                SelectionTargetKind.Card,
                $"card:{source.Value}",
                controller,
                Card: source,
                Permanent: sourcePermanent,
                Label: BattleRules.Definition(state, source).CardId,
                Zone: Zone.EvolutionSources))
            .ToArray();
    }

    public static SelectionRequest CreateOpponentBattleAreaDigimonSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OpponentBattleAreaDigimonCandidates(state, controller);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    public static SelectionRequest CreateOpponentBattleAreaDigimonWithSourcesSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt,
        int? maxLevel = null)
    {
        var candidates = OpponentBattleAreaDigimonWithSourcesCandidates(state, controller, maxLevel);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    public static SelectionRequest CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OpponentBattleAreaDigimonWithoutSourcesCandidates(state, controller);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    public static SelectionRequest CreateOwnerBattleAreaDigimonSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OwnerBattleAreaDigimonCandidates(state, controller);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    public static SelectionRequest CreateOwnerBattleAreaDigimonWithPlayableEvolutionSourceSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(state, controller);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    public static SelectionRequest CreatePlayableEvolutionSourceCardSelectionRequest(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent,
        string requestId,
        string prompt)
    {
        var candidates = PlayableEvolutionSourceCardCandidates(state, controller, sourcePermanent);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    private static PermanentState? FindPermanent(
        GameState state,
        PermanentId? sourcePermanent,
        CardInstanceId sourceCard)
    {
        var permanents = state.Players.SelectMany(player => player.FieldPermanents);
        if (sourcePermanent is not null)
        {
            return permanents.FirstOrDefault(permanent =>
                permanent.Id == sourcePermanent.Value && PermanentContainsCard(permanent, sourceCard));
        }

        return permanents.FirstOrDefault(permanent => PermanentContainsCard(permanent, sourceCard));
    }

    private static bool PermanentContainsCard(PermanentState permanent, CardInstanceId card) =>
        permanent.TopCardId == card
        || permanent.SourceCardIds.Contains(card)
        || permanent.LinkedCards.Contains(card);
}
