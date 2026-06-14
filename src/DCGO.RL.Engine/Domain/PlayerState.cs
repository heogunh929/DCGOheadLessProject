namespace DCGO.RL.Engine.Domain;

public sealed class PlayerState
{
    public PlayerState(PlayerId id)
    {
        Id = id;
    }

    public PlayerId Id { get; }
    public List<CardInstanceId> Deck { get; } = new();
    public List<CardInstanceId> DigiEggDeck { get; } = new();
    public List<CardInstanceId> Hand { get; } = new();
    public List<CardInstanceId> Trash { get; } = new();
    public List<CardInstanceId> Lost { get; } = new();
    public List<CardInstanceId> Security { get; } = new();
    public List<CardInstanceId> Executing { get; } = new();
    public List<CardInstanceId> Revealed { get; } = new();
    public List<CardInstanceId> OutsideGame { get; } = new();
    public List<PermanentState> FieldPermanents { get; } = new();

    public IEnumerable<PermanentState> BattleAreaPermanents =>
        FieldPermanents.Where(permanent => !permanent.IsBreedingArea);

    public PermanentState? BreedingAreaPermanent =>
        FieldPermanents.FirstOrDefault(permanent => permanent.IsBreedingArea);

    public IReadOnlyList<PermanentState> GetFieldPermanents() => FieldPermanents;

    public List<CardInstanceId> CardsIn(Zone zone) =>
        zone switch
        {
            Zone.Deck => Deck,
            Zone.DigiEggDeck => DigiEggDeck,
            Zone.Hand => Hand,
            Zone.Trash => Trash,
            Zone.Lost => Lost,
            Zone.Security => Security,
            Zone.Executing => Executing,
            Zone.Revealed => Revealed,
            Zone.OutsideGame => OutsideGame,
            _ => throw new DomainException($"Zone '{zone}' is not a player card-list zone."),
        };

    public PlayerState Clone()
    {
        var clone = new PlayerState(Id);
        clone.Deck.AddRange(Deck);
        clone.DigiEggDeck.AddRange(DigiEggDeck);
        clone.Hand.AddRange(Hand);
        clone.Trash.AddRange(Trash);
        clone.Lost.AddRange(Lost);
        clone.Security.AddRange(Security);
        clone.Executing.AddRange(Executing);
        clone.Revealed.AddRange(Revealed);
        clone.OutsideGame.AddRange(OutsideGame);
        clone.FieldPermanents.AddRange(FieldPermanents.Select(permanent => permanent.Clone()));
        return clone;
    }
}
