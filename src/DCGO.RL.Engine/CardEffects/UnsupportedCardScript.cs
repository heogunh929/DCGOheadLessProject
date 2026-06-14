using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public sealed class UnsupportedCardScript : ICardScript
{
    public UnsupportedCardScript(string cardId, string effectClassName, string reason)
    {
        Porting = new CardEffectPortingRecord(
            string.IsNullOrWhiteSpace(cardId) ? "<unknown>" : cardId,
            effectClassName ?? string.Empty,
            CardEffectPortingStatus.Unsupported,
            string.IsNullOrWhiteSpace(reason) ? "Card script is not ported." : reason);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        throw CreateException();

    public void Resolve(CardScriptExecutionContext context) =>
        throw CreateException();

    private UnsupportedMechanicException CreateException() =>
        new($"Unsupported card script '{Porting.CardId}' ({Porting.EffectClassName}): {Porting.Notes}");
}
