using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal abstract class St2SourceTrashScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly bool _requireInheritedSource;
    private readonly int? _maxTargetLevel;
    private readonly int _trashCount;

    protected St2SourceTrashScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        bool requireInheritedSource,
        int? maxTargetLevel,
        int trashCount,
        string notes)
    {
        _timing = timing;
        _requireInheritedSource = requireInheritedSource;
        _maxTargetLevel = maxTargetLevel;
        _trashCount = trashCount;
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
        var timingToken = TimingToken(_timing);

        return new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{timingToken}:source-trash-selection",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    IsValidSource(effectContext.State, context.SourceCard, context.SourcePermanent)
                    && StarterScriptSupport.OpponentBattleAreaDigimonWithSourcesCandidates(
                        effectContext.State,
                        controller,
                        _maxTargetLevel).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonWithSourcesSelectionRequest(
                    effectContext.State,
                    controller,
                    $"{Porting.CardId}:{timingToken}:source-trash",
                    $"Select 1 opponent Digimon to trash {_trashCount} bottom digivolution source card(s).",
                    _maxTargetLevel),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"{Porting.CardId} requires SelectionResultApplicator for its source-trash selection body.");

    private bool IsValidSource(GameState state, CardInstanceId? sourceCard, PermanentId? sourcePermanent) =>
        _requireInheritedSource
            ? StarterScriptSupport.IsInheritedSourceInBattleArea(state, sourceCard, sourcePermanent)
            : StarterScriptSupport.IsTopSourceInBattleArea(state, sourceCard, sourcePermanent);

    private void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.TrashBottomDigivolutionSources(
                context.State,
                permanent,
                _trashCount);
        }
    }

    private static string TimingToken(EffectTiming timing) =>
        timing switch
        {
            EffectTiming.OnAllyAttack => "on-ally-attack",
            EffectTiming.WhenDigivolving => "when-digivolving",
            _ => timing.ToString(),
        };
}

internal abstract class St2NoSourceInheritedContinuousScript : ICardScript, IContinuousCardScript
{
    private readonly ContinuousModifierKind _modifierKind;
    private readonly int _amount;

    protected St2NoSourceInheritedContinuousScript(
        string cardId,
        string effectClassName,
        ContinuousModifierKind modifierKind,
        int amount,
        string notes)
    {
        _modifierKind = modifierKind;
        _amount = amount;
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

        int Amount(ContinuousEffectEvaluationContext _) => _amount;

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return evaluation.State.TurnPlayerId == context.Controller
                && target.Id == context.SourcePermanent
                && target.SourceCardIds.Contains(context.SourceCard)
                && StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources(evaluation.State, context.Controller);
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"{Porting.CardId}:continuous:opponent-no-source:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                _modifierKind,
                Amount,
                Condition,
                $"{Porting.CardId} continuous {_modifierKind} {_amount}")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"{Porting.CardId} has only continuous inherited effects and no queued resolve body.");
}
