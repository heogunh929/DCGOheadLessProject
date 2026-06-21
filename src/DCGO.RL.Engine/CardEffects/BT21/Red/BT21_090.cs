// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_090.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_090Script : Bt21BlockedCardScript
{
    public Bt21_090Script()
        : base(
            "BT21-090",
            "BT21_090",
            CardEffectPortingStatus.Unsupported,
            "Ignore-color option requirement, OptionSkill reveal/search then Delay option placement, OnAddDigivolutionCards Delay hand digivolve, and Security hand/trash Gammamon play then Delay option placement remain blocked by text metadata, reveal ordering, delay-option battle-area placement, hand/trash play continuation, and hand digivolution support.")
    {
    }
}
