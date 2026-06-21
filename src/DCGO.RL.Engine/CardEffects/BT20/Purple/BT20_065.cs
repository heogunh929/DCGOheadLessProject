// Source mapping: DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_065.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt20_065Script : Bt20BlockedCardScript
{
    public Bt20_065Script()
        : base(
            "BT20-065",
            "BT20_065",
            CardEffectPortingStatus.PartiallyImplemented,
            "Inherited Retaliation is represented by CardDefinition.BattleKeywords in validated fixtures. OnPlay hand discard then grant an opponent Digimon an UntilOpponentTurnEnd OnDeletion memory-loss effect remains blocked by hand-discard cost continuation, granted trigger effects, duration cleanup, and effect-immunity checks.")
    {
    }
}
