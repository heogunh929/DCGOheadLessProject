// Source mapping: DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class St2WereGarurumonAceScript : St2SourceTrashScript
{
    public St2WereGarurumonAceScript()
        : base(
            "ST2-06",
            "ST2_06",
            EffectTiming.OnAllyAttack,
            requireInheritedSource: true,
            maxTargetLevel: null,
            trashCount: 1,
            "Inherited OnAllyAttack opponent Digimon selection trashes 1 bottom digivolution source through SelectionResultApplicator and ZoneMover.")
    {
    }
}
