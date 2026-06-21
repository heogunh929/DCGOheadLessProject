// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_049.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_049Script : Bt20BlockedCardScript
{
    public Bt20_049Script()
        : base(
            "BT20-049",
            "BT20_049",
            CardEffectPortingStatus.PartiallyImplemented,
            "Inherited Reboot is represented by CardDefinition.BattleKeywords in validated fixtures. OnPlay/WhenDigivolving target restriction 'cannot attack players until opponent turn end' is not equivalent to generic CannotAttack and remains blocked until defender-scoped attack restriction is implemented.")
    {
    }
}
