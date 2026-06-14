using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public static class St2St3CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateScripts() =>
        new ICardScript[]
        {
            new St2NoSourceInheritedContinuousScript(
                "ST2-01",
                "ST2_01",
                ContinuousModifierKind.DP,
                amount: 1000,
                "Inherited owner-turn DP +1000 while the opponent has a Digimon with no digivolution sources."),
            new NoEffectCardScript("ST2-02", notes: "No ST2_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2SourceTrashScript(
                "ST2-03",
                "ST2_03",
                EffectTiming.OnAllyAttack,
                requireInheritedSource: true,
                maxTargetLevel: 5,
                trashCount: 1,
                "Inherited OnAllyAttack level 5 or lower opponent Digimon selection trashes 1 bottom digivolution source through SelectionResultApplicator and ZoneMover."),
            new NoEffectCardScript("ST2-04", notes: "No ST2_04 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new NoEffectCardScript("ST2-05", notes: "No ST2_05 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2SourceTrashScript(
                "ST2-06",
                "ST2_06",
                EffectTiming.OnAllyAttack,
                requireInheritedSource: true,
                maxTargetLevel: null,
                trashCount: 1,
                "Inherited OnAllyAttack opponent Digimon selection trashes 1 bottom digivolution source through SelectionResultApplicator and ZoneMover."),
            new NoEffectCardScript("ST2-07", notes: "No ST2_07 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2NoSourceInheritedContinuousScript(
                "ST2-08",
                "ST2_08",
                ContinuousModifierKind.SecurityAttack,
                amount: 1,
                "Inherited owner-turn SecurityAttack +1 while the opponent has a Digimon with no digivolution sources."),
            new St2SourceTrashScript(
                "ST2-09",
                "ST2_09",
                EffectTiming.WhenDigivolving,
                requireInheritedSource: false,
                maxTargetLevel: null,
                trashCount: 2,
                "Original OnEnterFieldAnyone plus CanTriggerWhenDigivolving maps to WhenDigivolving; opponent Digimon selection trashes up to 2 bottom digivolution sources through SelectionResultApplicator and ZoneMover."),
            new NoEffectCardScript("ST2-10", notes: "No ST2_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2MetalGarurumonScript(),
            new St2MattIshidaScript(),
            new St2HammerSparkScript(),
            new St2HowlingBlasterScript(),
            new St2WereGarurumonScript(),
            new St2CocytusBreathScript(),

            new St3DpZeroDeletionTriggerScript(
                "ST3-01",
                "ST3_01",
                TriggerBody.DpPlus1000,
                "Inherited owner-turn once-per-turn trigger gives this Digimon DP +1000 when an opponent Digimon is deleted by dropping to 0 DP."),
            new NoEffectCardScript("ST3-02", notes: "No ST3_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new NoEffectCardScript("ST3-03", notes: "No ST3_03 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3DpZeroDeletionTriggerScript(
                "ST3-04",
                "ST3_04",
                TriggerBody.MemoryPlus1,
                "Inherited owner-turn once-per-turn trigger gains 1 memory when an opponent Digimon is deleted by dropping to 0 DP."),
            new St3PatamonScript(),
            new NoEffectCardScript("ST3-06", notes: "No ST3_06 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new NoEffectCardScript("ST3-07", notes: "No ST3_07 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3OnAttackDpReductionScript(
                "ST3-08",
                "ST3_08",
                amount: -1000,
                requireInheritedSource: true,
                "Inherited OnAllyAttack opponent Digimon selection applies temporary DP -1000 until turn end through SelectionResultApplicator and Tier1PrimitiveService."),
            new St3AngewomonScript(),
            new NoEffectCardScript("ST3-10", notes: "No ST3_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3OnAttackDpReductionScript(
                "ST3-11",
                "ST3_11",
                amount: -4000,
                requireInheritedSource: false,
                "OnAllyAttack opponent Digimon selection applies temporary DP -4000 until turn end through SelectionResultApplicator and Tier1PrimitiveService."),
            new St3TakeruTakaishiScript(),
            new St3HolyFlameScript(),
            new St3HeavensCharmScript(),
            new St3HolyWaveScript(),
            new St3SevenHeavensScript(),
        };

    public static CardScriptRegistry CreateRegistry() => new(CreateScripts());

    public static CardScriptRegistry CreateCombinedWithSt1Registry() =>
        new(St1CardScriptCatalog.CreateScripts().Concat(CreateScripts()));
}

internal sealed class St2HammerSparkScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-13",
        "ST2_13",
        CardEffectPortingStatus.Implemented,
        "Main option memory +1 and security memory +2 resolve through Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-13:option:memory-plus-1",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
            new EffectDescriptor(
                "ST2-13:security:memory-plus-2",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        var amount = context.Resolution.StableId switch
        {
            "ST2-13:option:memory-plus-1" => 1,
            "ST2-13:security:memory-plus-2" => 2,
            _ => throw new DomainException($"ST2-13 cannot resolve unknown effect '{context.Resolution.StableId}'."),
        };

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.ModifyMemory(state, controller, amount);
        });
    }
}

internal sealed class St2CocytusBreathScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-16",
        "ST2_16",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon selection returns the top card to hand and trashes all digivolution sources; security activates the same main option through SecurityEffectExecutionService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-16:option:bounce-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-16:option:bounce",
                    "Return 1 of your opponent's Digimon to its owner's hand. Trash all digivolution cards of that Digimon."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST2-16:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST2-16:security:main-option-activation")
        {
            throw new DomainException("ST2-16 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST2-16 main option selection must be resolved through SelectionResultApplicator.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.ReturnPermanentToHand(context.State, permanent, context.Trace);
        }
    }
}

internal sealed class St2SourceTrashScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly bool _requireInheritedSource;
    private readonly int? _maxTargetLevel;
    private readonly int _trashCount;

    public St2SourceTrashScript(
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

internal sealed class St2NoSourceInheritedContinuousScript : ICardScript, IContinuousCardScript
{
    private readonly ContinuousModifierKind _modifierKind;
    private readonly int _amount;

    public St2NoSourceInheritedContinuousScript(
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

internal sealed class St2MattIshidaScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-12",
        "ST2_12",
        CardEffectPortingStatus.Implemented,
        "OnStartTurn memory +1 while the opponent has a Digimon with no digivolution sources; security play-self tamer uses PlayWithoutPayingCost from Executing.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-12:on-start-turn:memory-plus-1",
                EffectTiming.OnStartTurn,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    effectContext.Player == controller
                    && effectContext.State.TurnPlayerId == controller
                    && StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources(effectContext.State, controller)),
            new EffectDescriptor(
                "ST2-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                    effectContext.State,
                    context.SourceCard,
                    controller)),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST2-12:on-start-turn:memory-plus-1")
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                if (!StarterScriptSupport.IsTopSourceInBattleArea(
                    state,
                    context.Resolution.SourceCard,
                    context.Resolution.SourcePermanent))
                {
                    throw new DomainException("ST2-12 memory gain requires this tamer in battle area.");
                }

                if (!StarterScriptSupport.HasOpponentBattleAreaDigimonWithoutSources(state, controller))
                {
                    throw new DomainException("ST2-12 memory gain requires an opponent Digimon with no digivolution sources.");
                }

                primitives.ModifyMemory(state, controller, 1);
            });
            return;
        }

        if (context.Resolution.StableId == "ST2-12:security:play-self-tamer")
        {
            context.WithState((state, primitives) =>
            {
                var controller = context.Resolution.Controller
                    ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
                var sourceCard = context.Resolution.SourceCard
                    ?? throw new DomainException("ST2-12 security play-self requires a source card.");
                var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
                primitives.PlayWithoutPayingCost(
                    state,
                    controller,
                    sourceCard,
                    Zone.Executing,
                    frame,
                    suspended: false);
            });
            return;
        }

        throw new DomainException($"ST2-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
    }
}

internal sealed class St2HowlingBlasterScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-14",
        "ST2_14",
        CardEffectPortingStatus.Implemented,
        "Main/security choose an opponent Digimon with no digivolution sources and apply temporary CannotAttack/CannotBlock restrictions through generic duration modifiers.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-14:option:attack-block-restriction-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonWithoutSourcesCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-14:option:attack-block-restriction",
                    "Select 1 opponent Digimon with no digivolution sources that cannot attack or block."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST2-14:security:attack-block-restriction-selection",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonWithoutSourcesCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-14:security:attack-block-restriction",
                    "Select 1 opponent Digimon with no digivolution sources that cannot attack or block."),
                SelectionContinuation: ApplySelection),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST2-14 requires SelectionResultApplicator for its attack/block restriction body.");

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var durationScope = context.Resolution.StableId switch
        {
            "ST2-14:option:attack-block-restriction-selection" => DurationScope.UntilOpponentTurnEnd,
            "ST2-14:security:attack-block-restriction-selection" => DurationScope.UntilOwnerTurnEnd,
            _ => throw new DomainException($"ST2-14 cannot apply unknown selection '{context.Resolution.StableId}'."),
        };

        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            if (!StarterScriptSupport.IsOpponentBattleAreaDigimonWithoutSources(context.State, controller, permanent))
            {
                throw new DomainException("ST2-14 target must be an opponent battle area Digimon with no digivolution sources.");
            }

            context.Primitives.AddTemporaryAttackRestriction(
                context.State,
                permanent,
                durationScope,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"{context.Resolution.StableId}:cannot-attack:{permanent.Value}",
                debugLabel: "ST2-14 cannot attack");
            context.Primitives.AddTemporaryBlockRestriction(
                context.State,
                permanent,
                durationScope,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"{context.Resolution.StableId}:cannot-block:{permanent.Value}",
                debugLabel: "ST2-14 cannot block");
        }
    }
}

internal sealed class St2WereGarurumonScript : ICardScript
{
    private const string SelectedSourcePermanentKey = "SelectedSourcePermanent";

    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-15",
        "ST2_15",
        CardEffectPortingStatus.Implemented,
        "Main/security choose one of your battle area Digimon, then choose one playable Digimon digivolution source under it and play it as a new Digimon without paying cost through chained SelectionResultApplication and ZoneMover-backed primitive.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-15:option:select-source-host",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
                        effectContext.State,
                        controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOwnerBattleAreaDigimonWithPlayableEvolutionSourceSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST2-15:option:source-host",
                    "Select 1 of your Digimon that has a playable Digimon digivolution source."),
                SelectionContinuation: ContinueWithSourceCardSelection),
            new EffectDescriptor(
                "ST2-15:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
                        effectContext.State,
                        controller).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST2-15:security:main-option-activation")
        {
            throw new DomainException("ST2-15 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST2-15 two-stage source play must be resolved through SelectionResultApplicator.");
    }

    private static void ContinueWithSourceCardSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        var selectedPermanent = context.SelectedPermanentIds.SingleOrDefault();
        if (selectedPermanent == default)
        {
            throw new DomainException("ST2-15 requires exactly one selected source host Digimon.");
        }

        if (!StarterScriptSupport.HasPlayableEvolutionSource(context.State, controller, selectedPermanent))
        {
            throw new DomainException("ST2-15 selected Digimon no longer has a playable Digimon digivolution source.");
        }

        var request = StarterScriptSupport.CreatePlayableEvolutionSourceCardSelectionRequest(
            context.State,
            controller,
            selectedPermanent,
            $"ST2-15:option:source-card:{selectedPermanent.Value}",
            "Select 1 Digimon digivolution source to play without paying its cost.");

        var values = new Dictionary<string, object?>(context.Resolution.Context.Payload, StringComparer.Ordinal)
        {
            [SelectedSourcePermanentKey] = selectedPermanent,
        };
        var nextContext = new EffectContext(
            context.State,
            context.Resolution.Timing,
            controller,
            context.Resolution.SourceCard,
            context.Resolution.SourcePermanent,
            values);

        context.SetNextResolution(new EffectResolution(
            $"ST2-15:option:play-source-card:{selectedPermanent.Value}",
            context.Resolution.Timing,
            context.Resolution.SourceCard,
            context.Resolution.SourcePermanent,
            controller,
            IsBackground: false,
            IsOptional: false,
            nextContext,
            SelectionRequest: request,
            SelectionContinuation: PlaySelectedSourceCard));
    }

    private static void PlaySelectedSourceCard(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        if (context.Resolution.Context.GetValueOrDefault(SelectedSourcePermanentKey) is not PermanentId sourcePermanent)
        {
            throw new DomainException("ST2-15 source-card selection requires the selected host permanent context.");
        }

        foreach (var selectedCard in context.SelectedCardIds)
        {
            if (!StarterScriptSupport.IsPlayableEvolutionSource(context.State, controller, sourcePermanent, selectedCard))
            {
                throw new DomainException("ST2-15 selected card is no longer a playable Digimon digivolution source.");
            }

            var targetFrame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(context.State, controller);
            context.Primitives.PlayEvolutionSourceAsNewPermanent(
                context.State,
                controller,
                selectedCard,
                sourcePermanent,
                targetFrame,
                suspended: false);
        }
    }
}

internal sealed class St2MetalGarurumonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST2-11",
        "ST2_11",
        CardEffectPortingStatus.Implemented,
        "Top-card once-per-turn OnAllyAttack unsuspends this Digimon through TriggerPipelineService and Tier1PrimitiveService.Unsuspend.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST2-11:on-ally-attack:unsuspend-this-digimon",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && StarterScriptSupport.IsSourcePermanentAttacker(effectContext, context.SourcePermanent)
                    && context.SourcePermanent is not null
                    && BattleRules.Permanent(effectContext.State, context.SourcePermanent.Value).IsSuspended,
                IsOncePerTurn: true,
                OncePerTurnKey: "ST2-11:on-ally-attack:unsuspend-this-digimon"),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            if (context.Resolution.StableId != "ST2-11:on-ally-attack:unsuspend-this-digimon")
            {
                throw new DomainException($"ST2-11 cannot resolve unknown effect '{context.Resolution.StableId}'.");
            }

            var sourcePermanent = context.Resolution.SourcePermanent
                ?? throw new DomainException("ST2-11 unsuspend requires a source permanent.");
            if (!StarterScriptSupport.IsTopSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this card as the top card in battle area.");
            }

            if (!StarterScriptSupport.IsSourcePermanentAttacker(context.Resolution.Context, sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this Digimon to be the attacker.");
            }

            if (!primitives.Unsuspend(state, sourcePermanent))
            {
                throw new DomainException("ST2-11 unsuspend requires this Digimon to be suspended.");
            }
        });
}

internal sealed class St3AngewomonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-09",
        "ST3_09",
        CardEffectPortingStatus.Implemented,
        "Original OnEnterFieldAnyone plus CanTriggerWhenDigivolving maps to WhenDigivolving; security 3 or fewer condition recovers 1 from deck through Tier1PrimitiveService.RecoverFromDeck.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-09:when-digivolving:recovery-plus-1-deck",
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsTopSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Security.Count <= 3
                    && effectContext.State.GetPlayer(controller).Deck.Count > 0),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            if (context.Resolution.StableId != "ST3-09:when-digivolving:recovery-plus-1-deck")
            {
                throw new DomainException($"ST3-09 cannot resolve unknown effect '{context.Resolution.StableId}'.");
            }

            if (!StarterScriptSupport.IsTopSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST3-09 recovery requires this card as the top card in battle area.");
            }

            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.GetPlayer(controller).Security.Count > 3)
            {
                throw new DomainException("ST3-09 recovery requires 3 or fewer security cards.");
            }

            primitives.RecoverFromDeck(state, controller, count: 1, context.Trace);
        });
}

internal enum TriggerBody
{
    DpPlus1000,
    MemoryPlus1,
}

internal sealed class St3DpZeroDeletionTriggerScript : ICardScript
{
    private readonly TriggerBody _body;

    public St3DpZeroDeletionTriggerScript(
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

internal sealed class St3TakeruTakaishiScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-12",
        "ST3_12",
        CardEffectPortingStatus.Implemented,
        "Opponent-turn Security Digimon DP +2000 aura is represented by continuous SecurityDigimonDP; security play-self tamer reuses PlayWithoutPayingCost from Executing.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => StarterScriptSupport.CanPlaySelfPermanentFromExecuting(
                    effectContext.State,
                    context.SourceCard,
                    controller)),
        };
    }

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        int Amount(ContinuousEffectEvaluationContext _) => 2000;

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPlayer == context.Controller
            && evaluation.State.TurnPlayerId == StarterScriptSupport.Opponent(evaluation.State, context.Controller)
            && StarterScriptSupport.IsTopSourceInBattleArea(
                evaluation.State,
                context.SourceCard,
                context.SourcePermanent);

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST3-12:continuous:security-digimon-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.OwnerPlayer,
                ContinuousModifierKind.SecurityDigimonDP,
                Amount,
                Condition,
                "ST3-12 opponent-turn Security Digimon DP +2000")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != "ST3-12:security:play-self-tamer")
        {
            throw new DomainException($"ST3-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-12 security play-self requires a source card.");
            var frame = StarterScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
            primitives.PlayWithoutPayingCost(
                state,
                controller,
                sourceCard,
                Zone.Executing,
                frame,
                suspended: false);
        });
    }
}

internal sealed class St3PatamonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-05",
        "ST3_05",
        CardEffectPortingStatus.Implemented,
        "Inherited OnAllyAttack security-count condition gains 1 memory through TriggerPipelineService and Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-05:on-ally-attack:memory-plus-1",
                EffectTiming.OnAllyAttack,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.IsInheritedSourceInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && effectContext.State.GetPlayer(controller).Security.Count >= 4),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.GetPlayer(controller).Security.Count < 4)
            {
                throw new DomainException("ST3-05 memory gain requires 4 or more security cards.");
            }

            if (!StarterScriptSupport.IsInheritedSourceInBattleArea(
                state,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST3-05 memory gain requires the source card as a digivolution source in battle area.");
            }

            primitives.ModifyMemory(state, controller, 1);
        });
}

internal sealed class St3OnAttackDpReductionScript : ICardScript
{
    private readonly int _amount;
    private readonly bool _requireInheritedSource;

    public St3OnAttackDpReductionScript(
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

internal sealed class St3HolyWaveScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-15",
        "ST3_15",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon SecurityAttack -3 until opponent turn end; security applies opponent player-wide SecurityAttack -1 until turn end.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-15:option:security-attack-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-15:option:security-attack",
                    "1 of your opponent's Digimon gets SecurityAttack -3 until the end of your opponent's next turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-15:security:opponent-security-attack-minus-1",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-15:option:security-attack-selection")
        {
            throw new DomainException("ST3-15 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-15:security:opponent-security-attack-minus-1")
        {
            throw new DomainException($"ST3-15 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var opponent = StarterScriptSupport.Opponent(state, controller);
            primitives.AddTemporarySecurityAttackModifier(
                state,
                opponent,
                amount: -1,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-15 opponent Digimon SecurityAttack -1 until turn end");
        });
    }

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporarySecurityAttackModifier(
                context.State,
                permanent,
                amount: -3,
                DurationScope.UntilOpponentTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-15 SecurityAttack -3 until opponent turn end");
        }
    }
}

internal sealed class St3SevenHeavensScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-16",
        "ST3_16",
        CardEffectPortingStatus.Implemented,
        "Main option DP -10000 selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-16:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-16:option:dp",
                    "1 of your opponent's Digimon gets -10000 DP for the turn."),
                SelectionContinuation: ApplySelection),
            new EffectDescriptor(
                "ST3-16:security:main-option-activation",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId.EndsWith(":security:main-option-activation", StringComparison.Ordinal))
        {
            throw new DomainException("ST3-16 security main-option activation must be resolved through SecurityEffectExecutionService.");
        }

        throw new DomainException("ST3-16 main option selection must be resolved through SelectionResultApplicator.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: -10000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-16 DP -10000 until turn end");
        }
    }
}

internal sealed class St3HolyFlameScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-13",
        "ST3_13",
        CardEffectPortingStatus.Implemented,
        "Main option owner Digimon DP +3000 selection; security applies player-wide field DP +5000 and Security Digimon DP +5000 until turn end, then moves this card from Executing to Hand through ZoneMover.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-13:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-13:option:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-13:security:dp-plus-5000-and-add-to-hand",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-13:option:dp-selection")
        {
            throw new DomainException("ST3-13 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-13:security:dp-plus-5000-and-add-to-hand")
        {
            throw new DomainException($"ST3-13 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporaryPlayerDPModifier(
                state,
                controller,
                amount: 5000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-13 all owner Digimon DP +5000 until turn end");
            primitives.AddTemporarySecurityDigimonDPModifier(
                state,
                controller,
                amount: 5000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-13 owner Security Digimon DP +5000 until turn end");

            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-13 security effect requires a source card.");
            primitives.AddExecutingSecurityEffectCardToHand(state, sourceCard, controller);
        });
    }

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
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
                debugLabel: "ST3-13 DP +3000 until turn end");
        }
    }
}

internal sealed class St3HeavensCharmScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST3-14",
        "ST3_14",
        CardEffectPortingStatus.Implemented,
        "Main option opponent Digimon DP -2000 selection; security moves this card from Executing to Hand through ZoneMover.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST3-14:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    StarterScriptSupport.OpponentBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => StarterScriptSupport.CreateOpponentBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST3-14:option:dp",
                    "1 of your opponent's Digimon gets -2000 DP for the turn."),
                SelectionContinuation: ApplyOptionSelection),
            new EffectDescriptor(
                "ST3-14:security:add-to-hand",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST3-14:option:dp-selection")
        {
            throw new DomainException("ST3-14 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST3-14:security:add-to-hand")
        {
            throw new DomainException($"ST3-14 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? StarterScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST3-14 security effect requires a source card.");
            primitives.AddExecutingSecurityEffectCardToHand(state, sourceCard, controller);
        });
    }

    private static void ApplyOptionSelection(SelectionResultApplicationContext context)
    {
        var controller = context.Resolution.Controller
            ?? StarterScriptSupport.RequireSourceCardOwner(context.State, context.Resolution.SourceCard);
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.AddTemporaryDPModifier(
                context.State,
                permanent,
                amount: -2000,
                DurationScope.UntilTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST3-14 DP -2000 until turn end");
        }
    }
}

internal static class StarterScriptSupport
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

    public static bool IsTopSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        var permanent = FindPermanent(state, sourcePermanent, sourceCard.Value);
        return permanent is { IsBreedingArea: false } && permanent.TopCardId == sourceCard.Value;
    }

    public static bool HasOpponentBattleAreaDigimonWithoutSources(GameState state, PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Any(permanent =>
                permanent.ControllerPlayerId == opponent
                && BattleRules.IsDigimon(state, permanent.TopCardId)
                && permanent.SourceCardIds.Count == 0);
    }

    public static bool IsOpponentBattleAreaDigimonWithoutSources(
        GameState state,
        PlayerId controller,
        PermanentId permanentId)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Any(permanent =>
                permanent.Id == permanentId
                && permanent.ControllerPlayerId == opponent
                && BattleRules.IsDigimon(state, permanent.TopCardId)
                && permanent.SourceCardIds.Count == 0);
    }

    public static bool IsSourcePermanentAttacker(
        EffectContext context,
        PermanentId? sourcePermanent) =>
        sourcePermanent is not null
        && context.GetValueOrDefault("Attacker") is PermanentId attacker
        && attacker == sourcePermanent.Value;

    public static bool IsInheritedSourceInBattleArea(
        GameState state,
        CardInstanceId? sourceCard,
        PermanentId? sourcePermanent)
    {
        if (sourceCard is null)
        {
            return false;
        }

        var permanent = FindPermanent(state, sourcePermanent, sourceCard.Value);
        return permanent is { IsBreedingArea: false } && permanent.SourceCardIds.Contains(sourceCard.Value);
    }

    public static bool HasPlayableEvolutionSource(
        GameState state,
        PlayerId controller,
        PermanentId permanentId)
    {
        var permanent = state.GetPlayer(controller)
            .BattleAreaPermanents
            .FirstOrDefault(candidate =>
                candidate.Id == permanentId
                && candidate.ControllerPlayerId == controller
                && BattleRules.IsDigimon(state, candidate.TopCardId));
        return permanent is not null
            && state.GetPlayer(controller).BattleAreaPermanents.Count() < state.Config.FieldSlotCount
            && permanent.SourceCardIds.Any(source => IsPlayableEvolutionSource(state, controller, permanent.Id, source));
    }

    public static bool IsPlayableEvolutionSource(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent,
        CardInstanceId sourceCard)
    {
        var player = state.GetPlayer(controller);
        var permanent = player.BattleAreaPermanents.FirstOrDefault(candidate =>
            candidate.Id == sourcePermanent
            && candidate.ControllerPlayerId == controller
            && BattleRules.IsDigimon(state, candidate.TopCardId));
        if (permanent is null || !permanent.SourceCardIds.Contains(sourceCard))
        {
            return false;
        }

        if (player.BattleAreaPermanents.Count() >= state.Config.FieldSlotCount)
        {
            return false;
        }

        if (!state.Cards.TryGetValue(sourceCard, out var instance))
        {
            return false;
        }

        if (instance.Owner != controller
            || instance.CurrentZone != Zone.EvolutionSources
            || instance.PermanentId != sourcePermanent)
        {
            return false;
        }

        var definition = BattleRules.Definition(state, sourceCard);
        return definition.CardKinds.Contains(CardKind.Digimon)
            && definition.IsPermanent
            && !definition.CardKinds.Contains(CardKind.Option);
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

    public static int FirstEmptyBattleFrameForPlay(GameState state, PlayerId controller) =>
        BattleRules.FirstEmptyBattleFrame(state.GetPlayer(controller), state.Config.FieldSlotCount);

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonCandidates(
        GameState state,
        PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonWithSourcesCandidates(
        GameState state,
        PlayerId controller,
        int? maxLevel = null)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => permanent.SourceCardIds.Count > 0)
            .Where(permanent => maxLevel is null || BattleRules.Definition(state, permanent.TopCardId).Level <= maxLevel.Value)
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
    }

    public static IReadOnlyList<SelectableTarget> OpponentBattleAreaDigimonWithoutSourcesCandidates(
        GameState state,
        PlayerId controller)
    {
        var opponent = Opponent(state, controller);
        return state.GetPlayer(opponent)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == opponent)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => permanent.SourceCardIds.Count == 0)
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();
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

    public static IReadOnlyList<SelectableTarget> OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(
        GameState state,
        PlayerId controller) =>
        state.GetPlayer(controller)
            .BattleAreaPermanents
            .Where(permanent => permanent.ControllerPlayerId == controller)
            .Where(permanent => BattleRules.IsDigimon(state, permanent.TopCardId))
            .Where(permanent => HasPlayableEvolutionSource(state, controller, permanent.Id))
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Label: BattleRules.Definition(state, permanent.TopCardId).CardId,
                Zone: Zone.BattleArea))
            .ToArray();

    public static IReadOnlyList<SelectableTarget> PlayableEvolutionSourceCardCandidates(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent)
    {
        var permanent = BattleRules.Permanent(state, sourcePermanent);
        return permanent.SourceCardIds
            .Where(source => IsPlayableEvolutionSource(state, controller, sourcePermanent, source))
            .Select(source => new SelectableTarget(
                SelectionTargetKind.Card,
                $"card:{source.Value}",
                controller,
                Card: source,
                Permanent: sourcePermanent,
                Label: BattleRules.Definition(state, source).CardId,
                Zone: Zone.EvolutionSources))
            .ToArray();
    }

    public static SelectionRequest CreateOpponentBattleAreaDigimonSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OpponentBattleAreaDigimonCandidates(state, controller);
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

    public static SelectionRequest CreateOpponentBattleAreaDigimonWithSourcesSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt,
        int? maxLevel = null)
    {
        var candidates = OpponentBattleAreaDigimonWithSourcesCandidates(state, controller, maxLevel);
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

    public static SelectionRequest CreateOpponentBattleAreaDigimonWithoutSourcesSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OpponentBattleAreaDigimonWithoutSourcesCandidates(state, controller);
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

    public static SelectionRequest CreateOwnerBattleAreaDigimonWithPlayableEvolutionSourceSelectionRequest(
        GameState state,
        PlayerId controller,
        string requestId,
        string prompt)
    {
        var candidates = OwnerBattleAreaDigimonWithPlayableEvolutionSourceCandidates(state, controller);
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

    public static SelectionRequest CreatePlayableEvolutionSourceCardSelectionRequest(
        GameState state,
        PlayerId controller,
        PermanentId sourcePermanent,
        string requestId,
        string prompt)
    {
        var candidates = PlayableEvolutionSourceCardCandidates(state, controller, sourcePermanent);
        return new SelectionRequest(
            requestId,
            controller,
            SelectionKind.SelectCard,
            SelectionTargetKind.Card,
            minCount: 1,
            maxCount: Math.Min(1, candidates.Count),
            canSkip: false,
            canEndNotMax: false,
            candidates,
            prompt);
    }

    private static PermanentState? FindPermanent(
        GameState state,
        PermanentId? sourcePermanent,
        CardInstanceId sourceCard)
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
