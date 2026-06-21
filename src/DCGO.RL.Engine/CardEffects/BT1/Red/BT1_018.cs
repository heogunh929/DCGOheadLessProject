// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_018.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_018Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-018",
        "BT1_018",
        CardEffectPortingStatus.Implemented,
        "Field-top owner-turn SecurityAttack +1 while owner has 3 or more memory is represented through ContinuousEffectDescriptor.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { Id: var permanentId }
            && permanentId == context.SourcePermanent
            && evaluation.State.TurnPlayerId == context.Controller
            && evaluation.State.Memory >= 3;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT1-018:continuous:self-security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.SecurityAttack,
                _ => 1,
                Condition,
                "BT1-018 owner-turn SecurityAttack +1 with 3+ memory")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-018 has only continuous SecurityAttack effects.");
}
