using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public enum CompletionGateStatus
{
    Passed,
    Failed,
    NotRun,
}

public sealed record CompletionGateResult(
    string Id,
    string Name,
    CompletionGateStatus Status,
    string Details)
{
    public bool Passed => Status == CompletionGateStatus.Passed;
}

public sealed record ScenarioSuiteReport(IReadOnlyList<ScenarioSuiteCaseResult> Results)
{
    public bool Passed => Results.All(result => result.Passed);
}

public sealed record ScenarioSuiteCaseResult(
    string Name,
    bool Passed,
    string Details,
    ScenarioResult? Result = null);

public sealed record ReplayDeterminismSuiteReport(IReadOnlyList<ReplayDeterminismCaseResult> Results)
{
    public bool Passed => Results.All(result => result.Passed);
}

public sealed record ReplayDeterminismCaseResult(
    string Name,
    bool Passed,
    string Details,
    ReplayDeterminismResult? Result = null);

public sealed record InvariantFuzzReport(IReadOnlyList<InvariantFuzzCaseResult> Results)
{
    public bool Passed => Results.All(result => result.Passed);
}

public sealed record InvariantFuzzCaseResult(
    string Name,
    bool Passed,
    string Details,
    ScenarioResult? Result = null);

public sealed record UnsupportedMechanicZeroCheckReport(
    bool Passed,
    IReadOnlyList<CardPoolCardStatus> UnsupportedCards,
    IReadOnlyList<string> DeckValidationErrors,
    IReadOnlyList<string> MissingLayers);

public sealed record EngineCompletionRequest(
    string TargetName,
    TargetCardPoolValidationRequest TargetCardPool,
    IReadOnlyList<ScriptedScenario> GoldenScenarios,
    IReadOnlyList<RandomLegalActionRunRequest> InvariantFuzzRuns,
    IReadOnlyList<ScriptedScenario>? ReplayScenarios = null);

public sealed record EngineCompletionReport(
    string TargetName,
    IReadOnlyList<CompletionGateResult> Gates,
    TargetCardPoolValidationReport TargetCardPool,
    ScenarioSuiteReport ScenarioSuite,
    ReplayDeterminismSuiteReport ReplayDeterminism,
    InvariantFuzzReport InvariantFuzz,
    UnsupportedMechanicZeroCheckReport UnsupportedMechanicZeroCheck)
{
    public bool IsComplete => Gates.All(gate => gate.Status == CompletionGateStatus.Passed);
    public IReadOnlyList<CompletionGateResult> FailedGates =>
        Gates.Where(gate => gate.Status == CompletionGateStatus.Failed).ToArray();
}

public sealed class ScenarioSuiteRunner
{
    private readonly ScriptedScenarioRunner _scenarioRunner;

    public ScenarioSuiteRunner(ScriptedScenarioRunner? scenarioRunner = null)
    {
        _scenarioRunner = scenarioRunner
            ?? throw new DomainException("ScenarioSuiteRunner requires a ScriptedScenarioRunner from BattleEngineServices.");
    }

    public ScenarioSuiteReport Run(IReadOnlyList<ScriptedScenario> scenarios)
    {
        var results = new List<ScenarioSuiteCaseResult>();
        foreach (var scenario in scenarios)
        {
            try
            {
                var result = _scenarioRunner.Run(scenario);
                var passed = result.Status is ScenarioRunStatus.Completed or ScenarioRunStatus.GameOver
                    && result.InvariantReports.All(report => report.IsValid);
                results.Add(new ScenarioSuiteCaseResult(
                    scenario.Name,
                    passed,
                    passed ? "Scenario completed with valid invariants." : $"Scenario status '{result.Status}' or invalid invariant.",
                    result));
            }
            catch (Exception exception) when (exception is DomainException or InvalidOperationException)
            {
                results.Add(new ScenarioSuiteCaseResult(scenario.Name, Passed: false, exception.Message));
            }
        }

        return new ScenarioSuiteReport(results);
    }
}

public sealed class ReplayDeterminismRunner
{
    private readonly ScriptedScenarioRunner _scenarioRunner;
    private readonly ReplayDeterminismHelper _replayDeterminismHelper;

    public ReplayDeterminismRunner(
        ScriptedScenarioRunner? scenarioRunner = null,
        ReplayDeterminismHelper? replayDeterminismHelper = null)
    {
        _scenarioRunner = scenarioRunner
            ?? throw new DomainException("ReplayDeterminismRunner requires a ScriptedScenarioRunner from BattleEngineServices.");
        _replayDeterminismHelper = replayDeterminismHelper
            ?? throw new DomainException("ReplayDeterminismRunner requires a ReplayDeterminismHelper from BattleEngineServices.");
    }

    public ReplayDeterminismSuiteReport Run(IReadOnlyList<ScriptedScenario> scenarios)
    {
        var results = new List<ReplayDeterminismCaseResult>();
        foreach (var scenario in scenarios)
        {
            try
            {
                var scenarioResult = _scenarioRunner.Run(scenario);
                var replay = _replayDeterminismHelper.ReplayAndCompare(
                    scenario.InitialState,
                    scenarioResult.Trace,
                    scenarioResult.FinalStateHash);
                results.Add(new ReplayDeterminismCaseResult(
                    scenario.Name,
                    replay.Matches,
                    replay.Matches
                        ? "Replay final hash matches."
                        : $"Replay mismatch expected '{replay.ExpectedFinalStateHash}', actual '{replay.ActualFinalStateHash}'.",
                    replay));
            }
            catch (Exception exception) when (exception is DomainException or InvalidOperationException)
            {
                results.Add(new ReplayDeterminismCaseResult(scenario.Name, Passed: false, exception.Message));
            }
        }

        return new ReplayDeterminismSuiteReport(results);
    }
}

public sealed class InvariantFuzzRunner
{
    private readonly RandomLegalActionRunner _randomLegalActionRunner;

    public InvariantFuzzRunner(RandomLegalActionRunner? randomLegalActionRunner = null)
    {
        _randomLegalActionRunner = randomLegalActionRunner
            ?? throw new DomainException("InvariantFuzzRunner requires a RandomLegalActionRunner from BattleEngineServices.");
    }

    public InvariantFuzzReport Run(IReadOnlyList<RandomLegalActionRunRequest> requests)
    {
        var results = new List<InvariantFuzzCaseResult>();
        foreach (var request in requests)
        {
            try
            {
                var result = _randomLegalActionRunner.Run(request);
                var passed = result.InvariantReports.All(report => report.IsValid)
                    && result.Status is ScenarioRunStatus.Completed or ScenarioRunStatus.GameOver or ScenarioRunStatus.MaxTurnAbort;
                results.Add(new InvariantFuzzCaseResult(
                    request.Name,
                    passed,
                    passed ? "Fuzz run kept invariants valid." : $"Fuzz run ended with status '{result.Status}' or invalid invariant.",
                    result));
            }
            catch (Exception exception) when (exception is DomainException or UnsupportedMechanicException or InvalidOperationException)
            {
                results.Add(new InvariantFuzzCaseResult(request.Name, Passed: false, exception.Message));
            }
        }

        return new InvariantFuzzReport(results);
    }
}

public sealed class UnsupportedMechanicZeroCheck
{
    public UnsupportedMechanicZeroCheckReport Check(TargetCardPoolValidationReport report)
    {
        var unsupportedCards = report.UnsupportedCards;
        var deckErrors = report.DeckValidation.Errors;
        var missingLayers = report.MissingLayers;

        return new UnsupportedMechanicZeroCheckReport(
            unsupportedCards.Count == 0 && deckErrors.Count == 0 && missingLayers.Count == 0,
            unsupportedCards,
            deckErrors,
            missingLayers);
    }
}

public sealed class EngineCompletionChecklistRunner
{
    private static readonly string[] BannedAssemblyNames =
    {
        "UnityEngine",
        "Photon",
        "Unity.",
        "UnityEditor",
    };

    private readonly TargetCardPoolValidator _targetCardPoolValidator;
    private readonly ScenarioSuiteRunner? _scenarioSuiteRunner;
    private readonly ReplayDeterminismRunner? _replayDeterminismRunner;
    private readonly InvariantFuzzRunner? _invariantFuzzRunner;
    private readonly UnsupportedMechanicZeroCheck _unsupportedMechanicZeroCheck;

    public EngineCompletionChecklistRunner(
        TargetCardPoolValidator? targetCardPoolValidator = null,
        ScenarioSuiteRunner? scenarioSuiteRunner = null,
        ReplayDeterminismRunner? replayDeterminismRunner = null,
        InvariantFuzzRunner? invariantFuzzRunner = null,
        UnsupportedMechanicZeroCheck? unsupportedMechanicZeroCheck = null)
    {
        _targetCardPoolValidator = targetCardPoolValidator ?? new TargetCardPoolValidator();
        _scenarioSuiteRunner = scenarioSuiteRunner;
        _replayDeterminismRunner = replayDeterminismRunner;
        _invariantFuzzRunner = invariantFuzzRunner;
        _unsupportedMechanicZeroCheck = unsupportedMechanicZeroCheck ?? new UnsupportedMechanicZeroCheck();
    }

    public EngineCompletionReport Run(EngineCompletionRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var dependencyErrors = CheckForbiddenDependencies();
        var cardPoolReport = _targetCardPoolValidator.Validate(request.TargetCardPool);
        var unsupportedZeroCheck = _unsupportedMechanicZeroCheck.Check(cardPoolReport);
        var runtimeServices = BattleEngineServices.Create(request.TargetCardPool.CardScriptRegistry);
        var scenarioSuiteRunner = _scenarioSuiteRunner ?? CreateScenarioSuiteRunner(runtimeServices);
        var replayDeterminismRunner = _replayDeterminismRunner ?? CreateReplayDeterminismRunner(runtimeServices);
        var invariantFuzzRunner = _invariantFuzzRunner ?? CreateInvariantFuzzRunner(runtimeServices);
        var scenarioSuite = scenarioSuiteRunner.Run(request.GoldenScenarios);
        var replayDeterminism = replayDeterminismRunner.Run(request.ReplayScenarios ?? request.GoldenScenarios);
        var invariantFuzz = invariantFuzzRunner.Run(request.InvariantFuzzRuns);

        var gates = new List<CompletionGateResult>
        {
            Gate(
                "forbidden-dependencies",
                "RL.Engine forbidden dependency check",
                dependencyErrors.Count == 0,
                dependencyErrors.Count == 0 ? "No Unity/Photon assembly references found." : string.Join(" | ", dependencyErrors)),
            Gate(
                "target-card-pool-documented",
                "Target card pool CardEffect status documented",
                cardPoolReport.CardStatuses.All(status => status.HasRegisteredScript),
                DescribeMissingScripts(cardPoolReport)),
            Gate(
                "target-deck-validation",
                "Target decklist validates without unsupported mechanics",
                cardPoolReport.DeckValidation.IsValid,
                cardPoolReport.DeckValidation.IsValid
                    ? "Target deck validation passed."
                    : string.Join(" | ", cardPoolReport.DeckValidation.Errors)),
            Gate(
                "unsupported-mechanic-zero",
                "Unsupported mechanic zero check",
                unsupportedZeroCheck.Passed,
                unsupportedZeroCheck.Passed
                    ? "No unsupported target card mechanics remain."
                    : DescribeUnsupported(unsupportedZeroCheck)),
            Gate(
                "golden-scenario-suite",
                "Golden scripted scenarios",
                scenarioSuite.Passed,
                DescribeScenarioSuite(scenarioSuite)),
            Gate(
                "replay-determinism",
                "Replay determinism",
                replayDeterminism.Passed,
                DescribeReplay(replayDeterminism)),
            Gate(
                "invariant-fuzz",
                "Invariant fuzz",
                invariantFuzz.Passed,
                DescribeFuzz(invariantFuzz)),
        };

        return new EngineCompletionReport(
            request.TargetName,
            gates,
            cardPoolReport,
            scenarioSuite,
            replayDeterminism,
            invariantFuzz,
            unsupportedZeroCheck);
    }

    private static IReadOnlyList<string> CheckForbiddenDependencies()
    {
        var references = typeof(GameState)
            .Assembly
            .GetReferencedAssemblies()
            .Select(assembly => assembly.Name ?? string.Empty)
            .ToArray();

        return BannedAssemblyNames
            .Where(bannedName => references.Any(reference => reference.Contains(bannedName, StringComparison.OrdinalIgnoreCase)))
            .Select(bannedName => $"Forbidden assembly reference found: {bannedName}")
            .ToArray();
    }

    private static CompletionGateResult Gate(string id, string name, bool passed, string details) =>
        new(id, name, passed ? CompletionGateStatus.Passed : CompletionGateStatus.Failed, details);

    private static ScenarioSuiteRunner CreateScenarioSuiteRunner(BattleEngineServices services) =>
        new(new ScriptedScenarioRunner(
            actionExecutor: services.ActionExecutor,
            phaseRunner: services.PhaseRunner,
            turnRunner: services.TurnRunner));

    private static ReplayDeterminismRunner CreateReplayDeterminismRunner(BattleEngineServices services) =>
        new(
            new ScriptedScenarioRunner(
                actionExecutor: services.ActionExecutor,
                phaseRunner: services.PhaseRunner,
                turnRunner: services.TurnRunner),
            new ReplayDeterminismHelper(new ReplayRunner(actionExecutor: services.ActionExecutor)));

    private static InvariantFuzzRunner CreateInvariantFuzzRunner(BattleEngineServices services) =>
        new(new RandomLegalActionRunner(
            actionExecutor: services.ActionExecutor,
            phaseRunner: services.PhaseRunner,
            turnRunner: services.TurnRunner));

    private static string DescribeMissingScripts(TargetCardPoolValidationReport report)
    {
        var missing = report.MissingScripts;
        return missing.Count == 0
            ? "All target cards have explicit card script records."
            : string.Join(", ", missing.Select(status => status.CardId));
    }

    private static string DescribeUnsupported(UnsupportedMechanicZeroCheckReport report)
    {
        var cards = report.UnsupportedCards.Select(status => $"{status.CardId}:{status.Notes}");
        var layers = report.MissingLayers.Select(layer => $"missing-layer:{layer}");
        var errors = report.DeckValidationErrors.Select(error => $"deck:{error}");
        return string.Join(" | ", cards.Concat(layers).Concat(errors));
    }

    private static string DescribeScenarioSuite(ScenarioSuiteReport report)
    {
        var failures = report.Results.Where(result => !result.Passed).ToArray();
        return failures.Length == 0
            ? $"All {report.Results.Count} golden scenarios passed."
            : string.Join(" | ", failures.Select(result => $"{result.Name}:{result.Details}"));
    }

    private static string DescribeReplay(ReplayDeterminismSuiteReport report)
    {
        var failures = report.Results.Where(result => !result.Passed).ToArray();
        return failures.Length == 0
            ? $"All {report.Results.Count} replay checks passed."
            : string.Join(" | ", failures.Select(result => $"{result.Name}:{result.Details}"));
    }

    private static string DescribeFuzz(InvariantFuzzReport report)
    {
        var failures = report.Results.Where(result => !result.Passed).ToArray();
        return failures.Length == 0
            ? $"All {report.Results.Count} invariant fuzz runs passed."
            : string.Join(" | ", failures.Select(result => $"{result.Name}:{result.Details}"));
    }
}
