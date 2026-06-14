using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed record SelectionResult(
    string RequestId,
    IReadOnlyList<SelectableTarget> SelectedTargets,
    int? SelectedCount = null,
    bool? SelectedBoolean = null,
    string? SelectedOption = null)
{
    public static SelectionResult ForTargets(string requestId, IReadOnlyList<SelectableTarget> targets) =>
        new(requestId, targets);

    public static SelectionResult Skip(string requestId) =>
        new(requestId, Array.Empty<SelectableTarget>());

    public static SelectionResult ForCount(string requestId, int count) =>
        new(requestId, Array.Empty<SelectableTarget>(), SelectedCount: count);

    public static SelectionResult ForBoolean(string requestId, bool value) =>
        new(requestId, Array.Empty<SelectableTarget>(), SelectedBoolean: value);

    public static SelectionResult ForOption(string requestId, string option) =>
        new(requestId, Array.Empty<SelectableTarget>(), SelectedOption: option);
}
