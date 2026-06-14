using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public enum ContinuousEffectSourceKind
{
    FieldTop,
    InheritedSource,
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
    PermanentId SourcePermanent,
    PlayerId Controller,
    ContinuousEffectSourceKind SourceKind);

public sealed record ContinuousEffectEvaluationContext(
    GameState State,
    ContinuousEffectDescriptor Descriptor,
    PermanentState? TargetPermanent,
    PlayerId? TargetPlayer);

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
