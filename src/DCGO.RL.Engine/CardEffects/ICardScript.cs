using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Domain;

namespace DCGO.RL.Engine.CardEffects;

public interface ICardScript
{
    CardEffectPortingRecord Porting { get; }

    IReadOnlyList<EffectDescriptor> CreateEffectDescriptors(CardScriptContext context);

    void Resolve(CardScriptExecutionContext context);
}

public interface ICardScriptRegistry
{
    IReadOnlyCollection<CardEffectPortingRecord> PortingRecords { get; }

    bool TryGetScript(CardDefinition definition, out ICardScript script);

    ICardScript GetScript(CardDefinition definition);
}
