// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_062.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_062Script : Bt18BlockedCardScript
{
    public Bt18_062Script()
        : base(
            "BT18-062",
            "BT18_062",
            CardEffectPortingStatus.Unsupported,
            "Source optional OnPlay/WhenDigivolving discards a hand card whose text contains Knightmon, then grants deletion-by-effect protection until the opponent turn end; inherited DP +1000 is static. Text search and effect-deletion immunity common layers remain blocked.")
    {
    }
}
