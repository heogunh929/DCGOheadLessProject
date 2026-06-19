// Source mapping: no set-local CardEffect source file exists at DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_07.cs.
// Shared source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs via CardBaseEntity ST2_07*.asset CardEffectClassName ST1_06.

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2GrizzlymonScript : SharedSt1_06BlockerMemoryLossScript
{
    public St2GrizzlymonScript()
        : base(
            "ST2-07",
            string.Empty,
            "Shared ST1_06 mapping from ST2_07*.asset: Blocker is represented by CardDefinition.BattleKeywords and OnAllyAttack loses 2 memory. Registry lookup is intentionally CardId-based to avoid duplicate ST1_06 effect-class aliases.")
    {
    }
}
