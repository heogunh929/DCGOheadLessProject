// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_071Script : Bt19BlockedCardScript
{
    public Bt19_071Script()
        : base(
            "BT19-071",
            "BT19_071",
            CardEffectPortingStatus.Unsupported,
            "OnPlay/WhenDigivolving trash top 2 deck cards and grant temporary Blocker, and once-per-turn OnDiscardLibrary deletion require library-discard event support and temporary keyword grants. No CardId shortcut body is used.")
    {
    }
}
