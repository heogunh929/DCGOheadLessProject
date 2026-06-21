namespace DCGO.RL.Engine.CardEffects;

public static class Bt19CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0027Scripts() =>
        new ICardScript[]
        {
            new Bt19_006Script(),
            new Bt19_015Script(),
            new Bt19_016Script(),
            new Bt19_021Script(),
            new Bt19_022Script(),
            new Bt19_026Script(),
        };

    public static CardScriptRegistry CreateC0027Registry() => new(CreateC0027Scripts());

    public static IReadOnlyList<ICardScript> CreateC0028Scripts() =>
        new ICardScript[]
        {
            new Bt19_027Script(),
            new Bt19_028Script(),
            new Bt19_039Script(),
            new Bt19_041Script(),
            new Bt19_045Script(),
            new Bt19_046Script(),
            new Bt19_052Script(),
            new Bt19_055Script(),
            new Bt19_067Script(),
            new Bt19_071Script(),
        };

    public static CardScriptRegistry CreateC0028Registry() => new(CreateC0028Scripts());

    public static IReadOnlyList<ICardScript> CreateC0029Scripts() =>
        new ICardScript[]
        {
            new Bt19_077Script(),
            new Bt19_084Script(),
            new Bt19_089Script(),
            new Bt19_092Script(),
            new Bt19_096Script(),
        };

    public static CardScriptRegistry CreateC0029Registry() => new(CreateC0029Scripts());
}
