namespace DCGO.RL.Engine.Domain;

public readonly record struct CardDefinitionIdentity(string CardId, int CardIndex, string VariantKey)
{
    public string StableId => StableIdOrCardId(CardId, CardIndex, VariantKey);

    public bool HasIndexedIdentity => CardIndex > 0;

    public static string NormalizeVariantKey(string? variantKey) =>
        string.IsNullOrWhiteSpace(variantKey) ? "base" : variantKey.Trim();

    public static string StableIdForIndexed(string cardId, int cardIndex, string? variantKey) =>
        $"{cardId}#{cardIndex}@{NormalizeVariantKey(variantKey)}";

    public static string StableIdOrCardId(string cardId, int cardIndex, string? variantKey) =>
        cardIndex > 0 ? StableIdForIndexed(cardId, cardIndex, variantKey) : cardId;

    public static CardDefinitionIdentity From(CardDefinition definition) =>
        new(definition.CardId, definition.CardIndex, definition.VariantKey);
}
