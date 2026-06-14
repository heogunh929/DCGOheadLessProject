// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_03.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1AgumonScript : St1InheritedDpScript
{
    public St1AgumonScript()
        : base(
            "ST1-03",
            "ST1_03",
            requiredSourceCount: 0,
            "Inherited owner-turn DP +1000 is represented by ContinuousEffectDescriptor.")
    {
    }
}
