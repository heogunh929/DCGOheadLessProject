using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var workspace = Directory.GetCurrentDirectory();
var generatedDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");
var docsDir = Path.Combine(workspace, "docs", "as-is-restart");
var inventoryPath = Path.Combine(generatedDir, "asis-full-file-inventory.json");
var rolePath = Path.Combine(generatedDir, "asis-role-reclassification.json");
var detailJsonPath = Path.Combine(generatedDir, "asis-source-of-truth-audit.json");
var summaryJsonPath = Path.Combine(generatedDir, "asis-source-of-truth-audit-summary.json");
var detailMarkdownPath = Path.Combine(docsDir, "GOAL_03_ASIS_SOURCE_OF_TRUTH_AUDIT.md");
var summaryMarkdownPath = Path.Combine(docsDir, "asis-source-of-truth-audit-summary.md");

using var inventoryJson = JsonDocument.Parse(File.ReadAllBytes(inventoryPath));
using var roleJson = JsonDocument.Parse(File.ReadAllBytes(rolePath));

var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");
var inventoryGeneratedAtUtc = inventoryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var roleGeneratedAtUtc = roleJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "";
var asisRoot = roleJson.RootElement.GetProperty("asisRoot").GetString()
    ?? inventoryJson.RootElement.GetProperty("asisRoot").GetString()
    ?? "";

var files = LoadFiles(roleJson.RootElement);
var folders = LoadFolders(roleJson.RootElement);
var filePathSet = files.Select(file => file.RelativePath).ToHashSet(StringComparer.Ordinal);
var folderPathSet = folders.Select(folder => folder.RelativePath).ToHashSet(StringComparer.Ordinal);

var sourceRoots = new[] { "Assets/Scripts", "Assets/CardBaseEntity" };
var sourceRootSidecars = sourceRoots.Select(root => root + ".meta").ToArray();
var sourceFiles = files.Where(IsSourceOfTruthFile).ToList();
var sourceFolders = folders.Where(IsSourceOfTruthFolder).ToList();

var falsePositiveSourceFiles = files
    .Where(file => IsSourceOfTruthFile(file) && !InExpectedSourceRoot(file.RelativePath))
    .Select(ToAuditFileRecord)
    .ToList();
var expectedSourceScopeNotSource = files
    .Where(file => InExpectedSourceFileScope(file.RelativePath) && !IsSourceOfTruthFile(file))
    .Select(file => ToAuditFileRecord(file) with
    {
        AuditReason = sourceRootSidecars.Contains(file.RelativePath, StringComparer.Ordinal)
            ? "Source root folder .meta sidecar is UnityProjectSource in GOAL 02; review whether source-root sidecars should inherit SourceOfTruth."
            : "Path is inside the expected SourceOfTruth roots but GOAL 02 did not assign SourceOfTruth."
    })
    .ToList();
var cardDataOutsideSource = files
    .Where(file => HasClassification(file, "CardDataCandidate") && !IsSourceOfTruthFile(file))
    .Select(ToAuditFileRecord)
    .ToList();
var scriptableOutsideSource = files
    .Where(file => HasClassification(file, "ScriptableObjectCandidate") && !IsSourceOfTruthFile(file))
    .Select(file => ToAuditFileRecord(file) with { AuditReason = ScriptableAuditReason(file) })
    .ToList();
var projectOwnedOutsideSource = files
    .Where(file =>
        file.RelativePath.StartsWith("Assets/", StringComparison.Ordinal) &&
        !IsSourceOfTruthFile(file) &&
        !file.Roles.Any(role => role is "ExternalPackage" or "GeneratedCache" or "BuildArtifact" or "RuntimeOutput" or "VisualOnly" or "SoundOnly"))
    .Select(file => ToAuditFileRecord(file) with { AuditReason = ProjectOwnedAuditReason(file) })
    .ToList();
var editorToolCandidates = projectOwnedOutsideSource
    .Where(file => file.RelativePath.StartsWith("Assets/Editor/", StringComparison.Ordinal) && file.Extension == ".cs")
    .Select(file => file with
    {
        AuditReason = "Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance."
    })
    .ToList();
var visualCodeOutsideSource = files
    .Where(file => file.RelativePath.StartsWith("Assets/", StringComparison.Ordinal) &&
                   file.Extension == ".cs" &&
                   !IsSourceOfTruthFile(file) &&
                   file.Roles.Contains("VisualOnly", StringComparer.Ordinal))
    .Select(ToAuditFileRecord)
    .ToList();
var externalCodeOutsideSource = files
    .Where(file => file.RelativePath.StartsWith("Assets/", StringComparison.Ordinal) &&
                   file.Extension == ".cs" &&
                   !IsSourceOfTruthFile(file) &&
                   file.Roles.Contains("ExternalPackage", StringComparer.Ordinal))
    .Select(ToAuditFileRecord)
    .ToList();

var nonMetaSourceFilesMissingMeta = sourceFiles
    .Where(file => InExpectedSourceRoot(file.RelativePath) &&
                   file.Extension != ".meta" &&
                   !filePathSet.Contains(file.RelativePath + ".meta"))
    .Select(ToAuditFileRecord)
    .ToList();
var orphanSourceMetaFiles = sourceFiles
    .Where(file =>
    {
        if (!InExpectedSourceRoot(file.RelativePath) || file.Extension != ".meta")
        {
            return false;
        }

        var basePath = file.RelativePath[..^".meta".Length];
        return !filePathSet.Contains(basePath) && !folderPathSet.Contains(basePath);
    })
    .Select(ToAuditFileRecord)
    .ToList();
var sourceFoldersMissingMeta = sourceFolders
    .Where(folder => folder.RelativePath != "." &&
                     InExpectedSourceRoot(folder.RelativePath) &&
                     !filePathSet.Contains(folder.RelativePath + ".meta"))
    .Select(ToAuditFolderRecord)
    .ToList();

var sourceCSharpFiles = sourceFiles.Where(file => file.Extension == ".cs").Select(ToAuditFileRecord).ToList();
var sourceCardDataFiles = sourceFiles.Where(file => HasClassification(file, "CardDataCandidate")).Select(ToAuditFileRecord).ToList();
var sourceScriptableFiles = sourceFiles.Where(file => HasClassification(file, "ScriptableObjectCandidate")).Select(ToAuditFileRecord).ToList();

var auditFindings = new[]
{
    Finding("expected-root-source-file-coverage", expectedSourceScopeNotSource.Count == 0 ? "Pass" : "Review", expectedSourceScopeNotSource.Count, "Expected SourceOfTruth file scope entries without SourceOfTruth role."),
    Finding("source-file-outside-expected-root", falsePositiveSourceFiles.Count == 0 ? "Pass" : "Review", falsePositiveSourceFiles.Count, "Files assigned SourceOfTruth outside Assets/Scripts and Assets/CardBaseEntity."),
    Finding("card-data-outside-source", cardDataOutsideSource.Count == 0 ? "Pass" : "Review", cardDataOutsideSource.Count, "CardDataCandidate entries not assigned SourceOfTruth."),
    Finding("scriptable-object-outside-source", scriptableOutsideSource.Count == 0 ? "Pass" : "Review", scriptableOutsideSource.Count, "ScriptableObjectCandidate entries outside SourceOfTruth. Most are expected visual/package/project resources; review before GOAL 06."),
    Finding("project-owned-assets-outside-source", projectOwnedOutsideSource.Count == 0 ? "Pass" : "Review", projectOwnedOutsideSource.Count, "Assets entries classified as project-owned UnityProjectSource but not SourceOfTruth."),
    Finding("editor-tool-code-outside-source", editorToolCandidates.Count == 0 ? "Pass" : "Review", editorToolCandidates.Count, "Assets/Editor C# tools outside SourceOfTruth that may explain card data provenance."),
    Finding("source-file-meta-pair-coverage", nonMetaSourceFilesMissingMeta.Count == 0 ? "Pass" : "Review", nonMetaSourceFilesMissingMeta.Count, "SourceOfTruth non-meta files missing Unity .meta sidecar."),
    Finding("source-meta-orphan-coverage", orphanSourceMetaFiles.Count == 0 ? "Pass" : "Review", orphanSourceMetaFiles.Count, "SourceOfTruth .meta files whose base file/folder is absent."),
    Finding("source-folder-meta-pair-coverage", sourceFoldersMissingMeta.Count == 0 ? "Pass" : "Review", sourceFoldersMissingMeta.Count, "SourceOfTruth folders missing Unity .meta sidecar.")
};

var summary = new SourceOfTruthAuditSummaryDocument(
    "dcgo.as-is-source-of-truth-audit-summary.v1",
    generatedAtUtc,
    "GOAL 03 AS-IS SourceOfTruth Classification Audit",
    inventoryPath,
    rolePath,
    inventoryGeneratedAtUtc,
    roleGeneratedAtUtc,
    asisRoot,
    new AuditPolicy(
        "GOAL 01 inventory metadata and GOAL 02 role metadata only; no source body parsing, call graph analysis, implementation trust audit, or headless necessity judgment.",
        sourceRoots,
        sourceRootSidecars,
        "Review means follow-up evidence is needed. It is not a final SourceOfTruth promotion or exclusion."),
    new AuditTotals(
        files.Count,
        folders.Count,
        sourceFiles.Count,
        sourceFolders.Count,
        sourceCSharpFiles.Count,
        sourceCardDataFiles.Count,
        sourceScriptableFiles.Count,
        expectedSourceScopeNotSource.Count,
        falsePositiveSourceFiles.Count,
        cardDataOutsideSource.Count,
        scriptableOutsideSource.Count,
        projectOwnedOutsideSource.Count,
        editorToolCandidates.Count,
        visualCodeOutsideSource.Count,
        externalCodeOutsideSource.Count,
        nonMetaSourceFilesMissingMeta.Count,
        orphanSourceMetaFiles.Count,
        sourceFoldersMissingMeta.Count),
    CountBy(sourceFiles, file => sourceRoots.FirstOrDefault(root => file.RelativePath.StartsWith(root + "/", StringComparison.Ordinal)) ?? "(outside expected roots)"),
    CountBy(sourceFolders, folder => sourceRoots.FirstOrDefault(root => folder.RelativePath == root || folder.RelativePath.StartsWith(root + "/", StringComparison.Ordinal)) ?? (folder.RelativePath == "." ? "(root)" : "(outside expected roots)")),
    CountBy(sourceFiles, file => string.IsNullOrWhiteSpace(file.Extension) ? "(none)" : file.Extension),
    CountMany(sourceFiles, file => file.Goal01Classifications),
    new CandidateDirectoryCounts(
        CountBy(expectedSourceScopeNotSource, TopDirectory),
        CountBy(scriptableOutsideSource, TopDirectory),
        CountBy(projectOwnedOutsideSource, TopDirectory),
        CountBy(editorToolCandidates, ParentDirectory),
        CountBy(visualCodeOutsideSource, TopDirectory),
        CountBy(externalCodeOutsideSource, TopDirectory)),
    auditFindings,
    new[]
    {
        "GOAL 06은 SourceOfTruth인 Assets/CardBaseEntity/*.asset 전체와 해당 .meta sidecar를 우선 입력으로 사용한다.",
        "Assets/Editor의 카드 데이터 import/fixup 도구 24개는 CardBaseEntity provenance 보조 후보로 별도 목록화하되, battle runtime SourceOfTruth로 즉시 승격하지 않는다.",
        "ScriptableObjectCandidate outside SourceOfTruth 67개는 대부분 visual/package/project resource로 보이며, GOAL 06에서는 CardBaseEntity 구조 분석과 분리한다.",
        "Assets/Scripts.meta 및 Assets/CardBaseEntity.meta는 source root sidecar boundary 후보로 남긴다. 파일/폴더 내부 coverage에는 누락이 없다."
    });

var detail = new SourceOfTruthAuditDocument(
    "dcgo.as-is-source-of-truth-audit.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    inventoryPath,
    rolePath,
    summary,
    new AuditCandidates(
        expectedSourceScopeNotSource,
        falsePositiveSourceFiles,
        cardDataOutsideSource,
        scriptableOutsideSource,
        projectOwnedOutsideSource,
        editorToolCandidates,
        visualCodeOutsideSource,
        externalCodeOutsideSource,
        nonMetaSourceFilesMissingMeta,
        orphanSourceMetaFiles,
        sourceFoldersMissingMeta),
    new AuditSamples(
        sourceCSharpFiles.Take(200).ToList(),
        sourceCardDataFiles.Take(200).ToList(),
        sourceScriptableFiles.Take(200).ToList()));

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

WriteUtf8(summaryJsonPath, JsonSerializer.Serialize(summary, jsonOptions) + Environment.NewLine);
WriteUtf8(detailJsonPath, JsonSerializer.Serialize(detail, jsonOptions) + Environment.NewLine);
WriteUtf8(summaryMarkdownPath, BuildSummaryMarkdown(summary));
WriteUtf8(detailMarkdownPath, BuildDetailMarkdown(summary, detail));

Console.WriteLine(JsonSerializer.Serialize(new
{
    asisRoot,
    summary.Totals.SourceOfTruthFileCount,
    summary.Totals.SourceOfTruthFolderCount,
    summary.Totals.ExpectedSourceScopeNotSourceCount,
    summary.Totals.FalsePositiveSourceFileCount,
    summary.Totals.CardDataOutsideSourceCount,
    summary.Totals.ScriptableObjectOutsideSourceCount,
    summary.Totals.ProjectOwnedAssetsOutsideSourceCount,
    summary.Totals.EditorToolCodeOutsideSourceCount,
    sourceMetaCoverageIssues = summary.Totals.NonMetaSourceFilesMissingMetaCount + summary.Totals.OrphanSourceMetaFileCount + summary.Totals.SourceFoldersMissingMetaCount
}, jsonOptions));

bool IsSourceOfTruthFile(RoleFile file) => file.Roles.Contains("SourceOfTruth", StringComparer.Ordinal);
bool IsSourceOfTruthFolder(RoleFolder folder) => folder.Roles.Contains("SourceOfTruth", StringComparer.Ordinal);
bool InExpectedSourceRoot(string relativePath) => sourceRoots.Any(root => relativePath == root || relativePath.StartsWith(root + "/", StringComparison.Ordinal));
bool InExpectedSourceFileScope(string relativePath) => sourceRootSidecars.Contains(relativePath, StringComparer.Ordinal) || InExpectedSourceRoot(relativePath);
bool HasClassification(RoleFile file, string classification) => file.Goal01Classifications.Contains(classification, StringComparer.Ordinal);

List<RoleFile> LoadFiles(JsonElement root)
{
    var result = new List<RoleFile>();
    foreach (var file in root.GetProperty("files").EnumerateArray())
    {
        result.Add(new RoleFile(
            file.GetProperty("path").GetString() ?? "",
            file.GetProperty("relativePath").GetString() ?? "",
            file.GetProperty("directory").GetString() ?? "",
            file.GetProperty("fileName").GetString() ?? "",
            file.GetProperty("extension").GetString() ?? "",
            file.GetProperty("sizeBytes").GetInt64(),
            file.GetProperty("goal01Classifications").EnumerateArray().Select(item => item.GetString() ?? "").Where(item => item.Length > 0).ToArray(),
            file.GetProperty("roles").EnumerateArray().Select(item => item.GetString() ?? "").Where(item => item.Length > 0).ToArray(),
            file.GetProperty("primaryRole").GetString() ?? "Unknown",
            file.GetProperty("reason").GetString() ?? ""));
    }
    return result;
}

List<RoleFolder> LoadFolders(JsonElement root)
{
    var result = new List<RoleFolder>();
    foreach (var folder in root.GetProperty("folders").EnumerateArray())
    {
        result.Add(new RoleFolder(
            folder.GetProperty("path").GetString() ?? "",
            folder.GetProperty("relativePath").GetString() ?? "",
            folder.GetProperty("folderName").GetString() ?? "",
            folder.GetProperty("descendantFileCount").GetInt32(),
            folder.GetProperty("roles").EnumerateArray().Select(item => item.GetString() ?? "").Where(item => item.Length > 0).ToArray(),
            folder.GetProperty("primaryRole").GetString() ?? "Unknown",
            folder.GetProperty("reason").GetString() ?? ""));
    }
    return result;
}

AuditFileRecord ToAuditFileRecord(RoleFile file)
{
    return new AuditFileRecord(
        file.Path,
        file.RelativePath,
        file.Directory,
        file.FileName,
        string.IsNullOrWhiteSpace(file.Extension) ? "(none)" : file.Extension,
        file.SizeBytes,
        file.Goal01Classifications,
        file.Roles,
        file.PrimaryRole,
        file.Reason,
        "");
}

AuditFolderRecord ToAuditFolderRecord(RoleFolder folder)
{
    return new AuditFolderRecord(
        folder.Path,
        folder.RelativePath,
        folder.FolderName,
        folder.DescendantFileCount,
        folder.Roles,
        folder.PrimaryRole,
        folder.Reason);
}

AuditFinding Finding(string id, string status, int count, string description) => new(id, status, count, description);

string ScriptableAuditReason(RoleFile file)
{
    if (file.Roles.Contains("ExternalPackage", StringComparer.Ordinal)) return "External package ScriptableObject candidate.";
    if (file.Roles.Contains("VisualOnly", StringComparer.Ordinal)) return "Visual or add-on/UI ScriptableObject candidate.";
    if (file.RelativePath.StartsWith("Assets/Resources/", StringComparison.Ordinal)) return "Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.";
    if (file.RelativePath.StartsWith("Assets/Editor/", StringComparison.Ordinal)) return "Editor tooling ScriptableObject candidate; possible card-data provenance support.";
    return "ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.";
}

string ProjectOwnedAuditReason(RoleFile file)
{
    if (sourceRootSidecars.Contains(file.RelativePath, StringComparer.Ordinal)) return "Expected source root .meta sidecar boundary candidate.";
    if (file.RelativePath.StartsWith("Assets/Editor/", StringComparison.Ordinal)) return "Project-owned editor tooling outside SourceOfTruth roots.";
    if (file.RelativePath.StartsWith("Assets/Prefab/", StringComparison.Ordinal)) return "Project prefab outside battle/card-data SourceOfTruth roots.";
    if (file.RelativePath.StartsWith("Assets/Resources/", StringComparison.Ordinal)) return "Project resource outside battle/card-data SourceOfTruth roots.";
    return "Project-owned Unity asset outside GOAL 02 SourceOfTruth roots.";
}

SortedDictionary<string, int> CountBy<T>(IEnumerable<T> source, Func<T, string> keySelector)
{
    var raw = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var item in source)
    {
        var key = keySelector(item);
        if (string.IsNullOrWhiteSpace(key)) key = "(empty)";
        raw[key] = raw.TryGetValue(key, out var count) ? count + 1 : 1;
    }

    return SortCounts(raw);
}

SortedDictionary<string, int> CountMany<T>(IEnumerable<T> source, Func<T, IEnumerable<string>> valuesSelector)
{
    var raw = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    foreach (var item in source)
    {
        var values = valuesSelector(item).Where(value => !string.IsNullOrWhiteSpace(value)).ToList();
        if (values.Count == 0) values.Add("(empty)");
        foreach (var value in values)
        {
            raw[value] = raw.TryGetValue(value, out var count) ? count + 1 : 1;
        }
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

string TopDirectory(AuditFileRecord record)
{
    var parts = record.RelativePath.Split('/');
    return parts.Length >= 2 ? $"{parts[0]}/{parts[1]}" : record.RelativePath;
}

string ParentDirectory(AuditFileRecord record)
{
    var index = record.RelativePath.LastIndexOf('/');
    return index >= 0 ? record.RelativePath[..index] : "(root)";
}

string BuildSummaryMarkdown(SourceOfTruthAuditSummaryDocument summary)
{
    return string.Join(Environment.NewLine, new[]
    {
        "# AS-IS SourceOfTruth Audit Summary",
        "",
        "> GOAL 03 SourceOfTruth 분류 검증과 누락 가능성 감사 요약이다. 이 문서는 role 승격, headless 필요 여부 판정, 구현 신뢰 감사, 코드 구현을 수행하지 않는다.",
        "",
        "## 기준선",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        $"- GOAL 03 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        $"- 감사 범위: {summary.AuditPolicy.EvidenceScope}",
        "",
        "## 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "SourceOfTruth 파일 수", summary.Totals.SourceOfTruthFileCount.ToString() },
            new[] { "SourceOfTruth 폴더 수", summary.Totals.SourceOfTruthFolderCount.ToString() },
            new[] { "SourceOfTruth C# 파일 수", summary.Totals.SourceCSharpFileCount.ToString() },
            new[] { "SourceOfTruth CardDataCandidate 수", summary.Totals.SourceCardDataCandidateCount.ToString() },
            new[] { "SourceOfTruth ScriptableObjectCandidate 수", summary.Totals.SourceScriptableObjectCandidateCount.ToString() },
            new[] { "expected source scope 중 SourceOfTruth 미부여", summary.Totals.ExpectedSourceScopeNotSourceCount.ToString() },
            new[] { "SourceOfTruth 파일 expected root 밖", summary.Totals.FalsePositiveSourceFileCount.ToString() },
            new[] { "CardDataCandidate outside SourceOfTruth", summary.Totals.CardDataOutsideSourceCount.ToString() },
            new[] { "ScriptableObjectCandidate outside SourceOfTruth", summary.Totals.ScriptableObjectOutsideSourceCount.ToString() },
            new[] { "Project-owned Assets outside SourceOfTruth", summary.Totals.ProjectOwnedAssetsOutsideSourceCount.ToString() },
            new[] { "Assets/Editor C# 후보", summary.Totals.EditorToolCodeOutsideSourceCount.ToString() },
            new[] { "SourceOfTruth meta coverage issue", (summary.Totals.NonMetaSourceFilesMissingMetaCount + summary.Totals.OrphanSourceMetaFileCount + summary.Totals.SourceFoldersMissingMetaCount).ToString() }
        }),
        "",
        "## 감사 finding",
        "",
        MarkdownTable(new[] { "Finding", "상태", "수", "설명" }, summary.AuditFindings.Select(item => new[] { item.Id, item.Status, item.Count.ToString(), item.Description })),
        "",
        "## 다음 GOAL 06 추천 입력",
        "",
        string.Join(Environment.NewLine, summary.NextGoalRecommendations.Select(item => "- " + item)),
        ""
    });
}

string BuildDetailMarkdown(SourceOfTruthAuditSummaryDocument summary, SourceOfTruthAuditDocument detail)
{
    return string.Join(Environment.NewLine, new[]
    {
        "# GOAL 03 AS-IS SourceOfTruth Audit",
        "",
        "> GOAL 01/02 산출물을 기준으로 SourceOfTruth 분류의 일관성과 누락 가능성을 감사한 기준선이다. 후보 목록은 확정 판정이 아니라 후속 분석 입력이다.",
        "",
        "## 입력 기준",
        "",
        $"- AS-IS root 경로: `{summary.AsisRoot}`",
        "- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`",
        "- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`",
        $"- GOAL 03 생성 시각 UTC: `{summary.GeneratedAtUtc}`",
        $"- expected SourceOfTruth roots: {string.Join(", ", summary.AuditPolicy.ExpectedSourceRoots.Select(root => $"`{root}`"))}",
        $"- source root sidecar 후보: {string.Join(", ", summary.AuditPolicy.ExpectedSourceRootSidecars.Select(root => $"`{root}`"))}",
        "- 본문/함수/call graph 분석: `false`",
        "- headless 필요 여부 판정: `false`",
        "- SourceOfTruth role 승격/수정: `false`",
        "",
        "## 전체 요약",
        "",
        MarkdownTable(new[] { "항목", "값" }, new[]
        {
            new[] { "전체 파일 수", summary.Totals.FileCount.ToString() },
            new[] { "전체 폴더 수(root 포함)", summary.Totals.FolderCountIncludingRoot.ToString() },
            new[] { "SourceOfTruth 파일 수", summary.Totals.SourceOfTruthFileCount.ToString() },
            new[] { "SourceOfTruth 폴더 수", summary.Totals.SourceOfTruthFolderCount.ToString() },
            new[] { "SourceOfTruth C# 파일 수", summary.Totals.SourceCSharpFileCount.ToString() },
            new[] { "SourceOfTruth CardDataCandidate 수", summary.Totals.SourceCardDataCandidateCount.ToString() },
            new[] { "SourceOfTruth ScriptableObjectCandidate 수", summary.Totals.SourceScriptableObjectCandidateCount.ToString() }
        }),
        "",
        "## SourceOfTruth root별 파일 수",
        "",
        MarkdownObjectTable("Root", "파일 수", summary.SourceFilesByRoot),
        "",
        "## SourceOfTruth 확장자별 파일 수",
        "",
        MarkdownObjectTable("확장자", "파일 수", summary.SourceFileExtensionCounts),
        "",
        "## 감사 finding",
        "",
        MarkdownTable(new[] { "Finding", "상태", "수", "설명" }, summary.AuditFindings.Select(item => new[] { item.Id, item.Status, item.Count.ToString(), item.Description })),
        "",
        "## Candidate directory counts",
        "",
        "### ScriptableObjectCandidate outside SourceOfTruth",
        "",
        MarkdownObjectTable("Directory", "파일 수", summary.CandidateDirectoryCounts.ScriptableObjectOutsideSource),
        "",
        "### Project-owned Assets outside SourceOfTruth",
        "",
        MarkdownObjectTable("Directory", "파일 수", summary.CandidateDirectoryCounts.ProjectOwnedAssetsOutsideSource),
        "",
        "### Assets/Editor C# 후보",
        "",
        MarkdownObjectTable("Directory", "파일 수", summary.CandidateDirectoryCounts.EditorToolCodeOutsideSource),
        "",
        "## 중요 후보 샘플",
        "",
        "### Expected source scope 중 SourceOfTruth 미부여",
        "",
        FileList(detail.Candidates.ExpectedSourceScopeNotSource, 50),
        "",
        "### Assets/Editor C# 후보",
        "",
        FileList(detail.Candidates.EditorToolCandidates, 100),
        "",
        "### ScriptableObjectCandidate outside SourceOfTruth 샘플",
        "",
        FileList(detail.Candidates.ScriptableOutsideSource, 100),
        "",
        "## GOAL 06 추천 범위",
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

string FileList(IReadOnlyList<AuditFileRecord> records, int limit)
{
    if (records.Count == 0) return "- 없음";
    var lines = records
        .Take(limit)
        .Select(item => $"- `{item.RelativePath}` ({item.PrimaryRole}; {(string.IsNullOrWhiteSpace(item.AuditReason) ? item.Reason : item.AuditReason)})")
        .ToList();
    if (records.Count > limit)
    {
        lines.Add($"- ... {records.Count - limit}개 추가 항목은 JSON 산출물 참조");
    }
    return string.Join(Environment.NewLine, lines);
}

void WriteUtf8(string filePath, string content)
{
    File.WriteAllText(filePath, content, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
}

public sealed record RoleFile(string Path, string RelativePath, string Directory, string FileName, string Extension, long SizeBytes, string[] Goal01Classifications, string[] Roles, string PrimaryRole, string Reason);
public sealed record RoleFolder(string Path, string RelativePath, string FolderName, int DescendantFileCount, string[] Roles, string PrimaryRole, string Reason);
public sealed record AuditFileRecord(string Path, string RelativePath, string Directory, string FileName, string Extension, long SizeBytes, string[] Goal01Classifications, string[] Roles, string PrimaryRole, string Reason, string AuditReason);
public sealed record AuditFolderRecord(string Path, string RelativePath, string FolderName, int DescendantFileCount, string[] Roles, string PrimaryRole, string Reason);
public sealed record AuditFinding(string Id, string Status, int Count, string Description);
public sealed record AuditPolicy(string EvidenceScope, string[] ExpectedSourceRoots, string[] ExpectedSourceRootSidecars, string CandidateStatusMeaning);
public sealed record AuditTotals(int FileCount, int FolderCountIncludingRoot, int SourceOfTruthFileCount, int SourceOfTruthFolderCount, int SourceCSharpFileCount, int SourceCardDataCandidateCount, int SourceScriptableObjectCandidateCount, int ExpectedSourceScopeNotSourceCount, int FalsePositiveSourceFileCount, int CardDataOutsideSourceCount, int ScriptableObjectOutsideSourceCount, int ProjectOwnedAssetsOutsideSourceCount, int EditorToolCodeOutsideSourceCount, int VisualCodeOutsideSourceCount, int ExternalCodeOutsideSourceCount, int NonMetaSourceFilesMissingMetaCount, int OrphanSourceMetaFileCount, int SourceFoldersMissingMetaCount);
public sealed record CandidateDirectoryCounts(IReadOnlyDictionary<string, int> ExpectedSourceScopeNotSource, IReadOnlyDictionary<string, int> ScriptableObjectOutsideSource, IReadOnlyDictionary<string, int> ProjectOwnedAssetsOutsideSource, IReadOnlyDictionary<string, int> EditorToolCodeOutsideSource, IReadOnlyDictionary<string, int> VisualCodeOutsideSource, IReadOnlyDictionary<string, int> ExternalCodeOutsideSource);
public sealed record SourceOfTruthAuditSummaryDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string InputInventoryPath, string InputRolePath, string InputInventoryGeneratedAtUtc, string InputRoleGeneratedAtUtc, string AsisRoot, AuditPolicy AuditPolicy, AuditTotals Totals, IReadOnlyDictionary<string, int> SourceFilesByRoot, IReadOnlyDictionary<string, int> SourceFoldersByRoot, IReadOnlyDictionary<string, int> SourceFileExtensionCounts, IReadOnlyDictionary<string, int> SourceFileClassificationCounts, CandidateDirectoryCounts CandidateDirectoryCounts, IReadOnlyList<AuditFinding> AuditFindings, IReadOnlyList<string> NextGoalRecommendations);
public sealed record AuditCandidates(IReadOnlyList<AuditFileRecord> ExpectedSourceScopeNotSource, IReadOnlyList<AuditFileRecord> FalsePositiveSourceFiles, IReadOnlyList<AuditFileRecord> CardDataOutsideSource, IReadOnlyList<AuditFileRecord> ScriptableOutsideSource, IReadOnlyList<AuditFileRecord> ProjectOwnedOutsideSource, IReadOnlyList<AuditFileRecord> EditorToolCandidates, IReadOnlyList<AuditFileRecord> VisualCodeOutsideSource, IReadOnlyList<AuditFileRecord> ExternalCodeOutsideSource, IReadOnlyList<AuditFileRecord> NonMetaSourceFilesMissingMeta, IReadOnlyList<AuditFileRecord> OrphanSourceMetaFiles, IReadOnlyList<AuditFolderRecord> SourceFoldersMissingMeta);
public sealed record AuditSamples(IReadOnlyList<AuditFileRecord> SourceCSharpFiles, IReadOnlyList<AuditFileRecord> SourceCardDataFiles, IReadOnlyList<AuditFileRecord> SourceScriptableFiles);
public sealed record SourceOfTruthAuditDocument(string SchemaVersion, string GeneratedAtUtc, string Goal, string AsisRoot, string InputInventoryPath, string InputRolePath, SourceOfTruthAuditSummaryDocument Summary, AuditCandidates Candidates, AuditSamples Samples);
