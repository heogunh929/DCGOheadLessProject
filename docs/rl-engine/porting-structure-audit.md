# Porting Structure Audit

## Current Snapshot - 2026-06-14

- 기준: source-aligned 로컬 작업트리. cached `origin/main`/로컬 기준점은 `a20f045a chore: checkpoint porting structure guards`이며 queue 34~38 변경은 아직 commit 전이다.
- ST1~ST3 카드별 파일/marker 구조와 registry snapshot은 존재한다.
- latest recorded structure guard: `All 212 tests passed.`
- `DCGO/Assets/Scripts` 원본 변경은 없다.
- 이 문서의 queue 35/36/37 섹션은 당시 감사 기록이므로, old expected-fail/planning 문구는 historical context로 해석한다.

## Queue 35 Current Commit Audit - 2026-06-14

기준 commit: `a20f045a chore: checkpoint porting structure guards`

이번 queue 35 작업은 구현 없이 현재 커밋 기준 구조를 다시 감사했다. `git fetch`, `git pull`, `git push`는 실행하지 않았고 `DCGO/Assets/Scripts` 변경도 없다.

### CardEffect File Layout

| Set | 원본 CardEffect 파일 수 | RL.Engine 카드별 파일 수 | 판정 |
| --- | ---: | ---: | --- |
| ST1 | 12 original effect files | 16 `ST1_*.cs` files | ST1은 NoEffect marker까지 포함해 카드별 파일 구조를 만족한다. |
| ST2 | 11 original effect files | 11 `ST2_*.cs` files | effect-bearing 파일은 대응된다. NoEffect marker 파일은 아직 없다. |
| ST3 | 11 original effect files | 11 `ST3_*.cs` files | effect-bearing 파일은 대응된다. NoEffect marker 파일은 아직 없다. |

확인된 예시 파일:

- `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_08.cs`: 존재
- `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_01.cs`: 존재
- `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_13.cs`: 존재

### Source Mapping Coverage

`Source mapping: DCGO/Assets/Scripts/CardEffect/...` 주석은 현재 ST1 구현 파일 12개에서 확인됐다. ST1 NoEffect marker 파일은 missing-source 근거를 별도로 가진다.

ST2/ST3 카드별 파일에는 아직 같은 형식의 source mapping 주석이 없다. 따라서 queue 36에서 ST2/ST3 파일에도 원본 path mapping guard를 확장해야 한다.

### Catalog Responsibility

`St1CardScriptCatalog`와 `St2St3CardScriptCatalog`에서 아래 패턴을 검색했다.

```text
new EffectDescriptor
SelectionRequest
SelectionResult
Tier1PrimitiveService
context.Primitives
Trash(
Delete(
Destroy(
```

결과: 매치 없음.

판정: 현재 catalog는 registry 등록 역할에 머문다.

### Common Layer Mapping Recheck

| 원본 DCGO 책임 | RL.Engine 대응 | 현재 판정 |
| --- | --- | --- |
| `AutoProcessing` / `MultipleSkills` trigger 수집/stack/drain | `TriggerPipelineService`, `TriggerCollector`, `EffectQueue`, `OptionalEffectBoundary`, `OncePerTurnTracker` | 대응은 있으나 full priority/동시 선택은 아직 제한으로 문서화 필요 |
| `CardEffectCommons` option/security/common helper | `Tier1PrimitiveService`, `StarterScriptSupport`, set별 support, effect services | 대응은 있으나 support helper가 카드 body를 숨길 위험 있음 |
| `CardController` play/use option/security/zone bridge | `PlayCardService`, `SecurityCheckService`, `SecurityEffectExecutionService`, `ZoneMover`, primitives | 분산 구조는 타당하나 source mapping 문서 보강 필요 |
| `AttackProcess` attack/block/security battle | `AttackService`, `BattleResolver`, `BattleKeywordService`, `SecurityCheckService` | 기본 대응은 있으나 counter/block 세부 timing은 별도 제한 |
| `TurnStateMachine` phase/timing/cleanup | `PhaseRunner`, `TurnRunner`, `DurationCleanupService`, `RuleProcessor` | 기본 hook은 있으나 원본 timing 전체와 1:1은 아님 |
| `SelectCardEffect` / `SelectPermanentEffect` | `SelectionRequest`, `SelectionResult`, `SelectionResultApplicator` | headless boundary로 적합 |

### Consolidation Risks After Current Commit

해결된 항목:

- ST1 NoEffect 카드도 marker 파일을 가지며 file/status guard가 존재한다.
- ST1 source mapping guard가 존재한다.
- ST1 catalog registry-only guard가 존재한다.
- CardEffect 파일의 직접 zone mutation guard가 존재한다.

남은 항목:

1. ST2/ST3 NoEffect 카드 marker 파일이 없다.
2. ST2/ST3 파일에는 ST1과 같은 source mapping 주석 guard가 없다.
3. ST2-07/ST3-07은 asset에서 shared `ST1_06`을 참조하며, 현재 `St2St3CardScriptCatalog`에는 card-id 기반 `Implemented` script로 등록한다.
4. ST3-02의 `ST3_02_P2.asset` variant가 `CardEffectClassName: ST3_02`를 가진다. 현재 `ST3-02` NoEffect 판정과 충돌 가능성이 있다.
5. `StarterScriptSupport`, `St2ScriptSupport`, `St3ScriptSupport`가 커지고 있어 카드별 body를 숨기지 않는지 지속 감사가 필요하다.
6. `docs/rl-engine`에는 ST1~ST3 completion 통과 문서와 planning-only/expected-fail 문서가 같이 남아 있다.

### ST2/ST3 Expansion Decision

ST2/ST3 기능 구현은 아직 바로 진행하지 않는 것이 맞다.

선행 권고:

1. queue 36에서 ST2/ST3 file layout/source mapping/NoEffect/shared-effect guard를 강화한다.
2. queue 37에서 공통 layer source mapping을 원본 파일별로 더 촘촘히 감사한다.
3. queue 38에서 문서의 Current Snapshot / Historical Notes / Planned Work 구분을 정리한다.

### Test Status

이번 queue 35 작업에서는 코드 변경을 하지 않았으므로 테스트를 새로 실행하지 않았다.

직전 구조 guard 기준 최신 테스트 결과:

```powershell
.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

결과: `All 202 tests passed.`

작성일: 2026-06-14

## Queue 36 Guard 강화 Update - 2026-06-14

queue 36에서는 새 카드 효과 구현 없이 구조 guard만 강화했다.

해결된 항목:

- ST2/ST3 `Implemented` 또는 `NoEffect` registry record가 모두 카드별 파일을 가지도록 NoEffect marker 파일을 추가했다.
- ST2/ST3 원본 set-local CardEffect 파일이 있는 카드 파일에 `Source mapping: DCGO/Assets/Scripts/CardEffect/...` 주석을 추가했다.
- `St2St3CardScriptCatalog` registry-only guard를 추가했다.
- `docs/rl-engine/cardeffect-porting-status.md`에 ST1~ST3 registry snapshot을 추가하고 실제 registry와 테스트로 대조한다.
- 카드별 파일의 직접 zone mutation guard를 ST1~ST3 전체 CardEffects 경로에 유지한다.

아직 남은 source-alignment 위험:

1. `ST2-07`, `ST3-07`은 원본 asset의 shared `ST1_06` 참조를 card-id 기반 `Implemented` script로 연결했다. registry에는 중복 `ST1_06` alias를 등록하지 않고 `CardId` lookup을 사용한다.
2. `ST3-02`는 `ST3_02_P2.asset` variant가 `CardEffectClassName: ST3_02`를 가진다. 이번 작업에서는 기능을 바꾸지 않고 marker/status/policy 문서에 variant 위험을 기록했다.
3. shared effect mapping과 variant 판정은 별도 queue에서 원본 asset 기준으로 재검토해야 한다.

테스트:

```powershell
.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

첫 실행은 기존 ST1 status parser가 새 ST1~ST3 registry snapshot까지 읽어 2개 테스트가 실패했다. parser를 `## 최신 ST1 상태표` 섹션으로 한정한 뒤 재실행했고 결과는 `All 212 tests passed.`다.

## 감사 범위

이번 작업은 구현이 아니라 구조 동등성 감사다. ST2/ST3 구현, ST1 코드 수정, 기존 `DCGO/` Unity 원본 수정, remote 사용, commit 생성은 하지 않는다.

감사 시작 시점에 이미 다음 uncommitted 변경이 존재했다.

- `src/DCGO.RL.Engine/CardEffects/St1CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/St2St3CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/ST1/**`
- `src/DCGO.RL.Engine/CardEffects/ST2/**`
- `src/DCGO.RL.Engine/CardEffects/ST3/**`
- `src/DCGO.RL.Engine/CardEffects/StarterScriptSupport.cs`
- 일부 `docs/rl-engine/*.md`

따라서 이 보고서는 현재 worktree 기준 구조를 감사한다. 기존 uncommitted 코드 변경은 이번 감사에서 새로 구현한 것으로 간주하지 않는다.

## 현재 문제 요약

1. 현재 방향은 원본 CardEffect별 파일 구조로 정렬되는 중이며, Catalog는 registry-only에 가까워졌다.
2. 다만 ST2/ST3 확장 전에 구조 검증 테스트가 없다.
3. 원본 asset 기준 shared effect가 있는 카드는 `NoEffect`로 등록하지 않도록 guard와 validator가 필요하다. `ST2-07`, `ST3-07`은 card-id 기반 shared mapping으로 정리됐다.
4. 공통 helper가 커지면서 카드별 body와 원본 공통 처리의 경계가 흐려질 수 있다.
5. `docs/rl-engine`에는 ST1 통과 상태, ST1-ST3 inventory 상태, local uncommitted 구현 상태가 섞여 있는 문서가 있다.

## 1. CardEffect 구조 감사

### 원본 CardEffect 구조

원본은 set/color/card 파일 구조를 사용한다.

| Set | 원본 CardEffect 파일 |
| --- | --- |
| ST1 | `ST1_01`, `ST1_03`, `ST1_06`, `ST1_07`, `ST1_08`, `ST1_09`, `ST1_11`, `ST1_12`, `ST1_13`, `ST1_14`, `ST1_15`, `ST1_16` |
| ST2 | `ST2_01`, `ST2_03`, `ST2_06`, `ST2_08`, `ST2_09`, `ST2_11`, `ST2_12`, `ST2_13`, `ST2_14`, `ST2_15`, `ST2_16` |
| ST3 | `ST3_01`, `ST3_04`, `ST3_05`, `ST3_08`, `ST3_09`, `ST3_11`, `ST3_12`, `ST3_13`, `ST3_14`, `ST3_15`, `ST3_16` |

원본 asset에서 `CardEffectClassName`이 비어 있는 카드는 NoEffect 후보가 될 수 있다. 단, 파일이 없더라도 asset이 다른 effect class를 참조할 수 있으므로 asset 기준 확인이 필요하다.

### RL.Engine CardEffects 구조

현재 worktree에는 다음 구조가 존재한다.

| 영역 | RL.Engine 파일 구조 | 감사 결과 |
| --- | --- | --- |
| ST1 | `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_*.cs` | 원본 CardEffect별 파일 구조와 대체로 일치 |
| ST2 | `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_*.cs` | 원본 CardEffect별 파일 구조와 대체로 일치 |
| ST3 | `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_*.cs` | 원본 CardEffect별 파일 구조와 대체로 일치 |
| Catalog | `St1CardScriptCatalog.cs`, `St2St3CardScriptCatalog.cs` | 현재는 registry 등록 중심 |
| Support | `StarterScriptSupport.cs`, `St1ScriptSupport.cs`, `St2ScriptSupport.cs`, `St3ScriptSupport.cs` | 반복 로직 분리 목적은 타당하나 body 은닉 위험 있음 |

### Catalog 책임

`St1CardScriptCatalog`와 `St2St3CardScriptCatalog`는 현재 `CreateScripts()`와 registry 생성만 담당한다. 카드별 class body는 Catalog에 남아 있지 않다. 구조 방향은 정책에 부합한다.

단, Catalog에 등록된 `NoEffectCardScript`가 원본 asset 기준으로도 NoEffect인지 검증하는 테스트가 필요하다.

### 원본 CardEffect 파일 대비 RL.Engine 대응 파일

ST1~ST3의 원본 CardEffect 파일이 있는 카드 대부분은 현재 RL.Engine에 카드별 파일이 존재한다. queue 48 기준으로 `ST2-07`, `ST3-07` shared effect 오분류는 해소됐고, 남은 위험은 `ST3-02`처럼 동일 `CardId` variant가 서로 다른 `CardEffectClassName`을 가지는 경우다.

| CardId | 원본 근거 | 현재 RL.Engine 처리 | 감사 판단 |
| --- | --- | --- | --- |
| ST2-07 | asset `CardEffectClassName: ST1_06` | `St2GrizzlymonScript` | 해소. shared `ST1_06` mapping을 card-id 기반으로 실행 |
| ST3-07 | asset `CardEffectClassName: ST1_06` | `St3UnimonScript` | 해소. shared `ST1_06` mapping을 card-id 기반으로 실행 |
| ST3-02 | 일부 asset variant에서 `CardEffectClassName: ST3_02` 확인 | `NoEffectCardScript("ST3-02")` | 불확실. variant data 확인 필요 |

## 2. 공통 처리 구조 감사

| 원본 파일/책임 | RL.Engine 대응 파일/서비스 | 구조 적합성 | 수정 필요 여부 |
| --- | --- | --- | --- |
| `AutoProcessing`: timing별 skill 수집, background effect, stack/drain, 후속 timing | `TriggerPipelineService`, `TriggerCollector`, `EffectQueue`, `OncePerTurnTracker`, `OptionalEffectBoundary` | 부분 적합. 여러 원본 책임이 한 service에 압축됨 | 필요. source collection/queue/drain/background 경계 테스트 추가 |
| `MultipleSkills`: 복수 trigger 처리와 선택/순서 | `EffectQueue`, `TriggerPipelineService` | 부분/누락. full priority와 동시 trigger 선택은 아직 제한적 | 필요. 지원 범위와 unsupported 조건 명시 |
| `CardEffectCommons`: 공통 CardEffect helper, option/security wrapper, 조건 확인 | `Tier1PrimitiveService`, `StarterScriptSupport`, set별 support, `SecurityEffectExecutionService`, `ContinuousEffectService`, `DurationCleanupService` | 혼합. generic helper는 필요하지만 카드 body를 숨길 위험 있음 | 필요. helper 책임 문서화 및 구조 테스트 |
| `CardController`: play/use option/security/digivolve/zone 이동/timing bridge | `PlayCardService`, `DigivolveService`, `SecurityCheckService`, `TriggerPipelineService`, `Tier1PrimitiveService`, `ZoneMover` | 과도 분산/부분 적합. 원본 monolith를 headless service로 나눈 방향은 타당하나 mapping 문서가 필요 | 필요 |
| `AttackProcess`: attack declaration, counter, block, target change, end attack, security battle | `AttackService`, `BattleResolver`, `BattleKeywordService`, `SecurityCheckService`, `TriggerPipelineService` | 부분 적합. counter/target change/block 세부 timing 미완성 | 필요 |
| `TurnStateMachine`: phase transition, start/end turn timing, cleanup | `PhaseRunner`, `TurnRunner`, `DurationCleanupService`, `RuleProcessor` | 부분 적합. phase hook은 있으나 원본 timing 전체와 1:1은 아님 | 필요 |
| `MainPhaseAction/*`: play/digivolve/attack/pass/activate action | `LegalActionGenerator`, `ActionExecutor`, `PlayCardService`, `DigivolveService`, `AttackService` | 부분 적합. activate permanent/card 세부 action coverage 확인 필요 | 필요 |
| `SelectCardEffect` / `SelectPermanentEffect`: UI 선택 요청과 결과 적용 | `SelectionRequest`, `SelectionResult`, `SelectionResultApplicator`, `SelectionValidator` | 적합. headless 구조에 맞게 boundary 분리됨 | 원본 selection metadata 추가 검증 필요 |

## 3. 통폐합 위험 목록

| 위험 | 내용 | 우선순위 |
| --- | --- | --- |
| shared effect NoEffect 오분류 | ST2-07/ST3-07 asset `ST1_06` 참조는 card-id 기반 `Implemented` script로 정리됨. 회귀 방지를 위해 asset registry validator가 필요 | 중간 |
| Catalog 검증 부재 | Catalog가 현재 registry-only라도 이를 보장하는 테스트 없음 | 높음 |
| 대형 support helper | `StarterScriptSupport`와 set별 support가 카드 body의 의미를 숨길 수 있음 | 중간 |
| TriggerPipeline 압축 | 원본 `AutoProcessing`, `MultipleSkills`, background effect, once-per-turn, optional boundary가 한 layer에 모임 | 중간 |
| 문서 상태 혼재 | ST1 완료, ST1-ST3 inventory, local uncommitted 구현 상태가 같은 문서에 섞임 | 높음 |
| 원본 mapping 테스트 부재 | 원본 CardEffect 파일/asset `CardEffectClassName`과 RL 파일/status 일치 검사 없음 | 높음 |
| ST2/ST3 shortcut 위험 | shared helper가 특정 카드 효과를 지나치게 일반화하면 원본 의미 차이를 놓칠 수 있음 | 중간 |
| Git hygiene | build 산출물 제외 원칙은 있으나 구조 refactor와 문서 변경이 큰 uncommitted 세트로 남아 있음 | 중간 |

## 4. 문서 상태 감사

| 문서 | 상태 |
| --- | --- |
| `docs/progress/CHATGPT_HANDOFF.md` | ST1-12 partial 시점과 ST1 completion 시점이 함께 남아 있어 최신/과거 구분이 더 필요함 |
| `docs/rl-engine/effect-system.md` | ST1-ST3 planning-only 문구와 local implementation 설명이 섞일 수 있음 |
| `docs/rl-engine/engine-completion-report-st1-st3.md` | ST1-ST3 validation이 아직 고정 gate인지, local implementation 결과인지 구분 필요 |
| `docs/rl-engine/validation-strategy.md` | inventory-only 단계 설명이 최신 local worktree 구현 상태와 충돌할 수 있음 |
| `docs/rl-engine/cardeffect-porting-status.md` | ST1 완료 기준은 유지하되 ST1-ST3 status 문서와 역할 분리 필요 |
| `docs/rl-engine/st1-st3-porting-plan.md` | pass 계획과 실제 local uncommitted 구현 결과를 분리해야 함 |

권장 정책은 문서마다 `Current Snapshot`, `Historical Notes`, `Planned Work` 섹션을 분리하는 것이다.

## 5. ST1 refactor 필요 항목

현재 worktree의 ST1 구조는 카드별 파일로 분리되어 있어 방향은 적합하다. 다만 ST1 확정 전 다음 항목이 필요하다.

1. Catalog registry-only 구조 검증 테스트 추가.
2. ST1 원본 CardEffect 파일과 RL.Engine 카드별 파일 매핑 테스트 추가.
3. `St1ScriptSupport`가 카드별 body를 과도하게 숨기지 않는지 점검.
4. ST1 NoEffect 카드가 asset `CardEffectClassName` 기준으로도 NoEffect인지 검증.
5. ST1 완료 문서와 ST1-ST3 확장 문서의 역할 분리.

## 6. ST2/ST3 확장 전에 반드시 고칠 항목

1. ST2-07과 ST3-07의 shared `ST1_06` effect mapping 회귀를 asset registry validator로 고정한다.
2. ST3-02 asset variant의 `CardEffectClassName: ST3_02` 근거를 확인한다.
3. 원본 asset `CardEffectClassName` 기반 status validator를 만든다.
4. Catalog가 body를 포함하지 않는지 구조 검증 테스트를 추가한다.
5. `StarterScriptSupport`를 generic primitive helper와 set-specific helper로 명확히 분리하거나 문서화한다.
6. ST1-ST3 docs의 최신 상태와 계획 상태를 분리한다.
7. ST2/ST3 validation은 모든 gap을 report로 드러내는 방식으로 유지한다.

## 7. ST2/ST3 확장 가능 여부

조건부 가능이다. 현재 구조는 원본 카드별 파일 배치로 나아가고 있어 ST2/ST3 확장 기반은 있다. 그러나 shared-effect 오분류와 구조 검증 테스트 부재를 해결하지 않으면 ST2/ST3 구현이 통과해도 원본 동등성을 신뢰하기 어렵다.

따라서 ST2/ST3 기능 구현은 보류하고, 먼저 구조 검증과 문서 정리를 수행해야 한다.

## 8. 권장 작업 순서

1. 현재 uncommitted 구조 refactor 범위를 별도 checkpoint로 고정할지 결정한다.
2. `porting-structure-policy.md` 기준 구조 검증 테스트를 추가한다.
3. ST2-07/ST3-07 shared `ST1_06` mapping 정책은 card-id 기반 `Implemented` script로 확정됐다. 다음 단계에서는 validator로 고정한다.
4. ST3-02 variant `CardEffectClassName` 불일치 여부를 조사한다.
5. Catalog registry-only, NoEffect asset 검증, status/file mapping 검증을 자동화한다.
6. docs의 최신 상태와 과거 기록을 분리한다.
7. 그 뒤 ST2/ST3 pass 구현을 재개한다.

## 9. 테스트

이번 감사 작업은 문서 생성만 수행한다. 구현 코드 변경을 새로 하지 않았으므로 전체 테스트는 실행하지 않는다.

단, 작업 시작 시 이미 존재하던 uncommitted 코드 변경은 별도 검증 대상이다. 해당 변경을 commit하거나 이어서 개발하기 전에는 전체 테스트와 구조 검증 테스트가 필요하다.

## 10. 결론

현재 RL.Engine 구조는 원본 CardEffect별 파일 구조로 정렬되는 방향은 맞다. queue 48 기준으로 shared-effect NoEffect 오분류는 `ST2-07`, `ST3-07`에서 해소됐지만, ST2/ST3 asset registry 검증 장치와 `ST3-02` variant identity 모델은 아직 필요하다.

ST2/ST3 구현을 바로 진행하기보다, 구조 검증 테스트와 문서 상태 정리를 먼저 수행해야 한다.

## 11. 2026-06-14 구조 검증 테스트 추가 결과

이번 후속 작업에서 ST2/ST3 구현은 시작하지 않고, 원본-like 포팅 구조를 강제하는 테스트를 `src/DCGO.RL.Engine.Tests/Program.cs`에 추가했다.

추가된 guard:

- ST1 `Implemented`/`NoEffect` registry record의 카드별 파일 존재 확인.
- 원본 CardEffect가 있는 ST1 카드의 source mapping 주석 확인.
- 원본 CardEffect가 없는 ST1 NoEffect 카드의 missing-source 근거 확인.
- `St1CardScriptCatalog`가 registry-only인지 금지 snippet으로 확인.
- ST1 status table과 실제 registry status 비교.
- ST1 status table과 실제 파일 존재 비교.
- CardEffect 파일의 직접 zone list mutation 패턴 금지.
- ST2/ST3 카드 효과 파일이 원본-like set path 아래에 있는지 확인.

이 guard를 통과시키기 위해 ST1 NoEffect 카드 `ST1-02`, `ST1-04`, `ST1-05`, `ST1-10`에는 효과 로직 없는 marker 파일을 추가했고, 기존 ST1 구현 파일에는 원본 source mapping 주석을 추가했다. 이는 새 카드 효과 구현이 아니라 구조 동등성 검증을 위한 파일/근거 보강이다.

테스트 결과:

- 첫 실행은 `St1GigaDestroyerScript` 카드명에 `Destroy` 금지 문자열이 걸리는 false positive로 실패했다.
- 금지 패턴을 body 호출 형태인 `Destroy(`, `Delete(`, `Trash(`로 좁혀 원칙을 유지하면서 false positive를 제거했다.
- 재실행 결과: `All 202 tests passed.`

남은 구조 위험:

- ST2-07/ST3-07 shared `ST1_06` mapping 문제는 card-id 기반 `Implemented` script로 해결했다.
- ST3-02 variant `CardEffectClassName` 불일치 가능성은 별도 감사가 필요하다.
- docs의 과거/최신 상태 분리는 아직 추가 정리가 필요하다.

## 12. 2026-06-14 공통 layer source mapping 감사

source-aligned queue 37에서는 기능 구현 없이 공통 layer가 원본 DCGO의 어떤 공통 처리에서 왔는지 감사했다. 상세 표는 `docs/rl-engine/common-layer-source-mapping.md`에 분리했다.

핵심 mapping:

| RL.Engine layer | 원본 DCGO 책임 | 감사 판정 |
| --- | --- | --- |
| `TriggerPipelineService` | `AutoProcessing.GetSkillInfos`, `StackSkillInfos`, `TriggeredSkillProcess`, `MultipleSkills` | 부분 적합. queue/drain/selection boundary는 있으나 full priority와 동시 trigger 선택은 partial |
| `SelectionResultApplicator` | `SelectCardEffect`, `SelectPermanentEffect`, 선택 callback | 적합. headless selection boundary를 유지함 |
| `TemporaryModifier` / `DurationCleanupService` | `CardEffectCommons` 일시 modifier와 turn/attack/security cleanup list | 부분 적합. 모든 Unity duration hook을 1:1로 덮지는 않음 |
| `ContinuousEffectService` | `EffectTiming.None` continuous/background effect와 effective stat 계산 | 부분 적합. field top/inherited 중심이며 모든 source zone을 포괄하지 않음 |
| `SecurityEffectExecutionService` | `CardController.ISecurityCheck.SecurityCheck`, security skill, option main activation | 부분 적합. ST1 security option/self-play 경로는 있으나 multiple security choice는 partial |
| `Tier1PrimitiveService` | `CardEffectCommons`, `CardController`, `Player`, `Permanent` 공통 조작 | 적합하나 넓음. primitive별 source mapping 유지 필요 |
| `ZoneMover` | `CardObjectController.RemoveFromAllArea`, zone add/remove, permanent source/top 이동 | 적합. 모든 zone 이동의 단일 primitive 역할 |
| `AttackService`/`BattleResolver`/`SecurityCheckService` | `AttackProcess`, `IBattle`, `ISecurityCheck` | 부분 적합. counter/block/target-change 세부 timing은 partial |
| `PhaseRunner`/`RuleProcessor` | `TurnStateMachine`, `AutoProcessing.AutoProcessCheck` | 부분 적합. phase hook과 rules processing은 있으나 모든 원본 phase detail은 아님 |

통폐합 위험:

1. `St1ScriptSupport.cs`의 `St1OptionDeleteScript.CreateSelectionRequest`에 `Porting.CardId == "ST1-15"` prompt 분기가 있다. 기능 shortcut은 아니지만 support helper가 카드별 차이를 직접 알기 시작한 위험 신호다.
2. `StarterScriptSupport`는 candidate/selection helper로 유용하지만, 계속 커지면 카드별 body를 숨기는 mini `CardEffectCommons`가 될 수 있다.
3. `St2ScriptSupport`와 `St3ScriptSupport`는 parameterized helper지만 `EffectDescriptor` 생성과 primitive 호출까지 포함한다. 카드 파일에 timing/조건/amount/source mapping이 계속 남아야 한다.
4. `TriggerPipelineService`는 원본 `AutoProcessing`과 `MultipleSkills`의 여러 책임을 합친 headless layer다. full priority 미지원 사실을 completion report와 tests에 계속 노출해야 한다.
5. `Tier1PrimitiveService`는 원본 `CardEffectCommons` 대응으로 정당하지만, card id 조건이 들어가면 즉시 구조 위반이다.

이번 감사에서 core common service 안의 특정 ST 카드 ID shortcut은 확인되지 않았다. 위험은 주로 support helper에 있으며, queue 37에서는 기능 변경 없이 문서화만 했다.

테스트:

- 문서만 생성/갱신했으므로 전체 테스트는 실행하지 않았다.
- 직전 queue 36 기준 최신 테스트 기록은 `All 212 tests passed.`다.

후속 권장:

1. `St1OptionDeleteScript`의 카드 ID prompt 분기를 카드별 파일 또는 생성자 configuration으로 이동한다.
2. `StarterScriptSupport`와 set별 support helper의 허용 책임을 구조 guard에 추가한다.
3. docs 최신/과거 상태를 queue 38에서 정리한다.
