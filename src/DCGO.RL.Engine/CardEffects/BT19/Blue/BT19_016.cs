// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_016.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_016Script : Bt19BlockedCardScript
{
    public Bt19_016Script()
        : base(
            "BT19-016",
            "BT19_016",
            CardEffectPortingStatus.Unsupported,
            "Source OnPlay and OnDeletion place one [Blue Flare] Digimon card from hand under an owner Tamer, then draw 1. CardDefinition does not yet preserve searchable traits, and hand-to-Tamer-source placement with follow-up draw remains a common layer blocker.")
    {
    }
}
