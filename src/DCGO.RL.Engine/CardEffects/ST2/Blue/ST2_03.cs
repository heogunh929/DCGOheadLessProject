// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2GarurumonScript : St2SourceTrashScript
{
    public St2GarurumonScript()
        : base(
            "ST2-03",
            "ST2_03",
            EffectTiming.OnAllyAttack,
            requireInheritedSource: true,
            maxTargetLevel: 5,
            trashCount: 1,
            "Inherited OnAllyAttack level 5 or lower opponent Digimon selection trashes 1 bottom digivolution source through SelectionResultApplicator and ZoneMover.")
    {
    }
}
