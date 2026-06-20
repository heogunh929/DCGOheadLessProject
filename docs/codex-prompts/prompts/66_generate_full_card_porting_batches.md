AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 66 - Full Card Porting Batch Queue 자동 생성

## 목표

62~65 산출물을 읽고 실제 카드 포팅 작업을 dependency-aware batch로 자동 생성한다.
이번 작업에서는 효과 구현을 하지 않는다.

## 생성 파일

```text
docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/prompts/generated/full-card/*.md
```

## Batch 생성 규칙

- 구체적인 CardId/variant/source file 목록을 prompt에 포함한다.
- `{카드명}` 같은 사용자가 채울 placeholder를 만들지 않는다.
- 한 batch는 하나의 공통 missing layer 또는 기존 layer로 구현 가능한 작은 카드 집합이다.
- 공통 layer가 선행돼야 하는 카드는 dependency 순서로 배치한다.
- 위험도가 높은 mechanic은 별도 batch다.
- batch별 최대 변경 범위를 제한한다.
- 카드별 파일 구현 원칙을 유지한다.
- 각 batch에 원본 파일, 테스트, 문서, blocker 조건을 적는다.
- source 불명확 카드는 구현 queue가 아니라 review queue로 분리한다.

## 권장 큰 순서

- 기존 layer로 즉시 구현
- draw/search/reveal/hidden information
- zone/security/recovery
- continuous/duration/inherited/linked
- attack/security timing
- simultaneous trigger/priority
- replacement/counter/cut-in
- special digivolution/play mechanics
- high-risk source review

## 완료

- 생성된 subqueue 요약
- batch 수, 카드 수, mechanic coverage
- 실행 명령:

```text
다음 full-card-porting batch 작업을 진행해.
```
