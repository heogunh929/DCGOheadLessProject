using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed record SelectableTarget(
    SelectionTargetKind Kind,
    string StableId,
    PlayerId? Owner = null,
    CardInstanceId? Card = null,
    PermanentId? Permanent = null,
    string Label = "",
    int? FieldSlotIndex = null,
    string? OptionValue = null,
    Zone? Zone = null)
{
    public string DebugLabel => string.IsNullOrWhiteSpace(Label) ? StableId : Label;
}
