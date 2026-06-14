using DCGO.RL.Engine.Effects;

// Source mapping: DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_07.cs
namespace DCGO.RL.Engine.CardEffects;

internal sealed class St1GreymonScript : DeclaredCapabilityCardScript
{
    public St1GreymonScript()
        : base(
            "ST1-07",
            "ST1_07",
            "Inherited Security Attack +1 is represented by CardDefinition.SecurityAttackModifier.")
    {
    }
}
