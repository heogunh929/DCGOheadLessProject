// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_077.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_077Script : ICardScript
{
    private const string TopEndTurnStableId = "BT22-077:on-end-turn:unsuspend-one";
    private const string InheritedEndTurnStableId = "BT22-077:inherited:on-end-turn:unsuspend-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT22-077",
        "BT22_077",
        CardEffectPortingStatus.PartiallyImplemented,
        "Top and inherited optional end-of-owner-turn unsuspend one owner Digimon are implemented. Alternative Light Fang/Night Claw/CS level-5 digivolution, Iceclad, Blocker metadata, WhenDigivolving source trash and bottom-deck return remain blocked by trait metadata, alternative digivolution requirements, Iceclad keyword support, static keyword registry propagation, source-trash selection, and bottom-deck movement.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt22ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);
        var sourceCard = context.SourceCard;

        return new[]
        {
            CreateUnsuspendDescriptor(
                TopEndTurnStableId,
                sourceCard,
                context.SourcePermanent,
                controller,
                state => Bt22ScriptSupport.IsTopSourceInBattleArea(state, sourceCard, context.SourcePermanent)),
            CreateUnsuspendDescriptor(
                InheritedEndTurnStableId,
                sourceCard,
                context.SourcePermanent,
                controller,
                state => Bt22ScriptSupport.IsInheritedSourceInBattleArea(state, sourceCard, context.SourcePermanent)),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT22-077 unsuspend body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateUnsuspendDescriptor(
        string stableId,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller,
        Func<GameState, bool> sourceCondition) =>
        new(
            stableId,
            EffectTiming.OnEndTurn,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            IsOptional: true,
            CanTrigger: effectContext =>
                effectContext.State.TurnPlayerId == controller
                && sourceCondition(effectContext.State)
                && OwnerCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt22ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target:{sourceCard?.Value}",
                controller,
                OwnerCandidates(effectContext.State, controller),
                "Select 1 of your Digimon to unsuspend.",
                canSkip: true),
            SelectionContinuation: ApplySelection,
            IsOncePerTurn: true,
            OncePerTurnKey: $"{stableId}:{sourceCard?.Value}");

    private static IReadOnlyList<SelectableTarget> OwnerCandidates(GameState state, PlayerId controller) =>
        Bt22ScriptSupport.OwnerBattleAreaDigimonCandidates(state, controller);

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.Unsuspend(context.State, permanent);
        }
    }
}
