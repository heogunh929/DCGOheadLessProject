using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Effects;

[Flags]
public enum TriggerSourceZone
{
    None = 0,
    FieldTop = 1,
    Inherited = 2,
    Hand = 4,
    Trash = 8,
    Executing = 16,
    FaceUpSecurity = 32,
}

public sealed record TriggerPipelineOptions(
    TriggerSourceZone SourceZones = TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Executing
        | TriggerSourceZone.FaceUpSecurity,
    bool ExecuteBackgroundEffects = false);

public sealed record TriggerPipelineResult(
    EffectContext Context,
    IReadOnlyList<EffectResolution> QueuedEffects,
    IReadOnlyList<EffectResolution> BackgroundEffects,
    IReadOnlyList<EffectResolution> ExecutedEffects,
    IReadOnlyList<EffectResolution> SkippedOptionalEffects,
    IReadOnlyList<SelectionResultApplicationResult> SelectionApplications,
    EffectResolution? PendingResolution = null,
    SelectionRequest? PendingSelectionRequest = null,
    TriggerPipelineContinuation? PendingContinuation = null)
{
    public bool HasPendingSelection => PendingContinuation is not null || PendingSelectionRequest is not null;
}

public sealed record TriggerPipelineContinuation(
    EffectContext Context,
    EffectResolution PendingResolution,
    SelectionRequest PendingSelectionRequest,
    EffectDecisionStage PendingStage,
    IReadOnlyList<EffectResolution> RemainingQueuedEffects,
    IReadOnlyList<EffectResolution> RemainingBackgroundEffects,
    TriggerPipelineOptions Options,
    bool WasDrainingBackground)
{
    public string StableContinuationId => PendingResolution.StableId;
}

public sealed record PreparedTriggerGroup(
    EffectContext Context,
    IReadOnlyList<EffectResolution> QueuedEffects,
    IReadOnlyList<EffectResolution> BackgroundEffects,
    TriggerPipelineOptions Options);

public sealed class TriggerPipelineService
{
    private readonly ICardScriptRegistry _cardScriptRegistry;
    private readonly IDecisionProvider? _decisionProvider;
    private readonly TriggerCollector _triggerCollector;
    private readonly OncePerTurnTracker _oncePerTurnTracker;
    private readonly SelectionResultApplicator _selectionApplicator;
    private readonly Tier1PrimitiveService _primitives;
    private readonly EngineInvariantChecker _invariantChecker;

    public TriggerPipelineService(
        ICardScriptRegistry cardScriptRegistry,
        IDecisionProvider? decisionProvider = null,
        TriggerCollector? triggerCollector = null,
        OncePerTurnTracker? oncePerTurnTracker = null,
        SelectionResultApplicator? selectionApplicator = null,
        Tier1PrimitiveService? primitives = null,
        EngineInvariantChecker? invariantChecker = null)
    {
        _cardScriptRegistry = cardScriptRegistry ?? throw new ArgumentNullException(nameof(cardScriptRegistry));
        _decisionProvider = decisionProvider;
        _triggerCollector = triggerCollector ?? new TriggerCollector();
        _oncePerTurnTracker = oncePerTurnTracker ?? new OncePerTurnTracker();
        _selectionApplicator = selectionApplicator ?? new SelectionResultApplicator();
        _primitives = primitives ?? new Tier1PrimitiveService();
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
    }

    internal Tier1PrimitiveService RuntimePrimitiveService => _primitives;

    public TriggerPipelineResult Run(
        GameState state,
        EffectTiming timing,
        PlayerId? player = null,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        IReadOnlyDictionary<string, object?>? values = null,
        TriggerPipelineOptions? options = null,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var context = new EffectContext(state, timing, player, sourceCard, sourcePermanent, values);
        var descriptors = CollectSourceDescriptors(state, context, options ?? new TriggerPipelineOptions());
        var collected = _triggerCollector.Collect(context, descriptors);
        return RunPrepared(
            state,
            new PreparedTriggerGroup(
                context,
                collected.QueuedEffects,
                collected.BackgroundEffects,
                options ?? new TriggerPipelineOptions()),
            trace);
    }

    public PreparedTriggerGroup Prepare(
        GameState state,
        EffectTiming timing,
        PlayerId? player = null,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        IReadOnlyDictionary<string, object?>? values = null,
        TriggerPipelineOptions? options = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var resolvedOptions = options ?? new TriggerPipelineOptions();
        var context = new EffectContext(state, timing, player, sourceCard, sourcePermanent, values);
        var descriptors = CollectSourceDescriptors(state, context, resolvedOptions);
        var collected = _triggerCollector.Collect(context, descriptors);
        return new PreparedTriggerGroup(
            context,
            collected.QueuedEffects,
            collected.BackgroundEffects,
            resolvedOptions);
    }

    public TriggerPipelineResult RunPrepared(
        GameState state,
        PreparedTriggerGroup prepared,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(prepared);

        if (!ReferenceEquals(state, prepared.Context.State))
        {
            throw new DomainException("Prepared trigger group must run against the same GameState instance used to prepare it.");
        }

        var queue = new EffectQueue();
        queue.EnqueueRange(prepared.QueuedEffects);

        var executedEffects = new List<EffectResolution>();
        var skippedOptionalEffects = new List<EffectResolution>();
        var selectionApplications = new List<SelectionResultApplicationResult>();

        var pending = DrainQueue(
            queue,
            state,
            prepared.BackgroundEffects,
            prepared.Options,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);

        return new TriggerPipelineResult(
            prepared.Context,
            prepared.QueuedEffects,
            prepared.BackgroundEffects,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications,
            pending?.Continuation.PendingResolution,
            pending?.Continuation.PendingSelectionRequest,
            pending?.Continuation);
    }

    public TriggerPipelineResult Resume(
        GameState state,
        TriggerPipelineContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(continuation);
        ArgumentNullException.ThrowIfNull(selectionResult);

        var executedEffects = new List<EffectResolution>();
        var skippedOptionalEffects = new List<EffectResolution>();
        var selectionApplications = new List<SelectionResultApplicationResult>();

        var pending = ResolvePendingSelection(
            state,
            continuation,
            selectionResult,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
        if (pending is not null)
        {
            return BuildResumeResult(continuation, pending, executedEffects, skippedOptionalEffects, selectionApplications);
        }

        pending = DrainContinuationTail(
            continuation,
            state,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);

        return BuildResumeResult(continuation, pending, executedEffects, skippedOptionalEffects, selectionApplications);
    }

    private PendingSelection? DrainQueue(
        EffectQueue queue,
        GameState state,
        IReadOnlyList<EffectResolution> backgroundEffects,
        TriggerPipelineOptions options,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        while (queue.HasPending)
        {
            var resolution = queue.Dequeue();
            var pending = ExecuteResolution(
                state,
                resolution,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                return AttachTail(
                    pending,
                    resolution.Context,
                    queue.Pending,
                    backgroundEffects,
                    options,
                    wasDrainingBackground: false);
            }
        }

        if (!options.ExecuteBackgroundEffects)
        {
            return null;
        }

        for (var i = 0; i < backgroundEffects.Count; i++)
        {
            var background = backgroundEffects[i];
            var pending = ExecuteResolution(
                state,
                background,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                return AttachTail(
                    pending,
                    background.Context,
                    Array.Empty<EffectResolution>(),
                    backgroundEffects.Skip(i + 1).ToArray(),
                    options,
                    wasDrainingBackground: true);
            }
        }

        return null;
    }

    private PendingSelection? ExecuteResolution(
        GameState state,
        EffectResolution resolution,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (resolution.OptionalSelectionRequest is not null)
        {
            if (_decisionProvider is null)
            {
                return CreatePendingSelection(resolution, resolution.OptionalSelectionRequest, EffectDecisionStage.Optional);
            }

            var optionalResult = _decisionProvider.ChooseSelection(resolution.OptionalSelectionRequest);
            SelectionValidator.Validate(resolution.OptionalSelectionRequest, optionalResult);
            if (optionalResult.SelectedBoolean == false)
            {
                skippedOptionalEffects.Add(resolution);
                return null;
            }

            if (resolution.SelectionRequest is not null)
            {
                return CreatePendingSelection(resolution, resolution.SelectionRequest, EffectDecisionStage.Selection);
            }
        }

        if (resolution.SelectionRequest is not null)
        {
            if (_decisionProvider is null)
            {
                return CreatePendingSelection(resolution, resolution.SelectionRequest, EffectDecisionStage.Selection);
            }

            var result = _decisionProvider.ChooseSelection(resolution.SelectionRequest);
            RegisterOncePerTurnIfNeeded(state, resolution);
            var application = _selectionApplicator.Apply(
                state,
                resolution,
                resolution.SelectionRequest,
                result,
                _primitives,
                trace);
            selectionApplications.Add(application);
            executedEffects.Add(resolution);
            if (application.NextResolution is not null)
            {
                return ExecuteResolution(
                    state,
                    application.NextResolution,
                    trace,
                    executedEffects,
                    skippedOptionalEffects,
                    selectionApplications);
            }

            return null;
        }

        RegisterOncePerTurnIfNeeded(state, resolution);
        _invariantChecker.ThrowIfInvalid(state);
        ResolveBody(state, resolution, trace);
        _invariantChecker.ThrowIfInvalid(state);
        executedEffects.Add(resolution);
        return null;
    }

    private void RegisterOncePerTurnIfNeeded(GameState state, EffectResolution resolution)
    {
        if (!resolution.IsOncePerTurn)
        {
            return;
        }

        var player = resolution.Controller
            ?? resolution.Context.Player
            ?? throw new DomainException($"Once-per-turn effect '{resolution.StableId}' requires a controller/player.");
        var key = resolution.OncePerTurnKey ?? resolution.StableId;
        _oncePerTurnTracker.RegisterUse(state, key, player, resolution.SourceCard);
    }

    private void ResolveBody(GameState state, EffectResolution resolution, GameTrace? trace)
    {
        var sourceCard = resolution.SourceCard
            ?? throw new DomainException($"Effect resolution '{resolution.StableId}' requires a source card.");
        if (!state.Cards.TryGetValue(sourceCard, out var instance))
        {
            throw new DomainException($"Effect resolution '{resolution.StableId}' source card '{sourceCard}' does not exist.");
        }

        if (!state.CardDefinitions.TryGetValue(instance.DefinitionId, out var definition))
        {
            throw new DomainException($"Card definition '{instance.DefinitionId}' does not exist.");
        }

        var script = _cardScriptRegistry.GetScript(definition);
        script.Resolve(new CardScriptExecutionContext(state, resolution, _primitives, trace));
    }

    private IReadOnlyList<EffectDescriptor> CollectSourceDescriptors(
        GameState state,
        EffectContext context,
        TriggerPipelineOptions options)
    {
        var descriptors = new List<EffectDescriptor>();
        foreach (var source in EnumerateSources(state, context, options.SourceZones))
        {
            if (!state.CardDefinitions.TryGetValue(source.Instance.DefinitionId, out var definition))
            {
                throw new DomainException($"Card definition '{source.Instance.DefinitionId}' does not exist.");
            }

            var script = _cardScriptRegistry.GetScript(definition);
            descriptors.AddRange(script.CreateEffectDescriptors(new CardScriptContext(
                state,
                source.Card,
                source.Permanent,
                source.Controller)));
        }

        return descriptors;
    }

    private static IEnumerable<TriggerSource> EnumerateSources(
        GameState state,
        EffectContext context,
        TriggerSourceZone sourceZones)
    {
        if (context.SourceCard is not null)
        {
            yield return CreateExplicitSource(state, context.SourceCard.Value, context.SourcePermanent, context.Player);
            yield break;
        }

        foreach (var player in state.Players.OrderBy(player => player.Id.Value))
        {
            foreach (var permanent in player.FieldPermanents.OrderBy(permanent => permanent.FrameIndex).ThenBy(permanent => permanent.Id.Value))
            {
                if (sourceZones.HasFlag(TriggerSourceZone.FieldTop))
                {
                    yield return CreatePermanentSource(state, permanent.TopCardId, permanent);
                }

                if (sourceZones.HasFlag(TriggerSourceZone.Inherited))
                {
                    foreach (var sourceCard in permanent.SourceCardIds)
                    {
                        yield return CreatePermanentSource(state, sourceCard, permanent);
                    }
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Hand))
            {
                foreach (var card in player.Hand)
                {
                    yield return CreatePlayerZoneSource(state, card);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Trash))
            {
                foreach (var card in player.Trash)
                {
                    yield return CreatePlayerZoneSource(state, card);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Executing))
            {
                foreach (var card in player.Executing)
                {
                    yield return CreatePlayerZoneSource(state, card);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.FaceUpSecurity))
            {
                foreach (var card in player.Security.Where(card => state.Cards[card].IsFaceUp))
                {
                    yield return CreatePlayerZoneSource(state, card);
                }
            }
        }
    }

    private static TriggerSource CreateExplicitSource(
        GameState state,
        CardInstanceId card,
        PermanentId? sourcePermanent,
        PlayerId? contextPlayer)
    {
        var instance = RequireCard(state, card);
        PermanentState? permanent = null;
        if (sourcePermanent is not null)
        {
            permanent = FindPermanent(state, sourcePermanent.Value)
                ?? throw new DomainException($"Source permanent '{sourcePermanent}' does not exist.");
        }
        else if (instance.PermanentId is not null)
        {
            permanent = FindPermanent(state, instance.PermanentId.Value);
        }

        var controller = permanent?.ControllerPlayerId ?? contextPlayer ?? instance.Owner;
        return new TriggerSource(card, instance, permanent?.Id, controller);
    }

    private static TriggerSource CreatePermanentSource(
        GameState state,
        CardInstanceId card,
        PermanentState permanent)
    {
        var instance = RequireCard(state, card);
        return new TriggerSource(card, instance, permanent.Id, permanent.ControllerPlayerId);
    }

    private static TriggerSource CreatePlayerZoneSource(GameState state, CardInstanceId card)
    {
        var instance = RequireCard(state, card);
        return new TriggerSource(card, instance, null, instance.Owner);
    }

    private static CardInstance RequireCard(GameState state, CardInstanceId card) =>
        state.Cards.TryGetValue(card, out var instance)
            ? instance
            : throw new DomainException($"Trigger source card '{card}' does not exist.");

    private static PermanentState? FindPermanent(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId);

    private PendingSelection? ResolvePendingSelection(
        GameState state,
        TriggerPipelineContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var resolution = continuation.PendingResolution;
        var request = continuation.PendingSelectionRequest;

        if (!string.Equals(selectionResult.RequestId, request.Id, StringComparison.Ordinal))
        {
            throw new DomainException(
                $"SelectionResult request id '{selectionResult.RequestId}' does not match pending request '{request.Id}'.");
        }

        if (continuation.PendingStage == EffectDecisionStage.Optional)
        {
            if (!ReferenceEquals(request, resolution.OptionalSelectionRequest))
            {
                throw new DomainException(
                    $"Pending optional stage for '{resolution.StableId}' is not bound to its optional request.");
            }

            SelectionValidator.Validate(request, selectionResult);
            if (selectionResult.SelectedBoolean == false)
            {
                skippedOptionalEffects.Add(resolution);
                return null;
            }

            if (resolution.SelectionRequest is not null)
            {
                return AttachNestedTail(
                    CreatePendingSelection(
                    resolution,
                    resolution.SelectionRequest,
                        EffectDecisionStage.Selection),
                    continuation);
            }

            RegisterOncePerTurnIfNeeded(state, resolution);
            _invariantChecker.ThrowIfInvalid(state);
            ResolveBody(state, resolution, trace);
            _invariantChecker.ThrowIfInvalid(state);
            executedEffects.Add(resolution);
            return null;
        }

        RegisterOncePerTurnIfNeeded(state, resolution);
        if (continuation.PendingStage != EffectDecisionStage.Selection)
        {
            throw new DomainException(
                $"Unsupported pending decision stage '{continuation.PendingStage}' for '{resolution.StableId}'.");
        }

        var application = _selectionApplicator.Apply(state, resolution, request, selectionResult, _primitives, trace);
        selectionApplications.Add(application);
        executedEffects.Add(resolution);
        if (application.NextResolution is null)
        {
            return null;
        }

        var pending = ExecuteResolution(
            state,
            application.NextResolution,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
        return pending is null
            ? null
            : AttachNestedTail(pending, continuation);
    }

    private PendingSelection? DrainContinuationTail(
        TriggerPipelineContinuation continuation,
        GameState state,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (continuation.WasDrainingBackground)
        {
            return DrainBackgroundEffects(
                continuation,
                state,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
        }

        var queue = new EffectQueue();
        queue.EnqueueRange(continuation.RemainingQueuedEffects);
        return DrainQueue(
            queue,
            state,
            continuation.RemainingBackgroundEffects,
            continuation.Options,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private PendingSelection? DrainBackgroundEffects(
        TriggerPipelineContinuation continuation,
        GameState state,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        for (var i = 0; i < continuation.RemainingBackgroundEffects.Count; i++)
        {
            var background = continuation.RemainingBackgroundEffects[i];
            var pending = ExecuteResolution(
                state,
                background,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                return AttachTail(
                    pending,
                    background.Context,
                    Array.Empty<EffectResolution>(),
                    continuation.RemainingBackgroundEffects.Skip(i + 1).ToArray(),
                    continuation.Options,
                    wasDrainingBackground: true);
            }
        }

        return null;
    }

    private static TriggerPipelineResult BuildResumeResult(
        TriggerPipelineContinuation continuation,
        PendingSelection? pending,
        IReadOnlyList<EffectResolution> executedEffects,
        IReadOnlyList<EffectResolution> skippedOptionalEffects,
        IReadOnlyList<SelectionResultApplicationResult> selectionApplications) =>
        new(
            continuation.Context,
            continuation.RemainingQueuedEffects,
            continuation.RemainingBackgroundEffects,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications,
            pending?.Continuation.PendingResolution,
            pending?.Continuation.PendingSelectionRequest,
            pending?.Continuation);

    private static PendingSelection CreatePendingSelection(
        EffectResolution resolution,
        SelectionRequest request,
        EffectDecisionStage stage) =>
        new(new TriggerPipelineContinuation(
            resolution.Context,
            resolution,
            request,
            stage,
            Array.Empty<EffectResolution>(),
            Array.Empty<EffectResolution>(),
            new TriggerPipelineOptions(),
            WasDrainingBackground: false));

    private static PendingSelection AttachTail(
        PendingSelection pending,
        EffectContext context,
        IReadOnlyList<EffectResolution> remainingQueuedEffects,
        IReadOnlyList<EffectResolution> remainingBackgroundEffects,
        TriggerPipelineOptions options,
        bool wasDrainingBackground) =>
        new(pending.Continuation with
        {
            Context = context,
            RemainingQueuedEffects = pending.Continuation.RemainingQueuedEffects.Concat(remainingQueuedEffects).ToArray(),
            RemainingBackgroundEffects = pending.Continuation.RemainingBackgroundEffects.Concat(remainingBackgroundEffects).ToArray(),
            Options = options,
            WasDrainingBackground = wasDrainingBackground,
        });

    private static PendingSelection AttachNestedTail(PendingSelection pending, TriggerPipelineContinuation tail) =>
        new(pending.Continuation with
        {
            Context = tail.Context,
            RemainingQueuedEffects = pending.Continuation.RemainingQueuedEffects.Concat(tail.RemainingQueuedEffects).ToArray(),
            RemainingBackgroundEffects = pending.Continuation.RemainingBackgroundEffects.Concat(tail.RemainingBackgroundEffects).ToArray(),
            Options = tail.Options,
            WasDrainingBackground = tail.WasDrainingBackground,
        });

    private sealed record TriggerSource(
        CardInstanceId Card,
        CardInstance Instance,
        PermanentId? Permanent,
        PlayerId Controller);

    private sealed record PendingSelection(TriggerPipelineContinuation Continuation);
}
