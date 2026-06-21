namespace DCGO.RL.Engine.CardEffects;

public static class Bt20CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0032Scripts() =>
        new ICardScript[]
        {
            new Bt20_001Script(),
            new Bt20_003Script(),
            new Bt20_004Script(),
            new Bt20_006Script(),
            new Bt20_009Script(),
            new Bt20_034Script(),
            new Bt20_039Script(),
            new Bt20_048Script(),
            new Bt20_049Script(),
            new Bt20_055Script(),
        };

    public static CardScriptRegistry CreateC0032Registry() => new(CreateC0032Scripts());

    public static IReadOnlyList<ICardScript> CreateC0033Scripts() =>
        new ICardScript[]
        {
            new Bt20_062Script(),
            new Bt20_063Script(),
            new Bt20_065Script(),
            new Bt20_067Script(),
            new Bt20_069Script(),
            new Bt20_072Script(),
            new Bt20_075Script(),
            new Bt20_079Script(),
            new Bt20_086Script(),
            new Bt20_088Script(),
        };

    public static CardScriptRegistry CreateC0033Registry() => new(CreateC0033Scripts());

    public static IReadOnlyList<ICardScript> CreateC0034Scripts() =>
        new ICardScript[]
        {
            new Bt20_092Script(),
            new Bt20_096Script(),
        };

    public static CardScriptRegistry CreateC0034Registry() => new(CreateC0034Scripts());
}
