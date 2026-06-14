// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_14.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1StarlightExplosionScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-14",
        "ST1_14",
        CardEffectPortingStatus.Implemented,
        "Main option and security effect apply player-wide Security Digimon DP +7000 duration modifiers through Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-14:option:security-digimon-dp",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
            new EffectDescriptor(
                "ST1-14:security:security-digimon-dp",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        var duration = context.Resolution.StableId switch
        {
            "ST1-14:option:security-digimon-dp" => DurationScope.UntilOpponentTurnEnd,
            "ST1-14:security:security-digimon-dp" => DurationScope.UntilTurnEnd,
            _ => throw new DomainException($"ST1-14 cannot resolve unknown effect '{context.Resolution.StableId}'."),
        };

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporarySecurityDigimonDPModifier(
                state,
                controller,
                amount: 7000,
                duration,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: $"ST1-14 Security Digimon DP +7000 until {duration}");
        });
    }
}
