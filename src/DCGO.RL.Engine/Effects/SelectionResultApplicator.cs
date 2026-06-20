using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Validation;

namespace DCGO.RL.Engine.Effects;

public delegate void SelectionResultContinuation(SelectionResultApplicationContext context);

public sealed record SelectionResultApplicationResult(
    string RequestId,
    bool Skipped,
    IReadOnlyList<SelectableTarget> SelectedTargets,
    EffectResolution? NextResolution = null);

public sealed class SelectionResultApplicationContext
{
    public SelectionResultApplicationContext(
        GameState state,
        EffectResolution resolution,
        SelectionRequest request,
        SelectionResult result,
        IReadOnlyList<SelectableTarget> selectedTargets,
        Tier1PrimitiveService primitives,
        GameTrace? trace = null)
    {
        State = state ?? throw new ArgumentNullException(nameof(state));
        Resolution = resolution ?? throw new ArgumentNullException(nameof(resolution));
        Request = request ?? throw new ArgumentNullException(nameof(request));
        Result = result ?? throw new ArgumentNullException(nameof(result));
        SelectedTargets = selectedTargets.ToArray();
        Primitives = primitives ?? throw new ArgumentNullException(nameof(primitives));
        Trace = trace;
    }

    public GameState State { get; }
    public EffectResolution Resolution { get; }
    public SelectionRequest Request { get; }
    public SelectionResult Result { get; }
    public IReadOnlyList<SelectableTarget> SelectedTargets { get; }
    public Tier1PrimitiveService Primitives { get; }
    public GameTrace? Trace { get; }
    public EffectResolution? NextResolution { get; private set; }
    public IReadOnlyList<CardInstanceId> SelectedCardIds =>
        SelectedTargets
            .Where(target => target.Card is not null)
            .Select(target => target.Card!.Value)
            .ToArray();
    public IReadOnlyList<PermanentId> SelectedPermanentIds =>
        SelectedTargets
            .Where(target => target.Permanent is not null)
            .Select(target => target.Permanent!.Value)
            .ToArray();

    public void SetNextResolution(EffectResolution resolution)
    {
        ArgumentNullException.ThrowIfNull(resolution);
        if (NextResolution is not null)
        {
            throw new DomainException(
                $"Selection continuation for '{Resolution.StableId}' already set next resolution '{NextResolution.StableId}'.");
        }

        if (resolution.PendingSelectionRequest is null)
        {
            throw new DomainException(
                $"Next resolution '{resolution.StableId}' must expose a selection request.");
        }

        NextResolution = resolution;
    }
}

public sealed class SelectionResultApplicator
{
    private readonly EngineInvariantChecker _invariantChecker;

    public SelectionResultApplicator(EngineInvariantChecker? invariantChecker = null)
    {
        _invariantChecker = invariantChecker ?? new EngineInvariantChecker();
    }

    public SelectionResultApplicationResult Apply(
        GameState state,
        EffectResolution resolution,
        SelectionResult result,
        Tier1PrimitiveService primitives,
        GameTrace? trace = null)
    {
        var request = RequireSingleSelectionRequest(resolution);
        return Apply(state, resolution, request, result, primitives, trace);
    }

    public SelectionResultApplicationResult Apply(
        GameState state,
        EffectResolution resolution,
        SelectionRequest request,
        SelectionResult result,
        Tier1PrimitiveService primitives,
        GameTrace? trace = null)
    {
        ArgumentNullException.ThrowIfNull(state);
        ArgumentNullException.ThrowIfNull(resolution);
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(result);
        ArgumentNullException.ThrowIfNull(primitives);

        _invariantChecker.ThrowIfInvalid(state);
        SelectionValidator.Validate(request, result);

        var selectedTargets = CanonicalizeSelectedTargets(request, result);
        foreach (var target in selectedTargets)
        {
            RevalidateSelectedTarget(state, target);
        }

        var skipped = IsExplicitSkip(request, result);
        if (skipped)
        {
            return new SelectionResultApplicationResult(request.Id, Skipped: true, selectedTargets);
        }

        if (resolution.SelectionContinuation is null)
        {
            throw new DomainException(
                $"Effect resolution '{resolution.StableId}' has no selection continuation for request '{request.Id}'.");
        }

        var context = new SelectionResultApplicationContext(
            state,
            resolution,
            request,
            result,
            selectedTargets,
            primitives,
            trace);

        resolution.SelectionContinuation(context);
        _invariantChecker.ThrowIfInvalid(state);

        return new SelectionResultApplicationResult(request.Id, Skipped: false, selectedTargets, context.NextResolution);
    }

    private static SelectionRequest RequireSingleSelectionRequest(EffectResolution resolution)
    {
        if (resolution.SelectionRequest is not null && resolution.OptionalSelectionRequest is not null)
        {
            throw new DomainException(
                $"Effect resolution '{resolution.StableId}' has both optional and explicit selection requests.");
        }

        return resolution.PendingSelectionRequest
            ?? throw new DomainException($"Effect resolution '{resolution.StableId}' has no selection request.");
    }

    private static IReadOnlyList<SelectableTarget> CanonicalizeSelectedTargets(
        SelectionRequest request,
        SelectionResult result)
    {
        if (result.SelectedTargets.Count == 0)
        {
            return Array.Empty<SelectableTarget>();
        }

        var candidatesByStableId = new Dictionary<string, SelectableTarget>(StringComparer.Ordinal);
        foreach (var candidate in request.Candidates)
        {
            if (!candidatesByStableId.TryAdd(candidate.StableId, candidate))
            {
                throw new DomainException(
                    $"SelectionRequest '{request.Id}' contains duplicate candidate stable id '{candidate.StableId}'.");
            }
        }

        return result.SelectedTargets
            .Select(target => candidatesByStableId[target.StableId])
            .ToArray();
    }

    private static bool IsExplicitSkip(SelectionRequest request, SelectionResult result) =>
        request.CanSkip
        && result.SelectedTargets.Count == 0
        && result.SelectedCount is null
        && result.SelectedBoolean is null
        && string.IsNullOrWhiteSpace(result.SelectedOption);

    private static void RevalidateSelectedTarget(GameState state, SelectableTarget target)
    {
        switch (target.Kind)
        {
            case SelectionTargetKind.Card:
            case SelectionTargetKind.Security:
                RevalidateCardTarget(state, target);
                return;

            case SelectionTargetKind.Permanent:
                RevalidatePermanentTarget(state, target);
                return;

            case SelectionTargetKind.FieldSlot:
                RevalidateFieldSlotTarget(state, target);
                return;

            case SelectionTargetKind.Count:
            case SelectionTargetKind.Boolean:
            case SelectionTargetKind.Option:
                return;

            default:
                throw new DomainException($"Unsupported selection target kind '{target.Kind}'.");
        }
    }

    private static void RevalidateCardTarget(GameState state, SelectableTarget target)
    {
        if (target.Card is null)
        {
            throw new DomainException($"Selected target '{target.StableId}' does not reference a card.");
        }

        if (!state.Cards.TryGetValue(target.Card.Value, out var card))
        {
            throw new DomainException($"Selected card '{target.Card}' no longer exists.");
        }

        if (target.Owner is not null && card.Owner != target.Owner.Value)
        {
            throw new DomainException(
                $"Selected card '{target.Card}' owner changed from '{target.Owner}' to '{card.Owner}'.");
        }

        if (target.Zone is not null && card.CurrentZone != target.Zone.Value)
        {
            throw new DomainException(
                $"Selected card '{target.Card}' moved from '{target.Zone}' to '{card.CurrentZone}' before selection application.");
        }

        if (card.PermanentId is not null)
        {
            var permanent = TryFindPermanent(state, card.PermanentId.Value);
            if (permanent is null || !PermanentContainsCard(permanent, target.Card.Value))
            {
                throw new DomainException(
                    $"Selected card '{target.Card}' permanent link '{card.PermanentId}' is no longer valid.");
            }
        }
    }

    private static void RevalidatePermanentTarget(GameState state, SelectableTarget target)
    {
        if (target.Permanent is null)
        {
            throw new DomainException($"Selected target '{target.StableId}' does not reference a permanent.");
        }

        var permanent = TryFindPermanent(state, target.Permanent.Value);
        if (permanent is null)
        {
            throw new DomainException($"Selected permanent '{target.Permanent}' no longer exists.");
        }

        if (target.Owner is not null && permanent.ControllerPlayerId != target.Owner.Value)
        {
            throw new DomainException(
                $"Selected permanent '{target.Permanent}' controller changed from '{target.Owner}' to '{permanent.ControllerPlayerId}'.");
        }

        if (!state.Cards.TryGetValue(permanent.TopCardId, out var topCard))
        {
            throw new DomainException($"Selected permanent '{target.Permanent}' top card no longer exists.");
        }

        var currentZone = permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea;
        if (topCard.CurrentZone != currentZone)
        {
            throw new DomainException(
                $"Selected permanent '{target.Permanent}' top card zone is '{topCard.CurrentZone}', expected '{currentZone}'.");
        }

        if (target.Zone is not null && currentZone != target.Zone.Value)
        {
            throw new DomainException(
                $"Selected permanent '{target.Permanent}' moved from '{target.Zone}' to '{currentZone}' before selection application.");
        }
    }

    private static void RevalidateFieldSlotTarget(GameState state, SelectableTarget target)
    {
        if (target.FieldSlotIndex is null)
        {
            throw new DomainException($"Selected target '{target.StableId}' does not reference a field slot.");
        }

        if (target.FieldSlotIndex.Value < 0 || target.FieldSlotIndex.Value >= state.Config.FieldSlotCount)
        {
            throw new DomainException($"Selected field slot '{target.FieldSlotIndex}' is outside the configured field.");
        }
    }

    private static PermanentState? TryFindPermanent(GameState state, PermanentId permanentId) =>
        state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(permanent => permanent.Id == permanentId);

    private static bool PermanentContainsCard(PermanentState permanent, CardInstanceId card) =>
        permanent.TopCardId == card
        || permanent.SourceCardIds.Contains(card)
        || permanent.LinkedCards.Contains(card);
}
