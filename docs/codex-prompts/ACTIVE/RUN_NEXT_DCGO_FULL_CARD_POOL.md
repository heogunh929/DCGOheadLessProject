# RUN_NEXT_DCGO_FULL_CARD_POOL

사용자가 다음처럼 입력하면:

```text
다음 dcgo-full queue 작업을 진행해.
```

Codex는 다음 절차를 따른다.

1. `docs/codex-prompts/state/QUEUE_DCGO_FULL_CARD_POOL.md`를 위에서 아래로 읽는다.
2. 가장 먼저 등장하는 미완료 항목을 확인한다.
3. 해당 항목 상태가 `blocked` 또는 `needs-review`면 다음 todo를 건너뛰지 말고 멈춰 사용자에게 이유를 보고한다.
4. 해당 항목이 `todo`면 `in-progress`로 변경한다.
5. prompt 파일과 다음 제약을 읽는다.
   - `templates/dcgo_full_card_pool_constraints.md`
   - `templates/engine_parity_common_constraints.md`, engine-core 단계인 경우
6. 한 항목만 수행한다.
7. 작업 후 상태를 `done`, `blocked`, `needs-review` 중 하나로 변경한다.
8. `PROGRESS_DCGO_FULL_CARD_POOL.md`에 기준 commit, 변경, 테스트, blocker를 기록한다.
9. 다음 항목은 자동 수행하지 않는다.
10. commit과 push를 하지 않는다.
11. 추천 commit message만 보고한다.

66번이 full-card-porting subqueue를 생성한 뒤에는 main queue를 자동 진행하지 않는다.
사용자는 생성된 subqueue를 완료한 후 main queue로 돌아온다.
