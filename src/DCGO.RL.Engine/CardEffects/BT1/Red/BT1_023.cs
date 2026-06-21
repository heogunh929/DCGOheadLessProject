// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_023.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_023Script : ICardScript
{
    private const string OnPlayStableId = "BT1-023:on-play:delete-blocker";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-023",
        "BT1_023",
        CardEffectPortingStatus.Implemented,
        "OnPlay selects and deletes one opponent battle-area Digimon with Blocker.");

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
                    Bt1ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && BlockerCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt1ScriptSupport.CreatePermanentSelectionRequest(
                    $"{OnPlayStableId}:target",
                    controller,
                    BlockerCandidates(effectContext.State, controller),
                    "Delete 1 opponent Digimon with Blocker."),
                SelectionContinuation: ApplyDeleteSelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-023 delete target body must be resolved through SelectionResultApplicator.");

    private static IReadOnlyList<SelectableTarget> BlockerCandidates(GameState state, PlayerId controller)
    {
        var keywords = BattleKeywordService.Default;
        return Bt1ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (permanent, _) => keywords.HasKeyword(state, permanent, BattleKeyword.Blocker));
    }

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
