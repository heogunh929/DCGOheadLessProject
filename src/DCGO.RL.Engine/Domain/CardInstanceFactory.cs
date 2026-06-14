namespace DCGO.RL.Engine.Domain;

public sealed class CardInstanceFactory
{
    private int _nextInstanceId;

    public CardInstanceFactory(int firstInstanceId = 1)
    {
        if (firstInstanceId < 1)
        {
            throw new DomainException("First card instance id must be positive.");
        }

        _nextInstanceId = firstInstanceId;
    }

    public CardInstance Create(CardDefinition definition, PlayerId owner, Zone zone = Zone.None, bool isFaceUp = true)
    {
        if (string.IsNullOrWhiteSpace(definition.CardId))
        {
            throw new DomainException("CardDefinition.CardId must not be empty.");
        }

        var instance = new CardInstance(new CardInstanceId(_nextInstanceId++), definition.CardId, owner)
        {
            Zone = zone,
            IsFaceUp = isFaceUp,
        };

        return instance;
    }

    public CardInstance Create(string cardId, PlayerId owner, ICardDatabase cardDatabase, Zone zone = Zone.None, bool isFaceUp = true) =>
        Create(cardDatabase.GetByCardId(cardId), owner, zone, isFaceUp);
}
