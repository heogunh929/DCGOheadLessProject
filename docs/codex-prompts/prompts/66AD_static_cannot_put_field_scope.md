# 66AD static cannot-put-field scope

## 목표

원본 `ICanNotPutFieldEffect` / `CanNotPutFieldClass` 계열을 RL.Engine의 공통 static card restriction layer에 연결한다.

이번 범위는 개별 `CardEffect` body 구현이 아니다. 원본에서 permanent card가 새 permanent로 field에 놓이는 것을 막는 공통 API를 RL.Engine foundation에 추가한다.

## Source of Truth

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`
  - `CanPlayFromHandDuringMainPhase`
  - `CanPutFieldThisPermanentCard`
  - `CanEnterField`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs`
  - `ICanNotPutFieldEffect.CanNotPutField(CardSource, ICardEffect)`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CanNotPutFieldClass.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT14\Red\BT14_017.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT20\Red\BT20_020.cs`

## 구현 요약

- `StaticCardRestrictionKind.CannotPutField`를 추가한다.
- `StaticCardRestrictionCause`를 추가해 원본 `cardEffectCondition(cardEffect)`에 대응할 수 있게 한다.
- hand Digimon/Tamer play legal action과 direct execution은 `CannotPutField` restriction이 있으면 차단한다.
- DigiXros, Assembly, DelayOption처럼 hand에서 새 permanent를 만드는 complex play도 같은 gate를 사용한다.
- effect로 새 permanent를 만드는 primitive는 optional effect source를 받아 effect-caused field entry restriction을 평가한다.

## 검증

- `Static card restriction blocks permanent field play`
- `CapabilityTruthAudit registry is conservative`
- full regression

## 금지 사항

- 개별 `CardEffect` body를 신규 구현하지 않는다.
- C0039 이후 card-porting batch를 실행하지 않는다.
- generated registry를 `Verified`로 수동 승격하지 않는다.
