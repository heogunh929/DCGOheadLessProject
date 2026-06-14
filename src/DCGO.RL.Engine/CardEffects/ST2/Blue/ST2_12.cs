using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2MattIshidaScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-12",
        "ST2_12",
        CardEffectPortingStatus.Implemented,
        "OnStartTurn memory +1 while the opponent has a Digimon with no digivolution sources; security play-self tamer uses PlayWithoutPayingCost from Executing.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-12:on-start-turn:memory-plus-1",
                EffectTiming.OnStartTurn,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.Player == controller
                    && effectContext.State.TurnPlayerId == controller
                    && StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources(effectContext.State, controller)),
            new EffectDescriptor(
                "ST2-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                    effectContext.State,
                    context.SourceCard,
                    controller)),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST2-12:on-start-turn:memory-plus-1")
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                if (!StarterScriptSupport.IsTopSourceInBattleArea(
                    state,
                    context.Resolution.SourceCard,
                    context.Resolution.SourcePermanent))
                {
                    throw new DomainException("ST2-12 memory gain requires this tamer in battle area.");
                }

                if (!StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources(state, controller))
                {
                    throw new DomainException("ST2-12 memory gain requires an opponent Digimon with no digivolution sources.");
                }

                primitives.ModifyMemory(state, controller, 1);
            });
            return;
        }

        if (context.Resolution.StableId == "ST2-12:security:play-self-tamer")
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                var sourceCard = context.Resolution.SourceCard
                    ?? throw new DomainException("ST2-12 security play-self requires a source card.");
                var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
                primitives.PlayWithoutPayingCost(
                    state,
                    controller,
                    sourceCard,
                    Zone.Executing,
                    frame,
                    suspended: false);
            });
            return;
        }

        throw new DomainException($"ST2-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}
