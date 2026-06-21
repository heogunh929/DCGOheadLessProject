// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_087.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_087Script : Bt18BlockedCardScript
{
    public Bt18_087Script()
        : base(
            "BT18-087",
            "BT18_087",
            CardEffectPortingStatus.Unsupported,
            "Source has SecuritySkill PlaySelfTamerSecurityEffect, OnStartTurn set-memory-to-3 Tamer effect, and OnLoseSecurity suspend-cost delete DP 4000 or less. Security tamer play and source-aligned suspend cost/replacement layer remain blocked.")
    {
    }
}
