# RUN_NEXT_GITHUB_CURRENT

이 파일은 Codex에게 `docs/codex-prompts/state/QUEUE_GITHUB_CURRENT.md`의 첫 번째 `todo` 항목을 실행하게 하기 위한 지시서다.

사용자는 다음처럼 지시한다.

```text
다음 github-current queue 작업을 진행해.
```

Codex는 아래 순서를 지킨다.

1. `docs/codex-prompts/state/QUEUE_GITHUB_CURRENT.md`를 읽는다.
2. 가장 위의 `todo` 항목 하나만 선택한다.
3. 해당 `prompts/*.md` 파일을 읽는다.
4. 그 prompt 하나만 수행한다.
5. 작업 시작 전/후 Git 상태를 보고한다.
6. 작업 완료 후 `QUEUE_GITHUB_CURRENT.md`에서 해당 항목 상태를 `done`, `blocked`, 또는 `needs-review`로 갱신한다.
7. 다음 항목은 수행하지 않는다.
8. commit은 사용자 승인 없이는 만들지 않는다.
