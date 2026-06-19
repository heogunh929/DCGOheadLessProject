AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 48 - 원본 Asset Effect Mapping 정합성 수정

## 목표

현재 source-alignment risk로 남아 있는 아래 항목을 원본 asset과 CardEffect 파일 기준으로 해결한다.

- `ST2-07`
- `ST3-07`
- `ST3-02` 및 variant asset

## 참조 원본

- `DCGO/Assets/Resources/Cards/**` 또는 실제 카드 asset 위치
- `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs`
- `DCGO/Assets/Scripts/CardEffect/ST2/**`
- `DCGO/Assets/Scripts/CardEffect/ST3/**`
- `CEntity_Base.CardID`
- `CEntity_Base.CardIndex`
- `CEntity_Base.CardEffectClassName`
- 카드 database/loader

## 수행할 것

1. 동일 CardId에 여러 asset variant가 있는지 조사한다.
2. 각 variant별 `CardIndex`, card name, color, effect class를 표로 기록한다.
3. `ST2-07`, `ST3-07`이 shared `ST1_06`을 참조하는 것이 맞다면:
   - 카드별 파일 `ST2_07.cs`, `ST3_07.cs`는 유지한다.
   - 각 카드 script가 원본 shared effect 의미를 재사용하도록 연결한다.
   - Catalog에 body를 넣지 않는다.
   - shared helper는 card ID에 의존하지 않는다.
4. `ST3-02`의 variant가 서로 다른 effect class를 가진다면 CardId 하나로 평탄화하지 않는다.
   - `CardDefinition` identity가 CardId만으로 부족한지 검토한다.
   - 필요하면 `CardDefinitionId`/`CardIndex`/variant key를 generic model로 추가한다.
   - variant-aware registry lookup을 설계한다.
5. source mapping 주석과 status 문서를 갱신한다.
6. 기존 잘못된 NoEffect 판정이 있다면 수정한다.
7. 원본 의미가 불확실하면 Implemented로 올리지 말고 `needs-review`로 멈춘다.

## 테스트

- ST2-07/ST3-07 shared effect mapping
- 카드별 파일 존재
- Catalog registry-only
- ST3-02 variant lookup
- 동일 CardId variant 간 definition identity 충돌 방지
- ST1~ST3 target validation
- 전체 regression

## 문서

- `cardeffect-porting-status.md`
- `cardeffect-file-layout-audit.md`
- `porting-structure-audit.md`
- variant mapping 문서가 필요하면 `card-definition-variant-mapping.md`
