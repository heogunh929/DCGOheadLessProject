// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_002.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_002Script : Bt22BlockedCardScript
{
    public Bt22_002Script()
        : base(
            "BT22-002",
            "BT22_002",
            CardEffectPortingStatus.Unsupported,
            "Inherited owner-turn once-per-turn draw when an owner Token or other Puppet Digimon is deleted remains blocked by token identity and Puppet trait metadata on the OnDestroyedAnyone payload.")
    {
    }
}
