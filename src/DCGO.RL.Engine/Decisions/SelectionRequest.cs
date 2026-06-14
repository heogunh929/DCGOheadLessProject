using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public sealed class SelectionRequest
{
    public SelectionRequest(
        string id,
        PlayerId player,
        SelectionTargetKind targetKind,
        int minCount,
        int maxCount,
        bool canSkip,
        bool canEndNotMax,
        IReadOnlyList<SelectableTarget> candidates,
        string prompt)
        : this(
            id,
            player,
            InferSelectionKind(targetKind),
            targetKind,
            minCount,
            maxCount,
            canSkip,
            canEndNotMax,
            candidates,
            prompt)
    {
    }

    public SelectionRequest(
        string id,
        PlayerId player,
        SelectionKind selectionKind,
        SelectionTargetKind targetKind,
        int minCount,
        int maxCount,
        bool canSkip,
        bool canEndNotMax,
        IReadOnlyList<SelectableTarget> candidates,
        string prompt)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new DomainException("SelectionRequest requires a stable id.");
        }

        if (minCount < 0 || maxCount < minCount)
        {
            throw new DomainException("SelectionRequest count bounds are invalid.");
        }

        Id = id;
        Player = player;
        Kind = selectionKind;
        TargetKind = targetKind;
        MinCount = minCount;
        MaxCount = maxCount;
        CanSkip = canSkip;
        CanEndNotMax = canEndNotMax;
        Candidates = candidates.ToArray();
        Prompt = prompt;
    }

    public string Id { get; }
    public PlayerId Player { get; }
    public SelectionKind Kind { get; }
    public SelectionTargetKind TargetKind { get; }
    public int MinCount { get; }
    public int MaxCount { get; }
    public bool CanSkip { get; }
    public bool CanEndNotMax { get; }
    public IReadOnlyList<SelectableTarget> Candidates { get; }
    public string Prompt { get; }
    public string DebugLabel =>
        $"{Kind} {Id}: {Prompt} [{MinCount}-{MaxCount}] candidates={Candidates.Count}";

    private static SelectionKind InferSelectionKind(SelectionTargetKind targetKind) =>
        targetKind switch
        {
            SelectionTargetKind.Card => SelectionKind.SelectCard,
            SelectionTargetKind.Permanent => SelectionKind.SelectPermanent,
            SelectionTargetKind.Security => SelectionKind.SelectSecurity,
            SelectionTargetKind.FieldSlot => SelectionKind.SelectFieldSlot,
            SelectionTargetKind.Count => SelectionKind.SelectCount,
            SelectionTargetKind.Boolean => SelectionKind.SelectYesNo,
            SelectionTargetKind.Option => SelectionKind.ChooseAction,
            _ => SelectionKind.SelectCard,
        };
}
