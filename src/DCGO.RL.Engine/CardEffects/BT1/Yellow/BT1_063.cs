// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_063.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_063Script : ICardScript, IContinuousCardScript
{
    private const string WhenDigivolvingStableId = "BT1-063:when-digivolving:recovery-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-063",
        "BT1_063",
        CardEffectPortingStatus.PartiallyImplemented,
        "WhenDigivolving Recovery +1 from deck and field-top owner-turn SecurityAttack +1 while owner has 3 or more security are implemented. CanAddSecurity replacement/restriction remains blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => Bt1ScriptSupport.IsTopSourceInBattleArea(
                    effectContext.State,
                    context.SourceCard,
                    context.SourcePermanent),
                CanActivate: effectContext => effectContext.State.GetPlayer(controller).Deck.Count >= 1),
        };
    }

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
            && evaluation.State.GetPlayer(context.Controller).Security.Count >= 3;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT1-063:continuous:self-security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.SecurityAttack,
                _ => 1,
                Condition,
                "BT1-063 owner-turn SecurityAttack +1 with 3+ security")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != WhenDigivolvingStableId)
        {
            throw new DomainException($"BT1-063 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.RecoverFromDeck(state, controller, 1, context.Trace);
        });
    }
}
