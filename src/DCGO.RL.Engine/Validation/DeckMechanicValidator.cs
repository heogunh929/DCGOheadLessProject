using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Setup;

namespace DCGO.RL.Engine.Validation;

public sealed record DeckMechanicValidatorOptions(
    IReadOnlySet<Mechanic>? SupportedMechanics = null,
    IReadOnlySet<BattleKeyword>? SupportedBattleKeywords = null,
    bool AllowStubbedForValidation = false)
{
    public IReadOnlySet<Mechanic> EffectiveSupportedMechanics { get; } =
        SupportedMechanics ?? Enum.GetValues<Mechanic>().ToHashSet();

    public IReadOnlySet<BattleKeyword> EffectiveSupportedBattleKeywords { get; } =
        SupportedBattleKeywords
            ?? Enum.GetValues<BattleKeyword>()
                .Where(keyword => keyword != BattleKeyword.Decoy)
                .ToHashSet();
}

public sealed class DeckMechanicValidator
{
    private readonly ICardScriptRegistry _cardScriptRegistry;
    private readonly DeckMechanicValidatorOptions _options;

    public DeckMechanicValidator(
        ICardScriptRegistry cardScriptRegistry,
        DeckMechanicValidatorOptions? options = null)
    {
        _cardScriptRegistry = cardScriptRegistry ?? throw new ArgumentNullException(nameof(cardScriptRegistry));
        _options = options ?? new DeckMechanicValidatorOptions();
    }

    public DeckValidationReport ValidateDecks(IEnumerable<PlayerDeckList> playerDecks, ICardDatabase cardDatabase)
    {
        var reporter = new UnsupportedMechanicReporter();
        var warnings = new List<string>();

        foreach (var entry in EnumerateDeckEntries(playerDecks, cardDatabase))
        {
            if (entry.Definition is null)
            {
                reporter.Record("UnknownCard", entry.Context);
                continue;
            }

            ValidateDefinition(entry.Definition, entry.Context, reporter, warnings);
        }

        return reporter.HasUnsupportedMechanics
            ? DeckValidationReport.Invalid(reporter.ToErrorMessages(), warnings)
            : new DeckValidationReport(true, Array.Empty<string>(), warnings);
    }

    public DeckValidationReport ValidateDefinitions(IEnumerable<CardDefinition> definitions)
    {
        var reporter = new UnsupportedMechanicReporter();
        var warnings = new List<string>();

        foreach (var definition in definitions)
        {
            ValidateDefinition(definition, $"definition:{definition.CardId}", reporter, warnings);
        }

        return reporter.HasUnsupportedMechanics
            ? DeckValidationReport.Invalid(reporter.ToErrorMessages(), warnings)
            : new DeckValidationReport(true, Array.Empty<string>(), warnings);
    }

    private void ValidateDefinition(
        CardDefinition definition,
        string context,
        UnsupportedMechanicReporter reporter,
        List<string> warnings)
    {
        foreach (var mechanic in definition.Mechanics.Distinct())
        {
            if (!_options.EffectiveSupportedMechanics.Contains(mechanic))
            {
                reporter.RecordCardMechanic(definition, mechanic.ToString(), context);
            }
        }

        foreach (var keyword in definition.BattleKeywords.Distinct())
        {
            if (!_options.EffectiveSupportedBattleKeywords.Contains(keyword))
            {
                reporter.RecordCardMechanic(definition, $"BattleKeyword:{keyword}", context);
            }
        }

        var script = _cardScriptRegistry.GetScript(definition);
        var status = script.Porting.Status;
        if (status == CardEffectPortingStatus.Unsupported)
        {
            reporter.RecordCardMechanic(definition, "CardEffect", $"{context}: {script.Porting.Notes}");
        }
        else if (status == CardEffectPortingStatus.PartiallyImplemented)
        {
            reporter.RecordCardMechanic(definition, "CardEffectPartial", $"{context}: {script.Porting.Notes}");
        }
        else if (status == CardEffectPortingStatus.StubbedForValidation && !_options.AllowStubbedForValidation)
        {
            reporter.RecordCardMechanic(definition, "CardEffectStub", $"{context}: stubbed scripts are not allowed in validated decks.");
        }
        else if (status == CardEffectPortingStatus.NoEffect
            && !string.IsNullOrWhiteSpace(definition.CardEffectClassName)
            && !string.Equals(definition.CardEffectClassName, script.Porting.EffectClassName, StringComparison.Ordinal))
        {
            reporter.RecordCardMechanic(definition, "CardEffectNoEffectMismatch", $"{context}: no-effect script does not match effect class.");
        }

        if (status == CardEffectPortingStatus.StubbedForValidation && _options.AllowStubbedForValidation)
        {
            warnings.Add($"{context}: card '{definition.CardId}' uses stubbed card script '{script.Porting.EffectClassName}'.");
        }
    }

    private static IEnumerable<(string Context, CardDefinition? Definition)> EnumerateDeckEntries(
        IEnumerable<PlayerDeckList> playerDecks,
        ICardDatabase cardDatabase)
    {
        foreach (var deck in playerDecks)
        {
            foreach (var cardId in deck.MainDeck)
            {
                yield return ($"player:{deck.Player}:deck:{cardId}", TryGetDefinition(cardDatabase, cardId));
            }

            foreach (var cardId in deck.DigiEggDeck)
            {
                yield return ($"player:{deck.Player}:digitama:{cardId}", TryGetDefinition(cardDatabase, cardId));
            }
        }
    }

    private static CardDefinition? TryGetDefinition(ICardDatabase cardDatabase, string cardId) =>
        cardDatabase.TryGetByCardId(cardId, out var definition) ? definition : null;
}
