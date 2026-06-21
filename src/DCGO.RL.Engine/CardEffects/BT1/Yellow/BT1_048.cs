// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_048.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_048Script : Bt1BlockedCardScript
{
    public Bt1_048Script()
        : base(
            "BT1-048",
            "BT1_048",
            CardEffectPortingStatus.Unsupported,
            "OnPlay reveal top 4, add all yellow Tamers, and place remaining cards at deck bottom in any order requires reveal/search all-match and deck-bottom ordering continuation.")
    {
    }
}
