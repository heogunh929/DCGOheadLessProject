namespace DCGO.RL.Engine.CardEffects;

public static class St1CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateScripts() =>
        new ICardScript[]
        {
            new St1KoromonScript(),
            new NoEffectCardScript("ST1-02", notes: "No ST1_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1AgumonScript(),
            new NoEffectCardScript("ST1-04", notes: "No ST1_04 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new NoEffectCardScript("ST1-05", notes: "No ST1_05 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1CoredramonScript(),
            new St1GreymonScript(),
            new St1GarudamonScript(),
            new St1MetalGreymonScript(),
            new NoEffectCardScript("ST1-10", notes: "No ST1_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST1."),
            new St1WarGreymonScript(),
            new St1TaiKamiyaScript(),
            new St1ShadowWingScript(),
            new St1StarlightExplosionScript(),
            new St1GigaDestroyerScript(),
            new St1GaiaForceScript(),
        };

    public static CardScriptRegistry CreateRegistry() => new(CreateScripts());
}
