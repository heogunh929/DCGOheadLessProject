using DCGO.RL.Engine.Effects;

namespace DCGO.RL.Engine.CardEffects;

public interface ICEntityEffectFactoryProvider
{
    Func<CEntity_Effect> CreateCEntityEffectFactory();
}
