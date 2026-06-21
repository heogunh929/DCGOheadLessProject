namespace DCGO.RL.Engine.CardEffects;

public static class Bt18CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0026Scripts() =>
        new ICardScript[]
        {
            new Bt18_052Script(),
            new Bt18_056Script(),
            new Bt18_057Script(),
            new Bt18_058Script(),
            new Bt18_059Script(),
            new Bt18_062Script(),
            new Bt18_068Script(),
            new Bt18_075Script(),
            new Bt18_080Script(),
            new Bt18_085Script(),
        };

    public static CardScriptRegistry CreateC0026Registry() => new(CreateC0026Scripts());

    public static IReadOnlyList<ICardScript> CreateC0027Scripts() =>
        new ICardScript[]
        {
            new Bt18_087Script(),
            new Bt18_090Script(),
            new Bt18_093Script(),
            new Bt18_098Script(),
        };

    public static CardScriptRegistry CreateC0027Registry() => new(CreateC0027Scripts());
}
