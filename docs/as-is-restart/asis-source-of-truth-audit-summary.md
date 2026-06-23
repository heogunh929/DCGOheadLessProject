# AS-IS SourceOfTruth Audit Summary

> GOAL 03 SourceOfTruth 분류 검증과 누락 가능성 감사 요약이다. 이 문서는 role 승격, headless 필요 여부 판정, 구현 신뢰 감사, 코드 구현을 수행하지 않는다.

## 기준선

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`
- GOAL 03 생성 시각 UTC: `2026-06-23T05:54:12.1637796+00:00`
- 감사 범위: GOAL 01 inventory metadata and GOAL 02 role metadata only; no source body parsing, call graph analysis, implementation trust audit, or headless necessity judgment.

## 요약

| 항목 | 값 |
| --- | --- |
| SourceOfTruth 파일 수 | 26884 |
| SourceOfTruth 폴더 수 | 1805 |
| SourceOfTruth C# 파일 수 | 4354 |
| SourceOfTruth CardDataCandidate 수 | 8187 |
| SourceOfTruth ScriptableObjectCandidate 수 | 8187 |
| expected source scope 중 SourceOfTruth 미부여 | 2 |
| SourceOfTruth 파일 expected root 밖 | 0 |
| CardDataCandidate outside SourceOfTruth | 0 |
| ScriptableObjectCandidate outside SourceOfTruth | 67 |
| Project-owned Assets outside SourceOfTruth | 631 |
| Assets/Editor C# 후보 | 24 |
| SourceOfTruth meta coverage issue | 0 |

## 감사 finding

| Finding | 상태 | 수 | 설명 |
| --- | --- | --- | --- |
| expected-root-source-file-coverage | Review | 2 | Expected SourceOfTruth file scope entries without SourceOfTruth role. |
| source-file-outside-expected-root | Pass | 0 | Files assigned SourceOfTruth outside Assets/Scripts and Assets/CardBaseEntity. |
| card-data-outside-source | Pass | 0 | CardDataCandidate entries not assigned SourceOfTruth. |
| scriptable-object-outside-source | Review | 67 | ScriptableObjectCandidate entries outside SourceOfTruth. Most are expected visual/package/project resources; review before GOAL 06. |
| project-owned-assets-outside-source | Review | 631 | Assets entries classified as project-owned UnityProjectSource but not SourceOfTruth. |
| editor-tool-code-outside-source | Review | 24 | Assets/Editor C# tools outside SourceOfTruth that may explain card data provenance. |
| source-file-meta-pair-coverage | Pass | 0 | SourceOfTruth non-meta files missing Unity .meta sidecar. |
| source-meta-orphan-coverage | Pass | 0 | SourceOfTruth .meta files whose base file/folder is absent. |
| source-folder-meta-pair-coverage | Pass | 0 | SourceOfTruth folders missing Unity .meta sidecar. |

## 다음 GOAL 06 추천 입력

- GOAL 06은 SourceOfTruth인 Assets/CardBaseEntity/*.asset 전체와 해당 .meta sidecar를 우선 입력으로 사용한다.
- Assets/Editor의 카드 데이터 import/fixup 도구 24개는 CardBaseEntity provenance 보조 후보로 별도 목록화하되, battle runtime SourceOfTruth로 즉시 승격하지 않는다.
- ScriptableObjectCandidate outside SourceOfTruth 67개는 대부분 visual/package/project resource로 보이며, GOAL 06에서는 CardBaseEntity 구조 분석과 분리한다.
- Assets/Scripts.meta 및 Assets/CardBaseEntity.meta는 source root sidecar boundary 후보로 남긴다. 파일/폴더 내부 coverage에는 누락이 없다.
