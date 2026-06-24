# QUEUE_FOUNDATION_COMPLETION

## 2026-06-23 Restart Queue Update

- `DATA-001` closed: CardBaseEntity/variant/EffectClassName policy artifacts are available under `docs/as-is-restart/` and `docs/generated/as-is-restart/`.
- `FND-003-A` closed: 15 `ImplementableFoundationTask` entries are split into 45 child goals in `docs/generated/as-is-restart/fnd-003-a-foundation-goal-queue.json`.
- `FND-003-B OnRemovedField` implemented in primitive destroy scope and verified by full regression (`All 612 tests passed.`).
- `FND-003-C AfterPayCost` implemented in standard play/option/digivolve cost-payment scope and verified by full regression (`All 613 tests passed.`).
- `FND-003-D OnDiscardSecurity` implemented in primitive security-trash event scope and verified by full regression (`All 614 tests passed.`).
- `FND-003-E OnAddSecurity` implemented in primitive security-add/recovery event scope and verified by full regression (`All 615 tests passed.`).
- `FND-003-F OnDiscardLibrary` implemented in primitive deck-trash event scope and verified by full regression (`All 616 tests passed.`).
- `FND-003-G OnUseOption` implemented in hand-option runtime scope and verified by full regression (`All 617 tests passed.`).
- `FND-003-H OnUnTappedAnyone` implemented in primitive unsuspend event scope and verified by full regression (`All 618 tests passed.`).
- `FND-003-I OnMove` implemented in primitive permanent-move event scope and verified by full regression (`All 619 tests passed.`).
- `FND-003-J OnAddDigivolutionCards` implemented in primitive source-add event scope and verified by full regression (`All 620 tests passed.`).
- `FND-003-K OnDigivolutionCardDiscarded` implemented in primitive source-trash event scope and verified by full regression (`All 621 tests passed.`).
- `FND-003-L OnEndBattle` implemented in battle-result runtime scope and verified by full regression (`All 623 tests passed.`).
- `FND-003-M OnDetermineDoSecurityCheck` implemented in battle security-check decision runtime scope and verified by full regression (`All 626 tests passed.`).
- `FND-003-N BeforePayCost` implemented in pre-cost play/option/digivolution runtime scope and verified by full regression (`All 629 tests passed.`).
- `FND-003-O OnTappedAnyone` implemented in actual suspend primitive and attack/block immediate-drain runtime scope and verified by full regression (`All 630 tests passed.`).
- `FND-003-P OnDeclaration` implemented in legal action generation and selected declaration execution scope and verified by full regression (`All 633 tests passed.`).
- `FND-001-A / FND001-CS-02 EffectTiming.None alias gate accounting` closed by evidence: raw `None` capability is absent from registry/source-required/batch-blocker/gate samples, and `None` is owned only by `ContinuousOrStaticEffect.inventoryAliases`.
- `FND-001-A / FND001-CS-03 continuous/static source collector scope parity` closed by evidence: `FieldTop`, `InheritedSource`, `LinkedCard`, `FaceUpSecurity`, `Hand`, `Trash`, and `Executing` source kinds are mapped to original source surfaces, headless collector roles, existing tests, and full-card scaffold fixture candidates. Strict Unity ordering parity remains assigned to `FND001-CS-14`, and executable full-card parity remains assigned to `PARITY-001`.
- `FND-001-A / FND001-CS-04 duration bucket cleanup/provider integration parity` closed by evidence: source duration buckets 7/7 and headless cleanup buckets 7/7 are mapped to original `Player`/`Permanent`/`TurnStateMachine`/`AttackProcess` plus cleanup callsite `CardController` evidence. Production provider catalog adoption remains assigned to TRUST-001-RERUN/FND-005, and strict bucket-name parity remains a source-mapping boundary.
- `FND-001-A / FND001-CS-06 supported static keyword descriptor parity` closed by evidence: supported static keyword wrappers `Blocker`, `Rush`, `Reboot`, `Collision`, and `Jamming` are mapped to original KeyWordEffects files/classes, headless `ContinuousKeywordDescriptor` wrappers, runtime keyword services, 470 source sample records, and 12 existing test candidates. Unsupported trigger/process keyword static factory mapping remains assigned to `FND001-CS-07`, and executable full-card parity remains assigned to `PARITY-001`.
- `FND-001-A / FND001-CS-08 static DP/SecurityAttack/SecurityDigimonDP descriptor parity` closed by evidence: source modifier groups 4/4 and headless runtime modifier kinds 3/3 are mapped to DP, SecurityAttack, and SecurityDigimonDP descriptor/runtime evidence with 532 source sample records, 554 factory references, and 12 existing test candidates. `ChangeBaseDPStaticEffect` exact origin-DP set semantics remains assigned to `PARITY-001`/TRUST rerun evidence.
- `FND-001-A / FND001-CS-11 static evolution/link requirement effective gates` closed by evidence: source requirement groups 3/3 and headless descriptor/runtime groups 3/3 are mapped to original AddDigivolutionRequirement/AddLinkRequirement/IgnoreDigivolutionRequirement surfaces with 1196 source sample records, 1257 factory references, and 15 existing test candidates. Missing headless source-facing link factory wrapper, direct `IgnorePermission` scaffold count 0, and executable full-card parity remain assigned to follow-up mapping/parity/TRUST work.
- All first-group FND-001 CloseableFoundationTask items are now closed by evidence.
- `PARITY-001` closed by plan/evidence: full-card parity remains `Passed 0`, `Failed 0`, `NotRun 3918`, and `docs/generated/as-is-restart/parity-001-full-card-fixture-reduction-plan.json` defines a 100-item source-locked candidate queue plus the required Unity fixture / RL fixture / comparison report contract. This does not reduce NotRun and does not permit card-porting.
- `TRUST-001-RERUN` closed by static evidence: current `src` C# file count is 340, with `CostPaymentRuleEventPayload.cs` and `DeclarationEffectService.cs` added since TRUST-001 and classified `PartialNeedsWork`. DATA-001 closed data-policy blockers were reflected, so `BlockedByDataPolicy=0`, but trusted-as-Verified remains 0 and CardEffectLocalScript 185 files remain `BlockedByFoundation`.
- `FND-002-RERUN` refreshed by generated evidence: original FND-002 `NeedsSourceReview` timing set moved from 27 to 0 after FND-002-A/B/C/D/E, with 15 FND-003-B through FND-003-P timing slices recorded as `PartiallyImplemented` rather than `Verified`, 10 source-mapped timings recorded as `Unsupported`, `OnFaceUpSecurityIncreased` recorded as `SourceMappedDataPolicyPartial`, and `OnStartBattle` recorded as `SourceKnownZeroCardTiming`.
- `FND-002-A` closed by source mapping evidence: replacement/cut-in timings `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, and `WhenWouldDigivolutionCardDiscarded` moved from original FND-002 `NeedsSourceReview` into source-mapped `Unsupported` blockers classified as `SourceMappedUnsupportedFoundation`. This is not runtime implementation.
- `FND-002-B` closed by source mapping evidence: link lifecycle timings `WhenWouldLink`, `WhenLinked`, and `OnLinkCardDiscarded` moved from original FND-002 `NeedsSourceReview` into source-mapped `Unsupported` blockers classified as `SourceMappedUnsupportedFoundation`. This is not runtime implementation.
- `FND-002-C` closed by source mapping/policy evidence: `WhenDigisorption` and `OnUseDigiburst` moved from original FND-002 `NeedsSourceReview` into source-mapped `Unsupported` blockers classified as `SourceMappedUnsupportedFoundation`. This is not Digisorption/DigiBurst runtime implementation and does not permit card body porting.
- `FND-002-D` closed by source mapping/data-policy evidence: `OnFaceUpSecurityIncreased` moved from original FND-002 `NeedsSourceReview` into source-mapped `PartiallyImplemented` data-policy foundation classified as `SourceMappedDataPolicyPartial`. Headless face-up `AddSecurity` event ordering exists, while source-aligned `IFlipSecurity` conversion and EX11-004 parity remain follow-ups.
- `FND-002-E` closed by source mapping/manual-review evidence: `OnStartBattle` moved from original FND-002 `NeedsSourceReview` into source-known `NotReferenced` for the current AS-IS card pool and is classified as `SourceKnownZeroCardTiming`. Current `CardEffect` references are 0 and affected cards are 0; future source references reopen a battle-start payload fixture task.
- `FND-003-Q` closed by source-aligned reconciliation evidence: `AfterPayCost`, `BeforePayCost`, `OnDeclaration`, `OnEndBattle`, and `OnMove` had FND-003 implementation/test evidence and `full-mechanic-inventory` `PartiallyImplemented` status, so capability truth audit no longer keeps them as `Unsupported`. They remain `PartiallyImplemented`, not `Verified`, because full-card parity is still `NotRun`.
- `FND-003-R` closed by source-aligned replacement/cut-in foundation evidence: `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, and `WhenWouldDigivolutionCardDiscarded` now have pre-mutation replacement window request APIs and tests, so capability truth audit no longer keeps them as `Unsupported`. They remain `PartiallyImplemented`, not `Verified`, because replacement continuation, target re-fix, and full-card parity remain incomplete.
- Foundation Gate remains `OpenCodeReady=false`; generated status was not promoted.
- Next recommended queue items: `FND-003-S` link lifecycle unsupported remediation, `FND-003-T` Digisorption/DigiBurst unsupported remediation, `FND-003-R1/R2` replacement continuation/target re-fix follow-up, `FND-001` partial closure continuation, and `PARITY-001-A` Unity full-card fixture exporter scenario contract. Remaining capability/data-policy tasks remain blocked follow-ups, not C0039 card-porting permission.

이 문서는 Script Runtime Foundation 완성 작업을 추적하는 전용 queue다.
`QUEUE_FULL_CARD_PORTING_BATCHES.md`에 섞여 있던 66B~66AE foundation-remediation 흐름은 이 문서에서 foundation 작업으로 다시 분류한다.

## 운영 규칙

- 이 queue는 `docs/codex-prompts/ACTIVE/RUN_NEXT_FOUNDATION_COMPLETION.md`와 함께 Foundation Completion 작업의 우선 기준으로 사용한다.
- `QUEUE_FULL_CARD_PORTING_BATCHES.md`의 66B~66J, 66W~66AE 항목과 Foundation active/progress 문서에 이어진 66K~66V 항목은 foundation-remediation 이력이다. 이 이력은 C0039 이후 card-porting 실행 허가로 해석하지 않는다.
- C0039 및 그 이후 generated full-card prompt는 `OpenCodeReady=true`가 되기 전까지 실행 금지다.
- `OpenCodeReady=false` 상태에서는 개별 CardEffect body 구현, 신규 카드 포팅, ST/BT/EX/P batch 확장을 하지 않는다.
- 문서/queue만 변경한 작업은 테스트를 생략할 수 있다. 단, 생략 사유를 progress 문서와 완료 보고에 기록한다.
- 파일 이동, namespace 변경, public facade 재배치는 별도 구현 작업으로만 수행한다. 이 문서는 장기 target plan만 기록한다.

## 현재 Foundation Gate 기준선

출처:

- `docs/rl-engine/foundation-completion-gate.md`
- `docs/generated/foundation-completion-gate.json`

| 항목 | 현재값 |
| --- | --- |
| OpenCodeReady | false |
| Passed gates | 12 |
| Failed gates | 2 |
| Selected next foundation capability | ContinuousOrStaticEffect |
| Selected next foundation status | PartiallyImplemented |
| Unknown common API count | 0 |
| Unsupported capability count | 5 |
| PartiallyImplemented capability count | 58 |
| Runtime/generated status mismatch count | 0 |
| Silent no-op count | 0 |
| Direct zone mutation count | 0 |

## 실패 gate와 다음 foundation 순서

| 순서 | 작업 ID | 실패 gate | 현재 수치 | 작업 방향 | 완료 기준 |
| --- | --- | --- | ---: | --- | --- |
| 1 | FND-001 | `partial-capabilities-zero` | 58 | Gate가 선택한 `ContinuousOrStaticEffect`를 먼저 줄인다. static/continuous descriptor, source scope, player/permanent/runtime modifier 연결이 실제 runtime 계약과 맞는지 확인하고 partial capability를 더 작은 구현 단위로 분해한다. | `ContinuousOrStaticEffect` 관련 partial 사유가 명시적으로 해소되거나 남은 blocker가 독립 queue 항목으로 분리된다. |
| 2 | FND-003 | `unsupported-capabilities-zero` | 5 | Unsupported capability를 silent no-op 없이 실패시키는 현재 원칙을 유지하면서, 각 capability를 구현 가능한 foundation prompt로 분해한다. link lifecycle, Digisorption/DigiBurst 계열을 우선 검토한다. | unsupported capability가 구현 완료, partial 전환, 또는 explicit blocked 상태로 재분류된다. |
| 3 | FND-004 | alignment prep | n/a | 장기 path/namespace alignment RFC를 별도 구현 전 사전 검토한다. 이 작업은 코드 이동 없이 target 구조, compatibility shim, 테스트 범위만 확정한다. | 파일 이동 전 compatibility plan과 테스트 계획이 문서화된다. |
| 4 | FND-005 | effect registry service graph prep | partial | 원본 `CEntity_EffectController.AddCardEffect(ID, ClassName)` 계열은 명시 `ICEntityEffectRegistry`/`CEntityEffectRegistry` 기반 `CardSource/Permanent.EffectList` facade와 `BattleEngineServices` root dependency까지 연결됐다. `CEntityEffectRegistry.FromEntries(...)`는 source-aligned `(CardId, EffectClassName, factory)` entry를 deterministic lookup key로 확장하고, `CEntityEffectRegistryBuilder`는 `CardEffectPortingRecord.EffectiveSourceEffectClassName` metadata에서 entry 목록을 만든다. `CEntityEffectFactoryCatalog`와 `BattleEngineServices.Create(ICardScriptRegistry, CEntityEffectFactoryCatalog, ...)` overload는 production service graph가 catalog에서 registry를 조립하는 경계를 제공한다. `ICEntityEffectFactoryProvider`와 `CEntityEffectFactoryCatalog.FromScripts(...)`는 bridge script registration이 catalog를 채우는 경로를 검증했으며, missing/multiple/null provider는 명시 실패한다. `Permanent.EffectList_Added(timing, TemporaryGrantedEffectRegistry)`와 `Player.EffectList(timing, TemporaryGrantedEffectRegistry)`는 duration-scoped `TemporaryGrantedEffect`를 descriptor-backed `ICardEffect`로 노출한다. 후속은 실제 production card script catalog의 provider 채택과 original `PermanentEffects`/`Until*` bucket cleanup parity다. | CardEffect body 구현 없이 bridge registration, dependency injection 경계, missing class explicit failure, duration-scoped target-permanent/player granted effect facade가 검증됐다. 실제 production catalog 채택은 별도 후속 작업으로만 진행한다. |

## 66B~66AE foundation-remediation 이력

이 표는 full-card queue와 Foundation active/progress 문서에 섞여 있던 foundation-remediation 항목을 Foundation queue 관점으로 재정리한 것이다.
`done`은 해당 remediation prompt가 완료되었다는 뜻이며, Foundation Gate 전체 완료 또는 card-porting 허가를 뜻하지 않는다.

현재 `QUEUE_FULL_CARD_PORTING_BATCHES.md`에는 66B~66J와 66W~66AE가 직접 행으로 남아 있고, 66K~66V는 `docs/codex-prompts/ACTIVE/RUN_NEXT_FOUNDATION_COMPLETION.md` 및 각 `docs/rl-engine/*-66*.md` scope 문서에 이어진 foundation remediation 이력으로 보존되어 있다.

| 항목 | 상태 | Foundation 분류 | 출처 | 현재 의미 |
| --- | --- | --- | --- | --- |
| 66B | done | capability truth audit | `docs/codex-prompts/prompts/66B_capability_truth_audit.md` | generated capability truth audit를 만들었고, C0039 실행 불가와 generated/code mismatch blocker를 확인했다. |
| 66C | done | runtime status variant registry | `docs/codex-prompts/prompts/66C_runtime_status_variant_registry_integration.md` | CardId-only registry blocker를 제거하고 runtime status variant registry를 정리했다. |
| 66D | done | capability dependency graph | `docs/codex-prompts/prompts/66D_card_effect_capability_dependency_graph.md` | card-porting batch는 관련 capability가 Verified일 때만 진행한다는 dependency 기준을 세웠다. |
| 66E | done | mechanic-first scheduler | `docs/codex-prompts/prompts/66E_mechanic_first_goal_scheduler.md` | individual card order보다 mechanic-first remediation 순서를 우선하도록 정리했다. |
| 66F | done | continuous/static source scope | `docs/codex-prompts/prompts/66F_continuous_static_source_scope.md` | linked source와 face-up security source를 포함한 ContinuousOrStaticEffect source scope를 확장했다. |
| 66G | done | player runtime static modifier | `docs/codex-prompts/prompts/66G_player_runtime_static_modifier_scope.md` | player-level DP, SecurityAttack, SecurityDigimonDP 임시 static modifier 기반을 정리했다. |
| 66H | done | hand/trash/executing static source | `docs/codex-prompts/prompts/66H_hand_trash_executing_static_source_scope.md` | hand, trash, executing source collection을 ContinuousOrStaticEffect 경로에 포함했다. |
| 66I | done | continuous/static keyword descriptor | `docs/codex-prompts/prompts/66I_continuous_static_keyword_descriptor_scope.md` | static keyword descriptor 기반을 정리했다. |
| 66J | done | static requirement descriptor | `docs/codex-prompts/prompts/66J_static_requirement_descriptor_scope.md` | static digivolution/link requirement descriptor 기반을 정리했다. |
| 66K | done | metadata criteria scope | `docs/rl-engine/continuous-static-metadata-criteria-scope-66K.md` | trait/name/text metadata 조건을 ContinuousOrStaticEffect foundation API로 분리한 scope 문서를 보존한다. |
| 66L | done | cost/restriction/immunity interface | `docs/rl-engine/continuous-static-cost-restriction-immunity-scope-66L.md` | cost modifier, attack/block restriction, immunity interface foundation scope 문서를 보존한다. |
| 66M | done | ignore digivolution permission | `docs/rl-engine/continuous-static-ignore-digivolution-permission-scope-66M.md` | ignore digivolution permission semantics를 foundation remediation 이력으로 보존한다. |
| 66N | done | temporary keyword grant | `docs/rl-engine/continuous-static-temporary-keyword-grant-scope-66N.md` | temporary keyword grant scope를 foundation remediation 이력으로 보존한다. |
| 66O | done | capability truth evidence refresh | `docs/rl-engine/capability-truth-evidence-refresh-scope-66O.md` | capability truth evidence refresh를 foundation remediation 이력으로 보존한다. |
| 66P | done | full-card parity evidence | `docs/rl-engine/full-card-parity-evidence-scope-66P.md` | full-card parity evidence 정리를 foundation remediation 이력으로 보존한다. |
| 66Q | done | generated/runtime mismatch closure | `docs/rl-engine/generated-runtime-status-mismatch-closure-scope-66Q.md` | generated/runtime status mismatch closure를 foundation remediation 이력으로 보존한다. |
| 66R | done | cannot-ignore digivolution restriction | `docs/rl-engine/continuous-static-cannot-ignore-digivolution-restriction-scope-66R.md` | cannot-ignore digivolution restriction foundation 작업을 보존한다. |
| 66S | done | player-wide keyword grant | `docs/rl-engine/continuous-static-player-wide-keyword-grant-scope-66S.md` | player-wide keyword grant foundation 작업을 보존한다. |
| 66T | done | temporary granted trigger | `docs/rl-engine/continuous-static-temporary-granted-trigger-scope-66T.md` | temporary granted trigger foundation 작업을 보존한다. |
| 66U | done | blocked empty descriptor scope | `docs/rl-engine/foundation-gate-blocked-empty-descriptor-scope-66U.md` | blocked empty descriptor scope 정리를 foundation 이력으로 보존한다. |
| 66V | done | EffectTiming.None continuous/static alias | `docs/rl-engine/effect-timing-none-continuous-static-alias-scope-66V.md` | `EffectTiming.None` continuous/static alias 정리를 foundation 이력으로 보존한다. |
| 66W | done | OnEnterFieldAnyone payload | `docs/codex-prompts/prompts/66W_on_enter_field_anyone_payload_scope.md` | chained global enter-field payload 기반을 정리했다. C0039는 계속 차단이다. |
| 66X | done | static card color requirement | `docs/codex-prompts/prompts/66X_static_card_color_requirement_scope.md` | static card color requirement 기반을 정리했다. C0039는 계속 차단이다. |
| 66Y | done | static card metadata requirement | `docs/codex-prompts/prompts/66Y_static_card_metadata_requirement_scope.md` | static card metadata requirement 기반을 정리했다. C0039는 계속 차단이다. |
| 66Z | done | static level requirement | `docs/codex-prompts/prompts/66Z_static_level_requirement_scope.md` | static level requirement 기반을 정리했다. C0039는 계속 차단이다. |
| 66AA | done | static link effective gate | `docs/codex-prompts/prompts/66AA_static_link_effective_gate_scope.md` | static link effective gate 기반을 정리했다. C0039는 계속 차단이다. |
| 66AB | done | static link cost modifier | `docs/codex-prompts/prompts/66AB_static_link_cost_modifier_scope.md` | static link cost modifier 기반을 정리했다. C0039는 계속 차단이다. |
| 66AC | done | static cannot-play option | `docs/codex-prompts/prompts/66AC_static_cannot_play_option_scope.md` | static cannot-play option 기반을 정리했다. C0039는 계속 차단이다. |
| 66AD | done | static cannot-put-field | `docs/codex-prompts/prompts/66AD_static_cannot_put_field_scope.md` | static cannot-put-field 기반을 정리했다. C0039는 계속 차단이다. |
| 66AE | done | static cannot-move return-to-hand | `docs/codex-prompts/prompts/66AE_static_cannot_move_return_to_hand_scope.md` | static cannot-move return-to-hand 기반을 정리했다. C0039는 계속 차단이다. |

## C0039 이후 card-porting 금지

현재 `QUEUE_FULL_CARD_PORTING_BATCHES.md`의 C0039 이후 항목은 실행 대기열이 아니라 future card-porting backlog다.

- `docs/codex-prompts/prompts/generated/full-card/C0039_zone_security_recovery.md` 및 이후 generated prompt는 Foundation Gate가 열리기 전까지 실행하지 않는다.
- C0039 이후 작업을 시작하려면 먼저 `OpenCodeReady=true`, failed gate 0, explicit user approval이 필요하다.
- Foundation 작업 중 source sample, generated status, card ID를 참조할 수는 있지만 card body를 구현하거나 batch를 진행하지 않는다.

## 장기 path/namespace alignment plan

이번 작업에서는 실제 코드 이동을 하지 않는다. 아래 계획은 향후 별도 implementation/RFC 작업의 target 구조다.

| 현재 파일 | 현재 역할 | 장기 target 방향 | 호환성 원칙 |
| --- | --- | --- | --- |
| `src/DCGO.RL.Engine/CardEffects/CardEffectCommons.cs` | 원본 `CardEffectCommons` API와 headless primitive/service 사이의 source-aligned facade | public facade 이름은 유지하되 helper 그룹별로 `CardEffects/Commons/` 또는 내부 service namespace로 분리한다. 원본 helper명은 source alignment를 위해 facade에서 계속 노출한다. | 기존 `DCGO.RL.Engine.CardEffects.CardEffectCommons` 호출 경로를 먼저 안정화하고, 내부 이동은 partial/shim을 통해 단계적으로 수행한다. |
| `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs` | 원본 `CardEffectFactory`의 primitive descriptor facade | DP, keyword, requirement, trigger/process restriction 그룹을 `CardEffects/Factory/` 하위 파일 또는 partial 파일로 나눈다. public type과 source-facing static method 이름은 유지한다. | generated/runtime mismatch가 0인 상태를 깨지 않도록 모든 이동은 compile compatibility test와 descriptor snapshot test 이후 수행한다. |
| `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs` | `GameContext`, `Player`, `CardSource`, `Permanent`, `CEntity_Base` 등 runtime state facade 집합 | 장기적으로 `Domain/ScriptRuntime/` 아래 state facade별 파일로 분해한다. namespace는 기존 `DCGO.RL.Engine.Domain` 호환을 우선하고, 필요하면 새 내부 namespace만 추가한다. | Unity 원본 이름과 headless domain primitive 경계를 분리하되, public facade rename은 마지막 단계에서만 검토한다. |
| `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs` | `ICardEffect`, `ActivateICardEffect`, `CEntity_Effect`, `CEntity_EffectController`, `EffectTiming` 등 effect model facade 집합 | 장기적으로 `Effects/ScriptRuntime/` 또는 `Effects/Foundation/` 아래 model, timing, controller facade 파일로 분리한다. `CEntityEffectRegistry`는 이미 별도 파일로 분리했으며, 장기 target에서는 `Effects/ScriptRuntime/CEntityEffectRegistry.cs` 또는 `Effects/Foundation/CEntityEffectRegistry.cs`로 옮기는 후보로 둔다. | effect timing/status contract가 generated documents와 일치해야 한다. namespace churn보다 source-aligned facade 안정성을 우선한다. |

Alignment 순서는 다음을 따른다.

1. 문서상 target 구조와 compatibility shim 전략을 먼저 확정한다.
2. generated gate와 foundation contract가 참조하는 public facade/type 이름을 고정한다.
3. 내부 helper만 작은 단위로 이동하고, public facade는 기존 path/namespace에서 forwarding한다.
4. 테스트가 충분해진 뒤에만 namespace 재배치를 검토한다.
5. file move 작업은 Foundation Gate remediation과 섞지 않고 별도 queue 항목으로 진행한다.
