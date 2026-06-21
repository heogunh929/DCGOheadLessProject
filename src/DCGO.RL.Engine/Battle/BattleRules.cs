using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Battle;

internal static class BattleRules
{
    public static CardDefinition Definition(GameState state, CardInstanceId card)
    {
        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Card instance '{card}' does not exist.");
        }

        return state.CardDefinitions.TryGetValue(instance.DefinitionId, out var definition)
            ? definition
            : throw new DomainException($"Card definition '{instance.DefinitionId}' does not exist.");
    }

    public static PermanentState Permanent(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId)
        ?? throw new DomainException($"Permanent '{permanentId}' does not exist.");

    public static PlayerState PermanentOwner(GameState state, PermanentId permanentId)
    {
        foreach (var player in state.Players)
        {
            if (player.FieldPermanents.Any(permanent => permanent.Id == permanentId))
            {
                return player;
            }
        }

        throw new DomainException($"Permanent '{permanentId}' does not exist.");
    }

    public static int NextPermanentId(GameState state) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .Select(permanent => permanent.Id.Value)
            .DefaultIfEmpty(0)
            .Max() + 1;

    public static bool IsEmptyBattleFrame(PlayerState player, int frameIndex) =>
        frameIndex >= 0 && player.BattleAreaPermanents.All(permanent => permanent.FrameIndex != frameIndex);

    public static int FirstEmptyBattleFrame(PlayerState player, int fieldSlotCount)
    {
        for (var i = 0; i < fieldSlotCount; i++)
        {
            if (IsEmptyBattleFrame(player, i))
            {
                return i;
            }
        }

        throw new DomainException($"Player '{player.Id}' has no empty battle frame.");
    }

    public static bool HasKind(GameState state, CardInstanceId card, CardKind kind) =>
        Definition(state, card).CardKinds.Contains(kind);

    public static bool IsPermanentCard(GameState state, CardInstanceId card)
    {
        var definition = Definition(state, card);
        return definition.CardKinds.Contains(CardKind.Digimon)
            || definition.CardKinds.Contains(CardKind.Tamer)
            || definition.CardKinds.Contains(CardKind.DigiEgg);
    }

    public static bool IsOptionCard(GameState state, CardInstanceId card) =>
        Definition(state, card).CardKinds.Contains(CardKind.Option);

    public static bool IsDigimon(GameState state, CardInstanceId card) =>
        Definition(state, card).CardKinds.Contains(CardKind.Digimon);

    public static int Dp(GameState state, CardInstanceId card)
    {
        var definition = Definition(state, card);
        var dp = definition.DP;

        if (state.Cards.TryGetValue(card, out var instance) && instance.CurrentZone == Zone.Security)
        {
            dp += SecurityDigimonDpModifierAmount(state, instance, definition);
        }

        return dp;
    }

    public static int SecurityDp(GameState state, CardInstanceId card)
    {
        var definition = Definition(state, card);
        var dp = definition.DP;
        if (state.Cards.TryGetValue(card, out var instance))
        {
            dp += SecurityDigimonDpModifierAmount(state, instance, definition);
        }

        return dp;
    }

    public static int Dp(GameState state, PermanentState permanent) =>
        Dp(state, permanent.TopCardId)
        + permanent.DpModifier
        + state.TemporaryModifiers
            .Where(modifier => AppliesToPermanentDpModifier(modifier, permanent))
            .Sum(modifier => modifier.Amount);

    private static bool AppliesToPermanentDpModifier(TemporaryModifier modifier, PermanentState permanent)
    {
        if (modifier.ModifierKind != TemporaryModifierKind.DP)
        {
            return false;
        }

        if (modifier.TargetPermanentId is not null)
        {
            return modifier.TargetPermanentId == permanent.Id;
        }

        return modifier.TargetPlayerId == permanent.ControllerPlayerId && !permanent.IsBreedingArea;
    }

    private static int SecurityDigimonDpModifierAmount(
        GameState state,
        CardInstance instance,
        CardDefinition definition)
    {
        if (!definition.CardKinds.Contains(CardKind.Digimon))
        {
            return 0;
        }

        return state.TemporaryModifiers
            .Where(modifier =>
                modifier.ModifierKind == TemporaryModifierKind.SecurityDigimonDP
                && modifier.TargetPlayerId == instance.Owner)
            .Sum(modifier => modifier.Amount);
    }

    public static bool CanAttack(
        GameState state,
        PermanentState permanent,
        BattleKeywordService? keywordService = null,
        EffectiveStatService? effectiveStats = null)
    {
        keywordService ??= BattleKeywordService.Default;
        effectiveStats ??= EffectiveStatService.NoContinuous;
        keywordService.EnsureSupportedKeywords(state, permanent);

        return permanent.ControllerPlayerId == state.TurnPlayerId
            && !permanent.IsBreedingArea
            && !permanent.IsSuspended
            && !HasTemporaryRestriction(state, permanent, TemporaryModifierKind.CannotAttack)
            && (permanent.EnterFieldTurnCount != state.TurnCount || keywordService.HasKeyword(state, permanent, BattleKeyword.Rush))
            && IsDigimon(state, permanent.TopCardId)
            && effectiveStats.Dp(state, permanent) > 0;
    }

    public static bool HasTemporaryRestriction(
        GameState state,
        PermanentState permanent,
        TemporaryModifierKind restrictionKind)
    {
        if (restrictionKind is not (
            TemporaryModifierKind.CannotAttack
            or TemporaryModifierKind.CannotBlock
            or TemporaryModifierKind.CannotSwitchAttackTarget))
        {
            throw new DomainException($"Temporary modifier kind '{restrictionKind}' is not a restriction.");
        }

        return state.TemporaryModifiers.Any(modifier =>
            modifier.ModifierKind == restrictionKind
            && modifier.TargetPermanentId == permanent.Id);
    }

    public static bool CanMoveFromBreeding(GameState state, PermanentState permanent)
    {
        var player = state.GetPlayer(permanent.ControllerPlayerId);
        return permanent.IsBreedingArea
            && IsDigimon(state, permanent.TopCardId)
            && Dp(state, permanent) > 0
            && player.BattleAreaPermanents.Count() < state.Config.FieldSlotCount;
    }

    public static bool CanDigivolve(GameState state, CardInstanceId card, PermanentState targetPermanent, out int cost)
    {
        cost = -1;
        if (!IsDigimon(state, card))
        {
            return false;
        }

        var cardDefinition = Definition(state, card);
        var targetDefinition = Definition(state, targetPermanent.TopCardId);
        foreach (var evoCost in cardDefinition.EvoCosts)
        {
            var levelMatches = evoCost.Level == 0 || evoCost.Level == targetDefinition.Level;
            var colorMatches = evoCost.CardColor == CardColor.None || targetDefinition.CardColors.Contains(evoCost.CardColor);
            if (levelMatches && colorMatches)
            {
                cost = Math.Max(0, evoCost.MemoryCost);
                return true;
            }
        }

        return false;
    }

    public static void PayMemory(GameState state, PlayerId actor, int cost)
    {
        if (actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can pay memory, actual '{actor}'.");
        }

        if (cost < 0)
        {
            throw new DomainException("Memory cost must not be negative.");
        }

        state.Memory -= cost;
    }
}
