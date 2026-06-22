using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public sealed record TemporaryGrantedEffectDescriptorContext(
    GameState State,
    TemporaryGrantedEffect GrantedEffect,
    CardInstanceId? SourceCard,
    PermanentId? SourcePermanent,
    PlayerId Controller,
    TriggerSourceSnapshot? SourceSnapshot)
{
    public bool AppliesToPermanent(PermanentState permanent)
    {
        ArgumentNullException.ThrowIfNull(permanent);

        if (GrantedEffect.TargetPermanentId is { } targetPermanent
            && permanent.Id != targetPermanent)
        {
            return false;
        }

        if (GrantedEffect.TargetPlayerId is { } targetPlayer
            && permanent.ControllerPlayerId != targetPlayer)
        {
            return false;
        }

        if (GrantedEffect.TargetMetadataCriteria is null)
        {
            return true;
        }

        return GrantedEffect.TargetMetadataCriteria.Matches(
            Battle.BattleRules.Definition(State, permanent.TopCardId));
    }
}

public sealed record TemporaryGrantedEffectHandler(
    string Key,
    Func<TemporaryGrantedEffectDescriptorContext, EffectDescriptor> CreateDescriptor,
    Action<CardScriptExecutionContext, TemporaryGrantedEffect> Resolve);

public sealed class TemporaryGrantedEffectRegistry
{
    public static TemporaryGrantedEffectRegistry Empty { get; } = new();

    private readonly Dictionary<string, TemporaryGrantedEffectHandler> _handlers;

    public TemporaryGrantedEffectRegistry(params TemporaryGrantedEffectHandler[] handlers)
        : this((IEnumerable<TemporaryGrantedEffectHandler>)handlers)
    {
    }

    public TemporaryGrantedEffectRegistry(IEnumerable<TemporaryGrantedEffectHandler>? handlers = null)
    {
        _handlers = new Dictionary<string, TemporaryGrantedEffectHandler>(StringComparer.Ordinal);
        if (handlers is null)
        {
            return;
        }

        foreach (var handler in handlers)
        {
            if (string.IsNullOrWhiteSpace(handler.Key))
            {
                throw new DomainException("Temporary granted effect handler key must not be empty.");
            }

            if (!_handlers.TryAdd(handler.Key, handler))
            {
                throw new DomainException($"Duplicate temporary granted effect handler key '{handler.Key}'.");
            }
        }
    }

    public bool TryCreateDescriptor(
        TemporaryGrantedEffect grantedEffect,
        TemporaryGrantedEffectDescriptorContext context,
        out EffectDescriptor descriptor)
    {
        if (_handlers.TryGetValue(grantedEffect.GrantedEffectKey, out var handler))
        {
            descriptor = handler.CreateDescriptor(context);
            return true;
        }

        descriptor = null!;
        return false;
    }

    public void Resolve(CardScriptExecutionContext context, TemporaryGrantedEffect grantedEffect)
    {
        if (!_handlers.TryGetValue(grantedEffect.GrantedEffectKey, out var handler))
        {
            throw Unsupported(grantedEffect);
        }

        handler.Resolve(context, grantedEffect);
    }

    public static UnsupportedMechanicException Unsupported(TemporaryGrantedEffect grantedEffect) =>
        new(
            $"Temporary granted effect '{grantedEffect.StableId}' key '{grantedEffect.GrantedEffectKey}' is not supported.");
}
