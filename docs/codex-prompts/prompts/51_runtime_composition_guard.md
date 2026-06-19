AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 51 - Runtime Composition Guard

## 목표

실전 runtime에서 `TriggerPipelineService` 또는 필수 engine service가 누락되어 trigger hook이 조용히 건너뛰는 구성을 금지한다.

## 감사 대상

- `PlayCardService`
- `DigivolveService`
- `AttackService`
- `PhaseRunner`
- `RuleProcessor`
- `SecurityCheckService`
- `ActionExecutor`
- `TurnRunner`
- validation/runtime factory

## 수행할 것

1. nullable/optional trigger pipeline 주입을 찾는다.
2. production 경로에는 필수 non-null dependency를 요구한다.
3. 명시적인 composition root를 만든다.
   - 예: `BattleEngineRuntime`, `BattleEngineServices`, `BattleEngineFactory`
4. 모든 production runner는 composition root를 통해 생성한다.
5. test fixture도 기본적으로 동일한 composition root를 사용한다.
6. trigger 없는 특수 테스트가 필요하면 명시적인 test-only builder를 사용하고, 실전 API로 노출하지 않는다.
7. pipeline 미주입 상태에서 silent skip하지 말고 즉시 명시 실패한다.
8. service graph validation report를 제공한다.

## 테스트

- complete composition succeeds
- missing TriggerPipeline fails
- missing ZoneMover/primitive/invariant service fails
- standard TurnRunner uses complete graph
- play/digivolve/attack/phase/rules timing hook 실행
- no forbidden Unity dependency
- existing regression 유지

## 문서

runtime composition 구조와 필수 dependency 목록을 문서화한다.
