// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_039.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_039Script : ICardScript
{
    private const string OnPlayStableId = "BT20-039:on-play:suspend-opponent-digimon";
    private const string WhenDigivolvingStableId = "BT20-039:when-digivolving:suspend-opponent-digimon";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-039",
        "BT20_039",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay and WhenDigivolving suspend one opponent Digimon through the common primitive. Inherited Piercing is represented by CardDefinition.BattleKeywords in validated fixtures. Alternate ACCEL digivolution requirement remains blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateSuspendDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            CreateSuspendDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-039 suspend body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateSuspendDescriptor(
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
                Bt20ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && OpponentCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                OpponentCandidates(effectContext.State, controller),
                "Select 1 opponent Digimon to suspend."),
            SelectionContinuation: ApplySelection);

    private static IReadOnlyList<SelectableTarget> OpponentCandidates(GameState state, PlayerId controller) =>
        Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(
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
