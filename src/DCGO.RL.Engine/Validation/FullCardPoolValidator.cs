using System.Text.Json;

namespace DCGO.RL.Engine.Validation;

public enum FullCardPoolValidationDecision
{
    Passed,
    Blocked,
}

public enum FullCardPoolValidationIssueCategory
{
    RegistryMissing,
    SourceMappingMissing,
    FalseNoEffect,
    MissingSourceBody,
    UnknownVariant,
    UnsupportedOrPartial,
    UnsupportedMechanic,
    StructureViolation,
    DeckCardMissing,
    DeckCardBlocked,
}

public sealed record FullCardPoolValidationRequest(
    string TargetName,
    string WorkspaceRoot,
    string ScaffoldIndexPath = "docs/generated/full-card-source-scaffold/index.json",
    IReadOnlyList<FullCardPoolDeckValidationRequest>? Decks = null);

public sealed record FullCardPoolDeckValidationRequest(
    string Name,
    IReadOnlyList<string> DefinitionStableIds);

public sealed record FullCardPoolValidationIssue(
    FullCardPoolValidationIssueCategory Category,
    string Code,
    string Severity,
    string Message,
    string? DefinitionStableId = null,
    string? CardId = null,
    string? SetId = null,
    string? Status = null,
    IReadOnlyList<string>? Evidence = null);

public sealed record FullCardPoolDeckValidationResult(
    string Name,
    int RequestedCardCount,
    int KnownCardCount,
    FullCardPoolValidationDecision Decision,
    IReadOnlyDictionary<string, int> StatusCounts,
    IReadOnlyList<FullCardPoolValidationIssue> Issues);

public sealed record FullCardPoolMechanicCountReport(
    IReadOnlyDictionary<string, int> Timings,
    IReadOnlyDictionary<string, int> Flags,
    IReadOnlyDictionary<string, int> SelectionKinds,
    IReadOnlyDictionary<string, int> RootZones);

public sealed record FullCardPoolValidationReport(
    string SchemaVersion,
    string TargetName,
    FullCardPoolValidationDecision Decision,
    string Scope,
    int ManifestRecordCount,
    int CardMappingRecordCount,
    int SourceScaffoldRecordCount,
    IReadOnlyDictionary<string, int> StatusCounts,
    IReadOnlyDictionary<string, int> SetCounts,
    IReadOnlyDictionary<string, int> ColorCounts,
    FullCardPoolMechanicCountReport MechanicCounts,
    IReadOnlyList<FullCardPoolValidationIssue> BlockingIssues,
    IReadOnlyList<FullCardPoolDeckValidationResult> DeckValidations)
{
    public bool IsBlocked => Decision == FullCardPoolValidationDecision.Blocked;
}

internal sealed record FullCardPoolCardMapping(
    string DefinitionStableId,
    string CardId,
    int CardIndex,
    string VariantKey,
    string SetId,
    string AssetColorPath,
    string AssetKindPath,
    string? CardEffectClassName,
    IReadOnlyList<string> SourceEffectPaths,
    IReadOnlyList<string> SourceEffectScaffoldRefs,
    bool SourceBodyExists,
    string MappingStatus,
    string StatusReason);

public sealed class FullCardPoolValidator
{
    public const string SchemaVersion = "dcgo.full-card-pool-validation-baseline.65.v1";

    private static readonly HashSet<string> AllowedStatuses = new(StringComparer.Ordinal)
    {
        "NoEffect",
        "Implemented",
        "Verified",
        "Unsupported",
        "PartiallyImplemented",
        "StubbedForValidation",
        "NeedsSourceReview",
        "UnknownVariant",
    };

    private static readonly HashSet<string> BlockingStatuses = new(StringComparer.Ordinal)
    {
        "Unsupported",
        "PartiallyImplemented",
        "StubbedForValidation",
        "NeedsSourceReview",
        "UnknownVariant",
    };

    public FullCardPoolValidationReport Validate(FullCardPoolValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.TargetName);
        ArgumentException.ThrowIfNullOrWhiteSpace(request.WorkspaceRoot);

        var workspaceRoot = Path.GetFullPath(request.WorkspaceRoot);
        var indexPath = Resolve(workspaceRoot, request.ScaffoldIndexPath);
        using var index = JsonDocument.Parse(File.ReadAllText(indexPath));
        var generatedFiles = index.RootElement.GetProperty("generatedFiles");
        var mechanicInventoryPath = Resolve(
            workspaceRoot,
            index.RootElement.GetProperty("inputManifests").GetProperty("fullMechanicInventory").GetProperty("path").GetString() ?? string.Empty);
        using var mechanicInventory = JsonDocument.Parse(File.ReadAllText(mechanicInventoryPath));

        var cards = LoadCardMappings(workspaceRoot, generatedFiles.GetProperty("cardMappings"));
        var sources = LoadSourceScaffolds(workspaceRoot, generatedFiles.GetProperty("sourceScaffolds"));
        var catalogs = LoadCatalogs(workspaceRoot, generatedFiles.GetProperty("setCatalogs"));
        var statusCounts = Count(cards.Select(card => card.MappingStatus));
        var sourceIds = sources
            .Select(source => RequiredString(source, "sourceScaffoldId"))
            .ToHashSet(StringComparer.Ordinal);

        var issues = new List<FullCardPoolValidationIssue>();
        ValidateCatalogs(catalogs, issues);
        ValidateCards(cards, sourceIds, issues);
        ValidateSources(sources, issues);
        ValidateMechanicInventory(mechanicInventory.RootElement, issues);

        var manifestRecordCount = index.RootElement.GetProperty("summary").GetProperty("manifestRecordCount").GetInt32();
        if (manifestRecordCount != cards.Count)
        {
            issues.Add(new FullCardPoolValidationIssue(
                FullCardPoolValidationIssueCategory.StructureViolation,
                "ManifestCountMismatch",
                "Blocking",
                $"Scaffold card mapping count does not match manifest record count: manifest={manifestRecordCount}, scaffold={cards.Count}."));
        }

        var deckValidations = (request.Decks ?? Array.Empty<FullCardPoolDeckValidationRequest>())
            .Select(deck => ValidateDeck(deck, cards))
            .ToArray();
        issues.AddRange(deckValidations.SelectMany(deck => deck.Issues));

        var decision = issues.Count == 0
            ? FullCardPoolValidationDecision.Passed
            : FullCardPoolValidationDecision.Blocked;

        return new FullCardPoolValidationReport(
            SchemaVersion,
            request.TargetName,
            decision,
            "Queue 65 establishes a full-card-pool validation baseline. A blocked result is expected until every source-bearing card is ported or explicitly reviewed.",
            manifestRecordCount,
            cards.Count,
            sources.Count,
            statusCounts,
            Count(cards.Select(card => card.SetId)),
            Count(cards.Select(card => card.AssetColorPath)),
            BuildMechanicCounts(sources),
            issues,
            deckValidations);
    }

    private static void ValidateCards(
        IReadOnlyList<FullCardPoolCardMapping> cards,
        HashSet<string> sourceIds,
        List<FullCardPoolValidationIssue> issues)
    {
        var seenDefinitionIds = new HashSet<string>(StringComparer.Ordinal);
        foreach (var card in cards)
        {
            if (!seenDefinitionIds.Add(card.DefinitionStableId))
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.StructureViolation,
                    "DuplicateDefinitionStableId",
                    card,
                    $"Duplicate card mapping definitionStableId: {card.DefinitionStableId}."));
            }

            if (!card.DefinitionStableId.Contains('#', StringComparison.Ordinal)
                || !card.DefinitionStableId.Contains('@', StringComparison.Ordinal))
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.UnknownVariant,
                    "UnknownVariantIdentity",
                    card,
                    $"Card mapping identity is not variant-aware: {card.DefinitionStableId}."));
            }

            if (!AllowedStatuses.Contains(card.MappingStatus))
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.StructureViolation,
                    "UnknownStatus",
                    card,
                    $"Card mapping status is not allowed by queue 65 baseline: {card.MappingStatus}."));
            }

            if (string.Equals(card.MappingStatus, "NoEffect", StringComparison.Ordinal)
                && !string.IsNullOrWhiteSpace(card.CardEffectClassName))
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.FalseNoEffect,
                    "FalseNoEffect",
                    card,
                    "NoEffect is only allowed when the source asset has an empty CardEffectClassName."));
            }

            if (!string.IsNullOrWhiteSpace(card.CardEffectClassName)
                && !card.SourceBodyExists)
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.MissingSourceBody,
                    "MissingSourceBody",
                    card,
                    "CardEffectClassName exists but no matching source body was found."));
            }

            if (!string.IsNullOrWhiteSpace(card.CardEffectClassName)
                && card.SourceBodyExists
                && card.SourceEffectPaths.Count != 1)
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.SourceMappingMissing,
                    "AmbiguousSourceBody",
                    card,
                    "CardEffectClassName resolves to zero or multiple source bodies; one source mapping is required."));
            }

            foreach (var sourceRef in card.SourceEffectScaffoldRefs)
            {
                if (!sourceIds.Contains(sourceRef))
                {
                    issues.Add(CardIssue(
                        FullCardPoolValidationIssueCategory.SourceMappingMissing,
                        "MissingSourceScaffoldRef",
                        card,
                        $"Card mapping references a missing source scaffold: {sourceRef}."));
                }
            }

            if (BlockingStatuses.Contains(card.MappingStatus))
            {
                issues.Add(CardIssue(
                    FullCardPoolValidationIssueCategory.UnsupportedOrPartial,
                    $"Status{card.MappingStatus}",
                    card,
                    $"Card is not runnable in the full-card-pool completion gate: {card.MappingStatus}."));
            }
        }
    }

    private static void ValidateSources(
        IReadOnlyList<JsonElement> sources,
        List<FullCardPoolValidationIssue> issues)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var source in sources)
        {
            var sourceId = RequiredString(source, "sourceScaffoldId");
            if (!seen.Add(sourceId))
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.StructureViolation,
                    "DuplicateSourceScaffoldId",
                    "Blocking",
                    $"Duplicate source scaffold id: {sourceId}.",
                    Evidence: new[] { sourceId }));
            }

            var status = RequiredString(source, "scaffoldStatus");
            if (!AllowedStatuses.Contains(status))
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.StructureViolation,
                    "UnknownSourceStatus",
                    "Blocking",
                    $"Source scaffold status is not allowed: {status}.",
                    Status: status,
                    Evidence: new[] { sourceId }));
            }

            if (BlockingStatuses.Contains(status))
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.UnsupportedOrPartial,
                    $"SourceStatus{status}",
                    "Blocking",
                    $"Source scaffold is not implemented for full-card-pool completion: {status}.",
                    Status: status,
                    Evidence: new[] { sourceId, RequiredString(source, "sourcePath") }));
            }

        }
    }

    private static void ValidateMechanicInventory(JsonElement inventory, List<FullCardPoolValidationIssue> issues)
    {
        foreach (var section in new[] { "timings", "features", "selections", "specialMechanics", "keywords" })
        {
            if (!inventory.TryGetProperty(section, out var items) || items.ValueKind != JsonValueKind.Array)
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.StructureViolation,
                    "MissingMechanicInventorySection",
                    "Blocking",
                    $"Full mechanic inventory section is missing: {section}.",
                    Evidence: new[] { section }));
                continue;
            }

            foreach (var item in items.EnumerateArray())
            {
                var name = RequiredString(item, "name");
                var status = RequiredString(item, "mappingStatus");
                if (!AllowedStatuses.Contains(status) && !string.Equals(status, "NotReferenced", StringComparison.Ordinal))
                {
                    issues.Add(new FullCardPoolValidationIssue(
                        FullCardPoolValidationIssueCategory.StructureViolation,
                        "UnknownMechanicStatus",
                        "Blocking",
                        $"Mechanic inventory status is not allowed: {section}/{name}={status}.",
                        Status: status,
                        Evidence: new[] { section, name }));
                }

                if (status is "Unsupported" or "PartiallyImplemented" or "NeedsSourceReview")
                {
                    issues.Add(new FullCardPoolValidationIssue(
                        FullCardPoolValidationIssueCategory.UnsupportedMechanic,
                        $"Mechanic{status}",
                        "Blocking",
                        $"Mechanic inventory item is not ready for full-card-pool completion: {section}/{name}={status}.",
                        Status: status,
                        Evidence: new[] { section, name }));
                }
            }
        }
    }

    private static void ValidateCatalogs(
        IReadOnlyList<JsonElement> catalogs,
        List<FullCardPoolValidationIssue> issues)
    {
        foreach (var catalog in catalogs)
        {
            var setId = RequiredString(catalog, "setId");
            if (!catalog.GetProperty("registryOnly").GetBoolean())
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.RegistryMissing,
                    "CatalogNotRegistryOnly",
                    "Blocking",
                    $"Set catalog is not registry-only: {setId}.",
                    SetId: setId));
            }

            if (catalog.GetProperty("cardMappingRecordCount").GetInt32() == 0
                && catalog.GetProperty("sourceScaffoldRecordCount").GetInt32() == 0)
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.RegistryMissing,
                    "EmptySetCatalog",
                    "Blocking",
                    $"Set catalog has no card mapping and no source scaffold records: {setId}.",
                    SetId: setId));
            }
        }
    }

    private static FullCardPoolDeckValidationResult ValidateDeck(
        FullCardPoolDeckValidationRequest deck,
        IReadOnlyList<FullCardPoolCardMapping> allCards)
    {
        ArgumentNullException.ThrowIfNull(deck);
        var byDefinitionId = allCards.ToDictionary(card => card.DefinitionStableId, StringComparer.Ordinal);
        var known = new List<FullCardPoolCardMapping>();
        var issues = new List<FullCardPoolValidationIssue>();

        foreach (var id in deck.DefinitionStableIds)
        {
            if (!byDefinitionId.TryGetValue(id, out var card))
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.DeckCardMissing,
                    "DeckCardMissing",
                    "Blocking",
                    $"Deck references a definitionStableId that is not present in the full-card-pool scaffold: {id}.",
                    DefinitionStableId: id,
                    Evidence: new[] { deck.Name }));
                continue;
            }

            known.Add(card);
            if (BlockingStatuses.Contains(card.MappingStatus))
            {
                issues.Add(new FullCardPoolValidationIssue(
                    FullCardPoolValidationIssueCategory.DeckCardBlocked,
                    "DeckCardBlocked",
                    "Blocking",
                    $"Deck card is blocked by full-card-pool baseline status: {card.MappingStatus}.",
                    DefinitionStableId: card.DefinitionStableId,
                    CardId: card.CardId,
                    SetId: card.SetId,
                    Status: card.MappingStatus,
                    Evidence: new[] { deck.Name, card.AssetKindPath }));
            }
        }

        return new FullCardPoolDeckValidationResult(
            deck.Name,
            deck.DefinitionStableIds.Count,
            known.Count,
            issues.Count == 0 ? FullCardPoolValidationDecision.Passed : FullCardPoolValidationDecision.Blocked,
            Count(known.Select(card => card.MappingStatus)),
            issues);
    }

    private static FullCardPoolValidationIssue CardIssue(
        FullCardPoolValidationIssueCategory category,
        string code,
        FullCardPoolCardMapping card,
        string message) =>
        new(
            category,
            code,
            "Blocking",
            message,
            card.DefinitionStableId,
            card.CardId,
            card.SetId,
            card.MappingStatus,
            new[] { card.StatusReason });

    private static FullCardPoolMechanicCountReport BuildMechanicCounts(IReadOnlyList<JsonElement> sourceRecords)
    {
        var timings = new List<string>();
        var flags = new List<string>();
        var selectionKinds = new List<string>();
        var rootZones = new List<string>();

        foreach (var source in sourceRecords)
        {
            timings.AddRange(ReadStringArray(source, "timings"));
            selectionKinds.AddRange(ReadStringArray(source, "selectionKinds"));
            rootZones.AddRange(ReadStringArray(source, "rootZones"));

            if (source.TryGetProperty("flags", out var flagsElement) && flagsElement.ValueKind == JsonValueKind.Object)
            {
                flags.AddRange(flagsElement.EnumerateObject()
                    .Where(property => property.Value.ValueKind == JsonValueKind.True)
                    .Select(property => property.Name));
            }
        }

        return new FullCardPoolMechanicCountReport(
            Count(timings),
            Count(flags),
            Count(selectionKinds),
            Count(rootZones));
    }

    private static IReadOnlyList<FullCardPoolCardMapping> LoadCardMappings(string root, JsonElement pathsElement)
    {
        var records = new List<FullCardPoolCardMapping>();
        foreach (var path in pathsElement.EnumerateArray().Select(item => item.GetString() ?? string.Empty))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(Resolve(root, path)));
            foreach (var item in document.RootElement.GetProperty("records").EnumerateArray())
            {
                records.Add(new FullCardPoolCardMapping(
                    RequiredString(item, "definitionStableId"),
                    RequiredString(item, "cardId"),
                    item.GetProperty("cardIndex").GetInt32(),
                    RequiredString(item, "variantKey"),
                    RequiredString(item, "setId"),
                    RequiredString(item, "assetColorPath"),
                    RequiredString(item, "assetKindPath"),
                    OptionalString(item, "cardEffectClassName"),
                    ReadStringArray(item, "sourceEffectPaths"),
                    ReadStringArray(item, "sourceEffectScaffoldRefs"),
                    item.GetProperty("sourceBodyExists").GetBoolean(),
                    RequiredString(item, "mappingStatus"),
                    RequiredString(item, "statusReason")));
            }
        }

        return records;
    }

    private static IReadOnlyList<JsonElement> LoadSourceScaffolds(string root, JsonElement pathsElement)
    {
        var records = new List<JsonElement>();
        foreach (var path in pathsElement.EnumerateArray().Select(item => item.GetString() ?? string.Empty))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(Resolve(root, path)));
            records.AddRange(document.RootElement.GetProperty("records").EnumerateArray().Select(record => record.Clone()));
        }

        return records;
    }

    private static IReadOnlyList<JsonElement> LoadCatalogs(string root, JsonElement pathsElement)
    {
        var records = new List<JsonElement>();
        foreach (var path in pathsElement.EnumerateArray().Select(item => item.GetString() ?? string.Empty))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(Resolve(root, path)));
            records.Add(document.RootElement.Clone());
        }

        return records;
    }

    private static IReadOnlyDictionary<string, int> Count(IEnumerable<string> values) =>
        values
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .GroupBy(value => value, StringComparer.Ordinal)
            .OrderBy(group => group.Key, StringComparer.Ordinal)
            .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);

    private static IReadOnlyList<string> ReadStringArray(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var array) || array.ValueKind != JsonValueKind.Array)
        {
            return Array.Empty<string>();
        }

        return array.EnumerateArray()
            .Select(item => item.GetString() ?? string.Empty)
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .ToArray();
    }

    private static string Resolve(string root, string path) =>
        Path.IsPathRooted(path) ? path : Path.Combine(root, path.Replace('/', Path.DirectorySeparatorChar));

    private static string RequiredString(JsonElement element, string propertyName)
    {
        var value = OptionalString(element, propertyName);
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDataException($"JSON property '{propertyName}' is missing or empty.");
        }

        return value;
    }

    private static string OptionalString(JsonElement element, string propertyName)
    {
        if (!element.TryGetProperty(propertyName, out var property) || property.ValueKind is JsonValueKind.Null)
        {
            return string.Empty;
        }

        return property.GetString() ?? string.Empty;
    }
}
