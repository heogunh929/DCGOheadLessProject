using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public interface IContinuousCardScript
{
    IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context);
}

public sealed class ContinuousEffectSourceCollector
{
    private readonly ICardScriptRegistry _cardScriptRegistry;

    public ContinuousEffectSourceCollector(ICardScriptRegistry cardScriptRegistry)
    {
        _cardScriptRegistry = cardScriptRegistry ?? throw new ArgumentNullException(nameof(cardScriptRegistry));
    }

    public IReadOnlyList<ContinuousEffectDescriptor> Collect(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<ContinuousEffectDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IContinuousCardScript continuousScript)
            {
                continue;
            }

            descriptors.AddRange(continuousScript.CreateContinuousEffectDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent.Id,
                    source.Permanent.ControllerPlayerId,
                    source.Kind)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    private static IEnumerable<ContinuousSource> EnumerateSources(GameState state)
    {
        foreach (var player in state.Players.OrderBy(player => player.Id.Value))
        {
            foreach (var permanent in player.FieldPermanents
                .OrderBy(permanent => permanent.FrameIndex)
                .ThenBy(permanent => permanent.Id.Value))
            {
                yield return CreateSource(state, permanent.TopCardId, permanent, ContinuousEffectSourceKind.FieldTop);

                foreach (var sourceCard in permanent.SourceCardIds)
                {
                    yield return CreateSource(state, sourceCard, permanent, ContinuousEffectSourceKind.InheritedSource);
                }
            }
        }
    }

    private static ContinuousSource CreateSource(
        GameState state,
        CardInstanceId card,
        PermanentState permanent,
        ContinuousEffectSourceKind kind)
    {
        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Continuous effect source card '{card}' does not exist.");
        }

        if (instance.PermanentId != permanent.Id)
        {
            throw new DomainException(
                $"Continuous effect source card '{card}' does not belong to permanent '{permanent.Id}'.");
        }

        return new ContinuousSource(card, instance, permanent, kind);
    }

    private sealed record ContinuousSource(
        CardInstanceId Card,
        CardInstance Instance,
        PermanentState Permanent,
        ContinuousEffectSourceKind Kind);
}

public sealed class ContinuousEffectService
{
    private readonly ContinuousEffectSourceCollector _sourceCollector;

    public ContinuousEffectService(ICardScriptRegistry cardScriptRegistry)
        : this(new ContinuousEffectSourceCollector(cardScriptRegistry))
    {
    }

    public ContinuousEffectService(ContinuousEffectSourceCollector sourceCollector)
    {
        _sourceCollector = sourceCollector ?? throw new ArgumentNullException(nameof(sourceCollector));
    }

    public int PermanentModifierAmount(GameState state, PermanentState target, ContinuousModifierKind modifierKind) =>
        EvaluateForPermanent(state, target, modifierKind).Sum(evaluation => evaluation.Amount);

    public int PlayerModifierAmount(GameState state, PlayerId targetPlayer, ContinuousModifierKind modifierKind) =>
        EvaluateForPlayer(state, targetPlayer, modifierKind).Sum(evaluation => evaluation.Amount);

    public IReadOnlyList<ContinuousEffectEvaluation> EvaluateForPermanent(
        GameState state,
        PermanentState target,
        ContinuousModifierKind modifierKind)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(target);

        return _sourceCollector.Collect(state)
            .Where(descriptor => descriptor.ModifierKind == modifierKind)
            .Where(descriptor => AppliesToPermanent(state, descriptor, target))
            .Select(descriptor => Evaluate(state, descriptor, target, targetPlayer: null))
            .Where(evaluation => evaluation.Amount != 0)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<ContinuousEffectEvaluation> EvaluateForPlayer(
        GameState state,
        PlayerId targetPlayer,
        ContinuousModifierKind modifierKind)
    {
        ArgumentNullException.ThrowIfNull(state);
        _ = state.GetPlayer(targetPlayer);

        return _sourceCollector.Collect(state)
            .Where(descriptor => descriptor.ModifierKind == modifierKind)
            .Where(descriptor => AppliesToPlayer(state, descriptor, targetPlayer))
            .Select(descriptor => Evaluate(state, descriptor, targetPermanent: null, targetPlayer))
            .Where(evaluation => evaluation.Amount != 0)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    private static ContinuousEffectEvaluation Evaluate(
        GameState state,
        ContinuousEffectDescriptor descriptor,
        PermanentState? targetPermanent,
        PlayerId? targetPlayer)
    {
        var context = new ContinuousEffectEvaluationContext(state, descriptor, targetPermanent, targetPlayer);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return new ContinuousEffectEvaluation(descriptor, targetPermanent?.Id, targetPlayer, 0);
        }

        return new ContinuousEffectEvaluation(
            descriptor,
            targetPermanent?.Id,
            targetPlayer,
            descriptor.Amount(context));
    }

    private static bool AppliesToPermanent(
        GameState state,
        ContinuousEffectDescriptor descriptor,
        PermanentState target)
    {
        return descriptor.AppliesTo switch
        {
            ContinuousEffectTargetKind.SelfPermanent =>
                descriptor.SourcePermanentId == target.Id,
            ContinuousEffectTargetKind.OwnerBattleAreaDigimon =>
                target.ControllerPlayerId == descriptor.ControllerPlayerId
                && !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId),
            _ => false,
        };
    }

    private static bool AppliesToPlayer(
        GameState state,
        ContinuousEffectDescriptor descriptor,
        PlayerId targetPlayer)
    {
        return descriptor.AppliesTo switch
        {
            ContinuousEffectTargetKind.OwnerPlayer => targetPlayer == descriptor.ControllerPlayerId,
            ContinuousEffectTargetKind.OpponentPlayer => targetPlayer == Opponent(state, descriptor.ControllerPlayerId),
            _ => false,
        };
    }

    private static PlayerId Opponent(GameState state, PlayerId player)
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
}

public sealed class EffectiveStatService
{
    public static EffectiveStatService NoContinuous { get; } = new();

    private readonly ContinuousEffectService? _continuousEffectService;

    public EffectiveStatService()
    {
    }

    public EffectiveStatService(ICardScriptRegistry cardScriptRegistry)
        : this(new ContinuousEffectService(cardScriptRegistry))
    {
    }

    public EffectiveStatService(ContinuousEffectService continuousEffectService)
    {
        _continuousEffectService = continuousEffectService
            ?? throw new ArgumentNullException(nameof(continuousEffectService));
    }

    public int Dp(GameState state, PermanentState permanent)
    {
        var dp = BattleRules.Dp(state, permanent)
            + ContinuousPermanentModifierAmount(state, permanent, ContinuousModifierKind.DP);
        return Math.Max(0, dp);
    }

    public int SecurityDp(GameState state, CardInstanceId card)
    {
        var dp = BattleRules.SecurityDp(state, card);
        var definition = BattleRules.Definition(state, card);
        if (definition.CardKinds.Contains(CardKind.Digimon)
            && state.Cards.TryGetValue(card, out var instance))
        {
            dp += ContinuousPlayerModifierAmount(
                state,
                instance.Owner,
                ContinuousModifierKind.SecurityDigimonDP);
        }

        return Math.Max(0, dp);
    }

    public int SecurityAttackModifierAmount(GameState state, PermanentState permanent) =>
        ContinuousPermanentModifierAmount(state, permanent, ContinuousModifierKind.SecurityAttack)
        + ContinuousPlayerModifierAmount(state, permanent.ControllerPlayerId, ContinuousModifierKind.SecurityAttack);

    public int ContinuousPermanentModifierAmount(
        GameState state,
        PermanentState permanent,
        ContinuousModifierKind modifierKind) =>
        _continuousEffectService?.PermanentModifierAmount(state, permanent, modifierKind) ?? 0;

    public int ContinuousPlayerModifierAmount(
        GameState state,
        PlayerId player,
        ContinuousModifierKind modifierKind) =>
        _continuousEffectService?.PlayerModifierAmount(state, player, modifierKind) ?? 0;
}
