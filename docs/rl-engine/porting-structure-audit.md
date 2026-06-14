# Porting Structure Audit

작성일: 2026-06-14

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
3. 원본 asset 기준 shared effect가 있는 카드가 `NoEffect`로 등록된 위험이 있다.
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

ST1~ST3의 원본 CardEffect 파일이 있는 카드 대부분은 현재 RL.Engine에 카드별 파일이 존재한다. 현재 감사에서 확인된 위험은 "파일 없음"보다 "asset shared effect를 NoEffect로 오분류"하는 쪽이다.

| CardId | 원본 근거 | 현재 RL.Engine 처리 | 감사 판단 |
| --- | --- | --- | --- |
| ST2-07 | asset `CardEffectClassName: ST1_06` | `NoEffectCardScript("ST2-07")` | 위험. shared `ST1_06` mapping 필요 |
| ST3-07 | asset `CardEffectClassName: ST1_06` | `NoEffectCardScript("ST3-07")` | 위험. shared `ST1_06` mapping 필요 |
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
| shared effect NoEffect 오분류 | ST2-07/ST3-07 asset이 `ST1_06`을 참조하지만 현재 NoEffect 등록 | 높음 |
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

1. ST2-07과 ST3-07의 shared `ST1_06` effect mapping을 확정한다.
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
3. ST2-07/ST3-07 shared `ST1_06` mapping 정책을 확정한다.
4. ST3-02 variant `CardEffectClassName` 불일치 여부를 조사한다.
5. Catalog registry-only, NoEffect asset 검증, status/file mapping 검증을 자동화한다.
6. docs의 최신 상태와 과거 기록을 분리한다.
7. 그 뒤 ST2/ST3 pass 구현을 재개한다.

## 9. 테스트

이번 감사 작업은 문서 생성만 수행한다. 구현 코드 변경을 새로 하지 않았으므로 전체 테스트는 실행하지 않는다.

단, 작업 시작 시 이미 존재하던 uncommitted 코드 변경은 별도 검증 대상이다. 해당 변경을 commit하거나 이어서 개발하기 전에는 전체 테스트와 구조 검증 테스트가 필요하다.

## 10. 결론

현재 RL.Engine 구조는 원본 CardEffect별 파일 구조로 정렬되는 방향은 맞다. 가장 큰 문제는 ST2/ST3 확장 전 검증 장치가 부족하고, 원본 asset의 shared-effect 정보를 NoEffect로 오분류할 위험이 있다는 점이다.

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

- ST2-07/ST3-07 shared `ST1_06` mapping 문제는 아직 기능 구현으로 해결하지 않았다.
- ST3-02 variant `CardEffectClassName` 불일치 가능성은 별도 감사가 필요하다.
- docs의 과거/최신 상태 분리는 아직 추가 정리가 필요하다.
