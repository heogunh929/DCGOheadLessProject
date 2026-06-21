// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_065.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_065Script : ICardScript
{
    private const string OnPlayStableId = "BT22-065:on-play:dp-minus-8000";
    private const string WhenDigivolvingStableId = "BT22-065:when-digivolving:dp-minus-8000";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-065",
        "BT22_065",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay and WhenDigivolving select 1 opponent Digimon and apply DP -8000 for the turn. Alternative CS level-5 digivolution and owner-turn once-per-turn opponent-deletion hand digivolution remain blocked by CS trait metadata, alternative digivolution requirements, optional hand digivolution, and free digivolution continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateDpMinusDescriptor(OnPlayStableId, EffectTiming.OnPlay, context.SourceCard, context.SourcePermanent, controller),
            CreateDpMinusDescriptor(WhenDigivolvingStableId, EffectTiming.WhenDigivolving, context.SourceCard, context.SourcePermanent, controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT22-065 target body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateDpMinusDescriptor(
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
                Bt22ScriptSupport.IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && OpponentCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt22ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                OpponentCandidates(effectContext.State, controller),
                "Select 1 opponent Digimon to get DP -8000 until turn end."),
            SelectionContinuation: ApplySelection);

    private static IReadOnlyList<SelectableTarget> OpponentCandidates(GameState state, PlayerId controller) =>
        Bt22ScriptSupport.OpponentBattleAreaDigimonCandidates(state, controller);

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
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
                stableId: $"BT22-065:dp:{context.Resolution.SourceCard?.Value}:{permanent.Value}",
                debugLabel: "BT22-065 DP -8000 until turn end");
        }
    }
}
