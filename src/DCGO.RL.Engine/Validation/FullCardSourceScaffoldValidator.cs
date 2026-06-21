using System.Text.Json;

namespace DCGO.RL.Engine.Validation;

public sealed record FullCardSourceScaffoldValidationReport(
    int ManifestRecordCount,
    int CardMappingRecordCount,
    int SourceInventoryRecordCount,
    int SourceScaffoldRecordCount,
    int SetCatalogCount,
    int DuplicateCardIdGroupCount,
    int NeedsSourceReviewMarkerCount,
    int NoEffectMarkerCount,
    IReadOnlyDictionary<string, int> CardMappingStatusCounts,
    IReadOnlyDictionary<string, int> SourceScaffoldStatusCounts,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings)
{
    public bool IsValid => Errors.Count == 0;
}

public sealed class FullCardSourceScaffoldValidator
{
    private const string IndexPath = "docs/generated/full-card-source-scaffold/index.json";

    public FullCardSourceScaffoldValidationReport Validate(string workspaceRoot, string? indexPath = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(workspaceRoot);

        var root = Path.GetFullPath(workspaceRoot);
        var resolvedIndexPath = Resolve(root, indexPath ?? IndexPath);
        using var index = JsonDocument.Parse(File.ReadAllText(resolvedIndexPath));
        var indexRoot = index.RootElement;
        var errors = new List<string>();
        var warnings = new List<string>();

        var generatedFiles = indexRoot.GetProperty("generatedFiles");
        var manifestPath = Resolve(root, indexRoot.GetProperty("inputManifests").GetProperty("fullCardPoolManifest").GetProperty("path").GetString() ?? string.Empty);
        var inventoryPath = Resolve(root, indexRoot.GetProperty("inputManifests").GetProperty("fullMechanicInventory").GetProperty("path").GetString() ?? string.Empty);
        var statusRegistryPath = Resolve(root, generatedFiles.GetProperty("statusRegistry").GetString() ?? string.Empty);

        using var manifest = JsonDocument.Parse(File.ReadAllText(manifestPath));
        using var inventory = JsonDocument.Parse(File.ReadAllText(inventoryPath));
        using var statusRegistry = JsonDocument.Parse(File.ReadAllText(statusRegistryPath));

        var manifestRecords = manifest.RootElement.GetProperty("records").EnumerateArray().ToArray();
        var inventorySourceRecords = inventory.RootElement.GetProperty("effectSourceRecords").EnumerateArray().ToArray();
        var manifestByDefinitionId = manifestRecords.ToDictionary(
            record => RequiredString(record, "definitionStableId"),
            StringComparer.Ordinal);
        var inventorySourceIds = inventorySourceRecords
            .Select(record => SourceScaffoldId(RequiredString(record, "effectClassName"), RequiredString(record, "sourcePath")))
            .ToHashSet(StringComparer.Ordinal);

        var cardRecords = LoadRecordFiles(root, generatedFiles.GetProperty("cardMappings"));
        var sourceRecords = LoadRecordFiles(root, generatedFiles.GetProperty("sourceScaffolds"));
        var catalogs = LoadRootElements(root, generatedFiles.GetProperty("setCatalogs"));

        var cardStatusCounts = new Dictionary<string, int>(StringComparer.Ordinal);
        var sourceStatusCounts = new Dictionary<string, int>(StringComparer.Ordinal);
        var mappedDefinitionIds = new HashSet<string>(StringComparer.Ordinal);
        var mappedCardIds = new HashSet<string>(StringComparer.Ordinal);
        var cardCountBySet = new Dictionary<string, int>(StringComparer.Ordinal);

        foreach (var card in cardRecords)
        {
            var definitionId = RequiredString(card, "definitionStableId");
            var cardId = RequiredString(card, "cardId");
            var setId = RequiredString(card, "setId");
            var status = RequiredString(card, "mappingStatus");
            AddCount(cardStatusCounts, status);
            AddCount(cardCountBySet, setId);
            mappedCardIds.Add(cardId);

            if (!mappedDefinitionIds.Add(definitionId))
            {
                errors.Add($"Duplicate card mapping definitionStableId: {definitionId}");
            }

            if (!definitionId.Contains('#', StringComparison.Ordinal) || !definitionId.Contains('@', StringComparison.Ordinal))
            {
                errors.Add($"definitionStableId is not variant-aware: {definitionId}");
            }

            if (!manifestByDefinitionId.TryGetValue(definitionId, out var sourceManifestRecord))
            {
                errors.Add($"Card mapping is not present in manifest: {definitionId}");
                continue;
            }

            var effectClass = OptionalString(sourceManifestRecord, "cardEffectClassName");
            var sourceBodyExists = sourceManifestRecord.GetProperty("sourceBodyExists").GetBoolean();
            var sourcePathCount = sourceManifestRecord.GetProperty("sourceEffectPaths").GetArrayLength();
            var expectedStatus = ExpectedCardStatus(effectClass, sourceBodyExists, sourcePathCount);
            if (!string.Equals(status, expectedStatus, StringComparison.Ordinal))
            {
                errors.Add($"Card mapping status mismatch for {definitionId}: expected={expectedStatus} actual={status}");
            }

            if (string.Equals(status, "NoEffect", StringComparison.Ordinal)
                && !string.IsNullOrWhiteSpace(effectClass))
            {
                errors.Add($"NoEffect marker has a source effect class: {definitionId} / {effectClass}");
            }
        }

        var sourceScaffoldIds = new HashSet<string>(StringComparer.Ordinal);
        var sourceCountBySet = new Dictionary<string, int>(StringComparer.Ordinal);
        foreach (var source in sourceRecords)
        {
            var sourceId = RequiredString(source, "sourceScaffoldId");
            var sourceSetId = RequiredString(source, "sourceSetId");
            var status = RequiredString(source, "scaffoldStatus");
            AddCount(sourceStatusCounts, status);
            AddCount(sourceCountBySet, sourceSetId);

            if (!sourceScaffoldIds.Add(sourceId))
            {
                errors.Add($"Duplicate source scaffold id: {sourceId}");
            }

            if (!string.Equals(status, "Unsupported", StringComparison.Ordinal))
            {
                errors.Add($"Source scaffold must stay Unsupported in queue 64: {sourceId} / {status}");
            }

            var plannedFile = RequiredString(source, "plannedRlSourceFile");
            if (!plannedFile.StartsWith("src/DCGO.RL.Engine/CardEffects/FullCardPool/SourceEffects/", StringComparison.Ordinal))
            {
                errors.Add($"Source scaffold planned file is outside FullCardPool source scaffold: {sourceId} / {plannedFile}");
            }
        }

        foreach (var sourceId in inventorySourceIds)
        {
            if (!sourceScaffoldIds.Contains(sourceId))
            {
                errors.Add($"Inventory source effect has no scaffold: {sourceId}");
            }
        }

        foreach (var card in cardRecords)
        {
            foreach (var sourceRef in card.GetProperty("sourceEffectScaffoldRefs").EnumerateArray().Select(item => item.GetString() ?? string.Empty))
            {
                if (!sourceScaffoldIds.Contains(sourceRef))
                {
                    errors.Add($"Card mapping references missing source scaffold: {RequiredString(card, "definitionStableId")} -> {sourceRef}");
                }
            }
        }

        foreach (var catalogRoot in catalogs)
        {
            var setId = RequiredString(catalogRoot, "setId");
            if (!catalogRoot.GetProperty("registryOnly").GetBoolean())
            {
                errors.Add($"Set catalog is not registry-only: {setId}");
            }

            foreach (var forbidden in new[] { "effectBodies", "rules", "cardIdBranches", "zoneMutationLogic" })
            {
                if (catalogRoot.TryGetProperty(forbidden, out _))
                {
                    errors.Add($"Set catalog contains forbidden behavior payload '{forbidden}': {setId}");
                }
            }

            var expectedCards = cardCountBySet.TryGetValue(setId, out var mappedCards) ? mappedCards : 0;
            var expectedSources = sourceCountBySet.TryGetValue(setId, out var mappedSources) ? mappedSources : 0;
            var catalogCards = catalogRoot.GetProperty("cardMappingRecordCount").GetInt32();
            var catalogSources = catalogRoot.GetProperty("sourceScaffoldRecordCount").GetInt32();
            if (catalogCards != expectedCards)
            {
                errors.Add($"Set catalog card count mismatch: {setId} expected={expectedCards} actual={catalogCards}");
            }

            if (catalogSources != expectedSources)
            {
                errors.Add($"Set catalog source count mismatch: {setId} expected={expectedSources} actual={catalogSources}");
            }
        }

        if (mappedDefinitionIds.Count != manifestRecords.Length)
        {
            errors.Add($"Card mapping coverage mismatch: manifest={manifestRecords.Length} mapping={mappedDefinitionIds.Count}");
        }

        if (sourceScaffoldIds.Count != inventorySourceIds.Count)
        {
            errors.Add($"Source scaffold coverage mismatch: inventory={inventorySourceIds.Count} scaffold={sourceScaffoldIds.Count}");
        }

        if (mappedCardIds.Count == mappedDefinitionIds.Count)
        {
            errors.Add("Card mappings appear flattened by CardId; duplicate CardId variants were not preserved.");
        }

        var summary = indexRoot.GetProperty("summary");
        if (!summary.GetProperty("duplicateCardIdGroupsPreserved").GetBoolean())
        {
            errors.Add("Index reports duplicate CardId groups were not preserved.");
        }

        if (summary.GetProperty("cardMappingRecordCount").GetInt32() != mappedDefinitionIds.Count)
        {
            errors.Add("Index cardMappingRecordCount does not match loaded card mapping records.");
        }

        if (summary.GetProperty("sourceScaffoldRecordCount").GetInt32() != sourceScaffoldIds.Count)
        {
            errors.Add("Index sourceScaffoldRecordCount does not match loaded source scaffold records.");
        }

        var implementedOrVerified = statusRegistry.RootElement.GetProperty("implementedOrVerifiedCount").GetInt32();
        if (implementedOrVerified != 0)
        {
            errors.Add($"Queue 64 scaffold must not auto-promote implemented/verified statuses: {implementedOrVerified}");
        }

        if (cardStatusCounts.ContainsKey("Implemented") || cardStatusCounts.ContainsKey("Verified"))
        {
            errors.Add("Card mapping status contains Implemented/Verified in queue 64 scaffold.");
        }

        if (sourceStatusCounts.ContainsKey("Implemented") || sourceStatusCounts.ContainsKey("Verified"))
        {
            errors.Add("Source scaffold status contains Implemented/Verified in queue 64 scaffold.");
        }

        return new FullCardSourceScaffoldValidationReport(
            manifestRecords.Length,
            mappedDefinitionIds.Count,
            inventorySourceIds.Count,
            sourceScaffoldIds.Count,
            catalogs.Count,
            summary.GetProperty("duplicateCardIdGroupCount").GetInt32(),
            summary.GetProperty("needsSourceReviewMarkerCount").GetInt32(),
            summary.GetProperty("noEffectMarkerCount").GetInt32(),
            cardStatusCounts,
            sourceStatusCounts,
            errors,
            warnings);
    }

    private static IReadOnlyList<JsonElement> LoadRecordFiles(string root, JsonElement pathsElement)
    {
        var records = new List<JsonElement>();
        foreach (var relativePath in pathsElement.EnumerateArray().Select(item => item.GetString() ?? string.Empty))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(Resolve(root, relativePath)));
            records.AddRange(document.RootElement.GetProperty("records").EnumerateArray().Select(record => record.Clone()));
        }

        return records;
    }

    private static IReadOnlyList<JsonElement> LoadRootElements(string root, JsonElement pathsElement)
    {
        var elements = new List<JsonElement>();
        foreach (var relativePath in pathsElement.EnumerateArray().Select(item => item.GetString() ?? string.Empty))
        {
            using var document = JsonDocument.Parse(File.ReadAllText(Resolve(root, relativePath)));
            elements.Add(document.RootElement.Clone());
        }

        return elements;
    }

    private static string ExpectedCardStatus(string effectClassName, bool sourceBodyExists, int sourcePathCount)
    {
        if (string.IsNullOrWhiteSpace(effectClassName))
        {
            return "NoEffect";
        }

        if (!sourceBodyExists || sourcePathCount != 1)
        {
            return "NeedsSourceReview";
        }

        return "Unsupported";
    }

    private static string SourceScaffoldId(string effectClassName, string sourcePath) =>
        $"{effectClassName}@{sourcePath}";

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

    private static void AddCount(Dictionary<string, int> counts, string key)
    {
        if (counts.TryGetValue(key, out var count))
        {
            counts[key] = count + 1;
            return;
        }

        counts.Add(key, 1);
    }
}
