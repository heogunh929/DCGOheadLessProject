// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_055.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_055Script : Bt21BlockedCardScript
{
    public Bt21_055Script()
        : base(
            "BT21-055",
            "BT21_055",
            CardEffectPortingStatus.Unsupported,
            "Your-turn Mineral/Rock digivolution cost -1 and inherited OnDigivolutionCardDiscarded delete play-cost-4-or-less opponent Digimon remain blocked by trait-aware cost modifiers, OnDigivolutionCardDiscarded payload/source validation, and trash-zone inherited trigger resolution.")
    {
    }
}
