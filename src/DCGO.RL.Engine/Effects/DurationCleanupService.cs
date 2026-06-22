using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed record DurationCleanupResult(
    DurationCleanupEvent CleanupEvent,
    IReadOnlyList<string> RemovedModifierStableIds,
    IReadOnlyList<string> RemovedGrantedEffectStableIds)
{
    public DurationCleanupResult(
        DurationCleanupEvent cleanupEvent,
        IReadOnlyList<string> removedModifierStableIds)
        : this(cleanupEvent, removedModifierStableIds, Array.Empty<string>())
    {
    }
}

public enum DurationCleanupEvent
{
    TurnEnd,
    BattleEnd,
    AttackEnd,
    SecurityCheckEnd,
    OwnerActivePhaseEnd,
    StaleTarget,
}

public sealed class DurationCleanupService
{
    public DurationCleanupResult CleanupTurnEnd(GameState state, PlayerId endingTurnPlayer)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(
            state,
            modifier =>
                IsStaleTarget(state, modifier)
                || modifier.DurationScope == DurationScope.UntilTurnEnd
                || (modifier.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd
                    && modifier.ExpiresAtTurnPlayerId == endingTurnPlayer));
        var removedGrantedEffects = RemoveGrantedEffectsWhere(
            state,
            effect =>
                IsStaleTarget(state, effect)
                || effect.DurationScope == DurationScope.UntilTurnEnd
                || (effect.DurationScope is DurationScope.UntilOwnerTurnEnd or DurationScope.UntilOpponentTurnEnd
                    && effect.ExpiresAtTurnPlayerId == endingTurnPlayer));

        return new DurationCleanupResult(DurationCleanupEvent.TurnEnd, removedModifiers, removedGrantedEffects);
    }

    public DurationCleanupResult CleanupBattleEnd(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(
            state,
            modifier =>
                IsStaleTarget(state, modifier)
                || modifier.DurationScope == DurationScope.UntilBattleEnd);
        var removedGrantedEffects = RemoveGrantedEffectsWhere(
            state,
            effect =>
                IsStaleTarget(state, effect)
                || effect.DurationScope == DurationScope.UntilBattleEnd);

        return new DurationCleanupResult(DurationCleanupEvent.BattleEnd, removedModifiers, removedGrantedEffects);
    }

    public DurationCleanupResult CleanupAttackEnd(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(
            state,
            modifier =>
                IsStaleTarget(state, modifier)
                || modifier.DurationScope == DurationScope.UntilAttackEnd);
        var removedGrantedEffects = RemoveGrantedEffectsWhere(
            state,
            effect =>
                IsStaleTarget(state, effect)
                || effect.DurationScope == DurationScope.UntilAttackEnd);

        return new DurationCleanupResult(DurationCleanupEvent.AttackEnd, removedModifiers, removedGrantedEffects);
    }

    public DurationCleanupResult CleanupSecurityCheckEnd(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(
            state,
            modifier =>
                IsStaleTarget(state, modifier)
                || modifier.DurationScope == DurationScope.UntilSecurityCheckEnd);
        var removedGrantedEffects = RemoveGrantedEffectsWhere(
            state,
            effect =>
                IsStaleTarget(state, effect)
                || effect.DurationScope == DurationScope.UntilSecurityCheckEnd);

        return new DurationCleanupResult(DurationCleanupEvent.SecurityCheckEnd, removedModifiers, removedGrantedEffects);
    }

    public DurationCleanupResult CleanupOwnerActivePhaseEnd(GameState state, PlayerId activePlayer)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(
            state,
            modifier =>
                IsStaleTarget(state, modifier)
                || (modifier.DurationScope == DurationScope.UntilOwnerActivePhaseEnd
                    && modifier.ExpiresAtTurnPlayerId == activePlayer));
        var removedGrantedEffects = RemoveGrantedEffectsWhere(
            state,
            effect =>
                IsStaleTarget(state, effect)
                || (effect.DurationScope == DurationScope.UntilOwnerActivePhaseEnd
                    && effect.ExpiresAtTurnPlayerId == activePlayer));

        return new DurationCleanupResult(DurationCleanupEvent.OwnerActivePhaseEnd, removedModifiers, removedGrantedEffects);
    }

    public DurationCleanupResult CleanupStaleTargets(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var removedModifiers = RemoveModifiersWhere(state, modifier => IsStaleTarget(state, modifier));
        var removedGrantedEffects = RemoveGrantedEffectsWhere(state, effect => IsStaleTarget(state, effect));
        return new DurationCleanupResult(DurationCleanupEvent.StaleTarget, removedModifiers, removedGrantedEffects);
    }

    private static IReadOnlyList<string> RemoveModifiersWhere(GameState state, Func<TemporaryModifier, bool> shouldRemove)
    {
        var removed = state.TemporaryModifiers
            .Where(shouldRemove)
            .Select(modifier => modifier.StableId)
            .ToArray();

        if (removed.Length == 0)
        {
            return removed;
        }

        var removedIds = new HashSet<string>(removed, StringComparer.Ordinal);
        state.TemporaryModifiers.RemoveAll(modifier => removedIds.Contains(modifier.StableId));
        return removed;
    }

    private static IReadOnlyList<string> RemoveGrantedEffectsWhere(
        GameState state,
        Func<TemporaryGrantedEffect, bool> shouldRemove)
    {
        var removed = state.TemporaryGrantedEffects
            .Where(shouldRemove)
            .Select(effect => effect.StableId)
            .ToArray();

        if (removed.Length == 0)
        {
            return removed;
        }

        var removedIds = new HashSet<string>(removed, StringComparer.Ordinal);
        state.TemporaryGrantedEffects.RemoveAll(effect => removedIds.Contains(effect.StableId));
        return removed;
    }

    private static bool IsStaleTarget(GameState state, TemporaryModifier modifier)
    {
        if (modifier.TargetPermanentId is not null && FindPermanent(state, modifier.TargetPermanentId.Value) is null)
        {
            return true;
        }

        if (modifier.TargetPlayerId is not null && state.Players.All(player => player.Id != modifier.TargetPlayerId.Value))
        {
            return true;
        }

        return false;
    }

    private static bool IsStaleTarget(GameState state, TemporaryGrantedEffect effect)
    {
        if (effect.TargetPermanentId is not null && FindPermanent(state, effect.TargetPermanentId.Value) is null)
        {
            return true;
        }

        if (effect.TargetPlayerId is not null && state.Players.All(player => player.Id != effect.TargetPlayerId.Value))
        {
            return true;
        }

        return false;
    }

    private static PermanentState? FindPermanent(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId);
}
