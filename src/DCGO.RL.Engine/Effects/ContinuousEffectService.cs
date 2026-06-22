using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public interface IContinuousCardScript
{
    IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context);
}

public interface IContinuousKeywordCardScript
{
    IReadOnlyList<ContinuousKeywordDescriptor> CreateContinuousKeywordDescriptors(ContinuousEffectScriptContext context);
}

public interface IStaticEvolutionRequirementCardScript
{
    IReadOnlyList<StaticEvolutionRequirementDescriptor> CreateStaticEvolutionRequirementDescriptors(
        ContinuousEffectScriptContext context);
}

public interface ICannotIgnoreDigivolutionRequirementCardScript
{
    IReadOnlyList<CannotIgnoreDigivolutionRequirementDescriptor> CreateCannotIgnoreDigivolutionRequirementDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticLinkRequirementCardScript
{
    IReadOnlyList<StaticLinkRequirementDescriptor> CreateStaticLinkRequirementDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCostModifierCardScript
{
    IReadOnlyList<StaticCostModifierDescriptor> CreateStaticCostModifierDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticRestrictionCardScript
{
    IReadOnlyList<StaticRestrictionDescriptor> CreateStaticRestrictionDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCardRestrictionCardScript
{
    IReadOnlyList<StaticCardRestrictionDescriptor> CreateStaticCardRestrictionDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticImmunityCardScript
{
    IReadOnlyList<StaticImmunityDescriptor> CreateStaticImmunityDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCardColorCardScript
{
    IReadOnlyList<StaticCardColorDescriptor> CreateStaticCardColorDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCardNameCardScript
{
    IReadOnlyList<StaticCardNameDescriptor> CreateStaticCardNameDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCardTraitCardScript
{
    IReadOnlyList<StaticCardTraitDescriptor> CreateStaticCardTraitDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticCardLevelCardScript
{
    IReadOnlyList<StaticCardLevelDescriptor> CreateStaticCardLevelDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IStaticPermanentLevelCardScript
{
    IReadOnlyList<StaticPermanentLevelDescriptor> CreateStaticPermanentLevelDescriptors(
        ContinuousEffectScriptContext context);
}

public interface IIgnoreColorRequirementCardScript
{
    IReadOnlyList<IgnoreColorRequirementDescriptor> CreateIgnoreColorRequirementDescriptors(
        ContinuousEffectScriptContext context);
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
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<ContinuousKeywordDescriptor> CollectKeywords(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<ContinuousKeywordDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IContinuousKeywordCardScript keywordScript)
            {
                continue;
            }

            descriptors.AddRange(keywordScript.CreateContinuousKeywordDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticEvolutionRequirementDescriptor> CollectStaticEvolutionRequirements(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticEvolutionRequirementDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticEvolutionRequirementCardScript requirementScript)
            {
                continue;
            }

            descriptors.AddRange(requirementScript.CreateStaticEvolutionRequirementDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<CannotIgnoreDigivolutionRequirementDescriptor> CollectCannotIgnoreDigivolutionRequirements(
        GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<CannotIgnoreDigivolutionRequirementDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not ICannotIgnoreDigivolutionRequirementCardScript restrictionScript)
            {
                continue;
            }

            descriptors.AddRange(restrictionScript.CreateCannotIgnoreDigivolutionRequirementDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticLinkRequirementDescriptor> CollectStaticLinkRequirements(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticLinkRequirementDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticLinkRequirementCardScript requirementScript)
            {
                continue;
            }

            descriptors.AddRange(requirementScript.CreateStaticLinkRequirementDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCostModifierDescriptor> CollectStaticCostModifiers(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCostModifierDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCostModifierCardScript costScript)
            {
                continue;
            }

            descriptors.AddRange(costScript.CreateStaticCostModifierDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticRestrictionDescriptor> CollectStaticRestrictions(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticRestrictionDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticRestrictionCardScript restrictionScript)
            {
                continue;
            }

            descriptors.AddRange(restrictionScript.CreateStaticRestrictionDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardRestrictionDescriptor> CollectStaticCardRestrictions(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCardRestrictionDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCardRestrictionCardScript restrictionScript)
            {
                continue;
            }

            descriptors.AddRange(restrictionScript.CreateStaticCardRestrictionDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticImmunityDescriptor> CollectStaticImmunities(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticImmunityDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticImmunityCardScript immunityScript)
            {
                continue;
            }

            descriptors.AddRange(immunityScript.CreateStaticImmunityDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardColorDescriptor> CollectStaticCardColors(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCardColorDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCardColorCardScript colorScript)
            {
                continue;
            }

            descriptors.AddRange(colorScript.CreateStaticCardColorDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardNameDescriptor> CollectStaticCardNames(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCardNameDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCardNameCardScript nameScript)
            {
                continue;
            }

            descriptors.AddRange(nameScript.CreateStaticCardNameDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardTraitDescriptor> CollectStaticCardTraits(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCardTraitDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCardTraitCardScript traitScript)
            {
                continue;
            }

            descriptors.AddRange(traitScript.CreateStaticCardTraitDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardLevelDescriptor> CollectStaticCardLevels(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticCardLevelDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticCardLevelCardScript levelScript)
            {
                continue;
            }

            descriptors.AddRange(levelScript.CreateStaticCardLevelDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticPermanentLevelDescriptor> CollectStaticPermanentLevels(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<StaticPermanentLevelDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IStaticPermanentLevelCardScript levelScript)
            {
                continue;
            }

            descriptors.AddRange(levelScript.CreateStaticPermanentLevelDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
        }

        return descriptors
            .OrderBy(descriptor => descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<IgnoreColorRequirementDescriptor> CollectIgnoreColorRequirements(GameState state)
    {
        ArgumentNullException.ThrowIfNull(state);

        var descriptors = new List<IgnoreColorRequirementDescriptor>();
        foreach (var source in EnumerateSources(state))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            if (script is not IIgnoreColorRequirementCardScript ignoreScript)
            {
                continue;
            }

            descriptors.AddRange(ignoreScript.CreateIgnoreColorRequirementDescriptors(
                new ContinuousEffectScriptContext(
                    state,
                    source.Card,
                    source.Permanent?.Id,
                    source.ControllerPlayerId,
                    source.Kind))
                .Where(descriptor => MatchesMetadata(descriptor.SourceMetadataCriteria, definition)));
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

                foreach (var linkedCard in permanent.LinkedCards)
                {
                    yield return CreateSource(state, linkedCard, permanent, ContinuousEffectSourceKind.LinkedCard);
                }
            }

            foreach (var securityCard in player.Security.Where(card => state.Cards[card].IsFaceUp))
            {
                yield return CreatePlayerZoneSource(state, securityCard, ContinuousEffectSourceKind.FaceUpSecurity);
            }

            foreach (var handCard in player.Hand)
            {
                yield return CreatePlayerZoneSource(state, handCard, ContinuousEffectSourceKind.Hand);
            }

            foreach (var trashCard in player.Trash)
            {
                yield return CreatePlayerZoneSource(state, trashCard, ContinuousEffectSourceKind.Trash);
            }

            foreach (var executingCard in player.Executing)
            {
                yield return CreatePlayerZoneSource(state, executingCard, ContinuousEffectSourceKind.Executing);
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

    private static ContinuousSource CreatePlayerZoneSource(
        GameState state,
        CardInstanceId card,
        ContinuousEffectSourceKind kind)
    {
        if (!state.Cards.TryGetValue(card, out var instance))
        {
            throw new DomainException($"Continuous effect source card '{card}' does not exist.");
        }

        if (instance.PermanentId is not null)
        {
            throw new DomainException(
                $"Continuous effect player-zone source card '{card}' should not belong to permanent '{instance.PermanentId}'.");
        }

        return new ContinuousSource(card, instance, Permanent: null, kind);
    }

    private sealed record ContinuousSource(
        CardInstanceId Card,
        CardInstance Instance,
        PermanentState? Permanent,
        ContinuousEffectSourceKind Kind)
    {
        public PlayerId ControllerPlayerId => Permanent?.ControllerPlayerId ?? Instance.Owner;
    }

    private static bool MatchesMetadata(CardMetadataCriteria? criteria, CardDefinition definition) =>
        criteria is null || criteria.Matches(definition);
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

    public IReadOnlyList<ContinuousKeywordEvaluation> EvaluateKeywordsForPermanent(
        GameState state,
        PermanentState target,
        BattleKeyword keyword)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(target);

        return _sourceCollector.CollectKeywords(state)
            .Where(descriptor => descriptor.Keyword == keyword)
            .Where(descriptor => AppliesToPermanent(state, descriptor, target))
            .Select(descriptor => EvaluateKeyword(state, descriptor, target))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<ContinuousKeywordEvaluation> EvaluateKeywordsForPermanent(
        GameState state,
        PermanentState target)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(target);

        return _sourceCollector.CollectKeywords(state)
            .Where(descriptor => AppliesToPermanent(state, descriptor, target))
            .Select(descriptor => EvaluateKeyword(state, descriptor, target))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
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

    private static ContinuousKeywordEvaluation? EvaluateKeyword(
        GameState state,
        ContinuousKeywordDescriptor descriptor,
        PermanentState targetPermanent)
    {
        var context = new ContinuousKeywordEvaluationContext(state, descriptor, targetPermanent);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new ContinuousKeywordEvaluation(descriptor, targetPermanent.Id);
    }

    private static bool AppliesToPermanent(
        GameState state,
        ContinuousEffectDescriptor descriptor,
        PermanentState target)
    {
        return descriptor.AppliesTo switch
        {
            ContinuousEffectTargetKind.SelfPermanent =>
                descriptor.SourcePermanentId == target.Id
                && MatchesTargetMetadata(state, descriptor.TargetMetadataCriteria, target),
            ContinuousEffectTargetKind.OwnerBattleAreaDigimon =>
                target.ControllerPlayerId == descriptor.ControllerPlayerId
                && !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId)
                && MatchesTargetMetadata(state, descriptor.TargetMetadataCriteria, target),
            _ => false,
        };
    }

    private static bool AppliesToPermanent(
        GameState state,
        ContinuousKeywordDescriptor descriptor,
        PermanentState target)
    {
        return descriptor.AppliesTo switch
        {
            ContinuousEffectTargetKind.SelfPermanent =>
                descriptor.SourcePermanentId == target.Id
                && MatchesTargetMetadata(state, descriptor.TargetMetadataCriteria, target),
            ContinuousEffectTargetKind.OwnerBattleAreaDigimon =>
                target.ControllerPlayerId == descriptor.ControllerPlayerId
                && !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId)
                && MatchesTargetMetadata(state, descriptor.TargetMetadataCriteria, target),
            _ => false,
        };
    }

    private static bool MatchesTargetMetadata(
        GameState state,
        CardMetadataCriteria? criteria,
        PermanentState target) =>
        criteria is null || criteria.Matches(BattleRules.Definition(state, target.TopCardId));

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

    public bool HasContinuousKeyword(GameState state, PermanentState permanent, BattleKeyword keyword) =>
        _continuousEffectService?.EvaluateKeywordsForPermanent(state, permanent, keyword).Count > 0;

    public IReadOnlyList<BattleKeyword> ContinuousKeywords(GameState state, PermanentState permanent) =>
        _continuousEffectService?.EvaluateKeywordsForPermanent(state, permanent)
            .Select(evaluation => evaluation.Descriptor.Keyword)
            .Distinct()
            .OrderBy(keyword => keyword)
            .ToArray()
        ?? Array.Empty<BattleKeyword>();
}
