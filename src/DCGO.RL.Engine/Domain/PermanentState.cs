namespace DCGO.RL.Engine.Domain;

public sealed class PermanentState
{
    public PermanentState(PermanentId id, PlayerId owner, CardInstanceId topCard)
    {
        Id = id;
        OwnerPlayerId = owner;
        ControllerPlayerId = owner;
        TopCardId = topCard;
    }

    public PermanentId Id { get; }
    public PlayerId OwnerPlayerId { get; }
    public PlayerId ControllerPlayerId { get; set; }
    public int FrameIndex { get; set; }
    public bool IsBreedingArea { get; set; }
    public bool IsSuspended { get; set; }
    public int EnterFieldTurnCount { get; set; } = -1;
    public int DpModifier { get; set; }
    public bool IsBurstDigivolved { get; set; }
    public bool IsAppFusion { get; set; }
    public bool IsDelayOption { get; set; }
    public int SecurityAttackModifier { get; set; }
    public CardInstanceId TopCardId { get; set; }
    public List<CardInstanceId> SourceCardIds { get; } = new();
    public List<CardInstanceId> LinkedCards { get; } = new();
    public List<BattleKeyword> BattleKeywords { get; } = new();

    public PlayerId Owner => OwnerPlayerId;
    public CardInstanceId TopCard
    {
        get => TopCardId;
        set => TopCardId = value;
    }

    public List<CardInstanceId> DigivolutionCards => SourceCardIds;
    public IReadOnlyList<CardInstanceId> LinkedCardIds => LinkedCards;
    public IEnumerable<CardInstanceId> StackCardIds => new[] { TopCardId }.Concat(SourceCardIds);

    public bool IsDigimon(IReadOnlyDictionary<string, CardDefinition> definitions, IReadOnlyDictionary<CardInstanceId, CardInstance> cards) =>
        HasTopCardKind(CardKind.Digimon, definitions, cards);

    public bool IsTamer(IReadOnlyDictionary<string, CardDefinition> definitions, IReadOnlyDictionary<CardInstanceId, CardInstance> cards) =>
        HasTopCardKind(CardKind.Tamer, definitions, cards);

    public bool IsOption(IReadOnlyDictionary<string, CardDefinition> definitions, IReadOnlyDictionary<CardInstanceId, CardInstance> cards) =>
        HasTopCardKind(CardKind.Option, definitions, cards);

    public PermanentState Clone()
    {
        var clone = new PermanentState(Id, OwnerPlayerId, TopCardId)
        {
            ControllerPlayerId = ControllerPlayerId,
            FrameIndex = FrameIndex,
            IsBreedingArea = IsBreedingArea,
            IsSuspended = IsSuspended,
            EnterFieldTurnCount = EnterFieldTurnCount,
            DpModifier = DpModifier,
            IsBurstDigivolved = IsBurstDigivolved,
            IsAppFusion = IsAppFusion,
            IsDelayOption = IsDelayOption,
            SecurityAttackModifier = SecurityAttackModifier,
        };

        clone.SourceCardIds.AddRange(SourceCardIds);
        clone.LinkedCards.AddRange(LinkedCards);
        clone.BattleKeywords.AddRange(BattleKeywords);
        return clone;
    }

    private bool HasTopCardKind(
        CardKind kind,
        IReadOnlyDictionary<string, CardDefinition> definitions,
        IReadOnlyDictionary<CardInstanceId, CardInstance> cards)
    {
        if (!cards.TryGetValue(TopCardId, out var card))
        {
            throw new DomainException($"Top card '{TopCardId}' does not exist.");
        }

        if (!definitions.TryGetValue(card.DefinitionId, out var definition))
        {
            throw new DomainException($"Card definition '{card.DefinitionId}' does not exist.");
        }

        return definition.CardKinds.Contains(kind);
    }
}
