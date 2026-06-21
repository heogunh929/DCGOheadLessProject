# 검증 전략

## Queue 66 검증 - 2026-06-21

`generate_full_card_porting_batches.py`는 62~65 산출물을 읽어 full-card porting generated subqueue를 만든다. 이 단계는 효과 구현 없이 dependency-aware batch prompt와 control files만 생성한다.

검증 항목:

- source scaffold coverage: 3,918 / 3,918 assigned
- NeedsSourceReview coverage: 40 / 40 assigned
- source 불명확 항목은 `source-review` batch로 분리
- generated prompt count와 manifest batch count 일치
- generated prompt에 `{카드명}`, `{CardId}` 같은 placeholder 없음
- `effectBodiesChanged=false`, `rlComponentsGenerated=false`
- control files 존재: goal, active runner, queue, progress

산출물:

- `scripts/generate_full_card_porting_batches.py`
- `docs/generated/full-card-porting-batches-66.json`
- `docs/rl-engine/full-card-porting-batches-66.md`
- `docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/prompts/generated/full-card/*.md`

실행 결과:

```powershell
python .\scripts\generate_full_card_porting_batches.py --workspace .
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: batch 423개 생성, `All 421 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 65 검증 - 2026-06-21

`FullCardPoolValidator`는 64번 scaffold를 전체 카드풀 completion gate의 baseline 입력으로 검증한다. 현재 결과는 `Blocked`가 정상이며, 이는 source-bearing 효과가 아직 porting되지 않았음을 structured blocker로 드러내기 위한 것이다.

검증 항목:

- 전체 manifest count: 8,186 card mapping records
- status counts: `Unsupported` 7,921 / `NoEffect` 225 / `NeedsSourceReview` 40
- source unavailable: missing source body 39, ambiguous source mapping 1
- mechanic inventory blocker: `Unsupported`, `PartiallyImplemented`, `NeedsSourceReview`
- deck subset validation API: no-effect subset pass, blocked/missing subset blocked
- deterministic report serialization
- ST1~ST3 및 기존 전체 regression 유지

산출물:

- `src/DCGO.RL.Engine/Validation/FullCardPoolValidator.cs`
- `scripts/generate_full_card_pool_validation_baseline.py`
- `docs/generated/full-card-pool-validation-baseline-65.json`
- `docs/rl-engine/full-card-pool-validation-baseline-65.md`

실행 결과:

```powershell
python .\scripts\generate_full_card_pool_validation_baseline.py --workspace .
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `Blocked` baseline report 생성, `All 417 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 64 검증 - 2026-06-21

`generate_full_per_card_source_scaffold.py`는 승인된 local manifest snapshot을 재검증한 뒤 `docs/generated/full-card-source-scaffold/` 아래에 전체 카드/source-effect scaffold를 생성한다. 이 단계는 효과 body 구현이 아니라 mapping/scaffold 계약 고정이다.

검증 항목:

- manifest identity coverage: 8,186 card mapping records
- source class coverage: 3,918 source scaffold records
- per-card mapping coverage: `CardId#CardIndex@VariantKey` 보존
- catalog registry-only: set catalog 63개, `registryOnly=true`
- conservative status policy: source-bearing default `Unsupported`, missing/ambiguous source `NeedsSourceReview`, empty `CardEffectClassName` only `NoEffect`
- no automatic `Implemented`/`Verified` promotion

산출물:

- `scripts/generate_full_per_card_source_scaffold.py`
- `docs/generated/full-card-source-scaffold/index.json`
- `docs/generated/full-card-source-scaffold/status-registry.json`
- `docs/generated/full-card-source-scaffold/cards/*.json`
- `docs/generated/full-card-source-scaffold/sources/*.json`
- `docs/generated/full-card-source-scaffold/catalogs/*.json`
- `docs/rl-engine/full-per-card-source-scaffold.md`
- `src/DCGO.RL.Engine/Validation/FullCardSourceScaffoldValidator.cs`

실행 결과:

```powershell
python .\scripts\generate_full_per_card_source_scaffold.py --workspace .
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: All 413 tests passed. MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 60A 검증 - 2026-06-21

`EngineCoreExpansionReadinessRunner`는 52~59A evidence를 종합해 `ReadyForFullCardPoolInventory`로 판정한다. 이 판정은 전체 DCGO snapshot inventory 진입 readiness이며, `ReadyForRlEnvironmentDesign`은 false로 고정한다.

검증 항목:

- 전체 regression
- 59A engine-core gate
- multiple deterministic seeds
- random legal action smoke
- decision pause/resume smoke
- golden scenario batch 1
- trace export/compare synthetic fixture
- invariant fuzz
- source mapping audit
- runtime composition audit
- Unity source unchanged

`ST3-02` P2 source body 미확인은 carry-forward finding으로 유지한다. full inventory는 막지 않지만 variant implementation, full snapshot completion, RL environment design은 계속 막는다.

산출물:

- `docs/generated/engine-core-expansion-readiness-60A.json`
- `docs/rl-engine/engine-core-expansion-readiness-60A.md`

실행 결과:

```powershell
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: All 409 tests passed. MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 59A 검증 - 2026-06-21

`EngineCoreMilestoneGateRunner`는 ST1~ST3 engine-core milestone을 `NeedsReview`로 판정한다. core parity 자체는 runtime composition, shared dependency identity, RuleProcessor/ZoneMover injection, decision pause/resume, option Executing lifecycle, security timing, MultipleSkills/AfterEffects, counter/block/target timing, golden scenario, replay determinism, invariant fuzz, parity trace contract/comparer evidence가 통과한다.

남은 finding은 `ST3-02` P2 source body 미확인이다. base/P1은 NoEffect 후보로 유지하고, P2는 `CardEffectClassName: ST3_02` source body가 없어 `NeedsReview`로 유지한다. 이 finding은 full card pool inventory 자체를 막지는 않지만 variant implementation, full snapshot completion, RL environment design을 막는다.

산출물:

- `docs/generated/engine-core-milestone-gate-59A.json`
- `docs/rl-engine/engine-core-milestone-gate-59A.md`

실행 결과:

```powershell
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 407 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

## Queue 54C 검증 - 2026-06-21

추가 검증 항목:

- `AfterEffectsActivate` candidate가 같아도 state hash가 바뀌면 재활성될 수 있다.
- 같은 state와 같은 `AfterEffectsActivate` candidate 반복은 self-loop guard로 명시 실패한다.
- foreground batch 실행 후 stale background frame으로 넘어가도 foreground `HadResolutionAttempt`가 보존되어 `AfterEffectsActivate`가 정확히 한 번 실행된다.
- `RuleProcessor.StabilizeStateOnly`가 DP 0 destruction을 `RuleStabilizationResult.Events`의 `OnDestroyedAnyone` payload로 보존한다.
- invalid/face-down permanent 정리, linked trim, stale duration cleanup은 현재 state-only cleanup으로 남겨 source evidence 없는 trigger 확장을 막는다.

실행 결과:

```powershell
$env:TEMP='E:\headlessDCGO\.tmp\MSBuildTemp'
$env:TMP='E:\headlessDCGO\.tmp\MSBuildTemp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 405 tests passed.` MSBuild temp/cache access warning은 있었지만 test runner는 성공 종료했다.

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
- `AttackAction`의 blocker selection은 `EngineSession.Resume(DecisionResult)`로만 재개하며, resume 시 현재 blocker 후보를 다시 만들어 stale blocker와 `CannotBlock` 제한을 검증한다.
- `OnCounterTiming`은 비-counter 후보와 counter 후보를 분리해 실행하며, counter timing selection은 block/battle 전에 pause한다.
- block 선택 후 `OnBlockAnyone`, `OnAttackTargetChanged`, battle/security, `OnEndAttack` 순서를 검증한다. `OnEndBlockDesignation`은 로컬 DCGO 원본에서 실제 호출 위치가 확인되지 않아 `NotReferenced` audit 테스트로 고정한다.
- `ST1_06`/`ST2-07`/`ST3-07` shared attack memory loss와 `ST1_09` inherited block hook은 원본 `CanTriggerOnAttack` 의미대로 실제 attacker source에서만 발동하는지 검증한다.
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

결과: queue 55B 기준 `All 369 tests passed.`

55B 추가 검증:

- non-turn player hand counter 후보를 수집한다.
- field/trash/face-up-security counter 후보를 수집한다.
- `CounterEffectHashtable` 대응 snapshot은 payload로 유지하지만 source whitelist로 쓰지 않는다.
- turn player skip 후 non-turn player counter 선택으로 진행한다.
- turn player가 counter를 사용하면 non-turn player counter는 실행하지 않는다.
- mandatory counter group은 skip할 수 없다.
- 같은 counter 카드 인스턴스 2장이 서로 다른 selection id를 가진다.
- counter 선택 뒤 optional yes/no를 중복 질문하지 않고 target selection으로 이어진다.
- counter 중 defender가 삭제되어도 blocker selection까지 진행한다.
- `OnBlockAnyone` 중 blocker가 삭제되면 target-change가 발생하지 않는다.
- 한 effect body의 target switch 2개가 순서대로 처리된다.
- `OnEndAttack`에서 `IsBlocking`/`Blocker` context를 조회할 수 있다.
- counter selection replay가 deterministic final hash를 만든다.

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
