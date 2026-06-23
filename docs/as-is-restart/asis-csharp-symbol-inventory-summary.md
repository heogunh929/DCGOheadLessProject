# AS-IS C# Symbol Inventory Summary

> GOAL 01/02 JSON 기반 syntax-only C# symbol inventory 요약이다. 코드 의미, 함수 호출, call graph 분석은 수행하지 않았다.

## 기준선

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 전체 `.cs` 파일 수: `7723`
- SourceOfTruth `.cs` 파일 수: `4354`
- 전체 type 수: `10514`
- method/property/field/enum member 수: `38948` / `9567` / `21369` / `3411`
- parsing 실패 수: `0`

## Type kind별 수

| Type kind | 수 |
| --- | --- |
| class | 8714 |
| enum | 677 |
| struct | 649 |
| interface | 370 |
| delegate | 104 |

## Symbol kind별 수

| Symbol kind | 수 |
| --- | --- |
| method | 38948 |
| attribute | 24804 |
| field | 21369 |
| type | 10514 |
| property | 9567 |
| namespace | 5856 |
| enumMember | 3411 |
| constructor | 2598 |
| event | 80 |

## 파일 primaryRole별 수

| PrimaryRole | 파일 수 |
| --- | --- |
| SourceOfTruth | 4354 |
| GeneratedCache | 2958 |
| ExternalPackage | 229 |
| VisualOnly | 158 |
| UnityProjectSource | 24 |

## 다음 Goal 05 입력

- `docs/generated/as-is-restart/asis-csharp-file-index.json`
- `docs/generated/as-is-restart/asis-csharp-symbol-index.json`
- `docs/generated/as-is-restart/asis-csharp-symbol-summary.json`
