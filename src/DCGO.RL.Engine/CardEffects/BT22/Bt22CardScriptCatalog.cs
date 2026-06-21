namespace DCGO.RL.Engine.CardEffects;

public static class Bt22CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0036Scripts() =>
        new ICardScript[]
        {
            new Bt22_001Script(),
            new Bt22_002Script(),
            new Bt22_004Script(),
        };

    public static CardScriptRegistry CreateC0036Registry() => new(CreateC0036Scripts());

    public static IReadOnlyList<ICardScript> CreateC0037Scripts() =>
        new ICardScript[]
        {
            new Bt22_005Script(),
            new Bt22_006Script(),
            new Bt22_018Script(),
            new Bt22_021Script(),
            new Bt22_027Script(),
            new Bt22_040Script(),
            new Bt22_043Script(),
            new Bt22_044Script(),
            new Bt22_046Script(),
            new Bt22_047Script(),
        };

    public static CardScriptRegistry CreateC0037Registry() => new(CreateC0037Scripts());

    public static IReadOnlyList<ICardScript> CreateC0038Scripts() =>
        new ICardScript[]
        {
            new Bt22_048Script(),
            new Bt22_051Script(),
            new Bt22_054Script(),
            new Bt22_056Script(),
            new Bt22_065Script(),
            new Bt22_069Script(),
            new Bt22_077Script(),
            new Bt22_079Script(),
            new Bt22_092Script(),
            new Bt22_093Script(),
        };

    public static CardScriptRegistry CreateC0038Registry() => new(CreateC0038Scripts());
}
