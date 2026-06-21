// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_048.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_048Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-048",
        "BT22_048",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +2000 is implemented through ContinuousEffectDescriptor. Alternative CS level-3 digivolution and OnPlay/WhenDigivolving DP +3000 followed by conditional temporary Raid/Piercing remain blocked by CS trait metadata, alternative digivolution requirements, same-level stack predicate coverage, temporary keyword grants, and duration cleanup.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { } target
            && target.Id == context.SourcePermanent
            && target.SourceCardIds.Contains(context.SourceCard)
            && evaluation.State.TurnPlayerId == context.Controller;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT22-048:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT22-048 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT22-048 has no direct effect body in the implemented subset.");
}
