using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record OptionPlayResult(
    CardInstanceId OptionCard,
    TriggerPipelineResult? PipelineResult,
    IReadOnlyList<SelectionResultApplicationResult> SelectionApplications,
    EffectResolution? PendingResolution,
    SelectionRequest? PendingSelectionRequest,
    bool MovedToTrash)
{
    public bool HasPendingSelection => PendingSelectionRequest is not null;
}

public sealed class PlayCardService
{
    private readonly IZoneMover _zoneMover;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly SelectionResultApplicator _selectionApplicator;
    private readonly EngineInvariantChecker _invariantChecker;
    private Tier1PrimitiveService? _primitives;

    public PlayCardService(
        IZoneMover? zoneMover = null,
        TriggerPipelineService? triggerPipelineService = null,
        SelectionResultApplicator? selectionApplicator = null,
        Tier1PrimitiveService? primitives = null,
        EngineInvariantChecker? invariantChecker = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _triggerPipelineService = triggerPipelineService;
        _selectionApplicator = selectionApplicator ?? new SelectionResultApplicator();
        _primitives = primitives;
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
    }

    public PermanentState? Play(GameState state, PlayCardAction action, GameTrace? trace = null)
    {
        var player = ValidateHandPlay(state, action);
        var definition = BattleRules.Definition(state, action.Card);

        if (definition.CardKinds.Contains(CardKind.Option))
        {
            var result = PlayOptionFromHandAfterValidation(state, action, definition, trace);
            if (result.HasPendingSelection)
            {
                throw new DomainException(
                    $"Option card '{action.Card}' requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
            }

            return null;
        }

        if (!definition.CardKinds.Contains(CardKind.Digimon) && !definition.CardKinds.Contains(CardKind.Tamer))
        {
            throw new UnsupportedMechanicException($"Playing card kind '{string.Join(",", definition.CardKinds)}'");
        }

        if (!BattleRules.IsEmptyBattleFrame(player, action.TargetFrameIndex))
        {
            throw new DomainException($"Battle frame '{action.TargetFrameIndex}' is not empty.");
        }

        var cost = Math.Max(0, definition.PlayCost);
        BattleRules.PayMemory(state, action.Actor, cost);

        var permanentId = new PermanentId(BattleRules.NextPermanentId(state));
        _zoneMover.MoveCard(
            state,
            new MoveCardCommand(
                action.Card,
                Zone.Hand,
                Zone.BattleArea,
                MoveReason.Play,
                DestinationPermanent: permanentId,
                DestinationFrameIndex: action.TargetFrameIndex));

        var permanent = BattleRules.Permanent(state, permanentId);
        permanent.EnterFieldTurnCount = state.TurnCount;
        RunTriggerPipeline(
            state,
            EffectTiming.OnPlay,
            action.Actor,
            action.Card,
            permanent.Id,
            new Dictionary<string, object?>
            {
                ["Card"] = action.Card,
                ["Permanent"] = permanent.Id,
                ["Played"] = true,
            });
        return permanent;
    }

    public OptionPlayResult PlayOptionFromHand(GameState state, PlayCardAction action, GameTrace? trace = null)
    {
        ValidateHandPlay(state, action);
        var definition = BattleRules.Definition(state, action.Card);
        if (!definition.CardKinds.Contains(CardKind.Option))
        {
            throw new DomainException($"Card '{action.Card}' is not an Option card.");
        }

        return PlayOptionFromHandAfterValidation(state, action, definition, trace);
    }

    public OptionPlayResult ApplyOptionSelection(
        GameState state,
        EffectResolution pendingResolution,
        SelectionResult selectionResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(pendingResolution);
        ArgumentNullException.ThrowIfNull(selectionResult);

        var sourceCard = pendingResolution.SourceCard
            ?? throw new DomainException($"Effect resolution '{pendingResolution.StableId}' requires an option source card.");

        var application = _selectionApplicator.Apply(
            state,
            pendingResolution,
            selectionResult,
            Primitives,
            trace);
        _invariantChecker.ThrowIfInvalid(state);

        if (application.NextResolution is not null)
        {
            return new OptionPlayResult(
                sourceCard,
                PipelineResult: null,
                new[] { application },
                application.NextResolution,
                application.NextResolution.PendingSelectionRequest,
                MovedToTrash: false);
        }

        var moved = TrashIfStillExecuting(state, state.Cards[sourceCard].Owner, sourceCard, trace);
        _invariantChecker.ThrowIfInvalid(state);
        return new OptionPlayResult(
            sourceCard,
            PipelineResult: null,
            new[] { application },
            PendingResolution: null,
            PendingSelectionRequest: null,
            moved);
    }

    private Tier1PrimitiveService Primitives =>
        _primitives ??= new Tier1PrimitiveService(_zoneMover, playCardService: this);

    private OptionPlayResult PlayOptionFromHandAfterValidation(
        GameState state,
        PlayCardAction action,
        CardDefinition definition,
        GameTrace? trace)
    {
        var cost = Math.Max(0, definition.PlayCost);
        BattleRules.PayMemory(state, action.Actor, cost);

        MoveHandOptionToExecuting(state, action.Actor, action.Card, trace);
        _invariantChecker.ThrowIfInvalid(state);

        var pipelineResult = RunOptionTriggerPipeline(state, action.Actor, action.Card, trace);
        if (pipelineResult?.PendingSelectionRequest is not null)
        {
            return new OptionPlayResult(
                action.Card,
                pipelineResult,
                pipelineResult.SelectionApplications,
                pipelineResult.PendingResolution,
                pipelineResult.PendingSelectionRequest,
                MovedToTrash: false);
        }

        var moved = TrashIfStillExecuting(state, action.Actor, action.Card, trace);
        _invariantChecker.ThrowIfInvalid(state);
        return new OptionPlayResult(
            action.Card,
            pipelineResult,
            pipelineResult?.SelectionApplications ?? Array.Empty<SelectionResultApplicationResult>(),
            PendingResolution: null,
            PendingSelectionRequest: null,
            moved);
    }

    private PlayerState ValidateHandPlay(GameState state, PlayCardAction action)
    {
        var player = state.GetPlayer(action.Actor);
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can play cards.");
        }

        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        return player;
    }

    private void MoveHandOptionToExecuting(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        GameTrace? trace)
    {
        var command = new MoveCardCommand(card, Zone.Hand, Zone.Executing, MoveReason.Play, FaceUp: true);
        var before = trace is null ? null : state.Clone();
        var result = _zoneMover.MoveCard(state, command);
        trace?.AddMove($"option-play-execute:{player.Value}:{card.Value}", before!, state, command, result);
    }

    private bool TrashIfStillExecuting(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        GameTrace? trace)
    {
        if (!state.Cards.TryGetValue(card, out var instance) || instance.CurrentZone != Zone.Executing)
        {
            return false;
        }

        var command = new MoveCardCommand(card, Zone.Executing, Zone.Trash, MoveReason.Play, FaceUp: true);
        var before = trace is null ? null : state.Clone();
        var result = _zoneMover.MoveCard(state, command);
        trace?.AddMove($"option-play-trash:{player.Value}:{card.Value}", before!, state, command, result);
        return true;
    }

    private TriggerPipelineResult? RunOptionTriggerPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId sourceCard,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        return _triggerPipelineService.Run(
            state,
            EffectTiming.OptionSkill,
            player,
            sourceCard,
            sourcePermanent: null,
            values: new Dictionary<string, object?>
            {
                ["Card"] = sourceCard,
                ["ActivatedFromSecurity"] = false,
                ["PayCost"] = true,
                ["SourceZone"] = Zone.Executing,
            },
            trace: trace);
    }

    private void RunTriggerPipeline(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        CardInstanceId sourceCard,
        PermanentId? sourcePermanent,
        IReadOnlyDictionary<string, object?> values)
    {
        if (_triggerPipelineService is null)
        {
            return;
        }

        var result = _triggerPipelineService.Run(
            state,
            timing,
            player,
            sourceCard,
            sourcePermanent,
            values);
        if (result.PendingSelectionRequest is not null)
        {
            throw new DomainException(
                $"Trigger timing '{timing}' requires SelectionResult for request '{result.PendingSelectionRequest.Id}'.");
        }
    }
}
