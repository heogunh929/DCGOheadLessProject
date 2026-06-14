# 46 - Golden scenario gap 계획

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 golden scenario gap 분석과 계획 수립이다. 구현은 최소화하고 문서화를 우선한다. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

현재 unit/regression 중심 검증에서 원본 Unity trace와 비교 가능한 golden scenario 검증으로 확장하기 위한 gap 계획을 만든다.

## 수행할 것

1. 현재 golden scenario 목록 확인.
2. ST1 카드별 주요 효과 중 golden scenario가 없는 항목 목록 작성.
3. ST2/ST3 구현 카드 중 golden scenario 후보 작성.
4. replay 가능한 scenario와 snapshot-only scenario 구분.
5. Unity 원본 trace가 필요한 scenario와 headless scripted scenario로 충분한 것을 구분.
6. 우선순위 1차 golden scenario 5개 이하를 제안.
7. 구현은 하지 말고 plan만 만든다. 단, 문서 test가 필요하면 최소 수정 허용.

## 문서 갱신

- docs/rl-engine/golden-scenarios.md
- docs/rl-engine/validation-strategy.md
- docs/rl-engine/current-github-local-state.md

## 금지

- RL 학습 데이터 생성 금지
- Unity trace 자동화 구현 금지
- 기존 DCGO 원본 수정 금지
- remote 추가/push/fetch/pull 금지
- 사용자 승인 없는 commit 금지

## 완료 보고

- golden scenario gap
- 우선순위 후보
- 구현 필요 여부
- 다음 queue 항목
