using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public enum TriggerSourceRole
{
    Unknown,
    FieldTop,
    Inherited,
    Linked,
    Hand,
    Trash,
    Executing,
    FaceUpSecurity,
}

public enum TriggerSourcePersistencePolicy
{
    RequireSameRole,
    AllowTriggeredSourceMove,
}

public sealed record TriggerSourceSnapshot(
    TriggerSourceRole Role,
    Zone SourceZone,
    CardInstanceId SourceCard,
    PermanentId? SourcePermanent,
    CardInstanceId? TopCardWhenTriggered,
    PlayerId Owner,
    PlayerId Controller);
