using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class Bt19BlockedCardScript : ICardScript
{
    protected Bt19BlockedCardScript(
        string cardId,
        string effectClassName,
        CardEffectPortingStatus status,
        string notes)
    {
        Porting = new CardEffectPortingRecord(cardId, effectClassName, status, notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public void Resolve(CardScriptExecutionContext context) =>
        throw new UnsupportedMechanicException($"{Porting.CardId} is blocked: {Porting.Notes}");
}

internal static class Bt19ScriptSupport
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

    public static PlayerId RequireSourceCardOwner(GameState state, CardInstanceId? sourceCard)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            throw new DomainException("BT19 card script requires a source card owner.");
        }

        return instance.Owner;
    }

    public static bool IsOwnedCardInZone(
        GameState state,
        CardInstanceId? sourceCard,
        PlayerId owner,
        Zone zone)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return false;
        }

        return instance.Owner == owner && instance.CurrentZone.Equals(zone);
    }

    public static bool IsTopSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null || sourcePermanent is null)
        {
            return false;
        }

        return state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Any(permanent =>
                permanent.Id == sourcePermanent.Value
                && permanent.TopCardId == sourceCard.Value
                && !permanent.IsBreedingArea);
    }

    public static bool IsInheritedSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null || sourcePermanent is null)
        {
            return false;
        }

        return state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Any(permanent =>
                permanent.Id == sourcePermanent.Value
                && permanent.SourceCardIds.Contains(sourceCard.Value)
                && !permanent.IsBreedingArea);
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller,
        Func<PermanentState, CardDefinition, bool> predicate)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => predicate(permanent, BattleRules.Definition(state, permanent.TopCardId)))
            .Select(permanent =>
            {
                var definition = BattleRules.Definition(state, permanent.TopCardId);
                return new SelectableTarget(
                    SelectionTargetKind.Permanent,
                    $"permanent:{permanent.Id.Value}",
                    permanent.ControllerPlayerId,
                    Permanent: permanent.Id,
                    Label: definition.CardId,
                    Zone: Zone.BattleArea);
            })
            .ToArray();
    }

    public static SelectionRequest CreatePermanentSelectionRequest(
        string requestId,
        PlayerId controller,
        IReadOnlyList<SelectableTarget> candidates,
        string prompt) =>
        new(
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

    public static bool IsOpponentDeletion(EffectContext context, PlayerId controller)
    {
        if (context.GetValueOrDefault("DestroyedController") is not PlayerId destroyedController)
        {
            return false;
        }

        return destroyedController != controller;
    }

    public static bool IsSelfDeletion(EffectContext context, PermanentId? sourcePermanent)
    {
        if (sourcePermanent is null)
        {
            return false;
        }

        return context.GetValueOrDefault("DestroyedPermanent") is PermanentId destroyed
            && destroyed == sourcePermanent.Value;
    }

    public static bool IsDeletedOtherThanBattle(EffectContext context)
    {
        if (context.GetValueOrDefault("DestroyedByBattle") is bool byBattle)
        {
            return !byBattle;
        }

        return true;
    }

    public static IReadOnlyList<CardInstanceId> OwnerTrashCandidates(
        GameState state,
        PlayerId owner,
        Func<CardDefinition, bool> predicate) =>
        state.GetPlayer(owner)
            .Trash
            .Where(card => predicate(BattleRules.Definition(state, card)))
            .ToArray();

    public static SelectionRequest CreateTrashCardSelectionRequest(
        GameState state,
        string requestId,
        PlayerId controller,
        IReadOnlyList<CardInstanceId> candidates,
        string prompt)
    {
        var targets = candidates
            .Select(card =>
            {
                var definition = BattleRules.Definition(state, card);
                return new SelectableTarget(
                    SelectionTargetKind.Card,
                    $"card:{card.Value}",
                    state.Cards[card].Owner,
                    Card: card,
                    Label: definition.CardId,
                    Zone: Zone.Trash);
            })
            .ToArray();

        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            minCount: 1,
            maxCount: Math.Min(1, targets.Length),
            canSkip: false,
            canEndNotMax: false,
            targets,
            prompt);
    }
}
