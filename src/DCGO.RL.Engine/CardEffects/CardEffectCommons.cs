using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public static partial class CardEffectCommons
{
    public static bool IsExistOnField(CardSource? card) =>
        card?.PermanentOfThisCard() is not null;

    public static bool IsExistOnBreedingArea(CardSource? card) =>
        card?.PermanentOfThisCard() is { IsBreedingArea: true };

    public static bool IsExistOnBreedingAreaDigimon(CardSource? card) =>
        card?.PermanentOfThisCard() is { IsBreedingArea: true, IsDigimon: true };

    public static bool IsExistOnBattleArea(CardSource? card) =>
        card?.PermanentOfThisCard() is { IsBreedingArea: false };

    public static bool IsExistOnBattleAreaDigimon(CardSource? card) =>
        card?.PermanentOfThisCard() is { IsBreedingArea: false, IsDigimon: true };

    public static bool IsExistOnHand(CardSource? card) =>
        card is not null && card.CurrentZone is Zone.Hand && card.Owner.HandCards.Any(candidate => candidate.Id == card.Id);

    public static bool IsExistOnTrash(CardSource? card) =>
        card is not null && card.CurrentZone is Zone.Trash && card.Owner.TrashCards.Any(candidate => candidate.Id == card.Id);

    public static bool IsExistOnSecurity(CardSource? card) =>
        card is not null && card.CurrentZone is Zone.Security && card.Owner.SecurityCards.Any(candidate => candidate.Id == card.Id);

    public static bool IsExistInAnyTrash(CardSource? card) =>
        card is not null
        && card.CurrentZone is Zone.Trash
        && (card.Owner.TrashCards.Any(candidate => candidate.Id == card.Id)
            || card.Owner.Enemy.TrashCards.Any(candidate => candidate.Id == card.Id));

    public static bool IsExistInSecurity(CardSource? card, bool isFlipped = false) =>
        card is not null
        && card.CurrentZone is Zone.Security
        && card.IsFlipped == isFlipped
        && card.Owner.SecurityCards.Any(candidate => candidate.Id == card.Id);

    public static bool IsExistDigivolutionCards(CardSource? card) =>
        card?.PermanentOfThisCard()?.DigivolutionCards.Any(candidate => candidate.Id == card.Id) == true;

    public static bool IsExistLinked(CardSource? card) =>
        card?.PermanentOfThisCard()?.LinkedCards.Any(candidate => candidate.Id == card.Id) == true;

    public static bool IsExistOnFieldTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistOnField(card));

    public static bool IsExistOnBreedingAreaTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistOnBreedingArea(card));

    public static bool IsExistOnBattleAreaTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistOnBattleArea(card));

    public static bool IsExistOnBattleAreaDigimonTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistOnBattleAreaDigimon(card));

    public static bool IsExistDigivolutionCardsTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistDigivolutionCards(card));

    public static bool IsExistLinkedTrigger(CardSource? card, ICardEffect? cardEffect) =>
        CaptureCurrentPermanent(card, cardEffect, IsExistLinked(card));

    public static bool IsExistOnFieldActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistOnField(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool IsExistOnBreedingAreaActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistOnBreedingArea(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool IsExistOnBattleAreaActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistOnBattleArea(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool IsExistOnBattleAreaDigimonActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistOnBattleAreaDigimon(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool IsExistDigivolutionCardsActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistDigivolutionCards(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool IsExistLinkedActivate(CardSource? card, ICardEffect? cardEffect) =>
        IsExistLinked(card) && IsCapturedPermanentCurrent(card, cardEffect);

    public static bool CanPlayAsNewPermanent(
        CardSource? cardSource,
        bool payCost,
        ICardEffect? cardEffect,
        SelectCardEffect.Root root = SelectCardEffect.Root.Hand,
        bool isBreedingArea = false,
        bool isPlayOption = false,
        int fixedCost = -1,
        StaticEffectService? staticEffects = null)
    {
        if (cardSource is null)
        {
            return false;
        }

        var definition = cardSource.Definition;
        var isOption = definition.CardKinds.Contains(CardKind.Option);
        if ((!isPlayOption && isOption) || (!isOption && !definition.IsPermanent))
        {
            return false;
        }

        if (!IsCardInRoot(cardSource, root)
            || IsSameTopPermanentWithoutSources(cardSource)
            || !HasAvailablePlayFrame(cardSource, isBreedingArea)
            || !HasPlayableCost(cardSource, payCost, fixedCost, staticEffects))
        {
            return false;
        }

        var effectSourceCard = cardEffect?.EffectSourceCard?.Id;
        var effectSourcePermanent = cardEffect?.EffectSourcePermanent?.Id;
        var restrictionCause = new StaticCardRestrictionCause(
            effectSourceCard,
            effectSourcePermanent,
            cardSource.Owner.Id,
            MoveReason.Effect);

        if (isOption)
        {
            return staticEffects?.HasCardRestriction(
                    cardSource.State,
                    cardSource.Id,
                    StaticCardRestrictionKind.CannotPlay,
                    restrictionCause) != true
                && BattleRules.MatchesOptionColorRequirement(
                    cardSource.State,
                    cardSource.Owner.Id,
                    cardSource.Id,
                    staticEffects);
        }

        return staticEffects?.HasCardRestriction(
            cardSource.State,
            cardSource.Id,
            StaticCardRestrictionKind.CannotPutField,
            restrictionCause) != true;
    }

    public static bool IsOwnerPermanent(Permanent? permanent, CardSource? card) =>
        permanent is not null && card is not null && permanent.TopCard.Owner.Id == card.Owner.Id;

    public static bool IsOpponentPermanent(Permanent? permanent, CardSource? card) =>
        permanent is not null && card is not null && permanent.TopCard.Owner.Id == card.Owner.Enemy.Id;

    public static bool IsPermanentExistsOnField(Permanent? permanent) =>
        IsPermanentExistsOnBattleArea(permanent) || IsPermanentExistsOnBreedingArea(permanent);

    public static bool IsPermanentExistsOnBattleArea(Permanent? permanent) =>
        permanent is not null
        && permanent.TopCard.Owner.GetBattleAreaPermanents().Any(candidate => candidate.Id == permanent.Id);

    public static bool IsPermanentExistsOnBreedingArea(Permanent? permanent) =>
        permanent is not null
        && permanent.TopCard.Owner.GetBreedingAreaPermanents().Any(candidate => candidate.Id == permanent.Id);

    public static bool IsPermanentExistsOnOwnerBattleArea(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnBattleArea(permanent) && IsOwnerPermanent(permanent, card);

    public static bool IsPermanentExistsOnOpponentBattleArea(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnBattleArea(permanent) && IsOpponentPermanent(permanent, card);

    public static bool IsPermanentExistsOnOwnerBreedingArea(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnBreedingArea(permanent) && IsOwnerPermanent(permanent, card);

    public static bool IsPermanentExistsOnOpponentBreedingArea(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnBreedingArea(permanent) && IsOpponentPermanent(permanent, card);

    public static bool IsPermanentExistsOnBattleAreaDigimon(Permanent? permanent) =>
        IsPermanentExistsOnBattleArea(permanent) && permanent?.IsDigimon == true;

    public static bool IsPermanentExistsOnOwnerBattleAreaDigimon(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnOwnerBattleArea(permanent, card) && permanent?.IsDigimon == true;

    public static bool IsPermanentExistsOnOpponentBattleAreaDigimon(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnOpponentBattleArea(permanent, card) && permanent?.IsDigimon == true;

    public static bool IsPermanentExistsOnBattleAreaTamer(Permanent? permanent) =>
        IsPermanentExistsOnBattleArea(permanent) && permanent?.IsTamer == true;

    public static bool IsPermanentExistsOnOwnerBattleAreaTamer(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnOwnerBattleArea(permanent, card) && permanent?.IsTamer == true;

    public static bool IsPermanentExistsOnOpponentBattleAreaTamer(Permanent? permanent, CardSource? card) =>
        IsPermanentExistsOnOpponentBattleArea(permanent, card) && permanent?.IsTamer == true;

    public static int MatchConditionPermanentCount(
        GameState state,
        Func<Permanent, bool> canSelectPermanentCondition,
        bool isContainBreedingArea = false)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(canSelectPermanentCondition);

        return Permanents(state, isContainBreedingArea).Count(canSelectPermanentCondition);
    }

    public static int MatchConditionPermanentCount(
        Func<Permanent, bool> canSelectPermanentCondition,
        bool isContainBreedingArea = false) =>
        throw new UnsupportedMechanicException(
            "CardEffectCommons.MatchConditionPermanentCount requires an explicit GameState in the headless runtime.");

    public static int MatchConditionOwnersPermanentCount(
        CardSource? card,
        Func<Permanent, bool> canSelectPermanentCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectPermanentCondition);
        if (card is null)
        {
            return 0;
        }

        return MatchConditionPermanentCount(
            card.State,
            permanent => IsOwnerPermanent(permanent, card) && canSelectPermanentCondition(permanent));
    }

    public static int MatchConditionOpponentsPermanentCount(
        CardSource? card,
        Func<Permanent, bool> canSelectPermanentCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectPermanentCondition);
        if (card is null)
        {
            return 0;
        }

        return MatchConditionPermanentCount(
            card.State,
            permanent => IsOpponentPermanent(permanent, card) && canSelectPermanentCondition(permanent));
    }

    public static bool HasMatchConditionPermanent(
        GameState state,
        Func<Permanent, bool> canSelectPermanentCondition,
        bool isContainBreedingArea = false) =>
        MatchConditionPermanentCount(state, canSelectPermanentCondition, isContainBreedingArea) > 0;

    public static bool HasMatchConditionPermanent(
        Func<Permanent, bool> canSelectPermanentCondition,
        bool isContainBreedingArea = false) =>
        throw new UnsupportedMechanicException(
            "CardEffectCommons.HasMatchConditionPermanent requires an explicit GameState in the headless runtime.");

    public static bool HasMatchConditionOwnersPermanent(
        CardSource? card,
        Func<Permanent, bool> canSelectPermanentCondition) =>
        MatchConditionOwnersPermanentCount(card, canSelectPermanentCondition) > 0;

    public static bool HasMatchConditionOwnersBreedingPermanent(
        CardSource? card,
        Func<Permanent, bool> canSelectPermanentCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectPermanentCondition);
        if (card is null)
        {
            return false;
        }

        return card.Owner.GetBreedingAreaPermanents().Any(permanent =>
            IsOwnerPermanent(permanent, card) && canSelectPermanentCondition(permanent));
    }

    public static bool HasMatchConditionOpponentsPermanent(
        CardSource? card,
        Func<Permanent, bool> canSelectPermanentCondition) =>
        MatchConditionOpponentsPermanentCount(card, canSelectPermanentCondition) > 0;

    public static bool HasMatchConditionOwnersHand(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition) =>
        MatchConditionOwnersCardCountInHand(card, canSelectCardCondition) > 0;

    public static int MatchConditionOwnersCardCountInHand(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectCardCondition);
        if (card is null)
        {
            return 0;
        }

        return card.Owner.HandCards.Count(canSelectCardCondition);
    }

    public static bool HasMatchConditionPermanentDigivolutionCards(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectCardCondition);
        return card?.PermanentOfThisCard()?.DigivolutionCards.Any(canSelectCardCondition) == true;
    }

    public static bool HasMatchConditionOwnersSecurity(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition,
        bool flipped = true)
    {
        ArgumentNullException.ThrowIfNull(canSelectCardCondition);
        if (card is null)
        {
            return false;
        }

        return card.Owner.SecurityCards.Any(candidate =>
            candidate.IsFlipped == flipped && canSelectCardCondition(candidate));
    }

    public static int MatchConditionOwnersCardCountInTrash(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectCardCondition);
        if (card is null)
        {
            return 0;
        }

        return card.Owner.TrashCards.Count(canSelectCardCondition);
    }

    public static int MatchConditionOpponentsCardCountInTrash(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition)
    {
        ArgumentNullException.ThrowIfNull(canSelectCardCondition);
        if (card is null)
        {
            return 0;
        }

        return card.Owner.Enemy.TrashCards.Count(canSelectCardCondition);
    }

    public static bool HasMatchConditionOwnersCardInTrash(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition) =>
        MatchConditionOwnersCardCountInTrash(card, canSelectCardCondition) > 0;

    public static bool HasMatchConditionOpponentsCardInTrash(
        CardSource? card,
        Func<CardSource, bool> canSelectCardCondition) =>
        MatchConditionOpponentsCardCountInTrash(card, canSelectCardCondition) > 0;

    public static int GetUniqueColourCountOnOwnerBattleArea(
        CardSource? card,
        Func<Permanent, bool> canGetCardColour) =>
        GetUniqueColourCountOnBattleArea(card?.Owner.GetBattleAreaPermanents(), canGetCardColour);

    public static int GetUniqueColourCountOnOpponentsBattleArea(
        CardSource? card,
        Func<Permanent, bool> canGetCardColour) =>
        GetUniqueColourCountOnBattleArea(card?.Owner.Enemy.GetBattleAreaPermanents(), canGetCardColour);

    public static bool IsOwnerTurn(CardSource? card) =>
        card is not null && card.State.TurnPlayerId == card.Owner.Id;

    public static bool IsOpponentTurn(CardSource? card) =>
        card is not null && !IsOwnerTurn(card);

    private static int GetUniqueColourCountOnBattleArea(
        IEnumerable<Permanent>? permanents,
        Func<Permanent, bool> canGetCardColour)
    {
        ArgumentNullException.ThrowIfNull(canGetCardColour);

        return permanents is null
            ? 0
            : permanents
                .Where(canGetCardColour)
                .SelectMany(permanent => permanent.TopCard.CardColors)
                .Distinct()
                .Count();
    }

    private static bool CaptureCurrentPermanent(CardSource? card, ICardEffect? cardEffect, bool exists)
    {
        if (!exists || card is null || cardEffect is null)
        {
            return exists;
        }

        var permanent = card.PermanentOfThisCard();
        cardEffect.SetEffectSourcePermanent(permanent);
        if (cardEffect is ActivateICardEffect activateICardEffect)
        {
            activateICardEffect.PermanentWhenTriggered = permanent;
            activateICardEffect.TopCardWhenTriggered = permanent?.TopCard;
        }

        return true;
    }

    private static bool IsCapturedPermanentCurrent(CardSource? card, ICardEffect? cardEffect)
    {
        if (card is null || cardEffect is null)
        {
            return false;
        }

        var currentPermanent = card.PermanentOfThisCard();
        var capturedPermanent = cardEffect is ActivateICardEffect activateICardEffect
            ? activateICardEffect.PermanentWhenTriggered ?? cardEffect.EffectSourcePermanent
            : cardEffect.EffectSourcePermanent;

        return currentPermanent is not null
            && capturedPermanent is not null
            && currentPermanent.Id == capturedPermanent.Id;
    }

    private static bool IsCardInRoot(CardSource card, SelectCardEffect.Root root) =>
        root switch
        {
            SelectCardEffect.Root.Library => IsInPlayerZone(card, Zone.Deck),
            SelectCardEffect.Root.Trash => IsInPlayerZone(card, Zone.Trash),
            SelectCardEffect.Root.Security => IsInPlayerZone(card, Zone.Security),
            SelectCardEffect.Root.Hand => IsInPlayerZone(card, Zone.Hand),
            SelectCardEffect.Root.Recollection => IsInPlayerZone(card, Zone.Lost),
            SelectCardEffect.Root.Execution => IsInPlayerZone(card, Zone.Executing),
            SelectCardEffect.Root.DigivolutionCards => card.CurrentZone is Zone.EvolutionSources
                && card.PermanentOfThisCard()?.DigivolutionCards.Any(candidate => candidate.Id == card.Id) == true,
            SelectCardEffect.Root.LinkedCards => card.CurrentZone is Zone.LinkedCards
                && card.PermanentOfThisCard()?.LinkedCards.Any(candidate => candidate.Id == card.Id) == true,
            SelectCardEffect.Root.None => false,
            SelectCardEffect.Root.Custom => throw new DomainException(
                "CardEffectCommons.CanPlayAsNewPermanent with Root.Custom requires an explicit custom root list in the headless runtime."),
            SelectCardEffect.Root.Clock => throw new UnsupportedMechanicException(
                "CardEffectCommons.CanPlayAsNewPermanent does not support SelectCardEffect.Root.Clock yet."),
            _ => throw new UnsupportedMechanicException(
                $"CardEffectCommons.CanPlayAsNewPermanent does not support SelectCardEffect.Root.{root}."),
        };

    private static bool IsInPlayerZone(CardSource card, Zone zone)
    {
        if (card.CurrentZone != zone)
        {
            return false;
        }

        var player = card.State.GetPlayer(card.Owner.Id);
        return zone switch
        {
            Zone.Deck => player.Deck.Contains(card.Id),
            Zone.Trash => player.Trash.Contains(card.Id),
            Zone.Security => player.Security.Contains(card.Id),
            Zone.Hand => player.Hand.Contains(card.Id),
            Zone.Lost => player.Lost.Contains(card.Id),
            Zone.Executing => player.Executing.Contains(card.Id),
            _ => throw new UnsupportedMechanicException(
                $"CardEffectCommons.CanPlayAsNewPermanent cannot validate player zone '{zone}'."),
        };
    }

    private static bool IsSameTopPermanentWithoutSources(CardSource card) =>
        card.PermanentOfThisCard() is { } permanent
        && permanent.TopCard.Id == card.Id
        && permanent.HasNoDigivolutionCards;

    private static bool HasAvailablePlayFrame(CardSource card, bool isBreedingArea)
    {
        var player = card.State.GetPlayer(card.Owner.Id);
        return isBreedingArea
            ? player.BreedingAreaPermanent is null
            : player.BattleAreaPermanents.Count() < card.State.Config.FieldSlotCount;
    }

    private static bool HasPlayableCost(
        CardSource card,
        bool payCost,
        int fixedCost,
        StaticEffectService? staticEffects)
    {
        if (!payCost)
        {
            return true;
        }

        var cost = ResolvePlayCost(card, fixedCost, staticEffects);
        return cost >= 0 && MaxPayableMemoryCost(card.State, card.Owner.Id) >= cost;
    }

    private static int ResolvePlayCost(CardSource card, int fixedCost, StaticEffectService? staticEffects)
    {
        if (fixedCost >= 0)
        {
            return staticEffects?.ApplyCostModifiers(
                card.State,
                card.Id,
                fixedCost,
                StaticCostKind.Play) ?? fixedCost;
        }

        if (card.Definition.PlayCost < 0)
        {
            return -1;
        }

        var baseCost = Math.Max(0, card.Definition.PlayCost);
        return staticEffects?.ApplyCostModifiers(
            card.State,
            card.Id,
            baseCost,
            StaticCostKind.Play) ?? baseCost;
    }

    private static int MaxPayableMemoryCost(GameState state, PlayerId player)
    {
        var playerMemory = player == state.TurnPlayerId ? state.Memory : -state.Memory;
        return Math.Max(0, 10 + playerMemory);
    }

    private static IEnumerable<Permanent> Permanents(GameState state, bool includeBreedingArea) =>
        state.Players
            .SelectMany(player => includeBreedingArea ? player.FieldPermanents : player.BattleAreaPermanents)
            .OrderBy(permanent => permanent.ControllerPlayerId.Value)
            .ThenBy(permanent => permanent.FrameIndex)
            .ThenBy(permanent => permanent.Id.Value)
            .Select(permanent => new Permanent(state, permanent.Id));
}
