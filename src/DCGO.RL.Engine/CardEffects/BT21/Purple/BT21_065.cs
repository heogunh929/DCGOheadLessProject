// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_065.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_065Script : Bt21BlockedCardScript
{
    public Bt21_065Script()
        : base(
            "BT21-065",
            "BT21_065",
            CardEffectPortingStatus.Unsupported,
            "BeforePayCost Ghost digivolution cost -1 and inherited OnDeletion memory +1 remain blocked by source-aligned BeforePayCost cost-modifier frames and deleted-source inherited trigger eligibility/activation payload coverage.")
    {
    }
}
