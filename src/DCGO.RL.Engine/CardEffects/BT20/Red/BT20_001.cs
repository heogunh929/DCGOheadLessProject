// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_001.cs
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_001Script : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "BT20-001",
        "BT20_001",
        CardEffectPortingStatus.Implemented,
        "Inherited owner-turn DP +2000 while the host Digimon has 4 or more sources and owner deck has at least 1 card is implemented through ContinuousEffectDescriptor.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return target.Id == context.SourcePermanent
                && target.SourceCardIds.Contains(context.SourceCard)
                && evaluation.State.TurnPlayerId == context.Controller
                && evaluation.State.GetPlayer(context.Controller).Deck.Count >= 1
                && target.SourceCardIds.Count >= 4;
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"BT20-001:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                _ => 2000,
                Condition,
                "BT20-001 inherited DP +2000"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("BT20-001 only has inherited continuous DP implemented.");
}
