AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 59 - Whole-Engine Completion Gate v1

## 목표

ST1~ST3 카드풀과 공통 battle/effect core를 대상으로 한 source-aligned whole-engine completion request를 새로 정의한다.

기존 ST1 target deck gate나 48장 registry validation을 재사용해 전체 엔진 통과라고 주장하지 않는다.

## Gate 항목

최소한 다음 gate를 포함한다.

1. forbidden dependency
2. original asset/effect mapping
3. per-card file/source mapping
4. no Unsupported/Partial in ST1~ST3 target scope
5. option Executing lifecycle
6. runtime composition completeness
7. ZoneMover/dependency consistency
8. security timing parity
9. MultipleSkills/AfterEffects priority support 범위
10. counter/block/target timing support 범위
11. golden scenario batch 1
12. replay determinism
13. invariant fuzz
14. parity trace schema/export
15. Unity fixture parity
16. silent skip/unsupported zero
17. performance smoke
18. RL training guard

## 판정 정책

- Unity fixture가 없으면 parity gate는 `NotRun` 또는 fail이며 whole-engine complete가 아니다.
- partial timing이 남으면 해당 범위를 명시하고 complete를 false로 둔다.
- request scope를 보고서에 명시한다.
- ST1~ST3 scope와 전체 DCGO card pool scope를 구분한다.
- gate 실패는 test process crash가 아니라 structured report로 남긴다.
- 코드 결함 자체를 검증하는 unit test는 정상적으로 실패해야 한다.

## 산출물

- request type
- runner
- report
- markdown/JSON output
- validation report template 갱신
- 학습 진입 가능 여부

## 테스트

- all-pass fixture
- each critical gate failure
- NotRun parity
- request scope isolation
- ST1-only pass를 whole-engine pass로 재사용하지 않음
- deterministic report
