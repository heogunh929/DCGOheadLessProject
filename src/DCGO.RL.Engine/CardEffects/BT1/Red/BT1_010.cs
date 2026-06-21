// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_010.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_010Script : Bt1BlockedCardScript
{
    public Bt1_010Script()
        : base(
            "BT1-010",
            "BT1_010",
            CardEffectPortingStatus.Unsupported,
            "OnPlay reveal top 5, select one Tamer to hand, and place remaining cards at deck bottom in any order. This needs source-aligned reveal/search ordering and deck-bottom ordering support before runnable porting. P-042 variants share this source effect class and remain blocked for the same reason.")
    {
    }
}
