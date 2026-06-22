# 66X Continuous/Static Card Color Requirement Scope

## 결정

이번 foundation 작업은 원본 Unity의 static 카드 색상 변경과 옵션 색상 조건 무시 흐름을 RL.Engine의 공통 evaluation layer로 연결했다.
개별 `CardEffect` body는 구현하지 않았고, C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardSource.CardColors`, `BaseCardColors`, `DualCardColors`, `BaseDualCardColors`는 자신과 field permanent가 제공하는 `IChangeCardColorEffect` / `IChangeBaseCardColorEffect`를 적용해 유효 색상을 계산한다.
옵션 카드의 색상 조건은 `MatchColorRequirement`에서 먼저 `IIgnoreColorConditionEffect`를 확인하고, 무시되지 않으면 owner field permanent의 유효 top-card 색상과 option requirement 색상을 비교한다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeCardColorClass.cs` | current card color list를 source/card 조건에 따라 변환한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeBaseCardColorClass.cs` | base card color list를 source/card 조건에 따라 변환한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\IgnoreColorConditionClass.cs` | option color requirement를 무시할 수 있는 static 조건을 제공한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `IChangeCardColorEffect`, `IChangeBaseCardColorEffect`, `IIgnoreColorConditionEffect` interface를 정의한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | effective colors와 option color requirement gate를 계산한다. |

## RL.Engine 추가 범위

- `StaticCardColorDescriptor`는 source script가 base/current 색상 layer에 deterministic transform을 제공하는 descriptor다.
- `IgnoreColorRequirementDescriptor`는 option color requirement를 통과시킬 수 있는 static predicate다.
- `StaticEffectService`는 `EffectiveCardColors`, `EffectiveOptionColorRequirements`, `MatchesOptionColorRequirement`를 제공한다.
- `BattleRules.CardColors`와 `BattleRules.MatchesOptionColorRequirement`는 static service가 있으면 effective color path를 사용한다.
- `LegalActionGenerator`와 `PlayCardService`는 option play 가능 여부와 실행 검증에서 같은 color gate를 공유한다.
- `StaticRequirementService`는 static digivolution requirement의 color check에서 target top card의 effective color를 사용할 수 있다.

## 검증

추가한 foundation tests:

- `Static card color modifier affects option color requirement`
- `Static ignore color requirement permits option`
- `Static card color modifier affects digivolution color requirement`

관련 회귀 검증:

- targeted `Static`
- targeted `Option lifecycle`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- full-card source parity evidence는 여전히 보수적 `NotRun` 상태다.
- 모든 원본 static color variant와 effect-digivolve corner case의 source-locked parity는 아직 남아 있다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
