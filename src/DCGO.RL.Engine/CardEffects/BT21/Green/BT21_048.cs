// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_048.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_048Script : ICardScript
{
    private const string OnPlayStableId = "BT21-048:on-play:suspend-one-digimon";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-048",
        "BT21_048",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay suspend one Digimon and inherited Piercing metadata are implemented. Alternate WG level-2 digivolution remains blocked by trait-aware digivolution requirement support. The source ActivateClass is optional, but after activation its SelectPermanentEffect uses canNoSelect=false; this port exposes the target-selection body only when candidates exist and leaves the pre-activation optional UI distinction to the common optional-decision layer.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt21ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && AnyUnsuspendedDigimonCandidates(effectContext.State).Count > 0,
                CreateSelectionRequest: effectContext => Bt20ScriptSupport.CreatePermanentSelectionRequest(
                    $"{OnPlayStableId}:target",
                    controller,
                    AnyUnsuspendedDigimonCandidates(effectContext.State),
                    "Select 1 Digimon to suspend."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT21-048 suspend body must be resolved through SelectionResultApplicator.");

    private static IReadOnlyList<SelectableTarget> AnyUnsuspendedDigimonCandidates(GameState state) =>
        state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Where(permanent => !permanent.IsBreedingArea)
            .Where(permanent => !permanent.IsSuspended)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
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

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.Suspend(context.State, permanent);
        }
    }
}
