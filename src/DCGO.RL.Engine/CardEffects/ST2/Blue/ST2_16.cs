// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2CocytusBreathScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-16",
        "ST2_16",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon selection returns the top card to hand and trashes all digivolution sources; security activates the same main option through SecurityEffectExecutionService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-16:option:bounce-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-16:option:bounce",
                    "Return 1 of your opponent's Digimon to its owner's hand. Trash all digivolution cards of that Digimon."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST2-16:security:main-option-activation",
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
        if (context.Resolution.StableId == "ST2-16:security:main-option-activation")
        {
            throw new DomainException("ST2-16 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST2-16 main option selection must be resolved through SelectionResultApplicator.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.ReturnPermanentToHand(context.State, permanent, context.Trace);
        }
    }
}
