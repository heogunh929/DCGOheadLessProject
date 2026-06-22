namespace DCGO.RL.Engine.Domain;

public sealed record CardDefinition
{
    private string _cardId = string.Empty;
    private string _cardNameEnglish = string.Empty;
    private string _cardTextEnglish = string.Empty;
    private string _cardTextJapanese = string.Empty;
    private string _cardEffectClassName = string.Empty;
    private string _variantKey = string.Empty;
    private int _dp;
    private IReadOnlyList<CardColor> _cardColors = Array.Empty<CardColor>();
    private IReadOnlyList<string> _traits = Array.Empty<string>();
    private IReadOnlyList<EvoCostDefinition> _evoCosts = Array.Empty<EvoCostDefinition>();
    private IReadOnlyList<CardKind> _cardKinds = Array.Empty<CardKind>();
    private IReadOnlyList<CardColor> _optionCardColorRequirements = Array.Empty<CardColor>();
    private IReadOnlyList<Mechanic> _mechanics = Array.Empty<Mechanic>();
    private IReadOnlyList<EvolutionRequirement> _evolutionRequirements = Array.Empty<EvolutionRequirement>();
    private IReadOnlyList<PlayRequirement> _playRequirements = Array.Empty<PlayRequirement>();
    private IReadOnlyList<BattleKeyword> _battleKeywords = Array.Empty<BattleKeyword>();

    public string CardId
    {
        get => _cardId;
        init => _cardId = value ?? string.Empty;
    }

    public string CardNumber
    {
        get => _cardId;
        init => _cardId = value ?? string.Empty;
    }

    public int CardIndex { get; init; }

    public string VariantKey
    {
        get => _variantKey;
        init => _variantKey = value ?? string.Empty;
    }

    public string DefinitionStableId =>
        CardDefinitionIdentity.StableIdOrCardId(CardId, CardIndex, VariantKey);

    public bool HasDefinitionIdentity => CardIndex > 0;

    public string CardNameEnglish
    {
        get => _cardNameEnglish;
        init => _cardNameEnglish = value ?? string.Empty;
    }

    public string Name
    {
        get => _cardNameEnglish;
        init => _cardNameEnglish = value ?? string.Empty;
    }

    public string CardNameJapanese { get; init; } = string.Empty;

    public string CardTextEnglish
    {
        get => _cardTextEnglish;
        init => _cardTextEnglish = value ?? string.Empty;
    }

    public string Text
    {
        get => _cardTextEnglish;
        init => _cardTextEnglish = value ?? string.Empty;
    }

    public string CardTextJapanese
    {
        get => _cardTextJapanese;
        init => _cardTextJapanese = value ?? string.Empty;
    }

    public IReadOnlyList<CardColor> CardColors
    {
        get => _cardColors;
        init => _cardColors = ToReadOnlyList(value);
    }

    public IReadOnlyList<CardColor> Colors
    {
        get => _cardColors;
        init => _cardColors = ToReadOnlyList(value);
    }

    public IReadOnlyList<string> Traits
    {
        get => _traits;
        init => _traits = ToReadOnlyStringList(value);
    }

    public IReadOnlyList<string> CardTraits
    {
        get => _traits;
        init => _traits = ToReadOnlyStringList(value);
    }

    public int Level { get; init; }
    public int PlayCost { get; init; } = -1;
    public IReadOnlyList<EvoCostDefinition> EvoCosts
    {
        get => _evoCosts;
        init => _evoCosts = ToReadOnlyList(value);
    }

    public IReadOnlyList<CardKind> CardKinds
    {
        get => _cardKinds;
        init => _cardKinds = ToReadOnlyList(value);
    }

    public int DP
    {
        get => _dp;
        init => _dp = value;
    }

    public int Dp
    {
        get => _dp;
        init => _dp = value;
    }

    public CardRarity Rarity { get; init; } = CardRarity.None;

    public string CardEffectClassName
    {
        get => _cardEffectClassName;
        init => _cardEffectClassName = value ?? string.Empty;
    }

    public string EffectClassName
    {
        get => _cardEffectClassName;
        init => _cardEffectClassName = value ?? string.Empty;
    }

    public int MaxCountInDeck { get; init; } = 4;
    public int OverflowMemory { get; init; }
    public int LinkDP { get; init; }
    public int SecurityAttackModifier { get; init; }

    public IReadOnlyList<CardColor> OptionCardColorRequirements
    {
        get => _optionCardColorRequirements;
        init => _optionCardColorRequirements = ToReadOnlyList(value);
    }

    public IReadOnlyList<Mechanic> Mechanics
    {
        get => _mechanics;
        init => _mechanics = ToReadOnlyList(value);
    }

    public IReadOnlyList<EvolutionRequirement> EvolutionRequirements
    {
        get => _evolutionRequirements;
        init => _evolutionRequirements = ToReadOnlyList(value);
    }

    public IReadOnlyList<PlayRequirement> PlayRequirements
    {
        get => _playRequirements;
        init => _playRequirements = ToReadOnlyList(value);
    }

    public IReadOnlyList<BattleKeyword> BattleKeywords
    {
        get => _battleKeywords;
        init => _battleKeywords = ToReadOnlyList(value);
    }

    public int LinkedMax { get; init; } = 1;

    public bool IsAce => OverflowMemory >= 1;
    public bool IsDualCard => CardKinds.Count > 1;
    public bool IsPermanent =>
        CardKinds.Contains(CardKind.Digimon)
        || CardKinds.Contains(CardKind.Tamer)
        || CardKinds.Contains(CardKind.DigiEgg);

    private static IReadOnlyList<T> ToReadOnlyList<T>(IEnumerable<T>? values) =>
        values is null ? Array.Empty<T>() : Array.AsReadOnly(values.ToArray());

    private static IReadOnlyList<string> ToReadOnlyStringList(IEnumerable<string>? values) =>
        values is null
            ? Array.Empty<string>()
            : Array.AsReadOnly(values
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray());
}

public sealed record EvoCostDefinition(CardColor CardColor, int Level, int MemoryCost);
