// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_093.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_093Script : ICardScript
{
    private const string SecurityDeleteStableId = "BT21-093:security:delete-highest-dp";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-093",
        "BT21_093",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill delete one opponent Digimon with the highest DP is implemented. Play/use cost -4 when opponent has 3 or fewer security, OptionSkill delete then Delay option placement, and OnLoseSecurity Delay hand digivolve remain blocked by cost-modifier frames, delay-option placement, trait metadata, and hand digivolution support.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                SecurityDeleteStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt21ScriptSupport.IsOwnedCardInZone(effectContext.State, context.SourceCard, controller, Zone.Executing)
                    && HighestDpCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                    $"{SecurityDeleteStableId}:target",
                    controller,
                    HighestDpCandidates(effectContext.State, controller),
                    "Delete 1 opponent Digimon with the highest DP."),
                SelectionContinuation: ApplyDeleteSelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-093 highest-DP delete body must be resolved through SelectionResultApplicator.");

    private static IReadOnlyList<SelectableTarget> HighestDpCandidates(GameState state, PlayerId controller)
    {
        var all = Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(state, controller);
        if (all.Count == 0)
        {
            return all;
        }

        var maxDp = all
            .Select(target => BattleRules.Dp(state, BattleRules.Permanent(state, target.Permanent!.Value)))
            .Max();
        return all
            .Where(target => BattleRules.Dp(state, BattleRules.Permanent(state, target.Permanent!.Value)) == maxDp)
            .ToArray();
    }

    private static void ApplyDeleteSelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }
    }
}
