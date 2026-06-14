using DCGO.RL.Engine.Battle;

// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_15.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1GigaDestroyerScript : St1OptionDeleteScript
{
    public St1GigaDestroyerScript()
        : base(
            "ST1-15",
            "ST1_15",
            "Main option deletion selection and security main-option activation share SelectionResultApplicator through SecurityEffectExecutionService.",
            maxTargets: 2,
            canEndNotMax: true,
            targetPredicate: (state, permanent) => BattleRules.Dp(state, permanent) <= 4000)
    {
    }
}
