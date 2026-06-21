// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_082.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_082Script : ICardScript
{
    private const string SecurityStableId = "BT21-082:security:play-self-tamer";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT21-082",
        "BT21_082",
        CardEffectPortingStatus.PartiallyImplemented,
        "SecuritySkill play-self Tamer is implemented. StartMainPhase hand digivolution into Hybrid/Hero with red-Tamer-name cost reduction and inherited OnLoseSecurity optional red Tamer play remain blocked by hand digivolution, trait metadata, cost reduction by unique names, optional hand play selection, and ETB continuation.");

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
        if (context.Resolution.StableId == SecurityStableId)
        {
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
            return;
        }

        throw new DomainException($"BT21-082 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
