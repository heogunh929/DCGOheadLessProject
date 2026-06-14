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
    string Notes = "")
{
    public bool IsRunnableInValidatedDeck =>
        Status is CardEffectPortingStatus.NoEffect
            or CardEffectPortingStatus.Implemented
            or CardEffectPortingStatus.Verified;
}
