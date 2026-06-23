# PROGRESS_FOUNDATION_COMPLETION

이 문서는 Script Runtime Foundation 완성 작업의 진행 기록이다.
card-porting batch 진행률이 아니라, Foundation Gate를 열기 위한 운영/문서/구현 remediation 이력을 기록한다.

## 현재 기준선

| 항목 | 값 |
| --- | --- |
| 기준일 | 2026-06-23 |
| OpenCodeReady | false |
| Passed gates | 11 |
| Failed gates | 3 |
| Selected next foundation capability | ContinuousOrStaticEffect |
| Selected next foundation status | PartiallyImplemented |
| Unknown common API count | 27 |
| Unsupported capability count | 26 |
| PartiallyImplemented capability count | 37 |
| C0039 이후 card-porting | 금지 |

## 진행 기록

| 날짜 | 작업 | 요약 | 근거 | 검증 | 남은 blocker |
| --- | --- | --- | --- | --- | --- |
| 2026-06-23 | Permanent EffectList_Added temporary grant facade | 원본 `Permanent.EffectList_Added` duration list 의미를 headless `TemporaryGrantedEffect` 상태와 연결했다. `EffectList_Added(timing, TemporaryGrantedEffectRegistry)`는 target permanent와 timing이 일치하는 grant를 descriptor-backed `ICardEffect`로 노출하고, registry 없는 호출 또는 missing granted-effect handler는 명시 실패한다. 기존 trigger pipeline의 `TemporaryGrantedEffect` execution 경로는 유지했다. | `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md`, `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 610 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | player-level effect provider, production card script catalog의 provider 채택, broad primitive target-immunity integration은 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectFactoryCatalog bridge script provider path | `ICEntityEffectFactoryProvider`를 추가하고 `CEntityEffectFactoryCatalog.FromScripts(...)`가 bridge script 목록에서 source effect class factory catalog를 구성하도록 했다. non-empty `EffectiveSourceEffectClassName` 그룹은 provider 1개를 요구하며, provider가 없거나 둘 이상이거나 null factory를 반환하면 명시 실패한다. shared source class는 metadata record 여러 개가 같은 source class를 가리키되 factory provider는 하나만 허용한다. | `src/DCGO.RL.Engine/CardEffects/ICEntityEffectFactoryProvider.cs`, `src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md`, `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 609 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | 실제 production card script catalog의 provider 채택, `EffectList_Added` duration-scoped grant, player-level effect provider는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectFactoryCatalog service graph overload | `CEntityEffectFactoryCatalog`를 추가해 source effect class factory catalog 검증을 명시 객체로 분리했다. `BattleEngineServices.Create(ICardScriptRegistry, CEntityEffectFactoryCatalog, IDecisionProvider?)` overload는 `ICardScriptRegistry.PortingRecords`와 catalog를 사용해 `ICEntityEffectRegistry` root dependency를 조립한다. duplicate/empty/null factory는 명시 실패한다. | `src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs`, `src/DCGO.RL.Engine/Battle/BattleEngineServices.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 608 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | bridge script registration이 catalog를 채우는 경로, `EffectList_Added` duration-scoped grant, player-level effect provider는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry porting metadata builder | `CEntityEffectRegistryBuilder`를 추가해 `ICardScriptRegistry.PortingRecords` 또는 명시 `CardEffectPortingRecord` 목록을 `CEntityEffectRegistryEntry` 목록과 registry로 변환할 수 있게 했다. `EffectiveSourceEffectClassName`을 기준으로 shared source class를 dedupe하고, NoEffect/empty class는 skip하며, missing/null factory와 invalid record는 명시 실패한다. | `src/DCGO.RL.Engine/CardEffects/CEntityEffectRegistryBuilder.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 607 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production factory catalog, bridge script registration, `EffectList_Added` duration-scoped grant, player-level effect provider는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry source-aligned entry builder | `CEntityEffectRegistryEntry`와 `CEntityEffectRegistry.FromEntries(...)`를 추가해 production metadata가 raw dictionary 대신 `(CardId, EffectClassName, factory)` entry를 제공하면 direct class, source namespace, token namespace lookup key로 deterministic하게 확장되도록 했다. duplicate key, empty CardId/class, null factory, null entry는 명시 실패한다. | `src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 606 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | asset/porting metadata에서 실제 entry 목록을 생성하는 조립 경로, bridge script 등록, `EffectList_Added` duration-scoped grant, player-level effect provider는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry service graph prep | raw factory dictionary를 runtime facade에 직접 넘기는 임시 경계를 명시 `ICEntityEffectRegistry`/`CEntityEffectRegistry`로 분리했다. `CEntity_EffectController`, `CardSource.EffectList*`, `Permanent.EffectList*`는 registry overload를 우선 사용하고 dictionary overload는 compatibility wrapper로 남겼다. registry는 direct class, source namespace, token namespace lookup과 duplicate/empty/null factory 검증을 담당한다. `BattleEngineServices`는 registry를 root dependency로 보유하고 validation report에서 누락을 잡는다. | `src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs`, `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs`, `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine/Battle/BattleEngineServices.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 605 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production asset/porting metadata에서 registry를 자동 populate하는 작업, `EffectList_Added` duration-scoped grant, player-level effect provider는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CardSource/Permanent EffectList facade | 원본 `CardSource.EffectList*`와 `Permanent.EffectList*` 계약을 headless `ScriptRuntimeFoundation`에 추가했다. Unity/GManager lookup은 복제하지 않고 명시적 `ICEntityEffectRegistry`를 요구한다. `Permanent.EffectList*`는 top-card, inherited source, linked source 역할을 구분해 수집하고 face-down source를 skip한다. `CardSource.IsFlipped`는 원본처럼 face-down 의미로 보정했다. | `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CEntity_EffectController.cs`, `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 605 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | `EffectList_Added` duration-scoped grant, player-level effect provider, production asset registry population은 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | Foundation queue/progress 분리 | `QUEUE_FOUNDATION_COMPLETION.md`와 `PROGRESS_FOUNDATION_COMPLETION.md`를 생성하고, full-card queue와 Foundation active/progress 문서에 흩어져 있던 66B~66AE foundation-remediation 이력을 Foundation queue로 재분류했다. C0039 이후 card-porting 금지 상태, 현재 Foundation Gate 실패 항목 3개, 다음 foundation 작업 순서, 장기 path/namespace alignment plan을 문서화했다. | `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`, `docs/codex-prompts/ACTIVE/RUN_NEXT_FOUNDATION_COMPLETION.md`, `docs/rl-engine/foundation-completion-gate.md`, `docs/generated/foundation-completion-gate.json` | 문서와 queue만 변경했다. 코드, generated gate input, 빌드 입력, 실제 path/namespace는 변경하지 않았으므로 테스트는 생략했다. commit/push도 수행하지 않았다. | OpenCodeReady=false, failed gate 3개 유지. 다음 작업은 `ContinuousOrStaticEffect` partial closure, unknown common API mapping, unsupported capability 분해 순서다. |

## 테스트 기록

최근 작업은 두 범위로 나뉜다.

- Foundation queue/progress 분리: 문서와 queue만 변경했고 코드, generated gate input, 빌드 입력, 실제 path/namespace를 변경하지 않아 빌드/테스트를 생략했다. commit/push도 수행하지 않았다.
- Permanent EffectList_Added temporary grant facade, CEntityEffectFactoryCatalog bridge script provider path, service graph overload, CEntityEffectRegistry porting metadata builder, source-aligned entry builder, service graph prep 및 CardSource/Permanent EffectList facade: 코드와 테스트를 변경했으므로 전체 테스트를 실행했다.

최신 검증:

- `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 610 tests passed.`
- `python scripts\\calculate_foundation_completion_gate.py --workspace .`
- 결과: `OpenCodeReady=false`, passed gate 11개, failed gate 3개, next foundation capability `ContinuousOrStaticEffect`

## 다음 추천 작업

1. `FND-001`: `ContinuousOrStaticEffect` partial capability closure 계획을 계속 실행한다.
2. `FND-002`: unknown common API 27개를 capability matrix 또는 explicit unsupported queue로 분류한다.
3. `FND-003`: unsupported capability 26개를 구현 가능한 foundation remediation prompt로 분해한다.
4. `FND-004`: 실제 파일 이동 전 path/namespace alignment RFC와 compatibility test 범위를 확정한다.
5. `FND-005` 후속: 실제 production card script catalog의 `ICEntityEffectFactoryProvider` 채택과 player-level effect provider 대응은 CardEffect body 구현 없이 별도 foundation 작업으로만 진행한다.

## 금지 상태

- C0039 이후 full-card porting은 계속 금지다.
- 개별 CardEffect body 구현은 금지다.
- ST/BT/EX/P batch 확장은 금지다.
- RL Environment, Observation, Reward, Dataset Exporter, Trainer 구현은 금지다.
- Unity, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성 추가는 금지다.
