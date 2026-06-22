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
        PermanentState targetPermanent)
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
            .Select(descriptor => EvaluateEvolutionRequirement(state, descriptor, evolvingCard, targetPermanent))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public StaticEvolutionRequirementEvaluation? FirstEvolutionRequirement(
        GameState state,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent) =>
        EvaluateEvolutionRequirements(state, evolvingCard, targetPermanent).FirstOrDefault();

    public IReadOnlyList<StaticLinkRequirementEvaluation> EvaluateLinkRequirements(
        GameState state,
        CardInstanceId linkCard,
        PermanentState targetPermanent)
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
            .Select(descriptor => EvaluateLinkRequirement(state, descriptor, linkCard, targetPermanent))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public StaticLinkRequirementEvaluation? FirstLinkRequirement(
        GameState state,
        CardInstanceId linkCard,
        PermanentState targetPermanent) =>
        EvaluateLinkRequirements(state, linkCard, targetPermanent).FirstOrDefault();

    private static StaticEvolutionRequirementEvaluation? EvaluateEvolutionRequirement(
        GameState state,
        StaticEvolutionRequirementDescriptor descriptor,
        CardInstanceId evolvingCard,
        PermanentState targetPermanent)
    {
        if (descriptor.IgnoreDigivolutionRequirement)
        {
            throw new UnsupportedMechanicException(
                $"Static evolution requirement '{descriptor.StableId}' uses ignore digivolution requirement permission.");
        }

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

        var targetDefinition = BattleRules.Definition(state, targetPermanent.TopCardId);
        if (descriptor.RequiredColor != CardColor.None
            && !targetDefinition.CardColors.Contains(descriptor.RequiredColor))
        {
            return null;
        }

        if (descriptor.RequiredLevel >= 0 && targetDefinition.Level != descriptor.RequiredLevel)
        {
            return null;
        }

        if (descriptor.MinLevel >= 0 && targetDefinition.Level < descriptor.MinLevel)
        {
            return null;
        }

        if (descriptor.MaxLevel >= 0 && targetDefinition.Level > descriptor.MaxLevel)
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
            Math.Max(0, cost));
    }

    private static StaticLinkRequirementEvaluation? EvaluateLinkRequirement(
        GameState state,
        StaticLinkRequirementDescriptor descriptor,
        CardInstanceId linkCard,
        PermanentState targetPermanent)
    {
        var context = new StaticLinkRequirementEvaluationContext(
            state,
            descriptor,
            linkCard,
            targetPermanent);

        if (descriptor.SourceCardCondition is not null && !descriptor.SourceCardCondition(context))
        {
            return null;
        }

        if (descriptor.TargetPermanentCondition is not null && !descriptor.TargetPermanentCondition(context))
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
}
