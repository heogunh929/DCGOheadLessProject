// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_068.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_068Script : Bt18BlockedCardScript
{
    public Bt18_068Script()
        : base(
            "BT18-068",
            "BT18_068",
            CardEffectPortingStatus.PartiallyImplemented,
            "Blocker is represented by CardDefinition.BattleKeywords. Source OnPlay/WhenDigivolving reveals either player's top 5 cards and returns them top/bottom in selected order; reveal ordering/top-bottom placement selection remains blocked.")
    {
    }
}
