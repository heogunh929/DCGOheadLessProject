// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_060.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_060Script : ICardScript, IContinuousCardScript
{
    private const string OnPlayStableId = "BT1-060:on-play:recovery-one";

    public CardEffectPortingRecord Porting { get; } = new(
        "BT1-060",
        "BT1_060",
        CardEffectPortingStatus.PartiallyImplemented,
        "OnPlay Recovery +1 from deck and inherited owner-turn DP +1000 per 3 owner security are implemented. CanAddSecurity replacement/restriction hooks remain blocked by the recovery/security-add common layer.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? Bt1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                OnPlayStableId,
                EffectTiming.OnPlay,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    Bt1ScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Deck.Count >= 1),
        };
    }

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent is { Id: var permanentId }
            && permanentId == context.SourcePermanent
            && evaluation.State.TurnPlayerId == context.Controller
            && evaluation.State.GetPlayer(context.Controller).Security.Count >= 3;

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT1-060:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                evaluation => evaluation.State.GetPlayer(context.Controller).Security.Count / 3 * 1000,
                Condition,
                "BT1-060 inherited owner-turn DP from security count")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != OnPlayStableId)
        {
            throw new DomainException($"BT1-060 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? Bt1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.RecoverFromDeck(state, controller, 1, context.Trace);
        });
    }
}
