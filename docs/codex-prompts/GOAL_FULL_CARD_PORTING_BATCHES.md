# GOAL_FULL_CARD_PORTING_BATCHES

이 subqueue는 고정된 DCGO snapshot의 전체 카드/source scaffold를 source-aligned RL.Engine card effects로 porting하기 위한 generated queue다.

- 이 subqueue는 66번 산출물이며, 각 batch는 한 번에 하나의 card-porting, mechanic-layer, source-review 범위만 다룬다.
- `CardId#CardIndex@VariantKey` identity를 유지하고 CardId만으로 variant를 평탄화하지 않는다.
- source body가 없거나 불명확한 항목은 source-review batch에서만 다루며 추측 구현하지 않는다.
- 공통 layer가 미구현된 상태는 `needs-review`가 아니라 `blocked`다.
- `needs-review`는 실제 사용자 판단 또는 source body/source 의미 불명확성에만 사용한다.
- blocker 문서화만으로 card-porting batch를 완료 처리하지 않는다.
- card-porting batch의 `done` 조건은 실제 effect body 구현, registry/status 갱신, 테스트, baseline blocker 감소다.
- `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
- dependency가 `blocked` 또는 `needs-review`이면 card-porting batch를 건너뛰고 가장 앞의 unresolved mechanic dependency를 먼저 구현 대상으로 보고한다.
- Full DCGO Snapshot Completion Gate 전에는 RL Environment, Observation, Reward, Dataset, Trainer를 구현하지 않는다.

현재 queue 66A scheduler는 `scripts/select_next_full_card_porting_batch.py --workspace .`로 다음 실행 대상을 계산한다. `L0006_zone_security_recovery`는 2026-06-22에 `done` 처리되었다.

- Batch count: 423
- Card-porting batches: 397
- Mechanic-layer batches: 12
- Source-review batches: 14
