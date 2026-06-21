// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_026.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_026Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-026",
        "BT19_026",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited All Turns DP +2000 is implemented through ContinuousEffectDescriptor. OnPlay/WhenDigivolving De-Digivolve 2 then level 4-or-lower bounce, and OnDeletion play [Blue Flare]/[Xros Heart] from under Tamer then Save, remain blocked by De-Digivolve, trait/source-under-Tamer play, and Save common layers.");

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
                $"BT19-026:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT19-026 inherited DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT19-026 only has inherited continuous DP implemented; active bodies remain blocked.");
}
