using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class Bt21BlockedCardScript : ICardScript
{
    protected Bt21BlockedCardScript(
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

internal static class Bt21ScriptSupport
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
            throw new DomainException("BT21 card script requires a source card owner.");
        }

        return instance.Owner;
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

        if (instance.Owner != owner || instance.CurrentZone != zone)
        {
            return false;
        }

        var player = state.GetPlayer(owner);
        return zone switch
        {
            Zone.Deck => player.Deck.Contains(sourceCard.Value),
            Zone.DigiEggDeck => player.DigiEggDeck.Contains(sourceCard.Value),
            Zone.Hand => player.Hand.Contains(sourceCard.Value),
            Zone.Security => player.Security.Contains(sourceCard.Value),
            Zone.Trash => player.Trash.Contains(sourceCard.Value),
            Zone.Lost => player.Lost.Contains(sourceCard.Value),
            Zone.Executing => player.Executing.Contains(sourceCard.Value),
            Zone.Revealed => player.Revealed.Contains(sourceCard.Value),
            Zone.OutsideGame => player.OutsideGame.Contains(sourceCard.Value),
            _ => true,
        };
    }
}
