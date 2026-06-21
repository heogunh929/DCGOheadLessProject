using System.Text.Json;
using System.Text.Json.Serialization;
using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public static class ParityTraceContract
{
    public const string SchemaVersion = "dcgo.parity.trace.v1";
}

public sealed record ParityTraceDocument(
    string SchemaVersion,
    string ScenarioId,
    ulong Seed,
    IReadOnlyList<ParityTraceEvent> Events);

public sealed record ParityTraceEvent(
    string SchemaVersion,
    string ScenarioId,
    ulong Seed,
    int StepIndex,
    string Kind,
    string Label,
    string Phase,
    int TurnPlayer,
    int TurnCount,
    int Memory,
    IReadOnlyList<RuleVisiblePlayerZoneCounts> PlayerZoneCounts,
    RuleVisibleSnapshot StateBefore,
    RuleVisibleSnapshot StateAfter,
    ParityActionSnapshot? Action,
    ParityMoveSnapshot? Move,
    ParityDecisionSnapshot? Decision,
    ParitySelectionSnapshot? Selection,
    ParityTriggerSnapshot? Trigger,
    ParityOutcomeSnapshot? Outcome,
    string CanonicalStateHash);

public sealed record RuleVisiblePlayerZoneCounts(
    int Player,
    IReadOnlyList<RuleVisibleZoneCount> Zones);

public sealed record ParityActionSnapshot(
    string Type,
    int Actor,
    int? Card,
    int? Permanent,
    int? TargetPermanent,
    int? TargetFrameIndex);

public sealed record ParityMoveSnapshot(
    int Card,
    int Owner,
    string SourceZone,
    string DestinationZone,
    string Reason,
    int? SourcePermanent,
    int? DestinationPermanent);

public sealed record ParityDecisionSnapshot(
    string Kind,
    int Player,
    string Phase,
    string Reason,
    long? Token,
    ParitySelectionRequestSnapshot? SelectionRequest);

public sealed record ParitySelectionRequestSnapshot(
    string Id,
    int Player,
    string Kind,
    string TargetKind,
    int MinCount,
    int MaxCount,
    bool CanSkip,
    bool CanEndNotMax,
    IReadOnlyList<ParitySelectableTargetSnapshot> Candidates);

public sealed record ParitySelectionSnapshot(
    int Player,
    long Token,
    string RequestId,
    IReadOnlyList<ParitySelectableTargetSnapshot> SelectedTargets,
    int? SelectedCount,
    bool? SelectedBoolean,
    string? SelectedOption);

public sealed record ParitySelectableTargetSnapshot(
    string Kind,
    string StableId,
    int? Owner,
    int? Card,
    int? Permanent,
    int? FieldSlotIndex,
    string? OptionValue,
    string? Zone);

public sealed record ParityTriggerSnapshot(
    string StableId,
    string? Timing,
    int? SourceCard,
    int? SourcePermanent);

public sealed record ParityOutcomeSnapshot(
    string? ZoneMoveReason,
    string? GameResultKind,
    int? Winner,
    string? BattleOutcome,
    string? SecurityOutcome,
    int? SecurityCard,
    int? SecurityDefender,
    int? DestroyedPermanent,
    int? DestroyedCard,
    string? FinalZone);

public sealed class ParityTraceExporter
{
    public ParityTraceDocument Export(string scenarioId, GameTrace trace)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(scenarioId);
        ArgumentNullException.ThrowIfNull(trace);

        var events = trace.Events
            .OrderBy(traceEvent => traceEvent.Index)
            .Select(traceEvent => ToParityEvent(scenarioId, traceEvent))
            .ToArray();
        var seed = events.FirstOrDefault()?.Seed ?? 0;
        return new ParityTraceDocument(
            ParityTraceContract.SchemaVersion,
            scenarioId,
            seed,
            events);
    }

    private static ParityTraceEvent ToParityEvent(string scenarioId, TraceEvent traceEvent)
    {
        var before = traceEvent.RuleVisibleStateBefore
            ?? throw new DomainException(
                $"Trace event '{traceEvent.Index}' has no rule-visible before snapshot.");
        var after = traceEvent.RuleVisibleStateAfter
            ?? throw new DomainException(
                $"Trace event '{traceEvent.Index}' has no rule-visible after snapshot.");
        var trigger = CreateTriggerSnapshot(traceEvent);
        var seed = traceEvent.Seed ?? 0;

        return new ParityTraceEvent(
            ParityTraceContract.SchemaVersion,
            scenarioId,
            seed,
            traceEvent.Index,
            traceEvent.Kind.ToString(),
            traceEvent.Label,
            after.Phase,
            after.TurnPlayer,
            after.TurnCount,
            after.Memory,
            after.Players
                .Select(player => new RuleVisiblePlayerZoneCounts(player.Player, player.ZoneCounts))
                .ToArray(),
            before,
            after,
            traceEvent.Action is null ? null : CreateActionSnapshot(traceEvent.Action),
            traceEvent.MoveCommand is null || traceEvent.MoveResult is null
                ? null
                : CreateMoveSnapshot(traceEvent.MoveCommand, traceEvent.MoveResult),
            traceEvent.DecisionPoint is null ? null : CreateDecisionSnapshot(traceEvent.DecisionPoint),
            traceEvent.DecisionResult is null ? null : CreateSelectionSnapshot(traceEvent.DecisionResult),
            trigger,
            CreateOutcomeSnapshot(traceEvent, after),
            after.CanonicalStateHash);
    }

    private static ParityActionSnapshot CreateActionSnapshot(GameAction action) =>
        action switch
        {
            HatchAction hatch => new("Hatch", hatch.Actor.Value, null, null, null, null),
            MoveFromBreedingAction move => new("MoveFromBreeding", move.Actor.Value, null, move.Permanent.Value, null, move.TargetFrameIndex),
            PlayCardAction play => new("PlayCard", play.Actor.Value, play.Card.Value, null, null, play.TargetFrameIndex),
            DigivolveAction digivolve => new("Digivolve", digivolve.Actor.Value, digivolve.Card.Value, null, digivolve.TargetPermanent.Value, null),
            AttackAction attack => new("Attack", attack.Actor.Value, null, attack.Attacker.Value, attack.Defender?.Value, null),
            PassAction pass => new("Pass", pass.Actor.Value, null, null, null, null),
            JogressAction jogress => new("Jogress", jogress.Actor.Value, jogress.Card.Value, null, null, null),
            BurstDigivolveAction burst => new("BurstDigivolve", burst.Actor.Value, burst.Card.Value, burst.BurstTamer.Value, burst.TargetPermanent.Value, null),
            AppFusionAction appFusion => new("AppFusion", appFusion.Actor.Value, appFusion.Card.Value, appFusion.LinkCard.Value, appFusion.TargetPermanent.Value, null),
            DigiXrosPlayAction digiXros => new("DigiXrosPlay", digiXros.Actor.Value, digiXros.Card.Value, null, null, digiXros.TargetFrameIndex),
            AssemblyPlayAction assembly => new("AssemblyPlay", assembly.Actor.Value, assembly.Card.Value, null, null, assembly.TargetFrameIndex),
            LinkAction link => new("Link", link.Actor.Value, link.LinkCard.Value, null, link.TargetPermanent.Value, null),
            DelayOptionPlayAction delay => new("DelayOptionPlay", delay.Actor.Value, delay.Card.Value, null, null, delay.TargetFrameIndex),
            SelectionAction selection => new("Selection", selection.Actor.Value, null, null, null, null),
            _ => throw new UnsupportedMechanicException($"Parity trace action '{action.GetType().Name}'."),
        };

    private static ParityMoveSnapshot CreateMoveSnapshot(MoveCardCommand command, MoveCardResult result) =>
        new(
            result.Card.Value,
            result.Trace.Owner.Value,
            result.SourceZone.ToString(),
            result.DestinationZone.ToString(),
            command.Reason.ToString(),
            result.Trace.SourcePermanent?.Value,
            result.Trace.DestinationPermanent?.Value);

    private static ParityDecisionSnapshot CreateDecisionSnapshot(DecisionPoint decision) =>
        new(
            decision.Kind.ToString(),
            decision.Player.Value,
            decision.Phase.ToString(),
            decision.Reason,
            decision.Token?.Value,
            decision.SelectionRequest is null ? null : CreateSelectionRequestSnapshot(decision.SelectionRequest));

    private static ParitySelectionRequestSnapshot CreateSelectionRequestSnapshot(SelectionRequest request) =>
        new(
            request.Id,
            request.Player.Value,
            request.Kind.ToString(),
            request.TargetKind.ToString(),
            request.MinCount,
            request.MaxCount,
            request.CanSkip,
            request.CanEndNotMax,
            request.Candidates.Select(CreateSelectableTargetSnapshot).ToArray());

    private static ParitySelectionSnapshot CreateSelectionSnapshot(DecisionResult result) =>
        new(
            result.Player.Value,
            result.Token.Value,
            result.SelectionResult.RequestId,
            result.SelectionResult.SelectedTargets.Select(CreateSelectableTargetSnapshot).ToArray(),
            result.SelectionResult.SelectedCount,
            result.SelectionResult.SelectedBoolean,
            result.SelectionResult.SelectedOption);

    private static ParitySelectableTargetSnapshot CreateSelectableTargetSnapshot(SelectableTarget target) =>
        new(
            target.Kind.ToString(),
            target.StableId,
            target.Owner?.Value,
            target.Card?.Value,
            target.Permanent?.Value,
            target.FieldSlotIndex,
            target.OptionValue,
            target.Zone?.ToString());

    private static ParityTriggerSnapshot? CreateTriggerSnapshot(TraceEvent traceEvent)
    {
        if (traceEvent.DecisionPoint is not null || traceEvent.DecisionResult is not null)
        {
            var parts = traceEvent.Label.Split(':', 3);
            if (parts.Length >= 2)
            {
                return new ParityTriggerSnapshot(parts[1], null, null, null);
            }
        }

        return null;
    }

    private static ParityOutcomeSnapshot? CreateOutcomeSnapshot(
        TraceEvent traceEvent,
        RuleVisibleSnapshot after)
    {
        if (traceEvent.MoveCommand is null && after.GameResult.Kind == GameResultKind.Ongoing.ToString())
        {
            return null;
        }

        var labelParts = traceEvent.Label.Split(':');
        var battleOutcome = CreateBattleOutcome(traceEvent, labelParts);
        var securityOutcome = CreateSecurityOutcome(traceEvent, labelParts);

        return new ParityOutcomeSnapshot(
            traceEvent.MoveCommand?.Reason.ToString(),
            after.GameResult.Kind,
            after.GameResult.Winner,
            battleOutcome.Outcome,
            securityOutcome.Outcome,
            securityOutcome.Card,
            securityOutcome.Defender,
            battleOutcome.Permanent,
            battleOutcome.Card,
            traceEvent.MoveResult?.DestinationZone.ToString());
    }

    private static (string? Outcome, int? Permanent, int? Card) CreateBattleOutcome(
        TraceEvent traceEvent,
        IReadOnlyList<string> labelParts)
    {
        if (traceEvent.MoveResult is null || labelParts.Count < 3)
        {
            return (null, null, null);
        }

        return labelParts[0] switch
        {
            "destroy-top" => ("DestroyTop", ParseInt(labelParts[1]), traceEvent.MoveResult.Card.Value),
            "destroy-source" => ("DestroySource", ParseInt(labelParts[1]), traceEvent.MoveResult.Card.Value),
            _ => (null, null, null),
        };
    }

    private static (string? Outcome, int? Card, int? Defender) CreateSecurityOutcome(
        TraceEvent traceEvent,
        IReadOnlyList<string> labelParts)
    {
        if (traceEvent.MoveResult is null || labelParts.Count < 3)
        {
            return (null, null, null);
        }

        return labelParts[0] switch
        {
            "security-check-execute" => ("ExecuteSecuritySkill", ParseInt(labelParts[2]), ParseInt(labelParts[1])),
            "security-check-trash" => ("TrashCheckedCard", ParseInt(labelParts[2]), ParseInt(labelParts[1])),
            _ => (null, null, null),
        };
    }

    private static int? ParseInt(string value) =>
        int.TryParse(value, out var parsed) ? parsed : null;
}

public sealed class ParityTraceStore
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public string Save(ParityTraceDocument document)
    {
        ValidateSchema(document.SchemaVersion);
        foreach (var traceEvent in document.Events)
        {
            ValidateSchema(traceEvent.SchemaVersion);
        }

        return JsonSerializer.Serialize(document, Options);
    }

    public ParityTraceDocument Load(string serializedTrace)
    {
        var document = JsonSerializer.Deserialize<ParityTraceDocument>(serializedTrace, Options)
            ?? throw new DomainException("Parity trace payload is empty.");
        ValidateSchema(document.SchemaVersion);
        foreach (var traceEvent in document.Events)
        {
            ValidateSchema(traceEvent.SchemaVersion);
        }

        return document;
    }

    private static void ValidateSchema(string schemaVersion)
    {
        if (!string.Equals(schemaVersion, ParityTraceContract.SchemaVersion, StringComparison.Ordinal))
        {
            throw new DomainException(
                $"Unsupported parity trace schema '{schemaVersion}', expected '{ParityTraceContract.SchemaVersion}'.");
        }
    }
}
