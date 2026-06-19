using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal static class St1TemporaryDpSelectionSupport
{
    public static void ApplyPlus3000UntilTurnEnd(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);

        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: 3000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: $"{context.Resolution.StableId} DP +3000 until turn end");
        }
    }
}

internal abstract class St1OptionDeleteScript : ICardScript
{
    private readonly int _maxTargets;
    private readonly bool _canEndNotMax;
    private readonly Func<GameState, PermanentState, bool> _targetPredicate;

    protected St1OptionDeleteScript(
        string cardId,
        string effectClassName,
        string notes,
        int maxTargets,
        bool canEndNotMax,
        Func<GameState, PermanentState, bool> targetPredicate)
    {
        _maxTargets = maxTargets;
        _canEndNotMax = canEndNotMax;
        _targetPredicate = targetPredicate;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:option:delete-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext => CreateCandidates(effectContext.State, ResolveController(effectContext)).Count > 0,
                CreateSelectionRequest: effectContext => CreateSelectionRequest(effectContext.State, ResolveController(effectContext)),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                $"{Porting.CardId}:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext => CreateCandidates(effectContext.State, ResolveController(effectContext)).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId.EndsWith(":security:main-option-activation", StringComparison.Ordinal))
        {
            throw new DomainException(
                $"{Porting.CardId} security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException(
            $"{Porting.CardId} main option selection must be resolved through SelectionResultApplicator.");
    }

    private PlayerId ResolveController(EffectContext context)
    {
        if (context.Player is not null)
        {
            return context.Player.Value;
        }

        return St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);
    }

    private SelectionRequest CreateSelectionRequest(GameState state, PlayerId controller)
    {
        var candidates = CreateCandidates(state, controller);
        var maxCount = Math.Min(_maxTargets, candidates.Count);

        return new SelectionRequest(
            $"{Porting.CardId}:option:delete",
            controller,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount,
            canSkip: false,
            canEndNotMax: _canEndNotMax,
            candidates,
            Porting.CardId == "ST1-15"
                ? "Delete up to 2 of your opponent's Digimon with 4000 DP or less."
                : "Delete 1 of your opponent's Digimon.");
    }

    private IReadOnlyList<SelectableTarget> CreateCandidates(GameState state, PlayerId controller)
    {
        var opponent = St1ScriptSupport.Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => _targetPredicate(state, permanent))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DestroyPermanent(context.State, permanent, context.Trace);
        }
    }
}

internal abstract class St1InheritedDpScript : ICardScript, IContinuousCardScript
{
    private readonly int _requiredSourceCount;

    protected St1InheritedDpScript(string cardId, string effectClassName, int requiredSourceCount, string notes)
    {
        _requiredSourceCount = requiredSourceCount;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.InheritedSource)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        int Amount(ContinuousEffectEvaluationContext _) => 1000;

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return evaluation.State.TurnPlayerId == context.Controller
                && BattleRules.IsDigimon(evaluation.State, target.TopCardId)
                && target.SourceCardIds.Contains(context.SourceCard)
                && target.SourceCardIds.Count >= _requiredSourceCount;
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"{Porting.CardId}:continuous:inherited-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.DP,
                Amount,
                Condition,
                $"{Porting.CardId} inherited DP +1000")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"{Porting.CardId} has only continuous inherited effects and no queued resolve body.");
}

internal abstract class DeclaredCapabilityCardScript : ICardScript
{
    protected DeclaredCapabilityCardScript(string cardId, string effectClassName, string notes)
    {
        Porting = new CardEffectPortingRecord(cardId, effectClassName, CardEffectPortingStatus.Implemented, notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"Card script '{Porting.CardId}' is implemented as a declared capability and has no queued resolve body.");
}

internal abstract class SharedSt1_06BlockerMemoryLossScript : ICardScript
{
    protected SharedSt1_06BlockerMemoryLossScript(string cardId, string effectClassName, string notes)
    {
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:on-ally-attack:memory-minus-2",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext => St1ScriptSupport.IsSourcePermanentInBattleArea(
                    effectContext.State,
                    context.SourceCard,
                    context.SourcePermanent)),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var owner = St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (!St1ScriptSupport.IsSourcePermanentInBattleArea(state, context.Resolution.SourceCard, context.Resolution.SourcePermanent))
            {
                throw new DomainException($"{Porting.CardId} memory loss requires the source permanent in the battle area.");
            }

            primitives.ModifyMemory(state, owner, -2);
        });
}

internal static class St1ScriptSupport
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

    public static PlayerId? TrySourceCardOwner(GameState state, CardInstanceId? sourceCard)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return null;
        }

        return instance.Owner;
    }

    public static PlayerId RequireSourceCardOwner(GameState state, CardInstanceId? sourceCard) =>
        TrySourceCardOwner(state, sourceCard)
        ?? throw new DomainException("Card script resolution requires a source card owner.");

    public static bool IsSourcePermanentInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        var permanent = FindPermanent(state, sourcePermanent, sourceCard.Value);
        return permanent is not null && !permanent.IsBreedingArea;
    }

    public static bool CanPlaySelfPermanentFromExecuting(
        GameState state,
        CardInstanceId? sourceCard,
        PlayerId controller)
    {
        if (sourceCard is null || !state.Cards.TryGetValue(sourceCard.Value, out var instance))
        {
            return false;
        }

        if (instance.Owner != controller || instance.CurrentZone != Zone.Executing)
        {
            return false;
        }

        var definition = BattleRules.Definition(state, sourceCard.Value);
        if (!definition.IsPermanent || definition.CardKinds.Contains(CardKind.Option))
        {
            return false;
        }

        return state.GetPlayer(controller).BattleAreaPermanents.Count() < state.Config.FieldSlotCount;
    }

    public static int FirstEmptyBattleFrameForPlay(GameState state, PlayerId controller)
    {
        var player = state.GetPlayer(controller);
        return BattleRules.FirstEmptyBattleFrame(player, state.Config.FieldSlotCount);
    }

    public static SelectionRequest CreateOwnerBattleAreaDigimonSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OwnerBattleAreaDigimonCandidates(state, controller);
        return new SelectionRequest(
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
    }

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();

    private static PermanentState? FindPermanent(GameState state, PermanentId? sourcePermanent, CardInstanceId sourceCard)
    {
        var permanents = state.Players.SelectMany(player => player.FieldPermanents);
        if (sourcePermanent is not null)
        {
            return permanents.FirstOrDefault(permanent =>
                permanent.Id == sourcePermanent.Value && PermanentContainsCard(permanent, sourceCard));
        }

        return permanents.FirstOrDefault(permanent => PermanentContainsCard(permanent, sourceCard));
    }

    private static bool PermanentContainsCard(PermanentState permanent, CardInstanceId card) =>
        permanent.TopCardId == card
        || permanent.SourceCardIds.Contains(card)
        || permanent.LinkedCards.Contains(card);
}
