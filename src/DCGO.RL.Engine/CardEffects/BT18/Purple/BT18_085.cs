// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_085.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_085Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT18-085",
        "BT18_085",
        CardEffectPortingStatus.PartiallyImplemented,
        "Owner-turn field-top DP and SecurityAttack increases from opponent trash color count are implemented through ContinuousEffectDescriptor. Self digivolution cost reduction while this card is not in battle area requires the BeforePayCost/cost resolver common layer and remains blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return evaluation.State.TurnPlayerId == context.Controller
                && target.Id == context.SourcePermanent
                && BattleRules.IsDigimon(evaluation.State, target.TopCardId);
        }

        int SecurityAttackAmount(ContinuousEffectEvaluationContext evaluation) =>
            Bt18ScriptSupport.DistinctTrashColorCount(
                evaluation.State,
                Bt18ScriptSupport.Opponent(evaluation.State, context.Controller)) / 2;

        int DpAmount(ContinuousEffectEvaluationContext evaluation) =>
            SecurityAttackAmount(evaluation) * 2000;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT18-085:continuous:self-security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.SecurityAttack,
                SecurityAttackAmount,
                Condition,
                "BT18-085 SecurityAttack from opponent trash color count"),
            new ContinuousEffectDescriptor(
                $"BT18-085:continuous:self-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                DpAmount,
                Condition,
                "BT18-085 DP from opponent trash color count"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT18-085 has only continuous implemented effects; cost reduction remains blocked.");
}
