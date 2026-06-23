# AS-IS Card Data Structure Summary

> GOAL 06 CardBaseEntity/ScriptableObject 데이터 구조 분석 요약이다. 카드 효과 구현, headless 필요 여부 최종 판정, 기존 구현 trust audit은 수행하지 않는다.

## 기준선

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`
- 입력 SourceOfTruth audit: `docs/generated/as-is-restart/asis-source-of-truth-audit-summary.json`
- GOAL 06 생성 시각 UTC: `2026-06-23T06:03:59.2983503+00:00`

## 요약

| 항목 | 값 |
| --- | --- |
| CardBaseEntity 전체 파일 수 | 17753 |
| .asset 수 | 8187 |
| 카드 asset 수 | 8186 |
| loader asset 수 | 1 |
| .meta sidecar 누락 asset 수 | 0 |
| field name 수 | 46 |
| CardID 누락 | 0 |
| CardIndex 누락 | 0 |
| CardIndex 중복 그룹 | 0 |
| CardID variant group 수 | 2384 |
| CardEffectClassName empty | 225 |
| NoEffect marker | 0 |
| effect source candidate 미발견 | 39 |
| parse warning | 0 |

## 카드 종류 폴더별

| Card kind folder | 수 |
| --- | --- |
| (missing kind folder) | 1 |
| DigiEgg | 497 |
| Digimon | 5927 |
| Option | 921 |
| Tamer | 840 |

## Effect class status

| Status | 수 |
| --- | --- |
| Empty | 225 |
| HasSourceCandidate | 7922 |
| NoSourceCandidate | 39 |

## 다음 GOAL 07 추천 입력

- GOAL 07에서는 이 산출물의 field summary, effect class status, CardKind/Level/Color/EvoCost 분포를 headless 필요 여부 matrix 입력으로 사용한다.
- CardBaseEntity asset 전문 텍스트는 복제하지 않았고, 긴 효과 설명은 길이/존재 여부만 기록했다.
- CardID 중복은 variant/parallel art 가능성이 있으므로 오류로 확정하지 않고 variant group 후보로 유지한다.
- CardEffectClassName source candidate 미존재 항목은 GOAL 07/08에서 NoEffect, external tool, generated registry와 교차 검토한다.
