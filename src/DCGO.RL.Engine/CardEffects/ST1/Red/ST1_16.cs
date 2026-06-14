// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_16.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1GaiaForceScript : St1OptionDeleteScript
{
    public St1GaiaForceScript()
        : base(
            "ST1-16",
            "ST1_16",
            "Main option deletion selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.",
            maxTargets: 1,
            canEndNotMax: false,
            targetPredicate: (_, _) => true)
    {
    }
}
