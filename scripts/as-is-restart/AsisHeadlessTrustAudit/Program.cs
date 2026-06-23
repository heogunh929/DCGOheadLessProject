using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

var workspace = Directory.GetCurrentDirectory();
var generatedDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");
var docsDir = Path.Combine(workspace, "docs", "as-is-restart");
var srcDir = Path.Combine(workspace, "src");
var engineDir = Path.Combine(srcDir, "DCGO.RL.Engine");
var testsDir = Path.Combine(srcDir, "DCGO.RL.Engine.Tests");

var headlessSummaryPath = Path.Combine(generatedDir, "asis-headless-requirement-summary.json");
var headlessMatrixPath = Path.Combine(generatedDir, "asis-headless-requirement-matrix.json");
var cardIndexPath = Path.Combine(generatedDir, "asis-cardbaseentity-card-index.json");
var foundationGatePath = Path.Combine(workspace, "docs", "generated", "foundation-completion-gate.json");
var capabilityRegistryPath = Path.Combine(workspace, "docs", "generated", "capability-truth-audit", "capability-registry.json");
var statusMismatchPath = Path.Combine(workspace, "docs", "generated", "capability-truth-audit", "status-mismatch-report.json");
var parityEvidencePath = Path.Combine(workspace, "docs", "generated", "full-card-parity-evidence.json");
var statusRegistryPath = Path.Combine(workspace, "docs", "generated", "full-card-source-scaffold", "status-registry.json");
var sourceScaffoldIndexPath = Path.Combine(workspace, "docs", "generated", "full-card-source-scaffold", "index.json");
var fullCardPoolBaselinePath = Path.Combine(workspace, "docs", "generated", "full-card-pool-validation-baseline-65.json");

var detailJsonPath = Path.Combine(generatedDir, "asis-headless-trust-audit.json");
var summaryJsonPath = Path.Combine(generatedDir, "asis-headless-trust-audit-summary.json");
var detailMarkdownPath = Path.Combine(docsDir, "GOAL_08_ASIS_HEADLESS_TRUST_AUDIT.md");
var summaryMarkdownPath = Path.Combine(docsDir, "asis-headless-trust-audit-summary.md");

using var headlessSummaryJson = JsonDocument.Parse(File.ReadAllBytes(headlessSummaryPath));
using var headlessMatrixJson = JsonDocument.Parse(File.ReadAllBytes(headlessMatrixPath));
using var cardIndexJson = JsonDocument.Parse(File.ReadAllBytes(cardIndexPath));
using var foundationGateJson = JsonDocument.Parse(File.ReadAllBytes(foundationGatePath));
using var capabilityRegistryJson = JsonDocument.Parse(File.ReadAllBytes(capabilityRegistryPath));
using var statusMismatchJson = JsonDocument.Parse(File.ReadAllBytes(statusMismatchPath));
using var parityEvidenceJson = JsonDocument.Parse(File.ReadAllBytes(parityEvidencePath));
using var statusRegistryJson = JsonDocument.Parse(File.ReadAllBytes(statusRegistryPath));
using var sourceScaffoldIndexJson = JsonDocument.Parse(File.ReadAllBytes(sourceScaffoldIndexPath));
using var fullCardPoolBaselineJson = JsonDocument.Parse(File.ReadAllBytes(fullCardPoolBaselinePath));

var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");
var asisRoot = headlessSummaryJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var srcFiles = Directory.Exists(srcDir)
    ? Directory.GetFiles(srcDir, "*.cs", SearchOption.AllDirectories).OrderBy(path => path, StringComparer.OrdinalIgnoreCase).ToList()
    : new List<string>();

var sourceEffectClassNames = cardIndexJson.RootElement.GetProperty("cards")
    .EnumerateArray()
    .Where(card => GetInt(card, "cardEffectSourceCandidateCount") > 0)
    .Select(card => card.GetProperty("cardEffectClassName").GetString() ?? "")
    .Where(value => !string.IsNullOrWhiteSpace(value) && !string.Equals(value, "NoEffect", StringComparison.OrdinalIgnoreCase))
    .ToHashSet(StringComparer.OrdinalIgnoreCase);

var sourceEffectPathByClass = cardIndexJson.RootElement.GetProperty("cards")
    .EnumerateArray()
    .Where(card => GetInt(card, "cardEffectSourceCandidateCount") > 0)
    .GroupBy(card => card.GetProperty("cardEffectClassName").GetString() ?? "", StringComparer.OrdinalIgnoreCase)
    .Where(group => !string.IsNullOrWhiteSpace(group.Key))
    .ToDictionary(
        group => group.Key,
        group => group.First().GetProperty("cardEffectSourceCandidates").EnumerateArray().FirstOrDefault().GetString() ?? "",
        StringComparer.OrdinalIgnoreCase);

var srcFileAudits = srcFiles.Select(AuditSourceFile).ToList();
var cardScriptAudits = srcFileAudits
    .Where(file => file.RelativePath.StartsWith("src/DCGO.RL.Engine/CardEffects/", StringComparison.Ordinal) && file.IsCardSpecificFile)
    .ToList();
var localSourceEffectTokens = cardScriptAudits.SelectMany(file => file.SourceEffectClassNameTokens).Where(IsCardEffectClassName).ToHashSet(StringComparer.OrdinalIgnoreCase);
var matchedLocalCardClasses = localSourceEffectTokens.Where(sourceEffectClassNames.Contains).OrderBy(value => value, StringComparer.OrdinalIgnoreCase).ToList();
var unmatchedLocalCardClasses = localSourceEffectTokens.Where(value => !sourceEffectClassNames.Contains(value)).OrderBy(value => value, StringComparer.OrdinalIgnoreCase).ToList();
var missingSourceEffectClasses = sourceEffectClassNames.Where(value => !localSourceEffectTokens.Contains(value)).OrderBy(value => value, StringComparer.OrdinalIgnoreCase).ToList();
var forbiddenHits = srcFileAudits.SelectMany(file => file.ForbiddenDependencyHits).ToList();
var prematureRlHits = srcFileAudits.SelectMany(file => file.PrematureRlComponentHits).ToList();

var testProgramPath = Path.Combine(testsDir, "Program.cs");
var staticTestCaseCount = File.Exists(testProgramPath)
    ? CountStaticTestCases(File.ReadAllText(testProgramPath))
    : 0;

var gateSummary = foundationGateJson.RootElement.GetProperty("summary");
var capabilitySummary = capabilityRegistryJson.RootElement.GetProperty("summary");
var paritySummary = parityEvidenceJson.RootElement.GetProperty("summary");
var statusMismatchSummary = statusMismatchJson.RootElement.GetProperty("summary");
var sourceScaffoldSummary = sourceScaffoldIndexJson.RootElement.GetProperty("summary");
var fullCardDecision = fullCardPoolBaselineJson.RootElement.GetProperty("decision").GetString() ?? "";

var localStatusTokenCounts = CountMany(srcFileAudits, file => file.CardEffectPortingStatusTokens);
var capabilityStatusCounts = ToSortedDictionary(capabilitySummary.GetProperty("statusCounts"));
var generatedStatusCounts = ToSortedDictionary(statusRegistryJson.RootElement.GetProperty("sourceScaffoldStatusCounts"));
var cardMappingStatusCounts = ToSortedDictionary(statusRegistryJson.RootElement.GetProperty("cardMappingStatusCounts"));
var parityCoverageCounts = ToSortedDictionary(paritySummary.GetProperty("coverageStatusCounts"));

var findings = BuildFindings(
    gateSummary,
    statusMismatchSummary,
    paritySummary,
    forbiddenHits,
    prematureRlHits,
    missingSourceEffectClasses,
    matchedLocalCardClasses,
    staticTestCaseCount);

var summary = new HeadlessTrustAuditSummaryDocument(
    "dcgo.as-is-headless-trust-audit-summary.v1",
    generatedAtUtc,
    "GOAL 08 AS-IS Existing Headless Implementation Trust Audit",
    asisRoot,
    new TrustAuditInputs(
        headlessSummaryPath,
        headlessSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        headlessMatrixPath,
        headlessMatrixJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        cardIndexPath,
        cardIndexJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        foundationGatePath,
        foundationGateJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        capabilityRegistryPath,
        capabilityRegistryJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        statusMismatchPath,
        statusMismatchJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        parityEvidencePath,
        parityEvidenceJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        statusRegistryPath,
        statusRegistryJson.RootElement.TryGetProperty("schemaVersion", out var statusSchema) ? statusSchema.ToString() : "",
        sourceScaffoldIndexPath,
        sourceScaffoldIndexJson.RootElement.TryGetProperty("schemaVersion", out var scaffoldSchema) ? scaffoldSchema.ToString() : "",
        fullCardPoolBaselinePath,
        fullCardPoolBaselineJson.RootElement.GetProperty("schemaVersion").GetString() ?? ""),
    new TrustAuditPolicy(
        "기존 headless 구현은 SourceOfTruth의 대체물이 아니라 검증 대상이다. 파일 존재, enum 상태, 문서 문자열만으로 Verified를 부여하지 않는다.",
        new[]
        {
            "OpenCodeReady=false이면 card-porting batch와 RL 학습 구성요소는 신뢰 승격 대상이 아니다.",
            "generated full-card source scaffold의 Implemented/Verified count가 0이면 전체 카드풀 구현 완료로 보지 않는다.",
            "local CardEffectPortingStatus.Implemented token은 legacy/pilot evidence일 수 있으므로 generated status와 parity evidence로 재검증해야 한다.",
            "UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성은 RL.Engine에 들어오면 trust blocker로 기록한다.",
            "Observation, Reward, Dataset, Trainer, RL Environment 계열은 OpenCodeReady 전 구현 금지 대상으로 정적 감사한다."
        }),
    new TrustAuditTotals(
        srcFileAudits.Count,
        srcFileAudits.Count(file => file.ProjectArea == "Engine"),
        srcFileAudits.Count(file => file.ProjectArea == "Tests"),
        srcFileAudits.Count(file => file.ProjectArea == "Other"),
        cardScriptAudits.Count,
        localSourceEffectTokens.Count,
        matchedLocalCardClasses.Count,
        unmatchedLocalCardClasses.Count,
        sourceEffectClassNames.Count,
        missingSourceEffectClasses.Count,
        forbiddenHits.Count,
        prematureRlHits.Count,
        staticTestCaseCount,
        findings.Count(finding => finding.Severity == "Blocker"),
        findings.Count(finding => finding.Severity == "High"),
        findings.Count(finding => finding.Severity == "Medium"),
        findings.Count(finding => finding.Status == "Pass")),
    new FoundationGateTrustSnapshot(
        GetBool(gateSummary, "openCodeReady"),
        GetInt(gateSummary, "passedGateCount"),
        GetInt(gateSummary, "failedGateCount"),
        GetInt(gateSummary, "unknownCommonApiCount"),
        GetInt(gateSummary, "unsupportedCapabilityCount"),
        GetInt(gateSummary, "partiallyImplementedCapabilityCount"),
        GetInt(gateSummary, "runtimeGeneratedStatusMismatchCount"),
        GetInt(gateSummary, "legacyPilotRuntimeDivergenceCount"),
        GetInt(gateSummary, "directZoneMutationCount"),
        gateSummary.GetProperty("selectedNextFoundationCapability").GetString() ?? "",
        gateSummary.GetProperty("selectedNextFoundationStatus").GetString() ?? ""),
    new GeneratedStatusTrustSnapshot(
        GetInt(statusRegistryJson.RootElement, "implementedOrVerifiedCount"),
        generatedStatusCounts,
        cardMappingStatusCounts,
        GetInt(sourceScaffoldSummary, "sourceScaffoldRecordCount"),
        GetInt(sourceScaffoldSummary, "missingSourceBodyCount"),
        GetInt(sourceScaffoldSummary, "needsSourceReviewMarkerCount"),
        GetInt(sourceScaffoldSummary, "noEffectMarkerCount"),
        fullCardDecision),
    new ParityTrustSnapshot(
        GetInt(paritySummary, "sourceEffectCount"),
        GetInt(paritySummary, "passedSourceEffectCount"),
        GetInt(paritySummary, "failedSourceEffectCount"),
        GetInt(paritySummary, "notRunSourceEffectCount"),
        GetBool(paritySummary, "allGeneratedSourceEffectsHaveUnityParity"),
        parityCoverageCounts),
    new CapabilityTrustSnapshot(
        GetInt(capabilitySummary, "capabilityCount"),
        GetInt(capabilitySummary, "documentationConflictCount"),
        capabilityStatusCounts),
    new LocalImplementationTrustSnapshot(
        CountBy(srcFileAudits, file => file.ProjectArea),
        CountBy(srcFileAudits, file => file.Component),
        localStatusTokenCounts,
        CountBy(cardScriptAudits, file => file.SetCode),
        matchedLocalCardClasses.Take(500).ToList(),
        unmatchedLocalCardClasses.Take(500).ToList(),
        missingSourceEffectClasses.Take(1000).ToList()),
    forbiddenHits.Take(500).ToList(),
    prematureRlHits.Take(500).ToList(),
    findings,
    new[]
    {
        "GOAL 09에서는 Required runtime source, CardEffect source, CardBaseEntity data, generated gate blockers를 분리해 구현 우선순위를 다시 작성한다.",
        "기존 src 구현은 reuse 후보로 보되, SourceOfTruth mapping, 현재 테스트, parity fixture, generated status가 함께 맞을 때만 신뢰 수준을 올린다.",
        "OpenCodeReady=false 상태에서는 C0039 이후 card-porting과 RL Environment/Observation/Reward/Dataset/Trainer 구현을 계속 제외한다.",
        "GOAL 09의 우선순위는 ContinuousOrStaticEffect partial closure, unknown common API 27건, unsupported capability 26건을 먼저 반영해야 한다.",
        "대용량 GOAL 04/05 raw index는 git 업로드 전에 추적 제거 또는 LFS/외부 보관 정책이 필요하다."
    });

var detail = new HeadlessTrustAuditDetailDocument(
    "dcgo.as-is-headless-trust-audit.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    summary,
    srcFileAudits,
    cardScriptAudits.Select(file => new CardScriptAuditRecord(
        file.RelativePath,
        file.SetCode,
        file.ClassNames,
        file.SourceEffectClassNameTokens,
        file.CardEffectPortingStatusTokens,
        file.SourceEffectClassNameTokens.Where(IsCardEffectClassName).Where(sourceEffectClassNames.Contains).ToArray(),
        file.SourceEffectClassNameTokens.Where(IsCardEffectClassName).Where(name => !sourceEffectClassNames.Contains(name)).ToArray())).ToList(),
    missingSourceEffectClasses.Select(name => new MissingSourceEffectRecord(name, sourceEffectPathByClass.GetValueOrDefault(name, ""))).Take(5000).ToList());

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
    srcCSharpFileCount = summary.Totals.SrcCSharpFileCount,
    engineCSharpFileCount = summary.Totals.EngineCSharpFileCount,
    testCSharpFileCount = summary.Totals.TestCSharpFileCount,
    cardScriptFileCount = summary.Totals.CardScriptFileCount,
    localCardEffectClassCount = summary.Totals.LocalCardEffectClassCount,
    matchedSourceEffectClassCount = summary.Totals.MatchedSourceEffectClassCount,
    missingSourceEffectClassCount = summary.Totals.MissingSourceEffectClassCount,
    forbiddenDependencyHitCount = summary.Totals.ForbiddenDependencyHitCount,
    prematureRlComponentHitCount = summary.Totals.PrematureRlComponentHitCount,
    blockerFindingCount = summary.Totals.BlockerFindingCount,
    outputFiles = new[] { detailMarkdownPath, summaryMarkdownPath, detailJsonPath, summaryJsonPath }
}, jsonOptions));

SourceFileAuditRecord AuditSourceFile(string path)
{
    var relative = Path.GetRelativePath(workspace, path).Replace('\\', '/');
    var text = File.ReadAllText(path);
    var classNames = Regex.Matches(text, @"\bclass\s+([A-Za-z_][A-Za-z0-9_]*)")
        .Select(match => match.Groups[1].Value)
        .Distinct(StringComparer.Ordinal)
        .OrderBy(value => value, StringComparer.Ordinal)
        .ToArray();
    var statusTokens = Regex.Matches(text, @"CardEffectPortingStatus\.([A-Za-z_][A-Za-z0-9_]*)")
        .Select(match => match.Groups[1].Value)
        .Distinct(StringComparer.Ordinal)
        .OrderBy(value => value, StringComparer.Ordinal)
        .ToArray();
    var sourceEffectClassNameTokens = Regex.Matches(text, "\"([A-Z]{1,3}[0-9]*_[0-9]{2,3}(?:_P[0-9]+)?)\"")
        .Select(match => match.Groups[1].Value)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .OrderBy(value => value, StringComparer.OrdinalIgnoreCase)
        .ToArray();
    var forbidden = ScanForbidden(relative, text);
    var prematureRl = ScanPrematureRlComponents(relative, classNames);
    return new SourceFileAuditRecord(
        relative,
        ProjectArea(relative),
        Component(relative),
        SetCode(relative),
        IsCardSpecificFile(relative),
        classNames,
        sourceEffectClassNameTokens,
        statusTokens,
        forbidden,
        prematureRl);
}

List<PatternHit> ScanForbidden(string relativePath, string text)
{
    var patterns = new[]
    {
        ("UnityEngineUsing", @"\busing\s+UnityEngine\b"),
        ("UnityEngineReference", @"\bUnityEngine\."),
        ("Photon", @"\busing\s+Photon\b|\bPhoton\."),
        ("MonoBehaviour", @"\bMonoBehaviour\b"),
        ("GameObject", @"\bGameObject\b"),
        ("CoroutineApi", @"\b(StartCoroutine|StopCoroutine)\b"),
        ("UnityUi", @"\bUnityEngine\.UI\b"),
        ("TMPro", @"\bTMPro\b")
    };
    return ScanPatterns(relativePath, text, patterns);
}

int CountStaticTestCases(string text)
{
    var count = 0;
    foreach (var line in text.Replace("\r\n", "\n").Split('\n'))
    {
        if (line.Contains("Console.WriteLine($\"All", StringComparison.Ordinal))
        {
            break;
        }
        if (Regex.IsMatch(line, "^\\s*\\(\\s*\"[^\"]+\"\\s*,\\s*[A-Za-z_][A-Za-z0-9_]*\\s*\\),?\\s*$"))
        {
            count++;
        }
    }
    return count;
}

List<PatternHit> ScanPrematureRlComponents(string relativePath, string[] classNames)
{
    var hits = new List<PatternHit>();
    var names = classNames.Concat(new[] { Path.GetFileNameWithoutExtension(relativePath) }).ToArray();
    foreach (var name in names)
    {
        foreach (var token in new[] { "Observation", "Reward", "Dataset", "Trainer", "Environment" })
        {
            if (name.Contains(token, StringComparison.OrdinalIgnoreCase))
            {
                hits.Add(new PatternHit(relativePath, token, 0, name));
            }
        }
    }
    return hits;
}

List<PatternHit> ScanPatterns(string relativePath, string text, (string Id, string Pattern)[] patterns)
{
    var hits = new List<PatternHit>();
    var lines = text.Replace("\r\n", "\n").Split('\n');
    for (var i = 0; i < lines.Length; i++)
    {
        foreach (var (id, pattern) in patterns)
        {
            if (Regex.IsMatch(lines[i], pattern))
            {
                hits.Add(new PatternHit(relativePath, id, i + 1, lines[i].Trim()));
            }
        }
    }
    return hits;
}

List<TrustFinding> BuildFindings(
    JsonElement gateSummary,
    JsonElement statusMismatchSummary,
    JsonElement paritySummary,
    List<PatternHit> forbiddenHits,
    List<PatternHit> prematureRlHits,
    List<string> missingSourceEffectClasses,
    List<string> matchedLocalCardClasses,
    int staticTestCaseCount)
{
    var result = new List<TrustFinding>();
    var openCodeReady = GetBool(gateSummary, "openCodeReady");
    result.Add(new TrustFinding(
        "foundation-gate-open-code-ready",
        openCodeReady ? "Pass" : "Blocked",
        openCodeReady ? "Medium" : "Blocker",
        $"OpenCodeReady={openCodeReady}. failedGateCount={GetInt(gateSummary, "failedGateCount")}, selected={gateSummary.GetProperty("selectedNextFoundationCapability").GetString()}:{gateSummary.GetProperty("selectedNextFoundationStatus").GetString()}.",
        "OpenCodeReady=false이면 기존 headless 구현을 전체 카드풀 완료 상태로 신뢰하지 않는다."));
    result.Add(new TrustFinding(
        "generated-status-implemented-or-verified",
        GetInt(statusRegistryJson.RootElement, "implementedOrVerifiedCount") == 0 ? "Blocked" : "Review",
        "Blocker",
        $"generated status registry implementedOrVerifiedCount={GetInt(statusRegistryJson.RootElement, "implementedOrVerifiedCount")}.",
        "generated source scaffold 기준 구현 완료/검증 완료가 0이므로 local 구현 상태 token은 승격 근거가 아니다."));
    result.Add(new TrustFinding(
        "full-card-parity-evidence",
        GetInt(paritySummary, "notRunSourceEffectCount") == 0 ? "Pass" : "Blocked",
        "Blocker",
        $"notRun={GetInt(paritySummary, "notRunSourceEffectCount")}, passed={GetInt(paritySummary, "passedSourceEffectCount")}, failed={GetInt(paritySummary, "failedSourceEffectCount")}.",
        "full-card parity fixture/report가 NotRun이면 SourceOfTruth parity 검증 완료로 보지 않는다."));
    result.Add(new TrustFinding(
        "status-mismatch-and-legacy-divergence",
        GetInt(statusMismatchSummary, "statusMismatchCount") == 0 ? "Review" : "Blocked",
        GetInt(statusMismatchSummary, "legacyPilotDivergenceCount") > 0 ? "High" : "Medium",
        $"statusMismatch={GetInt(statusMismatchSummary, "statusMismatchCount")}, legacyPilotDivergence={GetInt(statusMismatchSummary, "legacyPilotDivergenceCount")}.",
        "authoritative mismatch는 0이지만 legacy pilot divergence는 기존 구현을 자동 신뢰하지 말아야 하는 근거다."));
    result.Add(new TrustFinding(
        "forbidden-unity-photon-dependencies",
        forbiddenHits.Count == 0 ? "Pass" : "Blocked",
        forbiddenHits.Count == 0 ? "Medium" : "Blocker",
        $"forbiddenDependencyHitCount={forbiddenHits.Count}.",
        "RL.Engine은 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine/UI 의존성을 포함하지 않아야 한다."));
    result.Add(new TrustFinding(
        "premature-rl-components",
        prematureRlHits.Count == 0 ? "Pass" : "Blocked",
        prematureRlHits.Count == 0 ? "Medium" : "Blocker",
        $"prematureRlComponentHitCount={prematureRlHits.Count}.",
        "OpenCodeReady=false 동안 RL Environment/Observation/Reward/Dataset/Trainer 구현은 금지된다."));
    result.Add(new TrustFinding(
        "local-cardeffect-source-coverage",
        missingSourceEffectClasses.Count == 0 ? "Pass" : "Review",
        missingSourceEffectClasses.Count == 0 ? "Medium" : "High",
        $"matchedLocalCardEffectClassCount={matchedLocalCardClasses.Count}, missingSourceEffectClassCount={missingSourceEffectClasses.Count}.",
        "local CardEffects 파일 존재는 제한적 pilot coverage일 뿐이며 SourceOfTruth effect class 전체와 비교해야 한다."));
    result.Add(new TrustFinding(
        "static-test-case-inventory",
        staticTestCaseCount > 0 ? "Review" : "Blocked",
        staticTestCaseCount > 0 ? "Medium" : "High",
        $"staticTestCaseCount={staticTestCaseCount}. 이 스크립트는 테스트를 실행하지 않고 정적 목록만 센다.",
        "테스트 목록은 신뢰 후보 증거지만 현재 실행 결과와 SourceOfTruth coverage를 대체하지 않는다."));
    return result;
}

static bool IsCardSpecificFile(string relativePath)
{
    var fileName = Path.GetFileName(relativePath);
    return Regex.IsMatch(fileName, @"^[A-Z]{1,3}[0-9]*_[0-9]{2,3}(_P[0-9]+)?\.cs$", RegexOptions.IgnoreCase);
}

static bool IsCardEffectClassName(string name) => Regex.IsMatch(name, @"^[A-Z]{1,3}[0-9]*_[0-9]{2,3}(_P[0-9]+)?$", RegexOptions.IgnoreCase);

static string ProjectArea(string relativePath)
{
    if (relativePath.StartsWith("src/DCGO.RL.Engine.Tests/", StringComparison.Ordinal))
    {
        return "Tests";
    }
    if (relativePath.StartsWith("src/DCGO.RL.Engine/", StringComparison.Ordinal))
    {
        return "Engine";
    }
    return "Other";
}

static string Component(string relativePath)
{
    if (relativePath.StartsWith("src/DCGO.RL.Engine.Tests/", StringComparison.Ordinal))
    {
        return "Tests";
    }
    if (!relativePath.StartsWith("src/DCGO.RL.Engine/", StringComparison.Ordinal))
    {
        return "Other";
    }
    var rest = relativePath["src/DCGO.RL.Engine/".Length..];
    var slash = rest.IndexOf('/');
    return slash < 0 ? "(root)" : rest[..slash];
}

static string SetCode(string relativePath)
{
    var marker = "src/DCGO.RL.Engine/CardEffects/";
    if (!relativePath.StartsWith(marker, StringComparison.Ordinal))
    {
        return "(not-cardeffects)";
    }
    var rest = relativePath[marker.Length..];
    var slash = rest.IndexOf('/');
    return slash < 0 ? "(root)" : rest[..slash];
}

static int GetInt(JsonElement item, string propertyName)
{
    if (!item.TryGetProperty(propertyName, out var property))
    {
        return 0;
    }
    return property.ValueKind == JsonValueKind.Number && property.TryGetInt32(out var value) ? value : 0;
}

static bool GetBool(JsonElement item, string propertyName)
{
    if (!item.TryGetProperty(propertyName, out var property))
    {
        return false;
    }
    return property.ValueKind == JsonValueKind.True || (property.ValueKind == JsonValueKind.String && bool.TryParse(property.GetString(), out var parsed) && parsed);
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

static SortedDictionary<string, int> CountMany<T>(IEnumerable<T> items, Func<T, IEnumerable<string>> keySelector)
{
    var result = new SortedDictionary<string, int>(StringComparer.Ordinal);
    foreach (var item in items)
    {
        foreach (var keyValue in keySelector(item))
        {
            var key = string.IsNullOrWhiteSpace(keyValue) ? "(blank)" : keyValue;
            result[key] = result.GetValueOrDefault(key) + 1;
        }
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

static void WriteUtf8(string path, string contents)
{
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    File.WriteAllText(path, contents, new UTF8Encoding(false));
}

static string BuildSummaryMarkdown(HeadlessTrustAuditSummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# AS-IS Headless Trust Audit Summary");
    builder.AppendLine();
    builder.AppendLine($"- 생성 시각(UTC): `{summary.GeneratedAtUtc}`");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- src C# 파일 수: {summary.Totals.SrcCSharpFileCount:N0}");
    builder.AppendLine($"- Engine C# 파일 수: {summary.Totals.EngineCSharpFileCount:N0}");
    builder.AppendLine($"- Test C# 파일 수: {summary.Totals.TestCSharpFileCount:N0}");
    builder.AppendLine($"- CardEffects 파일 수: {summary.Totals.CardScriptFileCount:N0}");
    builder.AppendLine($"- SourceEffect class 매칭 수: {summary.Totals.MatchedSourceEffectClassCount:N0}");
    builder.AppendLine($"- SourceEffect class 미매칭 수: {summary.Totals.MissingSourceEffectClassCount:N0}");
    builder.AppendLine($"- 금지 의존성 hit: {summary.Totals.ForbiddenDependencyHitCount:N0}");
    builder.AppendLine($"- 조기 RL 구성요소 hit: {summary.Totals.PrematureRlComponentHitCount:N0}");
    builder.AppendLine($"- Blocker finding 수: {summary.Totals.BlockerFindingCount:N0}");
    builder.AppendLine();
    builder.AppendLine("## Findings");
    AppendTable(builder, new[] { "ID", "Status", "Severity", "Evidence" }, summary.Findings.Select(finding => new[] { finding.Id, finding.Status, finding.Severity, finding.Evidence }));
    builder.AppendLine("## 다음 Goal 추천");
    foreach (var recommendation in summary.NextGoalRecommendations)
    {
        builder.AppendLine($"- {recommendation}");
    }
    return builder.ToString();
}

static string BuildDetailMarkdown(HeadlessTrustAuditSummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# GOAL 08 AS-IS Headless Trust Audit");
    builder.AppendLine();
    builder.AppendLine("이번 문서는 현재 `src` headless 구현과 generated status를 SourceOfTruth 기준으로 신뢰 가능한지 감사한 기준선이다.");
    builder.AppendLine("구현 변경, CardEffect body 추가, generated status 승격, card-porting batch 실행은 수행하지 않았다.");
    builder.AppendLine();
    builder.AppendLine("## 입력");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- GOAL 07 matrix: `{summary.Inputs.HeadlessMatrixPath}`");
    builder.AppendLine($"- Foundation gate: `{summary.Inputs.FoundationGatePath}`");
    builder.AppendLine($"- Capability registry: `{summary.Inputs.CapabilityRegistryPath}`");
    builder.AppendLine($"- Full-card parity evidence: `{summary.Inputs.ParityEvidencePath}`");
    builder.AppendLine($"- Generated status registry: `{summary.Inputs.StatusRegistryPath}`");
    builder.AppendLine();
    builder.AppendLine("## 정책");
    builder.AppendLine(summary.Policy.EvidenceScope);
    foreach (var rule in summary.Policy.Rules)
    {
        builder.AppendLine($"- {rule}");
    }
    builder.AppendLine();
    builder.AppendLine("## Trust totals");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "src C# files", summary.Totals.SrcCSharpFileCount.ToString("N0") },
        new[] { "Engine C# files", summary.Totals.EngineCSharpFileCount.ToString("N0") },
        new[] { "Test C# files", summary.Totals.TestCSharpFileCount.ToString("N0") },
        new[] { "Card script files", summary.Totals.CardScriptFileCount.ToString("N0") },
        new[] { "Local CardEffect classes", summary.Totals.LocalCardEffectClassCount.ToString("N0") },
        new[] { "Matched SourceEffect classes", summary.Totals.MatchedSourceEffectClassCount.ToString("N0") },
        new[] { "Missing SourceEffect classes", summary.Totals.MissingSourceEffectClassCount.ToString("N0") },
        new[] { "Forbidden dependency hits", summary.Totals.ForbiddenDependencyHitCount.ToString("N0") },
        new[] { "Premature RL component hits", summary.Totals.PrematureRlComponentHitCount.ToString("N0") },
        new[] { "Static test case count", summary.Totals.StaticTestCaseCount.ToString("N0") }
    });
    builder.AppendLine("## Foundation gate snapshot");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "OpenCodeReady", summary.FoundationGate.OpenCodeReady.ToString() },
        new[] { "Passed gates", summary.FoundationGate.PassedGateCount.ToString("N0") },
        new[] { "Failed gates", summary.FoundationGate.FailedGateCount.ToString("N0") },
        new[] { "Unknown common API", summary.FoundationGate.UnknownCommonApiCount.ToString("N0") },
        new[] { "Unsupported capability", summary.FoundationGate.UnsupportedCapabilityCount.ToString("N0") },
        new[] { "PartiallyImplemented capability", summary.FoundationGate.PartiallyImplementedCapabilityCount.ToString("N0") },
        new[] { "Runtime/generated status mismatch", summary.FoundationGate.RuntimeGeneratedStatusMismatchCount.ToString("N0") },
        new[] { "Legacy pilot divergence", summary.FoundationGate.LegacyPilotRuntimeDivergenceCount.ToString("N0") },
        new[] { "Selected next", $"{summary.FoundationGate.SelectedNextFoundationCapability}:{summary.FoundationGate.SelectedNextFoundationStatus}" }
    });
    builder.AppendLine("## Generated status snapshot");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "ImplementedOrVerified", summary.GeneratedStatus.ImplementedOrVerifiedCount.ToString("N0") },
        new[] { "Source scaffold records", summary.GeneratedStatus.SourceScaffoldRecordCount.ToString("N0") },
        new[] { "Missing source body", summary.GeneratedStatus.MissingSourceBodyCount.ToString("N0") },
        new[] { "NeedsSourceReview markers", summary.GeneratedStatus.NeedsSourceReviewMarkerCount.ToString("N0") },
        new[] { "NoEffect markers", summary.GeneratedStatus.NoEffectMarkerCount.ToString("N0") },
        new[] { "Full-card pool decision", summary.GeneratedStatus.FullCardPoolDecision }
    });
    builder.AppendLine("## Parity evidence snapshot");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "Source effects", summary.Parity.SourceEffectCount.ToString("N0") },
        new[] { "Passed", summary.Parity.PassedSourceEffectCount.ToString("N0") },
        new[] { "Failed", summary.Parity.FailedSourceEffectCount.ToString("N0") },
        new[] { "NotRun", summary.Parity.NotRunSourceEffectCount.ToString("N0") },
        new[] { "All generated source effects have Unity parity", summary.Parity.AllGeneratedSourceEffectsHaveUnityParity.ToString() }
    });
    builder.AppendLine("## Local status token counts");
    AppendTable(builder, new[] { "Status token", "파일 수" }, summary.LocalImplementation.LocalStatusTokenCounts.Select(pair => new[] { pair.Key, pair.Value.ToString("N0") }));
    builder.AppendLine("## Findings");
    AppendTable(builder, new[] { "ID", "Status", "Severity", "Evidence", "Trust impact" }, summary.Findings.Select(finding => new[] { finding.Id, finding.Status, finding.Severity, finding.Evidence, finding.TrustImpact }));
    builder.AppendLine("## 금지 사항 준수");
    builder.AppendLine("- `src/` C# 코드 수정 없음.");
    builder.AppendLine("- 원본 `DCGO/Assets` 수정 없음.");
    builder.AppendLine("- CardEffect body 구현 없음.");
    builder.AppendLine("- C0039 이후 card-porting 실행 없음.");
    builder.AppendLine("- generated status 승격 없음.");
    builder.AppendLine("- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.");
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

record TrustAuditInputs(
    string HeadlessSummaryPath,
    string HeadlessSummaryGeneratedAtUtc,
    string HeadlessMatrixPath,
    string HeadlessMatrixGeneratedAtUtc,
    string CardIndexPath,
    string CardIndexGeneratedAtUtc,
    string FoundationGatePath,
    string FoundationGateSchemaVersion,
    string CapabilityRegistryPath,
    string CapabilityRegistrySchemaVersion,
    string StatusMismatchPath,
    string StatusMismatchSchemaVersion,
    string ParityEvidencePath,
    string ParityEvidenceSchemaVersion,
    string StatusRegistryPath,
    string StatusRegistrySchemaVersion,
    string SourceScaffoldIndexPath,
    string SourceScaffoldIndexSchemaVersion,
    string FullCardPoolBaselinePath,
    string FullCardPoolBaselineSchemaVersion);

record TrustAuditPolicy(string EvidenceScope, string[] Rules);

record TrustAuditTotals(
    int SrcCSharpFileCount,
    int EngineCSharpFileCount,
    int TestCSharpFileCount,
    int OtherCSharpFileCount,
    int CardScriptFileCount,
    int LocalCardEffectClassCount,
    int MatchedSourceEffectClassCount,
    int UnmatchedLocalCardEffectClassCount,
    int SourceEffectClassCount,
    int MissingSourceEffectClassCount,
    int ForbiddenDependencyHitCount,
    int PrematureRlComponentHitCount,
    int StaticTestCaseCount,
    int BlockerFindingCount,
    int HighFindingCount,
    int MediumFindingCount,
    int PassFindingCount);

record FoundationGateTrustSnapshot(
    bool OpenCodeReady,
    int PassedGateCount,
    int FailedGateCount,
    int UnknownCommonApiCount,
    int UnsupportedCapabilityCount,
    int PartiallyImplementedCapabilityCount,
    int RuntimeGeneratedStatusMismatchCount,
    int LegacyPilotRuntimeDivergenceCount,
    int DirectZoneMutationCount,
    string SelectedNextFoundationCapability,
    string SelectedNextFoundationStatus);

record GeneratedStatusTrustSnapshot(
    int ImplementedOrVerifiedCount,
    SortedDictionary<string, int> SourceScaffoldStatusCounts,
    SortedDictionary<string, int> CardMappingStatusCounts,
    int SourceScaffoldRecordCount,
    int MissingSourceBodyCount,
    int NeedsSourceReviewMarkerCount,
    int NoEffectMarkerCount,
    string FullCardPoolDecision);

record ParityTrustSnapshot(
    int SourceEffectCount,
    int PassedSourceEffectCount,
    int FailedSourceEffectCount,
    int NotRunSourceEffectCount,
    bool AllGeneratedSourceEffectsHaveUnityParity,
    SortedDictionary<string, int> CoverageStatusCounts);

record CapabilityTrustSnapshot(
    int CapabilityCount,
    int DocumentationConflictCount,
    SortedDictionary<string, int> StatusCounts);

record LocalImplementationTrustSnapshot(
    SortedDictionary<string, int> ProjectAreaCounts,
    SortedDictionary<string, int> ComponentCounts,
    SortedDictionary<string, int> LocalStatusTokenCounts,
    SortedDictionary<string, int> CardScriptSetCounts,
    List<string> MatchedLocalCardEffectClasses,
    List<string> UnmatchedLocalCardEffectClasses,
    List<string> MissingSourceEffectClasses);

record PatternHit(string RelativePath, string PatternId, int Line, string Text);

record TrustFinding(string Id, string Status, string Severity, string Evidence, string TrustImpact);

record SourceFileAuditRecord(
    string RelativePath,
    string ProjectArea,
    string Component,
    string SetCode,
    bool IsCardSpecificFile,
    string[] ClassNames,
    string[] SourceEffectClassNameTokens,
    string[] CardEffectPortingStatusTokens,
    List<PatternHit> ForbiddenDependencyHits,
    List<PatternHit> PrematureRlComponentHits);

record CardScriptAuditRecord(
    string RelativePath,
    string SetCode,
    string[] ClassNames,
    string[] SourceEffectClassNameTokens,
    string[] CardEffectPortingStatusTokens,
    string[] MatchedSourceEffectClassNames,
    string[] UnmatchedLocalCardEffectClassNames);

record MissingSourceEffectRecord(string SourceEffectClassName, string SourcePath);

record HeadlessTrustAuditSummaryDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    TrustAuditInputs Inputs,
    TrustAuditPolicy Policy,
    TrustAuditTotals Totals,
    FoundationGateTrustSnapshot FoundationGate,
    GeneratedStatusTrustSnapshot GeneratedStatus,
    ParityTrustSnapshot Parity,
    CapabilityTrustSnapshot Capability,
    LocalImplementationTrustSnapshot LocalImplementation,
    List<PatternHit> ForbiddenDependencyHits,
    List<PatternHit> PrematureRlComponentHits,
    List<TrustFinding> Findings,
    string[] NextGoalRecommendations);

record HeadlessTrustAuditDetailDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    HeadlessTrustAuditSummaryDocument Summary,
    List<SourceFileAuditRecord> SourceFiles,
    List<CardScriptAuditRecord> CardScripts,
    List<MissingSourceEffectRecord> MissingSourceEffects);
