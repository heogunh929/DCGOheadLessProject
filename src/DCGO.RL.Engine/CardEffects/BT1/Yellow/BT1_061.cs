// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_061.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_061Script : ICardScript
{
    private const string OnPlayStableId = "BT1-061:on-play:two-dp-minus-3000";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-061",
        "BT1_061",
        CardEffectPortingStatus.Implemented,
        "OnPlay selects up to the original max-count result, requiring exactly min(2, candidate count) opponent Digimon, and applies DP -3000 until turn end to each selected target.");

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
                CreateSelectionRequest: effectContext =>
                {
                    var candidates = OpponentCandidates(effectContext.State, controller);
                    return Bt1ScriptSupport.CreatePermanentSelectionRequest(
                        $"{OnPlayStableId}:target",
                        controller,
                        candidates,
                        "Select opponent Digimon to get DP -3000 until turn end.",
                        maxCount: Math.Min(2, candidates.Count));
                },
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-061 target body must be resolved through SelectionResultApplicator.");

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
                amount: -3000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"BT1-061:dp:{context.Resolution.SourceCard?.Value}:{permanent.Value}",
                debugLabel: "BT1-061 DP -3000 until turn end");
        }
    }
}
