// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_021.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_021Script : ICardScript
{
    private const string OnPlayStableId = "BT19-021:on-play:return-level-3";
    private const string WhenDigivolvingStableId = "BT19-021:when-digivolving:return-level-3";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-021",
        "BT19_021",
        CardEffectPortingStatus.Implemented,
        "OnPlay and WhenDigivolving return one opponent level 3 Digimon to hand through ReturnPermanentToHandWithEvents. Inherited Jamming is represented by CardDefinition.BattleKeywords.");

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
        throw new DomainException("BT19-021 return-to-hand body must be resolved through SelectionResultApplicator.");

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
                && LevelThreeCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt19ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                LevelThreeCandidates(effectContext.State, controller),
                "Return 1 opponent level 3 Digimon to hand."),
            SelectionContinuation: ApplySelection);

    private static IReadOnlyList<SelectableTarget> LevelThreeCandidates(GameState state, PlayerId controller) =>
        Bt19ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            (_, definition) => definition.Level == 3);

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.ReturnPermanentToHandWithEvents(
                context.State,
                permanent,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                context.Trace);
        }
    }
}
