using DCGO.RL.Engine.Battle;

namespace DCGO.RL.Engine.Validation;

public enum EngineCoreMilestoneDecision
{
    ReadyForFullCardPoolInventory,
    BlockedByCoreParity,
    BlockedBySourceData,
    NeedsReview,
}

public enum EngineCoreMilestoneGateStatus
{
    Passed,
    Failed,
    NeedsReview,
    NotRun,
}

public sealed record EngineCoreMilestoneEvidence(
    string Id,
    string Name,
    EngineCoreMilestoneGateStatus Status,
    string Details,
    IReadOnlyList<string> Evidence);

public sealed record EngineCoreMilestoneFinding(
    string Id,
    EngineCoreMilestoneGateStatus Status,
    string Details,
    IReadOnlyList<string> Evidence,
    bool BlocksFullCardPoolInventory,
    bool BlocksVariantImplementation,
    bool BlocksRlEnvironmentDesign);

public sealed record EngineCoreMilestoneGateRequest(
    string TargetName,
    BattleEngineServiceGraphValidationReport ServiceGraph,
    TargetCardPoolValidationReport TargetCardPool,
    AssetRegistryMappingReport AssetRegistryMapping,
    IReadOnlyList<EngineCoreMilestoneEvidence> EvidenceChecks);

public sealed record EngineCoreMilestoneGateReport(
    string SchemaVersion,
    string TargetName,
    EngineCoreMilestoneDecision Decision,
    bool CoreParityBlocked,
    bool FullCardPoolInventoryBlocked,
    bool ReadyForRlEnvironmentDesign,
    string Scope,
    IReadOnlyList<EngineCoreMilestoneEvidence> Gates,
    IReadOnlyList<EngineCoreMilestoneFinding> Findings)
{
    public IReadOnlyList<EngineCoreMilestoneEvidence> FailedGates =>
        Gates.Where(gate => gate.Status == EngineCoreMilestoneGateStatus.Failed).ToArray();

    public IReadOnlyList<EngineCoreMilestoneEvidence> NeedsReviewGates =>
        Gates.Where(gate => gate.Status == EngineCoreMilestoneGateStatus.NeedsReview).ToArray();
}

public sealed class EngineCoreMilestoneGateRunner
{
    public const string SchemaVersion = "dcgo.engine-core-milestone.59A.v1";

    public EngineCoreMilestoneGateReport Evaluate(EngineCoreMilestoneGateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var gates = new List<EngineCoreMilestoneEvidence>();
        gates.Add(ServiceGraphGate(request.ServiceGraph));
        gates.Add(TargetCardPoolGate(request.TargetCardPool));
        gates.Add(AssetRegistryGate(request.AssetRegistryMapping));
        gates.AddRange(request.EvidenceChecks);

        var findings = new List<EngineCoreMilestoneFinding>();
        findings.AddRange(CreateAssetRegistryFindings(request.AssetRegistryMapping));
        findings.AddRange(gates
            .Where(gate => gate.Status == EngineCoreMilestoneGateStatus.Failed)
            .Select(gate => new EngineCoreMilestoneFinding(
                gate.Id,
                gate.Status,
                gate.Details,
                gate.Evidence,
                BlocksFullCardPoolInventory: true,
                BlocksVariantImplementation: true,
                BlocksRlEnvironmentDesign: true)));

        var sourceDataBlocked = request.AssetRegistryMapping.SourceStatus != AssetRegistrySourceStatus.Available
            || request.AssetRegistryMapping.Issues.Any(issue => issue.Severity == AssetRegistryMappingSeverity.Error);
        var coreParityBlocked = gates.Any(gate => gate.Status == EngineCoreMilestoneGateStatus.Failed)
            && !sourceDataBlocked;
        var hasNeedsReview = gates.Any(gate => gate.Status == EngineCoreMilestoneGateStatus.NeedsReview)
            || findings.Any(finding => finding.Status == EngineCoreMilestoneGateStatus.NeedsReview);
        var decision = sourceDataBlocked
            ? EngineCoreMilestoneDecision.BlockedBySourceData
            : coreParityBlocked
                ? EngineCoreMilestoneDecision.BlockedByCoreParity
                : hasNeedsReview
                    ? EngineCoreMilestoneDecision.NeedsReview
                    : EngineCoreMilestoneDecision.ReadyForFullCardPoolInventory;

        return new EngineCoreMilestoneGateReport(
            SchemaVersion,
            request.TargetName,
            decision,
            CoreParityBlocked: coreParityBlocked,
            FullCardPoolInventoryBlocked: findings.Any(finding => finding.BlocksFullCardPoolInventory),
            ReadyForRlEnvironmentDesign: false,
            "ST1~ST3 engine-core milestone only. This is not a full DCGO snapshot completion gate and not RL training approval.",
            gates,
            findings);
    }

    private static EngineCoreMilestoneEvidence ServiceGraphGate(BattleEngineServiceGraphValidationReport report) =>
        new(
            "runtime-composition-graph",
            "Production runtime composition graph",
            report.IsValid ? EngineCoreMilestoneGateStatus.Passed : EngineCoreMilestoneGateStatus.Failed,
            report.IsValid
                ? "BattleEngineServices graph validates shared dependencies."
                : string.Join(" | ", report.Issues.Select(issue => $"{issue.ServiceName}.{issue.DependencyName}:{issue.Message}")),
            new[] { "BattleEngineServices.ValidationReport" });

    private static EngineCoreMilestoneEvidence TargetCardPoolGate(TargetCardPoolValidationReport report) =>
        new(
            "st1-st3-target-pool",
            "ST1~ST3 target pool registry/status validation",
            report.IsValid ? EngineCoreMilestoneGateStatus.Passed : EngineCoreMilestoneGateStatus.Failed,
            report.IsValid
                ? $"Target pool '{report.TargetName}' validates with {report.CardStatuses.Count} card records."
                : DescribeTargetCardPoolFailure(report),
            new[] { "TargetCardPoolValidator", report.TargetName });

    private static EngineCoreMilestoneEvidence AssetRegistryGate(AssetRegistryMappingReport report)
    {
        var sourceUnavailable = report.SourceStatus != AssetRegistrySourceStatus.Available;
        var status = report.SourceStatus != AssetRegistrySourceStatus.Available
            || report.Issues.Any(issue => issue.Severity == AssetRegistryMappingSeverity.Error)
                ? EngineCoreMilestoneGateStatus.Failed
                : report.Issues.Any(issue => issue.Severity == AssetRegistryMappingSeverity.NeedsReview)
                    ? EngineCoreMilestoneGateStatus.NeedsReview
                    : EngineCoreMilestoneGateStatus.Passed;
        var relevantIssues = report.Issues
            .Where(issue =>
                issue.Severity is AssetRegistryMappingSeverity.Error or AssetRegistryMappingSeverity.NeedsReview
                || (sourceUnavailable && issue.Code == "SourceUnavailable"))
            .ToArray();
        var details = status == EngineCoreMilestoneGateStatus.Passed
            ? "Asset registry mapping has no blocking finding."
            : string.Join(
                " | ",
                relevantIssues.Select(issue => $"{issue.Code}:{issue.Message}"));

        return new EngineCoreMilestoneEvidence(
            "asset-registry-validator",
            "Variant-aware asset registry mapping",
            status,
            details,
            new[] { "AssetRegistryMappingValidator", report.TargetName });
    }

    private static IReadOnlyList<EngineCoreMilestoneFinding> CreateAssetRegistryFindings(AssetRegistryMappingReport report)
    {
        var findings = new List<EngineCoreMilestoneFinding>();
        foreach (var issue in report.Issues.Where(issue =>
            issue.Severity is AssetRegistryMappingSeverity.Error or AssetRegistryMappingSeverity.NeedsReview
            || (report.SourceStatus != AssetRegistrySourceStatus.Available && issue.Code == "SourceUnavailable")))
        {
            var assetIds = issue.Assets.Select(asset => asset.StableId).ToArray();
            var isSt3Two = issue.Assets.Any(asset =>
                string.Equals(asset.CardId, "ST3-02", StringComparison.Ordinal));
            var blocksFullCardPoolInventory = issue.Severity == AssetRegistryMappingSeverity.Error
                || issue.Code == "SourceUnavailable";
            findings.Add(new EngineCoreMilestoneFinding(
                issue.Code,
                blocksFullCardPoolInventory
                    ? EngineCoreMilestoneGateStatus.Failed
                    : EngineCoreMilestoneGateStatus.NeedsReview,
                issue.Message,
                assetIds,
                BlocksFullCardPoolInventory: blocksFullCardPoolInventory,
                BlocksVariantImplementation: true,
                BlocksRlEnvironmentDesign: true));

            if (isSt3Two && issue.Code == "MissingSourceEffectBody")
            {
                findings.Add(new EngineCoreMilestoneFinding(
                    "ST3-02-P2-source-body-unconfirmed",
                    EngineCoreMilestoneGateStatus.NeedsReview,
                    "ST3-02 base/P1 remain NoEffect candidates, but P2 has CardEffectClassName ST3_02 without source body. Do not guess implementation.",
                    assetIds,
                    BlocksFullCardPoolInventory: false,
                    BlocksVariantImplementation: true,
                    BlocksRlEnvironmentDesign: true));
            }
        }

        return findings;
    }

    private static string DescribeTargetCardPoolFailure(TargetCardPoolValidationReport report)
    {
        var deckErrors = report.DeckValidation.Errors.Select(error => $"deck:{error}");
        var unsupported = report.UnsupportedCards.Select(card => $"unsupported:{card.CardId}");
        var partial = report.PartiallyImplementedCards.Select(card => $"partial:{card.CardId}");
        var missing = report.MissingScripts.Select(card => $"missing-script:{card.CardId}");
        var layers = report.MissingLayers.Select(layer => $"missing-layer:{layer}");
        return string.Join(" | ", deckErrors.Concat(unsupported).Concat(partial).Concat(missing).Concat(layers));
    }
}
