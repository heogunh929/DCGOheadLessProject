// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_08.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1GarudamonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-08",
        "ST1_08",
        CardEffectPortingStatus.Implemented,
        "WhenDigivolving owner battle area Digimon selection applies temporary DP +3000 until turn end through SelectionResultApplicator and Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-08:when-digivolving:dp-selection",
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    St1ScriptSupport.IsSourcePermanentInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && St1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => St1ScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST1-08:when-digivolving:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: St1TemporaryDpSelectionSupport.ApplyPlus3000UntilTurnEnd),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST1-08 requires SelectionResultApplicator for its WhenDigivolving selection body.");
}
