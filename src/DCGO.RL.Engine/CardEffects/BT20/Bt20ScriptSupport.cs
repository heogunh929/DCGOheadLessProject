using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class Bt20BlockedCardScript : ICardScript
{
    protected Bt20BlockedCardScript(
        string cardId,
        string effectClassName,
        CardEffectPortingStatus status,
        string notes)
    {
        Porting = new CardEffectPortingRecord(cardId, effectClassName, status, notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public void Resolve(CardScriptExecutionContext context) =>
        throw new UnsupportedMechanicException($"{Porting.CardId} is blocked: {Porting.Notes}");
}

internal static class Bt20ScriptSupport
{
    public static PlayerId Opponent(GameState state, PlayerId player)
    {
        foreach (var candidate in state.Players.Select(candidate => candidate.Id))
        {
            if (candidate != player)
            {
                return candidate;
            }
        }

        throw new DomainException($"Player '{player}' has no opponent.");
    }

    public static PlayerId RequireSourceCardOwner(GameState state, CardInstanceId? sourceCard)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            throw new DomainException("BT20 card script requires a source card owner.");
        }

        return instance.Owner;
    }

    public static bool IsTopSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null || sourcePermanent is null)
        {
            return false;
        }

        return state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Any(permanent =>
                permanent.Id == sourcePermanent.Value
                && permanent.TopCardId == sourceCard.Value
                && !permanent.IsBreedingArea);
    }

    public static bool IsInheritedSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null || sourcePermanent is null)
        {
            return false;
        }

        return state.Players
            .SelectMany(player => player.BattleAreaPermanents)
            .Any(permanent =>
                permanent.Id == sourcePermanent.Value
                && permanent.SourceCardIds.Contains(sourceCard.Value)
                && !permanent.IsBreedingArea);
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller,
        Func<PermanentState, CardDefinition, bool>? predicate = null)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => predicate?.Invoke(permanent, BattleRules.Definition(state, permanent.TopCardId)) ?? true)
            .Select(permanent =>
            {
                var definition = BattleRules.Definition(state, permanent.TopCardId);
                return new SelectableTarget(
                    SelectionTargetKind.Permanent,
                    $"permanent:{permanent.Id.Value}",
                    permanent.ControllerPlayerId,
                    Permanent: permanent.Id,
                    Label: definition.CardId,
                    Zone: Zone.BattleArea);
            })
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller,
        Func<PermanentState, CardDefinition, bool>? predicate = null) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => predicate?.Invoke(permanent, BattleRules.Definition(state, permanent.TopCardId)) ?? true)
            .Select(permanent =>
            {
                var definition = BattleRules.Definition(state, permanent.TopCardId);
                return new SelectableTarget(
                    SelectionTargetKind.Permanent,
                    $"permanent:{permanent.Id.Value}",
                    permanent.ControllerPlayerId,
                    Permanent: permanent.Id,
                    Label: definition.CardId,
                    Zone: Zone.BattleArea);
            })
            .ToArray();

    public static SelectionRequest CreatePermanentSelectionRequest(
        string requestId,
        PlayerId controller,
        IReadOnlyList<SelectableTarget> candidates,
        string prompt) =>
        new(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);

    public static SelectionRequest CreateCardSelectionRequest(
        GameState state,
        string requestId,
        PlayerId controller,
        SelectionTargetKind targetKind,
        IReadOnlyList<CardInstanceId> candidates,
        Zone zone,
        bool canSkip,
        string prompt)
    {
        var targets = candidates
            .Select(card =>
            {
                var definition = BattleRules.Definition(state, card);
                return new SelectableTarget(
                    targetKind,
                    $"card:{card.Value}",
                    state.Cards[card].Owner,
                    Card: card,
                    Label: definition.CardId,
                    Zone: zone);
            })
            .ToArray();

        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectCard,
            targetKind,
            minCount: 1,
            maxCount: Math.Min(1, targets.Length),
            canSkip,
            canEndNotMax: false,
            targets,
            prompt);
    }

    public static EffectDescriptor CreateSetMemoryTo3TamerDescriptor(
        string stableId,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller) =>
        new(
            stableId,
            EffectTiming.OnStartTurn,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            CanTrigger: effectContext =>
                effectContext.State.TurnPlayerId == controller
                && IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent)
                && effectContext.State.Memory < 3);

    public static EffectDescriptor CreatePlaySelfTamerSecurityDescriptor(
        string stableId,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent,
        PlayerId controller) =>
        new(
            stableId,
            EffectTiming.SecuritySkill,
            SourceCard: sourceCard,
            SourcePermanent: sourcePermanent,
            Controller: controller,
            CanTrigger: effectContext => CanPlaySelfPermanentFromExecuting(
                effectContext.State,
                sourceCard,
                controller));

    public static bool CanPlaySelfPermanentFromExecuting(
        GameState state,
        CardInstanceId? sourceCard,
        PlayerId controller)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return false;
        }

        return instance.Owner == controller
            && instance.CurrentZone.Equals(Zone.Executing)
            && state.GetPlayer(controller).Executing.Contains(sourceCard.Value)
            && FirstEmptyBattleFrameForPlay(state, controller) >= 0;
    }

    public static int FirstEmptyBattleFrameForPlay(GameState state, PlayerId controller)
    {
        var player = state.GetPlayer(controller);
        var occupied = player.BattleAreaPermanents
            .Select(permanent => permanent.FrameIndex)
            .ToHashSet();
        for (var frame = 0; frame < state.Config.FieldSlotCount; frame++)
        {
            if (!occupied.Contains(frame))
            {
                return frame;
            }
        }

        return -1;
    }

    public static void SetMemoryTo3IfLow(GameState state, PlayerId controller)
    {
        if (state.TurnPlayerId == controller && state.Memory < 3)
        {
            state.Memory = 3;
        }
    }

    public static void PlaySelfTamerFromExecuting(
        GameState state,
        Tier1PrimitiveService primitives,
        PlayerId controller,
        CardInstanceId? sourceCard)
    {
        var card = sourceCard
            ?? throw new DomainException("BT20 security play-self requires a source card.");
        var frame = FirstEmptyBattleFrameForPlay(state, controller);
        if (frame < 0)
        {
            throw new DomainException($"Player '{controller}' has no empty battle area frame.");
        }

        primitives.PlayWithoutPayingCost(
            state,
            controller,
            card,
            Zone.Executing,
            frame,
            suspended: false);
    }
}
