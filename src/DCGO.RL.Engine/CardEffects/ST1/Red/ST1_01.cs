// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_01.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1KoromonScript : St1InheritedDpScript
{
    public St1KoromonScript()
        : base(
            "ST1-01",
            "ST1_01",
            requiredSourceCount: 4,
            "Inherited owner-turn DP +1000 while the host Digimon has 4 or more digivolution cards is represented by ContinuousEffectDescriptor.")
    {
    }
}
