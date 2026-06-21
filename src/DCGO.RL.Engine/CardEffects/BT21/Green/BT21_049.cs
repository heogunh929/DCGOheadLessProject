// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_049.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_049Script : Bt21BlockedCardScript
{
    public Bt21_049Script()
        : base(
            "BT21-049",
            "BT21_049",
            CardEffectPortingStatus.PartiallyImplemented,
            "Inherited Piercing is represented by CardDefinition.BattleKeywords in validated fixtures. Alternate WG level-3 digivolution, OnPlay/WhenDigivolving optional suspend, and All Turns once-per-turn opponent-play suspend require trait-aware digivolution requirements, source-aligned optional target selection, and OnPermanentPlay payload coverage.")
    {
    }
}
