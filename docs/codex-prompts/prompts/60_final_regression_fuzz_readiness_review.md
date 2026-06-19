AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 60 - Final Regression, Fuzz, Readiness Review

## 목표

47~59 작업을 종합해 ST1~ST3 scope engine-core의 실제 readiness를 검수한다.

## 실행할 검증

- 전체 unit/regression
- structure guards
- asset mapping audit
- ST1 target deck gate
- ST1~ST3 target pool validation
- whole-engine completion gate v1
- golden scenario suite
- replay determinism
- parity fixtures, 존재하는 경우
- invariant fuzz multiple seeds
- random legal action runs with representative ST1/ST2/ST3 decks
- infinite loop/max iteration guard
- performance smoke
- forbidden dependency
- DCGO original modification check

## 판정

다음 중 하나로 결론 낸다.

- `ReadyForRlEnvironmentDesign`
- `BlockedByParity`
- `BlockedByRuleGaps`
- `BlockedByCardScope`
- `NeedsReview`

whole-engine gate가 통과하고 사용자가 승인하기 전에는 RL API를 구현하지 않는다.

## 문서

- 최종 readiness report
- 남은 gate/위험
- ST1~ST3 범위 의미
- 전체 DCGO 카드풀과의 차이
- UnityAdapter 선행 조건
- 다음 queue 제안

## queue 처리

결과가 complete가 아니면 다음 missing layer queue 초안을 생성하되 실제 구현은 하지 않는다.
결과가 complete여도 RL queue는 사용자 승인 전 생성만 하고 실행하지 않는다.

## 완료 보고

- 테스트 결과
- seed/fuzz 범위
- whole-engine gate 결과
- parity 결과
- readiness 판정
- 다음 권장 단계
- commit은 만들지 않았다는 확인
