# RUN_NEXT_ENGINE_PARITY

사용자가 다음처럼 입력하면:

```text
다음 engine-parity queue 작업을 진행해.
```

Codex는 아래 절차를 따른다.

1. `docs/codex-prompts/state/QUEUE_ENGINE_PARITY.md`를 읽는다.
2. 가장 위에 있는 `todo` 항목 하나를 선택한다.
3. 해당 `prompts/*.md` 파일과 `templates/engine_parity_common_constraints.md`를 읽는다.
4. 선택한 prompt 하나만 수행한다.
5. 시작 시 해당 항목을 `in-progress`로 바꾼다.
6. 작업 후 항목 상태를 `done`, `blocked`, `needs-review` 중 하나로 갱신한다.
7. `docs/codex-prompts/state/PROGRESS_ENGINE_PARITY.md`에 수행 날짜, commit 기준점, 테스트 결과, 남은 위험을 한 줄 추가한다.
8. 다음 queue 항목은 자동으로 수행하지 않는다.
9. 사용자 승인 없이 commit하지 않는다.
10. `DCGO/Assets/Scripts`를 수정하지 않는다.

항목이 `blocked` 또는 `needs-review`가 되면 다음 작업으로 자동 이동하지 말고 사용자 판단을 기다린다.
