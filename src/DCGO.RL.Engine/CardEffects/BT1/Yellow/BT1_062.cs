// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_062.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_062Script : ICardScript
{
    private const string WhenDigivolvingStableId = "BT1-062:when-digivolving:dp-minus-8000";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-062",
        "BT1_062",
        CardEffectPortingStatus.Implemented,
        "WhenDigivolving selects 1 opponent battle-area Digimon and applies DP -8000 until turn end.");

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
                CanTrigger: effectContext =>
                    Bt1ScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && OpponentCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt1ScriptSupport.CreatePermanentSelectionRequest(
                    $"{WhenDigivolvingStableId}:target",
                    controller,
                    OpponentCandidates(effectContext.State, controller),
                    "Select 1 opponent Digimon to get DP -8000 until turn end."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-062 target body must be resolved through SelectionResultApplicator.");

    private static IReadOnlyList<SelectableTarget> OpponentCandidates(GameState state, PlayerId controller) =>
        Bt1ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            static (permanent, _) => !permanent.IsBreedingArea);

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: -8000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"BT1-062:dp:{context.Resolution.SourceCard?.Value}:{permanent.Value}",
                debugLabel: "BT1-062 DP -8000 until turn end");
        }
    }
}
