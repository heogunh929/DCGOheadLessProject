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
    string DebugLabel = "");

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
    string DebugLabel = "");

public sealed record ContinuousKeywordEvaluation(
    ContinuousKeywordDescriptor Descriptor,
    PermanentId TargetPermanentId);

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
    string DebugLabel = "");

public sealed record StaticEvolutionRequirementEvaluation(
    StaticEvolutionRequirementDescriptor Descriptor,
    CardInstanceId EvolvingCard,
    PermanentId TargetPermanentId,
    int Cost);

public sealed record StaticLinkRequirementEvaluationContext(
    GameState State,
    StaticLinkRequirementDescriptor Descriptor,
    CardInstanceId LinkCard,
    PermanentState TargetPermanent);

public sealed record StaticLinkRequirementDescriptor(
    string StableId,
    CardInstanceId? SourceCardId,
    PermanentId? SourcePermanentId,
    PlayerId ControllerPlayerId,
    int LinkCost,
    Func<StaticLinkRequirementEvaluationContext, bool>? SourceCardCondition = null,
    Func<StaticLinkRequirementEvaluationContext, bool>? TargetPermanentCondition = null,
    string DebugLabel = "");

public sealed record StaticLinkRequirementEvaluation(
    StaticLinkRequirementDescriptor Descriptor,
    CardInstanceId LinkCard,
    PermanentId TargetPermanentId,
    int Cost);
