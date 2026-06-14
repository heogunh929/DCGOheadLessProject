// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3PatamonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-05",
        "ST3_05",
        CardEffectPortingStatus.Implemented,
        "Inherited OnAllyAttack security-count condition gains 1 memory through TriggerPipelineService and Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-05:on-ally-attack:memory-plus-1",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Security.Count >= 4),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.GetPlayer(controller).Security.Count < 4)
            {
                throw new DomainException("ST3-05 memory gain requires 4 or more security cards.");
            }

            if (!StarterScriptSupport.IsInheritedSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST3-05 memory gain requires the source card as a digivolution source in battle area.");
            }

            primitives.ModifyMemory(state, controller, 1);
        });
}
