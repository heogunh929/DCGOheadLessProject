# 66X Static Card Color Requirement Scope

## 목적

`ContinuousOrStaticEffect` 중 원본의 카드 색상 변경과 옵션 색상 조건 무시 흐름을 개별 `CardEffect` body 구현 없이 foundation layer로 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `ChangeCardColorClass`, `ChangeBaseCardColorClass`, `IgnoreColorConditionClass`, `CardSource.MatchColorRequirement` 흐름을 확인한다.
- RL.Engine에는 static card color descriptor와 ignore color requirement descriptor를 추가한다.
- 옵션 플레이 legal action과 execution validation은 effective option color requirement를 사용한다.
- digivolution color requirement는 target top card의 effective card color를 사용한다.
- base `CardDefinition` 색상 값은 continuous/static 효과로 직접 수정하지 않는다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- `StaticEffectService`가 base/current card color descriptor와 ignore color requirement descriptor를 deterministic하게 평가한다.
- `BattleRules`, `LegalActionGenerator`, `PlayCardService`, `StaticRequirementService`가 같은 effective color gate를 공유한다.
- 옵션 색상 조건, 색상 조건 무시, digivolution 색상 조건에 대한 foundation test가 있다.
- capability truth audit에 66X evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
