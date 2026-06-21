// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_005.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_005Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-005",
        "BT1_005",
        CardEffectPortingStatus.Implemented,
        "Inherited owner-turn DP +2000 while owner has 6 or more security cards is represented through ContinuousEffectDescriptor.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { Id: var permanentId }
            && permanentId == context.SourcePermanent
            && evaluation.State.TurnPlayerId == context.Controller
            && evaluation.State.GetPlayer(context.Controller).Security.Count >= 6;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT1-005:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT1-005 inherited owner-turn DP +2000 with 6+ security")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-005 has only continuous inherited DP effects.");
}
