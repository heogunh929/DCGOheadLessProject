# GOAL 06 AS-IS Card Data Structure

> 원본 `Assets/CardBaseEntity` ScriptableObject asset 구조를 전수 분석한 기준선이다. 카드 효과 body 구현, headless 필요 여부 최종 판정, 기존 headless 구현 trust audit은 수행하지 않는다.

## 입력 기준

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 분석 root: `Assets/CardBaseEntity`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`
- 입력 SourceOfTruth audit: `docs/generated/as-is-restart/asis-source-of-truth-audit-summary.json`
- 입력 C# file index: `docs/generated/as-is-restart/asis-csharp-file-index.json`
- GOAL 06 생성 시각 UTC: `2026-06-23T06:03:59.2983503+00:00`
- 긴 효과 설명 전문 복제: `false`
- CardEffect body 분석/구현: `false`
- headless 필요 여부 최종 판정: `false`

## 전체 요약

| 항목 | 값 |
| --- | --- |
| CardBaseEntity 전체 파일 수 | 17753 |
| .asset 수 | 8187 |
| 카드 asset 수 | 8186 |
| loader asset 수 | 1 |
| .meta sidecar 보유 asset 수 | 8187 |
| .meta sidecar 누락 asset 수 | 0 |
| Meta GUID 중복 그룹 | 0 |
| field name 수 | 46 |
| CardID 보유/누락 | 8186/0 |
| CardIndex 보유/누락 | 8186/0 |
| CardIndex 중복 그룹 | 0 |
| CardID variant group 수 | 2384 |
| CardEffectClassName empty | 225 |
| NoEffect marker | 0 |
| effect source candidate 미발견 | 39 |
| parse warning | 0 |

## Set별 카드 수

| Set | 수 |
| --- | --- |
| AD1 | 45 |
| BT1 | 214 |
| BT10 | 237 |
| BT11 | 219 |
| BT12 | 272 |
| BT13 | 246 |
| BT14 | 223 |
| BT15 | 205 |
| BT16 | 222 |
| BT17 | 229 |
| BT18 | 152 |
| BT19 | 155 |
| BT2 | 229 |
| BT20 | 181 |
| BT21 | 181 |
| BT22 | 154 |
| BT23 | 132 |
| BT24 | 137 |
| BT25 | 131 |
| BT3 | 217 |
| BT4 | 250 |
| BT5 | 263 |
| BT6 | 252 |
| BT7 | 280 |
| BT8 | 272 |
| BT9 | 233 |
| EX1 | 136 |
| EX10 | 138 |
| EX11 | 149 |
| EX2 | 145 |
| EX3 | 110 |
| EX4 | 127 |
| EX5 | 118 |
| EX6 | 119 |
| EX7 | 163 |
| EX8 | 161 |
| EX9 | 148 |
| LM | 91 |
| P | 559 |
| RB1 | 50 |
| ST1 | 52 |
| ST10 | 28 |
| ST12 | 22 |
| ST13 | 19 |
| ST14 | 29 |
| ST15 | 33 |
| ST16 | 34 |
| ST17 | 37 |
| ST18 | 22 |
| ST19 | 19 |
| ST2 | 36 |
| ST20 | 28 |
| ST21 | 28 |
| ST22 | 19 |
| ST23 | 15 |
| ST24 | 17 |
| ST3 | 39 |
| ST4 | 27 |
| ST5 | 22 |
| ST6 | 27 |
| ST7 | 31 |
| ST8 | 21 |
| ST9 | 36 |

## Color folder별 카드 수

| Color folder | 수 |
| --- | --- |
| Black | 1288 |
| Blue | 1249 |
| Green | 1240 |
| Purple | 1340 |
| Red | 1430 |
| White | 408 |
| Yellow | 1231 |

## Card kind folder별 카드 수

| Card kind folder | 수 |
| --- | --- |
| (missing kind folder) | 1 |
| DigiEgg | 497 |
| Digimon | 5927 |
| Option | 921 |
| Tamer | 840 |

## EvoCost 개수 분포

| EvoCost count | 카드 수 |
| --- | --- |
| 0 | 2445 |
| 1 | 3855 |
| 2 | 1796 |
| 3 | 46 |
| 4 | 15 |
| 6 | 4 |
| 7 | 25 |

## Effect class status

| Status | 카드 수 |
| --- | --- |
| Empty | 225 |
| HasSourceCandidate | 7922 |
| NoSourceCandidate | 39 |

## Field summary

| Field | present | non-empty | kind |
| --- | --- | --- | --- |
| m_CorrespondingSourceObject | 8187 | 8187 | scalar |
| m_EditorClassIdentifier | 8187 | 0 | emptyScalar |
| m_EditorHideFlags | 8187 | 8187 | scalar |
| m_Enabled | 8187 | 8187 | scalar |
| m_GameObject | 8187 | 8187 | scalar |
| m_Name | 8187 | 8187 | scalar |
| m_ObjectHideFlags | 8187 | 8187 | scalar |
| m_PrefabAsset | 8187 | 8187 | scalar |
| m_PrefabInstance | 8187 | 8187 | scalar |
| m_Script | 8187 | 8187 | scalar |
| Attribute_ENG | 8186 | 7322 | emptyList,objectList |
| Attribute_JPN | 8186 | 2812 | emptyList,objectList |
| CardEffectClassName | 8186 | 7961 | emptyScalar,scalar |
| CardID | 8186 | 8186 | scalar |
| CardIndex | 8186 | 8186 | scalar |
| CardName_ENG | 8186 | 8186 | multilineScalar,scalar |
| CardName_JPN | 8186 | 3609 | emptyScalar,scalar |
| CardSpriteName | 8186 | 8186 | scalar |
| DP | 8186 | 8186 | scalar |
| EffectDiscription_ENG | 8186 | 7489 | emptyScalar,multilineScalar,scalar |
| EffectDiscription_JPN | 8186 | 2617 | emptyScalar,scalar |
| EvoCosts | 8186 | 5741 | emptyList,objectList |
| Form_ENG | 8186 | 7530 | emptyList,objectList |
| Form_JPN | 8186 | 2999 | emptyList,objectList |
| InheritedEffectDiscription_ENG | 8186 | 5848 | emptyScalar,multilineScalar,scalar |
| InheritedEffectDiscription_JPN | 8186 | 1467 | emptyScalar,scalar |
| Level | 8186 | 8186 | scalar |
| LinkDP | 8186 | 8186 | scalar |
| LinkEffect | 8186 | 2582 | emptyScalar,multilineScalar,scalar |
| LinkRequirement | 8186 | 2583 | emptyScalar,scalar |
| MaxCountInDeck | 8186 | 8186 | scalar |
| OptionCardColorRequirements | 8186 | 1383 | emptyScalar,scalar |
| OptionEffect | 8186 | 1383 | emptyScalar,multilineScalar,scalar |
| OverflowMemory | 8186 | 8186 | scalar |
| PlayCost | 8186 | 8186 | scalar |
| SecurityEffectDiscription_ENG | 8186 | 5212 | emptyScalar,multilineScalar,scalar |
| SecurityEffectDiscription_JPN | 8186 | 628 | emptyScalar,scalar |
| Type_ENG | 8186 | 7594 | emptyList,objectList |
| Type_JPN | 8186 | 3051 | emptyList,objectList |
| cardColors | 8186 | 8186 | scalar |
| cardKind | 8186 | 8186 | scalar |
| dualEffect | 8186 | 1383 | emptyScalar,scalar |
| rarity | 8186 | 8186 | scalar |
| prevCardIndex | 1 | 1 | scalar |
| promoCardIndex | 1 | 1 | scalar |
| setCardIndex | 1 | 1 | scalar |

## 주요 후보

`CardEffectClassName`은 있으나 `Assets/Scripts/CardEffect/**/{ClassName}.cs` 후보를 찾지 못한 카드 수: `39`
`NoEffect` marker 수: `0`
CardID variant group 수: `2384`

## 다음 GOAL 07 추천 입력

- GOAL 07에서는 이 산출물의 field summary, effect class status, CardKind/Level/Color/EvoCost 분포를 headless 필요 여부 matrix 입력으로 사용한다.
- CardBaseEntity asset 전문 텍스트는 복제하지 않았고, 긴 효과 설명은 길이/존재 여부만 기록했다.
- CardID 중복은 variant/parallel art 가능성이 있으므로 오류로 확정하지 않고 variant group 후보로 유지한다.
- CardEffectClassName source candidate 미존재 항목은 GOAL 07/08에서 NoEffect, external tool, generated registry와 교차 검토한다.

## 금지 범위 준수

- `src/` 아래 C# 코드는 수정하지 않았다.
- 원본 `DCGO/Assets`는 수정하지 않았다.
- CardEffect body 구현은 수행하지 않았다.
- C0039 이후 card-porting은 수행하지 않았다.
- headless 필요 여부 최종 판정은 수행하지 않았다.
- 기존 headless 구현 trust audit은 수행하지 않았다.
- commit/push는 수행하지 않았다.
