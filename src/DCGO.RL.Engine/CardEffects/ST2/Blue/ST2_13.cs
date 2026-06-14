using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2HammerSparkScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-13",
        "ST2_13",
        CardEffectPortingStatus.Implemented,
        "Main option memory +1 and security memory +2 resolve through Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-13:option:memory-plus-1",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
            new EffectDescriptor(
                "ST2-13:security:memory-plus-2",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        var amount = context.Resolution.StableId switch
        {
            "ST2-13:option:memory-plus-1" => 1,
            "ST2-13:security:memory-plus-2" => 2,
            _ => throw new DomainException($"ST2-13 cannot resolve unknown effect '{context.Resolution.StableId}'."),
        };

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.ModifyMemory(state, controller, amount);
        });
    }
}
