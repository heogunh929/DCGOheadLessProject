// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_088.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_088Script : ICardScript
{
    private const string SecurityStableId = "BT21-088:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-088",
        "BT21_088",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill play-self Tamer is implemented. StartMainPhase hand Save/Hero source placement, draw, memory, and BeforePayCost suspend-cost/source-transfer digivolution cost reduction remain blocked by trait/text metadata, hand-to-source placement, source transfer, suspend-cost continuation, and cost-modifier frames.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt21ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt20ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
                SecurityStableId,
                context.SourceCard,
                context.SourcePermanent,
                controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != SecurityStableId)
        {
            throw new DomainException($"BT21-088 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt21ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            Bt20ScriptSupport.PlaySelfTamerFromExecuting(
                state,
                primitives,
                controller,
                context.Resolution.SourceCard);
        });
    }
}
