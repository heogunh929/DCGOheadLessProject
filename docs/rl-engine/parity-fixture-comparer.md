# Parity Fixture Comparer

58번 단계는 Unity 원본에서 추출한 rule-visible trace fixture와 RL.Engine parity trace를 비교하는 deterministic comparer를 추가한 것이다. 현재 실제 Unity exporter fixture는 아직 없으므로 synthetic fixture만 테스트하며, 이 결과를 Unity/RL parity 통과로 주장하지 않는다.

## Fixture Location

권장 저장 위치:

| 종류 | 위치 | 비고 |
| --- | --- | --- |
| Unity exporter output | `docs/generated/parity-fixtures/unity/{scenario-id}.parity.json` | UnityAdapter 또는 Unity 실행 스크립트가 생성 |
| RL.Engine output | `docs/generated/parity-fixtures/rl/{scenario-id}.parity.json` | `ParityTraceExporter` 결과 |
| 비교 결과 JSON | `docs/generated/parity-fixtures/reports/{scenario-id}.comparison.json` | `ParityFixtureComparisonResultStore` 결과 |
| 비교 결과 Markdown | `docs/generated/parity-fixtures/reports/{scenario-id}.comparison.md` | `ParityFixtureMarkdownReportRenderer` 결과 |

파일 이름의 `{scenario-id}`는 trace 내부 `scenarioId`와 같은 안정 문자열을 사용한다. 로컬 절대 경로, 사용자 이름, Unity object pointer, 임시 파일명은 fixture payload에 넣지 않는다.

## Result Semantics

`ParityFixtureComparer` 결과 상태:

| Status | 의미 |
| --- | --- |
| `Passed` | schema가 맞고 모든 비교 대상 field가 일치한다. |
| `Failed` | schema, event alignment, timing, zone, permanent, modifier, decision/selection, trigger, memory/phase, hidden-info redaction 중 하나 이상이 다르다. |
| `NotRun` | Unity fixture file이 없다. missing fixture는 절대 pass로 처리하지 않는다. |

첫 mismatch는 `FirstMismatch`에 저장한다. 전체 diff는 category, step, path, Unity value, RL value, message를 가진 machine-readable list로 유지한다.

## Comparison Scope

비교 category:

- `Schema`: 문서/event schema version.
- `EventAlignment`: scenario id, seed, event count, step index, canonical state hash.
- `Timing`: event kind/label/outcome.
- `MemoryPhase`: phase, turn player, turn count, memory.
- `Zone`: player zone counts와 ordered public zone card identities.
- `Permanent`: permanent id, owner/controller, top/source/link, suspended, frame.
- `Modifier`: temporary modifier list.
- `DecisionSelection`: decision request와 submitted selection result.
- `Trigger`: trigger stable id/timing/source payload.
- `HiddenInformation`: Deck/DigiEggDeck/Hand/face-down Security card identity leak.

field ordering처럼 rule-visible 의미가 있는 값은 normalization하지 않는다. Unity object instance id처럼 non-semantic 값만 future normalization 대상으로 허용한다. 현재 DTO에는 그런 field가 없으므로 comparer는 typed DTO 값을 그대로 비교한다.

## Unity Exporter Draft

후속 Unity exporter는 다음 절차를 따른다.

1. Unity battle scenario를 deterministic seed와 decklist로 시작한다.
2. 각 action/selection/timing boundary에서 `dcgo.parity.trace.v1` DTO와 같은 rule-visible snapshot을 캡처한다.
3. hidden zone은 count만 기록한다. Deck/Hand/face-down Security card identity는 fixture에 넣지 않는다.
4. public zone, battle area, breeding area, source/link, temporary modifier, decision/selection, trigger, movement outcome을 stable id로 기록한다.
5. `docs/generated/parity-fixtures/unity/{scenario-id}.parity.json`에 저장한다.
6. 같은 scenario를 RL.Engine에서 실행해 `docs/generated/parity-fixtures/rl/{scenario-id}.parity.json`을 만든다.
7. `ParityFixtureComparer.CompareFixtureFile(...)`로 Unity fixture와 RL trace를 비교한다.
8. 결과가 `NotRun`이면 fixture 미생성 상태로 보고한다. `Failed`이면 첫 mismatch와 structured diff를 queue blocker로 다룬다.

이 단계에서는 Unity 원본 파일과 `DCGO/Assets/Scripts`를 수정하지 않았다.

