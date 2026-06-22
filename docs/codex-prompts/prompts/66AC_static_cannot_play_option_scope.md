# 66AC Static Cannot-Play Option Scope

## 목적

`ContinuousOrStaticEffect` 중 원본 `ICanNotPlayCardEffect` / `CanNotPlayClass`가 option play legal action과 실행 validation에 반영되도록 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- 원본 `CardSource.CanNotPlayThisOption`, `CanNotPlayClass`, `ICanNotPlayCardEffect` 흐름을 확인한다.
- RL.Engine은 `StaticCardRestrictionDescriptor`로 card-target static restriction을 표현한다.
- `StaticEffectService.HasCardRestriction(..., StaticCardRestrictionKind.CannotPlay)`는 source/target metadata criteria와 condition을 평가한다.
- option play legal action generation과 direct play execution은 같은 static restriction gate를 공유한다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- static cannot-play option restriction이 legal `PlayCardAction` 생성을 막는다.
- direct `PlayCardService.Play`도 같은 static restriction으로 실패한다.
- card metadata criteria와 source metadata criteria가 restriction gate에 반영된다.
- capability truth audit이 66AC evidence를 반영하되 `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
