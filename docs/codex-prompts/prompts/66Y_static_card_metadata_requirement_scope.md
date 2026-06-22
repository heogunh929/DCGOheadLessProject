# 66Y Static Card Metadata Requirement Scope

## 목적

`ContinuousOrStaticEffect` 중 원본 Unity의 static 카드명/특성 변경 흐름을 개별 `CardEffect` body 구현 없이 공통 foundation layer로 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `CardSource.BaseCardNames`, `CardSource.CardNames`, `CardSource.CardTraits`, `ChangeBaseCardNameClass`, `ChangeCardNamesClass`, `ChangeTraitsClass` 흐름을 확인한다.
- RL.Engine에는 base/current card name descriptor와 card trait descriptor를 추가한다.
- static cost/restriction/immunity/color/ignore-color criteria와 static evolution requirement criteria는 `StaticEffectService`가 있을 때 effective metadata를 사용한다.
- base `CardDefinition`의 name/trait/text 값은 continuous/static evaluation 중 직접 수정하지 않는다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- `StaticEffectService`가 effective card names/traits와 `CardMetadataSnapshot` 기반 criteria matching을 deterministic하게 계산한다.
- static metadata descriptor가 cost criteria에 반영되는 foundation test가 있다.
- capability truth audit이 66Y evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
