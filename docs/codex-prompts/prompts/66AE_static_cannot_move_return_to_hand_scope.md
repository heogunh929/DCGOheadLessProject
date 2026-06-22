# 66AE static cannot-move return-to-hand scope

## 목표

원본 `ICanNotMoveEffect` / `CanNotMoveClass` 계열을 RL.Engine의 공통 static card restriction layer에 연결한다.

이번 범위는 개별 `CardEffect` body 구현이 아니라, 원본에서 permanent 이동 가능 여부를 묻는 공통 API 중 return-to-hand 이동을 막는 foundation gate를 추가하는 것이다.

## Source of Truth

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`
  - `CanMove`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs`
  - `ICanNotMoveEffect.CanNotMove(Permanent, ICardEffect)`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CanNotMoveClass.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\EX7\Red\EX7_014.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GiveEffect\GiveEffectToPermanent\CanNotReturnToHand.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\CannotReturnToHand.cs`

## 구현 요약

- `StaticCardRestrictionKind.CannotMove`를 추가한다.
- `Tier1PrimitiveService.ReturnPermanentToHand(...)`와 `ReturnPermanentToHandWithEvents(...)`가 같은 static restriction gate를 사용하게 한다.
- `ReturnPermanentToHandWithEvents(...)`는 would-return rule event를 큐잉하기 전에 `CannotMove`를 먼저 확인한다.
- `StaticCardRestrictionCause`를 통해 effect source card/permanent와 `MoveReason.Effect`를 condition에 전달한다.

## 검증

- `Static card restriction blocks return to hand`
- `CapabilityTruthAudit registry is conservative`
- full regression

## 금지 사항

- 개별 `CardEffect` body를 신규 구현하지 않는다.
- C0039 이후 card-porting batch를 실행하지 않는다.
- generated registry를 `Verified`로 수동 승격하지 않는다.
- return-to-deck 이동 restriction은 이번 범위에 포함하지 않는다.
