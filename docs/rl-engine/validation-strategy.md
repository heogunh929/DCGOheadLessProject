# 검증 전략

## Queue 54B 검증 - 2026-06-21

추가/유지된 검증 항목:

- empty `RulesTiming` batch는 `AfterEffectsActivate`를 실행하지 않는다.
- stale source/`CanActivate` 실패만 있었던 batch도 `AfterEffectsActivate`를 예약하지 않는다.
- ordering -> optional yes/no, ordering -> target selection, same-source multi descriptor ordering을 분리 검증한다.
- 모두 optional인 ordering group은 전체 skip 가능, optional/non-optional 혼합 group은 전체 skip 불가로 검증한다.
- DP 0 삭제가 `RuleProcessor.StabilizeStateOnly`에서 event로 보존되고 `OnDestroyedAnyone` nested frame이 parent tail보다 먼저 drain된다.
- nested event frame의 selection pause/resume이 parent tail과 섞이지 않는다.
- `AfterEffectsActivate` A -> B 정상 연쇄와 동일 candidate self-loop guard를 각각 검증한다.
- `RequireSameRole` source role policy와 `AllowTriggeredSourceMove` policy를 분리 검증한다.
- ordering -> optional -> target replay/hash determinism을 검증한다.

실행 결과:

```powershell
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 337 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 54A 검증 - 2026-06-21

추가/유지된 검증 항목:

- batch A/B가 모두 처리된 뒤 `AfterEffectsActivate` C가 1회만 실행된다.
- 같은 source card의 두 descriptor도 ordering decision 대상이다.
- turn player group 전체가 non-turn player group보다 먼저 drain된다.
- effect 1개가 DP 0 상태를 만들면 outer tail의 다음 effect 전에 state-only RuleProcessor stabilization이 먼저 적용된다.
- 첫 effect가 새 `RulesTiming` trigger를 만들면 nested trigger가 outer tail보다 먼저 drain된다.
- Hand->Trash, Trash->Hand, FaceUpSecurity->Trash, inherited->top, inherited->linked, field top->source 전환은 trigger source snapshot revalidation에서 stale source로 skip된다.
- security auto-process는 `RulesTiming`보다 state-only RuleProcessor stabilization을 먼저 수행한다.
- 기존 optional/chained/security/phase replay regression과 ST1~ST3 card effect regression이 계속 통과한다.

실행 결과:

```powershell
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 327 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

최신 기준일: 2026-06-18

검증의 목적은 Unity 원본 battle 로직과 RL.Engine의 rule-visible state 차이를 찾는 것이다. self-play/trace는 엔진 완성 전 학습 데이터가 아니라 검증 데이터로만 사용한다.

## 최신 상태 요약 - 2026-06-19

- ST1 target deck validation: 통과
- Unsupported card/effect: 0
- PartiallyImplemented card/effect: 0
- EngineCompletionChecklistRunner failed gate: 0
- ST1~ST3 registry snapshot: `cardeffect-porting-status.md` 기준 48장 문서화
- 최신 기록된 구조 guard 테스트: `All 225 tests passed.`
- source-alignment 상태: `ST2-07`, `ST3-07` shared `ST1_06` mapping은 card-id 기반 `Implemented` script와 `SourceEffectClassName` metadata로 정리됨. `ST3-02`는 base/P1 NoEffect 후보와 P2 source body 미확인 variant를 분리해 `needs-review`로 유지한다.
- queue 46 기준 golden scenario gap plan 작성 완료. ST1/minimal battle 7개 scenario 외 ST1~ST3 expanded golden suite는 아직 구현되지 않았다.
- RL 학습 구성: 아직 구현 금지

## Historical ST1 실행 명령

```powershell
$env:DOTNET_CLI_HOME='E:\headlessDCGO\.dotnet_home'
$env:NUGET_PACKAGES='E:\headlessDCGO\.nuget\packages'
$env:TEMP='E:\headlessDCGO\.tmp'
$env:TMP='E:\headlessDCGO\.tmp'
.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

해당 ST1 완료 시점 결과는 `All 170 tests passed.`였다. 이는 historical ST1 checkpoint 기록이며 현재 source-aligned 구조 guard의 최신 테스트 수와 다르다. MSBuild cache/temp access denied warning이 있었지만 test runner는 성공 종료했다.

## ST1-12 추가 검증

- security check에서 ST1-12가 `Security -> Executing -> BattleArea`로 이동하는지 확인.
- play-self tamer가 memory cost를 지불하지 않는지 확인.
- play 성공 후 checked card가 trash로 이동하지 않는지 확인.
- ST1-12 field aura가 owner turn 조건에서만 effective DP에 반영되는지 확인.
- `TriggerPipelineService`가 explicit executing source의 `SecuritySkill` descriptor를 실행할 수 있는지 확인.
- field가 가득 찬 경우 ST1-12 security effect가 실행되지 않고 checked card가 trash로 이동하는지 확인.

## 유지되는 주요 검증 축

- forbidden dependency check
- deck setup/draw/security determinism
- deck-out loss
- normal play/digivolve/pass/memory crossing
- DP battle, security attack, direct attack win
- ZoneMover와 `CardInstance.CurrentZone` invariant
- Permanent top/source/linked state invariant
- `TemporaryModifier` cleanup/state hash/replay determinism
- continuous effective stat determinism
- selection request/result validation
- unsupported mechanic explicit failure
- random legal action invariant smoke

## Historical ST1-ST3 validation gate design

2026-06-14 ST1-ST3 inventory 단계에서는 validation 구조만 설계했다. 아래 내용은 당시 planning-only baseline이다.

Current note: 이후 source-aligned 로컬 작업트리에는 ST1~ST3 registry snapshot과 구조 guard가 추가되어 최신 `All 225 tests passed.` 기록이 남았다. 따라서 아래 expected-fail 문장은 현재 snapshot이 아니라 historical planning note다.

Planned ST1-ST3 validation output:

- target card pool count: 48 cards.
- cards with non-empty `CardEffectClassName`: 36 cards.
- unique CardEffect discovery count under ST1/ST2/ST3: 34 files.
- NoEffect candidate count with empty `CardEffectClassName`: 12 cards.
- per-card expected status: `NoEffect`, `ImplementableNow`, `NeedsCommonLayer`, `NeedsComplexMechanic`, or `Unsupported`.
- unsupported/missing layer report grouped by reusable engine capability.
- ST1 baseline regression result kept separate from ST2/ST3 expansion failures.
- explicit no-RL-training check.
- explicit DCGO Unity source unchanged check.

The gate must fail explicitly while ST2/ST3 missing layers remain. A NoEffect card can pass only when the absence of a Unity CardEffect file has been documented. Unsupported ST2/ST3 effects must not be represented as silent no-op CardScripts.

Suggested validation batches are now fixed in `docs/rl-engine/st1-st3-porting-plan.md` as Implementation Pass 01 through Implementation Pass 09. In particular, `ST2-07` and `ST3-07` must be validated as shared `ST1_06` effect-class mappings, not NoEffect cards.

No code test command was required for this document-only inventory task.

## 구조 검증 테스트

2026-06-14 queue 36 강화 기준:

- ST1 카드별 파일 존재, source mapping, NoEffect 근거를 테스트한다.
- ST2/ST3 카드별 파일 존재, source mapping, NoEffect marker 근거를 테스트한다.
- `St1CardScriptCatalog`와 `St2St3CardScriptCatalog`가 registry-only인지 테스트한다.
- `cardeffect-porting-status.md` ST1 status table이 registry와 파일 존재 상태에 맞는지 테스트한다.
- `cardeffect-porting-status.md` ST1~ST3 registry snapshot이 실제 registry와 파일 존재 상태에 맞는지 테스트한다.
- `ST2-07`, `ST3-07`처럼 shared effect class를 참조하는 카드는 NoEffect가 아니라 shared mapping으로 등록됐는지 테스트한다. registry lookup alias와 원본 `SourceEffectClassName` metadata가 분리되어 보존되는지도 테스트한다.
- `ST3-02`처럼 variant별 effect class가 갈리는 카드는 `CardId`만으로 flatten하지 않고 `CardIndex`와 `VariantKey`를 포함한 `AssetCardDefinitionId`로 보고한다.
- CardEffect 파일에서 직접 zone list 수정 패턴을 금지한다.
- ST2/ST3 파일이 `CardEffects/ST2/...`, `CardEffects/ST3/...` 아래에 있는지 테스트한다.

이 테스트는 새 카드 효과 구현 없이도 포팅 구조가 원본-like 형태에서 벗어나는 것을 막기 위한 guard다. queue 36에서 추가한 ST2/ST3 NoEffect marker 파일은 동작 없는 구조 근거 파일이며, effect body를 추가한 것이 아니다.

실행 결과:

```powershell
.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

실패/수정 내역: 첫 실행에서 기존 `LoadSt1PortingStatusTable` parser가 새 ST1~ST3 registry snapshot의 `ST1-*` 행을 함께 읽어 `PortingStructure ST1 status table matches registry`와 `PortingStructure ST1 status table matches files`가 실패했다. parser를 `## 최신 ST1 상태표` 섹션으로 한정해 수정했다.

queue 43에서 concrete class/card-file 선언 guard와 NoEffect marker-only guard를 추가로 고정했다.

queue 49에서는 `AssetRegistryMappingValidator`를 추가했다. 이 validator는 원본 `CardBaseEntity` asset의 `CardEffectClassName`, RL registry `CardEffectPortingRecord`, status snapshot, 카드별 RL.Engine 파일, 원본 source body 존재 여부를 대조한다. source root가 없으면 조용히 skip하지 않고 `SourceUnavailable` report를 반환한다.

현재 repo의 `src/DCGO.RL.Cli`는 아직 CLI project가 아니라 `.gitkeep`만 있는 placeholder다. 따라서 queue 49에서는 audit surface를 `AssetRegistryMappingValidator.ValidateLocalSource` service와 `AssetRegistryMapping actual local DCGO audit` 테스트로 고정했다. `scripts/run_asset_registry_mapping_audit.ps1`는 test runner filter로 `AssetRegistryMapping` 감사 테스트 묶음만 실행한다. report record는 JSON 직렬화 가능하고, 한국어 요약은 `AssetRegistryMappingReport.ToKoreanSummaryLines()`로 제공한다. 현재 로컬 source audit은 `ST3-02` P2 needs-review 때문에 `IsValid == false`가 정상이다.

로컬 감사 결과:

- `ST2-07`, `ST3-07`: asset `CardEffectClassName`은 `ST1_06`, registry lookup alias는 빈 문자열, `SourceEffectClassName`은 `ST1_06`으로 분리 보존한다.
- `ST3-02`: `ST3_02.asset` CardIndex 76과 `ST3_02_P1.asset` CardIndex 77은 빈 `CardEffectClassName`의 NoEffect 후보로 남긴다. `ST3_02_P2.asset` CardIndex 4977은 `CardEffectClassName: ST3_02`이지만 source body가 없어 `FalseNoEffect` 및 `MissingSourceEffectBody` needs-review로 보고한다.
- machine-readable report는 `AssetRegistryMappingReport`/`AssetRegistryMappingIssue`/`AssetRegistryMappingCardReport` record로 제공하며 JSON 직렬화 가능하다.

결과: `All 225 tests passed.` MSBuild `AssemblyReference.cache` 및 `MSBuildTemp` 접근 경고가 있었지만 test runner는 성공 종료했다.

## StateHash 기준

StateHash에는 원천 state가 포함된다.

- turn player, phase, memory, turn count
- player zone ordering과 card instance id
- permanent top/source/linked/suspended state
- public security 정보와 hidden placeholder
- pending decision/selection request stable id
- active `TemporaryModifier`

continuous effect는 derived value이므로 별도 저장하지 않는다. 대신 effective stat 계산 determinism을 테스트한다.

## 다음 검증 보강 후보

queue 46 기준 1차 golden scenario 우선순위는 다음 5개다.

| 우선순위 | Scenario | 검증 책임 | 구현 방식 |
| ---: | --- | --- | --- |
| 1 | ST1 security tamer aura battle | ST1-12 security play 후 다음 owner turn DP battle 결과 변화 | headless scripted replay |
| 2 | ST1 dynamic SecurityAttack replay | ST1-11 source 수 기반 security check 횟수 | headless scripted replay |
| 3 | Blue source trash attack replay | ST2-03/ST2-06/ST2-09 source order, selection validation, bottom source trash | headless scripted replay, 필요 시 Unity trace 비교 |
| 4 | Blue bounce/security option replay | ST2-16 top card hand 이동, sources trash, security activation | headless scripted replay |
| 5 | Yellow recovery/security hand replay | ST3-09 recovery, ST3-13/ST3-14 checked card hand 이동 | headless scripted replay |

별도 snapshot-only 또는 Unity trace 우선 후보:

- full `MultipleSkills` priority와 동시 trigger 선택 순서 검증
- counter/block 세부 timing end-to-end 검증
- ST2-15 chained source-card play
- ST3-01/ST3-04 DP-zero deletion trigger timing
- Unity 원본 trace와 RL.Engine trace의 구조화 비교 harness

## Queue 45 Gate 범위 정합성 - 2026-06-15

현재 validation 결과는 세 범위로 분리한다.

| 범위 | runner/테스트 근거 | 결과 | 비고 |
| --- | --- | --- | --- |
| ST1 target deck | `EngineCompletionChecklistRunner`, `ValidationHarnessV2 completion gate reports ST1 complete` | 통과 | ST1 request 한정 |
| ST1~ST3 target pool | `TargetCardPoolValidator`, `ST1-ST3 target pool validation passes` | 통과 | 48장 registry/status/file/deck validation 기준 |
| 전체 엔진 completion | whole-engine request 없음 | 미실행 / 검증 필요 | RL 학습 단계 진입 근거 아님 |

failed gate는 테스트 실패가 아니라 completion report의 gate 결과로 표현한다. Unsupported/Partial 항목이 남는 경우 test runner 자체를 실패시키기보다 report에 failed gate와 missing layer를 명시한다.

RL 학습 단계 진입: 불가. 현재 검증은 source-aligned porting 상태를 확인하기 위한 검증 데이터이며 학습 데이터 생성 단계가 아니다.

## Queue 50 Option Lifecycle 검증 - 2026-06-19

Option hand play lifecycle은 원본 `UseOptionClass` 기준으로 `Hand -> Executing -> OptionSkill -> Trash`를 검증한다.

검증 항목:

- 정상 hand play option이 cost 지급 후 `Hand -> Executing -> Trash`로 이동한다.
- `OptionSkill` context payload의 `SourceZone`과 source card current zone은 effect 실행 중 `Executing`이다.
- selection pending 중에는 option card가 `Executing`에 남는다.
- selection completion 후 다음 chained selection이 없으면 아직 `Executing`인 option만 trash로 이동한다.
- option body가 source card를 `Hand` 등 다른 zone으로 이동한 경우 후속 trash를 생략한다.
- invalid hand play는 memory/zone/state hash를 오염시키지 않는다.
- action trace replay에서 동일 final state hash를 만든다.
- ST1-13, ST1-14, ST1-15, ST1-16 hand option regression을 유지한다.
- ST2-13, ST2-15 chained selection, ST3-13 hand option regression을 유지한다.

실행 결과:

- `dotnet run --no-restore --project src/DCGO.RL.Engine.Tests/DCGO.RL.Engine.Tests.csproj -- "Option lifecycle"`: 9개 테스트 통과.
- `dotnet run --no-restore --project src/DCGO.RL.Engine.Tests/DCGO.RL.Engine.Tests.csproj`: 전체 234개 테스트 통과.
- restore는 로컬 사용자 temp/NuGet lock 권한 문제로 실행하지 않았고, repo-local SDK에 `DOTNET_CLI_HOME`, `TEMP`, `TMP`를 workspace 내부로 지정한 뒤 `--no-restore`로 실행했다.

주의:

- 이 검증은 option lifecycle에 한정된다.
- `ST3-02` variant finding은 계속 blocking/needs-review이다. `ST3_02_P2.asset`의 source body가 확인되지 않았으므로 효과를 추측 구현하지 않으며, whole-engine completion gate에서 숨기지 않는다.
- whole-engine completion gate는 여전히 미실행이며 RL 학습 단계 진입 근거가 아니다.

## Queue 52A EngineSession pause/resume 검증 - 2026-06-20

공통 pause/resume foundation 검증을 추가했다.

검증 항목:

- hand option action이 selection 필요 시 `EngineStepResult.PausedForDecision`을 반환한다.
- pending 중 새 action 실행은 실패한다.
- `DecisionResult.Player`, `SelectionResult.RequestId`, `DecisionToken`이 pending request와 다르면 실패한다.
- optional yes는 explicit target selection을 두 번째 decision으로 반환한 뒤 resolve한다.
- optional no는 effect body를 skip하고 기존 queue tail을 drain한다.
- security optional yes도 explicit target selection을 두 번째 decision으로 반환한 뒤 resolve한다.
- provider가 optional yes를 자동 공급해도 `TriggerPipelineService`와 `SecurityEffectExecutionService`는 explicit target selection을 별도 pending으로 반환한다.
- `OnPlay` pending selection은 play 완료 후 battle area permanent를 유지하고 selection resume 후 rules timing을 실행한다.
- `WhenDigivolving` pending selection은 digivolve 완료 후 source stack을 유지하고 selection resume 후 rules timing을 실행한다.
- `OnStartMainPhase` pending selection은 phase를 `Main`으로 전환한 상태에서 pause하고, selection resume 후 rules timing을 실행한다.
- `PassAction`의 `OnEndTurn` pending selection은 phase `End`와 기존 turn player를 유지한 상태에서 pause하고, selection resume 후 turn cleanup, turn player 전환, active phase 진입을 이어간다.
- `PassAction` tail의 `OnStartTurn` pending selection은 turn player 전환과 active phase 진입 후 pause하고, selection resume 후 rules timing을 실행한다.
- `AttackAction`의 `OnAllyAttack` pending selection은 attacker suspend 후 battle/security 처리 전에 pause하고, selection resume 후 attack tail과 rules timing을 이어간다.
- `AttackAction`의 `OnEndAttack` pending selection은 battle/security 처리 후 battle-end cleanup 전에 pause하고, selection resume 후 cleanup과 rules timing을 이어간다.
- action 후 `RulesTiming` pending selection은 action state change를 유지한 채 pause하고, selection resume 후 rule stabilization을 이어간다.
- selection resume 후 `RulesTiming`이 다시 selection을 반환하면 새 pending interaction으로 pause한다.
- attack security check 중 `SecuritySkill`/Activate Main pending selection은 security card를 `Executing`으로 이동한 상태에서 pause하고, selection resume 후 security card final zone, attack tail, rules timing을 이어간다.
- `EngineSession.RunToMainPhase()`는 `None`/`End`에서 `OnStartTurn` selection을 pause/resume한 뒤 `Main`까지 진행한다.
- ST2-15 같은 chained option selection은 첫 selection 후 다시 pause하고, 두 번째 selection 후 완료된다.
- selection chain 완료 후에만 rules timing/cleanup이 실행된다.
- action event와 selection event를 포함한 trace를 `ReplayRunner(services: ...)`로 재생해 동일 final state hash를 만든다.
- `PassAction` phase timing selection trace도 `ReplayRunner(services: ...)`로 재생해 동일 final state hash를 만든다.
- attack timing selection trace와 rules timing selection trace도 `ReplayRunner(services: ...)`로 재생해 동일 final state hash를 만든다.
- security selection trace도 `ReplayRunner(services: ...)`로 재생해 동일 final state hash를 만든다.
- `EngineSession.RunMainPhase()` phase-only trace도 `TraceEventKind.Phase` event로 재생해 동일 final state hash를 만든다.
- `ScriptedScenarioRunner`와 `RandomLegalActionRunner`는 services 기반 실행에서 pending selection을 `ScenarioRunStatus.PausedForDecision`로 반환한다.
- `Play`, `Digivolve`, `Attack`, `CheckSecurity` 직접 동기 API는 pending 발생 시 상태와 trace를 호출 전으로 되돌리고 실패한다.

실행 결과:

```powershell
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 278 tests passed.`

남은 범위:

- full security timing sequence인 `OnSecurityCheck`, `OnLoseSecurity`, security 감소 확정, `AfterEffectsActivate`는 queue 53에서 source-aligned 순서로 정렬한다.

## Queue 52D runner result snapshot and one-shot boundary 검증 - 2026-06-21

검증 항목:

- paused scripted `ScenarioResult` snapshot은 resume 후에도 state hash, trace event count, invariant report count가 변하지 않는다.
- paused random `ScenarioResult` snapshot은 resume 후에도 state hash, trace event count, invariant report count가 변하지 않는다.
- result `FinalState`를 외부에서 변경해도 내부 runner session에는 영향이 없다.
- result `Trace`를 외부에서 변경해도 내부 runner session에는 영향이 없다.
- providerless one-shot `Run(...)`이 pending decision에서 pause되면 `StartSession` 안내와 함께 실패한다.
- provider-driven one-shot `Run(...)`은 scripted/random runner 모두 계속 동작한다.
- chained decision, random RNG sequence, action count, replay determinism 회귀가 계속 통과한다.

실행 결과:

```powershell
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 302 tests passed.`

남은 범위:

- full security timing sequence인 `OnSecurityCheck`, `OnLoseSecurity`, security 감소 확정, `AfterEffectsActivate`는 queue 53에서 source-aligned 순서로 정렬한다.
- 52A는 보완 요구사항 통과로 `done` 처리한다.

## Queue 52B runtime state/token hardening 검증 - 2026-06-20

검증 항목:

- once-per-turn 사용 이력은 `GameState.RuntimeRules`에 저장되고 `Clone`, `RestoreFrom`, `ComputeStateHash`에 반영된다.
- `TurnCount`가 진행되면 과거 once-per-turn 이력이 정리된다.
- 직접 동기 `Play` pending rollback은 once-per-turn 사용 이력까지 호출 전으로 복구한다.
- 같은 `BattleEngineServices`에서 만든 두 `EngineSession`은 once-per-turn 이력을 공유하지 않는다.
- 반복 game/session 실행은 service graph에 once-per-turn 이력을 누적하지 않는다.
- public `EngineSession.Resume`은 `DecisionResult`만 받으며 token 없는 resume path가 없다.
- selection trace와 `TraceStore`는 `DecisionResult(Player, DecisionToken, SelectionResult)`를 보존한다.
- `ReplayRunner`는 trace에 기록된 token을 사용하고 stale token, wrong player, wrong request id를 거부한다.
- optional/chained/security/phase pause/resume 회귀가 계속 통과한다.

실행 결과:

```powershell
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 285 tests passed.`

남은 범위:

- runner 재개 가능한 continuation/session API는 queue 52C로 분리한다.
- full security timing sequence인 `OnSecurityCheck`, `OnLoseSecurity`, security 감소 확정, `AfterEffectsActivate`는 queue 53에서 source-aligned 순서로 정렬한다.

## Queue 52C runner continuation 검증 - 2026-06-20

검증 항목:

- scripted action이 selection pause 후 resume되면 같은 action을 다시 실행하지 않고 다음 step부터 계속한다.
- scripted optional yes -> target selection 같은 chained decision을 같은 runner session에서 이어 간다.
- random action이 selection pause 후 resume되면 이미 선택한 random action을 다시 뽑지 않고 action count를 이어 간다.
- random runner의 pause/resume 실행과 provider 기반 one-shot 실행의 final hash가 같다.
- 같은 seed/request의 두 random session이 같은 final hash와 action count를 만든다.
- 다른 runner에 session continuation을 넘기면 실패한다.
- stale `DecisionToken` resume은 실패한다.
- 두 runner 병렬 session은 state/RNG/decision state를 공유하지 않는다.
- `MaxActions` count는 resume 후 초기화되지 않는다.
- runner session trace는 `ReplayRunner`로 재생했을 때 같은 final hash를 만든다.
- external decision session은 shared stateful provider graph를 거부한다.

실행 결과:

```powershell
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 296 tests passed.`

남은 범위:

- full security timing sequence인 `OnSecurityCheck`, `OnLoseSecurity`, security 감소 확정, `AfterEffectsActivate`는 queue 53에서 source-aligned 순서로 정렬한다.
