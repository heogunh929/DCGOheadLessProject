// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_096.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_096Script : Bt19BlockedCardScript
{
    public Bt19_096Script()
        : base(
            "BT19-096",
            "BT19_096",
            CardEffectPortingStatus.Unsupported,
            "OptionSkill may place one Royal Base Digimon from trash face-up as bottom security, then deletes opponent Digimon up to a dynamic total play-cost cap. SecuritySkill activates the main option. This requires trait metadata, face-up bottom-security placement, multi-target sum constraint selection, deletion continuation, and security main-option continuation.")
    {
    }
}
