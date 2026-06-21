namespace DCGO.RL.Engine.Validation;

public enum EngineCoreExpansionReadinessDecision
{
    ReadyForFullCardPoolInventory,
    BlockedByCoreParity,
    BlockedByRuntimeDecisionBoundary,
    BlockedBySourceAlignment,
    NeedsReview,
}

public enum EngineCoreExpansionEvidenceCategory
{
    CoreParity,
    RuntimeDecisionBoundary,
    SourceAlignment,
    Determinism,
    RandomLegalActionSmoke,
    GoldenScenario,
    TraceParity,
    RuntimeComposition,
}

public enum EngineCoreExpansionEvidenceStatus
{
    Passed,
    Failed,
    NeedsReview,
    NotRun,
}

public sealed record EngineCoreExpansionEvidence(
    string Id,
    EngineCoreExpansionEvidenceCategory Category,
    EngineCoreExpansionEvidenceStatus Status,
    string Details,
    IReadOnlyList<string> Evidence);

public sealed record EngineCoreExpansionReadinessRequest(
    string TargetName,
    EngineCoreMilestoneGateReport EngineCoreGate,
    IReadOnlyList<EngineCoreExpansionEvidence> Evidence,
    bool UnitySourceUnchanged);

public sealed record EngineCoreExpansionReadinessReport(
    string SchemaVersion,
    string TargetName,
    EngineCoreExpansionReadinessDecision Decision,
    bool ReadyForFullCardPoolInventory,
    bool ReadyForRlEnvironmentDesign,
    string RecommendedNextQueue,
    string Scope,
    IReadOnlyList<EngineCoreExpansionEvidence> Evidence,
    IReadOnlyList<EngineCoreMilestoneFinding> CarryForwardFindings)
{
    public IReadOnlyList<EngineCoreExpansionEvidence> FailedEvidence =>
        Evidence.Where(item => item.Status == EngineCoreExpansionEvidenceStatus.Failed).ToArray();

    public IReadOnlyList<EngineCoreExpansionEvidence> NotRunEvidence =>
        Evidence.Where(item => item.Status == EngineCoreExpansionEvidenceStatus.NotRun).ToArray();
}

public sealed class EngineCoreExpansionReadinessRunner
{
    public const string SchemaVersion = "dcgo.engine-core-expansion-readiness.60A.v1";

    public EngineCoreExpansionReadinessReport Evaluate(EngineCoreExpansionReadinessRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var evidence = request.Evidence.ToArray();
        var decision = Decide(request, evidence);

        return new EngineCoreExpansionReadinessReport(
            SchemaVersion,
            request.TargetName,
            decision,
            ReadyForFullCardPoolInventory: decision == EngineCoreExpansionReadinessDecision.ReadyForFullCardPoolInventory,
            ReadyForRlEnvironmentDesign: false,
            RecommendedNextQueue: decision == EngineCoreExpansionReadinessDecision.ReadyForFullCardPoolInventory
                ? "61_dcgo_source_snapshot_pin, or skip to next unfinished queue if source snapshot is already approved."
                : "Create a concrete remediation prompt for the blocking category before full-card-pool inventory.",
            "60A only reviews readiness for full DCGO snapshot inventory. It is not a full snapshot completion gate and not RL environment approval.",
            evidence,
            request.EngineCoreGate.Findings);
    }

    private static EngineCoreExpansionReadinessDecision Decide(
        EngineCoreExpansionReadinessRequest request,
        IReadOnlyList<EngineCoreExpansionEvidence> evidence)
    {
        if (!request.UnitySourceUnchanged
            || request.EngineCoreGate.FullCardPoolInventoryBlocked
            || evidence.Any(item =>
                item.Status == EngineCoreExpansionEvidenceStatus.Failed
                && item.Category == EngineCoreExpansionEvidenceCategory.SourceAlignment))
        {
            return EngineCoreExpansionReadinessDecision.BlockedBySourceAlignment;
        }

        if (request.EngineCoreGate.CoreParityBlocked
            || evidence.Any(item =>
                item.Status == EngineCoreExpansionEvidenceStatus.Failed
                && item.Category == EngineCoreExpansionEvidenceCategory.CoreParity))
        {
            return EngineCoreExpansionReadinessDecision.BlockedByCoreParity;
        }

        if (evidence.Any(item =>
            item.Status == EngineCoreExpansionEvidenceStatus.Failed
            && item.Category == EngineCoreExpansionEvidenceCategory.RuntimeDecisionBoundary))
        {
            return EngineCoreExpansionReadinessDecision.BlockedByRuntimeDecisionBoundary;
        }

        if (evidence.Any(item => item.Status == EngineCoreExpansionEvidenceStatus.NotRun))
        {
            return EngineCoreExpansionReadinessDecision.NeedsReview;
        }

        return EngineCoreExpansionReadinessDecision.ReadyForFullCardPoolInventory;
    }
}
