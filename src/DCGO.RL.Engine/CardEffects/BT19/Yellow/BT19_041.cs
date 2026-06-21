// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_041.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_041Script : Bt19BlockedCardScript
{
    public Bt19_041Script()
        : base(
            "BT19-041",
            "BT19_041",
            CardEffectPortingStatus.Unsupported,
            "OnPlay/WhenDigivolving trash top security before selecting a friendly Digimon, then grant Blocker and DP +6000 until opponent turn end. This requires pre-selection mutation continuation and temporary keyword grant support. WhenRemoveField recovery also depends on source-aligned would-leave timing.")
    {
    }
}
