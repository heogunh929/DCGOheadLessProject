using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record EngineInvariantViolation(
    string Code,
    string Message,
    CardInstanceId? Card = null,
    PermanentId? Permanent = null);
