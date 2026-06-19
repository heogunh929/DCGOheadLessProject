# Engine Parity 공통 제약

모든 engine-parity prompt는 다음을 따른다.

## Source of Truth

- `DCGO/Assets/Scripts`의 Unity 원본을 먼저 읽는다.
- 기존 Unity 원본 파일은 수정하지 않는다.
- 원본과 다른 판단은 문서에 명시한다.
- 불확실한 규칙을 추측으로 구현하지 않는다.

## 구조

- 카드별 effect body는 카드별 파일에 둔다.
- Catalog는 registry 등록만 담당한다.
- 공통 helper는 candidate/query/primitive wrapper까지만 담당한다.
- 카드별 amount, timing, source mapping, branching은 카드 파일에서 확인 가능해야 한다.
- core service에 `ST1-`, `ST2-`, `ST3-` 같은 카드 ID 분기를 넣지 않는다.

## 상태 변경

- 직접 zone list를 수정하지 않는다.
- `ZoneMover`, primitive service, battle/effect service를 사용한다.
- base `CardDefinition` 값을 duration/continuous 효과로 직접 변경하지 않는다.
- unsupported는 silent no-op이 아니라 명시 예외/report로 유지한다.

## Git

작업 시작 전과 종료 후:

```text
git status --short
git diff --stat
git diff --name-only
git log --oneline -5
git remote -v
git status --short -- DCGO
git status --short -- DCGO\Assets\Scripts
git diff --name-only -- DCGO\Assets\Scripts
```

- remote가 있어도 fetch/pull/push는 사용자 승인 없이 실행하지 않는다.
- commit은 사용자 승인 없이 만들지 않는다.

## 테스트

- 코드 변경 시 가능한 전체 테스트를 실행한다.
- MSBuild cache/temp 경고와 test runner 실패를 구분한다.
- ST1 regression, ST1~ST3 target pool validation, replay determinism, invariant 검증을 유지한다.
- 테스트를 통과시키기 위해 원본 의미를 완화하지 않는다.

## 학습 단계 금지

whole-engine completion gate 통과와 사용자 승인 전까지 다음을 구현하지 않는다.

- ObservationEncoder
- RewardCalculator
- DatasetExporter
- Trainer
- RL Environment API
- 학습용 self-play dataset
