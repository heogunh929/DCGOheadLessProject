# 38 - 문서 최신 상태/과거 상태 정합성 정리

AGENTS.md 지침, LOCAL_GIT_GUIDE.md, common_constraints.md, 현재 /goal을 따르라.

이번 작업은 문서 정합성 정리다. 기능 구현을 하지 마라.

## 목표

업로드/문서에 섞여 있는 과거 ST1 실패 상태와 최신 GitHub main 상태를 분리한다. Codex가 과거 상태를 현재 상태로 오해하지 않게 한다.

## 정리 대상

- docs/rl-engine/cardeffect-porting-status.md
- docs/rl-engine/effect-system.md
- docs/rl-engine/engine-completion-checklist.md
- docs/rl-engine/engine-completion-report-st1.md
- docs/rl-engine/engine-completion-report-st1-st3.md
- docs/rl-engine/validation-strategy.md
- docs/rl-engine/golden-scenarios.md
- docs/rl-engine/decision-and-selection.md
- docs/progress/CHATGPT_HANDOFF.md
- 새로 만든 audit/policy 문서

## 수행할 것

1. 각 문서 최상단에 "최신 상태"를 명확히 적는다.
2. 과거 실패 기록은 "이전 단계 기록" 또는 "Historical notes"로 내린다.
3. 최신 상태와 과거 상태가 같은 섹션 안에서 충돌하지 않게 한다.
4. ST1 상태는 현재 GitHub main 기준으로 기록한다.
5. ST1~ST3 상태는 현재 GitHub main 기준으로 기록한다.
6. 학습 단계는 아직 별도 승인 전까지 진입하지 않는다고 기록한다.
7. "St1CardScriptCatalog가 effect body를 갖는다" 같은 과거 표현이 최신 구조와 다르면 수정한다.
8. `All 202 tests passed` 같은 최신 테스트 결과가 문서에 있다면 그 범위와 의미를 명확히 한다.

## 금지

- 코드 수정
- 기능 구현
- 원본 DCGO 수정
- commit

## 테스트

- 문서만 수정하면 테스트 생략 사유 보고
- 코드 수정이 발생하면 전체 테스트 실행

## 완료 보고

- 정리한 문서 목록
- 최신 상태 요약
- 과거 상태와 최신 상태 충돌 해소 여부
- 다음 queue 작업 추천
