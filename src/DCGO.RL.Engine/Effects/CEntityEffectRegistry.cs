using System.Diagnostics.CodeAnalysis;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public interface ICEntityEffectRegistry
{
    IReadOnlyCollection<string> RegisteredKeys { get; }
    bool TryCreate(string cardId, string className, [NotNullWhen(true)] out CEntity_Effect? effect);
}

public sealed record CEntityEffectRegistryEntry(
    string CardId,
    string EffectClassName,
    Func<CEntity_Effect> Factory)
{
    public IReadOnlyList<string> LookupKeys =>
        CEntity_EffectController.EffectLookupKeys(CardId, EffectClassName);
}

public sealed class CEntityEffectRegistry : ICEntityEffectRegistry
{
    private readonly IReadOnlyDictionary<string, Func<CEntity_Effect>> _factories;
    private readonly IReadOnlyCollection<string> _registeredKeys;

    public static CEntityEffectRegistry Empty { get; } = new();

    public CEntityEffectRegistry()
    {
        _factories = new Dictionary<string, Func<CEntity_Effect>>(StringComparer.Ordinal);
        _registeredKeys = Array.Empty<string>();
    }

    public CEntityEffectRegistry(IReadOnlyDictionary<string, Func<CEntity_Effect>> factories)
        : this((IEnumerable<KeyValuePair<string, Func<CEntity_Effect>>>)factories)
    {
    }

    public CEntityEffectRegistry(IEnumerable<KeyValuePair<string, Func<CEntity_Effect>>> factories)
    {
        ArgumentNullException.ThrowIfNull(factories);

        var map = new Dictionary<string, Func<CEntity_Effect>>(StringComparer.Ordinal);
        foreach (var factory in factories)
        {
            if (string.IsNullOrWhiteSpace(factory.Key))
            {
                throw new DomainException("CEntity effect registry keys must be non-empty.");
            }

            if (factory.Value is null)
            {
                throw new DomainException($"CEntity effect registry key '{factory.Key}' has no factory.");
            }

            if (map.ContainsKey(factory.Key))
            {
                throw new DomainException($"CEntity effect registry key '{factory.Key}' is already registered.");
            }

            map.Add(factory.Key, factory.Value);
        }

        _factories = map;
        _registeredKeys = map.Keys.OrderBy(key => key, StringComparer.Ordinal).ToArray();
    }

    public static CEntityEffectRegistry FromEntries(IEnumerable<CEntityEffectRegistryEntry> entries)
    {
        ArgumentNullException.ThrowIfNull(entries);

        var factories = new List<KeyValuePair<string, Func<CEntity_Effect>>>();
        foreach (var entry in entries)
        {
            if (entry is null)
            {
                throw new DomainException("CEntity effect registry entries must be non-null.");
            }

            if (string.IsNullOrWhiteSpace(entry.CardId))
            {
                throw new DomainException("CEntity effect registry entry CardId must be non-empty.");
            }

            if (string.IsNullOrWhiteSpace(entry.EffectClassName))
            {
                throw new DomainException($"CEntity effect registry entry for card '{entry.CardId}' must have a non-empty effect class name.");
            }

            if (entry.Factory is null)
            {
                throw new DomainException(
                    $"CEntity effect registry entry '{entry.CardId}:{entry.EffectClassName}' has no factory.");
            }

            foreach (var lookupKey in entry.LookupKeys)
            {
                factories.Add(new KeyValuePair<string, Func<CEntity_Effect>>(lookupKey, entry.Factory));
            }
        }

        return new CEntityEffectRegistry(factories);
    }

    public IReadOnlyCollection<string> RegisteredKeys => _registeredKeys;

    public bool TryCreate(
        string cardId,
        string className,
        [NotNullWhen(true)] out CEntity_Effect? effect)
    {
        effect = null;
        if (string.IsNullOrWhiteSpace(className))
        {
            return false;
        }

        foreach (var lookupKey in CEntity_EffectController.EffectLookupKeys(cardId, className))
        {
            if (!_factories.TryGetValue(lookupKey, out var factory))
            {
                continue;
            }

            effect = factory()
                ?? throw new DomainException($"Card effect factory for '{lookupKey}' returned null.");
            return true;
        }

        return false;
    }
}
