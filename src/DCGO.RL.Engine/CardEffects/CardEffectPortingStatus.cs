using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.CardEffects;

public enum CardEffectPortingStatus
{
    Unsupported,
    NoEffect,
    StubbedForValidation,
    PartiallyImplemented,
    Implemented,
    Verified,
}

public sealed record CardEffectPortingRecord(
    string CardId,
    string EffectClassName,
    CardEffectPortingStatus Status,
    string Notes = "",
    string SourceEffectClassName = "",
    int CardIndex = 0,
    string VariantKey = "")
{
    public bool HasDefinitionIdentity => CardIndex > 0;

    public string DefinitionStableId =>
        CardDefinitionIdentity.StableIdOrCardId(CardId, CardIndex, VariantKey);

    public string NormalizedVariantKey => CardDefinitionIdentity.NormalizeVariantKey(VariantKey);

    public string EffectiveSourceEffectClassName =>
        string.IsNullOrWhiteSpace(SourceEffectClassName) ? EffectClassName : SourceEffectClassName;

    public bool IsRunnableInValidatedDeck =>
        Status is CardEffectPortingStatus.NoEffect
            or CardEffectPortingStatus.Implemented
            or CardEffectPortingStatus.Verified;
}
