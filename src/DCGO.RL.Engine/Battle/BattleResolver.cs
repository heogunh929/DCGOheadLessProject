using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record BattleResolutionResult(
    IReadOnlyList<PermanentId> DestroyedPermanents,
    bool WasTie);

public sealed class BattleResolver
{
    private readonly IZoneMover _zoneMover;
    private readonly BattleKeywordService _keywordService;
    private readonly EffectiveStatService _effectiveStats;

    public BattleResolver(
        IZoneMover? zoneMover = null,
        BattleKeywordService? keywordService = null,
        EffectiveStatService? effectiveStats = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _effectiveStats = effectiveStats ?? EffectiveStatService.NoContinuous;
        _keywordService = keywordService ?? new BattleKeywordService(_effectiveStats);
    }

    public BattleResolutionResult ResolvePermanentBattle(GameState state, PermanentId attackerId, PermanentId defenderId, GameTrace? trace = null)
    {
        var attacker = BattleRules.Permanent(state, attackerId);
        var defender = BattleRules.Permanent(state, defenderId);
        _keywordService.EnsureSupportedKeywords(state, attacker);
        _keywordService.EnsureSupportedKeywords(state, defender);
        var attackerDp = _effectiveStats.Dp(state, attacker);
        var defenderDp = _effectiveStats.Dp(state, defender);

        var destroyed = new List<PermanentId>();
        if (attackerDp > defenderDp)
        {
            destroyed.Add(defenderId);
            AddRetaliationTarget(state, defender, attacker, destroyed);
            DestroyPermanents(state, destroyed, trace);
            return new BattleResolutionResult(destroyed, WasTie: false);
        }

        if (attackerDp < defenderDp)
        {
            destroyed.Add(attackerId);
            AddRetaliationTarget(state, attacker, defender, destroyed);
            DestroyPermanents(state, destroyed, trace);
            return new BattleResolutionResult(destroyed, WasTie: false);
        }

        destroyed.Add(attackerId);
        destroyed.Add(defenderId);
        DestroyPermanents(state, destroyed, trace);
        return new BattleResolutionResult(destroyed, WasTie: true);
    }

    public BattleResolutionResult ResolveSecurityBattle(GameState state, PermanentId attackerId, CardInstanceId securityCard, GameTrace? trace = null)
    {
        var attacker = BattleRules.Permanent(state, attackerId);
        _keywordService.EnsureSupportedKeywords(state, attacker);
        var attackerDp = _effectiveStats.Dp(state, attacker);
        var securityDp = _effectiveStats.SecurityDp(state, securityCard);

        if (attackerDp > securityDp)
        {
            return new BattleResolutionResult(Array.Empty<PermanentId>(), WasTie: false);
        }

        if (_keywordService.HasKeyword(state, attacker, BattleKeyword.Jamming))
        {
            return new BattleResolutionResult(Array.Empty<PermanentId>(), WasTie: attackerDp == securityDp);
        }

        DestroyPermanent(state, attacker, trace);
        return new BattleResolutionResult(new[] { attackerId }, WasTie: attackerDp == securityDp);
    }

    private void AddRetaliationTarget(
        GameState state,
        PermanentState destroyedPermanent,
        PermanentState opponent,
        List<PermanentId> destroyed)
    {
        if (_keywordService.HasKeyword(state, destroyedPermanent, BattleKeyword.Retaliation)
            && !destroyed.Contains(opponent.Id))
        {
            destroyed.Add(opponent.Id);
        }
    }

    private void DestroyPermanents(GameState state, IReadOnlyList<PermanentId> permanentIds, GameTrace? trace)
    {
        foreach (var permanentId in permanentIds.Distinct())
        {
            if (state.Players.SelectMany(player => player.FieldPermanents).FirstOrDefault(permanent => permanent.Id == permanentId) is { } permanent)
            {
                DestroyPermanent(state, permanent, trace);
            }
        }
    }

    public void DestroyPermanent(GameState state, PermanentState permanent, GameTrace? trace = null)
    {
        if (permanent.LinkedCards.Count > 0)
        {
            throw new UnsupportedMechanicException("Destroying permanents with linked cards");
        }

        foreach (var source in permanent.SourceCardIds.ToArray())
        {
            var command = new MoveCardCommand(source, Zone.EvolutionSources, Zone.Trash, MoveReason.Trash, SourcePermanent: permanent.Id);
            var before = trace is null ? null : state.Clone();
            var result = _zoneMover.MoveCard(state, command);
            trace?.AddMove($"destroy-source:{permanent.Id.Value}:{source.Value}", before!, state, command, result);
        }

        var topZone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
        var top = permanent.TopCardId;
        var topCommand = new MoveCardCommand(top, topZone, Zone.Trash, MoveReason.Trash, SourcePermanent: permanent.Id);
        var topBefore = trace is null ? null : state.Clone();
        var topResult = _zoneMover.MoveCard(state, topCommand);
        trace?.AddMove($"destroy-top:{permanent.Id.Value}:{top.Value}", topBefore!, state, topCommand, topResult);
    }
}
