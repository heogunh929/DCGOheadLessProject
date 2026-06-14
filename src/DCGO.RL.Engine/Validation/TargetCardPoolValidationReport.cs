using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Setup;
using System.Text.RegularExpressions;

namespace DCGO.RL.Engine.Validation;

public sealed record TargetCardPoolValidationRequest(
    string TargetName,
    IReadOnlyList<PlayerDeckList> Decks,
    ICardDatabase CardDatabase,
    ICardScriptRegistry CardScriptRegistry);

public sealed record TargetCardPoolValidationReport(
    string TargetName,
    DeckValidationReport DeckValidation,
    IReadOnlyList<CardPoolCardStatus> CardStatuses,
    IReadOnlyList<string> MissingLayers)
{
    public bool IsValid =>
        DeckValidation.IsValid
        && UnsupportedCards.Count == 0
        && PartiallyImplementedCards.Count == 0
        && MissingScripts.Count == 0;

    public IReadOnlyList<CardPoolCardStatus> UnsupportedCards =>
        CardStatuses
            .Where(status => status.PortingStatus == CardEffectPortingStatus.Unsupported)
            .ToArray();

    public IReadOnlyList<CardPoolCardStatus> PartiallyImplementedCards =>
        CardStatuses
            .Where(status => status.PortingStatus == CardEffectPortingStatus.PartiallyImplemented)
            .ToArray();

    public IReadOnlyList<CardPoolCardStatus> MissingScripts =>
        CardStatuses
            .Where(status => !status.HasRegisteredScript)
            .ToArray();
}

public sealed record CardPoolCardStatus(
    string CardId,
    string EffectClassName,
    CardEffectPortingStatus? PortingStatus,
    bool HasRegisteredScript,
    string Notes,
    IReadOnlyList<string> MissingLayers);

public sealed class TargetCardPoolValidator
{
    public TargetCardPoolValidationReport Validate(TargetCardPoolValidationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var deckValidation = new DeckMechanicValidator(request.CardScriptRegistry)
            .ValidateDecks(request.Decks, request.CardDatabase);

        var statuses = EnumerateTargetCardIds(request.Decks)
            .Select(cardId => CreateStatus(cardId, request.CardDatabase, request.CardScriptRegistry))
            .OrderBy(status => status.CardId, StringComparer.Ordinal)
            .ToArray();

        var missingLayers = statuses
            .SelectMany(status => status.MissingLayers)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(layer => layer, StringComparer.Ordinal)
            .ToArray();

        return new TargetCardPoolValidationReport(request.TargetName, deckValidation, statuses, missingLayers);
    }

    private static CardPoolCardStatus CreateStatus(
        string cardId,
        ICardDatabase cardDatabase,
        ICardScriptRegistry cardScriptRegistry)
    {
        if (!cardDatabase.TryGetByCardId(cardId, out var definition))
        {
            return new CardPoolCardStatus(
                cardId,
                string.Empty,
                null,
                HasRegisteredScript: false,
                "Card definition is missing.",
                new[] { "card-definition" });
        }

        var hasScript = cardScriptRegistry.TryGetScript(definition, out var script);
        var record = hasScript
            ? script.Porting
            : cardScriptRegistry.GetScript(definition).Porting;
        var notes = record.Notes;

        return new CardPoolCardStatus(
            definition.CardId,
            definition.CardEffectClassName,
            record.Status,
            hasScript,
            notes,
            record.IsRunnableInValidatedDeck
                ? Array.Empty<string>()
                : InferMissingLayers(notes).ToArray());
    }

    private static IEnumerable<string> EnumerateTargetCardIds(IReadOnlyList<PlayerDeckList> decks) =>
        decks
            .SelectMany(deck => deck.MainDeck.Concat(deck.DigiEggDeck))
            .Distinct(StringComparer.Ordinal);

    private static IEnumerable<string> InferMissingLayers(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
        {
            yield break;
        }

        foreach (Match match in Regex.Matches(notes, @"missing-layer:([A-Za-z0-9_.-]+)"))
        {
            yield return match.Groups[1].Value;
        }

        if (Contains(notes, "continuous") || Contains(notes, "aura") || Contains(notes, "dynamic"))
        {
            yield return "continuous-effect";
        }

        if (Contains(notes, "duration") || Contains(notes, "until") || Contains(notes, "cleanup"))
        {
            yield return "duration-cleanup";
        }

        if (Contains(notes, "select") || Contains(notes, "selection-result"))
        {
            yield return "selection-result-application";
        }

        if (Contains(notes, "security option") || Contains(notes, "security effect") || Contains(notes, "security play"))
        {
            yield return "security-option-execution";
        }

        if (Contains(notes, "OnAllyAttack")
            || Contains(notes, "OnBlockAnyone")
            || Contains(notes, "trigger pipeline"))
        {
            yield return "full-trigger-pipeline";
        }
    }

    private static bool Contains(string value, string pattern) =>
        value.Contains(pattern, StringComparison.OrdinalIgnoreCase);
}
