// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_079.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_079Script : ICardScript
{
    private const string OnPlayDeleteStableId = "BT20-079:on-play:delete-lowest-level";
    private const string WhenDigivolvingDeleteStableId = "BT20-079:when-digivolving:delete-lowest-level";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-079",
        "BT20_079",
        CardEffectPortingStatus.PartiallyImplemented,
        "Field-top SecurityAttack +1 is represented by CardDefinition.SecurityAttackModifier. OnPlay/WhenDigivolving delete one opponent Digimon with the lowest level is implemented. Execute and play level 5-or-lower Ghost Digimon from trash on play/deletion remain blocked by Execute timing, trait metadata, trash play continuation, ETB continuation, and deleted-source trigger eligibility.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt20ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            CreateDeleteDescriptor(
                OnPlayDeleteStableId,
                EffectTiming.OnPlay,
                context.SourceCard,
                context.SourcePermanent,
                controller),
            CreateDeleteDescriptor(
                WhenDigivolvingDeleteStableId,
                EffectTiming.WhenDigivolving,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-079 lowest-level delete body must be resolved through SelectionResultApplicator.");

    private static EffectDescriptor CreateDeleteDescriptor(
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
                && LowestLevelCandidates(effectContext.State, controller).Count > 0,
            CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                $"{stableId}:target",
                controller,
                LowestLevelCandidates(effectContext.State, controller),
                "Delete 1 opponent Digimon with the lowest level."),
            SelectionContinuation: ApplyDeleteSelection);

    private static IReadOnlyList<SelectableTarget> LowestLevelCandidates(GameState state, PlayerId controller)
    {
        var all = Bt20ScriptSupport.OpponentBattleAreaDigimonCandidates(
            state,
            controller,
            static (_, definition) => definition.Level > 0);
        if (all.Count == 0)
        {
            return all;
        }

        var minLevel = all
            .Select(target => BattleRules.Definition(state, BattleRules.Permanent(state, target.Permanent!.Value).TopCardId).Level)
            .Min();
        return all
            .Where(target => BattleRules.Definition(state, BattleRules.Permanent(state, target.Permanent!.Value).TopCardId).Level == minLevel)
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
