AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 56 - Golden Scenarios Batch 1

## 목표

현재 단위 테스트만 존재하는 대표 효과를 실제 phase/action/selection/zone lifecycle을 포함하는 end-to-end 검증 scenario로 승격한다.

모든 scenario는 학습 데이터가 아니라 검증 데이터다.

## 구현할 scenario

1. `ST1 security tamer aura battle`
   - ST1-12가 security에서 play
   - 다음 owner turn aura 적용
   - 실제 DP battle 결과 변화
2. `ST1 dynamic SecurityAttack replay`
   - ST1-11 source 수 변화
   - 실제 security check 횟수 변화
3. `Blue source trash attack replay`
   - ST2-03/ST2-06/ST2-09
   - source 선택, bottom source trash, 후속 상태
4. `Blue bounce/security option replay`
   - ST2-16
   - top card hand, sources trash, security option lifecycle
5. `Yellow recovery/security hand replay`
   - ST3-09 recovery
   - ST3-13/ST3-14 checked card final Hand zone

## 요구사항

- 가능한 경우 public `GameAction`과 `SelectionResult`로 실행한다.
- test-only direct state mutation은 setup fixture에만 제한한다.
- action/selection trace를 저장한다.
- replay 가능한 scenario는 final hash를 비교한다.
- snapshot-only 부분은 이유를 명시한다.
- scenario별 rule-visible checkpoint를 기록한다.
- 해당 카드의 `Verified` 승격 기준을 정의하되, Unity trace 비교 전에는 함부로 Verified로 올리지 않는다.

## 테스트/문서

- golden suite에 5개 추가
- 기존 7개 scenario 유지
- deterministic seed
- invariant check
- replay report
- `golden-scenarios.md` 갱신
