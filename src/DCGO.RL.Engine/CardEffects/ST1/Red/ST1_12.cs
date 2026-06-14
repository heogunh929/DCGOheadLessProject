using DCGO.RL.Engine.Domain;
// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_12.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1TaiKamiyaScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-12",
        "ST1_12",
        CardEffectPortingStatus.Implemented,
        "Main field DP +1000 effect is implemented through ContinuousEffectDescriptor; security play-self tamer plays itself from Executing without paying memory cost through Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => St1ScriptSupport.CanPlaySelfPermanentFromExecuting(
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

        int Amount(ContinuousEffectEvaluationContext _) => 1000;

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.State.TurnPlayerId == context.Controller
            && evaluation.TargetPermanent is { IsBreedingArea: false }
            && St1ScriptSupport.IsSourcePermanentInBattleArea(
                evaluation.State,
                context.SourceCard,
                context.SourcePermanent);

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST1-12:continuous:tamer-aura-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.OwnerBattleAreaDigimon,
                ContinuousModifierKind.DP,
                Amount,
                Condition,
                "ST1-12 owner battle area Digimon DP +1000")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != "ST1-12:security:play-self-tamer")
        {
            throw new DomainException($"ST1-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST1-12 security play-self requires a source card.");
            var frame = St1ScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
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
