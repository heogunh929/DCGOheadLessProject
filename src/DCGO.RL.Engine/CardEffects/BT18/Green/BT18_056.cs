// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_056.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_056Script : Bt18BlockedCardScript
{
    public Bt18_056Script()
        : base(
            "BT18-056",
            "BT18_056",
            CardEffectPortingStatus.PartiallyImplemented,
            "Piercing and Reboot are represented by CardDefinition.BattleKeywords. Source also has alternate Royal Base digivolution, OnPlay/WhenDigivolving suspend plus cannot-unsuspend duration, and optional once-per-turn battle-deletion unsuspend; those common layers remain blocked.")
    {
    }
}
