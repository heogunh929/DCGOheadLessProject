// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_089.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_089Script : ICardScript
{
    private const string SecurityAddToHandStableId = "BT19-089:security:add-this-card-to-hand";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-089",
        "BT19_089",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill add-this-card-to-hand is implemented from Executing. Ignore-color requirement and option main immunity/cannot-DP-reduce duration remain blocked by cost/color and effect-immunity common layers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt19ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                SecurityAddToHandStableId,
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => Bt19ScriptSupport.IsOwnedCardInZone(
                    effectContext.State,
                    context.SourceCard,
                    controller,
                    Zone.Executing)),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != SecurityAddToHandStableId)
        {
            throw new DomainException($"BT19-089 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt19ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("BT19-089 security add-to-hand requires a source card.");
            primitives.AddExecutingSecurityEffectCardToHand(state, sourceCard, controller);
        });
    }
}
