using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.Effects;

public static class EnterFieldEventPayload
{
    public const string Card = "Card";
    public const string Permanent = "Permanent";
    public const string EnteredCards = "EnteredCards";
    public const string EnteredPermanents = "EnteredPermanents";
    public const string IsEvolution = "IsEvolution";
    public const string IsJogress = "IsJogress";
    public const string DigiXrosCount = "DigiXrosCount";
    public const string AssemblyCount = "AssemblyCount";
    public const string Played = "Played";
    public const string Digivolved = "Digivolved";

    public static IReadOnlyDictionary<string, object?> ForSinglePermanent(
        CardInstanceId card,
        PermanentId permanent,
        bool isEvolution,
        bool isJogress = false,
        int digiXrosCount = 0,
        int assemblyCount = 0) =>
        new Dictionary<string, object?>(StringComparer.Ordinal)
        {
            [Card] = card,
            [Permanent] = permanent,
            [EnteredCards] = new[] { card },
            [EnteredPermanents] = new[] { permanent },
            [IsEvolution] = isEvolution,
            [IsJogress] = isJogress,
            [DigiXrosCount] = digiXrosCount,
            [AssemblyCount] = assemblyCount,
            [Played] = !isEvolution,
            [Digivolved] = isEvolution,
        };

    public static bool IsEvolutionEvent(EffectContext context) =>
        context.GetValueOrDefault(IsEvolution) is bool value && value;

    public static bool IsPlayedEvent(EffectContext context) =>
        context.GetValueOrDefault(Played) is bool value && value;

    public static bool ContainsEnteredCard(EffectContext context, CardInstanceId card) =>
        Contains(context.GetValueOrDefault(EnteredCards), card);

    public static bool ContainsEnteredPermanent(EffectContext context, PermanentId permanent) =>
        Contains(context.GetValueOrDefault(EnteredPermanents), permanent);

    private static bool Contains<T>(object? value, T target)
        where T : struct, IEquatable<T> =>
        value switch
        {
            T single => single.Equals(target),
            IEnumerable<T> values => values.Contains(target),
            _ => false,
        };
}
