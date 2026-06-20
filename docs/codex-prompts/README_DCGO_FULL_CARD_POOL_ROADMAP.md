# DCGO 전체 카드풀 이식 및 RL 학습 Roadmap

이 queue는 `headlessDCGO`의 최종 목표를 다음으로 고정한다.

> 고정된 `DCGO2/DCGO` 원본 snapshot의 전체 카드풀과 battle rule을 headless RL.Engine으로 이식하고,
> 그 환경에서 self-play 모델을 학습한다.

ST1~ST3는 엔진 구조와 공통 처리의 첫 vertical slice일 뿐이며 최종 학습 범위가 아니다.

## 현재 GitHub 기준점

이 패키지 작성 시점의 `heogunh929/DCGOheadLessProject` `main` 기준점:

```text
535163ad 202606201257 local
```

현재 완료 범위:

- 47~51 engine-parity 작업 완료
- production runtime composition root 보강
- shared service graph 검증
- option `Hand -> Executing -> OptionSkill -> Trash` 정렬
- asset/registry/source mapping validator
- ST1~ST3 카드별 파일 구조 및 registry/status 검증

현재 알려진 blocker:

- `ST3_02_P2.asset`는 `CardEffectClassName: ST3_02`이나 source body가 확인되지 않음
- 모든 trigger timing의 공통 pending selection resume boundary 미완성
- security timing, MultipleSkills priority, counter/block timing 미완성
- Unity/RL trace parity 미완성
- 전체 DCGO 카드풀 inventory/porting 미시작

## Queue 구조

```text
52     Core dependency injection 정리
52A    모든 effect timing의 공통 selection pause/resume boundary
53~58  Security/priority/attack/golden/trace parity
59A    ST1~ST3 engine-core milestone gate
60A    전체 카드풀 확장 readiness
61     DCGO 원본 snapshot 고정
62     전체 card asset manifest
63     전체 mechanic/effect timing inventory
64     전체 카드별/source-effect scaffold
65     전체 카드풀 validation baseline
66     실제 카드 포팅 batch queue 자동 생성
67     생성된 batch 완료 감사
68     전체 asset/source/registry 정합성 재검증
69     전체 카드풀 golden/parity coverage
70     Full DCGO Snapshot Completion Gate
71     Gate 결과에 따라 RL queue 또는 remediation queue 생성
```

## 적용

압축을 프로젝트 루트에 풀면 `docs/codex-prompts/` 아래에 파일이 추가 또는 갱신된다.

처음 한 번:

```text
/mention docs/codex-prompts/GOAL_DCGO_FULL_CARD_POOL.md
/mention docs/codex-prompts/ACTIVE/RUN_NEXT_DCGO_FULL_CARD_POOL.md
/mention docs/codex-prompts/state/QUEUE_DCGO_FULL_CARD_POOL.md
/mention docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md
```

그다음 `GOAL_DCGO_FULL_CARD_POOL.md` 안의 `/goal`을 실행한다.

이후:

```text
다음 dcgo-full queue 작업을 진행해.
```

## Commit 정책

Codex는 commit과 push를 하지 않는다.

작업 종료 시 아래만 보고한다.

- 변경 파일
- 테스트 결과
- git status/diff
- 추천 commit message

실제 commit은 사용자가 직접 수행한다.

## Full Card Porting Subqueue

66번이 완료되면 Codex가 현재 전체 카드 manifest와 mechanic inventory를 바탕으로 다음을 자동 생성한다.

```text
docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md
docs/codex-prompts/prompts/generated/full-card/*.md
```

그 뒤 사용자는 다음 한 줄만 반복한다.

```text
다음 full-card-porting batch 작업을 진행해.
```

동적 batch queue가 전부 완료된 후 다시 main queue 67번으로 돌아온다.

## RL Queue

Full DCGO Snapshot Completion Gate가 통과하기 전에는 RL queue를 실행하지 않는다.

71번은 다음 중 하나만 수행한다.

- gate 통과: RL queue를 생성하고 사용자 승인 대기
- gate 실패: missing-layer remediation queue 생성

사용자 승인 없는 RL 구현이나 학습은 금지한다.
