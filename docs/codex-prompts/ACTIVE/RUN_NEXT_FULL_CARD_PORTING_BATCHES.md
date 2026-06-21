# RUN_NEXT_FULL_CARD_PORTING_BATCHES

사용자가 다음처럼 요청하면:

```text
다음 full-card-porting batch 작업을 진행해.
```

Codex는 다음 절차를 따른다.

1. `scripts/select_next_full_card_porting_batch.py --workspace .`를 실행해 다음 실행 가능 batch를 계산한다.
2. `decision`이 `executable`일 때만 해당 `todo` batch 하나를 `in-progress`로 바꾸고 수행한다.
3. `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
4. dependency가 `blocked` 또는 `needs-review`이면 dependent card-porting batch를 건너뛰고, selector가 보고한 `firstUnresolvedMechanicDependency`를 사용자에게 보고한다.
5. 공통 layer 미구현은 `needs-review`가 아니라 `blocked`로 분류한다.
6. `needs-review`는 실제 사용자 판단이 필요하거나 source body/source 의미가 불명확한 경우에만 사용한다.
7. blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
8. card-porting batch의 `done` 조건은 실제 effect body 구현, registry/status 갱신, 테스트, baseline blocker 감소다.
9. batch prompt와 `docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md`를 함께 읽고 한 번에 queue 하나만 수행한다.
10. main `QUEUE_DCGO_FULL_CARD_POOL.md`의 67번은 generated subqueue가 모두 끝난 뒤에만 진행한다.
11. commit/push는 하지 않고 추천 commit message만 보고한다.

현재 보정 기준:

- `L0006_zone_security_recovery`는 `done`이다.
- 다음 실행 가능 batch는 반드시 selector 결과를 따른다.
- selector가 card-porting batch를 `executable`로 반환하더라도, 현재 corrective 작업 턴에서는 자동으로 다음 card-porting batch를 시작하지 않는다. 사용자가 다음 queue 실행을 별도로 요청할 때 selector 결과를 기준으로 진행한다.
- goal은 L0006 완료 후 일시 정지된 상태로 유지한다.
