using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public enum ContinuousEffectSourceKind
{
    FieldTop,
    InheritedSource,
    LinkedCard,
    FaceUpSecurity,
    Hand,
    Trash,
    Executing,
}

public enum ContinuousEffectTargetKind
{
    SelfPermanent,
    OwnerBattleAreaDigimon,
    OwnerPlayer,
    OpponentPlayer,
}

public enum ContinuousModifierKind
{
    DP,
    SecurityAttack,
    SecurityDigimonDP,
}

public enum StaticCostKind
{
    Play,
    Digivolution,
    Link,
}

public enum StaticCostTargetKind
{
    OwnerCards,
    OpponentCards,
    AnyCard,
}

public enum StaticEffectPermanentTargetKind
{
    SelfPermanent,
    OwnerBattleAreaDigimon,
    OpponentBattleAreaDigimon,
    AnyBattleAreaDigimon,
}

public enum StaticEffectPlayerTargetKind
{
    OwnerPlayer,
    OpponentPlayer,
    AnyPlayer,
}

public enum StaticCardTargetKind
{
    SelfCard,
    OwnerCards,
    OpponentCards,
    AnyCard,
}

public enum StaticCardColorLayer
{
    Base,
    Current,
}

public enum StaticCardNameLayer
{
    Base,
    Current,
}

public enum StaticRestrictionKind
{
    CannotAttack,
    CannotBlock,
    CannotSwitchAttackTarget,
}

public enum StaticCardRestrictionKind
{
    CannotPlay,
    CannotPutField,
    CannotMove,
}

public enum StaticImmunityKind
{
    EffectDeletion,
    EffectReturnToHand,
    EffectReturnToDeck,
    DpReduction,
}

public sealed record ContinuousEffectScriptContext(
    GameState State,
    CardInstanceId SourceCard,
    PermanentId? SourcePermanent,
    PlayerId Controller,
    ContinuousEffectSourceKind SourceKind);

public sealed record ContinuousEffectEvaluationContext(
    GameState State,
    ContinuousEffectDescriptor Descriptor,
    PermanentState? TargetPermanent,
    PlayerId? TargetPlayer);

public sealed record ContinuousKeywordEvaluationContext(
    GameState State,
    ContinuousKeywordDescriptor Descriptor,
    PermanentState TargetPermanent);

public sealed record ContinuousEffectDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    ContinuousEffectTargetKind AppliesTo,
    ContinuousModifierKind ModifierKind,
    Func<ContinuousEffectEvaluationContext, int> Amount,
    Func<ContinuousEffectEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record ContinuousEffectEvaluation(
    ContinuousEffectDescriptor Descriptor,
    PermanentId? TargetPermanentId,
    PlayerId? TargetPlayerId,
    int Amount);

public sealed record ContinuousKeywordDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    BattleKeyword Keyword,
    ContinuousEffectTargetKind AppliesTo,
    Func<ContinuousKeywordEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record ContinuousKeywordEvaluation(
    ContinuousKeywordDescriptor Descriptor,
    PermanentId TargetPermanentId);

public sealed record StaticCostModifierEvaluationContext(
    GameState State,
    StaticCostModifierDescriptor Descriptor,
    CardInstanceId TargetCard,
    PermanentState? TargetPermanent,
    StaticCostKind CostKind,
    Mechanic Mechanic);

public sealed record StaticCostModifierDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCostKind CostKind,
    StaticCostTargetKind AppliesTo,
    Func<StaticCostModifierEvaluationContext, int> Amount,
    Func<StaticCostModifierEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null,
    CardMetadataCriteria? TargetPermanentMetadataCriteria = null);

public sealed record StaticCostModifierEvaluation(
    StaticCostModifierDescriptor Descriptor,
    CardInstanceId TargetCard,
    PermanentId? TargetPermanentId,
    int Amount);

public sealed record StaticRestrictionEvaluationContext(
    GameState State,
    StaticRestrictionDescriptor Descriptor,
    PermanentState TargetPermanent);

public sealed record StaticRestrictionDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticEffectPermanentTargetKind AppliesTo,
    StaticRestrictionKind RestrictionKind,
    Func<StaticRestrictionEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record StaticRestrictionEvaluation(
    StaticRestrictionDescriptor Descriptor,
    PermanentId TargetPermanentId);

public sealed record StaticCardRestrictionEvaluationContext(
    GameState State,
    StaticCardRestrictionDescriptor Descriptor,
    CardInstanceId TargetCard,
    StaticCardRestrictionCause? Cause = null);

public sealed record StaticCardRestrictionCause(
    CardInstanceId? EffectSourceCardId,
    PermanentId? EffectSourcePermanentId,
    PlayerId ControllerPlayerId,
    MoveReason MoveReason);

public sealed record StaticCardRestrictionDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    StaticCardRestrictionKind RestrictionKind,
    Func<StaticCardRestrictionEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record StaticCardRestrictionEvaluation(
    StaticCardRestrictionDescriptor Descriptor,
    CardInstanceId TargetCard);

public sealed record StaticImmunityEvaluationContext(
    GameState State,
    StaticImmunityDescriptor Descriptor,
    PermanentState TargetPermanent);

public sealed record StaticImmunityDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticEffectPermanentTargetKind AppliesTo,
    StaticImmunityKind ImmunityKind,
    Func<StaticImmunityEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record StaticImmunityEvaluation(
    StaticImmunityDescriptor Descriptor,
    PermanentId TargetPermanentId);

public sealed record StaticCardColorEvaluationContext(
    GameState State,
    StaticCardColorDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<CardColor> CurrentColors);

public sealed record StaticCardColorDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    StaticCardColorLayer ColorLayer,
    Func<StaticCardColorEvaluationContext, IReadOnlyList<CardColor>> Apply,
    Func<StaticCardColorEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record StaticCardColorEvaluation(
    StaticCardColorDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<CardColor> Colors);

public sealed record StaticCardNameEvaluationContext(
    GameState State,
    StaticCardNameDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<string> CurrentNames);

public sealed record StaticCardNameDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    StaticCardNameLayer NameLayer,
    Func<StaticCardNameEvaluationContext, IReadOnlyList<string>> Apply,
    Func<StaticCardNameEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record StaticCardNameEvaluation(
    StaticCardNameDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<string> Names);

public sealed record StaticCardTraitEvaluationContext(
    GameState State,
    StaticCardTraitDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<string> CurrentTraits);

public sealed record StaticCardTraitDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    Func<StaticCardTraitEvaluationContext, IReadOnlyList<string>> Apply,
    Func<StaticCardTraitEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record StaticCardTraitEvaluation(
    StaticCardTraitDescriptor Descriptor,
    CardInstanceId TargetCard,
    IReadOnlyList<string> Traits);

public sealed record StaticCardLevelEvaluationContext(
    GameState State,
    StaticCardLevelDescriptor Descriptor,
    CardInstanceId TargetCard,
    int CurrentLevel);

public sealed record StaticCardLevelDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    Func<StaticCardLevelEvaluationContext, int> Apply,
    Func<StaticCardLevelEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record StaticCardLevelEvaluation(
    StaticCardLevelDescriptor Descriptor,
    CardInstanceId TargetCard,
    int Level);

public sealed record StaticPermanentLevelEvaluationContext(
    GameState State,
    StaticPermanentLevelDescriptor Descriptor,
    PermanentState TargetPermanent,
    int CurrentLevel);

public sealed record StaticPermanentLevelDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticEffectPermanentTargetKind AppliesTo,
    Func<StaticPermanentLevelEvaluationContext, int> Apply,
    Func<StaticPermanentLevelEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record StaticPermanentLevelEvaluation(
    StaticPermanentLevelDescriptor Descriptor,
    PermanentId TargetPermanentId,
    int Level);

public sealed record IgnoreColorRequirementEvaluationContext(
    GameState State,
    IgnoreColorRequirementDescriptor Descriptor,
    CardInstanceId TargetCard);

public sealed record IgnoreColorRequirementDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticCardTargetKind AppliesTo,
    Func<IgnoreColorRequirementEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetCardMetadataCriteria = null);

public sealed record IgnoreColorRequirementEvaluation(
    IgnoreColorRequirementDescriptor Descriptor,
    CardInstanceId TargetCard);

public sealed record StaticEvolutionRequirementEvaluationContext(
    GameState State,
    StaticEvolutionRequirementDescriptor Descriptor,
    CardInstanceId EvolvingCard,
    PermanentState TargetPermanent);

public sealed record StaticEvolutionRequirementDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    int Cost,
    CardColor RequiredColor = CardColor.None,
    int RequiredLevel = -1,
    int MinLevel = -1,
    int MaxLevel = -1,
    bool IgnoreDigivolutionRequirement = false,
    Func<StaticEvolutionRequirementEvaluationContext, bool>? SourceCardCondition = null,
    Func<StaticEvolutionRequirementEvaluationContext, bool>? TargetPermanentCondition = null,
    Func<StaticEvolutionRequirementEvaluationContext, int>? CostEquation = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record StaticEvolutionRequirementEvaluation(
    StaticEvolutionRequirementDescriptor Descriptor,
    CardInstanceId EvolvingCard,
    PermanentId TargetPermanentId,
    int Cost,
    bool IgnoresDigivolutionRequirement = false);

public sealed record CannotIgnoreDigivolutionRequirementEvaluationContext(
    GameState State,
    CannotIgnoreDigivolutionRequirementDescriptor Descriptor,
    PlayerId Player,
    PermanentState TargetPermanent,
    CardInstanceId EvolvingCard);

public sealed record CannotIgnoreDigivolutionRequirementDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    StaticEffectPlayerTargetKind AppliesTo,
    Func<CannotIgnoreDigivolutionRequirementEvaluationContext, bool>? Condition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null,
    CardMetadataCriteria? EvolvingCardMetadataCriteria = null);

public sealed record CannotIgnoreDigivolutionRequirementEvaluation(
    CannotIgnoreDigivolutionRequirementDescriptor Descriptor,
    PlayerId Player,
    PermanentId TargetPermanentId,
    CardInstanceId EvolvingCard);

public sealed record StaticLinkRequirementEvaluationContext(
    GameState State,
    StaticLinkRequirementDescriptor Descriptor,
    CardInstanceId LinkCard,
    PermanentState TargetPermanent,
    StaticEffectService? StaticEffects = null);

public sealed record StaticLinkRequirementDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    int LinkCost,
    Func<StaticLinkRequirementEvaluationContext, bool>? SourceCardCondition = null,
    Func<StaticLinkRequirementEvaluationContext, bool>? TargetPermanentCondition = null,
    string DebugLabel = "",
    CardMetadataCriteria? SourceMetadataCriteria = null,
    CardMetadataCriteria? TargetMetadataCriteria = null);

public sealed record StaticLinkRequirementEvaluation(
    StaticLinkRequirementDescriptor Descriptor,
    CardInstanceId LinkCard,
    PermanentId TargetPermanentId,
    int Cost);
