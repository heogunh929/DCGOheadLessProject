using DCGO.RL.Engine.CardEffects;

namespace DCGO.RL.Engine.Validation;

public enum AssetRegistrySourceStatus
{
    Available,
    SourceUnavailable,
}

public enum AssetRegistryMappingSeverity
{
    Info,
    Warning,
    NeedsReview,
    Error,
}

public sealed record AssetCardDefinitionId(string CardId, int CardIndex, string VariantKey)
{
    public string StableId =>
        string.IsNullOrWhiteSpace(VariantKey)
            ? $"{CardId}#{CardIndex}"
            : $"{CardId}#{CardIndex}:{VariantKey}";
}

public sealed record AssetCardMetadata(
    AssetCardDefinitionId DefinitionId,
    string AssetName,
    string RelativePath,
    string CardEffectClassName)
{
    public string CardId => DefinitionId.CardId;
    public int CardIndex => DefinitionId.CardIndex;
    public string VariantKey => DefinitionId.VariantKey;
    public bool HasSourceEffectClass => !string.IsNullOrWhiteSpace(CardEffectClassName);
}

public sealed record AssetRegistryMappingCardReport(
    AssetCardDefinitionId DefinitionId,
    string AssetPath,
    string AssetEffectClassName,
    string RegistryEffectClassName,
    string SourceEffectClassName,
    CardEffectPortingStatus? PortingStatus,
    bool HasRegisteredScript,
    bool HasCardFile,
    bool HasSourceEffectBody);

public sealed record AssetRegistryMappingIssue(
    AssetRegistryMappingSeverity Severity,
    string Code,
    string Message,
    IReadOnlyList<AssetCardDefinitionId> Assets);

public sealed record AssetRegistryMappingReport(
    string TargetName,
    AssetRegistrySourceStatus SourceStatus,
    IReadOnlyList<AssetRegistryMappingCardReport> Cards,
    IReadOnlyList<AssetRegistryMappingIssue> Issues)
{
    public bool HasBlockingFindings =>
        SourceStatus != AssetRegistrySourceStatus.Available
        || Issues.Any(issue => issue.Severity is AssetRegistryMappingSeverity.Error or AssetRegistryMappingSeverity.NeedsReview);

    public bool IsValid => !HasBlockingFindings;

    public IReadOnlyList<string> ToKoreanSummaryLines()
    {
        var lines = new List<string>
        {
            $"대상: {TargetName}",
            $"source 상태: {SourceStatus}",
            $"asset 정의 수: {Cards.Count}",
            $"blocking finding 수: {Issues.Count(issue => issue.Severity is AssetRegistryMappingSeverity.Error or AssetRegistryMappingSeverity.NeedsReview)}",
        };

        foreach (var group in Issues.GroupBy(issue => issue.Severity).OrderByDescending(group => group.Key))
        {
            lines.Add($"{group.Key}: {group.Count()}건");
        }

        return lines;
    }
}

public sealed record AssetRegistryMappingRequest(
    string TargetName,
    IReadOnlyList<AssetCardMetadata> Assets,
    ICardScriptRegistry CardScriptRegistry,
    AssetRegistrySourceStatus SourceStatus = AssetRegistrySourceStatus.Available,
    IReadOnlyDictionary<string, CardEffectPortingStatus>? StatusSnapshot = null,
    IReadOnlyDictionary<string, string>? CardFileContentsByCardId = null,
    IReadOnlyDictionary<string, string>? SourceEffectRelativePathsByClass = null);

public sealed record AssetRegistryLocalAuditRequest(
    string TargetName,
    string WorkspaceRoot,
    string DcgoRoot,
    ICardScriptRegistry CardScriptRegistry,
    IReadOnlyDictionary<string, CardEffectPortingStatus>? StatusSnapshot = null);

public sealed class AssetRegistryMappingValidator
{
    public AssetRegistryMappingReport Validate(AssetRegistryMappingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var issues = new List<AssetRegistryMappingIssue>();
        var cardReports = new List<AssetRegistryMappingCardReport>();
        var recordsByCardId = request.CardScriptRegistry.PortingRecords
            .GroupBy(record => record.CardId, StringComparer.Ordinal)
            .ToDictionary(group => group.Key, group => group.Single(), StringComparer.Ordinal);
        var cardFileContents = request.CardFileContentsByCardId ?? new Dictionary<string, string>(StringComparer.Ordinal);
        var sourceEffectPaths = request.SourceEffectRelativePathsByClass ?? new Dictionary<string, string>(StringComparer.Ordinal);
        var statusSnapshot = request.StatusSnapshot;

        if (request.SourceStatus == AssetRegistrySourceStatus.SourceUnavailable)
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Warning,
                "SourceUnavailable",
                "DCGO source root is unavailable; asset/source mapping audit cannot be completed.",
                Array.Empty<AssetCardDefinitionId>()));
        }

        foreach (var group in request.Assets.GroupBy(asset => asset.CardId, StringComparer.Ordinal))
        {
            var distinctEffectClasses = group
                .Select(asset => NormalizeEffectClass(asset.CardEffectClassName))
                .Distinct(StringComparer.Ordinal)
                .ToArray();

            if (distinctEffectClasses.Length > 1)
            {
                issues.Add(new AssetRegistryMappingIssue(
                    AssetRegistryMappingSeverity.NeedsReview,
                    "VariantEffectClassSplit",
                    $"CardId '{group.Key}' has variant-specific CardEffectClassName values: {string.Join(", ", distinctEffectClasses.Select(DisplayEffectClass))}.",
                    group.Select(asset => asset.DefinitionId).ToArray()));
            }
        }

        foreach (var asset in request.Assets.OrderBy(asset => asset.CardId, StringComparer.Ordinal).ThenBy(asset => asset.CardIndex))
        {
            recordsByCardId.TryGetValue(asset.CardId, out var record);
            var hasCardFile = cardFileContents.ContainsKey(asset.CardId);
            var assetEffectClass = NormalizeEffectClass(asset.CardEffectClassName);
            var sourceEffectClass = record?.EffectiveSourceEffectClassName ?? string.Empty;
            var hasSourceEffectBody = string.IsNullOrWhiteSpace(assetEffectClass)
                || sourceEffectPaths.ContainsKey(assetEffectClass);

            cardReports.Add(new AssetRegistryMappingCardReport(
                asset.DefinitionId,
                asset.RelativePath,
                assetEffectClass,
                record?.EffectClassName ?? string.Empty,
                sourceEffectClass,
                record?.Status,
                record is not null,
                hasCardFile,
                hasSourceEffectBody));

            ValidateRegistryRecord(asset, record, issues);
            ValidateStatusSnapshot(asset, record, statusSnapshot, issues);
            ValidateCardFile(asset, record, hasCardFile, cardFileContents, sourceEffectPaths, issues);
            ValidateSourceEffect(asset, record, request.SourceStatus, sourceEffectPaths, issues);
        }

        return new AssetRegistryMappingReport(
            request.TargetName,
            request.SourceStatus,
            cardReports,
            issues);
    }

    public AssetRegistryMappingReport ValidateLocalSource(AssetRegistryLocalAuditRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!Directory.Exists(request.DcgoRoot))
        {
            return Validate(new AssetRegistryMappingRequest(
                request.TargetName,
                Array.Empty<AssetCardMetadata>(),
                request.CardScriptRegistry,
                AssetRegistrySourceStatus.SourceUnavailable,
                request.StatusSnapshot));
        }

        var assets = LoadStarterCardAssets(request.DcgoRoot);
        var sourceEffectPaths = LoadSourceEffectClassPaths(request.DcgoRoot);
        var cardFileContents = LoadCardFileContents(request.WorkspaceRoot, assets.Select(asset => asset.CardId));

        return Validate(new AssetRegistryMappingRequest(
            request.TargetName,
            assets,
            request.CardScriptRegistry,
            AssetRegistrySourceStatus.Available,
            request.StatusSnapshot,
            cardFileContents,
            sourceEffectPaths));
    }

    public static IReadOnlyList<AssetCardMetadata> LoadStarterCardAssets(string dcgoRoot)
    {
        var cardBaseRoot = Path.Combine(dcgoRoot, "Assets", "CardBaseEntity");
        if (!Directory.Exists(cardBaseRoot))
        {
            return Array.Empty<AssetCardMetadata>();
        }

        return Directory.GetFiles(cardBaseRoot, "*.asset", SearchOption.AllDirectories)
            .Select(path => TryLoadAssetMetadata(dcgoRoot, path))
            .Where(asset => asset is not null)
            .Select(asset => asset!)
            .Where(asset => asset.CardId.StartsWith("ST1-", StringComparison.Ordinal)
                || asset.CardId.StartsWith("ST2-", StringComparison.Ordinal)
                || asset.CardId.StartsWith("ST3-", StringComparison.Ordinal))
            .OrderBy(asset => asset.CardId, StringComparer.Ordinal)
            .ThenBy(asset => asset.CardIndex)
            .ToArray();
    }

    public static IReadOnlyDictionary<string, string> LoadSourceEffectClassPaths(string dcgoRoot)
    {
        var sourceRoot = Path.Combine(dcgoRoot, "Assets", "Scripts", "CardEffect");
        if (!Directory.Exists(sourceRoot))
        {
            return new Dictionary<string, string>(StringComparer.Ordinal);
        }

        var dcgoName = Path.GetFileName(Path.GetFullPath(dcgoRoot).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        return Directory.GetFiles(sourceRoot, "*.cs", SearchOption.AllDirectories)
            .GroupBy(path => Path.GetFileNameWithoutExtension(path), StringComparer.Ordinal)
            .ToDictionary(
                group => group.Key,
                group => $"{dcgoName}/{Path.GetRelativePath(dcgoRoot, group.First()).Replace('\\', '/')}",
                StringComparer.Ordinal);
    }

    public static IReadOnlyDictionary<string, string> LoadCardFileContents(string workspaceRoot, IEnumerable<string> cardIds)
    {
        var contents = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var cardId in cardIds.Distinct(StringComparer.Ordinal))
        {
            var path = RlCardEffectPath(workspaceRoot, cardId);
            if (File.Exists(path))
            {
                contents[cardId] = File.ReadAllText(path);
            }
        }

        return contents;
    }

    private static void ValidateRegistryRecord(
        AssetCardMetadata asset,
        CardEffectPortingRecord? record,
        List<AssetRegistryMappingIssue> issues)
    {
        if (record is null)
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "MissingRegistryScript",
                $"Asset '{asset.DefinitionId.StableId}' has no registered card script.",
                new[] { asset.DefinitionId }));
            return;
        }

        if (asset.HasSourceEffectClass && record.Status == CardEffectPortingStatus.NoEffect)
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.NeedsReview,
                "FalseNoEffect",
                $"Asset '{asset.DefinitionId.StableId}' has source effect class '{asset.CardEffectClassName}' but registry status is NoEffect.",
                new[] { asset.DefinitionId }));
        }

        if (asset.HasSourceEffectClass
            && record.Status != CardEffectPortingStatus.NoEffect
            && string.IsNullOrWhiteSpace(record.EffectiveSourceEffectClassName))
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "MissingSourceEffectClassMetadata",
                $"Registry record for '{asset.CardId}' does not retain source effect class metadata.",
                new[] { asset.DefinitionId }));
        }

        if (asset.HasSourceEffectClass
            && record.Status != CardEffectPortingStatus.NoEffect
            && !string.Equals(record.EffectiveSourceEffectClassName, asset.CardEffectClassName, StringComparison.Ordinal))
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "SourceEffectClassMismatch",
                $"Asset '{asset.DefinitionId.StableId}' source effect class '{asset.CardEffectClassName}' does not match registry source effect class '{record.EffectiveSourceEffectClassName}'.",
                new[] { asset.DefinitionId }));
        }

        if (asset.HasSourceEffectClass
            && record.Status != CardEffectPortingStatus.NoEffect
            && string.Equals(record.EffectiveSourceEffectClassName, asset.CardEffectClassName, StringComparison.Ordinal)
            && !string.Equals(record.EffectClassName, asset.CardEffectClassName, StringComparison.Ordinal))
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Info,
                "RegistryAliasSeparatedFromSourceEffectClass",
                $"Asset '{asset.DefinitionId.StableId}' uses source effect class '{asset.CardEffectClassName}' while registry alias is '{DisplayEffectClass(record.EffectClassName)}'.",
                new[] { asset.DefinitionId }));
        }
    }

    private static void ValidateStatusSnapshot(
        AssetCardMetadata asset,
        CardEffectPortingRecord? record,
        IReadOnlyDictionary<string, CardEffectPortingStatus>? statusSnapshot,
        List<AssetRegistryMappingIssue> issues)
    {
        if (record is null || statusSnapshot is null)
        {
            return;
        }

        if (!statusSnapshot.TryGetValue(asset.CardId, out var status))
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "MissingStatusSnapshot",
                $"Status snapshot does not contain '{asset.CardId}'.",
                new[] { asset.DefinitionId }));
        }
        else if (status != record.Status)
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "StaleStatusSnapshot",
                $"Status snapshot for '{asset.CardId}' is '{status}' but registry is '{record.Status}'.",
                new[] { asset.DefinitionId }));
        }
    }

    private static void ValidateCardFile(
        AssetCardMetadata asset,
        CardEffectPortingRecord? record,
        bool hasCardFile,
        IReadOnlyDictionary<string, string> cardFileContents,
        IReadOnlyDictionary<string, string> sourceEffectPaths,
        List<AssetRegistryMappingIssue> issues)
    {
        if (record is not null && record.IsRunnableInValidatedDeck && !hasCardFile)
        {
            issues.Add(new AssetRegistryMappingIssue(
                AssetRegistryMappingSeverity.Error,
                "MissingPerCardFile",
                $"Runnable registry record '{asset.CardId}' has no per-card RL.Engine file.",
                new[] { asset.DefinitionId }));
            return;
        }

        if (!hasCardFile || !cardFileContents.TryGetValue(asset.CardId, out var content))
        {
            return;
        }

        if (!asset.HasSourceEffectClass && record?.Status == CardEffectPortingStatus.NoEffect)
        {
            if (!content.Contains("NoEffect", StringComparison.Ordinal)
                || (!content.Contains("No original CardEffect source file exists", StringComparison.Ordinal)
                    && !content.Contains("No set-local CardEffect source file exists", StringComparison.Ordinal)))
            {
                issues.Add(new AssetRegistryMappingIssue(
                    AssetRegistryMappingSeverity.Error,
                    "MissingNoEffectProvenance",
                    $"NoEffect card file for '{asset.CardId}' does not document missing source provenance.",
                    new[] { asset.DefinitionId }));
            }
        }

        if (asset.HasSourceEffectClass)
        {
            var hasMappingText = sourceEffectPaths.TryGetValue(asset.CardEffectClassName, out var sourcePath)
                && !string.IsNullOrWhiteSpace(sourcePath)
                    ? content.Contains(sourcePath, StringComparison.Ordinal)
                    : content.Contains(asset.CardEffectClassName, StringComparison.Ordinal);

            if (!hasMappingText)
            {
                issues.Add(new AssetRegistryMappingIssue(
                    AssetRegistryMappingSeverity.Error,
                    "MissingSourceMappingComment",
                    $"Card file for '{asset.CardId}' does not retain source effect mapping '{asset.CardEffectClassName}'.",
                    new[] { asset.DefinitionId }));
            }
        }
    }

    private static void ValidateSourceEffect(
        AssetCardMetadata asset,
        CardEffectPortingRecord? record,
        AssetRegistrySourceStatus sourceStatus,
        IReadOnlyDictionary<string, string> sourceEffectPaths,
        List<AssetRegistryMappingIssue> issues)
    {
        if (!asset.HasSourceEffectClass || sourceStatus == AssetRegistrySourceStatus.SourceUnavailable)
        {
            return;
        }

        if (!sourceEffectPaths.ContainsKey(asset.CardEffectClassName))
        {
            issues.Add(new AssetRegistryMappingIssue(
                record?.Status == CardEffectPortingStatus.NoEffect
                    ? AssetRegistryMappingSeverity.NeedsReview
                    : AssetRegistryMappingSeverity.Error,
                "MissingSourceEffectBody",
                $"Asset '{asset.DefinitionId.StableId}' references source effect class '{asset.CardEffectClassName}', but no source body was found.",
                new[] { asset.DefinitionId }));
        }
    }

    private static AssetCardMetadata? TryLoadAssetMetadata(string dcgoRoot, string path)
    {
        var values = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var rawLine in File.ReadLines(path))
        {
            var line = rawLine.Trim();
            var separator = line.IndexOf(':');
            if (separator <= 0)
            {
                continue;
            }

            var key = line[..separator].Trim();
            if (key is not ("m_Name" or "CardIndex" or "CardEffectClassName" or "CardID"))
            {
                continue;
            }

            values[key] = line[(separator + 1)..].Trim();
        }

        if (!values.TryGetValue("CardID", out var cardId) || string.IsNullOrWhiteSpace(cardId))
        {
            return null;
        }

        var assetName = values.TryGetValue("m_Name", out var name) && !string.IsNullOrWhiteSpace(name)
            ? name
            : Path.GetFileNameWithoutExtension(path);
        var cardIndex = values.TryGetValue("CardIndex", out var indexText) && int.TryParse(indexText, out var parsedIndex)
            ? parsedIndex
            : 0;
        var effectClass = values.GetValueOrDefault("CardEffectClassName", string.Empty);
        var relativePath = Path.GetRelativePath(dcgoRoot, path).Replace('\\', '/');

        return new AssetCardMetadata(
            new AssetCardDefinitionId(cardId, cardIndex, InferVariantKey(cardId, assetName)),
            assetName,
            relativePath,
            effectClass);
    }

    private static string InferVariantKey(string cardId, string assetName)
    {
        var prefix = cardId.Replace("-", "_", StringComparison.Ordinal);
        if (string.Equals(assetName, prefix, StringComparison.Ordinal))
        {
            return "base";
        }

        var variantPrefix = $"{prefix}_";
        return assetName.StartsWith(variantPrefix, StringComparison.Ordinal)
            ? assetName[variantPrefix.Length..]
            : assetName;
    }

    private static string RlCardEffectPath(string workspaceRoot, string cardId)
    {
        var (set, color) = cardId.Split('-', StringSplitOptions.TrimEntries) switch
        {
            ["ST1", _] => ("ST1", "Red"),
            ["ST2", _] => ("ST2", "Blue"),
            ["ST3", _] => ("ST3", "Yellow"),
            _ => throw new InvalidOperationException($"Unsupported starter CardId for asset registry audit: {cardId}"),
        };

        return Path.Combine(
            workspaceRoot,
            "src",
            "DCGO.RL.Engine",
            "CardEffects",
            set,
            color,
            $"{cardId.Replace("-", "_", StringComparison.Ordinal)}.cs");
    }

    private static string NormalizeEffectClass(string effectClassName) =>
        effectClassName?.Trim() ?? string.Empty;

    private static string DisplayEffectClass(string effectClassName) =>
        string.IsNullOrWhiteSpace(effectClassName) ? "<none>" : effectClassName;
}
