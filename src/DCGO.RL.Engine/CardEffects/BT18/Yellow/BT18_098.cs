// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_098.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_098Script : ICardScript
{
    private const string SecurityStableId = "BT18-098:security:delete-then-recovery";
    private const string SecurityRecoverOnlyStableId = "BT18-098:security:recovery-only";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT18-098",
        "BT18_098",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill delete one opponent Digimon with 6000 DP or less, then Recovery +1 if owner security is 0. Main option top-security trash before selection, bottom-security self placement, OnDiscardSecurity reactivation, and ignore-color requirement remain blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt18ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                SecurityStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => SecurityDeleteCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt18ScriptSupport.CreatePermanentSelectionRequest(
                    $"{SecurityStableId}:target",
                    controller,
                    SecurityDeleteCandidates(effectContext.State, controller),
                    "Delete 1 opponent Digimon with 6000 DP or less."),
                SelectionContinuation: ApplySecurityDelete),
            new EffectDescriptor(
                SecurityRecoverOnlyStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    SecurityDeleteCandidates(effectContext.State, controller).Count == 0
                    && effectContext.State.GetPlayer(controller).Security.Count == 0),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != SecurityRecoverOnlyStableId)
        {
            throw new DomainException("BT18-098 target selection body must be resolved through SelectionResultApplicator.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt18ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.GetPlayer(controller).Security.Count == 0)
            {
                primitives.RecoverFromDeck(state, controller, 1, context.Trace);
            }
        });
    }

    private static IReadOnlyList<SelectableTarget> SecurityDeleteCandidates(GameState state, PlayerId controller)
    {
        var opponent = Bt18ScriptSupport.Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => BattleRules.Dp(state, permanent) <= 6000)
            .Select(permanent =>
            {
                var definition = BattleRules.Definition(state, permanent.TopCardId);
                return new SelectableTarget(
                    SelectionTargetKind.Permanent,
                    $"permanent:{permanent.Id.Value}",
                    permanent.ControllerPlayerId,
                    Permanent: permanent.Id,
                    Label: definition.CardId,
                    Zone: Zone.BattleArea);
            })
            .ToArray();
    }

    private static void ApplySecurityDelete(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DeletePermanent(context.State, permanent, context.Trace);
        }

        var controller = context.Resolution.Controller
            ?? Bt18ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        if (context.State.GetPlayer(controller).Security.Count == 0)
        {
            context.Primitives.RecoverFromDeck(context.State, controller, 1, context.Trace);
        }
    }
}
