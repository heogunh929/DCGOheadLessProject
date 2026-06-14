using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal enum TriggerBody
{
    DpPlus1000,
    MemoryPlus1,
}

internal abstract class St3DpZeroDeletionTriggerScript : ICardScript
{
    private readonly TriggerBody _body;

    protected St3DpZeroDeletionTriggerScript(
        string cardId,
        string effectClassName,
        TriggerBody body,
        string notes)
    {
        _body = body;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:on-destroyed-anyone:dp-zero",
                EffectTiming.OnDestroyedAnyone,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.State.TurnPlayerId == controller
                    && StarterScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && IsOpponentDpZeroDeletion(effectContext, controller),
                IsOncePerTurn: true,
                OncePerTurnKey: $"{Porting.CardId}:dp-zero-deletion"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (!StarterScriptSupport.IsInheritedSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent))
            {
                throw new DomainException($"{Porting.CardId} requires this card as an inherited source in battle area.");
            }

            if (!IsOpponentDpZeroDeletion(context.Resolution.Context, controller))
            {
                throw new DomainException($"{Porting.CardId} requires an opponent Digimon deleted by dropping to 0 DP.");
            }

            switch (_body)
            {
                case TriggerBody.DpPlus1000:
                    var sourcePermanent = context.Resolution.SourcePermanent
                        ?? throw new DomainException($"{Porting.CardId} DP body requires a source permanent.");
                    primitives.AddTemporaryDPModifier(
                        state,
                        sourcePermanent,
                        amount: 1000,
                        DurationScope.UntilTurnEnd,
                        controller,
                        context.Resolution.SourceCard,
                        context.Resolution.SourcePermanent,
                        stableId: $"{context.Resolution.StableId}:dp:{sourcePermanent.Value}",
                        debugLabel: $"{Porting.CardId} DP +1000 until turn end");
                    return;

                case TriggerBody.MemoryPlus1:
                    primitives.ModifyMemory(state, controller, 1);
                    return;

                default:
                    throw new DomainException($"{Porting.CardId} has unsupported trigger body '{_body}'.");
            }
        });

    private static bool IsOpponentDpZeroDeletion(EffectContext context, PlayerId controller)
    {
        if (context.GetValueOrDefault("DestroyedByDpZero") is not true)
        {
            return false;
        }

        if (context.GetValueOrDefault("DestroyedController") is not PlayerId destroyedController)
        {
            return false;
        }

        return destroyedController != controller;
    }
}

internal abstract class St3OnAttackDpReductionScript : ICardScript
{
    private readonly int _amount;
    private readonly bool _requireInheritedSource;

    protected St3OnAttackDpReductionScript(
        string cardId,
        string effectClassName,
        int amount,
        bool requireInheritedSource,
        string notes)
    {
        _amount = amount;
        _requireInheritedSource = requireInheritedSource;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:on-ally-attack:dp-selection",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    IsValidSource(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    $"{Porting.CardId}:on-ally-attack:dp",
                    $"1 of your opponent's Digimon gets {_amount} DP for the turn."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"{Porting.CardId} requires SelectionResultApplicator for its OnAllyAttack selection body.");

    private bool IsValidSource(GameState state, CardInstanceId? sourceCard, PermanentId? sourcePermanent) =>
        _requireInheritedSource
            ? StarterScriptSupport.IsInheritedSourceInBattleArea(state, sourceCard, sourcePermanent)
            : StarterScriptSupport.IsTopSourceInBattleArea(state, sourceCard, sourcePermanent);

    private void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                _amount,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: $"{context.Resolution.StableId} DP {_amount} until turn end");
        }
    }
}
