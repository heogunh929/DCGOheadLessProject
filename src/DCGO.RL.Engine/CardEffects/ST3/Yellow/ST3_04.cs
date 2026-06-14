// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3TokomonScript : St3DpZeroDeletionTriggerScript
{
    public St3TokomonScript()
        : base(
            "ST3-04",
            "ST3_04",
            TriggerBody.MemoryPlus1,
            "Inherited owner-turn once-per-turn trigger gains 1 memory when an opponent Digimon is deleted by dropping to 0 DP.")
    {
    }
}
