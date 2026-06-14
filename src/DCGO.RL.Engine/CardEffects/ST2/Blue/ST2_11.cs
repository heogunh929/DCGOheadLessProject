// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_11.cs
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2MetalGarurumonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-11",
        "ST2_11",
        CardEffectPortingStatus.Implemented,
        "Top-card once-per-turn OnAllyAttack unsuspends this Digimon through TriggerPipelineService and Tier1PrimitiveService.Unsuspend.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-11:on-ally-attack:unsuspend-this-digimon",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && StarterScriptSupport.IsSourcePermanentAttacker(effectContext, context.SourcePermanent)
                    && context.SourcePermanent is not null
                    && BattleRules.Permanent(effectContext.State, context.SourcePermanent.Value).IsSuspended,
                IsOncePerTurn: true,
                OncePerTurnKey: "ST2-11:on-ally-attack:unsuspend-this-digimon"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            if (context.Resolution.StableId != "ST2-11:on-ally-attack:unsuspend-this-digimon")
            {
                throw new DomainException($"ST2-11 cannot resolve unknown effect '{context.Resolution.StableId}'.");
            }

            var sourcePermanent = context.Resolution.SourcePermanent
                ?? throw new DomainException("ST2-11 unsuspend requires a source permanent.");
            if (!StarterScriptSupport.IsTopSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this card as the top card in battle area.");
            }

            if (!StarterScriptSupport.IsSourcePermanentAttacker(context.Resolution.Context, sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this Digimon to be the attacker.");
            }

            if (!primitives.Unsuspend(state, sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this Digimon to be suspended.");
            }
        });
}
