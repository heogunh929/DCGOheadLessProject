// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_070.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_070Script : ICardScript
{
    private const string OnPlayStableId = "BT1-070:on-play:suspend-opponent-digimon";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-070",
        "BT1_070",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay target selection and suspend are implemented through the common primitive. Suspend replacement/restriction and OnTappedAnyone timing coverage remain blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
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
                    $"{OnPlayStableId}:target",
                    controller,
                    OpponentCandidates(effectContext.State, controller),
                    "Select 1 opponent Digimon to suspend."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-070 target body must be resolved through SelectionResultApplicator.");

    private static IReadOnlyList<SelectableTarget> OpponentCandidates(GameState state, PlayerId controller) =>
        Bt1ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            static (permanent, _) => !permanent.IsBreedingArea && !permanent.IsSuspended);

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.Suspend(context.State, permanent);
        }
    }
}
