// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_052.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_052Script : ICardScript
{
    private const string OnPlayStableId = "BT19-052:on-play:delete-by-security-cost";
    private const string WhenDigivolvingStableId = "BT19-052:when-digivolving:delete-by-security-cost";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-052",
        "BT19_052",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay and WhenDigivolving delete one opponent Digimon whose play cost is at most 2 plus 2 per owner face-up security card. Alternate Royal Base digivolution, inherited OnEndBattle security trash, and security-zone Royal Base Blocker aura remain blocked by trait, OnEndBattle payload, and security-zone continuous source layers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt19ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            CreateDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT19-052 deletion target body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateDescriptor(
        string stableId,
        EffectTiming timing,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller) =>
        new(
            stableId,
            timing,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            CanTrigger: effectContext =>
                Bt19ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && DeleteCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt19ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                DeleteCandidates(effectContext.State, controller),
                "Delete 1 opponent Digimon by play-cost threshold."),
            SelectionContinuation: ApplyDeleteSelection);

    private static IReadOnlyList<SelectableTarget> DeleteCandidates(GameState state, PlayerId controller)
    {
        var maxCost = 2 + FaceUpSecurityCount(state, controller) * 2;
        return Bt19ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (_, definition) => definition.PlayCost <= maxCost);
    }

    private static int FaceUpSecurityCount(GameState state, PlayerId controller) =>
        state.GetPlayer(controller)
            .Security
            .Count(card => state.Cards.TryGetValue(card, out var instance) && instance.IsFaceUp);

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
