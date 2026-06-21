using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Primitives;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class Bt1BlockedCardScript : ICardScript
{
    protected Bt1BlockedCardScript(
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

internal static class Bt1ScriptSupport
{
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
                && IsTopSourceInBattleArea(effectContext.State, sourceCard, sourcePermanent),
            CanActivate: effectContext => MemoryForPlayer(effectContext.State, controller) <= 2);

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
            CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                effectContext.State,
                sourceCard,
                controller));

    public static void SetMemoryTo3IfLow(GameState state, PlayerId controller)
    {
        if (MemoryForPlayer(state, controller) > 2)
        {
            return;
        }

        SetMemoryForPlayer(state, controller, 3);
    }

    public static void PlaySelfTamerFromExecuting(
        GameState state,
        Tier1PrimitiveService primitives,
        PlayerId controller,
        CardInstanceId? sourceCard)
    {
        var card = sourceCard
            ?? throw new DomainException("BT1 security play-self Tamer requires a source card.");
        var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
        primitives.PlayWithoutPayingCost(
            state,
            controller,
            card,
            Zone.Executing,
            frame,
            suspended: false);
    }

    public static int MemoryForPlayer(GameState state, PlayerId player) =>
        player == state.TurnPlayerId ? state.Memory : -state.Memory;

    public static void SetMemoryForPlayer(GameState state, PlayerId player, int memory)
    {
        _ = state.GetPlayer(player);
        state.Memory = Math.Clamp(player == state.TurnPlayerId ? memory : -memory, -10, 10);
    }

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
            throw new DomainException("BT1 card script requires a source card owner.");
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

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller,
        Func<PermanentState, CardDefinition, bool>? predicate = null) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => predicate?.Invoke(permanent, BattleRules.Definition(state, permanent.TopCardId)) ?? true)
            .Select(permanent => ToPermanentTarget(state, permanent))
            .ToArray();

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller,
        Func<PermanentState, CardDefinition, bool> predicate)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => predicate(permanent, BattleRules.Definition(state, permanent.TopCardId)))
            .Select(permanent => ToPermanentTarget(state, permanent))
            .ToArray();
    }

    public static SelectionRequest CreatePermanentSelectionRequest(
        string requestId,
        PlayerId controller,
        IReadOnlyList<SelectableTarget> candidates,
        string prompt,
        int maxCount = 1,
        bool canSkip = false,
        bool canEndNotMax = false) =>
        new(
            requestId,
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: Math.Min(maxCount, candidates.Count),
            canSkip,
            canEndNotMax,
            candidates,
            prompt);

    private static SelectableTarget ToPermanentTarget(GameState state, PermanentState permanent)
    {
        var definition = BattleRules.Definition(state, permanent.TopCardId);
        return new SelectableTarget(
            SelectionTargetKind.Permanent,
            $"permanent:{permanent.Id.Value}",
            permanent.ControllerPlayerId,
            Permanent: permanent.Id,
            Label: definition.CardId,
            Zone: Zone.BattleArea);
    }
}
