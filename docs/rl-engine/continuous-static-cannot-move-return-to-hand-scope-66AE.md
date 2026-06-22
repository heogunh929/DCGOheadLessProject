# 66AE static cannot-move return-to-hand scope

이번 foundation 작업은 원본 `ICanNotMoveEffect` / `CanNotMoveClass` 계열을 RL.Engine의 static card restriction 공통 경로에 연결했다.

원본 `Permanent.CanMove`는 owner, player, self, opponent의 `EffectTiming.None` effect 중 `ICanNotMoveEffect`를 평가해 permanent 이동을 막는다. `CanNotMoveClass`는 target permanent 조건과 원인 effect 조건을 함께 평가하므로, RL.Engine에서는 이를 `StaticCardRestrictionKind.CannotMove`와 `StaticCardRestrictionCause`로 표현한다.

## 확인한 원본 source

| Source | 확인한 흐름 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs` | `CanMove`가 `ICanNotMoveEffect`를 조회해 이동 가능 여부를 결정 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `ICanNotMoveEffect.CanNotMove(Permanent, ICardEffect)` |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CanNotMoveClass.cs` | target permanent condition과 card effect condition 조합 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\EX7\Red\EX7_014.cs` | opponent DP 6000 이하 Digimon 이동 제한 예시 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GiveEffect\GiveEffectToPermanent\CanNotReturnToHand.cs` | return-to-hand 제한 helper |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\CannotReturnToHand.cs` | return-to-hand 제한 factory |

## RL.Engine 대응

- `StaticCardRestrictionKind.CannotMove`를 추가했다.
- `Tier1PrimitiveService.ReturnPermanentToHand(...)`는 top card 이동 전에 `CannotMove` restriction을 확인한다.
- `Tier1PrimitiveService.ReturnPermanentToHandWithEvents(...)`는 would-return rule event를 큐잉하기 전에 같은 gate를 확인한다.
- effect source card/permanent와 `MoveReason.Effect`는 `StaticCardRestrictionCause`로 condition에 전달한다.

## 검증

- `Static card restriction blocks return to hand`
- `Static`: `All 28 tests passed.`
- `CapabilityTruthAudit`: `All 3 tests passed.`

## 남은 범위

- return-to-deck 이동 제한은 별도 movement primitive와 cut-in semantics 정리가 필요하다.
- `ICanNotAffectedEffect`
- `ICanNotTrashFromDigivolutionCardsEffect`
- `CanAddMemory` / `CanAddSecurity` replacement 계열
- full-card source parity evidence는 여전히 `NotRun`이므로 `ContinuousOrStaticEffect`는 `Verified`가 아니다.
