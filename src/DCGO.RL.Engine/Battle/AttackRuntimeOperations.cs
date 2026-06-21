using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Battle;

public static class AttackRuntimeOperations
{
    public static AttackRuntimeContext EndAttack(GameState state) =>
        state.RuntimeRules.EndAttack();

    public static bool SwitchDefender(
        GameState state,
        PermanentId? newDefender,
        bool isBlock = false,
        PermanentId? blocker = null,
        string? sourceEffectStableId = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        var context = state.RuntimeRules.RequireAttack();
        if (!CanSwitchAttackTarget(state, context.Attacker))
        {
            return false;
        }

        if (newDefender is not null && !IsValidDefender(state, context.Attacker, newDefender.Value))
        {
            return false;
        }

        state.RuntimeRules.SwitchDefender(newDefender, isBlock, blocker, sourceEffectStableId);
        return true;
    }

    public static bool SwitchToSecurityTarget(GameState state, string? sourceEffectStableId = null) =>
        SwitchDefender(state, null, isBlock: false, blocker: null, sourceEffectStableId);

    public static AttackRuntimeContext SetBlocking(GameState state, bool isBlocking, PermanentId? blocker = null) =>
        state.RuntimeRules.SetBlocking(isBlocking, blocker);

    public static PermanentId CurrentAttacker(GameState state) =>
        state.RuntimeRules.RequireAttack().Attacker;

    public static PermanentId? CurrentDefender(GameState state) =>
        state.RuntimeRules.RequireAttack().Defender;

    public static PermanentId? CurrentBlocker(GameState state) =>
        state.RuntimeRules.RequireAttack().Blocker;

    public static bool IsBlocking(GameState state) =>
        state.RuntimeRules.RequireAttack().IsBlocking;

    public static bool CanSwitchAttackTarget(GameState state, PermanentId attackerId)
    {
        if (!TryFindPermanent(state, attackerId, out var attacker))
        {
            return false;
        }

        return !attacker.IsBreedingArea
            && BattleRules.IsDigimon(state, attacker.TopCardId)
            && !BattleRules.HasTemporaryRestriction(state, attacker, TemporaryModifierKind.CannotSwitchAttackTarget);
    }

    private static bool IsValidDefender(GameState state, PermanentId attackerId, PermanentId defenderId)
    {
        if (!TryFindPermanent(state, attackerId, out var attacker)
            || !TryFindPermanent(state, defenderId, out var defender))
        {
            return false;
        }

        return defender.ControllerPlayerId != attacker.ControllerPlayerId
            && !defender.IsBreedingArea
            && BattleRules.IsDigimon(state, defender.TopCardId);
    }

    private static bool TryFindPermanent(GameState state, PermanentId id, out PermanentState permanent)
    {
        foreach (var player in state.Players)
        {
            var found = player.FieldPermanents.FirstOrDefault(permanent => permanent.Id == id);
            if (found is not null)
            {
                permanent = found;
                return true;
            }
        }

        permanent = null!;
        return false;
    }
}
