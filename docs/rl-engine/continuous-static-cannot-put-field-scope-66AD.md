# 66AD static cannot-put-field scope

이번 foundation 작업은 원본 `ICanNotPutFieldEffect` / `CanNotPutFieldClass` 계열을 RL.Engine의 static card restriction layer에 연결했다.

원본 `CardSource.CanEnterField`는 player, permanent, self의 `EffectTiming.None` 효과 중 `ICanNotPutFieldEffect`를 조회해 대상 card가 field에 새 permanent로 놓이는 것을 막는다. `CanNotPutFieldClass`는 대상 card 조건과 원인이 된 `ICardEffect` 조건을 함께 평가한다.

RL.Engine은 이를 `StaticCardRestrictionKind.CannotPutField`와 `StaticCardRestrictionCause`로 표현한다.

## 확인한 원본 source

| Source | 확인한 흐름 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | `CanPlayFromHandDuringMainPhase`, `CanPutFieldThisPermanentCard`, `CanEnterField` |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `ICanNotPutFieldEffect.CanNotPutField(CardSource, ICardEffect)` |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CanNotPutFieldClass.cs` | target card condition과 card effect condition 조합 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT14\Red\BT14_017.cs` | 일반 play까지 막는 static put-field restriction |
| `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT20\Red\BT20_020.cs` | effect source owner를 보는 by-effect put-field restriction |

## RL.Engine 대응

- `StaticCardRestrictionKind.CannotPutField`를 추가했다.
- `StaticCardRestrictionCause`는 effect source card, source permanent, controller, move reason을 담는다.
- `LegalActionGenerator`는 hand Digimon/Tamer play action 생성 전에 `CannotPutField`를 확인한다.
- `PlayCardService`는 direct hand permanent play 실행 전에 같은 gate를 확인한다.
- `ComplexMechanicService`는 DigiXros, Assembly, DelayOption play action 생성과 실행에서 같은 gate를 확인한다.
- `Tier1PrimitiveService.PlayWithoutPayingCost`와 `PlayEvolutionSourceAsNewPermanent`는 effect-caused field entry를 위해 optional effect source를 받는다.

## 검증

- `Static card restriction blocks permanent field play`
- `Static`: `All 27 tests passed.`
- `CapabilityTruthAudit`: `All 3 tests passed.`

## 남은 범위

- `ICanNotMoveEffect`
- `ICanNotTrashFromDigivolutionCardsEffect`
- `ICanNotAffectedEffect`
- `ICanNotSelectBySkillEffect`
- `ICanNotUnsuspendEffect` / `ICanNotSuspendEffect`
- `CanAddMemory` / `CanAddSecurity` replacement 계열
- full-card source parity evidence는 여전히 `NotRun`이므로 `ContinuousOrStaticEffect`는 `Verified`가 아니다.
