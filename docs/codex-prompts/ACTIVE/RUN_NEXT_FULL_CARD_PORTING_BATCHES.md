# RUN_NEXT_FULL_CARD_PORTING_BATCHES

사용자가 다음처럼 입력하면:

```text
다음 full-card-porting batch 작업을 진행해.
```

Codex는 다음 절차를 따른다.

1. `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`를 위에서 아래로 읽는다.
2. 가장 먼저 등장하는 `todo` 항목 하나만 `in-progress`로 바꾼다.
3. 해당 prompt 파일과 `docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md`를 읽는다.
4. 한 batch만 수행한다.
5. 완료 시 `done`, `blocked`, `needs-review` 중 하나로 상태를 바꾼다.
6. `docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md`에 변경, 테스트, blocker를 기록한다.
7. main `QUEUE_DCGO_FULL_CARD_POOL.md`의 67번은 generated subqueue가 모두 끝난 뒤 실행한다.
8. commit/push는 하지 않고 추천 commit message만 보고한다.
