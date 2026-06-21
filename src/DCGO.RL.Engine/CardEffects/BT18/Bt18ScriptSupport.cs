using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class Bt18BlockedCardScript : ICardScript
{
    protected Bt18BlockedCardScript(
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

internal static class Bt18ScriptSupport
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
            throw new DomainException("BT18 card script requires a source card owner.");
        }

        return instance.Owner;
    }

    public static bool IsTopSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        return state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Any(permanent =>
                sourcePermanent is not null
                && permanent.Id == sourcePermanent.Value
                && permanent.TopCardId == sourceCard.Value
                && !permanent.IsBreedingArea);
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaPermanentCandidates(
        GameState state,
        PlayerId controller,
        Func<CardDefinition, bool> definitionPredicate)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => definitionPredicate(BattleRules.Definition(state, permanent.TopCardId)))
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

    public static int DistinctTrashColorCount(GameState state, PlayerId player)
    {
        return state.GetPlayer(player)
            .Trash
            .SelectMany(card => BattleRules.Definition(state, card).CardColors)
            .Where(color => color != CardColor.None)
            .Distinct()
            .Count();
    }

    public static bool HasAnyColor(CardDefinition definition, params CardColor[] colors) =>
        definition.CardColors.Any(color => colors.Contains(color));
}
