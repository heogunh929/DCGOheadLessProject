# TRUST-001 Src Implementation Trust Boundary

이 문서는 기존 `src` headless 구현을 신뢰하지 않는 상태에서 SourceOfTruth mapping evidence 기준으로 재사용 가능/폐기 후보/검증 후보를 분류한 결과다.
구현, `src` 수정, 원본 DCGO 수정, CardEffect body 구현, generated status 승격, Foundation Gate 수치 조작은 수행하지 않았다.

## Scope

- SourceOfTruth root: `E:/headlessDCGO/DCGO`
- SourceOfTruth assets root: `E:/headlessDCGO/DCGO/Assets`
- src C# file count: 338
- OpenCodeReady: `False`
- Generated Implemented/Verified count: 0
- Full-card parity passed/not-run: 0 / 3918

## Trust Policy

- local `Implemented` 또는 `Verified` token은 신뢰 승격 근거가 아니다.
- SourceOfTruth class/file/data mapping, generated gate, parity evidence가 같이 맞기 전까지 기존 구현은 검증 대상이다.
- CardEffect 파일은 카드별 body 구현 여부가 아니라 원본 class match와 generated 상태/근거만 본다.
- 테스트는 정적 inventory와 실행 가능 후보만 기록한다. TRUST-001에서는 테스트를 실행하지 않는다.
- OpenCodeReady=false 상태이므로 C0039 이후 card-porting과 RL 구성요소는 Deferred다.

## Classification Counts

| Classification | Count |
| --- | --- |
| ReuseCandidate | 52 |
| VerifiedCandidateNeedsTest | 27 |
| PartialNeedsWork | 50 |
| StaleOrWrongMapping | 0 |
| BlockedByFoundation | 185 |
| BlockedByDataPolicy | 17 |
| DeleteCandidate | 7 |
| ManualReview | 0 |

## Source Mapping Kinds

| Source mapping kind | Count |
| --- | --- |
| ActionBoundaryConcept | 1 |
| CardBaseEntityDataSource | 5 |
| CardDataNoEffectOrMissingEffectClassPolicy | 12 |
| CardEffectRegistryScaffold | 20 |
| ComplexMechanicSource | 5 |
| DirectBattleSource | 9 |
| DirectCommonEffectSource | 2 |
| DirectContinuousStaticSource | 6 |
| DirectEffectRegistrySource | 7 |
| DirectEffectRuntimeSource | 6 |
| DirectRuntimeSource | 4 |
| DirectSourceEffectClass | 165 |
| DirectTimingPipelineSource | 10 |
| GeneratedEvidenceHarness | 25 |
| HeadlessInvariantPolicy | 2 |
| NoSourceMapping | 7 |
| PrimitiveSource | 1 |
| RuntimeConceptSource | 8 |
| RuntimeStateSource | 22 |
| SelectionConceptSource | 10 |
| SetupSource | 9 |
| TestInventoryCandidate | 2 |

## Feature Groups

| Group | Files | Classification | Mapping kind | Source evidence |
| --- | --- | --- | --- | --- |
| ActionModel | 1 | ReuseCandidate | ActionBoundaryConcept | E:/headlessDCGO/DCGO/Assets/Scripts/Script/TurnStateMachine.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs |
| BattleOrchestration | 8 | ReuseCandidate | RuntimeConceptSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/GameContext.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/TurnStateMachine.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Player.cs |
| BattleRuntime | 9 | PartialNeedsWork | DirectBattleSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/AttackProcess.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/TurnStateMachine.cs |
| CEntityEffectRegistryFoundation | 7 | PartialNeedsWork | DirectEffectRegistrySource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CEntity_EffectController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardSource.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Permanent.cs |
| CardDataPolicy | 5 | BlockedByDataPolicy | CardBaseEntityDataSource | E:/headlessDCGO/DCGO/Assets/CardBaseEntity, docs/generated/as-is-restart/asis-card-data-structure.json |
| CardEffectFactoryFoundation | 2 | PartialNeedsWork | DirectCommonEffectSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectFactory.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectCommons.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs |
| CardEffectLocalScript | 165 | BlockedByFoundation | DirectSourceEffectClass | (none) |
| CardEffectNoSourceToken | 12 | BlockedByDataPolicy | CardDataNoEffectOrMissingEffectClassPolicy | docs/generated/as-is-restart/asis-card-data-structure.json |
| CardScriptCatalogSupport | 20 | BlockedByFoundation | CardEffectRegistryScaffold | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CEntity_EffectController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardSource.cs, docs/generated/full-card-source-scaffold/index.json |
| ContinuousStaticFoundation | 6 | PartialNeedsWork | DirectContinuousStaticSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectFactory.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffects |
| DecisionBoundary | 10 | ReuseCandidate | SelectionConceptSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/SelectCardPanel.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/SelectPermanentEffect.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs |
| DeterministicInfrastructure | 2 | ReuseCandidate | HeadlessInvariantPolicy | docs/codex-prompts/GOAL_FOUNDATION_COMPLETION.md |
| DomainStateModel | 22 | ReuseCandidate | RuntimeStateSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/GameContext.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Player.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Permanent.cs |
| GeneratedBuildArtifacts | 7 | DeleteCandidate | NoSourceMapping | (none) |
| MechanicFoundation | 5 | PartialNeedsWork | ComplexMechanicSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectFactory.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffects |
| PrimitiveFoundation | 1 | PartialNeedsWork | PrimitiveSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectCommons.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Player.cs |
| ScriptRuntimeDomainFoundation | 4 | PartialNeedsWork | DirectRuntimeSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/GameContext.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Player.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardSource.cs |
| ScriptRuntimeEffectFoundation | 6 | PartialNeedsWork | DirectEffectRuntimeSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CEntity_Effect.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CEntity_EffectController.cs |
| SetupRuntime | 9 | ReuseCandidate | SetupSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/GameContext.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/Player.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/TurnStateMachine.cs |
| TestHarness | 2 | VerifiedCandidateNeedsTest | TestInventoryCandidate | docs/generated/as-is-restart/asis-headless-trust-audit.json |
| TriggerPipelineFoundation | 10 | PartialNeedsWork | DirectTimingPipelineSource | E:/headlessDCGO/DCGO/Assets/Scripts/Script/AutoProcessing.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs, E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs |
| ValidationHarness | 25 | VerifiedCandidateNeedsTest | GeneratedEvidenceHarness | docs/generated/foundation-completion-gate.json, docs/generated/full-card-parity-evidence.json, docs/generated/as-is-restart/asis-headless-trust-audit.json |

## ReuseCandidate

- `src/DCGO.RL.Engine/Actions/GameAction.cs` (ActionModel)
- `src/DCGO.RL.Engine/Battle/BattleEngineServices.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/BattleResolver.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/EngineSession.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/HatchService.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/MoveFromBreedingService.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/PhaseRunner.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/RuleProcessor.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Battle/TurnRunner.cs` (BattleOrchestration)
- `src/DCGO.RL.Engine/Decisions/DecisionPoint.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/DecisionToken.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/IDecisionProvider.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/LegalAction.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/SelectEffectFacades.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/SelectableTarget.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/SelectionRequest.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/SelectionResult.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/SelectionValidator.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Decisions/TestDecisionProvider.cs` (DecisionBoundary)
- `src/DCGO.RL.Engine/Domain/AttackRuntimeContext.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/CardInstance.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/CardInstanceFactory.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/ComplexMechanicDefinitions.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/DigivolveCardCommand.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/DigivolveCardResult.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/DomainException.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/Enums.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/GameConfig.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/GameResult.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/GameState.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/IZoneMover.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/Ids.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/MoveCardCommand.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/MoveCardResult.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/PermanentState.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/PermanentZoneMoveCommand.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/PermanentZoneMoveResult.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/PlayerState.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/UnsupportedMechanicException.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/ZoneMoveTrace.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Domain/ZoneMover.cs` (DomainStateModel)
- `src/DCGO.RL.Engine/Infrastructure/IDeterministicRng.cs` (DeterministicInfrastructure)
- `src/DCGO.RL.Engine/Infrastructure/XorShiftDeterministicRng.cs` (DeterministicInfrastructure)
- `src/DCGO.RL.Engine/Setup/DeckInstantiationService.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/DrawService.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/FirstPlayerSelector.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/GameSetupRequest.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/GameSetupResult.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/GameSetupService.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/PlayerDeckList.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/SecuritySetupService.cs` (SetupRuntime)
- `src/DCGO.RL.Engine/Setup/WinConditionChecker.cs` (SetupRuntime)

## VerifiedCandidateNeedsTest

- `src/DCGO.RL.Engine.Tests/CardEffectTestFixture.cs` (TestHarness)
- `src/DCGO.RL.Engine.Tests/Program.cs` (TestHarness)
- `src/DCGO.RL.Engine/Validation/AssetRegistryMappingValidator.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/CliDebugRenderer.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/DeckMechanicValidator.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/DeckValidationReport.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineCompletionChecklistRunner.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineCoreExpansionReadiness.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineCoreMilestoneGate.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineInvariantChecker.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineInvariantReport.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/EngineInvariantViolation.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/FullCardPoolValidator.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/FullCardSourceScaffoldValidator.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/GameTrace.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/ParityFixtureComparer.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/ParityTrace.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/RandomLegalActionRunner.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/ReplayDeterminismHelper.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/ReplayRunner.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/RuleVisibleSnapshot.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/ScriptedScenarioRunner.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/StateHasher.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/TargetCardPoolValidationReport.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/TraceEvent.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/TraceStore.cs` (ValidationHarness)
- `src/DCGO.RL.Engine/Validation/UnsupportedMechanicReporter.cs` (ValidationHarness)

## PartialNeedsWork

- `src/DCGO.RL.Engine/Battle/ActionExecutor.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/AttackRuntimeOperations.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/AttackService.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/BattleRules.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/DigivolveService.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/LegalActionGenerator.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/PlayCardService.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/Battle/SecurityCheckService.cs` (BattleRuntime)
- `src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CEntityEffectRegistryBuilder.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CardEffectCommons.cs` (CardEffectFactoryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs` (CardEffectFactoryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CardEffectPortingStatus.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CardScriptContext.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/CardScriptRegistry.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/ICEntityEffectFactoryProvider.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/CardEffects/ICardScript.cs` (CEntityEffectRegistryFoundation)
- `src/DCGO.RL.Engine/Domain/RuntimeRuleState.cs` (ScriptRuntimeDomainFoundation)
- `src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs` (ScriptRuntimeDomainFoundation)
- `src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs` (ScriptRuntimeDomainFoundation)
- `src/DCGO.RL.Engine/Domain/TemporaryModifier.cs` (ScriptRuntimeDomainFoundation)
- `src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/DurationCleanupService.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/EffectContext.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/EffectDescriptor.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/EffectQueue.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/EffectResolution.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/EffectTiming.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/EnterFieldEventPayload.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/OncePerTurnTracker.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/OptionalEffectBoundary.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/RuleStabilizationResult.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs` (ScriptRuntimeEffectFoundation)
- `src/DCGO.RL.Engine/Effects/SecurityEffectExecutionService.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/SelectionResultApplicator.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/StaticEffectService.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/StaticRequirementService.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs` (ContinuousStaticFoundation)
- `src/DCGO.RL.Engine/Effects/TriggerCollector.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Effects/TriggerSourceSnapshot.cs` (TriggerPipelineFoundation)
- `src/DCGO.RL.Engine/Mechanics/ComplexMechanicMatcher.cs` (MechanicFoundation)
- `src/DCGO.RL.Engine/Mechanics/ComplexMechanicModels.cs` (MechanicFoundation)
- `src/DCGO.RL.Engine/Mechanics/ComplexMechanicSelectionFactory.cs` (MechanicFoundation)
- `src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs` (MechanicFoundation)
- `src/DCGO.RL.Engine/Mechanics/CostResolver.cs` (MechanicFoundation)
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs` (PrimitiveFoundation)

## BlockedByFoundation

- `src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_029.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_036.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_043.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_086.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Bt1CardScriptCatalog.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT1/Bt1ScriptSupport.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_067.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_070.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_074.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_088.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_089.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_010.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_017.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_018.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_023.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_025.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_085.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_005.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_048.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_049.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_053.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_055.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_060.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_061.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_062.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_063.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_087.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_057.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_058.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_059.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_062.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_068.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Bt18CardScriptCatalog.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT18/Bt18ScriptSupport.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_052.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_056.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_090.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_075.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_080.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_085.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_093.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Red/BT18_087.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT18/Yellow/BT18_098.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Black/BT19_055.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_016.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_021.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_022.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_026.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_027.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_028.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_092.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Bt19CardScriptCatalog.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT19/Bt19ScriptSupport.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_045.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_046.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_052.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_084.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_096.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_006.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_067.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_071.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Red/BT19_015.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Red/BT19_089.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/White/BT19_077.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Yellow/BT19_039.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT19/Yellow/BT19_041.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_048.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_049.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_055.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_086.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Bt20CardScriptCatalog.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT20/Bt20ScriptSupport.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_004.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_039.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_006.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_062.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_063.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_065.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_067.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_069.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_072.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_075.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_079.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_088.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_096.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_001.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_009.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/White/BT20_092.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_003.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_034.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Black/BT21_055.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_003.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_033.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_085.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_095.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21CardScriptCatalog.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21ScriptSupport.cs` (CardScriptCatalogSupport)
- `src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_048.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_049.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_063.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_065.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_088.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_099.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_001.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_007.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_008.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_011.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_012.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_015.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_024.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_080.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_082.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_090.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_091.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_092.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_093.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_005.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_054.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_056.cs` (CardEffectLocalScript)
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_065.cs` (CardEffectLocalScript)
- ... 65 more entries in JSON

## BlockedByDataPolicy

- `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_02.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_04.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_05.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_10.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_02.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_04.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_05.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_10.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_02.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_03.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_06.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_10.cs` (CardEffectNoSourceToken)
- `src/DCGO.RL.Engine/Domain/CardDefinition.cs` (CardDataPolicy)
- `src/DCGO.RL.Engine/Domain/CardDefinitionIdentity.cs` (CardDataPolicy)
- `src/DCGO.RL.Engine/Domain/CardMetadataCriteria.cs` (CardDataPolicy)
- `src/DCGO.RL.Engine/Domain/ICardDatabase.cs` (CardDataPolicy)
- `src/DCGO.RL.Engine/Domain/InMemoryCardDatabase.cs` (CardDataPolicy)

## DeleteCandidate

- `src/DCGO.RL.Engine.Tests/.tmp/66AA-build-obj/Debug/net8.0/DCGO.RL.Engine.Tests.GlobalUsings.g.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/DCGO.RL.Engine.Tests.AssemblyInfo.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/DCGO.RL.Engine.Tests.GlobalUsings.g.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine/obj/Debug/net8.0/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine/obj/Debug/net8.0/DCGO.RL.Engine.AssemblyInfo.cs` (GeneratedBuildArtifacts)
- `src/DCGO.RL.Engine/obj/Debug/net8.0/DCGO.RL.Engine.GlobalUsings.g.cs` (GeneratedBuildArtifacts)

## ManualReview

- 없음

## File Inventory

전체 `src` C# 파일은 아래 표와 JSON `files` 배열에 전수 기록했다.

| Path | Classification | Group | Mapping | Local status tokens |
| --- | --- | --- | --- | --- |
| src/DCGO.RL.Engine.Tests/.tmp/66AA-build-obj/Debug/net8.0/DCGO.RL.Engine.Tests.GlobalUsings.g.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine.Tests/CardEffectTestFixture.cs | VerifiedCandidateNeedsTest | TestHarness | TestInventoryCandidate | Implemented |
| src/DCGO.RL.Engine.Tests/Program.cs | VerifiedCandidateNeedsTest | TestHarness | TestInventoryCandidate | Implemented, NoEffect, PartiallyImplemented, Unsupported |
| src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/DCGO.RL.Engine.Tests.AssemblyInfo.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine.Tests/obj/Debug/net8.0/DCGO.RL.Engine.Tests.GlobalUsings.g.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine/Actions/GameAction.cs | ReuseCandidate | ActionModel | ActionBoundaryConcept |  |
| src/DCGO.RL.Engine/Battle/ActionExecutor.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/AttackRuntimeOperations.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/AttackService.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/BattleEngineServices.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/BattleKeywordService.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/BattleResolver.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/BattleRules.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/DigivolveService.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/EngineSession.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/HatchService.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/LegalActionGenerator.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/MoveFromBreedingService.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/PhaseRunner.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/PlayCardService.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/RuleProcessor.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/Battle/SecurityCheckService.cs | PartialNeedsWork | BattleRuntime | DirectBattleSource |  |
| src/DCGO.RL.Engine/Battle/TurnRunner.cs | ReuseCandidate | BattleOrchestration | RuntimeConceptSource |  |
| src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_029.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_036.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_043.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Blue/BT1_086.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Bt1CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT1/Bt1ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_067.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_070.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_074.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_088.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Green/BT1_089.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_010.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_017.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_018.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_023.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_025.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Red/BT1_085.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_005.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_048.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_049.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_053.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_055.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_060.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_061.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_062.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_063.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT1/Yellow/BT1_087.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_057.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_058.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_059.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_062.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Black/BT18_068.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Bt18CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT18/Bt18ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_052.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_056.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Green/BT18_090.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_075.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_080.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_085.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT18/Purple/BT18_093.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Red/BT18_087.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT18/Yellow/BT18_098.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Black/BT19_055.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_016.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_021.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_022.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_026.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_027.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_028.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Blue/BT19_092.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Bt19CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT19/Bt19ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_045.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_046.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_052.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_084.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Green/BT19_096.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_006.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_067.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Purple/BT19_071.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Red/BT19_015.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Red/BT19_089.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/White/BT19_077.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT19/Yellow/BT19_039.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT19/Yellow/BT19_041.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_048.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_049.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_055.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_086.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Bt20CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT20/Bt20ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_004.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_039.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_006.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_062.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_063.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_065.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_067.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_069.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_072.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_075.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_079.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_088.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_096.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_001.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_009.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/White/BT20_092.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_003.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_034.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Black/BT21_055.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_003.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_033.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_085.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_095.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Bt21CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT21/Bt21ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_048.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_049.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_063.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_065.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_088.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_099.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_001.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_007.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_008.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_011.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_012.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_015.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_024.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_080.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_082.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_090.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_091.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_092.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_093.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_005.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_054.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_056.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_065.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_001.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_018.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_021.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_027.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Bt22CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT22/Bt22ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_004.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_043.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_044.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_046.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_047.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_048.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_051.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_006.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_069.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_077.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_092.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/White/BT22_079.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/White/BT22_093.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | PartiallyImplemented |
| src/DCGO.RL.Engine/CardEffects/BT22/Yellow/BT22_002.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/BT22/Yellow/BT22_040.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Unsupported |
| src/DCGO.RL.Engine/CardEffects/CEntityEffectFactoryCatalog.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/CEntityEffectRegistryBuilder.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/CardEffectCommons.cs | PartialNeedsWork | CardEffectFactoryFoundation | DirectCommonEffectSource |  |
| src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs | PartialNeedsWork | CardEffectFactoryFoundation | DirectCommonEffectSource |  |
| src/DCGO.RL.Engine/CardEffects/CardEffectPortingStatus.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource | Implemented, NoEffect, Verified |
| src/DCGO.RL.Engine/CardEffects/CardScriptContext.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/CardScriptRegistry.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/ICEntityEffectFactoryProvider.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/ICardScript.cs | PartialNeedsWork | CEntityEffectRegistryFoundation | DirectEffectRegistrySource |  |
| src/DCGO.RL.Engine/CardEffects/NoEffectCardScript.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold | NoEffect |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_01.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_02.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_03.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_04.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_05.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_06.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_07.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_08.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_09.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_10.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_11.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_12.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_13.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_14.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_15.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_16.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST1/Red/St1ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_01.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_02.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_03.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_04.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_05.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_06.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_07.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_08.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_09.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_10.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_11.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_12.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_13.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_14.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_15.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_16.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST2/Blue/St2ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_01.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_02.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_03.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_04.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_05.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_06.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_07.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_08.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_09.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_10.cs | BlockedByDataPolicy | CardEffectNoSourceToken | CardDataNoEffectOrMissingEffectClassPolicy |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_11.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass |  |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_12.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_13.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_14.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_15.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_16.cs | BlockedByFoundation | CardEffectLocalScript | DirectSourceEffectClass | Implemented |
| src/DCGO.RL.Engine/CardEffects/ST3/Yellow/St3ScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold | Implemented |
| src/DCGO.RL.Engine/CardEffects/St1CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/St2St3CardScriptCatalog.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/StarterScriptSupport.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold |  |
| src/DCGO.RL.Engine/CardEffects/UnsupportedCardScript.cs | BlockedByFoundation | CardScriptCatalogSupport | CardEffectRegistryScaffold | Unsupported |
| src/DCGO.RL.Engine/Decisions/DecisionPoint.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/DecisionToken.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/IDecisionProvider.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/LegalAction.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/SelectEffectFacades.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/SelectableTarget.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/SelectionRequest.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/SelectionResult.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/SelectionValidator.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Decisions/TestDecisionProvider.cs | ReuseCandidate | DecisionBoundary | SelectionConceptSource |  |
| src/DCGO.RL.Engine/Domain/AttackRuntimeContext.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/CardDefinition.cs | BlockedByDataPolicy | CardDataPolicy | CardBaseEntityDataSource |  |
| src/DCGO.RL.Engine/Domain/CardDefinitionIdentity.cs | BlockedByDataPolicy | CardDataPolicy | CardBaseEntityDataSource |  |
| src/DCGO.RL.Engine/Domain/CardInstance.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/CardInstanceFactory.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/CardMetadataCriteria.cs | BlockedByDataPolicy | CardDataPolicy | CardBaseEntityDataSource |  |
| src/DCGO.RL.Engine/Domain/ComplexMechanicDefinitions.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/DigivolveCardCommand.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/DigivolveCardResult.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/DomainException.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/Enums.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/GameConfig.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/GameResult.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/GameState.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/ICardDatabase.cs | BlockedByDataPolicy | CardDataPolicy | CardBaseEntityDataSource |  |
| src/DCGO.RL.Engine/Domain/IZoneMover.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/Ids.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/InMemoryCardDatabase.cs | BlockedByDataPolicy | CardDataPolicy | CardBaseEntityDataSource |  |
| src/DCGO.RL.Engine/Domain/MoveCardCommand.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/MoveCardResult.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/PermanentState.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/PermanentZoneMoveCommand.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/PermanentZoneMoveResult.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/PlayerState.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/RuntimeRuleState.cs | PartialNeedsWork | ScriptRuntimeDomainFoundation | DirectRuntimeSource |  |
| src/DCGO.RL.Engine/Domain/ScriptRuntimeFoundation.cs | PartialNeedsWork | ScriptRuntimeDomainFoundation | DirectRuntimeSource |  |
| src/DCGO.RL.Engine/Domain/TemporaryGrantedEffect.cs | PartialNeedsWork | ScriptRuntimeDomainFoundation | DirectRuntimeSource |  |
| src/DCGO.RL.Engine/Domain/TemporaryModifier.cs | PartialNeedsWork | ScriptRuntimeDomainFoundation | DirectRuntimeSource |  |
| src/DCGO.RL.Engine/Domain/UnsupportedMechanicException.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/ZoneMoveTrace.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Domain/ZoneMover.cs | ReuseCandidate | DomainStateModel | RuntimeStateSource |  |
| src/DCGO.RL.Engine/Effects/CEntityEffectRegistry.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/DurationCleanupService.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/EffectContext.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/EffectDescriptor.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/EffectQueue.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/EffectResolution.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/EffectTiming.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/EnterFieldEventPayload.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/OncePerTurnTracker.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/OptionalEffectBoundary.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/RuleStabilizationResult.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/ScriptRuntimeEffectFoundation.cs | PartialNeedsWork | ScriptRuntimeEffectFoundation | DirectEffectRuntimeSource |  |
| src/DCGO.RL.Engine/Effects/SecurityEffectExecutionService.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/SelectionResultApplicator.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/StaticEffectService.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/StaticRequirementService.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/TemporaryGrantedEffectRegistry.cs | PartialNeedsWork | ContinuousStaticFoundation | DirectContinuousStaticSource |  |
| src/DCGO.RL.Engine/Effects/TriggerCollector.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/TriggerPipelineService.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Effects/TriggerSourceSnapshot.cs | PartialNeedsWork | TriggerPipelineFoundation | DirectTimingPipelineSource |  |
| src/DCGO.RL.Engine/Infrastructure/IDeterministicRng.cs | ReuseCandidate | DeterministicInfrastructure | HeadlessInvariantPolicy |  |
| src/DCGO.RL.Engine/Infrastructure/XorShiftDeterministicRng.cs | ReuseCandidate | DeterministicInfrastructure | HeadlessInvariantPolicy |  |
| src/DCGO.RL.Engine/Mechanics/ComplexMechanicMatcher.cs | PartialNeedsWork | MechanicFoundation | ComplexMechanicSource |  |
| src/DCGO.RL.Engine/Mechanics/ComplexMechanicModels.cs | PartialNeedsWork | MechanicFoundation | ComplexMechanicSource |  |
| src/DCGO.RL.Engine/Mechanics/ComplexMechanicSelectionFactory.cs | PartialNeedsWork | MechanicFoundation | ComplexMechanicSource |  |
| src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs | PartialNeedsWork | MechanicFoundation | ComplexMechanicSource |  |
| src/DCGO.RL.Engine/Mechanics/CostResolver.cs | PartialNeedsWork | MechanicFoundation | ComplexMechanicSource |  |
| src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs | PartialNeedsWork | PrimitiveFoundation | PrimitiveSource |  |
| src/DCGO.RL.Engine/Setup/DeckInstantiationService.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/DrawService.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/FirstPlayerSelector.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/GameSetupRequest.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/GameSetupResult.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/GameSetupService.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/PlayerDeckList.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/SecuritySetupService.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Setup/WinConditionChecker.cs | ReuseCandidate | SetupRuntime | SetupSource |  |
| src/DCGO.RL.Engine/Validation/AssetRegistryMappingValidator.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness | NoEffect, Unsupported |
| src/DCGO.RL.Engine/Validation/CliDebugRenderer.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/DeckMechanicValidator.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness | NoEffect, PartiallyImplemented, StubbedForValidation, Unsupported |
| src/DCGO.RL.Engine/Validation/DeckValidationReport.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineCompletionChecklistRunner.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineCoreExpansionReadiness.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineCoreMilestoneGate.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineInvariantChecker.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineInvariantReport.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/EngineInvariantViolation.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/FullCardPoolValidator.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/FullCardSourceScaffoldValidator.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/GameTrace.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/ParityFixtureComparer.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/ParityTrace.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/RandomLegalActionRunner.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/ReplayDeterminismHelper.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/ReplayRunner.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/RuleVisibleSnapshot.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/ScriptedScenarioRunner.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/StateHasher.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/TargetCardPoolValidationReport.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness | PartiallyImplemented, Unsupported |
| src/DCGO.RL.Engine/Validation/TraceEvent.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/TraceStore.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/Validation/UnsupportedMechanicReporter.cs | VerifiedCandidateNeedsTest | ValidationHarness | GeneratedEvidenceHarness |  |
| src/DCGO.RL.Engine/obj/Debug/net8.0/.NETCoreApp,Version=v8.0.AssemblyAttributes.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine/obj/Debug/net8.0/DCGO.RL.Engine.AssemblyInfo.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |
| src/DCGO.RL.Engine/obj/Debug/net8.0/DCGO.RL.Engine.GlobalUsings.g.cs | DeleteCandidate | GeneratedBuildArtifacts | NoSourceMapping |  |

## Test Inventory

- Static test case count from GOAL 08: 611
- Parsed test case tuple names: 620
- Execution candidate: `.\.dotnet\dotnet.exe run --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj`
- TRUST-001에서는 테스트를 실행하지 않았다.

## Trust Conclusion

- Existing implementation trust: Do not trust as Verified. Reuse only as source-mapped candidates until SourceOfTruth parity and foundation blockers are closed.
- Next implementation start possible: `False`
- Reason: OpenCodeReady=false, unsupported capability count=26, partially implemented capability count=37, full-card parity NotRun.

## Non-Goals Confirmed

- `src/` 구현 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.
- 기존 구현을 Verified로 확정하지 않음.
- generated status 승격 없음.
- Foundation Gate 수치 조작 없음.
- commit/push 없음.

## Recommended Commit Message

`docs: define TRUST-001 src implementation trust boundary`
