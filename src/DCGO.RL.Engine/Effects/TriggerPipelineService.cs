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
    bool ExecuteBackgroundEffects = false,
    bool ResolveAfterEffectsActivate = false,
    bool UseMultipleSkillsOrdering = true);

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
    IReadOnlyList<EffectResolution> OrderingCandidates,
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
    private readonly TriggerPipelineOptions _defaultOptions;
    private readonly bool _pauseForOrderingWithoutProvider;

    public TriggerPipelineService(
        ICardScriptRegistry cardScriptRegistry,
        IDecisionProvider? decisionProvider = null,
        TriggerCollector? triggerCollector = null,
        OncePerTurnTracker? oncePerTurnTracker = null,
        SelectionResultApplicator? selectionApplicator = null,
        Tier1PrimitiveService? primitives = null,
        EngineInvariantChecker? invariantChecker = null,
        bool resolveAfterEffectsByDefault = false,
        bool pauseForOrderingWithoutProvider = false)
    {
        _cardScriptRegistry = cardScriptRegistry ?? throw new ArgumentNullException(nameof(cardScriptRegistry));
        _decisionProvider = decisionProvider;
        _triggerCollector = triggerCollector ?? new TriggerCollector();
        _oncePerTurnTracker = oncePerTurnTracker ?? new OncePerTurnTracker();
        _selectionApplicator = selectionApplicator ?? new SelectionResultApplicator();
        _primitives = primitives ?? new Tier1PrimitiveService();
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
        _defaultOptions = new TriggerPipelineOptions(ResolveAfterEffectsActivate: resolveAfterEffectsByDefault);
        _pauseForOrderingWithoutProvider = pauseForOrderingWithoutProvider;
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

        var resolvedOptions = options ?? _defaultOptions;
        var context = new EffectContext(state, timing, player, sourceCard, sourcePermanent, values);
        var descriptors = CollectSourceDescriptors(state, context, resolvedOptions);
        var collected = _triggerCollector.Collect(context, descriptors);
        return RunPrepared(
            state,
            new PreparedTriggerGroup(
                context,
                collected.QueuedEffects,
                collected.BackgroundEffects,
                resolvedOptions),
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

        var resolvedOptions = options ?? _defaultOptions;
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

        if (continuation.PendingStage == EffectDecisionStage.Ordering)
        {
            var orderingPending = ResolveOrderingSelectionAndDrainTail(
                state,
                continuation,
                selectionResult,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);

            return BuildResumeResult(continuation, orderingPending, executedEffects, skippedOptionalEffects, selectionApplications);
        }

        var executedBeforeResume = executedEffects.Count;
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

        if (executedEffects.Count > executedBeforeResume)
        {
            pending = RunAfterEffectsActivate(
                state,
                continuation.PendingResolution.Context,
                continuation.Options,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                pending = AttachNestedTail(pending, continuation);
                return BuildResumeResult(continuation, pending, executedEffects, skippedOptionalEffects, selectionApplications);
            }
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
        return DrainQueuedEffects(
            queue.Pending,
            state,
            backgroundEffects,
            options,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private PendingSelection? DrainQueuedEffects(
        IReadOnlyList<EffectResolution> queuedEffects,
        GameState state,
        IReadOnlyList<EffectResolution> backgroundEffects,
        TriggerPipelineOptions options,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var remaining = queuedEffects.ToList();
        while (remaining.Count > 0)
        {
            var active = remaining
                .Where(resolution => CanActivateNow(state, resolution))
                .ToArray();
            if (active.Length == 0)
            {
                break;
            }

            var candidates = SelectPriorityCandidates(state, active, options);
            EffectResolution resolution;
            if (ShouldSelectEffectOrder(candidates))
            {
                var request = CreateEffectOrderingRequest(state, candidates);
                if (_decisionProvider is null)
                {
                    if (_pauseForOrderingWithoutProvider)
                    {
                        return new PendingSelection(new TriggerPipelineContinuation(
                            candidates[0].Context,
                            candidates[0],
                            request,
                            EffectDecisionStage.Ordering,
                            candidates,
                            remaining,
                            backgroundEffects,
                            options,
                            WasDrainingBackground: false));
                    }

                    resolution = candidates[0];
                    RemoveFirst(remaining, resolution);
                    var executedBeforeAutomaticOrder = executedEffects.Count;
                    var automaticOrderPending = ExecuteResolution(
                        state,
                        resolution,
                        trace,
                        executedEffects,
                        skippedOptionalEffects,
                        selectionApplications);
                    if (automaticOrderPending is not null)
                    {
                        return AttachTail(
                            automaticOrderPending,
                            resolution.Context,
                            remaining,
                            backgroundEffects,
                            options,
                            wasDrainingBackground: false);
                    }

                    if (executedEffects.Count > executedBeforeAutomaticOrder)
                    {
                        automaticOrderPending = RunAfterEffectsActivate(
                            state,
                            resolution.Context,
                            options,
                            trace,
                            executedEffects,
                            skippedOptionalEffects,
                            selectionApplications);
                        if (automaticOrderPending is not null)
                        {
                            return AttachTail(
                                automaticOrderPending,
                                resolution.Context,
                                remaining,
                                backgroundEffects,
                                options,
                                wasDrainingBackground: false);
                        }
                    }

                    continue;
                }

                var orderingResult = _decisionProvider.ChooseSelection(request);
                resolution = ResolveOrderingChoice(candidates, request, orderingResult);
            }
            else
            {
                resolution = candidates[0];
            }

            RemoveFirst(remaining, resolution);
            var executedBefore = executedEffects.Count;
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
                    remaining,
                    backgroundEffects,
                    options,
                    wasDrainingBackground: false);
            }

            if (executedEffects.Count > executedBefore)
            {
                pending = RunAfterEffectsActivate(
                    state,
                    resolution.Context,
                    options,
                    trace,
                    executedEffects,
                    skippedOptionalEffects,
                    selectionApplications);
                if (pending is not null)
                {
                    return AttachTail(
                        pending,
                        resolution.Context,
                        remaining,
                        backgroundEffects,
                        options,
                        wasDrainingBackground: false);
                }
            }
        }

        if (!options.ExecuteBackgroundEffects)
        {
            return null;
        }

        for (var i = 0; i < backgroundEffects.Count; i++)
        {
            var background = backgroundEffects[i];
            var executedBefore = executedEffects.Count;
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

            if (executedEffects.Count > executedBefore)
            {
                pending = RunAfterEffectsActivate(
                    state,
                    background.Context,
                    options,
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
        if (!CanActivateNow(state, resolution))
        {
            return null;
        }

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

    private PendingSelection? ResolveOrderingSelectionAndDrainTail(
        GameState state,
        TriggerPipelineContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var selected = ResolveOrderingChoice(continuation.OrderingCandidates, continuation.PendingSelectionRequest, selectionResult);
        var remaining = continuation.RemainingQueuedEffects.ToList();
        RemoveFirst(remaining, selected);

        var executedBefore = executedEffects.Count;
        var pending = ExecuteResolution(
            state,
            selected,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
        if (pending is not null)
        {
            return AttachNestedTail(pending, continuation with { RemainingQueuedEffects = remaining });
        }

        if (executedEffects.Count > executedBefore)
        {
            pending = RunAfterEffectsActivate(
                state,
                selected.Context,
                continuation.Options,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                return AttachNestedTail(pending, continuation with { RemainingQueuedEffects = remaining });
            }
        }

        return DrainQueuedEffects(
            remaining,
            state,
            continuation.RemainingBackgroundEffects,
            continuation.Options,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private PendingSelection? RunAfterEffectsActivate(
        GameState state,
        EffectContext completedContext,
        TriggerPipelineOptions options,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (!options.ResolveAfterEffectsActivate || completedContext.Timing == EffectTiming.AfterEffectsActivate)
        {
            return null;
        }

        var afterOptions = options with { ResolveAfterEffectsActivate = false };
        var prepared = Prepare(
            state,
            EffectTiming.AfterEffectsActivate,
            state.TurnPlayerId,
            options: afterOptions);

        return DrainQueuedEffects(
            prepared.QueuedEffects,
            state,
            prepared.BackgroundEffects,
            prepared.Options,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private static IReadOnlyList<EffectResolution> SelectPriorityCandidates(
        GameState state,
        IReadOnlyList<EffectResolution> active,
        TriggerPipelineOptions options)
    {
        if (!options.UseMultipleSkillsOrdering || active.Count <= 1)
        {
            return new[] { active[0] };
        }

        var turnPlayer = active
            .Where(resolution => ControllerOf(resolution) == state.TurnPlayerId)
            .ToArray();
        if (turnPlayer.Length > 0)
        {
            return turnPlayer;
        }

        var firstController = active
            .Select(ControllerOf)
            .OrderBy(player => player.Value)
            .First();
        return active
            .Where(resolution => ControllerOf(resolution) == firstController)
            .ToArray();
    }

    private static SelectionRequest CreateEffectOrderingRequest(
        GameState state,
        IReadOnlyList<EffectResolution> candidates)
    {
        var player = ControllerOf(candidates[0]);
        var optionCandidates = candidates
            .Select((resolution, index) => new SelectableTarget(
                SelectionTargetKind.Option,
                $"effect-order:{index}:{resolution.StableId}",
                player,
                resolution.SourceCard,
                resolution.SourcePermanent,
                Label: resolution.StableId,
                OptionValue: index.ToString(),
                Zone: resolution.SourceCard is null || !state.Cards.TryGetValue(resolution.SourceCard.Value, out var card)
                    ? null
                    : card.CurrentZone))
            .ToArray();

        return new SelectionRequest(
            id: $"effect-order:{candidates[0].Context.Timing}:{player.Value}:{string.Join(",", optionCandidates.Select(candidate => candidate.StableId))}",
            player: player,
            selectionKind: SelectionKind.ChooseAction,
            targetKind: SelectionTargetKind.Option,
            minCount: 1,
            maxCount: 1,
            canSkip: false,
            canEndNotMax: false,
            candidates: optionCandidates,
            prompt: "Choose effect order.");
    }

    private static bool ShouldSelectEffectOrder(IReadOnlyList<EffectResolution> candidates)
    {
        if (candidates.Count <= 1)
        {
            return false;
        }

        return candidates
            .Select(resolution => resolution.SourceCard)
            .Where(source => source is not null)
            .Distinct()
            .Count() > 1;
    }

    private static EffectResolution ResolveOrderingChoice(
        IReadOnlyList<EffectResolution> candidates,
        SelectionRequest request,
        SelectionResult result)
    {
        SelectionValidator.Validate(request, result);

        string? selectedOption = result.SelectedOption;
        if (selectedOption is null && result.SelectedTargets.Count == 1)
        {
            selectedOption = result.SelectedTargets[0].OptionValue ?? result.SelectedTargets[0].StableId;
        }

        if (selectedOption is null)
        {
            throw new DomainException($"Effect ordering request '{request.Id}' requires one selected effect.");
        }

        var selectedRequestCandidate = request.Candidates.FirstOrDefault(candidate =>
            string.Equals(candidate.OptionValue, selectedOption, StringComparison.Ordinal)
            || string.Equals(candidate.StableId, selectedOption, StringComparison.Ordinal));
        if (selectedRequestCandidate is null || !int.TryParse(selectedRequestCandidate.OptionValue, out var index))
        {
            throw new DomainException($"Effect ordering result '{selectedOption}' is not bound to a candidate index.");
        }

        if (index < 0 || index >= candidates.Count)
        {
            throw new DomainException($"Effect ordering candidate index '{index}' is out of range.");
        }

        return candidates[index];
    }

    private static bool CanActivateNow(GameState state, EffectResolution resolution)
    {
        if (resolution.SourceCard is not null && !state.Cards.ContainsKey(resolution.SourceCard.Value))
        {
            return false;
        }

        if (resolution.SourcePermanent is not null)
        {
            var permanent = FindPermanent(state, resolution.SourcePermanent.Value);
            if (permanent is null)
            {
                return false;
            }

            if (resolution.SourceCard is not null
                && permanent.TopCardId != resolution.SourceCard.Value
                && !permanent.SourceCardIds.Contains(resolution.SourceCard.Value)
                && !permanent.LinkedCards.Contains(resolution.SourceCard.Value))
            {
                return false;
            }
        }

        return resolution.CanActivate?.Invoke(resolution.Context) ?? true;
    }

    private static PlayerId ControllerOf(EffectResolution resolution) =>
        resolution.Controller ?? resolution.Context.Player ?? PlayerId.Player0;

    private static void RemoveFirst(List<EffectResolution> effects, EffectResolution effect)
    {
        var index = effects.FindIndex(candidate => EqualityComparer<EffectResolution>.Default.Equals(candidate, effect));
        if (index >= 0)
        {
            effects.RemoveAt(index);
            return;
        }

        throw new DomainException($"Effect '{effect.StableId}' is not present in the pending trigger queue.");
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

            if (!CanActivateNow(state, resolution))
            {
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

        if (continuation.PendingStage != EffectDecisionStage.Selection)
        {
            throw new DomainException(
                $"Unsupported pending decision stage '{continuation.PendingStage}' for '{resolution.StableId}'.");
        }

        if (!CanActivateNow(state, resolution))
        {
            return null;
        }

        RegisterOncePerTurnIfNeeded(state, resolution);

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
            var executedBefore = executedEffects.Count;
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

            if (executedEffects.Count > executedBefore)
            {
                pending = RunAfterEffectsActivate(
                    state,
                    background.Context,
                    continuation.Options,
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
