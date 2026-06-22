# 66Z Continuous/Static Level Requirement Scope

## 결정

이번 foundation 작업은 원본 Unity의 static card/permanent level 변경 흐름을 RL.Engine의 공통 effective level layer로 연결했다.
개별 `CardEffect` body는 구현하지 않았고 C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardSource.TreatedLevel`은 card source가 제공하는 `IChangeCardLevelEffect`를 적용해 top card의 유효 level을 계산한다.
원본 `Permanent.Level`은 top card level을 시작값으로 삼고, field permanent와 player가 제공하는 `IChangePermanentLevelEffect`를 적용해 permanent의 유효 level을 계산한다.
RL.Engine은 이를 `StaticCardLevelDescriptor`, `StaticPermanentLevelDescriptor`와 `StaticEffectService.EffectivePermanentLevel(...)`로 표현한다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | `TreatedLevel`이 `IChangeCardLevelEffect`를 적용하는 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs` | `Level`이 top card level 뒤 field/player `IChangePermanentLevelEffect`를 적용하는 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeCardLevelClass .cs` | card source level 변경 조건 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangePermanentLevelClass.cs` | permanent level 변경 조건 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `IChangeCardLevelEffect`, `IChangePermanentLevelEffect` interface |

## RL.Engine 추가 범위

- `StaticCardLevelDescriptor`는 target card의 effective level transform을 제공한다.
- `StaticPermanentLevelDescriptor`는 target permanent의 effective level transform을 제공한다.
- `StaticEffectService.EffectiveCardLevel`은 card-level descriptor를 적용한다.
- `StaticEffectService.EffectivePermanentLevel`은 effective card level을 시작값으로 삼고 permanent-level descriptor를 적용한다.
- `BattleRules.CanDigivolve` normal digivolution level check는 `StaticEffectService`가 있을 때 effective permanent level을 사용한다.
- `StaticRequirementService` static evolution requirement level check도 같은 effective permanent level을 사용한다.

## 검증

추가 foundation tests:

- `Static card level modifier feeds permanent level requirement`
- `Static permanent level modifier affects normal digivolution requirement`

관련 검증:

- targeted `Static`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- player-level `IChangePermanentLevelEffect` source를 별도 player source descriptor로 표현하는 것은 아직 남아 있다.
- `IChangeCardLevelForAssemblyEffect`, `IAddJogressLevelsEffect`, DigiXros/Assembly 전용 level list 변경은 이번 66Z 범위에 포함하지 않았다.
- full-card source parity evidence는 여전히 보수적 `NotRun` 상태다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
