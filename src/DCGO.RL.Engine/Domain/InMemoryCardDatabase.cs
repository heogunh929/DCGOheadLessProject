namespace DCGO.RL.Engine.Domain;

public sealed class InMemoryCardDatabase : ICardDatabase
{
    private readonly Dictionary<string, CardDefinition> _definitions;
    private readonly IReadOnlyCollection<CardDefinition> _all;

    public InMemoryCardDatabase(IEnumerable<CardDefinition> definitions)
    {
        _definitions = new Dictionary<string, CardDefinition>(StringComparer.Ordinal);

        foreach (var definition in definitions)
        {
            if (string.IsNullOrWhiteSpace(definition.CardId))
            {
                throw new DomainException("CardDefinition.CardId must not be empty.");
            }

            if (!_definitions.TryAdd(definition.CardId, definition))
            {
                throw new DomainException($"Duplicate card definition id '{definition.CardId}'.");
            }
        }

        _all = Array.AsReadOnly(_definitions.Values.OrderBy(definition => definition.CardId, StringComparer.Ordinal).ToArray());
    }

    public IReadOnlyCollection<CardDefinition> All => _all;

    public bool TryGetByCardId(string cardId, out CardDefinition definition) =>
        _definitions.TryGetValue(cardId, out definition!);

    public CardDefinition GetByCardId(string cardId) =>
        TryGetByCardId(cardId, out var definition)
            ? definition
            : throw new DomainException($"Card definition '{cardId}' does not exist.");
}
