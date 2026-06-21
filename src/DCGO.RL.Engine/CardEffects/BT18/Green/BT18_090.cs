// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_090.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_090Script : Bt18BlockedCardScript
{
    public Bt18_090Script()
        : base(
            "BT18-090",
            "BT18_090",
            CardEffectPortingStatus.Unsupported,
            "Source has SecuritySkill PlaySelfTamerSecurityEffect, OnStartMainPhase discard [Hybrid]/[Ten Warriors] from hand to draw, and inherited battle-deletion play Tamer with inherited effects. CardDefinition does not yet preserve searchable traits/inherited-effect metadata, and security tamer play/free play with ETB continuation remains blocked.")
    {
    }
}
