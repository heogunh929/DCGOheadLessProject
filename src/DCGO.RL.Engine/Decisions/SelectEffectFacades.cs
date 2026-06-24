using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.Decisions;

public sealed class SelectCardEffect
{
    public enum Mode
    {
        AddHand,
        Discard,
        PlayForFree,
        PlayForCost,
        Custom,
    }

    public enum Root
    {
        Library,
        Trash,
        Clock,
        Security,
        Custom,
        Hand,
        Recollection,
        Execution,
        DigivolutionCards,
        LinkedCards,
        None,
    }

    public static SelectionRequest CreateRequest(
        GameState state,
        string requestId,
        PlayerId selectPlayer,
        PlayerId rootPlayer,
        Root root,
        Mode mode,
        int maxCount,
        bool canNoSelect,
        bool canEndNotMax,
        Func<CardSource, bool>? canTargetCondition = null,
        IReadOnlyList<CardInstanceId>? customRootCardList = null,
        PermanentId? sourcePermanent = null,
        bool allowFaceDown = false,
        bool canLookReverseCard = false,
        string prompt = "")
    {
        ArgumentNullException.ThrowIfNull(state);
        if (maxCount < 0)
        {
            throw new DomainException("SelectCardEffect max count cannot be negative.");
        }

        var targetKind = root == Root.Security
            ? SelectionTargetKind.Security
            : SelectionTargetKind.Card;
        var selectionKind = root == Root.Security
            ? SelectionKind.SelectSecurity
            : SelectionKind.SelectCard;
        var candidates = CreateCandidates(
            state,
            rootPlayer,
            root,
            canTargetCondition,
            customRootCardList,
            sourcePermanent,
            allowFaceDown,
            canLookReverseCard)
            .ToArray();

        return new SelectionRequest(
            requestId,
            selectPlayer,
            selectionKind,
            targetKind,
            MinCount(maxCount, canNoSelect),
            maxCount,
            canNoSelect,
            canEndNotMax,
            candidates,
            string.IsNullOrWhiteSpace(prompt)
                ? $"Select card for {mode} from {root}."
                : prompt);
    }

    public static IReadOnlyList<SelectableTarget> CreateCandidates(
        GameState state,
        PlayerId rootPlayer,
        Root root,
        Func<CardSource, bool>? canTargetCondition = null,
        IReadOnlyList<CardInstanceId>? customRootCardList = null,
        PermanentId? sourcePermanent = null,
        bool allowFaceDown = false,
        bool canLookReverseCard = false)
    {
        ArgumentNullException.ThrowIfNull(state);

        var context = new GameContext(state);
        return RootCards(state, rootPlayer, root, customRootCardList, sourcePermanent)
            .Where(card => CanSelectCard(state, context, card, root, canTargetCondition, allowFaceDown, canLookReverseCard))
            .Select(card => CreateCardTarget(state, card, root))
            .ToArray();
    }

    private static bool CanSelectCard(
        GameState state,
        GameContext context,
        CardInstanceId card,
        Root root,
        Func<CardSource, bool>? canTargetCondition,
        bool allowFaceDown,
        bool canLookReverseCard)
    {
        var instance = RequireCard(state, card);
        var canSeeFaceDown = allowFaceDown
            || root == Root.Library
            || root == Root.Custom
            || (root == Root.Security && canLookReverseCard);
        if (!canSeeFaceDown && !instance.IsFaceUp)
        {
            return false;
        }

        return canTargetCondition?.Invoke(context.CardSourceFromId(card)) ?? true;
    }

    private static IEnumerable<CardInstanceId> RootCards(
        GameState state,
        PlayerId rootPlayer,
        Root root,
        IReadOnlyList<CardInstanceId>? customRootCardList,
        PermanentId? sourcePermanent)
    {
        if (customRootCardList is not null)
        {
            return customRootCardList;
        }

        var player = state.GetPlayer(rootPlayer);
        return root switch
        {
            Root.Library => player.Deck,
            Root.Trash => player.Trash,
            Root.Security => player.Security,
            Root.Hand => player.Hand,
            Root.Recollection => player.Lost,
            Root.Execution => player.Executing,
            Root.DigivolutionCards => RequirePermanent(state, sourcePermanent, root).SourceCardIds,
            Root.LinkedCards => RequirePermanent(state, sourcePermanent, root).LinkedCards,
            Root.Custom => throw new DomainException("SelectCardEffect.Root.Custom requires a custom root card list."),
            Root.None => Array.Empty<CardInstanceId>(),
            Root.Clock => throw new UnsupportedMechanicException(
                "SelectCardEffect.Root.Clock has no headless zone mapping yet."),
            _ => throw new UnsupportedMechanicException($"SelectCardEffect root '{root}' is not supported."),
        };
    }

    private static SelectableTarget CreateCardTarget(GameState state, CardInstanceId cardId, Root root)
    {
        var card = RequireCard(state, cardId);
        var definition = state.CardDefinitions.TryGetValue(card.DefinitionId, out var found)
            ? found
            : null;
        var targetKind = root == Root.Security
            ? SelectionTargetKind.Security
            : SelectionTargetKind.Card;

        return new SelectableTarget(
            targetKind,
            $"{targetKind.ToString().ToLowerInvariant()}:{cardId.Value}",
            card.Owner,
            Card: cardId,
            Label: definition?.CardId ?? card.DefinitionId,
            Zone: card.CurrentZone);
    }

    private static int MinCount(int maxCount, bool canNoSelect) =>
        canNoSelect ? 0 : Math.Min(1, maxCount);

    public static SelectionResultContinuation CreateModeContinuation(Mode mode) =>
        context =>
        {
            ArgumentNullException.ThrowIfNull(context);
            ApplyMode(context, mode);
        };

    public static SelectionResultContinuation CreatePlayForFreePrimitiveContinuation(
        Func<SelectionResultApplicationContext, CardInstanceId, int> targetFrameIndexResolver,
        bool suspended = false)
    {
        ArgumentNullException.ThrowIfNull(targetFrameIndexResolver);

        return context =>
        {
            ArgumentNullException.ThrowIfNull(context);
            PlaySelectedCardsForFreePrimitive(context, targetFrameIndexResolver, suspended);
        };
    }

    public static void ApplyMode(SelectionResultApplicationContext context, Mode mode)
    {
        ArgumentNullException.ThrowIfNull(context);
        switch (mode)
        {
            case Mode.AddHand:
                AddSelectedCardsToHand(context);
                break;
            case Mode.Discard:
                DiscardSelectedCards(context);
                break;
            default:
                throw new UnsupportedMechanicException(
                    $"SelectCardEffect.Mode.{mode} has no headless primitive continuation yet.");
        }
    }

    private static void PlaySelectedCardsForFreePrimitive(
        SelectionResultApplicationContext context,
        Func<SelectionResultApplicationContext, CardInstanceId, int> targetFrameIndexResolver,
        bool suspended)
    {
        foreach (var target in SelectedCardTargets(context))
        {
            var targetFrame = ResolveTargetFrame(context, target.Card, targetFrameIndexResolver);
            if (target.SourceZone == Zone.EvolutionSources)
            {
                if (target.SourcePermanent is null)
                {
                    throw new DomainException(
                        $"SelectCardEffect.Mode.PlayForFree source card '{target.Card}' requires a host permanent.");
                }

                context.Primitives.PlayEvolutionSourceAsNewPermanent(
                    context.State,
                    target.Owner,
                    target.Card,
                    target.SourcePermanent.Value,
                    targetFrame,
                    suspended,
                    context.Resolution.SourceCard,
                    context.Resolution.SourcePermanent);
                continue;
            }

            if (!CanPlayForFreeFrom(target.SourceZone))
            {
                throw new UnsupportedMechanicException(
                    $"SelectCardEffect.Mode.PlayForFree from '{target.SourceZone}' is not supported by the primitive placement continuation.");
            }

            context.Primitives.PlayWithoutPayingCost(
                context.State,
                target.Owner,
                target.Card,
                target.SourceZone,
                targetFrame,
                suspended,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent);
        }
    }

    private static void AddSelectedCardsToHand(SelectionResultApplicationContext context)
    {
        foreach (var group in SelectedCardTargets(context).GroupBy(target => (target.Owner, target.SourceZone)))
        {
            if (!CanAddToHandFrom(group.Key.SourceZone))
            {
                throw new UnsupportedMechanicException(
                    $"SelectCardEffect.Mode.AddHand from '{group.Key.SourceZone}' is not supported by the generic hand primitive.");
            }

            context.Primitives.AddCardsToHandWithEvents(
                context.State,
                group.Key.Owner,
                group.Select(target => target.Card).ToArray(),
                group.Key.SourceZone,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                isDraw: false,
                trace: context.Trace);
        }
    }

    private static void DiscardSelectedCards(SelectionResultApplicationContext context)
    {
        var targets = SelectedCardTargets(context).ToArray();
        foreach (var handGroup in targets
            .Where(target => target.SourceZone == Zone.Hand)
            .GroupBy(target => target.Owner))
        {
            context.Primitives.DiscardHandWithEvents(
                context.State,
                handGroup.Key,
                handGroup.Select(target => target.Card).ToArray(),
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                context.Trace);
        }

        foreach (var target in targets.Where(target => target.SourceZone != Zone.Hand))
        {
            if (target.SourceZone == Zone.Trash)
            {
                throw new DomainException("SelectCardEffect.Mode.Discard target is already in trash.");
            }

            if (target.SourceZone is Zone.BattleArea or Zone.BreedingArea)
            {
                throw new UnsupportedMechanicException(
                    $"SelectCardEffect.Mode.Discard from '{target.SourceZone}' should use a permanent primitive.");
            }

            context.Primitives.TrashCard(
                context.State,
                target.Card,
                target.SourceZone,
                target.SourcePermanent,
            MoveReason.Effect);
        }
    }

    private static int ResolveTargetFrame(
        SelectionResultApplicationContext context,
        CardInstanceId card,
        Func<SelectionResultApplicationContext, CardInstanceId, int> targetFrameIndexResolver)
    {
        var frame = targetFrameIndexResolver(context, card);
        if (frame < 0 || frame >= context.State.Config.FieldSlotCount)
        {
            throw new DomainException(
                $"SelectCardEffect.Mode.PlayForFree target frame '{frame}' is outside the configured field slot range.");
        }

        return frame;
    }

    private static IReadOnlyList<SelectedCardMoveTarget> SelectedCardTargets(SelectionResultApplicationContext context) =>
        context.SelectedTargets
            .Select(target =>
            {
                if (target.Card is null)
                {
                    throw new DomainException($"Selected target '{target.StableId}' does not reference a card.");
                }

                var card = RequireCard(context.State, target.Card.Value);
                return new SelectedCardMoveTarget(
                    target.Card.Value,
                    target.Owner ?? card.Owner,
                    target.Zone ?? card.CurrentZone,
                    card.PermanentId);
            })
            .ToArray();

    private static bool CanAddToHandFrom(Zone sourceZone) =>
        sourceZone is Zone.Deck
            or Zone.Security
            or Zone.Trash
            or Zone.Lost
            or Zone.Executing
            or Zone.Revealed
            or Zone.OutsideGame;

    private static bool CanPlayForFreeFrom(Zone sourceZone) =>
        sourceZone is Zone.Hand
            or Zone.Deck
            or Zone.Security
            or Zone.Trash
            or Zone.Lost
            or Zone.Executing
            or Zone.Revealed
            or Zone.OutsideGame;

    private static CardInstance RequireCard(GameState state, CardInstanceId card) =>
        state.Cards.TryGetValue(card, out var instance)
            ? instance
            : throw new DomainException($"SelectCardEffect root contains missing card '{card}'.");

    private static PermanentState RequirePermanent(GameState state, PermanentId? permanent, Root root)
    {
        if (permanent is null)
        {
            throw new DomainException($"SelectCardEffect.Root.{root} requires a source permanent.");
        }

        return state.Players
            .SelectMany(player => player.FieldPermanents)
            .FirstOrDefault(candidate => candidate.Id == permanent.Value)
            ?? throw new DomainException($"SelectCardEffect source permanent '{permanent}' does not exist.");
    }

    private sealed record SelectedCardMoveTarget(
        CardInstanceId Card,
        PlayerId Owner,
        Zone SourceZone,
        PermanentId? SourcePermanent);
}

public sealed class SelectPermanentEffect
{
    public enum Mode
    {
        Tap,
        UnTap,
        Destroy,
        Bounce,
        PutLibraryBottom,
        PutLibraryTop,
        PutSecurityBottom,
        PutSecurityTop,
        Degenerate,
        Attack,
        Custom,
    }

    public static SelectionRequest CreateRequest(
        GameState state,
        string requestId,
        PlayerId selectPlayer,
        Mode mode,
        int maxCount,
        bool canNoSelect,
        bool canEndNotMax,
        Func<Permanent, bool>? canTargetCondition = null,
        string prompt = "")
    {
        ArgumentNullException.ThrowIfNull(state);
        if (maxCount < 0)
        {
            throw new DomainException("SelectPermanentEffect max count cannot be negative.");
        }

        var candidates = CreateCandidates(state, canTargetCondition);
        return new SelectionRequest(
            requestId,
            selectPlayer,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            canNoSelect ? 0 : Math.Min(1, maxCount),
            maxCount,
            canNoSelect,
            canEndNotMax,
            candidates,
            string.IsNullOrWhiteSpace(prompt)
                ? $"Select permanent for {mode}."
                : prompt);
    }

    public static IReadOnlyList<SelectableTarget> CreateCandidates(
        GameState state,
        Func<Permanent, bool>? canTargetCondition = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        return state.Players
            .OrderBy(player => player.Id.Value)
            .SelectMany(player => player.FieldPermanents
                .OrderBy(permanent => permanent.FrameIndex)
                .ThenBy(permanent => permanent.Id.Value))
            .Where(permanent => canTargetCondition?.Invoke(new Permanent(state, permanent.Id)) ?? true)
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: state.Cards.TryGetValue(permanent.TopCardId, out var card)
                    && state.CardDefinitions.TryGetValue(card.DefinitionId, out var definition)
                    ? definition.CardId
                    : permanent.TopCardId.Value.ToString(),
                Zone: permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea))
            .ToArray();
    }

    public static SelectionResultContinuation CreateModeContinuation(Mode mode) =>
        context =>
        {
            ArgumentNullException.ThrowIfNull(context);
            foreach (var permanent in context.SelectedPermanentIds)
            {
                ApplyMode(context, mode, permanent);
            }
        };

    public static void ApplyMode(SelectionResultApplicationContext context, Mode mode, PermanentId permanent)
    {
        ArgumentNullException.ThrowIfNull(context);
        switch (mode)
        {
            case Mode.Destroy:
                context.Primitives.DestroyPermanent(context.State, permanent, context.Trace);
                break;
            case Mode.Tap:
                context.Primitives.Suspend(
                    context.State,
                    permanent,
                    context.Resolution.SourceCard,
                    context.Resolution.SourcePermanent);
                break;
            case Mode.UnTap:
                context.Primitives.Unsuspend(context.State, permanent);
                break;
            default:
                throw new UnsupportedMechanicException(
                    $"SelectPermanentEffect.Mode.{mode} has no headless primitive continuation yet.");
        }
    }
}

public sealed class SelectCountEffect
{
    public static SelectionRequest CreateRequest(
        string requestId,
        PlayerId selectPlayer,
        int maxCount,
        bool canNoSelect,
        IReadOnlyList<int>? candidates = null,
        bool preferMin = false,
        bool isDigivolutionCost = false,
        string prompt = "")
    {
        if (maxCount < 0)
        {
            throw new DomainException("SelectCountEffect max count cannot be negative.");
        }

        var countCandidates = CreateCountCandidates(maxCount, canNoSelect, candidates);
        if (countCandidates.Length == 0)
        {
            throw new DomainException("SelectCountEffect requires at least one count candidate.");
        }

        return new SelectionRequest(
            requestId,
            selectPlayer,
            SelectionKind.SelectCount,
            SelectionTargetKind.Count,
            countCandidates.Min(),
            countCandidates.Max(),
            canNoSelect,
            canEndNotMax: true,
            countCandidates
                .Select(count => new SelectableTarget(
                    SelectionTargetKind.Count,
                    $"count:{count}",
                    OptionValue: count.ToString(),
                    Label: count.ToString()))
                .ToArray(),
            string.IsNullOrWhiteSpace(prompt)
                ? $"Select count ({(preferMin ? "prefer-min" : "prefer-max")}{(isDigivolutionCost ? ", digivolution-cost" : string.Empty)})."
                : prompt);
    }

    private static int[] CreateCountCandidates(
        int maxCount,
        bool canNoSelect,
        IReadOnlyList<int>? candidates)
    {
        if (candidates is not null && candidates.Count > 0)
        {
            return candidates
                .Distinct()
                .OrderBy(value => value)
                .ToArray();
        }

        var start = canNoSelect ? 0 : 1;
        return Enumerable.Range(start, maxCount - start + 1).ToArray();
    }
}
