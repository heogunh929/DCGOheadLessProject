# 66AA Static Link Effective Gate Scope

## 목적

`ContinuousOrStaticEffect` 중 static link requirement가 기존 static metadata/level layer를 공유하도록 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `CardEffectFactory.AddLinkRequirement`, `AddLinkConditionClass`, link `PermanentCondition`/`CardCondition` 흐름을 확인한다.
- RL.Engine의 `StaticRequirementService` link evaluation은 `StaticEffectService`가 있을 때 effective source/target metadata를 사용한다.
- `StaticLinkRequirementEvaluationContext`는 static effect query를 전달받아 target condition에서 effective permanent level을 확인할 수 있다.
- `ComplexMechanicService`의 legal action generation과 execution은 같은 `StaticEffectService` instance를 공유한다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- static link requirement의 source/target metadata criteria가 effective metadata를 볼 수 있다.
- static link requirement target condition이 shared `StaticEffectService`를 통해 effective permanent level을 볼 수 있다.
- production `BattleEngineServices` graph가 `ComplexMechanicService`에도 같은 `StaticEffectService`를 주입한다.
- capability truth audit이 66AA evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
