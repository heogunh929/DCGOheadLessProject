// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_088Script : ICardScript
{
    private const string SecurityStableId = "BT1-088:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-088",
        "BT1_088",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill play-self Tamer is implemented. Main suspend-cost reveal top card, add Digimon to hand or put non-Digimon to deck bottom requires suspend-cost lifecycle and reveal/deck-bottom continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            Bt1ScriptSupport.CreatePlaySelfTamerSecurityDescriptor(
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
            throw new DomainException($"BT1-088 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            Bt1ScriptSupport.PlaySelfTamerFromExecuting(
                state,
                primitives,
                controller,
                context.Resolution.SourceCard);
        });
    }
}
