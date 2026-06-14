// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_13.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1ShadowWingScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-13",
        "ST1_13",
        CardEffectPortingStatus.Implemented,
        "Main option owner Digimon selection applies temporary DP +3000 until turn end; security effect applies player-wide SecurityAttack +1 until owner turn end.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-13:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    St1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => St1ScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST1-13:option:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: St1TemporaryDpSelectionSupport.ApplyPlus3000UntilTurnEnd),
            new EffectDescriptor(
                "ST1-13:security:security-attack-plus-1",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST1-13:option:dp-selection")
        {
            throw new DomainException("ST1-13 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST1-13:security:security-attack-plus-1")
        {
            throw new DomainException($"ST1-13 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporarySecurityAttackModifier(
                state,
                controller,
                amount: 1,
                DurationScope.UntilOwnerTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST1-13 SecurityAttack +1 until owner turn end");
        });
    }
}
