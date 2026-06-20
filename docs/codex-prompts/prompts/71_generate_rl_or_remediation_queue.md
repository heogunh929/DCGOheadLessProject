AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 71 - RL Queue 또는 Remediation Queue 생성

## 목표

70번 gate 결과에 따라 다음 queue를 자동 생성한다.

## Gate 실패 시

다음을 생성한다.

```text
docs/codex-prompts/GOAL_FULL_DCGO_REMEDIATION.md
docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_DCGO_REMEDIATION.md
docs/codex-prompts/state/QUEUE_FULL_DCGO_REMEDIATION.md
docs/codex-prompts/prompts/generated/remediation/*.md
```

각 prompt는 구체적인 blocker/card/mechanic/source path를 포함한다.

## Gate 통과 시

RL queue 파일은 생성할 수 있지만 모두 `blocked` 상태로 둔다.

```text
docs/codex-prompts/GOAL_RL_FULL_DCGO.md
docs/codex-prompts/ACTIVE/RUN_NEXT_RL_FULL_DCGO.md
docs/codex-prompts/state/QUEUE_RL_FULL_DCGO.md
docs/codex-prompts/prompts/generated/rl/*.md
```

포함할 단계:

- environment contract
- observation/hidden information
- action encoding/mask
- Reset/Step/Resume
- vectorized environments
- performance benchmark
- baseline agents
- self-play training harness
- evaluation
- model export/inference adapter

## 사용자 승인

- gate pass와 사용자 명시 승인 전 RL queue 첫 항목을 todo로 바꾸지 않는다.
- 이 작업 자체에서는 RL 코드를 구현하지 않는다.
