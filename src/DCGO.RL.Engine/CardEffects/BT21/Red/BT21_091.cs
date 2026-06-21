// Source mapping: DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_091.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt21_091Script : Bt21BlockedCardScript
{
    public Bt21_091Script()
        : base(
            "BT21-091",
            "BT21_091",
            CardEffectPortingStatus.Unsupported,
            "Ignore-color option requirement, OptionSkill Hybrid discard then draw and Delay placement, OnEnterField Delay hand digivolve from Tamer, and Security optional hand/trash Tamer play then add this card to hand remain blocked by trait/inherited-effect metadata, hand discard cost continuation, delay-option placement, hand/trash play continuation, and hand digivolution support.")
    {
    }
}
