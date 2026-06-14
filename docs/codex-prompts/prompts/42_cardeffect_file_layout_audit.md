# 42 - CardEffect 파일 구조 감사

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 구조 감사다. 코드 구현은 하지 마라. 단, 감사 결과 문서 작성은 허용한다. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

RL.Engine의 CardEffect 파일 구조가 원본 DCGO의 `CardEffect/{Set}/{Color}/{CardId}.cs` 구조를 최대한 따르는지 확인한다.

## 감사 대상

- DCGO/Assets/Scripts/CardEffect/ST1/**
- DCGO/Assets/Scripts/CardEffect/ST2/**
- DCGO/Assets/Scripts/CardEffect/ST3/**
- src/DCGO.RL.Engine/CardEffects/**
- docs/rl-engine/cardeffect-porting-status.md

## 수행할 것

1. 원본 ST1/ST2/ST3 CardEffect 파일 목록을 수집한다.
2. RL.Engine 대응 파일 목록을 수집한다.
3. 원본 파일이 있는 카드인데 RL.Engine 대응 파일이 없는 목록을 만든다.
4. 원본 파일이 없는 NoEffect 카드인데 RL.Engine 명시 marker/NoEffect 근거가 없는 목록을 만든다.
5. Catalog 파일들이 registry 외 책임을 갖는지 검사한다.
6. 카드별 effect body가 catalog/support 파일에 몰려 있는지 검사한다.
7. ST2/ST3도 카드별 파일 구조를 따르는지 검사한다.
8. helper가 특정 카드 body를 숨기고 있지는 않은지 검사한다.

## 문서 생성/갱신

`docs/rl-engine/cardeffect-file-layout-audit.md`를 생성 또는 갱신한다.

반드시 포함:

- 원본 파일 구조 요약
- RL.Engine 파일 구조 요약
- Set별 mapping 표
- Catalog 책임 검사 결과
- 구조 위반 목록
- 수정 필요 항목
- ST2/ST3 확장 가능성 판단

## 금지

- 코드 수정 금지
- 기존 DCGO Unity 원본 수정 금지
- 사용자 승인 없는 commit 금지

## 완료 보고

- 감사 결과 요약
- 구조 위반 수
- 즉시 수정 필요한 항목
- 다음 queue 항목
