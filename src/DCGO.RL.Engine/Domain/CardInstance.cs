namespace DCGO.RL.Engine.Domain;

public sealed class CardInstance
{
    public CardInstance(CardInstanceId id, string definitionId, PlayerId owner)
    {
        Id = id;
        DefinitionId = definitionId;
        Owner = owner;
    }

    public CardInstanceId Id { get; }
    public string DefinitionId { get; }
    public PlayerId Owner { get; }
    public Zone Zone { get; set; } = Zone.None;
    public Zone CurrentZone
    {
        get => Zone;
        set => Zone = value;
    }

    public bool IsFaceUp { get; set; } = true;
    public PermanentId? PermanentId { get; set; }

    public CardInstance Clone() =>
        new(Id, DefinitionId, Owner)
        {
            Zone = Zone,
            IsFaceUp = IsFaceUp,
            PermanentId = PermanentId,
        };
}
