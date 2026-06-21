namespace DCGO.RL.Engine.CardEffects;

public static class Bt21CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0034Scripts() =>
        new ICardScript[]
        {
            new Bt21_001Script(),
            new Bt21_003Script(),
            new Bt21_007Script(),
            new Bt21_008Script(),
            new Bt21_011Script(),
            new Bt21_012Script(),
            new Bt21_015Script(),
            new Bt21_024Script(),
        };

    public static CardScriptRegistry CreateC0034Registry() => new(CreateC0034Scripts());

    public static IReadOnlyList<ICardScript> CreateC0035Scripts() =>
        new ICardScript[]
        {
            new Bt21_033Script(),
            new Bt21_048Script(),
            new Bt21_049Script(),
            new Bt21_055Script(),
            new Bt21_063Script(),
            new Bt21_065Script(),
            new Bt21_080Script(),
            new Bt21_082Script(),
            new Bt21_085Script(),
        };

    public static CardScriptRegistry CreateC0035Registry() => new(CreateC0035Scripts());

    public static IReadOnlyList<ICardScript> CreateC0036Scripts() =>
        new ICardScript[]
        {
            new Bt21_088Script(),
            new Bt21_090Script(),
            new Bt21_091Script(),
            new Bt21_092Script(),
            new Bt21_093Script(),
            new Bt21_095Script(),
            new Bt21_099Script(),
        };

    public static CardScriptRegistry CreateC0036Registry() => new(CreateC0036Scripts());
}
