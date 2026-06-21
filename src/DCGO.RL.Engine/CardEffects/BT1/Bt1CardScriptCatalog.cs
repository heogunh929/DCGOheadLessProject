namespace DCGO.RL.Engine.CardEffects;

public static class Bt1CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateC0029Scripts() =>
        new ICardScript[]
        {
            new Bt1_005Script(),
            new Bt1_010Script(),
            new Bt1_017Script(),
            new Bt1_018Script(),
            new Bt1_023Script(),
        };

    public static CardScriptRegistry CreateC0029Registry() => new(CreateC0029Scripts());

    public static IReadOnlyList<ICardScript> CreateC0030Scripts() =>
        new ICardScript[]
        {
            new Bt1_025Script(),
            new Bt1_029Script(),
            new Bt1_036Script(),
            new Bt1_043Script(),
            new Bt1_048Script(),
            new Bt1_049Script(),
            new Bt1_053Script(),
            new Bt1_055Script(),
            new Bt1_060Script(),
            new Bt1_061Script(),
        };

    public static CardScriptRegistry CreateC0030Registry() => new(CreateC0030Scripts());

    public static IReadOnlyList<ICardScript> CreateC0031Scripts() =>
        new ICardScript[]
        {
            new Bt1_062Script(),
            new Bt1_063Script(),
            new Bt1_067Script(),
            new Bt1_070Script(),
            new Bt1_074Script(),
            new Bt1_085Script(),
            new Bt1_086Script(),
            new Bt1_087Script(),
            new Bt1_088Script(),
            new Bt1_089Script(),
        };

    public static CardScriptRegistry CreateC0031Registry() => new(CreateC0031Scripts());
}
