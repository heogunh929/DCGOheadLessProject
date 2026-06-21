// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_062.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_062Script : Bt20BlockedCardScript
{
    public Bt20_062Script()
        : base(
            "BT20-062",
            "BT20_062",
            CardEffectPortingStatus.PartiallyImplemented,
            "Field-top Retaliation is represented by CardDefinition.BattleKeywords in validated fixtures. Inherited optional OnDeletion hand discard then delete level 4-or-lower opponent Digimon remains blocked by deleted-source trigger eligibility, hand-discard cost continuation, and target deletion continuation.")
    {
    }
}
