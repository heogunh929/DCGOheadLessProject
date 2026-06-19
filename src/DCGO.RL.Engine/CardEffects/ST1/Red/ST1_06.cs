// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1CoredramonScript : SharedSt1_06BlockerMemoryLossScript
{
    public St1CoredramonScript()
        : base(
            "ST1-06",
            "ST1_06",
            "Blocker is represented by CardDefinition.BattleKeywords. OnAllyAttack loses 2 memory through Tier1PrimitiveService.ModifyMemory.")
    {
    }
}
