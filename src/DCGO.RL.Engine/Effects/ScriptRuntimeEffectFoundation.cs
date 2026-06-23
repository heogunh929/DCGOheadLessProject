using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public abstract class ICardEffect
{
    private Func<EffectContext, bool>? _canUseCondition;
    private Func<EffectContext, string, SelectionRequest>? _createSelectionRequest;
    private SelectionResultContinuation? _selectionContinuation;
    private string _selectionRequestKey = "selection";

    public CardSource? EffectSourceCard { get; private set; }
    public Permanent? EffectSourcePermanent { get; private set; }
    public string EffectName { get; private set; } = string.Empty;
    public string EffectDiscription { get; private set; } = string.Empty;
    public string HashString { get; private set; } = string.Empty;
    public int MaxCountPerTurn { get; private set; } = -1;
    public bool IsOptional { get; private set; }
    public bool IsInheritedEffect { get; private set; }
    public bool IsLinkedEffect { get; private set; }
    public bool IsSecurityEffect { get; private set; }
    public bool IsCounterEffect { get; private set; }
    public bool IsDigimonEffect { get; private set; }
    public bool IsTamerEffect { get; private set; }
    public bool IsOptionEffect { get; private set; }
    public bool IsBackgroundProcess { get; private set; }
    public bool IsSkippable { get; private set; }

    public void SetUpICardEffect(string effectName, Func<EffectContext, bool>? canUseCondition, CardSource card)
    {
        ArgumentNullException.ThrowIfNull(card);

        SetEffectSourceCard(card);
        SetEffectSourcePermanent(null);
        SetMaxCountPerTurn(-1);
        SetEffectName(effectName);
        SetEffectDiscription(string.Empty);
        SetHashString(string.Empty);
        SetCanUseCondition(canUseCondition);
        SetIsOptional(false);
        SetIsInheritedEffect(false);
        SetIsLinkedEffect(false);
        SetIsSecurityEffect(false);
        SetIsCounterEffect(false);
        SetIsDigimonEffect(false);
        SetIsTamerEffect(false);
        SetIsOptionEffect(false);
        SetIsBackgroundProcess(false);
        SetIsSkippable(false);
        ClearSelectionRequest();
    }

    public void SetEffectSourceCard(CardSource? effectSourceCard) => EffectSourceCard = effectSourceCard;
    public void SetEffectSourcePermanent(Permanent? effectSourcePermanent) => EffectSourcePermanent = effectSourcePermanent;
    public void SetMaxCountPerTurn(int maxCountPerTurn) => MaxCountPerTurn = maxCountPerTurn;
    public void SetHashString(string? hashString) => HashString = hashString ?? string.Empty;
    public void SetCanUseCondition(Func<EffectContext, bool>? canUseCondition) => _canUseCondition = canUseCondition;
    public void SetIsOptional(bool isOptional) => IsOptional = isOptional;
    public void SetIsInheritedEffect(bool isInheritedEffect) => IsInheritedEffect = isInheritedEffect;
    public void SetIsLinkedEffect(bool isLinkedEffect) => IsLinkedEffect = isLinkedEffect;
    public void SetIsSecurityEffect(bool isSecurityEffect) => IsSecurityEffect = isSecurityEffect;
    public void SetIsCounterEffect(bool isCounterEffect) => IsCounterEffect = isCounterEffect;
    public void SetIsDigimonEffect(bool isDigimonEffect) => IsDigimonEffect = isDigimonEffect;
    public void SetIsTamerEffect(bool isTamerEffect) => IsTamerEffect = isTamerEffect;
    public void SetIsOptionEffect(bool isOptionEffect) => IsOptionEffect = isOptionEffect;
    public void SetIsBackgroundProcess(bool isBackgroundProcess) => IsBackgroundProcess = isBackgroundProcess;
    public void SetIsSkippable(bool isSkippable) => IsSkippable = isSkippable;
    public void SetSelectionRequest(
        Func<EffectContext, string, SelectionRequest> createSelectionRequest,
        SelectionResultContinuation selectionContinuation,
        string selectionRequestKey = "selection")
    {
        ArgumentNullException.ThrowIfNull(createSelectionRequest);
        ArgumentNullException.ThrowIfNull(selectionContinuation);
        if (string.IsNullOrWhiteSpace(selectionRequestKey))
        {
            throw new DomainException("Selection request key must be a stable non-empty value.");
        }

        _createSelectionRequest = createSelectionRequest;
        _selectionContinuation = selectionContinuation;
        _selectionRequestKey = selectionRequestKey;
    }

    public void ClearSelectionRequest()
    {
        _createSelectionRequest = null;
        _selectionContinuation = null;
        _selectionRequestKey = "selection";
    }

    internal void SetEffectName(string? effectName) => EffectName = effectName ?? string.Empty;
    internal void SetEffectDiscription(string? effectDiscription) => EffectDiscription = effectDiscription ?? string.Empty;

    public bool CanUse(EffectContext context) => _canUseCondition?.Invoke(context) ?? true;

    public virtual EffectDescriptor ToEffectDescriptor(
        EffectTiming timing,
        PlayerId? controller = null,
        PermanentId? sourcePermanent = null)
    {
        if (MaxCountPerTurn > 1)
        {
            throw new UnsupportedMechanicException(
                $"MaxCountPerTurn '{MaxCountPerTurn}' for effect '{EffectName}' requires an explicit use-count contract.");
        }

        var sourceCard = EffectSourceCard?.Id;
        var sourcePermanentFacade = EffectSourcePermanent ?? EffectSourceCard?.PermanentOfThisCard();
        var resolvedSourcePermanent = sourcePermanentFacade?.Id ?? sourcePermanent;
        var resolvedController = controller ?? sourcePermanentFacade?.Controller.Id ?? EffectSourceCard?.Owner.Id;
        var stableId = StableId(timing);
        var sourceSnapshot = CreateSourceSnapshot(sourcePermanentFacade, resolvedSourcePermanent, resolvedController);
        var selectionRequestId = _createSelectionRequest is null
            ? null
            : StableSelectionRequestId(timing, _selectionRequestKey);

        return new EffectDescriptor(
            stableId,
            timing,
            SourceCard: sourceCard,
            SourcePermanent: resolvedSourcePermanent,
            Controller: resolvedController,
            IsBackground: IsBackgroundProcess,
            IsOptional: IsOptional,
            CanTrigger: context => CanUse(context),
            CreateSelectionRequest: _createSelectionRequest is null
                ? null
                : context => _createSelectionRequest(context, selectionRequestId!),
            SelectionContinuation: _selectionContinuation,
            IsOncePerTurn: MaxCountPerTurn == 1,
            OncePerTurnKey: MaxCountPerTurn == 1 ? stableId : null,
            SourceSnapshot: sourceSnapshot,
            IsCounterEffect: IsCounterEffect,
            IsSkippable: IsSkippable);
    }

    public string StableIdFor(EffectTiming timing) => StableId(timing);

    public string StableSelectionRequestId(EffectTiming timing, string selectionRequestKey = "selection")
    {
        if (string.IsNullOrWhiteSpace(selectionRequestKey))
        {
            throw new DomainException("Selection request key must be a stable non-empty value.");
        }

        return $"{StableId(timing)}:{selectionRequestKey}";
    }

    private TriggerSourceSnapshot? CreateSourceSnapshot(
        Permanent? sourcePermanent,
        PermanentId? sourcePermanentId,
        PlayerId? controller)
    {
        if (EffectSourceCard is null)
        {
            return null;
        }

        var role = ResolveSourceRole(EffectSourceCard);
        ValidateSourceRole(EffectSourceCard, sourcePermanent, role);

        return new TriggerSourceSnapshot(
            role,
            EffectSourceCard.CurrentZone,
            EffectSourceCard.Id,
            sourcePermanent?.Id ?? sourcePermanentId,
            sourcePermanent?.TopCard.Id,
            EffectSourceCard.Owner.Id,
            controller ?? EffectSourceCard.Owner.Id);
    }

    private TriggerSourceRole ResolveSourceRole(CardSource source)
    {
        if (IsInheritedEffect)
        {
            return TriggerSourceRole.Inherited;
        }

        if (IsLinkedEffect)
        {
            return TriggerSourceRole.Linked;
        }

        if (IsSecurityEffect)
        {
            return TriggerSourceRole.FaceUpSecurity;
        }

        return source.CurrentZone switch
        {
            Zone.BattleArea or Zone.BreedingArea => TriggerSourceRole.FieldTop,
            Zone.EvolutionSources => TriggerSourceRole.Inherited,
            Zone.LinkedCards => TriggerSourceRole.Linked,
            Zone.Hand => TriggerSourceRole.Hand,
            Zone.Trash => TriggerSourceRole.Trash,
            Zone.Executing => TriggerSourceRole.Executing,
            Zone.Security => TriggerSourceRole.FaceUpSecurity,
            _ => TriggerSourceRole.Unknown,
        };
    }

    private void ValidateSourceRole(CardSource source, Permanent? sourcePermanent, TriggerSourceRole role)
    {
        if (role == TriggerSourceRole.Inherited
            && (sourcePermanent is null
                || source.CurrentZone != Zone.EvolutionSources
                || sourcePermanent.DigivolutionCards.All(card => card.Id != source.Id)))
        {
            throw new UnsupportedMechanicException(
                $"Inherited effect '{EffectName}' source card '{source.Id}' is not an evolution source of its permanent.");
        }

        if (role == TriggerSourceRole.Linked
            && (sourcePermanent is null
                || source.CurrentZone != Zone.LinkedCards
                || sourcePermanent.LinkedCards.All(card => card.Id != source.Id)))
        {
            throw new UnsupportedMechanicException(
                $"Linked effect '{EffectName}' source card '{source.Id}' is not a linked card of its permanent.");
        }

        if (role == TriggerSourceRole.FaceUpSecurity
            && (source.CurrentZone != Zone.Security
                || !source.IsFaceUp
                || !IsInOwnerZone(source, Zone.Security)))
        {
            throw new UnsupportedMechanicException(
                $"Security effect '{EffectName}' source card '{source.Id}' is not a face-up security card.");
        }

        if (role == TriggerSourceRole.Hand
            && (source.CurrentZone != Zone.Hand || !IsInOwnerZone(source, Zone.Hand)))
        {
            throw new UnsupportedMechanicException(
                $"Hand effect '{EffectName}' source card '{source.Id}' is not in its owner's hand.");
        }

        if (role == TriggerSourceRole.Trash
            && (source.CurrentZone != Zone.Trash || !IsInOwnerZone(source, Zone.Trash)))
        {
            throw new UnsupportedMechanicException(
                $"Trash effect '{EffectName}' source card '{source.Id}' is not in its owner's trash.");
        }

        if (role == TriggerSourceRole.Executing
            && (source.CurrentZone != Zone.Executing || !IsInOwnerZone(source, Zone.Executing)))
        {
            throw new UnsupportedMechanicException(
                $"Executing effect '{EffectName}' source card '{source.Id}' is not in its owner's executing zone.");
        }

        if (role == TriggerSourceRole.FieldTop
            && sourcePermanent is not null
            && sourcePermanent.TopCard.Id != source.Id)
        {
            throw new UnsupportedMechanicException(
                $"Field-top effect '{EffectName}' source card '{source.Id}' does not match permanent top card '{sourcePermanent.TopCard.Id}'.");
        }
    }

    private static bool IsInOwnerZone(CardSource source, Zone zone) =>
        source.State.GetPlayer(source.Owner.Id).CardsIn(zone).Contains(source.Id);

    private string StableId(EffectTiming timing)
    {
        if (!string.IsNullOrWhiteSpace(HashString))
        {
            return HashString;
        }

        var source = EffectSourceCard?.Id.Value.ToString() ?? "no-source";
        var name = string.IsNullOrWhiteSpace(EffectName) ? GetType().Name : EffectName;
        return $"{source}:{name}:{timing}";
    }
}

public abstract class ActivateICardEffect : ICardEffect
{
    public Permanent? PermanentWhenTriggered { get; set; }
    public CardSource? TopCardWhenTriggered { get; set; }
}

public sealed class DescriptorBackedCardEffect : ICardEffect
{
    private readonly EffectDescriptor _descriptor;

    public DescriptorBackedCardEffect(
        EffectDescriptor descriptor,
        CardSource? effectSourceCard = null,
        Permanent? effectSourcePermanent = null,
        string? effectName = null)
    {
        _descriptor = descriptor ?? throw new ArgumentNullException(nameof(descriptor));
        SetEffectSourceCard(effectSourceCard);
        SetEffectSourcePermanent(effectSourcePermanent);
        SetHashString(descriptor.StableId);
        SetEffectName(effectName ?? descriptor.StableId);
    }

    public override EffectDescriptor ToEffectDescriptor(
        EffectTiming timing,
        PlayerId? controller = null,
        PermanentId? sourcePermanent = null)
    {
        if (timing != _descriptor.Timing)
        {
            throw new DomainException(
                $"Descriptor-backed effect '{_descriptor.StableId}' was requested for timing '{timing}' but is registered for '{_descriptor.Timing}'.");
        }

        return _descriptor;
    }
}

public interface IAddSkillEffect
{
    bool ShouldAddEffect(EffectTiming timing);
    List<ICardEffect> GetCardEffect(CardSource card, List<ICardEffect> GetCardEffects, EffectTiming timing);
}

public interface ICanNotAffectedEffect
{
    bool CanNotAffect(CardSource cardSource, ICardEffect cardEffect);
}

public abstract class CEntity_Effect
{
    public virtual IReadOnlyList<ICardEffect> CardEffects(EffectTiming timing, CardSource cardSource) =>
        Array.Empty<ICardEffect>();

    public IReadOnlyList<ICardEffect> GetCardEffects(EffectTiming timing, CardSource cardSource) =>
        CardEffects(timing, cardSource).Where(cardEffect => cardEffect is not null).ToArray();

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(
        GameState state,
        EffectTiming timing,
        CardInstanceId sourceCard,
        PermanentId? sourcePermanent = null,
        PlayerId? controller = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var cardSource = new GameContext(state).CardSourceFromId(sourceCard);
        return GetCardEffects(timing, cardSource)
            .Select(cardEffect =>
            {
                if (cardEffect.EffectSourceCard is null)
                {
                    cardEffect.SetEffectSourceCard(cardSource);
                }

                return cardEffect.ToEffectDescriptor(timing, controller, sourcePermanent);
            })
            .ToArray();
    }

    public static bool isExistOnField(CardSource card) => card.PermanentOfThisCard() is not null;
}

public sealed class CEntity_EffectController
{
    private readonly ICEntityEffectRegistry _effectRegistry;

    public CEntity_EffectController(
        CEntity_Effect? cEntityEffect = null,
        IReadOnlyDictionary<string, Func<CEntity_Effect>>? effectFactories = null,
        ICEntityEffectRegistry? effectRegistry = null)
    {
        if (effectFactories is not null && effectRegistry is not null)
        {
            throw new DomainException("CEntity_EffectController requires either effectFactories or effectRegistry, not both.");
        }

        cEntity_Effect = cEntityEffect ?? new EmptyEffectClass();
        _effectRegistry = effectRegistry
            ?? (effectFactories is null
                ? CEntityEffectRegistry.Empty
                : new CEntityEffectRegistry(effectFactories));
    }

    public CEntity_Effect cEntity_Effect { get; private set; }

    public void AddCardEffect(CEntity_Effect cEntityEffect)
    {
        ArgumentNullException.ThrowIfNull(cEntityEffect);
        cEntity_Effect = cEntityEffect;
    }

    public void AddCardEffect(string ID, string ClassName)
    {
        if (string.IsNullOrWhiteSpace(ClassName))
        {
            cEntity_Effect = new EmptyEffectClass();
            return;
        }

        if (_effectRegistry.TryCreate(ID, ClassName, out var effect))
        {
            cEntity_Effect = effect;
            return;
        }

        throw new UnsupportedMechanicException(
            $"Card effect class '{ClassName}' for card '{ID}' is not registered in the headless CEntity_Effect registry.");
    }

    public static IReadOnlyList<string> EffectLookupKeys(string ID, string ClassName)
    {
        if (string.IsNullOrWhiteSpace(ClassName))
        {
            return Array.Empty<string>();
        }

        var className = ClassName.Trim();
        var idPrefix = (ID ?? string.Empty)
            .Split('-', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault() ?? string.Empty;
        var lookupKeys = new List<string> { className };

        if (className.Contains("token", StringComparison.OrdinalIgnoreCase))
        {
            lookupKeys.Add($"DCGO.CardEffects.Tokens.{className}");
        }
        else if (!string.IsNullOrWhiteSpace(idPrefix))
        {
            lookupKeys.Add($"DCGO.CardEffects.{idPrefix}.{className}");
        }

        return lookupKeys
            .Distinct(StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<ICardEffect> GetCardEffects_ExceptAddedEffects(EffectTiming timing, CardSource card) =>
        cEntity_Effect.GetCardEffects(timing, card);

    public IReadOnlyList<ICardEffect> GetCardEffects(EffectTiming timing, CardSource card)
    {
        ArgumentNullException.ThrowIfNull(card);

        var cardEffects = GetCardEffects_ExceptAddedEffects(timing, card)
            .Where(cardEffect => cardEffect is not null)
            .ToList();

        if (timing == EffectTiming.None || IsDigivolutionSource(card))
        {
            return cardEffects;
        }

        foreach (var provider in EnumerateAddedEffectProviderSources(card.State))
        {
            cardEffects = AddSkillEffectsFromProvider(timing, card, cardEffects, provider.Source, provider.Permanent);
        }

        return cardEffects
            .Where(cardEffect => cardEffect is not null)
            .ToArray();
    }

    public IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(
        GameState state,
        EffectTiming timing,
        CardInstanceId sourceCard,
        PermanentId? sourcePermanent = null,
        PlayerId? controller = null)
    {
        ArgumentNullException.ThrowIfNull(state);

        var cardSource = new GameContext(state).CardSourceFromId(sourceCard);
        return GetCardEffects(timing, cardSource)
            .Select(cardEffect =>
            {
                if (cardEffect.EffectSourceCard is null)
                {
                    cardEffect.SetEffectSourceCard(cardSource);
                }

                return cardEffect.ToEffectDescriptor(timing, controller, sourcePermanent);
            })
            .ToArray();
    }

    private List<ICardEffect> AddSkillEffectsFromProvider(
        EffectTiming timing,
        CardSource targetCard,
        List<ICardEffect> cardEffects,
        CardSource providerSource,
        Permanent? providerPermanent)
    {
        var providerEntity = CreateEntityForSource(providerSource);
        if (providerEntity is null)
        {
            return cardEffects;
        }

        var effectHostCard = providerPermanent?.TopCard ?? providerSource;
        foreach (var cardEffect in providerEntity.GetCardEffects(EffectTiming.None, effectHostCard))
        {
            if (cardEffect is not IAddSkillEffect addSkillEffect)
            {
                continue;
            }

            if (ShouldSkipNoneTimingProviderEffect(cardEffect, providerSource, providerPermanent)
                || !addSkillEffect.ShouldAddEffect(timing))
            {
                continue;
            }

            var controller = providerPermanent?.Controller.Id ?? providerSource.Owner.Id;
            var context = new EffectContext(
                targetCard.State,
                EffectTiming.None,
                controller,
                providerSource.Id,
                providerPermanent?.Id);

            if (!cardEffect.CanUse(context))
            {
                continue;
            }

            if (targetCard.CanNotBeAffected(cardEffect, EnumerateCanNotAffectedProviderEffects(targetCard.State)))
            {
                continue;
            }

            cardEffects = addSkillEffect.GetCardEffect(targetCard, cardEffects, timing)
                ?? throw new DomainException(
                    $"Added skill effect '{cardEffect.EffectName}' returned null for target card '{targetCard.Id}'.");
        }

        return cardEffects;
    }

    private IEnumerable<ICardEffect> EnumerateCanNotAffectedProviderEffects(GameState state)
    {
        foreach (var provider in EnumerateAddedEffectProviderSources(state))
        {
            var providerEntity = CreateEntityForSource(provider.Source);
            if (providerEntity is null)
            {
                continue;
            }

            var effectHostCard = provider.Permanent?.TopCard ?? provider.Source;
            foreach (var cardEffect in providerEntity.GetCardEffects(EffectTiming.None, effectHostCard))
            {
                if (cardEffect is not ICanNotAffectedEffect
                    || ShouldSkipNoneTimingProviderEffect(cardEffect, provider.Source, provider.Permanent))
                {
                    continue;
                }

                var controller = provider.Permanent?.Controller.Id ?? provider.Source.Owner.Id;
                var context = new EffectContext(
                    state,
                    EffectTiming.None,
                    controller,
                    provider.Source.Id,
                    provider.Permanent?.Id);

                if (cardEffect.CanUse(context))
                {
                    yield return cardEffect;
                }
            }
        }
    }

    private CEntity_Effect? CreateEntityForSource(CardSource source)
    {
        var className = source.Entity.CardEffectClassName;
        if (string.IsNullOrWhiteSpace(className))
        {
            return null;
        }

        if (_effectRegistry.TryCreate(source.Entity.CardID, className, out var effect))
        {
            return effect;
        }

        throw new UnsupportedMechanicException(
            $"Added-effect provider class '{className}' for card '{source.Entity.CardID}' is not registered in the headless CEntity_Effect registry.");
    }

    private static IEnumerable<(CardSource Source, Permanent? Permanent)> EnumerateAddedEffectProviderSources(GameState state)
    {
        var context = new GameContext(state);
        foreach (var player in context.Players)
        {
            foreach (var permanent in player.GetFieldPermanents())
            {
                yield return (permanent.TopCard, permanent);

                if (!permanent.IsDigimon)
                {
                    continue;
                }

                foreach (var sourceCard in permanent.DigivolutionCards)
                {
                    yield return (sourceCard, permanent);
                }
            }

            foreach (var securityCard in player.SecurityCards)
            {
                if (securityCard.IsFaceUp)
                {
                    yield return (securityCard, null);
                }
            }
        }
    }

    private static bool IsDigivolutionSource(CardSource card) =>
        card.PermanentOfThisCard() is { } permanent
        && permanent.DigivolutionCards.Any(sourceCard => sourceCard.Id == card.Id);

    private static bool ShouldSkipNoneTimingProviderEffect(
        ICardEffect cardEffect,
        CardSource providerSource,
        Permanent? providerPermanent)
    {
        if (!providerSource.IsFaceUp)
        {
            return true;
        }

        if (providerPermanent is null)
        {
            return false;
        }

        var isTopCard = providerPermanent.TopCard.Id == providerSource.Id;
        return cardEffect.IsInheritedEffect == isTopCard;
    }
}

public sealed class EmptyEffectClass : CEntity_Effect;
