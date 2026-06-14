// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3MagnaAngemonScript : St3OnAttackDpReductionScript
{
    public St3MagnaAngemonScript()
        : base(
            "ST3-08",
            "ST3_08",
            amount: -1000,
            requireInheritedSource: true,
            "Inherited OnAllyAttack opponent Digimon selection applies temporary DP -1000 until turn end through SelectionResultApplicator and Tier1PrimitiveService.")
    {
    }
}
