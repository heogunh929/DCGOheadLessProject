// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_039.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_039Script : ICardScript
{
    private const string InheritedLoseSecurityStableId = "BT19-039:inherited:on-lose-security:unsuspend";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT19-039",
        "BT19_039",
        CardEffectPortingStatus.PartiallyImplemented,
        "Inherited optional once-per-turn OnLoseSecurity unsuspend is implemented. OnDeletion Recovery +1 requires deleted-source trigger collection, and OnPlay/WhenDigivolving first trash top security, then delete a level 4-or-lower Digimon and gain memory requires a source-aligned pre-selection mutation continuation.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt19ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                InheritedLoseSecurityStableId,
                EffectTiming.OnLoseSecurity,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                IsOptional: true,
                CanTrigger: effectContext =>
                    IsOwnerSecurityLoss(effectContext, controller)
                    && Bt19ScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent),
                IsOncePerTurn: true,
                OncePerTurnKey: "BT19-039:inherited:on-lose-security:unsuspend"),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == InheritedLoseSecurityStableId)
        {
            context.WithState((state, primitives) =>
            {
                var permanent = context.Resolution.SourcePermanent
                    ?? throw new DomainException("BT19-039 inherited unsuspend requires a source permanent.");
                if (!Bt19ScriptSupport.IsInheritedSourceInBattleArea(
                    state,
                    context.Resolution.SourceCard,
                    context.Resolution.SourcePermanent))
                {
                    throw new DomainException("BT19-039 inherited unsuspend requires this card as a battle-area source.");
                }

                primitives.Unsuspend(state, permanent);
            });
            return;
        }

        throw new DomainException($"BT19-039 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }

    private static bool IsOwnerSecurityLoss(EffectContext context, PlayerId controller) =>
        context.Player == controller
        || context.GetValueOrDefault("Player") is PlayerId player && player == controller
        || context.GetValueOrDefault("Defender") is PlayerId defender && defender == controller;
}
