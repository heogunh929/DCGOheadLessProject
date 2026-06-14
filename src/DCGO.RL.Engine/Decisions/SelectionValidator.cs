using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Decisions;

public static class SelectionValidator
{
    public static void Validate(SelectionRequest request, SelectionResult result)
    {
        if (!string.Equals(request.Id, result.RequestId, StringComparison.Ordinal))
        {
            throw new DomainException($"SelectionResult request id '{result.RequestId}' does not match '{request.Id}'.");
        }

        switch (request.Kind)
        {
            case SelectionKind.SelectCount:
                ValidateCount(request, result);
                return;

            case SelectionKind.SelectYesNo:
                ValidateBoolean(request, result);
                return;

            default:
                if (request.TargetKind == SelectionTargetKind.Option && result.SelectedOption is not null)
                {
                    ValidateOption(request, result);
                    return;
                }

                ValidateTargets(request, result);
                return;
        }
    }

    public static bool IsValid(SelectionRequest request, SelectionResult result)
    {
        try
        {
            Validate(request, result);
            return true;
        }
        catch (DomainException)
        {
            return false;
        }
    }

    private static void ValidateCount(SelectionRequest request, SelectionResult result)
    {
        if (result.SelectedCount is null)
        {
            if (request.CanSkip && result.SelectedTargets.Count == 0)
            {
                return;
            }

            throw new DomainException("SelectionResult requires a selected count.");
        }

        if (result.SelectedCount.Value < request.MinCount || result.SelectedCount.Value > request.MaxCount)
        {
            throw new DomainException(
                $"Selected count '{result.SelectedCount}' is outside '{request.MinCount}-{request.MaxCount}'.");
        }
    }

    private static void ValidateBoolean(SelectionRequest request, SelectionResult result)
    {
        if (result.SelectedBoolean is null)
        {
            if (request.CanSkip && result.SelectedTargets.Count == 0)
            {
                return;
            }

            throw new DomainException("SelectionResult requires a selected boolean.");
        }
    }

    private static void ValidateOption(SelectionRequest request, SelectionResult result)
    {
        if (string.IsNullOrWhiteSpace(result.SelectedOption))
        {
            throw new DomainException("SelectionResult requires a selected option.");
        }

        var exists = request.Candidates.Any(candidate =>
            candidate.Kind == SelectionTargetKind.Option
            && (string.Equals(candidate.OptionValue, result.SelectedOption, StringComparison.Ordinal)
                || string.Equals(candidate.StableId, result.SelectedOption, StringComparison.Ordinal)));

        if (!exists)
        {
            throw new DomainException($"Selected option '{result.SelectedOption}' is not a candidate for request '{request.Id}'.");
        }
    }

    private static void ValidateTargets(SelectionRequest request, SelectionResult result)
    {
        var selectedTargets = result.SelectedTargets;
        if (selectedTargets.Count == 0 && request.CanSkip)
        {
            return;
        }

        if (selectedTargets.Count < request.MinCount)
        {
            throw new DomainException(
                $"Selected target count '{selectedTargets.Count}' is below minimum '{request.MinCount}'.");
        }

        if (selectedTargets.Count > request.MaxCount)
        {
            throw new DomainException(
                $"Selected target count '{selectedTargets.Count}' is above maximum '{request.MaxCount}'.");
        }

        if (!request.CanEndNotMax && selectedTargets.Count != request.MaxCount)
        {
            throw new DomainException(
                $"Selected target count '{selectedTargets.Count}' must equal max count '{request.MaxCount}'.");
        }

        var candidatesByStableId = request.Candidates.ToDictionary(candidate => candidate.StableId, StringComparer.Ordinal);
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var target in selectedTargets)
        {
            if (!seen.Add(target.StableId))
            {
                throw new DomainException($"Target '{target.StableId}' was selected more than once.");
            }

            if (!candidatesByStableId.TryGetValue(target.StableId, out var candidate))
            {
                throw new DomainException($"Target '{target.StableId}' is not a candidate for request '{request.Id}'.");
            }

            if (candidate.Kind != target.Kind || candidate.Kind != request.TargetKind)
            {
                throw new DomainException($"Target '{target.StableId}' has invalid kind '{target.Kind}'.");
            }
        }
    }
}
