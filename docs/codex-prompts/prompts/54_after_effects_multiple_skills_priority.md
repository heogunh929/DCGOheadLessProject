AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 54 - AfterEffectsActivate / MultipleSkills Priority

## 목표

원본 `AutoProcessing`, `MultipleSkills`의 동시 trigger 그룹과 후속 `AfterEffectsActivate` 처리를 generic engine layer로 이식한다.

## 구현 범위

- 같은 timing에 발생한 effect를 `SimultaneousTriggerGroup` 또는 동등한 모델로 묶는다.
- turn player / non-turn player effect를 원본 규칙에 따라 그룹화한다.
- 자동 순서와 사용자 선택 순서를 구분한다.
- 선택이 필요하면 `SelectionRequest`로 effect ordering을 요청한다.
- 한 effect resolution 완료 후 필요한 `AfterEffectsActivate` 후속 timing을 stack한다.
- once-per-turn 및 stale source를 재검증한다.
- queue drain 중 새 trigger가 생기는 경우 deterministic stack 규칙을 사용한다.

## 명시적 제한

이 작업에서 counter/cut-in과 attack target change 전체를 억지로 포함하지 않는다.
그 항목은 다음 queue에서 처리한다.

## 테스트

- 한 플레이어 다중 trigger
- 양 플레이어 동시 trigger
- ordering selection
- automatic order
- AfterEffectsActivate enqueue
- selection pending/resume
- once-per-turn
- source가 사라진 trigger 처리
- deterministic trace/hash
- 기존 ST1~ST3 trigger regression

## 문서

지원하는 priority 범위와 아직 미지원인 범위를 completion report에 직접 노출한다.
