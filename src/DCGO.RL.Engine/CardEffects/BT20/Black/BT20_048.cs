// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_048.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_048Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-048",
        "BT20_048",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited opponent-turn DP +2000 is implemented through ContinuousEffectDescriptor. Alternate black level-2 X Antibody digivolution and OnPlay reveal top 3, select X Antibody plus Chronicle Tamer/Option, and bottom-deck rest require common digivolution requirement and reveal ordering layers.");

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
                && evaluation.State.TurnPlayerId == Bt20ScriptSupport.Opponent(evaluation.State, context.Controller);
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT20-048:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT20-048 inherited opponent-turn DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-048 only has inherited continuous DP implemented; reveal/alternate digivolution remain blocked.");
}
