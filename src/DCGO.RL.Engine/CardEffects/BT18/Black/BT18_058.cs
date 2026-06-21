// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_058.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_058Script : Bt18BlockedCardScript
{
    public Bt18_058Script()
        : base(
            "BT18-058",
            "BT18_058",
            CardEffectPortingStatus.Unsupported,
            "Source optional OnPlay discards a hand card whose text contains Knightmon, then draws 2; inherited DP +1000 is static. RL CardDefinition does not yet preserve searchable source text, so the hand text-search body is blocked rather than guessed from CardId or name.")
    {
    }
}
