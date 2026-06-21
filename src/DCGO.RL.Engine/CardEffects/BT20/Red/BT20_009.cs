// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_009.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_009Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-009",
        "BT20_009",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +2000 is implemented through ContinuousEffectDescriptor. OnEnterFieldAnyone hand digivolution into Free trait with reduced cost requires source-aligned hand digivolve/cost continuation and remains blocked.");

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
                && target.SourceCardIds.Contains(context.SourceCard)
                && evaluation.State.TurnPlayerId == context.Controller;
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT20-009:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT20-009 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-009 only has inherited continuous DP implemented; hand digivolve remains blocked.");
}
