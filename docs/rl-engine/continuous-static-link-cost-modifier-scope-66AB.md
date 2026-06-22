# 66AB Continuous/Static Link Cost Modifier Scope

## 결정

이번 foundation 작업은 원본 `IChangeLinkCostEffect` 계열의 link cost modifier 흐름을 RL.Engine의 link execution 비용 계산에 연결했다.
개별 `CardEffect` body는 구현하지 않았고 C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardSource.GetChangedLinkCost`는 permanent/player/self의 `EffectTiming.None` 효과 중 `IChangeLinkCostEffect`를 모아 link card 조건, target permanent 조건, root 조건을 평가한 뒤 link 비용을 변경한다.
RL.Engine은 root별 source-zone 세분화와 `WhenWouldLink` cut-in은 아직 별도 foundation 범위로 남기고, 이번 범위에서는 `StaticCostKind.Link` modifier가 shared `StaticEffectService`를 통해 link execution cost에 적용되도록 했다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | `GetChangedLinkCost`가 `IChangeLinkCostEffect`를 모아 link cost를 재계산하는 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `IChangeLinkCostEffect.GetCost`, `CardCondition`, `PermanentCondition`, `IsUpDown` interface |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeLinkCostClass.cs` | link card/permanent/root 조건을 적용한 cost 변경 class |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\KeyWordEffects\Link.cs` | `GrantedReduceLinkCostClass`, `ReduceLinkCostClass`, `ChangeLinkCostClass` factory 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs` | link 실행 시 `GetChangedLinkCost`로 실제 비용을 지급하는 흐름 |

## RL.Engine 추가 범위

- `CostResolver.ResolveLink(...)`는 link card, target permanent, base link cost를 받아 `StaticCostKind.Link` modifier를 적용한다.
- `ComplexMechanicService.ExecuteLink`는 normal play requirement link와 static link requirement 모두 같은 link cost resolver를 사용한다.
- production graph에서 `ComplexMechanicService`가 shared `StaticEffectService`로 생성한 `CostResolver`를 사용한다.
- `StaticCostModifierDescriptor.TargetCardMetadataCriteria`와 `TargetPermanentMetadataCriteria`를 통해 원본의 link card/permanent 조건을 공통 descriptor로 표현한다.

## 검증

추가 foundation test:

- `Static link cost modifier adjusts link cost`

관련 검증:

- targeted `Static`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- `WhenWouldLink` timing/cut-in과 root별 link-cost source-zone semantics는 별도 foundation 범위로 남아 있다.
- full-card source parity fixture가 아직 `NotRun` 상태이므로 generated registry를 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
