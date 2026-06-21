// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_017.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_017Script : ICardScript
{
    private const string OnPlayStableId = "BT1-017:on-play:security-attack-plus-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-017",
        "BT1_017",
        CardEffectPortingStatus.Implemented,
        "OnPlay selects one owner battle-area Digimon and gives SecurityAttack +1 until turn end through a duration modifier.");

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
                    Bt1ScriptSupport.IsTopSourceInBattleArea(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && Bt1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => Bt1ScriptSupport.CreatePermanentSelectionRequest(
                    $"{OnPlayStableId}:target",
                    controller,
                    Bt1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller),
                    "Select 1 of your Digimon to gain SecurityAttack +1 until turn end."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT1-017 target body must be resolved through SelectionResultApplicator.");

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporarySecurityAttackModifier(
                context.State,
                permanent,
                amount: 1,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"BT1-017:security-attack:{context.Resolution.SourceCard?.Value}:{permanent.Value}",
                debugLabel: "BT1-017 SecurityAttack +1 until turn end");
        }
    }
}
