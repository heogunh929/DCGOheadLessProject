# 39 - source-aligned 상태 checkpoint 준비

AGENTS.md 지침, LOCAL_GIT_GUIDE.md, common_constraints.md, 현재 /goal을 따르라.

이번 작업은 source-aligned 구조 정렬 결과를 checkpoint로 만들기 전 최종 점검이다. 사용자의 승인 없이 commit하지 마라.

## 목표

구조 감사, 구조 guard, 문서 정합성 정리 결과가 안전한지 확인하고, 사용자가 승인하면 로컬 checkpoint commit을 만들 수 있도록 준비한다.

## 실행

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO
- git status --short -- DCGO\Assets\Scripts
- git diff --name-only -- DCGO\Assets\Scripts

## 확인

1. DCGO 원본 변경 없음
2. remote 없음
3. 테스트/빌드 산출물 제외
4. 구조 guard 테스트 통과
5. 최신 문서 정합성 유지
6. ST1/ST2/ST3 상태표와 실제 파일/registry 일치
7. 학습용 API 미구현 유지

## 테스트

- 가능한 전체 테스트 실행
- MSBuild 경고와 테스트 실패를 구분해 보고

## 산출물

- docs/progress/CHATGPT_HANDOFF.md 갱신
- checkpoint 권고 보고

## commit 정책

- 이 프롬프트만으로는 commit하지 않는다.
- 완료 보고에서 추천 commit message만 제안한다.

추천 commit message:
chore: align card effect porting structure with original DCGO layout

## 완료 보고

- 테스트 결과
- git status 요약
- commit 가능 여부
- 추천 commit message
