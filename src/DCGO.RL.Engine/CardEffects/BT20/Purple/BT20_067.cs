// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_067.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_067Script : Bt20BlockedCardScript
{
    public Bt20_067Script()
        : base(
            "BT20-067",
            "BT20_067",
            CardEffectPortingStatus.Unsupported,
            "OnPlay/WhenDigivolving temporary Retaliation grant needs temporary keyword grants and UntilOpponentTurnEnd duration. Inherited optional OnDeletion hand discard then delete level 4-or-lower opponent Digimon remains blocked by deleted-source trigger eligibility and hand-discard cost continuation.")
    {
    }
}
