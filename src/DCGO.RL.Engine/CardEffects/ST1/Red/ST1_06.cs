using DCGO.RL.Engine.Domain;
// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1CoredramonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-06",
        "ST1_06",
        CardEffectPortingStatus.Implemented,
        "Blocker is represented by CardDefinition.BattleKeywords. OnAllyAttack loses 2 memory through Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                "ST1-06:on-ally-attack:memory-minus-2",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext => St1ScriptSupport.IsSourcePermanentInBattleArea(
                    effectContext.State,
                    context.SourceCard,
                    context.SourcePermanent)),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var owner = St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (!St1ScriptSupport.IsSourcePermanentInBattleArea(state, context.Resolution.SourceCard, context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST1-06 memory loss requires the source permanent in the battle area.");
            }

            primitives.ModifyMemory(state, owner, -2);
        });
}
