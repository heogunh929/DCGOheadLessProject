// Source mapping: DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_093.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt18_093Script : Bt18BlockedCardScript
{
    public Bt18_093Script()
        : base(
            "BT18-093",
            "BT18_093",
            CardEffectPortingStatus.Unsupported,
            "Source has OnStartTurn set-memory-to-3 Tamer effect, OnStartMainPhase discard Option/[Ghost]/[Three Musketeers] from hand to draw, and SecuritySkill PlaySelfTamerSecurityEffect. CardDefinition does not yet preserve searchable traits and security tamer play remains blocked.")
    {
    }
}
