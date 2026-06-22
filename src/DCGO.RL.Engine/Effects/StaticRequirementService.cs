using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed class StaticRequirementService
{
    public static StaticRequirementService NoRequirements { get; } = new();

    private readonly ContinuousEffectSourceCollector? _sourceCollector;

    public StaticRequirementService()
    {
    }

    public StaticRequirementService(ICardScriptRegistry cardScriptRegistry)
        : this(new ContinuousEffectSourceCollector(cardScriptRegistry))
    {
    }

    public StaticRequirementService(ContinuousEffectSourceCollector sourceCollector)
    {
        _sourceCollector = sourceCollector ?? throw new ArgumentNullException(nameof(sourceCollector));
    }

    public IReadOnlyList<StaticEvolutionRequirementEvaluation> EvaluateEvolutionRequirements(
        GameState state,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(targetPermanent);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticEvolutionRequirementEvaluation>();
        }

        if (!state.Cards.TryGetValue(evolvingCard, out var evolvingInstance))
        {
            throw new DomainException($"Static evolution requirement card '{evolvingCard}' does not exist.");
        }

        return _sourceCollector.CollectStaticEvolutionRequirements(state)
            .Where(descriptor => descriptor.SourceCardId == evolvingCard)
            .Where(descriptor => descriptor.ControllerPlayerId == evolvingInstance.Owner)
            .Select(descriptor => EvaluateEvolutionRequirement(state, descriptor, evolvingCard, targetPermanent, staticEffects))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public StaticEvolutionRequirementEvaluation? FirstEvolutionRequirement(
        GameState state,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects = null) =>
        EvaluateEvolutionRequirements(state, evolvingCard, targetPermanent, staticEffects).FirstOrDefault();

    public IReadOnlyList<CannotIgnoreDigivolutionRequirementEvaluation> EvaluateCannotIgnoreDigivolutionRequirements(
        GameState state,
        PlayerId player,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(targetPermanent);

        if (_sourceCollector is null)
        {
            return Array.Empty<CannotIgnoreDigivolutionRequirementEvaluation>();
        }

        if (!state.Cards.ContainsKey(evolvingCard))
        {
            throw new DomainException($"Cannot-ignore digivolution requirement card '{evolvingCard}' does not exist.");
        }

        var evolvingDefinition = BattleRules.Definition(state, evolvingCard);
        var targetDefinition = BattleRules.Definition(state, targetPermanent.TopCardId);
        return _sourceCollector.CollectCannotIgnoreDigivolutionRequirements(state)
            .Where(descriptor => AppliesToPlayer(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, player))
            .Where(descriptor => MatchesMetadata(descriptor.TargetMetadataCriteria, targetDefinition))
            .Where(descriptor => MatchesMetadata(descriptor.EvolvingCardMetadataCriteria, evolvingDefinition))
            .Select(descriptor => EvaluateCannotIgnoreDigivolutionRequirement(
                state,
                descriptor,
                player,
                evolvingCard,
                targetPermanent))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticLinkRequirementEvaluation> EvaluateLinkRequirements(
        GameState state,
        CardInstanceId linkCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(targetPermanent);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticLinkRequirementEvaluation>();
        }

        if (!state.Cards.TryGetValue(linkCard, out var linkInstance))
        {
            throw new DomainException($"Static link requirement card '{linkCard}' does not exist.");
        }

        return _sourceCollector.CollectStaticLinkRequirements(state)
            .Where(descriptor => descriptor.SourceCardId == linkCard)
            .Where(descriptor => descriptor.ControllerPlayerId == linkInstance.Owner)
            .Select(descriptor => EvaluateLinkRequirement(state, descriptor, linkCard, targetPermanent, staticEffects))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public StaticLinkRequirementEvaluation? FirstLinkRequirement(
        GameState state,
        CardInstanceId linkCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects = null) =>
        EvaluateLinkRequirements(state, linkCard, targetPermanent, staticEffects).FirstOrDefault();

    private StaticEvolutionRequirementEvaluation? EvaluateEvolutionRequirement(
        GameState state,
        StaticEvolutionRequirementDescriptor descriptor,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects)
    {
        var context = new StaticEvolutionRequirementEvaluationContext(
            state,
            descriptor,
            evolvingCard,
            targetPermanent);

        if (descriptor.SourceCardCondition is not null && !descriptor.SourceCardCondition(context))
        {
            return null;
        }

        if (descriptor.TargetPermanentCondition is not null && !descriptor.TargetPermanentCondition(context))
        {
            return null;
        }

        if (descriptor.IgnoreDigivolutionRequirement && !HasExplicitTargetGate(descriptor))
        {
            throw new UnsupportedMechanicException(
                $"Static evolution requirement '{descriptor.StableId}' uses ignore digivolution requirement permission without an explicit target gate.");
        }

        if (descriptor.IgnoreDigivolutionRequirement
            && !CanIgnoreDigivolutionRequirement(state, descriptor.ControllerPlayerId, evolvingCard, targetPermanent))
        {
            return null;
        }

        var targetDefinition = BattleRules.Definition(state, targetPermanent.TopCardId);
        var targetColors = staticEffects?.EffectiveCardColors(state, targetPermanent.TopCardId)
            ?? targetDefinition.CardColors;
        var targetLevel = staticEffects?.EffectivePermanentLevel(state, targetPermanent)
            ?? targetDefinition.Level;
        if (!MatchesMetadata(state, evolvingCard, descriptor.SourceMetadataCriteria, staticEffects)
            || !MatchesMetadata(state, targetPermanent.TopCardId, descriptor.TargetMetadataCriteria, staticEffects))
        {
            return null;
        }

        if (descriptor.RequiredColor != CardColor.None
            && !targetColors.Contains(descriptor.RequiredColor))
        {
            return null;
        }

        if (descriptor.RequiredLevel >= 0 && targetLevel != descriptor.RequiredLevel)
        {
            return null;
        }

        if (descriptor.MinLevel >= 0 && targetLevel < descriptor.MinLevel)
        {
            return null;
        }

        if (descriptor.MaxLevel >= 0 && targetLevel > descriptor.MaxLevel)
        {
            return null;
        }

        var cost = descriptor.CostEquation?.Invoke(context) ?? descriptor.Cost;
        if (cost < 0)
        {
            return null;
        }

        return new StaticEvolutionRequirementEvaluation(
            descriptor,
            evolvingCard,
            targetPermanent.Id,
            Math.Max(0, cost),
            descriptor.IgnoreDigivolutionRequirement);
    }

    private bool CanIgnoreDigivolutionRequirement(
        GameState state,
        PlayerId player,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent) =>
        EvaluateCannotIgnoreDigivolutionRequirements(state, player, evolvingCard, targetPermanent).Count == 0;

    private static CannotIgnoreDigivolutionRequirementEvaluation? EvaluateCannotIgnoreDigivolutionRequirement(
        GameState state,
        CannotIgnoreDigivolutionRequirementDescriptor descriptor,
        PlayerId player,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent)
    {
        var context = new CannotIgnoreDigivolutionRequirementEvaluationContext(
            state,
            descriptor,
            player,
            targetPermanent,
            evolvingCard);

        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new CannotIgnoreDigivolutionRequirementEvaluation(
            descriptor,
            player,
            targetPermanent.Id,
            evolvingCard);
    }

    private static StaticLinkRequirementEvaluation? EvaluateLinkRequirement(
        GameState state,
        StaticLinkRequirementDescriptor descriptor,
        CardInstanceId linkCard,
        PermanentState targetPermanent,
        StaticEffectService? staticEffects)
    {
        var context = new StaticLinkRequirementEvaluationContext(
            state,
            descriptor,
            linkCard,
            targetPermanent,
            staticEffects);

        if (descriptor.SourceCardCondition is not null && !descriptor.SourceCardCondition(context))
        {
            return null;
        }

        if (descriptor.TargetPermanentCondition is not null && !descriptor.TargetPermanentCondition(context))
        {
            return null;
        }

        if (!MatchesMetadata(state, linkCard, descriptor.SourceMetadataCriteria, staticEffects)
            || !MatchesMetadata(state, targetPermanent.TopCardId, descriptor.TargetMetadataCriteria, staticEffects))
        {
            return null;
        }

        if (descriptor.LinkCost < 0)
        {
            return null;
        }

        return new StaticLinkRequirementEvaluation(
            descriptor,
            linkCard,
            targetPermanent.Id,
            Math.Max(0, descriptor.LinkCost));
    }

    private static bool MatchesMetadata(CardMetadataCriteria? criteria, CardDefinition definition) =>
        criteria is null || criteria.Matches(definition);

    private static bool MatchesMetadata(
        GameState state,
        CardInstanceId card,
        CardMetadataCriteria? criteria,
        StaticEffectService? staticEffects) =>
        criteria is null
        || (staticEffects is not null
            ? staticEffects.MatchesCardMetadata(state, card, criteria)
            : criteria.Matches(BattleRules.Definition(state, card)));

    private static bool AppliesToPlayer(
        GameState state,
        StaticEffectPlayerTargetKind appliesTo,
        PlayerId controller,
        PlayerId player) =>
        appliesTo switch
        {
            StaticEffectPlayerTargetKind.OwnerPlayer => player == controller,
            StaticEffectPlayerTargetKind.OpponentPlayer => player == Opponent(state, controller),
            StaticEffectPlayerTargetKind.AnyPlayer => state.Players.Any(candidate => candidate.Id == player),
            _ => false,
        };

    private static PlayerId Opponent(GameState state, PlayerId player)
    {
        foreach (var candidate in state.Players.Select(candidate => candidate.Id))
        {
            if (candidate != player)
            {
                return candidate;
            }
        }

        throw new DomainException($"Opponent for player '{player}' does not exist.");
    }

    private static bool HasExplicitTargetGate(StaticEvolutionRequirementDescriptor descriptor) =>
        descriptor.RequiredColor != CardColor.None
        || descriptor.RequiredLevel >= 0
        || descriptor.MinLevel >= 0
        || descriptor.MaxLevel >= 0
        || descriptor.TargetPermanentCondition is not null
        || descriptor.TargetMetadataCriteria is not null;
}
