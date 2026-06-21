// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_055.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_055Script : Bt19BlockedCardScript
{
    public Bt19_055Script()
        : base(
            "BT19-055",
            "BT19_055",
            CardEffectPortingStatus.PartiallyImplemented,
            "Inherited Reboot is represented by CardDefinition.BattleKeywords in validated fixtures. OnDeletion reveal top 3, select Knightmon/Twilight card to hand, select another to place under a Tamer, and bottom-deck rest requires text/trait metadata, reveal ordering, and card-to-Tamer-source placement.")
    {
    }
}
