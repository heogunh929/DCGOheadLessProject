// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_01.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2GabumonScript : St2NoSourceInheritedContinuousScript
{
    public St2GabumonScript()
        : base(
            "ST2-01",
            "ST2_01",
            ContinuousModifierKind.DP,
            amount: 1000,
            "Inherited owner-turn DP +1000 while the opponent has a Digimon with no digivolution sources.")
    {
    }
}
