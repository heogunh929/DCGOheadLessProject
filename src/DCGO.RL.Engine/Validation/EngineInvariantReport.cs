using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed class EngineInvariantReport
{
    public EngineInvariantReport(IEnumerable<EngineInvariantViolation> violations)
    {
        Violations = violations.ToArray();
    }

    public IReadOnlyList<EngineInvariantViolation> Violations { get; }
    public bool IsValid => Violations.Count == 0;

    public void ThrowIfInvalid()
    {
        if (!IsValid)
        {
            throw new DomainException($"Engine invariant violation: {Violations[0].Code}: {Violations[0].Message}");
        }
    }
}
