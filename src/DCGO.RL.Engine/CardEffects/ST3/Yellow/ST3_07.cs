// Source mapping: no set-local CardEffect source file exists at DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_07.cs.
// Shared source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs via CardBaseEntity ST3_07*.asset CardEffectClassName ST1_06.

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3UnimonScript : SharedSt1_06BlockerMemoryLossScript
{
    public St3UnimonScript()
        : base(
            "ST3-07",
            string.Empty,
            "ST1_06",
            "Shared ST1_06 mapping from ST3_07*.asset: Blocker is represented by CardDefinition.BattleKeywords and OnAllyAttack loses 2 memory. Registry lookup is intentionally CardId-based to avoid duplicate ST1_06 effect-class aliases.")
    {
    }
}
