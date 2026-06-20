AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 67 - Full Card Porting Batch Completion Audit

## 목표

66번이 생성한 subqueue의 완료 여부와 실제 코드/status/source mapping을 검증한다.

## 처리

1. `QUEUE_FULL_CARD_PORTING_BATCHES.md`를 확인한다.
2. todo/in-progress/blocked/needs-review가 하나라도 있으면:
   - 이 항목을 `blocked`로 표시
   - 미완료 목록을 보고
   - main queue 다음 항목으로 넘어가지 않음
3. 모두 done이면:
   - 전체 테스트
   - full card validator
   - structure/source mapping audit
   - Unsupported/Partial/Stub/NeedsSourceReview/UnknownVariant counts
4. status를 문서가 아니라 registry/code에서 계산한다.
5. source lock drift를 확인한다.

## 완료 기준

- dynamic batch queue 전부 done
- 모든 구현 batch 테스트 통과
- 남은 blocker가 68번에서 다룰 source alignment 범위로 명시됨
