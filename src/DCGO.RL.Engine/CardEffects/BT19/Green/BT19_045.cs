// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_045.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_045Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-045",
        "BT19_045",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited All Turns DP +1000 is implemented through ContinuousEffectDescriptor. Alternate Royal Base digivolution, Royal Base digivolution cost reduction, and face-up security Royal Base DP aura require trait metadata, security-zone continuous sources, and BeforePayCost cost modifier layers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return target.Id == context.SourcePermanent
                && target.SourceCardIds.Contains(context.SourceCard);
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT19-045:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 1000,
                Condition,
                "BT19-045 inherited DP +1000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT19-045 only has inherited continuous DP implemented; active bodies remain blocked.");
}
