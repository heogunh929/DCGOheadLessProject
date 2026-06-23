using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Battle;

internal static class CostPaymentRuleEventPayload
{
    public static IReadOnlyDictionary<string, object?> CreateAfterPayCost(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        int paidCost,
        int memoryBeforeCost,
        int memoryAfterCost,
        Zone root,
        Zone sourceZone,
        bool isEvolution,
        IReadOnlyList<PermanentId>? targetPermanents = null,
        bool isJogress = false,
        string? costKind = null)
    {
        var permanents = targetPermanents?.ToArray() ?? Array.Empty<PermanentId>();
        var payload = new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            ["PayCost"] = true,
            ["Card"] = card,
            ["Cards"] = new[] { card },
            ["CardSources"] = new[] { card },
            ["Root"] = root,
            ["SourceZone"] = sourceZone,
            ["isEvolution"] = isEvolution,
            ["isJogress"] = isJogress,
            ["Permanents"] = permanents,
            ["Permanent"] = permanents.Length == 1 ? permanents[0] : null,
            ["CardEffect"] = null,
            ["PlayCardClass"] = null,
            ["PaidCost"] = paidCost,
            ["Cost"] = paidCost,
            ["MemoryBeforeCost"] = memoryBeforeCost,
            ["MemoryAfterCost"] = memoryAfterCost,
            ["CostTransactionId"] = CreateTransactionId(
                state,
                player,
                card,
                paidCost,
                memoryBeforeCost,
                memoryAfterCost,
                isEvolution,
                isJogress),
        };

        if (!string.IsNullOrWhiteSpace(costKind))
        {
            payload["CostKind"] = costKind;
        }

        return payload;
    }

    private static string CreateTransactionId(
        GameState state,
        PlayerId player,
        CardInstanceId card,
        int paidCost,
        int memoryBeforeCost,
        int memoryAfterCost,
        bool isEvolution,
        bool isJogress) =>
        string.Join(
            ":",
            "cost",
            state.TurnCount,
            player.Value,
            card.Value,
            paidCost,
            memoryBeforeCost,
            memoryAfterCost,
            isEvolution ? "evolution" : "play",
            isJogress ? "jogress" : "normal");
}
