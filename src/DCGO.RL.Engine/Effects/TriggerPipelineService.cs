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
    Linked = 64,
}

public sealed record TriggerPipelineOptions(
    TriggerSourceZone SourceZones = TriggerSourceZone.FieldTop
        | TriggerSourceZone.Inherited
        | TriggerSourceZone.Linked
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

public enum TriggerStackFrameKind
{
    Batch,
    Background,
    AfterEffectsActivate,
}

public sealed record TriggerStackFrame
{
    public TriggerStackFrame(
        EffectContext context,
        IReadOnlyList<EffectResolution> remainingEffects,
        IReadOnlyList<EffectResolution> backgroundEffects,
        TriggerPipelineOptions options,
        TriggerStackFrame? parentFrame = null,
        TriggerStackFrameKind kind = TriggerStackFrameKind.Batch,
        IReadOnlyList<EffectResolution>? orderingCandidates = null,
        bool afterEffectsActivateScheduled = false,
        int depth = 0,
        bool hadCandidate = false,
        bool hadResolutionAttempt = false,
        IReadOnlyList<string>? afterEffectsCandidateSignatures = null)
    {
        Context = context;
        RemainingEffects = remainingEffects.ToArray();
        BackgroundEffects = backgroundEffects.ToArray();
        Options = options;
        ParentFrame = parentFrame;
        Kind = kind;
        OrderingCandidates = orderingCandidates?.ToArray() ?? Array.Empty<EffectResolution>();
        AfterEffectsActivateScheduled = afterEffectsActivateScheduled;
        Depth = depth;
        HadCandidate = hadCandidate;
        HadResolutionAttempt = hadResolutionAttempt;
        AfterEffectsCandidateSignatures = afterEffectsCandidateSignatures?.ToArray()
            ?? Array.Empty<string>();
    }

    public EffectContext Context { get; init; }
    public IReadOnlyList<EffectResolution> RemainingEffects { get; init; }
    public IReadOnlyList<EffectResolution> BackgroundEffects { get; init; }
    public TriggerPipelineOptions Options { get; init; }
    public TriggerStackFrame? ParentFrame { get; init; }
    public TriggerStackFrameKind Kind { get; init; }
    public IReadOnlyList<EffectResolution> OrderingCandidates { get; init; }
    public bool AfterEffectsActivateScheduled { get; init; }
    public int Depth { get; init; }
    public bool HadCandidate { get; init; }
    public bool HadResolutionAttempt { get; init; }
    public IReadOnlyList<string> AfterEffectsCandidateSignatures { get; init; }
}

public sealed record TriggerPipelineContinuation(
    EffectResolution PendingResolution,
    SelectionRequest PendingSelectionRequest,
    EffectDecisionStage PendingStage,
    TriggerStackFrame Frame)
{
    public EffectContext Context => Frame.Context;
    public IReadOnlyList<EffectResolution> OrderingCandidates => Frame.OrderingCandidates;
    public IReadOnlyList<EffectResolution> RemainingQueuedEffects => Frame.RemainingEffects;
    public IReadOnlyList<EffectResolution> RemainingBackgroundEffects => Frame.BackgroundEffects;
    public TriggerPipelineOptions Options => Frame.Options;
    public bool WasDrainingBackground => Frame.Kind == TriggerStackFrameKind.Background;
    public string StableContinuationId => PendingResolution.StableId;
}

public sealed record PreparedTriggerGroup(
    EffectContext Context,
    IReadOnlyList<EffectResolution> QueuedEffects,
    IReadOnlyList<EffectResolution> BackgroundEffects,
    TriggerPipelineOptions Options);

public sealed class TriggerPipelineService
{
    private const int MaxTriggerStackDepth = 64;

    private readonly ICardScriptRegistry _cardScriptRegistry;
    private readonly IDecisionProvider? _decisionProvider;
    private readonly TriggerCollector _triggerCollector;
    private readonly OncePerTurnTracker _oncePerTurnTracker;
    private readonly SelectionResultApplicator _selectionApplicator;
    private readonly Tier1PrimitiveService _primitives;
    private readonly EngineInvariantChecker _invariantChecker;
    private readonly TriggerPipelineOptions _defaultOptions;
    private readonly bool _pauseForOrderingWithoutProvider;
    private Func<GameState, GameTrace?, RuleStabilizationResult>? _stateOnlyStabilizer;

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

    public void AttachStateOnlyStabilizer(Func<GameState, GameTrace?, RuleStabilizationResult> stabilizer)
    {
        _stateOnlyStabilizer = stabilizer ?? throw new ArgumentNullException(nameof(stabilizer));
    }

    public TriggerPipelineResult RunAutoProcess(
        GameState state,
        PlayerId? player = null,
        TriggerPipelineOptions? options = null,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var resolvedOptions = options ?? _defaultOptions;
        var stabilization = _stateOnlyStabilizer?.Invoke(state, trace)
            ?? RuleStabilizationResult.Empty;
        var preparedGroups = new List<PreparedTriggerGroup>();
        preparedGroups.AddRange(PrepareRuleEventGroups(state, stabilization.Events, resolvedOptions));

        var rulesTiming = Prepare(
            state,
            EffectTiming.RulesTiming,
            player ?? state.TurnPlayerId,
            options: resolvedOptions);
        if (HasPreparedEffects(rulesTiming))
        {
            preparedGroups.Add(rulesTiming);
        }

        var context = rulesTiming.Context;
        if (preparedGroups.Count == 0)
        {
            return new TriggerPipelineResult(
                context,
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<EffectResolution>(),
                Array.Empty<SelectionResultApplicationResult>());
        }

        return RunPreparedSequence(state, context, preparedGroups, trace);
    }

    public TriggerPipelineResult Run(
        GameState state,
        EffectTiming timing,
        PlayerId? player = null,
        CardInstanceId? sourceCard = null,
        PermanentId? sourcePermanent = null,
        IReadOnlyDictionary<string, object?>? values = null,
        TriggerPipelineOptions? options = null,
        Func<EffectDescriptor, bool>? descriptorFilter = null,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var resolvedOptions = options ?? _defaultOptions;
        var context = new EffectContext(state, timing, player, sourceCard, sourcePermanent, values);
        var descriptors = CollectSourceDescriptors(state, context, resolvedOptions);
        if (descriptorFilter is not null)
        {
            descriptors = descriptors.Where(descriptorFilter).ToArray();
        }

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
        TriggerPipelineOptions? options = null,
        Func<EffectDescriptor, bool>? descriptorFilter = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var resolvedOptions = options ?? _defaultOptions;
        var context = new EffectContext(state, timing, player, sourceCard, sourcePermanent, values);
        var descriptors = CollectSourceDescriptors(state, context, resolvedOptions);
        if (descriptorFilter is not null)
        {
            descriptors = descriptors.Where(descriptorFilter).ToArray();
        }

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

        var executedEffects = new List<EffectResolution>();
        var skippedOptionalEffects = new List<EffectResolution>();
        var selectionApplications = new List<SelectionResultApplicationResult>();

        var rootFrame = CreateFrame(prepared, parentFrame: null, TriggerStackFrameKind.Batch, depth: 0);
        var outcome = DrainFrame(
            rootFrame,
            state,
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
            outcome.Pending?.Continuation.PendingResolution,
            outcome.Pending?.Continuation.PendingSelectionRequest,
            outcome.Pending?.Continuation);
    }

    private TriggerPipelineResult RunPreparedSequence(
        GameState state,
        EffectContext resultContext,
        IReadOnlyList<PreparedTriggerGroup> preparedGroups,
        GameTrace? trace)
    {
        var executedEffects = new List<EffectResolution>();
        var skippedOptionalEffects = new List<EffectResolution>();
        var selectionApplications = new List<SelectionResultApplicationResult>();
        var rootFrame = CreateFrameChain(preparedGroups, parentFrame: null, depthBase: -1)
            ?? throw new DomainException("Prepared trigger sequence must contain at least one group.");

        var outcome = DrainFrame(
            rootFrame,
            state,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);

        return new TriggerPipelineResult(
            resultContext,
            preparedGroups.SelectMany(group => group.QueuedEffects).ToArray(),
            preparedGroups.SelectMany(group => group.BackgroundEffects).ToArray(),
            executedEffects,
            skippedOptionalEffects,
            selectionApplications,
            outcome.Pending?.Continuation.PendingResolution,
            outcome.Pending?.Continuation.PendingSelectionRequest,
            outcome.Pending?.Continuation);
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

        FrameDrainOutcome outcome;
        if (continuation.PendingStage == EffectDecisionStage.Ordering)
        {
            outcome = ResolveOrderingSelectionAndDrainFrame(
                state,
                continuation,
                selectionResult,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);

            return BuildResumeResult(continuation, outcome.Pending, executedEffects, skippedOptionalEffects, selectionApplications);
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
            var resumeFrame = continuation.Frame with { HadResolutionAttempt = true };
            var autoProcessOutcome = RunAutoProcessAfterEffect(
                state,
                resumeFrame,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (autoProcessOutcome is not null)
            {
                return BuildResumeResult(
                    continuation,
                    autoProcessOutcome.Pending,
                    executedEffects,
                    skippedOptionalEffects,
                    selectionApplications);
            }
        }

        outcome = DrainFrame(
            executedEffects.Count > executedBeforeResume
                ? continuation.Frame with { HadResolutionAttempt = true }
                : continuation.Frame,
            state,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);

        return BuildResumeResult(continuation, outcome.Pending, executedEffects, skippedOptionalEffects, selectionApplications);
    }

    private FrameDrainOutcome DrainFrame(
        TriggerStackFrame frame,
        GameState state,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var current = frame;
        while (current is not null)
        {
            if (current.Depth > MaxTriggerStackDepth)
            {
                throw new UnsupportedMechanicException(
                    $"Trigger stack exceeded max depth '{MaxTriggerStackDepth}'.");
            }

            var remaining = current.RemainingEffects.ToList();
            var active = remaining
                .Where(resolution => CanActivateNow(state, resolution))
                .ToArray();
            if (active.Length == 0)
            {
                current = CompleteFrameOrScheduleAfterEffects(state, current);
                continue;
            }

            var candidates = SelectPriorityCandidates(state, active, current.Options);
            EffectResolution? resolution;
            if (ShouldSelectEffectOrder(candidates))
            {
                var request = CreateEffectOrderingRequest(state, candidates);
                if (_decisionProvider is null)
                {
                    if (_pauseForOrderingWithoutProvider)
                    {
                        return FrameDrainOutcome.ForPending(new PendingSelection(new TriggerPipelineContinuation(
                            candidates[0],
                            request,
                            EffectDecisionStage.Ordering,
                            current with { OrderingCandidates = candidates })));
                    }

                    resolution = candidates[0];
                }
                else
                {
                    var orderingResult = _decisionProvider.ChooseSelection(request);
                    resolution = ResolveOrderingChoice(candidates, request, orderingResult);
                    if (resolution is null)
                    {
                        RemoveAll(remaining, candidates);
                        skippedOptionalEffects.AddRange(candidates);
                        current = current with
                        {
                            RemainingEffects = remaining.ToArray(),
                            OrderingCandidates = Array.Empty<EffectResolution>(),
                        };
                        continue;
                    }
                }
            }
            else
            {
                resolution = candidates[0];
            }

            RemoveFirst(remaining, resolution);
            current = current with
            {
                RemainingEffects = remaining.ToArray(),
                OrderingCandidates = Array.Empty<EffectResolution>(),
            };

            var executedBefore = executedEffects.Count;
            var pending = ExecuteResolution(
                state,
                resolution,
                current,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (pending is not null)
            {
                return FrameDrainOutcome.ForPending(pending);
            }

            if (executedEffects.Count > executedBefore)
            {
                current = current with { HadResolutionAttempt = true };
                var autoProcessOutcome = RunAutoProcessAfterEffect(
                    state,
                    current,
                    trace,
                    executedEffects,
                    skippedOptionalEffects,
                    selectionApplications);
                if (autoProcessOutcome is not null)
                {
                    return autoProcessOutcome;
                }
            }
        }

        return FrameDrainOutcome.Completed;
    }

    private PendingSelection? ExecuteResolution(
        GameState state,
        EffectResolution resolution,
        TriggerStackFrame frame,
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
                return CreatePendingSelection(resolution, resolution.OptionalSelectionRequest, EffectDecisionStage.Optional, frame);
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
                return CreatePendingSelection(resolution, resolution.SelectionRequest, EffectDecisionStage.Selection, frame);
            }
        }

        if (resolution.SelectionRequest is not null)
        {
            if (_decisionProvider is null)
            {
                return CreatePendingSelection(resolution, resolution.SelectionRequest, EffectDecisionStage.Selection, frame);
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
                    frame,
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

    private FrameDrainOutcome ResolveOrderingSelectionAndDrainFrame(
        GameState state,
        TriggerPipelineContinuation continuation,
        SelectionResult selectionResult,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        var selected = ResolveOrderingChoice(continuation.OrderingCandidates, continuation.PendingSelectionRequest, selectionResult);
        var remaining = continuation.Frame.RemainingEffects.ToList();
        if (selected is null)
        {
            RemoveAll(remaining, continuation.OrderingCandidates);
            skippedOptionalEffects.AddRange(continuation.OrderingCandidates);
            return DrainFrame(
                continuation.Frame with
                {
                    RemainingEffects = remaining.ToArray(),
                    OrderingCandidates = Array.Empty<EffectResolution>(),
                },
                state,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
        }

        RemoveFirst(remaining, selected);
        var frame = continuation.Frame with
        {
            RemainingEffects = remaining.ToArray(),
            OrderingCandidates = Array.Empty<EffectResolution>(),
        };

        var executedBefore = executedEffects.Count;
        var pending = ExecuteResolution(
            state,
            selected,
            frame,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
        if (pending is not null)
        {
            return FrameDrainOutcome.ForPending(pending);
        }

        if (executedEffects.Count > executedBefore)
        {
            frame = frame with { HadResolutionAttempt = true };
            var autoProcessOutcome = RunAutoProcessAfterEffect(
                state,
                frame,
                trace,
                executedEffects,
                skippedOptionalEffects,
                selectionApplications);
            if (autoProcessOutcome is not null)
            {
                return autoProcessOutcome;
            }
        }

        return DrainFrame(
            frame,
            state,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private FrameDrainOutcome? RunAutoProcessAfterEffect(
        GameState state,
        TriggerStackFrame parentFrame,
        GameTrace? trace,
        List<EffectResolution> executedEffects,
        List<EffectResolution> skippedOptionalEffects,
        List<SelectionResultApplicationResult> selectionApplications)
    {
        if (_stateOnlyStabilizer is null)
        {
            return null;
        }

        var stabilization = _stateOnlyStabilizer.Invoke(state, trace);
        var preparedGroups = new List<PreparedTriggerGroup>();
        preparedGroups.AddRange(PrepareRuleEventGroups(state, stabilization.Events, parentFrame.Options));

        var shouldCollectRulesTiming =
            parentFrame.Context.Timing != EffectTiming.RulesTiming
            || stabilization.ChangesApplied != 0
            || preparedGroups.Count > 0;
        if (shouldCollectRulesTiming)
        {
            var rulesTiming = Prepare(
                state,
                EffectTiming.RulesTiming,
                state.TurnPlayerId,
                options: parentFrame.Options);
            if (HasPreparedEffects(rulesTiming))
            {
                preparedGroups.Add(rulesTiming);
            }
        }

        if (preparedGroups.Count == 0)
        {
            return null;
        }

        return DrainFrame(
            CreateFrameChain(preparedGroups, parentFrame, parentFrame.Depth)
                ?? throw new DomainException("Auto-process trigger sequence must contain at least one group."),
            state,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
    }

    private IReadOnlyList<PreparedTriggerGroup> PrepareRuleEventGroups(
        GameState state,
        IReadOnlyList<RuleStabilizationEvent> events,
        TriggerPipelineOptions options) =>
        events
            .Select(ruleEvent => Prepare(
                state,
                ruleEvent.Timing,
                ruleEvent.Player,
                values: ruleEvent.Values,
                options: options))
            .Where(HasPreparedEffects)
            .ToArray();

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
        var canSkip = candidates.All(candidate => candidate.IsOptional);
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
            canSkip: canSkip,
            canEndNotMax: canSkip,
            candidates: optionCandidates,
            prompt: "Choose effect order.");
    }

    private static bool ShouldSelectEffectOrder(IReadOnlyList<EffectResolution> candidates) =>
        candidates.Count > 1;

    private static EffectResolution? ResolveOrderingChoice(
        IReadOnlyList<EffectResolution> candidates,
        SelectionRequest request,
        SelectionResult result)
    {
        SelectionValidator.Validate(request, result);

        if (request.CanSkip
            && string.IsNullOrWhiteSpace(result.SelectedOption)
            && result.SelectedTargets.Count == 0)
        {
            return null;
        }

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

        if (resolution.SourceSnapshot is not null
            && resolution.SourcePersistencePolicy == TriggerSourcePersistencePolicy.RequireSameRole
            && !IsSourceSnapshotStillValid(state, resolution.SourceSnapshot))
        {
            return false;
        }

        if (resolution.SourcePermanent is not null
            && resolution.SourcePersistencePolicy == TriggerSourcePersistencePolicy.RequireSameRole)
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

    private static bool IsSourceSnapshotStillValid(GameState state, TriggerSourceSnapshot snapshot)
    {
        if (!state.Cards.TryGetValue(snapshot.SourceCard, out var card))
        {
            return false;
        }

        if (card.Owner != snapshot.Owner)
        {
            return false;
        }

        var permanent = snapshot.SourcePermanent is null
            ? null
            : FindPermanent(state, snapshot.SourcePermanent.Value);

        return snapshot.Role switch
        {
            TriggerSourceRole.FieldTop =>
                permanent is not null
                && permanent.ControllerPlayerId == snapshot.Controller
                && permanent.TopCardId == snapshot.SourceCard
                && snapshot.TopCardWhenTriggered == permanent.TopCardId
                && card.CurrentZone == snapshot.SourceZone,

            TriggerSourceRole.Inherited =>
                permanent is not null
                && permanent.ControllerPlayerId == snapshot.Controller
                && permanent.TopCardId == snapshot.TopCardWhenTriggered
                && permanent.SourceCardIds.Contains(snapshot.SourceCard)
                && card.CurrentZone == Zone.EvolutionSources,

            TriggerSourceRole.Linked =>
                permanent is not null
                && permanent.ControllerPlayerId == snapshot.Controller
                && permanent.TopCardId == snapshot.TopCardWhenTriggered
                && permanent.LinkedCards.Contains(snapshot.SourceCard)
                && card.CurrentZone == Zone.LinkedCards,

            TriggerSourceRole.Hand =>
                card.CurrentZone == Zone.Hand && state.GetPlayer(snapshot.Owner).Hand.Contains(snapshot.SourceCard),

            TriggerSourceRole.Trash =>
                card.CurrentZone == Zone.Trash && state.GetPlayer(snapshot.Owner).Trash.Contains(snapshot.SourceCard),

            TriggerSourceRole.Executing =>
                card.CurrentZone == Zone.Executing && state.GetPlayer(snapshot.Owner).Executing.Contains(snapshot.SourceCard),

            TriggerSourceRole.FaceUpSecurity =>
                card.CurrentZone == Zone.Security
                && card.IsFaceUp
                && state.GetPlayer(snapshot.Owner).Security.Contains(snapshot.SourceCard),

            _ => true,
        };
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
            descriptors.AddRange(script
                .CreateEffectDescriptors(new CardScriptContext(
                    state,
                    source.Card,
                    source.Permanent,
                    source.Controller))
                .Select(descriptor => descriptor with
                {
                    SourceSnapshot = descriptor.SourceSnapshot ?? source.Snapshot,
                }));
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
                    yield return CreatePermanentSource(state, permanent.TopCardId, permanent, TriggerSourceRole.FieldTop);
                }

                if (sourceZones.HasFlag(TriggerSourceZone.Inherited))
                {
                    foreach (var sourceCard in permanent.SourceCardIds)
                    {
                        yield return CreatePermanentSource(state, sourceCard, permanent, TriggerSourceRole.Inherited);
                    }
                }

                if (sourceZones.HasFlag(TriggerSourceZone.Linked))
                {
                    foreach (var linkedCard in permanent.LinkedCards)
                    {
                        yield return CreatePermanentSource(state, linkedCard, permanent, TriggerSourceRole.Linked);
                    }
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Hand))
            {
                foreach (var card in player.Hand)
                {
                    yield return CreatePlayerZoneSource(state, card, TriggerSourceRole.Hand);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Trash))
            {
                foreach (var card in player.Trash)
                {
                    yield return CreatePlayerZoneSource(state, card, TriggerSourceRole.Trash);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.Executing))
            {
                foreach (var card in player.Executing)
                {
                    yield return CreatePlayerZoneSource(state, card, TriggerSourceRole.Executing);
                }
            }

            if (sourceZones.HasFlag(TriggerSourceZone.FaceUpSecurity))
            {
                foreach (var card in player.Security.Where(card => state.Cards[card].IsFaceUp))
                {
                    yield return CreatePlayerZoneSource(state, card, TriggerSourceRole.FaceUpSecurity);
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
        return new TriggerSource(
            card,
            instance,
            permanent?.Id,
            controller,
            CreateSourceSnapshot(instance, permanent, controller, RoleFromZone(instance.CurrentZone)));
    }

    private static TriggerSource CreatePermanentSource(
        GameState state,
        CardInstanceId card,
        PermanentState permanent,
        TriggerSourceRole role)
    {
        var instance = RequireCard(state, card);
        return new TriggerSource(
            card,
            instance,
            permanent.Id,
            permanent.ControllerPlayerId,
            CreateSourceSnapshot(instance, permanent, permanent.ControllerPlayerId, role));
    }

    private static TriggerSource CreatePlayerZoneSource(GameState state, CardInstanceId card, TriggerSourceRole role)
    {
        var instance = RequireCard(state, card);
        return new TriggerSource(
            card,
            instance,
            null,
            instance.Owner,
            CreateSourceSnapshot(instance, permanent: null, instance.Owner, role));
    }

    private static TriggerSourceSnapshot CreateSourceSnapshot(
        CardInstance instance,
        PermanentState? permanent,
        PlayerId controller,
        TriggerSourceRole role) =>
        new(
            role,
            instance.CurrentZone,
            instance.Id,
            permanent?.Id,
            permanent?.TopCardId,
            instance.Owner,
            controller);

    private static TriggerSourceRole RoleFromZone(Zone zone) =>
        zone switch
        {
            Zone.Hand => TriggerSourceRole.Hand,
            Zone.Trash => TriggerSourceRole.Trash,
            Zone.Executing => TriggerSourceRole.Executing,
            Zone.Security => TriggerSourceRole.FaceUpSecurity,
            Zone.EvolutionSources => TriggerSourceRole.Inherited,
            Zone.LinkedCards => TriggerSourceRole.Linked,
            Zone.BattleArea or Zone.BreedingArea => TriggerSourceRole.FieldTop,
            _ => TriggerSourceRole.Unknown,
        };

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
                return CreatePendingSelection(
                    resolution,
                    resolution.SelectionRequest,
                    EffectDecisionStage.Selection,
                    continuation.Frame);
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
            continuation.Frame,
            trace,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications);
        return pending;
    }

    private static TriggerPipelineResult BuildResumeResult(
        TriggerPipelineContinuation continuation,
        PendingSelection? pending,
        IReadOnlyList<EffectResolution> executedEffects,
        IReadOnlyList<EffectResolution> skippedOptionalEffects,
        IReadOnlyList<SelectionResultApplicationResult> selectionApplications) =>
        new(
            pending?.Continuation.Context ?? continuation.Context,
            pending?.Continuation.RemainingQueuedEffects ?? continuation.RemainingQueuedEffects,
            pending?.Continuation.RemainingBackgroundEffects ?? continuation.RemainingBackgroundEffects,
            executedEffects,
            skippedOptionalEffects,
            selectionApplications,
            pending?.Continuation.PendingResolution,
            pending?.Continuation.PendingSelectionRequest,
            pending?.Continuation);

    private static PendingSelection CreatePendingSelection(
        EffectResolution resolution,
        SelectionRequest request,
        EffectDecisionStage stage,
        TriggerStackFrame frame) =>
        new(new TriggerPipelineContinuation(
            resolution,
            request,
            stage,
            frame));

    private TriggerStackFrame? CompleteFrameOrScheduleAfterEffects(GameState state, TriggerStackFrame frame)
    {
        if (frame.Kind == TriggerStackFrameKind.Batch
            && frame.Options.ExecuteBackgroundEffects
            && frame.BackgroundEffects.Count > 0)
        {
            return new TriggerStackFrame(
                frame.Context,
                frame.BackgroundEffects,
                Array.Empty<EffectResolution>(),
                frame.Options,
                frame.ParentFrame,
                TriggerStackFrameKind.Background,
                depth: frame.Depth,
                hadCandidate: frame.BackgroundEffects.Count > 0,
                hadResolutionAttempt: false,
                afterEffectsCandidateSignatures: frame.AfterEffectsCandidateSignatures);
        }

        if (ShouldScheduleAfterEffects(frame))
        {
            var afterFrame = CreateAfterEffectsFrame(state, frame);
            if (afterFrame is not null)
            {
                return afterFrame;
            }
        }

        return frame.ParentFrame;
    }

    private bool ShouldScheduleAfterEffects(TriggerStackFrame frame) =>
        frame.Options.ResolveAfterEffectsActivate
        && frame.HadResolutionAttempt
        && !frame.AfterEffectsActivateScheduled;

    private TriggerStackFrame? CreateAfterEffectsFrame(GameState state, TriggerStackFrame completedFrame)
    {
        var prepared = Prepare(
            state,
            EffectTiming.AfterEffectsActivate,
            state.TurnPlayerId,
            options: completedFrame.Options);
        if (!HasPreparedEffects(prepared))
        {
            return null;
        }

        var signature = CreateAfterEffectsSignature(prepared);
        if (completedFrame.AfterEffectsCandidateSignatures.Contains(signature, StringComparer.Ordinal))
        {
            throw new UnsupportedMechanicException(
                $"Repeated AfterEffectsActivate candidate set detected: '{signature}'.");
        }

        return CreateFrame(
            prepared,
            completedFrame.ParentFrame,
            TriggerStackFrameKind.AfterEffectsActivate,
            completedFrame.Depth + 1,
            completedFrame.AfterEffectsCandidateSignatures.Concat(new[] { signature }).ToArray());
    }

    private static TriggerStackFrame CreateFrame(
        PreparedTriggerGroup prepared,
        TriggerStackFrame? parentFrame,
        TriggerStackFrameKind kind,
        int depth,
        IReadOnlyList<string>? afterEffectsCandidateSignatures = null) =>
        new(
            prepared.Context,
            prepared.QueuedEffects,
            prepared.BackgroundEffects,
            prepared.Options,
            parentFrame,
            kind,
            depth: depth,
            hadCandidate: HasPreparedEffects(prepared),
            hadResolutionAttempt: false,
            afterEffectsCandidateSignatures: afterEffectsCandidateSignatures
                ?? parentFrame?.AfterEffectsCandidateSignatures);

    private static TriggerStackFrame? CreateFrameChain(
        IReadOnlyList<PreparedTriggerGroup> preparedGroups,
        TriggerStackFrame? parentFrame,
        int depthBase)
    {
        TriggerStackFrame? next = parentFrame;
        for (var index = preparedGroups.Count - 1; index >= 0; index--)
        {
            next = CreateFrame(
                preparedGroups[index],
                next,
                TriggerStackFrameKind.Batch,
                depthBase + index + 1,
                parentFrame?.AfterEffectsCandidateSignatures);
        }

        return next;
    }

    private static string CreateAfterEffectsSignature(PreparedTriggerGroup prepared) =>
        string.Join(
            "|",
            prepared.QueuedEffects
                .Concat(prepared.BackgroundEffects)
                .Select(effect => effect.StableId)
                .OrderBy(stableId => stableId, StringComparer.Ordinal));

    private static bool HasPreparedEffects(PreparedTriggerGroup prepared) =>
        prepared.QueuedEffects.Count > 0 || prepared.BackgroundEffects.Count > 0;

    private static void RemoveAll(
        List<EffectResolution> effects,
        IReadOnlyList<EffectResolution> removals)
    {
        foreach (var removal in removals)
        {
            RemoveFirst(effects, removal);
        }
    }

    private sealed record TriggerSource(
        CardInstanceId Card,
        CardInstance Instance,
        PermanentId? Permanent,
        PlayerId Controller,
        TriggerSourceSnapshot Snapshot);

    private sealed record FrameDrainOutcome(PendingSelection? Pending)
    {
        public static FrameDrainOutcome Completed { get; } = new((PendingSelection?)null);

        public static FrameDrainOutcome ForPending(PendingSelection pending) => new(pending);
    }

    private sealed record PendingSelection(TriggerPipelineContinuation Continuation);
}
