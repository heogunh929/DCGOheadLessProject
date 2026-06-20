using System.Text.Json;
using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed class TraceStore
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public string Save(GameTrace trace)
    {
        var dto = new TraceDto(trace.Events.Select(ToDto).ToArray());
        return JsonSerializer.Serialize(dto, Options);
    }

    public GameTrace Load(string serializedTrace)
    {
        var dto = JsonSerializer.Deserialize<TraceDto>(serializedTrace, Options)
            ?? throw new DomainException("Trace payload is empty.");
        return new GameTrace(dto.Events.Select(FromDto));
    }

    private static TraceEventDto ToDto(TraceEvent traceEvent) =>
        new(
            traceEvent.Index,
            traceEvent.Kind.ToString(),
            traceEvent.Label,
            traceEvent.StateHashBefore,
            traceEvent.StateHashAfter,
            traceEvent.Action is null ? null : ToActionDto(traceEvent.Action),
            traceEvent.DecisionResult is null ? null : ToDecisionResultDto(traceEvent.DecisionResult),
            traceEvent.Details);

    private static TraceEvent FromDto(TraceEventDto dto) =>
        new(
            dto.Index,
            Enum.Parse<TraceEventKind>(dto.Kind),
            dto.Label,
            dto.StateHashBefore,
            dto.StateHashAfter,
            Action: dto.Action is null ? null : FromActionDto(dto.Action),
            SelectionResult: dto.DecisionResult is null ? null : FromSelectionResultDto(dto.DecisionResult.SelectionResult),
            DecisionResult: dto.DecisionResult is null ? null : FromDecisionResultDto(dto.DecisionResult),
            Details: dto.Details);

    private static DecisionResultDto ToDecisionResultDto(DecisionResult result) =>
        new(result.Player.Value, result.Token.Value, ToSelectionResultDto(result.SelectionResult));

    private static DecisionResult FromDecisionResultDto(DecisionResultDto dto) =>
        new(new PlayerId(dto.Player), new DecisionToken(dto.Token), FromSelectionResultDto(dto.SelectionResult));

    private static SelectionResultDto ToSelectionResultDto(SelectionResult result) =>
        new(
            result.RequestId,
            result.SelectedTargets.Select(ToSelectableTargetDto).ToArray(),
            result.SelectedCount,
            result.SelectedBoolean,
            result.SelectedOption);

    private static SelectionResult FromSelectionResultDto(SelectionResultDto dto) =>
        new(
            dto.RequestId,
            dto.SelectedTargets.Select(FromSelectableTargetDto).ToArray(),
            dto.SelectedCount,
            dto.SelectedBoolean,
            dto.SelectedOption);

    private static SelectableTargetDto ToSelectableTargetDto(SelectableTarget target) =>
        new(
            target.Kind.ToString(),
            target.StableId,
            target.Owner?.Value,
            target.Card?.Value,
            target.Permanent?.Value,
            target.Label,
            target.FieldSlotIndex,
            target.OptionValue,
            target.Zone?.ToString());

    private static SelectableTarget FromSelectableTargetDto(SelectableTargetDto dto) =>
        new(
            Enum.Parse<SelectionTargetKind>(dto.Kind),
            dto.StableId,
            dto.Owner is null ? null : new PlayerId(dto.Owner.Value),
            dto.Card is null ? null : new CardInstanceId(dto.Card.Value),
            dto.Permanent is null ? null : new PermanentId(dto.Permanent.Value),
            dto.Label,
            dto.FieldSlotIndex,
            dto.OptionValue,
            dto.Zone is null ? null : Enum.Parse<Zone>(dto.Zone));

    private static ActionDto ToActionDto(GameAction action) =>
        action switch
        {
            HatchAction hatch => new("Hatch", hatch.Actor.Value, null, null, null, null, null, null, null, null),
            MoveFromBreedingAction move => new("MoveFromBreeding", move.Actor.Value, null, move.Permanent.Value, null, move.TargetFrameIndex, null, null, null, null),
            PlayCardAction play => new("PlayCard", play.Actor.Value, play.Card.Value, null, null, play.TargetFrameIndex, null, null, null, null),
            DigivolveAction digivolve => new("Digivolve", digivolve.Actor.Value, digivolve.Card.Value, null, digivolve.TargetPermanent.Value, null, null, null, null, null),
            JogressAction jogress => new("Jogress", jogress.Actor.Value, jogress.Card.Value, null, null, null, null, jogress.SourcePermanents.Select(id => id.Value).ToArray(), null, null),
            BurstDigivolveAction burst => new("BurstDigivolve", burst.Actor.Value, burst.Card.Value, burst.BurstTamer.Value, burst.TargetPermanent.Value, null, null, null, null, null),
            AppFusionAction appFusion => new("AppFusion", appFusion.Actor.Value, appFusion.Card.Value, appFusion.LinkCard.Value, appFusion.TargetPermanent.Value, null, null, null, null, null),
            DigiXrosPlayAction digiXros => new("DigiXrosPlay", digiXros.Actor.Value, digiXros.Card.Value, null, null, digiXros.TargetFrameIndex, null, null, digiXros.Materials.Select(id => id.Value).ToArray(), null),
            AssemblyPlayAction assembly => new("AssemblyPlay", assembly.Actor.Value, assembly.Card.Value, null, null, assembly.TargetFrameIndex, null, null, assembly.Materials.Select(id => id.Value).ToArray(), null),
            LinkAction link => new("Link", link.Actor.Value, link.LinkCard.Value, null, link.TargetPermanent.Value, null, null, null, null, null),
            DelayOptionPlayAction delay => new("DelayOptionPlay", delay.Actor.Value, delay.Card.Value, null, null, delay.TargetFrameIndex, null, null, null, null),
            AttackAction attack => new("Attack", attack.Actor.Value, null, attack.Attacker.Value, attack.Defender?.Value, null, null, null, null, null),
            PassAction pass => new("Pass", pass.Actor.Value, null, null, null, null, null, null, null, null),
            _ => throw new UnsupportedMechanicException($"Trace action serialization '{action.GetType().Name}'"),
        };

    private static GameAction FromActionDto(ActionDto dto)
    {
        var actor = new PlayerId(dto.Actor);
        return dto.Type switch
        {
            "Hatch" => new HatchAction(actor),
            "MoveFromBreeding" => new MoveFromBreedingAction(actor, new PermanentId(Require(dto.Permanent, dto.Type)), Require(dto.TargetFrameIndex, dto.Type)),
            "PlayCard" => new PlayCardAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), Require(dto.TargetFrameIndex, dto.Type)),
            "Digivolve" => new DigivolveAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), new PermanentId(Require(dto.TargetPermanent, dto.Type))),
            "Jogress" => new JogressAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), RequireArray(dto.SourcePermanents, dto.Type).Select(id => new PermanentId(id)).ToArray()),
            "BurstDigivolve" => new BurstDigivolveAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), new PermanentId(Require(dto.TargetPermanent, dto.Type)), new PermanentId(Require(dto.Permanent, dto.Type))),
            "AppFusion" => new AppFusionAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), new PermanentId(Require(dto.TargetPermanent, dto.Type)), new CardInstanceId(Require(dto.Permanent, dto.Type))),
            "DigiXrosPlay" => new DigiXrosPlayAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), Require(dto.TargetFrameIndex, dto.Type), RequireArray(dto.Materials, dto.Type).Select(id => new CardInstanceId(id)).ToArray()),
            "AssemblyPlay" => new AssemblyPlayAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), Require(dto.TargetFrameIndex, dto.Type), RequireArray(dto.Materials, dto.Type).Select(id => new CardInstanceId(id)).ToArray()),
            "Link" => new LinkAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), new PermanentId(Require(dto.TargetPermanent, dto.Type))),
            "DelayOptionPlay" => new DelayOptionPlayAction(actor, new CardInstanceId(Require(dto.Card, dto.Type)), Require(dto.TargetFrameIndex, dto.Type)),
            "Attack" => new AttackAction(actor, new PermanentId(Require(dto.Permanent, dto.Type)), dto.TargetPermanent is null ? null : new PermanentId(dto.TargetPermanent.Value)),
            "Pass" => new PassAction(actor),
            _ => throw new UnsupportedMechanicException($"Trace action deserialization '{dto.Type}'"),
        };
    }

    private static int Require(int? value, string actionType) =>
        value ?? throw new DomainException($"Trace action '{actionType}' is missing a required value.");

    private static IReadOnlyList<int> RequireArray(IReadOnlyList<int>? value, string actionType) =>
        value ?? throw new DomainException($"Trace action '{actionType}' is missing a required list.");

    private sealed record TraceDto(IReadOnlyList<TraceEventDto> Events);

    private sealed record TraceEventDto(
        int Index,
        string Kind,
        string Label,
        string StateHashBefore,
        string StateHashAfter,
        ActionDto? Action,
        DecisionResultDto? DecisionResult,
        string Details);

    private sealed record DecisionResultDto(
        int Player,
        long Token,
        SelectionResultDto SelectionResult);

    private sealed record SelectionResultDto(
        string RequestId,
        IReadOnlyList<SelectableTargetDto> SelectedTargets,
        int? SelectedCount,
        bool? SelectedBoolean,
        string? SelectedOption);

    private sealed record SelectableTargetDto(
        string Kind,
        string StableId,
        int? Owner,
        int? Card,
        int? Permanent,
        string Label,
        int? FieldSlotIndex,
        string? OptionValue,
        string? Zone);

    private sealed record ActionDto(
        string Type,
        int Actor,
        int? Card,
        int? Permanent,
        int? TargetPermanent,
        int? TargetFrameIndex,
        string? OptionValue,
        IReadOnlyList<int>? SourcePermanents,
        IReadOnlyList<int>? Materials,
        IReadOnlyList<int>? ExtraValues);
}
