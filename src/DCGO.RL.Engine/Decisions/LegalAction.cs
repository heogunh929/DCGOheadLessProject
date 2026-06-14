using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed record LegalAction(
    string Id,
    LegalActionKind Kind,
    GameAction Action,
    string Label,
    IReadOnlyList<SelectableTarget> Targets)
{
    public GameActionMetadata Metadata => Action.Metadata;

    public string DebugLabel => string.IsNullOrWhiteSpace(Label)
        ? $"{Kind}:{Id}"
        : $"{Kind}: {Label}";

    public SelectionRequest ToSelectionRequest(
        string requestId,
        PlayerId player,
        Phase phase,
        SelectionKind selectionKind,
        SelectionTargetKind targetKind,
        int minCount,
        int maxCount,
        bool canSkip = false,
        bool canEndNotMax = false,
        string? prompt = null) =>
        new(
            id: requestId,
            player: player,
            selectionKind: selectionKind,
            targetKind: targetKind,
            minCount: minCount,
            maxCount: maxCount,
            canSkip: canSkip,
            canEndNotMax: canEndNotMax,
            candidates: Targets,
            prompt: prompt ?? $"{Label} during {phase}.");
}
