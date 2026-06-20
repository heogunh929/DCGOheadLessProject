# Engine Parity 공통 제약 - Full Card Pool Goal

모든 engine-parity prompt는 `dcgo_full_card_pool_constraints.md`를 함께 따른다.

## 현재 단계의 의미

- 52~60은 ST1~ST3를 이용해 공통 battle/effect core를 검증하는 단계다.
- 이 단계의 통과는 전체 DCGO 카드풀 completion이나 RL 학습 승인을 뜻하지 않는다.
- 60A 이후 원본 snapshot 고정과 전체 카드풀 inventory/porting으로 이동한다.

## Source / Structure / State

- `DCGO/Assets` 원본은 읽기 전용이다.
- 카드별 body는 카드별 파일에 둔다.
- Catalog는 registry만 담당한다.
- 공통 helper가 카드별 의미를 숨기지 않는다.
- 직접 zone list 수정과 카드 ID shortcut을 금지한다.
- unsupported 및 source 불일치를 silent no-op으로 처리하지 않는다.

## Selection / Runtime

- production runtime은 완전한 `BattleEngineServices` graph를 사용한다.
- service 누락과 mixed graph를 금지한다.
- 모든 effect timing의 pending decision은 공통 pause/resume 경계를 사용한다.
- runner는 pending selection을 무시하지 않는다.

## Git / Report

- Codex는 commit 또는 push하지 않는다.
- 사용자 승인 없이 fetch/pull하지 않는다.
- 종료 시 git status/diff, 테스트 결과, 변경 파일, 추천 commit message를 보고한다.

## RL 금지

Full DCGO Snapshot Completion Gate와 사용자 승인 전에는 RL API와 학습 코드를 구현하지 않는다.
