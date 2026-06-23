using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var workspace = Directory.GetCurrentDirectory();
var generatedDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");
var docsDir = Path.Combine(workspace, "docs", "as-is-restart");

var rolePath = Path.Combine(generatedDir, "asis-role-reclassification.json");
var fileTypeSummaryPath = Path.Combine(generatedDir, "asis-file-type-summary.json");
var sourceAuditSummaryPath = Path.Combine(generatedDir, "asis-source-of-truth-audit-summary.json");
var csharpFileIndexPath = Path.Combine(generatedDir, "asis-csharp-file-index.json");
var csharpCallGraphPath = Path.Combine(generatedDir, "asis-csharp-call-graph.json");
var csharpCallGraphSummaryPath = Path.Combine(generatedDir, "asis-csharp-call-graph-summary.json");
var cardDataStructurePath = Path.Combine(generatedDir, "asis-card-data-structure.json");
var cardIndexPath = Path.Combine(generatedDir, "asis-cardbaseentity-card-index.json");

var detailJsonPath = Path.Combine(generatedDir, "asis-headless-requirement-matrix.json");
var summaryJsonPath = Path.Combine(generatedDir, "asis-headless-requirement-summary.json");
var detailMarkdownPath = Path.Combine(docsDir, "GOAL_07_ASIS_HEADLESS_REQUIREMENT_MATRIX.md");
var summaryMarkdownPath = Path.Combine(docsDir, "asis-headless-requirement-matrix-summary.md");

using var roleJson = JsonDocument.Parse(File.ReadAllBytes(rolePath));
using var fileTypeSummaryJson = JsonDocument.Parse(File.ReadAllBytes(fileTypeSummaryPath));
using var sourceAuditSummaryJson = JsonDocument.Parse(File.ReadAllBytes(sourceAuditSummaryPath));
using var csharpFileIndexJson = JsonDocument.Parse(File.ReadAllBytes(csharpFileIndexPath));
using var csharpCallGraphJson = JsonDocument.Parse(File.ReadAllBytes(csharpCallGraphPath));
using var csharpCallGraphSummaryJson = JsonDocument.Parse(File.ReadAllBytes(csharpCallGraphSummaryPath));
using var cardDataStructureJson = JsonDocument.Parse(File.ReadAllBytes(cardDataStructurePath));
using var cardIndexJson = JsonDocument.Parse(File.ReadAllBytes(cardIndexPath));

var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");
var asisRoot = roleJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var files = roleJson.RootElement.GetProperty("files").EnumerateArray().Select(LoadRoleFile).ToList();
var folders = roleJson.RootElement.GetProperty("folders").EnumerateArray().Select(LoadRoleFolder).ToList();
var csharpFiles = csharpFileIndexJson.RootElement.GetProperty("files").EnumerateArray().Select(LoadCSharpFile).ToDictionary(file => file.RelativePath, StringComparer.OrdinalIgnoreCase);
var callFiles = csharpCallGraphJson.RootElement.GetProperty("files").EnumerateArray().Select(LoadCallFile).ToDictionary(file => file.RelativePath, StringComparer.OrdinalIgnoreCase);
var cards = cardIndexJson.RootElement.GetProperty("cards").EnumerateArray().Select(LoadCardRecord).ToDictionary(card => card.RelativePath, StringComparer.OrdinalIgnoreCase);

var fileMatrix = files
    .OrderBy(file => file.RelativePath, StringComparer.OrdinalIgnoreCase)
    .Select(file =>
    {
        csharpFiles.TryGetValue(file.RelativePath, out var csharp);
        callFiles.TryGetValue(file.RelativePath, out var call);
        cards.TryGetValue(file.RelativePath, out var card);
        return AssessFile(file, csharp, call, card);
    })
    .ToList();

var fileMatrixByPath = fileMatrix.ToDictionary(file => file.RelativePath, StringComparer.OrdinalIgnoreCase);
var folderMatrix = BuildFolderMatrix(folders, fileMatrixByPath);
var sourceRequired = fileMatrix.Where(file => file.NeedLevel == "Required").ToList();
var reviewFiles = fileMatrix.Where(file => file.NeedLevel == "CandidateReview").ToList();
var unknownFiles = fileMatrix.Where(file => file.NeedLevel == "Unknown").ToList();
var excludedFiles = fileMatrix.Where(file => file.NeedLevel == "Excluded").ToList();
var referenceFiles = fileMatrix.Where(file => file.NeedLevel == "Reference").ToList();

var cardDataSummary = cardDataStructureJson.RootElement.GetProperty("summary");
var sourceAuditTotals = sourceAuditSummaryJson.RootElement.GetProperty("totals");
var csharpCallTotals = csharpCallGraphSummaryJson.RootElement.GetProperty("totals");

var summary = new HeadlessRequirementSummaryDocument(
    "dcgo.as-is-headless-requirement-summary.v1",
    generatedAtUtc,
    "GOAL 07 AS-IS Headless Requirement Matrix",
    asisRoot,
    new InputArtifacts(
        rolePath,
        roleJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        fileTypeSummaryPath,
        fileTypeSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        sourceAuditSummaryPath,
        sourceAuditSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        csharpFileIndexPath,
        csharpFileIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        csharpCallGraphPath,
        csharpCallGraphJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        csharpCallGraphSummaryPath,
        csharpCallGraphSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        cardDataStructurePath,
        cardDataStructureJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        cardIndexPath,
        cardIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? ""),
    new RequirementPolicy(
        "GOAL 01-06 generated evidence only. This matrix does not modify source, does not trust existing headless implementation, and does not execute card-porting.",
        new[]
        {
            new RequirementPolicyRule("Required", "headless 엔진 기준선에 직접 필요한 원본 로직 또는 카드 데이터"),
            new RequirementPolicyRule("Reference", "headless 런타임 직접 구현 대상은 아니지만 원본 재현, GUID, Unity 프로젝트 맥락, 의존성 확인에 필요한 참조"),
            new RequirementPolicyRule("CandidateReview", "후속 Goal에서 headless 필요 여부를 추가 검증해야 하는 후보"),
            new RequirementPolicyRule("Excluded", "headless battle engine 기준선에는 기본적으로 제외하는 생성물, 빌드 산출물, 시각/사운드 전용 파일"),
            new RequirementPolicyRule("Unknown", "현 단계 증거만으로 필요 여부를 판단하지 않는 항목")
        },
        new[]
        {
            "SourceOfTruth C# runtime and CardEffect source files are Required evidence, but individual CardEffect body implementation remains prohibited by Foundation Gate.",
            "CardBaseEntity .asset files are Required data extraction sources; their .meta sidecars are Reference provenance.",
            "VisualOnly and SoundOnly assets are Excluded from headless runtime unless a later UnityAdapter or data-provenance goal explicitly promotes them.",
            "GeneratedCache, BuildArtifact, and RuntimeOutput are Excluded because they are reproducible or execution outputs.",
            "ExternalPackage files are Reference only; they are not copied into the headless engine without a later dependency decision."
        }),
    new HeadlessRequirementTotals(
        files.Count,
        folders.Count,
        sourceRequired.Count,
        referenceFiles.Count,
        reviewFiles.Count,
        excludedFiles.Count,
        unknownFiles.Count,
        fileMatrix.Count(file => file.IsSourceOfTruth),
        fileMatrix.Count(file => file.IsCSharp),
        fileMatrix.Count(file => file.CardData is not null),
        folderMatrix.Count(folder => folder.NeedLevel == "Required"),
        folderMatrix.Count(folder => folder.NeedLevel == "Reference"),
        folderMatrix.Count(folder => folder.NeedLevel == "CandidateReview"),
        folderMatrix.Count(folder => folder.NeedLevel == "Excluded"),
        folderMatrix.Count(folder => folder.NeedLevel == "Unknown")),
    CountBy(fileMatrix, file => file.NeedLevel),
    CountBy(fileMatrix, file => file.HeadlessCategory),
    CountBy(fileMatrix, file => file.PrimaryRole),
    CountBy(fileMatrix, file => file.SourceScope),
    CountBy(fileMatrix, file => file.TopLevelFolder),
    CountBy(folderMatrix, folder => folder.NeedLevel),
    CountBy(folderMatrix, folder => folder.HeadlessCategory),
    BuildCSharpMatrix(fileMatrix),
    new CardDataRequirementMatrix(
        GetInt(cardDataSummary.GetProperty("totals"), "cardAssetCount"),
        GetInt(cardDataSummary.GetProperty("totals"), "loaderAssetCount"),
        GetInt(cardDataSummary.GetProperty("totals"), "missingEffectSourceCandidateCount"),
        GetInt(cardDataSummary.GetProperty("totals"), "emptyEffectClassNameCount"),
        GetInt(cardDataSummary.GetProperty("totals"), "noEffectMarkerCount"),
        ToSortedDictionary(cardDataSummary.GetProperty("cardKindFolderCounts")),
        ToSortedDictionary(cardDataSummary.GetProperty("effectClassStatusCounts"))),
    new SourceAuditRequirementMatrix(
        GetInt(sourceAuditTotals, "sourceOfTruthFileCount"),
        GetInt(sourceAuditTotals, "sourceCSharpFileCount"),
        GetInt(sourceAuditTotals, "sourceCardDataCandidateCount"),
        GetInt(sourceAuditTotals, "expectedSourceScopeNotSourceCount"),
        GetInt(sourceAuditTotals, "falsePositiveSourceFileCount"),
        GetInt(sourceAuditTotals, "scriptableObjectOutsideSourceCount"),
        GetInt(sourceAuditTotals, "projectOwnedAssetsOutsideSourceCount"),
        GetInt(sourceAuditTotals, "editorToolCodeOutsideSourceCount")),
    new CallGraphRequirementMatrix(
        GetInt(csharpCallTotals, "callEdgeCount"),
        GetInt(csharpCallTotals, "sourceOfTruthCallerEdgeCount"),
        GetInt(csharpCallTotals, "unityTaggedCount"),
        GetInt(csharpCallTotals, "gManagerTaggedCount"),
        GetInt(csharpCallTotals, "photonTaggedCount"),
        GetInt(csharpCallTotals, "uiTaggedCount"),
        GetInt(csharpCallTotals, "coroutineTaggedCount"),
        ToSortedDictionary(csharpCallGraphSummaryJson.RootElement.GetProperty("crossBoundaryCounts")),
        ToSortedDictionary(csharpCallGraphSummaryJson.RootElement.GetProperty("tagCounts"))),
    SelectSamples(sourceRequired, 200),
    SelectSamples(reviewFiles, 300),
    SelectSamples(unknownFiles, 500),
    SelectSamples(excludedFiles.Where(file => file.HeadlessCategory is "VisualOnlyExcluded" or "SoundOnlyExcluded"), 200),
    SelectLargest(sourceRequired, 100),
    new[]
    {
        "GOAL 08에서는 Required/CandidateReview 범위를 기존 src headless 구현과 비교하되, 기존 구현 결과를 신뢰하지 않고 SourceOfTruth mapping evidence로 재검증한다.",
        "Assets/Scripts/Script runtime 공통 로직, Assets/Scripts/CardEffect source, Assets/CardBaseEntity 카드 데이터의 대응 여부를 별도 축으로 나눈다.",
        "GOAL 05 call graph의 Unity/GManager/Photon/UI/Coroutine tag는 구현 우선순위가 아니라 의존성 제거 및 trust audit 리스크 지표로 사용한다.",
        "GOAL 06의 missing effect source candidate 39건과 duplicate CardID group은 구현 전에 데이터/variant 정책 후보로 감사한다.",
        "VisualOnly/SoundOnly/GeneratedCache/BuildArtifact는 GOAL 08 headless 구현 감사 기본 범위에서 제외하되, UnityAdapter 목표에서 다시 열 수 있다."
    });

var detail = new HeadlessRequirementDetailDocument(
    "dcgo.as-is-headless-requirement-matrix.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    summary,
    fileMatrix,
    folderMatrix);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

WriteUtf8(summaryJsonPath, JsonSerializer.Serialize(summary, jsonOptions) + Environment.NewLine);
WriteUtf8(detailJsonPath, JsonSerializer.Serialize(detail, jsonOptions) + Environment.NewLine);
WriteUtf8(summaryMarkdownPath, BuildSummaryMarkdown(summary));
WriteUtf8(detailMarkdownPath, BuildDetailMarkdown(summary));

Console.WriteLine(JsonSerializer.Serialize(new
{
    asisRoot,
    fileCount = summary.Totals.FileCount,
    folderCount = summary.Totals.FolderCount,
    requiredFileCount = summary.Totals.RequiredFileCount,
    referenceFileCount = summary.Totals.ReferenceFileCount,
    candidateReviewFileCount = summary.Totals.CandidateReviewFileCount,
    excludedFileCount = summary.Totals.ExcludedFileCount,
    unknownFileCount = summary.Totals.UnknownFileCount,
    requiredFolderCount = summary.Totals.RequiredFolderCount,
    outputFiles = new[] { detailMarkdownPath, summaryMarkdownPath, detailJsonPath, summaryJsonPath }
}, jsonOptions));

MatrixFileRecord AssessFile(RoleFile file, CSharpFileInfo? csharp, CallFileInfo? call, CardRecord? card)
{
    var evidence = new List<string>();
    var needLevel = "Unknown";
    var category = "UnknownReview";
    var use = "UnknownReview";
    var rationale = "현 단계 증거만으로 headless 필요 여부를 판단하지 않음.";

    var isSource = HasRole(file, "SourceOfTruth");
    var isCSharp = file.Extension.Equals(".cs", StringComparison.OrdinalIgnoreCase);
    var sourceScope = SourceScope(file.RelativePath);
    if (isSource)
    {
        evidence.Add("GOAL02 SourceOfTruth");
    }
    if (file.Goal01Classifications.Length > 0)
    {
        evidence.Add("GOAL01 " + string.Join(",", file.Goal01Classifications));
    }

    if (isSource && isCSharp && InPath(file.RelativePath, "Assets/Scripts/Script"))
    {
        needLevel = "Required";
        category = "RuntimeLogicRequired";
        use = "BattleRuntimeLogic";
        rationale = "원본 battle/runtime 공통 C# 코드로 headless 엔진 동작 기준선에 직접 필요.";
    }
    else if (isSource && isCSharp && InPath(file.RelativePath, "Assets/Scripts/CardEffect"))
    {
        needLevel = "Required";
        category = "CardEffectLogicRequired";
        use = "CardEffectSourceEvidence";
        rationale = "원본 카드 효과 C# source. 구현은 금지되어 있지만 parity/source evidence로 필요.";
    }
    else if (isSource && isCSharp)
    {
        needLevel = "CandidateReview";
        category = "SourceCodeReviewCandidate";
        use = "SourceCodeBoundaryReview";
        rationale = "SourceOfTruth C#이나 runtime/CardEffect 핵심 하위 경로가 아니므로 후속 감사에서 필요 범위를 확인.";
    }
    else if (isSource && HasClassification(file, "CardDataCandidate") && file.Extension.Equals(".asset", StringComparison.OrdinalIgnoreCase))
    {
        needLevel = "Required";
        category = "CardDataRequired";
        use = "CardDefinitionData";
        rationale = "CardBaseEntity 카드 정의 ScriptableObject asset으로 headless 카드 데이터 추출에 직접 필요.";
    }
    else if (isSource && file.Extension.Equals(".meta", StringComparison.OrdinalIgnoreCase))
    {
        needLevel = "Reference";
        category = "SourceMetaReference";
        use = "UnityGuidAndAssetProvenance";
        rationale = "SourceOfTruth의 Unity .meta sidecar. runtime 로직은 아니지만 GUID/provenance 확인 참조.";
    }
    else if (isSource)
    {
        needLevel = "CandidateReview";
        category = "SourceSerializedReviewCandidate";
        use = "SourceBoundaryReview";
        rationale = "SourceOfTruth 범위 안의 비 C#·비 카드 asset. 후속 Goal에서 headless 필요 여부 확인.";
    }
    else if (file.RelativePath.StartsWith("Assets/Editor/", StringComparison.Ordinal) && isCSharp)
    {
        needLevel = "CandidateReview";
        category = "DataProvenanceToolCandidate";
        use = "CardDataProvenanceReview";
        rationale = "Assets/Editor 도구 C#은 battle runtime은 아니지만 카드 데이터 import/fixup provenance 후보.";
    }
    else if (HasRole(file, "GeneratedCache") || HasRole(file, "BuildArtifact") || HasRole(file, "RuntimeOutput"))
    {
        needLevel = "Excluded";
        category = "GeneratedOrBuildExcluded";
        use = "RegenerableOrRuntimeOutput";
        rationale = "Unity cache/build/runtime output 성격이므로 headless 기준선 산출물에서 제외.";
    }
    else if (HasRole(file, "SoundOnly"))
    {
        needLevel = "Excluded";
        category = "SoundOnlyExcluded";
        use = "AudioPresentationOnly";
        rationale = "사운드 전용 asset은 headless battle engine 직접 구현 범위에서 제외.";
    }
    else if (HasRole(file, "VisualOnly"))
    {
        needLevel = "Excluded";
        category = "VisualOnlyExcluded";
        use = "VisualPresentationOnly";
        rationale = "시각 표현 전용 asset/code는 headless battle engine 직접 구현 범위에서 제외.";
    }
    else if (file.RelativePath.StartsWith("ProjectSettings/", StringComparison.Ordinal) ||
             file.RelativePath.StartsWith("Packages/", StringComparison.Ordinal) ||
             file.FileName.Equals("manifest.json", StringComparison.OrdinalIgnoreCase) ||
             file.FileName.Equals("packages-lock.json", StringComparison.OrdinalIgnoreCase))
    {
        needLevel = "Reference";
        category = "UnityProjectConfigReference";
        use = "UnityProjectReproductionContext";
        rationale = "Unity 프로젝트 재현과 의존성 맥락 참조용이며 headless runtime 직접 구현 대상은 아님.";
    }
    else if (HasRole(file, "ExternalPackage"))
    {
        needLevel = "Reference";
        category = "ExternalDependencyReference";
        use = "ExternalDependencyContext";
        rationale = "외부 패키지/플러그인 의존성 맥락 참조용. 직접 이식 대상은 아님.";
    }
    else if (HasClassification(file, "ScriptableObjectCandidate") || HasClassification(file, "UnitySerializedAsset") || HasClassification(file, "Prefab") || HasClassification(file, "Scene"))
    {
        needLevel = "CandidateReview";
        category = "SerializedAssetReviewCandidate";
        use = "SerializedDataBoundaryReview";
        rationale = "Unity serialized asset이지만 CardBaseEntity SourceOfTruth가 아니므로 후속 필요 여부 검토 후보.";
    }
    else if (HasRole(file, "UnityProjectSource") || file.PrimaryRole == "UnityProjectSource")
    {
        needLevel = "Reference";
        category = "UnityProjectSourceReference";
        use = "ProjectContextReference";
        rationale = "프로젝트 소유 파일이나 headless runtime 직접 구현 대상이라는 증거는 없음. 원본 맥락 참조로 유지.";
    }
    else if (HasRole(file, "Unknown") || file.PrimaryRole == "Unknown" || HasClassification(file, "Unknown"))
    {
        needLevel = "Unknown";
        category = "UnknownReview";
        use = "UnknownReview";
        rationale = "GOAL 01/02 기준으로 명확히 분류되지 않아 후속 검토 필요.";
    }
    else
    {
        needLevel = "Excluded";
        category = "NotHeadlessRequired";
        use = "NoHeadlessUseIdentified";
        rationale = "현 단계에서 headless 기준선에 필요한 증거가 없음.";
    }

    if (csharp is not null)
    {
        evidence.Add($"GOAL04 CSharp parse={csharp.ParseStatus}");
    }
    if (call is not null)
    {
        evidence.Add($"GOAL05 fileEdgeCount={call.EdgeCount}");
    }
    if (card is not null)
    {
        evidence.Add($"GOAL06 cardId={card.CardId}");
    }

    return new MatrixFileRecord(
        file.RelativePath,
        file.Directory,
        file.FileName,
        file.Extension,
        file.SizeBytes,
        file.TopLevelFolder,
        file.Roles,
        file.PrimaryRole,
        file.Goal01Classifications,
        isSource,
        isCSharp,
        sourceScope,
        needLevel,
        category,
        use,
        rationale,
        evidence.ToArray(),
        csharp is null ? null : new MatrixCSharpEvidence(csharp.ParseStatus, csharp.TypeCount, csharp.MethodCount, csharp.PropertyCount, csharp.FieldCount),
        call is null ? null : new MatrixCallEvidence(call.EdgeCount),
        card is null ? null : new MatrixCardEvidence(card.CardId, card.BaseCardId, card.CardKindFolder, card.CardEffectClassName, card.EffectClassStatus, card.CardEffectSourceCandidateCount));
}

List<MatrixFolderRecord> BuildFolderMatrix(List<RoleFolder> roleFolders, Dictionary<string, MatrixFileRecord> matrixFiles)
{
    var aggregate = new Dictionary<string, FolderAccumulator>(StringComparer.OrdinalIgnoreCase);
    foreach (var folder in roleFolders)
    {
        aggregate[folder.RelativePath] = new FolderAccumulator(folder);
    }

    foreach (var file in matrixFiles.Values)
    {
        foreach (var ancestor in Ancestors(file.Directory))
        {
            if (!aggregate.TryGetValue(ancestor, out var acc))
            {
                var name = ancestor == "." ? "(root)" : ancestor.Split('/').Last();
                acc = new FolderAccumulator(new RoleFolder(ancestor, name, TopLevelFolder(ancestor), 0, Array.Empty<string>(), "Unknown", ""));
                aggregate[ancestor] = acc;
            }
            acc.Add(file);
        }
    }

    return aggregate.Values
        .Select(acc => acc.ToRecord())
        .OrderBy(record => record.RelativePath == "." ? "" : record.RelativePath, StringComparer.OrdinalIgnoreCase)
        .ToList();
}

IEnumerable<string> Ancestors(string directory)
{
    yield return ".";
    if (string.IsNullOrWhiteSpace(directory))
    {
        yield break;
    }

    var parts = directory.Split('/', StringSplitOptions.RemoveEmptyEntries);
    var current = "";
    foreach (var part in parts)
    {
        current = string.IsNullOrWhiteSpace(current) ? part : current + "/" + part;
        yield return current;
    }
}

CSharpRequirementMatrix BuildCSharpMatrix(List<MatrixFileRecord> matrix)
{
    var csharp = matrix.Where(file => file.IsCSharp).ToList();
    return new CSharpRequirementMatrix(
        csharp.Count,
        csharp.Count(file => file.IsSourceOfTruth),
        csharp.Count(file => file.NeedLevel == "Required"),
        csharp.Count(file => file.NeedLevel == "CandidateReview"),
        csharp.Count(file => file.NeedLevel == "Excluded"),
        csharp.Sum(file => file.CallGraph?.EdgeCount ?? 0),
        CountBy(csharp, file => file.NeedLevel),
        CountBy(csharp, file => file.HeadlessCategory),
        CountBy(csharp, file => file.SourceScope));
}

RoleFile LoadRoleFile(JsonElement item)
{
    return new RoleFile(
        item.GetProperty("path").GetString() ?? "",
        item.GetProperty("relativePath").GetString() ?? "",
        item.GetProperty("directory").GetString() ?? "",
        item.GetProperty("fileName").GetString() ?? "",
        item.GetProperty("extension").GetString() ?? "",
        item.GetProperty("sizeBytes").GetInt64(),
        GetStringArray(item, "goal01Classifications"),
        GetStringArray(item, "roles"),
        item.GetProperty("primaryRole").GetString() ?? "",
        item.GetProperty("reason").GetString() ?? "",
        item.GetProperty("topLevelFolder").GetString() ?? "");
}

RoleFolder LoadRoleFolder(JsonElement item)
{
    return new RoleFolder(
        item.GetProperty("relativePath").GetString() ?? "",
        item.GetProperty("folderName").GetString() ?? "",
        item.GetProperty("topLevelFolder").GetString() ?? "",
        GetInt(item, "descendantFileCount"),
        GetStringArray(item, "roles"),
        item.GetProperty("primaryRole").GetString() ?? "",
        item.GetProperty("reason").GetString() ?? "");
}

CSharpFileInfo LoadCSharpFile(JsonElement item)
{
    return new CSharpFileInfo(
        item.GetProperty("relativePath").GetString() ?? "",
        item.GetProperty("parseStatus").GetString() ?? "",
        GetInt(item, "typeCount"),
        GetInt(item, "methodCount"),
        GetInt(item, "propertyCount"),
        GetInt(item, "fieldCount"));
}

CallFileInfo LoadCallFile(JsonElement item)
{
    return new CallFileInfo(
        item.GetProperty("relativePath").GetString() ?? "",
        GetInt(item, "edgeCount"));
}

CardRecord LoadCardRecord(JsonElement item)
{
    return new CardRecord(
        item.GetProperty("relativePath").GetString() ?? "",
        item.GetProperty("cardId").GetString() ?? "",
        item.GetProperty("baseCardId").GetString() ?? "",
        item.GetProperty("cardKindFolder").GetString() ?? "",
        item.GetProperty("cardEffectClassName").GetString() ?? "",
        item.GetProperty("effectClassStatus").GetString() ?? "",
        GetInt(item, "cardEffectSourceCandidateCount"));
}

static bool HasRole(RoleFile file, string role) => file.Roles.Contains(role, StringComparer.Ordinal);

static bool HasClassification(RoleFile file, string classification) => file.Goal01Classifications.Contains(classification, StringComparer.Ordinal);

static bool InPath(string relativePath, string root)
{
    return relativePath.Equals(root, StringComparison.Ordinal) || relativePath.StartsWith(root + "/", StringComparison.Ordinal);
}

static string SourceScope(string relativePath)
{
    if (InPath(relativePath, "Assets/Scripts/Script"))
    {
        return "Assets/Scripts/Script";
    }
    if (InPath(relativePath, "Assets/Scripts/CardEffect"))
    {
        return "Assets/Scripts/CardEffect";
    }
    if (InPath(relativePath, "Assets/Scripts"))
    {
        return "Assets/Scripts/(other)";
    }
    if (InPath(relativePath, "Assets/CardBaseEntity"))
    {
        return "Assets/CardBaseEntity";
    }
    if (relativePath.StartsWith("Assets/Editor/", StringComparison.Ordinal))
    {
        return "Assets/Editor";
    }
    return "(outside SourceOfTruth)";
}

static string TopLevelFolder(string relativePath)
{
    if (relativePath == "." || string.IsNullOrWhiteSpace(relativePath))
    {
        return "(root)";
    }
    var slash = relativePath.IndexOf('/');
    return slash < 0 ? relativePath : relativePath[..slash];
}

static string[] GetStringArray(JsonElement item, string propertyName)
{
    if (!item.TryGetProperty(propertyName, out var property) || property.ValueKind != JsonValueKind.Array)
    {
        return Array.Empty<string>();
    }
    return property.EnumerateArray()
        .Select(value => value.GetString() ?? "")
        .Where(value => value.Length > 0)
        .ToArray();
}

static int GetInt(JsonElement item, string propertyName)
{
    if (!item.TryGetProperty(propertyName, out var property))
    {
        return 0;
    }
    return property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out var value) ? value : 0;
}

static SortedDictionary<string, int> CountBy<T>(IEnumerable<T> items, Func<T, string> keySelector)
{
    var result = new SortedDictionary<string, int>(StringComparer.Ordinal);
    foreach (var item in items)
    {
        var key = keySelector(item);
        if (string.IsNullOrWhiteSpace(key))
        {
            key = "(blank)";
        }
        result[key] = result.GetValueOrDefault(key) + 1;
    }
    return result;
}

static SortedDictionary<string, int> ToSortedDictionary(JsonElement item)
{
    var result = new SortedDictionary<string, int>(StringComparer.Ordinal);
    foreach (var property in item.EnumerateObject())
    {
        result[property.Name] = property.Value.ValueKind == JsonValueKind.Number && property.Value.TryGetInt32(out var value) ? value : 0;
    }
    return result;
}

static List<MatrixFileSample> SelectSamples(IEnumerable<MatrixFileRecord> records, int limit)
{
    return records
        .OrderBy(record => record.RelativePath, StringComparer.OrdinalIgnoreCase)
        .Take(limit)
        .Select(record => new MatrixFileSample(record.RelativePath, record.NeedLevel, record.HeadlessCategory, record.PrimaryRole, record.Rationale))
        .ToList();
}

static List<MatrixFileSample> SelectLargest(IEnumerable<MatrixFileRecord> records, int limit)
{
    return records
        .OrderByDescending(record => record.SizeBytes)
        .ThenBy(record => record.RelativePath, StringComparer.OrdinalIgnoreCase)
        .Take(limit)
        .Select(record => new MatrixFileSample(record.RelativePath, record.NeedLevel, record.HeadlessCategory, record.PrimaryRole, $"{record.SizeBytes} bytes"))
        .ToList();
}

static void WriteUtf8(string path, string contents)
{
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    File.WriteAllText(path, contents, new UTF8Encoding(false));
}

static string BuildSummaryMarkdown(HeadlessRequirementSummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# AS-IS Headless Requirement Matrix Summary");
    builder.AppendLine();
    builder.AppendLine($"- 생성 시각(UTC): `{summary.GeneratedAtUtc}`");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- 전체 파일 수: {summary.Totals.FileCount:N0}");
    builder.AppendLine($"- 전체 폴더 수: {summary.Totals.FolderCount:N0}");
    builder.AppendLine($"- Required 파일 수: {summary.Totals.RequiredFileCount:N0}");
    builder.AppendLine($"- Reference 파일 수: {summary.Totals.ReferenceFileCount:N0}");
    builder.AppendLine($"- CandidateReview 파일 수: {summary.Totals.CandidateReviewFileCount:N0}");
    builder.AppendLine($"- Excluded 파일 수: {summary.Totals.ExcludedFileCount:N0}");
    builder.AppendLine($"- Unknown 파일 수: {summary.Totals.UnknownFileCount:N0}");
    builder.AppendLine();
    builder.AppendLine("## NeedLevel별 파일 수");
    AppendTable(builder, new[] { "NeedLevel", "파일 수" }, summary.NeedLevelCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## Headless category별 파일 수");
    AppendTable(builder, new[] { "Category", "파일 수" }, summary.HeadlessCategoryCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## Source scope별 파일 수");
    AppendTable(builder, new[] { "Scope", "파일 수" }, summary.SourceScopeCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## 다음 Goal 추천");
    foreach (var recommendation in summary.NextGoalRecommendations)
    {
        builder.AppendLine($"- {recommendation}");
    }
    return builder.ToString();
}

static string BuildDetailMarkdown(HeadlessRequirementSummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# GOAL 07 AS-IS Headless Requirement Matrix");
    builder.AppendLine();
    builder.AppendLine("이번 문서는 GOAL 01-06 산출물을 기준으로 원본 DCGO 파일과 폴더의 headless battle engine 필요 여부를 분류한 기준선이다.");
    builder.AppendLine("기존 headless 구현은 신뢰하지 않고, 구현/이식 작업도 수행하지 않았다.");
    builder.AppendLine();
    builder.AppendLine("## 입력");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- GOAL 02 role: `{summary.InputArtifacts.RolePath}`");
    builder.AppendLine($"- GOAL 03 SourceOfTruth audit: `{summary.InputArtifacts.SourceAuditSummaryPath}`");
    builder.AppendLine($"- GOAL 04 C# file index: `{summary.InputArtifacts.CSharpFileIndexPath}`");
    builder.AppendLine($"- GOAL 05 call graph summary/detail: `{summary.InputArtifacts.CSharpCallGraphSummaryPath}`, `{summary.InputArtifacts.CSharpCallGraphPath}`");
    builder.AppendLine($"- GOAL 06 CardBaseEntity index: `{summary.InputArtifacts.CardIndexPath}`");
    builder.AppendLine();
    builder.AppendLine("## 분류 정책");
    builder.AppendLine(summary.Policy.EvidenceScope);
    builder.AppendLine();
    AppendTable(builder, new[] { "NeedLevel", "의미" }, summary.Policy.NeedLevelRules.Select(rule => new[] { rule.NeedLevel, rule.Description }));
    builder.AppendLine("## 전체 요약");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "전체 파일", summary.Totals.FileCount.ToString("N0") },
        new[] { "전체 폴더", summary.Totals.FolderCount.ToString("N0") },
        new[] { "Required 파일", summary.Totals.RequiredFileCount.ToString("N0") },
        new[] { "Reference 파일", summary.Totals.ReferenceFileCount.ToString("N0") },
        new[] { "CandidateReview 파일", summary.Totals.CandidateReviewFileCount.ToString("N0") },
        new[] { "Excluded 파일", summary.Totals.ExcludedFileCount.ToString("N0") },
        new[] { "Unknown 파일", summary.Totals.UnknownFileCount.ToString("N0") },
        new[] { "SourceOfTruth 파일", summary.Totals.SourceOfTruthFileCount.ToString("N0") },
        new[] { "C# 파일", summary.Totals.CSharpFileCount.ToString("N0") },
        new[] { "CardData 파일", summary.Totals.CardDataFileCount.ToString("N0") }
    });
    builder.AppendLine("## NeedLevel별 파일 수");
    AppendTable(builder, new[] { "NeedLevel", "파일 수" }, summary.NeedLevelCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## Headless category별 파일 수");
    AppendTable(builder, new[] { "Category", "파일 수" }, summary.HeadlessCategoryCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## C# matrix");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "전체 C# 파일", summary.CSharpMatrix.CSharpFileCount.ToString("N0") },
        new[] { "SourceOfTruth C# 파일", summary.CSharpMatrix.SourceOfTruthCSharpFileCount.ToString("N0") },
        new[] { "Required C# 파일", summary.CSharpMatrix.RequiredCSharpFileCount.ToString("N0") },
        new[] { "CandidateReview C# 파일", summary.CSharpMatrix.CandidateReviewCSharpFileCount.ToString("N0") },
        new[] { "Excluded C# 파일", summary.CSharpMatrix.ExcludedCSharpFileCount.ToString("N0") },
        new[] { "파일별 edgeCount 합계", summary.CSharpMatrix.FileEdgeCountSum.ToString("N0") }
    });
    builder.AppendLine("## CardBaseEntity matrix");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "카드 asset", summary.CardDataMatrix.CardAssetCount.ToString("N0") },
        new[] { "loader asset", summary.CardDataMatrix.LoaderAssetCount.ToString("N0") },
        new[] { "effect source 후보 없음", summary.CardDataMatrix.MissingEffectSourceCandidateCount.ToString("N0") },
        new[] { "empty effect class", summary.CardDataMatrix.EmptyEffectClassNameCount.ToString("N0") },
        new[] { "NoEffect marker", summary.CardDataMatrix.NoEffectMarkerCount.ToString("N0") }
    });
    builder.AppendLine("## SourceOfTruth audit 연결");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "SourceOfTruth 파일", summary.SourceAuditMatrix.SourceOfTruthFileCount.ToString("N0") },
        new[] { "SourceOfTruth C# 파일", summary.SourceAuditMatrix.SourceCSharpFileCount.ToString("N0") },
        new[] { "Source CardDataCandidate", summary.SourceAuditMatrix.SourceCardDataCandidateCount.ToString("N0") },
        new[] { "Expected source scope not source", summary.SourceAuditMatrix.ExpectedSourceScopeNotSourceCount.ToString("N0") },
        new[] { "False positive SourceOfTruth", summary.SourceAuditMatrix.FalsePositiveSourceFileCount.ToString("N0") },
        new[] { "Scriptable outside source", summary.SourceAuditMatrix.ScriptableObjectOutsideSourceCount.ToString("N0") },
        new[] { "Project-owned assets outside source", summary.SourceAuditMatrix.ProjectOwnedAssetsOutsideSourceCount.ToString("N0") },
        new[] { "Editor tool code outside source", summary.SourceAuditMatrix.EditorToolCodeOutsideSourceCount.ToString("N0") }
    });
    builder.AppendLine("## Call graph 리스크 지표");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "전체 call edge", summary.CallGraphMatrix.CallEdgeCount.ToString("N0") },
        new[] { "SourceOfTruth caller edge", summary.CallGraphMatrix.SourceOfTruthCallerEdgeCount.ToString("N0") },
        new[] { "Unity tag", summary.CallGraphMatrix.UnityTaggedCount.ToString("N0") },
        new[] { "GManager tag", summary.CallGraphMatrix.GManagerTaggedCount.ToString("N0") },
        new[] { "Photon tag", summary.CallGraphMatrix.PhotonTaggedCount.ToString("N0") },
        new[] { "UI tag", summary.CallGraphMatrix.UiTaggedCount.ToString("N0") },
        new[] { "Coroutine tag", summary.CallGraphMatrix.CoroutineTaggedCount.ToString("N0") }
    });
    builder.AppendLine("## Unknown 파일 샘플");
    AppendTable(builder, new[] { "경로", "Category", "근거" }, summary.UnknownFileSamples.Take(50).Select(sample => new[] { sample.RelativePath, sample.HeadlessCategory, sample.Note }));
    builder.AppendLine("## CandidateReview 샘플");
    AppendTable(builder, new[] { "경로", "Category", "근거" }, summary.CandidateReviewFileSamples.Take(50).Select(sample => new[] { sample.RelativePath, sample.HeadlessCategory, sample.Note }));
    builder.AppendLine("## Required 대용량 파일 샘플");
    AppendTable(builder, new[] { "경로", "Category", "크기" }, summary.LargestRequiredFileSamples.Take(50).Select(sample => new[] { sample.RelativePath, sample.HeadlessCategory, sample.Note }));
    builder.AppendLine("## 다음 Goal 추천");
    foreach (var recommendation in summary.NextGoalRecommendations)
    {
        builder.AppendLine($"- {recommendation}");
    }
    builder.AppendLine();
    builder.AppendLine("## 금지 사항 준수");
    builder.AppendLine("- `src/` C# 코드 수정 없음.");
    builder.AppendLine("- 원본 `DCGO/Assets` 수정 없음.");
    builder.AppendLine("- CardEffect body 구현 없음.");
    builder.AppendLine("- C0039 이후 card-porting 실행 없음.");
    builder.AppendLine("- commit/push 없음.");
    return builder.ToString();
}

static void AppendTable(StringBuilder builder, string[] headers, IEnumerable<string[]> rows)
{
    builder.AppendLine("| " + string.Join(" | ", headers) + " |");
    builder.AppendLine("| " + string.Join(" | ", headers.Select(_ => "---")) + " |");
    foreach (var row in rows)
    {
        builder.AppendLine("| " + string.Join(" | ", row.Select(EscapeMarkdownCell)) + " |");
    }
    builder.AppendLine();
}

static string EscapeMarkdownCell(string value)
{
    return value.Replace("|", "\\|").Replace("\r", " ").Replace("\n", " ");
}

sealed class FolderAccumulator
{
    private static readonly string[] NeedOrder = { "Required", "CandidateReview", "Reference", "Unknown", "Excluded" };
    private readonly RoleFolder folder;
    private readonly SortedDictionary<string, int> needLevelCounts = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, int> categoryCounts = new(StringComparer.Ordinal);

    public FolderAccumulator(RoleFolder folder)
    {
        this.folder = folder;
    }

    public void Add(MatrixFileRecord file)
    {
        needLevelCounts[file.NeedLevel] = needLevelCounts.GetValueOrDefault(file.NeedLevel) + 1;
        categoryCounts[file.HeadlessCategory] = categoryCounts.GetValueOrDefault(file.HeadlessCategory) + 1;
    }

    public MatrixFolderRecord ToRecord()
    {
        var needLevel = NeedOrder.FirstOrDefault(level => needLevelCounts.GetValueOrDefault(level) > 0) ?? "Unknown";
        var category = categoryCounts
            .OrderByDescending(pair => NeedOrderScore(pair.Key))
            .ThenByDescending(pair => pair.Value)
            .ThenBy(pair => pair.Key, StringComparer.Ordinal)
            .FirstOrDefault().Key ?? "UnknownReview";
        return new MatrixFolderRecord(
            folder.RelativePath,
            folder.FolderName,
            folder.TopLevelFolder,
            folder.DescendantFileCount,
            folder.Roles,
            folder.PrimaryRole,
            needLevel,
            category,
            new SortedDictionary<string, int>(needLevelCounts, StringComparer.Ordinal),
            new SortedDictionary<string, int>(categoryCounts, StringComparer.Ordinal));
    }

    private static int NeedOrderScore(string category)
    {
        if (category.Contains("Required", StringComparison.Ordinal))
        {
            return 5;
        }
        if (category.Contains("Candidate", StringComparison.Ordinal))
        {
            return 4;
        }
        if (category.Contains("Reference", StringComparison.Ordinal))
        {
            return 3;
        }
        if (category.Contains("Unknown", StringComparison.Ordinal))
        {
            return 2;
        }
        return 1;
    }
}

record RoleFile(
    string Path,
    string RelativePath,
    string Directory,
    string FileName,
    string Extension,
    long SizeBytes,
    string[] Goal01Classifications,
    string[] Roles,
    string PrimaryRole,
    string Reason,
    string TopLevelFolder);

record RoleFolder(
    string RelativePath,
    string FolderName,
    string TopLevelFolder,
    int DescendantFileCount,
    string[] Roles,
    string PrimaryRole,
    string Reason);

record CSharpFileInfo(
    string RelativePath,
    string ParseStatus,
    int TypeCount,
    int MethodCount,
    int PropertyCount,
    int FieldCount);

record CallFileInfo(string RelativePath, int EdgeCount);

record CardRecord(
    string RelativePath,
    string CardId,
    string BaseCardId,
    string CardKindFolder,
    string CardEffectClassName,
    string EffectClassStatus,
    int CardEffectSourceCandidateCount);

record MatrixFileRecord(
    string RelativePath,
    string Directory,
    string FileName,
    string Extension,
    long SizeBytes,
    string TopLevelFolder,
    string[] Roles,
    string PrimaryRole,
    string[] Goal01Classifications,
    bool IsSourceOfTruth,
    bool IsCSharp,
    string SourceScope,
    string NeedLevel,
    string HeadlessCategory,
    string HeadlessUse,
    string Rationale,
    string[] Evidence,
    MatrixCSharpEvidence? CSharp,
    MatrixCallEvidence? CallGraph,
    MatrixCardEvidence? CardData);

record MatrixFolderRecord(
    string RelativePath,
    string FolderName,
    string TopLevelFolder,
    int DescendantFileCount,
    string[] Roles,
    string PrimaryRole,
    string NeedLevel,
    string HeadlessCategory,
    SortedDictionary<string, int> NeedLevelCounts,
    SortedDictionary<string, int> HeadlessCategoryCounts);

record MatrixCSharpEvidence(string ParseStatus, int TypeCount, int MethodCount, int PropertyCount, int FieldCount);

record MatrixCallEvidence(int EdgeCount);

record MatrixCardEvidence(string CardId, string BaseCardId, string CardKindFolder, string CardEffectClassName, string EffectClassStatus, int CardEffectSourceCandidateCount);

record InputArtifacts(
    string RolePath,
    string RoleGeneratedAtUtc,
    string FileTypeSummaryPath,
    string FileTypeSummaryGeneratedAtUtc,
    string SourceAuditSummaryPath,
    string SourceAuditSummaryGeneratedAtUtc,
    string CSharpFileIndexPath,
    string CSharpFileIndexGeneratedAtUtc,
    string CSharpCallGraphPath,
    string CSharpCallGraphGeneratedAtUtc,
    string CSharpCallGraphSummaryPath,
    string CSharpCallGraphSummaryGeneratedAtUtc,
    string CardDataStructurePath,
    string CardDataStructureGeneratedAtUtc,
    string CardIndexPath,
    string CardIndexGeneratedAtUtc);

record RequirementPolicy(string EvidenceScope, RequirementPolicyRule[] NeedLevelRules, string[] ClassificationRules);

record RequirementPolicyRule(string NeedLevel, string Description);

record HeadlessRequirementTotals(
    int FileCount,
    int FolderCount,
    int RequiredFileCount,
    int ReferenceFileCount,
    int CandidateReviewFileCount,
    int ExcludedFileCount,
    int UnknownFileCount,
    int SourceOfTruthFileCount,
    int CSharpFileCount,
    int CardDataFileCount,
    int RequiredFolderCount,
    int ReferenceFolderCount,
    int CandidateReviewFolderCount,
    int ExcludedFolderCount,
    int UnknownFolderCount);

record CSharpRequirementMatrix(
    int CSharpFileCount,
    int SourceOfTruthCSharpFileCount,
    int RequiredCSharpFileCount,
    int CandidateReviewCSharpFileCount,
    int ExcludedCSharpFileCount,
    int FileEdgeCountSum,
    SortedDictionary<string, int> NeedLevelCounts,
    SortedDictionary<string, int> HeadlessCategoryCounts,
    SortedDictionary<string, int> SourceScopeCounts);

record CardDataRequirementMatrix(
    int CardAssetCount,
    int LoaderAssetCount,
    int MissingEffectSourceCandidateCount,
    int EmptyEffectClassNameCount,
    int NoEffectMarkerCount,
    SortedDictionary<string, int> CardKindFolderCounts,
    SortedDictionary<string, int> EffectClassStatusCounts);

record SourceAuditRequirementMatrix(
    int SourceOfTruthFileCount,
    int SourceCSharpFileCount,
    int SourceCardDataCandidateCount,
    int ExpectedSourceScopeNotSourceCount,
    int FalsePositiveSourceFileCount,
    int ScriptableObjectOutsideSourceCount,
    int ProjectOwnedAssetsOutsideSourceCount,
    int EditorToolCodeOutsideSourceCount);

record CallGraphRequirementMatrix(
    int CallEdgeCount,
    int SourceOfTruthCallerEdgeCount,
    int UnityTaggedCount,
    int GManagerTaggedCount,
    int PhotonTaggedCount,
    int UiTaggedCount,
    int CoroutineTaggedCount,
    SortedDictionary<string, int> CrossBoundaryCounts,
    SortedDictionary<string, int> TagCounts);

record MatrixFileSample(string RelativePath, string NeedLevel, string HeadlessCategory, string PrimaryRole, string Note);

record HeadlessRequirementSummaryDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    InputArtifacts InputArtifacts,
    RequirementPolicy Policy,
    HeadlessRequirementTotals Totals,
    SortedDictionary<string, int> NeedLevelCounts,
    SortedDictionary<string, int> HeadlessCategoryCounts,
    SortedDictionary<string, int> PrimaryRoleCounts,
    SortedDictionary<string, int> SourceScopeCounts,
    SortedDictionary<string, int> TopLevelFolderCounts,
    SortedDictionary<string, int> FolderNeedLevelCounts,
    SortedDictionary<string, int> FolderHeadlessCategoryCounts,
    CSharpRequirementMatrix CSharpMatrix,
    CardDataRequirementMatrix CardDataMatrix,
    SourceAuditRequirementMatrix SourceAuditMatrix,
    CallGraphRequirementMatrix CallGraphMatrix,
    List<MatrixFileSample> RequiredFileSamples,
    List<MatrixFileSample> CandidateReviewFileSamples,
    List<MatrixFileSample> UnknownFileSamples,
    List<MatrixFileSample> ExcludedVisualSoundSamples,
    List<MatrixFileSample> LargestRequiredFileSamples,
    string[] NextGoalRecommendations);

record HeadlessRequirementDetailDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    HeadlessRequirementSummaryDocument Summary,
    List<MatrixFileRecord> Files,
    List<MatrixFolderRecord> Folders);
