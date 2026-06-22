using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public sealed class NoEffectCardScript : ICardScript
{
    public NoEffectCardScript(
        string cardId,
        string effectClassName = "",
        string notes = "Explicit no-effect card.",
        int cardIndex = 0,
        string variantKey = "",
        string sourceEffectClassName = "")
    {
        if (string.IsNullOrWhiteSpace(cardId))
        {
            throw new DomainException("NoEffectCardScript requires a card id.");
        }

        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.NoEffect,
            notes,
            sourceEffectClassName,
            cardIndex,
            variantKey);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public void Resolve(CardScriptExecutionContext context)
    {
    }
}
