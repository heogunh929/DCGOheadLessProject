// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_003.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_003Script : Bt21BlockedCardScript
{
    public Bt21_003Script()
        : base(
            "BT21-003",
            "BT21_003",
            CardEffectPortingStatus.Unsupported,
            "Inherited once-per-turn owner-turn draw when an owner WG trait Digimon is played requires global OnEnterFieldAnyone play payload and trait metadata. Current OnPlay only runs for the played card source itself.")
    {
    }
}
