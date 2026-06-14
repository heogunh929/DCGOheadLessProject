namespace DCGO.RL.Engine.CardEffects;

public static class St2St3CardScriptCatalog
{
    public static IReadOnlyList<ICardScript> CreateScripts() =>
        new ICardScript[]
        {
            new St2GabumonScript(),
            new NoEffectCardScript("ST2-02", notes: "No ST2_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2GarurumonScript(),
            new NoEffectCardScript("ST2-04", notes: "No ST2_04 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new NoEffectCardScript("ST2-05", notes: "No ST2_05 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2WereGarurumonAceScript(),
            new NoEffectCardScript("ST2-07", notes: "No ST2_07 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2VeedramonScript(),
            new St2ZudomonScript(),
            new NoEffectCardScript("ST2-10", notes: "No ST2_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST2."),
            new St2MetalGarurumonScript(),
            new St2MattIshidaScript(),
            new St2HammerSparkScript(),
            new St2HowlingBlasterScript(),
            new St2WereGarurumonScript(),
            new St2CocytusBreathScript(),

            new St3PoyomonScript(),
            new NoEffectCardScript("ST3-02", notes: "No ST3_02 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new NoEffectCardScript("ST3-03", notes: "No ST3_03 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3TokomonScript(),
            new St3PatamonScript(),
            new NoEffectCardScript("ST3-06", notes: "No ST3_06 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new NoEffectCardScript("ST3-07", notes: "No ST3_07 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3MagnaAngemonScript(),
            new St3AngewomonScript(),
            new NoEffectCardScript("ST3-10", notes: "No ST3_10 CardEffect source file exists in DCGO/Assets/Scripts/CardEffect/ST3."),
            new St3SeraphimonScript(),
            new St3TakeruTakaishiScript(),
            new St3HolyFlameScript(),
            new St3HeavensCharmScript(),
            new St3HolyWaveScript(),
            new St3SevenHeavensScript(),
        };

    public static CardScriptRegistry CreateRegistry() => new(CreateScripts());

    public static CardScriptRegistry CreateCombinedWithSt1Registry() =>
        new(St1CardScriptCatalog.CreateScripts().Concat(CreateScripts()));
}
