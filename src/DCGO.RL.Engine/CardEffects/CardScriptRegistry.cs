using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.CardEffects;

public sealed class CardScriptRegistry : ICardScriptRegistry
{
    private readonly Dictionary<string, ICardScript> _byCardId = new(StringComparer.Ordinal);
    private readonly Dictionary<string, ICardScript> _byEffectClassName = new(StringComparer.Ordinal);

    public CardScriptRegistry(IEnumerable<ICardScript>? scripts = null)
    {
        if (scripts is null)
        {
            return;
        }

        foreach (var script in scripts)
        {
            Register(script);
        }
    }

    public IReadOnlyCollection<CardEffectPortingRecord> PortingRecords =>
        _byCardId.Values
            .Concat(_byEffectClassName.Values)
            .Distinct()
            .Select(script => script.Porting)
            .OrderBy(record => record.CardId, StringComparer.Ordinal)
            .ThenBy(record => record.EffectClassName, StringComparer.Ordinal)
            .ToArray();

    public void Register(ICardScript script)
    {
        ArgumentNullException.ThrowIfNull(script);

        var record = script.Porting;
        if (!string.IsNullOrWhiteSpace(record.CardId))
        {
            if (!_byCardId.TryAdd(record.CardId, script))
            {
                throw new DomainException($"Card script for card '{record.CardId}' is already registered.");
            }
        }

        if (!string.IsNullOrWhiteSpace(record.EffectClassName))
        {
            if (!_byEffectClassName.TryAdd(record.EffectClassName, script))
            {
                throw new DomainException($"Card script for effect class '{record.EffectClassName}' is already registered.");
            }
        }
    }

    public bool TryGetScript(CardDefinition definition, out ICardScript script)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (_byCardId.TryGetValue(definition.CardId, out script!))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(definition.CardEffectClassName)
            && _byEffectClassName.TryGetValue(definition.CardEffectClassName, out script!))
        {
            return true;
        }

        script = null!;
        return false;
    }

    public ICardScript GetScript(CardDefinition definition)
    {
        if (TryGetScript(definition, out var script))
        {
            return script;
        }

        var reason = string.IsNullOrWhiteSpace(definition.CardEffectClassName)
            ? "No-effect cards must be explicitly registered."
            : "Card effect class is not registered.";

        return new UnsupportedCardScript(definition.CardId, definition.CardEffectClassName, reason);
    }
}
