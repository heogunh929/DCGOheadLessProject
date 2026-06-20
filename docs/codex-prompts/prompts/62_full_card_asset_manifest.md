AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 62 - 전체 Card Asset Manifest

## 선행 조건

승인된 `dcgo-source-lock.json`이 있어야 한다.
lock과 현재 원본 fingerprint가 다르면 중단한다.

## 목표

고정 snapshot의 모든 gameplay card asset identity를 추출한다.
ST1~ST3 prefix로 제한하지 않는다.

## 최소 필드

- `DefinitionStableId`
- `CardId`
- `CardIndex`
- `VariantKey`
- asset name/path
- set id
- color
- card kind
- level
- play cost
- DP
- digivolution requirements
- traits
- keywords
- `CardEffectClassName`
- 대응 CardEffect source path
- source body 존재 여부
- duplicate CardId group
- import/read error

## 출력

- `docs/generated/full-card-pool-manifest.json`
- `docs/generated/full-card-pool-summary.md`
- `docs/generated/full-card-pool-anomalies.json`
- manifest content hash

## 검증

- 모든 `Assets/CardBaseEntity/**/*.asset` 처리
- CardIndex uniqueness
- variant identity 보존
- shared effect class 보존
- missing source body 명시
- parse 실패를 누락으로 숨기지 않음
- deterministic 재생성

## 금지

- effect 구현
- CardId 기준 variant 병합
- source 없는 effect를 NoEffect로 자동 판정
