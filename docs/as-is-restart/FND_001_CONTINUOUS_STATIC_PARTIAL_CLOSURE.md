# FND-001 ContinuousOrStaticEffect Partial Closure

이번 문서는 FND-001만 수행한다. 구현, `src` 수정, 원본 `DCGO` 수정, `CardEffect` body 구현, C0039 이후 card-porting, TRUST-001 수행, Foundation Gate 수치 조작, generated status 승격, commit/push는 하지 않는다.

## AS-IS Root

- AS-IS root: `E:\headlessDCGO\DCGO\Assets`

## Partial Source

- Capability: `ContinuousOrStaticEffect`
- Current capability status: `PartiallyImplemented`
- Inventory section/status: `timings` / `PartiallyImplemented`
- Inventory aliases: `None`
- Registry affected/source counts: 4326 cards / 2220 files
- Mechanic scheduler selected counts: 7867 cards / 3896 source effects / 397 batches
- Source-required records containing `ContinuousOrStaticEffect`: 3897 / 3918
- Full-card parity coverage: NotRun 3918, Passed 0, Failed 0

## Partial Reason From Registry

Continuous descriptors support field top, inherited, linked, face-up security, hand, trash, and executing source scopes; metadata criteria cover trait/name/text source and target gates; continuous static keyword descriptors cover source/condition-aware supported BattleKeyword grants; StaticRequirementService covers source/condition-aware static digivolution/link requirements, ignore-digivolution permission semantics, cannot-ignore digivolution restriction descriptors, effective card colors and effective permanent levels for digivolution requirements, effective link requirement metadata/level gates through ComplexMechanicService, static link cost modifiers through CostResolver, and effective metadata criteria where a StaticEffectService is available; StaticEffectService covers static cost/restriction/immunity descriptor evaluation, static card play restriction descriptors for option play gates, static card put-field restriction descriptors for permanent field-entry gates, static card move restriction descriptors for return-to-hand gates, effective card/base/current color descriptors, effective card name/trait metadata descriptors, effective card/permanent level descriptors, and ignore color requirements for option play. TemporaryModifier covers supported player-level DP/SecurityAttack/SecurityDigimonDP runtime stat effects, target permanent temporary keyword grants, and player-wide temporary keyword grants with metadata-gated battle-area Digimon targets. TemporaryGrantedEffect covers duration-bound granted trigger source/timing descriptors through the trigger pipeline. Full-card parity evidence is generated conservatively as NotRun, and generated/runtime status mismatch is closed by separating legacy pilot divergence from generated source truth. The capability remains partial until remaining full-card continuous/static variants have source-locked parity evidence.

## Partial Source Files

- `docs/generated/capability-truth-audit/capability-registry.json`
- `docs/generated/foundation-completion-gate.json`
- `docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json`
- `docs/generated/capability-truth-audit/source-required-capabilities.json`
- `docs/generated/full-card-parity-evidence.json`
- `docs/rl-engine/capability-truth-audit-66B.md`
- `docs/rl-engine/full-card-parity-evidence-scope-66P.md`
- `docs/rl-engine/validation-strategy.md`
- `docs/as-is-restart/GOAL_09_ASIS_IMPLEMENTATION_PRIORITY_REWRITE.md`
- `docs/as-is-restart/GOAL_08_ASIS_HEADLESS_TRUST_AUDIT.md`
- `docs/as-is-restart/GOAL_07_ASIS_HEADLESS_REQUIREMENT_MATRIX.md`
- `docs/as-is-restart/GOAL_05_ASIS_CSHARP_CALL_GRAPH.md`

## Original Source Surfaces

- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/CardObjectController.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/**/*.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons/**/*.cs`
- `DCGO/Assets/CardBaseEntity/**/*.asset`

## Classification Counts

- CloseableFoundationTask: 6
- NeedsSourceMapping: 3
- NeedsDataPolicy: 2
- NeedsCapabilityRemediation: 3
- CardEffectBodySpecific: 1
- UnityOnlyExcluded: 1
- ExternalAdapterRequired: 0
- BlockedNeedsManualReview: 0

## Task Matrix

| ID | Classification | Task | Source evidence | Current headless candidates | Test candidates | TRUST-001 |
| --- | --- | --- | --- | --- | --- | --- |
| FND001-CS-01 | NeedsSourceMapping | Full-card continuous/static source-locked parity evidence 계획 | `docs/generated/full-card-parity-evidence.json`<br>`docs/rl-engine/full-card-parity-evidence-scope-66P.md`<br>`docs/generated/capability-truth-audit/source-required-capabilities.json` | `docs/generated/parity-fixtures/unity/full-card-source`<br>`docs/generated/parity-fixtures/rl/full-card-source`<br>`docs/generated/parity-fixtures/reports/full-card-source` | `full-card parity evidence NotRun count remains 3918 until fixture exists`<br>`ContinuousOrStaticEffect sourceEffect sample retains sourcePath/card asset mapping` | yes |
| FND001-CS-02 | CloseableFoundationTask | EffectTiming.None alias와 gate accounting 고정 | `DCGO/Assets/Scripts/Script/ICardEffect.cs`<br>`DCGO/Assets/Scripts/Script/CardSource.cs`<br>`DCGO/Assets/Scripts/Script/Permanent.cs` | `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`<br>`src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`<br>`src/DCGO.RL.Engine/Effects/StaticEffectService.cs` | `validation-strategy 66V alias assertions`<br>`capability registry has inventoryAliases [None]` | yes |
| FND001-CS-03 | CloseableFoundationTask | continuous/static source collector scope parity | `DCGO/Assets/Scripts/Script/AutoProcessing.cs`<br>`DCGO/Assets/Scripts/Script/CardSource.cs`<br>`DCGO/Assets/Scripts/Script/Permanent.cs` | `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`<br>`src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`<br>`src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs` | `Continuous linked source applies from linked zone`<br>`Continuous face-up security source applies`<br>`Continuous hand source applies only from hand` | yes |
| FND001-CS-04 | CloseableFoundationTask | duration bucket cleanup/provider integration parity | `DCGO/Assets/Scripts/Script/Player.cs`<br>`DCGO/Assets/Scripts/Script/Permanent.cs`<br>`DCGO/Assets/Scripts/Script/TurnStateMachine.cs` | `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs`<br>`src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs`<br>`src/DCGO.RL.Engine/Effects/DurationCleanupService.cs` | `Duration player runtime modifiers replay deterministic`<br>`Duration temporary keyword grants Blocker until cleanup`<br>`Duration temporary granted trigger runs from target permanent timing` | yes |
| FND001-CS-05 | NeedsCapabilityRemediation | temporary granted trigger와 FND-003 event timing 의존성 | `docs/generated/as-is-restart/fnd-003-next-action-queue.json`<br>`DCGO/Assets/Scripts/Script/CardController.cs`<br>`DCGO/Assets/Scripts/Script/Permanent.cs` | `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`<br>`src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs`<br>`src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs` | `Duration temporary granted trigger hash and replay deterministic`<br>`FND-003 event-specific tests after remediation` | yes |
| FND001-CS-06 | CloseableFoundationTask | supported static keyword descriptor parity | `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs` | `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`<br>`src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`<br>`src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs` | `Continuous static keyword field source grants Blocker`<br>`CardEffectFactory static keyword wrappers map supported keywords`<br>`Continuous static keyword replay deterministic` | yes |
| FND001-CS-07 | NeedsSourceMapping | unsupported trigger/process keyword static factory mapping | `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Pierce.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Alliance.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/VortexCanAttackPlayers.cs` | `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs`<br>`src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`<br>`src/DCGO.RL.Engine/Battle/BattleKeywordService.cs` | `CardEffectFactory keyword static effect rejects unsupported keyword shape`<br>`future trigger/process keyword mapping fixtures` | yes |
| FND001-CS-08 | CloseableFoundationTask | static DP/SecurityAttack/SecurityDigimonDP descriptor parity | `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeDP.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeSAttack.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeCardDP.cs` | `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`<br>`src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`<br>`src/DCGO.RL.Engine/Battle/EffectiveStatService.cs` | `Continuous DP modifier affects effective DP`<br>`Continuous dynamic SecurityAttack from source count`<br>`Continuous effects are derived for state hash` | yes |
| FND001-CS-09 | NeedsCapabilityRemediation | static cost modifiers와 pay timing windows | `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangePlayCost.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeDigivolutionCost.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs` | `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`<br>`src/DCGO.RL.Engine/Mechanics/CostResolver.cs`<br>`src/DCGO.RL.Engine/Battle/PlayCardService.cs` | `Static cost modifier adjusts play and digivolution cost`<br>`Static link cost modifier adjusts link cost`<br>`future BeforePayCost/AfterPayCost window fixtures` | yes |
| FND001-CS-10 | NeedsCapabilityRemediation | static restriction/immunity와 replacement window 의존성 | `DCGO/Assets/Scripts/Script/CardEffectFactory/CanNotBeDeleted.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/CanNotReturnToHand.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/CanNoReturnToDeck.cs` | `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`<br>`src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`<br>`src/DCGO.RL.Engine/Battle/AttackService.cs` | `Static restriction blocks attack and block`<br>`Static card restriction blocks return to hand`<br>`Static immunity descriptor evaluates metadata` | yes |
| FND001-CS-11 | CloseableFoundationTask | static evolution/link requirement effective gates | `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`<br>`DCGO/Assets/Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPlayer/IgnoreDigivolutionRequirement.cs` | `src/DCGO.RL.Engine/Effects/StaticRequirementService.cs`<br>`src/DCGO.RL.Engine/Effects/StaticEffectService.cs`<br>`src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs` | `Static evolution requirement hand source generates and executes`<br>`Static evolution requirement cannot-ignore restriction blocks permission`<br>`Static link requirement uses effective metadata and level` | yes |
| FND001-CS-12 | NeedsDataPolicy | effective card metadata name/trait/text policy | `DCGO/Assets/Scripts/Script/CardSource.cs`<br>`DCGO/Assets/CardBaseEntity/**/*.asset`<br>`docs/generated/as-is-restart/asis-card-data-field-summary.json` | `src/DCGO.RL.Engine/Domain/CardMetadataCriteria.cs`<br>`src/DCGO.RL.Engine/Effects/StaticEffectService.cs`<br>`src/DCGO.RL.Engine/Domain/CardDefinition.cs` | `Continuous metadata criteria gates target trait and text`<br>`Static card metadata modifier affects cost criteria`<br>`Static requirement metadata criteria gates source and target` | yes |
| FND001-CS-13 | NeedsDataPolicy | effective color/level/face-up security data policy | `DCGO/Assets/Scripts/Script/CardSource.cs`<br>`DCGO/Assets/Scripts/Script/Permanent.cs`<br>`DCGO/Assets/Scripts/Script/CardController.cs` | `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`<br>`src/DCGO.RL.Engine/Domain/CardDefinition.cs`<br>`src/DCGO.RL.Engine/Domain/CardInstance.cs` | `Static card color modifier affects option color requirement`<br>`Static ignore color requirement permits option`<br>`Static card level modifier feeds permanent level requirement` | yes |
| FND001-CS-14 | NeedsSourceMapping | AutoProcessing priority/cut-in/background ordering parity | `DCGO/Assets/Scripts/Script/AutoProcessing.cs`<br>`DCGO/Assets/Scripts/Script/CardController.cs`<br>`DCGO/Assets/Scripts/Script/TurnStateMachine.cs` | `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`<br>`src/DCGO.RL.Engine/Effects/TriggerCollector.cs`<br>`src/DCGO.RL.Engine/Effects/SelectionContinuation.cs` | `future MultipleSkills priority/source ordering fixtures`<br>`existing TriggerPipeline continuation tests as candidates only` | yes |
| FND001-CS-15 | CardEffectBodySpecific | CardEffect body-specific static/continuous variants 제외 | `DCGO/Assets/Scripts/CardEffect/**/*.cs`<br>`docs/generated/as-is-restart/fnd-003-next-action-queue.json` | `future card-specific porting only after OpenCodeReady`<br>`generated full-card source scaffold` | `none in FND-001; future mechanic-specific tests only` | yes |
| FND001-CS-16 | UnityOnlyExcluded | Unity visual/display-only continuous surfaces 제외 | `DCGO/Assets/Scripts/Script/CardController.cs`<br>`DCGO/Assets/Scripts/Script/CardObjectController.cs` | `UnityAdapter future scope`<br>`DecisionPoint/SelectionRequest for non-visual decisions` | `none for visual display; only state-change parity candidates remain` | no |

## CloseableFoundationTask 목록

- FND001-CS-02: EffectTiming.None alias와 gate accounting 고정
- FND001-CS-03: continuous/static source collector scope parity
- FND001-CS-04: duration bucket cleanup/provider integration parity
- FND001-CS-06: supported static keyword descriptor parity
- FND001-CS-08: static DP/SecurityAttack/SecurityDigimonDP descriptor parity
- FND001-CS-11: static evolution/link requirement effective gates

## Blocked 목록

- BlockedNeedsManualReview 항목 없음. NeedsSourceMapping/NeedsDataPolicy/NeedsCapabilityRemediation 항목은 별도 의존 작업으로 분리했다.

## FND-003 Dependency

FND-003에서 `ContinuousOrStaticEffect`/source collection dependency로 넘긴 capability는 25건이다. 이 문서는 FND-003을 수행하지 않고 dependency 범위만 소비한다.

- `OnDeclaration`
- `WhenPermanentWouldBeDeleted`
- `OnTappedAnyone`
- `WhenRemoveField`
- `BeforePayCost`
- `OnDetermineDoSecurityCheck`
- `OnEndBattle`
- `OnDigivolutionCardDiscarded`
- `OnAddDigivolutionCards`
- `WhenLinked`
- `OnMove`
- `OnUnTappedAnyone`
- `OnUseOption`
- `OnDiscardLibrary`
- `OnAddSecurity`
- `WhenReturntoLibraryAnyone`
- `OnDiscardSecurity`
- `AfterPayCost`
- `WhenDigisorption`
- `OnLinkCardDiscarded`
- `OnRemovedField`
- `OnFaceUpSecurityIncreased`
- `WhenWouldLink`
- `WhenUntapAnyone`
- `WhenWouldDigivolutionCardDiscarded`

## TRUST-001 Handoff

기존 `src` 구현은 Source of Truth completion evidence로 신뢰하지 않고 TRUST-001에서 대조할 후보로만 넘긴다. FND-001 task handoff는 15건이다.

- `FND001-CS-01`
- `FND001-CS-02`
- `FND001-CS-03`
- `FND001-CS-04`
- `FND001-CS-05`
- `FND001-CS-06`
- `FND001-CS-07`
- `FND001-CS-08`
- `FND001-CS-09`
- `FND001-CS-10`
- `FND001-CS-11`
- `FND001-CS-12`
- `FND001-CS-13`
- `FND001-CS-14`
- `FND001-CS-15`

FND-003 capability-level TRUST-001 handoff는 26건이다.

## Test Candidates

- `CardEffectFactory keyword static effect rejects unsupported keyword shape`
- `CardEffectFactory static keyword wrappers map supported keywords`
- `Continuous DP modifier affects effective DP`
- `Continuous and duration modifiers stack deterministically`
- `Continuous dynamic SecurityAttack from source count`
- `Continuous effects are derived for state hash`
- `Continuous executing source applies during execution`
- `Continuous face-down security source is ignored`
- `Continuous face-up security source applies`
- `Continuous hand source applies only from hand`
- `Continuous linked source applies from linked zone`
- `Continuous metadata criteria gates target trait and text`
- `Continuous static keyword condition gates keyword`
- `Continuous static keyword field source grants Blocker`
- `Continuous static keyword inherited source stops after move`
- `Continuous static keyword replay deterministic`
- `Continuous trash source applies from trash`
- `ContinuousOrStaticEffect sourceEffect sample retains sourcePath/card asset mapping`
- `Duration invariant detects invalid granted trigger`
- `Duration invariant detects invalid keyword modifier`
- `Duration player DP modifier affects owner battle area`
- `Duration player SecurityAttack modifier affects owner Digimon`
- `Duration player keyword grants Rush to matching battle area Digimon`
- `Duration player keyword hash and replay deterministic`
- `Duration player runtime modifiers clone restore hash`
- `Duration player runtime modifiers replay deterministic`
- `Duration temporary granted trigger hash and replay deterministic`
- `Duration temporary granted trigger runs from target permanent timing`
- `Duration temporary keyword grants Blocker until cleanup`
- `Duration temporary keyword grants Rush attack legality`
- `Duration temporary keyword hash and replay deterministic`
- `FND-003 event-specific tests after remediation`
- `Script runtime Player effect list uses temporary granted effects`
- `Static card color modifier affects digivolution color requirement`
- `Static card color modifier affects option color requirement`
- `Static card level modifier feeds permanent level requirement`
- `Static card metadata modifier affects cost criteria`
- `Static card restriction blocks option play`
- `Static card restriction blocks permanent field play`
- `Static card restriction blocks return to hand`
- `Static cost modifier adjusts play and digivolution cost`
- `Static evolution requirement cannot-ignore restriction blocks permission`
- `Static evolution requirement cannot-ignore restriction condition gates`
- `Static evolution requirement condition gates target`
- `Static evolution requirement hand source generates and executes`
- `Static evolution requirement ignore permission generates and executes`
- `Static evolution requirement ignore permission requires target gate`
- `Static evolution requirement stops after source move`
- `Static ignore color requirement permits option`
- `Static immunity descriptor evaluates metadata`
- `Static link cost modifier adjusts link cost`
- `Static link requirement hand source generates and executes`
- `Static link requirement uses effective metadata and level`
- `Static permanent level modifier affects normal digivolution requirement`
- `Static requirement metadata criteria gates source and target`
- `Static requirement replay deterministic`
- `Static restriction blocks attack and block`
- `capability registry has inventoryAliases [None]`
- `existing TriggerPipeline continuation tests as candidates only`
- `full-card parity evidence NotRun count remains 3918 until fixture exists`
- `future BeforePayCost/AfterPayCost window fixtures`
- `future MultipleSkills priority/source ordering fixtures`
- `future trigger/process keyword mapping fixtures`
- `future would/delete/remove timing fixtures`
- `none for visual display; only state-change parity candidates remain`
- `none in FND-001; future mechanic-specific tests only`
- `validation-strategy 66V alias assertions`

## Gate Recalculation Candidates

- `ContinuousOrStaticEffect` status는 이 문서에서 승격하지 않는다.
- Gate 수치는 직접 조작하지 않는다.
- 위 task가 실제 implementation/validation goal에서 닫힌 뒤 `scripts/calculate_foundation_completion_gate.py --workspace .` 재계산 후보로만 기록한다.

## Non-Goals Confirmed

- `src` 구현 코드 수정 없음.
- 원본 `DCGO` 수정 없음.
- `CardEffect` body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- RL 구성요소 구현 없음.
- TRUST-001 수행 없음.
- Foundation Gate 수치 조작 없음.
- generated status `Implemented`/`Verified` 승격 없음.
- 100MiB 이상 raw JSON 추가 없음.
- commit/push 없음.

## Next Work

1. FND-003의 `NeedsCapabilityRemediation` 항목 중 event/source payload foundation task를 별도 goal로 처리한다.
2. DATA-001에서 CardBaseEntity metadata/variant/face-up security policy를 고정한다.
3. TRUST-001에서 현재 `src` 후보 파일과 Source of Truth를 대조한다.
4. PARITY-001에서 source-locked Unity/RL fixture/report 생성 정책을 실제 실행 가능한 validation goal로 전환한다.

Recommended commit message: `docs: decompose FND-001 continuous static partial closure`

## Task Details

### FND001-CS-01 Full-card continuous/static source-locked parity evidence 계획

- Classification: NeedsSourceMapping
- Partial reason: registry reason과 66P parity evidence가 full-card continuous/static variants의 source-locked parity evidence를 NotRun으로 남겨 ContinuousOrStaticEffect를 PartiallyImplemented로 유지한다.
- Source files: `docs/generated/full-card-parity-evidence.json`, `docs/rl-engine/full-card-parity-evidence-scope-66P.md`, `docs/generated/capability-truth-audit/source-required-capabilities.json`
- Source classes: `FullCardParityEvidence records`, `sourceRequiredCapabilities.sourceEffects`
- Source methods/calls: `coverageStatus=NotRun`, `missingUnityFixture=true`, `requiredCapabilities includes ContinuousOrStaticEffect`
- Card data evidence: {"sourceScaffoldId": "AD1_001@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs", "sourceEffectClassName": "AD1_001", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-001#7817@base", "cardId": "AD1-001", "cardIndex": 7817, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset"}, {"definitionStableId": "AD1-001#7818@P1", "cardId": "AD1-001", "cardIndex": 7818, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset"}]}; {"sourceScaffoldId": "AD1_002@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_002.cs", "sourceEffectClassName": "AD1_002", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_002.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-002#7819@base", "cardId": "AD1-002", "cardIndex": 7819, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset"}, {"definitionStableId": "AD1-002#7820@P1", "cardId": "AD1-002", "cardIndex": 7820, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset"}]}; {"sourceScaffoldId": "AD1_003@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs", "sourceEffectClassName": "AD1_003", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs", "affectedCardCount": 1, "sampleCards": [{"definitionStableId": "AD1-003#7821@base", "cardId": "AD1-003", "cardIndex": 7821, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset"}]}; {"sourceScaffoldId": "AD1_004@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs", "sourceEffectClassName": "AD1_004", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-004#7822@base", "cardId": "AD1-004", "cardIndex": 7822, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset"}, {"definitionStableId": "AD1-004#7823@P1", "cardId": "AD1-004", "cardIndex": 7823, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset"}]}
- Current headless candidates: `docs/generated/parity-fixtures/unity/full-card-source`, `docs/generated/parity-fixtures/rl/full-card-source`, `docs/generated/parity-fixtures/reports/full-card-source`, `scripts/generate_full_card_parity_evidence.py`
- Closure tasks:
- ContinuousOrStaticEffect 포함 sourceEffect 3897건을 fixture 대상 set으로 고정한다.
- Unity fixture/RL fixture/comparison report missing 정책을 카드별 body 구현 없이 문서화한다.
- NotRun은 pass가 아니라는 gate 재계산 후보만 기록한다.
- Test candidates: `full-card parity evidence NotRun count remains 3918 until fixture exists`, `ContinuousOrStaticEffect sourceEffect sample retains sourcePath/card asset mapping`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-02 EffectTiming.None alias와 gate accounting 고정

- Classification: CloseableFoundationTask
- Partial reason: 원본 EffectTiming.None은 별도 unsupported capability가 아니라 ContinuousOrStaticEffect alias로 집계되어야 한다.
- Source files: `DCGO/Assets/Scripts/Script/ICardEffect.cs`, `DCGO/Assets/Scripts/Script/CardSource.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`, `DCGO/Assets/Scripts/Script/Player.cs`, `docs/rl-engine/validation-strategy.md`
- Source classes: `EffectTiming`, `CardSource`, `Permanent`, `Player`
- Source methods/calls: `EffectTiming.None`, `CardSource.EffectList(EffectTiming.None)`, `Permanent.EffectList(EffectTiming.None)`, `Player.EffectList(EffectTiming.None)`
- Card data evidence: static_or_continuous/None timing records are capability aliases, not raw None blockers
- Current headless candidates: `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`, `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`, `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `docs/generated/capability-truth-audit/capability-registry.json`
- Closure tasks:
- raw None capability가 registry에 없고 ContinuousOrStaticEffect.inventoryAliases에만 남는지 audit한다.
- source-required-capabilities에서 None timing이 unsupported blocker로 재등장하지 않도록 verifier 후보를 만든다.
- Test candidates: `validation-strategy 66V alias assertions`, `capability registry has inventoryAliases [None]`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-03 continuous/static source collector scope parity

- Classification: CloseableFoundationTask
- Partial reason: field top, inherited, linked, face-up security, hand, trash, executing source가 headless collector에 후보로 있으나 full-card source ordering parity는 아직 닫히지 않았다.
- Source files: `DCGO/Assets/Scripts/Script/AutoProcessing.cs`, `DCGO/Assets/Scripts/Script/CardSource.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`, `DCGO/Assets/Scripts/Script/Player.cs`
- Source classes: `AutoProcessing`, `CardSource`, `Permanent`, `Player`
- Source methods/calls: `AutoProcessing.GetSkillInfos`, `GetSkillInfosOfCards`, `EffectList_ForCard`, `EffectList_Added`
- Card data evidence: {"sourceScaffoldId": "AD1_005@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs", "sourceEffectClassName": "AD1_005", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-005#7824@base", "cardId": "AD1-005", "cardIndex": 7824, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset"}, {"definitionStableId": "AD1-005#7825@P1", "cardId": "AD1-005", "cardIndex": 7825, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset"}]}; {"sourceScaffoldId": "AD1_006@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_006.cs", "sourceEffectClassName": "AD1_006", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_006.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-006#7826@base", "cardId": "AD1-006", "cardIndex": 7826, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset"}, {"definitionStableId": "AD1-006#7827@P1", "cardId": "AD1-006", "cardIndex": 7827, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset"}]}; {"sourceScaffoldId": "AD1_007@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_007.cs", "sourceEffectClassName": "AD1_007", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_007.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-007#7828@base", "cardId": "AD1-007", "cardIndex": 7828, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset"}, {"definitionStableId": "AD1-007#7829@P1", "cardId": "AD1-007", "cardIndex": 7829, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset"}]}; {"sourceScaffoldId": "AD1_008@DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs", "sourceEffectClassName": "AD1_008", "sourcePath": "DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs", "affectedCardCount": 2, "sampleCards": [{"definitionStableId": "AD1-008#7830@base", "cardId": "AD1-008", "cardIndex": 7830, "variantKey": "base", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset"}, {"definitionStableId": "AD1-008#7831@P1", "cardId": "AD1-008", "cardIndex": 7831, "variantKey": "P1", "assetPath": "DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset"}]}
- Current headless candidates: `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`, `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`, `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs`
- Closure tasks:
- source kind별 fixture 후보를 full-card scaffold sourcePath와 연결한다.
- linked/inherited/face-up security ordering이 원본 AutoProcessing 수집 순서와 다르면 별도 blocker로 분리한다.
- Test candidates: `Continuous linked source applies from linked zone`, `Continuous face-up security source applies`, `Continuous hand source applies only from hand`, `Continuous trash source applies from trash`, `Continuous executing source applies during execution`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-04 duration bucket cleanup/provider integration parity

- Classification: CloseableFoundationTask
- Partial reason: Player/Permanent의 PermanentEffects 및 Until* bucket은 TemporaryModifier/TemporaryGrantedEffect 후보와 연결되어 있지만 original cleanup parity와 production provider catalog adoption이 남아 있다.
- Source files: `DCGO/Assets/Scripts/Script/Player.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`, `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`, `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- Source classes: `Player`, `Permanent`, `TurnStateMachine`, `AttackProcess`
- Source methods/calls: `Player.EffectList`, `Permanent.EffectList_Added`, `UntilEndBattleEffects`, `UntilEachTurnEndEffects`, `UntilOwnerTurnEndEffects`, `UntilEndAttackEffects`, `UntilNextUntapEffects`
- Card data evidence: duration-bound granted effects and modifiers from CardEffectCommons/GiveEffect paths
- Current headless candidates: `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs`, `src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs`, `src/DCGO.RL.Engine/Effects/DurationCleanupService.cs`, `src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs`
- Closure tasks:
- Until* bucket별 cleanup timing을 원본 Player/Permanent/TurnStateMachine/AttackProcess와 대조한다.
- PermanentEffects/provider catalog adoption 미완 범위를 TRUST-001 후보와 분리한다.
- Test candidates: `Duration player runtime modifiers replay deterministic`, `Duration temporary keyword grants Blocker until cleanup`, `Duration temporary granted trigger runs from target permanent timing`, `Script runtime Player effect list uses temporary granted effects`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-05 temporary granted trigger와 FND-003 event timing 의존성

- Classification: NeedsCapabilityRemediation
- Partial reason: TemporaryGrantedEffect는 duration-bound trigger descriptor를 제공하지만 FND-003의 unsupported timing 25건이 source collection/event payload layer와 맞물려 있다.
- Source files: `docs/generated/as-is-restart/fnd-003-next-action-queue.json`, `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`, `DCGO/Assets/Scripts/Script/CardObjectController.cs`
- Source classes: `CardController`, `Permanent`, `CardObjectController`, `AutoProcessing`
- Source methods/calls: `OnDeclaration`, `WhenPermanentWouldBeDeleted`, `OnTappedAnyone`, `WhenRemoveField`, `BeforePayCost`, `OnDetermineDoSecurityCheck`, `OnEndBattle`, `OnDigivolutionCardDiscarded`, `OnAddDigivolutionCards`, `WhenLinked`, `OnMove`, `OnUnTappedAnyone`, `OnUseOption`, `OnDiscardLibrary`, `OnAddSecurity`, `WhenReturntoLibraryAnyone`, `OnDiscardSecurity`, `AfterPayCost`, `WhenDigisorption`, `OnLinkCardDiscarded`, `OnRemovedField`, `OnFaceUpSecurityIncreased`, `WhenWouldLink`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`
- Card data evidence: FND-003 fnd001ContinuousOrStaticHandoffCount=25
- Current headless candidates: `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`, `src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs`, `src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs`
- Closure tasks:
- FND-003 event hooks가 common source payload로 구현/partial/blocked 재분류된 뒤 ContinuousOrStaticEffect dependency를 재계산한다.
- Unsupported timing을 빈 descriptor로 통과시키지 않는다.
- Test candidates: `Duration temporary granted trigger hash and replay deterministic`, `FND-003 event-specific tests after remediation`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-06 supported static keyword descriptor parity

- Classification: CloseableFoundationTask
- Partial reason: Blocker/Rush/Reboot/Collision/Jamming 등 supported static keyword wrappers는 descriptor 후보가 있으나 full-card source parity가 필요하다.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs`
- Source classes: `BlockerClass`, `RushClass`, `RebootClass`, `CollisionClass`, `CanNotBeDestroyedByBattleClass`
- Source methods/calls: `BlockerSelfStaticEffect`, `RushSelfStaticEffect`, `RebootSelfStaticEffect`, `CollisionSelfStaticEffect`, `JammingSelfStaticEffect`
- Card data evidence: BT/EX/ST inherited/static keyword source samples in source-required-capabilities
- Current headless candidates: `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`, `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`, `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs`
- Closure tasks:
- supported keyword factory method별 source sample을 descriptor snapshot 후보로 묶는다.
- condition/source metadata/stable ID가 registry evidence와 일치하는지 검증 후보로 둔다.
- Test candidates: `Continuous static keyword field source grants Blocker`, `CardEffectFactory static keyword wrappers map supported keywords`, `Continuous static keyword replay deterministic`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-07 unsupported trigger/process keyword static factory mapping

- Classification: NeedsSourceMapping
- Partial reason: factory facade는 일부 static keyword만 지원하며 Pierce/Retaliation/Alliance/Vortex/Scapegoat/Iceclad/Progress 등 trigger/process 성격 keyword shape는 source mapping 없이는 static descriptor로 추정하면 안 된다.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Pierce.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Alliance.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/VortexCanAttackPlayers.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Scapegoat.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Iceclad.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Progress.cs`
- Source classes: `ActivateClass`, `VortexCanAttackPlayersClass`, `ScapegoatClass`, `IcecladClass`, `CanNotAffectedClass`
- Source methods/calls: `AllianceStaticEffect`, `VortexCanAttackPlayersStaticEffect`, `ScapegoatStaticEffect`, `IcecladStaticEffect`, `ProgressStaticEffect`
- Card data evidence: keyword effect source samples from CardEffectFactory/KeyWordEffects and CardEffectCommons/KeyWordEffects
- Current headless candidates: `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs`, `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`, `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`
- Closure tasks:
- factory method를 static keyword, trigger keyword, process/replacement keyword로 분류한다.
- unsupported keyword shape는 explicit failure/remediation queue로 남기고 ContinuousOrStaticEffect Verified 근거로 쓰지 않는다.
- Test candidates: `CardEffectFactory keyword static effect rejects unsupported keyword shape`, `future trigger/process keyword mapping fixtures`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-08 static DP/SecurityAttack/SecurityDigimonDP descriptor parity

- Classification: CloseableFoundationTask
- Partial reason: DP/SecurityAttack/SecurityDigimonDP static modifiers have descriptor and runtime tests, but full-card fixture parity must lock source variants and dynamic amount delegates.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeDP.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeSAttack.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeCardDP.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeOriginDP.cs`
- Source classes: `ChangeDPClass`, `ChangeSAttackClass`, `ChangeCardDPClass`, `ChangeBaseDPClass`
- Source methods/calls: `ChangeSelfDPStaticEffect`, `ChangeDPStaticEffect`, `ChangeSelfSAttackStaticEffect`, `ChangeSecurityDigimonCardDPStaticEffect`, `ChangeBaseDPStaticEffect`
- Card data evidence: BT19_026, BT19_045, BT1_005, BT1_018, BT22 C0037/C0038 test samples
- Current headless candidates: `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`, `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`, `src/DCGO.RL.Engine/Battle/EffectiveStatService.cs`, `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs`
- Closure tasks:
- fixed/dynamic amount delegate를 source scaffold sample과 매핑한다.
- base definition mutation 금지와 derived stat hash evidence를 gate 후보로 연결한다.
- Test candidates: `Continuous DP modifier affects effective DP`, `Continuous dynamic SecurityAttack from source count`, `Continuous effects are derived for state hash`, `Continuous and duration modifiers stack deterministically`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-09 static cost modifiers와 pay timing windows

- Classification: NeedsCapabilityRemediation
- Partial reason: play/digivolution/link cost static modifiers는 후보가 있으나 BeforePayCost/AfterPayCost unsupported timing과 원본 cost payment window가 남아 있다.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangePlayCost.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/ChangeDigivolutionCost.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`, `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/CardSource.cs`
- Source classes: `ChangeCostClass`, `AddLinkConditionClass`, `CardController`, `CardSource`
- Source methods/calls: `ChangePlayCostStaticEffect`, `ChangeDigivolutionCostStaticEffect`, `AddLinkConditionStaticEffect`, `BeforePayCost`, `AfterPayCost`, `CardSource.PayingCost`
- Card data evidence: FND-003 BeforePayCost affected 284, AfterPayCost affected 15
- Current headless candidates: `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `src/DCGO.RL.Engine/Mechanics/CostResolver.cs`, `src/DCGO.RL.Engine/Battle/PlayCardService.cs`, `src/DCGO.RL.Engine/Battle/DigivolveService.cs`
- Closure tasks:
- FND-003 BeforePayCost/AfterPayCost remediation 이후 static cost graph와 pay windows를 재검증한다.
- DigiXros/Assembly selected-material exclusion은 별도 source mapping 후보로 남긴다.
- Test candidates: `Static cost modifier adjusts play and digivolution cost`, `Static link cost modifier adjusts link cost`, `future BeforePayCost/AfterPayCost window fixtures`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-10 static restriction/immunity와 replacement window 의존성

- Classification: NeedsCapabilityRemediation
- Partial reason: restriction/immunity descriptors exist but WhenPermanentWouldBeDeleted/WhenRemoveField/WhenReturntoLibraryAnyone 등 replacement-like unsupported timings이 ContinuousOrStaticEffect source payload와 연결된다.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/CanNotBeDeleted.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/CanNotReturnToHand.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/CanNoReturnToDeck.cs`, `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`
- Source classes: `CanNotBeDestroyedClass`, `CannotReturnToHandClass`, `CannotReturnToLibraryClass`, `CardController`, `Permanent`
- Source methods/calls: `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `CannotReturnToHandStaticEffect`, `CannotReturnToDeckStaticEffect`
- Card data evidence: FND-003 WhenPermanentWouldBeDeleted affected 405, WhenRemoveField affected 304
- Current headless candidates: `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`, `src/DCGO.RL.Engine/Battle/AttackService.cs`
- Closure tasks:
- FND-003 replacement/removal timing이 source-aligned task로 분리된 뒤 static restriction coverage를 다시 계산한다.
- restriction을 silent no-op로 처리하지 않고 explicit unsupported/partial로 유지한다.
- Test candidates: `Static restriction blocks attack and block`, `Static card restriction blocks return to hand`, `Static immunity descriptor evaluates metadata`, `future would/delete/remove timing fixtures`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-11 static evolution/link requirement effective gates

- Classification: CloseableFoundationTask
- Partial reason: static digivolution/link requirement, ignore permission, cannot-ignore restriction, effective metadata/level query는 후보가 있으나 source variants 전체 parity가 필요하다.
- Source files: `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`, `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`, `DCGO/Assets/Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPlayer/IgnoreDigivolutionRequirement.cs`, `DCGO/Assets/Scripts/Script/CardSource.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`
- Source classes: `AddDigivolutionRequirementClass`, `AddLinkConditionClass`, `CardSource`, `Permanent`
- Source methods/calls: `AddSelfDigivolutionRequirementStaticEffect`, `AddLinkConditionStaticEffect`, `CardSource.CostList`, `Permanent.Level`, `GetJogressLevels`
- Card data evidence: AD1_010, BT7_017, BT7_111, EX7/P/BT samples with AddSelfDigivolutionRequirementStaticEffect
- Current headless candidates: `src/DCGO.RL.Engine/Effects/StaticRequirementService.cs`, `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs`
- Closure tasks:
- digivolution/link requirement source sample을 fixture 후보로 묶는다.
- effective metadata/level query가 있을 때와 없을 때의 gate 결과를 별도 테스트 후보로 유지한다.
- Test candidates: `Static evolution requirement hand source generates and executes`, `Static evolution requirement cannot-ignore restriction blocks permission`, `Static link requirement uses effective metadata and level`, `Static requirement replay deterministic`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-12 effective card metadata name/trait/text policy

- Classification: NeedsDataPolicy
- Partial reason: CardSource의 BaseCardNames/CardNames/CardTraits 및 text metadata는 static criteria에 필요하지만 CardBaseEntity import/variant/data anomaly policy와 맞물린다.
- Source files: `DCGO/Assets/Scripts/Script/CardSource.cs`, `DCGO/Assets/CardBaseEntity/**/*.asset`, `docs/generated/as-is-restart/asis-card-data-field-summary.json`
- Source classes: `CardSource`, `CEntity_Base`, `CardMetadataSnapshot`
- Source methods/calls: `BaseCardNames`, `CardNames`, `CardTraits`, `HasSameCardName`, `CardTextEnglish`
- Card data evidence: GOAL 09 DATA-001 duplicateCardIdGroupCount=2384, missingEffectSourceCandidateCount=39
- Current headless candidates: `src/DCGO.RL.Engine/Domain/CardMetadataCriteria.cs`, `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `src/DCGO.RL.Engine/Domain/CardDefinition.cs`
- Closure tasks:
- name/trait/text normalization policy를 DATA-001과 연결한다.
- metadata criteria가 asset text 전문 복제 없이 stable field만 참조하도록 한다.
- Test candidates: `Continuous metadata criteria gates target trait and text`, `Static card metadata modifier affects cost criteria`, `Static requirement metadata criteria gates source and target`
- TRUST-001 handoff: yes
- Gate recalculation candidate: no

### FND001-CS-13 effective color/level/face-up security data policy

- Classification: NeedsDataPolicy
- Partial reason: CardSource CardColors/DualCardColors/TreatedLevel/IsFaceUp와 face-up security trigger는 static criteria와 source scope에 필요하지만 data/variant policy를 먼저 고정해야 한다.
- Source files: `DCGO/Assets/Scripts/Script/CardSource.cs`, `DCGO/Assets/Scripts/Script/Permanent.cs`, `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/CardBaseEntity/**/*.asset`
- Source classes: `CardSource`, `Permanent`, `CardController`
- Source methods/calls: `CardColors`, `DualCardColors`, `TreatedLevel`, `IsFaceUp`, `SetFaceUp`, `OnFaceUpSecurityIncreased`
- Card data evidence: FND-003 OnFaceUpSecurityIncreased DataPolicyRequired affected 2
- Current headless candidates: `src/DCGO.RL.Engine/Effects/StaticEffectService.cs`, `src/DCGO.RL.Engine/Domain/CardDefinition.cs`, `src/DCGO.RL.Engine/Domain/CardInstance.cs`
- Closure tasks:
- face-up security와 color/level variant policy를 DATA-001에 넘긴다.
- effective card/base/current color와 option color requirement fixture 후보를 유지한다.
- Test candidates: `Static card color modifier affects option color requirement`, `Static ignore color requirement permits option`, `Static card level modifier feeds permanent level requirement`, `Continuous face-up security source applies`
- TRUST-001 handoff: yes
- Gate recalculation candidate: no

### FND001-CS-14 AutoProcessing priority/cut-in/background ordering parity

- Classification: NeedsSourceMapping
- Partial reason: AutoProcessing.GetSkillInfos/StackSkillInfos/ActivateBackgroundEffects와 cut-in/background ordering은 source collection의 실제 실행 순서를 좌우하므로 source mapping 없이 Verified 근거로 쓸 수 없다.
- Source files: `DCGO/Assets/Scripts/Script/AutoProcessing.cs`, `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- Source classes: `AutoProcessing`, `SkillInfo`, `CardController`, `TurnStateMachine`
- Source methods/calls: `GetSkillInfos`, `GetSkillInfosOfCards`, `StackSkillInfos`, `ActivateBackgroundEffects`, `autoProcessing_CutIn`
- Card data evidence: BeforePayCost/OnUseOption/OnSecurityCheck/cut-in samples in GOAL 05 call graph
- Current headless candidates: `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs`, `src/DCGO.RL.Engine/Effects/TriggerCollector.cs`, `src/DCGO.RL.Engine/Effects/SelectionContinuation.cs`
- Closure tasks:
- priority/cut-in/background ordering을 ContinuousOrStaticEffect closure blocker 또는 별도 capability로 분리한다.
- 동시 trigger priority를 현재 passing tests만으로 source parity 처리하지 않는다.
- Test candidates: `future MultipleSkills priority/source ordering fixtures`, `existing TriggerPipeline continuation tests as candidates only`
- TRUST-001 handoff: yes
- Gate recalculation candidate: yes

### FND001-CS-15 CardEffect body-specific static/continuous variants 제외

- Classification: CardEffectBodySpecific
- Partial reason: 일부 static/continuous-like surface는 개별 CardEffect body나 special mechanic 분해가 필요하므로 FND-001에서 구현하지 않는다.
- Source files: `DCGO/Assets/Scripts/CardEffect/**/*.cs`, `docs/generated/as-is-restart/fnd-003-next-action-queue.json`
- Source classes: `individual CardEffect classes`, `OnUseDigiburst special mechanic`
- Source methods/calls: `OnUseDigiburst`, `card-specific CanUse/Activate bodies`
- Card data evidence: FND-003 CardEffectBodySpecificCount=1
- Current headless candidates: `future card-specific porting only after OpenCodeReady`, `generated full-card source scaffold`
- Closure tasks:
- FND-001 closure list에서 개별 CardEffect body 구현을 제외한다.
- special mechanic은 별도 remediation queue로만 남긴다.
- Test candidates: `none in FND-001; future mechanic-specific tests only`
- TRUST-001 handoff: yes
- Gate recalculation candidate: no

### FND001-CS-16 Unity visual/display-only continuous surfaces 제외

- Classification: UnityOnlyExcluded
- Partial reason: Security display, ShowCardEffect, BrainStormCoroutine, ContinuousController coroutine/UI surfaces는 headless closure 근거가 아니라 UnityAdapter/visual-only 범위다.
- Source files: `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/CardObjectController.cs`
- Source classes: `ContinuousController`, `Effects`, `BrainStormObject`
- Source methods/calls: `ShowCardEffect`, `MoveToExecuteCardEffect`, `BrainStormCoroutine`, `ShrinkSecurityDigimonDisplay`
- Card data evidence: visual/security display calls around security and remove-field flows
- Current headless candidates: `UnityAdapter future scope`, `DecisionPoint/SelectionRequest for non-visual decisions`
- Closure tasks:
- visual-only calls를 ContinuousOrStaticEffect foundation blocker에서 제외한다.
- rule-visible state 변화가 있는 호출만 source mapping 대상으로 유지한다.
- Test candidates: `none for visual display; only state-change parity candidates remain`
- TRUST-001 handoff: no
- Gate recalculation candidate: no

