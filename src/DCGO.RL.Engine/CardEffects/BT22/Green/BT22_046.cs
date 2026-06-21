// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_046.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_046Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-046",
        "BT22_046",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited All Turns DP +1000 is implemented through ContinuousEffectDescriptor. Alternative CS level-3 digivolution and WhenDigivolving optional CS Tamer hand play remain blocked by CS trait metadata, alternative digivolution requirement, optional hand play continuation, and hand-play ETB continuation.");

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
            && target.SourceCardIds.Contains(context.SourceCard);

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT22-046:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 1000,
                Condition,
                "BT22-046 inherited DP +1000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT22-046 has no direct effect body in the implemented subset.");
}
