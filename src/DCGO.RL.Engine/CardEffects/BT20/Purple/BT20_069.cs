// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_069.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_069Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-069",
        "BT20_069",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +2000 is implemented through ContinuousEffectDescriptor. Alternate Evil trait digivolution and OnPlay/WhenDigivolving hand discard then temporary Blocker/Retaliation grant remain blocked by trait metadata, hand-discard cost continuation, and temporary keyword duration layers.");

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
                $"BT20-069:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT20-069 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-069 only has inherited continuous DP implemented; hand discard and temporary keyword grants remain blocked.");
}
