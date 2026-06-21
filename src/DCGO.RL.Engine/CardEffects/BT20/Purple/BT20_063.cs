// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_063.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_063Script : Bt20BlockedCardScript
{
    public Bt20_063Script()
        : base(
            "BT20-063",
            "BT20_063",
            CardEffectPortingStatus.Unsupported,
            "OnPlay reveal top 3, choose Ghost and LIBERATOR cards, and bottom-deck rest requires trait metadata, multi-category reveal selection, and bottom-deck ordering. Inherited OnDeletion memory +1 remains blocked until deleted-source trigger snapshot/eligibility is available.")
    {
    }
}
