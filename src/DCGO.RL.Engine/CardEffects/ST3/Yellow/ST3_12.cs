// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3TakeruTakaishiScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-12",
        "ST3_12",
        CardEffectPortingStatus.Implemented,
        "Opponent-turn Security Digimon DP +2000 aura is represented by continuous SecurityDigimonDP; security play-self tamer reuses PlayWithoutPayingCost from Executing.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                    effectContext.State,
                    context.SourceCard,
                    controller)),
        };
    }

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        int Amount(ContinuousEffectEvaluationContext _) => 2000;

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPlayer == context.Controller
            && evaluation.State.TurnPlayerId == StarterScriptSupport.Opponent(evaluation.State, context.Controller)
            && StarterScriptSupport.IsTopSourceInBattleArea(
                evaluation.State,
                context.SourceCard,
                context.SourcePermanent);

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST3-12:continuous:security-digimon-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.OwnerPlayer,
                ContinuousModifierKind.SecurityDigimonDP,
                Amount,
                Condition,
                "ST3-12 opponent-turn Security Digimon DP +2000")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != "ST3-12:security:play-self-tamer")
        {
            throw new DomainException($"ST3-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-12 security play-self requires a source card.");
            var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
            primitives.PlayWithoutPayingCost(
                state,
                controller,
                sourceCard,
                Zone.Executing,
                frame,
                suspended: false);
        });
    }
}
