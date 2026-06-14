namespace DCGO.RL.Engine.Domain;

public sealed record MaterialRequirement
{
    private IReadOnlyList<string> _definitionIds = Array.Empty<string>();
    private IReadOnlyList<CardKind> _cardKinds = Array.Empty<CardKind>();
    private IReadOnlyList<CardColor> _cardColors = Array.Empty<CardColor>();
    private IReadOnlyList<int> _levels = Array.Empty<int>();
    private IReadOnlyList<Zone> _zones = Array.Empty<Zone>();

    public string Label { get; init; } = string.Empty;
    public int Count { get; init; } = 1;

    public IReadOnlyList<string> DefinitionIds
    {
        get => _definitionIds;
        init => _definitionIds = ToReadOnlyList(value);
    }

    public IReadOnlyList<CardKind> CardKinds
    {
        get => _cardKinds;
        init => _cardKinds = ToReadOnlyList(value);
    }

    public IReadOnlyList<CardColor> CardColors
    {
        get => _cardColors;
        init => _cardColors = ToReadOnlyList(value);
    }

    public IReadOnlyList<int> Levels
    {
        get => _levels;
        init => _levels = ToReadOnlyList(value);
    }

    public IReadOnlyList<Zone> Zones
    {
        get => _zones;
        init => _zones = ToReadOnlyList(value);
    }

    public bool AllowBattleArea { get; init; }
    public bool AllowHand { get; init; }
    public bool AllowTrash { get; init; }
    public bool AllowEvolutionSources { get; init; }
    public bool AllowLinkedCards { get; init; }

    private static IReadOnlyList<T> ToReadOnlyList<T>(IEnumerable<T>? values) =>
        values is null ? Array.Empty<T>() : Array.AsReadOnly(values.ToArray());
}

public sealed record EvolutionRequirement
{
    private IReadOnlyList<MaterialRequirement> _materials = Array.Empty<MaterialRequirement>();

    public EvolutionMode Mode { get; init; }
    public int Cost { get; init; }
    public IReadOnlyList<MaterialRequirement> Materials
    {
        get => _materials;
        init => _materials = ToReadOnlyList(value);
    }

    public MaterialRequirement? TargetRequirement { get; init; }
    public MaterialRequirement? BurstTamerRequirement { get; init; }
    public MaterialRequirement? AppFusionLinkRequirement { get; init; }

    private static IReadOnlyList<T> ToReadOnlyList<T>(IEnumerable<T>? values) =>
        values is null ? Array.Empty<T>() : Array.AsReadOnly(values.ToArray());
}

public sealed record PlayRequirement
{
    private IReadOnlyList<MaterialRequirement> _materials = Array.Empty<MaterialRequirement>();

    public PlayMode Mode { get; init; }
    public int? FixedCost { get; init; }
    public int ReduceCost { get; init; }
    public int ReduceCostPerMaterial { get; init; }
    public int LinkCost { get; init; }
    public int MaxMaterials { get; init; }
    public IReadOnlyList<MaterialRequirement> Materials
    {
        get => _materials;
        init => _materials = ToReadOnlyList(value);
    }

    public MaterialRequirement? LinkTargetRequirement { get; init; }

    private static IReadOnlyList<T> ToReadOnlyList<T>(IEnumerable<T>? values) =>
        values is null ? Array.Empty<T>() : Array.AsReadOnly(values.ToArray());
}
