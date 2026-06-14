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

    public SelectionPrimitiveCardScript(
        string cardId,
        string effectClassName,
        SelectionPrimitiveMode mode,
        PlayerId targetController,
        int minCount = 1,
        int maxCount = 1,
        bool canSkip = false,
        bool canEndNotMax = false)
    {
        _mode = mode;
        _targetController = targetController;
        _minCount = minCount;
        _maxCount = maxCount;
        _canSkip = canSkip;
        _canEndNotMax = canEndNotMax;
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
                EffectTiming.OptionSkill,
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
