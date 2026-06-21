// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_022.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_022Script : Bt19BlockedCardScript
{
    public Bt19_022Script()
        : base(
            "BT19-022",
            "BT19_022",
            CardEffectPortingStatus.PartiallyImplemented,
            "Blocker and inherited Blocker are represented by CardDefinition.BattleKeywords. OnDeletion place [Blue Flare] from trash under owner Tamer, then Save, requires trait metadata, trash-to-Tamer-source placement, Save, and deleted-source trigger collection.")
    {
    }
}
