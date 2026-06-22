# 66W OnEnterFieldAnyone Payload Scope

## 목적

`OnEnterFieldAnyone` timing을 개별 `CardEffect` body 구현 없이 foundation payload layer로 보강한다.

## 범위

- Source of Truth는 `E:\headlessDCGO\DCGO\Assets`의 Unity battle source다.
- `OnEnterFieldAnyone`를 `OnPlay` 또는 `WhenDigivolving`으로 일괄 평탄화하지 않는다.
- play/digivolve 완료 후 self timing group과 global enter-field timing group을 같은 prepared trigger sequence로 연결한다.
- generated full-card scaffold나 registry를 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 금지한다.

## 완료 조건

- `EnterFieldEventPayload`가 play/digivolve enter-field payload key를 공통화한다.
- `PlayCardService`와 `DigivolveService`가 self timing group 뒤에 global `OnEnterFieldAnyone` group을 tail로 연결한다.
- selection pause/resume 이후에도 global tail group이 이어지는 테스트가 있다.
- Foundation Completion Gate를 다시 계산하고 `OpenCodeReady=false` 상태를 보고한다.
