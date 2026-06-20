using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Setup;

internal static class CardEffectTestFixture
{
    public static CardDefinition NoEffectDefinition(string cardId) =>
        new()
        {
            CardId = cardId,
            Name = cardId,
            CardKinds = new[] { CardKind.Digimon },
            Colors = new[] { CardColor.Red },
            Level = 3,
            PlayCost = 3,
            Dp = 3000,
        };

    public static CardDefinition EffectDefinition(
        string cardId,
        string effectClassName,
        IReadOnlyList<Mechanic>? mechanics = null,
        IReadOnlyList<BattleKeyword>? battleKeywords = null) =>
        NoEffectDefinition(cardId) with
        {
            CardEffectClassName = effectClassName,
            Mechanics = mechanics ?? Array.Empty<Mechanic>(),
            BattleKeywords = battleKeywords ?? Array.Empty<BattleKeyword>(),
        };

    public static CardDefinition OptionEffectDefinition(string cardId, string effectClassName, int playCost = 1) =>
        EffectDefinition(cardId, effectClassName) with
        {
            CardKinds = new[] { CardKind.Option },
            Level = 0,
            PlayCost = playCost,
            Dp = 0,
        };

    public static ICardDatabase Database(params CardDefinition[] definitions) =>
        new InMemoryCardDatabase(definitions);

    public static PlayerDeckList Deck(PlayerId player, params string[] mainDeck) =>
        new(player, mainDeck, Array.Empty<string>());

    public static CardScriptRegistry Registry(params ICardScript[] scripts) =>
        new(scripts);

    public static EffectResolution Resolution(GameState state, EffectTiming timing, PlayerId player, CardInstanceId sourceCard) =>
        new(
            $"test:{sourceCard.Value}:{timing}",
            timing,
            sourceCard,
            SourcePermanent: null,
            Controller: player,
            IsBackground: false,
            IsOptional: false,
            new EffectContext(state, timing, player, sourceCard));
}

internal sealed class PrimitiveDrawCardScript : ICardScript
{
    public PrimitiveDrawCardScript(string cardId, string effectClassName)
    {
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that draws through Tier1PrimitiveService.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:draw",
                EffectTiming.OnDraw,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        context.WithState((state, primitives) =>
        {
            var player = context.Resolution.Controller ?? PlayerId.Player0;
            primitives.Draw(state, player, 1);
        });
}

internal enum SelectionPrimitiveMode
{
    ModifyDp,
    Destroy,
}

internal sealed class SelectionPrimitiveCardScript : ICardScript
{
    private readonly SelectionPrimitiveMode _mode;
    private readonly PlayerId _targetController;
    private readonly int _minCount;
    private readonly int _maxCount;
    private readonly bool _canSkip;
    private readonly bool _canEndNotMax;
    private readonly EffectTiming _timing;

    public SelectionPrimitiveCardScript(
        string cardId,
        string effectClassName,
        SelectionPrimitiveMode mode,
        PlayerId targetController,
        int minCount = 1,
        int maxCount = 1,
        bool canSkip = false,
        bool canEndNotMax = false,
        EffectTiming timing = EffectTiming.OptionSkill)
    {
        _mode = mode;
        _targetController = targetController;
        _minCount = minCount;
        _maxCount = maxCount;
        _canSkip = canSkip;
        _canEndNotMax = canEndNotMax;
        _timing = timing;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that resolves a SelectionRequest through SelectionResultApplicator.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:selection:{_mode}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CreateSelectionRequest: effectContext => CreateSelectionRequest(
                    effectContext.State,
                    context.Controller ?? effectContext.Player ?? PlayerId.Player0),
                SelectionContinuation: ApplySelection),
        };

    public void Resolve(CardScriptExecutionContext context) =>
        throw new DomainException(
            $"Card script '{Porting.CardId}' must be resolved through SelectionResultApplicator.");

    private SelectionRequest CreateSelectionRequest(GameState state, PlayerId selectionPlayer)
    {
        var candidates = state.GetPlayer(_targetController)
            .BattleAreaPermanents
            .Select(permanent =>
            {
                var topCard = state.Cards[permanent.TopCardId];
                var definition = state.CardDefinitions[topCard.DefinitionId];
                return new SelectableTarget(
                    SelectionTargetKind.Permanent,
                    $"permanent:{permanent.Id.Value}",
                    permanent.ControllerPlayerId,
                    Permanent: permanent.Id,
                    Label: definition.CardId,
                    Zone: Zone.BattleArea);
            })
            .ToArray();

        return new SelectionRequest(
            $"test-selection:{Porting.CardId}",
            selectionPlayer,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            _minCount,
            _maxCount,
            _canSkip,
            _canEndNotMax,
            candidates,
            "Select test permanent.");
    }

    private void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            if (_mode == SelectionPrimitiveMode.ModifyDp)
            {
                context.Primitives.ModifyDP(context.State, permanent, -1000);
            }
            else
            {
                context.Primitives.DestroyPermanent(context.State, permanent, context.Trace);
            }
        }
    }
}

internal sealed class OptionalSelectionPrimitiveCardScript : ICardScript
{
    private readonly PlayerId _targetController;
    private readonly EffectTiming _timing;
    private readonly int _tailMemory;
    private readonly bool _includeSecurityActivation;

    public OptionalSelectionPrimitiveCardScript(
        string cardId,
        string effectClassName,
        PlayerId targetController,
        EffectTiming timing = EffectTiming.OptionSkill,
        int tailMemory = 0,
        bool includeSecurityActivation = false)
    {
        _targetController = targetController;
        _timing = timing;
        _tailMemory = tailMemory;
        _includeSecurityActivation = includeSecurityActivation;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script for optional yes/no followed by explicit target selection.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context)
    {
        var descriptors = new List<EffectDescriptor>();
        if (_includeSecurityActivation)
        {
            descriptors.Add(new EffectDescriptor(
                $"{Porting.CardId}:security:activate-main",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                SecurityExecutionMode: SecurityEffectExecutionMode.ActivateMainOption));
        }

        descriptors.Add(new EffectDescriptor(
            $"{Porting.CardId}:optional-target",
            _timing,
            SourceCard: context.SourceCard,
            SourcePermanent: context.SourcePermanent,
            Controller: context.Controller,
            IsOptional: true,
            CreateSelectionRequest: effectContext => CreateTargetRequest(
                effectContext.State,
                context.Controller ?? effectContext.Player ?? PlayerId.Player0),
            SelectionContinuation: ApplySelection));

        if (_tailMemory != 0)
        {
            descriptors.Add(new EffectDescriptor(
                $"{Porting.CardId}:tail-memory:{_tailMemory}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller));
        }

        return descriptors;
    }

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId.Contains(":tail-memory:", StringComparison.Ordinal))
        {
            var player = context.Resolution.Controller
                ?? context.Resolution.Context.Player
                ?? PlayerId.Player0;
            context.WithState((state, primitives) => primitives.ModifyMemory(state, player, _tailMemory));
            return;
        }

        throw new DomainException(
            $"Card script '{Porting.CardId}' optional target body must be resolved through SelectionResultApplicator.");
    }

    private SelectionRequest CreateTargetRequest(GameState state, PlayerId player)
    {
        var candidates = state.GetPlayer(_targetController)
            .BattleAreaPermanents
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Zone: Zone.BattleArea))
            .ToArray();

        return new SelectionRequest(
            $"test-target:{Porting.CardId}",
            player,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: 1,
            canSkip: false,
            canEndNotMax: false,
            candidates,
            "Select optional target.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DestroyPermanent(context.State, permanent, context.Trace);
        }
    }
}

internal sealed class TimingMemoryCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly int _amount;
    private readonly bool _isBackground;
    private readonly bool _isOncePerTurn;
    private readonly bool _throwsUnsupported;

    public TimingMemoryCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        int amount,
        bool isBackground = false,
        bool isOncePerTurn = false,
        bool throwsUnsupported = false)
    {
        _timing = timing;
        _amount = amount;
        _isBackground = isBackground;
        _isOncePerTurn = isOncePerTurn;
        _throwsUnsupported = throwsUnsupported;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script for trigger pipeline timing and queue drain.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:memory:{_amount}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                IsBackground: _isBackground,
                IsOncePerTurn: _isOncePerTurn,
                OncePerTurnKey: $"{Porting.CardId}:{_timing}"),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        if (_throwsUnsupported)
        {
            throw new UnsupportedMechanicException($"Unsupported fixture body '{Porting.CardId}'.");
        }

        context.WithState((state, primitives) =>
        {
            var player = context.Resolution.Controller
                ?? context.Resolution.Context.Player
                ?? state.Cards[context.Resolution.SourceCard!.Value].Owner;
            primitives.ModifyMemory(state, player, _amount);
        });
    }
}

internal sealed class RecordingTimingCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly string _label;
    private readonly IList<string> _order;

    public RecordingTimingCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        string label,
        IList<string> order)
    {
        _timing = timing;
        _label = label;
        _order = order;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that records security timing resolution order.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:record:{_label}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context) => _order.Add(_label);
}

internal sealed class DoubleDescriptorRecordingCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly string _firstLabel;
    private readonly string _secondLabel;
    private readonly IList<string> _order;

    public DoubleDescriptorRecordingCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        string firstLabel,
        string secondLabel,
        IList<string> order)
    {
        _timing = timing;
        _firstLabel = firstLabel;
        _secondLabel = secondLabel;
        _order = order;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that emits two independent descriptors from one source card.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:record:{_firstLabel}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:record:{_secondLabel}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        if (context.Resolution.StableId.EndsWith(_firstLabel, StringComparison.Ordinal))
        {
            _order.Add(_firstLabel);
            return;
        }

        _order.Add(_secondLabel);
    }
}

internal sealed class ConditionalRecordingTimingCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly string _label;
    private readonly IList<string> _order;
    private readonly Func<EffectContext, bool> _canTrigger;

    public ConditionalRecordingTimingCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        string label,
        IList<string> order,
        Func<EffectContext, bool> canTrigger)
    {
        _timing = timing;
        _label = label;
        _order = order;
        _canTrigger = canTrigger;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that records only when CanTrigger is true.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:record:{_label}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CanTrigger: _canTrigger),
        };

    public void Resolve(CardScriptExecutionContext context) => _order.Add(_label);
}

internal sealed class DpZeroCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly string _targetDefinitionId;
    private readonly string _label;
    private readonly IList<string> _order;

    public DpZeroCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        string targetDefinitionId,
        string label,
        IList<string> order)
    {
        _timing = timing;
        _targetDefinitionId = targetDefinitionId;
        _label = label;
        _order = order;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that reduces a target permanent to DP zero.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:dp-zero:{_targetDefinitionId}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        context.WithState((state, primitives) =>
        {
            var target = state.Players
                .SelectMany(player => player.FieldPermanents)
                .FirstOrDefault(permanent => state.Cards[permanent.TopCardId].DefinitionId == _targetDefinitionId);
            if (target is null)
            {
                throw new DomainException($"{Porting.CardId} could not find target '{_targetDefinitionId}'.");
            }

            primitives.ModifyDP(state, target.Id, -10000);
            _order.Add(_label);
        });
    }
}

internal sealed class PermanentPresenceProbeScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly string _targetDefinitionId;
    private readonly string _label;
    private readonly IList<string> _order;

    public PermanentPresenceProbeScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        string targetDefinitionId,
        string label,
        IList<string> order)
    {
        _timing = timing;
        _targetDefinitionId = targetDefinitionId;
        _label = label;
        _order = order;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that records whether a target permanent still exists.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:probe:{_targetDefinitionId}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        context.WithState((state, _) =>
        {
            var exists = state.Players
                .SelectMany(player => player.FieldPermanents)
                .Any(permanent => state.Cards[permanent.TopCardId].DefinitionId == _targetDefinitionId);
            _order.Add($"{_label}:{(exists ? "present" : "absent")}");
        });
    }
}

internal sealed class SecurityAttackModifierCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly int _amount;
    private readonly DurationScope _durationScope;
    private readonly bool _targetAttacker;

    public SecurityAttackModifierCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        int amount,
        DurationScope durationScope,
        bool targetAttacker = true)
    {
        _timing = timing;
        _amount = amount;
        _durationScope = durationScope;
        _targetAttacker = targetAttacker;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that changes SecurityAttack through a card-local effect body.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:security-attack:{_amount}",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        context.WithState((state, primitives) =>
        {
            var controller = context.Resolution.Controller
                ?? context.Resolution.Context.Player
                ?? PlayerId.Player0;
            var target = _targetAttacker
                ? context.Resolution.Context.GetValueOrDefault("Attacker") as PermanentId?
                : context.Resolution.SourcePermanent;
            if (target is null)
            {
                throw new DomainException($"{Porting.CardId} requires a SecurityAttack target permanent.");
            }

            primitives.AddTemporarySecurityAttackModifier(
                state,
                target.Value,
                _amount,
                _durationScope,
                controller,
                context.Resolution.SourceCard,
                context.Resolution.SourcePermanent,
                stableId: $"{Porting.CardId}:{_timing}:security-attack:{_amount}:{state.TemporaryModifiers.Count + 1}");
        });
    }
}

internal sealed class SecuritySkillPlayHandCardScript : ICardScript
{
    private readonly string _definitionIdToPlay;
    private readonly int _targetFrameIndex;
    private readonly Zone _sourceZone;

    public SecuritySkillPlayHandCardScript(
        string cardId,
        string effectClassName,
        string definitionIdToPlay,
        int targetFrameIndex,
        Zone sourceZone = Zone.Hand)
    {
        _definitionIdToPlay = definitionIdToPlay;
        _targetFrameIndex = targetFrameIndex;
        _sourceZone = sourceZone;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture SecuritySkill that plays a card after security trigger candidates were prepared.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:security:play-hand:{_definitionIdToPlay}",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        context.WithState((state, primitives) =>
        {
            var player = context.Resolution.Controller
                ?? context.Resolution.Context.Player
                ?? PlayerId.Player0;
            var card = state.GetPlayer(player).CardsIn(_sourceZone)
                .FirstOrDefault(candidate => state.Cards[candidate].DefinitionId == _definitionIdToPlay);
            if (card.Value == 0)
            {
                throw new DomainException($"{Porting.CardId} could not find '{_definitionIdToPlay}' in '{_sourceZone}'.");
            }

            primitives.PlayWithoutPayingCost(
                state,
                player,
                card,
                _sourceZone,
                _targetFrameIndex);
        });
    }
}

internal sealed class SecuritySkillMovePermanentToLostScript : ICardScript
{
    private readonly string _definitionIdToMove;

    public SecuritySkillMovePermanentToLostScript(
        string cardId,
        string effectClassName,
        string definitionIdToMove)
    {
        _definitionIdToMove = definitionIdToMove;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture SecuritySkill that removes a prepared trigger source before that trigger resolves.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:security:move-source-lost:{_definitionIdToMove}",
                EffectTiming.SecuritySkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        context.WithState((state, primitives) =>
        {
            var permanent = state.Players
                .SelectMany(player => player.FieldPermanents)
                .FirstOrDefault(candidate => state.Cards[candidate.TopCardId].DefinitionId == _definitionIdToMove);
            if (permanent is null)
            {
                throw new DomainException($"{Porting.CardId} could not find source permanent '{_definitionIdToMove}'.");
            }

            primitives.MoveCard(
                state,
                new MoveCardCommand(
                    permanent.TopCardId,
                    permanent.IsBreedingArea ? Zone.BreedingArea : Zone.BattleArea,
                    Zone.Lost,
                    MoveReason.Effect,
                    SourcePermanent: permanent.Id,
                    FaceUp: true));
        });
    }
}

internal sealed class OnceThenSelectionCardScript : ICardScript
{
    private readonly EffectTiming _timing;
    private readonly PlayerId _targetController;

    public OnceThenSelectionCardScript(
        string cardId,
        string effectClassName,
        EffectTiming timing,
        PlayerId targetController)
    {
        _timing = timing;
        _targetController = targetController;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that registers once-per-turn state before a later selection pause.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:once-memory",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                IsOncePerTurn: true,
                OncePerTurnKey: $"{Porting.CardId}:{_timing}:once-memory"),
            new EffectDescriptor(
                $"{Porting.CardId}:{_timing}:selection",
                _timing,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller,
                CreateSelectionRequest: effectContext => CreateTargetRequest(
                    effectContext.State,
                    context.Controller ?? effectContext.Player ?? PlayerId.Player0),
                SelectionContinuation: ApplySelection),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        if (!context.Resolution.StableId.EndsWith(":once-memory", StringComparison.Ordinal))
        {
            throw new DomainException(
                $"Card script '{Porting.CardId}' selection body must be resolved through SelectionResultApplicator.");
        }

        context.WithState((state, primitives) =>
        {
            var player = context.Resolution.Controller
                ?? context.Resolution.Context.Player
                ?? PlayerId.Player0;
            primitives.ModifyMemory(state, player, amount: 1);
        });
    }

    private SelectionRequest CreateTargetRequest(GameState state, PlayerId player)
    {
        var candidates = state.GetPlayer(_targetController)
            .BattleAreaPermanents
            .Select(permanent => new SelectableTarget(
                SelectionTargetKind.Permanent,
                $"permanent:{permanent.Id.Value}",
                permanent.ControllerPlayerId,
                Permanent: permanent.Id,
                Zone: Zone.BattleArea))
            .ToArray();

        return new SelectionRequest(
            $"test-once-selection:{Porting.CardId}",
            player,
            SelectionKind.SelectPermanent,
            SelectionTargetKind.Permanent,
            minCount: 1,
            maxCount: 1,
            canSkip: false,
            canEndNotMax: false,
            candidates,
            "Select once-per-turn target.");
    }

    private static void ApplySelection(SelectionResultApplicationContext context)
    {
        foreach (var permanent in context.SelectedPermanentIds)
        {
            context.Primitives.DestroyPermanent(context.State, permanent, context.Trace);
        }
    }
}

internal sealed class OptionSourceZoneProbeScript : ICardScript
{
    public OptionSourceZoneProbeScript(string cardId, string effectClassName)
    {
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that requires hand-played OptionSkill source context to be Executing.");
    }

    public CardEffectPortingRecord Porting { get; }
    public Zone? ObservedPayloadSourceZone { get; private set; }
    public Zone? ObservedCardZone { get; private set; }
    public int ResolveCount { get; private set; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:option:probe-executing-source",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        var sourceCard = context.Resolution.SourceCard
            ?? throw new DomainException($"{Porting.CardId} probe requires a source card.");
        context.WithState((state, _) =>
        {
            ObservedPayloadSourceZone = context.Resolution.Context.GetValueOrDefault("SourceZone") as Zone?;
            ObservedCardZone = state.Cards[sourceCard].CurrentZone;
            ResolveCount++;

            if (ObservedPayloadSourceZone != Zone.Executing)
            {
                throw new DomainException(
                    $"{Porting.CardId} expected SourceZone payload Executing, actual '{ObservedPayloadSourceZone}'.");
            }

            if (ObservedCardZone != Zone.Executing)
            {
                throw new DomainException(
                    $"{Porting.CardId} expected source card in Executing during OptionSkill, actual '{ObservedCardZone}'.");
            }
        });
    }
}

internal sealed class MoveSourceOptionScript : ICardScript
{
    private readonly Zone _destinationZone;

    public MoveSourceOptionScript(string cardId, string effectClassName, Zone destinationZone)
    {
        _destinationZone = destinationZone;
        Porting = new CardEffectPortingRecord(
            cardId,
            effectClassName,
            CardEffectPortingStatus.Implemented,
            "Test fixture script that moves its own option source away from Executing during resolution.");
    }

    public CardEffectPortingRecord Porting { get; }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context) =>
        new[]
        {
            new EffectDescriptor(
                $"{Porting.CardId}:option:move-source",
                EffectTiming.OptionSkill,
                SourceCard: context.SourceCard,
                SourcePermanent: context.SourcePermanent,
                Controller: context.Controller),
        };

    public void Resolve(CardScriptExecutionContext context)
    {
        var sourceCard = context.Resolution.SourceCard
            ?? throw new DomainException($"{Porting.CardId} source move requires a source card.");
        context.WithState((state, primitives) =>
            primitives.MoveCard(
                state,
                new MoveCardCommand(sourceCard, Zone.Executing, _destinationZone, MoveReason.Effect, FaceUp: true)));
    }
}
