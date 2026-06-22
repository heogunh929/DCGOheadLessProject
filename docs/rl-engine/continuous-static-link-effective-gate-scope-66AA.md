# 66AA Continuous/Static Link Effective Gate Scope

## 결정

이번 foundation 작업은 static link requirement evaluation이 기존 `StaticEffectService` effective metadata/level layer를 공유하게 했다.
개별 `CardEffect` body는 구현하지 않았고 C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardEffectFactory.AddLinkRequirement`는 link card 조건과 target permanent 조건을 static effect로 제공한다.
RL.Engine은 `StaticLinkRequirementDescriptor`의 source/target metadata criteria와 target condition을 `StaticEffectService`가 있는 경로에서 effective metadata/level 기준으로 평가한다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\AddLinkRequirement.cs` | static link condition의 card/permanent condition 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | link/static effect interface 위치 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | effective metadata/level source query의 기존 static timing 연결 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs` | permanent effective level 흐름 |

## RL.Engine 추가 범위

- `StaticRequirementService.EvaluateLinkRequirements`와 `FirstLinkRequirement`에 optional `StaticEffectService`를 전달한다.
- link source/target metadata criteria는 `StaticEffectService`가 있을 때 effective metadata를 사용한다.
- `StaticLinkRequirementEvaluationContext.StaticEffects`를 통해 target condition이 effective permanent level을 조회할 수 있다.
- `ComplexMechanicService`는 legal action generation과 execution 모두에서 shared static effect service를 사용한다.
- `BattleEngineServices` production graph validation은 `ComplexMechanicService`의 `StaticEffectService` instance 공유를 검증한다.

## 검증

추가 foundation test:

- `Static link requirement uses effective metadata and level`

관련 검증:

- targeted `Static`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- link requirement source parity는 full-card source fixture가 아직 `NotRun` 상태다.
- DigiXros/Assembly/Jogress 전용 material matching의 effective metadata/level 공유는 별도 foundation 범위로 남아 있다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
