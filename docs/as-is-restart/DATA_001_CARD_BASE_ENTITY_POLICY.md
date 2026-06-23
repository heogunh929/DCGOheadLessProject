# DATA-001 CardBaseEntity / Variant / EffectClassName Policy

> DATA-001은 CardBaseEntity 데이터 정책을 닫기 위한 AS-IS 분석 산출물이다. 구현, CardEffect body 작성, Foundation Gate 수치 조작, DCGO 원본 수정은 수행하지 않았다.

## 기준선

- AS-IS root: `E:\headlessDCGO\DCGO`
- Source of Truth: `Assets/CardBaseEntity`
- 입력 카드 인덱스: `docs/generated/as-is-restart/asis-cardbaseentity-card-index.json`
- 입력 데이터 구조 분석: `docs/generated/as-is-restart/asis-card-data-structure.json`
- 입력 포팅 scope: `docs/generated/as-is-restart/asis-porting-scope-decision-summary.json`
- Foundation Gate: `OpenCodeReady=False`, selected=`ContinuousOrStaticEffect:PartiallyImplemented`

## 핵심 결론

1. `Assets/CardBaseEntity`의 카드 데이터는 `Port` 대상이다. CardData bucket은 8,187개이며, 카드 asset 8,186건과 loader asset 1건을 포함한다.
2. `CardID`는 unique key가 아니다. distinct CardID는 4,201개이고 duplicate CardID group은 2,384개다.
3. importer/runtime identity는 최소 `relativePath`, `metaGuid`, `cardIndex`, `cardId`, `variantSuffix`를 보존해야 한다. `CardID` 단독 dictionary는 금지한다.
4. duplicate CardID variant는 지금 단계에서 접지 않는다. 1,363개 duplicate group이 데이터 shape 차이를 보이며, 33개 group은 card kind folder도 다르다.
5. `CardEffectClassName`은 source mapping evidence로만 사용한다. source candidate가 있는 것은 body 구현 허가가 아니라 source class 매칭 근거다.
6. empty `CardEffectClassName` 225건은 `NoEffectClassDeclaredByCardData`로 보존한다. `NoEffect` marker는 0건이므로 empty를 generated Verified/Implemented로 승격하지 않는다.
7. source candidate가 없는 non-empty `CardEffectClassName`은 39 asset / 34 class이며, `NeedsSourceMapping`이다.

## CardBaseEntity 정책

| 항목 | 정책 |
| --- | --- |
| CardBaseEntity .asset | Port |
| CardBaseEntity .asset.meta | SourceMeta/identity evidence로 보존 |
| loader asset | 카드 정의가 아닌 데이터 로더/프로비넌스 evidence |
| 원본 수정 | 금지 |
| importer 우선순위 | asset-level record 보존 후 gameplay equivalence 검토 |

## Variant Identity 정책

| 항목 | 값 |
| --- | ---: |
| 카드 asset 수 | 8,186 |
| distinct CardID | 4,201 |
| duplicate CardID group | 2,384 |
| duplicate CardID asset | 6,369 |
| unique CardIndex | 8,186 |
| unique meta GUID | 8,186 |
| duplicate CardIndex group | 0 |

### Variant Group Size

| Group size | Count |
| --- | ---: |
| 1 | 1817 |
| 2 | 1475 |
| 3 | 521 |
| 4 | 200 |
| 5 | 106 |
| 6 | 56 |
| 7 | 21 |
| 8 | 3 |
| 10 | 1 |
| 9 | 1 |

### Duplicate Group Review Flags

| Flag | Count |
| --- | ---: |
| dataSignatureConflictGroups | 1363 |
| cardKindFolderConflictGroups | 33 |
| effectClassNameConflictGroups | 32 |
| effectClassStatusConflictGroups | 32 |
| colorSignatureConflictGroups | 2 |
| levelConflictGroups | 0 |
| playCostConflictGroups | 1 |
| evoCostConflictGroups | 15 |
| descriptionLengthConflictGroups | 1328 |

### Largest Duplicate Groups

| CardID | Asset count | Data signature count | Review flags |
| --- | ---: | ---: | --- |
| BT5-086 | 10 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT16-082 | 9 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT16-028 | 8 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT4-005 | 8 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId, CardKindDiffersWithinCardId |
| BT4-011 | 8 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT10-042 | 7 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT12-043 | 7 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT14-041 | 7 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT17-077 | 7 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT2-004 | 7 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId, CardKindDiffersWithinCardId |
| BT3-046 | 7 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT7-005 | 7 | 3 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId, CardKindDiffersWithinCardId |
| BT7-056 | 7 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT8-053 | 7 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |
| BT9-092 | 7 | 2 | DuplicateCardIdPreserveVariants, DataShapeDiffersWithinCardId |

## EffectClassName 정책

| 항목 | 수 |
| --- | ---: |
| Empty | 225 |
| HasSourceCandidate | 7922 |
| NoSourceCandidate | 39 |
| non-empty EffectClassName asset | 7961 |
| distinct non-empty EffectClassName | 3939 |
| distinct source-mapped EffectClassName | 3904 |
| distinct NeedsSourceMapping EffectClassName | 34 |
| ambiguous source candidate EffectClassName | 1 |
| NoEffect marker | 0 |

정책은 다음과 같이 닫는다.

- non-empty `CardEffectClassName` + source candidate 있음: `SourceMappedEffectClass`. 단, body 구현은 Foundation Gate와 card-porting 해제 이후로 deferred.
- non-empty `CardEffectClassName` + source candidate 없음: `NeedsSourceMapping`. NoEffect로 취급하지 않는다.
- empty `CardEffectClassName`: `NoEffectClassDeclaredByCardData`. generated status 승격 근거로 쓰지 않는다.
- candidate source path가 2개 이상이면 `AmbiguousSourceCandidate`이며, 구현 전에 별도 해소한다.

### NeedsSourceMapping 상위 항목

| EffectClassName | Asset count | CardID count | Sample |
| --- | ---: | ---: | --- |
| BT10_055 | 1 | 1 | Assets/CardBaseEntity/BT10/Green/Digimon/BT10_055_P0.asset |
| BT14_009 | 4 | 1 | Assets/CardBaseEntity/BT14/Red/Digimon/BT14_009_P1.asset |
| BT18_020 | 1 | 1 | Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_020.asset |
| BT25_003 | 1 | 1 | Assets/CardBaseEntity/BT25/Yellow/DigiEgg/BT25_003.asset |
| BT2_009 | 2 | 1 | Assets/CardBaseEntity/BT2/Red/Digimon/BT2_009_P1.asset |
| BT3_021 | 1 | 1 | Assets/CardBaseEntity/BT3/Blue/Digimon/BT3_021_P5.asset |
| BT3_061 | 1 | 1 | Assets/CardBaseEntity/BT3/Black/Digimon/BT3_061_P2.asset |
| BT3_062 | 1 | 1 | Assets/CardBaseEntity/BT3/Black/Digimon/BT3_062_P1.asset |
| BT3_077 | 1 | 1 | Assets/CardBaseEntity/BT3/Purple/Digimon/BT3_077_P2.asset |
| BT4_001 | 1 | 1 | Assets/CardBaseEntity/BT4/Red/DigiEgg/BT4_001_P0.asset |
| BT4_042 | 1 | 1 | Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_042_P2.asset |
| BT4_043 | 1 | 1 | Assets/CardBaseEntity/BT4/Yellow/Digimon/BT4_043_P0.asset |
| BT4_052 | 1 | 1 | Assets/CardBaseEntity/BT4/Green/Digimon/BT4_052_P0.asset |
| BT4_053 | 1 | 1 | Assets/CardBaseEntity/BT4/Green/Digimon/BT4_053_P0.asset |
| BT4_064 | 1 | 1 | Assets/CardBaseEntity/BT4/Black/Digimon/BT4_064_P0.asset |
| BT4_067 | 1 | 1 | Assets/CardBaseEntity/BT4/Black/Digimon/BT4_067_P0.asset |
| BT4_077 | 1 | 1 | Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_077_P0.asset |
| BT4_080 | 1 | 1 | Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_080_P0.asset |
| BT4_082 | 1 | 1 | Assets/CardBaseEntity/BT4/Purple/Digimon/BT4_082_P0.asset |
| BT5_033 | 2 | 1 | Assets/CardBaseEntity/BT5/Yellow/Digimon/BT5_033_P1.asset |

## 닫힌 정책으로 생기는 작업 큐

| ID | 분류 | 작업 |
| --- | --- | --- |
| DATA-001-A | CloseableFoundationTask | CardBaseEntity importer identity contract test 작성 |
| DATA-001-B | CloseableFoundationTask | EffectClassName source mapping guard 작성 |

## 테스트 후보

- CardBaseEntity importer preserves 8,186 card asset records and excludes the 1 loader asset from playable definitions.
- CardID grouping never overwrites variants; group BT5-086 preserves 10 asset records.
- CardIndex uniqueness and meta GUID uniqueness are asserted against current source inventory.
- Empty CardEffectClassName records remain NoEffectClassDeclaredByCardData and produce no generated CardEffect body.
- NoSourceCandidate CardEffectClassName records fail with NeedsSourceMapping instead of silently becoming NoEffect.
- OpenCodeReady=false prevents SourceMappedEffectClass from being treated as implementation permission.

## Blocked / Deferred

- CardEffect body 구현: `OpenCodeReady=false` 및 C0039 이후 card-porting 금지 때문에 deferred.
- variant collapse: source asset 보존 테스트 전에는 금지.
- generated status 승격: DATA-001 범위 밖이며 금지.

## 산출물

- `docs/as-is-restart/DATA_001_CARD_BASE_ENTITY_POLICY.md`
- `docs/as-is-restart/data-001-card-base-entity-policy-summary.md`
- `docs/generated/as-is-restart/data-001-card-base-entity-policy.json`
- `docs/generated/as-is-restart/data-001-card-base-entity-variant-index.json`

## 다음 Goal 추천

1. FND-003-A: 15개 ImplementableFoundationTask를 작은 구현 goal로 분해한다.
2. DATA-001-A/B: importer identity와 EffectClassName guard를 테스트 후보로 구체화한다.
3. PARITY-001: CardID 단독이 아니라 asset-level definition 기준으로 parity fixture key를 설계한다.
