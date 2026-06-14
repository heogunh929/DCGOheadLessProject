using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public static class St1CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateScripts() =>
        new ICardScript[]
        {
            new St1InheritedDpScript(
                "ST1-01",
                "ST1_01",
                requiredSourceCount: 4,
                "Inherited owner-turn DP +1000 while the host Digimon has 4 or more digivolution cards is represented by ContinuousEffectDescriptor."),
            new NoEffectCardScript("ST1-02", notes: "No ST1_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1InheritedDpScript(
                "ST1-03",
                "ST1_03",
                requiredSourceCount: 0,
                "Inherited owner-turn DP +1000 is represented by ContinuousEffectDescriptor."),
            new NoEffectCardScript("ST1-04", notes: "No ST1_04 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new NoEffectCardScript("ST1-05", notes: "No ST1_05 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1CoredramonScript(),
            new DeclaredCapabilityCardScript(
                "ST1-07",
                "ST1_07",
                "Inherited Security Attack +1 is represented by CardDefinition.SecurityAttackModifier."),
            new St1GarudamonScript(),
            new St1MetalGreymonScript(),
            new NoEffectCardScript("ST1-10", notes: "No ST1_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1WarGreymonScript(),
            new St1TaiKamiyaScript(),
            new St1ShadowWingScript(),
            new St1StarlightExplosionScript(),
            new St1GigaDestroyerScript(),
            new St1GaiaForceScript(),
        };

    public static CardScriptRegistry CreateRegistry() => new(CreateScripts());

    private static ICardScript Unsupported(string cardId, string effectClassName, string reason) =>
        new UnsupportedCardScript(cardId, effectClassName, reason);
}

internal sealed class St1GarudamonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-08",
        "ST1_08",
        CardEffectPortingStatus.Implemented,
        "WhenDigivolving owner battle area Digimon selection applies temporary DP +3000 until turn end through SelectionResultApplicator and Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-08:when-digivolving:dp-selection",
                EffectTiming.WhenDigivolving,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    St1ScriptSupport.IsSourcePermanentInBattleArea(
                        effectContext.State,
                        context.SourceCard,
                        context.SourcePermanent)
                    && St1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => St1ScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST1-08:when-digivolving:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: St1TemporaryDpSelectionSupport.ApplyPlus3000UntilTurnEnd),
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST1-08 requires SelectionResultApplicator for its WhenDigivolving selection body.");
}

internal sealed class St1ShadowWingScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-13",
        "ST1_13",
        CardEffectPortingStatus.Implemented,
        "Main option owner Digimon selection applies temporary DP +3000 until turn end; security effect applies player-wide SecurityAttack +1 until owner turn end.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-13:option:dp-selection",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext =>
                    St1ScriptSupport.OwnerBattleAreaDigimonCandidates(effectContext.State, controller).Count > 0,
                CreateSelectionRequest: effectContext => St1ScriptSupport.CreateOwnerBattleAreaDigimonSelectionRequest(
                    effectContext.State,
                    controller,
                    "ST1-13:option:dp",
                    "1 of your Digimon gets +3000 DP for the turn."),
                SelectionContinuation: St1TemporaryDpSelectionSupport.ApplyPlus3000UntilTurnEnd),
            new EffectDescriptor(
                "ST1-13:security:security-attack-plus-1",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId == "ST1-13:option:dp-selection")
        {
            throw new DomainException("ST1-13 main option selection must be resolved through SelectionResultApplicator.");
        }

        if (context.Resolution.StableId != "ST1-13:security:security-attack-plus-1")
        {
            throw new DomainException($"ST1-13 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporarySecurityAttackModifier(
                state,
                controller,
                amount: 1,
                DurationScope.UntilOwnerTurnEnd,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: "ST1-13 SecurityAttack +1 until owner turn end");
        });
    }
}

internal sealed class St1StarlightExplosionScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-14",
        "ST1_14",
        CardEffectPortingStatus.Implemented,
        "Main option and security effect apply player-wide Security Digimon DP +7000 duration modifiers through Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-14:option:security-digimon-dp",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
            new EffectDescriptor(
                "ST1-14:security:security-digimon-dp",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller),
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        var duration = context.Resolution.StableId switch
        {
            "ST1-14:option:security-digimon-dp" => DurationScope.UntilOpponentTurnEnd,
            "ST1-14:security:security-digimon-dp" => DurationScope.UntilTurnEnd,
            _ => throw new DomainException($"ST1-14 cannot resolve unknown effect '{context.Resolution.StableId}'."),
        };

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            primitives.AddTemporarySecurityDigimonDPModifier(
                state,
                controller,
                amount: 7000,
                duration,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                debugLabel: $"ST1-14 Security Digimon DP +7000 until {duration}");
        });
    }
}

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

internal sealed class St1GigaDestroyerScript : St1OptionDeleteScript
{
    public St1GigaDestroyerScript()
        : base(
            "ST1-15",
            "ST1_15",
            "Main option deletion selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.",
            maxTargets: 2,
            canEndNotMax: true,
            targetPredicate: (state, permanent) => BattleRules.Dp(state, permanent) <= 4000)
    {
    }
}

internal sealed class St1GaiaForceScript : St1OptionDeleteScript
{
    public St1GaiaForceScript()
        : base(
            "ST1-16",
            "ST1_16",
            "Main option deletion selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.",
            maxTargets: 1,
            canEndNotMax: false,
            targetPredicate: (_, _) => true)
    {
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

internal sealed class DeclaredCapabilityCardScript : ICardScript
{
    public DeclaredCapabilityCardScript(string cardId, string effectClassName, string notes)
    {
        Porting = new CardEffectPortingRecord(cardId, effectClassName, CardEffectPortingStatus.Implemented, notes);
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException($"Card script '{Porting.CardId}' is implemented as a declared capability and has no queued resolve body.");
}

internal sealed class St1InheritedDpScript : ICardScript, IContinuousCardScript
{
    private readonly int _requiredSourceCount;

    public St1InheritedDpScript(string cardId, string effectClassName, int requiredSourceCount, string notes)
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

internal sealed class St1WarGreymonScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-11",
        "ST1_11",
        CardEffectPortingStatus.Implemented,
        "Owner-turn dynamic SecurityAttack is derived from the top card's digivolution source count through ContinuousEffectDescriptor.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        Array.Empty<EffectDescriptor>();

    public IReadOnlyList<ContinuousEffectDescriptor> CreateContinuousEffectDescriptors(ContinuousEffectScriptContext context)
    {
        if (context.SourceKind != ContinuousEffectSourceKind.FieldTop)
        {
            return Array.Empty<ContinuousEffectDescriptor>();
        }

        int Amount(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.TargetPermanent?.SourceCardIds.Count / 2 ?? 0;

        bool Condition(ContinuousEffectEvaluationContext evaluation)
        {
            if (evaluation.TargetPermanent is not { } target)
            {
                return false;
            }

            return evaluation.State.TurnPlayerId == context.Controller
                && !target.IsBreedingArea
                && target.TopCardId == context.SourceCard
                && target.SourceCardIds.Count >= 2;
        }

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST1-11:continuous:security-attack:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.SelfPermanent,
                ContinuousModifierKind.SecurityAttack,
                Amount,
                Condition,
                "ST1-11 dynamic SecurityAttack from source count")
        };
    }

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException("ST1-11 has only continuous SecurityAttack effects and no queued resolve body.");
}

internal sealed class St1TaiKamiyaScript : ICardScript, IContinuousCardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-12",
        "ST1_12",
        CardEffectPortingStatus.Implemented,
        "Main field DP +1000 effect is implemented through ContinuousEffectDescriptor; security play-self tamer plays itself from Executing without paying memory cost through Tier1PrimitiveService.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var controller = context.Controller
            ?? St1ScriptSupport.RequireSourceCardOwner(context.State, context.SourceCard);

        return new[]
        {
            new EffectDescriptor(
                "ST1-12:security:play-self-tamer",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: controller,
                CanTrigger: effectContext => St1ScriptSupport.CanPlaySelfPermanentFromExecuting(
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

        int Amount(ContinuousEffectEvaluationContext _) => 1000;

        bool Condition(ContinuousEffectEvaluationContext evaluation) =>
            evaluation.State.TurnPlayerId == context.Controller
            && evaluation.TargetPermanent is { IsBreedingArea: false }
            && St1ScriptSupport.IsSourcePermanentInBattleArea(
                evaluation.State,
                context.SourceCard,
                context.SourcePermanent);

        return new[]
        {
            new ContinuousEffectDescriptor(
                $"ST1-12:continuous:tamer-aura-dp:{context.SourceCard.Value}",
                context.SourceCard,
                context.SourcePermanent,
                context.Controller,
                ContinuousEffectTargetKind.OwnerBattleAreaDigimon,
                ContinuousModifierKind.DP,
                Amount,
                Condition,
                "ST1-12 owner battle area Digimon DP +1000")
        };
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId != "ST1-12:security:play-self-tamer")
        {
            throw new DomainException($"ST1-12 cannot resolve unknown effect '{context.Resolution.StableId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            var sourceCard = context.Resolution.SourceCard
                ?? throw new DomainException("ST1-12 security play-self requires a source card.");
            var frame = St1ScriptSupport.FirstEmptyBattleFrameForPlay(state, controller);
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

internal sealed class St1CoredramonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-06",
        "ST1_06",
        CardEffectPortingStatus.Implemented,
        "Blocker is represented by CardDefinition.BattleKeywords. OnAllyAttack loses 2 memory through Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                "ST1-06:on-ally-attack:memory-minus-2",
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
                throw new DomainException("ST1-06 memory loss requires the source permanent in the battle area.");
            }

            primitives.ModifyMemory(state, owner, -2);
        });
}

internal sealed class St1MetalGreymonScript : ICardScript
{
    public CardEffectPortingRecord Porting { get; } = new(
        "ST1-09",
        "ST1_09",
        CardEffectPortingStatus.Implemented,
        "Inherited OnBlockAnyone owner-turn memory +3 is represented with Tier1PrimitiveService.ModifyMemory.");

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                "ST1-09:on-block:anyone:memory-plus-3",
                EffectTiming.OnBlockAnyone,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: effectContext =>
                {
                    var owner = St1ScriptSupport.TrySourceCardOwner(effectContext.State, context.SourceCard);
                    return owner is not null
                        && effectContext.State.TurnPlayerId == owner.Value
                        && St1ScriptSupport.IsSourcePermanentInBattleArea(
                            effectContext.State,
                            context.SourceCard,
                            context.SourcePermanent)
                        && (effectContext.SourcePermanent is null || effectContext.SourcePermanent == context.SourcePermanent);
                }),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var owner = St1ScriptSupport.RequireSourceCardOwner(state, context.Resolution.SourceCard);
            if (state.TurnPlayerId != owner)
            {
                throw new DomainException("ST1-09 memory gain requires the source card owner's turn.");
            }

            if (!St1ScriptSupport.IsSourcePermanentInBattleArea(state, context.Resolution.SourceCard, context.Resolution.SourcePermanent))
            {
                throw new DomainException("ST1-09 memory gain requires the source permanent in the battle area.");
            }

            primitives.ModifyMemory(state, owner, 3);
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
