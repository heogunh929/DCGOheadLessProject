# RUN_NEXT_SOURCE_ALIGNED

이 파일은 긴 프롬프트를 매번 붙여넣지 않기 위한 실행 지시서다.

## 사용 방법

사용자가 아래처럼 말하면:

```text
다음 source-aligned queue 작업을 진행해.
```

Codex는 다음 순서로 처리한다.

1. `docs/codex-prompts/state/QUEUE_SOURCE_ALIGNED.md`를 읽는다.
2. 첫 번째 `status: todo` 작업을 찾는다.
3. 해당 작업의 `prompt:` 경로를 연다.
4. 그 프롬프트 하나만 수행한다.
5. 작업 완료 후 `QUEUE_SOURCE_ALIGNED.md`의 해당 항목 status를 다음 중 하나로 갱신한다.
   - `done`
   - `needs-review`
   - `blocked`
6. 완료 보고는 한국어로 작성한다.
7. 사용자가 명시 승인하지 않으면 commit하지 않는다.

## 엄격한 제한

- 한 번에 하나의 queue item만 수행한다.
- 다음 queue item을 자동으로 이어서 수행하지 않는다.
- 기존 DCGO Unity 원본 파일은 수정하지 않는다.
- remote 추가/push/fetch/pull 금지.
- RL 학습용 API 구현 금지.
