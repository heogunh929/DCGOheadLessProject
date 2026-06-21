// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_046.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_046Script : Bt19BlockedCardScript
{
    public Bt19_046Script()
        : base(
            "BT19-046",
            "BT19_046",
            CardEffectPortingStatus.Unsupported,
            "OnPlay/WhenDigivolving suspend one opponent Digimon, then choose a Data-trait Digimon that cannot unsuspend until opponent turn end. Trait metadata and cannot-unsuspend duration support are not implemented.")
    {
    }
}
