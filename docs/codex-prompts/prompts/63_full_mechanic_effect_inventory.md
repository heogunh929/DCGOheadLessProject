AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 63 - 전체 Mechanic / Effect Inventory

## 목표

고정 snapshot에서 전체 카드풀이 사용하는 rule/effect capability를 machine-readable하게 추출하고 현재 RL.Engine 지원 상태와 매핑한다.

## 원본 조사 범위

- `Assets/Scripts/CardEffect/**/*.cs`
- `ICardEffect` 및 `EffectTiming`
- `CardEffectFactory`
- `CardEffectCommons`
- keyword effects
- complex mechanism
- selection effect classes
- battle/attack/security/turn state logic
- CardBaseEntity keyword/effect metadata

## 추출 항목

- EffectTiming 사용
- static/continuous/inherited/linked/security/counter/declarative/background
- optional/skippable
- max count/chain activation
- selection 종류와 root zone
- zone movement
- modifier/duration
- trigger priority
- replacement/cut-in
- special digivolution/play mechanics
- CardEffectFactory/Commons API 사용
- keyword
- 영향을 받는 card/variant 목록

## Engine mapping 상태

```text
Implemented
PartiallyImplemented
Unsupported
NotReferenced
NeedsSourceReview
Verified
```

## 출력

- `docs/generated/full-mechanic-inventory.json`
- `docs/generated/full-mechanic-matrix.md`
- `docs/generated/full-missing-layer-report.md`
- mechanic dependency graph

## 금지

- 단순 문자열 발견만으로 Implemented 판정
- ST1~ST3 테스트만으로 전체 capability 지원 판정
- 구현 코드 변경
