# 66J static requirement descriptor scope

## Source mapping

- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`
  - 원본은 `CardSource card`가 제공하는 static effect에서 `CardCondition(cardSource)`와 `PermanentCondition(permanent)`를 검사한다.
  - color/level requirement가 맞고 cost equation이 있으면 해당 cost를 반환한다.
  - `ignoreDigivolutionRequirement`는 player의 ignore permission과 함께 판정한다.
- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`
  - 원본은 `cardSource == card`, `CardCondition(cardSource)`, `PermanentCondition(permanent)`가 맞을 때 `LinkCondition`과 cost를 반환한다.
  - inherited static effect 여부는 source role에 따라 분리된다.

## RL.Engine mapping

- `StaticEvolutionRequirementDescriptor`
  - `SourceCardId`, `SourcePermanentId`, `ControllerPlayerId`를 보존한다.
  - `RequiredColor`, `RequiredLevel`, `MinLevel`, `MaxLevel`, `Cost`, `CostEquation`, source/target condition delegate를 가진다.
  - 66J 시점의 `IgnoreDigivolutionRequirement`는 player permission layer가 없으므로 명시 `UnsupportedMechanicException`으로 막았다. 66M에서 descriptor 기반 permission semantics를 별도 연결했다.
- `StaticLinkRequirementDescriptor`
  - `SourceCardId`, `SourcePermanentId`, `ControllerPlayerId`, `LinkCost`, source/target condition delegate를 가진다.
- 두 descriptor 모두 `ContinuousEffectSourceCollector`의 field top, inherited source, linked card, face-up security, hand, trash, executing source enumeration을 공유한다.
- `StaticRequirementService`는 descriptor를 GameState에서 매번 파생 평가하므로 별도 mutable runtime state를 갖지 않는다.

## Runtime integration

- `BattleRules.CanDigivolve`는 normal `EvoCosts`가 매칭되지 않을 때 `StaticRequirementService` fallback을 본다.
- `LegalActionGenerator`는 static digivolution requirement가 허용하는 action을 생성한다.
- `DigivolveService`는 같은 service로 action execution cost를 검증한다.
- `ComplexMechanicService`는 static link requirement가 허용하는 Link action을 생성하고 실행한다.
- `BattleEngineServices`는 `StaticRequirementService`, `ComplexMechanicService`, `LegalActionGenerator`, `DigivolveService`가 같은 production graph instance를 공유하는지 validation한다.
- `RandomLegalActionRunner`는 services graph의 `LegalActionGenerator`를 사용한다.

## Evidence

- `Static evolution requirement hand source generates and executes`
- `Static evolution requirement stops after source move`
- `Static evolution requirement condition gates target`
- `Static link requirement hand source generates and executes`
- `Static requirement replay deterministic`

## Remaining blockers

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다.

- trait/name/text metadata 기반 condition
- unsupported static effect interfaces
- static cost/restriction/immunity interfaces
- generated full-card parity evidence
