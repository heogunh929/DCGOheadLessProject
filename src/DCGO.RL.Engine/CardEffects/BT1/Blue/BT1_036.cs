// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_036.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_036Script : ICardScript
{
    private const string OnPlayStableId = "BT1-036:on-play:unsuspend-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-036",
        "BT1_036",
        CardEffectPortingStatus.Implemented,
        "OnPlay selects 1 owner battle-area Digimon and runs the common unsuspend primitive.");

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
                    && Bt1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt1ScriptSupport.CreatePermanentSelectionRequest(
                    $"{OnPlayStableId}:target",
                    controller,
                    Bt1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller),
                    "Select 1 of your Digimon to unsuspend."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-036 target body must be resolved through SelectionResultApplicator.");

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.Unsuspend(context.State, permanent);
        }
    }
}
