namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3SeraphimonScript : St3OnAttackDpReductionScript
{
    public St3SeraphimonScript()
        : base(
            "ST3-11",
            "ST3_11",
            amount: -4000,
            requireInheritedSource: false,
            "OnAllyAttack opponent Digimon selection applies temporary DP -4000 until turn end through SelectionResultApplicator and Tier1PrimitiveService.")
    {
    }
}
