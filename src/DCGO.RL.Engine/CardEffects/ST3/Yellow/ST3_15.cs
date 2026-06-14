// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3HolyWaveScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-15",
        "ST3_15",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon SecurityAttack -3 until opponent turn end; security applies opponent player-wide SecurityAttack -1 until turn end.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-15:option:security-attack-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-15:option:security-attack",
                    "1 of your opponent's Digimon gets SecurityAttack -3 until the end of your opponent's next turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-15:security:opponent-security-attack-minus-1",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-15:option:security-attack-selection")
        {
            throw new DomainException("ST3-15 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-15:security:opponent-security-attack-minus-1")
        {
            throw new DomainException($"ST3-15 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var opponent = StarterScriptSupport.Opponent(state, controller);
            primitives.AddTemporarySecurityAttackModifier(
                state,
                opponent,
                amount: -1,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-15 opponent Digimon SecurityAttack -1 until turn end");
        });
    }

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporarySecurityAttackModifier(
                context.State,
                permanent,
                amount: -3,
                DurationScope.UntilOpponentTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-15 SecurityAttack -3 until opponent turn end");
        }
    }
}
