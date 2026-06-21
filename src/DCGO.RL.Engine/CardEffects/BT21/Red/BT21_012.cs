// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_012.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_012Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-012",
        "BT21_012",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited owner-turn DP +2000 is implemented through ContinuousEffectDescriptor. Main suspend this Digimon, play red Tamer with inherited effects from hand, then place this Digimon under that Tamer remains blocked by suspend-cost continuation, inherited-effect metadata, hand play ETB continuation, and permanent-to-source placement.");

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
                $"BT21-012:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT21-012 inherited owner-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-012 only has inherited continuous DP implemented; suspend-cost Tamer play remains blocked.");
}
