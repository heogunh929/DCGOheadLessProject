namespace DCGO.RL.Engine.Domain;

public interface ICardDatabase
{
    IReadOnlyCollection<CardDefinition> All { get; }

    bool TryGetByCardId(string cardId, out CardDefinition definition);

    CardDefinition GetByCardId(string cardId);
}
