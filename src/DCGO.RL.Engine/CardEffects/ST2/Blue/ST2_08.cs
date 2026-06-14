using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2VeedramonScript : St2NoSourceInheritedContinuousScript
{
    public St2VeedramonScript()
        : base(
            "ST2-08",
            "ST2_08",
            ContinuousModifierKind.SecurityAttack,
            amount: 1,
            "Inherited owner-turn SecurityAttack +1 while the opponent has a Digimon with no digivolution sources.")
    {
    }
}
