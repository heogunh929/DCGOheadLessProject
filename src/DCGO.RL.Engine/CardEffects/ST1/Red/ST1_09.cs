using DCGO.RL.Engine.Domain;
// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_09.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1MetalGreymonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-09",
        "ST1_09",
        CardEffectPortingStatus.Implemented,
        "Inherited OnBlockAnyone owner-turn memory +3 is represented with Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                "ST1-09:on-block:anyone:memory-plus-3",
                EffectTiming.OnBlockAnyone,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext =>
                {
                    var owner = St1ScriptSupport.TrySourceCardOwner(effectContext.State, context.SourceCard);
                    return owner is not null
                        && effectContext.State.TurnPlayerId == owner.Value
                        && St1ScriptSupport.IsSourcePermanentInBattleArea(
                            effectContext.State,
                            context.SourceCard,
                            context.SourcePermanent)
                        && (effectContext.SourcePermanent is null || effectContext.SourcePermanent == context.SourcePermanent);
                }),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var owner = St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.TurnPlayerId != owner)
            {
                throw new DomainException("ST1-09 memory gain requires the source card owner's turn.");
            }

            if (!St1ScriptSupport.IsSourcePermanentInBattleArea(state, context.Resolution.SourceCard, context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST1-09 memory gain requires the source permanent in the battle area.");
            }

            primitives.ModifyMemory(state, owner, 3);
        });
}
