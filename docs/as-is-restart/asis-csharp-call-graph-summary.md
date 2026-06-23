# AS-IS C# Call Graph Summary

> GOAL 05 syntax-only C# call graph 요약이다. 구현 변경이나 headless 필요 여부 판정은 포함하지 않는다.

## 기준선

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 분석한 `.cs` 파일 수: `7723`
- call edge 총수: `1378594`
- Exact/Probable/Heuristic/Unresolved: `162648` / `445797` / `711911` / `58238`
- Unity/GManager/Photon/UI/Coroutine 후보: `122493` / `38651` / `32878` / `98062` / `62387`
- SourceOfTruth caller 호출 수: `642654`

## Boundary별 호출 수

| Boundary | 호출 수 |
| --- | --- |
| NonSourceOfTruthCaller | 735940 |
| SourceOfTruthInternal | 447207 |
| SourceOfTruthToExternalPackage | 106311 |
| SourceOfTruthToOtherOrUnresolved | 70681 |
| SourceOfTruthToUnity | 18455 |

## Call kind별 수

| Call kind | 수 |
| --- | --- |
| methodInvocation | 297482 |
| propertyAccess | 272556 |
| fieldAccess | 240984 |
| typeReference | 217785 |
| enumReference | 107650 |
| identifierReference | 62190 |
| methodReference | 55235 |
| constructorCall | 47376 |
| staticMemberAccess | 23883 |
| coroutineYield | 20609 |
| coroutineCall | 17237 |
| memberAccessReference | 10985 |
| eventSubscription | 2977 |
| eventUnsubscription | 1045 |
| coroutineObjectCreation | 394 |
| eventReference | 141 |
| eventInvocationCandidate | 65 |

## 다음 Goal 06 추천 입력

- `docs/generated/as-is-restart/asis-csharp-call-graph.json`
- `docs/generated/as-is-restart/asis-csharp-call-edge-index.json`
- `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json`
- `docs/generated/as-is-restart/asis-csharp-call-graph-summary.json`
