# 66AC Continuous/Static Cannot-Play Option Scope

## 결정

이번 foundation 작업은 원본 `ICanNotPlayCardEffect` / `CanNotPlayClass` 계열의 option play restriction을 RL.Engine의 option play legal action과 실행 validation에 연결했다.
개별 `CardEffect` body는 구현하지 않았고 C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardSource.CanNotPlayThisOption`은 option card에 한해 player, permanent, self의 `EffectTiming.None` 효과 중 `ICanNotPlayCardEffect`를 조회하고, 조건이 맞으면 option 사용을 막는다.
RL.Engine은 `StaticCardRestrictionDescriptor`와 `StaticCardRestrictionKind.CannotPlay`로 같은 공통 gate를 표현한다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | `CanNotPlayThisOption`이 `EffectTiming.None`의 `ICanNotPlayCardEffect`를 모으는 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `ICanNotPlayCardEffect.CanNotPlay(CardSource)` interface |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CanNotPlayClass.cs` | `CardSource` 조건 기반 cannot-play class |
| `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\EX1\White\EX1_072.cs` | opponent option play restriction을 player duration effect로 부여하는 대표 source |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons.cs` | option 후보 필터에서 `CanNotPlayThisOption`을 제외하는 흐름 |

## RL.Engine 추가 범위

- `StaticCardRestrictionKind.CannotPlay`, `StaticCardRestrictionDescriptor`, `StaticCardRestrictionEvaluation`을 추가했다.
- `ContinuousEffectSourceCollector`는 `IStaticCardRestrictionCardScript`를 통해 source scope별 card restriction descriptor를 수집한다.
- `StaticEffectService.HasCardRestriction(...)`는 target card owner, effective target metadata, condition을 평가한다.
- `LegalActionGenerator`는 option play legal action 생성 전 static cannot-play restriction을 확인한다.
- `PlayCardService`는 direct option play 실행 전 같은 static cannot-play restriction을 확인한다.

## 검증

추가 foundation test:

- `Static card restriction blocks option play`

관련 검증:

- targeted `Static`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- non-option play restriction은 원본에서 `ICanNotPutFieldEffect`가 별도 역할을 하므로 이번 범위에 포함하지 않는다.
- full-card source parity fixture가 아직 `NotRun` 상태이므로 generated registry를 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
