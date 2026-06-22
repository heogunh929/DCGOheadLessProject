# RUN_NEXT_FULL_CARD_PORTING_BATCHES

## 66J Static Requirement Descriptor Scope

66J에서 source/condition-aware static digivolution/link requirement descriptor 경계를 추가했다. `StaticRequirementService`는 `ContinuousEffectSourceCollector` source scope를 공유하며, `LegalActionGenerator`, `DigivolveService`, `ComplexMechanicService`, `RandomLegalActionRunner`가 production graph에서 같은 requirement service를 사용한다.

현재 상태:

- `ContinuousOrStaticEffect`: `PartiallyImplemented`
- 막힌 card batch cursor: `C0039_zone_security_recovery`
- 다음 selector decision: card batch 실행이 아니라 `mechanic-remediation`
- commit/push 금지. 추천 commit message만 보고한다.

사용자가 다음처럼 요청하면:

```text
다음 full-card-porting batch 작업을 진행해
```

Codex는 다음 절차를 따른다.

1. `scripts/select_next_full_card_porting_batch.py --workspace .`를 실행해 다음 대상을 계산한다.
2. `decision`이 `executable`일 때만 해당 `todo` queue 하나를 수행한다.
3. `decision`이 `mechanic-remediation`이면 card-porting batch로 넘어가지 않는다.
4. `blockedCardBatch`가 `C0039_zone_security_recovery`여도 실행하지 않는다.
5. `selectedMechanic` capability를 먼저 구현 대상으로 삼는다.
6. 한 번에 queue 항목 하나만 수행한다.
7. `DCGO/Assets` 원본은 수정하지 않는다.

## 66E Mechanic-First Scheduler

66E 이후 card-porting batch는 coarse category 또는 `dependencyBatchIds`만으로 실행되지 않는다. selector는 현재 `C0039_zone_security_recovery`를 실행하지 않고 `mechanic-remediation` decision을 반환한다.

## Mechanic-First Scheduler Policy

- `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
- card-porting batch는 모든 source `requiredCapabilities`가 `Verified`일 때만 실행 가능하다.
- blocked card batch를 건너뛰어 다음 card batch로 이동하지 않는다.
- 공통 layer 미구현은 `blocked`로 분류한다.
- `needs-review`는 사용자 판단 또는 source 불명확성에만 사용한다.
- card-porting batch `done` 조건은 실제 effect body, registry/status 갱신, 테스트, replay, baseline blocker 감소를 모두 요구한다.

## Current Mechanic Blocker

`ContinuousOrStaticEffect`는 66F~66J에서 source scope, player-runtime stat modifier, hand/trash/executing source, static keyword descriptor, static digivolution/link requirement descriptor coverage를 보강했지만 아직 `Verified`가 아니다.

남은 범위:

- trait/name/text metadata 기반 condition
- cost/restriction/immunity static interfaces
- ignore-digivolution-permission semantics
- trigger body로 처리되는 keyword 흐름
- generated full-card parity evidence

`ContinuousOrStaticEffect`가 실제 구현/테스트/replay/invariant evidence로 `Verified`된 뒤에만 scheduler가 관련 card batches를 다시 `todo`로 여는 후속 queue를 생성한다.
