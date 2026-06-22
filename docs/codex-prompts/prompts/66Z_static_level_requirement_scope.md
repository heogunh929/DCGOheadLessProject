# 66Z Static Level Requirement Scope

## 목적

`ContinuousOrStaticEffect` 중 원본 Unity의 static card/permanent level 변경 흐름을 개별 `CardEffect` body 구현 없이 공통 foundation layer로 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `CardSource.TreatedLevel`, `Permanent.Level`, `ChangeCardLevelClass`, `ChangePermanentLevelClass`, `CardEffectInterfaces` 흐름을 확인한다.
- RL.Engine에는 card level descriptor와 permanent level descriptor를 추가한다.
- normal digivolution level requirement와 static evolution requirement level gate는 `StaticEffectService`가 있을 때 effective permanent level을 사용한다.
- base `CardDefinition.Level` 값은 continuous/static evaluation 중 직접 수정하지 않는다.
- DigiXros/Assembly 전용 level list 변경과 permanent-level source parity 전체 검증은 이번 범위에 포함하지 않는다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- `StaticEffectService`가 effective card level과 effective permanent level을 deterministic하게 계산한다.
- normal digivolution과 static evolution requirement level gate가 같은 effective permanent level query를 공유한다.
- static card/permanent level descriptor에 대한 foundation tests가 있다.
- capability truth audit이 66Z evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
