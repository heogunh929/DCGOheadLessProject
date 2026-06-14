// Source mapping: DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St3PoyomonScript : St3DpZeroDeletionTriggerScript
{
    public St3PoyomonScript()
        : base(
            "ST3-01",
            "ST3_01",
            TriggerBody.DpPlus1000,
            "Inherited owner-turn once-per-turn trigger gives this Digimon DP +1000 when an opponent Digimon is deleted by dropping to 0 DP.")
    {
    }
}
