namespace DCGO.RL.Engine.Domain;

public sealed record CardMetadataSnapshot(
    IReadOnlyList<string> CardNames,
    IReadOnlyList<string> Traits,
    string CardTextEnglish,
    string CardTextJapanese);

public sealed record CardMetadataCriteria
{
    private IReadOnlyList<string> _requiredTraits = Array.Empty<string>();
    private IReadOnlyList<string> _anyTraits = Array.Empty<string>();
    private IReadOnlyList<string> _requiredNameSubstrings = Array.Empty<string>();
    private IReadOnlyList<string> _requiredTextSubstrings = Array.Empty<string>();

    public IReadOnlyList<string> RequiredTraits
    {
        get => _requiredTraits;
        init => _requiredTraits = ToReadOnlyStringList(value);
    }

    public IReadOnlyList<string> AnyTraits
    {
        get => _anyTraits;
        init => _anyTraits = ToReadOnlyStringList(value);
    }

    public IReadOnlyList<string> RequiredNameSubstrings
    {
        get => _requiredNameSubstrings;
        init => _requiredNameSubstrings = ToReadOnlyStringList(value);
    }

    public IReadOnlyList<string> RequiredTextSubstrings
    {
        get => _requiredTextSubstrings;
        init => _requiredTextSubstrings = ToReadOnlyStringList(value);
    }

    public bool Matches(CardDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return Matches(new CardMetadataSnapshot(
            CardNames: new[] { definition.CardNameEnglish, definition.CardNameJapanese }
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToArray(),
            Traits: definition.Traits,
            CardTextEnglish: definition.CardTextEnglish,
            CardTextJapanese: definition.CardTextJapanese));
    }

    public bool Matches(CardMetadataSnapshot metadata)
    {
        ArgumentNullException.ThrowIfNull(metadata);

        if (!RequiredTraits.All(trait => HasTrait(metadata, trait)))
        {
            return false;
        }

        if (AnyTraits.Count > 0 && !AnyTraits.Any(trait => HasTrait(metadata, trait)))
        {
            return false;
        }

        if (!RequiredNameSubstrings.All(term => ContainsName(metadata, term)))
        {
            return false;
        }

        return RequiredTextSubstrings.All(term => ContainsText(metadata, term));
    }

    public static bool HasTrait(CardDefinition definition, string trait) =>
        HasTrait(
            new CardMetadataSnapshot(
                CardNames: new[] { definition.CardNameEnglish, definition.CardNameJapanese }
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToArray(),
                Traits: definition.Traits,
                CardTextEnglish: definition.CardTextEnglish,
                CardTextJapanese: definition.CardTextJapanese),
            trait);

    public static bool HasTrait(CardMetadataSnapshot metadata, string trait) =>
        !string.IsNullOrWhiteSpace(trait)
        && metadata.Traits.Any(candidate => string.Equals(candidate, trait.Trim(), StringComparison.OrdinalIgnoreCase));

    public static bool ContainsName(CardDefinition definition, string term) =>
        ContainsName(
            new CardMetadataSnapshot(
                CardNames: new[] { definition.CardNameEnglish, definition.CardNameJapanese }
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToArray(),
                Traits: definition.Traits,
                CardTextEnglish: definition.CardTextEnglish,
                CardTextJapanese: definition.CardTextJapanese),
            term);

    public static bool ContainsName(CardMetadataSnapshot metadata, string term) =>
        metadata.CardNames.Any(name => Contains(name, term));

    public static bool ContainsText(CardDefinition definition, string term) =>
        ContainsText(
            new CardMetadataSnapshot(
                CardNames: new[] { definition.CardNameEnglish, definition.CardNameJapanese }
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .ToArray(),
                Traits: definition.Traits,
                CardTextEnglish: definition.CardTextEnglish,
                CardTextJapanese: definition.CardTextJapanese),
            term);

    public static bool ContainsText(CardMetadataSnapshot metadata, string term) =>
        Contains(metadata.CardTextEnglish, term)
        || Contains(metadata.CardTextJapanese, term);

    public static CardMetadataSnapshot FromDefinition(CardDefinition definition) =>
        new(
            CardNames: new[] { definition.CardNameEnglish, definition.CardNameJapanese }
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .ToArray(),
            Traits: definition.Traits,
            CardTextEnglish: definition.CardTextEnglish,
            CardTextJapanese: definition.CardTextJapanese);

    private static bool Contains(string value, string term) =>
        !string.IsNullOrWhiteSpace(term)
        && value.Contains(term.Trim(), StringComparison.OrdinalIgnoreCase);

    private static IReadOnlyList<string> ToReadOnlyStringList(IEnumerable<string>? values) =>
        values is null
            ? Array.Empty<string>()
            : Array.AsReadOnly(values
                .Where(value => !string.IsNullOrWhiteSpace(value))
                .Select(value => value.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray());
}
