// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_033.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_033Script : Bt21BlockedCardScript
{
    public Bt21_033Script()
        : base(
            "BT21-033",
            "BT21_033",
            CardEffectPortingStatus.PartiallyImplemented,
            "Inherited Jamming is represented by CardDefinition.BattleKeywords in validated fixtures. Alternate WG level-2 digivolution and OnPlay reveal top 3, add Avian/Bird and WG, bottom-deck rest require trait metadata, reveal ordering, and multi-condition search selection.")
    {
    }
}
