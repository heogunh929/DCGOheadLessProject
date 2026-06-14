# 43 - CardEffect 파일 구조 guard 테스트

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 구조 guard 테스트를 추가/강화하는 작업이다. 새 카드 효과 구현은 하지 마라. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

앞으로 Codex가 카드 효과를 Catalog나 큰 통합 파일에 몰아넣지 못하도록 구조 검증 테스트를 추가/강화한다.

## 구현할 테스트

1. `Implemented` 또는 `NoEffect` 상태의 ST1/ST2/ST3 카드가 대응 파일을 갖는지 검사한다.
2. 대응 파일 경로는 `src/DCGO.RL.Engine/CardEffects/{Set}/{Color}/{CardId}.cs` 패턴을 따른다.
3. 원본 CardEffect 파일이 존재하는 카드의 RL 파일에는 source mapping 문자열이 있어야 한다.
4. 원본 CardEffect 파일이 없는 NoEffect 카드의 RL 파일에는 원본 파일 없음 근거가 있어야 한다.
5. Catalog 파일은 registry만 담당해야 한다.
6. Catalog 파일에 `SelectionContinuation`, `Resolve`, `CreateSelectionRequest`, primitive 호출 body가 있으면 실패한다. 단 catalog의 단순 생성/등록은 허용.
7. 카드별 파일에서 직접 zone list 수정 패턴을 금지한다.
8. 상태표와 registry status가 일치해야 한다.
9. 상태표와 실제 파일 존재가 일치해야 한다.

## 문서 갱신

- docs/rl-engine/cardeffect-file-layout-audit.md
- docs/rl-engine/validation-strategy.md
- docs/rl-engine/cardeffect-porting-status.md

## 금지

- 새 카드 효과 구현 금지
- ST2/ST3 구현 확장 금지
- 테스트를 우회하기 위한 과도한 whitelist 금지
- 기존 DCGO Unity 원본 수정 금지
- 학습용 RL 구성 구현 금지
- remote 추가/push/fetch/pull 금지
- 사용자 승인 없는 commit 금지

## 테스트

가능한 전체 테스트를 실행한다.

## 완료 보고

- 추가/수정한 테스트
- 테스트 결과
- 구조 guard가 잡는 위반 목록
- git status/diff 요약
- commit은 만들지 않았다는 확인
