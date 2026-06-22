# 66AB Static Link Cost Modifier Scope

## 목적

`ContinuousOrStaticEffect` 중 static link cost modifier가 link execution cost 계산에 반영되도록 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `CardSource.GetChangedLinkCost`, `IChangeLinkCostEffect`, `ChangeLinkCostClass`, `CardEffectFactory.KeyWordEffects.Link` 흐름을 확인한다.
- RL.Engine의 `StaticCostKind.Link`는 normal/static link 실행 비용 계산에 적용된다.
- link card 조건은 `targetCardMetadataCriteria`로, link target permanent 조건은 `targetPermanentMetadataCriteria`로 검증한다.
- `ComplexMechanicService`의 link execution은 `CostResolver`를 통해 shared `StaticEffectService` cost modifier graph를 사용한다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- static link requirement로 생성된 link action이 `StaticCostKind.Link` modifier를 적용한 비용을 지급한다.
- link cost modifier는 link card metadata와 target permanent metadata 조건을 모두 볼 수 있다.
- capability truth audit이 66AB evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
