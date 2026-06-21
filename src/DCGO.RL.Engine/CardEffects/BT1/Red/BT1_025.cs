// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_025.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_025Script : ICardScript
{
    private const string WhenDigivolvingStableId = "BT1-025:when-digivolving:security-attack-plus-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-025",
        "BT1_025",
        CardEffectPortingStatus.PartiallyImplemented,
        "WhenDigivolving SecurityAttack +1 until turn end is implemented. Static owner-turn security option invalidation while attacking requires the security-effect cancellation/immunity layer and remains blocked.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                WhenDigivolvingStableId,
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt1ScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != WhenDigivolvingStableId)
        {
            throw new DomainException($"BT1-025 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var permanent = context.Resolution.SourcePermanent
                ?? throw new DomainException("BT1-025 SecurityAttack modifier requires a source permanent.");
            primitives.AddTemporarySecurityAttackModifier(
                state,
                permanent,
                amount: 1,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"BT1-025:security-attack:{context.Resolution.SourceCard?.Value}:{permanent.Value}",
                debugLabel: "BT1-025 SecurityAttack +1 until turn end");
        });
    }
}
