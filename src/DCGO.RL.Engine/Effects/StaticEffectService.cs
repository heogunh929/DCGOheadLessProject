using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed class StaticEffectService
{
    public static StaticEffectService NoStaticEffects { get; } = new();

    private readonly ContinuousEffectSourceCollector? _sourceCollector;

    public StaticEffectService()
    {
    }

    public StaticEffectService(ICardScriptRegistry cardScriptRegistry)
        : this(new ContinuousEffectSourceCollector(cardScriptRegistry))
    {
    }

    public StaticEffectService(ContinuousEffectSourceCollector sourceCollector)
    {
        _sourceCollector = sourceCollector ?? throw new ArgumentNullException(nameof(sourceCollector));
    }

    public int ApplyCostModifiers(
        GameState state,
        CardInstanceId targetCard,
        int baseCost,
        StaticCostKind costKind,
        PermanentState? targetPermanent = null,
        Mechanic mechanic = Mechanic.Normal)
    {
        if (baseCost < 0)
        {
            throw new DomainException("Base cost must not be negative.");
        }

        var totalDelta = EvaluateCostModifiers(state, targetCard, costKind, targetPermanent, mechanic)
            .Sum(evaluation => evaluation.Amount);
        return Math.Max(0, baseCost + totalDelta);
    }

    public IReadOnlyList<StaticCostModifierEvaluation> EvaluateCostModifiers(
        GameState state,
        CardInstanceId targetCard,
        StaticCostKind costKind,
        PermanentState? targetPermanent = null,
        Mechanic mechanic = Mechanic.Normal)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCostModifierEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static cost target card '{targetCard}' does not exist.");
        }

        _ = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCostModifiers(state)
            .Where(descriptor => descriptor.CostKind == costKind)
            .Where(descriptor => AppliesToCardOwner(state, descriptor, targetInstance.Owner))
            .Where(descriptor => MatchesCardMetadata(state, targetCard, descriptor.TargetCardMetadataCriteria))
            .Where(descriptor => MatchesTargetPermanentMetadata(state, descriptor.TargetPermanentMetadataCriteria, targetPermanent))
            .Select(descriptor => EvaluateCostModifier(state, descriptor, targetCard, targetPermanent, costKind, mechanic))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .Where(evaluation => evaluation.Amount != 0)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public bool HasRestriction(GameState state, PermanentState target, StaticRestrictionKind restrictionKind) =>
        EvaluateRestrictions(state, target, restrictionKind).Count > 0;

    public IReadOnlyList<StaticRestrictionEvaluation> EvaluateRestrictions(
        GameState state,
        PermanentState target,
        StaticRestrictionKind restrictionKind)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(target);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticRestrictionEvaluation>();
        }

        return _sourceCollector.CollectStaticRestrictions(state)
            .Where(descriptor => descriptor.RestrictionKind == restrictionKind)
            .Where(descriptor => AppliesToPermanent(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourcePermanentId, target))
            .Where(descriptor => MatchesCardMetadata(state, target.TopCardId, descriptor.TargetMetadataCriteria))
            .Select(descriptor => EvaluateRestriction(state, descriptor, target))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public bool HasCardRestriction(
        GameState state,
        CardInstanceId targetCard,
        StaticCardRestrictionKind restrictionKind,
        StaticCardRestrictionCause? cause = null) =>
        EvaluateCardRestrictions(state, targetCard, restrictionKind, cause).Count > 0;

    public IReadOnlyList<StaticCardRestrictionEvaluation> EvaluateCardRestrictions(
        GameState state,
        CardInstanceId targetCard,
        StaticCardRestrictionKind restrictionKind,
        StaticCardRestrictionCause? cause = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCardRestrictionEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static card restriction target card '{targetCard}' does not exist.");
        }

        _ = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCardRestrictions(state)
            .Where(descriptor => descriptor.RestrictionKind == restrictionKind)
            .Where(descriptor => AppliesToCard(
                state,
                descriptor.AppliesTo,
                descriptor.ControllerPlayerId,
                descriptor.SourceCardId,
                targetCard,
                targetInstance.Owner))
            .Where(descriptor => MatchesCardMetadata(state, targetCard, descriptor.TargetCardMetadataCriteria))
            .Select(descriptor => EvaluateCardRestriction(state, descriptor, targetCard, cause))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public bool HasImmunity(GameState state, PermanentState target, StaticImmunityKind immunityKind) =>
        EvaluateImmunities(state, target, immunityKind).Count > 0;

    public IReadOnlyList<StaticImmunityEvaluation> EvaluateImmunities(
        GameState state,
        PermanentState target,
        StaticImmunityKind immunityKind)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(target);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticImmunityEvaluation>();
        }

        return _sourceCollector.CollectStaticImmunities(state)
            .Where(descriptor => descriptor.ImmunityKind == immunityKind)
            .Where(descriptor => AppliesToPermanent(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourcePermanentId, target))
            .Where(descriptor => MatchesCardMetadata(state, target.TopCardId, descriptor.TargetMetadataCriteria))
            .Select(descriptor => EvaluateImmunity(state, descriptor, target))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<CardColor> EffectiveCardColors(GameState state, CardInstanceId targetCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        var definition = BattleRules.Definition(state, targetCard);
        var colors = ApplyColorLayer(state, targetCard, definition.CardColors, StaticCardColorLayer.Base);
        return ApplyColorLayer(state, targetCard, colors, StaticCardColorLayer.Current);
    }

    public IReadOnlyList<CardColor> EffectiveOptionColorRequirements(GameState state, CardInstanceId targetCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        var definition = BattleRules.Definition(state, targetCard);
        var baseColors = definition.OptionCardColorRequirements.Count > 0
            ? definition.OptionCardColorRequirements
            : definition.CardColors;
        var colors = ApplyColorLayer(state, targetCard, baseColors, StaticCardColorLayer.Base);
        return ApplyColorLayer(state, targetCard, colors, StaticCardColorLayer.Current);
    }

    public CardMetadataSnapshot EffectiveCardMetadata(GameState state, CardInstanceId targetCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        var definition = BattleRules.Definition(state, targetCard);
        var baseNames = new[] { definition.CardNameEnglish, definition.CardNameJapanese }
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .ToArray();
        var names = ApplyNameLayer(state, targetCard, baseNames, StaticCardNameLayer.Base);
        names = ApplyNameLayer(state, targetCard, names, StaticCardNameLayer.Current);
        var traits = ApplyTraitLayer(state, targetCard, definition.Traits);

        return new CardMetadataSnapshot(
            CardNames: names,
            Traits: traits,
            CardTextEnglish: definition.CardTextEnglish,
            CardTextJapanese: definition.CardTextJapanese);
    }

    public IReadOnlyList<string> EffectiveCardNames(GameState state, CardInstanceId targetCard) =>
        EffectiveCardMetadata(state, targetCard).CardNames;

    public IReadOnlyList<string> EffectiveCardTraits(GameState state, CardInstanceId targetCard) =>
        EffectiveCardMetadata(state, targetCard).Traits;

    public int EffectiveCardLevel(GameState state, CardInstanceId targetCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        var definition = BattleRules.Definition(state, targetCard);
        return ApplyCardLevelLayer(state, targetCard, definition.Level);
    }

    public int EffectivePermanentLevel(GameState state, PermanentState targetPermanent)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(targetPermanent);

        return ApplyPermanentLevelLayer(
            state,
            targetPermanent,
            EffectiveCardLevel(state, targetPermanent.TopCardId));
    }

    public bool MatchesCardMetadata(GameState state, CardInstanceId targetCard, CardMetadataCriteria? criteria) =>
        criteria is null || criteria.Matches(EffectiveCardMetadata(state, targetCard));

    public bool MatchesOptionColorRequirement(GameState state, PlayerId player, CardInstanceId optionCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        var definition = BattleRules.Definition(state, optionCard);
        if (!definition.CardKinds.Contains(CardKind.Option))
        {
            return true;
        }

        if (EvaluateIgnoreColorRequirements(state, optionCard).Count > 0)
        {
            return true;
        }

        var requiredColors = EffectiveOptionColorRequirements(state, optionCard)
            .Where(color => color != CardColor.None)
            .Distinct()
            .ToArray();
        if (requiredColors.Length == 0)
        {
            return true;
        }

        var playerState = state.GetPlayer(player);
        return requiredColors.All(requiredColor =>
            playerState.FieldPermanents.Any(permanent =>
                BattleRules.IsPermanentCard(state, permanent.TopCardId)
                && EffectiveCardColors(state, permanent.TopCardId).Contains(requiredColor)));
    }

    public IReadOnlyList<StaticCardColorEvaluation> EvaluateCardColors(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<CardColor> currentColors,
        StaticCardColorLayer colorLayer)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(currentColors);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCardColorEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static card color target card '{targetCard}' does not exist.");
        }

        _ = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCardColors(state)
            .Where(descriptor => descriptor.ColorLayer == colorLayer)
            .Where(descriptor => AppliesToCard(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourceCardId, targetCard, targetInstance.Owner))
            .Where(descriptor => MatchesCardMetadata(state, targetCard, descriptor.TargetCardMetadataCriteria))
            .Select(descriptor => EvaluateCardColor(state, descriptor, targetCard, currentColors))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<IgnoreColorRequirementEvaluation> EvaluateIgnoreColorRequirements(
        GameState state,
        CardInstanceId targetCard)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (_sourceCollector is null)
        {
            return Array.Empty<IgnoreColorRequirementEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Ignore color requirement target card '{targetCard}' does not exist.");
        }

        _ = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectIgnoreColorRequirements(state)
            .Where(descriptor => AppliesToCard(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourceCardId, targetCard, targetInstance.Owner))
            .Where(descriptor => MatchesCardMetadata(state, targetCard, descriptor.TargetCardMetadataCriteria))
            .Select(descriptor => EvaluateIgnoreColorRequirement(state, descriptor, targetCard))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardNameEvaluation> EvaluateCardNames(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<string> currentNames,
        StaticCardNameLayer nameLayer)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(currentNames);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCardNameEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static card name target card '{targetCard}' does not exist.");
        }

        var targetDefinition = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCardNames(state)
            .Where(descriptor => descriptor.NameLayer == nameLayer)
            .Where(descriptor => AppliesToCard(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourceCardId, targetCard, targetInstance.Owner))
            .Where(descriptor => MatchesMetadata(descriptor.TargetCardMetadataCriteria, targetDefinition))
            .Select(descriptor => EvaluateCardName(state, descriptor, targetCard, currentNames))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardTraitEvaluation> EvaluateCardTraits(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<string> currentTraits)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(currentTraits);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCardTraitEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static card trait target card '{targetCard}' does not exist.");
        }

        var targetDefinition = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCardTraits(state)
            .Where(descriptor => AppliesToCard(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourceCardId, targetCard, targetInstance.Owner))
            .Where(descriptor => MatchesMetadata(descriptor.TargetCardMetadataCriteria, targetDefinition))
            .Select(descriptor => EvaluateCardTrait(state, descriptor, targetCard, currentTraits))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticCardLevelEvaluation> EvaluateCardLevels(
        GameState state,
        CardInstanceId targetCard,
        int currentLevel)
    {
        ArgumentNullException.ThrowIfNull(state);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticCardLevelEvaluation>();
        }

        if (!state.Cards.TryGetValue(targetCard, out var targetInstance))
        {
            throw new DomainException($"Static card level target card '{targetCard}' does not exist.");
        }

        var targetDefinition = BattleRules.Definition(state, targetCard);
        return _sourceCollector.CollectStaticCardLevels(state)
            .Where(descriptor => AppliesToCard(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourceCardId, targetCard, targetInstance.Owner))
            .Where(descriptor => MatchesMetadata(descriptor.TargetCardMetadataCriteria, targetDefinition))
            .Select(descriptor => EvaluateCardLevel(state, descriptor, targetCard, currentLevel))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<StaticPermanentLevelEvaluation> EvaluatePermanentLevels(
        GameState state,
        PermanentState targetPermanent,
        int currentLevel)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(targetPermanent);

        if (_sourceCollector is null)
        {
            return Array.Empty<StaticPermanentLevelEvaluation>();
        }

        return _sourceCollector.CollectStaticPermanentLevels(state)
            .Where(descriptor => AppliesToPermanent(state, descriptor.AppliesTo, descriptor.ControllerPlayerId, descriptor.SourcePermanentId, targetPermanent))
            .Where(descriptor => MatchesTargetPermanentMetadata(state, descriptor.TargetMetadataCriteria, targetPermanent))
            .Select(descriptor => EvaluatePermanentLevel(state, descriptor, targetPermanent, currentLevel))
            .Where(evaluation => evaluation is not null)
            .Select(evaluation => evaluation!)
            .DistinctBy(evaluation => evaluation.Descriptor.StableId)
            .OrderBy(evaluation => evaluation.Descriptor.StableId, StringComparer.Ordinal)
            .ToArray();
    }

    private static StaticCostModifierEvaluation? EvaluateCostModifier(
        GameState state,
        StaticCostModifierDescriptor descriptor,
        CardInstanceId targetCard,
        PermanentState? targetPermanent,
        StaticCostKind costKind,
        Mechanic mechanic)
    {
        var context = new StaticCostModifierEvaluationContext(
            state,
            descriptor,
            targetCard,
            targetPermanent,
            costKind,
            mechanic);

        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCostModifierEvaluation(
            descriptor,
            targetCard,
            targetPermanent?.Id,
            descriptor.Amount(context));
    }

    private static StaticRestrictionEvaluation? EvaluateRestriction(
        GameState state,
        StaticRestrictionDescriptor descriptor,
        PermanentState target)
    {
        var context = new StaticRestrictionEvaluationContext(state, descriptor, target);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticRestrictionEvaluation(descriptor, target.Id);
    }

    private static StaticCardRestrictionEvaluation? EvaluateCardRestriction(
        GameState state,
        StaticCardRestrictionDescriptor descriptor,
        CardInstanceId targetCard,
        StaticCardRestrictionCause? cause)
    {
        var context = new StaticCardRestrictionEvaluationContext(state, descriptor, targetCard, cause);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCardRestrictionEvaluation(descriptor, targetCard);
    }

    private static StaticImmunityEvaluation? EvaluateImmunity(
        GameState state,
        StaticImmunityDescriptor descriptor,
        PermanentState target)
    {
        var context = new StaticImmunityEvaluationContext(state, descriptor, target);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticImmunityEvaluation(descriptor, target.Id);
    }

    private IReadOnlyList<CardColor> ApplyColorLayer(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<CardColor> baseColors,
        StaticCardColorLayer colorLayer)
    {
        var colors = NormalizeColors(baseColors);
        foreach (var evaluation in EvaluateCardColors(state, targetCard, colors, colorLayer))
        {
            colors = NormalizeColors(evaluation.Colors);
        }

        return colors;
    }

    private IReadOnlyList<string> ApplyNameLayer(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<string> baseNames,
        StaticCardNameLayer nameLayer)
    {
        var names = NormalizeStrings(baseNames);
        foreach (var evaluation in EvaluateCardNames(state, targetCard, names, nameLayer))
        {
            names = NormalizeStrings(evaluation.Names);
        }

        return names;
    }

    private IReadOnlyList<string> ApplyTraitLayer(
        GameState state,
        CardInstanceId targetCard,
        IReadOnlyList<string> baseTraits)
    {
        var traits = NormalizeStrings(baseTraits);
        foreach (var evaluation in EvaluateCardTraits(state, targetCard, traits))
        {
            traits = NormalizeStrings(evaluation.Traits);
        }

        return traits;
    }

    private int ApplyCardLevelLayer(
        GameState state,
        CardInstanceId targetCard,
        int baseLevel)
    {
        var level = baseLevel;
        foreach (var evaluation in EvaluateCardLevels(state, targetCard, level))
        {
            level = evaluation.Level;
        }

        return level;
    }

    private int ApplyPermanentLevelLayer(
        GameState state,
        PermanentState targetPermanent,
        int baseLevel)
    {
        var level = baseLevel;
        foreach (var evaluation in EvaluatePermanentLevels(state, targetPermanent, level))
        {
            level = evaluation.Level;
        }

        return level;
    }

    private static StaticCardColorEvaluation? EvaluateCardColor(
        GameState state,
        StaticCardColorDescriptor descriptor,
        CardInstanceId targetCard,
        IReadOnlyList<CardColor> currentColors)
    {
        var context = new StaticCardColorEvaluationContext(state, descriptor, targetCard, currentColors);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCardColorEvaluation(
            descriptor,
            targetCard,
            NormalizeColors(descriptor.Apply(context)));
    }

    private static StaticCardNameEvaluation? EvaluateCardName(
        GameState state,
        StaticCardNameDescriptor descriptor,
        CardInstanceId targetCard,
        IReadOnlyList<string> currentNames)
    {
        var context = new StaticCardNameEvaluationContext(state, descriptor, targetCard, currentNames);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCardNameEvaluation(
            descriptor,
            targetCard,
            NormalizeStrings(descriptor.Apply(context)));
    }

    private static StaticCardTraitEvaluation? EvaluateCardTrait(
        GameState state,
        StaticCardTraitDescriptor descriptor,
        CardInstanceId targetCard,
        IReadOnlyList<string> currentTraits)
    {
        var context = new StaticCardTraitEvaluationContext(state, descriptor, targetCard, currentTraits);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCardTraitEvaluation(
            descriptor,
            targetCard,
            NormalizeStrings(descriptor.Apply(context)));
    }

    private static StaticCardLevelEvaluation? EvaluateCardLevel(
        GameState state,
        StaticCardLevelDescriptor descriptor,
        CardInstanceId targetCard,
        int currentLevel)
    {
        var context = new StaticCardLevelEvaluationContext(state, descriptor, targetCard, currentLevel);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticCardLevelEvaluation(
            descriptor,
            targetCard,
            descriptor.Apply(context));
    }

    private static StaticPermanentLevelEvaluation? EvaluatePermanentLevel(
        GameState state,
        StaticPermanentLevelDescriptor descriptor,
        PermanentState targetPermanent,
        int currentLevel)
    {
        var context = new StaticPermanentLevelEvaluationContext(state, descriptor, targetPermanent, currentLevel);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new StaticPermanentLevelEvaluation(
            descriptor,
            targetPermanent.Id,
            descriptor.Apply(context));
    }

    private static IgnoreColorRequirementEvaluation? EvaluateIgnoreColorRequirement(
        GameState state,
        IgnoreColorRequirementDescriptor descriptor,
        CardInstanceId targetCard)
    {
        var context = new IgnoreColorRequirementEvaluationContext(state, descriptor, targetCard);
        if (descriptor.Condition is not null && !descriptor.Condition(context))
        {
            return null;
        }

        return new IgnoreColorRequirementEvaluation(descriptor, targetCard);
    }

    private static IReadOnlyList<CardColor> NormalizeColors(IEnumerable<CardColor> colors) =>
        colors
            .Distinct()
            .ToArray();

    private static IReadOnlyList<string> NormalizeStrings(IEnumerable<string> values) =>
        values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();

    private static bool AppliesToCardOwner(
        GameState state,
        StaticCostModifierDescriptor descriptor,
        PlayerId targetOwner) =>
        descriptor.AppliesTo switch
        {
            StaticCostTargetKind.OwnerCards => targetOwner == descriptor.ControllerPlayerId,
            StaticCostTargetKind.OpponentCards => targetOwner == Opponent(state, descriptor.ControllerPlayerId),
            StaticCostTargetKind.AnyCard => state.Players.Any(player => player.Id == targetOwner),
            _ => false,
        };

    private static bool AppliesToCard(
        GameState state,
        StaticCardTargetKind appliesTo,
        PlayerId controller,
        CardInstanceId? sourceCard,
        CardInstanceId targetCard,
        PlayerId targetOwner) =>
        appliesTo switch
        {
            StaticCardTargetKind.SelfCard => sourceCard == targetCard,
            StaticCardTargetKind.OwnerCards => targetOwner == controller,
            StaticCardTargetKind.OpponentCards => targetOwner == Opponent(state, controller),
            StaticCardTargetKind.AnyCard => state.Players.Any(player => player.Id == targetOwner),
            _ => false,
        };

    private static bool AppliesToPermanent(
        GameState state,
        StaticEffectPermanentTargetKind appliesTo,
        PlayerId controller,
        PermanentId? sourcePermanent,
        PermanentState target) =>
        appliesTo switch
        {
            StaticEffectPermanentTargetKind.SelfPermanent =>
                sourcePermanent == target.Id,
            StaticEffectPermanentTargetKind.OwnerBattleAreaDigimon =>
                target.ControllerPlayerId == controller
                && !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId),
            StaticEffectPermanentTargetKind.OpponentBattleAreaDigimon =>
                target.ControllerPlayerId == Opponent(state, controller)
                && !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId),
            StaticEffectPermanentTargetKind.AnyBattleAreaDigimon =>
                !target.IsBreedingArea
                && BattleRules.IsDigimon(state, target.TopCardId),
            _ => false,
        };

    private bool MatchesTargetPermanentMetadata(
        GameState state,
        CardMetadataCriteria? criteria,
        PermanentState? targetPermanent) =>
        criteria is null
        || (targetPermanent is not null
            && MatchesCardMetadata(state, targetPermanent.TopCardId, criteria));

    private static bool MatchesMetadata(CardMetadataCriteria? criteria, CardDefinition definition) =>
        criteria is null || criteria.Matches(definition);

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
