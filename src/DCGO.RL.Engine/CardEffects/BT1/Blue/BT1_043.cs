// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_043.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_043Script : Bt1BlockedCardScript
{
    public Bt1_043Script()
        : base(
            "BT1-043",
            "BT1_043",
            CardEffectPortingStatus.Unsupported,
            "WhenDigivolving target selection then trash 4 opponent digivolution cards from bottom requires source-trash ordering, protection/replacement, and source-aligned digivolution-card discard continuation.")
    {
    }
}
