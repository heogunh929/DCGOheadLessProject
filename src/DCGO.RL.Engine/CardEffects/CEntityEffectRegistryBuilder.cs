using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public static class CEntityEffectRegistryBuilder
{
    public static CEntityEffectRegistry CreateRegistry(
        ICardScriptRegistry cardScriptRegistry,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> factoriesBySourceEffectClass)
    {
        ArgumentNullException.ThrowIfNull(cardScriptRegistry);

        return CreateRegistry(cardScriptRegistry.PortingRecords, factoriesBySourceEffectClass);
    }

    public static CEntityEffectRegistry CreateRegistry(
        IEnumerable<CardEffectPortingRecord> portingRecords,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> factoriesBySourceEffectClass) =>
        CEntityEffectRegistry.FromEntries(CreateEntries(portingRecords, factoriesBySourceEffectClass));

    public static IReadOnlyList<CEntityEffectRegistryEntry> CreateEntries(
        IEnumerable<CardEffectPortingRecord> portingRecords,
        IReadOnlyDictionary<string, Func<CEntity_Effect>> factoriesBySourceEffectClass)
    {
        ArgumentNullException.ThrowIfNull(portingRecords);
        ArgumentNullException.ThrowIfNull(factoriesBySourceEffectClass);

        var candidates = new List<(string EffectClassName, CardEffectPortingRecord Record)>();
        foreach (var record in portingRecords)
        {
            if (record is null)
            {
                throw new DomainException("CEntity effect registry porting records must be non-null.");
            }

            var effectClassName = record.EffectiveSourceEffectClassName.Trim();
            if (string.IsNullOrWhiteSpace(effectClassName))
            {
                continue;
            }

            if (string.IsNullOrWhiteSpace(record.CardId))
            {
                throw new DomainException(
                    $"CEntity effect registry porting record for source effect class '{effectClassName}' must have a non-empty CardId.");
            }

            candidates.Add((effectClassName, record));
        }

        var entries = new List<CEntityEffectRegistryEntry>();
        var seenEffectClassNames = new HashSet<string>(StringComparer.Ordinal);
        foreach (var candidate in candidates
            .OrderBy(candidate => candidate.EffectClassName, StringComparer.Ordinal)
            .ThenBy(candidate => candidate.Record.CardId, StringComparer.Ordinal)
            .ThenBy(candidate => candidate.Record.CardIndex)
            .ThenBy(candidate => candidate.Record.NormalizedVariantKey, StringComparer.Ordinal))
        {
            if (!seenEffectClassNames.Add(candidate.EffectClassName))
            {
                continue;
            }

            if (!factoriesBySourceEffectClass.TryGetValue(candidate.EffectClassName, out var factory))
            {
                throw new DomainException(
                    $"CEntity effect factory for source effect class '{candidate.EffectClassName}' is not registered.");
            }

            if (factory is null)
            {
                throw new DomainException(
                    $"CEntity effect factory for source effect class '{candidate.EffectClassName}' is null.");
            }

            entries.Add(new CEntityEffectRegistryEntry(
                candidate.Record.CardId,
                candidate.EffectClassName,
                factory));
        }

        return entries;
    }
}
