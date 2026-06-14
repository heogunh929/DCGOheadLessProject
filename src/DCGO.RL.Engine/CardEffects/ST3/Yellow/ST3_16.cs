using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3SevenHeavensScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-16",
        "ST3_16",
        CardEffectPortingStatus.Implemented,
        "Main option DP -10000 selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-16:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-16:option:dp",
                    "1 of your opponent's Digimon gets -10000 DP for the turn."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST3-16:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId.EndsWith(":security:main-option-activation", StringComparison.Ordinal))
        {
            throw new DomainException("ST3-16 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST3-16 main option selection must be resolved through SelectionResultApplicator.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: -10000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-16 DP -10000 until turn end");
        }
    }
}
