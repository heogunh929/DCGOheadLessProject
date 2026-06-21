// Source mapping: DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_047.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt22_047Script : Bt22BlockedCardScript
{
    public Bt22_047Script()
        : base(
            "BT22-047",
            "BT22_047",
            CardEffectPortingStatus.Unsupported,
            "Alternative CS level-3 digivolution, OnPlay/WhenDigivolving suspend then conditional can't-unsuspend, and inherited battle-deletion memory +1 remain blocked by CS trait metadata, alternative digivolution requirements, same-level stack predicate, can't-unsuspend duration, and battle-deletion OnEndBattle payload coverage.")
    {
    }
}
