// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2HowlingBlasterScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-14",
        "ST2_14",
        CardEffectPortingStatus.Implemented,
        "Main/security choose an opponent Digimon with no digivolution sources and apply temporary CannotAttack/CannotBlock restrictions through generic duration modifiers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-14:option:attack-block-restriction-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonWithoutSourcesCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-14:option:attack-block-restriction",
                    "Select 1 opponent Digimon with no digivolution sources that cannot attack or block."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST2-14:security:attack-block-restriction-selection",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonWithoutSourcesCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-14:security:attack-block-restriction",
                    "Select 1 opponent Digimon with no digivolution sources that cannot attack or block."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST2-14 requires SelectionResultApplicator for its attack/block restriction body.");

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var durationScope = context.Resolution.StableId switch
        {
            "ST2-14:option:attack-block-restriction-selection" => DurationScope.UntilOpponentTurnEnd,
            "ST2-14:security:attack-block-restriction-selection" => DurationScope.UntilOwnerTurnEnd,
            _ => throw new DomainException($"ST2-14 cannot apply unknown selection '{context.Resolution.StableId}'."),
        };

        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            if (!StarterScriptSupport.IsOpponentBattleAreaDigimonWithoutSources(context.State, controller, permanent))
            {
                throw new DomainException("ST2-14 target must be an opponent battle area Digimon with no digivolution sources.");
            }

            context.Primitives.AddTemporaryAttackRestriction(
                context.State,
                permanent,
                durationScope,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"{context.Resolution.StableId}:cannot-attack:{permanent.Value}",
                debugLabel: "ST2-14 cannot attack");
            context.Primitives.AddTemporaryBlockRestriction(
                context.State,
                permanent,
                durationScope,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"{context.Resolution.StableId}:cannot-block:{permanent.Value}",
                debugLabel: "ST2-14 cannot block");
        }
    }
}
