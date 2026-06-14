using DCGO.RL.Engine.Domain;
// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_11.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1WarGreymonScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-11",
        "ST1_11",
        CardEffectPortingStatus.Implemented,
        "Owner-turn dynamic SecurityAttack is derived from the top card's digivolution source count through ContinuousEffectDescriptor.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        int Amount(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent?.SourceCardIds.Count / 2 ?? 0;

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return evaluation.State.TurnPlayerId == context.Controller
                && !target.IsBreedingArea
                && target.TopCardId == context.SourceCard
                && target.SourceCardIds.Count >= 2;
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST1-11:continuous:security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.SecurityAttack,
                Amount,
                Condition,
                "ST1-11 dynamic SecurityAttack from source count")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST1-11 has only continuous SecurityAttack effects and no queued resolve body.");
}
