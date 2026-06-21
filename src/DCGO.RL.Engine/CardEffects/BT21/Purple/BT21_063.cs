// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_063.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_063Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-063",
        "BT21_063",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +2000 is implemented. Alternate digivolution from level-2 Save/Hero, OnPlay discard Save/Hero then draw 2, and Save on deletion remain blocked by text/trait metadata, hand-discard cost continuation, draw sequencing, Save, and deleted-source trigger coverage.");

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
                $"BT21-063:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT21-063 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-063 has no direct effect body in the implemented subset.");
}
