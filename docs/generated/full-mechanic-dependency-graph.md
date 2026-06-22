# Mechanic Dependency Graph

- Inventory SHA-256: `f10fff14bd37d401b3e4a48ddd6941d767ebd046ac73f9259795cb7e25ffff97`

```mermaid
flowchart TD
  CardBaseEntity["CardBaseEntity assets"]
  CardEffectClassName["CardEffectClassName"]
  CardEffectSource["CardEffect source files"]
  ICardEffect["ICardEffect flags/timings"]
  FactoryCommons["CardEffectFactory/Commons"]
  SelectionApis["Unity selection APIs"]
  TriggerPipeline["RL TriggerPipelineService"]
  RuleProcessor["RL RuleProcessor"]
  AttackSecurity["RL Attack/Security services"]
  ComplexMechanics["RL ComplexMechanicService"]
  MissingLayers["Missing/partial layers"]
  CardBaseEntity -->|"8186 gameplay records"| CardEffectClassName
  CardEffectClassName -->|"3917 source classes"| CardEffectSource
  CardEffectSource -->|"EffectTiming / flags / CanActivate"| ICardEffect
  CardEffectSource -->|"factory/commons API usage"| FactoryCommons
  CardEffectSource -->|"selection/root-zone usage"| SelectionApis
  ICardEffect -->|"timing, priority, optional, background"| TriggerPipeline
  ICardEffect -->|"RulesTiming / state stabilization"| RuleProcessor
  ICardEffect -->|"counter/block/security/battle hooks"| AttackSecurity
  FactoryCommons -->|"special play/digivolve/material mechanics"| ComplexMechanics
  SelectionApis -->|"unsupported root/selection gaps"| MissingLayers
  FactoryCommons -->|"unsupported factory/keyword gaps"| MissingLayers
```

## Nodes

| Node | Label |
| --- | --- |
| AttackSecurity | RL Attack/Security services |
| CardBaseEntity | CardBaseEntity assets |
| CardEffectClassName | CardEffectClassName |
| CardEffectSource | CardEffect source files |
| ComplexMechanics | RL ComplexMechanicService |
| FactoryCommons | CardEffectFactory/Commons |
| ICardEffect | ICardEffect flags/timings |
| MissingLayers | Missing/partial layers |
| RuleProcessor | RL RuleProcessor |
| SelectionApis | Unity selection APIs |
| TriggerPipeline | RL TriggerPipelineService |
