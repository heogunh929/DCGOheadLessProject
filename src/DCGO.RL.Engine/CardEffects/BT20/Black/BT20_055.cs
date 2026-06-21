// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_055.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_055Script : Bt20BlockedCardScript
{
    public Bt20_055Script()
        : base(
            "BT20-055",
            "BT20_055",
            CardEffectPortingStatus.Unsupported,
            "Security-zone end-of-opponent-turn play-self, OnPlay/WhenDigivolving De-Digivolve 2 plus face-up security flip plus deletion by source count, and OnSecurityCheck top stacked card to face-up bottom security require security-zone trigger source support, De-Digivolve, face-up security selection/flip, and source-to-security movement layers.")
    {
    }
}
