AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 64 - 전체 Per-Card / Source-Effect Scaffold

## 목표

전체 asset manifest의 모든 identity와 전체 source effect inventory를 RL.Engine의 추적 가능한 파일/registry 구조로 만든다.
이 단계에서는 실제 효과를 추측 구현하지 않는다.

## 구조 정책

1. 모든 `AssetCardDefinitionId`는 mapping record를 가진다.
2. 모든 원본 CardEffect source class는 대응 RL 파일을 가진다.
3. 원본 source가 없는 명시 NoEffect는 marker 파일을 가진다.
4. source가 missing/unknown이면 `Unsupported` 또는 `NeedsSourceReview` marker다.
5. shared source effect:
   - source effect 구현은 공유 가능
   - 각 카드/variant의 wrapper/mapping은 유지
6. variant별 effect가 다르면 variant identity별 mapping을 분리한다.
7. Catalog는 registry만 담당한다.

## 경로

원본 set/color/asset/source 구조를 최대한 보존한다.
현재 프로젝트 naming policy와 충돌하면 mapping 문서와 구조 guard를 먼저 갱신한다.

## 생성 내용

- card/variant mapping files
- source-effect scaffold files
- set별 registry catalog
- status registry
- source mapping comments/records
- structure guard tests

## 초기 상태

실제 porting 완료 전 효과 보유 scaffold는 `Unsupported`여야 한다.
`NoEffect` 또는 `Implemented`로 자동 승격하지 않는다.

## 테스트

- manifest identity coverage
- source class coverage
- per-card mapping coverage
- catalog registry-only
- no direct zone mutation
- no duplicate identity flatten
- compile
