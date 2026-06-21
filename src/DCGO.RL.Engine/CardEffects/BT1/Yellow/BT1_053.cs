// Source mapping: DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_053.cs
using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt1_053Script : Bt1BlockedCardScript
{
    public Bt1_053Script()
        : base(
            "BT1-053",
            "BT1_053",
            CardEffectPortingStatus.Unsupported,
            "Owner-turn trigger when another level 3 yellow Digimon is played while this Digimon is suspended requires source-aligned OnEnterFieldAnyone global enter payload. Do not flatten this to this card's OnPlay.")
    {
    }
}
