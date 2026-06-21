// Source mapping: DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_092.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class Bt19_092Script : Bt19BlockedCardScript
{
    public Bt19_092Script()
        : base(
            "BT19-092",
            "BT19_092",
            CardEffectPortingStatus.Unsupported,
            "OptionSkill optionally returns one own blue Digimon to deck bottom, then returns one opponent level 4 or 6-or-lower Digimon to deck bottom. SecuritySkill activates the main option. Source-aligned implementation requires return-to-library-bottom cut-in/replacement, chained optional target stages, and security main-option continuation.")
    {
    }
}
