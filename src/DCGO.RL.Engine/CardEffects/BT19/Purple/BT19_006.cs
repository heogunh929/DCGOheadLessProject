// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_006.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_006Script : Bt19BlockedCardScript
{
    public Bt19_006Script()
        : base(
            "BT19-006",
            "BT19_006",
            CardEffectPortingStatus.Unsupported,
            "Source inherited OnDeletion returns one level 3 purple Digimon card from trash to hand if deleted other than by battle. Trash-to-hand primitive exists, but OnDeletion trigger collection for deleted inherited sources and by-battle deletion payload are not source-aligned yet.")
    {
    }
}
