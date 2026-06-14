using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Validation;

public sealed record UnsupportedMechanicRecord(string Mechanic, string Context);

public sealed class UnsupportedMechanicReporter
{
    private readonly List<UnsupportedMechanicRecord> _records = new();

    public IReadOnlyList<UnsupportedMechanicRecord> Records => _records;
    public bool HasUnsupportedMechanics => _records.Count > 0;

    public void Record(string mechanic, string context) =>
        _records.Add(new UnsupportedMechanicRecord(mechanic, context));

    public void Record(UnsupportedMechanicException exception, string context) =>
        Record(exception.Mechanic, context);

    public void RecordCardMechanic(CardDefinition definition, string mechanic, string context) =>
        Record(mechanic, $"card:{definition.CardId}:effect:{definition.CardEffectClassName}: {context}");

    public IReadOnlyList<string> ToErrorMessages() =>
        _records
            .Select(record => $"Unsupported {record.Mechanic}: {record.Context}")
            .ToArray();

    public DeckValidationReport ToDeckValidationReport(IEnumerable<string>? warnings = null) =>
        HasUnsupportedMechanics
            ? DeckValidationReport.Invalid(ToErrorMessages(), warnings)
            : new DeckValidationReport(true, Array.Empty<string>(), warnings?.ToArray() ?? Array.Empty<string>());

    public void ThrowIfAny(string context)
    {
        if (HasUnsupportedMechanics)
        {
            throw new UnsupportedMechanicException($"{context}: {string.Join(" | ", ToErrorMessages())}");
        }
    }
}
