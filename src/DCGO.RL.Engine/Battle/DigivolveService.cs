using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Battle;

public sealed record DigivolveResult(PermanentState Permanent, TriggerPipelineResult? TriggerResult)
{
    public bool HasPendingSelection => TriggerResult?.HasPendingSelection == true;

    public SelectionRequest? PendingSelectionRequest => TriggerResult?.PendingSelectionRequest;

    public EffectResolution? PendingResolution => TriggerResult?.PendingResolution;

    public TriggerPipelineContinuation? PendingContinuation => TriggerResult?.PendingContinuation;
}

public sealed class DigivolveService
{
    private static readonly TriggerPipelineOptions BeforePayCostOptions = new(
        SourceZones: TriggerSourceZone.FieldTop
            | TriggerSourceZone.Inherited
            | TriggerSourceZone.Linked
            | TriggerSourceZone.Hand
            | TriggerSourceZone.Trash
            | TriggerSourceZone.Executing
            | TriggerSourceZone.FaceUpSecurity,
        ExecuteBackgroundEffects: true,
        ExecuteBackgroundEffectsFirst: true,
        ResolveAfterEffectsActivate: true,
        UseMultipleSkillsOrdering: false);

    private readonly IZoneMover _zoneMover;
    private readonly DrawService _drawService;
    private readonly TriggerPipelineService? _triggerPipelineService;
    private readonly StaticRequirementService? _staticRequirements;
    private readonly StaticEffectService? _staticEffects;

    public DigivolveService(
        IZoneMover? zoneMover = null,
        DrawService? drawService = null,
        TriggerPipelineService? triggerPipelineService = null,
        StaticRequirementService? staticRequirements = null,
        StaticEffectService? staticEffects = null)
    {
        _zoneMover = zoneMover ?? new ZoneMover();
        _drawService = drawService ?? new DrawService(_zoneMover);
        _triggerPipelineService = triggerPipelineService;
        _staticRequirements = staticRequirements;
        _staticEffects = staticEffects;
    }

    internal IZoneMover RuntimeZoneMover => _zoneMover;

    internal TriggerPipelineService? RuntimeTriggerPipelineService => _triggerPipelineService;

    internal StaticRequirementService? RuntimeStaticRequirementService => _staticRequirements;

    internal StaticEffectService? RuntimeStaticEffectService => _staticEffects;

    public PermanentState Digivolve(GameState state, DigivolveAction action, GameTrace? trace = null)
    {
        var snapshot = state.Clone();
        var traceCount = trace?.Events.Count ?? 0;
        var result = DigivolveWithResult(state, action, trace);
        if (result.HasPendingSelection)
        {
            RestoreAfterPendingSynchronousCall(state, snapshot, trace, traceCount);
            throw new DomainException(
                $"WhenDigivolving requires SelectionResult for request '{result.PendingSelectionRequest!.Id}'.");
        }

        return result.Permanent;
    }

    public DigivolveResult DigivolveWithResult(GameState state, DigivolveAction action, GameTrace? trace = null)
    {
        var player = state.GetPlayer(action.Actor);
        if (action.Actor != state.TurnPlayerId)
        {
            throw new DomainException($"Only turn player '{state.TurnPlayerId}' can digivolve.");
        }

        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        var permanent = BattleRules.Permanent(state, action.TargetPermanent);
        if (permanent.ControllerPlayerId != action.Actor)
        {
            throw new DomainException($"Permanent '{action.TargetPermanent}' is not controlled by player '{action.Actor}'.");
        }

        if (!BattleRules.CanDigivolve(state, action.Card, permanent, out var cost, _staticRequirements, _staticEffects))
        {
            throw new DomainException($"Card '{action.Card}' cannot digivolve onto permanent '{action.TargetPermanent}'.");
        }

        RunBeforePayCostPipeline(
            state,
            action.Actor,
            action.Card,
            currentCost: cost,
            baseCost: cost,
            memoryBeforeCost: state.Memory,
            action.TargetPermanent,
            trace);

        player = state.GetPlayer(action.Actor);
        if (!player.Hand.Contains(action.Card))
        {
            throw new DomainException($"Card '{action.Card}' is not in player '{action.Actor}' hand.");
        }

        permanent = BattleRules.Permanent(state, action.TargetPermanent);
        if (!BattleRules.CanDigivolve(state, action.Card, permanent, out cost, _staticRequirements, _staticEffects))
        {
            throw new DomainException($"Card '{action.Card}' cannot digivolve onto permanent '{action.TargetPermanent}'.");
        }

        var memoryBeforeCost = state.Memory;
        BattleRules.PayMemory(state, action.Actor, cost);
        RunAfterPayCostPipeline(
            state,
            action.Actor,
            action.Card,
            cost,
            memoryBeforeCost,
            state.Memory,
            action.TargetPermanent,
            trace);
        _zoneMover.DigivolveCard(state, new DigivolveCardCommand(action.Card, Zone.Hand, action.TargetPermanent));
        _drawService.DrawCards(state, action.Actor, 1, trace);
        var triggerResult = RunTriggerPipeline(state, action.Actor, action.Card, action.TargetPermanent, trace);
        return new DigivolveResult(permanent, triggerResult);
    }

    private TriggerPipelineResult? RunBeforePayCostPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        int currentCost,
        int baseCost,
        int memoryBeforeCost,
        PermanentId targetPermanent,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        var result = _triggerPipelineService.Run(
            state,
            EffectTiming.BeforePayCost,
            player,
            sourceCard: null,
            sourcePermanent: null,
            values: CostPaymentRuleEventPayload.CreateBeforePayCost(
                state,
                player,
                card,
                currentCost,
                baseCost,
                memoryBeforeCost,
                root: Zone.Hand,
                sourceZone: Zone.Hand,
                isEvolution: true,
                targetPermanents: new[] { targetPermanent },
                isJogress: false,
                costKind: "Digivolution"),
            options: BeforePayCostOptions,
            trace: trace);

        if (result.HasPendingSelection)
        {
            throw new UnsupportedMechanicException("BeforePayCost selection continuation before digivolution cost payment is not implemented.");
        }

        return result;
    }

    private TriggerPipelineResult? RunAfterPayCostPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        int paidCost,
        int memoryBeforeCost,
        int memoryAfterCost,
        PermanentId targetPermanent,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

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
                root: Zone.Hand,
                sourceZone: Zone.Hand,
                isEvolution: true,
                targetPermanents: new[] { targetPermanent },
                isJogress: false,
                costKind: "Digivolution"),
            trace: trace);

        if (result.HasPendingSelection)
        {
            throw new UnsupportedMechanicException("AfterPayCost selection continuation before digivolution resolution is not implemented.");
        }

        return result;
    }

    private TriggerPipelineResult? RunTriggerPipeline(
        GameState state,
        PlayerId player,
        CardInstanceId sourceCard,
        PermanentId sourcePermanent,
        GameTrace? trace)
    {
        if (_triggerPipelineService is null)
        {
            return null;
        }

        var selfTiming = _triggerPipelineService.Prepare(
            state,
            EffectTiming.WhenDigivolving,
            player,
            sourceCard,
            sourcePermanent,
            new Dictionary<string, object?>
            {
                ["Card"] = sourceCard,
                ["Permanent"] = sourcePermanent,
                ["Digivolved"] = true,
            });
        var enterFieldTiming = _triggerPipelineService.Prepare(
            state,
            EffectTiming.OnEnterFieldAnyone,
            player,
            values: EnterFieldEventPayload.ForSinglePermanent(
                sourceCard,
                sourcePermanent,
                isEvolution: true));

        return _triggerPipelineService.RunPreparedSequence(
            state,
            new[] { selfTiming, enterFieldTiming },
            trace);
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
}
