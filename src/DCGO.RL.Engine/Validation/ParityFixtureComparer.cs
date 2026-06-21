using System.Text.Json;
using System.Text.Json.Serialization;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public enum ParityFixtureComparisonStatus
{
    Passed,
    Failed,
    NotRun,
}

public enum ParityFixtureDifferenceCategory
{
    Schema,
    EventAlignment,
    Timing,
    MemoryPhase,
    Zone,
    Permanent,
    Modifier,
    DecisionSelection,
    Trigger,
    HiddenInformation,
}

public sealed record ParityFixtureDifference(
    int StepIndex,
    ParityFixtureDifferenceCategory Category,
    string Path,
    string UnityValue,
    string RlValue,
    string Message);

public sealed record ParityFixtureComparisonResult(
    string ScenarioId,
    ParityFixtureComparisonStatus Status,
    string Summary,
    int UnityEventCount,
    int RlEventCount,
    ParityFixtureDifference? FirstMismatch,
    IReadOnlyList<ParityFixtureDifference> Differences)
{
    public bool IsMatch => Status == ParityFixtureComparisonStatus.Passed;
}

public enum ParityFixtureLoadStatus
{
    Loaded,
    Failed,
    NotRun,
}

public sealed record ParityFixtureLoadResult(
    ParityFixtureLoadStatus Status,
    ParityTraceDocument? Document,
    string? Error)
{
    public static ParityFixtureLoadResult Loaded(ParityTraceDocument document) =>
        new(ParityFixtureLoadStatus.Loaded, document, null);

    public static ParityFixtureLoadResult Failed(string error) =>
        new(ParityFixtureLoadStatus.Failed, null, error);

    public static ParityFixtureLoadResult NotRun(string error) =>
        new(ParityFixtureLoadStatus.NotRun, null, error);
}

public sealed class ParityFixtureLoader
{
    private readonly ParityTraceStore _store;

    public ParityFixtureLoader(ParityTraceStore? store = null)
    {
        _store = store ?? new ParityTraceStore();
    }

    public ParityFixtureLoadResult Load(string fixturePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fixturePath);

        if (!File.Exists(fixturePath))
        {
            return ParityFixtureLoadResult.NotRun("Unity parity fixture file is missing.");
        }

        try
        {
            return ParityFixtureLoadResult.Loaded(_store.Load(File.ReadAllText(fixturePath)));
        }
        catch (DomainException ex)
        {
            return ParityFixtureLoadResult.Failed(ex.Message);
        }
        catch (JsonException ex)
        {
            return ParityFixtureLoadResult.Failed(ex.Message);
        }
        catch (NotSupportedException ex)
        {
            return ParityFixtureLoadResult.Failed(ex.Message);
        }
    }
}

public sealed class ParityFixtureComparer
{
    private const int DocumentStep = -1;
    private static readonly JsonSerializerOptions CanonicalOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private readonly ParityFixtureLoader _loader;

    public ParityFixtureComparer(ParityFixtureLoader? loader = null)
    {
        _loader = loader ?? new ParityFixtureLoader();
    }

    public ParityFixtureComparisonResult CompareFixtureFile(
        string scenarioId,
        string unityFixturePath,
        ParityTraceDocument rlTrace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scenarioId);
        ArgumentException.ThrowIfNullOrWhiteSpace(unityFixturePath);
        ArgumentNullException.ThrowIfNull(rlTrace);

        var load = _loader.Load(unityFixturePath);
        return load.Status switch
        {
            ParityFixtureLoadStatus.NotRun => NotRun(scenarioId, rlTrace.Events.Count, load.Error ?? "Unity parity fixture was not run."),
            ParityFixtureLoadStatus.Failed => FailedFromLoadError(scenarioId, rlTrace.Events.Count, load.Error ?? "Unity parity fixture failed to load."),
            _ => Compare(load.Document!, rlTrace),
        };
    }

    public ParityFixtureComparisonResult Compare(ParityTraceDocument unityTrace, ParityTraceDocument rlTrace)
    {
        ArgumentNullException.ThrowIfNull(unityTrace);
        ArgumentNullException.ThrowIfNull(rlTrace);

        var differences = new List<ParityFixtureDifference>();
        CompareDocumentSchema(unityTrace, rlTrace, differences);
        ValidateNoHiddenInformation("unity", unityTrace, differences);
        ValidateNoHiddenInformation("rl", rlTrace, differences);
        CompareDocumentHeader(unityTrace, rlTrace, differences);

        if (unityTrace.Events.Count != rlTrace.Events.Count)
        {
            differences.Add(new ParityFixtureDifference(
                DocumentStep,
                ParityFixtureDifferenceCategory.EventAlignment,
                "events.count",
                unityTrace.Events.Count.ToString(),
                rlTrace.Events.Count.ToString(),
                "Unity/RL event count mismatch."));
        }

        var pairedCount = Math.Min(unityTrace.Events.Count, rlTrace.Events.Count);
        for (var index = 0; index < pairedCount; index++)
        {
            CompareEvent(unityTrace.Events[index], rlTrace.Events[index], differences);
        }

        var status = differences.Count == 0
            ? ParityFixtureComparisonStatus.Passed
            : ParityFixtureComparisonStatus.Failed;
        var summary = status == ParityFixtureComparisonStatus.Passed
            ? "Unity/RL parity fixture matched exactly."
            : $"Unity/RL parity fixture mismatch at {differences[0].Path}.";

        return new ParityFixtureComparisonResult(
            unityTrace.ScenarioId,
            status,
            summary,
            unityTrace.Events.Count,
            rlTrace.Events.Count,
            differences.FirstOrDefault(),
            differences.ToArray());
    }

    private static ParityFixtureComparisonResult NotRun(string scenarioId, int rlEventCount, string summary) =>
        new(
            scenarioId,
            ParityFixtureComparisonStatus.NotRun,
            summary,
            0,
            rlEventCount,
            null,
            Array.Empty<ParityFixtureDifference>());

    private static ParityFixtureComparisonResult FailedFromLoadError(string scenarioId, int rlEventCount, string error)
    {
        var difference = new ParityFixtureDifference(
            DocumentStep,
            ParityFixtureDifferenceCategory.Schema,
            "fixture",
            error,
            "rl-trace-loaded",
            "Unity parity fixture failed schema or JSON validation.");

        return new ParityFixtureComparisonResult(
            scenarioId,
            ParityFixtureComparisonStatus.Failed,
            error,
            0,
            rlEventCount,
            difference,
            new[] { difference });
    }

    private static void CompareDocumentSchema(
        ParityTraceDocument unityTrace,
        ParityTraceDocument rlTrace,
        List<ParityFixtureDifference> differences)
    {
        CompareScalar(
            differences,
            DocumentStep,
            ParityFixtureDifferenceCategory.Schema,
            "schemaVersion",
            unityTrace.SchemaVersion,
            rlTrace.SchemaVersion);
        for (var index = 0; index < unityTrace.Events.Count; index++)
        {
            if (!string.Equals(unityTrace.Events[index].SchemaVersion, ParityTraceContract.SchemaVersion, StringComparison.Ordinal))
            {
                differences.Add(new ParityFixtureDifference(
                    unityTrace.Events[index].StepIndex,
                    ParityFixtureDifferenceCategory.Schema,
                    $"events[{index}].schemaVersion",
                    unityTrace.Events[index].SchemaVersion,
                    ParityTraceContract.SchemaVersion,
                    "Unity parity event schema version is unsupported."));
            }
        }
    }

    private static void CompareDocumentHeader(
        ParityTraceDocument unityTrace,
        ParityTraceDocument rlTrace,
        List<ParityFixtureDifference> differences)
    {
        CompareScalar(differences, DocumentStep, ParityFixtureDifferenceCategory.EventAlignment, "scenarioId", unityTrace.ScenarioId, rlTrace.ScenarioId);
        CompareScalar(differences, DocumentStep, ParityFixtureDifferenceCategory.EventAlignment, "seed", unityTrace.Seed, rlTrace.Seed);
    }

    private static void CompareEvent(
        ParityTraceEvent unityEvent,
        ParityTraceEvent rlEvent,
        List<ParityFixtureDifference> differences)
    {
        var step = Math.Min(unityEvent.StepIndex, rlEvent.StepIndex);

        CompareScalar(differences, step, ParityFixtureDifferenceCategory.EventAlignment, "stepIndex", unityEvent.StepIndex, rlEvent.StepIndex);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.Timing, $"events[{step}].kind", unityEvent.Kind, rlEvent.Kind);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.Timing, $"events[{step}].label", unityEvent.Label, rlEvent.Label);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.MemoryPhase, $"events[{step}].phase", unityEvent.Phase, rlEvent.Phase);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.MemoryPhase, $"events[{step}].turnPlayer", unityEvent.TurnPlayer, rlEvent.TurnPlayer);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.MemoryPhase, $"events[{step}].turnCount", unityEvent.TurnCount, rlEvent.TurnCount);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.MemoryPhase, $"events[{step}].memory", unityEvent.Memory, rlEvent.Memory);

        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.Zone, $"events[{step}].playerZoneCounts", unityEvent.PlayerZoneCounts, rlEvent.PlayerZoneCounts);
        CompareCanonical(
            differences,
            step,
            ParityFixtureDifferenceCategory.Zone,
            $"events[{step}].stateAfter.zones",
            unityEvent.StateAfter.Players.Select(player => new { player.Player, player.ZoneCounts, player.PublicZones }).ToArray(),
            rlEvent.StateAfter.Players.Select(player => new { player.Player, player.ZoneCounts, player.PublicZones }).ToArray());
        CompareCanonical(
            differences,
            step,
            ParityFixtureDifferenceCategory.Permanent,
            $"events[{step}].stateAfter.permanents",
            unityEvent.StateAfter.Players.Select(player => new { player.Player, player.Permanents }).ToArray(),
            rlEvent.StateAfter.Players.Select(player => new { player.Player, player.Permanents }).ToArray());
        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.Modifier, $"events[{step}].stateAfter.temporaryModifiers", unityEvent.StateAfter.TemporaryModifiers, rlEvent.StateAfter.TemporaryModifiers);
        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.DecisionSelection, $"events[{step}].decision", unityEvent.Decision, rlEvent.Decision);
        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.DecisionSelection, $"events[{step}].selection", unityEvent.Selection, rlEvent.Selection);
        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.Trigger, $"events[{step}].trigger", unityEvent.Trigger, rlEvent.Trigger);
        CompareCanonical(differences, step, ParityFixtureDifferenceCategory.Timing, $"events[{step}].outcome", unityEvent.Outcome, rlEvent.Outcome);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.EventAlignment, $"events[{step}].stateBefore.canonicalStateHash", unityEvent.StateBefore.CanonicalStateHash, rlEvent.StateBefore.CanonicalStateHash);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.EventAlignment, $"events[{step}].stateAfter.canonicalStateHash", unityEvent.StateAfter.CanonicalStateHash, rlEvent.StateAfter.CanonicalStateHash);
        CompareScalar(differences, step, ParityFixtureDifferenceCategory.EventAlignment, $"events[{step}].canonicalStateHash", unityEvent.CanonicalStateHash, rlEvent.CanonicalStateHash);
    }

    private static void ValidateNoHiddenInformation(
        string side,
        ParityTraceDocument trace,
        List<ParityFixtureDifference> differences)
    {
        foreach (var traceEvent in trace.Events)
        {
            ValidateNoHiddenInformation(side, traceEvent.StepIndex, "stateBefore", traceEvent.StateBefore, differences);
            ValidateNoHiddenInformation(side, traceEvent.StepIndex, "stateAfter", traceEvent.StateAfter, differences);
        }
    }

    private static void ValidateNoHiddenInformation(
        string side,
        int stepIndex,
        string snapshotName,
        RuleVisibleSnapshot snapshot,
        List<ParityFixtureDifference> differences)
    {
        foreach (var player in snapshot.Players)
        {
            foreach (var zone in player.PublicZones)
            {
                if (IsAlwaysHiddenZone(zone.Zone) && zone.Cards.Count > 0)
                {
                    differences.Add(new ParityFixtureDifference(
                        stepIndex,
                        ParityFixtureDifferenceCategory.HiddenInformation,
                        $"{side}.{snapshotName}.players[{player.Player}].publicZones.{zone.Zone}",
                        Canonical(zone.Cards),
                        "redacted",
                        "Hidden zone card identities must not appear in parity fixtures."));
                }

                foreach (var card in zone.Cards)
                {
                    if (IsAlwaysHiddenZone(card.Zone)
                        || (string.Equals(card.Zone, "Security", StringComparison.Ordinal) && !card.IsFaceUp))
                    {
                        differences.Add(new ParityFixtureDifference(
                            stepIndex,
                            ParityFixtureDifferenceCategory.HiddenInformation,
                            $"{side}.{snapshotName}.players[{player.Player}].publicZones.{zone.Zone}.cards[{card.InstanceId}]",
                            Canonical(card),
                            "redacted",
                            "Hidden card identity leaked into parity fixture."));
                    }
                }
            }
        }
    }

    private static bool IsAlwaysHiddenZone(string zone) =>
        string.Equals(zone, "Deck", StringComparison.Ordinal)
        || string.Equals(zone, "DigiEggDeck", StringComparison.Ordinal)
        || string.Equals(zone, "Hand", StringComparison.Ordinal);

    private static void CompareScalar<T>(
        List<ParityFixtureDifference> differences,
        int stepIndex,
        ParityFixtureDifferenceCategory category,
        string path,
        T unityValue,
        T rlValue)
    {
        if (EqualityComparer<T>.Default.Equals(unityValue, rlValue))
        {
            return;
        }

        differences.Add(new ParityFixtureDifference(
            stepIndex,
            category,
            path,
            Format(unityValue),
            Format(rlValue),
            $"{category} mismatch."));
    }

    private static void CompareCanonical<T>(
        List<ParityFixtureDifference> differences,
        int stepIndex,
        ParityFixtureDifferenceCategory category,
        string path,
        T unityValue,
        T rlValue)
    {
        var unityCanonical = Canonical(unityValue);
        var rlCanonical = Canonical(rlValue);
        if (string.Equals(unityCanonical, rlCanonical, StringComparison.Ordinal))
        {
            return;
        }

        differences.Add(new ParityFixtureDifference(
            stepIndex,
            category,
            path,
            unityCanonical,
            rlCanonical,
            $"{category} mismatch."));
    }

    private static string Canonical<T>(T value) =>
        JsonSerializer.Serialize(value, CanonicalOptions);

    private static string Format<T>(T value) =>
        value switch
        {
            null => "null",
            string text => text,
            _ => Canonical(value),
        };
}

public sealed class ParityFixtureComparisonResultStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public string Save(ParityFixtureComparisonResult result) =>
        JsonSerializer.Serialize(result, Options);

    public ParityFixtureComparisonResult Load(string serializedResult) =>
        JsonSerializer.Deserialize<ParityFixtureComparisonResult>(serializedResult, Options)
        ?? throw new DomainException("Parity fixture comparison result payload is empty.");
}

public sealed class ParityFixtureMarkdownReportRenderer
{
    public string Render(ParityFixtureComparisonResult result)
    {
        ArgumentNullException.ThrowIfNull(result);

        var lines = new List<string>
        {
            $"# Parity Fixture Report - {result.ScenarioId}",
            string.Empty,
            $"Status: `{result.Status}`",
            $"Summary: {result.Summary}",
            $"Unity events: `{result.UnityEventCount}`",
            $"RL events: `{result.RlEventCount}`",
        };

        if (result.FirstMismatch is not null)
        {
            lines.Add($"First mismatch: step `{result.FirstMismatch.StepIndex}`, `{result.FirstMismatch.Category}`, `{result.FirstMismatch.Path}`");
        }

        lines.Add(string.Empty);
        lines.Add("| Step | Category | Path | Message |");
        lines.Add("| --- | --- | --- | --- |");
        foreach (var difference in result.Differences)
        {
            lines.Add($"| {difference.StepIndex} | {difference.Category} | `{difference.Path}` | {difference.Message} |");
        }

        if (result.Differences.Count == 0)
        {
            lines.Add("| - | - | - | No differences. |");
        }

        return string.Join(Environment.NewLine, lines);
    }
}

