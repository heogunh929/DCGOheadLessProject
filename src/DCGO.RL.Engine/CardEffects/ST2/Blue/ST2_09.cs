// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_09.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2ZudomonScript : St2SourceTrashScript
{
    public St2ZudomonScript()
        : base(
            "ST2-09",
            "ST2_09",
            EffectTiming.WhenDigivolving,
            requireInheritedSource: false,
            maxTargetLevel: null,
            trashCount: 2,
            "Original OnEnterFieldAnyone plus CanTriggerWhenDigivolving maps to WhenDigivolving; opponent Digimon selection trashes up to 2 bottom digivolution sources through SelectionResultApplicator and ZoneMover.")
    {
    }
}
