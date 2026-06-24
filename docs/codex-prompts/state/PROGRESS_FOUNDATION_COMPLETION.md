# PROGRESS_FOUNDATION_COMPLETION

## 2026-06-24 FND-003-R Replacement / Cut-In Foundation Update

- `FND-003-R replacement/cut-in foundation scope` completed by source-aligned implementation and generated evidence.
- Generator: `scripts/generate_fnd003r_replacement_cut_in_foundation_scope.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-003-r-replacement-cut-in-foundation-scope.json`, `docs/generated/as-is-restart/fnd-003-r-next-action-queue.json`, `docs/as-is-restart/FND_003_R_REPLACEMENT_CUT_IN_FOUNDATION_SCOPE.md`, `docs/as-is-restart/fnd-003-r-replacement-cut-in-foundation-scope-summary.md`.
- Implemented foundation boundary: `QueuePermanentRemovalReplacementWindow`, `QueueDigivolutionCardDiscardReplacementWindow`, and `QueueUnsuspendReplacementWindow` queue source-aligned would/replacement payloads before mutation for `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, and `WhenWouldDigivolutionCardDiscarded`.
- Classification counts: `SourceAlignedReplacementWindowPartial` 5. All 5 targets moved from previous source-mapped `Unsupported` to current `PartiallyImplemented`.
- Boundary: this is not `Verified`, does not execute replacement bodies, does not implement cut-in continuation or target re-fix, and does not reduce full-card parity `NotRun`.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 12, failed gate 2, unknown common API 0, unsupported capability 5, partially implemented capability 58, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Remaining unsupported capabilities: `OnLinkCardDiscarded`, `OnUseDigiburst`, `WhenDigisorption`, `WhenLinked`, `WhenWouldLink`.
- Verification: `python -m py_compile scripts\generate_full_mechanic_inventory.py scripts\generate_capability_truth_audit.py scripts\generate_fnd003r_replacement_cut_in_foundation_scope.py`; `.\\.dotnet\\dotnet.exe build .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> warning 0/error 0; `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 643 tests passed.`
- Prohibited actions kept: no original `DCGO/Assets` modification, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-003-S` link lifecycle unsupported remediation, `FND-003-T` Digisorption/DigiBurst unsupported remediation, `FND-003-R1/R2` replacement continuation/target re-fix follow-up, `FND-001` partial closure continuation, and `PARITY-001-A` Unity full-card fixture exporter scenario contract.

## 2026-06-24 FND-003-Q Capability Status Reconciliation Update

- `FND-003-Q capability status reconciliation` completed by generated evidence only.
- Generator: `scripts/generate_fnd003q_capability_status_reconciliation.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-003-q-capability-status-reconciliation.json`, `docs/generated/as-is-restart/fnd-003-q-next-action-queue.json`, `docs/as-is-restart/FND_003_Q_CAPABILITY_STATUS_RECONCILIATION.md`, `docs/as-is-restart/fnd-003-q-capability-status-reconciliation-summary.md`.
- Reconciled targets: `AfterPayCost`, `BeforePayCost`, `OnDeclaration`, `OnEndBattle`, `OnMove`.
- Classification counts: `SourceAlignedPartialFoundation` 5. All 5 targets moved from previous capability truth audit `Unsupported` to current `PartiallyImplemented` based on completed FND-003 implementation/test evidence and full-mechanic inventory partial status.
- Boundary: this is not `Verified`, does not promote generated card status, and does not reduce full-card parity `NotRun`. Full-card parity, card-specific body semantics, and remaining edge variants stay as follow-up work.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 12, failed gate 2, unknown common API 0, unsupported capability 10, partially implemented capability 53, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Remaining unsupported capabilities: `OnLinkCardDiscarded`, `OnUseDigiburst`, `WhenDigisorption`, `WhenLinked`, `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`, `WhenWouldLink`.
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Tests were not executed for this evidence-only reconciliation; `python -m py_compile scripts\generate_capability_truth_audit.py scripts\generate_fnd003q_capability_status_reconciliation.py`, JSON validation, and Foundation Gate recalculation were used instead.
- Next recommended work: `FND-003-R` replacement/cut-in unsupported remediation, `FND-003-S` link lifecycle unsupported remediation, `FND-003-T` Digisorption/DigiBurst unsupported remediation, `FND-001` partial closure continuation, and `PARITY-001-A` Unity full-card fixture exporter scenario contract.

## 2026-06-24 FND-002-E OnStartBattle Manual Review Update

- `FND-002-E OnStartBattle manual review` completed by generated evidence only.
- Generator: `scripts/generate_fnd002e_on_start_battle_manual_review.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-e-on-start-battle-manual-review.json`, `docs/generated/as-is-restart/fnd-002-e-next-action-queue.json`, `docs/as-is-restart/FND_002_E_ON_START_BATTLE_MANUAL_REVIEW.md`, `docs/as-is-restart/fnd-002-e-on-start-battle-manual-review-summary.md`.
- Target timing: `OnStartBattle`.
- Source evidence: `ICardEffect.cs` and `CardController.cs` battle start flow. Source payload keys are `AttackingPermanent`, `DefendingPermanent`, and `DefendingCard`.
- Classification counts: `SourceKnownZeroCardTiming` 1. The target moved from original FND-002 `NeedsSourceReview` to current `NotReferenced` for the current AS-IS card pool.
- Boundary: current `CardEffect` source references are 0 and affected cards are 0. If a future source snapshot adds `EffectTiming.OnStartBattle` card references, the timing reopens as a battle-start payload fixture task.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 12, failed gate 2, unknown common API 0, unsupported capability 15, partially implemented capability 48, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-003` unsupported capability continuation, `FND-001` partial closure continuation, and `PARITY-001-A` Unity full-card fixture exporter scenario contract.

## 2026-06-24 FND-002-D Face-Up Security Data Policy Update

- `FND-002-D face-up security data policy` completed by generated evidence only.
- Generator: `scripts/generate_fnd002d_face_up_security_data_policy.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-d-face-up-security-data-policy.json`, `docs/generated/as-is-restart/fnd-002-d-next-action-queue.json`, `docs/as-is-restart/FND_002_D_FACE_UP_SECURITY_DATA_POLICY.md`, `docs/as-is-restart/fnd-002-d-face-up-security-data-policy-summary.md`.
- Target timing: `OnFaceUpSecurityIncreased`.
- Source evidence: `ICardEffect.cs`, `CardController.cs` `IAddSecurity`/`IFlipSecurity`, `CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs`, `CardSource.cs`, and source body `EX11_004.cs`.
- Classification counts: `SourceMappedDataPolicyPartial` 1. The target moved from original FND-002 `NeedsSourceReview` to current source-mapped `PartiallyImplemented`.
- Boundary: headless face-up `AddSecurity` event ordering exists, but source-aligned `IFlipSecurity` conversion, EX11-004 body parity, and Unity/RL full-card replay evidence remain incomplete.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, unknown common API 1, unsupported capability 15, partially implemented capability 48, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-002-E` OnStartBattle manual review, `FND-003` unsupported capability continuation, and `FND-001` partial closure continuation.

## 2026-06-24 FND-002-C Digisorption / DigiBurst Timing Policy Update

- `FND-002-C digisorption/digiburst timing policy` completed by generated evidence only.
- Generator: `scripts/generate_fnd002c_digisorption_digiburst_policy.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-c-digisorption-digiburst-timing-policy.json`, `docs/generated/as-is-restart/fnd-002-c-next-action-queue.json`, `docs/as-is-restart/FND_002_C_DIGISORPTION_DIGIBURST_TIMING_POLICY.md`, `docs/as-is-restart/fnd-002-c-digisorption-digiburst-timing-policy-summary.md`.
- Target timings: `WhenDigisorption`, `OnUseDigiburst`.
- Source evidence: `ICardEffect.cs`, `CardSource.cs`, `Player.cs`, `CardController.cs`, `CardEffectInterfaces.cs`, `CanSuspendByDigisorptionClass.cs`, `WhenUseDigiBurst.cs`, `OnTrashBySelfDigiBurst.cs`, and sample source bodies `BT2_045.cs`, `BT3_056.cs`, `BT5_056.cs`, `BT5_004.cs`.
- Classification counts: `SourceMappedUnsupportedFoundation` 2. Both targets moved from original FND-002 `NeedsSourceReview` to current source-mapped `Unsupported`.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, unknown common API 2, unsupported capability 26, partially implemented capability 37, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-002-D` face-up security data policy, `FND-002-E` OnStartBattle manual review, and `FND-003` unsupported capability continuation.

## 2026-06-24 FND-002-B Link Lifecycle Source Mapping Update

- `FND-002-B link lifecycle source mapping` completed by generated evidence only.
- Generator: `scripts/generate_fnd002b_link_lifecycle_mapping.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-b-link-lifecycle-source-mapping.json`, `docs/generated/as-is-restart/fnd-002-b-next-action-queue.json`, `docs/as-is-restart/FND_002_B_LINK_LIFECYCLE_SOURCE_MAPPING.md`, `docs/as-is-restart/fnd-002-b-link-lifecycle-source-mapping-summary.md`.
- Target timings: `WhenWouldLink`, `WhenLinked`, `OnLinkCardDiscarded`.
- Source evidence: `ICardEffect.cs`, `CardController.cs`, `Permanent.cs`, `CardEffectCommons/HashtableSetting.cs`, `CardEffectCommons/CanUseEffects/WhenLinked.cs`, `CardEffectCommons/CanUseEffects/WhenWouldLink.cs`, and `CardEffectFactory.cs`.
- Classification counts: `SourceMappedUnsupportedFoundation` 3. All 3 targets moved from original FND-002 `NeedsSourceReview` to current source-mapped `Unsupported`.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, unknown common API 4, unsupported capability 26, partially implemented capability 37, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-002-C` digisorption/digiburst timing policy, `FND-002-D` face-up security data policy, `FND-002-E` OnStartBattle manual review, and `FND-003` unsupported capability continuation.

## 2026-06-24 FND-002-A Replacement/Cut-In Source Mapping Update

- `FND-002-A replacement/cut-in source mapping` completed by generated evidence only.
- Generator: `scripts/generate_fnd002a_replacement_cut_in_mapping.py`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-a-replacement-cut-in-source-mapping.json`, `docs/generated/as-is-restart/fnd-002-a-next-action-queue.json`, `docs/as-is-restart/FND_002_A_REPLACEMENT_CUT_IN_SOURCE_MAPPING.md`, `docs/as-is-restart/fnd-002-a-replacement-cut-in-source-mapping-summary.md`.
- Target timings: `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`.
- Source evidence: `ICardEffect.cs`, `Permanent.cs`, `CardController.cs`, `CardEffectCommons/HashtableSetting.cs`, `CardEffectCommons/CanUseEffects/WhenRemoveField.cs`, and keyword/factory references for Barrier, Evade, ArmorPurge, Decode, Fragment, and MaterialSave.
- Classification counts: `SourceMappedUnsupportedFoundation` 5. All 5 targets moved from original FND-002 `NeedsSourceReview` to current source-mapped `Unsupported`.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, unknown common API 7, unsupported capability 26, partially implemented capability 37, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-002-B` link lifecycle source mapping, `FND-002-C` digisorption/digiburst timing policy, `FND-002-D` face-up security data policy, `FND-002-E` OnStartBattle manual review, and `FND-003` unsupported capability continuation.

## 2026-06-24 FND-002-RERUN Update

- `FND-002-RERUN unknown common API source mapping refresh` completed by generated evidence only.
- Generator: `scripts/rerun_fnd002_unknown_common_api_mapping.py`.
- Inventory generator update: `scripts/generate_full_mechanic_inventory.py` records FND-003-B through FND-003-P timing slices as `PartiallyImplemented` rather than `Verified`, because full-card parity remains `NotRun 3918`.
- Output artifacts: `docs/generated/as-is-restart/fnd-002-rerun-unknown-common-api-mapping.json`, `docs/generated/as-is-restart/fnd-002-rerun-next-action-queue.json`, `docs/as-is-restart/FND_002_RERUN_UNKNOWN_COMMON_API_SOURCE_MAPPING.md`, `docs/as-is-restart/fnd-002-rerun-unknown-common-api-mapping-summary.md`.
- Previous FND-002 NeedsSourceReview count: 27. Current original FND-002 set NeedsSourceReview count: 0 after FND-002-A/B/C/D/E. Reduction: 27.
- The 15 FND-003-B through FND-003-P timings are source-mapped partial foundation, 10 FND-002-A/B/C timings are source-mapped unsupported foundation blockers, `OnFaceUpSecurityIncreased` is source-mapped data-policy partial, and `OnStartBattle` is source-known zero-card timing.
- Remaining NeedsSourceReview items: none.
- Classification counts: SourceMappedPartialFoundation 15, SourceMappedDataPolicyPartial 1, SourceMappedUnsupportedFoundation 10, SourceKnownZeroCardTiming 1.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 12, failed gate 2, unknown common API 0, unsupported capability 15, partially implemented capability 48, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `FND-003` unsupported capability continuation, `FND-001` partial closure continuation, and `PARITY-001-A` Unity full-card fixture exporter scenario contract.

## 2026-06-24 TRUST-001-RERUN Update

- `TRUST-001-RERUN source implementation trust boundary refresh` completed by static evidence only.
- Generator: `scripts/rerun_trust001_src_implementation_trust_boundary.py`.
- Output artifacts: `docs/generated/as-is-restart/trust-001-rerun-src-implementation-trust-boundary.json`, `docs/generated/as-is-restart/trust-001-rerun-reuse-candidate-queue.json`, `docs/as-is-restart/TRUST_001_RERUN_SRC_IMPLEMENTATION_TRUST_BOUNDARY.md`, `docs/as-is-restart/trust-001-rerun-src-implementation-trust-boundary-summary.md`.
- Current src C# file count: 340. Previous TRUST-001 count was 338.
- Added files since TRUST-001: `src/DCGO.RL.Engine/Battle/CostPaymentRuleEventPayload.cs`, `src/DCGO.RL.Engine/Battle/DeclarationEffectService.cs`. Both are source-mapped to FND-003 timing evidence and classified `PartialNeedsWork`.
- DATA-001 reclassification: 17 files. CardBaseEntity data model candidates moved out of `BlockedByDataPolicy`; no-source CardEffect marker files moved to `VerifiedCandidateNeedsTest` as marker-only candidates.
- Classification counts: ReuseCandidate 56, VerifiedCandidateNeedsTest 39, PartialNeedsWork 53, BlockedByFoundation 185, BlockedByDataPolicy 0, DeleteCandidate 7, ManualReview 0, StaleOrWrongMapping 0.
- Trusted-as-Verified count remains 0. Existing src implementation is still not trusted as Verified.
- Tests were not executed in TRUST-001-RERUN; static test inventory parsed 640 test names.
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, unknown common API 27, unsupported capability 26, partially implemented capability 37, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `PARITY-001-A`, `FND001-CS-07`, `FND-002`, and `FND-003` unsupported capability continuation.

## 2026-06-24 PARITY-001 Full-Card Fixture Reduction Plan

- `PARITY-001 full-card parity evidence NotRun reduction framework` completed by plan/evidence only.
- Generator: `scripts/plan_parity001_full_card_fixture_reduction.py`.
- Output artifacts: `docs/generated/as-is-restart/parity-001-full-card-fixture-reduction-plan.json`, `docs/as-is-restart/PARITY_001_FULL_CARD_PARITY_REDUCTION_PLAN.md`, `docs/as-is-restart/parity-001-full-card-parity-reduction-summary.md`.
- Current parity evidence remains conservative: source effects 3918, affected definitions 7922, Passed 0, Failed 0, NotRun 3918, compared 0.
- Candidate queue: 100 source-locked source effects with `sourceScaffoldId`, `sourcePath`, `sourceEffectClassName`, `affectedDefinitionStableIds`, expected Unity fixture path, expected RL fixture path, and expected comparison report path.
- Missing artifacts: Unity fixture 3918, RL fixture 3918, comparison report 3918. First executable reduction target count is 0 until a real Unity exporter fixture exists.
- Boundary: `FND001-CS-07` unsupported trigger/process keyword mapping and `FND001-CS-14` strict Unity source ordering remain source-mapping follow-ups; `TRUST-001-RERUN` remains required before implementation reuse decisions are trusted.
- Verification: `python scripts\plan_parity001_full_card_fixture_reduction.py --workspace . --candidate-limit 100` generated the plan; `.\\.dotnet\\dotnet.exe build .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` passed with warning 0/error 0; `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 640 tests passed.`
- Foundation Gate recalculation: `OpenCodeReady=false`, passed gate 11, failed gate 3, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no generated status promotion, no commit/push.
- Next recommended work: `PARITY-001-A` Unity full-card fixture exporter scenario contract, `FND001-CS-07`, then `TRUST-001-RERUN`.

## 2026-06-24 FND-001-A Static Requirement Descriptor Update

- `FND001-CS-11 static evolution/link requirement effective gates` completed by evidence.
- Source evidence: `AddDigivolutionRequirement.cs`, `AddLinkRequirement.cs`, `IgnoreDigivolutionRequirement.cs`, `AddEvolutionConditionClass.cs`, `AddLinkConditionClass.cs`, `CannotIgnoreDigivolutionConditionClass.cs`, `CardEffectInterfaces.cs`, `CardSource.cs`, and `Player.cs` under `E:\headlessDCGO\DCGO\Assets`.
- Output artifacts: `scripts/verify_fnd001_static_requirement_scope.py`, `docs/generated/as-is-restart/fnd-001-cs-11-static-requirement-verification.json`, `docs/rl-engine/continuous-static-requirement-descriptor-scope-fnd001cs11.md`.
- Verification result: source requirement groups 3 / 3, headless descriptor/runtime groups 3 / 3, source sample candidates 1196, factory references 1257, source-required linked samples 1196, parity `NotRun` samples 1196, test candidates 15 / 15.
- Boundary: headless source-facing `AddSelfLinkConditionStaticEffect`/`AddLinkConditionStaticEffect` wrapper is not present, `IgnorePermission` direct scaffold count is 0, and full-card parity remains `NotRun 3918`, `Passed 0`.
- The first FND-001 CloseableFoundationTask group is now exhausted: `FND001-CS-02`, `FND001-CS-03`, `FND001-CS-04`, `FND001-CS-06`, `FND001-CS-08`, `FND001-CS-11`.
- `ContinuousOrStaticEffect` remains `PartiallyImplemented`; generated status was not promoted and Foundation Gate values were not manually changed.
- Verification: `python scripts\verify_fnd001_static_requirement_scope.py --workspace .` -> `passed=true`.
- Next recommended work: `FND001-CS-07`, `PARITY-001`, then `TRUST-001-RERUN`.
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no commit/push.

## 2026-06-24 FND-001-A Static Modifier Descriptor Update

- `FND001-CS-08 static DP/SecurityAttack/SecurityDigimonDP descriptor parity` completed by evidence.
- Source evidence: `ChangeDP.cs`, `ChangeSAttack.cs`, `ChangeCardDP.cs`, `ChangeOriginDP.cs` and their `ChangeDPClass`, `ChangeSAttackClass`, `ChangeCardDPClass`, `ChangeBaseDPClass` contracts under `E:\headlessDCGO\DCGO\Assets`.
- Output artifacts: `scripts/verify_fnd001_static_modifier_scope.py`, `docs/generated/as-is-restart/fnd-001-cs-08-static-modifier-verification.json`, `docs/rl-engine/continuous-static-modifier-descriptor-scope-fnd001cs08.md`.
- Verification result: source modifier groups 4 / 4, headless runtime modifier kinds 3 / 3, source sample candidates 532, factory references 554, source-required linked samples 532, parity `NotRun` samples 532, test candidates 12 / 12.
- Boundary: `ChangeBaseDPStaticEffect` exact origin-DP set semantics is not closed and remains assigned to `PARITY-001`/TRUST rerun evidence. `InvertSAttack*` is outside CS-08 scope.
- `ContinuousOrStaticEffect` remains `PartiallyImplemented`; generated status was not promoted and Foundation Gate values were not manually changed.
- Verification: `python scripts\verify_fnd001_static_modifier_scope.py --workspace .` -> `passed=true`.
- Next recommended work: `FND001-CS-11 static evolution/link requirement effective gates`.
- Prohibited actions kept: no original `DCGO/Assets` modification, no `src/DCGO.RL.Engine` implementation modification for this task, no individual `CardEffect` body implementation, no C0039+ card-porting, no RL component implementation, no commit/push.


## 2026-06-24 FND-001-A Static Keyword Descriptor Update

- `FND001-CS-06 supported static keyword descriptor parity` 완료: `Blocker`, `Rush`, `Reboot`, `Collision`, `Jamming` static keyword wrapper 5종을 원본 KeyWordEffects wrapper, 원본 class/interface, headless `ContinuousKeywordDescriptor`, runtime keyword service, 기존 테스트 후보와 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\KeyWordEffects\Blocker.cs`, `Rush.cs`, `Reboot.cs`, `Collision.cs`, `Jamming.cs`, 그리고 `CardEffects\BlockerClass.cs`, `RushClass.cs`, `RebootClass.cs`, `CollisionClass.cs`, `CanNotBeDestroyedByBattleClass.cs`다.
- `Jamming`은 원본에서 별도 `JammingClass`가 아니라 `CanNotBeDestroyedByBattleClass`를 통해 security Digimon battle 파괴 방지를 표현하므로 이 매핑을 evidence로 고정했다.
- 추가 산출물: `scripts/verify_fnd001_static_keyword_scope.py`, `docs/generated/as-is-restart/fnd-001-cs-06-static-keyword-verification.json`, `docs/rl-engine/continuous-static-keyword-descriptor-scope-fnd001cs06.md`.
- 검증 결과: source keyword wrapper 5 / 5, headless keyword wrapper 5 / 5, source sample candidate 470 records, factory reference 521, test candidate 12 / 12.
- Boundary: unsupported trigger/process keyword static factory mapping은 닫지 않고 `FND001-CS-07`로 남겼다. executable full-card parity는 `PARITY-001`로 남겼고 full-card parity evidence는 `NotRun 3918`, `Passed 0`이다.
- `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이며 generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- `OpenCodeReady=false` 상태를 유지했고 Foundation Gate 수치를 직접 조작하지 않았다.
- 검증: `python scripts\verify_fnd001_static_keyword_scope.py --workspace .` -> `passed=true`.
- 빌드: `.\\.dotnet\\dotnet.exe build .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> 경고 0개, 오류 0개.
- 전체 테스트: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 637 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- 다음 추천 작업: `FND001-CS-08 static DP/SecurityAttack/SecurityDigimonDP descriptor parity`.
- 금지 사항 준수: 원본 `DCGO/Assets` 수정 없음, `src/DCGO.RL.Engine` 구현 수정 없음, 개별 `CardEffect` body 구현 없음, C0039 이후 card-porting 없음, RL 구성요소 구현 없음, commit/push 없음.

## 2026-06-24 FND-001-A Duration Bucket Cleanup Update

- `FND001-CS-04 duration bucket cleanup/provider integration parity` 완료: 원본 `Player`, `Permanent`, `TurnStateMachine`, `AttackProcess`와 추가 cleanup callsite `CardController`에서 duration bucket cleanup timing을 대조했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`, `Permanent.cs`, `TurnStateMachine.cs`, `AttackProcess.cs`, `CardController.cs`, `CardEffectCommons\GiveEffect\GiveEffectToPermanentOrPlayer.cs`다.
- 추가 산출물: `scripts/verify_fnd001_duration_bucket_scope.py`, `docs/generated/as-is-restart/fnd-001-cs-04-duration-bucket-verification.json`, `docs/rl-engine/continuous-static-duration-bucket-scope-fnd001cs04.md`.
- 검증 결과: source duration bucket 7 / 7, headless cleanup bucket 7 / 7, test candidate 16 / 16.
- Boundary: production provider catalog adoption은 닫지 않고 TRUST-001-RERUN/FND-005 handoff로 남겼다. `UntilNextUntapEffects`와 `UntilOwnerActivePhaseEnd`는 timing 대응만 닫고 strict bucket-name parity는 닫지 않았다.
- `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이며 generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- `OpenCodeReady=false` 상태를 유지했고 Foundation Gate 수치를 직접 조작하지 않았다.
- 검증: `python scripts\verify_fnd001_duration_bucket_scope.py --workspace .` -> `passed=true`.
- 다음 추천 작업: `FND001-CS-06 supported static keyword descriptor parity`.
- 금지사항 준수: 원본 `DCGO/Assets` 수정 없음, `src/DCGO.RL.Engine` 구현 수정 없음, 개별 `CardEffect` body 구현 없음, C0039 이후 card-porting 없음, RL 구성요소 구현 없음, commit/push 없음.

## 2026-06-24 FND-001-A Source Collector Scope Update

- `FND001-CS-03 continuous/static source collector scope parity` 완료: `FieldTop`, `InheritedSource`, `LinkedCard`, `FaceUpSecurity`, `Hand`, `Trash`, `Executing` source kind 7개를 원본 source surface, headless collector, 기존 테스트 후보, full-card scaffold fixture 후보에 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs`, `CardSource.cs`, `Permanent.cs`, `Player.cs`, `CardController.cs`다.
- 추가 산출물: `scripts/verify_fnd001_source_collector_scope.py`, `docs/generated/as-is-restart/fnd-001-cs-03-source-collector-scope-verification.json`, `docs/rl-engine/continuous-static-source-collector-scope-fnd001cs03.md`.
- 검증 결과: source kind coverage 7 / 7, source surface coverage 5 / 5, test candidate coverage 10 / 10, fixture candidate source effects 4.
- Boundary: strict Unity source ordering parity는 닫지 않았고 `FND001-CS-14`로 남겼다. full-card parity는 `NotRun 3918`, `Passed 0`으로 유지하며 `PARITY-001` 전까지 pass 근거로 쓰지 않는다.
- `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이며 generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- `OpenCodeReady=false` 상태를 유지했고 Foundation Gate 수치를 직접 조작하지 않았다.
- 검증: `python scripts\verify_fnd001_source_collector_scope.py --workspace .` -> `passed=true`.
- 다음 추천 작업: `FND001-CS-04 duration bucket cleanup/provider integration parity`.
- 금지사항 준수: 원본 `DCGO/Assets` 수정 없음, 개별 `CardEffect` body 구현 없음, C0039 이후 card-porting 없음, RL 구성요소 구현 없음, commit/push 없음.

## 2026-06-24 FND-001-A Update

- `FND001-CS-02 EffectTiming.None alias gate accounting` 완료: raw `None` capability가 독립 capability/blocker로 남아 있지 않고 `ContinuousOrStaticEffect.inventoryAliases`에서만 소유되는지 별도 evidence로 고정했다.
- 추가 산출물: `scripts/verify_fnd001_none_alias.py`, `docs/generated/as-is-restart/fnd-001-a-none-alias-verification.json`, `docs/rl-engine/effect-timing-none-alias-fnd001a.md`.
- 검증 결과: raw `None` capability entry 0, source-required raw `None` 0, source non-verified raw `None` 0, batch blocker raw `None` 0, gate unsupported/partial raw `None` 0, alias owner 1.
- `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이며 generated status를 `Implemented` 또는 `Verified`로 승격하지 않았다.
- `OpenCodeReady=false` 상태를 유지했고 Foundation Gate 수치를 직접 조작하지 않았다.
- 검증: `python scripts\verify_fnd001_none_alias.py --workspace .` -> `passed=true`.
- 전체 회귀: `.\.dotnet\dotnet.exe run --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj` -> `All 634 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3, selected next foundation capability `ContinuousOrStaticEffect` (`PartiallyImplemented`).
- 다음 추천 작업: `FND001-CS-03 continuous/static source collector scope parity`.
- 금지사항 준수: 원본 `DCGO/Assets` 수정 없음, 개별 `CardEffect` body 구현 없음, C0039 이후 card-porting 없음, RL 구성요소 구현 없음, commit/push 없음.

## 2026-06-24 FND-003-P Update

- `FND-003-P OnDeclaration` 완료: Unity 원본의 `CardSource.CanDeclareSkillList`, `Permanent.CanDeclareSkillList`, `TurnStateMachine`의 `ActivateCardAction` / `ActivatePermanentAction` 흐름에 맞춰 headless legal action과 선택 선언 실행 경로를 추가했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:973`, `CardSource.cs:1051`, `Permanent.cs:1613-1627`, `TurnStateMachine.cs:917-929`, `TurnStateMachine.cs:1467-1502`, `TurnStateMachine.cs:2802-2850`, `TurnStateMachine.cs:3050-3080`, `MainPhaseAction\ActivateCardAction.cs`, `MainPhaseAction\ActivatePermanentAction.cs`다.
- `DeclarationEffectService`를 추가해 main phase의 turn player가 field top/inherited/linked/hand/trash source에서 `EffectTiming.OnDeclaration` descriptor를 legal action으로 받을 수 있게 했다.
- `DeclareEffectAction`은 source card, source permanent, source zone, source role, once-per-turn key, activation owner metadata를 보존하고, 실행 시점에 같은 source role인지 다시 검증한다.
- `LegalActionGenerator`, `ActionExecutor`, `BattleEngineServices` production service graph에 같은 `TriggerPipelineService` 기반 declaration service를 연결했다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 633 tests passed.`
- Foundation Gate 수치는 직접 조작하지 않았고, generated status 승격도 수행하지 않았다. `OpenCodeReady=false` 상태를 유지한다.
- 다음 추천 작업: `FND-001-A ContinuousOrStaticEffect` CloseableFoundationTask 첫 묶음.
- 잔여 범위: card-specific `OnDeclaration` body/cost/payment/selection continuation 세부 구현, BT20-096 같은 trash `[Main]` self-return body, exact Unity command index parity, broader once-per-turn UI policy fixture.

## 2026-06-24 FND-003-O Update

- `FND-003-O OnTappedAnyone` 완료: 실제 suspend가 성공한 직후 `OnTappedAnyone` pending rule event를 생성하도록 `Tier1PrimitiveService.Suspend(...)` primitive scope를 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1000`, `CardController.cs:5558-5650`, `AttackProcess.cs:544-557`, `CardEffectCommons\GetFromHashtable.cs:87-96`이다.
- Payload는 원본의 `Permanents`, `IsBlock`, `CardEffect`를 중심으로 `Permanent`, `TappedPermanent`, `SuspendedPermanent`, `CardSources`, `SourceCard`, `SourcePermanent`, `SourceZone`, `DestinationZone`을 함께 보존한다.
- `AttackService`는 attack declaration suspend 직후 `OnTappedAnyone` pending event를 먼저 drain한 뒤 `OnAllyAttack`으로 진행하고, block suspend는 `IsBlock=true` payload를 남긴 뒤 `OnBlockAnyone`보다 먼저 `OnTappedAnyone`을 drain한다.
- `SelectEffectFacades` generic tap selection은 `EffectResolution.SourceCard` / `SourcePermanent`를 primitive에 전달한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 630 tests passed.`
- Foundation Gate는 수치 조작하지 않았고 `OpenCodeReady=false`, passed gate 11, failed gate 3 상태를 유지한다. generated status 승격은 수행하지 않았다.
- 다음 추천 작업: `FND-003-P OnDeclaration`.
- 남은 범위: `CanSuspend` / `ICanNotSuspendEffect` policy parity, card-specific `OnTappedAnyone` body/source metadata wiring, strict Unity `SuspendPermanentsClass` stack object parity.

## 2026-06-24 FND-003-N Update

- `FND-003-N BeforePayCost` 완료: 일반 permanent play, option play, 일반 digivolution의 비용 지불 전에 `BeforePayCost` trigger/background pipeline을 실행하도록 runtime scope를 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:604-621`, `CardController.cs:709-711`, `CardController.cs:970-985`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs:2641`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs:770-1045`다.
- `TriggerPipelineOptions.ExecuteBackgroundEffectsFirst`를 추가해 `BeforePayCost`에서 background effect가 foreground cut-in보다 먼저 실행되는 Unity 순서를 opt-in으로 표현했다.
- `CostPaymentRuleEventPayload.CreateBeforePayCost(...)`를 추가해 `PayCost`, `Card`, `Cards`, `CardSources`, `Root`, `SourceZone`, `isEvolution`, `isJogress`, `Permanents`, `Cost`, `BaseCost`, `MemoryBeforeCost`, `CostTransactionId`, `CostKind` payload를 보존했다.
- `PlayCardService`는 pre-cost pipeline 뒤 hand/frame/static CannotPutField 또는 CannotPlay/color requirement를 재검증하고, `DigivolveService`는 hand/digivolution legality와 cost를 재계산한 뒤 메모리를 지불한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 629 tests passed.`
- Foundation Gate 재계산 대상이며, generated status 승격은 수행하지 않았다.
- 다음 추천 작업: `FND-003-O OnTappedAnyone`.
- 남은 범위: `BeforePayCost` selection continuation before payment, DigiXros/Assembly/Burst/AppFusion/Jogress/Digisorption 특수 비용 지불 flow, card-specific BeforePayCost body wiring, strict `PutStackedSkill` stack object parity, max-payable memory/material-selection policy.

## 2026-06-24 FND-003-M Update

- `FND-003-M OnDetermineDoSecurityCheck` 완료: permanent-vs-permanent battle resolution 이후 `OnEndBattle`가 끝난 뒤, `UntilBattleEnd` cleanup과 piercing security check 이전에 `OnDetermineDoSecurityCheck` trigger pipeline을 실행하도록 `AttackService` runtime scope를 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:4731`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs:2591`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Pierce.cs:78`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1013`이다.
- Unity 원본의 `skillInfos_Pierce[0]` 동작에 맞춰 `TriggerPipelineOptions.UseFirstActiveOnly`를 추가했고, 이 옵션은 `OnDetermineDoSecurityCheck` 경로에서만 사용한다.
- Payload는 기존 battle-end snapshot에 `SecurityCheckCount`, `ChecksCompleted`, `DoSecurityCheck`, `CanDoSecurityCheck`, `WillDoSecurityCheck`, `PiercingSecurityCheck`를 추가해 security-check 결정 후보를 관측할 수 있게 했다.
- `ContinueAfterDetermineDoSecurityCheck` continuation을 추가해 selection-bound effect가 pause/resume되어도 cleanup과 piercing security check가 이후에 이어지도록 했다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 626 tests passed.`
- Foundation Gate 재계산 결과는 `OpenCodeReady=false`, passed gate 11, failed gate 3 상태를 유지한다. generated status 승격은 수행하지 않았다.
- 다음 추천 작업: `FND-003-N BeforePayCost`.
- 남은 범위: card-specific Pierce body wiring, direct attack `SecurityAttack` count parity, strict `PutStackedSkill` stack object parity, broader security-check modifier source parity, `OnSecurityCheck` ordering fixture.

## 2026-06-24 FND-003-L Update

- `FND-003-L OnEndBattle` 완료: permanent-vs-permanent battle resolution 후, `UntilBattleEnd` cleanup과 piercing/security check 및 `OnEndAttack` 이전에 `OnEndBattle` trigger pipeline을 실행하도록 `AttackService` battle-result runtime scope를 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:4675-4718`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1012`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory.cs:452`이다.
- Payload는 `Battle`/`battle`/`BattleResult`, `WinnerPermanents`, `WinnerPermanents_real`, `WinnerTopCards`, `LoserPermanents`, `LoserPermanents_real`, `DestroyedPermanents`, `LoserTopCards`, `LoserCard`, `WasTie`, attack context를 보존한다.
- `ContinueAfterEndBattle` continuation을 추가해 `OnEndBattle` 중 selection이 발생해도 resume 후 cleanup, piercing/security check, `OnEndAttack` 순서가 이어지도록 했다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 623 tests passed.`
- Foundation Gate 재계산 결과는 `OpenCodeReady=false`, passed gate 11, failed gate 3 상태를 유지한다. generated status 승격은 수행하지 않았다.
- 다음 추천 작업: `FND-003-M OnDetermineDoSecurityCheck`.
- 남은 범위: security Digimon battle `OnEndBattle` parity, strict cloned Permanent object parity, `LoserCard` security-card payload, battle object lifetime/stale permanent parity, card-specific body wiring.

## 2026-06-24 FND-003-K Update

- `FND-003-K OnDigivolutionCardDiscarded` 완료: 효과로 진화원을 trash로 보내는 shared primitive 경계에서 `OnDigivolutionCardDiscarded` pending rule event를 생성하도록 `Tier1PrimitiveService.TrashDigivolutionCardsWithEvents(...)`를 추가했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:5127-5234`, `CardEffectCommons\CanUseEffects\OnTrashDigivolutionCard.cs`, `CardEffectCommons\GetFromHashtable.cs:569`이며, 원본 payload는 `Permanent`, `CardEffect`, `DiscardedCards`를 사용한다.
- `TrashDigivolutionCardsWithEvents(...)`는 battle-area Digimon의 `EvolutionSources` 대상만 검증하고 `ZoneMover`를 통해 `EvolutionSources -> Trash`로 이동시킨 뒤, effect source가 있을 때만 `OnDigivolutionCardDiscarded`를 enqueue한다.
- `TrashBottomDigivolutionSources(...)`는 같은 shared primitive를 사용하도록 연결했고, `skipEffectAndActivateSkill=true` 경로에서는 원본 null-effect 조건처럼 trigger enqueue를 생략한다.
- `TriggerPipelineService`는 `OnDigivolutionCardDiscarded` payload의 `TriggeredSourceCards`/`DiscardedCards`를 trash-zone source로 수집해, 방금 버려진 진화원 카드가 pending rule event를 관찰할 수 있게 했다.
- Payload는 `Permanent`, `TargetPermanent`, `DiscardedFromPermanent`, `DiscardedFromTopCard`, `DiscardedCards`, `DiscardedCard`, `CardEffect`, `SourceCard`, `SourcePermanent`, `SourceZone`, `DestinationZone`, `MoveReason`, `TriggeredSourceCards`, `TriggeredSourceZone`, `TriggeredSourceOriginalZone`, `TriggeredSourceOriginalPermanent`를 보존한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 621 tests passed.`
- Foundation Gate 재계산 결과는 `OpenCodeReady=false`, passed gate 11, failed gate 3이며, selected next foundation capability는 여전히 `ContinuousOrStaticEffect` `PartiallyImplemented`이다. generated status 승격은 하지 않았다.
- 다음 추천 작업: `FND-003-L OnEndBattle`.
- 남은 범위: `WhenWouldDigivolutionCardDiscarded` cut-in, strict pre-move inherited-source snapshot, `CanNotTrashFromDigivolutionCards` static policy, card-specific body wiring.

## 2026-06-24 FND-003-J Update

- `FND-003-J OnAddDigivolutionCards` 완료: 효과로 카드를 진화원에 추가하는 shared primitive 경계에서 `OnAddDigivolutionCards` pending rule event를 생성하도록 `Tier1PrimitiveService.AddDigivolutionCardsWithEvents(...)`를 추가했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs:1045/1130`, `ICardEffect.cs:1002`, `CardEffectCommons\CanUseEffects\OnAddDigivolutionCards.cs`, `CardEffectCommons\GetFromHashtable.cs:128/146`이며, 원본 payload는 `Permanent`, `CardEffect`, `CardSources`, `isFromSameDigimon`, `isFromDigimon`를 사용한다.
- 일반 digivolve의 `AddCardSource(...)`에는 원본상 `OnAddDigivolutionCards` stack이 없으므로 `DigivolveCard(...)` 경로에는 이벤트를 추가하지 않았다.
- `ComplexMechanicService`의 Jogress/AppFusion/DigiXros/Assembly source placement가 같은 source-add primitive를 통과하도록 연결했다. 단, `CardEffect == null`인 복합 메커니즘 null-source 경로는 원본 `CanTriggerOnAddDigivolutionCard(...)`처럼 실제 발동 대상이 아니므로 `skipEffectAndActivateSkill=true`로 이벤트 enqueue를 생략한다.
- Payload는 `Permanent`, `DestinationPermanent`, `CardEffect`, `SourceCard`, `SourcePermanent`, `Cards`, `CardSources`, `AddedDigivolutionCards`, `isFromSameDigimon`, `isFromDigimon`, `SourceZone`, `DestinationZone`, `SourceZones`, `MoveReason`, `ToTop`, `FaceUp`를 보존한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 620 tests passed.`
- Foundation Gate 재계산 결과는 `OpenCodeReady=false`, passed gate 11, failed gate 3이며, selected next foundation capability는 `ContinuousOrStaticEffect` `PartiallyImplemented`다. generated status 승격은 하지 않았다.
- 다음 추천 작업: `FND-003-K OnDigivolutionCardDiscarded`.
- 남은 범위: face-down/source-bottom add parity fixture, broader source add ordering fixture, source-specific card body wiring.

## 2026-06-23 FND-003-I Update

- `FND-003-I OnMove` 완료: breeding area에서 battle area로 이동한 `Permanent`가 battle area에 생존한 경우 `OnMove` pending rule event를 생성하도록 primitive permanent-move 경계를 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardObjectController.cs:1111`, `CardEffectFactory.cs:854/940`, `CardEffectCommons\CanUseEffects\OnMove.cs:10`이며, 원본은 `Hashtable { "Permanent", movingPermanent }`와 `CanTriggerOnMove`의 battle-area 생존 조건을 사용한다.
- `MoveFromBreedingService`는 공유 `Tier1PrimitiveService.MovePermanentWithEvents(...)`를 사용하고, `BattleEngineServices`는 같은 primitive instance를 주입한다.
- Payload는 `Permanents`, `Permanent`, `MovedPermanents`, `MovedPermanent`, `MovedController`, `MovedTopCard`, `Cards`, `CardSources`, `CardEffect`, `SourceCard`, `SourcePermanent`, `SourceZone`, `DestinationZone`, `OldZone`, `NewZone`, `MoveReason`, `BattleAreaSurvived`를 보존한다.
- `BattleArea -> BreedingArea` 이동은 원본의 `OnMove` 생존 조건과 맞지 않으므로 이번 hook에서 `OnMove`를 생성하지 않는다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 619 tests passed.`
- Foundation Gate 재계산은 정상이며 generated status 승격은 하지 않는다. `OpenCodeReady=false` 상태를 유지한다.
- 다음 추천 작업: `FND-003-J OnAddDigivolutionCards`.
- 남은 범위: broader frame movement parity, non-breeding movement source metadata 확장, stale target parity fixture, source-specific card body wiring.

## 2026-06-23 FND-003-H Update

- `FND-003-H OnUnTappedAnyone` 완료: 실제 battle-area permanent가 suspended에서 unsuspended로 바뀌는 primitive 경계에서 `OnUnTappedAnyone` pending rule event를 생성하도록 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`의 `IUnsuspendPermanents.Unsuspend()`이며, 실제 unsuspend 이후 `EffectTiming.OnUnTappedAnyone`를 `Permanents` payload로 stack하는 순서를 반영했다.
- `PhaseRunner.RunActivePhaseWithResult(...)`의 turn-player field unsuspend와 opponent Reboot unsuspend도 shared `Tier1PrimitiveService.Unsuspend(...)`를 통과한다. breeding-area active phase unsuspend는 원본처럼 event stack에 넣지 않는다.
- Payload는 `Permanents`, `Permanent`, `UnsuspendedPermanents`, `UnsuspendedPermanent`, `UnsuspendedController`, `UnsuspendedTopCard`, `Cards`, `CardSources`, `CardEffect`, `SourceCard`, `SourcePermanent`, `SourceZone`, `DestinationZone`를 보존한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 618 tests passed.`
- Foundation Gate 재계산 대상이며, generated status 승격은 하지 않았다. `OpenCodeReady=false` 상태를 유지한다.
- 다음 추천 작업: `FND-003-I OnMove`.
- 남은 범위: `WhenUntapAnyone` pre-cut-in/replacement, cannot-unsuspend 정책, effect immunity re-check, primitive call-site source metadata 전달 확장.

## 2026-06-23 FND-003-G Update

- `FND-003-G OnUseOption` 완료: hand option play lifecycle에서 cost payment와 `AfterPayCost` 이후, option card의 `OptionSkill` 이전에 `OnUseOption` trigger/background pipeline을 실행하도록 연결했다.
- 원본 근거는 `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`의 option play flow이며, `StackSkillInfos(..., EffectTiming.OnUseOption)`와 `ActivateBackgroundEffects(..., EffectTiming.OnUseOption)`가 `OptionSkill` loop보다 먼저 실행되는 순서를 반영했다.
- Payload는 `Card`, `Cards`, `CardSources`, `SourceCard`, `Root`, `Cost`, `PaidCost`, `PayCost`, `SourceZone`, `ActivatedFromSecurity`를 보존한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 617 tests passed.`
- Foundation Gate 재계산 대상이며, generated status 승격은 하지 않았다. `OpenCodeReady=false` 상태를 유지한다.
- 다음 추천 작업: `FND-003-H OnUnTappedAnyone`.
- 남은 범위: `OnUseOption` 중 pending selection을 만든 뒤 `OptionSkill`로 이어지는 continuation, security-activated option main-flow parity, card-specific option body wiring.

## 2026-06-23 FND-003-F Update

- `FND-003-F OnDiscardLibrary` 완료: deck top trash primitive 경계에서 실제 `Zone.Deck -> Zone.Trash` 이동 후 `OnDiscardLibrary` pending rule event를 큐에 넣도록 연결했다.
- Payload는 원본 `ITrashDeckCards`에 맞춰 `DiscardedCards`, `CardEffect`를 포함하고, headless 검증용으로 `SourceZone`, `DestinationZone`, `RequestedCount`, `RequestedMoreThanAvailable`, `MoveReason`을 함께 보존한다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 616 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3. generated status 승격은 하지 않았다.
- 다음 추천 작업: `FND-003-G OnUseOption`.
- 후속으로 남긴 범위: card-specific deck trash body wiring, reveal/select 후 trash되는 별도 flow, direct battle destruction `OnRemovedField` hook, 복합 mechanic `AfterPayCost` hook, multi-card security discard batching, `IFlipSecurity` face-up flip hook.

## 2026-06-23 FND-003-E Update

- `FND-003-E OnAddSecurity` 완료: security-add/recovery primitive 경계에서 `OnAddSecurity` pending rule event를 큐에 넣고, face-up security 추가는 `OnFaceUpSecurityIncreased`를 이어서 큐에 넣도록 연결했다.
- 검증: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 615 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3. generated status 승격은 하지 않았다.
- 다음 추천 작업: `FND-003-F OnDiscardLibrary`.
- 후속으로 남긴 범위: direct battle destruction `OnRemovedField` hook, 복합 mechanic `AfterPayCost` hook, multi-card security discard batching, `IFlipSecurity` face-up flip hook.

## 2026-06-23 Restart Foundation Progress

- `DATA-001` 완료: CardBaseEntity asset, variant identity, `CardEffectClassName` 정책을 source-aligned 기준으로 정리했고 `docs/as-is-restart/` 및 `docs/generated/as-is-restart/`에 산출물을 생성했다.
- `FND-003-A` 완료: 15개 `ImplementableFoundationTask`를 45개 하위 foundation goal로 분해했고 다음 작업 순서를 `FND-003-B`, `FND-003-C`, `FND-003-D` 순으로 고정했다.
- `FND-003-B OnRemovedField` 완료: primitive destroy scope에서 명시적 `OnRemovedField` pending rule event를 큐잉하도록 구현했고 full regression `All 612 tests passed.`로 확인했다.
- `FND-003-C AfterPayCost` 완료: 일반 play, option play, digivolution의 표준 cost payment 경계에서 `AfterPayCost` trigger pipeline과 source-aligned payload를 연결했고 full regression `All 613 tests passed.`로 확인했다.
- `FND-003-D OnDiscardSecurity` 완료: security-to-trash primitive 경계에서 `OnLoseSecurity` 다음 `OnDiscardSecurity` pending rule event를 source-aligned payload로 큐잉했고 full regression `All 614 tests passed.`로 확인했다.
- Foundation Gate 재계산 결과는 `OpenCodeReady=false`, passed gate 11개, failed gate 3개다. generated status는 승격하지 않았다.
- 다음 추천 작업은 `FND-003-F OnDiscardLibrary`다. direct battle destruction `OnRemovedField` hook, 복합 mechanic `AfterPayCost` hook, multi-card security discard batching, `IFlipSecurity` face-up flip hook은 별도 trace-aware follow-up으로 남긴다.

이 문서는 Script Runtime Foundation 완성 작업의 진행 기록이다.
card-porting batch 진행률이 아니라, Foundation Gate를 열기 위한 운영/문서/구현 remediation 이력을 기록한다.

## 현재 기준선

| 항목 | 값 |
| --- | --- |
| 기준일 | 2026-06-24 |
| OpenCodeReady | false |
| Passed gates | 11 |
| Failed gates | 3 |
| Selected next foundation capability | ContinuousOrStaticEffect |
| Selected next foundation status | PartiallyImplemented |
| Unknown common API count | 1 |
| Unsupported capability count | 5 |
| PartiallyImplemented capability count | 58 |
| C0039 이후 card-porting | 금지 |

## 진행 기록

| 날짜 | 작업 | 요약 | 근거 | 검증 | 남은 blocker |
| --- | --- | --- | --- | --- | --- |
| 2026-06-23 | Player EffectList temporary grant facade | 원본 `Player.EffectList(timing)` duration/provider list 의미 중 target-player temporary grant 경로를 headless `TemporaryGrantedEffect` 상태와 연결했다. `Player.EffectList(timing, TemporaryGrantedEffectRegistry)`는 target player와 timing이 일치하는 grant를 descriptor-backed `ICardEffect`로 노출하고 source card/permanent/controller metadata를 보존한다. registry 없는 호출 또는 missing granted-effect handler는 명시 실패한다. | `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md`, `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 611 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production card script catalog의 provider 채택, original `PermanentEffects`/`Until*` bucket cleanup parity, broad primitive target-immunity integration은 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | Permanent EffectList_Added temporary grant facade | 원본 `Permanent.EffectList_Added` duration list 의미를 headless `TemporaryGrantedEffect` 상태와 연결했다. `EffectList_Added(timing, TemporaryGrantedEffectRegistry)`는 target permanent와 timing이 일치하는 grant를 descriptor-backed `ICardEffect`로 노출하고, registry 없는 호출 또는 missing granted-effect handler는 명시 실패한다. 기존 trigger pipeline의 `TemporaryGrantedEffect` execution 경로는 유지했다. | `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md`, `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 610 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production card script catalog의 provider 채택, original `PermanentEffects`/`Until*` bucket cleanup parity, broad primitive target-immunity integration은 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectFactoryCatalog bridge script provider path | `ICEntityEffectFactoryProvider`를 추가하고 `CEntityEffectFactoryCatalog.FromScripts(...)`가 bridge script 목록에서 source effect class factory catalog를 구성하도록 했다. non-empty `EffectiveSourceEffectClassName` 그룹은 provider 1개를 요구하며, provider가 없거나 둘 이상이거나 null factory를 반환하면 명시 실패한다. shared source class는 metadata record 여러 개가 같은 source class를 가리키되 factory provider는 하나만 허용한다. | `src/DCGO.RL.Engine/CardEffects/ICEntityEffectFactoryProvider.cs`, `src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md`, `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 609 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | 실제 production card script catalog의 provider 채택, duration-scoped grant facade, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectFactoryCatalog service graph overload | `CEntityEffectFactoryCatalog`를 추가해 source effect class factory catalog 검증을 명시 객체로 분리했다. `BattleEngineServices.Create(ICardScriptRegistry, CEntityEffectFactoryCatalog, IDecisionProvider?)` overload는 `ICardScriptRegistry.PortingRecords`와 catalog를 사용해 `ICEntityEffectRegistry` root dependency를 조립한다. duplicate/empty/null factory는 명시 실패한다. | `src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs`, `src/DCGO.RL.Engine/Battle/BattleEngineServices.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 608 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | bridge script registration이 catalog를 채우는 경로, duration-scoped grant facade, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry porting metadata builder | `CEntityEffectRegistryBuilder`를 추가해 `ICardScriptRegistry.PortingRecords` 또는 명시 `CardEffectPortingRecord` 목록을 `CEntityEffectRegistryEntry` 목록과 registry로 변환할 수 있게 했다. `EffectiveSourceEffectClassName`을 기준으로 shared source class를 dedupe하고, NoEffect/empty class는 skip하며, missing/null factory와 invalid record는 명시 실패한다. | `src/DCGO.RL.Engine/CardEffects/CEntityEffectRegistryBuilder.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 607 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production factory catalog, bridge script registration, duration-scoped grant facade, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry source-aligned entry builder | `CEntityEffectRegistryEntry`와 `CEntityEffectRegistry.FromEntries(...)`를 추가해 production metadata가 raw dictionary 대신 `(CardId, EffectClassName, factory)` entry를 제공하면 direct class, source namespace, token namespace lookup key로 deterministic하게 확장되도록 했다. duplicate key, empty CardId/class, null factory, null entry는 명시 실패한다. | `src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 606 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | asset/porting metadata에서 실제 entry 목록을 생성하는 조립 경로, bridge script 등록, duration-scoped grant facade, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CEntityEffectRegistry service graph prep | raw factory dictionary를 runtime facade에 직접 넘기는 임시 경계를 명시 `ICEntityEffectRegistry`/`CEntityEffectRegistry`로 분리했다. `CEntity_EffectController`, `CardSource.EffectList*`, `Permanent.EffectList*`는 registry overload를 우선 사용하고 dictionary overload는 compatibility wrapper로 남겼다. registry는 direct class, source namespace, token namespace lookup과 duplicate/empty/null factory 검증을 담당한다. `BattleEngineServices`는 registry를 root dependency로 보유하고 validation report에서 누락을 잡는다. | `src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs`, `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs`, `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine/Battle/BattleEngineServices.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md`, `docs/rl-engine/runtime-composition.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 605 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | production asset/porting metadata에서 registry를 자동 populate하는 작업, duration-scoped grant facade, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | CardSource/Permanent EffectList facade | 원본 `CardSource.EffectList*`와 `Permanent.EffectList*` 계약을 headless `ScriptRuntimeFoundation`에 추가했다. Unity/GManager lookup은 복제하지 않고 명시적 `ICEntityEffectRegistry`를 요구한다. `Permanent.EffectList*`는 top-card, inherited source, linked source 역할을 구분해 수집하고 face-down source를 skip한다. `CardSource.IsFlipped`는 원본처럼 face-down 의미로 보정했다. | `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`, `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CEntity_EffectController.cs`, `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`, `src/DCGO.RL.Engine.Tests/Program.cs`, `docs/rl-engine/script-runtime-foundation-contract.md` | full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 605 tests passed.` Foundation Gate 재계산: `OpenCodeReady=false`, failed gate 3개 유지. | duration-scoped grant facade, production asset registry population, original `PermanentEffects`/`Until*` bucket cleanup parity는 남아 있다. C0039 이후 card-porting은 계속 금지다. |
| 2026-06-23 | Foundation queue/progress 분리 | `QUEUE_FOUNDATION_COMPLETION.md`와 `PROGRESS_FOUNDATION_COMPLETION.md`를 생성하고, full-card queue와 Foundation active/progress 문서에 흩어져 있던 66B~66AE foundation-remediation 이력을 Foundation queue로 재분류했다. C0039 이후 card-porting 금지 상태, 현재 Foundation Gate 실패 항목 3개, 다음 foundation 작업 순서, 장기 path/namespace alignment plan을 문서화했다. | `docs/codex-prompts/state/QUEUE_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/PROGRESS_FOUNDATION_COMPLETION.md`, `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`, `docs/codex-prompts/ACTIVE/RUN_NEXT_FOUNDATION_COMPLETION.md`, `docs/rl-engine/foundation-completion-gate.md`, `docs/generated/foundation-completion-gate.json` | 문서와 queue만 변경했다. 코드, generated gate input, 빌드 입력, 실제 path/namespace는 변경하지 않았으므로 테스트는 생략했다. commit/push도 수행하지 않았다. | OpenCodeReady=false, failed gate 3개 유지. 다음 작업은 `ContinuousOrStaticEffect` partial closure, unknown common API mapping, unsupported capability 분해 순서다. |

## 테스트 기록

최근 작업은 두 범위로 나뉜다.

- Foundation queue/progress 분리: 문서와 queue만 변경했고 코드, generated gate input, 빌드 입력, 실제 path/namespace를 변경하지 않아 빌드/테스트를 생략했다. commit/push도 수행하지 않았다.
- Player EffectList temporary grant facade, Permanent EffectList_Added temporary grant facade, CEntityEffectFactoryCatalog bridge script provider path, service graph overload, CEntityEffectRegistry porting metadata builder, source-aligned entry builder, service graph prep 및 CardSource/Permanent EffectList facade: 코드와 테스트를 변경했으므로 전체 테스트를 실행했다.

최신 검증:

- `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: latest full regression after FND-003-R was `All 643 tests passed.`
- `python scripts\\calculate_foundation_completion_gate.py --workspace .`
- 결과: `OpenCodeReady=false`, passed gate 12개, failed gate 2개, unknown common API 0개, unsupported capability 5개, partially implemented capability 58개, next foundation capability `ContinuousOrStaticEffect`

## 다음 추천 작업

1. `FND-003-S`: 남은 link lifecycle unsupported capability를 구현 가능한 foundation remediation prompt로 분해한다.
2. `FND-001`: `ContinuousOrStaticEffect` partial capability closure 계획을 계속 실행한다.
3. `PARITY-001-A`: Unity full-card fixture exporter scenario contract를 구체화한다.
4. `FND-004`: 실제 파일 이동 전 path/namespace alignment RFC와 compatibility test 범위를 확정한다.
5. `FND-005` 후속: 실제 production card script catalog의 `ICEntityEffectFactoryProvider` 채택과 original `PermanentEffects`/`Until*` bucket cleanup parity는 CardEffect body 구현 없이 별도 foundation 작업으로만 진행한다.

## 금지 상태

- C0039 이후 full-card porting은 계속 금지다.
- 개별 CardEffect body 구현은 금지다.
- ST/BT/EX/P batch 확장은 금지다.
- RL Environment, Observation, Reward, Dataset Exporter, Trainer 구현은 금지다.
- Unity, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성 추가는 금지다.
