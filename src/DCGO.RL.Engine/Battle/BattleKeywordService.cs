using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Battle;

public sealed class BattleKeywordService
{
    public static BattleKeywordService Default { get; } = new();

    private readonly EffectiveStatService _effectiveStats;

    public BattleKeywordService(EffectiveStatService? effectiveStats = null)
    {
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
    }

    public bool HasKeyword(GameState state, PermanentState permanent, BattleKeyword keyword)
    {
        if (keyword == BattleKeyword.Decoy)
        {
            ThrowUnsupported(BattleKeyword.Decoy);
        }

        return permanent.BattleKeywords.Contains(keyword)
            || PermanentKeywordCards(state, permanent)
                .Select(card => BattleRules.Definition(state, card))
                .Any(definition => definition.BattleKeywords.Contains(keyword));
    }

    public int SecurityAttackCount(GameState state, PermanentState permanent)
    {
        EnsureSupportedKeywords(state, permanent);

        var modifier = permanent.SecurityAttackModifier
            + PermanentKeywordCards(state, permanent)
                .Select(card => BattleRules.Definition(state, card).SecurityAttackModifier)
                .Sum()
            + state.TemporaryModifiers
                .Where(modifier =>
                    modifier.ModifierKind == TemporaryModifierKind.SecurityAttack
                    && (modifier.TargetPermanentId == permanent.Id
                        || (modifier.TargetPermanentId is null && modifier.TargetPlayerId == permanent.ControllerPlayerId)))
                .Sum(modifier => modifier.Amount)
            + _effectiveStats.SecurityAttackModifierAmount(state, permanent);

        return Math.Max(0, 1 + modifier);
    }

    public bool CanBlock(GameState state, PermanentState blocker, PermanentState attacker)
    {
        if (blocker.ControllerPlayerId == attacker.ControllerPlayerId
            || blocker.IsBreedingArea
            || blocker.IsSuspended
            || BattleRules.HasTemporaryRestriction(state, blocker, TemporaryModifierKind.CannotBlock)
            || !BattleRules.IsDigimon(state, blocker.TopCardId)
            || !BattleRules.IsDigimon(state, attacker.TopCardId)
            || _effectiveStats.Dp(state, blocker) <= 0)
        {
            return false;
        }

        return HasKeyword(state, blocker, BattleKeyword.Blocker)
            || HasKeyword(state, attacker, BattleKeyword.Collision);
    }

    public SelectionRequest? CreateBlockerSelectionRequest(
        GameState state,
        PermanentId attackerId,
        PermanentId? currentDefender = null)
    {
        var attacker = BattleRules.Permanent(state, attackerId);
        EnsureSupportedKeywords(state, attacker);

        var canSkip = !HasKeyword(state, attacker, BattleKeyword.Collision);
        var candidates = state.GetPlayer(attacker.ControllerPlayerId == PlayerId.Player0 ? PlayerId.Player1 : PlayerId.Player0)
            .BattleAreaPermanents
            .Where(permanent => permanent.Id != currentDefender)
            .Where(permanent => CanBlock(state, permanent, attacker))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                StableId: $"blocker:{attacker.Id.Value}:{permanent.Id.Value}",
                Owner: permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();

        if (candidates.Length == 0)
        {
            return null;
        }

        return new SelectionRequest(
            $"blocker:{attacker.Id.Value}",
            attacker.ControllerPlayerId == PlayerId.Player0 ? PlayerId.Player1 : PlayerId.Player0,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: 1,
            canSkip,
            canEndNotMax: false,
            candidates,
            "Select 1 Digimon that will block.");
    }

    public void EnsureSupportedKeywords(GameState state)
    {
        foreach (var permanent in state.Players.SelectMany(player => player.FieldPermanents))
        {
            EnsureSupportedKeywords(state, permanent);
        }
    }

    public void EnsureSupportedKeywords(GameState state, PermanentState permanent)
    {
        foreach (var keyword in permanent.BattleKeywords)
        {
            EnsureSupportedKeyword(keyword);
        }

        foreach (var card in PermanentKeywordCards(state, permanent))
        {
            foreach (var keyword in BattleRules.Definition(state, card).BattleKeywords)
            {
                EnsureSupportedKeyword(keyword);
            }
        }
    }

    private static IEnumerable<CardInstanceId> PermanentKeywordCards(GameState state, PermanentState permanent)
    {
        yield return permanent.TopCardId;

        foreach (var source in permanent.SourceCardIds)
        {
            yield return source;
        }

        foreach (var linkedCard in permanent.LinkedCards)
        {
            yield return linkedCard;
        }
    }

    private static void EnsureSupportedKeyword(BattleKeyword keyword)
    {
        if (keyword == BattleKeyword.Decoy)
        {
            ThrowUnsupported(keyword);
        }
    }

    private static void ThrowUnsupported(BattleKeyword keyword) =>
        throw new UnsupportedMechanicException($"Battle keyword '{keyword}' is not supported yet.");
}
