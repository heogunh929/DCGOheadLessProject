using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public sealed class CEntityEffectFactoryCatalog
{
    private readonly IReadOnlyDictionary<string, Func<CEntity_Effect>> _factoriesBySourceEffectClass;
    private readonly IReadOnlyCollection<string> _registeredSourceEffectClassNames;

    public static CEntityEffectFactoryCatalog Empty { get; } = new();

    public CEntityEffectFactoryCatalog()
    {
        _factoriesBySourceEffectClass = new Dictionary<string, Func<CEntity_Effect>>(StringComparer.Ordinal);
        _registeredSourceEffectClassNames = Array.Empty<string>();
    }

    public CEntityEffectFactoryCatalog(IReadOnlyDictionary<string, Func<CEntity_Effect>> factoriesBySourceEffectClass)
        : this((IEnumerable<KeyValuePair<string, Func<CEntity_Effect>>>)factoriesBySourceEffectClass)
    {
    }

    public CEntityEffectFactoryCatalog(IEnumerable<KeyValuePair<string, Func<CEntity_Effect>>> factoriesBySourceEffectClass)
    {
        ArgumentNullException.ThrowIfNull(factoriesBySourceEffectClass);

        var map = new Dictionary<string, Func<CEntity_Effect>>(StringComparer.Ordinal);
        foreach (var factory in factoriesBySourceEffectClass)
        {
            if (string.IsNullOrWhiteSpace(factory.Key))
            {
                throw new DomainException("CEntity effect factory catalog keys must be non-empty source effect class names.");
            }

            if (factory.Value is null)
            {
                throw new DomainException($"CEntity effect factory catalog key '{factory.Key}' has no factory.");
            }

            if (map.ContainsKey(factory.Key))
            {
                throw new DomainException($"CEntity effect factory catalog key '{factory.Key}' is already registered.");
            }

            map.Add(factory.Key, factory.Value);
        }

        _factoriesBySourceEffectClass = map;
        _registeredSourceEffectClassNames = map.Keys.OrderBy(key => key, StringComparer.Ordinal).ToArray();
    }

    public IReadOnlyCollection<string> RegisteredSourceEffectClassNames => _registeredSourceEffectClassNames;

    public bool TryGetFactory(string sourceEffectClassName, out Func<CEntity_Effect>? factory) =>
        _factoriesBySourceEffectClass.TryGetValue(sourceEffectClassName, out factory);

    public static CEntityEffectFactoryCatalog FromScripts(IEnumerable<ICardScript> scripts)
    {
        ArgumentNullException.ThrowIfNull(scripts);

        var candidates = new List<(string EffectClassName, CardEffectPortingRecord Record, ICEntityEffectFactoryProvider? Provider)>();
        foreach (var script in scripts)
        {
            if (script is null)
            {
                throw new DomainException("CEntity effect factory catalog scripts must be non-null.");
            }

            var record = script.Porting;
            var effectClassName = record.EffectiveSourceEffectClassName.Trim();
            if (string.IsNullOrWhiteSpace(effectClassName))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(record.CardId))
            {
                throw new DomainException(
                    $"CEntity effect factory provider for source effect class '{effectClassName}' must have a non-empty CardId.");
            }

            candidates.Add((effectClassName, record, script as ICEntityEffectFactoryProvider));
        }

        var factories = new List<KeyValuePair<string, Func<CEntity_Effect>>>();
        foreach (var group in candidates
            .GroupBy(candidate => candidate.EffectClassName, StringComparer.Ordinal)
            .OrderBy(group => group.Key, StringComparer.Ordinal))
        {
            var providers = group
                .Where(candidate => candidate.Provider is not null)
                .OrderBy(candidate => candidate.Record.CardId, StringComparer.Ordinal)
                .ThenBy(candidate => candidate.Record.CardIndex)
                .ThenBy(candidate => candidate.Record.NormalizedVariantKey, StringComparer.Ordinal)
                .ToArray();

            if (providers.Length == 0)
            {
                throw new DomainException(
                    $"CEntity effect factory provider for source effect class '{group.Key}' is not registered.");
            }

            if (providers.Length > 1)
            {
                throw new DomainException(
                    $"CEntity effect factory provider for source effect class '{group.Key}' is registered more than once.");
            }

            var factory = providers[0].Provider!.CreateCEntityEffectFactory();
            if (factory is null)
            {
                throw new DomainException(
                    $"CEntity effect factory provider for source effect class '{group.Key}' returned no factory.");
            }

            factories.Add(new KeyValuePair<string, Func<CEntity_Effect>>(group.Key, factory));
        }

        return new CEntityEffectFactoryCatalog(factories);
    }

    public IReadOnlyList<CEntityEffectRegistryEntry> CreateEntries(ICardScriptRegistry cardScriptRegistry)
    {
        ArgumentNullException.ThrowIfNull(cardScriptRegistry);
        return CreateEntries(cardScriptRegistry.PortingRecords);
    }

    public IReadOnlyList<CEntityEffectRegistryEntry> CreateEntries(IEnumerable<CardEffectPortingRecord> portingRecords) =>
        CEntityEffectRegistryBuilder.CreateEntries(portingRecords, _factoriesBySourceEffectClass);

    public CEntityEffectRegistry CreateRegistry(ICardScriptRegistry cardScriptRegistry)
    {
        ArgumentNullException.ThrowIfNull(cardScriptRegistry);
        return CreateRegistry(cardScriptRegistry.PortingRecords);
    }

    public CEntityEffectRegistry CreateRegistry(IEnumerable<CardEffectPortingRecord> portingRecords) =>
        CEntityEffectRegistryBuilder.CreateRegistry(portingRecords, _factoriesBySourceEffectClass);
}
