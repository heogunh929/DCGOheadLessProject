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

    public TriggerPipelineContinuation? PendingContinuation => PipelineResult?.PendingContinuation;
}

public sealed record PlayCardResult(PermanentState? Permanent, OptionPlayResult? OptionPlay, TriggerPipelineResult? TriggerResult = null)
{
    public bool HasPendingSelection => OptionPlay?.HasPendingSelection == true || TriggerResult?.HasPendingSelection == true;

    public SelectionRequest? PendingSelectionRequest => OptionPlay?.PendingSelectionRequest ?? TriggerResult?.PendingSelectionRequest;

    public EffectResolution? PendingResolution => OptionPlay?.PendingResolution ?? TriggerResult?.PendingResolution;

    public TriggerPipelineContinuation? PendingContinuation => OptionPlay?.PendingContinuation ?? TriggerResult?.PendingContinuation;
}

public sealed class PlayCardService
{
    private readonly IZoneMover _zoneMover;
    private readonly TriggerPipelineService _triggerPipelineService;
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly StaticEffectService? _staticEffects;
    private Tier1PrimitiveService? _primitives;

    public PlayCardService(
        TriggerPipelineService triggerPipelineService,
        IZoneMover? zoneMover = null,
        Tier1PrimitiveService? primitives = null,
        EngineInvariantChecker? invariantChecker = null,
        StaticEffectService? staticEffects = null)
    {
        _triggerPipelineService = triggerPipelineService ?? throw new ArgumentNullException(nameof(triggerPipelineService));
        _zoneMover = zoneMover ?? new ZoneMover();
        _primitives = primitives;
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _staticEffects = staticEffects;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal TriggerPipelineService RuntimeTriggerPipelineService => _triggerPipelineService;

    internal Tier1PrimitiveService? RuntimePrimitiveService => _primitives;

    internal StaticEffectService? RuntimeStaticEffectService => _staticEffects;

    public PermanentState? Play(GameState state, PlayCardAction action, GameTrace? trace = null)
    {
        var snapshot = state.Clone();
        var traceCount = trace?.Events.Count ?? 0;
        var result = PlayWithResult(state, action, trace);
        if (result.HasPendingSelection)
        {
            RestoreAfterPendingSynchronousCall(state, snapshot, trace, traceCount);
            throw new DomainException(
                $"Card '{action.Card}' requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
        }

        return result.Permanent;
    }

    public PlayCardResult PlayWithResult(GameState state, PlayCardAction action, GameTrace? trace = null)
    {
        var player = ValidateHandPlay(state, action);
        var definition = BattleRules.Definition(state, action.Card);

        if (definition.CardKinds.Contains(CardKind.Option))
        {
            var result = PlayOptionFromHandAfterValidation(state, action, definition, trace);
            return new PlayCardResult(Permanent: null, result);
        }

        if (!definition.CardKinds.Contains(CardKind.Digimon) && !definition.CardKinds.Contains(CardKind.Tamer))
        {
            throw new UnsupportedMechanicException($"Playing card kind '{string.Join(",", definition.CardKinds)}'");
        }

        if (_staticEffects?.HasCardRestriction(
            state,
            action.Card,
            StaticCardRestrictionKind.CannotPutField,
            new StaticCardRestrictionCause(
                EffectSourceCardId: null,
                EffectSourcePermanentId: null,
                ControllerPlayerId: action.Actor,
                MoveReason: MoveReason.Play)) == true)
        {
            throw new DomainException($"Permanent card '{action.Card}' cannot be played by a static effect.");
        }

        if (!BattleRules.IsEmptyBattleFrame(player, action.TargetFrameIndex))
        {
            throw new DomainException($"Battle frame '{action.TargetFrameIndex}' is not empty.");
        }

        var cost = ResolvePlayCost(state, action.Card, definition);
        var memoryBeforeCost = state.Memory;
        BattleRules.PayMemory(state, action.Actor, cost);
        RunAfterPayCostPipeline(
            state,
            action.Actor,
            action.Card,
            cost,
            memoryBeforeCost,
            state.Memory,
            root: Zone.Hand,
            sourceZone: Zone.Hand,
            isEvolution: false,
            targetPermanents: Array.Empty<PermanentId>(),
            costKind: "Play",
            trace);

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
        var triggerResult = RunTriggerPipeline(
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
            },
            trace);
        return new PlayCardResult(permanent, OptionPlay: null, triggerResult);
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

    private OptionPlayResult PlayOptionFromHandAfterValidation(
        GameState state,
        PlayCardAction action,
        CardDefinition definition,
        GameTrace? trace)
    {
        if (_staticEffects?.HasCardRestriction(state, action.Card, StaticCardRestrictionKind.CannotPlay) == true)
        {
            throw new DomainException($"Option card '{action.Card}' cannot be played by a static effect.");
        }

        if (!BattleRules.MatchesOptionColorRequirement(state, action.Actor, action.Card, _staticEffects))
        {
            throw new DomainException($"Option card '{action.Card}' color requirements are not met.");
        }

        var cost = ResolvePlayCost(state, action.Card, definition);
        var memoryBeforeCost = state.Memory;
        BattleRules.PayMemory(state, action.Actor, cost);

        MoveHandOptionToExecuting(state, action.Actor, action.Card, trace);
        _invariantChecker.ThrowIfInvalid(state);
        RunAfterPayCostPipeline(
            state,
            action.Actor,
            action.Card,
            cost,
            memoryBeforeCost,
            state.Memory,
            root: Zone.Hand,
            sourceZone: Zone.Executing,
            isEvolution: false,
            targetPermanents: Array.Empty<PermanentId>(),
            costKind: "Play",
            trace);
        _invariantChecker.ThrowIfInvalid(state);

        var pipelineResult = RunOptionTriggerPipeline(state, action.Actor, action.Card, trace);
        if (pipelineResult.PendingSelectionRequest is not null)
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
            pipelineResult.SelectionApplications,
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

    private int ResolvePlayCost(GameState state, CardInstanceId card, CardDefinition definition)
    {
        var baseCost = Math.Max(0, definition.PlayCost);
        return _staticEffects?.ApplyCostModifiers(
            state,
            card,
            baseCost,
            StaticCostKind.Play)
            ?? baseCost;
    }

    private TriggerPipelineResult RunAfterPayCostPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        int paidCost,
        int memoryBeforeCost,
        int memoryAfterCost,
        Zone root,
        Zone sourceZone,
        bool isEvolution,
        IReadOnlyList<PermanentId> targetPermanents,
        string costKind,
        GameTrace? trace)
    {
        var result = _triggerPipelineService.Run(
            state,
            EffectTiming.AfterPayCost,
            player,
            card,
            sourcePermanent: null,
            values: CostPaymentRuleEventPayload.CreateAfterPayCost(
                state,
                player,
                card,
                paidCost,
                memoryBeforeCost,
                memoryAfterCost,
                root,
                sourceZone,
                isEvolution,
                targetPermanents,
                isJogress: false,
                costKind),
            trace: trace);

        if (result.HasPendingSelection)
        {
            throw new UnsupportedMechanicException("AfterPayCost selection continuation before play resolution is not implemented.");
        }

        return result;
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

    internal OptionPlayResult CompleteOptionPipelineResult(
        GameState state,
        CardInstanceId optionCard,
        TriggerPipelineResult pipelineResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(pipelineResult);

        if (pipelineResult.PendingSelectionRequest is not null)
        {
            return new OptionPlayResult(
                optionCard,
                pipelineResult,
                pipelineResult.SelectionApplications,
                pipelineResult.PendingResolution,
                pipelineResult.PendingSelectionRequest,
                MovedToTrash: false);
        }

        var moved = TrashIfStillExecuting(state, state.Cards[optionCard].Owner, optionCard, trace);
        _invariantChecker.ThrowIfInvalid(state);
        return new OptionPlayResult(
            optionCard,
            pipelineResult,
            pipelineResult.SelectionApplications,
            PendingResolution: null,
            PendingSelectionRequest: null,
            moved);
    }

    private static void RestoreAfterPendingSynchronousCall(
        GameState state,
        GameState snapshot,
        GameTrace? trace,
        int traceCount)
    {
        state.RestoreFrom(snapshot);
        trace?.Truncate(traceCount);
    }

    private TriggerPipelineResult RunOptionTriggerPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId sourceCard,
        GameTrace? trace)
    {
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

    private TriggerPipelineResult RunTriggerPipeline(
        GameState state,
        EffectTiming timing,
        PlayerId player,
        CardInstanceId sourceCard,
        PermanentId? sourcePermanent,
        IReadOnlyDictionary<string, object?> values,
        GameTrace? trace)
    {
        if (sourcePermanent is null || timing != EffectTiming.OnPlay)
        {
            return _triggerPipelineService.Run(
                state,
                timing,
                player,
                sourceCard,
                sourcePermanent,
                values,
                trace: trace);
        }

        var selfTiming = _triggerPipelineService.Prepare(
            state,
            timing,
            player,
            sourceCard,
            sourcePermanent,
            values);
        var enterFieldTiming = _triggerPipelineService.Prepare(
            state,
            EffectTiming.OnEnterFieldAnyone,
            player,
            values: EnterFieldEventPayload.ForSinglePermanent(
                sourceCard,
                sourcePermanent.Value,
                isEvolution: false));

        return _triggerPipelineService.RunPreparedSequence(
            state,
            new[] { selfTiming, enterFieldTiming },
            trace);
    }
}
