# DCGO Full Card Pool 공통 제약

## 최종 범위

- 최종 대상은 ST1~ST3가 아니라 사용자 승인으로 고정된 `DCGO2/DCGO` commit의 전체 카드풀이다.
- ST1~ST3 관련 gate는 engine-core 중간 milestone이다.
- 전체 카드풀 gate 통과 전 RL 단계 진입을 금지한다.

## Source of Truth

- `DCGO/Assets` 원본을 먼저 읽는다.
- Unity 원본 파일을 수정하지 않는다.
- 원본 commit SHA가 고정되지 않은 상태에서 전체 카드풀 완료를 주장하지 않는다.
- 원본 asset과 source가 충돌하면 추측하지 않고 blocker로 남긴다.
- upstream 자체 결함을 제외하려면 사용자 명시 승인이 필요하다.

## Identity

- `CardId`는 공개 카드 번호이며 asset identity 전체를 대체하지 않는다.
- variant가 존재하면 `CardIndex`, asset path, `VariantKey`를 보존한다.
- 동일 CardId의 variant를 조용히 하나로 병합하지 않는다.
- shared `CardEffectClassName`과 registry lookup alias를 구분한다.

## 구조

- 카드별 effect body는 원본 CardEffect 경로에 대응되는 파일에 둔다.
- 모든 asset identity는 per-card mapping record를 가진다.
- shared source effect를 여러 카드가 참조해도 카드별 wrapper/mapping은 유지한다.
- Catalog는 registry만 담당한다.
- support helper는 candidate/query/primitive wrapper까지만 담당한다.
- 카드별 amount, timing, 조건, branching, source mapping은 카드 파일에서 확인 가능해야 한다.
- core service에 특정 CardId 또는 set 전용 분기를 넣지 않는다.

## 상태 변경

- 직접 zone list를 수정하지 않는다.
- `ZoneMover`, primitive, battle/effect service를 사용한다.
- continuous/duration 효과로 base CardDefinition을 직접 변경하지 않는다.
- unsupported는 예외와 report에 명시한다.
- pending selection을 상태 변경 후 예외로만 끝내지 않는다. 공통 pause/resume boundary를 사용한다.

## Git

작업 시작 전과 종료 후:

```text
git status --short
git diff --stat
git diff --name-only
git log --oneline -5
git remote -v
git status --short -- DCGO
git status --short -- DCGO\Assets
git status --short -- DCGO\Assets\Scripts
git diff --name-only -- DCGO\Assets\Scripts
```

- Codex는 commit하지 않는다.
- Codex는 push하지 않는다.
- fetch/pull도 사용자 명시 승인 없이 실행하지 않는다.
- 작업 종료 시 추천 commit message만 보고한다.

## 테스트

- 코드 변경 시 관련 subset과 가능한 전체 테스트를 실행한다.
- MSBuild 경고와 test runner 실패를 구분한다.
- replay determinism, invariant, source mapping, structure guard를 유지한다.
- 테스트 통과를 위해 원본 의미를 완화하지 않는다.
- GitHub CI가 없다면 로컬 테스트 결과임을 명시한다.

## RL 단계 금지

다음 조건 전에는 RL 구성요소를 구현하지 않는다.

1. 사용자 승인된 DCGO source lock 존재
2. 전체 asset manifest 완료
3. 전체 mechanic inventory 완료
4. 전체 카드별 mapping 및 porting 완료
5. full-card-pool validator에 blocking finding 없음
6. Full DCGO Snapshot Completion Gate 통과
7. 사용자 명시 승인

금지 항목:

- ObservationEncoder
- RewardCalculator
- DatasetExporter
- Trainer
- RL Environment API
- 학습용 self-play dataset
