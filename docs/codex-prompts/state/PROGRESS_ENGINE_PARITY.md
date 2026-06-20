# Engine Parity Progress

| Date | Queue | Result | Baseline commit | Tests | Remaining risk |
| --- | ---: | --- | --- | --- | --- |
| 2026-06-18 | start | GitHub main `a101acd2` 기준 engine-parity queue 생성 | `a101acd2` | latest recorded `All 214 tests passed` | whole-engine completion 미실행 |
| 2026-06-18 | 47 | engine-parity queue 정렬, README/INDEX 갱신, github-current 47 superseded 처리 | `a101acd2` | 문서/queue 갱신만 수행 | 48번 asset effect mapping reconcile 대기 |
| 2026-06-19 | 48 | `ST2-07`/`ST3-07` shared `ST1_06` mapping을 card-id 기반 `Implemented` script로 정리하고 `ST3-02` variant를 문서화 | `8e4739f9` | `All 216 tests passed` | `ST3_02_P2.asset` source effect body/variant identity needs-review |
| 2026-06-19 | 49 | asset registry mapping validator를 추가해 원본 asset, registry, status snapshot, 카드별 파일, source body 존재를 대조 | `69529bc1` + local changes | `All 225 tests passed` | `ST3-02` base/P1은 NoEffect 후보, P2는 source body 미확인 needs-review 유지 |
| 2026-06-19 | 50 | Option hand play lifecycle을 원본 `UseOptionClass` 기준 `Hand -> Executing -> OptionSkill -> Trash`로 정렬 | `15665a95` + local changes | `All 234 tests passed` | `ST3-02` P2 source body 미확인 needs-review/blocking finding 유지, whole-engine gate 미실행 |

## Queue 51 Runtime Composition Guard - 2026-06-19

- 결과: `BattleEngineServices` runtime composition root를 추가하고, production `ActionExecutor`/`TurnRunner`가 `TriggerPipelineService` 없는 graph를 만들지 않도록 기본 조립 경로를 변경했다.
- `PlayCardService`는 `TriggerPipelineService`를 필수 생성자 인자로 받으며, option trigger pipeline이 없을 때 silent skip하던 nullable 경로를 제거했다.
- `ActionExecutor.Execute`는 `ActionExecutionResult`를 반환한다. hand option pending selection은 `SelectionRequest`와 `DecisionPoint`로 상위 boundary에 반환하고, selection 완료 전에는 rules timing을 실행하지 않는다.
- 후속 범위: `OnPlay`, `WhenDigivolving`, attack/phase/rules timing에서 발생하는 pending selection continuation 일반화는 queue 51 범위를 넘으므로 `docs/rl-engine/runtime-composition.md`의 후속 queue 항목으로 문서화했다.
- 테스트: `.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj` -> `All 238 tests passed.` MSBuild temp/cache write warning은 있었지만 test runner는 성공 종료했다.
- 남은 위험: `ST3-02` P2 source body 미확인 finding은 계속 needs-review/blocking이며 whole-engine completion gate에서 숨기지 않는다.
