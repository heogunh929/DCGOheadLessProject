using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

var workspace = Directory.GetCurrentDirectory();
var generatedDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");
var docsDir = Path.Combine(workspace, "docs", "as-is-restart");
var inventoryPath = Path.Combine(generatedDir, "asis-full-file-inventory.json");
var rolePath = Path.Combine(generatedDir, "asis-role-reclassification.json");
var sourceAuditPath = Path.Combine(generatedDir, "asis-source-of-truth-audit-summary.json");
var csharpFileIndexPath = Path.Combine(generatedDir, "asis-csharp-file-index.json");

var detailMarkdownPath = Path.Combine(docsDir, "GOAL_06_ASIS_CARD_DATA_STRUCTURE.md");
var summaryMarkdownPath = Path.Combine(docsDir, "asis-card-data-structure-summary.md");
var structureJsonPath = Path.Combine(generatedDir, "asis-card-data-structure.json");
var fieldSummaryJsonPath = Path.Combine(generatedDir, "asis-card-data-field-summary.json");
var cardIndexJsonPath = Path.Combine(generatedDir, "asis-cardbaseentity-card-index.json");

using var inventoryJson = JsonDocument.Parse(File.ReadAllBytes(inventoryPath));
using var roleJson = JsonDocument.Parse(File.ReadAllBytes(rolePath));
using var sourceAuditJson = JsonDocument.Parse(File.ReadAllBytes(sourceAuditPath));
using var csharpFileIndexJson = JsonDocument.Parse(File.ReadAllBytes(csharpFileIndexPath));

var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");
var asisRoot = inventoryJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var assetRootRelative = "Assets/CardBaseEntity";
var assetRootPath = Path.Combine(asisRoot, "Assets", "CardBaseEntity");
var csharpRelativePaths = csharpFileIndexJson.RootElement
    .GetProperty("files")
    .EnumerateArray()
    .Select(item => item.GetProperty("relativePath").GetString() ?? "")
    .Where(path => path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
    .ToHashSet(StringComparer.OrdinalIgnoreCase);

var roleFiles = roleJson.RootElement.GetProperty("files").EnumerateArray().Select(LoadRoleFile).ToList();
var cardBaseFiles = roleFiles
    .Where(file => file.RelativePath == assetRootRelative || file.RelativePath.StartsWith(assetRootRelative + "/", StringComparison.Ordinal))
    .ToList();
var assetFiles = cardBaseFiles
    .Where(file => file.Extension.Equals(".asset", StringComparison.OrdinalIgnoreCase))
    .OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase)
    .ToList();
var metaPathSet = cardBaseFiles
    .Where(file => file.Extension.Equals(".meta", StringComparison.OrdinalIgnoreCase))
    .Select(file => file.RelativePath)
    .ToHashSet(StringComparer.OrdinalIgnoreCase);

var fieldStats = new Dictionary<string, FieldStats>(StringComparer.Ordinal);
var records = new List<CardAssetRecord>();
var parseWarnings = new List<ParseWarningRecord>();

foreach (var file in assetFiles)
{
    var record = ParseAsset(file);
    records.Add(record);
    foreach (var warning in record.ParseWarnings)
    {
        parseWarnings.Add(new ParseWarningRecord(record.RelativePath, warning));
    }
}

var cardRecords = records.Where(record => !record.IsLoaderAsset).ToList();
var loaderRecords = records.Where(record => record.IsLoaderAsset).ToList();
var guidGroups = records.Where(record => !string.IsNullOrWhiteSpace(record.MetaGuid)).GroupBy(record => record.MetaGuid, StringComparer.OrdinalIgnoreCase).Where(group => group.Count() > 1).ToList();
var cardIndexDuplicates = cardRecords.Where(record => record.CardIndex.HasValue).GroupBy(record => record.CardIndex!.Value).Where(group => group.Count() > 1).ToList();
var cardIdGroups = cardRecords.Where(record => !string.IsNullOrWhiteSpace(record.CardId)).GroupBy(record => record.CardId, StringComparer.OrdinalIgnoreCase).ToList();
var duplicateCardIdGroups = cardIdGroups.Where(group => group.Count() > 1).ToList();
var missingEffectSource = cardRecords.Where(record => !string.IsNullOrWhiteSpace(record.CardEffectClassName) && record.CardEffectSourceCandidateCount == 0).ToList();
var noEffectMarkers = cardRecords.Where(record => string.Equals(record.CardEffectClassName, "NoEffect", StringComparison.OrdinalIgnoreCase)).ToList();
var emptyEffectClassRecords = cardRecords.Where(record => string.IsNullOrWhiteSpace(record.CardEffectClassName)).ToList();

var fieldSummary = fieldStats.Values
    .OrderByDescending(field => field.PresenceCount)
    .ThenBy(field => field.FieldName, StringComparer.Ordinal)
    .Select(field => field.ToRecord())
    .ToList();

var summary = new CardDataStructureSummaryDocument(
    "dcgo.as-is-card-data-structure-summary.v1",
    generatedAtUtc,
    "GOAL 06 AS-IS CardBaseEntity and ScriptableObject Data Structure Analysis",
    inventoryPath,
    rolePath,
    sourceAuditPath,
    csharpFileIndexPath,
    inventoryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
    roleJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
    sourceAuditJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
    csharpFileIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
    asisRoot,
    new CardDataStructureTotals(
        cardBaseFiles.Count,
        assetFiles.Count,
        cardRecords.Count,
        loaderRecords.Count,
        metaPathSet.Count,
        records.Count(record => record.HasMetaSidecar),
        records.Count(record => !record.HasMetaSidecar),
        records.Count(record => !string.IsNullOrWhiteSpace(record.MetaGuid)),
        guidGroups.Count,
        fieldSummary.Count,
        cardRecords.Count(record => !string.IsNullOrWhiteSpace(record.CardId)),
        cardRecords.Count(record => string.IsNullOrWhiteSpace(record.CardId)),
        cardRecords.Count(record => record.CardIndex.HasValue),
        cardRecords.Count(record => !record.CardIndex.HasValue),
        cardIndexDuplicates.Count,
        cardIdGroups.Count,
        duplicateCardIdGroups.Count,
        emptyEffectClassRecords.Count,
        noEffectMarkers.Count,
        missingEffectSource.Count,
        parseWarnings.Count),
    CountBy(cardRecords, record => record.SetCode),
    CountBy(cardRecords, record => record.ColorFolder),
    CountBy(cardRecords, record => record.CardKindFolder),
    CountBy(cardRecords, record => record.CardKindRawText),
    CountBy(cardRecords, record => record.RarityText),
    CountBy(cardRecords, record => record.LevelText),
    CountBy(cardRecords, record => record.EvoCostCount.ToString()),
    CountBy(cardRecords, record => record.ColorCodeSignature),
    CountBy(cardRecords, record => record.EffectClassStatus),
    CountBy(cardRecords, record => record.CardEffectSourceCandidateCount > 0 ? "HasSourceCandidate" : "NoSourceCandidate"),
    TopGroups(duplicateCardIdGroups, group => group.Key, 200),
    cardIndexDuplicates.Select(group => new DuplicateIntGroupRecord(group.Key, group.Select(record => record.RelativePath).OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToArray())).OrderBy(group => group.Key).Take(200).ToList(),
    missingEffectSource.Select(ToCardReference).Take(500).ToList(),
    emptyEffectClassRecords.Select(ToCardReference).Take(500).ToList(),
    noEffectMarkers.Select(ToCardReference).Take(500).ToList(),
    parseWarnings.Take(500).ToList(),
    new[]
    {
        "GOAL 07에서는 이 산출물의 field summary, effect class status, CardKind/Level/Color/EvoCost 분포를 headless 필요 여부 matrix 입력으로 사용한다.",
        "CardBaseEntity asset 전문 텍스트는 복제하지 않았고, 긴 효과 설명은 길이/존재 여부만 기록했다.",
        "CardID 중복은 variant/parallel art 가능성이 있으므로 오류로 확정하지 않고 variant group 후보로 유지한다.",
        "CardEffectClassName source candidate 미존재 항목은 GOAL 07/08에서 NoEffect, external tool, generated registry와 교차 검토한다."
    });

var structure = new CardDataStructureDocument(
    "dcgo.as-is-card-data-structure.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    assetRootRelative,
    summary,
    fieldSummary,
    cardRecords.Take(250).ToList(),
    loaderRecords,
    new CardDataAnomalySamples(
        missingEffectSource.Select(ToCardReference).Take(1000).ToList(),
        emptyEffectClassRecords.Select(ToCardReference).Take(1000).ToList(),
        noEffectMarkers.Select(ToCardReference).Take(1000).ToList(),
        cardIndexDuplicates.Select(group => new DuplicateIntGroupRecord(group.Key, group.Select(record => record.RelativePath).OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToArray())).OrderBy(group => group.Key).Take(500).ToList(),
        TopGroups(duplicateCardIdGroups, group => group.Key, 500)));

var cardIndex = new CardIndexDocument(
    "dcgo.as-is-cardbaseentity-card-index.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    assetRootRelative,
    cardRecords);

var fieldSummaryDocument = new CardDataFieldSummaryDocument(
    "dcgo.as-is-card-data-field-summary.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    fieldSummary);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

WriteUtf8(structureJsonPath, JsonSerializer.Serialize(structure, jsonOptions) + Environment.NewLine);
WriteUtf8(fieldSummaryJsonPath, JsonSerializer.Serialize(fieldSummaryDocument, jsonOptions) + Environment.NewLine);
WriteUtf8(cardIndexJsonPath, JsonSerializer.Serialize(cardIndex, jsonOptions) + Environment.NewLine);
WriteUtf8(detailMarkdownPath, BuildDetailMarkdown(summary, fieldSummary));
WriteUtf8(summaryMarkdownPath, BuildSummaryMarkdown(summary));

Console.WriteLine(JsonSerializer.Serialize(new
{
    asisRoot,
    totalAssetCount = summary.Totals.TotalAssetCount,
    cardAssetCount = summary.Totals.CardAssetCount,
    loaderAssetCount = summary.Totals.LoaderAssetCount,
    fieldCount = summary.Totals.FieldNameCount,
    missingMetaSidecarCount = summary.Totals.MissingMetaSidecarCount,
    missingCardIdCount = summary.Totals.MissingCardIdCount,
    duplicateCardIdGroupCount = summary.Totals.DuplicateCardIdGroupCount,
    missingEffectSourceCandidateCount = summary.Totals.MissingEffectSourceCandidateCount,
    noEffectMarkerCount = summary.Totals.NoEffectMarkerCount,
    parseWarningCount = summary.Totals.ParseWarningCount
}, jsonOptions));

CardAssetRecord ParseAsset(RoleFile file)
{
    var lines = File.ReadAllLines(file.Path);
    var blocks = ExtractTopLevelBlocks(lines);
    foreach (var block in blocks)
    {
        if (!fieldStats.TryGetValue(block.Key, out var stat))
        {
            stat = new FieldStats(block.Key);
            fieldStats[block.Key] = stat;
        }
        stat.Record(block);
    }

    var top = blocks.ToDictionary(block => block.Key, block => block, StringComparer.Ordinal);
    var mScriptGuid = ExtractGuid(GetScalar(top, "m_Script"));
    var metaPath = file.RelativePath + ".meta";
    var fullMetaPath = file.Path + ".meta";
    var metaGuid = File.Exists(fullMetaPath) ? ReadMetaGuid(fullMetaPath) : "";
    var parts = file.RelativePath.Split('/');
    var isLoader = file.RelativePath.Equals("Assets/CardBaseEntity/CardEntity_JSONLoader.asset", StringComparison.OrdinalIgnoreCase);
    var setCode = isLoader ? "(loader)" : parts.Length > 2 ? parts[2] : "(root)";
    var colorFolder = isLoader ? "(loader)" : parts.Length > 3 ? parts[3] : "(root)";
    var kindFolder = isLoader ? "Loader" : parts.Length > 5 ? parts[4] : "(missing kind folder)";
    var cardId = CleanScalar(GetScalar(top, "CardID"));
    var cardEffectClassName = CleanScalar(GetScalar(top, "CardEffectClassName"));
    var candidatePaths = CandidateEffectSourcePaths(cardEffectClassName);
    var missingRequired = new List<string>();
    var warnings = new List<string>();

    var cardIndex = ParseInt(GetScalar(top, "CardIndex"));
    var playCost = ParseInt(GetScalar(top, "PlayCost"));
    var level = ParseInt(GetScalar(top, "Level"));
    var dp = ParseInt(GetScalar(top, "DP"));
    var rarity = ParseInt(GetScalar(top, "rarity"));
    var overflowMemory = ParseInt(GetScalar(top, "OverflowMemory"));
    var linkDp = ParseInt(GetScalar(top, "LinkDP"));
    var maxCount = ParseInt(GetScalar(top, "MaxCountInDeck"));
    var cardKindRaw = ParseLittleEndianIntHex(GetScalar(top, "cardKind"));
    var colorCodes = ParseLittleEndianIntArrayHex(GetScalar(top, "cardColors"));
    var evoCosts = ParseEvoCosts(top.TryGetValue("EvoCosts", out var evoBlock) ? evoBlock : null);

    if (!isLoader)
    {
        if (string.IsNullOrWhiteSpace(cardId)) missingRequired.Add("CardID");
        if (!cardIndex.HasValue) missingRequired.Add("CardIndex");
        if (string.IsNullOrWhiteSpace(cardEffectClassName)) missingRequired.Add("CardEffectClassName");
    }

    if (top.Count == 0)
    {
        warnings.Add("No top-level MonoBehaviour fields parsed.");
    }
    if (!File.Exists(fullMetaPath))
    {
        warnings.Add("Missing .meta sidecar.");
    }

    var effectDescriptions = new DescriptionStats(
        LengthOf(top, "EffectDiscription_ENG"),
        LengthOf(top, "InheritedEffectDiscription_ENG"),
        LengthOf(top, "SecurityEffectDiscription_ENG"),
        LengthOf(top, "EffectDiscription_JPN"),
        LengthOf(top, "InheritedEffectDiscription_JPN"),
        LengthOf(top, "SecurityEffectDiscription_JPN"));

    var nonEmptyOptional = new[]
    {
        ("dualEffect", CleanScalar(GetScalar(top, "dualEffect"))),
        ("OptionCardColorRequirements", CleanScalar(GetScalar(top, "OptionCardColorRequirements"))),
        ("OptionEffect", CleanScalar(GetScalar(top, "OptionEffect"))),
        ("LinkEffect", CleanScalar(GetScalar(top, "LinkEffect"))),
        ("LinkRequirement", CleanScalar(GetScalar(top, "LinkRequirement")))
    }
        .Where(item => IsMeaningful(item.Item2))
        .Select(item => item.Item1)
        .ToArray();

    return new CardAssetRecord(
        file.Path,
        file.RelativePath,
        file.Directory,
        file.FileName,
        metaPath,
        File.Exists(fullMetaPath),
        metaGuid,
        mScriptGuid,
        CleanScalar(GetScalar(top, "m_Name")),
        setCode,
        colorFolder,
        kindFolder,
        isLoader,
        cardId,
        BaseCardId(cardId),
        VariantSuffix(file.FileName, cardId),
        cardIndex,
        CleanScalar(GetScalar(top, "CardSpriteName")),
        cardEffectClassName,
        candidatePaths,
        candidatePaths.Count,
        EffectClassStatus(cardEffectClassName, candidatePaths.Count),
        colorCodes,
        colorCodes.Length == 0 ? "(empty)" : string.Join(",", colorCodes),
        playCost,
        evoCosts,
        evoCosts.Count,
        level,
        level?.ToString() ?? "(missing)",
        cardKindRaw,
        cardKindRaw.HasValue ? cardKindRaw.Value.ToString() : "(missing)",
        CleanScalar(GetScalar(top, "CardName_ENG")),
        ParseStringList(top, "Form_ENG"),
        ParseStringList(top, "Attribute_ENG"),
        ParseStringList(top, "Type_ENG"),
        dp,
        rarity,
        rarity?.ToString() ?? "(missing)",
        overflowMemory,
        linkDp,
        maxCount,
        effectDescriptions,
        nonEmptyOptional,
        missingRequired.ToArray(),
        warnings.ToArray());
}

List<FieldBlock> ExtractTopLevelBlocks(string[] lines)
{
    var result = new List<FieldBlock>();
    for (var i = 0; i < lines.Length; i++)
    {
        var line = lines[i];
        if (!line.StartsWith("  ", StringComparison.Ordinal) || line.StartsWith("    ", StringComparison.Ordinal))
        {
            continue;
        }

        var trimmed = line[2..];
        if (trimmed.StartsWith("- ", StringComparison.Ordinal))
        {
            continue;
        }
        var colon = trimmed.IndexOf(':');
        if (colon <= 0)
        {
            continue;
        }

        var key = trimmed[..colon];
        var firstValue = trimmed[(colon + 1)..].TrimStart();
        var blockLines = new List<string> { line };
        var j = i + 1;
        while (j < lines.Length)
        {
            var next = lines[j];
            if (next.StartsWith("  ", StringComparison.Ordinal) &&
                !next.StartsWith("    ", StringComparison.Ordinal) &&
                !next[2..].StartsWith("- ", StringComparison.Ordinal) &&
                next[2..].Contains(':'))
            {
                break;
            }
            blockLines.Add(next);
            j++;
        }

        result.Add(new FieldBlock(key, firstValue, blockLines));
        i = j - 1;
    }
    return result;
}

string GetScalar(IReadOnlyDictionary<string, FieldBlock> fields, string key)
{
    return fields.TryGetValue(key, out var block) ? block.AsJoinedValue() : "";
}

int LengthOf(IReadOnlyDictionary<string, FieldBlock> fields, string key)
{
    var value = GetScalar(fields, key);
    return IsMeaningful(value) ? CleanScalar(value).Length : 0;
}

string CleanScalar(string value)
{
    var trimmed = value.Trim();
    if (trimmed.Length >= 2 && ((trimmed[0] == '\'' && trimmed[^1] == '\'') || (trimmed[0] == '"' && trimmed[^1] == '"')))
    {
        trimmed = trimmed[1..^1];
    }
    return trimmed.Trim();
}

bool IsMeaningful(string value)
{
    var cleaned = CleanScalar(value);
    return !string.IsNullOrWhiteSpace(cleaned) && cleaned != "-" && cleaned != "[]";
}

int? ParseInt(string value)
{
    return int.TryParse(CleanScalar(value), out var parsed) ? parsed : null;
}

int? ParseLittleEndianIntHex(string value)
{
    var bytes = ParseHexBytes(value);
    if (bytes.Length < 4) return null;
    return BitConverter.ToInt32(bytes, 0);
}

int[] ParseLittleEndianIntArrayHex(string value)
{
    var bytes = ParseHexBytes(value);
    if (bytes.Length < 4 || bytes.Length % 4 != 0) return Array.Empty<int>();
    var result = new List<int>();
    for (var i = 0; i < bytes.Length; i += 4)
    {
        result.Add(BitConverter.ToInt32(bytes, i));
    }
    return result.ToArray();
}

byte[] ParseHexBytes(string value)
{
    var hex = CleanScalar(value);
    if (string.IsNullOrWhiteSpace(hex) || hex.Length % 2 != 0 || !hex.All(Uri.IsHexDigit))
    {
        return Array.Empty<byte>();
    }
    var bytes = new byte[hex.Length / 2];
    for (var i = 0; i < bytes.Length; i++)
    {
        bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
    }
    return bytes;
}

List<EvoCostRecord> ParseEvoCosts(FieldBlock? block)
{
    var result = new List<EvoCostRecord>();
    if (block is null || block.FirstValue == "[]")
    {
        return result;
    }

    int? color = null;
    int? level = null;
    int? memory = null;
    foreach (var line in block.Lines.Skip(1))
    {
        var trimmed = line.Trim();
        if (trimmed.StartsWith("- ", StringComparison.Ordinal))
        {
            if (color.HasValue || level.HasValue || memory.HasValue)
            {
                result.Add(new EvoCostRecord(color, level, memory));
            }
            color = level = memory = null;
            trimmed = trimmed[2..];
        }

        var colon = trimmed.IndexOf(':');
        if (colon <= 0) continue;
        var key = trimmed[..colon].Trim();
        var value = trimmed[(colon + 1)..].Trim();
        if (key == "CardColor") color = ParseInt(value);
        if (key == "Level") level = ParseInt(value);
        if (key == "MemoryCost") memory = ParseInt(value);
    }
    if (color.HasValue || level.HasValue || memory.HasValue)
    {
        result.Add(new EvoCostRecord(color, level, memory));
    }
    return result;
}

string[] ParseStringList(IReadOnlyDictionary<string, FieldBlock> fields, string key)
{
    if (!fields.TryGetValue(key, out var block) || block.FirstValue == "[]")
    {
        return Array.Empty<string>();
    }

    return block.Lines
        .Skip(1)
        .Select(line => line.Trim())
        .Where(line => line.StartsWith("- ", StringComparison.Ordinal))
        .Select(line => CleanScalar(line[2..]))
        .Where(value => value.Length > 0)
        .ToArray();
}

string ExtractGuid(string value)
{
    var match = Regex.Match(value, @"guid:\s*([0-9a-fA-F]+)");
    return match.Success ? match.Groups[1].Value : "";
}

string ReadMetaGuid(string metaPath)
{
    foreach (var line in File.ReadLines(metaPath))
    {
        if (line.StartsWith("guid:", StringComparison.Ordinal))
        {
            return line["guid:".Length..].Trim();
        }
    }
    return "";
}

string BaseCardId(string cardId)
{
    if (string.IsNullOrWhiteSpace(cardId)) return "";
    var cleaned = cardId.Trim();
    var underscore = cleaned.IndexOf('_');
    return underscore >= 0 ? cleaned[..underscore] : cleaned;
}

string VariantSuffix(string fileName, string cardId)
{
    var name = Path.GetFileNameWithoutExtension(fileName);
    var normalizedId = cardId.Replace("-", "_", StringComparison.Ordinal);
    if (!string.IsNullOrWhiteSpace(normalizedId) && name.StartsWith(normalizedId, StringComparison.OrdinalIgnoreCase))
    {
        return name[normalizedId.Length..].TrimStart('_');
    }
    var match = Regex.Match(name, @"(_P\d+.*|_J)$", RegexOptions.IgnoreCase);
    return match.Success ? match.Value.TrimStart('_') : "";
}

List<string> CandidateEffectSourcePaths(string className)
{
    var cleaned = CleanScalar(className);
    if (string.IsNullOrWhiteSpace(cleaned) || cleaned == "-" || cleaned == "NoEffect")
    {
        return new List<string>();
    }

    var fileName = cleaned + ".cs";
    return csharpRelativePaths
        .Where(path => path.StartsWith("Assets/Scripts/CardEffect/", StringComparison.OrdinalIgnoreCase) && path.EndsWith("/" + fileName, StringComparison.OrdinalIgnoreCase))
        .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
        .Take(20)
        .ToList();
}

string EffectClassStatus(string className, int sourceCandidateCount)
{
    var cleaned = CleanScalar(className);
    if (string.IsNullOrWhiteSpace(cleaned)) return "Empty";
    if (cleaned == "-") return "Dash";
    if (string.Equals(cleaned, "NoEffect", StringComparison.OrdinalIgnoreCase)) return "NoEffectMarker";
    return sourceCandidateCount > 0 ? "HasSourceCandidate" : "NoSourceCandidate";
}

RoleFile LoadRoleFile(JsonElement file)
{
    return new RoleFile(
        file.GetProperty("path").GetString() ?? "",
        file.GetProperty("relativePath").GetString() ?? "",
        file.GetProperty("directory").GetString() ?? "",
        file.GetProperty("fileName").GetString() ?? "",
        file.GetProperty("extension").GetString() ?? "",
        file.GetProperty("sizeBytes").GetInt64());
}

SortedDictionary<string, int> CountBy<T>(IEnumerable<T> source, Func<T, string> selector)
{
    var raw = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var item in source)
    {
        var key = selector(item);
        if (string.IsNullOrWhiteSpace(key)) key = "(empty)";
        raw[key] = raw.TryGetValue(key, out var count) ? count + 1 : 1;
    }
    return SortCounts(raw);
}

SortedDictionary<string, int> SortCounts(Dictionary<string, int> raw)
{
    var result = new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var item in raw.OrderByDescending(item => item.Value).ThenBy(item => item.Key, StringComparer.OrdinalIgnoreCase))
    {
        result[item.Key] = item.Value;
    }
    return result;
}

List<DuplicateStringGroupRecord> TopGroups(IReadOnlyList<IGrouping<string, CardAssetRecord>> groups, Func<IGrouping<string, CardAssetRecord>, string> keySelector, int limit)
{
    return groups
        .OrderByDescending(group => group.Count())
        .ThenBy(group => keySelector(group), StringComparer.OrdinalIgnoreCase)
        .Take(limit)
        .Select(group => new DuplicateStringGroupRecord(keySelector(group), group.Count(), group.Select(ToCardReference).ToList()))
        .ToList();
}

CardReferenceRecord ToCardReference(CardAssetRecord record)
{
    return new CardReferenceRecord(record.RelativePath, record.CardId, record.CardIndex, record.CardNameEng, record.CardKindFolder, record.CardEffectClassName, record.EffectClassStatus);
}

string BuildSummaryMarkdown(CardDataStructureSummaryDocument summary)
{
    return string.Join(Environment.NewLine, new[]
    {
        "# AS-IS Card Data Structure Summary",
        "",
        "> GOAL 06 CardBaseEntity/ScriptableObject 데이터 구조 분석 요약이다. 카드 효과 구현, headless 필요 여부 최종 판정, 기존 구현 trust audit은 수행하지 않는다.",
        "",
        "## 기준선",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        "- 입력 SourceOfTruth audit: `docs/generated/as-is-restart/asis-source-of-truth-audit-summary.json`",
        $"- GOAL 06 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        "",
        "## 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "CardBaseEntity 전체 파일 수", summary.Totals.CardBaseEntityFileCount.ToString() },
            new[] { ".asset 수", summary.Totals.TotalAssetCount.ToString() },
            new[] { "카드 asset 수", summary.Totals.CardAssetCount.ToString() },
            new[] { "loader asset 수", summary.Totals.LoaderAssetCount.ToString() },
            new[] { ".meta sidecar 누락 asset 수", summary.Totals.MissingMetaSidecarCount.ToString() },
            new[] { "field name 수", summary.Totals.FieldNameCount.ToString() },
            new[] { "CardID 누락", summary.Totals.MissingCardIdCount.ToString() },
            new[] { "CardIndex 누락", summary.Totals.MissingCardIndexCount.ToString() },
            new[] { "CardIndex 중복 그룹", summary.Totals.DuplicateCardIndexGroupCount.ToString() },
            new[] { "CardID variant group 수", summary.Totals.DuplicateCardIdGroupCount.ToString() },
            new[] { "CardEffectClassName empty", summary.Totals.EmptyEffectClassNameCount.ToString() },
            new[] { "NoEffect marker", summary.Totals.NoEffectMarkerCount.ToString() },
            new[] { "effect source candidate 미발견", summary.Totals.MissingEffectSourceCandidateCount.ToString() },
            new[] { "parse warning", summary.Totals.ParseWarningCount.ToString() }
        }),
        "",
        "## 카드 종류 폴더별",
        "",
        MarkdownObjectTable("Card kind folder", "수", summary.CardKindFolderCounts),
        "",
        "## Effect class status",
        "",
        MarkdownObjectTable("Status", "수", summary.EffectClassStatusCounts),
        "",
        "## 다음 GOAL 07 추천 입력",
        "",
        string.Join(Environment.NewLine, summary.NextGoalRecommendations.Select(item => "- " + item)),
        ""
    });
}

string BuildDetailMarkdown(CardDataStructureSummaryDocument summary, IReadOnlyList<FieldSummaryRecord> fields)
{
    return string.Join(Environment.NewLine, new[]
    {
        "# GOAL 06 AS-IS Card Data Structure",
        "",
        "> 원본 `Assets/CardBaseEntity` ScriptableObject asset 구조를 전수 분석한 기준선이다. 카드 효과 body 구현, headless 필요 여부 최종 판정, 기존 headless 구현 trust audit은 수행하지 않는다.",
        "",
        "## 입력 기준",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 분석 root: `Assets/CardBaseEntity`",
        "- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        "- 입력 SourceOfTruth audit: `docs/generated/as-is-restart/asis-source-of-truth-audit-summary.json`",
        "- 입력 C# file index: `docs/generated/as-is-restart/asis-csharp-file-index.json`",
        $"- GOAL 06 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        "- 긴 효과 설명 전문 복제: `false`",
        "- CardEffect body 분석/구현: `false`",
        "- headless 필요 여부 최종 판정: `false`",
        "",
        "## 전체 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "CardBaseEntity 전체 파일 수", summary.Totals.CardBaseEntityFileCount.ToString() },
            new[] { ".asset 수", summary.Totals.TotalAssetCount.ToString() },
            new[] { "카드 asset 수", summary.Totals.CardAssetCount.ToString() },
            new[] { "loader asset 수", summary.Totals.LoaderAssetCount.ToString() },
            new[] { ".meta sidecar 보유 asset 수", summary.Totals.AssetsWithMetaSidecarCount.ToString() },
            new[] { ".meta sidecar 누락 asset 수", summary.Totals.MissingMetaSidecarCount.ToString() },
            new[] { "Meta GUID 중복 그룹", summary.Totals.DuplicateMetaGuidGroupCount.ToString() },
            new[] { "field name 수", summary.Totals.FieldNameCount.ToString() },
            new[] { "CardID 보유/누락", $"{summary.Totals.CardIdPresentCount}/{summary.Totals.MissingCardIdCount}" },
            new[] { "CardIndex 보유/누락", $"{summary.Totals.CardIndexPresentCount}/{summary.Totals.MissingCardIndexCount}" },
            new[] { "CardIndex 중복 그룹", summary.Totals.DuplicateCardIndexGroupCount.ToString() },
            new[] { "CardID variant group 수", summary.Totals.DuplicateCardIdGroupCount.ToString() },
            new[] { "CardEffectClassName empty", summary.Totals.EmptyEffectClassNameCount.ToString() },
            new[] { "NoEffect marker", summary.Totals.NoEffectMarkerCount.ToString() },
            new[] { "effect source candidate 미발견", summary.Totals.MissingEffectSourceCandidateCount.ToString() },
            new[] { "parse warning", summary.Totals.ParseWarningCount.ToString() }
        }),
        "",
        "## Set별 카드 수",
        "",
        MarkdownObjectTable("Set", "수", summary.SetCounts),
        "",
        "## Color folder별 카드 수",
        "",
        MarkdownObjectTable("Color folder", "수", summary.ColorFolderCounts),
        "",
        "## Card kind folder별 카드 수",
        "",
        MarkdownObjectTable("Card kind folder", "수", summary.CardKindFolderCounts),
        "",
        "## EvoCost 개수 분포",
        "",
        MarkdownObjectTable("EvoCost count", "카드 수", summary.EvoCostCountDistribution),
        "",
        "## Effect class status",
        "",
        MarkdownObjectTable("Status", "카드 수", summary.EffectClassStatusCounts),
        "",
        "## Field summary",
        "",
        MarkdownTable(new[] { "Field", "present", "non-empty", "kind" }, fields.Select(field => new[] { field.FieldName, field.PresenceCount.ToString(), field.NonEmptyCount.ToString(), string.Join(",", field.ValueKindCounts.Keys) })),
        "",
        "## 주요 후보",
        "",
        $"`CardEffectClassName`은 있으나 `Assets/Scripts/CardEffect/**/{'{'}ClassName{'}'}.cs` 후보를 찾지 못한 카드 수: `{summary.Totals.MissingEffectSourceCandidateCount}`",
        $"`NoEffect` marker 수: `{summary.Totals.NoEffectMarkerCount}`",
        $"CardID variant group 수: `{summary.Totals.DuplicateCardIdGroupCount}`",
        "",
        "## 다음 GOAL 07 추천 입력",
        "",
        string.Join(Environment.NewLine, summary.NextGoalRecommendations.Select(item => "- " + item)),
        "",
        "## 금지 범위 준수",
        "",
        "- `src/` 아래 C# 코드는 수정하지 않았다.",
        "- 원본 `DCGO/Assets`는 수정하지 않았다.",
        "- CardEffect body 구현은 수행하지 않았다.",
        "- C0039 이후 card-porting은 수행하지 않았다.",
        "- headless 필요 여부 최종 판정은 수행하지 않았다.",
        "- 기존 headless 구현 trust audit은 수행하지 않았다.",
        "- commit/push는 수행하지 않았다.",
        ""
    });
}

string MarkdownObjectTable(string keyHeader, string valueHeader, IReadOnlyDictionary<string, int> values)
{
    return MarkdownTable(new[] { keyHeader, valueHeader }, values.Select(item => new[] { item.Key, item.Value.ToString() }));
}

string MarkdownTable(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
{
    var headerArray = headers.Select(EscapeTableValue).ToArray();
    var builder = new StringBuilder();
    builder.Append("| ");
    builder.Append(string.Join(" | ", headerArray));
    builder.AppendLine(" |");
    builder.Append("| ");
    builder.Append(string.Join(" | ", headerArray.Select(_ => "---")));
    builder.AppendLine(" |");
    foreach (var row in rows)
    {
        builder.Append("| ");
        builder.Append(string.Join(" | ", row.Select(EscapeTableValue)));
        builder.AppendLine(" |");
    }
    return builder.ToString().TrimEnd();
}

string EscapeTableValue(string value) => value.Replace("|", "\\|");

void WriteUtf8(string filePath, string content)
{
    File.WriteAllText(filePath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
}

public sealed record RoleFile(string Path, string RelativePath, string Directory, string FileName, string Extension, long SizeBytes);
public sealed record FieldBlock(string Key, string FirstValue, IReadOnlyList<string> Lines)
{
    public string AsJoinedValue()
    {
        if (Lines.Count <= 1) return FirstValue;
        var rest = Lines.Skip(1).Select(line => line.Trim()).Where(line => line.Length > 0);
        return string.Join(" ", new[] { FirstValue }.Concat(rest)).Trim();
    }

    public string ValueKind()
    {
        if (FirstValue == "[]") return "emptyList";
        if (Lines.Any(line => line.TrimStart().StartsWith("- ", StringComparison.Ordinal))) return Lines.Any(line => line.Contains(':')) ? "objectList" : "scalarList";
        if (Lines.Count > 1) return "multilineScalar";
        if (string.IsNullOrWhiteSpace(FirstValue)) return "emptyScalar";
        return "scalar";
    }
}

public sealed class FieldStats
{
    private readonly Dictionary<string, int> _kindCounts = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, int> _sampleCounts = new(StringComparer.Ordinal);

    public FieldStats(string fieldName)
    {
        FieldName = fieldName;
    }

    public string FieldName { get; }
    public int PresenceCount { get; private set; }
    public int NonEmptyCount { get; private set; }

    public void Record(FieldBlock block)
    {
        PresenceCount += 1;
        var value = block.AsJoinedValue();
        if (!string.IsNullOrWhiteSpace(value) && value != "[]") NonEmptyCount += 1;
        var kind = block.ValueKind();
        _kindCounts[kind] = _kindCounts.TryGetValue(kind, out var kindCount) ? kindCount + 1 : 1;
        var sample = value.Length > 120 ? value[..120] : value;
        if (!string.IsNullOrWhiteSpace(sample))
        {
            _sampleCounts[sample] = _sampleCounts.TryGetValue(sample, out var sampleCount) ? sampleCount + 1 : 1;
        }
    }

    public FieldSummaryRecord ToRecord()
    {
        return new FieldSummaryRecord(
            FieldName,
            PresenceCount,
            NonEmptyCount,
            new SortedDictionary<string, int>(_kindCounts, StringComparer.OrdinalIgnoreCase),
            _sampleCounts.OrderByDescending(item => item.Value).ThenBy(item => item.Key, StringComparer.Ordinal).Take(20).Select(item => new FieldSampleRecord(item.Key, item.Value)).ToList());
    }
}

public sealed record FieldSummaryRecord(string FieldName, int PresenceCount, int NonEmptyCount, IReadOnlyDictionary<string, int> ValueKindCounts, IReadOnlyList<FieldSampleRecord> Samples);
public sealed record FieldSampleRecord(string Value, int Count);
public sealed record EvoCostRecord(int? CardColor, int? Level, int? MemoryCost);
public sealed record DescriptionStats(int EffectEngLength, int InheritedEngLength, int SecurityEngLength, int EffectJpnLength, int InheritedJpnLength, int SecurityJpnLength);
public sealed record CardAssetRecord(string Path, string RelativePath, string Directory, string FileName, string MetaRelativePath, bool HasMetaSidecar, string MetaGuid, string UnityScriptGuid, string UnityName, string SetCode, string ColorFolder, string CardKindFolder, bool IsLoaderAsset, string CardId, string BaseCardId, string VariantSuffix, int? CardIndex, string CardSpriteName, string CardEffectClassName, IReadOnlyList<string> CardEffectSourceCandidates, int CardEffectSourceCandidateCount, string EffectClassStatus, int[] ColorCodes, string ColorCodeSignature, int? PlayCost, IReadOnlyList<EvoCostRecord> EvoCosts, int EvoCostCount, int? Level, string LevelText, int? CardKindRaw, string CardKindRawText, string CardNameEng, string[] FormEng, string[] AttributeEng, string[] TypeEng, int? Dp, int? Rarity, string RarityText, int? OverflowMemory, int? LinkDp, int? MaxCountInDeck, DescriptionStats DescriptionStats, string[] NonEmptyOptionalFields, string[] MissingRequiredFields, string[] ParseWarnings);
public sealed record CardReferenceRecord(string RelativePath, string CardId, int? CardIndex, string CardNameEng, string CardKindFolder, string CardEffectClassName, string EffectClassStatus);
public sealed record ParseWarningRecord(string RelativePath, string Warning);
public sealed record DuplicateStringGroupRecord(string Key, int Count, IReadOnlyList<CardReferenceRecord> Cards);
public sealed record DuplicateIntGroupRecord(int Key, string[] RelativePaths);
public sealed record CardDataStructureTotals(int CardBaseEntityFileCount, int TotalAssetCount, int CardAssetCount, int LoaderAssetCount, int MetaFileCount, int AssetsWithMetaSidecarCount, int MissingMetaSidecarCount, int AssetsWithMetaGuidCount, int DuplicateMetaGuidGroupCount, int FieldNameCount, int CardIdPresentCount, int MissingCardIdCount, int CardIndexPresentCount, int MissingCardIndexCount, int DuplicateCardIndexGroupCount, int DistinctCardIdCount, int DuplicateCardIdGroupCount, int EmptyEffectClassNameCount, int NoEffectMarkerCount, int MissingEffectSourceCandidateCount, int ParseWarningCount);
public sealed record CardDataStructureSummaryDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string InputInventoryPath, string InputRolePath, string InputSourceAuditPath, string InputCSharpFileIndexPath, string InputInventoryGeneratedAtUtc, string InputRoleGeneratedAtUtc, string InputSourceAuditGeneratedAtUtc, string InputCSharpFileIndexGeneratedAtUtc, string AsisRoot, CardDataStructureTotals Totals, IReadOnlyDictionary<string, int> SetCounts, IReadOnlyDictionary<string, int> ColorFolderCounts, IReadOnlyDictionary<string, int> CardKindFolderCounts, IReadOnlyDictionary<string, int> CardKindRawCounts, IReadOnlyDictionary<string, int> RarityCounts, IReadOnlyDictionary<string, int> LevelCounts, IReadOnlyDictionary<string, int> EvoCostCountDistribution, IReadOnlyDictionary<string, int> ColorCodeSignatureCounts, IReadOnlyDictionary<string, int> EffectClassStatusCounts, IReadOnlyDictionary<string, int> EffectSourceCandidateStatusCounts, IReadOnlyList<DuplicateStringGroupRecord> DuplicateCardIdGroups, IReadOnlyList<DuplicateIntGroupRecord> DuplicateCardIndexGroups, IReadOnlyList<CardReferenceRecord> MissingEffectSourceCandidates, IReadOnlyList<CardReferenceRecord> EmptyEffectClassNameCards, IReadOnlyList<CardReferenceRecord> NoEffectMarkerCards, IReadOnlyList<ParseWarningRecord> ParseWarnings, IReadOnlyList<string> NextGoalRecommendations);
public sealed record CardDataAnomalySamples(IReadOnlyList<CardReferenceRecord> MissingEffectSourceCandidates, IReadOnlyList<CardReferenceRecord> EmptyEffectClassNameCards, IReadOnlyList<CardReferenceRecord> NoEffectMarkerCards, IReadOnlyList<DuplicateIntGroupRecord> DuplicateCardIndexGroups, IReadOnlyList<DuplicateStringGroupRecord> DuplicateCardIdGroups);
public sealed record CardDataStructureDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, string AssetRootRelativePath, CardDataStructureSummaryDocument Summary, IReadOnlyList<FieldSummaryRecord> FieldSummary, IReadOnlyList<CardAssetRecord> CardSamples, IReadOnlyList<CardAssetRecord> LoaderAssets, CardDataAnomalySamples AnomalySamples);
public sealed record CardDataFieldSummaryDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, IReadOnlyList<FieldSummaryRecord> Fields);
public sealed record CardIndexDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, string AssetRootRelativePath, IReadOnlyList<CardAssetRecord> Cards);
