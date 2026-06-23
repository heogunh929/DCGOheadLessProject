using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

var workspace = Directory.GetCurrentDirectory();
var generatedDir = Path.Combine(workspace, "docs", "generated", "as-is-restart");
var docsDir = Path.Combine(workspace, "docs", "as-is-restart");

var headlessSummaryPath = Path.Combine(generatedDir, "asis-headless-requirement-summary.json");
var trustAuditSummaryPath = Path.Combine(generatedDir, "asis-headless-trust-audit-summary.json");
var cardDataStructurePath = Path.Combine(generatedDir, "asis-card-data-structure.json");
var foundationGatePath = Path.Combine(workspace, "docs", "generated", "foundation-completion-gate.json");
var mechanicSchedulerPath = Path.Combine(workspace, "docs", "generated", "capability-truth-audit", "mechanic-first-scheduler-66E.json");
var capabilityRegistryPath = Path.Combine(workspace, "docs", "generated", "capability-truth-audit", "capability-registry.json");
var sourceRequiredCapabilitiesPath = Path.Combine(workspace, "docs", "generated", "capability-truth-audit", "source-required-capabilities.json");
var fullCardPoolBaselinePath = Path.Combine(workspace, "docs", "generated", "full-card-pool-validation-baseline-65.json");

var detailJsonPath = Path.Combine(generatedDir, "asis-implementation-priority-rewrite.json");
var summaryJsonPath = Path.Combine(generatedDir, "asis-implementation-priority-summary.json");
var detailMarkdownPath = Path.Combine(docsDir, "GOAL_09_ASIS_IMPLEMENTATION_PRIORITY_REWRITE.md");
var summaryMarkdownPath = Path.Combine(docsDir, "asis-implementation-priority-rewrite-summary.md");

using var headlessSummaryJson = JsonDocument.Parse(File.ReadAllBytes(headlessSummaryPath));
using var trustAuditSummaryJson = JsonDocument.Parse(File.ReadAllBytes(trustAuditSummaryPath));
using var cardDataStructureJson = JsonDocument.Parse(File.ReadAllBytes(cardDataStructurePath));
using var foundationGateJson = JsonDocument.Parse(File.ReadAllBytes(foundationGatePath));
using var mechanicSchedulerJson = JsonDocument.Parse(File.ReadAllBytes(mechanicSchedulerPath));
using var capabilityRegistryJson = JsonDocument.Parse(File.ReadAllBytes(capabilityRegistryPath));
using var sourceRequiredCapabilitiesJson = JsonDocument.Parse(File.ReadAllBytes(sourceRequiredCapabilitiesPath));
using var fullCardPoolBaselineJson = JsonDocument.Parse(File.ReadAllBytes(fullCardPoolBaselinePath));

var generatedAtUtc = DateTimeOffset.UtcNow.ToString("O");
var asisRoot = headlessSummaryJson.RootElement.GetProperty("asisRoot").GetString() ?? "";
var gateSummary = foundationGateJson.RootElement.GetProperty("summary");
var schedulerSummary = mechanicSchedulerJson.RootElement.GetProperty("summary");
var selectedMechanic = mechanicSchedulerJson.RootElement.GetProperty("selectedMechanic");
var cardDataSummary = cardDataStructureJson.RootElement.GetProperty("summary");
var cardDataTotals = cardDataSummary.GetProperty("totals");
var trustTotals = trustAuditSummaryJson.RootElement.GetProperty("totals");
var headlessTotals = headlessSummaryJson.RootElement.GetProperty("totals");
var capabilityBlockers = mechanicSchedulerJson.RootElement.GetProperty("capabilityBlockers")
    .EnumerateArray()
    .Select(LoadCapabilityBlocker)
    .OrderByDescending(blocker => blocker.AffectedCardCount)
    .ThenBy(blocker => blocker.CapabilityId, StringComparer.Ordinal)
    .ToList();

var largeGeneratedFiles = Directory.GetFiles(generatedDir, "*", SearchOption.AllDirectories)
    .Select(path => new FileInfo(path))
    .Where(file => file.Length > 100L * 1024L * 1024L)
    .OrderByDescending(file => file.Length)
    .Select(file => new LargeGeneratedFileRecord(
        Path.GetRelativePath(workspace, file.FullName).Replace('\\', '/'),
        file.Length,
        "GitHub 일반 git push 제한 초과. raw generated artifact이므로 추적 제거, LFS, 압축/분할, 외부 보관 중 정책 결정 필요."))
    .ToList();

var priorityItems = new List<PriorityItem>
{
    new(
        1,
        "FND-001",
        "FoundationGate",
        "Immediate",
        "ContinuousOrStaticEffect partial closure",
        selectedMechanic.GetProperty("capabilityId").GetString() ?? "ContinuousOrStaticEffect",
        selectedMechanic.GetProperty("status").GetString() ?? "PartiallyImplemented",
        GetInt(selectedMechanic, "affectedCardCount"),
        GetInt(selectedMechanic, "sourceEffectCount"),
        "Mechanic-first scheduler가 선택한 최다 영향 blocker다.",
        new[]
        {
            $"OpenCodeReady={GetBool(gateSummary, "openCodeReady")}",
            $"selectedNextFoundationCapability={gateSummary.GetProperty("selectedNextFoundationCapability").GetString()}",
            $"selectedAffectedCardCount={GetInt(selectedMechanic, "affectedCardCount")}",
            $"selectedSourceEffectCount={GetInt(selectedMechanic, "sourceEffectCount")}"
        },
        new[] { "개별 CardEffect body 구현", "C0039 이후 card-porting batch 실행", "generated status 수동 승격" },
        "해당 capability의 partial 사유가 구체 하위 작업으로 소거되거나 명시 blocker queue로 분리되고 Foundation Gate 수치가 재계산된다."),
    new(
        2,
        "FND-002",
        "FoundationGate",
        "Immediate",
        "Unknown common API 27건 source-aligned mapping",
        "common-api-mapping-known",
        "NeedsSourceReview",
        0,
        0,
        "Foundation Gate가 unknown common API 27건 때문에 실패한다.",
        new[]
        {
            $"unknownCommonApiCount={GetInt(gateSummary, "unknownCommonApiCount")}",
            "GOAL 07 Required runtime source를 기준으로 mapping 대상만 좁힌다."
        },
        new[] { "unknown API를 silent no-op 처리", "문서 문자열만으로 Verified 처리" },
        "unknown common API가 capability matrix 또는 explicit unsupported queue로 모두 분류된다."),
    new(
        3,
        "FND-003",
        "FoundationGate",
        "Immediate",
        "Unsupported capability 26건 remediation 분해",
        "unsupported-capabilities-zero",
        "Unsupported",
        0,
        0,
        "Foundation Gate가 unsupported capability 26건 때문에 실패한다.",
        new[]
        {
            $"unsupportedCapabilityCount={GetInt(gateSummary, "unsupportedCapabilityCount")}",
            $"partiallyImplementedCapabilityCount={GetInt(gateSummary, "partiallyImplementedCapabilityCount")}"
        },
        new[] { "지원하지 않는 메커니즘을 통과 처리", "Unsupported descriptor를 빈 구현으로 대체" },
        "각 unsupported capability가 구현 가능 foundation prompt, partial 상태, 또는 명시 blocked 상태로 재분류된다."),
    new(
        4,
        "TRUST-001",
        "TrustAudit",
        "Immediate",
        "기존 src 구현 trust boundary 고정",
        "existing-headless-trust",
        "Review",
        0,
        0,
        "GOAL 08에서 local CardEffect token 163개는 SourceOfTruth와 매칭되지만 전체 source effect 3,905개 중 3,742개가 local 파일로 매칭되지 않는다.",
        new[]
        {
            $"matchedSourceEffectClassCount={GetInt(trustTotals, "matchedSourceEffectClassCount")}",
            $"missingSourceEffectClassCount={GetInt(trustTotals, "missingSourceEffectClassCount")}",
            $"legacyPilotRuntimeDivergenceCount={GetInt(gateSummary, "legacyPilotRuntimeDivergenceCount")}",
            $"forbiddenDependencyHitCount={GetInt(trustTotals, "forbiddenDependencyHitCount")}"
        },
        new[] { "local Implemented token을 full-card Generated Verified로 승격", "legacy pilot divergence 무시" },
        "기존 src 구현은 reuse candidate, blocked, stale, verified-by-test 후보로 분리되고 SourceOfTruth mapping evidence를 가진다."),
    new(
        5,
        "DATA-001",
        "SourceData",
        "ReviewNext",
        "CardBaseEntity import/variant/data anomaly policy",
        "card-data-policy",
        "Review",
        GetInt(headlessTotals, "cardDataFileCount"),
        0,
        "GOAL 06/07에서 CardBaseEntity asset은 headless data Required지만 variant/duplicate/effect-source anomaly 정책이 먼저 필요하다.",
        new[]
        {
            $"cardAssetCount={GetInt(cardDataTotals, "cardAssetCount")}",
            $"duplicateCardIdGroupCount={GetInt(cardDataTotals, "duplicateCardIdGroupCount")}",
            $"missingEffectSourceCandidateCount={GetInt(cardDataTotals, "missingEffectSourceCandidateCount")}",
            $"requiredCardDataFileCount={GetInt(headlessTotals, "cardDataFileCount")}"
        },
        new[] { "CardID 중복을 오류로 단정", "카드 설명 전문을 generated JSON에 무제한 복제" },
        "variant identity, CardIndex, CardID, EffectClassName source candidate 정책이 GOAL 08 trust boundary와 합쳐진다."),
    new(
        6,
        "PARITY-001",
        "Validation",
        "AfterFoundation",
        "full-card parity evidence NotRun 해소 계획",
        "full-card-parity-evidence",
        "Blocked",
        0,
        GetInt(trustAuditSummaryJson.RootElement.GetProperty("parity"), "sourceEffectCount"),
        "GOAL 08에서 full-card parity evidence는 NotRun 3,918건이다.",
        new[]
        {
            $"notRunSourceEffectCount={GetInt(trustAuditSummaryJson.RootElement.GetProperty("parity"), "notRunSourceEffectCount")}",
            $"passedSourceEffectCount={GetInt(trustAuditSummaryJson.RootElement.GetProperty("parity"), "passedSourceEffectCount")}"
        },
        new[] { "NotRun을 pass로 간주", "synthetic fixture만으로 Unity parity 주장" },
        "source-locked Unity fixture/RL fixture/comparison report 생성 정책과 실행 기준이 정의된다."),
    new(
        7,
        "GIT-001",
        "RepositoryHygiene",
        largeGeneratedFiles.Count > 0 ? "Immediate" : "Done",
        "100MB 초과 generated raw index 업로드 정책",
        "large-generated-artifacts",
        largeGeneratedFiles.Count > 0 ? "BlockedForPush" : "Pass",
        0,
        0,
        "GitHub 업로드 전에 GOAL 04/05 raw generated artifact 처리 정책이 필요하다.",
        largeGeneratedFiles.Select(file => $"{file.RelativePath}={file.SizeBytes} bytes").ToArray(),
        new[] { "100MB 초과 파일을 일반 git blob으로 push", "기존 tracked 대용량 파일을 .gitignore만 추가하고 방치" },
        "대용량 raw index가 git tracking에서 제거되거나 LFS/외부 보관 정책으로 명시 처리된다."),
    new(
        8,
        "DEFER-CARDPORTING",
        "Deferred",
        "Deferred",
        "C0039 이후 card-porting batch",
        "card-porting",
        "ProhibitedUntilOpenCodeReady",
        0,
        0,
        "OpenCodeReady=false이며 failed gate가 3개 남아 있다.",
        new[]
        {
            $"openCodeReady={GetBool(gateSummary, "openCodeReady")}",
            $"failedGateCount={GetInt(gateSummary, "failedGateCount")}",
            $"c0039Executable={GetBool(schedulerSummary, "c0039Executable")}"
        },
        new[] { "C0039 이후 batch 실행", "개별 CardEffect body 추가 구현" },
        "OpenCodeReady=true, failed gate 0, 사용자 명시 승인 후에만 재개한다."),
    new(
        9,
        "DEFER-RL",
        "Deferred",
        "Deferred",
        "RL Environment/Observation/Reward/Dataset/Trainer",
        "rl-components",
        "ProhibitedUntilEngineComplete",
        0,
        0,
        "Foundation Gate 전 RL 학습 구성요소 구현은 금지되어 있고 GOAL 08 정적 감사에서 premature hit는 0이다.",
        new[]
        {
            $"prematureRlComponentHitCount={GetInt(trustTotals, "prematureRlComponentHitCount")}",
            $"openCodeReady={GetBool(gateSummary, "openCodeReady")}"
        },
        new[] { "RL Environment 구현", "Observation/Reward/Dataset/Trainer 구현" },
        "엔진 foundation completion과 parity/invariant evidence 이후 별도 목표로 시작한다.")
};

var topCapabilityBlockers = capabilityBlockers.Take(25).ToList();
var summary = new ImplementationPrioritySummaryDocument(
    "dcgo.as-is-implementation-priority-summary.v1",
    generatedAtUtc,
    "GOAL 09 AS-IS Implementation Priority Rewrite",
    asisRoot,
    new PriorityRewriteInputs(
        headlessSummaryPath,
        headlessSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        trustAuditSummaryPath,
        trustAuditSummaryJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        cardDataStructurePath,
        cardDataStructureJson.RootElement.GetProperty("generatedAtUtc").GetString() ?? "",
        foundationGatePath,
        foundationGateJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        mechanicSchedulerPath,
        mechanicSchedulerJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        capabilityRegistryPath,
        capabilityRegistryJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        sourceRequiredCapabilitiesPath,
        sourceRequiredCapabilitiesJson.RootElement.GetProperty("schemaVersion").GetString() ?? "",
        fullCardPoolBaselinePath,
        fullCardPoolBaselineJson.RootElement.GetProperty("schemaVersion").GetString() ?? ""),
    new PriorityPolicy(
        "GOAL 09는 구현을 수행하지 않고 우선순위 기준선만 재작성한다. 기존 구현/문서는 historical reference이며 SourceOfTruth, GOAL 07 matrix, GOAL 08 trust audit, Foundation Gate를 우선 증거로 사용한다.",
        new[]
        {
            "OpenCodeReady=false 동안 card-porting batch와 RL 학습 구성요소는 Deferred로 둔다.",
            "Required 파일 수보다 Foundation Gate blocker 해소를 우선한다.",
            "local src 구현은 reuse candidate이지 completion evidence가 아니다.",
            "Generated status/registry는 수동 승격하지 않는다.",
            "대용량 generated raw index는 push 전에 repository hygiene 항목으로 처리한다."
        }),
    new PriorityTotals(
        priorityItems.Count,
        priorityItems.Count(item => item.Status == "Immediate"),
        priorityItems.Count(item => item.Status == "ReviewNext"),
        priorityItems.Count(item => item.Status == "AfterFoundation"),
        priorityItems.Count(item => item.Status == "Deferred"),
        topCapabilityBlockers.Count,
        largeGeneratedFiles.Count,
        GetInt(gateSummary, "failedGateCount"),
        GetInt(gateSummary, "unknownCommonApiCount"),
        GetInt(gateSummary, "unsupportedCapabilityCount"),
        GetInt(gateSummary, "partiallyImplementedCapabilityCount")),
    priorityItems,
    topCapabilityBlockers,
    largeGeneratedFiles,
    new[]
    {
        "다음 실제 작업은 FND-001 ContinuousOrStaticEffect partial closure를 더 작은 source-aligned foundation task로 쪼개는 것이다.",
        "FND-002/FND-003은 Foundation Gate 실패 수치를 직접 줄이는 작업이며 GOAL 09 기준 최우선이다.",
        "DATA-001은 구현 전 데이터 정책 감사로, GOAL 06 anomaly와 GOAL 07 Required CardData를 묶는다.",
        "TRUST-001은 기존 src 구현을 재사용할지 버릴지 결정하기 전에 SourceOfTruth mapping과 generated truth를 맞추는 작업이다.",
        "GIT-001은 GitHub 업로드 전 별도 처리해야 한다."
    });

var detail = new ImplementationPriorityDetailDocument(
    "dcgo.as-is-implementation-priority-rewrite.v1",
    generatedAtUtc,
    summary.Goal,
    asisRoot,
    summary,
    capabilityBlockers);

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
    priorityItemCount = summary.Totals.PriorityItemCount,
    immediateItemCount = summary.Totals.ImmediateItemCount,
    deferredItemCount = summary.Totals.DeferredItemCount,
    topCapabilityBlockerCount = summary.Totals.TopCapabilityBlockerCount,
    largeGeneratedFileCount = summary.Totals.LargeGeneratedFileCount,
    nextPriority = summary.PriorityItems.First().Id,
    outputFiles = new[] { detailMarkdownPath, summaryMarkdownPath, detailJsonPath, summaryJsonPath }
}, jsonOptions));

CapabilityBlocker LoadCapabilityBlocker(JsonElement item)
{
    return new CapabilityBlocker(
        item.GetProperty("capabilityId").GetString() ?? "",
        item.GetProperty("status").GetString() ?? "",
        GetInt(item, "affectedCardCount"),
        GetInt(item, "sourceEffectCount"),
        GetInt(item, "cardBatchCount"),
        GetStringArray(item, "reopenCardBatchIds").Take(50).ToArray(),
        GetStringArray(item, "sourceEffectClassNames").Take(50).ToArray());
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

static void WriteUtf8(string path, string contents)
{
    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
    File.WriteAllText(path, contents, new UTF8Encoding(false));
}

static string BuildSummaryMarkdown(ImplementationPrioritySummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# AS-IS Implementation Priority Rewrite Summary");
    builder.AppendLine();
    builder.AppendLine($"- 생성 시각(UTC): `{summary.GeneratedAtUtc}`");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- 우선순위 항목 수: {summary.Totals.PriorityItemCount:N0}");
    builder.AppendLine($"- Immediate: {summary.Totals.ImmediateItemCount:N0}");
    builder.AppendLine($"- ReviewNext: {summary.Totals.ReviewNextItemCount:N0}");
    builder.AppendLine($"- AfterFoundation: {summary.Totals.AfterFoundationItemCount:N0}");
    builder.AppendLine($"- Deferred: {summary.Totals.DeferredItemCount:N0}");
    builder.AppendLine($"- 100MB 초과 generated 파일: {summary.Totals.LargeGeneratedFileCount:N0}");
    builder.AppendLine();
    builder.AppendLine("## 우선순위");
    AppendPriorityTable(builder, summary.PriorityItems);
    builder.AppendLine("## 다음 작업");
    foreach (var recommendation in summary.NextActionRecommendations)
    {
        builder.AppendLine($"- {recommendation}");
    }
    return builder.ToString();
}

static string BuildDetailMarkdown(ImplementationPrioritySummaryDocument summary)
{
    var builder = new StringBuilder();
    builder.AppendLine("# GOAL 09 AS-IS Implementation Priority Rewrite");
    builder.AppendLine();
    builder.AppendLine("이번 문서는 GOAL 01-08 기준선을 바탕으로 개발 재시작용 구현 우선순위를 다시 작성한 결과다.");
    builder.AppendLine("구현, CardEffect body 추가, C0039 이후 card-porting, generated status 승격, RL 학습 구성요소 구현은 수행하지 않았다.");
    builder.AppendLine();
    builder.AppendLine("## 입력");
    builder.AppendLine($"- AS-IS root: `{summary.AsisRoot}`");
    builder.AppendLine($"- GOAL 07 summary: `{summary.Inputs.HeadlessRequirementSummaryPath}`");
    builder.AppendLine($"- GOAL 08 trust audit: `{summary.Inputs.HeadlessTrustAuditSummaryPath}`");
    builder.AppendLine($"- Foundation gate: `{summary.Inputs.FoundationGatePath}`");
    builder.AppendLine($"- Mechanic-first scheduler: `{summary.Inputs.MechanicSchedulerPath}`");
    builder.AppendLine($"- Source-required capabilities: `{summary.Inputs.SourceRequiredCapabilitiesPath}`");
    builder.AppendLine();
    builder.AppendLine("## 정책");
    builder.AppendLine(summary.Policy.EvidenceScope);
    foreach (var rule in summary.Policy.Rules)
    {
        builder.AppendLine($"- {rule}");
    }
    builder.AppendLine();
    builder.AppendLine("## 전체 요약");
    AppendTable(builder, new[] { "항목", "값" }, new[]
    {
        new[] { "Priority items", summary.Totals.PriorityItemCount.ToString("N0") },
        new[] { "Immediate", summary.Totals.ImmediateItemCount.ToString("N0") },
        new[] { "ReviewNext", summary.Totals.ReviewNextItemCount.ToString("N0") },
        new[] { "AfterFoundation", summary.Totals.AfterFoundationItemCount.ToString("N0") },
        new[] { "Deferred", summary.Totals.DeferredItemCount.ToString("N0") },
        new[] { "Failed gates", summary.Totals.FailedGateCount.ToString("N0") },
        new[] { "Unknown common API", summary.Totals.UnknownCommonApiCount.ToString("N0") },
        new[] { "Unsupported capability", summary.Totals.UnsupportedCapabilityCount.ToString("N0") },
        new[] { "PartiallyImplemented capability", summary.Totals.PartiallyImplementedCapabilityCount.ToString("N0") },
        new[] { "Large generated files", summary.Totals.LargeGeneratedFileCount.ToString("N0") }
    });
    builder.AppendLine("## 우선순위 항목");
    AppendPriorityTable(builder, summary.PriorityItems);
    builder.AppendLine("## Top capability blockers");
    AppendTable(builder, new[] { "Capability", "Status", "Affected cards", "Source effects", "Card batches" },
        summary.TopCapabilityBlockers.Select(blocker => new[]
        {
            blocker.CapabilityId,
            blocker.Status,
            blocker.AffectedCardCount.ToString("N0"),
            blocker.SourceEffectCount.ToString("N0"),
            blocker.CardBatchCount.ToString("N0")
        }));
    builder.AppendLine("## 100MB 초과 generated 파일");
    AppendTable(builder, new[] { "경로", "크기(bytes)", "처리 필요" },
        summary.LargeGeneratedFiles.Select(file => new[] { file.RelativePath, file.SizeBytes.ToString("N0"), file.Reason }));
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

static void AppendPriorityTable(StringBuilder builder, IEnumerable<PriorityItem> items)
{
    AppendTable(builder, new[] { "Rank", "ID", "Lane", "Status", "Title", "Completion evidence" },
        items.Select(item => new[]
        {
            item.Rank.ToString(),
            item.Id,
            item.Lane,
            item.Status,
            item.Title,
            item.CompletionEvidence
        }));
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

record PriorityRewriteInputs(
    string HeadlessRequirementSummaryPath,
    string HeadlessRequirementSummaryGeneratedAtUtc,
    string HeadlessTrustAuditSummaryPath,
    string HeadlessTrustAuditSummaryGeneratedAtUtc,
    string CardDataStructurePath,
    string CardDataStructureGeneratedAtUtc,
    string FoundationGatePath,
    string FoundationGateSchemaVersion,
    string MechanicSchedulerPath,
    string MechanicSchedulerSchemaVersion,
    string CapabilityRegistryPath,
    string CapabilityRegistrySchemaVersion,
    string SourceRequiredCapabilitiesPath,
    string SourceRequiredCapabilitiesSchemaVersion,
    string FullCardPoolBaselinePath,
    string FullCardPoolBaselineSchemaVersion);

record PriorityPolicy(string EvidenceScope, string[] Rules);

record PriorityTotals(
    int PriorityItemCount,
    int ImmediateItemCount,
    int ReviewNextItemCount,
    int AfterFoundationItemCount,
    int DeferredItemCount,
    int TopCapabilityBlockerCount,
    int LargeGeneratedFileCount,
    int FailedGateCount,
    int UnknownCommonApiCount,
    int UnsupportedCapabilityCount,
    int PartiallyImplementedCapabilityCount);

record PriorityItem(
    int Rank,
    string Id,
    string Lane,
    string Status,
    string Title,
    string CapabilityOrScope,
    string CurrentEvidenceStatus,
    int AffectedCardCount,
    int SourceEffectCount,
    string Rationale,
    string[] Evidence,
    string[] ProhibitedActions,
    string CompletionEvidence);

record CapabilityBlocker(
    string CapabilityId,
    string Status,
    int AffectedCardCount,
    int SourceEffectCount,
    int CardBatchCount,
    string[] ReopenCardBatchIds,
    string[] SourceEffectClassNames);

record LargeGeneratedFileRecord(string RelativePath, long SizeBytes, string Reason);

record ImplementationPrioritySummaryDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    PriorityRewriteInputs Inputs,
    PriorityPolicy Policy,
    PriorityTotals Totals,
    List<PriorityItem> PriorityItems,
    List<CapabilityBlocker> TopCapabilityBlockers,
    List<LargeGeneratedFileRecord> LargeGeneratedFiles,
    string[] NextActionRecommendations);

record ImplementationPriorityDetailDocument(
    string SchemaVersion,
    string GeneratedAtUtc,
    string Goal,
    string AsisRoot,
    ImplementationPrioritySummaryDocument Summary,
    List<CapabilityBlocker> CapabilityBlockers);
