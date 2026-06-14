namespace DCGO.RL.Engine.Validation;

public sealed record DeckValidationReport(
    bool IsValid,
    IReadOnlyList<string> Errors,
    IReadOnlyList<string> Warnings)
{
    public static DeckValidationReport Valid { get; } =
        new(true, Array.Empty<string>(), Array.Empty<string>());

    public static DeckValidationReport Invalid(IEnumerable<string> errors, IEnumerable<string>? warnings = null) =>
        new(false, errors.ToArray(), warnings?.ToArray() ?? Array.Empty<string>());
}
