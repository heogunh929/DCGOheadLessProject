// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_024.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_024Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-024",
        "BT21_024",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +4000 is implemented through ContinuousEffectDescriptor. OnPlay/WhenDigivolving opponent hand-to-bottom-security then top-security trash remains blocked by opponent hidden-hand selection, hand-to-security-bottom movement, security trash continuation, and OnLoseSecurity interleaving.");

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
                $"BT21-024:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 4000,
                Condition,
                "BT21-024 inherited owner-turn DP +4000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-024 only has inherited continuous DP implemented; security manipulation remains blocked.");
}
