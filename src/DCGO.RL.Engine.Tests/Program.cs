using DCGO.RL.Engine.Actions;
using DCGO.RL.Engine.Battle;
using DCGO.RL.Engine.CardEffects;
using DCGO.RL.Engine.Decisions;
using DCGO.RL.Engine.Domain;
using DCGO.RL.Engine.Effects;
using DCGO.RL.Engine.Infrastructure;
using DCGO.RL.Engine.Mechanics;
using DCGO.RL.Engine.Primitives;
using DCGO.RL.Engine.Setup;
using DCGO.RL.Engine.Validation;
using System.Text.Json;

var tests = new (string Name, Action Test)[]
{
    ("GameState 생성", GameStateCreation),
    ("PlayerState zone list 초기화", PlayerStateZoneInitialization),
    ("PlayerState zone list 비공유", PlayerStateZoneListsAreIndependent),
    ("PermanentState top/source 구분", PermanentStateSeparatesTopAndSources),
    ("TurnPlayer/NonTurnPlayer helper", TurnPlayerHelpers),
    ("CardDefinition 생성", CardDefinitionCreation),
    ("CardInstanceFactory instance id 분리", CardInstanceFactoryCreatesDistinctInstances),
    ("CardDatabase CardId 조회", CardDatabaseLookupByCardId),
    ("CardDatabase duplicate CardId fails", CardDatabaseDuplicateCardIdFails),
    ("CardDefinition immutable 컬렉션", CardDefinitionDefensivelyCopiesCollections),
    ("ZoneMover Deck -> Hand", ZoneMoverDeckToHand),
    ("ZoneMover Deck -> Security", ZoneMoverDeckToSecurity),
    ("ZoneMover Hand -> Trash", ZoneMoverHandToTrash),
    ("ZoneMover field top -> Trash", ZoneMoverFieldTopToTrashPromotesSource),
    ("ZoneMover evolution source -> Trash", ZoneMoverEvolutionSourceToTrash),
    ("ZoneMover 잘못된 source zone 예외", ZoneMoverInvalidSourceZoneThrows),
    ("ZoneMover public zone 중복 예외", ZoneMoverDuplicatePublicZoneThrows),
    ("ZoneMover trace 생성", ZoneMoverCreatesTrace),
    ("deterministic RNG 동일 seed", DeterministicRngSameSeed),
    ("GameState clone deep copy", GameStateCloneDoesNotShareMutableLists),
    ("Clone 후 원본 수정 격리", OriginalMutationDoesNotAffectClone),
    ("StateHash 안정성과 변화", StateHashIsStableAndChangesAfterZoneMove),
    ("SelectionRequest 생성", SelectionRequestCreation),
    ("SelectionValidator 후보 수 검증", SelectionValidatorValidatesMinMax),
    ("SelectionValidator skip 허용", SelectionValidatorAllowsSkip),
    ("SelectionValidator invalid target 실패", SelectionValidatorRejectsInvalidTarget),
    ("SelectionValidator option value 검증", SelectionValidatorValidatesOptionValue),
    ("SelectionResultApplicator valid result applies continuation", SelectionResultApplicatorValidResultAppliesContinuation),
    ("SelectionResultApplicator invalid target fails", SelectionResultApplicatorInvalidTargetFails),
    ("SelectionResultApplicator min max violation fails", SelectionResultApplicatorMinMaxViolationFails),
    ("SelectionResultApplicator rejects required skip", SelectionResultApplicatorRejectsRequiredSkip),
    ("SelectionResultApplicator empty non-skip applies continuation", SelectionResultApplicatorEmptyNonSkipAppliesContinuation),
    ("SelectionResultApplicator rejects moved card target", SelectionResultApplicatorRejectsMovedCardTarget),
    ("SelectionResultApplicator test effect destroys permanent", SelectionResultApplicatorTestEffectDestroysPermanent),
    ("LegalAction -> SelectionRequest 변환", LegalActionConvertsToSelectionRequest),
    ("CLI DebugLabel 생성", SelectionDebugLabelCreation),
    ("TestDecisionProvider 선택 검증", TestDecisionProviderValidatesSelection),
    ("Unity assembly 참조 없음", EngineDoesNotReferenceUnityAssemblies),
    ("EngineInvariantChecker valid state", EngineInvariantCheckerValidStatePasses),
    ("EngineInvariantChecker duplicate zone card", EngineInvariantCheckerDuplicateZoneCardFails),
    ("EngineInvariantChecker CurrentZone mismatch", EngineInvariantCheckerCurrentZoneMismatchFails),
    ("StateHasher same state same hash", StateHasherSameStateSameHash),
    ("GameTrace records TraceEvent", GameTraceRecordsTraceEvent),
    ("ReplayRunner empty trace", ReplayRunnerEmptyTrace),
    ("GameSetup same seed same state", GameSetupSameSeedSameState),
    ("GameSetup different seed changes shuffle", GameSetupDifferentSeedChangesShuffle),
    ("GameSetup opening hand count", GameSetupOpeningHandCount),
    ("GameSetup security count", GameSetupSecurityCount),
    ("DrawPhase first turn skip", DrawPhaseFirstTurnSkip),
    ("DrawPhase after first turn draws one", DrawPhaseAfterFirstTurnDrawsOne),
    ("DrawPhase empty deck loses", DrawPhaseEmptyDeckLoses),
    ("GameSetup invariant valid", GameSetupInvariantValid),
    ("MinimalBattle direct attack security 0 wins", MinimalBattleDirectAttackSecurityZeroWins),
    ("MinimalBattle lower DP trashes loser", MinimalBattleLowerDpTrashesLoser),
    ("MinimalBattle tie trashes both", MinimalBattleTieTrashesBoth),
    ("MinimalBattle attack security reduces security", MinimalBattleAttackSecurityReducesSecurity),
    ("MinimalBattle normal digivolve draws one", MinimalBattleNormalDigivolveDrawsOne),
    ("MinimalBattle hatch condition", MinimalBattleHatchCondition),
    ("MinimalBattle move from breeding condition", MinimalBattleMoveFromBreedingCondition),
    ("MinimalBattle memory crossing changes turn", MinimalBattleMemoryCrossingChangesTurn),
    ("LegalActionGenerator generated actions execute", LegalActionGeneratorGeneratedActionsExecute),
    ("ActionExecutor invalid action fails", ActionExecutorInvalidActionFails),
    ("MinimalBattle same action sequence same hash", MinimalBattleSameActionSequenceSameHash),
    ("ValidationHarnessV1 scripted scenarios pass", ValidationHarnessV1ScriptedScenariosPass),
    ("ValidationHarnessV1 random legal action smoke", ValidationHarnessV1RandomLegalActionSmoke),
    ("ValidationHarnessV1 random max action abort", ValidationHarnessV1RandomMaxActionAbort),
    ("ValidationHarnessV1 invariant after every action", ValidationHarnessV1InvariantAfterEveryAction),
    ("ValidationHarnessV1 trace replay same final hash", ValidationHarnessV1TraceReplaySameFinalHash),
    ("ValidationHarnessV1 trace save load", ValidationHarnessV1TraceSaveLoad),
    ("ValidationHarnessV1 CLI debug renderer", ValidationHarnessV1CliDebugRenderer),
    ("ValidationHarnessV2 completion gate reports ST1 complete", ValidationHarnessV2CompletionGateReportsSt1Complete),
    ("ValidationHarnessV2 ST1 unsupported report", ValidationHarnessV2St1UnsupportedReport),
    ("ValidationHarnessV2 invariant fuzz captures failures", ValidationHarnessV2InvariantFuzzCapturesFailures),
    ("Tier1 primitives move draw shuffle reveal search trash", Tier1PrimitivesMoveDrawShuffleRevealSearchTrash),
    ("Tier1 primitives security suspend memory dp", Tier1PrimitivesSecuritySuspendMemoryDp),
    ("Duration modifier DP affects effective DP", DurationModifierDpAffectsEffectiveDp),
    ("Duration modifier DP preserves base definition DP", DurationModifierDpDoesNotMutateBaseDefinitionDp),
    ("Duration cleanup UntilTurnEnd removes modifier", DurationCleanupUntilTurnEndRemovesModifier),
    ("Duration cleanup keeps modifier before expiry", DurationCleanupKeepsModifierBeforeExpiry),
    ("Duration cleanup removes stale field target", DurationCleanupRemovesStaleFieldTarget),
    ("Duration invariant detects stale modifier target", DurationInvariantDetectsStaleModifierTarget),
    ("Duration modifiers stack deterministically", DurationModifiersStackDeterministically),
    ("Duration modifier state hash changes", DurationModifierStateHashChanges),
    ("Duration replay determinism includes modifiers", DurationReplayDeterminismIncludesModifiers),
    ("Duration cleanup UntilBattleEnd removes modifier", DurationCleanupUntilBattleEndRemovesModifier),
    ("Duration security Digimon DP affects security battle", DurationSecurityDigimonDpAffectsSecurityBattle),
    ("Continuous DP modifier affects effective DP", ContinuousDpModifierAffectsEffectiveDp),
    ("Continuous DP modifier preserves base definition DP", ContinuousDpModifierDoesNotMutateBaseDefinitionDp),
    ("Continuous inherited source applies only from source zone", ContinuousInheritedSourceAppliesOnlyFromSourceZone),
    ("Continuous inherited source stops after zone move", ContinuousInheritedSourceStopsAfterZoneMove),
    ("Continuous owner turn condition", ContinuousOwnerTurnCondition),
    ("Continuous source count condition", ContinuousSourceCountCondition),
    ("Continuous tamer aura applies owner battle area", ContinuousTamerAuraAppliesOwnerBattleArea),
    ("Continuous dynamic SecurityAttack from source count", ContinuousDynamicSecurityAttackFromSourceCount),
    ("Continuous and duration modifiers stack deterministically", ContinuousAndDurationModifiersStackDeterministically),
    ("Continuous effects are derived for state hash", ContinuousEffectsAreDerivedForStateHash),
    ("ST1 continuous layer validation report", St1ContinuousLayerValidationReport),
    ("Tier1 primitives play digivolve battle token", Tier1PrimitivesPlayDigivolveBattleToken),
    ("Tier1 RuleProcessor stabilizes invalid permanents", Tier1RuleProcessorStabilizesInvalidPermanents),
    ("Tier1 RuleProcessor DP zero permanent", Tier1RuleProcessorDpZeroPermanent),
    ("Tier1 RuleProcessor trims excess link cards", Tier1RuleProcessorTrimsExcessLinkCards),
    ("Tier1 RuleProcessor uses injected ZoneMover for link trim", Tier1RuleProcessorUsesInjectedZoneMoverForLinkTrim),
    ("Tier1 RuleProcessor max iteration guard", Tier1RuleProcessorMaxIterationGuard),
    ("Tier1 RuleProcessor has no direct ZoneMover construction", Tier1RuleProcessorHasNoDirectZoneMoverConstruction),
    ("Tier1 EffectQueue trigger collection by timing", Tier1EffectQueueTriggerCollectionByTiming),
    ("TriggerPipeline collects field top card descriptor", TriggerPipelineCollectsFieldTopCardDescriptor),
    ("TriggerPipeline collects inherited source descriptor", TriggerPipelineCollectsInheritedSourceDescriptor),
    ("TriggerPipeline enqueues and drains non-background effect", TriggerPipelineEnqueuesAndDrainsNonBackgroundEffect),
    ("TriggerPipeline separates background effect", TriggerPipelineSeparatesBackgroundEffect),
    ("TriggerPipeline selection boundary returns pending request", TriggerPipelineSelectionBoundaryReturnsPendingRequest),
    ("TriggerPipeline optional provider pauses explicit selection", TriggerPipelineOptionalProviderPausesExplicitSelection),
    ("TriggerPipeline unsupported body fails explicitly", TriggerPipelineUnsupportedBodyFailsExplicitly),
    ("TriggerPipeline once-per-turn prevents duplicate execution", TriggerPipelineOncePerTurnPreventsDuplicateExecution),
    ("TriggerPipeline OnStartMainPhase hook invokes descriptor", TriggerPipelineOnStartMainPhaseHookInvokesDescriptor),
    ("TriggerPipeline WhenDigivolving hook invokes descriptor", TriggerPipelineWhenDigivolvingHookInvokesDescriptor),
    ("TriggerPipeline OnAllyAttack hook invokes descriptor", TriggerPipelineOnAllyAttackHookInvokesDescriptor),
    ("TriggerPipeline OnEndAttack deterministic", TriggerPipelineOnEndAttackDeterministic),
    ("TriggerPipeline OptionSkill remains compatible with ST1-15", TriggerPipelineOptionSkillCompatibleWithSt1Option),
    ("TriggerPipeline SecuritySkill remains compatible with service", TriggerPipelineSecuritySkillCompatibleWithSecurityService),
    ("SecurityEffectExecution optional provider pauses explicit selection", SecurityEffectExecutionOptionalProviderPausesExplicitSelection),
    ("Option lifecycle hand play moves through Executing to Trash", OptionLifecycleHandPlayMovesThroughExecutingToTrash),
    ("Option lifecycle source context is Executing", OptionLifecycleSourceContextIsExecuting),
    ("Option lifecycle pending selection keeps Executing", OptionLifecyclePendingSelectionKeepsExecuting),
    ("Option lifecycle selection completion trashes option", OptionLifecycleSelectionCompletionTrashesOption),
    ("Option lifecycle moved source skips follow-up Trash", OptionLifecycleMovedSourceSkipsFollowupTrash),
    ("Option lifecycle invalid action leaves zones clean", OptionLifecycleInvalidActionLeavesZonesClean),
    ("Option lifecycle action trace replay deterministic", OptionLifecycleActionTraceReplayDeterministic),
    ("Runtime composition production graph validates required services", RuntimeCompositionProductionGraphValidatesRequiredServices),
    ("Runtime composition missing TriggerPipeline fails validation", RuntimeCompositionMissingTriggerPipelineFailsValidation),
    ("Runtime composition PlayCardService requires TriggerPipeline", RuntimeCompositionPlayCardServiceRequiresTriggerPipeline),
    ("Runtime composition ActionExecutor returns pending option selection", RuntimeCompositionActionExecutorReturnsPendingOptionSelection),
    ("Runtime composition primitive SecurityCheck uses security effects", RuntimeCompositionPrimitiveSecurityCheckUsesSecurityEffects),
    ("Runtime composition primitive SecurityCheck uses continuous SecurityAttack", RuntimeCompositionPrimitiveSecurityCheckUsesContinuousSecurityAttack),
    ("Runtime composition primitive SecurityCheck uses continuous SecurityDigimonDP", RuntimeCompositionPrimitiveSecurityCheckUsesContinuousSecurityDigimonDp),
    ("Runtime composition default constructors fail", RuntimeCompositionDefaultConstructorsFail),
    ("Runtime composition effect-bearing cards do not become legacy NoEffect", RuntimeCompositionEffectBearingCardDoesNotBecomeLegacyNoEffect),
    ("Runtime composition mixed custom/default graph fails", RuntimeCompositionMixedCustomDefaultGraphFails),
    ("Runtime composition scripted runner returns pending selection", RuntimeCompositionScriptedRunnerReturnsPendingSelection),
    ("Runtime composition random runner returns pending selection", RuntimeCompositionRandomRunnerReturnsPendingSelection),
    ("Runtime rule state clone restore hash", RuntimeRuleStateCloneRestoreHash),
    ("Direct API rollback restores once-per-turn availability", DirectApiRollbackRestoresOncePerTurnAvailability),
    ("EngineSession once-per-turn state is session local", EngineSessionOncePerTurnStateIsSessionLocal),
    ("Repeated games do not accumulate once-per-turn state", RepeatedGamesDoNotAccumulateOncePerTurnState),
    ("EngineSession public resume requires DecisionToken", EngineSessionPublicResumeRequiresDecisionToken),
    ("Replay uses recorded DecisionToken", ReplayUsesRecordedDecisionToken),
    ("Replay rejects invalid decision payloads", ReplayRejectsInvalidDecisionPayloads),
    ("EngineSession option selection pause/resume", EngineSessionOptionSelectionPauseResume),
    ("EngineSession OnPlay selection pause/resume", EngineSessionOnPlaySelectionPauseResume),
    ("EngineSession WhenDigivolving selection pause/resume", EngineSessionWhenDigivolvingSelectionPauseResume),
    ("EngineSession OnStartMainPhase selection pause/resume", EngineSessionOnStartMainPhaseSelectionPauseResume),
    ("EngineSession Pass OnEndTurn selection pause/resume", EngineSessionPassOnEndTurnSelectionPauseResume),
    ("EngineSession Pass OnStartTurn selection pause/resume", EngineSessionPassOnStartTurnSelectionPauseResume),
    ("EngineSession OnAllyAttack selection pause/resume", EngineSessionOnAllyAttackSelectionPauseResume),
    ("EngineSession OnEndAttack selection pause/resume", EngineSessionOnEndAttackSelectionPauseResume),
    ("EngineSession RulesTiming selection pause/resume", EngineSessionRulesTimingSelectionPauseResume),
    ("EngineSession SecuritySkill selection pause/resume", EngineSessionSecuritySkillSelectionPauseResume),
    ("EngineSession optional yes then target selection resolves", EngineSessionOptionalYesThenTargetSelectionResolves),
    ("EngineSession optional no skips and drains queue tail", EngineSessionOptionalNoSkipsAndDrainsQueueTail),
    ("EngineSession security optional then target selection", EngineSessionSecurityOptionalThenTargetSelection),
    ("EngineSession chained selection pause/resume", EngineSessionChainedSelectionPauseResume),
    ("EngineSession stale DecisionToken fails", EngineSessionStaleDecisionTokenFails),
    ("EngineSession selection then RulesTiming pauses again", EngineSessionSelectionThenRulesTimingPausesAgain),
    ("EngineSession RunToMainPhase OnStartTurn pause/resume", EngineSessionRunToMainPhaseOnStartTurnPauseResume),
    ("ScriptedScenarioRunner returns phase decision", ScriptedScenarioRunnerReturnsPhaseDecision),
    ("RandomLegalActionRunner returns phase decision", RandomLegalActionRunnerReturnsPhaseDecision),
    ("EngineSession validates pending resume boundary", EngineSessionValidatesPendingResumeBoundary),
    ("Direct synchronous pending calls roll back state", DirectSynchronousPendingCallsRollbackState),
    ("EngineSession action and selection replay deterministic", EngineSessionActionAndSelectionReplayDeterministic),
    ("EngineSession chained decision replay deterministic", EngineSessionChainedDecisionReplayDeterministic),
    ("EngineSession phase selection replay deterministic", EngineSessionPhaseSelectionReplayDeterministic),
    ("EngineSession pass selection replay deterministic", EngineSessionPassSelectionReplayDeterministic),
    ("EngineSession attack selection replay deterministic", EngineSessionAttackSelectionReplayDeterministic),
    ("EngineSession rules timing selection replay deterministic", EngineSessionRulesTimingSelectionReplayDeterministic),
    ("EngineSession security selection replay deterministic", EngineSessionSecuritySelectionReplayDeterministic),
    ("Option lifecycle ST1 hand option regression", OptionLifecycleSt1HandOptionRegression),
    ("Option lifecycle ST2/ST3 hand option regression", OptionLifecycleSt2St3HandOptionRegression),
    ("TriggerPipeline ST1-09 OnBlockAnyone hook", TriggerPipelineSt1OnBlockAnyoneHook),
    ("TriggerPipeline ST1 completion report has no partials", TriggerPipelineSt1CompletionReportHasNoPartials),
    ("Tier1 Optional effect creates selection request", Tier1OptionalEffectCreatesSelectionRequest),
    ("Tier1 OncePerTurnTracker prevents duplicate use", Tier1OncePerTurnTrackerPreventsDuplicateUse),
    ("Tier1 unsupported primitive fails explicitly", Tier1UnsupportedPrimitiveFailsExplicitly),
    ("CardEffectFoundation registry lookup", CardEffectFoundationRegistryLookup),
    ("CardEffectFoundation unsupported script fails", CardEffectFoundationUnsupportedScriptFails),
    ("CardEffectFoundation deck validation catches unsupported", CardEffectFoundationDeckValidationCatchesUnsupported),
    ("CardEffectFoundation no effect must be explicit", CardEffectFoundationNoEffectMustBeExplicit),
    ("CardEffectFoundation primitive service boundary", CardEffectFoundationPrimitiveServiceBoundary),
    ("ST1 CardEffect catalog covers target decklist", St1CardEffectCatalogCoversTargetDecklist),
    ("ST1 CardEffect deck validation passes", St1CardEffectDeckValidationPasses),
    ("PortingStructure ST1 runnable records have files", PortingStructureSt1RunnableRecordsHaveFiles),
    ("PortingStructure ST2 runnable records have files", PortingStructureSt2RunnableRecordsHaveFiles),
    ("PortingStructure ST3 runnable records have files", PortingStructureSt3RunnableRecordsHaveFiles),
    ("PortingStructure ST1 source mapping documented", PortingStructureSt1SourceMappingDocumented),
    ("PortingStructure ST2 source mapping documented", PortingStructureSt2SourceMappingDocumented),
    ("PortingStructure ST3 source mapping documented", PortingStructureSt3SourceMappingDocumented),
    ("PortingStructure ST1 no-effect provenance documented", PortingStructureSt1NoEffectProvenanceDocumented),
    ("PortingStructure ST2 no-effect provenance documented", PortingStructureSt2NoEffectProvenanceDocumented),
    ("PortingStructure ST3 no-effect provenance documented", PortingStructureSt3NoEffectProvenanceDocumented),
    ("PortingStructure ST1 catalog is registry only", PortingStructureSt1CatalogIsRegistryOnly),
    ("PortingStructure ST2/ST3 catalog is registry only", PortingStructureSt2St3CatalogIsRegistryOnly),
    ("PortingStructure ST1 status table matches registry", PortingStructureSt1StatusTableMatchesRegistry),
    ("PortingStructure ST1-ST3 registry snapshot matches registry", PortingStructureSt1ToSt3StatusSnapshotMatchesRegistry),
    ("PortingStructure ST1 status table matches files", PortingStructureSt1StatusTableMatchesFiles),
    ("PortingStructure ST1-ST3 registry snapshot matches files", PortingStructureSt1ToSt3StatusSnapshotMatchesFiles),
    ("PortingStructure implemented scripts declare concrete classes in card files", PortingStructureImplementedScriptsDeclareConcreteClassesInCardFiles),
    ("PortingStructure NoEffect files are marker only", PortingStructureNoEffectFilesAreMarkerOnly),
    ("PortingStructure NoEffect asset conflicts are documented", PortingStructureNoEffectAssetConflictsAreDocumented),
    ("PortingStructure card files avoid direct zone mutation", PortingStructureCardFilesAvoidDirectZoneMutation),
    ("PortingStructure starter set files use original-like paths", PortingStructureStarterSetFilesUseOriginalLikePaths),
    ("AssetRegistryMapping exact mapping", AssetRegistryMappingExactMapping),
    ("AssetRegistryMapping shared effect preserves source metadata", AssetRegistryMappingSharedEffectPreservesSourceMetadata),
    ("AssetRegistryMapping variant split reports ST3-02", AssetRegistryMappingVariantSplitReportsSt3Two),
    ("AssetRegistryMapping false NoEffect detection", AssetRegistryMappingFalseNoEffectDetection),
    ("AssetRegistryMapping missing per-card file", AssetRegistryMappingMissingPerCardFile),
    ("AssetRegistryMapping stale status snapshot", AssetRegistryMappingStaleStatusSnapshot),
    ("AssetRegistryMapping unregistered effect", AssetRegistryMappingUnregisteredEffect),
    ("AssetRegistryMapping source unavailable report", AssetRegistryMappingSourceUnavailableReport),
    ("AssetRegistryMapping actual local DCGO audit", AssetRegistryMappingActualLocalDcgoAudit),
    ("ST2/ST3 CardEffect catalog skeleton covers target pool", St2St3CardEffectCatalogSkeletonCoversTargetPool),
    ("ST1-ST3 target pool validation passes", St1ToSt3TargetPoolValidationPasses),
    ("ST2/ST3 shared ST1_06 mapping uses card-id scripts", St2St3SharedSt106MappingUsesCardIdScripts),
    ("ST2-01 inherited no-source DP", St2OneInheritedNoSourceDp),
    ("ST2-08 inherited no-source SecurityAttack", St2EightInheritedNoSourceSecurityAttack),
    ("ST2-12 start turn memory and security play", St2MattIshidaStartTurnMemoryAndSecurityPlay),
    ("ST2-03 inherited source trash level condition", St2ThreeInheritedSourceTrashLevelCondition),
    ("ST2-06 inherited source trash", St2SixInheritedSourceTrash),
    ("ST2-09 when digivolving source trash", St2NineWhenDigivolvingSourceTrash),
    ("ST2-11 OnAllyAttack unsuspend once per turn", St2MetalGarurumonOnAllyAttackUnsuspendOncePerTurn),
    ("ST2-13 memory option and security", St2HammerSparkMemoryOptionAndSecurity),
    ("ST2-14 attack block restriction option and security", St2HowlingBlasterAttackBlockRestrictionOptionAndSecurity),
    ("ST2-15 source play option and security", St2WereGarurumonSourcePlayOptionAndSecurity),
    ("ST2-15 stale source selection fails", St2WereGarurumonStaleSourceSelectionFails),
    ("ST2-16 main and security bounce to hand", St2CocytusBreathMainAndSecurityBounceToHand),
    ("ST3-01/04 DP zero deletion trigger", St3DpZeroDeletionTriggersInheritedEffects),
    ("ST3-05 inherited OnAllyAttack memory", St3PatamonInheritedOnAllyAttackMemory),
    ("ST3-08 inherited OnAllyAttack DP reduction", St3EightInheritedOnAllyAttackDpReduction),
    ("ST3-09 WhenDigivolving recovery from deck", St3AngewomonWhenDigivolvingRecoveryFromDeck),
    ("ST3-11 top OnAllyAttack DP reduction", St3ElevenTopOnAllyAttackDpReduction),
    ("ST3-12 continuous Security Digimon DP and security play", St3TakeruTakaishiContinuousSecurityDigimonDpAndSecurityPlay),
    ("ST3-13 main and security DP plus hand", St3HolyFlameMainAndSecurityDpPlusHand),
    ("ST3-14 main DP reduction and security hand", St3HeavensCharmMainDpReductionAndSecurityHand),
    ("ST3-15 main and security SecurityAttack reduction", St3HolyWaveMainAndSecurityAttackReduction),
    ("ST3-16 main and security DP reduction", St3SevenHeavensMainAndSecurityDpReduction),
    ("ST1 CardEffect implemented memory scripts", St1CardEffectImplementedMemoryScripts),
    ("ST1-15 main option deletion selection", St1GigaDestroyerMainOptionDeletesSelectedTargets),
    ("ST1-15 invalid selection fails", St1GigaDestroyerInvalidSelectionFails),
    ("ST1-15 out of candidate selection fails", St1GigaDestroyerOutOfCandidateSelectionFails),
    ("ST1-15 stale target selection fails", St1GigaDestroyerStaleTargetSelectionFails),
    ("ST1-16 main option deletion selection", St1GaiaForceMainOptionDeletesSelectedTarget),
    ("ST1-15 security activates main option deletion", St1GigaDestroyerSecurityActivatesMainOptionDeletion),
    ("ST1-16 security activates main option deletion", St1GaiaForceSecurityActivatesMainOptionDeletion),
    ("Security option execution rejects invalid target", SecurityOptionExecutionRejectsInvalidTarget),
    ("Security option execution rejects stale target", SecurityOptionExecutionRejectsStaleTarget),
    ("ST1-08 WhenDigivolving creates selection", St1GarudamonWhenDigivolvingCreatesSelection),
    ("ST1-08 valid selection applies duration DP", St1GarudamonSelectionAppliesDurationDp),
    ("ST1-08 invalid target selection fails", St1GarudamonInvalidTargetSelectionFails),
    ("ST1-08 stale target selection fails", St1GarudamonStaleTargetSelectionFails),
    ("ST1-13 main option applies duration DP", St1ShadowWingMainOptionAppliesDurationDp),
    ("ST1-13 security applies player SecurityAttack", St1ShadowWingSecurityAppliesPlayerSecurityAttack),
    ("ST1-14 main option applies security Digimon DP", St1StarlightExplosionMainOptionAppliesSecurityDigimonDp),
    ("ST1-14 security applies security Digimon DP", St1StarlightExplosionSecurityAppliesSecurityDigimonDp),
    ("ST1-12 security plays self tamer", St1TaiKamiyaSecurityPlaysSelfTamer),
    ("ST1-12 security trigger pipeline plays self tamer", St1TaiKamiyaSecurityTriggerPipelinePlaysSelfTamer),
    ("ST1-12 security play full field does not play", St1TaiKamiyaSecurityPlayFullFieldDoesNotPlay),
    ("ST1 card body wiring validation report", St1CardBodyWiringValidationReport),
    ("ST1 CardEffect keyword capability smoke", St1CardEffectKeywordCapabilitySmoke),
    ("ST1 CardEffect replay determinism", St1CardEffectReplayDeterminism),
    ("BattleKeywords Blocker selection request", BattleKeywordsBlockerSelectionRequest),
    ("BattleKeywords Security Attack extra checks", BattleKeywordsSecurityAttackExtraChecks),
    ("BattleKeywords Piercing security check", BattleKeywordsPiercingSecurityCheck),
    ("BattleKeywords Jamming security battle", BattleKeywordsJammingSecurityBattle),
    ("BattleKeywords Retaliation DP battle", BattleKeywordsRetaliationDpBattle),
    ("BattleKeywords Reboot active phase", BattleKeywordsRebootActivePhase),
    ("BattleKeywords Rush attack legality", BattleKeywordsRushAttackLegality),
    ("BattleKeywords Collision forced block request", BattleKeywordsCollisionForcedBlockRequest),
    ("BattleKeywords Decoy unsupported explicit", BattleKeywordsDecoyUnsupportedExplicit),
    ("ComplexMechanics Jogress legal action", ComplexMechanicsJogressLegalAction),
    ("ComplexMechanics Jogress execution top sources", ComplexMechanicsJogressExecutionTopSources),
    ("ComplexMechanics Burst legal action", ComplexMechanicsBurstLegalAction),
    ("ComplexMechanics Burst tamer selection request", ComplexMechanicsBurstTamerSelectionRequest),
    ("ComplexMechanics App Fusion link card selection", ComplexMechanicsAppFusionLinkCardSelection),
    ("ComplexMechanics DigiXros materials and cost", ComplexMechanicsDigiXrosMaterialsAndCost),
    ("ComplexMechanics Assembly material selection", ComplexMechanicsAssemblyMaterialSelection),
    ("ComplexMechanics Link card state consistency", ComplexMechanicsLinkCardStateConsistency),
    ("ComplexMechanics unsupported mechanic explicit", ComplexMechanicsUnsupportedMechanicExplicit),
    ("ComplexMechanics replay determinism", ComplexMechanicsReplayDeterminism),
};

var selectedTests = args.Length == 0
    ? tests
    : tests
        .Where(test => args.Any(filter => test.Name.Contains(filter, StringComparison.OrdinalIgnoreCase)))
        .ToArray();

if (selectedTests.Length == 0)
{
    Console.WriteLine($"No tests matched filter: {string.Join(", ", args)}");
    return 1;
}

var failures = new List<string>();

foreach (var (name, test) in selectedTests)
{
    try
    {
        test();
        Console.WriteLine($"PASS {name}");
    }
    catch (Exception exception)
    {
        failures.Add($"{name}: {exception.Message}");
        Console.WriteLine($"FAIL {name}: {exception}");
    }
}

if (failures.Count > 0)
{
    Console.WriteLine();
    Console.WriteLine("FAILED TESTS");
    foreach (var failure in failures)
    {
        Console.WriteLine(failure);
    }

    return 1;
}

Console.WriteLine();
Console.WriteLine($"All {selectedTests.Length} tests passed.");
return 0;

static void GameStateCreation()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 42 });

    AssertEqual(2, state.Players.Count);
    AssertEqual(0, state.Memory);
    AssertEqual(0, state.TurnCount);
    AssertEqual(Phase.None, state.Phase);
    AssertEqual(PlayerId.Player0, state.TurnPlayerId);
    AssertEqual(PlayerId.Player1, state.NonTurnPlayerId);
    AssertEqual(GameResultKind.Ongoing, state.Result.Kind);
    AssertFalse(state.IsGameOver);
    AssertEqual<PlayerId?>(null, state.WinnerPlayerId);
    AssertEmpty(state.ActiveCardIds);
}

static void PlayerStateZoneInitialization()
{
    var player = new PlayerState(PlayerId.Player0);

    AssertEmpty(player.Deck);
    AssertEmpty(player.DigiEggDeck);
    AssertEmpty(player.Hand);
    AssertEmpty(player.Trash);
    AssertEmpty(player.Lost);
    AssertEmpty(player.Security);
    AssertEmpty(player.Executing);
    AssertEmpty(player.Revealed);
    AssertEmpty(player.OutsideGame);
    AssertEmpty(player.FieldPermanents);
}

static void PlayerStateZoneListsAreIndependent()
{
    var player = new PlayerState(PlayerId.Player0);
    var card = new CardInstanceId(1);

    player.Deck.Add(card);

    AssertEqual(1, player.Deck.Count);
    AssertEmpty(player.Hand);
    AssertEmpty(player.Trash);
}

static void PermanentStateSeparatesTopAndSources()
{
    var top = new CardInstanceId(10);
    var source = new CardInstanceId(11);
    var linked = new CardInstanceId(12);
    var permanent = new PermanentState(new PermanentId(1), PlayerId.Player0, top)
    {
        ControllerPlayerId = PlayerId.Player1,
        FrameIndex = 3,
        EnterFieldTurnCount = 2,
    };

    permanent.SourceCardIds.Add(source);
    permanent.LinkedCards.Add(linked);

    AssertEqual(top, permanent.TopCardId);
    AssertEqual(1, permanent.SourceCardIds.Count);
    AssertEqual(source, permanent.SourceCardIds[0]);
    AssertEqual(linked, permanent.LinkedCardIds[0]);
    AssertEqual(PlayerId.Player1, permanent.ControllerPlayerId);
    AssertSequence(new[] { top, source }, permanent.StackCardIds.ToArray());

    var definitions = new Dictionary<string, CardDefinition>(StringComparer.Ordinal)
    {
        ["BT1-001"] = new CardDefinition { CardId = "BT1-001", CardKinds = new[] { CardKind.Digimon } },
    };
    var cards = new Dictionary<CardInstanceId, CardInstance>
    {
        [top] = new CardInstance(top, "BT1-001", PlayerId.Player0),
    };

    AssertTrue(permanent.IsDigimon(definitions, cards));
    AssertFalse(permanent.IsTamer(definitions, cards));
}

static void TurnPlayerHelpers()
{
    var state = GameState.CreateDefault();

    AssertEqual(PlayerId.Player0, state.TurnPlayerId);
    AssertEqual(PlayerId.Player1, state.NonTurnPlayerId);

    state.TurnPlayerId = PlayerId.Player1;

    AssertEqual(PlayerId.Player1, state.TurnPlayer);
    AssertEqual(PlayerId.Player0, state.NonTurnPlayer);
}

static void CardDefinitionCreation()
{
    var definition = new CardDefinition
    {
        CardId = "BT1-001",
        CardIndex = 101,
        CardNameEnglish = "Agumon",
        CardNameJapanese = "アグモン",
        CardColors = new[] { CardColor.Red },
        PlayCost = 3,
        EvoCosts = new[] { new EvoCostDefinition(CardColor.Red, 2, 0) },
        Level = 3,
        CardKinds = new[] { CardKind.Digimon },
        DP = 2000,
        Rarity = CardRarity.C,
        CardEffectClassName = "BT1_001",
        MaxCountInDeck = 4,
        OverflowMemory = 1,
        LinkDP = 1000,
        OptionCardColorRequirements = new[] { CardColor.Red, CardColor.Blue },
    };

    AssertEqual("BT1-001", definition.CardId);
    AssertEqual(101, definition.CardIndex);
    AssertEqual("Agumon", definition.CardNameEnglish);
    AssertEqual("アグモン", definition.CardNameJapanese);
    AssertEqual(CardColor.Red, definition.CardColors[0]);
    AssertEqual(new EvoCostDefinition(CardColor.Red, 2, 0), definition.EvoCosts[0]);
    AssertEqual(3, definition.PlayCost);
    AssertEqual(3, definition.Level);
    AssertEqual(CardKind.Digimon, definition.CardKinds[0]);
    AssertEqual(2000, definition.DP);
    AssertEqual(CardRarity.C, definition.Rarity);
    AssertEqual("BT1_001", definition.CardEffectClassName);
    AssertEqual(4, definition.MaxCountInDeck);
    AssertEqual(1, definition.OverflowMemory);
    AssertEqual(1000, definition.LinkDP);
    AssertSequence(new[] { CardColor.Red, CardColor.Blue }, definition.OptionCardColorRequirements);
    AssertTrue(definition.IsAce);
    AssertFalse(definition.IsDualCard);
    AssertTrue(definition.IsPermanent);
}

static void CardInstanceFactoryCreatesDistinctInstances()
{
    var definition = new CardDefinition { CardId = "BT1-002", CardNameEnglish = "Gabumon" };
    var factory = new CardInstanceFactory(firstInstanceId: 50);

    var first = factory.Create(definition, PlayerId.Player0, Zone.Deck);
    var second = factory.Create(definition, PlayerId.Player0, Zone.Security, isFaceUp: false);

    AssertNotEqual(first.Id, second.Id);
    AssertEqual(new CardInstanceId(50), first.Id);
    AssertEqual(new CardInstanceId(51), second.Id);
    AssertEqual("BT1-002", first.DefinitionId);
    AssertEqual("BT1-002", second.DefinitionId);
    AssertEqual(Zone.Deck, first.Zone);
    AssertEqual(Zone.Security, second.Zone);
    AssertFalse(second.IsFaceUp);
}

static void CardDatabaseLookupByCardId()
{
    var definition = new CardDefinition { CardId = "BT1-003", CardNameEnglish = "Test Card" };
    ICardDatabase database = new InMemoryCardDatabase(new[] { definition });
    var factory = new CardInstanceFactory();

    AssertEqual(1, database.All.Count);
    AssertTrue(database.TryGetByCardId("BT1-003", out var found));
    AssertEqual(definition, found);
    AssertEqual(definition, database.GetByCardId("BT1-003"));

    var instance = factory.Create("BT1-003", PlayerId.Player1, database);

    AssertEqual("BT1-003", instance.DefinitionId);
    AssertEqual(PlayerId.Player1, instance.Owner);
    AssertThrows<DomainException>(() => database.GetByCardId("BT1-999"));
}

static void CardDatabaseDuplicateCardIdFails()
{
    var baseSalamon = new CardDefinition
    {
        CardId = "ST3-02",
        CardIndex = 76,
        CardNameEnglish = "Salamon",
        CardEffectClassName = string.Empty,
    };
    var promoSalamon = baseSalamon with
    {
        CardIndex = 4977,
        CardEffectClassName = "ST3_02",
    };

    AssertThrows<DomainException>(() => new InMemoryCardDatabase(new[] { baseSalamon, promoSalamon }));
}

static void CardDefinitionDefensivelyCopiesCollections()
{
    var colors = new List<CardColor> { CardColor.Red };
    var kinds = new List<CardKind> { CardKind.Digimon };
    var evoCosts = new List<EvoCostDefinition> { new(CardColor.Red, 2, 0) };
    var optionRequirements = new List<CardColor> { CardColor.Yellow };

    var definition = new CardDefinition
    {
        CardId = "BT1-004",
        CardColors = colors,
        CardKinds = kinds,
        EvoCosts = evoCosts,
        OptionCardColorRequirements = optionRequirements,
    };

    colors[0] = CardColor.Blue;
    colors.Add(CardColor.Green);
    kinds[0] = CardKind.Tamer;
    evoCosts[0] = new EvoCostDefinition(CardColor.Blue, 3, 2);
    optionRequirements[0] = CardColor.Purple;

    AssertSequence(new[] { CardColor.Red }, definition.CardColors);
    AssertSequence(new[] { CardKind.Digimon }, definition.CardKinds);
    AssertSequence(new[] { new EvoCostDefinition(CardColor.Red, 2, 0) }, definition.EvoCosts);
    AssertSequence(new[] { CardColor.Yellow }, definition.OptionCardColorRequirements);
}

static void ZoneMoverDeckToHand()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(301), PlayerId.Player0, Zone.Deck);
    IZoneMover mover = new ZoneMover();

    var result = mover.MoveCard(state, new MoveCardCommand(card, Zone.Deck, Zone.Hand, MoveReason.Draw));

    AssertEmpty(state.GetPlayer(PlayerId.Player0).Deck);
    AssertSequence(new[] { card }, state.GetPlayer(PlayerId.Player0).Hand);
    AssertEqual(Zone.Hand, state.Cards[card].CurrentZone);
    AssertEqual(Zone.Deck, result.SourceZone);
    AssertEqual(Zone.Hand, result.DestinationZone);
}

static void ZoneMoverDeckToSecurity()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(302), PlayerId.Player0, Zone.Deck);
    IZoneMover mover = new ZoneMover();

    mover.MoveCard(state, new MoveCardCommand(card, Zone.Deck, Zone.Security, MoveReason.SecuritySetup));

    AssertEmpty(state.GetPlayer(PlayerId.Player0).Deck);
    AssertSequence(new[] { card }, state.GetPlayer(PlayerId.Player0).Security);
    AssertEqual(Zone.Security, state.Cards[card].CurrentZone);
    AssertFalse(state.Cards[card].IsFaceUp);
}

static void ZoneMoverHandToTrash()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(303), PlayerId.Player0, Zone.Hand);
    IZoneMover mover = new ZoneMover();

    mover.MoveCard(state, new MoveCardCommand(card, Zone.Hand, Zone.Trash, MoveReason.Trash));

    AssertEmpty(state.GetPlayer(PlayerId.Player0).Hand);
    AssertSequence(new[] { card }, state.GetPlayer(PlayerId.Player0).Trash);
    AssertEqual(Zone.Trash, state.Cards[card].CurrentZone);
}

static void ZoneMoverFieldTopToTrashPromotesSource()
{
    var state = GameState.CreateDefault();
    var top = new CardInstanceId(304);
    var source = new CardInstanceId(305);
    var permanentId = new PermanentId(30);
    var permanent = new PermanentState(permanentId, PlayerId.Player0, top);

    permanent.SourceCardIds.Add(source);
    state.Cards.Add(top, new CardInstance(top, "BT1-304", PlayerId.Player0) { CurrentZone = Zone.BattleArea, PermanentId = permanentId });
    state.Cards.Add(source, new CardInstance(source, "BT1-305", PlayerId.Player0) { CurrentZone = Zone.EvolutionSources, PermanentId = permanentId });
    state.GetPlayer(PlayerId.Player0).FieldPermanents.Add(permanent);

    IZoneMover mover = new ZoneMover();
    var result = mover.MoveCard(state, new MoveCardCommand(top, Zone.BattleArea, Zone.Trash, MoveReason.Trash, SourcePermanent: permanentId));

    AssertSequence(new[] { top }, state.GetPlayer(PlayerId.Player0).Trash);
    AssertEqual(source, permanent.TopCardId);
    AssertEmpty(permanent.SourceCardIds);
    AssertEqual(Zone.BattleArea, state.Cards[source].CurrentZone);
    AssertEqual(permanentId, state.Cards[source].PermanentId);
    AssertEqual(Zone.Trash, state.Cards[top].CurrentZone);
    AssertEqual<PermanentId?>(null, state.Cards[top].PermanentId);
    AssertEqual(permanentId, result.Trace.SourcePermanent);
}

static void ZoneMoverEvolutionSourceToTrash()
{
    var state = GameState.CreateDefault();
    var top = new CardInstanceId(306);
    var source = new CardInstanceId(307);
    var permanentId = new PermanentId(31);
    var permanent = new PermanentState(permanentId, PlayerId.Player0, top);

    permanent.SourceCardIds.Add(source);
    state.Cards.Add(top, new CardInstance(top, "BT1-306", PlayerId.Player0) { CurrentZone = Zone.BattleArea, PermanentId = permanentId });
    state.Cards.Add(source, new CardInstance(source, "BT1-307", PlayerId.Player0) { CurrentZone = Zone.EvolutionSources, PermanentId = permanentId });
    state.GetPlayer(PlayerId.Player0).FieldPermanents.Add(permanent);

    IZoneMover mover = new ZoneMover();
    mover.MoveCard(state, new MoveCardCommand(source, Zone.EvolutionSources, Zone.Trash, MoveReason.Trash, SourcePermanent: permanentId));

    AssertEqual(top, permanent.TopCardId);
    AssertEmpty(permanent.SourceCardIds);
    AssertSequence(new[] { source }, state.GetPlayer(PlayerId.Player0).Trash);
    AssertEqual(Zone.Trash, state.Cards[source].CurrentZone);
    AssertEqual<PermanentId?>(null, state.Cards[source].PermanentId);
}

static void ZoneMoverInvalidSourceZoneThrows()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(308), PlayerId.Player0, Zone.Deck);
    IZoneMover mover = new ZoneMover();

    AssertThrows<DomainException>(() =>
        mover.MoveCard(state, new MoveCardCommand(card, Zone.Hand, Zone.Trash, MoveReason.Trash)));
}

static void ZoneMoverDuplicatePublicZoneThrows()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(309), PlayerId.Player0, Zone.Deck);
    state.GetPlayer(PlayerId.Player0).Hand.Add(card);
    IZoneMover mover = new ZoneMover();

    AssertThrows<DomainException>(() =>
        mover.MoveCard(state, new MoveCardCommand(card, Zone.Deck, Zone.Trash, MoveReason.Trash)));
}

static void ZoneMoverCreatesTrace()
{
    var state = GameState.CreateDefault();
    var card = AddCardToPlayerZone(state, new CardInstanceId(310), PlayerId.Player0, Zone.Deck);
    IZoneMover mover = new ZoneMover();

    var result = mover.MoveCard(state, new MoveCardCommand(card, Zone.Deck, Zone.Hand, MoveReason.Draw));

    AssertEqual(card, result.Trace.Card);
    AssertEqual(PlayerId.Player0, result.Trace.Owner);
    AssertEqual(Zone.Deck, result.Trace.SourceZone);
    AssertEqual(Zone.Hand, result.Trace.DestinationZone);
    AssertEqual(MoveReason.Draw, result.Trace.Reason);
    AssertFalse(string.IsNullOrWhiteSpace(result.Trace.StateHashBefore));
    AssertFalse(string.IsNullOrWhiteSpace(result.Trace.StateHashAfter));
    AssertNotEqual(result.Trace.StateHashBefore, result.Trace.StateHashAfter);
}

static void DeterministicRngSameSeed()
{
    IDeterministicRng rng1 = new XorShiftDeterministicRng(1234);
    IDeterministicRng rng2 = new XorShiftDeterministicRng(1234);

    for (var i = 0; i < 32; i++)
    {
        AssertEqual(rng1.NextInt(1000), rng2.NextInt(1000));
    }
}

static void GameStateCloneDoesNotShareMutableLists()
{
    var state = GameState.CreateDefault();
    var cardId = new CardInstanceId(7);
    var permanentId = new PermanentId(3);

    state.Cards.Add(cardId, new CardInstance(cardId, "BT1-001", PlayerId.Player0) { Zone = Zone.Hand });
    state.GetPlayer(PlayerId.Player0).Hand.Add(cardId);
    state.GetPlayer(PlayerId.Player0).FieldPermanents.Add(new PermanentState(permanentId, PlayerId.Player0, cardId));

    var clone = state.Clone();
    clone.GetPlayer(PlayerId.Player0).Hand.Add(new CardInstanceId(8));
    clone.GetPlayer(PlayerId.Player0).FieldPermanents[0].DigivolutionCards.Add(new CardInstanceId(9));
    clone.Cards[cardId].Zone = Zone.Trash;

    AssertEqual(1, state.GetPlayer(PlayerId.Player0).Hand.Count);
    AssertEmpty(state.GetPlayer(PlayerId.Player0).FieldPermanents[0].DigivolutionCards);
    AssertEqual(Zone.Hand, state.Cards[cardId].Zone);
}

static void OriginalMutationDoesNotAffectClone()
{
    var state = GameState.CreateDefault();
    var cardId = new CardInstanceId(17);
    var sourceId = new CardInstanceId(18);
    var permanentId = new PermanentId(5);

    state.ActiveCardIds.Add(cardId);
    state.Cards.Add(cardId, new CardInstance(cardId, "BT1-017", PlayerId.Player0) { Zone = Zone.BattleArea, PermanentId = permanentId });
    state.GetPlayer(PlayerId.Player0).FieldPermanents.Add(new PermanentState(permanentId, PlayerId.Player0, cardId));

    var clone = state.Clone();

    state.ActiveCardIds.Add(sourceId);
    state.GetPlayer(PlayerId.Player0).FieldPermanents[0].SourceCardIds.Add(sourceId);
    state.Cards[cardId].Zone = Zone.Trash;

    AssertEqual(1, clone.ActiveCardIds.Count);
    AssertEmpty(clone.GetPlayer(PlayerId.Player0).FieldPermanents[0].SourceCardIds);
    AssertEqual(Zone.BattleArea, clone.Cards[cardId].Zone);
}

static void StateHashIsStableAndChangesAfterZoneMove()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 99 });
    state.CardDefinitions.Add("BT1-001", new CardDefinition
    {
        CardId = "BT1-001",
        CardNumber = "BT1-001",
        Name = "Test Digimon",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 3,
        PlayCost = 3,
        Dp = 1000,
    });

    var cardId = new CardInstanceId(1);
    state.Cards.Add(cardId, new CardInstance(cardId, "BT1-001", PlayerId.Player0) { Zone = Zone.Hand });
    state.ActiveCardIds.Add(cardId);
    state.GetPlayer(PlayerId.Player0).Hand.Add(cardId);

    var clone = state.Clone();

    AssertEqual(state.ComputeStateHash(), clone.ComputeStateHash());

    state.GetPlayer(PlayerId.Player0).Hand.Remove(cardId);
    state.GetPlayer(PlayerId.Player0).Trash.Add(cardId);
    state.Cards[cardId].Zone = Zone.Trash;

    AssertNotEqual(clone.ComputeStateHash(), state.ComputeStateHash());
}

static void SelectionRequestCreation()
{
    var cardTarget = new SelectableTarget(
        SelectionTargetKind.Card,
        StableId: "card:1",
        Owner: PlayerId.Player0,
        Card: new CardInstanceId(1),
        Label: "BT1-001");

    var request = new SelectionRequest(
        id: "select-card-1",
        player: PlayerId.Player0,
        targetKind: SelectionTargetKind.Card,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        candidates: new[] { cardTarget },
        prompt: "Select 1 card.");

    var decision = DecisionPoint.ForSelection(PlayerId.Player0, Phase.Main, "effect-selection", request);
    var result = SelectionResult.ForTargets(request.Id, new[] { cardTarget });

    AssertEqual(DecisionKind.Selection, decision.Kind);
    AssertEqual(request, decision.SelectionRequest);
    AssertEqual(SelectionKind.SelectCard, request.Kind);
    AssertEqual("select-card-1", result.RequestId);
    AssertEqual(1, result.SelectedTargets.Count);
    SelectionValidator.Validate(request, result);
}

static void SelectionValidatorValidatesMinMax()
{
    var first = new SelectableTarget(SelectionTargetKind.Card, "card:1", PlayerId.Player0, Card: new CardInstanceId(1), Label: "BT1-001");
    var second = new SelectableTarget(SelectionTargetKind.Card, "card:2", PlayerId.Player0, Card: new CardInstanceId(2), Label: "BT1-002");
    var request = new SelectionRequest(
        id: "select-two",
        player: PlayerId.Player0,
        selectionKind: SelectionKind.SelectCard,
        targetKind: SelectionTargetKind.Card,
        minCount: 1,
        maxCount: 2,
        canSkip: false,
        canEndNotMax: true,
        candidates: new[] { first, second },
        prompt: "Select up to 2 cards.");

    SelectionValidator.Validate(request, SelectionResult.ForTargets(request.Id, new[] { first }));
    SelectionValidator.Validate(request, SelectionResult.ForTargets(request.Id, new[] { first, second }));
    AssertThrows<DomainException>(() =>
        SelectionValidator.Validate(request, SelectionResult.ForTargets(request.Id, Array.Empty<SelectableTarget>())));
}

static void SelectionValidatorAllowsSkip()
{
    var target = new SelectableTarget(SelectionTargetKind.Permanent, "permanent:1", PlayerId.Player0, Permanent: new PermanentId(1), Label: "Agumon");
    var request = new SelectionRequest(
        id: "optional-block",
        player: PlayerId.Player1,
        selectionKind: SelectionKind.SelectPermanent,
        targetKind: SelectionTargetKind.Permanent,
        minCount: 1,
        maxCount: 1,
        canSkip: true,
        canEndNotMax: false,
        candidates: new[] { target },
        prompt: "Select a blocker.");

    SelectionValidator.Validate(request, SelectionResult.Skip(request.Id));
}

static void SelectionValidatorRejectsInvalidTarget()
{
    var target = new SelectableTarget(SelectionTargetKind.Card, "card:1", PlayerId.Player0, Card: new CardInstanceId(1), Label: "BT1-001");
    var invalid = new SelectableTarget(SelectionTargetKind.Card, "card:999", PlayerId.Player0, Card: new CardInstanceId(999), Label: "BT1-999");
    var request = new SelectionRequest(
        id: "select-card-invalid",
        player: PlayerId.Player0,
        targetKind: SelectionTargetKind.Card,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        candidates: new[] { target },
        prompt: "Select 1 card.");

    AssertThrows<DomainException>(() =>
        SelectionValidator.Validate(request, SelectionResult.ForTargets(request.Id, new[] { invalid })));
}

static void SelectionValidatorValidatesOptionValue()
{
    var yes = new SelectableTarget(SelectionTargetKind.Option, "option:yes", OptionValue: "yes", Label: "Yes");
    var no = new SelectableTarget(SelectionTargetKind.Option, "option:no", OptionValue: "no", Label: "No");
    var request = new SelectionRequest(
        id: "choose-option",
        player: PlayerId.Player0,
        selectionKind: SelectionKind.ChooseAction,
        targetKind: SelectionTargetKind.Option,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        candidates: new[] { yes, no },
        prompt: "Choose yes or no.");

    SelectionValidator.Validate(request, SelectionResult.ForOption(request.Id, "yes"));
    AssertThrows<DomainException>(() =>
        SelectionValidator.Validate(request, SelectionResult.ForOption(request.Id, "maybe")));
}

static void SelectionResultApplicatorValidResultAppliesContinuation()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8011, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 8012, 812, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var script = new SelectionPrimitiveCardScript(
        "TEST-SELECT-DP",
        "Test_SelectDp",
        SelectionPrimitiveMode.ModifyDp,
        PlayerId.Player0);
    var resolution = CreateSelectionPrimitiveResolution(state, script, source);
    var request = resolution.PendingSelectionRequest!;

    var result = new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, new[] { request.Candidates[0] }),
        new Tier1PrimitiveService());

    AssertFalse(result.Skipped);
    AssertEqual(1, result.SelectedTargets.Count);
    AssertEqual(-1000, target.DpModifier);
}

static void SelectionResultApplicatorInvalidTargetFails()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8021, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 8022, 822, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var script = new SelectionPrimitiveCardScript(
        "TEST-SELECT-INVALID",
        "Test_SelectInvalid",
        SelectionPrimitiveMode.ModifyDp,
        PlayerId.Player0);
    var resolution = CreateSelectionPrimitiveResolution(state, script, source);
    var request = resolution.PendingSelectionRequest!;
    var invalid = new SelectableTarget(
        SelectionTargetKind.Permanent,
        "permanent:999",
        PlayerId.Player0,
        Permanent: new PermanentId(999),
        Label: "Invalid",
        Zone: Zone.BattleArea);

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, new[] { invalid }),
            new Tier1PrimitiveService()));
    AssertEqual(0, target.DpModifier);
}

static void SelectionResultApplicatorMinMaxViolationFails()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8031, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var first = AddBattlePermanent(state, 8032, 832, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var second = AddBattlePermanent(state, 8033, 833, "BT1-WEAK", PlayerId.Player0, 1, enterTurn: 1);
    var script = new SelectionPrimitiveCardScript(
        "TEST-SELECT-MAX",
        "Test_SelectMax",
        SelectionPrimitiveMode.ModifyDp,
        PlayerId.Player0,
        maxCount: 1);
    var resolution = CreateSelectionPrimitiveResolution(state, script, source);
    var request = resolution.PendingSelectionRequest!;

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, request.Candidates),
            new Tier1PrimitiveService()));
    AssertEqual(0, first.DpModifier);
    AssertEqual(0, second.DpModifier);
}

static void SelectionResultApplicatorRejectsRequiredSkip()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8041, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 8042, 842, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var script = new SelectionPrimitiveCardScript(
        "TEST-SELECT-NO-SKIP",
        "Test_SelectNoSkip",
        SelectionPrimitiveMode.ModifyDp,
        PlayerId.Player0,
        canSkip: false);
    var resolution = CreateSelectionPrimitiveResolution(state, script, source);
    var request = resolution.PendingSelectionRequest!;

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.Skip(request.Id),
            new Tier1PrimitiveService()));
    AssertEqual(0, target.DpModifier);
}

static void SelectionResultApplicatorEmptyNonSkipAppliesContinuation()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8046, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var request = new SelectionRequest(
        "test-empty-non-skip",
        PlayerId.Player0,
        SelectionKind.SelectPermanent,
        SelectionTargetKind.Permanent,
        minCount: 0,
        maxCount: 0,
        canSkip: false,
        canEndNotMax: true,
        Array.Empty<SelectableTarget>(),
        "Resolve empty target set.");
    var resolution = new EffectResolution(
        "test-empty-non-skip-resolution",
        EffectTiming.OptionSkill,
        source,
        SourcePermanent: null,
        Controller: PlayerId.Player0,
        IsBackground: false,
        IsOptional: false,
        Context: new EffectContext(state, EffectTiming.OptionSkill, PlayerId.Player0, source),
        SelectionRequest: request,
        SelectionContinuation: context => context.Primitives.ModifyMemory(context.State, PlayerId.Player0, 1));

    var result = new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, Array.Empty<SelectableTarget>()),
        new Tier1PrimitiveService());

    AssertFalse(result.Skipped);
    AssertEqual(6, state.Memory);
}

static void SelectionResultApplicatorRejectsMovedCardTarget()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8051, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var selected = AddCardToZone(state, 8052, "BT1-OPTION", PlayerId.Player0, Zone.Revealed);
    var target = new SelectableTarget(
        SelectionTargetKind.Card,
        $"card:{selected.Value}",
        PlayerId.Player0,
        Card: selected,
        Label: "BT1-OPTION",
        Zone: Zone.Revealed);
    var request = new SelectionRequest(
        "test-moved-card",
        PlayerId.Player0,
        SelectionKind.SelectCard,
        SelectionTargetKind.Card,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        new[] { target },
        "Select revealed card.");
    var resolution = new EffectResolution(
        "test-moved-card-resolution",
        EffectTiming.OptionSkill,
        source,
        SourcePermanent: null,
        Controller: PlayerId.Player0,
        IsBackground: false,
        IsOptional: false,
        Context: new EffectContext(state, EffectTiming.OptionSkill, PlayerId.Player0, source),
        SelectionRequest: request,
        SelectionContinuation: context => context.Primitives.ModifyMemory(context.State, PlayerId.Player0, 1));

    new Tier1PrimitiveService().TrashCard(state, selected, Zone.Revealed);

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, new[] { target }),
            new Tier1PrimitiveService()));
    AssertEqual(5, state.Memory);
}

static void SelectionResultApplicatorTestEffectDestroysPermanent()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 8061, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 8062, 862, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var script = new SelectionPrimitiveCardScript(
        "TEST-SELECT-DESTROY",
        "Test_SelectDestroy",
        SelectionPrimitiveMode.Destroy,
        PlayerId.Player1);
    var resolution = CreateSelectionPrimitiveResolution(state, script, source);
    var request = resolution.PendingSelectionRequest!;

    new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, new[] { request.Candidates[0] }),
        new Tier1PrimitiveService());

    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
}

static EffectResolution CreateSelectionPrimitiveResolution(
    GameState state,
    SelectionPrimitiveCardScript script,
    CardInstanceId source,
    PlayerId selectionPlayer = default)
{
    var player = selectionPlayer == default ? PlayerId.Player0 : selectionPlayer;
    var descriptors = script.CreateEffectDescriptors(new CardScriptContext(
        state,
        source,
        SourcePermanent: null,
        Controller: player));
    var collected = new TriggerCollector().Collect(
        new EffectContext(state, EffectTiming.OptionSkill, player, source),
        descriptors);
    var queue = new EffectQueue();
    queue.EnqueueRange(collected.QueuedEffects);

    AssertEqual(1, queue.Count);
    var resolution = queue.Dequeue();
    AssertTrue(resolution.PendingSelectionRequest is not null);
    AssertTrue(resolution.SelectionContinuation is not null);
    return resolution;
}

static void LegalActionConvertsToSelectionRequest()
{
    var target = new SelectableTarget(
        SelectionTargetKind.Permanent,
        "permanent:3",
        PlayerId.Player1,
        Permanent: new PermanentId(3),
        Label: "Target Digimon");
    var action = new AttackAction(PlayerId.Player0, new PermanentId(1), null)
    {
        Metadata = new GameActionMetadata { StableId = "attack:1", DebugLabel = "Attack with Agumon" },
    };
    var legalAction = new LegalAction(
        Id: "attack-1",
        Kind: LegalActionKind.Attack,
        Action: action,
        Label: "Attack",
        Targets: new[] { target });

    var request = legalAction.ToSelectionRequest(
        requestId: "attack-target",
        player: PlayerId.Player0,
        phase: Phase.Main,
        selectionKind: SelectionKind.SelectPermanent,
        targetKind: SelectionTargetKind.Permanent,
        minCount: 1,
        maxCount: 1,
        prompt: "Select attack target.");

    AssertEqual("attack-target", request.Id);
    AssertEqual(SelectionKind.SelectPermanent, request.Kind);
    AssertSequence(new[] { target }, request.Candidates);
    SelectionValidator.Validate(request, SelectionResult.ForTargets(request.Id, new[] { target }));
}

static void SelectionDebugLabelCreation()
{
    var target = new SelectableTarget(
        SelectionTargetKind.FieldSlot,
        "slot:0:2",
        Owner: PlayerId.Player0,
        Label: "Player 0 slot 2",
        FieldSlotIndex: 2);
    var request = new SelectionRequest(
        id: "choose-slot",
        player: PlayerId.Player0,
        selectionKind: SelectionKind.SelectFieldSlot,
        targetKind: SelectionTargetKind.FieldSlot,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        candidates: new[] { target },
        prompt: "Choose a field slot.");
    var action = new PlayCardAction(PlayerId.Player0, new CardInstanceId(1), 2)
    {
        Metadata = new GameActionMetadata { StableId = "play:1:2", DebugLabel = "Play BT1-001 to slot 2" },
    };
    var legalAction = new LegalAction("play-slot-2", LegalActionKind.PlayCard, action, "Play BT1-001", new[] { target });

    AssertEqual("Player 0 slot 2", target.DebugLabel);
    AssertTrue(request.DebugLabel.Contains("choose-slot", StringComparison.Ordinal));
    AssertTrue(request.DebugLabel.Contains("candidates=1", StringComparison.Ordinal));
    AssertEqual("Play BT1-001 to slot 2", action.DebugLabel);
    AssertTrue(legalAction.DebugLabel.Contains("Play BT1-001", StringComparison.Ordinal));
}

static void TestDecisionProviderValidatesSelection()
{
    var target = new SelectableTarget(SelectionTargetKind.Card, "card:1", PlayerId.Player0, Card: new CardInstanceId(1), Label: "BT1-001");
    var request = new SelectionRequest(
        id: "provider-select",
        player: PlayerId.Player0,
        targetKind: SelectionTargetKind.Card,
        minCount: 1,
        maxCount: 1,
        canSkip: false,
        canEndNotMax: false,
        candidates: new[] { target },
        prompt: "Select 1 card.");
    var provider = new TestDecisionProvider();

    provider.EnqueueSelectionResult(SelectionResult.ForTargets(request.Id, new[] { target }));

    var result = provider.ChooseSelection(request);

    AssertEqual(request.Id, result.RequestId);
    AssertSequence(new[] { target }, result.SelectedTargets);
}

static void EngineInvariantCheckerValidStatePasses()
{
    var state = CreateInvariantValidState();
    var report = new EngineInvariantChecker().Check(state);

    AssertTrue(report.IsValid);
    AssertEqual(0, report.Violations.Count);
}

static void EngineInvariantCheckerDuplicateZoneCardFails()
{
    var state = CreateInvariantValidState();
    var card = new CardInstanceId(401);

    state.GetPlayer(PlayerId.Player0).Hand.Add(card);

    var report = new EngineInvariantChecker().Check(state);

    AssertFalse(report.IsValid);
    AssertViolation(report, "duplicate-zone-membership");
}

static void EngineInvariantCheckerCurrentZoneMismatchFails()
{
    var state = CreateInvariantValidState();
    var card = new CardInstanceId(401);

    state.Cards[card].CurrentZone = Zone.Hand;

    var report = new EngineInvariantChecker().Check(state);

    AssertFalse(report.IsValid);
    AssertViolation(report, "current-zone-mismatch");
}

static void StateHasherSameStateSameHash()
{
    var state = CreateInvariantValidState();
    var clone = state.Clone();
    var hasher = new StateHasher();

    AssertEqual(hasher.ComputeHash(state), hasher.ComputeHash(clone));
}

static void GameTraceRecordsTraceEvent()
{
    var state = CreateInvariantValidState();
    var trace = new GameTrace();

    var traceEvent = trace.AddStateSnapshot("initial", state);

    AssertEqual(1, trace.Events.Count);
    AssertEqual(0, traceEvent.Index);
    AssertEqual(TraceEventKind.StateSnapshot, traceEvent.Kind);
    AssertEqual("initial", traceEvent.Label);
    AssertFalse(string.IsNullOrWhiteSpace(traceEvent.StateHashBefore));
    AssertEqual(traceEvent.StateHashBefore, traceEvent.StateHashAfter);
}

static void ReplayRunnerEmptyTrace()
{
    var state = CreateInvariantValidState();
    var trace = new GameTrace();
    var result = new ReplayRunner(actionExecutor: CreateTestActionExecutor()).Replay(state, trace);

    AssertTrue(result.InvariantReport.IsValid);
    AssertEqual(0, result.Trace.Events.Count);
    AssertEqual(state.ComputeStateHash(), result.FinalState.ComputeStateHash());
    AssertNotSame(state, result.FinalState);
}

static void GameSetupSameSeedSameState()
{
    var database = CreateSetupCardDatabase();
    var service = new GameSetupService();

    var first = service.SetupGame(CreateSetupRequest(seed: 9001), database);
    var second = service.SetupGame(CreateSetupRequest(seed: 9001), database);

    AssertEqual(first.State.ComputeStateHash(), second.State.ComputeStateHash());
}

static void GameSetupDifferentSeedChangesShuffle()
{
    var database = CreateSetupCardDatabase();
    var service = new GameSetupService();

    var first = service.SetupGame(CreateSetupRequest(seed: 11), database);
    var second = service.SetupGame(CreateSetupRequest(seed: 12), database);

    AssertNotEqual(
        string.Join(",", first.State.GetPlayer(PlayerId.Player0).Deck.Select(card => card.Value)),
        string.Join(",", second.State.GetPlayer(PlayerId.Player0).Deck.Select(card => card.Value)));
}

static void GameSetupOpeningHandCount()
{
    var result = new GameSetupService().SetupGame(CreateSetupRequest(seed: 101), CreateSetupCardDatabase());

    AssertEqual(5, result.State.GetPlayer(PlayerId.Player0).Hand.Count);
    AssertEqual(5, result.State.GetPlayer(PlayerId.Player1).Hand.Count);
}

static void GameSetupSecurityCount()
{
    var result = new GameSetupService().SetupGame(CreateSetupRequest(seed: 102), CreateSetupCardDatabase());

    AssertEqual(5, result.State.GetPlayer(PlayerId.Player0).Security.Count);
    AssertEqual(5, result.State.GetPlayer(PlayerId.Player1).Security.Count);
    AssertTrue(result.State.GetPlayer(PlayerId.Player0).Security.All(card => !result.State.Cards[card].IsFaceUp));
    AssertTrue(result.State.GetPlayer(PlayerId.Player1).Security.All(card => !result.State.Cards[card].IsFaceUp));
}

static void DrawPhaseFirstTurnSkip()
{
    var result = new GameSetupService().SetupGame(CreateSetupRequest(seed: 103), CreateSetupCardDatabase());
    var state = result.State;
    var player = state.TurnPlayerId;
    var handCount = state.GetPlayer(player).Hand.Count;
    var deckCount = state.GetPlayer(player).Deck.Count;

    state.TurnCount = 1;
    var drawPhase = new DrawService().ExecuteDrawPhase(state);

    AssertTrue(drawPhase.SkippedFirstTurnDraw);
    AssertEqual(Phase.Draw, state.Phase);
    AssertEqual(handCount, state.GetPlayer(player).Hand.Count);
    AssertEqual(deckCount, state.GetPlayer(player).Deck.Count);
}

static void DrawPhaseAfterFirstTurnDrawsOne()
{
    var result = new GameSetupService().SetupGame(CreateSetupRequest(seed: 104), CreateSetupCardDatabase());
    var state = result.State;
    var player = state.TurnPlayerId;
    var handCount = state.GetPlayer(player).Hand.Count;
    var deckCount = state.GetPlayer(player).Deck.Count;

    state.TurnCount = 2;
    var drawPhase = new DrawService().ExecuteDrawPhase(state);

    AssertFalse(drawPhase.SkippedFirstTurnDraw);
    AssertFalse(drawPhase.DeckOutLoss);
    AssertEqual(1, drawPhase.Draw.DrawnCards.Count);
    AssertEqual(handCount + 1, state.GetPlayer(player).Hand.Count);
    AssertEqual(deckCount - 1, state.GetPlayer(player).Deck.Count);
}

static void DrawPhaseEmptyDeckLoses()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 105 });
    state.TurnPlayerId = PlayerId.Player0;
    state.TurnCount = 2;

    var drawPhase = new DrawService().ExecuteDrawPhase(state);

    AssertTrue(drawPhase.DeckOutLoss);
    AssertEqual(GameResultKind.Win, state.Result.Kind);
    AssertEqual<PlayerId?>(PlayerId.Player1, state.Result.Winner);
    AssertEqual(WinConditionChecker.DeckOutReason, state.Result.Reason);
}

static void GameSetupInvariantValid()
{
    var result = new GameSetupService().SetupGame(CreateSetupRequest(seed: 106), CreateSetupCardDatabase());
    var report = new EngineInvariantChecker().Check(result.State);

    AssertTrue(report.IsValid);
}

static void MinimalBattleDirectAttackSecurityZeroWins()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 1001, 1, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);

    CreateTestActionExecutor().Execute(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertEqual(GameResultKind.Win, state.Result.Kind);
    AssertEqual<PlayerId?>(PlayerId.Player0, state.Result.Winner);
    AssertEqual(WinConditionChecker.DirectAttackReason, state.Result.Reason);
}

static void MinimalBattleLowerDpTrashesLoser()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 1011, 11, "BT1-WEAK", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 1012, 12, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);

    CreateTestActionExecutor().Execute(state, new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
    AssertFalse(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == defender.Id));
}

static void MinimalBattleTieTrashesBoth()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 1021, 21, "BT1-TIE", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 1022, 22, "BT1-TIE", PlayerId.Player1, 0, enterTurn: 1);

    CreateTestActionExecutor().Execute(state, new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));
    AssertFalse(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == defender.Id));
}

static void MinimalBattleAttackSecurityReducesSecurity()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 1031, 31, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 1032, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);

    CreateTestActionExecutor().Execute(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertEqual(0, state.GetPlayer(PlayerId.Player1).Security.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertEqual(GameResultKind.Ongoing, state.Result.Kind);
}

static void MinimalBattleNormalDigivolveDrawsOne()
{
    var state = CreateMinimalBattleState();
    var target = AddBattlePermanent(state, 1035, 35, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var champion = AddCardToZone(state, 1036, "BT1-CHAMPION", PlayerId.Player0, Zone.Hand);
    var drawCard = AddCardToZone(state, 1037, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    var legalActions = new LegalActionGenerator().Generate(state, PlayerId.Player0);
    AssertTrue(legalActions.Any(action => action.Kind == LegalActionKind.Digivolve));

    CreateTestActionExecutor().Execute(state, new DigivolveAction(PlayerId.Player0, champion, target.Id));

    AssertEqual(champion, target.TopCardId);
    AssertSequence(new[] { new CardInstanceId(1035) }, target.SourceCardIds);
    AssertEqual(Zone.BattleArea, state.Cards[champion].CurrentZone);
    AssertEqual(Zone.EvolutionSources, state.Cards[new CardInstanceId(1035)].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(drawCard));
    AssertEqual(3, state.Memory);
}

static void MinimalBattleHatchCondition()
{
    var state = CreateMinimalBattleState();
    state.Phase = Phase.Breeding;
    var egg = AddCardToZone(state, 1041, "BT1-EGG", PlayerId.Player0, Zone.DigiEggDeck);

    var legalActions = new LegalActionGenerator().Generate(state, PlayerId.Player0);
    AssertTrue(legalActions.Any(action => action.Kind == LegalActionKind.Hatch));

    CreateTestActionExecutor().Execute(state, new HatchAction(PlayerId.Player0));

    AssertFalse(state.GetPlayer(PlayerId.Player0).DigiEggDeck.Contains(egg));
    AssertEqual(Zone.BreedingArea, state.Cards[egg].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player0).BreedingAreaPermanent is not null);
}

static void MinimalBattleMoveFromBreedingCondition()
{
    var state = CreateMinimalBattleState();
    state.Phase = Phase.Breeding;
    var permanent = AddBattlePermanent(state, 1051, 51, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: -1, isBreeding: true);

    var legalActions = new LegalActionGenerator().Generate(state, PlayerId.Player0);
    AssertTrue(legalActions.Any(action => action.Kind == LegalActionKind.MoveFromBreeding));

    CreateTestActionExecutor().Execute(state, new MoveFromBreedingAction(PlayerId.Player0, permanent.Id, 2));

    AssertFalse(permanent.IsBreedingArea);
    AssertEqual(2, permanent.FrameIndex);
    AssertEqual(Zone.BattleArea, state.Cards[permanent.TopCardId].CurrentZone);
}

static void MinimalBattleMemoryCrossingChangesTurn()
{
    var state = CreateMinimalBattleState();
    state.Memory = 1;
    var card = AddCardToZone(state, 1061, "BT1-COST3", PlayerId.Player0, Zone.Hand);

    CreateTestActionExecutor().Execute(state, new PlayCardAction(PlayerId.Player0, card, 0));

    AssertEqual(PlayerId.Player1, state.TurnPlayerId);
    AssertEqual(2, state.Memory);
    AssertEqual(Phase.Active, state.Phase);
}

static void LegalActionGeneratorGeneratedActionsExecute()
{
    var state = CreateMinimalBattleState();
    AddCardToZone(state, 1071, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    AddBattlePermanent(state, 1072, 72, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 1073, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var generator = new LegalActionGenerator();
    var executor = CreateTestActionExecutor();

    var legalActions = generator.Generate(state, PlayerId.Player0);

    AssertTrue(legalActions.Count >= 1);
    foreach (var legalAction in legalActions)
    {
        var clone = state.Clone();
        executor.Execute(clone, legalAction.Action);
    }
}

static void ActionExecutorInvalidActionFails()
{
    var state = CreateMinimalBattleState();

    AssertThrows<DomainException>(() =>
        CreateTestActionExecutor().Execute(state, new PlayCardAction(PlayerId.Player0, new CardInstanceId(9999), 0)));
}

static void MinimalBattleSameActionSequenceSameHash()
{
    var first = CreateMinimalBattleState();
    var second = CreateMinimalBattleState();
    var firstCard = AddCardToZone(first, 1091, "BT1-FREE", PlayerId.Player0, Zone.Hand);
    var secondCard = AddCardToZone(second, 1091, "BT1-FREE", PlayerId.Player0, Zone.Hand);
    var executor = CreateTestActionExecutor();

    executor.Execute(first, new PlayCardAction(PlayerId.Player0, firstCard, 0));
    executor.Execute(first, new PassAction(PlayerId.Player0));
    executor.Execute(second, new PlayCardAction(PlayerId.Player0, secondCard, 0));
    executor.Execute(second, new PassAction(PlayerId.Player0));

    AssertEqual(first.ComputeStateHash(), second.ComputeStateHash());
}

static void ValidationHarnessV1ScriptedScenariosPass()
{
    var runner = CreateTestScriptedScenarioRunner();
    var scenarios = CreateValidationHarnessV1Scenarios();

    AssertEqual(7, scenarios.Count);
    foreach (var scenario in scenarios)
    {
        var result = runner.Run(scenario);

        AssertTrue(result.Status is ScenarioRunStatus.Completed or ScenarioRunStatus.GameOver);
        AssertTrue(result.InvariantReport.IsValid);
        AssertFalse(string.IsNullOrWhiteSpace(result.FinalStateHash));
        AssertTrue(result.Trace.Events.Count >= 2);
    }
}

static void ValidationHarnessV1RandomLegalActionSmoke()
{
    var state = CreateMinimalBattleState();
    AddBattlePermanent(state, 1101, 101, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 1102, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);
    AddCardToZone(state, 1103, "BT1-FREE", PlayerId.Player0, Zone.Hand);

    var result = CreateTestRandomLegalActionRunner().Run(new RandomLegalActionRunRequest("smoke", state, MaxActions: 8, Seed: 77));

    AssertTrue(result.Status is ScenarioRunStatus.Completed or ScenarioRunStatus.GameOver or ScenarioRunStatus.MaxTurnAbort);
    AssertTrue(result.InvariantReport.IsValid);
    AssertFalse(string.IsNullOrWhiteSpace(result.FinalStateHash));
}

static void ValidationHarnessV1RandomMaxActionAbort()
{
    var state = CreateMinimalBattleState();
    AddCardToZone(state, 1104, "BT1-OPTION", PlayerId.Player1, Zone.Deck);

    var result = CreateTestRandomLegalActionRunner().Run(new RandomLegalActionRunRequest("max-abort", state, MaxActions: 1, Seed: 78));

    AssertEqual(ScenarioRunStatus.MaxTurnAbort, result.Status);
    AssertTrue(result.MaxTurnAbort is not null);
    AssertEqual(1, result.MaxTurnAbort!.MaxActions);
    AssertEqual(1, result.MaxTurnAbort.ActionsExecuted);
}

static void ValidationHarnessV1InvariantAfterEveryAction()
{
    var state = CreateMinimalBattleState();
    AddCardToZone(state, 1111, "BT1-EGG", PlayerId.Player0, Zone.DigiEggDeck);
    var scenario = new ScriptedScenario(
        "hatch-move-invariant",
        state,
        new ScriptedScenarioStep[]
        {
            new ActionScenarioStep(new HatchAction(PlayerId.Player0)),
            new ActionScenarioStep(new MoveFromBreedingAction(PlayerId.Player0, new PermanentId(1), 0)),
        });

    var result = CreateTestScriptedScenarioRunner().Run(scenario);

    AssertEqual(3, result.InvariantReports.Count);
    AssertTrue(result.InvariantReports.All(report => report.IsValid));
}

static void ValidationHarnessV1TraceReplaySameFinalHash()
{
    var scenario = CreateValidationHarnessV1Scenarios().First(candidate => candidate.Name == "normal digivolve draw");
    var result = CreateTestScriptedScenarioRunner().Run(scenario);

    var replay = CreateTestReplayDeterminismHelper().ReplayAndCompare(scenario.InitialState, result.Trace, result.FinalStateHash);

    AssertTrue(replay.Matches);
    AssertEqual(result.FinalStateHash, replay.ActualFinalStateHash);
}

static void ValidationHarnessV1TraceSaveLoad()
{
    var scenario = CreateValidationHarnessV1Scenarios().First(candidate => candidate.Name == "security 0 direct attack win");
    var result = CreateTestScriptedScenarioRunner().Run(scenario);
    var store = new TraceStore();

    var serialized = store.Save(result.Trace);
    var loaded = store.Load(serialized);
    var replay = CreateTestReplayDeterminismHelper().ReplayAndCompare(scenario.InitialState, loaded, result.FinalStateHash);

    AssertEqual(result.Trace.Events.Count, loaded.Events.Count);
    AssertTrue(replay.Matches);
}

static void ValidationHarnessV1CliDebugRenderer()
{
    var scenario = CreateValidationHarnessV1Scenarios().First(candidate => candidate.Name == "lower DP Digimon deleted");
    var result = CreateTestScriptedScenarioRunner().Run(scenario);
    var renderer = new CliDebugRenderer();

    var rendered = renderer.RenderScenarioResult(result);

    AssertTrue(rendered.Contains("Scenario: lower DP Digimon deleted", StringComparison.Ordinal));
    AssertTrue(rendered.Contains("FinalHash:", StringComparison.Ordinal));
}

static void ValidationHarnessV2CompletionGateReportsSt1Complete()
{
    var report = CreateTestEngineCompletionChecklistRunner().Run(CreateSt1EngineCompletionRequest());

    AssertTrue(report.IsComplete);
    AssertTrue(report.Gates.Any(gate => gate.Id == "forbidden-dependencies" && gate.Status == CompletionGateStatus.Passed));
    AssertTrue(report.Gates.Any(gate => gate.Id == "target-deck-validation" && gate.Status == CompletionGateStatus.Passed));
    AssertTrue(report.Gates.Any(gate => gate.Id == "unsupported-mechanic-zero" && gate.Status == CompletionGateStatus.Passed));
    AssertTrue(report.Gates.Any(gate => gate.Id == "golden-scenario-suite" && gate.Status == CompletionGateStatus.Passed));
    AssertTrue(report.Gates.Any(gate => gate.Id == "replay-determinism" && gate.Status == CompletionGateStatus.Passed));
    AssertTrue(report.Gates.Any(gate => gate.Id == "invariant-fuzz" && gate.Status == CompletionGateStatus.Passed));
    AssertEmpty(report.FailedGates.ToArray());
    AssertEqual(0, report.TargetCardPool.UnsupportedCards.Count);
    AssertEqual(0, report.TargetCardPool.PartiallyImplementedCards.Count);
}

static void ValidationHarnessV2St1UnsupportedReport()
{
    var report = new TargetCardPoolValidator().Validate(new TargetCardPoolValidationRequest(
        "ST1",
        CreateSt1TargetDecks(),
        CreateSt1CardDatabase(),
        St1CardScriptCatalog.CreateRegistry()));

    AssertTrue(report.IsValid);
    AssertEqual(0, report.UnsupportedCards.Count);
    AssertEqual(0, report.PartiallyImplementedCards.Count);

    foreach (var cardId in St1UnsupportedCardIds())
    {
        AssertTrue(report.UnsupportedCards.Any(status => status.CardId == cardId));
    }

    foreach (var cardId in St1PartiallyImplementedCardIds())
    {
        AssertTrue(report.PartiallyImplementedCards.Any(status => status.CardId == cardId));
    }

    AssertFalse(report.MissingLayers.Contains("continuous-effect"));
    AssertFalse(report.MissingLayers.Contains("selection-result-application"));
    AssertFalse(report.MissingLayers.Contains("security-option-execution"));
    AssertFalse(report.MissingLayers.Contains("full-trigger-pipeline"));
    AssertFalse(report.MissingLayers.Contains("duration-cleanup"));
    AssertEmpty(report.MissingLayers.ToArray());
    AssertFalse(report.MissingScripts.Any());
}

static void ValidationHarnessV2InvariantFuzzCapturesFailures()
{
    var invalidState = CreateMinimalBattleState();
    var card = AddCardToZone(invalidState, 12101, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    invalidState.GetPlayer(PlayerId.Player0).Hand.Add(card);

    var report = new InvariantFuzzRunner(CreateTestRandomLegalActionRunner()).Run(new[]
    {
        new RandomLegalActionRunRequest("invalid-duplicate-membership", invalidState, MaxActions: 2, Seed: 1701),
    });

    AssertFalse(report.Passed);
    AssertEqual(1, report.Results.Count);
    AssertFalse(report.Results[0].Passed);
    AssertTrue(report.Results[0].Details.Contains("Engine invariant violation", StringComparison.Ordinal));
}

static void Tier1PrimitivesMoveDrawShuffleRevealSearchTrash()
{
    var state = CreateMinimalBattleState();
    var service = new Tier1PrimitiveService();
    var moved = AddCardToZone(state, 2011, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    service.MoveCard(state, new MoveCardCommand(moved, Zone.Deck, Zone.Hand, MoveReason.Effect));

    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(moved));

    var drawn = AddCardToZone(state, 2012, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    var drawResult = service.Draw(state, PlayerId.Player0, 1);

    AssertSequence(new[] { drawn }, drawResult.DrawnCards);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(drawn));

    var revealedSource = AddCardToZone(state, 2013, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    var reveal = service.RevealFromDeckTop(state, PlayerId.Player0, 1);

    AssertSequence(new[] { revealedSource }, reveal.RevealedCards);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Revealed.Contains(revealedSource));

    var searchRequest = service.CreateSearchRequest(
        state,
        PlayerId.Player0,
        "search:test",
        state.GetPlayer(PlayerId.Player0).Revealed,
        minCount: 0,
        maxCount: 1,
        canSkip: true,
        prompt: "search");

    AssertEqual(SelectionKind.SelectCard, searchRequest.Kind);
    AssertEqual(1, searchRequest.Candidates.Count);

    service.TrashCard(state, revealedSource, Zone.Revealed);

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(revealedSource));

    var firstShuffle = CreateMinimalBattleState();
    var secondShuffle = CreateMinimalBattleState();
    AddCardToZone(firstShuffle, 2021, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    AddCardToZone(firstShuffle, 2022, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    AddCardToZone(firstShuffle, 2023, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    AddCardToZone(secondShuffle, 2021, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    AddCardToZone(secondShuffle, 2022, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    AddCardToZone(secondShuffle, 2023, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    service.Shuffle(firstShuffle, PlayerId.Player0, Zone.Deck, new XorShiftDeterministicRng(123));
    service.Shuffle(secondShuffle, PlayerId.Player0, Zone.Deck, new XorShiftDeterministicRng(123));

    AssertSequence(firstShuffle.GetPlayer(PlayerId.Player0).Deck, secondShuffle.GetPlayer(PlayerId.Player0).Deck);
}

static void Tier1PrimitivesSecuritySuspendMemoryDp()
{
    var state = CreateMinimalBattleState();
    var service = new Tier1PrimitiveService();
    var security = AddCardToZone(state, 2031, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var permanent = AddBattlePermanent(state, 2032, 232, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    service.AddSecurity(state, PlayerId.Player0, security, Zone.Hand, faceUp: false);

    AssertTrue(state.GetPlayer(PlayerId.Player0).Security.Contains(security));
    AssertFalse(state.Cards[security].IsFaceUp);

    service.RemoveSecurity(state, PlayerId.Player0, security);

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(security));

    AssertTrue(service.Suspend(state, permanent.Id));
    AssertTrue(permanent.IsSuspended);
    AssertTrue(service.Unsuspend(state, permanent.Id));
    AssertFalse(permanent.IsSuspended);

    state.Memory = 3;
    AssertEqual(5, service.ModifyMemory(state, PlayerId.Player0, 2));
    AssertEqual(2, service.ModifyMemory(state, PlayerId.Player1, 3));

    AssertEqual(500, service.ModifyDP(state, permanent.Id, -2500));
    AssertEqual(-2500, permanent.DpModifier);
}

static void DurationModifierDpAffectsEffectiveDp()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 2101, 301, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 2102, 302, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        attacker.Id,
        amount: 3000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:dp");

    var result = new BattleResolver().ResolvePermanentBattle(state, attacker.Id, defender.Id);

    AssertSequence(new[] { defender.Id }, result.DestroyedPermanents);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
}

static void DurationModifierDpDoesNotMutateBaseDefinitionDp()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2111, 311, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 3000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:base-dp");

    AssertEqual(3000, state.CardDefinitions["BT1-ROOKIE"].DP);
    AssertEqual(0, permanent.DpModifier);
}

static void DurationCleanupUntilTurnEndRemovesModifier()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2121, 321, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 1000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:turn-end");

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEmpty(state.TemporaryModifiers);
}

static void DurationCleanupKeepsModifierBeforeExpiry()
{
    var state = CreateMinimalBattleState();
    state.TurnPlayerId = PlayerId.Player1;
    var permanent = AddBattlePermanent(state, 2131, 331, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 1000,
        DurationScope.UntilOwnerTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:owner-turn-end");

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(1, state.TemporaryModifiers.Count);
    AssertEqual("duration:test:owner-turn-end", state.TemporaryModifiers[0].StableId);
}

static void DurationCleanupRemovesStaleFieldTarget()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2141, 341, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var service = new Tier1PrimitiveService();

    service.AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 1000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:stale");
    service.DestroyPermanent(state, permanent.Id);

    AssertEmpty(state.TemporaryModifiers);
    AssertFalse(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(candidate => candidate.Id == permanent.Id));
}

static void DurationInvariantDetectsStaleModifierTarget()
{
    var state = CreateMinimalBattleState();
    state.TemporaryModifiers.Add(new TemporaryModifier(
        "duration:test:manual-stale",
        SourceCardId: null,
        SourcePermanentId: null,
        PlayerId.Player0,
        new PermanentId(9999),
        TargetPlayerId: null,
        TemporaryModifierKind.DP,
        Amount: 1000,
        DurationScope.UntilTurnEnd,
        CreatedTurnCount: state.TurnCount,
        CreatedPhase: state.Phase,
        ExpiresAtTurnPlayerId: null,
        DebugLabel: "manual stale"));

    AssertViolation(new EngineInvariantChecker().Check(state), "temporary-modifier-target-stale");
}

static void DurationModifiersStackDeterministically()
{
    var first = CreateMinimalBattleState();
    var second = first.Clone();
    var firstPermanent = AddBattlePermanent(first, 2151, 351, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var secondPermanent = AddBattlePermanent(second, 2151, 351, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var service = new Tier1PrimitiveService();

    service.AddTemporaryDPModifier(first, firstPermanent.Id, 1000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test:stack-a");
    service.AddTemporaryDPModifier(first, firstPermanent.Id, 2000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test:stack-b");
    service.AddTemporaryDPModifier(second, secondPermanent.Id, 1000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test:stack-a");
    service.AddTemporaryDPModifier(second, secondPermanent.Id, 2000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test:stack-b");

    AssertEqual(first.ComputeStateHash(), second.ComputeStateHash());

    var firstDefender = AddBattlePermanent(first, 2152, 352, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);
    var secondDefender = AddBattlePermanent(second, 2152, 352, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);

    AssertSequence(
        new[] { firstDefender.Id },
        new BattleResolver().ResolvePermanentBattle(first, firstPermanent.Id, firstDefender.Id).DestroyedPermanents);
    AssertSequence(
        new[] { secondDefender.Id },
        new BattleResolver().ResolvePermanentBattle(second, secondPermanent.Id, secondDefender.Id).DestroyedPermanents);
}

static void DurationModifierStateHashChanges()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2161, 361, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var before = state.ComputeStateHash();

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 1000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:hash");

    AssertNotEqual(before, state.ComputeStateHash());
}

static void DurationReplayDeterminismIncludesModifiers()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2171, 371, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        permanent.Id,
        amount: 1000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "duration:test:replay");

    var replay = new ReplayRunner(actionExecutor: CreateTestActionExecutor()).Replay(state, new GameTrace());

    AssertTrue(replay.InvariantReport.IsValid);
    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertEqual(1, replay.FinalState.TemporaryModifiers.Count);
}

static void DurationCleanupUntilBattleEndRemovesModifier()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 2181, 381, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 2182, 382, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);
    var service = new Tier1PrimitiveService();

    service.AddTemporaryDPModifier(
        state,
        attacker.Id,
        amount: 3000,
        DurationScope.UntilBattleEnd,
        PlayerId.Player0,
        stableId: "duration:test:battle-end");

    var result = new AttackService().Attack(state, new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(result.Battle is not null);
    AssertSequence(new[] { defender.Id }, result.Battle!.DestroyedPermanents);
    AssertEmpty(state.TemporaryModifiers);
}

static void DurationSecurityDigimonDpAffectsSecurityBattle()
{
    var state = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(state, 2191, 391, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 2192, "BT1-ROOKIE", PlayerId.Player1, Zone.Security, isFaceUp: false);

    new Tier1PrimitiveService().AddTemporarySecurityDigimonDPModifier(
        state,
        PlayerId.Player1,
        amount: 7000,
        DurationScope.UntilSecurityCheckEnd,
        PlayerId.Player1,
        stableId: "duration:test:security-dp");

    var result = new SecurityCheckService().CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertTrue(result.BattleResult is not null);
    AssertSequence(new[] { attacker.Id }, result.BattleResult!.DestroyedPermanents);
    AssertEmpty(state.TemporaryModifiers);
}

static void ContinuousDpModifierAffectsEffectiveDp()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6201, 741, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6202, "ST1-03", PlayerId.Player0, host.Id);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(4000, state.CardDefinitions["ST1-04"].DP);
    AssertEqual(5000, stats.Dp(state, host));
}

static void ContinuousDpModifierDoesNotMutateBaseDefinitionDp()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6211, 742, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6212, "ST1-03", PlayerId.Player0, host.Id);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(5000, stats.Dp(state, host));
    AssertEqual(4000, state.CardDefinitions["ST1-04"].DP);
    AssertEqual(2000, state.CardDefinitions["ST1-03"].DP);
}

static void ContinuousInheritedSourceAppliesOnlyFromSourceZone()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6221, 743, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 6222, "ST1-03", PlayerId.Player0, Zone.Hand);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(4000, stats.Dp(state, host));
}

static void ContinuousInheritedSourceStopsAfterZoneMove()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6231, 744, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 6232, "ST1-03", PlayerId.Player0, host.Id);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(5000, stats.Dp(state, host));

    new ZoneMover().MoveCard(
        state,
        new MoveCardCommand(source, Zone.EvolutionSources, Zone.Trash, MoveReason.Trash, SourcePermanent: host.Id));

    AssertEqual(4000, stats.Dp(state, host));
}

static void ContinuousOwnerTurnCondition()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6241, 745, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6242, "ST1-03", PlayerId.Player0, host.Id);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(5000, stats.Dp(state, host));

    state.TurnPlayerId = PlayerId.Player1;

    AssertEqual(4000, stats.Dp(state, host));
}

static void ContinuousSourceCountCondition()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6251, 746, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6252, "ST1-01", PlayerId.Player0, host.Id);
    AddEvolutionSource(state, 6253, "ST1-02", PlayerId.Player0, host.Id);
    AddEvolutionSource(state, 6254, "ST1-03", PlayerId.Player0, host.Id);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(5000, stats.Dp(state, host));

    AddEvolutionSource(state, 6255, "ST1-05", PlayerId.Player0, host.Id);

    AssertEqual(6000, stats.Dp(state, host));
}

static void ContinuousTamerAuraAppliesOwnerBattleArea()
{
    var state = CreateSt1ScenarioState();
    var tamer = AddBattlePermanent(state, 6261, 747, "ST1-12", PlayerId.Player0, 0, enterTurn: 1);
    var ownerDigimon = AddBattlePermanent(state, 6262, 748, "ST1-04", PlayerId.Player0, 1, enterTurn: 1);
    var ownerBreeding = AddBattlePermanent(state, 6263, 749, "ST1-03", PlayerId.Player0, 0, enterTurn: 1, isBreeding: true);
    var opponentDigimon = AddBattlePermanent(state, 6264, 750, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(0, stats.Dp(state, tamer));
    AssertEqual(5000, stats.Dp(state, ownerDigimon));
    AssertEqual(2000, stats.Dp(state, ownerBreeding));
    AssertEqual(4000, stats.Dp(state, opponentDigimon));
}

static void ContinuousDynamicSecurityAttackFromSourceCount()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6271, 751, "ST1-11", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6272, "ST1-02", PlayerId.Player0, attacker.Id);
    var keywordService = new BattleKeywordService(new EffectiveStatService(St1CardScriptCatalog.CreateRegistry()));

    AssertEqual(1, keywordService.SecurityAttackCount(state, attacker));

    AddEvolutionSource(state, 6273, "ST1-03", PlayerId.Player0, attacker.Id);
    AddEvolutionSource(state, 6274, "ST1-04", PlayerId.Player0, attacker.Id);
    AddEvolutionSource(state, 6275, "ST1-05", PlayerId.Player0, attacker.Id);

    AssertEqual(3, keywordService.SecurityAttackCount(state, attacker));

    state.TurnPlayerId = PlayerId.Player1;

    AssertEqual(1, keywordService.SecurityAttackCount(state, attacker));
}

static void ContinuousAndDurationModifiersStackDeterministically()
{
    var first = CreateSt1ScenarioState();
    var second = CreateSt1ScenarioState();
    var firstHost = AddBattlePermanent(first, 6281, 752, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var secondHost = AddBattlePermanent(second, 6281, 752, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(first, 6282, "ST1-03", PlayerId.Player0, firstHost.Id);
    AddEvolutionSource(second, 6282, "ST1-03", PlayerId.Player0, secondHost.Id);
    var primitives = new Tier1PrimitiveService();
    primitives.AddTemporaryDPModifier(first, firstHost.Id, 2000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test");
    primitives.AddTemporaryDPModifier(second, secondHost.Id, 2000, DurationScope.UntilTurnEnd, PlayerId.Player0, stableId: "duration:test");
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());

    AssertEqual(7000, stats.Dp(first, firstHost));
    AssertEqual(stats.Dp(first, firstHost), stats.Dp(second, secondHost));
    AssertEqual(first.ComputeStateHash(), second.ComputeStateHash());
}

static void ContinuousEffectsAreDerivedForStateHash()
{
    var state = CreateSt1ScenarioState();
    var host = AddBattlePermanent(state, 6291, 753, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());
    var beforeHash = state.ComputeStateHash();

    AssertEqual(4000, stats.Dp(state, host));
    AssertEmpty(state.TemporaryModifiers);

    AddEvolutionSource(state, 6292, "ST1-03", PlayerId.Player0, host.Id);

    AssertEqual(5000, stats.Dp(state, host));
    AssertEmpty(state.TemporaryModifiers);
    AssertNotEqual(beforeHash, state.ComputeStateHash());
}

static void St1ContinuousLayerValidationReport()
{
    var report = new TargetCardPoolValidator().Validate(new TargetCardPoolValidationRequest(
        "ST1",
        CreateSt1TargetDecks(),
        CreateSt1CardDatabase(),
        St1CardScriptCatalog.CreateRegistry()));

    AssertTrue(report.IsValid);
    AssertEqual(0, report.UnsupportedCards.Count);
    AssertEqual(0, report.PartiallyImplementedCards.Count);
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-01"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-03"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-11"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-08"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-13"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-14"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-12"));
    AssertFalse(report.MissingLayers.Contains("continuous-effect"));
    AssertFalse(report.MissingLayers.Contains("selection-result-application"));
    AssertFalse(report.MissingLayers.Contains("security-option-execution"));
}

static void Tier1PrimitivesPlayDigivolveBattleToken()
{
    var playState = CreateMinimalBattleState();
    var service = new Tier1PrimitiveService();
    var freeCard = AddCardToZone(playState, 2041, "BT1-FREE", PlayerId.Player0, Zone.Hand);

    var played = service.PlayWithoutPayingCost(playState, PlayerId.Player0, freeCard, Zone.Hand, 0, suspended: true);

    AssertEqual(freeCard, played.TopCardId);
    AssertTrue(played.IsSuspended);
    AssertEqual(5, playState.Memory);

    var digivolveState = CreateMinimalBattleState();
    var target = AddBattlePermanent(digivolveState, 2042, 242, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var champion = AddCardToZone(digivolveState, 2043, "BT1-CHAMPION", PlayerId.Player0, Zone.Revealed);
    var drawCard = AddCardToZone(digivolveState, 2044, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    service.DigivolveByEffect(digivolveState, PlayerId.Player0, champion, Zone.Revealed, target.Id, drawForDigivolution: true);

    AssertEqual(champion, target.TopCardId);
    AssertTrue(digivolveState.GetPlayer(PlayerId.Player0).Hand.Contains(drawCard));
    AssertEqual(5, digivolveState.Memory);

    var battleState = CreateMinimalBattleState();
    var attacker = AddBattlePermanent(battleState, 2045, 245, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(battleState, 2046, 246, "BT1-WEAK", PlayerId.Player1, 0, enterTurn: 1);

    var battle = service.Battle(battleState, attacker.Id, defender.Id);

    AssertSequence(new[] { defender.Id }, battle.DestroyedPermanents);
    AssertTrue(battleState.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));

    var tokenDefinition = new CardDefinition
    {
        CardId = "TOKEN-001",
        Name = "Token",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.White },
        Level = 3,
        PlayCost = 0,
        Dp = 1000,
    };

    var token = service.CreateToken(battleState, PlayerId.Player0, tokenDefinition, targetFrameIndex: 1);

    AssertEqual("TOKEN-001", battleState.Cards[token.TopCardId].DefinitionId);
    AssertEqual(Zone.BattleArea, battleState.Cards[token.TopCardId].CurrentZone);
}

static void Tier1RuleProcessorStabilizesInvalidPermanents()
{
    var state = CreateMinimalBattleState();
    var breedingOption = AddBattlePermanent(state, 2051, 251, "BT1-OPTION", PlayerId.Player0, 0, enterTurn: 1, isBreeding: true);
    var faceDown = AddBattlePermanent(state, 2052, 252, "BT1-ROOKIE", PlayerId.Player0, 1, enterTurn: 1);
    state.Cards[faceDown.TopCardId].IsFaceUp = false;

    var result = CreateTestRuleProcessor().ProcessUntilStable(state);

    AssertEqual(2, result.ChangesApplied);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(breedingOption.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(faceDown.TopCardId));
    AssertTrue(new EngineInvariantChecker().Check(state).IsValid);
}

static void Tier1RuleProcessorDpZeroPermanent()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2061, 261, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    new Tier1PrimitiveService().ModifyDP(state, permanent.Id, -3000);

    CreateTestRuleProcessor().ProcessUntilStable(state);

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(permanent.TopCardId));
    AssertFalse(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(candidate => candidate.Id == permanent.Id));
}

static void Tier1RuleProcessorTrimsExcessLinkCards()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2071, 271, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var first = new CardInstanceId(2072);
    var second = new CardInstanceId(2073);
    permanent.LinkedCards.Add(first);
    permanent.LinkedCards.Add(second);
    state.Cards.Add(first, new CardInstance(first, "BT1-OPTION", PlayerId.Player0)
    {
        CurrentZone = Zone.LinkedCards,
        PermanentId = permanent.Id,
    });
    state.Cards.Add(second, new CardInstance(second, "BT1-OPTION", PlayerId.Player0)
    {
        CurrentZone = Zone.LinkedCards,
        PermanentId = permanent.Id,
    });

    CreateTestRuleProcessor().ProcessUntilStable(state);

    AssertSequence(new[] { first }, permanent.LinkedCards);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(second));
}

static void Tier1RuleProcessorUsesInjectedZoneMoverForLinkTrim()
{
    var state = CreateMinimalBattleState();
    var permanent = AddBattlePermanent(state, 2074, 274, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var first = AddLinkedCard(state, 2075, "BT1-OPTION", PlayerId.Player0, permanent.Id);
    var second = AddLinkedCard(state, 2076, "BT1-OPTION", PlayerId.Player0, permanent.Id);
    var spyMover = new SpyZoneMover();

    CreateTestRuleProcessor(zoneMover: spyMover).ProcessUntilStable(state);

    AssertSequence(new[] { first }, permanent.LinkedCards);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(second));
    AssertEqual(1, spyMover.MoveCardCommands.Count(command =>
        command.Card == second
        && command.SourceZone == Zone.LinkedCards
        && command.DestinationZone == Zone.Trash
        && command.SourcePermanent == permanent.Id));
}

static void Tier1RuleProcessorMaxIterationGuard()
{
    var state = CreateMinimalBattleState();

    AssertThrows<UnsupportedMechanicException>(() =>
        CreateTestRuleProcessor(options: new RuleProcessorOptions(MaxIterations: 0)).ProcessUntilStable(state));
}

static void Tier1RuleProcessorHasNoDirectZoneMoverConstruction()
{
    var path = Path.Combine(WorkspaceRoot(), "src", "DCGO.RL.Engine", "Battle", "RuleProcessor.cs");
    var content = File.ReadAllText(path);

    AssertFalse(content.Contains("new ZoneMover", StringComparison.Ordinal));
}

static void Tier1EffectQueueTriggerCollectionByTiming()
{
    var state = CreateMinimalBattleState();
    var context = new EffectContext(state, EffectTiming.OnDraw, PlayerId.Player0);
    var collector = new TriggerCollector();
    collector.Register(new EffectDescriptor("draw:queued", EffectTiming.OnDraw, Controller: PlayerId.Player0));
    collector.Register(new EffectDescriptor("draw:background", EffectTiming.OnDraw, Controller: PlayerId.Player0, IsBackground: true));
    collector.Register(new EffectDescriptor("security:queued", EffectTiming.OnAddSecurity, Controller: PlayerId.Player0));

    var collected = collector.Collect(context);
    var queue = new EffectQueue();
    queue.EnqueueRange(collected.QueuedEffects);

    AssertEqual(1, collected.QueuedEffects.Count);
    AssertEqual(1, collected.BackgroundEffects.Count);
    AssertEqual("draw:queued", queue.Dequeue().StableId);
    AssertFalse(queue.HasPending);
}

static void TriggerPipelineCollectsFieldTopCardDescriptor()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-FIELD"] = CardEffectTestFixture.EffectDefinition("FX-FIELD", "FX_Field");
    var permanent = AddBattlePermanent(state, 6001, 601, "FX-FIELD", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript("FX-FIELD", "FX_Field", EffectTiming.OnStartMainPhase, amount: 1)));

    var result = pipeline.Run(state, EffectTiming.OnStartMainPhase, PlayerId.Player0);

    AssertEqual(1, result.QueuedEffects.Count);
    AssertEqual(permanent.TopCardId, result.QueuedEffects[0].SourceCard!.Value);
    AssertEqual(permanent.Id, result.QueuedEffects[0].SourcePermanent!.Value);
    AssertEqual(6, state.Memory);
}

static void TriggerPipelineCollectsInheritedSourceDescriptor()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-HOST"] = CardEffectTestFixture.NoEffectDefinition("FX-HOST");
    state.CardDefinitions["FX-SOURCE"] = CardEffectTestFixture.EffectDefinition("FX-SOURCE", "FX_Source");
    var host = AddBattlePermanent(state, 6011, 611, "FX-HOST", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 6012, "FX-SOURCE", PlayerId.Player0, host.Id);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new NoEffectCardScript("FX-HOST"),
        new TimingMemoryCardScript("FX-SOURCE", "FX_Source", EffectTiming.OnBlockAnyone, amount: 2)));

    var result = pipeline.Run(state, EffectTiming.OnBlockAnyone, PlayerId.Player0, sourcePermanent: host.Id);

    AssertEqual(1, result.QueuedEffects.Count);
    AssertEqual(source, result.QueuedEffects[0].SourceCard!.Value);
    AssertEqual(host.Id, result.QueuedEffects[0].SourcePermanent!.Value);
    AssertEqual(7, state.Memory);
}

static void TriggerPipelineEnqueuesAndDrainsNonBackgroundEffect()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-DRAIN"] = CardEffectTestFixture.EffectDefinition("FX-DRAIN", "FX_Drain");
    AddBattlePermanent(state, 6021, 621, "FX-DRAIN", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript("FX-DRAIN", "FX_Drain", EffectTiming.RulesTiming, amount: 1)));

    var result = pipeline.Run(state, EffectTiming.RulesTiming, PlayerId.Player0);

    AssertEqual(1, result.QueuedEffects.Count);
    AssertEqual(1, result.ExecutedEffects.Count);
    AssertFalse(result.HasPendingSelection);
    AssertEqual(6, state.Memory);
}

static void TriggerPipelineSeparatesBackgroundEffect()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-BACKGROUND"] = CardEffectTestFixture.EffectDefinition("FX-BACKGROUND", "FX_Background");
    AddBattlePermanent(state, 6031, 631, "FX-BACKGROUND", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript(
            "FX-BACKGROUND",
            "FX_Background",
            EffectTiming.RulesTiming,
            amount: 1,
            isBackground: true,
            throwsUnsupported: true)));

    var result = pipeline.Run(state, EffectTiming.RulesTiming, PlayerId.Player0);

    AssertEqual(0, result.QueuedEffects.Count);
    AssertEqual(1, result.BackgroundEffects.Count);
    AssertEqual(0, result.ExecutedEffects.Count);
    AssertEqual(5, state.Memory);
}

static void TriggerPipelineSelectionBoundaryReturnsPendingRequest()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-SELECT"] = CardEffectTestFixture.EffectDefinition("FX-SELECT", "FX_Select");
    var source = AddCardToZone(state, 6041, "FX-SELECT", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 6042, 642, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1)));

    var result = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, source);

    AssertTrue(result.HasPendingSelection);
    AssertEqual("test-selection:FX-SELECT", result.PendingSelectionRequest!.Id);
    AssertEqual(target.Id, result.PendingSelectionRequest.Candidates[0].Permanent!.Value);
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
}

static void TriggerPipelineOptionalProviderPausesExplicitSelection()
{
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        true));
    var services = CreateOptionalSelectionServices(decisionProvider: provider);
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out var option, out var target);

    var result = services.TriggerPipelineService.Run(
        state,
        EffectTiming.OptionSkill,
        PlayerId.Player0,
        option);

    AssertTrue(result.HasPendingSelection);
    AssertEqual("test-target:FX-OPT", result.PendingSelectionRequest!.Id);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));
}

static void TriggerPipelineUnsupportedBodyFailsExplicitly()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-UNSUPPORTED"] = CardEffectTestFixture.EffectDefinition("FX-UNSUPPORTED", "FX_Unsupported");
    AddBattlePermanent(state, 6051, 651, "FX-UNSUPPORTED", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript(
            "FX-UNSUPPORTED",
            "FX_Unsupported",
            EffectTiming.RulesTiming,
            amount: 1,
            throwsUnsupported: true)));

    AssertThrows<UnsupportedMechanicException>(() => pipeline.Run(state, EffectTiming.RulesTiming, PlayerId.Player0));
}

static void TriggerPipelineOncePerTurnPreventsDuplicateExecution()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-ONCE"] = CardEffectTestFixture.EffectDefinition("FX-ONCE", "FX_Once");
    AddBattlePermanent(state, 6061, 661, "FX-ONCE", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript(
            "FX-ONCE",
            "FX_Once",
            EffectTiming.OnStartMainPhase,
            amount: 1,
            isOncePerTurn: true)));

    pipeline.Run(state, EffectTiming.OnStartMainPhase, PlayerId.Player0);

    AssertEqual(6, state.Memory);
    AssertThrows<DomainException>(() => pipeline.Run(state, EffectTiming.OnStartMainPhase, PlayerId.Player0));
}

static void TriggerPipelineOnStartMainPhaseHookInvokesDescriptor()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-MAIN"] = CardEffectTestFixture.EffectDefinition("FX-MAIN", "FX_Main");
    AddBattlePermanent(state, 6071, 671, "FX-MAIN", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript("FX-MAIN", "FX_Main", EffectTiming.OnStartMainPhase, amount: 1)));

    new PhaseRunner(triggerPipelineService: pipeline).RunMainPhase(state);

    AssertEqual(Phase.Main, state.Phase);
    AssertEqual(6, state.Memory);
}

static void TriggerPipelineWhenDigivolvingHookInvokesDescriptor()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-CHAMPION"] = state.CardDefinitions["BT1-CHAMPION"] with
    {
        CardId = "FX-CHAMPION",
        Name = "FX-CHAMPION",
        CardEffectClassName = "FX_Digivolve",
    };
    var target = AddBattlePermanent(state, 6081, 681, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var evolveCard = AddCardToZone(state, 6082, "FX-CHAMPION", PlayerId.Player0, Zone.Hand);
    AddCardToZone(state, 6083, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript("FX-CHAMPION", "FX_Digivolve", EffectTiming.WhenDigivolving, amount: 1)));

    new DigivolveService(triggerPipelineService: pipeline).Digivolve(
        state,
        new DigivolveAction(PlayerId.Player0, evolveCard, target.Id));

    AssertEqual(evolveCard, target.TopCardId);
    AssertEqual(4, state.Memory);
}

static void TriggerPipelineOnAllyAttackHookInvokesDescriptor()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-ATTACK", "FX_Attack");
    var attacker = AddBattlePermanent(state, 6091, 691, "FX-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 6092, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new TimingMemoryCardScript("FX-ATTACK", "FX_Attack", EffectTiming.OnAllyAttack, amount: 1)));

    new AttackService(triggerPipelineService: pipeline).Attack(
        state,
        new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertEqual(6, state.Memory);
}

static void TriggerPipelineOnEndAttackDeterministic()
{
    static GameState Run()
    {
        var state = CreateMinimalBattleState();
        state.CardDefinitions["FX-END-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-END-ATTACK", "FX_EndAttack");
        var attacker = AddBattlePermanent(state, 6101, 701, "FX-END-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
        AddCardToZone(state, 6102, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);
        var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
            new TimingMemoryCardScript("FX-END-ATTACK", "FX_EndAttack", EffectTiming.OnEndAttack, amount: 1)));

        new AttackService(triggerPipelineService: pipeline).Attack(
            state,
            new AttackAction(PlayerId.Player0, attacker.Id, null));
        return state;
    }

    var first = Run();
    var second = Run();

    AssertEqual(6, first.Memory);
    AssertEqual(first.ComputeStateHash(), second.ComputeStateHash());
}

static void TriggerPipelineOptionSkillCompatibleWithSt1Option()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 6111, "ST1-15", PlayerId.Player0, Zone.Executing);
    var first = AddBattlePermanent(state, 6112, 712, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-15:option:delete",
        new[] { PermanentSelectionTarget(first) }));
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry(), provider);

    var result = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, source);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(first.TopCardId));
}

static void TriggerPipelineSecuritySkillCompatibleWithSecurityService()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6121, 721, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6122, 722, "ST1-05", PlayerId.Player0, 1, enterTurn: 1);
    var security = AddCardToZone(state, 6123, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(target) }));

    var result = CreateSt1SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertEqual(1, result.SecurityEffectResults.Count);
    AssertEqual(1, result.SecurityEffectResults[0].ActivatedMainOptionResolutions.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(target.TopCardId));
}

static void SecurityEffectExecutionOptionalProviderPausesExplicitSelection()
{
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        true));
    var services = CreateOptionalSelectionServices(
        includeSecurityActivation: true,
        decisionProvider: provider);
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out _, out var target);
    var security = AddCardToZone(state, 6124, "FX-OPT", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var attacker = AddBattlePermanent(state, 6125, 723, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);

    var result = services.SecurityCheckService.CheckSecurityWithResult(state, attacker.Id, PlayerId.Player1);

    AssertTrue(result.HasPendingSelection);
    AssertEqual("test-target:FX-OPT", result.PendingSelectionRequest!.Id);
    AssertEqual(Zone.Executing, state.Cards[security].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));
}

static void OptionLifecycleHandPlayMovesThroughExecutingToTrash()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-OPTION"] = CardEffectTestFixture.OptionEffectDefinition("FX-OPTION", "FX_Option", playCost: 2);
    var option = AddCardToZone(state, 6201, "FX-OPTION", PlayerId.Player0, Zone.Hand);
    var script = new OptionSourceZoneProbeScript("FX-OPTION", "FX_Option");
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(script));
    var service = new PlayCardService(triggerPipelineService: pipeline);

    var result = service.PlayOptionFromHand(state, new PlayCardAction(PlayerId.Player0, option, -1));

    AssertFalse(result.HasPendingSelection);
    AssertTrue(result.MovedToTrash);
    AssertEqual(1, script.ResolveCount);
    AssertEqual(Zone.Executing, script.ObservedPayloadSourceZone!.Value);
    AssertEqual(Zone.Executing, script.ObservedCardZone!.Value);
    AssertEqual(3, state.Memory);
    AssertFalse(state.GetPlayer(PlayerId.Player0).Hand.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertEqual(Zone.Trash, state.Cards[option].CurrentZone);
    new EngineInvariantChecker().ThrowIfInvalid(state);
}

static void OptionLifecycleSourceContextIsExecuting()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-CONTEXT"] = CardEffectTestFixture.OptionEffectDefinition("FX-CONTEXT", "FX_Context");
    var option = AddCardToZone(state, 6211, "FX-CONTEXT", PlayerId.Player0, Zone.Hand);
    var script = new OptionSourceZoneProbeScript("FX-CONTEXT", "FX_Context");
    var service = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(CardEffectTestFixture.Registry(script)));

    service.PlayOptionFromHand(state, new PlayCardAction(PlayerId.Player0, option, -1));

    AssertEqual(Zone.Executing, script.ObservedPayloadSourceZone!.Value);
    AssertEqual(Zone.Executing, script.ObservedCardZone!.Value);
    AssertEqual(Zone.Trash, state.Cards[option].CurrentZone);
}

static void OptionLifecyclePendingSelectionKeepsExecuting()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-SELECT"] = CardEffectTestFixture.OptionEffectDefinition("FX-SELECT", "FX_Select");
    var option = AddCardToZone(state, 6221, "FX-SELECT", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 6222, 822, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1)));
    var service = new PlayCardService(triggerPipelineService: pipeline);

    var result = service.PlayOptionFromHand(state, new PlayCardAction(PlayerId.Player0, option, -1));

    AssertTrue(result.HasPendingSelection);
    AssertEqual("test-selection:FX-SELECT", result.PendingSelectionRequest!.Id);
    AssertEqual(Zone.Executing, state.Cards[option].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
    AssertEqual(4, state.Memory);
    new EngineInvariantChecker().ThrowIfInvalid(state);
}

static void OptionLifecycleSelectionCompletionTrashesOption()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-SELECT"] = CardEffectTestFixture.OptionEffectDefinition("FX-SELECT", "FX_Select");
    var option = AddCardToZone(state, 6231, "FX-SELECT", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 6232, 832, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1)));
    var session = services.CreateSession(state);
    var pending = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    var request = pending.PendingDecisionPoint!.SelectionRequest!;
    var selected = request.Candidates.Single(candidate => candidate.Permanent == target.Id);

    var completed = ResumeWithDecision(session, pending, SelectionResult.ForTargets(request.Id, new[] { selected }));

    AssertFalse(completed.IsPaused);
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertEqual(Zone.Trash, state.Cards[option].CurrentZone);
    new EngineInvariantChecker().ThrowIfInvalid(state);
}

static void OptionLifecycleMovedSourceSkipsFollowupTrash()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-MOVE"] = CardEffectTestFixture.OptionEffectDefinition("FX-MOVE", "FX_Move");
    var option = AddCardToZone(state, 6241, "FX-MOVE", PlayerId.Player0, Zone.Hand);
    var pipeline = new TriggerPipelineService(CardEffectTestFixture.Registry(
        new MoveSourceOptionScript("FX-MOVE", "FX_Move", Zone.Hand)));
    var service = new PlayCardService(triggerPipelineService: pipeline);

    var result = service.PlayOptionFromHand(state, new PlayCardAction(PlayerId.Player0, option, -1));

    AssertFalse(result.HasPendingSelection);
    AssertFalse(result.MovedToTrash);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertEqual(Zone.Hand, state.Cards[option].CurrentZone);
    new EngineInvariantChecker().ThrowIfInvalid(state);
}

static void OptionLifecycleInvalidActionLeavesZonesClean()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-OPTION"] = CardEffectTestFixture.OptionEffectDefinition("FX-OPTION", "FX_Option");
    var option = AddCardToZone(state, 6251, "FX-OPTION", PlayerId.Player0, Zone.Deck);
    var beforeHash = state.ComputeStateHash();
    var beforeMemory = state.Memory;
    var service = new PlayCardService(triggerPipelineService: new TriggerPipelineService(CardEffectTestFixture.Registry(
        new OptionSourceZoneProbeScript("FX-OPTION", "FX_Option"))));

    AssertThrows<DomainException>(() =>
        service.PlayOptionFromHand(state, new PlayCardAction(PlayerId.Player0, option, -1)));

    AssertEqual(beforeMemory, state.Memory);
    AssertEqual(beforeHash, state.ComputeStateHash());
    AssertTrue(state.GetPlayer(PlayerId.Player0).Deck.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertEqual(Zone.Deck, state.Cards[option].CurrentZone);
}

static void OptionLifecycleActionTraceReplayDeterministic()
{
    static (string Hash, GameTrace Trace) Run()
    {
        var state = CreateMinimalBattleState();
        state.CardDefinitions["FX-OPTION"] = CardEffectTestFixture.OptionEffectDefinition("FX-OPTION", "FX_Option");
        var option = AddCardToZone(state, 6261, "FX-OPTION", PlayerId.Player0, Zone.Hand);
        var initial = state.Clone();
        var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
            new TimingMemoryCardScript("FX-OPTION", "FX_Option", EffectTiming.OptionSkill, amount: 1)));
        var executor = services.ActionExecutor;
        var trace = new GameTrace();

        executor.Execute(state, new PlayCardAction(PlayerId.Player0, option, -1), trace);

        new EngineInvariantChecker().ThrowIfInvalid(state);
        var replay = new ReplayRunner(actionExecutor: executor).Replay(initial, trace);
        AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
        return (state.ComputeStateHash(), trace);
    }

    var first = Run();
    var second = Run();

    AssertEqual(first.Hash, second.Hash);
    AssertTrue(first.Trace.Events.Count > 0);
}

static void RuntimeCompositionProductionGraphValidatesRequiredServices()
{
    var services = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry());

    AssertTrue(services.ValidationReport.IsValid);
    AssertTrue(services.TriggerPipelineService is not null);
    AssertTrue(services.PrimitiveService is not null);
    AssertTrue(services.ActionExecutor is not null);
    AssertTrue(services.TurnRunner is not null);
}

static void RuntimeCompositionMissingTriggerPipelineFailsValidation()
{
    var report = BattleEngineServices.Validate(
        triggerPipelineService: null,
        zoneMover: new ZoneMover(),
        primitiveService: new Tier1PrimitiveService(),
        invariantChecker: new EngineInvariantChecker(),
        securityCheckService: new SecurityCheckService());

    AssertFalse(report.IsValid);
    AssertTrue(report.Issues.Any(issue => issue.DependencyName == nameof(TriggerPipelineService)));
    AssertThrows<DomainException>(() => report.ThrowIfInvalid());
}

static void RuntimeCompositionPlayCardServiceRequiresTriggerPipeline()
{
    AssertThrows<ArgumentNullException>(() => new PlayCardService(null!));
}

static void RuntimeCompositionActionExecutorReturnsPendingOptionSelection()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-SELECT"] = CardEffectTestFixture.OptionEffectDefinition("FX-SELECT", "FX_Select");
    var option = AddCardToZone(state, 6265, "FX-SELECT", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 6266, 866, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1)));
    var executor = services.ActionExecutor;

    var result = executor.Execute(state, new PlayCardAction(PlayerId.Player0, option, -1));

    AssertTrue(result.HasPendingSelection);
    AssertFalse(result.RulesProcessed);
    AssertTrue(result.PendingDecisionPoint is not null);
    AssertEqual(DecisionKind.Selection, result.PendingDecisionPoint!.Kind);
    AssertEqual(result.PendingSelectionRequest, result.PendingDecisionPoint.SelectionRequest);
    AssertEqual("test-selection:FX-SELECT", result.PendingSelectionRequest!.Id);
    AssertEqual(target.Id, result.PendingSelectionRequest.Candidates[0].Permanent!.Value);
    AssertEqual(Zone.Executing, state.Cards[option].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Executing.Contains(option));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
}

static void RuntimeCompositionPrimitiveSecurityCheckUsesSecurityEffects()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6267, 867, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6268, 868, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    var security = AddCardToZone(state, 6269, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(target) }));
    var services = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry(), provider);

    var result = services.PrimitiveService.SecurityCheck(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertEqual(1, result.SecurityEffectResults.Count);
    AssertEqual("ST1-16:security:main-option-activation", result.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
}

static void RuntimeCompositionPrimitiveSecurityCheckUsesContinuousSecurityAttack()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6270, 870, "ST1-11", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6271, "ST1-02", PlayerId.Player0, attacker.Id);
    AddEvolutionSource(state, 6272, "ST1-03", PlayerId.Player0, attacker.Id);
    AddEvolutionSource(state, 6273, "ST1-04", PlayerId.Player0, attacker.Id);
    AddEvolutionSource(state, 6274, "ST1-05", PlayerId.Player0, attacker.Id);
    var first = AddCardToZone(state, 6275, "ST1-04", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var second = AddCardToZone(state, 6276, "ST1-04", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var third = AddCardToZone(state, 6277, "ST1-04", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var services = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry());

    var result = services.PrimitiveService.SecurityCheck(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { first, second, third }, result.CheckedCards);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(first));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(second));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(third));
}

static void RuntimeCompositionPrimitiveSecurityCheckUsesContinuousSecurityDigimonDp()
{
    var state = CreateSt2St3ScenarioState();
    AddBattlePermanent(state, 6278, 878, "ST3-12", PlayerId.Player1, 0, enterTurn: 1);
    var attacker = AddBattlePermanent(state, 6279, 879, "ST3-09", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 6280, "ST3-06", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var services = BattleEngineServices.Create(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var result = services.PrimitiveService.SecurityCheck(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertTrue(result.BattleResult is not null);
    AssertSequence(new[] { attacker.Id }, result.BattleResult!.DestroyedPermanents);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
}

static void RuntimeCompositionDefaultConstructorsFail()
{
    AssertThrows<DomainException>(() => new ActionExecutor());
    AssertThrows<DomainException>(() => new TurnRunner());
    AssertThrows<DomainException>(() => new ScriptedScenarioRunner());
    AssertThrows<DomainException>(() => new RandomLegalActionRunner());
    AssertThrows<DomainException>(() => new ReplayRunner());
    AssertThrows<DomainException>(() => new ReplayDeterminismHelper());
}

static void RuntimeCompositionEffectBearingCardDoesNotBecomeLegacyNoEffect()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-EFFECT"] = CardEffectTestFixture.OptionEffectDefinition("FX-EFFECT", "FX_Effect", playCost: 0);
    var option = AddCardToZone(state, 6281, "FX-EFFECT", PlayerId.Player0, Zone.Hand);
    var services = BattleEngineServices.Create(new CardScriptRegistry());

    AssertThrows<UnsupportedMechanicException>(() =>
        services.ActionExecutor.Execute(state, new PlayCardAction(PlayerId.Player0, option, -1)));
}

static void RuntimeCompositionMixedCustomDefaultGraphFails()
{
    var playCardService = new PlayCardService(
        new TriggerPipelineService(new TestNoEffectCardScriptRegistry()));

    AssertThrows<DomainException>(() => new ActionExecutor(playCardService: playCardService));
}

static void RuntimeCompositionScriptedRunnerReturnsPendingSelection()
{
    var state = CreatePendingOptionScenarioState(out var option);
    var services = CreatePendingOptionServices();
    var scenario = new ScriptedScenario(
        "pending option",
        state,
        new[] { new PlayCardAction(PlayerId.Player0, option, -1) });
    var runner = new ScriptedScenarioRunner(services);

    var result = runner.Run(scenario);

    AssertEqual(ScenarioRunStatus.PausedForDecision, result.Status);
    AssertTrue(result.InvariantReport.IsValid);
    AssertEqual("test-selection:FX-SELECT", result.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-SELECT:selection:Destroy", result.PendingStableContinuationId);
}

static void RuntimeCompositionRandomRunnerReturnsPendingSelection()
{
    var state = CreatePendingOptionScenarioState(out _);
    var services = CreatePendingOptionServices();
    var runner = new RandomLegalActionRunner(services);

    var result = runner.Run(new RandomLegalActionRunRequest(
        "pending-option",
        state,
        MaxActions: 1,
        Seed: 2));

    AssertEqual(ScenarioRunStatus.PausedForDecision, result.Status);
    AssertTrue(result.InvariantReport.IsValid);
    AssertEqual("test-selection:FX-SELECT", result.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-SELECT:selection:Destroy", result.PendingStableContinuationId);
}

static void RuntimeRuleStateCloneRestoreHash()
{
    var state = CreateMinimalBattleState();
    state.TurnCount = 1;
    state.RuntimeRules.RegisterOncePerTurnUse(
        state.TurnCount,
        PlayerId.Player0,
        "effect:once",
        new CardInstanceId(1));
    var clone = state.Clone();
    var beforeHash = state.ComputeStateHash();

    AssertEqual(1, clone.RuntimeRules.OncePerTurnUses.Count);
    AssertEqual(beforeHash, clone.ComputeStateHash());

    state.RuntimeRules.RegisterOncePerTurnUse(
        state.TurnCount,
        PlayerId.Player0,
        "effect:other",
        new CardInstanceId(2));
    AssertNotEqual(beforeHash, state.ComputeStateHash());

    state.RestoreFrom(clone);
    AssertEqual(beforeHash, state.ComputeStateHash());
    AssertEqual(1, state.RuntimeRules.OncePerTurnUses.Count);

    state.TurnCount = 2;
    AssertEqual(0, state.RuntimeRules.OncePerTurnUses.Count);
}

static void DirectApiRollbackRestoresOncePerTurnAvailability()
{
    var services = CreateOnceThenSelectionServices();
    var state = CreateOnceThenSelectionScenarioState(out var card, out _);
    var beforeHash = state.ComputeStateHash();

    AssertPendingSynchronousCallRollsBack(
        state,
        (currentState, trace) => services.PlayCardService.Play(
            currentState,
            new PlayCardAction(PlayerId.Player0, card, 0),
            trace));
    AssertEqual(beforeHash, state.ComputeStateHash());
    AssertEqual(0, state.RuntimeRules.OncePerTurnUses.Count);

    var session = services.CreateSession(state);
    var paused = session.Step(new PlayCardAction(PlayerId.Player0, card, 0));

    AssertTrue(paused.IsPaused);
    AssertEqual(1, state.RuntimeRules.OncePerTurnUses.Count);
    AssertEqual("test-once-selection:FX-ONCE-SELECT", paused.PendingDecisionPoint!.SelectionRequest!.Id);
}

static void EngineSessionOncePerTurnStateIsSessionLocal()
{
    var services = CreateOnceThenSelectionServices();
    var firstState = CreateOnceThenSelectionScenarioState(out var firstCard, out _);
    var secondState = CreateOnceThenSelectionScenarioState(out var secondCard, out _);
    var firstSession = services.CreateSession(firstState);
    var secondSession = services.CreateSession(secondState);

    var firstPaused = firstSession.Step(new PlayCardAction(PlayerId.Player0, firstCard, 0));
    var secondPaused = secondSession.Step(new PlayCardAction(PlayerId.Player0, secondCard, 0));

    AssertTrue(firstPaused.IsPaused);
    AssertTrue(secondPaused.IsPaused);
    AssertEqual(1, firstState.RuntimeRules.OncePerTurnUses.Count);
    AssertEqual(1, secondState.RuntimeRules.OncePerTurnUses.Count);
}

static void RepeatedGamesDoNotAccumulateOncePerTurnState()
{
    var services = CreateOnceThenSelectionServices();

    for (var i = 0; i < 5; i++)
    {
        var state = CreateOnceThenSelectionScenarioState(out var card, out _);
        var session = services.CreateSession(state);

        var paused = session.Step(new PlayCardAction(PlayerId.Player0, card, 0));

        AssertTrue(paused.IsPaused);
        AssertEqual(1, state.RuntimeRules.OncePerTurnUses.Count);
    }
}

static void EngineSessionPublicResumeRequiresDecisionToken()
{
    var resumeMethods = typeof(EngineSession)
        .GetMethods()
        .Where(method => method.Name == nameof(EngineSession.Resume))
        .ToArray();

    AssertTrue(resumeMethods.All(method =>
        method.GetParameters().Length == 1
        && method.GetParameters()[0].ParameterType == typeof(DecisionResult)));
    AssertFalse(typeof(DecisionResult).GetConstructors()
        .Any(ctor => ctor.GetParameters().Length == 2));
}

static void ReplayUsesRecordedDecisionToken()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var initial = state.Clone();
    var target = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-SELECT",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var selectionEvent = session.Trace.Events.Single(traceEvent => traceEvent.Kind == TraceEventKind.Selection);
    AssertEqual(paused.PendingDecisionToken!.Value, selectionEvent.DecisionResult!.Token);

    var serialized = new TraceStore().Save(session.Trace);
    var loaded = new TraceStore().Load(serialized);
    var loadedSelection = loaded.Events.Single(traceEvent => traceEvent.Kind == TraceEventKind.Selection);
    AssertEqual(paused.PendingDecisionToken.Value, loadedSelection.DecisionResult!.Token);

    var replay = new ReplayRunner(services: services).Replay(initial, loaded);
    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
}

static void ReplayRejectsInvalidDecisionPayloads()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var initial = state.Clone();
    var target = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    _ = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-SELECT",
        new[] { PermanentSelectionTarget(target) }));

    AssertReplayDecisionMutationFails(
        services,
        initial,
        session.Trace,
        decision => decision with { Token = new DecisionToken(decision.Token.Value + 1) });
    AssertReplayDecisionMutationFails(
        services,
        initial,
        session.Trace,
        decision => decision with { Player = PlayerId.Player1 });
    AssertReplayDecisionMutationFails(
        services,
        initial,
        session.Trace,
        decision => decision with { SelectionResult = SelectionResult.Skip("wrong-request") });
}

static void EngineSessionOptionSelectionPauseResume()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var target = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual("test-selection:FX-SELECT", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-SELECT:selection:Destroy", paused.PendingStableContinuationId);
    AssertEqual(Zone.Executing, state.Cards[option].CurrentZone);
    AssertThrows<DomainException>(() => session.Step(new PassAction(PlayerId.Player0)));

    var completed = session.Resume(new DecisionResult(
        PlayerId.Player0,
        paused.PendingDecisionToken!.Value,
        SelectionResult.ForTargets(
            "test-selection:FX-SELECT",
            new[] { PermanentSelectionTarget(target) })));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertFalse(session.HasPendingDecision);
}

static void EngineSessionOnPlaySelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-ONPLAY",
            "FX_OnPlay",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnPlay)));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-ONPLAY"] = CardEffectTestFixture.EffectDefinition("FX-ONPLAY", "FX_OnPlay");
    var card = AddCardToZone(state, 6295, "FX-ONPLAY", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 6296, 896, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, card, 0));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual("test-selection:FX-ONPLAY", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-ONPLAY:selection:Destroy", paused.PendingStableContinuationId);
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == card));

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-ONPLAY",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == card));
}

static void EngineSessionWhenDigivolvingSelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-DIGI",
            "FX_Digi",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.WhenDigivolving),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only digivolution source host.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-DIGI"] = CardEffectTestFixture.EffectDefinition("FX-DIGI", "FX_Digi") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
        EvoCosts = new[] { new EvoCostDefinition(CardColor.Red, 3, 2) },
    };
    state.Memory = 5;
    var host = AddBattlePermanent(state, 6297, 897, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var card = AddCardToZone(state, 6298, "FX-DIGI", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 6299, 898, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new DigivolveAction(PlayerId.Player0, card, host.Id));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual("test-selection:FX-DIGI", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-DIGI:selection:Destroy", paused.PendingStableContinuationId);
    AssertEqual(card, host.TopCardId);
    AssertTrue(host.SourceCardIds.Contains(new CardInstanceId(6297)));

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-DIGI",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertEqual(card, host.TopCardId);
}

static void EngineSessionOnStartMainPhaseSelectionPauseResume()
{
    var services = CreatePhaseSelectionServices();
    var state = CreatePhaseSelectionScenarioState(out var target);
    var session = services.CreateSession(state);

    var paused = session.RunMainPhase();

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual(Phase.Main, state.Phase);
    AssertEqual("test-selection:FX-PHASE", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-PHASE:selection:Destroy", paused.PendingStableContinuationId);
    AssertThrows<DomainException>(() => session.Step(new PassAction(PlayerId.Player0)));

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-PHASE",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OnStartMainPhase, completed.CompletedTiming);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertEqual(Phase.Main, state.Phase);
}

static void EngineSessionPassOnEndTurnSelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-END",
            "FX_End",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnEndTurn),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only pass end-turn target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-END"] = CardEffectTestFixture.EffectDefinition("FX-END", "FX_End");
    AddBattlePermanent(state, 6302, 901, "FX-END", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6303, 902, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new PassAction(PlayerId.Player0));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual(Phase.End, state.Phase);
    AssertEqual(PlayerId.Player0, state.TurnPlayerId);
    AssertEqual("test-selection:FX-END", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-END:selection:Destroy", paused.PendingStableContinuationId);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-END",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OnEndTurn, completed.CompletedTiming);
    AssertEqual(Phase.Active, state.Phase);
    AssertEqual(PlayerId.Player1, state.TurnPlayerId);
    AssertEqual(3, state.Memory);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
}

static void EngineSessionPassOnStartTurnSelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-START",
            "FX_Start",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player0,
            timing: EffectTiming.OnStartTurn),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only pass start-turn target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-START"] = CardEffectTestFixture.EffectDefinition("FX-START", "FX_Start");
    AddBattlePermanent(state, 6304, 903, "FX-START", PlayerId.Player1, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6305, 904, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new PassAction(PlayerId.Player0));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual(Phase.Active, state.Phase);
    AssertEqual(PlayerId.Player1, state.TurnPlayerId);
    AssertEqual(3, state.Memory);
    AssertEqual("test-selection:FX-START", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-START:selection:Destroy", paused.PendingStableContinuationId);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-START",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OnStartTurn, completed.CompletedTiming);
    AssertEqual(Phase.Active, state.Phase);
    AssertEqual(PlayerId.Player1, state.TurnPlayerId);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(target.TopCardId));
}

static void EngineSessionOnAllyAttackSelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-ATTACK",
            "FX_Attack",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnAllyAttack),
        new NoEffectCardScript("BT1-WEAK", notes: "Test-only attack defender."),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only attack trigger target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-ATTACK", "FX_Attack") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
    };
    var attacker = AddBattlePermanent(state, 6308, 907, "FX-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 6309, 908, "BT1-WEAK", PlayerId.Player1, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6310, 909, "BT1-ROOKIE", PlayerId.Player1, 1, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertTrue(attacker.IsSuspended);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == defender.Id));
    AssertEqual("test-selection:FX-ATTACK", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-ATTACK:selection:Destroy", paused.PendingStableContinuationId);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-ATTACK",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OnAllyAttack, completed.CompletedTiming);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.Id == attacker.Id));
}

static void EngineSessionOnEndAttackSelectionPauseResume()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-END-ATTACK",
            "FX_EndAttack",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnEndAttack),
        new NoEffectCardScript("BT1-WEAK", notes: "Test-only end-attack defender."),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only end-attack target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-END-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-END-ATTACK", "FX_EndAttack") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
    };
    var attacker = AddBattlePermanent(state, 6311, 910, "FX-END-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 6312, 911, "BT1-WEAK", PlayerId.Player1, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6313, 912, "BT1-ROOKIE", PlayerId.Player1, 1, enterTurn: 1);
    var session = services.CreateSession(state);

    var paused = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertTrue(attacker.IsSuspended);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));
    AssertEqual("test-selection:FX-END-ATTACK", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-END-ATTACK:selection:Destroy", paused.PendingStableContinuationId);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-END-ATTACK",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OnEndAttack, completed.CompletedTiming);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.Id == attacker.Id));
}

static void EngineSessionRulesTimingSelectionPauseResume()
{
    var services = CreateRulesTimingSelectionServices();
    var state = CreateRulesTimingSelectionScenarioState(out var playedCard, out var target);
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, playedCard, 1));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual(Zone.BattleArea, state.Cards[playedCard].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));
    AssertEqual("test-selection:FX-RULES", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("FX-RULES:selection:Destroy", paused.PendingStableContinuationId);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-RULES",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.RulesTiming, completed.CompletedTiming);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == playedCard));
}

static void EngineSessionSecuritySkillSelectionPauseResume()
{
    var services = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry());
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6320, 918, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6321, 919, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    var security = AddCardToZone(state, 6322, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var session = services.CreateSession(state);

    var paused = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(paused.IsPaused);
    AssertFalse(paused.RulesProcessed);
    AssertEqual("ST1-16:option:delete", paused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("ST1-16:option:delete-selection", paused.PendingStableContinuationId);
    AssertFalse(state.GetPlayer(PlayerId.Player1).Security.Contains(security));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.Executing, state.Cards[security].CurrentZone);
    AssertTrue(state.Cards[security].IsFaceUp);
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertEqual(EffectTiming.OptionSkill, completed.CompletedTiming);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.Trash, state.Cards[security].CurrentZone);
}

static void EngineSessionChainedSelectionPauseResume()
{
    var services = BattleEngineServices.Create(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());
    var state = CreateSt2St3ScenarioState();
    state.Memory = 10;
    var option = AddCardToZone(state, 6292, "ST2-15", PlayerId.Player0, Zone.Hand);
    var host = AddBattlePermanent(state, 6293, 893, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 6294, "ST2-03", PlayerId.Player0, host.Id);
    var session = services.CreateSession(state);

    var firstPaused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));

    AssertTrue(firstPaused.IsPaused);
    AssertEqual("ST2-15:option:source-host", firstPaused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual("ST2-15:option:select-source-host", firstPaused.PendingStableContinuationId);

    var secondPaused = ResumeWithDecision(session, firstPaused, SelectionResult.ForTargets(
        "ST2-15:option:source-host",
        new[] { PermanentSelectionTarget(host) }));

    AssertTrue(secondPaused.IsPaused);
    AssertEqual($"ST2-15:option:source-card:{host.Id.Value}", secondPaused.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual($"ST2-15:option:play-source-card:{host.Id.Value}", secondPaused.PendingStableContinuationId);
    AssertEqual(Zone.Executing, state.Cards[option].CurrentZone);

    var completed = ResumeWithDecision(session, secondPaused, SelectionResult.ForTargets(
        $"ST2-15:option:source-card:{host.Id.Value}",
        new[] { CardSelectionTarget(source, PlayerId.Player0, Zone.EvolutionSources, host.Id) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(completed.RulesProcessed);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertFalse(host.SourceCardIds.Contains(source));
    AssertTrue(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == source));
}

static void EngineSessionValidatesPendingResumeBoundary()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var session = services.CreateSession(state);

    AssertThrows<DomainException>(() => session.Resume(new DecisionResult(
        PlayerId.Player0,
        new DecisionToken(1),
        SelectionResult.Skip("missing"))));

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    AssertTrue(paused.IsPaused);

    AssertThrows<DomainException>(() => session.Resume(new DecisionResult(
        PlayerId.Player1,
        paused.PendingDecisionToken!.Value,
        SelectionResult.Skip("test-selection:FX-SELECT"))));
    AssertThrows<DomainException>(() => session.Resume(new DecisionResult(
        PlayerId.Player0,
        paused.PendingDecisionToken!.Value,
        SelectionResult.Skip("wrong-request"))));
}

static void DirectSynchronousPendingCallsRollbackState()
{
    var playServices = CreatePendingOptionServices();
    var playState = CreatePendingOptionScenarioState(out var option);
    AssertPendingSynchronousCallRollsBack(
        playState,
        (state, trace) => playServices.PlayCardService.Play(
            state,
            new PlayCardAction(PlayerId.Player0, option, -1),
            trace));

    var digivolveServices = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-DIGI",
            "FX_Digi",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.WhenDigivolving),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only direct pending digivolution source host.")));
    var digivolveState = CreateMinimalBattleState();
    digivolveState.CardDefinitions["FX-DIGI"] = CardEffectTestFixture.EffectDefinition("FX-DIGI", "FX_Digi") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
        EvoCosts = new[] { new EvoCostDefinition(CardColor.Red, 3, 2) },
    };
    digivolveState.Memory = 5;
    var host = AddBattlePermanent(digivolveState, 6360, 960, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var digivolveCard = AddCardToZone(digivolveState, 6361, "FX-DIGI", PlayerId.Player0, Zone.Hand);
    AddBattlePermanent(digivolveState, 6362, 961, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    AssertPendingSynchronousCallRollsBack(
        digivolveState,
        (state, trace) => digivolveServices.DigivolveService.Digivolve(
            state,
            new DigivolveAction(PlayerId.Player0, digivolveCard, host.Id),
            trace));

    var attackServices = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-ATTACK",
            "FX_Attack",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnAllyAttack),
        new NoEffectCardScript("BT1-WEAK", notes: "Test-only direct pending attack defender."),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only direct pending attack target.")));
    var attackState = CreateMinimalBattleState();
    attackState.CardDefinitions["FX-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-ATTACK", "FX_Attack") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
    };
    var attacker = AddBattlePermanent(attackState, 6363, 962, "FX-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(attackState, 6364, 963, "BT1-WEAK", PlayerId.Player1, 0, enterTurn: 1);
    AddBattlePermanent(attackState, 6365, 964, "BT1-ROOKIE", PlayerId.Player1, 1, enterTurn: 1);
    AssertPendingSynchronousCallRollsBack(
        attackState,
        (state, trace) => attackServices.AttackService.Attack(
            state,
            new AttackAction(PlayerId.Player0, attacker.Id, defender.Id),
            trace));

    var securityServices = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry());
    var securityState = CreateSt1ScenarioState();
    var securityAttacker = AddBattlePermanent(securityState, 6366, 965, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddBattlePermanent(securityState, 6367, 966, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    AddCardToZone(securityState, 6368, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    AssertPendingSynchronousCallRollsBack(
        securityState,
        (state, trace) => securityServices.SecurityCheckService.CheckSecurity(
            state,
            securityAttacker.Id,
            PlayerId.Player1,
            trace));
}

static void EngineSessionActionAndSelectionReplayDeterministic()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var initial = state.Clone();
    var target = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-SELECT",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionPhaseSelectionReplayDeterministic()
{
    var services = CreatePhaseSelectionServices();
    var state = CreatePhaseSelectionScenarioState(out var target);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var paused = session.RunMainPhase();
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-PHASE",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionPassSelectionReplayDeterministic()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-START",
            "FX_Start",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player0,
            timing: EffectTiming.OnStartTurn),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only pass replay target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-START"] = CardEffectTestFixture.EffectDefinition("FX-START", "FX_Start");
    AddBattlePermanent(state, 6306, 905, "FX-START", PlayerId.Player1, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6307, 906, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var paused = session.Step(new PassAction(PlayerId.Player0));
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-START",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionAttackSelectionReplayDeterministic()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-ATTACK",
            "FX_Attack",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnAllyAttack),
        new NoEffectCardScript("BT1-WEAK", notes: "Test-only attack replay defender."),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only attack replay target.")));
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-ATTACK"] = CardEffectTestFixture.EffectDefinition("FX-ATTACK", "FX_Attack") with
    {
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
    };
    var attacker = AddBattlePermanent(state, 6314, 913, "FX-ATTACK", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 6315, 914, "BT1-WEAK", PlayerId.Player1, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6316, 915, "BT1-ROOKIE", PlayerId.Player1, 1, enterTurn: 1);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var paused = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-ATTACK",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionRulesTimingSelectionReplayDeterministic()
{
    var services = CreateRulesTimingSelectionServices();
    var state = CreateRulesTimingSelectionScenarioState(out var playedCard, out var target);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, playedCard, 1));
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-RULES",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionSecuritySelectionReplayDeterministic()
{
    var services = BattleEngineServices.Create(St1CardScriptCatalog.CreateRegistry());
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6323, 920, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 6324, 921, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    AddCardToZone(state, 6325, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var paused = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, null));
    AssertTrue(paused.IsPaused);
    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void EngineSessionOptionalYesThenTargetSelectionResolves()
{
    var services = CreateOptionalSelectionServices();
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out var option, out var target);
    var session = services.CreateSession(state);

    var optional = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));

    AssertTrue(optional.IsPaused);
    AssertEqual("optional:FX-OPT:optional-target:OptionSkill", optional.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual(optional.PendingDecisionToken, optional.PendingDecisionPoint.Token);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));

    var targetDecision = ResumeWithDecision(session, optional, SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        true));

    AssertTrue(targetDecision.IsPaused);
    AssertEqual("test-target:FX-OPT", targetDecision.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertNotEqual(optional.PendingDecisionToken!.Value.Value, targetDecision.PendingDecisionToken!.Value.Value);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));

    var completed = ResumeWithDecision(session, targetDecision, SelectionResult.ForTargets(
        "test-target:FX-OPT",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
}

static void EngineSessionOptionalNoSkipsAndDrainsQueueTail()
{
    var services = CreateOptionalSelectionServices(tailMemory: 2);
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out var option, out var target);
    var session = services.CreateSession(state);

    var optional = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    AssertTrue(optional.IsPaused);

    var completed = ResumeWithDecision(session, optional, SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        false));

    AssertFalse(completed.IsPaused);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(option));
    AssertEqual(6, state.Memory);
}

static void EngineSessionSecurityOptionalThenTargetSelection()
{
    var services = CreateOptionalSelectionServices(includeSecurityActivation: true);
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out _, out var target);
    var security = AddCardToZone(state, 6330, "FX-OPT", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var attacker = AddBattlePermanent(state, 6331, 930, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    var session = services.CreateSession(state);

    var optional = session.Step(new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(optional.IsPaused);
    AssertEqual("optional:FX-OPT:optional-target:OptionSkill", optional.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertEqual(Zone.Executing, state.Cards[security].CurrentZone);

    var targetDecision = ResumeWithDecision(session, optional, SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        true));

    AssertTrue(targetDecision.IsPaused);
    AssertEqual("test-target:FX-OPT", targetDecision.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == target.Id));

    var completed = ResumeWithDecision(session, targetDecision, SelectionResult.ForTargets(
        "test-target:FX-OPT",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
}

static void EngineSessionStaleDecisionTokenFails()
{
    var services = CreatePendingOptionServices();
    var state = CreatePendingOptionScenarioState(out var option);
    var target = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var session = services.CreateSession(state);

    var paused = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    var staleToken = new DecisionToken(paused.PendingDecisionToken!.Value.Value + 1);

    AssertThrows<DomainException>(() => session.Resume(new DecisionResult(
        PlayerId.Player0,
        staleToken,
        SelectionResult.ForTargets(
            "test-selection:FX-SELECT",
            new[] { PermanentSelectionTarget(target) }))));
}

static void EngineSessionSelectionThenRulesTimingPausesAgain()
{
    var services = BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1),
        new SelectionPrimitiveCardScript(
            "FX-RULES",
            "FX_Rules",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.RulesTiming),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only rules timing target.")));
    var state = CreatePendingOptionScenarioState(out var option);
    state.CardDefinitions["FX-RULES"] = CardEffectTestFixture.EffectDefinition("FX-RULES", "FX_Rules");
    AddBattlePermanent(state, 6332, 932, "FX-RULES", PlayerId.Player0, 0, enterTurn: 1);
    var firstTarget = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single();
    var secondTarget = AddBattlePermanent(state, 6333, 933, "BT1-ROOKIE", PlayerId.Player1, 1, enterTurn: 1);
    var session = services.CreateSession(state);

    var first = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    AssertTrue(first.IsPaused);

    var second = ResumeWithDecision(session, first, SelectionResult.ForTargets(
        "test-selection:FX-SELECT",
        new[] { PermanentSelectionTarget(firstTarget) }));

    AssertTrue(second.IsPaused);
    AssertEqual("test-selection:FX-RULES", second.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(firstTarget.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.Id == secondTarget.Id));

    var completed = ResumeWithDecision(session, second, SelectionResult.ForTargets(
        "test-selection:FX-RULES",
        new[] { PermanentSelectionTarget(secondTarget) }));

    AssertFalse(completed.IsPaused);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(secondTarget.TopCardId));
}

static void EngineSessionRunToMainPhaseOnStartTurnPauseResume()
{
    var services = CreateStartTurnSelectionServices();
    var state = CreateStartTurnSelectionScenarioState(out var target);
    var session = services.CreateSession(state);

    var paused = session.RunToMainPhase();

    AssertTrue(paused.IsPaused);
    AssertEqual(Phase.Active, state.Phase);
    AssertEqual("test-selection:FX-START", paused.PendingDecisionPoint!.SelectionRequest!.Id);

    var completed = ResumeWithDecision(session, paused, SelectionResult.ForTargets(
        "test-selection:FX-START",
        new[] { PermanentSelectionTarget(target) }));

    AssertFalse(completed.IsPaused);
    AssertEqual(Phase.Main, state.Phase);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
}

static void ScriptedScenarioRunnerReturnsPhaseDecision()
{
    var services = CreateStartTurnSelectionServices();
    var state = CreateStartTurnSelectionScenarioState(out _);
    var scenario = new ScriptedScenario(
        "phase-decision",
        state,
        new ScriptedScenarioStep[] { new RunToMainPhaseScenarioStep() });

    var result = new ScriptedScenarioRunner(services).Run(scenario);

    AssertEqual(ScenarioRunStatus.PausedForDecision, result.Status);
    AssertEqual("test-selection:FX-START", result.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertTrue(result.PendingDecisionToken is not null);
}

static void RandomLegalActionRunnerReturnsPhaseDecision()
{
    var services = CreateStartTurnSelectionServices();
    var state = CreateStartTurnSelectionScenarioState(out _);

    var result = new RandomLegalActionRunner(services).Run(new RandomLegalActionRunRequest(
        "phase-decision",
        state,
        MaxActions: 1,
        Seed: 2));

    AssertEqual(ScenarioRunStatus.PausedForDecision, result.Status);
    AssertEqual("test-selection:FX-START", result.PendingDecisionPoint!.SelectionRequest!.Id);
    AssertTrue(result.PendingDecisionToken is not null);
}

static void EngineSessionChainedDecisionReplayDeterministic()
{
    var services = CreateOptionalSelectionServices();
    var state = CreateOptionalSelectionScenarioState("FX-OPT", out var option, out var target);
    var initial = state.Clone();
    var session = services.CreateSession(state);

    var optional = session.Step(new PlayCardAction(PlayerId.Player0, option, -1));
    AssertTrue(optional.IsPaused);
    var targetDecision = ResumeWithDecision(session, optional, SelectionResult.ForBoolean(
        "optional:FX-OPT:optional-target:OptionSkill",
        true));
    AssertTrue(targetDecision.IsPaused);
    var completed = ResumeWithDecision(session, targetDecision, SelectionResult.ForTargets(
        "test-target:FX-OPT",
        new[] { PermanentSelectionTarget(target) }));
    AssertFalse(completed.IsPaused);

    var replay = new ReplayRunner(services: services).Replay(initial, session.Trace);

    AssertEqual(state.ComputeStateHash(), replay.FinalState.ComputeStateHash());
    AssertTrue(replay.InvariantReport.IsValid);
}

static void OptionLifecycleSt1HandOptionRegression()
{
    var registry = St1CardScriptCatalog.CreateRegistry();

    var shadowWingState = CreateSt1ScenarioState();
    shadowWingState.Memory = 10;
    var shadowWing = AddCardToZone(shadowWingState, 6271, "ST1-13", PlayerId.Player0, Zone.Hand);
    var shadowTarget = AddBattlePermanent(shadowWingState, 6272, 872, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var shadowProvider = new TestDecisionProvider();
    shadowProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-13:option:dp",
        new[] { PermanentSelectionTarget(shadowTarget) }));
    var shadowResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry, shadowProvider))
        .PlayOptionFromHand(shadowWingState, new PlayCardAction(PlayerId.Player0, shadowWing, -1));
    AssertTrue(shadowResult.MovedToTrash);
    AssertEqual(7000, EffectiveStatService.NoContinuous.Dp(shadowWingState, shadowTarget));
    AssertTrue(shadowWingState.GetPlayer(PlayerId.Player0).Trash.Contains(shadowWing));

    var starlightState = CreateSt1ScenarioState();
    starlightState.Memory = 10;
    var starlight = AddCardToZone(starlightState, 6273, "ST1-14", PlayerId.Player0, Zone.Hand);
    var starlightResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry))
        .PlayOptionFromHand(starlightState, new PlayCardAction(PlayerId.Player0, starlight, -1));
    AssertTrue(starlightResult.MovedToTrash);
    AssertTrue(starlightState.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.SecurityDigimonDP
        && modifier.Amount == 7000
        && modifier.DurationScope == DurationScope.UntilOpponentTurnEnd));
    AssertTrue(starlightState.GetPlayer(PlayerId.Player0).Trash.Contains(starlight));

    var gigaState = CreateSt1ScenarioState();
    gigaState.Memory = 10;
    var giga = AddCardToZone(gigaState, 6274, "ST1-15", PlayerId.Player0, Zone.Hand);
    var smallTarget = AddBattlePermanent(gigaState, 6275, 875, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var gigaProvider = new TestDecisionProvider();
    gigaProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-15:option:delete",
        new[] { PermanentSelectionTarget(smallTarget) }));
    var gigaResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry, gigaProvider))
        .PlayOptionFromHand(gigaState, new PlayCardAction(PlayerId.Player0, giga, -1));
    AssertTrue(gigaResult.MovedToTrash);
    AssertTrue(gigaState.GetPlayer(PlayerId.Player1).Trash.Contains(smallTarget.TopCardId));
    AssertTrue(gigaState.GetPlayer(PlayerId.Player0).Trash.Contains(giga));

    var gaiaState = CreateSt1ScenarioState();
    gaiaState.Memory = 10;
    var gaia = AddCardToZone(gaiaState, 6276, "ST1-16", PlayerId.Player0, Zone.Hand);
    var largeTarget = AddBattlePermanent(gaiaState, 6277, 877, "ST1-06", PlayerId.Player1, 0, enterTurn: 1);
    var gaiaProvider = new TestDecisionProvider();
    gaiaProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(largeTarget) }));
    var gaiaResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry, gaiaProvider))
        .PlayOptionFromHand(gaiaState, new PlayCardAction(PlayerId.Player0, gaia, -1));
    AssertTrue(gaiaResult.MovedToTrash);
    AssertTrue(gaiaState.GetPlayer(PlayerId.Player1).Trash.Contains(largeTarget.TopCardId));
    AssertTrue(gaiaState.GetPlayer(PlayerId.Player0).Trash.Contains(gaia));
}

static void OptionLifecycleSt2St3HandOptionRegression()
{
    var registry = St2St3CardScriptCatalog.CreateCombinedWithSt1Registry();

    var hammerSparkState = CreateSt2St3ScenarioState();
    hammerSparkState.Memory = 5;
    var hammerSpark = AddCardToZone(hammerSparkState, 6281, "ST2-13", PlayerId.Player0, Zone.Hand);
    var hammerResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry))
        .PlayOptionFromHand(hammerSparkState, new PlayCardAction(PlayerId.Player0, hammerSpark, -1));
    AssertTrue(hammerResult.MovedToTrash);
    AssertEqual(5, hammerSparkState.Memory);
    AssertTrue(hammerSparkState.GetPlayer(PlayerId.Player0).Trash.Contains(hammerSpark));

    var sourcePlayState = CreateSt2St3ScenarioState();
    sourcePlayState.Memory = 10;
    var sourcePlayOption = AddCardToZone(sourcePlayState, 6282, "ST2-15", PlayerId.Player0, Zone.Hand);
    var host = AddBattlePermanent(sourcePlayState, 6283, 883, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(sourcePlayState, 6284, "ST2-03", PlayerId.Player0, host.Id);
    var sourcePlaySession = BattleEngineServices.Create(registry).CreateSession(sourcePlayState);

    var firstPending = sourcePlaySession.Step(new PlayCardAction(PlayerId.Player0, sourcePlayOption, -1));
    AssertTrue(firstPending.IsPaused);
    AssertEqual(Zone.Executing, sourcePlayState.Cards[sourcePlayOption].CurrentZone);

    var secondPending = ResumeWithDecision(sourcePlaySession, firstPending, SelectionResult.ForTargets(
            "ST2-15:option:source-host",
            new[] { PermanentSelectionTarget(host) }));
    AssertTrue(secondPending.IsPaused);
    AssertEqual(Zone.Executing, sourcePlayState.Cards[sourcePlayOption].CurrentZone);

    var completed = ResumeWithDecision(sourcePlaySession, secondPending, SelectionResult.ForTargets(
            $"ST2-15:option:source-card:{host.Id.Value}",
            new[] { CardSelectionTarget(source, PlayerId.Player0, Zone.EvolutionSources, host.Id) }));
    AssertFalse(completed.IsPaused);
    AssertTrue(sourcePlayState.GetPlayer(PlayerId.Player0).Trash.Contains(sourcePlayOption));
    AssertFalse(host.SourceCardIds.Contains(source));
    AssertTrue(sourcePlayState.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == source));

    var holyFlameState = CreateSt2St3ScenarioState();
    holyFlameState.Memory = 5;
    var holyFlame = AddCardToZone(holyFlameState, 6285, "ST3-13", PlayerId.Player0, Zone.Hand);
    var holyTarget = AddBattlePermanent(holyFlameState, 6286, 886, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var holyProvider = new TestDecisionProvider();
    holyProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-13:option:dp",
        new[] { PermanentSelectionTarget(holyTarget) }));
    var holyResult = new PlayCardService(
        triggerPipelineService: new TriggerPipelineService(registry, holyProvider))
        .PlayOptionFromHand(holyFlameState, new PlayCardAction(PlayerId.Player0, holyFlame, -1));
    AssertTrue(holyResult.MovedToTrash);
    AssertEqual(9000, EffectiveStatService.NoContinuous.Dp(holyFlameState, holyTarget));
    AssertTrue(holyFlameState.GetPlayer(PlayerId.Player0).Trash.Contains(holyFlame));
}

static void TriggerPipelineSt1OnBlockAnyoneHook()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 6131, 731, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var blocker = AddBattlePermanent(state, 6132, 732, "ST1-10", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 6133, "ST1-09", PlayerId.Player0, blocker.Id);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());

    var result = new AttackService(triggerPipelineService: pipeline).RunBlockTrigger(state, blocker.Id, attacker.Id);

    AssertEqual(1, result.ExecutedEffects.Count);
    AssertEqual(3, state.Memory);
}

static void TriggerPipelineSt1CompletionReportHasNoPartials()
{
    var report = new TargetCardPoolValidator().Validate(new TargetCardPoolValidationRequest(
        "ST1",
        CreateSt1TargetDecks(),
        CreateSt1CardDatabase(),
        St1CardScriptCatalog.CreateRegistry()));

    AssertTrue(report.IsValid);
    AssertEqual(0, report.UnsupportedCards.Count);
    AssertEqual(0, report.PartiallyImplementedCards.Count);
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-08"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-13"));
    AssertFalse(report.UnsupportedCards.Any(status => status.CardId == "ST1-14"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-12"));
    AssertFalse(report.MissingLayers.Contains("continuous-effect"));
    AssertFalse(report.MissingLayers.Contains("selection-result-application"));
    AssertFalse(report.MissingLayers.Contains("security-option-execution"));
    AssertFalse(report.MissingLayers.Contains("full-trigger-pipeline"));
    AssertFalse(report.MissingLayers.Contains("duration-cleanup"));
}

static void Tier1OptionalEffectCreatesSelectionRequest()
{
    var state = CreateMinimalBattleState();
    var collector = new TriggerCollector();
    collector.Register(new EffectDescriptor("optional:on-draw", EffectTiming.OnDraw, Controller: PlayerId.Player0, IsOptional: true));

    var collected = collector.Collect(new EffectContext(state, EffectTiming.OnDraw, PlayerId.Player0));
    var request = collected.QueuedEffects[0].OptionalSelectionRequest;

    AssertTrue(request is not null);
    AssertEqual(SelectionKind.SelectYesNo, request!.Kind);
    AssertEqual(2, request.Candidates.Count);
    AssertEqual(PlayerId.Player0, request.Player);
}

static void Tier1OncePerTurnTrackerPreventsDuplicateUse()
{
    var state = CreateMinimalBattleState();
    var tracker = new OncePerTurnTracker();
    var source = new CardInstanceId(2091);

    AssertTrue(tracker.CanUse(state, "effect:once", PlayerId.Player0, source));
    tracker.RegisterUse(state, "effect:once", PlayerId.Player0, source);
    AssertFalse(tracker.CanUse(state, "effect:once", PlayerId.Player0, source));
    AssertThrows<DomainException>(() => tracker.RegisterUse(state, "effect:once", PlayerId.Player0, source));

    state.TurnCount++;
    AssertTrue(tracker.CanUse(state, "effect:once", PlayerId.Player0, source));
}

static void Tier1UnsupportedPrimitiveFailsExplicitly()
{
    var state = CreateMinimalBattleState();
    var option = AddCardToZone(state, 2101, "BT1-OPTION", PlayerId.Player0, Zone.Hand);

    AssertThrows<UnsupportedMechanicException>(() =>
        new Tier1PrimitiveService().PlayWithoutPayingCost(state, PlayerId.Player0, option, Zone.Hand, 0));
}

static void CardEffectFoundationRegistryLookup()
{
    var noEffect = new NoEffectCardScript("FX-NO");
    var byClass = new NoEffectCardScript("FX-CLASS", "FX_Class");
    var registry = CardEffectTestFixture.Registry(noEffect, byClass);

    AssertTrue(registry.TryGetScript(CardEffectTestFixture.NoEffectDefinition("FX-NO"), out var foundByCard));
    AssertEqual(CardEffectPortingStatus.NoEffect, foundByCard.Porting.Status);

    var definition = CardEffectTestFixture.EffectDefinition("FX-OTHER", "FX_Class");
    AssertTrue(registry.TryGetScript(definition, out var foundByClass));
    AssertEqual("FX-CLASS", foundByClass.Porting.CardId);
    AssertEqual("FX_Class", foundByClass.Porting.EffectClassName);
}

static void CardEffectFoundationUnsupportedScriptFails()
{
    var state = CreateMinimalBattleState();
    var source = AddCardToZone(state, 3901, "BT1-OPTION", PlayerId.Player0, Zone.Hand);
    var definition = CardEffectTestFixture.EffectDefinition("FX-MISSING", "FX_Missing");
    var script = new CardScriptRegistry().GetScript(definition);
    var resolution = CardEffectTestFixture.Resolution(state, EffectTiming.OnDraw, PlayerId.Player0, source);

    AssertEqual(CardEffectPortingStatus.Unsupported, script.Porting.Status);
    AssertThrows<UnsupportedMechanicException>(() => script.CreateEffectDescriptors(new CardScriptContext(state, source)));
    AssertThrows<UnsupportedMechanicException>(() =>
        script.Resolve(new CardScriptExecutionContext(state, resolution, new Tier1PrimitiveService())));
}

static void CardEffectFoundationDeckValidationCatchesUnsupported()
{
    var definition = CardEffectTestFixture.EffectDefinition(
        "FX-DECOY",
        "",
        battleKeywords: new[] { BattleKeyword.Decoy });
    var registry = CardEffectTestFixture.Registry(new NoEffectCardScript("FX-DECOY"));
    var validator = new DeckMechanicValidator(registry);

    var report = validator.ValidateDecks(
        new[] { CardEffectTestFixture.Deck(PlayerId.Player0, "FX-DECOY") },
        CardEffectTestFixture.Database(definition));

    AssertFalse(report.IsValid);
    AssertTrue(report.Errors.Any(error => error.Contains("BattleKeyword:Decoy", StringComparison.Ordinal)));
}

static void CardEffectFoundationNoEffectMustBeExplicit()
{
    var definition = CardEffectTestFixture.NoEffectDefinition("FX-EMPTY");
    var database = CardEffectTestFixture.Database(definition);
    var deck = new[] { CardEffectTestFixture.Deck(PlayerId.Player0, "FX-EMPTY") };

    var missingReport = new DeckMechanicValidator(new CardScriptRegistry()).ValidateDecks(deck, database);
    AssertFalse(missingReport.IsValid);
    AssertTrue(missingReport.Errors.Any(error => error.Contains("No-effect cards must be explicitly registered", StringComparison.Ordinal)));

    var explicitReport = new DeckMechanicValidator(
        CardEffectTestFixture.Registry(new NoEffectCardScript("FX-EMPTY"))).ValidateDecks(deck, database);
    AssertTrue(explicitReport.IsValid);
}

static void CardEffectFoundationPrimitiveServiceBoundary()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-DRAW"] = CardEffectTestFixture.EffectDefinition("FX-DRAW", "FX_Draw");
    var source = AddCardToZone(state, 3911, "FX-DRAW", PlayerId.Player0, Zone.Hand);
    var drawCard = AddCardToZone(state, 3912, "BT1-OPTION", PlayerId.Player0, Zone.Deck);
    var script = new PrimitiveDrawCardScript("FX-DRAW", "FX_Draw");
    var resolution = CardEffectTestFixture.Resolution(state, EffectTiming.OnDraw, PlayerId.Player0, source);
    var context = new CardScriptExecutionContext(state, resolution, new Tier1PrimitiveService());

    AssertFalse(typeof(CardScriptExecutionContext).GetProperties().Any(property => property.PropertyType == typeof(GameState)));
    AssertTrue(typeof(CardScriptExecutionContext).GetProperties().Any(property => property.PropertyType == typeof(Tier1PrimitiveService)));

    script.Resolve(context);

    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(drawCard));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Deck.Contains(drawCard));
}

static void St1CardEffectCatalogCoversTargetDecklist()
{
    var records = St1CardScriptCatalog.CreateRegistry().PortingRecords.ToArray();
    var targetIds = St1TargetCardIds();

    AssertEqual(16, targetIds.Length);
    AssertEqual(16, records.Length);

    foreach (var cardId in targetIds)
    {
        AssertTrue(records.Any(record => record.CardId == cardId));
    }

    AssertEqual(CardEffectPortingStatus.NoEffect, records.Single(record => record.CardId == "ST1-02").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-01").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-03").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-06").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-07").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-08").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-09").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-11").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-12").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-13").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-14").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-15").Status);
    AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == "ST1-16").Status);
}

static void St1CardEffectDeckValidationPasses()
{
    var database = CreateSt1CardDatabase();
    var registry = St1CardScriptCatalog.CreateRegistry();
    var report = new DeckMechanicValidator(registry).ValidateDecks(CreateSt1TargetDecks(), database);

    AssertTrue(report.IsValid);
    AssertEmpty(report.Errors.ToArray());

    foreach (var cardId in St1UnsupportedCardIds())
    {
        AssertTrue(report.Errors.Any(error => error.Contains(cardId, StringComparison.Ordinal)));
    }

    foreach (var cardId in St1PartiallyImplementedCardIds())
    {
        AssertTrue(report.Errors.Any(error => error.Contains(cardId, StringComparison.Ordinal)));
    }

    foreach (var cardId in new[]
    {
        "ST1-01", "ST1-02", "ST1-03", "ST1-04", "ST1-05", "ST1-06", "ST1-07", "ST1-08",
        "ST1-09", "ST1-10", "ST1-11", "ST1-13", "ST1-14", "ST1-15", "ST1-16",
    })
    {
        AssertFalse(report.Errors.Any(error => error.Contains(cardId, StringComparison.Ordinal)));
    }
}

static void PortingStructureSt1RunnableRecordsHaveFiles()
{
    var records = St1CardScriptCatalog.CreateRegistry().PortingRecords
        .Where(record => record.Status is CardEffectPortingStatus.Implemented or CardEffectPortingStatus.NoEffect)
        .ToArray();

    foreach (var record in records)
    {
        var path = RlSt1CardEffectPath(record.CardId);
        if (!File.Exists(path))
        {
            throw new InvalidOperationException($"Missing RL.Engine ST1 card file for {record.CardId}: {path}");
        }
    }
}

static void PortingStructureSt2RunnableRecordsHaveFiles() =>
    AssertRunnableRecordsHaveFiles(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST2-", StringComparison.Ordinal)));

static void PortingStructureSt3RunnableRecordsHaveFiles() =>
    AssertRunnableRecordsHaveFiles(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST3-", StringComparison.Ordinal)));

static void PortingStructureSt1SourceMappingDocumented()
{
    foreach (var record in St1CardScriptCatalog.CreateRegistry().PortingRecords)
    {
        var originalPath = OriginalSt1CardEffectPath(record.CardId);
        if (!File.Exists(originalPath))
        {
            continue;
        }

        var rlPath = RlSt1CardEffectPath(record.CardId);
        var content = File.ReadAllText(rlPath);
        var expected = $"DCGO/Assets/Scripts/CardEffect/ST1/Red/{St1EffectClassName(record.CardId)}.cs";
        if (!content.Contains(expected, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{record.CardId} file does not document source mapping '{expected}'.");
        }
    }
}

static void PortingStructureSt2SourceMappingDocumented() =>
    AssertSourceMappingDocumented(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST2-", StringComparison.Ordinal)));

static void PortingStructureSt3SourceMappingDocumented() =>
    AssertSourceMappingDocumented(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST3-", StringComparison.Ordinal)));

static void PortingStructureSt1NoEffectProvenanceDocumented()
{
    foreach (var record in St1CardScriptCatalog.CreateRegistry().PortingRecords
        .Where(record => record.Status == CardEffectPortingStatus.NoEffect))
    {
        var originalPath = OriginalSt1CardEffectPath(record.CardId);
        if (File.Exists(originalPath))
        {
            throw new InvalidOperationException($"{record.CardId} is registered as NoEffect but original CardEffect exists: {originalPath}");
        }

        var rlPath = RlSt1CardEffectPath(record.CardId);
        var content = File.ReadAllText(rlPath);
        if (!content.Contains("No original CardEffect source file exists", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{record.CardId} NoEffect file does not document the missing original CardEffect source.");
        }
    }
}

static void PortingStructureSt2NoEffectProvenanceDocumented() =>
    AssertNoEffectProvenanceDocumented(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST2-", StringComparison.Ordinal)));

static void PortingStructureSt3NoEffectProvenanceDocumented() =>
    AssertNoEffectProvenanceDocumented(
        St2St3CardScriptCatalog.CreateRegistry().PortingRecords.Where(record => record.CardId.StartsWith("ST3-", StringComparison.Ordinal)));

static void PortingStructureSt1CatalogIsRegistryOnly()
{
    AssertCatalogRegistryOnly("St1CardScriptCatalog.cs");
}

static void PortingStructureSt2St3CatalogIsRegistryOnly()
{
    AssertCatalogRegistryOnly("St2St3CardScriptCatalog.cs");
}

static void AssertCatalogRegistryOnly(string fileName)
{
    var catalogPath = Path.Combine(WorkspaceRoot(), "src", "DCGO.RL.Engine", "CardEffects", fileName);
    var content = File.ReadAllText(catalogPath);
    var bannedSnippets = new[]
    {
        "EffectDescriptor",
        "SelectionRequest",
        "SelectionResult",
        "SelectionContinuation",
        "CreateSelectionRequest",
        "Tier1PrimitiveService",
        "ZoneMover",
        "TemporaryModifier",
        "AddTemporary",
        ".Resolve(",
        "context.Primitives",
        ".Primitives.",
        "MoveCard",
        "Trash(",
        "Delete(",
        "Destroy(",
        "ModifyMemory(",
        "PlayWithoutPayingCost(",
        "ReturnPermanentToHand(",
    };

    foreach (var snippet in bannedSnippets)
    {
        if (content.Contains(snippet, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{fileName} contains non-registry logic snippet '{snippet}'.");
        }
    }
}

static void PortingStructureSt1StatusTableMatchesRegistry()
{
    var table = LoadSt1PortingStatusTable();
    var records = St1CardScriptCatalog.CreateRegistry().PortingRecords.ToArray();

    AssertEqual(St1TargetCardIds().Length, table.Count);
    AssertEqual(St1TargetCardIds().Length, records.Length);

    foreach (var record in records)
    {
        if (!table.TryGetValue(record.CardId, out var status))
        {
            throw new InvalidOperationException($"CardEffect status table is missing {record.CardId}.");
        }

        AssertEqual(record.Status, status);
    }
}

static void PortingStructureSt1ToSt3StatusSnapshotMatchesRegistry()
{
    var table = LoadCurrentRegistryStatusSnapshot();
    var records = St1CardScriptCatalog.CreateRegistry().PortingRecords
        .Concat(St2St3CardScriptCatalog.CreateRegistry().PortingRecords)
        .ToArray();

    AssertEqual(St1TargetCardIds().Length + St2TargetCardIds().Length + St3TargetCardIds().Length, table.Count);
    AssertEqual(St1TargetCardIds().Length + St2TargetCardIds().Length + St3TargetCardIds().Length, records.Length);

    foreach (var record in records)
    {
        if (!table.TryGetValue(record.CardId, out var status))
        {
            throw new InvalidOperationException($"Current registry snapshot is missing {record.CardId}.");
        }

        AssertEqual(record.Status, status);
    }
}

static void PortingStructureSt1StatusTableMatchesFiles()
{
    var table = LoadSt1PortingStatusTable();

    foreach (var (cardId, status) in table)
    {
        if (status is not (CardEffectPortingStatus.Implemented or CardEffectPortingStatus.NoEffect))
        {
            continue;
        }

        var rlPath = RlSt1CardEffectPath(cardId);
        if (!File.Exists(rlPath))
        {
            throw new InvalidOperationException($"Status table marks {cardId} as {status}, but RL.Engine file is missing: {rlPath}");
        }
    }
}

static void PortingStructureSt1ToSt3StatusSnapshotMatchesFiles()
{
    var table = LoadCurrentRegistryStatusSnapshot();

    foreach (var (cardId, status) in table)
    {
        if (status is not (CardEffectPortingStatus.Implemented or CardEffectPortingStatus.NoEffect))
        {
            continue;
        }

        var rlPath = RlCardEffectPath(cardId);
        if (!File.Exists(rlPath))
        {
            throw new InvalidOperationException($"Current registry snapshot marks {cardId} as {status}, but RL.Engine file is missing: {rlPath}");
        }
    }
}

static void PortingStructureImplementedScriptsDeclareConcreteClassesInCardFiles()
{
    var scripts = St1CardScriptCatalog.CreateScripts()
        .Concat(St2St3CardScriptCatalog.CreateScripts())
        .Where(script => script.Porting.Status == CardEffectPortingStatus.Implemented)
        .ToArray();

    foreach (var script in scripts)
    {
        var path = RlCardEffectPath(script.Porting.CardId);
        var content = File.ReadAllText(path);
        var expectedDeclaration = $"class {script.GetType().Name}";
        if (!content.Contains(expectedDeclaration, StringComparison.Ordinal))
        {
            throw new InvalidOperationException(
                $"{script.Porting.CardId} implemented script '{script.GetType().Name}' must be declared in its card file: {Path.GetRelativePath(WorkspaceRoot(), path)}");
        }
    }
}

static void PortingStructureNoEffectFilesAreMarkerOnly()
{
    var records = St1CardScriptCatalog.CreateRegistry().PortingRecords
        .Concat(St2St3CardScriptCatalog.CreateRegistry().PortingRecords)
        .Where(record => record.Status == CardEffectPortingStatus.NoEffect)
        .ToArray();

    var bannedSnippets = new[]
    {
        "class ",
        "EffectDescriptor",
        "SelectionRequest",
        "SelectionContinuation",
        "Resolve(",
        "context.Primitives",
        "Tier1PrimitiveService",
        "ZoneMover",
        "TemporaryModifier",
        "CurrentZone =",
    };

    foreach (var record in records)
    {
        var path = RlCardEffectPath(record.CardId);
        var content = File.ReadAllText(path);
        if (!content.Contains("Source mapping:", StringComparison.Ordinal)
            || !content.Contains("NoEffect", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{record.CardId} NoEffect marker must document source mapping and explicit NoEffect status.");
        }

        foreach (var snippet in bannedSnippets)
        {
            if (content.Contains(snippet, StringComparison.Ordinal))
            {
                throw new InvalidOperationException($"{record.CardId} NoEffect marker contains implementation snippet '{snippet}': {Path.GetRelativePath(WorkspaceRoot(), path)}");
            }
        }
    }
}

static void PortingStructureNoEffectAssetConflictsAreDocumented()
{
    var policyPath = Path.Combine(WorkspaceRoot(), "docs", "rl-engine", "porting-structure-policy.md");
    var statusPath = Path.Combine(WorkspaceRoot(), "docs", "rl-engine", "cardeffect-porting-status.md");
    var policy = File.ReadAllText(policyPath);
    var status = File.ReadAllText(statusPath);

    foreach (var cardId in new[] { "ST2-07", "ST3-07", "ST3-02" })
    {
        if (!policy.Contains(cardId, StringComparison.Ordinal) || !status.Contains(cardId, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{cardId} NoEffect/source-mapping conflict must be documented in policy and status docs.");
        }
    }

    foreach (var shared in new[] { "ST2-07", "ST3-07" })
    {
        if (!policy.Contains("ST1_06", StringComparison.Ordinal) || !status.Contains("ST1_06", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{shared} shared ST1_06 mapping risk must be documented.");
        }
    }

    if (!policy.Contains("ST3_02", StringComparison.Ordinal) || !status.Contains("ST3_02", StringComparison.Ordinal))
    {
        throw new InvalidOperationException("ST3-02 variant ST3_02 asset conflict must be documented.");
    }
}

static void PortingStructureCardFilesAvoidDirectZoneMutation()
{
    var root = Path.Combine(WorkspaceRoot(), "src", "DCGO.RL.Engine", "CardEffects");
    var files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories);
    var bannedSnippets = new[]
    {
        ".Hand.Add(",
        ".Hand.Remove(",
        ".Deck.Add(",
        ".Deck.Remove(",
        ".Security.Add(",
        ".Security.Remove(",
        ".Trash.Add(",
        ".Trash.Remove(",
        ".DigiEggDeck.Add(",
        ".DigiEggDeck.Remove(",
        ".FieldPermanents.Add(",
        ".FieldPermanents.Remove(",
        ".CardsIn(",
        "CurrentZone =",
    };

    foreach (var file in files)
    {
        var content = File.ReadAllText(file);
        foreach (var snippet in bannedSnippets)
        {
            if (content.Contains(snippet, StringComparison.Ordinal))
            {
                throw new InvalidOperationException($"CardEffect file directly mutates zone state with '{snippet}': {Path.GetRelativePath(WorkspaceRoot(), file)}");
            }
        }
    }
}

static void PortingStructureStarterSetFilesUseOriginalLikePaths()
{
    var root = Path.Combine(WorkspaceRoot(), "src", "DCGO.RL.Engine", "CardEffects");
    AssertCardEffectSetPath(root, "ST2");
    AssertCardEffectSetPath(root, "ST3");
}

static void AssetRegistryMappingExactMapping()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-exact",
        new[] { Asset("FX-01", 1, "base", "FX_01") },
        CardEffectTestFixture.Registry(new PrimitiveDrawCardScript("FX-01", "FX_01")),
        StatusSnapshot: new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal)
        {
            ["FX-01"] = CardEffectPortingStatus.Implemented,
        },
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX-01"] = "// Source mapping: DCGO/Assets/Scripts/CardEffect/FX/FX_01.cs",
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX_01"] = "DCGO/Assets/Scripts/CardEffect/FX/FX_01.cs",
        }));

    AssertTrue(report.IsValid);
    AssertFalse(report.Issues.Any(issue => issue.Severity != AssetRegistryMappingSeverity.Info));
    AssertTrue(JsonSerializer.Serialize(report).Contains("\"TargetName\"", StringComparison.Ordinal));
    AssertTrue(report.ToKoreanSummaryLines().Any(line => line.Contains("대상", StringComparison.Ordinal)));
}

static void AssetRegistryMappingSharedEffectPreservesSourceMetadata()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-shared",
        new[] { Asset("ST2-07", 53, "base", "ST1_06") },
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(),
        StatusSnapshot: new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal)
        {
            ["ST2-07"] = CardEffectPortingStatus.Implemented,
        },
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["ST2-07"] = File.ReadAllText(RlCardEffectPath("ST2-07")),
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["ST1_06"] = "DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs",
        }));

    AssertTrue(report.IsValid);
    var card = report.Cards.Single();
    AssertEqual("ST2-07", card.DefinitionId.CardId);
    AssertEqual(string.Empty, card.RegistryEffectClassName);
    AssertEqual("ST1_06", card.SourceEffectClassName);
    AssertIssue(report, "RegistryAliasSeparatedFromSourceEffectClass", "ST2-07");
    AssertFalse(report.Issues.Any(issue => issue.Code == "FalseNoEffect"));
}

static void AssetRegistryMappingVariantSplitReportsSt3Two()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-variant",
        new[]
        {
            Asset("ST3-02", 76, "base", string.Empty),
            Asset("ST3-02", 77, "P1", string.Empty),
            Asset("ST3-02", 4977, "P2", "ST3_02"),
        },
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(),
        StatusSnapshot: new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal)
        {
            ["ST3-02"] = CardEffectPortingStatus.NoEffect,
        },
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["ST3-02"] = File.ReadAllText(RlCardEffectPath("ST3-02")),
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)));

    AssertFalse(report.IsValid);
    AssertEqual(3, report.Cards.Count);
    AssertIssue(report, "VariantEffectClassSplit", "ST3-02");
    AssertIssue(report, "FalseNoEffect", "ST3-02", 4977);
    AssertIssue(report, "MissingSourceEffectBody", "ST3-02", 4977);
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 76
        && card.DefinitionId.VariantKey == "base"
        && string.IsNullOrWhiteSpace(card.AssetEffectClassName)
        && card.PortingStatus == CardEffectPortingStatus.NoEffect));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 77
        && card.DefinitionId.VariantKey == "P1"
        && string.IsNullOrWhiteSpace(card.AssetEffectClassName)
        && card.PortingStatus == CardEffectPortingStatus.NoEffect));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 4977
        && card.DefinitionId.VariantKey == "P2"
        && card.AssetEffectClassName == "ST3_02"
        && !card.HasSourceEffectBody));
}

static void AssetRegistryMappingFalseNoEffectDetection()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-false-noeffect",
        new[] { Asset("FX-NO", 2, "base", "FX_No") },
        CardEffectTestFixture.Registry(new NoEffectCardScript("FX-NO")),
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX-NO"] = "// Source mapping: FX_No\n// NoEffect marker",
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX_No"] = "DCGO/Assets/Scripts/CardEffect/FX/FX_No.cs",
        }));

    AssertFalse(report.IsValid);
    AssertIssue(report, "FalseNoEffect", "FX-NO");
}

static void AssetRegistryMappingMissingPerCardFile()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-missing-file",
        new[] { Asset("FX-01", 3, "base", "FX_01") },
        CardEffectTestFixture.Registry(new PrimitiveDrawCardScript("FX-01", "FX_01")),
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX_01"] = "DCGO/Assets/Scripts/CardEffect/FX/FX_01.cs",
        }));

    AssertFalse(report.IsValid);
    AssertIssue(report, "MissingPerCardFile", "FX-01");
}

static void AssetRegistryMappingStaleStatusSnapshot()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-stale-status",
        new[] { Asset("FX-01", 4, "base", "FX_01") },
        CardEffectTestFixture.Registry(new PrimitiveDrawCardScript("FX-01", "FX_01")),
        StatusSnapshot: new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal)
        {
            ["FX-01"] = CardEffectPortingStatus.NoEffect,
        },
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX-01"] = "// Source mapping: DCGO/Assets/Scripts/CardEffect/FX/FX_01.cs",
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX_01"] = "DCGO/Assets/Scripts/CardEffect/FX/FX_01.cs",
        }));

    AssertFalse(report.IsValid);
    AssertIssue(report, "StaleStatusSnapshot", "FX-01");
}

static void AssetRegistryMappingUnregisteredEffect()
{
    var report = new AssetRegistryMappingValidator().Validate(new AssetRegistryMappingRequest(
        "fixture-unregistered",
        new[] { Asset("FX-MISS", 5, "base", "FX_Missing") },
        new CardScriptRegistry(),
        CardFileContentsByCardId: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX-MISS"] = "// Source mapping: DCGO/Assets/Scripts/CardEffect/FX/FX_Missing.cs",
        },
        SourceEffectRelativePathsByClass: new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["FX_Missing"] = "DCGO/Assets/Scripts/CardEffect/FX/FX_Missing.cs",
        }));

    AssertFalse(report.IsValid);
    AssertIssue(report, "MissingRegistryScript", "FX-MISS");
}

static void AssetRegistryMappingSourceUnavailableReport()
{
    var report = new AssetRegistryMappingValidator().ValidateLocalSource(new AssetRegistryLocalAuditRequest(
        "missing-source",
        WorkspaceRoot(),
        Path.Combine(WorkspaceRoot(), ".missing-dcgo-source"),
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry()));

    AssertFalse(report.IsValid);
    AssertEqual(AssetRegistrySourceStatus.SourceUnavailable, report.SourceStatus);
    AssertTrue(report.Issues.Any(issue => issue.Code == "SourceUnavailable"));
}

static void AssetRegistryMappingActualLocalDcgoAudit()
{
    var report = new AssetRegistryMappingValidator().ValidateLocalSource(new AssetRegistryLocalAuditRequest(
        "ST1-ST3 local DCGO asset audit",
        WorkspaceRoot(),
        Path.Combine(WorkspaceRoot(), "DCGO"),
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(),
        LoadCurrentRegistryStatusSnapshot()));

    AssertFalse(report.IsValid);
    AssertEqual(AssetRegistrySourceStatus.Available, report.SourceStatus);
    AssertTrue(report.Cards.Count >= 48);
    AssertIssue(report, "VariantEffectClassSplit", "ST3-02");
    AssertIssue(report, "FalseNoEffect", "ST3-02", 4977);
    AssertIssue(report, "MissingSourceEffectBody", "ST3-02", 4977);
    AssertFalse(report.Issues.Any(issue => issue.Severity == AssetRegistryMappingSeverity.Error));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 76
        && card.DefinitionId.VariantKey == "base"
        && string.IsNullOrWhiteSpace(card.AssetEffectClassName)
        && card.PortingStatus == CardEffectPortingStatus.NoEffect));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 77
        && card.DefinitionId.VariantKey == "P1"
        && string.IsNullOrWhiteSpace(card.AssetEffectClassName)
        && card.PortingStatus == CardEffectPortingStatus.NoEffect));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-02"
        && card.DefinitionId.CardIndex == 4977
        && card.DefinitionId.VariantKey == "P2"
        && card.AssetEffectClassName == "ST3_02"
        && !card.HasSourceEffectBody));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST2-07"
        && card.AssetEffectClassName == "ST1_06"
        && card.RegistryEffectClassName == string.Empty
        && card.SourceEffectClassName == "ST1_06"));
    AssertTrue(report.Cards.Any(card =>
        card.DefinitionId.CardId == "ST3-07"
        && card.AssetEffectClassName == "ST1_06"
        && card.RegistryEffectClassName == string.Empty
        && card.SourceEffectClassName == "ST1_06"));
    AssertTrue(JsonSerializer.Serialize(report).Contains("ST3_02", StringComparison.Ordinal));
    AssertTrue(report.ToKoreanSummaryLines().Any(line => line.Contains("blocking", StringComparison.Ordinal)));
}

static void St2St3CardEffectCatalogSkeletonCoversTargetPool()
{
    var records = St2St3CardScriptCatalog.CreateRegistry().PortingRecords.ToArray();
    var targetIds = St2TargetCardIds().Concat(St3TargetCardIds()).ToArray();

    AssertEqual(32, targetIds.Length);
    AssertEqual(32, records.Length);

    foreach (var cardId in targetIds)
    {
        AssertTrue(records.Any(record => record.CardId == cardId));
    }

    foreach (var cardId in St2St3NoEffectCardIds())
    {
        AssertEqual(CardEffectPortingStatus.NoEffect, records.Single(record => record.CardId == cardId).Status);
    }

    foreach (var cardId in St2St3UnsupportedCardIds())
    {
        AssertEqual(CardEffectPortingStatus.Unsupported, records.Single(record => record.CardId == cardId).Status);
    }

    foreach (var cardId in St2St3ImplementedCardIds())
    {
        AssertEqual(CardEffectPortingStatus.Implemented, records.Single(record => record.CardId == cardId).Status);
    }

    AssertFalse(records.Any(record => record.Status == CardEffectPortingStatus.PartiallyImplemented));
}

static void St1ToSt3TargetPoolValidationPasses()
{
    var report = new TargetCardPoolValidator().Validate(new TargetCardPoolValidationRequest(
        "ST1-ST3",
        CreateSt1ToSt3TargetDecks(),
        CreateSt1ToSt3CardDatabase(),
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry()));

    AssertTrue(report.IsValid);
    AssertTrue(report.DeckValidation.IsValid);
    AssertFalse(report.MissingScripts.Any());
    AssertEqual(0, report.UnsupportedCards.Count);
    AssertEqual(0, report.PartiallyImplementedCards.Count);

    foreach (var cardId in St1TargetCardIds())
    {
        AssertFalse(report.UnsupportedCards.Any(status => status.CardId == cardId));
    }

    foreach (var cardId in St2St3UnsupportedCardIds())
    {
        AssertTrue(report.UnsupportedCards.Any(status => status.CardId == cardId));
    }

    foreach (var cardId in St2St3NoEffectCardIds())
    {
        AssertFalse(report.UnsupportedCards.Any(status => status.CardId == cardId));
    }

    foreach (var cardId in St2St3ImplementedCardIds())
    {
        AssertFalse(report.UnsupportedCards.Any(status => status.CardId == cardId));
    }

    AssertFalse(report.MissingLayers.Contains("dp-zero-deletion-trigger"));
    AssertFalse(report.MissingLayers.Contains("digivolution-source-trash"));
    AssertFalse(report.MissingLayers.Contains("opponent-no-source-condition"));
    AssertFalse(report.MissingLayers.Contains("negative-security-attack-duration"));
    AssertFalse(report.MissingLayers.Contains("security-add-this-card-to-hand"));
    AssertFalse(report.MissingLayers.Contains("st2st3-card-body-wiring"));
    AssertFalse(report.MissingLayers.Contains("bounce-to-hand"));
    AssertFalse(report.MissingLayers.Contains("recovery-from-deck"));
    AssertFalse(report.MissingLayers.Contains("on-enter-field-when-digivolving-compat"));
    AssertFalse(report.MissingLayers.Contains("continuous-security-digimon-dp"));
    AssertFalse(report.MissingLayers.Contains("cannot-attack-block-duration"));
    AssertFalse(report.MissingLayers.Contains("evolution-source-card-play"));
}

static void St2St3SharedSt106MappingUsesCardIdScripts()
{
    var database = CreateSt1ToSt3CardDatabase();
    var registry = St2St3CardScriptCatalog.CreateCombinedWithSt1Registry();

    foreach (var cardId in new[] { "ST2-07", "ST3-07" })
    {
        var definition = database.GetByCardId(cardId);
        AssertEqual("ST1_06", definition.CardEffectClassName);
        AssertTrue(definition.BattleKeywords.Contains(BattleKeyword.Blocker));
        AssertTrue(registry.TryGetScript(definition, out var script));
        AssertEqual(cardId, script.Porting.CardId);
        AssertEqual(string.Empty, script.Porting.EffectClassName);
        AssertEqual("ST1_06", script.Porting.SourceEffectClassName);
        AssertEqual("ST1_06", script.Porting.EffectiveSourceEffectClassName);
        AssertEqual(CardEffectPortingStatus.Implemented, script.Porting.Status);
        AssertTrue(script.Porting.Notes.Contains("ST1_06", StringComparison.Ordinal));

        var state = CreateSt2St3ScenarioState();
        var source = AddBattlePermanent(state, 6101, 611, cardId, PlayerId.Player0, 0, enterTurn: 1);
        var result = new TriggerPipelineService(registry).Run(
            state,
            EffectTiming.OnAllyAttack,
            PlayerId.Player0,
            source.TopCardId,
            source.Id);

        AssertEqual(-2, state.Memory);
        AssertEqual(1, result.ExecutedEffects.Count);
        AssertEqual($"{cardId}:on-ally-attack:memory-minus-2", result.ExecutedEffects.Single().StableId);
    }
}

static void St2OneInheritedNoSourceDp()
{
    var state = CreateSt2St3ScenarioState();
    var host = AddBattlePermanent(state, 7101, 900, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 7102, "ST2-01", PlayerId.Player0, host.Id);
    var opponentNoSource = AddBattlePermanent(state, 7103, 901, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var stats = new EffectiveStatService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    AssertEqual(7000, stats.Dp(state, host));

    AddEvolutionSource(state, 7104, "ST3-03", PlayerId.Player1, opponentNoSource.Id);

    AssertEqual(6000, stats.Dp(state, host));
}

static void St2EightInheritedNoSourceSecurityAttack()
{
    var state = CreateSt2St3ScenarioState();
    var host = AddBattlePermanent(state, 7111, 910, "ST2-09", PlayerId.Player0, 0, enterTurn: 1);
    AddEvolutionSource(state, 7112, "ST2-08", PlayerId.Player0, host.Id);
    AddBattlePermanent(state, 7113, 911, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var keywords = new BattleKeywordService(new EffectiveStatService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry()));

    AssertEqual(2, keywords.SecurityAttackCount(state, host));

    state.TurnPlayerId = PlayerId.Player1;

    AssertEqual(1, keywords.SecurityAttackCount(state, host));
}

static void St2MattIshidaStartTurnMemoryAndSecurityPlay()
{
    var state = CreateSt2St3ScenarioState();
    AddBattlePermanent(state, 7121, 920, "ST2-12", PlayerId.Player0, 0, enterTurn: 1);
    AddBattlePermanent(state, 7122, 921, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var startTurnResult = pipeline.Run(state, EffectTiming.OnStartTurn, PlayerId.Player0);

    AssertEqual(1, startTurnResult.ExecutedEffects.Count);
    AssertEqual("ST2-12:on-start-turn:memory-plus-1", startTurnResult.ExecutedEffects[0].StableId);
    AssertEqual(1, state.Memory);

    var securityState = CreateSt2St3ScenarioState();
    var attacker = AddBattlePermanent(securityState, 7123, 922, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(securityState, 7124, "ST2-12", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var securityResult = CreateSt2St3SecurityCheckService(new TestDecisionProvider())
        .CheckSecurity(securityState, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual("ST2-12:security:play-self-tamer", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertFalse(securityState.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertFalse(securityState.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.BattleArea, securityState.Cards[security].CurrentZone);
    AssertTrue(securityState.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.TopCardId == security));
}

static void St2ThreeInheritedSourceTrashLevelCondition()
{
    var state = CreateSt2St3ScenarioState();
    var host = AddBattlePermanent(state, 7071, 870, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7072, "ST2-03", PlayerId.Player0, host.Id);
    var levelFive = AddBattlePermanent(state, 7073, 871, "ST3-08", PlayerId.Player1, 0, enterTurn: 1);
    var levelFiveTopSource = AddEvolutionSource(state, 7074, "ST3-03", PlayerId.Player1, levelFive.Id);
    var levelFiveBottomSource = AddEvolutionSource(state, 7075, "ST3-05", PlayerId.Player1, levelFive.Id);
    var levelSix = AddBattlePermanent(state, 7076, 872, "ST3-11", PlayerId.Player1, 1, enterTurn: 1);
    AddEvolutionSource(state, 7077, "ST3-06", PlayerId.Player1, levelSix.Id);
    var noSource = AddBattlePermanent(state, 7078, 873, "ST3-06", PlayerId.Player1, 2, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-03:on-ally-attack:source-trash",
        new[] { PermanentSelectionTarget(levelFive) }));

    var result = pipeline.Run(state, EffectTiming.OnAllyAttack, PlayerId.Player0, source, host.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(levelFiveBottomSource));
    AssertTrue(levelFive.SourceCardIds.Contains(levelFiveTopSource));
    AssertFalse(levelFive.SourceCardIds.Contains(levelFiveBottomSource));
    AssertEqual(Zone.Trash, state.Cards[levelFiveBottomSource].CurrentZone);
    AssertTrue(levelSix.SourceCardIds.Count == 1);
    AssertTrue(noSource.SourceCardIds.Count == 0);
}

static void St2SixInheritedSourceTrash()
{
    var state = CreateSt2St3ScenarioState();
    var host = AddBattlePermanent(state, 7081, 880, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7082, "ST2-06", PlayerId.Player0, host.Id);
    var target = AddBattlePermanent(state, 7083, 881, "ST3-11", PlayerId.Player1, 0, enterTurn: 1);
    var topSource = AddEvolutionSource(state, 7084, "ST3-03", PlayerId.Player1, target.Id);
    var bottomSource = AddEvolutionSource(state, 7085, "ST3-05", PlayerId.Player1, target.Id);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-06:on-ally-attack:source-trash",
        new[] { PermanentSelectionTarget(target) }));

    var result = pipeline.Run(state, EffectTiming.OnAllyAttack, PlayerId.Player0, source, host.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertTrue(target.SourceCardIds.Contains(topSource));
    AssertFalse(target.SourceCardIds.Contains(bottomSource));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(bottomSource));
}

static void St2NineWhenDigivolvingSourceTrash()
{
    var state = CreateSt2St3ScenarioState();
    var source = AddBattlePermanent(state, 7091, 890, "ST2-09", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 7092, 891, "ST3-11", PlayerId.Player1, 0, enterTurn: 1);
    var topSource = AddEvolutionSource(state, 7093, "ST3-03", PlayerId.Player1, target.Id);
    var middleSource = AddEvolutionSource(state, 7094, "ST3-05", PlayerId.Player1, target.Id);
    var bottomSource = AddEvolutionSource(state, 7095, "ST3-06", PlayerId.Player1, target.Id);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-09:when-digivolving:source-trash",
        new[] { PermanentSelectionTarget(target) }));

    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertSequence(new[] { topSource }, target.SourceCardIds.ToArray());
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(bottomSource));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(middleSource));
}

static void St2MetalGarurumonOnAllyAttackUnsuspendOncePerTurn()
{
    var state = CreateSt2St3ScenarioState();
    var attacker = AddBattlePermanent(state, 7131, 930, "ST2-11", PlayerId.Player0, 0, enterTurn: 1, isSuspended: true);
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var result = pipeline.Run(
        state,
        EffectTiming.OnAllyAttack,
        PlayerId.Player0,
        values: new Dictionary<string, object?>
        {
            ["Attacker"] = attacker.Id,
        });

    AssertEqual(1, result.ExecutedEffects.Count);
    AssertEqual("ST2-11:on-ally-attack:unsuspend-this-digimon", result.ExecutedEffects[0].StableId);
    AssertFalse(attacker.IsSuspended);

    attacker.IsSuspended = true;
    AssertThrows<DomainException>(() => pipeline.Run(
        state,
        EffectTiming.OnAllyAttack,
        PlayerId.Player0,
        values: new Dictionary<string, object?>
        {
            ["Attacker"] = attacker.Id,
        }));

    var attackState = CreateSt2St3ScenarioState();
    var attackServiceAttacker = AddBattlePermanent(attackState, 7132, 931, "ST2-11", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(attackState, 7133, "ST3-06", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var attackPipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    new AttackService(triggerPipelineService: attackPipeline).Attack(
        attackState,
        new AttackAction(PlayerId.Player0, attackServiceAttacker.Id, null));

    AssertFalse(attackServiceAttacker.IsSuspended);
}

static void St2HammerSparkMemoryOptionAndSecurity()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7001, "ST2-13", PlayerId.Player0, Zone.Executing);
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.ExecutedEffects.Count);
    AssertEqual(1, state.Memory);

    var attacker = AddBattlePermanent(state, 7002, 800, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 7003, "ST2-13", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var securityResult = CreateSt2St3SecurityCheckService(new TestDecisionProvider())
        .CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEqual(1, securityResult.SecurityEffectResults.Count);
    AssertEqual("ST2-13:security:memory-plus-2", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertEqual(-1, state.Memory);
}

static void St2HowlingBlasterAttackBlockRestrictionOptionAndSecurity()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7141, "ST2-14", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 7142, 940, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    target.BattleKeywords.Add(BattleKeyword.Blocker);
    var sourcedTarget = AddBattlePermanent(state, 7143, 941, "ST3-06", PlayerId.Player1, 1, enterTurn: 1);
    AddEvolutionSource(state, 7144, "ST3-03", PlayerId.Player1, sourcedTarget.Id);
    var attacker = AddBattlePermanent(state, 7145, 942, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var attackService = new AttackService();

    AssertTrue(attackService.CreateBlockerSelectionRequest(
        state,
        new AttackAction(PlayerId.Player0, attacker.Id, null)) is not null);

    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-14:option:attack-block-restriction",
        new[] { PermanentSelectionTarget(target) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertEqual(2, state.TemporaryModifiers.Count(modifier => modifier.TargetPermanentId == target.Id));
    AssertTrue(state.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.CannotAttack
        && modifier.TargetPermanentId == target.Id
        && modifier.DurationScope == DurationScope.UntilOpponentTurnEnd
        && modifier.ExpiresAtTurnPlayerId == PlayerId.Player1));
    AssertTrue(state.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.CannotBlock
        && modifier.TargetPermanentId == target.Id
        && modifier.DurationScope == DurationScope.UntilOpponentTurnEnd
        && modifier.ExpiresAtTurnPlayerId == PlayerId.Player1));
    AssertFalse(state.TemporaryModifiers.Any(modifier => modifier.TargetPermanentId == sourcedTarget.Id));
    AssertTrue(attackService.CreateBlockerSelectionRequest(
        state,
        new AttackAction(PlayerId.Player0, attacker.Id, null)) is null);

    state.TurnPlayerId = PlayerId.Player1;
    AssertThrows<DomainException>(() => new AttackService().Attack(
        state,
        new AttackAction(PlayerId.Player1, target.Id, null)));

    new DurationCleanupService().CleanupTurnEnd(state, PlayerId.Player0);
    AssertTrue(state.TemporaryModifiers.Any(modifier => modifier.TargetPermanentId == target.Id));

    new DurationCleanupService().CleanupTurnEnd(state, PlayerId.Player1);
    AssertFalse(state.TemporaryModifiers.Any(modifier => modifier.TargetPermanentId == target.Id));
    new AttackService().Attack(state, new AttackAction(PlayerId.Player1, target.Id, null));

    var securityState = CreateSt2St3ScenarioState();
    var securityAttacker = AddBattlePermanent(securityState, 7146, 943, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(securityState, 7147, "ST2-14", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var securityProvider = new TestDecisionProvider();
    securityProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-14:security:attack-block-restriction",
        new[] { PermanentSelectionTarget(securityAttacker) }));

    var securityResult = CreateSt2St3SecurityCheckService(securityProvider)
        .CheckSecurity(securityState, securityAttacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual("ST2-14:security:attack-block-restriction-selection", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertTrue(securityState.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.CannotAttack
        && modifier.TargetPermanentId == securityAttacker.Id
        && modifier.DurationScope == DurationScope.UntilOwnerTurnEnd
        && modifier.ExpiresAtTurnPlayerId == PlayerId.Player1));
    AssertTrue(securityState.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.CannotBlock
        && modifier.TargetPermanentId == securityAttacker.Id
        && modifier.DurationScope == DurationScope.UntilOwnerTurnEnd
        && modifier.ExpiresAtTurnPlayerId == PlayerId.Player1));
    AssertTrue(securityState.GetPlayer(PlayerId.Player1).Trash.Contains(security));
}

static void St2CocytusBreathMainAndSecurityBounceToHand()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7161, "ST2-16", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 7162, 960, "ST3-11", PlayerId.Player1, 0, enterTurn: 1);
    var topSource = AddEvolutionSource(state, 7163, "ST3-03", PlayerId.Player1, target.Id);
    var bottomSource = AddEvolutionSource(state, 7164, "ST3-05", PlayerId.Player1, target.Id);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-16:option:bounce",
        new[] { PermanentSelectionTarget(target) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Hand.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(topSource));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(bottomSource));
    AssertEqual(Zone.Hand, state.Cards[target.TopCardId].CurrentZone);
    AssertEqual(Zone.Trash, state.Cards[topSource].CurrentZone);
    AssertEqual(Zone.Trash, state.Cards[bottomSource].CurrentZone);
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));

    var securityState = CreateSt2St3ScenarioState();
    var attacker = AddBattlePermanent(securityState, 7165, 961, "ST3-11", PlayerId.Player0, 0, enterTurn: 1);
    var attackerSource = AddEvolutionSource(securityState, 7166, "ST3-03", PlayerId.Player0, attacker.Id);
    var security = AddCardToZone(securityState, 7167, "ST2-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var securityProvider = new TestDecisionProvider();
    securityProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-16:option:bounce",
        new[] { PermanentSelectionTarget(attacker) }));

    var securityResult = CreateSt2St3SecurityCheckService(securityProvider)
        .CheckSecurity(securityState, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual("ST2-16:security:main-option-activation", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertEqual("ST2-16:option:bounce-selection", securityResult.SecurityEffectResults[0].ActivatedMainOptionResolutions[0].StableId);
    AssertTrue(securityState.GetPlayer(PlayerId.Player0).Hand.Contains(attacker.TopCardId));
    AssertTrue(securityState.GetPlayer(PlayerId.Player0).Trash.Contains(attackerSource));
    AssertFalse(securityState.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertTrue(securityState.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertFalse(securityState.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.Trash, securityState.Cards[security].CurrentZone);
}

static void St2WereGarurumonSourcePlayOptionAndSecurity()
{
    var state = CreateSt2St3ScenarioState();
    state.Memory = 4;
    var option = AddCardToZone(state, 7151, "ST2-15", PlayerId.Player0, Zone.Executing);
    var host = AddBattlePermanent(state, 7152, 950, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7153, "ST2-03", PlayerId.Player0, host.Id);
    var optionProvider = new TestDecisionProvider();
    optionProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-15:option:source-host",
        new[] { PermanentSelectionTarget(host) }));
    optionProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        $"ST2-15:option:source-card:{host.Id.Value}",
        new[] { CardSelectionTarget(source, PlayerId.Player0, Zone.EvolutionSources, host.Id) }));
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), optionProvider);

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(2, optionResult.SelectionApplications.Count);
    AssertEqual(2, optionResult.ExecutedEffects.Count);
    AssertEqual("ST2-15:option:select-source-host", optionResult.ExecutedEffects[0].StableId);
    AssertEqual($"ST2-15:option:play-source-card:{host.Id.Value}", optionResult.ExecutedEffects[1].StableId);
    AssertFalse(host.SourceCardIds.Contains(source));
    var played = state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Single(permanent => permanent.TopCardId == source);
    AssertEqual(1, played.FrameIndex);
    AssertFalse(played.IsSuspended);
    AssertEqual(Zone.BattleArea, state.Cards[source].CurrentZone);
    AssertEqual(played.Id, state.Cards[source].PermanentId);
    AssertEqual(4, state.Memory);

    var securityState = CreateSt2St3ScenarioState();
    var attacker = AddBattlePermanent(securityState, 7154, 951, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var securityHost = AddBattlePermanent(securityState, 7155, 952, "ST2-06", PlayerId.Player1, 0, enterTurn: 1);
    var securitySource = AddEvolutionSource(securityState, 7156, "ST2-03", PlayerId.Player1, securityHost.Id);
    var security = AddCardToZone(securityState, 7157, "ST2-15", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var securityProvider = new TestDecisionProvider();
    securityProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST2-15:option:source-host",
        new[] { PermanentSelectionTarget(securityHost) }));
    securityProvider.EnqueueSelectionResult(SelectionResult.ForTargets(
        $"ST2-15:option:source-card:{securityHost.Id.Value}",
        new[] { CardSelectionTarget(securitySource, PlayerId.Player1, Zone.EvolutionSources, securityHost.Id) }));

    var securityResult = CreateSt2St3SecurityCheckService(securityProvider)
        .CheckSecurity(securityState, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual("ST2-15:security:main-option-activation", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertEqual(2, securityResult.SecurityEffectResults[0].ActivatedMainOptionResolutions.Count);
    AssertEqual("ST2-15:option:select-source-host", securityResult.SecurityEffectResults[0].ActivatedMainOptionResolutions[0].StableId);
    AssertEqual($"ST2-15:option:play-source-card:{securityHost.Id.Value}", securityResult.SecurityEffectResults[0].ActivatedMainOptionResolutions[1].StableId);
    AssertEqual(2, securityResult.SecurityEffectResults[0].SelectionApplications.Count);
    AssertFalse(securityHost.SourceCardIds.Contains(securitySource));
    var securityPlayed = securityState.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single(permanent => permanent.TopCardId == securitySource);
    AssertEqual(1, securityPlayed.FrameIndex);
    AssertFalse(securityPlayed.IsSuspended);
    AssertEqual(Zone.BattleArea, securityState.Cards[securitySource].CurrentZone);
    AssertTrue(securityState.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertEqual(Zone.Trash, securityState.Cards[security].CurrentZone);
}

static void St2WereGarurumonStaleSourceSelectionFails()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7158, "ST2-15", PlayerId.Player0, Zone.Executing);
    var host = AddBattlePermanent(state, 7159, 953, "ST2-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7160, "ST2-03", PlayerId.Player0, host.Id);
    var provider = new MutatingSelectionDecisionProvider(request =>
    {
        if (request.Id.StartsWith("ST2-15:option:source-card:", StringComparison.Ordinal))
        {
            new ZoneMover().MoveCard(
                state,
                new MoveCardCommand(
                    source,
                    Zone.EvolutionSources,
                    Zone.Trash,
                    MoveReason.Effect,
                    SourcePermanent: host.Id));
        }
    });
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);

    AssertThrows<DomainException>(() => pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(source));
    AssertFalse(state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Any(permanent => permanent.TopCardId == source));
}

static void St3DpZeroDeletionTriggersInheritedEffects()
{
    var registry = St2St3CardScriptCatalog.CreateCombinedWithSt1Registry();
    var state = CreateSt2St3ScenarioState();
    state.Memory = 0;
    var host = AddBattlePermanent(state, 7001, 800, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var dpSource = AddEvolutionSource(state, 7002, "ST3-01", PlayerId.Player0, host.Id);
    var memorySource = AddEvolutionSource(state, 7003, "ST3-04", PlayerId.Player0, host.Id);
    var target = AddBattlePermanent(state, 7004, 801, "ST3-03", PlayerId.Player1, 0, enterTurn: 1);
    new Tier1PrimitiveService().AddTemporaryDPModifier(
        state,
        target.Id,
        amount: -3000,
        DurationScope.UntilTurnEnd,
        PlayerId.Player0,
        stableId: "test:dp-zero-target");
    var pipeline = new TriggerPipelineService(registry);
    var stats = new EffectiveStatService(registry);
    var rules = CreateTestRuleProcessor(triggerPipelineService: pipeline, effectiveStats: stats);

    var result = rules.ProcessUntilStable(state);

    AssertTrue(result.ChangesApplied >= 1);
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == host.Id));
    AssertEqual(1, state.Memory);
    AssertTrue(state.TemporaryModifiers.Any(modifier =>
        modifier.SourceCardId == dpSource
        && modifier.TargetPermanentId == host.Id
        && modifier.ModifierKind == TemporaryModifierKind.DP
        && modifier.Amount == 1000
        && modifier.DurationScope == DurationScope.UntilTurnEnd));
    AssertFalse(state.TemporaryModifiers.Any(modifier => modifier.SourceCardId == memorySource));
    AssertEqual(7000, stats.Dp(state, host));
}

static void St3PatamonInheritedOnAllyAttackMemory()
{
    var state = CreateSt2St3ScenarioState();
    AddCardToZone(state, 7011, "ST3-02", PlayerId.Player0, Zone.Security);
    AddCardToZone(state, 7012, "ST3-03", PlayerId.Player0, Zone.Security);
    AddCardToZone(state, 7013, "ST3-06", PlayerId.Player0, Zone.Security);
    AddCardToZone(state, 7014, "ST3-07", PlayerId.Player0, Zone.Security);
    var host = AddBattlePermanent(state, 7015, 810, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7016, "ST3-05", PlayerId.Player0, host.Id);
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var result = pipeline.Run(state, EffectTiming.OnAllyAttack, PlayerId.Player0, source, host.Id);

    AssertEqual(1, result.ExecutedEffects.Count);
    AssertEqual("ST3-05:on-ally-attack:memory-plus-1", result.ExecutedEffects[0].StableId);
    AssertEqual(1, state.Memory);
}

static void St3EightInheritedOnAllyAttackDpReduction()
{
    var state = CreateSt2St3ScenarioState();
    var host = AddBattlePermanent(state, 7021, 820, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var source = AddEvolutionSource(state, 7022, "ST3-08", PlayerId.Player0, host.Id);
    var target = AddBattlePermanent(state, 7023, 821, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-08:on-ally-attack:dp",
        new[] { PermanentSelectionTarget(target) }));

    var result = pipeline.Run(state, EffectTiming.OnAllyAttack, PlayerId.Player0, source, host.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertEqual(5000, EffectiveStatService.NoContinuous.Dp(state, target));
}

static void St3AngewomonWhenDigivolvingRecoveryFromDeck()
{
    var state = CreateSt2St3ScenarioState();
    AddCardToZone(state, 7171, "ST3-02", PlayerId.Player0, Zone.Security, isFaceUp: false);
    AddCardToZone(state, 7172, "ST3-03", PlayerId.Player0, Zone.Security, isFaceUp: false);
    AddCardToZone(state, 7173, "ST3-06", PlayerId.Player0, Zone.Security, isFaceUp: false);
    var recoveredCard = AddCardToZone(state, 7174, "ST3-07", PlayerId.Player0, Zone.Deck, isFaceUp: false);
    var source = AddBattlePermanent(state, 7175, 970, "ST3-09", PlayerId.Player0, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry());

    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);

    AssertEqual(1, result.ExecutedEffects.Count);
    AssertEqual("ST3-09:when-digivolving:recovery-plus-1-deck", result.ExecutedEffects[0].StableId);
    AssertFalse(state.GetPlayer(PlayerId.Player0).Deck.Contains(recoveredCard));
    AssertEqual(recoveredCard, state.GetPlayer(PlayerId.Player0).Security[0]);
    AssertEqual(4, state.GetPlayer(PlayerId.Player0).Security.Count);
    AssertEqual(Zone.Security, state.Cards[recoveredCard].CurrentZone);
    AssertFalse(state.Cards[recoveredCard].IsFaceUp);

    var fullSecurityState = CreateSt2St3ScenarioState();
    AddCardToZone(fullSecurityState, 7176, "ST3-02", PlayerId.Player0, Zone.Security, isFaceUp: false);
    AddCardToZone(fullSecurityState, 7177, "ST3-03", PlayerId.Player0, Zone.Security, isFaceUp: false);
    AddCardToZone(fullSecurityState, 7178, "ST3-06", PlayerId.Player0, Zone.Security, isFaceUp: false);
    AddCardToZone(fullSecurityState, 7179, "ST3-07", PlayerId.Player0, Zone.Security, isFaceUp: false);
    var blockedDeckTop = AddCardToZone(fullSecurityState, 7180, "ST3-10", PlayerId.Player0, Zone.Deck, isFaceUp: false);
    var blockedSource = AddBattlePermanent(fullSecurityState, 7181, 971, "ST3-09", PlayerId.Player0, 0, enterTurn: 1);

    var blockedResult = pipeline.Run(
        fullSecurityState,
        EffectTiming.WhenDigivolving,
        PlayerId.Player0,
        blockedSource.TopCardId,
        blockedSource.Id);

    AssertEqual(0, blockedResult.ExecutedEffects.Count);
    AssertTrue(fullSecurityState.GetPlayer(PlayerId.Player0).Deck.Contains(blockedDeckTop));
    AssertEqual(4, fullSecurityState.GetPlayer(PlayerId.Player0).Security.Count);
}

static void St3ElevenTopOnAllyAttackDpReduction()
{
    var state = CreateSt2St3ScenarioState();
    var source = AddBattlePermanent(state, 7031, 830, "ST3-11", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 7032, 831, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-11:on-ally-attack:dp",
        new[] { PermanentSelectionTarget(target) }));

    var result = pipeline.Run(state, EffectTiming.OnAllyAttack, PlayerId.Player0, source.TopCardId, source.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertEqual(2000, EffectiveStatService.NoContinuous.Dp(state, target));
}

static void St3TakeruTakaishiContinuousSecurityDigimonDpAndSecurityPlay()
{
    var registry = St2St3CardScriptCatalog.CreateCombinedWithSt1Registry();
    var stats = new EffectiveStatService(registry);
    var state = CreateSt2St3ScenarioState();
    AddBattlePermanent(state, 7041, 840, "ST3-12", PlayerId.Player1, 0, enterTurn: 1);
    var securityDigimon = AddCardToZone(state, 7042, "ST3-06", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var attacker = AddBattlePermanent(state, 7043, 841, "ST3-09", PlayerId.Player0, 0, enterTurn: 1);

    AssertEqual(8000, stats.SecurityDp(state, securityDigimon));

    new Tier1PrimitiveService().AddTemporarySecurityDigimonDPModifier(
        state,
        PlayerId.Player1,
        amount: 1000,
        DurationScope.UntilSecurityCheckEnd,
        PlayerId.Player1,
        stableId: "ST3-12:test:temporary-security-dp");
    AssertEqual(9000, stats.SecurityDp(state, securityDigimon));

    var securityBattleResult = new SecurityCheckService(
            securityEffectExecutionService: new SecurityEffectExecutionService(registry, new TestDecisionProvider()),
            effectiveStats: stats)
        .CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { securityDigimon }, securityBattleResult.CheckedCards);
    AssertTrue(securityBattleResult.BattleResult is not null);
    AssertSequence(new[] { attacker.Id }, securityBattleResult.BattleResult!.DestroyedPermanents);
    AssertEmpty(state.TemporaryModifiers);

    var ownerTurnState = CreateSt2St3ScenarioState();
    ownerTurnState.TurnPlayerId = PlayerId.Player1;
    AddBattlePermanent(ownerTurnState, 7044, 842, "ST3-12", PlayerId.Player1, 0, enterTurn: 1);
    var ownerTurnSecurity = AddCardToZone(ownerTurnState, 7045, "ST3-06", PlayerId.Player1, Zone.Security, isFaceUp: false);
    AssertEqual(6000, stats.SecurityDp(ownerTurnState, ownerTurnSecurity));

    var playState = CreateSt2St3ScenarioState();
    var playAttacker = AddBattlePermanent(playState, 7046, 843, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var securityTamer = AddCardToZone(playState, 7047, "ST3-12", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var playResult = new SecurityCheckService(
            securityEffectExecutionService: new SecurityEffectExecutionService(registry, new TestDecisionProvider()),
            effectiveStats: stats)
        .CheckSecurity(playState, playAttacker.Id, PlayerId.Player1);

    AssertSequence(new[] { securityTamer }, playResult.CheckedCards);
    AssertEmpty(playResult.BattleResults.ToArray());
    AssertEqual("ST3-12:security:play-self-tamer", playResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertTrue(playState.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Any(permanent => permanent.TopCardId == securityTamer));
    AssertFalse(playState.GetPlayer(PlayerId.Player1).Trash.Contains(securityTamer));
    AssertFalse(playState.GetPlayer(PlayerId.Player1).Executing.Contains(securityTamer));
    AssertEqual(Zone.BattleArea, playState.Cards[securityTamer].CurrentZone);
}

static void St3HolyFlameMainAndSecurityDpPlusHand()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7051, "ST3-13", PlayerId.Player0, Zone.Executing);
    var ownerTarget = AddBattlePermanent(state, 7052, 850, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var ownerBreeding = AddBattlePermanent(state, 7053, 851, "ST3-06", PlayerId.Player0, 1, enterTurn: 1, isBreeding: true);
    var opponentTarget = AddBattlePermanent(state, 7054, 852, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-13:option:dp",
        new[] { PermanentSelectionTarget(ownerTarget) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertEqual(9000, EffectiveStatService.NoContinuous.Dp(state, ownerTarget));
    AssertEqual(6000, EffectiveStatService.NoContinuous.Dp(state, ownerBreeding));
    AssertEqual(6000, EffectiveStatService.NoContinuous.Dp(state, opponentTarget));

    var attacker = AddBattlePermanent(state, 7055, 853, "ST3-06", PlayerId.Player0, 2, enterTurn: 1);
    var defenderDigimon = AddBattlePermanent(state, 7056, 854, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var defenderBreeding = AddBattlePermanent(state, 7057, 855, "ST3-06", PlayerId.Player1, 1, enterTurn: 1, isBreeding: true);
    var securityOption = AddCardToZone(state, 7059, "ST3-13", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var securityBuffed = AddCardToZone(state, 7058, "ST3-06", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var securityResult = CreateSt2St3SecurityCheckService(new TestDecisionProvider())
        .CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { securityOption }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual(1, securityResult.SecurityEffectResults.Count);
    AssertEqual("ST3-13:security:dp-plus-5000-and-add-to-hand", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertEqual(11000, EffectiveStatService.NoContinuous.Dp(state, defenderDigimon));
    AssertEqual(6000, EffectiveStatService.NoContinuous.Dp(state, defenderBreeding));
    AssertTrue(state.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.SecurityDigimonDP
        && modifier.TargetPlayerId == PlayerId.Player1
        && modifier.Amount == 5000));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Hand.Contains(securityOption));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Trash.Contains(securityOption));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(securityOption));
    AssertEqual(Zone.Hand, state.Cards[securityOption].CurrentZone);
}

static void St3HeavensCharmMainDpReductionAndSecurityHand()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7061, "ST3-14", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 7062, 860, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-14:option:dp",
        new[] { PermanentSelectionTarget(target) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertEqual(4000, EffectiveStatService.NoContinuous.Dp(state, target));

    var attacker = AddBattlePermanent(state, 7063, 861, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 7064, "ST3-14", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var securityResult = CreateSt2St3SecurityCheckService(new TestDecisionProvider())
        .CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEmpty(securityResult.BattleResults.ToArray());
    AssertEqual("ST3-14:security:add-to-hand", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Hand.Contains(security));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.Hand, state.Cards[security].CurrentZone);
}

static void St3HolyWaveMainAndSecurityAttackReduction()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7131, "ST3-15", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 7132, 930, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    var keywords = new BattleKeywordService();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-15:option:security-attack",
        new[] { PermanentSelectionTarget(target) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertEqual(0, keywords.SecurityAttackCount(state, target));
    AssertTrue(state.TemporaryModifiers.Any(modifier =>
        modifier.ModifierKind == TemporaryModifierKind.SecurityAttack
        && modifier.TargetPermanentId == target.Id
        && modifier.Amount == -3
        && modifier.DurationScope == DurationScope.UntilOpponentTurnEnd
        && modifier.ExpiresAtTurnPlayerId == PlayerId.Player1));

    new DurationCleanupService().CleanupTurnEnd(state, PlayerId.Player0);
    AssertEqual(0, keywords.SecurityAttackCount(state, target));

    new DurationCleanupService().CleanupTurnEnd(state, PlayerId.Player1);
    AssertEqual(1, keywords.SecurityAttackCount(state, target));

    var securityState = CreateSt2St3ScenarioState();
    var attacker = AddBattlePermanent(securityState, 7133, 931, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var otherOpponent = AddBattlePermanent(securityState, 7134, 932, "ST3-08", PlayerId.Player0, 1, enterTurn: 1);
    var defender = AddBattlePermanent(securityState, 7135, 933, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var security = AddCardToZone(securityState, 7136, "ST3-15", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var securityResult = CreateSt2St3SecurityCheckService(new TestDecisionProvider())
        .CheckSecurity(securityState, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEqual("ST3-15:security:opponent-security-attack-minus-1", securityResult.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertEqual(0, keywords.SecurityAttackCount(securityState, attacker));
    AssertEqual(0, keywords.SecurityAttackCount(securityState, otherOpponent));
    AssertEqual(1, keywords.SecurityAttackCount(securityState, defender));
    AssertTrue(securityState.GetPlayer(PlayerId.Player1).Trash.Contains(security));
}

static void St3SevenHeavensMainAndSecurityDpReduction()
{
    var state = CreateSt2St3ScenarioState();
    var option = AddCardToZone(state, 7041, "ST3-16", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 7042, 840, "ST3-06", PlayerId.Player1, 0, enterTurn: 1);
    var provider = new TestDecisionProvider();
    var pipeline = new TriggerPipelineService(St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(), provider);
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-16:option:dp",
        new[] { PermanentSelectionTarget(target) }));

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, option);

    AssertEqual(1, optionResult.SelectionApplications.Count);
    AssertTrue(EffectiveStatService.NoContinuous.Dp(state, target) <= 0);
    CreateTestRuleProcessor().ProcessUntilStable(state);
    AssertFalse(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(target.TopCardId));

    var attacker = AddBattlePermanent(state, 7043, 841, "ST3-06", PlayerId.Player0, 0, enterTurn: 1);
    var securityTarget = AddBattlePermanent(state, 7044, 842, "ST3-06", PlayerId.Player0, 1, enterTurn: 1);
    var security = AddCardToZone(state, 7045, "ST3-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST3-16:option:dp",
        new[] { PermanentSelectionTarget(securityTarget) }));

    var securityResult = CreateSt2St3SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, securityResult.CheckedCards);
    AssertEqual(1, securityResult.SecurityEffectResults.Count);
    AssertEqual(SecurityEffectExecutionMode.ActivateMainOption, securityResult.SecurityEffectResults[0].SecurityResolutions[0].SecurityExecutionMode);
    AssertEqual(1, securityResult.SecurityEffectResults[0].ActivatedMainOptionResolutions.Count);
    AssertTrue(EffectiveStatService.NoContinuous.Dp(state, securityTarget) <= 0);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
}

static void St1CardEffectImplementedMemoryScripts()
{
    var state = CreateSt1ScenarioState();
    var registry = St1CardScriptCatalog.CreateRegistry();
    var coredramon = AddBattlePermanent(state, 5101, 511, "ST1-06", PlayerId.Player0, 0, enterTurn: 1);
    var sourceHost = AddBattlePermanent(state, 5102, 512, "ST1-11", PlayerId.Player0, 1, enterTurn: 1);
    var metalGreymonSource = AddEvolutionSource(state, 5103, "ST1-09", PlayerId.Player0, sourceHost.Id);
    var primitives = new Tier1PrimitiveService();

    var coredramonScript = registry.GetScript(state.CardDefinitions["ST1-06"]);
    coredramonScript.Resolve(new CardScriptExecutionContext(
        state,
        new EffectResolution(
            "ST1-06:on-ally-attack:memory-minus-2",
            EffectTiming.OnAllyAttack,
            coredramon.TopCardId,
            coredramon.Id,
            PlayerId.Player0,
            IsBackground: false,
            IsOptional: false,
            new EffectContext(state, EffectTiming.OnAllyAttack, PlayerId.Player0, coredramon.TopCardId, coredramon.Id)),
        primitives));

    AssertEqual(-2, state.Memory);

    var metalGreymonScript = registry.GetScript(state.CardDefinitions["ST1-09"]);
    metalGreymonScript.Resolve(new CardScriptExecutionContext(
        state,
        new EffectResolution(
            "ST1-09:on-block:anyone:memory-plus-3",
            EffectTiming.OnBlockAnyone,
            metalGreymonSource,
            sourceHost.Id,
            PlayerId.Player0,
            IsBackground: false,
            IsOptional: false,
            new EffectContext(state, EffectTiming.OnBlockAnyone, PlayerId.Player0, metalGreymonSource, sourceHost.Id)),
        primitives));

    AssertEqual(1, state.Memory);
}

static void St1GigaDestroyerMainOptionDeletesSelectedTargets()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5301, "ST1-15", PlayerId.Player0, Zone.Executing);
    var first = AddBattlePermanent(state, 5302, 632, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var second = AddBattlePermanent(state, 5303, 633, "ST1-05", PlayerId.Player1, 1, enterTurn: 1);
    var tooLarge = AddBattlePermanent(state, 5304, 634, "ST1-06", PlayerId.Player1, 2, enterTurn: 1);

    var resolution = CreateSt1OptionResolution(state, "ST1-15", source);
    var request = resolution.PendingSelectionRequest!;

    AssertEqual(2, request.Candidates.Count);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == first.Id));
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == second.Id));
    AssertFalse(request.Candidates.Any(candidate => candidate.Permanent == tooLarge.Id));

    new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, request.Candidates),
        new Tier1PrimitiveService());

    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(first.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(second.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == tooLarge.Id));
}

static void St1GigaDestroyerInvalidSelectionFails()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5311, "ST1-15", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 5312, 642, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var resolution = CreateSt1OptionResolution(state, "ST1-15", source);
    var request = resolution.PendingSelectionRequest!;
    var invalidKind = request.Candidates[0] with
    {
        Kind = SelectionTargetKind.Card,
        Card = target.TopCardId,
    };

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, new[] { invalidKind }),
            new Tier1PrimitiveService()));

    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == target.Id));
}

static void St1GigaDestroyerOutOfCandidateSelectionFails()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5321, "ST1-15", PlayerId.Player0, Zone.Executing);
    var valid = AddBattlePermanent(state, 5322, 652, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var nonCandidate = AddBattlePermanent(state, 5323, 653, "ST1-06", PlayerId.Player1, 1, enterTurn: 1);
    var resolution = CreateSt1OptionResolution(state, "ST1-15", source);
    var request = resolution.PendingSelectionRequest!;
    var invalid = new SelectableTarget(
        SelectionTargetKind.Permanent,
        $"permanent:{nonCandidate.Id.Value}",
        PlayerId.Player1,
        Permanent: nonCandidate.Id,
        Label: "ST1-06",
        Zone: Zone.BattleArea);

    AssertEqual(1, request.Candidates.Count);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == valid.Id));
    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, new[] { invalid }),
            new Tier1PrimitiveService()));

    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == valid.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == nonCandidate.Id));
}

static void St1GigaDestroyerStaleTargetSelectionFails()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5331, "ST1-15", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 5332, 662, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var resolution = CreateSt1OptionResolution(state, "ST1-15", source);
    var request = resolution.PendingSelectionRequest!;
    var selected = request.Candidates[0];

    new Tier1PrimitiveService().DestroyPermanent(state, target.Id);

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            resolution,
            SelectionResult.ForTargets(request.Id, new[] { selected }),
            new Tier1PrimitiveService()));
}

static void St1GaiaForceMainOptionDeletesSelectedTarget()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5341, "ST1-16", PlayerId.Player0, Zone.Executing);
    var largeTarget = AddBattlePermanent(state, 5342, 672, "ST1-06", PlayerId.Player1, 0, enterTurn: 1);
    var secondTarget = AddBattlePermanent(state, 5343, 673, "ST1-04", PlayerId.Player1, 1, enterTurn: 1);

    var resolution = CreateSt1OptionResolution(state, "ST1-16", source);
    var request = resolution.PendingSelectionRequest!;
    var selected = request.Candidates.Single(candidate => candidate.Permanent == largeTarget.Id);

    AssertEqual(2, request.Candidates.Count);
    new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, new[] { selected }),
        new Tier1PrimitiveService());

    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(largeTarget.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).FieldPermanents.Any(permanent => permanent.Id == secondTarget.Id));
}

static void St1GigaDestroyerSecurityActivatesMainOptionDeletion()
{
    var state = CreateSt1ScenarioState();
    state.Memory = 7;
    var attacker = AddBattlePermanent(state, 5351, 681, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var secondTarget = AddBattlePermanent(state, 5352, 682, "ST1-05", PlayerId.Player0, 1, enterTurn: 1);
    var tooLarge = AddBattlePermanent(state, 5353, 683, "ST1-06", PlayerId.Player0, 2, enterTurn: 1);
    var security = AddCardToZone(state, 5354, "ST1-15", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-15:option:delete",
        new[]
        {
            PermanentSelectionTarget(attacker),
            PermanentSelectionTarget(secondTarget),
        }));

    var result = CreateSt1SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertEmpty(result.BattleResults.ToArray());
    AssertEqual(1, result.SecurityEffectResults.Count);
    var securityEffect = result.SecurityEffectResults[0];
    AssertEqual(1, securityEffect.SecurityResolutions.Count);
    AssertEqual(SecurityEffectExecutionMode.ActivateMainOption, securityEffect.SecurityResolutions[0].SecurityExecutionMode);
    AssertEqual(1, securityEffect.ActivatedMainOptionResolutions.Count);
    var request = securityEffect.ActivatedMainOptionResolutions[0].PendingSelectionRequest!;
    AssertEqual(2, request.Candidates.Count);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == attacker.Id));
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == secondTarget.Id));
    AssertFalse(request.Candidates.Any(candidate => candidate.Permanent == tooLarge.Id));
    AssertEqual(1, securityEffect.SelectionApplications.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(secondTarget.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == tooLarge.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Security.Contains(security));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(security));
    AssertEqual(Zone.Trash, state.Cards[security].CurrentZone);
    AssertTrue(state.Cards[security].IsFaceUp);
    AssertEqual(7, state.Memory);
}

static void St1GaiaForceSecurityActivatesMainOptionDeletion()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5361, 691, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var largeTarget = AddBattlePermanent(state, 5362, 692, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    var security = AddCardToZone(state, 5363, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-16:option:delete",
        new[] { PermanentSelectionTarget(largeTarget) }));

    var result = CreateSt1SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertEmpty(result.BattleResults.ToArray());
    AssertEqual(1, result.SecurityEffectResults.Count);
    var request = result.SecurityEffectResults[0].ActivatedMainOptionResolutions.Single().PendingSelectionRequest!;
    AssertEqual(2, request.Candidates.Count);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == attacker.Id));
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == largeTarget.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(largeTarget.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));
}

static void SecurityOptionExecutionRejectsInvalidTarget()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5371, 701, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var tooLarge = AddBattlePermanent(state, 5372, 702, "ST1-06", PlayerId.Player0, 1, enterTurn: 1);
    AddCardToZone(state, 5373, "ST1-15", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-15:option:delete",
        new[] { PermanentSelectionTarget(tooLarge) }));

    AssertThrows<DomainException>(() =>
        CreateSt1SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1));

    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == tooLarge.Id));
}

static void SecurityOptionExecutionRejectsStaleTarget()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5381, 711, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 5382, "ST1-16", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var primitives = new Tier1PrimitiveService();
    var provider = new MutatingSelectionDecisionProvider(
        request =>
        {
            var permanent = request.Candidates[0].Permanent
                ?? throw new DomainException("Expected permanent candidate.");
            primitives.DestroyPermanent(state, permanent);
        });

    AssertThrows<DomainException>(() =>
        CreateSt1SecurityCheckService(provider).CheckSecurity(state, attacker.Id, PlayerId.Player1));

    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
}

static void St1GarudamonWhenDigivolvingCreatesSelection()
{
    var state = CreateSt1ScenarioState();
    var source = AddBattlePermanent(state, 5391, 731, "ST1-08", PlayerId.Player0, 0, enterTurn: 1);
    var ownerTarget = AddBattlePermanent(state, 5392, 732, "ST1-04", PlayerId.Player0, 1, enterTurn: 1);
    var opponentTarget = AddBattlePermanent(state, 5393, 733, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());

    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);

    AssertTrue(result.HasPendingSelection);
    var request = result.PendingSelectionRequest!;
    AssertEqual("ST1-08:when-digivolving:dp", request.Id);
    AssertEqual(PlayerId.Player0, request.Player);
    AssertEqual(SelectionKind.SelectPermanent, request.Kind);
    AssertEqual(1, request.MinCount);
    AssertEqual(1, request.MaxCount);
    AssertFalse(request.CanSkip);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == source.Id));
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == ownerTarget.Id));
    AssertFalse(request.Candidates.Any(candidate => candidate.Permanent == opponentTarget.Id));
}

static void St1GarudamonSelectionAppliesDurationDp()
{
    var state = CreateSt1ScenarioState();
    var source = AddBattlePermanent(state, 5401, 741, "ST1-08", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 5402, 742, "ST1-04", PlayerId.Player0, 1, enterTurn: 1);
    var provider = new TestDecisionProvider();
    provider.EnqueueSelectionResult(SelectionResult.ForTargets(
        "ST1-08:when-digivolving:dp",
        new[] { PermanentSelectionTarget(target) }));
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry(), provider);

    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);

    AssertEqual(1, result.SelectionApplications.Count);
    AssertEqual(7000, EffectiveStatService.NoContinuous.Dp(state, target));
    AssertEqual(4000, state.CardDefinitions["ST1-04"].DP);

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(4000, EffectiveStatService.NoContinuous.Dp(state, target));
}

static void St1GarudamonInvalidTargetSelectionFails()
{
    var state = CreateSt1ScenarioState();
    var source = AddBattlePermanent(state, 5411, 751, "ST1-08", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 5412, 752, "ST1-04", PlayerId.Player0, 1, enterTurn: 1);
    var nonCandidate = AddBattlePermanent(state, 5413, 753, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());
    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);
    var request = result.PendingSelectionRequest!;
    var invalid = new SelectableTarget(
        SelectionTargetKind.Permanent,
        $"permanent:{nonCandidate.Id.Value}",
        PlayerId.Player1,
        Permanent: nonCandidate.Id,
        Zone: Zone.BattleArea);

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            result.PendingResolution!,
            SelectionResult.ForTargets(request.Id, new[] { invalid }),
            new Tier1PrimitiveService()));

    AssertEqual(4000, EffectiveStatService.NoContinuous.Dp(state, target));
}

static void St1GarudamonStaleTargetSelectionFails()
{
    var state = CreateSt1ScenarioState();
    var source = AddBattlePermanent(state, 5421, 761, "ST1-08", PlayerId.Player0, 0, enterTurn: 1);
    var target = AddBattlePermanent(state, 5422, 762, "ST1-04", PlayerId.Player0, 1, enterTurn: 1);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());
    var result = pipeline.Run(state, EffectTiming.WhenDigivolving, PlayerId.Player0, source.TopCardId, source.Id);
    var request = result.PendingSelectionRequest!;
    var selected = request.Candidates.Single(candidate => candidate.Permanent == target.Id);

    new Tier1PrimitiveService().DestroyPermanent(state, target.Id);

    AssertThrows<DomainException>(() =>
        new SelectionResultApplicator().Apply(
            state,
            result.PendingResolution!,
            SelectionResult.ForTargets(request.Id, new[] { selected }),
            new Tier1PrimitiveService()));
}

static void St1ShadowWingMainOptionAppliesDurationDp()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5431, "ST1-13", PlayerId.Player0, Zone.Executing);
    var target = AddBattlePermanent(state, 5432, 772, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);

    var resolution = CreateSt1OptionResolution(state, "ST1-13", source);
    var request = resolution.PendingSelectionRequest!;

    AssertEqual("ST1-13:option:dp", request.Id);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == target.Id));

    new SelectionResultApplicator().Apply(
        state,
        resolution,
        SelectionResult.ForTargets(request.Id, new[] { PermanentSelectionTarget(target) }),
        new Tier1PrimitiveService());

    AssertEqual(7000, EffectiveStatService.NoContinuous.Dp(state, target));

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(4000, EffectiveStatService.NoContinuous.Dp(state, target));
}

static void St1ShadowWingSecurityAppliesPlayerSecurityAttack()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5441, 781, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var defenderDigimon = AddBattlePermanent(state, 5442, 782, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    var security = AddCardToZone(state, 5443, "ST1-13", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = CreateSt1SecurityCheckService(new TestDecisionProvider()).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { security }, result.CheckedCards);
    AssertEmpty(result.BattleResults.ToArray());
    AssertEqual(1, result.SecurityEffectResults.Count);
    AssertEqual(1, result.SecurityEffectResults[0].SecurityResolutions.Count);
    AssertEqual(2, BattleKeywordService.Default.SecurityAttackCount(state, defenderDigimon));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(security));

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(2, BattleKeywordService.Default.SecurityAttackCount(state, defenderDigimon));

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(1, BattleKeywordService.Default.SecurityAttackCount(state, defenderDigimon));
}

static void St1StarlightExplosionMainOptionAppliesSecurityDigimonDp()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5451, "ST1-14", PlayerId.Player0, Zone.Executing);
    var attacker = AddBattlePermanent(state, 5452, 792, "ST1-06", PlayerId.Player1, 0, enterTurn: 1);
    var securityDigimon = AddCardToZone(state, 5453, "ST1-04", PlayerId.Player0, Zone.Security, isFaceUp: false);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());

    var optionResult = pipeline.Run(state, EffectTiming.OptionSkill, PlayerId.Player0, source);

    AssertEqual(1, optionResult.ExecutedEffects.Count);
    var securityResult = new SecurityCheckService().CheckSecurity(state, attacker.Id, PlayerId.Player0);
    AssertSequence(new[] { securityDigimon }, securityResult.CheckedCards);
    AssertEqual(1, securityResult.BattleResults.Count);
    AssertTrue(securityResult.BattleResults[0].DestroyedPermanents.Contains(attacker.Id));

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(1, state.TemporaryModifiers.Count);

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEmpty(state.TemporaryModifiers);
}

static void St1StarlightExplosionSecurityAppliesSecurityDigimonDp()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5461, 801, "ST1-07", PlayerId.Player0, 0, enterTurn: 1);
    var optionSecurity = AddCardToZone(state, 5462, "ST1-14", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var digimonSecurity = AddCardToZone(state, 5463, "ST1-04", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = CreateSt1SecurityCheckService(new TestDecisionProvider()).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { optionSecurity, digimonSecurity }, result.CheckedCards);
    AssertEqual(1, result.BattleResults.Count);
    AssertTrue(result.BattleResults[0].DestroyedPermanents.Contains(attacker.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(optionSecurity));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(digimonSecurity));
    AssertEqual(1, state.TemporaryModifiers.Count);

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEmpty(state.TemporaryModifiers);
}

static void St1TaiKamiyaSecurityPlaysSelfTamer()
{
    var state = CreateSt1ScenarioState();
    state.Memory = 4;
    var attacker = AddBattlePermanent(state, 5471, 811, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    var ownerDigimon = AddBattlePermanent(state, 5472, 812, "ST1-04", PlayerId.Player1, 1, enterTurn: 1);
    var securityTamer = AddCardToZone(state, 5473, "ST1-12", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = CreateSt1SecurityCheckService(new TestDecisionProvider()).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { securityTamer }, result.CheckedCards);
    AssertEmpty(result.BattleResults.ToArray());
    AssertEqual(1, result.SecurityEffectResults.Count);
    AssertEqual(1, result.SecurityEffectResults[0].SecurityResolutions.Count);
    AssertEqual("ST1-12:security:play-self-tamer", result.SecurityEffectResults[0].SecurityResolutions[0].StableId);
    AssertFalse(state.GetPlayer(PlayerId.Player1).Security.Contains(securityTamer));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(securityTamer));
    AssertFalse(state.GetPlayer(PlayerId.Player1).Trash.Contains(securityTamer));
    var played = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single(permanent => permanent.TopCardId == securityTamer);
    AssertEqual(0, played.FrameIndex);
    AssertFalse(played.IsSuspended);
    AssertEqual(Zone.BattleArea, state.Cards[securityTamer].CurrentZone);
    AssertEqual<PermanentId?>(played.Id, state.Cards[securityTamer].PermanentId);
    AssertTrue(state.Cards[securityTamer].IsFaceUp);
    AssertEqual(4, state.Memory);

    var stats = new EffectiveStatService(St1CardScriptCatalog.CreateRegistry());
    AssertEqual(4000, stats.Dp(state, ownerDigimon));

    new PhaseRunner().EndCurrentTurn(state, memoryForNextPlayer: 3);

    AssertEqual(5000, stats.Dp(state, ownerDigimon));
}

static void St1TaiKamiyaSecurityTriggerPipelinePlaysSelfTamer()
{
    var state = CreateSt1ScenarioState();
    var source = AddCardToZone(state, 5491, "ST1-12", PlayerId.Player1, Zone.Executing);
    var pipeline = new TriggerPipelineService(St1CardScriptCatalog.CreateRegistry());

    var result = pipeline.Run(state, EffectTiming.SecuritySkill, PlayerId.Player1, source);

    AssertEqual(1, result.ExecutedEffects.Count);
    AssertEqual("ST1-12:security:play-self-tamer", result.ExecutedEffects[0].StableId);
    AssertFalse(state.GetPlayer(PlayerId.Player1).Executing.Contains(source));
    var played = state.GetPlayer(PlayerId.Player1).BattleAreaPermanents.Single(permanent => permanent.TopCardId == source);
    AssertEqual(0, played.FrameIndex);
    AssertEqual(Zone.BattleArea, state.Cards[source].CurrentZone);
    AssertEqual<PermanentId?>(played.Id, state.Cards[source].PermanentId);
}

static void St1TaiKamiyaSecurityPlayFullFieldDoesNotPlay()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5481, 821, "ST1-04", PlayerId.Player0, 0, enterTurn: 1);
    AddBattlePermanent(state, 5482, 822, "ST1-04", PlayerId.Player1, 0, enterTurn: 1);
    AddBattlePermanent(state, 5483, 823, "ST1-05", PlayerId.Player1, 1, enterTurn: 1);
    AddBattlePermanent(state, 5484, 824, "ST1-06", PlayerId.Player1, 2, enterTurn: 1);
    AddBattlePermanent(state, 5485, 825, "ST1-09", PlayerId.Player1, 3, enterTurn: 1);
    var securityTamer = AddCardToZone(state, 5486, "ST1-12", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = CreateSt1SecurityCheckService(new TestDecisionProvider()).CheckSecurity(state, attacker.Id, PlayerId.Player1);

    AssertSequence(new[] { securityTamer }, result.CheckedCards);
    AssertEqual(1, result.SecurityEffectResults.Count);
    AssertEqual(0, result.SecurityEffectResults[0].SecurityResolutions.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(securityTamer));
    AssertEqual(Zone.Trash, state.Cards[securityTamer].CurrentZone);
}

static void St1CardBodyWiringValidationReport()
{
    var report = new TargetCardPoolValidator().Validate(new TargetCardPoolValidationRequest(
        "ST1",
        CreateSt1TargetDecks(),
        CreateSt1CardDatabase(),
        St1CardScriptCatalog.CreateRegistry()));

    AssertTrue(report.IsValid);
    AssertEqual(0, report.UnsupportedCards.Count);
    AssertEqual(0, report.PartiallyImplementedCards.Count);
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-12"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-08"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-13"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-14"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-15"));
    AssertFalse(report.PartiallyImplementedCards.Any(status => status.CardId == "ST1-16"));
    AssertFalse(report.MissingLayers.Contains("duration-cleanup"));
    AssertFalse(report.MissingLayers.Contains("security-option-execution"));
    AssertFalse(report.MissingLayers.Contains("selection-result-application"));
    AssertFalse(report.MissingLayers.Contains("full-trigger-pipeline"));
}

static void St1CardEffectKeywordCapabilitySmoke()
{
    var state = CreateSt1ScenarioState();
    var attacker = AddBattlePermanent(state, 5111, 521, "ST1-07", PlayerId.Player0, 0, enterTurn: 1);
    var blocker = AddBattlePermanent(state, 5112, 522, "ST1-06", PlayerId.Player1, 0, enterTurn: 1);

    var securityAttackCount = BattleKeywordService.Default.SecurityAttackCount(state, attacker);
    var blockRequest = new AttackService().CreateBlockerSelectionRequest(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertEqual(2, securityAttackCount);
    AssertTrue(blockRequest is not null);
    AssertEqual(PlayerId.Player1, blockRequest!.Player);
    AssertTrue(blockRequest.Candidates.Any(candidate => candidate.Permanent == blocker.Id));
}

static void St1CardEffectReplayDeterminism()
{
    var first = RunSt1ImplementedEffectSequence();
    var second = RunSt1ImplementedEffectSequence();

    AssertEqual(first.ComputeStateHash(), second.ComputeStateHash());
}

static void BattleKeywordsBlockerSelectionRequest()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4001, 401, "KW-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var blocker = AddBattlePermanent(state, 4002, 402, "KW-BLOCKER", PlayerId.Player1, 0, enterTurn: 1);

    var request = new AttackService().CreateBlockerSelectionRequest(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(request is not null);
    AssertEqual(PlayerId.Player1, request!.Player);
    AssertTrue(request.CanSkip);
    AssertEqual(1, request.Candidates.Count);
    AssertEqual(blocker.Id, request.Candidates[0].Permanent);
}

static void BattleKeywordsSecurityAttackExtraChecks()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4011, 411, "KW-SEC-PLUS", PlayerId.Player0, 0, enterTurn: 1);
    var firstSecurity = AddCardToZone(state, 4012, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);
    var secondSecurity = AddCardToZone(state, 4013, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = new AttackService().Attack(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(result.SecurityCheck is not null);
    AssertSequence(new[] { firstSecurity, secondSecurity }, result.SecurityCheck!.CheckedCards);
    AssertEqual(0, state.GetPlayer(PlayerId.Player1).Security.Count);
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(firstSecurity));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(secondSecurity));
}

static void BattleKeywordsPiercingSecurityCheck()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4021, 421, "KW-PIERCING", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 4022, 422, "KW-WEAK", PlayerId.Player1, 0, enterTurn: 1);
    var security = AddCardToZone(state, 4023, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = new AttackService().Attack(state, new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(result.Battle is not null);
    AssertTrue(result.Battle!.DestroyedPermanents.Contains(defender.Id));
    AssertTrue(result.SecurityCheck is not null);
    AssertSequence(new[] { security }, result.SecurityCheck!.CheckedCards);
}

static void BattleKeywordsJammingSecurityBattle()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4031, 431, "KW-JAMMING", PlayerId.Player0, 0, enterTurn: 1);
    var security = AddCardToZone(state, 4032, "KW-SECURITY-DIGIMON", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var result = new AttackService().Attack(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(result.SecurityCheck is not null);
    AssertEqual(security, result.SecurityCheck!.CheckedCard);
    AssertTrue(state.GetPlayer(PlayerId.Player0).FieldPermanents.Any(permanent => permanent.Id == attacker.Id));
    AssertFalse(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
}

static void BattleKeywordsRetaliationDpBattle()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4041, 441, "KW-STRONG", PlayerId.Player0, 0, enterTurn: 1);
    var defender = AddBattlePermanent(state, 4042, 442, "KW-RETALIATION", PlayerId.Player1, 0, enterTurn: 1);

    var result = new AttackService().Attack(state, new AttackAction(PlayerId.Player0, attacker.Id, defender.Id));

    AssertTrue(result.Battle is not null);
    AssertTrue(result.Battle!.DestroyedPermanents.Contains(attacker.Id));
    AssertTrue(result.Battle.DestroyedPermanents.Contains(defender.Id));
    AssertTrue(state.GetPlayer(PlayerId.Player0).Trash.Contains(attacker.TopCardId));
    AssertTrue(state.GetPlayer(PlayerId.Player1).Trash.Contains(defender.TopCardId));
}

static void BattleKeywordsRebootActivePhase()
{
    var state = CreateBattleKeywordState();
    state.TurnPlayerId = PlayerId.Player1;
    var reboot = AddBattlePermanent(state, 4051, 451, "KW-REBOOT", PlayerId.Player0, 0, enterTurn: 1, isSuspended: true);

    new PhaseRunner().RunActivePhase(state);

    AssertFalse(reboot.IsSuspended);
}

static void BattleKeywordsRushAttackLegality()
{
    var state = CreateBattleKeywordState();
    var rush = AddBattlePermanent(state, 4061, 461, "KW-RUSH", PlayerId.Player0, 0, enterTurn: state.TurnCount);
    AddBattlePermanent(state, 4062, 462, "KW-ROOKIE", PlayerId.Player0, 1, enterTurn: state.TurnCount);
    AddCardToZone(state, 4063, "BT1-OPTION", PlayerId.Player1, Zone.Security, isFaceUp: false);

    var actions = new LegalActionGenerator().Generate(state, PlayerId.Player0)
        .Where(action => action.Kind == LegalActionKind.Attack)
        .Select(action => (AttackAction)action.Action)
        .ToArray();

    AssertTrue(actions.Any(action => action.Attacker == rush.Id));
    AssertFalse(actions.Any(action => action.Attacker == new PermanentId(462)));
}

static void BattleKeywordsCollisionForcedBlockRequest()
{
    var state = CreateBattleKeywordState();
    var attacker = AddBattlePermanent(state, 4071, 471, "KW-COLLISION", PlayerId.Player0, 0, enterTurn: 1);
    var first = AddBattlePermanent(state, 4072, 472, "KW-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    var second = AddBattlePermanent(state, 4073, 473, "KW-WEAK", PlayerId.Player1, 1, enterTurn: 1);

    var request = new AttackService().CreateBlockerSelectionRequest(state, new AttackAction(PlayerId.Player0, attacker.Id, null));

    AssertTrue(request is not null);
    AssertFalse(request!.CanSkip);
    AssertEqual(2, request.Candidates.Count);
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == first.Id));
    AssertTrue(request.Candidates.Any(candidate => candidate.Permanent == second.Id));
}

static void BattleKeywordsDecoyUnsupportedExplicit()
{
    var state = CreateBattleKeywordState();
    AddBattlePermanent(state, 4081, 481, "KW-DECOY", PlayerId.Player0, 0, enterTurn: 1);

    AssertThrows<UnsupportedMechanicException>(() =>
        CreateTestRuleProcessor().ProcessUntilStable(state));
}

static void ComplexMechanicsJogressLegalAction()
{
    var state = CreateComplexMechanicState();
    AddCardToZone(state, 3001, "CM-JOGRESS", PlayerId.Player0, Zone.Hand);
    AddBattlePermanent(state, 3002, 302, "CM-GREEN-L4", PlayerId.Player0, 0, enterTurn: 1);
    AddBattlePermanent(state, 3003, 303, "CM-BLUE-L4", PlayerId.Player0, 1, enterTurn: 1);

    var actions = new LegalActionGenerator().Generate(state, PlayerId.Player0);

    AssertTrue(actions.Any(action => action.Kind == LegalActionKind.Jogress));
}

static void ComplexMechanicsJogressExecutionTopSources()
{
    var state = CreateComplexMechanicState();
    var jogress = AddCardToZone(state, 3011, "CM-JOGRESS", PlayerId.Player0, Zone.Hand);
    var left = AddBattlePermanent(state, 3012, 312, "CM-GREEN-L4", PlayerId.Player0, 0, enterTurn: 1);
    var right = AddBattlePermanent(state, 3013, 313, "CM-BLUE-L4", PlayerId.Player0, 1, enterTurn: 1);
    AddCardToZone(state, 3014, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    CreateTestActionExecutor().Execute(state, new JogressAction(PlayerId.Player0, jogress, new[] { left.Id, right.Id }));

    var permanent = state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Single(candidate => candidate.TopCardId == jogress);
    AssertEqual(jogress, permanent.TopCardId);
    AssertSequence(new[] { left.TopCardId, right.TopCardId }, permanent.SourceCardIds);
    AssertEqual(Zone.EvolutionSources, state.Cards[left.TopCardId].CurrentZone);
    AssertEqual(Zone.EvolutionSources, state.Cards[right.TopCardId].CurrentZone);
    AssertTrue(state.GetPlayer(PlayerId.Player0).Hand.Contains(new CardInstanceId(3014)));
}

static void ComplexMechanicsBurstLegalAction()
{
    var state = CreateComplexMechanicState();
    AddCardToZone(state, 3021, "CM-BURST", PlayerId.Player0, Zone.Hand);
    AddBattlePermanent(state, 3022, 322, "CM-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    AddBattlePermanent(state, 3023, 323, "CM-TAMER", PlayerId.Player0, 1, enterTurn: 1);

    var actions = new LegalActionGenerator().Generate(state, PlayerId.Player0);

    AssertTrue(actions.Any(action => action.Kind == LegalActionKind.BurstDigivolve));
}

static void ComplexMechanicsBurstTamerSelectionRequest()
{
    var state = CreateComplexMechanicState();
    var burstCard = AddCardToZone(state, 3031, "CM-BURST", PlayerId.Player0, Zone.Hand);
    var tamer = AddBattlePermanent(state, 3032, 332, "CM-TAMER", PlayerId.Player0, 0, enterTurn: 1);
    var request = new ComplexMechanicSelectionFactory().CreateBurstTamerSelection(
        state,
        PlayerId.Player0,
        burstCard,
        new[] { tamer });

    AssertEqual(SelectionKind.SelectPermanent, request.Kind);
    AssertEqual(1, request.MinCount);
    AssertEqual(1, request.Candidates.Count);
    AssertEqual(tamer.Id, request.Candidates[0].Permanent);
}

static void ComplexMechanicsAppFusionLinkCardSelection()
{
    var state = CreateComplexMechanicState();
    var appFusionCard = AddCardToZone(state, 3041, "CM-APPFUSION", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 3042, 342, "CM-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var linkCard = AddLinkedCard(state, 3043, "CM-LINK", PlayerId.Player0, target.Id);
    var request = new ComplexMechanicSelectionFactory().CreateAppFusionLinkCardSelection(
        state,
        PlayerId.Player0,
        appFusionCard,
        new[] { linkCard });

    AssertEqual(SelectionKind.SelectCard, request.Kind);
    AssertEqual(linkCard, request.Candidates[0].Card);
}

static void ComplexMechanicsDigiXrosMaterialsAndCost()
{
    var state = CreateComplexMechanicState();
    state.Memory = 5;
    var card = AddCardToZone(state, 3051, "CM-DIGIXROS", PlayerId.Player0, Zone.Hand);
    var handMaterial = AddCardToZone(state, 3052, "CM-XROS-A", PlayerId.Player0, Zone.Hand);
    var trashMaterial = AddCardToZone(state, 3053, "CM-XROS-B", PlayerId.Player0, Zone.Trash);
    var requirement = state.CardDefinitions["CM-DIGIXROS"].PlayRequirements.Single(requirement => requirement.Mode == PlayMode.DigiXros);

    var cost = new CostResolver().ResolveDigiXros(state, card, requirement, selectedMaterialCount: 2);
    var candidates = new ComplexMechanicMatcher()
        .FindMaterialCandidates(state, PlayerId.Player0, requirement.Materials[0])
        .Concat(new ComplexMechanicMatcher().FindMaterialCandidates(state, PlayerId.Player0, requirement.Materials[1]))
        .ToArray();
    var request = new ComplexMechanicSelectionFactory().CreateDigiXrosMaterialSelection(state, PlayerId.Player0, card, candidates, maxCount: 2);

    CreateTestActionExecutor().Execute(state, new DigiXrosPlayAction(PlayerId.Player0, card, 0, new[] { handMaterial, trashMaterial }));

    var permanent = state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Single(candidate => candidate.TopCardId == card);
    AssertEqual(2, cost.FinalCost);
    AssertEqual(3, state.Memory);
    AssertSequence(new[] { handMaterial, trashMaterial }, permanent.SourceCardIds);
    AssertEqual(2, request.MaxCount);
}

static void ComplexMechanicsAssemblyMaterialSelection()
{
    var state = CreateComplexMechanicState();
    state.Memory = 5;
    var card = AddCardToZone(state, 3061, "CM-ASSEMBLY", PlayerId.Player0, Zone.Hand);
    var first = AddCardToZone(state, 3062, "CM-ASSEMBLY-A", PlayerId.Player0, Zone.Trash);
    var second = AddCardToZone(state, 3063, "CM-ASSEMBLY-B", PlayerId.Player0, Zone.Trash);
    var requirement = state.CardDefinitions["CM-ASSEMBLY"].PlayRequirements.Single(requirement => requirement.Mode == PlayMode.Assembly);
    var candidates = requirement.Materials
        .SelectMany(material => new ComplexMechanicMatcher().FindMaterialCandidates(state, PlayerId.Player0, material))
        .ToArray();
    var request = new ComplexMechanicSelectionFactory().CreateAssemblyMaterialSelection(state, PlayerId.Player0, card, candidates, count: 2);

    CreateTestActionExecutor().Execute(state, new AssemblyPlayAction(PlayerId.Player0, card, 0, new[] { first, second }));

    var permanent = state.GetPlayer(PlayerId.Player0).BattleAreaPermanents.Single(candidate => candidate.TopCardId == card);
    AssertEqual(2, request.MinCount);
    AssertEqual(3, state.Memory);
    AssertSequence(new[] { first, second }, permanent.SourceCardIds);
}

static void ComplexMechanicsLinkCardStateConsistency()
{
    var state = CreateComplexMechanicState();
    state.Memory = 5;
    var linkCard = AddCardToZone(state, 3071, "CM-LINK", PlayerId.Player0, Zone.Hand);
    var target = AddBattlePermanent(state, 3072, 372, "CM-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);

    CreateTestActionExecutor().Execute(state, new LinkAction(PlayerId.Player0, linkCard, target.Id));

    AssertSequence(new[] { linkCard }, target.LinkedCards);
    AssertEqual(Zone.LinkedCards, state.Cards[linkCard].CurrentZone);
    AssertEqual<PermanentId?>(target.Id, state.Cards[linkCard].PermanentId);
    AssertEqual(3, state.Memory);
    AssertTrue(new EngineInvariantChecker().Check(state).IsValid);
}

static void ComplexMechanicsUnsupportedMechanicExplicit()
{
    var state = CreateComplexMechanicState();
    var card = AddCardToZone(state, 3081, "CM-ROOKIE", PlayerId.Player0, Zone.Hand);
    var left = AddBattlePermanent(state, 3082, 382, "CM-GREEN-L4", PlayerId.Player0, 0, enterTurn: 1);
    var right = AddBattlePermanent(state, 3083, 383, "CM-BLUE-L4", PlayerId.Player0, 1, enterTurn: 1);

    AssertThrows<UnsupportedMechanicException>(() =>
        CreateTestActionExecutor().Execute(state, new JogressAction(PlayerId.Player0, card, new[] { left.Id, right.Id })));
}

static void ComplexMechanicsReplayDeterminism()
{
    var state = CreateComplexMechanicState();
    var jogress = AddCardToZone(state, 3091, "CM-JOGRESS", PlayerId.Player0, Zone.Hand);
    var left = AddBattlePermanent(state, 3092, 392, "CM-GREEN-L4", PlayerId.Player0, 0, enterTurn: 1);
    var right = AddBattlePermanent(state, 3093, 393, "CM-BLUE-L4", PlayerId.Player0, 1, enterTurn: 1);
    var scenario = new ScriptedScenario(
        "complex replay jogress",
        state,
        new[] { new JogressAction(PlayerId.Player0, jogress, new[] { left.Id, right.Id }) });

    var result = CreateTestScriptedScenarioRunner().Run(scenario);
    var replay = CreateTestReplayDeterminismHelper().ReplayAndCompare(scenario.InitialState, result.Trace, result.FinalStateHash);

    AssertTrue(replay.Matches);
    AssertEqual(result.FinalStateHash, replay.ActualFinalStateHash);
}

static void EngineDoesNotReferenceUnityAssemblies()
{
    var bannedNames = new[] { "UnityEngine", "Photon", "Unity.", "UnityEditor" };
    var references = typeof(GameState)
        .Assembly
        .GetReferencedAssemblies()
        .Select(assembly => assembly.Name ?? string.Empty)
        .ToArray();

    foreach (var bannedName in bannedNames)
    {
        if (references.Any(reference => reference.Contains(bannedName, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Forbidden assembly reference found: {bannedName}");
        }
    }
}

static void AssertEqual<T>(T expected, T actual)
{
    if (!EqualityComparer<T>.Default.Equals(expected, actual))
    {
        throw new InvalidOperationException($"Expected '{expected}', actual '{actual}'.");
    }
}

static void AssertEmpty<T>(ICollection<T> collection)
{
    if (collection.Count != 0)
    {
        throw new InvalidOperationException($"Expected empty collection, actual count {collection.Count}.");
    }
}

static void AssertTrue(bool value)
{
    if (!value)
    {
        throw new InvalidOperationException("Expected true.");
    }
}

static void AssertFalse(bool value)
{
    if (value)
    {
        throw new InvalidOperationException("Expected false.");
    }
}

static void AssertNotEqual<T>(T notExpected, T actual)
{
    if (EqualityComparer<T>.Default.Equals(notExpected, actual))
    {
        throw new InvalidOperationException($"Did not expect '{actual}'.");
    }
}

static void AssertNotSame<T>(T notExpected, T actual)
    where T : class
{
    if (ReferenceEquals(notExpected, actual))
    {
        throw new InvalidOperationException("Did not expect the same reference.");
    }
}

static void AssertViolation(EngineInvariantReport report, string code)
{
    if (!report.Violations.Any(violation => violation.Code == code))
    {
        throw new InvalidOperationException($"Expected invariant violation '{code}'.");
    }
}

static AssetCardMetadata Asset(string cardId, int cardIndex, string variantKey, string effectClassName)
{
    var assetNamePrefix = cardId.Replace("-", "_", StringComparison.Ordinal);
    var assetName = string.Equals(variantKey, "base", StringComparison.Ordinal)
        ? assetNamePrefix
        : $"{assetNamePrefix}_{variantKey}";

    return new AssetCardMetadata(
        new AssetCardDefinitionId(cardId, cardIndex, variantKey),
        assetName,
        $"Assets/CardBaseEntity/Fixture/{assetName}.asset",
        effectClassName);
}

static void AssertIssue(AssetRegistryMappingReport report, string code, string cardId, int? cardIndex = null)
{
    if (!report.Issues.Any(issue =>
        issue.Code == code
        && issue.Assets.Any(asset =>
            asset.CardId == cardId
            && (cardIndex is null || asset.CardIndex == cardIndex.Value))))
    {
        var indexText = cardIndex is null ? string.Empty : $"#{cardIndex.Value}";
        throw new InvalidOperationException($"Expected asset registry issue '{code}' for {cardId}{indexText}.");
    }
}

static void AssertSequence<T>(IReadOnlyList<T> expected, IReadOnlyList<T> actual)
{
    AssertEqual(expected.Count, actual.Count);

    for (var i = 0; i < expected.Count; i++)
    {
        AssertEqual(expected[i], actual[i]);
    }
}

static void AssertPendingSynchronousCallRollsBack(GameState state, Action<GameState, GameTrace> action)
{
    var beforeHash = state.ComputeStateHash();
    var trace = new GameTrace();

    AssertThrows<DomainException>(() => action(state, trace));
    AssertEqual(beforeHash, state.ComputeStateHash());
    AssertEqual(0, trace.Events.Count);
}

static EngineStepResult ResumeWithDecision(
    EngineSession session,
    EngineStepResult pending,
    SelectionResult selectionResult) =>
    session.Resume(CreateDecisionResult(pending, selectionResult));

static DecisionResult CreateDecisionResult(EngineStepResult pending, SelectionResult selectionResult)
{
    var decisionPoint = pending.PendingDecisionPoint
        ?? throw new InvalidOperationException("Pending step result has no DecisionPoint.");
    var token = pending.PendingDecisionToken
        ?? throw new InvalidOperationException("Pending step result has no DecisionToken.");
    return new DecisionResult(decisionPoint.Player, token, selectionResult);
}

static void AssertReplayDecisionMutationFails(
    BattleEngineServices services,
    GameState initial,
    GameTrace trace,
    Func<DecisionResult, DecisionResult> mutate)
{
    var mutated = new GameTrace(trace.Events.Select(traceEvent =>
    {
        if (traceEvent.Kind != TraceEventKind.Selection || traceEvent.DecisionResult is null)
        {
            return traceEvent;
        }

        var decision = mutate(traceEvent.DecisionResult);
        return traceEvent with
        {
            SelectionResult = decision.SelectionResult,
            DecisionResult = decision,
        };
    }));

    AssertThrows<DomainException>(() => new ReplayRunner(services: services).Replay(initial, mutated));
}

static void AssertThrows<TException>(Action action)
    where TException : Exception
{
    try
    {
        action();
    }
    catch (TException)
    {
        return;
    }

    throw new InvalidOperationException($"Expected exception '{typeof(TException).Name}'.");
}

static string WorkspaceRoot()
{
    var directory = new DirectoryInfo(Environment.CurrentDirectory);
    while (directory is not null)
    {
        if (File.Exists(Path.Combine(directory.FullName, "AGENTS.md"))
            && Directory.Exists(Path.Combine(directory.FullName, "src"))
            && Directory.Exists(Path.Combine(directory.FullName, "DCGO")))
        {
            return directory.FullName;
        }

        directory = directory.Parent;
    }

    throw new InvalidOperationException($"Could not locate workspace root from {Environment.CurrentDirectory}.");
}

static string St1EffectClassName(string cardId) => cardId.Replace("-", "_", StringComparison.Ordinal);

static string RlSt1CardEffectPath(string cardId) =>
    Path.Combine(
        WorkspaceRoot(),
        "src",
        "DCGO.RL.Engine",
        "CardEffects",
        "ST1",
        "Red",
        $"{St1EffectClassName(cardId)}.cs");

static string OriginalSt1CardEffectPath(string cardId) =>
    Path.Combine(
        WorkspaceRoot(),
        "DCGO",
        "Assets",
        "Scripts",
        "CardEffect",
        "ST1",
        "Red",
        $"{St1EffectClassName(cardId)}.cs");

static string StarterEffectClassName(string cardId) => cardId.Replace("-", "_", StringComparison.Ordinal);

static string RlCardEffectPath(string cardId)
{
    var (set, color) = StarterSetPathParts(cardId);
    return Path.Combine(
        WorkspaceRoot(),
        "src",
        "DCGO.RL.Engine",
        "CardEffects",
        set,
        color,
        $"{StarterEffectClassName(cardId)}.cs");
}

static string OriginalCardEffectPath(string cardId)
{
    var (set, color) = StarterSetPathParts(cardId);
    return Path.Combine(
        WorkspaceRoot(),
        "DCGO",
        "Assets",
        "Scripts",
        "CardEffect",
        set,
        color,
        $"{StarterEffectClassName(cardId)}.cs");
}

static string OriginalCardEffectMappingText(string cardId)
{
    var (set, color) = StarterSetPathParts(cardId);
    return $"DCGO/Assets/Scripts/CardEffect/{set}/{color}/{StarterEffectClassName(cardId)}.cs";
}

static (string Set, string Color) StarterSetPathParts(string cardId) =>
    cardId.Split('-', StringSplitOptions.TrimEntries) switch
    {
        ["ST1", _] => ("ST1", "Red"),
        ["ST2", _] => ("ST2", "Blue"),
        ["ST3", _] => ("ST3", "Yellow"),
        _ => throw new InvalidOperationException($"Unsupported starter CardId for structure guard: {cardId}"),
    };

static void AssertRunnableRecordsHaveFiles(IEnumerable<CardEffectPortingRecord> records)
{
    foreach (var record in records
        .Where(record => record.Status is CardEffectPortingStatus.Implemented or CardEffectPortingStatus.NoEffect))
    {
        var path = RlCardEffectPath(record.CardId);
        if (!File.Exists(path))
        {
            throw new InvalidOperationException($"Missing RL.Engine card file for {record.CardId}: {path}");
        }
    }
}

static void AssertSourceMappingDocumented(IEnumerable<CardEffectPortingRecord> records)
{
    foreach (var record in records)
    {
        var originalPath = OriginalCardEffectPath(record.CardId);
        if (!File.Exists(originalPath))
        {
            continue;
        }

        var rlPath = RlCardEffectPath(record.CardId);
        var content = File.ReadAllText(rlPath);
        var expected = OriginalCardEffectMappingText(record.CardId);
        if (!content.Contains(expected, StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{record.CardId} file does not document source mapping '{expected}'.");
        }
    }
}

static void AssertNoEffectProvenanceDocumented(IEnumerable<CardEffectPortingRecord> records)
{
    foreach (var record in records.Where(record => record.Status == CardEffectPortingStatus.NoEffect))
    {
        var originalPath = OriginalCardEffectPath(record.CardId);
        if (File.Exists(originalPath))
        {
            throw new InvalidOperationException($"{record.CardId} is registered as NoEffect but set-local original CardEffect exists: {originalPath}");
        }

        var rlPath = RlCardEffectPath(record.CardId);
        var content = File.ReadAllText(rlPath);
        if (!content.Contains("No original CardEffect source file exists", StringComparison.Ordinal)
            && !content.Contains("No set-local CardEffect source file exists", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{record.CardId} NoEffect file does not document the missing original CardEffect source.");
        }
    }
}

static Dictionary<string, CardEffectPortingStatus> LoadSt1PortingStatusTable()
{
    var path = Path.Combine(WorkspaceRoot(), "docs", "rl-engine", "cardeffect-porting-status.md");
    var statuses = new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal);
    var inSection = false;

    foreach (var line in File.ReadLines(path))
    {
        if (line.StartsWith("## 최신 ST1 상태표", StringComparison.Ordinal))
        {
            inSection = true;
            continue;
        }

        if (inSection && line.StartsWith("## ", StringComparison.Ordinal))
        {
            break;
        }

        if (!inSection || !line.StartsWith("| ST1-", StringComparison.Ordinal))
        {
            continue;
        }

        var columns = line.Split('|', StringSplitOptions.TrimEntries);
        if (columns.Length < 5)
        {
            continue;
        }

        var cardId = columns[1];
        var statusText = columns[3].Replace("`", string.Empty, StringComparison.Ordinal);
        statuses[cardId] = Enum.Parse<CardEffectPortingStatus>(statusText);
    }

    return statuses;
}

static Dictionary<string, CardEffectPortingStatus> LoadCurrentRegistryStatusSnapshot()
{
    var path = Path.Combine(WorkspaceRoot(), "docs", "rl-engine", "cardeffect-porting-status.md");
    var statuses = new Dictionary<string, CardEffectPortingStatus>(StringComparer.Ordinal);
    var inSection = false;

    foreach (var line in File.ReadLines(path))
    {
        if (line.StartsWith("## Current ST1-ST3 Registry Snapshot", StringComparison.Ordinal))
        {
            inSection = true;
            continue;
        }

        if (inSection && line.StartsWith("## ", StringComparison.Ordinal))
        {
            break;
        }

        if (!inSection || !line.StartsWith("| ST", StringComparison.Ordinal))
        {
            continue;
        }

        var columns = line.Split('|', StringSplitOptions.TrimEntries);
        if (columns.Length < 4)
        {
            continue;
        }

        var cardId = columns[1];
        var statusText = columns[2].Replace("`", string.Empty, StringComparison.Ordinal);
        statuses[cardId] = Enum.Parse<CardEffectPortingStatus>(statusText);
    }

    return statuses;
}

static void AssertCardEffectSetPath(string root, string set)
{
    var files = Directory.GetFiles(root, $"{set}_*.cs", SearchOption.AllDirectories);
    if (files.Length == 0)
    {
        throw new InvalidOperationException($"No {set} CardEffect files found under {root}.");
    }

    foreach (var file in files)
    {
        var relative = Path.GetRelativePath(root, file).Replace('\\', '/');
        if (!relative.StartsWith($"{set}/", StringComparison.Ordinal))
        {
            throw new InvalidOperationException($"{set} CardEffect file must live under CardEffects/{set}/...: {relative}");
        }
    }
}

static CardInstanceId AddCardToPlayerZone(GameState state, CardInstanceId cardId, PlayerId owner, Zone zone)
{
    state.Cards.Add(cardId, new CardInstance(cardId, $"BT1-{cardId.Value:000}", owner) { CurrentZone = zone });
    state.GetPlayer(owner).CardsIn(zone).Add(cardId);
    return cardId;
}

static GameState CreateInvariantValidState()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 777 });
    AddCardToPlayerZone(state, new CardInstanceId(401), PlayerId.Player0, Zone.Deck);
    return state;
}

static ICardDatabase CreateSetupCardDatabase()
{
    var mainDeckDefinitions = Enumerable.Range(1, 24)
        .Select(index => new CardDefinition
        {
            CardId = $"BT1-{index:000}",
            Name = $"Setup Card {index}",
            CardKinds = new[] { CardKind.Digimon },
            Colors = new[] { CardColor.Red },
            Level = 3,
            PlayCost = 3,
            Dp = 1000,
        });
    var eggDefinitions = Enumerable.Range(1, 5)
        .Select(index => new CardDefinition
        {
            CardId = $"BT1-E{index:00}",
            Name = $"Setup Egg {index}",
            CardKinds = new[] { CardKind.DigiEgg },
            Colors = new[] { CardColor.Red },
            Level = 2,
        });

    return new InMemoryCardDatabase(mainDeckDefinitions.Concat(eggDefinitions));
}

static string[] St1TargetCardIds() =>
    new[]
    {
        "ST1-01",
        "ST1-02",
        "ST1-03",
        "ST1-04",
        "ST1-05",
        "ST1-06",
        "ST1-07",
        "ST1-08",
        "ST1-09",
        "ST1-10",
        "ST1-11",
        "ST1-12",
        "ST1-13",
        "ST1-14",
        "ST1-15",
        "ST1-16",
    };

static string[] St1UnsupportedCardIds() =>
    Array.Empty<string>();

static string[] St1PartiallyImplementedCardIds() =>
    Array.Empty<string>();

static string[] St2TargetCardIds() =>
    new[]
    {
        "ST2-01",
        "ST2-02",
        "ST2-03",
        "ST2-04",
        "ST2-05",
        "ST2-06",
        "ST2-07",
        "ST2-08",
        "ST2-09",
        "ST2-10",
        "ST2-11",
        "ST2-12",
        "ST2-13",
        "ST2-14",
        "ST2-15",
        "ST2-16",
    };

static string[] St3TargetCardIds() =>
    new[]
    {
        "ST3-01",
        "ST3-02",
        "ST3-03",
        "ST3-04",
        "ST3-05",
        "ST3-06",
        "ST3-07",
        "ST3-08",
        "ST3-09",
        "ST3-10",
        "ST3-11",
        "ST3-12",
        "ST3-13",
        "ST3-14",
        "ST3-15",
        "ST3-16",
    };

static string[] St2St3NoEffectCardIds() =>
    new[]
    {
        "ST2-02",
        "ST2-04",
        "ST2-05",
        "ST2-10",
        "ST3-02",
        "ST3-03",
        "ST3-06",
        "ST3-10",
    };

static string[] St2St3ImplementedCardIds() =>
    new[]
    {
        "ST2-01",
        "ST2-03",
        "ST2-06",
        "ST2-07",
        "ST2-08",
        "ST2-09",
        "ST2-11",
        "ST2-12",
        "ST2-13",
        "ST2-14",
        "ST2-15",
        "ST2-16",
        "ST3-01",
        "ST3-04",
        "ST3-05",
        "ST3-07",
        "ST3-08",
        "ST3-09",
        "ST3-11",
        "ST3-12",
        "ST3-13",
        "ST3-14",
        "ST3-15",
        "ST3-16",
    };

static string[] St2St3UnsupportedCardIds() =>
    St2TargetCardIds()
        .Concat(St3TargetCardIds())
        .Except(St2St3NoEffectCardIds(), StringComparer.Ordinal)
        .Except(St2St3ImplementedCardIds(), StringComparer.Ordinal)
        .ToArray();

static IReadOnlyList<PlayerDeckList> CreateSt1TargetDecks() =>
    new[]
    {
        new PlayerDeckList(
            PlayerId.Player0,
            new[]
            {
                "ST1-01", "ST1-01", "ST1-01", "ST1-01",
                "ST1-16", "ST1-16",
                "ST1-15", "ST1-15",
                "ST1-12", "ST1-12", "ST1-12", "ST1-12",
                "ST1-14", "ST1-14", "ST1-14", "ST1-14",
                "ST1-13", "ST1-13", "ST1-13", "ST1-13",
                "ST1-04", "ST1-04", "ST1-04", "ST1-04",
                "ST1-03", "ST1-03", "ST1-03", "ST1-03",
                "ST1-02", "ST1-02", "ST1-02", "ST1-02",
                "ST1-06", "ST1-06", "ST1-06", "ST1-06",
                "ST1-07", "ST1-07",
                "ST1-05", "ST1-05", "ST1-05", "ST1-05",
                "ST1-09", "ST1-09", "ST1-09", "ST1-09",
                "ST1-08", "ST1-08", "ST1-08", "ST1-08",
                "ST1-11", "ST1-11",
                "ST1-10", "ST1-10",
            }),
    };

static IReadOnlyList<PlayerDeckList> CreateSt1ToSt3TargetDecks() =>
    new[]
    {
        new PlayerDeckList(PlayerId.Player0, St1TargetCardIds().Concat(St2TargetCardIds()).ToArray()),
        new PlayerDeckList(PlayerId.Player1, St3TargetCardIds()),
    };

static ICardDatabase CreateSt1CardDatabase() =>
    new InMemoryCardDatabase(CreateSt1CardDefinitions());

static ICardDatabase CreateSt1ToSt3CardDatabase() =>
    new InMemoryCardDatabase(CreateSt1CardDefinitions().Concat(CreateSt2St3CardDefinitions()));

static BattleEngineServices CreateTestNoEffectServices() =>
    BattleEngineServices.Create(new TestNoEffectCardScriptRegistry());

static RuleProcessor CreateTestRuleProcessor(
    IZoneMover? zoneMover = null,
    RuleProcessorOptions? options = null,
    TriggerPipelineService? triggerPipelineService = null,
    EffectiveStatService? effectiveStats = null)
{
    var mover = zoneMover ?? new ZoneMover();
    var stats = effectiveStats ?? EffectiveStatService.NoContinuous;
    var keywordService = new BattleKeywordService(stats);
    var durationCleanupService = new DurationCleanupService();
    var drawService = new DrawService(mover);
    var phaseRunner = new PhaseRunner(
        drawService,
        keywordService,
        durationCleanupService,
        triggerPipelineService);
    var battleResolver = new BattleResolver(mover, keywordService, stats);

    return new RuleProcessor(
        mover,
        phaseRunner,
        battleResolver,
        keywordService,
        durationCleanupService,
        new EngineInvariantChecker(),
        options,
        triggerPipelineService,
        stats);
}

static ActionExecutor CreateTestActionExecutor() =>
    CreateTestNoEffectServices().ActionExecutor;

static ScriptedScenarioRunner CreateTestScriptedScenarioRunner()
{
    var services = CreateTestNoEffectServices();
    return new ScriptedScenarioRunner(services);
}

static RandomLegalActionRunner CreateTestRandomLegalActionRunner()
{
    var services = CreateTestNoEffectServices();
    return new RandomLegalActionRunner(services);
}

static ReplayDeterminismHelper CreateTestReplayDeterminismHelper()
{
    var services = CreateTestNoEffectServices();
    return new ReplayDeterminismHelper(new ReplayRunner(services: services));
}

static EngineCompletionChecklistRunner CreateTestEngineCompletionChecklistRunner()
{
    var services = CreateTestNoEffectServices();
    var scriptedRunner = new ScriptedScenarioRunner(services);
    var replayScriptedRunner = new ScriptedScenarioRunner(services);

    return new EngineCompletionChecklistRunner(
        scenarioSuiteRunner: new ScenarioSuiteRunner(scriptedRunner),
        replayDeterminismRunner: new ReplayDeterminismRunner(
            replayScriptedRunner,
            new ReplayDeterminismHelper(new ReplayRunner(services: services))),
        invariantFuzzRunner: new InvariantFuzzRunner(new RandomLegalActionRunner(services)));
}

static BattleEngineServices CreatePendingOptionServices() =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-SELECT",
            "FX_Select",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only pending option target.")));

static GameState CreatePendingOptionScenarioState(out CardInstanceId option)
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-SELECT"] = CardEffectTestFixture.OptionEffectDefinition("FX-SELECT", "FX_Select");
    option = AddCardToZone(state, 6290, "FX-SELECT", PlayerId.Player0, Zone.Hand);
    AddBattlePermanent(state, 6291, 891, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    return state;
}

static BattleEngineServices CreateOnceThenSelectionServices() =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new OnceThenSelectionCardScript(
            "FX-ONCE-SELECT",
            "FX_OnceSelect",
            EffectTiming.OnPlay,
            PlayerId.Player1),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only once rollback target.")));

static GameState CreateOnceThenSelectionScenarioState(
    out CardInstanceId card,
    out PermanentState target)
{
    var state = CreateMinimalBattleState();
    state.Memory = 10;
    state.CardDefinitions["FX-ONCE-SELECT"] = CardEffectTestFixture.EffectDefinition("FX-ONCE-SELECT", "FX_OnceSelect") with
    {
        Level = 4,
        PlayCost = 3,
        Dp = 5000,
    };
    card = AddCardToZone(state, 6370, "FX-ONCE-SELECT", PlayerId.Player0, Zone.Hand);
    target = AddBattlePermanent(state, 6371, 970, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    return state;
}

static BattleEngineServices CreateOptionalSelectionServices(
    int tailMemory = 0,
    bool includeSecurityActivation = false,
    IDecisionProvider? decisionProvider = null) =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new OptionalSelectionPrimitiveCardScript(
            "FX-OPT",
            "FX_Optional",
            PlayerId.Player1,
            tailMemory: tailMemory,
            includeSecurityActivation: includeSecurityActivation),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only optional selection target."),
        new NoEffectCardScript("BT1-STRONG", notes: "Test-only optional security attacker.")),
        decisionProvider);

static GameState CreateOptionalSelectionScenarioState(
    string cardId,
    out CardInstanceId option,
    out PermanentState target)
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions[cardId] = CardEffectTestFixture.OptionEffectDefinition(cardId, "FX_Optional");
    option = AddCardToZone(state, 6334, cardId, PlayerId.Player0, Zone.Hand);
    target = AddBattlePermanent(state, 6335, 935, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    return state;
}

static BattleEngineServices CreateStartTurnSelectionServices() =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-START",
            "FX_Start",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnStartTurn),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only start-turn target.")));

static GameState CreateStartTurnSelectionScenarioState(out PermanentState target)
{
    var state = CreateMinimalBattleState();
    state.Phase = Phase.None;
    state.TurnCount = 0;
    state.CardDefinitions["FX-START"] = CardEffectTestFixture.EffectDefinition("FX-START", "FX_Start");
    AddBattlePermanent(state, 6336, 936, "FX-START", PlayerId.Player0, 0, enterTurn: 0);
    target = AddBattlePermanent(state, 6337, 937, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 0);
    return state;
}

static BattleEngineServices CreatePhaseSelectionServices() =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-PHASE",
            "FX_Phase",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.OnStartMainPhase),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only phase target.")));

static GameState CreatePhaseSelectionScenarioState(out PermanentState target)
{
    var state = CreateMinimalBattleState();
    state.Phase = Phase.Breeding;
    state.CardDefinitions["FX-PHASE"] = CardEffectTestFixture.EffectDefinition("FX-PHASE", "FX_Phase");
    AddBattlePermanent(state, 6300, 899, "FX-PHASE", PlayerId.Player0, 0, enterTurn: 1);
    target = AddBattlePermanent(state, 6301, 900, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    return state;
}

static BattleEngineServices CreateRulesTimingSelectionServices() =>
    BattleEngineServices.Create(CardEffectTestFixture.Registry(
        new SelectionPrimitiveCardScript(
            "FX-RULES",
            "FX_Rules",
            SelectionPrimitiveMode.Destroy,
            PlayerId.Player1,
            timing: EffectTiming.RulesTiming),
        new NoEffectCardScript("BT1-FREE", notes: "Test-only no-effect played card."),
        new NoEffectCardScript("BT1-ROOKIE", notes: "Test-only rules timing target.")));

static GameState CreateRulesTimingSelectionScenarioState(
    out CardInstanceId playedCard,
    out PermanentState target)
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["FX-RULES"] = CardEffectTestFixture.EffectDefinition("FX-RULES", "FX_Rules") with
    {
        Level = 3,
        PlayCost = 3,
        Dp = 3000,
    };
    AddBattlePermanent(state, 6317, 916, "FX-RULES", PlayerId.Player0, 0, enterTurn: 1);
    playedCard = AddCardToZone(state, 6318, "BT1-FREE", PlayerId.Player0, Zone.Hand);
    target = AddBattlePermanent(state, 6319, 917, "BT1-ROOKIE", PlayerId.Player1, 0, enterTurn: 1);
    return state;
}

static EngineCompletionRequest CreateSt1EngineCompletionRequest() =>
    new(
        "ST1",
        new TargetCardPoolValidationRequest(
            "ST1",
            CreateSt1TargetDecks(),
            CreateSt1CardDatabase(),
            St1CardScriptCatalog.CreateRegistry()),
        CreateValidationHarnessV1Scenarios(),
        new[]
        {
            new RandomLegalActionRunRequest(
                "ST1 completion minimal fuzz",
                CreateSt1CompletionFuzzState(),
                MaxActions: 8,
                Seed: 1702),
        },
        CreateValidationHarnessV1Scenarios()
            .Where(scenario => scenario.Name is "normal digivolve draw" or "security 0 direct attack win")
            .ToArray());

static IReadOnlyList<CardDefinition> CreateSt1CardDefinitions() =>
    new[]
    {
        St1Definition("ST1-01", "Koromon", CardKind.DigiEgg, level: 2, playCost: -1, dp: 1000, effectClassName: "ST1_01"),
        St1Definition("ST1-02", "Biyomon", CardKind.Digimon, level: 3, playCost: 2, dp: 3000),
        St1Definition("ST1-03", "Agumon", CardKind.Digimon, level: 3, playCost: 2, dp: 2000, effectClassName: "ST1_03"),
        St1Definition("ST1-04", "Dracomon", CardKind.Digimon, level: 3, playCost: 3, dp: 4000),
        St1Definition("ST1-05", "Birdramon", CardKind.Digimon, level: 4, playCost: 3, dp: 4000),
        St1Definition("ST1-06", "Coredramon", CardKind.Digimon, level: 4, playCost: 5, dp: 6000, effectClassName: "ST1_06", battleKeywords: new[] { BattleKeyword.Blocker }),
        St1Definition("ST1-07", "Greymon", CardKind.Digimon, level: 4, playCost: 4, dp: 4000, effectClassName: "ST1_07", securityAttackModifier: 1),
        St1Definition("ST1-08", "Garudamon", CardKind.Digimon, level: 5, playCost: 6, dp: 7000, effectClassName: "ST1_08"),
        St1Definition("ST1-09", "MetalGreymon", CardKind.Digimon, level: 5, playCost: 7, dp: 7000, effectClassName: "ST1_09"),
        St1Definition("ST1-10", "Phoenixmon", CardKind.Digimon, level: 6, playCost: 10, dp: 12000),
        St1Definition("ST1-11", "WarGreymon", CardKind.Digimon, level: 6, playCost: 12, dp: 11000, effectClassName: "ST1_11"),
        St1Definition("ST1-12", "Tai Kamiya", CardKind.Tamer, level: 0, playCost: 2, dp: 0, effectClassName: "ST1_12"),
        St1Definition("ST1-13", "Shadow Wing", CardKind.Option, level: 0, playCost: 1, dp: 0, effectClassName: "ST1_13"),
        St1Definition("ST1-14", "Starlight Explosion", CardKind.Option, level: 0, playCost: 2, dp: 0, effectClassName: "ST1_14"),
        St1Definition("ST1-15", "Giga Destroyer", CardKind.Option, level: 0, playCost: 6, dp: 0, effectClassName: "ST1_15"),
        St1Definition("ST1-16", "Gaia Force", CardKind.Option, level: 0, playCost: 8, dp: 0, effectClassName: "ST1_16"),
    };

static IReadOnlyList<CardDefinition> CreateSt2St3CardDefinitions() =>
    new[]
    {
        StarterDefinition("ST2-01", CardColor.Blue, CardKind.DigiEgg, level: 2, playCost: -1, dp: 1000, effectClassName: "ST2_01"),
        StarterDefinition("ST2-02", CardColor.Blue, CardKind.Digimon, level: 3, playCost: 2, dp: 3000),
        StarterDefinition("ST2-03", CardColor.Blue, CardKind.Digimon, level: 3, playCost: 3, dp: 2000, effectClassName: "ST2_03"),
        StarterDefinition("ST2-04", CardColor.Blue, CardKind.Digimon, level: 3, playCost: 3, dp: 4000),
        StarterDefinition("ST2-05", CardColor.Blue, CardKind.Digimon, level: 4, playCost: 4, dp: 5000),
        StarterDefinition("ST2-06", CardColor.Blue, CardKind.Digimon, level: 4, playCost: 5, dp: 6000, effectClassName: "ST2_06"),
        StarterDefinition("ST2-07", CardColor.Blue, CardKind.Digimon, level: 4, playCost: 5, dp: 6000, effectClassName: "ST1_06", battleKeywords: new[] { BattleKeyword.Blocker }),
        StarterDefinition("ST2-08", CardColor.Blue, CardKind.Digimon, level: 5, playCost: 6, dp: 7000, effectClassName: "ST2_08"),
        StarterDefinition("ST2-09", CardColor.Blue, CardKind.Digimon, level: 5, playCost: 7, dp: 7000, effectClassName: "ST2_09"),
        StarterDefinition("ST2-10", CardColor.Blue, CardKind.Digimon, level: 6, playCost: 10, dp: 12000),
        StarterDefinition("ST2-11", CardColor.Blue, CardKind.Digimon, level: 6, playCost: 12, dp: 11000, effectClassName: "ST2_11"),
        StarterDefinition("ST2-12", CardColor.Blue, CardKind.Tamer, level: 0, playCost: 2, dp: 0, effectClassName: "ST2_12"),
        StarterDefinition("ST2-13", CardColor.Blue, CardKind.Option, level: 0, playCost: 1, dp: 0, effectClassName: "ST2_13"),
        StarterDefinition("ST2-14", CardColor.Blue, CardKind.Option, level: 0, playCost: 2, dp: 0, effectClassName: "ST2_14"),
        StarterDefinition("ST2-15", CardColor.Blue, CardKind.Option, level: 0, playCost: 5, dp: 0, effectClassName: "ST2_15"),
        StarterDefinition("ST2-16", CardColor.Blue, CardKind.Option, level: 0, playCost: 7, dp: 0, effectClassName: "ST2_16"),
        StarterDefinition("ST3-01", CardColor.Yellow, CardKind.DigiEgg, level: 2, playCost: -1, dp: 1000, effectClassName: "ST3_01"),
        StarterDefinition("ST3-02", CardColor.Yellow, CardKind.Digimon, level: 3, playCost: 2, dp: 3000),
        StarterDefinition("ST3-03", CardColor.Yellow, CardKind.Digimon, level: 3, playCost: 3, dp: 2000),
        StarterDefinition("ST3-04", CardColor.Yellow, CardKind.Digimon, level: 3, playCost: 3, dp: 2000, effectClassName: "ST3_04"),
        StarterDefinition("ST3-05", CardColor.Yellow, CardKind.Digimon, level: 4, playCost: 4, dp: 4000, effectClassName: "ST3_05"),
        StarterDefinition("ST3-06", CardColor.Yellow, CardKind.Digimon, level: 4, playCost: 5, dp: 6000),
        StarterDefinition("ST3-07", CardColor.Yellow, CardKind.Digimon, level: 5, playCost: 5, dp: 6000, effectClassName: "ST1_06", battleKeywords: new[] { BattleKeyword.Blocker }),
        StarterDefinition("ST3-08", CardColor.Yellow, CardKind.Digimon, level: 5, playCost: 6, dp: 7000, effectClassName: "ST3_08"),
        StarterDefinition("ST3-09", CardColor.Yellow, CardKind.Digimon, level: 5, playCost: 7, dp: 7000, effectClassName: "ST3_09"),
        StarterDefinition("ST3-10", CardColor.Yellow, CardKind.Digimon, level: 6, playCost: 10, dp: 12000),
        StarterDefinition("ST3-11", CardColor.Yellow, CardKind.Digimon, level: 6, playCost: 12, dp: 11000, effectClassName: "ST3_11"),
        StarterDefinition("ST3-12", CardColor.Yellow, CardKind.Tamer, level: 0, playCost: 2, dp: 0, effectClassName: "ST3_12"),
        StarterDefinition("ST3-13", CardColor.Yellow, CardKind.Option, level: 0, playCost: 1, dp: 0, effectClassName: "ST3_13"),
        StarterDefinition("ST3-14", CardColor.Yellow, CardKind.Option, level: 0, playCost: 2, dp: 0, effectClassName: "ST3_14"),
        StarterDefinition("ST3-15", CardColor.Yellow, CardKind.Option, level: 0, playCost: 6, dp: 0, effectClassName: "ST3_15"),
        StarterDefinition("ST3-16", CardColor.Yellow, CardKind.Option, level: 0, playCost: 7, dp: 0, effectClassName: "ST3_16"),
    };

static CardDefinition St1Definition(
    string cardId,
    string name,
    CardKind kind,
    int level,
    int playCost,
    int dp,
    string effectClassName = "",
    IReadOnlyList<BattleKeyword>? battleKeywords = null,
    int securityAttackModifier = 0) =>
    new()
    {
        CardId = cardId,
        Name = name,
        CardKinds = new[] { kind },
        Colors = new[] { CardColor.Red },
        Level = level,
        PlayCost = playCost,
        Dp = dp,
        CardEffectClassName = effectClassName,
        BattleKeywords = battleKeywords ?? Array.Empty<BattleKeyword>(),
        SecurityAttackModifier = securityAttackModifier,
    };

static CardDefinition StarterDefinition(
    string cardId,
    CardColor color,
    CardKind kind,
    int level,
    int playCost,
    int dp,
    string effectClassName = "",
    IReadOnlyList<BattleKeyword>? battleKeywords = null) =>
    new()
    {
        CardId = cardId,
        Name = cardId,
        CardKinds = new[] { kind },
        Colors = new[] { color },
        Level = level,
        PlayCost = playCost,
        Dp = dp,
        CardEffectClassName = effectClassName,
        BattleKeywords = battleKeywords ?? Array.Empty<BattleKeyword>(),
    };

static GameState CreateSt1ScenarioState()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 1101, FieldSlotCount = 4 });
    state.TurnPlayerId = PlayerId.Player0;
    state.FirstPlayerId = PlayerId.Player0;
    state.TurnCount = 2;
    state.Phase = Phase.Main;
    state.Memory = 0;

    foreach (var definition in CreateSt1CardDefinitions())
    {
        state.CardDefinitions[definition.CardId] = definition;
    }

    return state;
}

static GameState CreateSt2St3ScenarioState()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 2301, FieldSlotCount = 4 });
    state.TurnPlayerId = PlayerId.Player0;
    state.FirstPlayerId = PlayerId.Player0;
    state.TurnCount = 2;
    state.Phase = Phase.Main;
    state.Memory = 0;

    foreach (var definition in CreateSt1CardDefinitions().Concat(CreateSt2St3CardDefinitions()))
    {
        state.CardDefinitions[definition.CardId] = definition;
    }

    return state;
}

static GameState CreateSt1CompletionFuzzState()
{
    var state = CreateMinimalBattleState();
    foreach (var definition in CreateSt1CardDefinitions())
    {
        state.CardDefinitions[definition.CardId] = definition;
    }

    AddBattlePermanent(state, 5251, 551, "ST1-06", PlayerId.Player0, 0, enterTurn: 1);
    AddCardToZone(state, 5252, "ST1-02", PlayerId.Player0, Zone.Hand);
    AddCardToZone(state, 5253, "ST1-04", PlayerId.Player1, Zone.Security, isFaceUp: false);
    return state;
}

static GameState RunSt1ImplementedEffectSequence()
{
    var state = CreateSt1ScenarioState();
    var registry = St1CardScriptCatalog.CreateRegistry();
    var coredramon = AddBattlePermanent(state, 5201, 531, "ST1-06", PlayerId.Player0, 0, enterTurn: 1);
    var sourceHost = AddBattlePermanent(state, 5202, 532, "ST1-11", PlayerId.Player0, 1, enterTurn: 1);
    var metalGreymonSource = AddEvolutionSource(state, 5203, "ST1-09", PlayerId.Player0, sourceHost.Id);
    var primitives = new Tier1PrimitiveService();

    registry.GetScript(state.CardDefinitions["ST1-06"]).Resolve(new CardScriptExecutionContext(
        state,
        new EffectResolution(
            "ST1-06:on-ally-attack:memory-minus-2",
            EffectTiming.OnAllyAttack,
            coredramon.TopCardId,
            coredramon.Id,
            PlayerId.Player0,
            IsBackground: false,
            IsOptional: false,
            new EffectContext(state, EffectTiming.OnAllyAttack, PlayerId.Player0, coredramon.TopCardId, coredramon.Id)),
        primitives));

    registry.GetScript(state.CardDefinitions["ST1-09"]).Resolve(new CardScriptExecutionContext(
        state,
        new EffectResolution(
            "ST1-09:on-block:anyone:memory-plus-3",
            EffectTiming.OnBlockAnyone,
            metalGreymonSource,
            sourceHost.Id,
            PlayerId.Player0,
            IsBackground: false,
            IsOptional: false,
            new EffectContext(state, EffectTiming.OnBlockAnyone, PlayerId.Player0, metalGreymonSource, sourceHost.Id)),
        primitives));

    return state;
}

static EffectResolution CreateSt1OptionResolution(GameState state, string cardId, CardInstanceId source)
{
    var registry = St1CardScriptCatalog.CreateRegistry();
    var script = registry.GetScript(state.CardDefinitions[cardId]);
    var descriptors = script.CreateEffectDescriptors(new CardScriptContext(
        state,
        source,
        SourcePermanent: null,
        Controller: PlayerId.Player0));
    var collected = new TriggerCollector().Collect(
        new EffectContext(state, EffectTiming.OptionSkill, PlayerId.Player0, source),
        descriptors);
    var queue = new EffectQueue();
    queue.EnqueueRange(collected.QueuedEffects);

    AssertEqual(1, queue.Count);
    var resolution = queue.Dequeue();
    AssertTrue(resolution.PendingSelectionRequest is not null);
    AssertTrue(resolution.SelectionContinuation is not null);
    return resolution;
}

static SecurityCheckService CreateSt1SecurityCheckService(IDecisionProvider provider) =>
    new(securityEffectExecutionService: new SecurityEffectExecutionService(
        St1CardScriptCatalog.CreateRegistry(),
        provider));

static SecurityCheckService CreateSt2St3SecurityCheckService(IDecisionProvider provider) =>
    new(securityEffectExecutionService: new SecurityEffectExecutionService(
        St2St3CardScriptCatalog.CreateCombinedWithSt1Registry(),
        provider));

static SelectableTarget PermanentSelectionTarget(PermanentState permanent) =>
    new(
        SelectionTargetKind.Permanent,
        $"permanent:{permanent.Id.Value}",
        permanent.ControllerPlayerId,
        Permanent: permanent.Id,
        Zone: Zone.BattleArea);

static SelectableTarget CardSelectionTarget(
    CardInstanceId card,
    PlayerId owner,
    Zone zone,
    PermanentId? sourcePermanent = null) =>
    new(
        SelectionTargetKind.Card,
        $"card:{card.Value}",
        owner,
        Card: card,
        Permanent: sourcePermanent,
        Zone: zone);

static GameSetupRequest CreateSetupRequest(ulong seed)
{
    var player0Main = Enumerable.Range(1, 24).Select(index => $"BT1-{index:000}").ToArray();
    var player1Main = player0Main.Reverse().ToArray();
    var eggs = Enumerable.Range(1, 5).Select(index => $"BT1-E{index:00}").ToArray();

    return new GameSetupRequest(
        new GameConfig { Seed = seed },
        new[]
        {
            new PlayerDeckList(PlayerId.Player0, player0Main, eggs),
            new PlayerDeckList(PlayerId.Player1, player1Main, eggs),
        },
        firstPlayer: PlayerId.Player0);
}

static GameState CreateMinimalBattleState()
{
    var state = GameState.CreateDefault(new GameConfig { Seed = 2020, FieldSlotCount = 4 });
    state.TurnPlayerId = PlayerId.Player0;
    state.FirstPlayerId = PlayerId.Player0;
    state.TurnCount = 2;
    state.Phase = Phase.Main;
    state.Memory = 5;

    AddBattleDefinition(state, "BT1-WEAK", CardKind.Digimon, level: 3, playCost: 2, dp: 2000);
    AddBattleDefinition(state, "BT1-ROOKIE", CardKind.Digimon, level: 3, playCost: 3, dp: 3000);
    AddBattleDefinition(state, "BT1-TIE", CardKind.Digimon, level: 3, playCost: 3, dp: 4000);
    AddBattleDefinition(state, "BT1-STRONG", CardKind.Digimon, level: 4, playCost: 5, dp: 5000);
    AddBattleDefinition(state, "BT1-COST3", CardKind.Digimon, level: 3, playCost: 3, dp: 3000);
    AddBattleDefinition(state, "BT1-FREE", CardKind.Digimon, level: 3, playCost: 0, dp: 3000);
    AddBattleDefinition(state, "BT1-OPTION", CardKind.Option, level: 0, playCost: 1, dp: 0);
    state.CardDefinitions["BT1-EGG"] = new CardDefinition
    {
        CardId = "BT1-EGG",
        Name = "DigiEgg",
        CardKinds = new[] { CardKind.DigiEgg, CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 2,
        PlayCost = -1,
        Dp = 1000,
    };
    state.CardDefinitions["BT1-CHAMPION"] = new CardDefinition
    {
        CardId = "BT1-CHAMPION",
        Name = "Champion",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        EvoCosts = new[] { new EvoCostDefinition(CardColor.Red, 3, 2) },
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
    };

    return state;
}

static GameState CreateBattleKeywordState()
{
    var state = CreateMinimalBattleState();
    state.CardDefinitions["KW-WEAK"] = KeywordDefinition("KW-WEAK", dp: 2000);
    state.CardDefinitions["KW-ROOKIE"] = KeywordDefinition("KW-ROOKIE", dp: 3000);
    state.CardDefinitions["KW-STRONG"] = KeywordDefinition("KW-STRONG", dp: 7000);
    state.CardDefinitions["KW-SECURITY-DIGIMON"] = KeywordDefinition("KW-SECURITY-DIGIMON", dp: 6000);
    state.CardDefinitions["KW-BLOCKER"] = KeywordDefinition("KW-BLOCKER", dp: 4000, keywords: new[] { BattleKeyword.Blocker });
    state.CardDefinitions["KW-SEC-PLUS"] = KeywordDefinition(
        "KW-SEC-PLUS",
        dp: 5000,
        keywords: new[] { BattleKeyword.SecurityAttack },
        securityAttackModifier: 1);
    state.CardDefinitions["KW-PIERCING"] = KeywordDefinition("KW-PIERCING", dp: 7000, keywords: new[] { BattleKeyword.Piercing });
    state.CardDefinitions["KW-JAMMING"] = KeywordDefinition("KW-JAMMING", dp: 2000, keywords: new[] { BattleKeyword.Jamming });
    state.CardDefinitions["KW-RETALIATION"] = KeywordDefinition("KW-RETALIATION", dp: 2000, keywords: new[] { BattleKeyword.Retaliation });
    state.CardDefinitions["KW-REBOOT"] = KeywordDefinition("KW-REBOOT", dp: 3000, keywords: new[] { BattleKeyword.Reboot });
    state.CardDefinitions["KW-RUSH"] = KeywordDefinition("KW-RUSH", dp: 3000, keywords: new[] { BattleKeyword.Rush });
    state.CardDefinitions["KW-COLLISION"] = KeywordDefinition("KW-COLLISION", dp: 3000, keywords: new[] { BattleKeyword.Collision });
    state.CardDefinitions["KW-DECOY"] = KeywordDefinition("KW-DECOY", dp: 3000, keywords: new[] { BattleKeyword.Decoy });
    return state;
}

static CardDefinition KeywordDefinition(
    string cardId,
    int dp,
    IReadOnlyList<BattleKeyword>? keywords = null,
    int securityAttackModifier = 0) =>
    new()
    {
        CardId = cardId,
        Name = cardId,
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 3,
        PlayCost = 3,
        Dp = dp,
        BattleKeywords = keywords ?? Array.Empty<BattleKeyword>(),
        SecurityAttackModifier = securityAttackModifier,
    };

static GameState CreateComplexMechanicState()
{
    var state = CreateMinimalBattleState();
    AddComplexDefinition(state, "CM-ROOKIE", CardKind.Digimon, level: 3, playCost: 3, dp: 3000, colors: new[] { CardColor.Red });
    AddComplexDefinition(state, "CM-GREEN-L4", CardKind.Digimon, level: 4, playCost: 4, dp: 4000, colors: new[] { CardColor.Green });
    AddComplexDefinition(state, "CM-BLUE-L4", CardKind.Digimon, level: 4, playCost: 4, dp: 4000, colors: new[] { CardColor.Blue });
    AddComplexDefinition(state, "CM-TAMER", CardKind.Tamer, level: 0, playCost: 3, dp: 0, colors: new[] { CardColor.Red });
    AddComplexDefinition(state, "CM-LINK", CardKind.Digimon, level: 3, playCost: 3, dp: 2000, colors: new[] { CardColor.Red });
    AddComplexDefinition(state, "CM-XROS-A", CardKind.Digimon, level: 3, playCost: 3, dp: 2000, colors: new[] { CardColor.Red });
    AddComplexDefinition(state, "CM-XROS-B", CardKind.Digimon, level: 3, playCost: 3, dp: 2000, colors: new[] { CardColor.Blue });
    AddComplexDefinition(state, "CM-ASSEMBLY-A", CardKind.Digimon, level: 5, playCost: 6, dp: 7000, colors: new[] { CardColor.Red });
    AddComplexDefinition(state, "CM-ASSEMBLY-B", CardKind.Digimon, level: 5, playCost: 6, dp: 7000, colors: new[] { CardColor.Blue });

    state.CardDefinitions["CM-JOGRESS"] = new CardDefinition
    {
        CardId = "CM-JOGRESS",
        Name = "Jogress",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.White },
        Level = 5,
        PlayCost = 7,
        Dp = 7000,
        Mechanics = new[] { Mechanic.Jogress },
        EvolutionRequirements = new[]
        {
            new EvolutionRequirement
            {
                Mode = EvolutionMode.Jogress,
                Cost = 0,
                Materials = new[]
                {
                    new MaterialRequirement
                    {
                        Label = "green level 4",
                        CardKinds = new[] { CardKind.Digimon },
                        CardColors = new[] { CardColor.Green },
                        Levels = new[] { 4 },
                        Zones = new[] { Zone.BattleArea },
                    },
                    new MaterialRequirement
                    {
                        Label = "blue level 4",
                        CardKinds = new[] { CardKind.Digimon },
                        CardColors = new[] { CardColor.Blue },
                        Levels = new[] { 4 },
                        Zones = new[] { Zone.BattleArea },
                    },
                },
            },
        },
    };

    state.CardDefinitions["CM-BURST"] = new CardDefinition
    {
        CardId = "CM-BURST",
        Name = "Burst",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
        Mechanics = new[] { Mechanic.BurstDigivolution },
        EvolutionRequirements = new[]
        {
            new EvolutionRequirement
            {
                Mode = EvolutionMode.BurstDigivolution,
                Cost = 1,
                TargetRequirement = new MaterialRequirement
                {
                    Label = "red Digimon",
                    CardKinds = new[] { CardKind.Digimon },
                    CardColors = new[] { CardColor.Red },
                    Zones = new[] { Zone.BattleArea },
                },
                BurstTamerRequirement = new MaterialRequirement
                {
                    Label = "red Tamer",
                    CardKinds = new[] { CardKind.Tamer },
                    CardColors = new[] { CardColor.Red },
                    Zones = new[] { Zone.BattleArea },
                },
            },
        },
    };

    state.CardDefinitions["CM-APPFUSION"] = new CardDefinition
    {
        CardId = "CM-APPFUSION",
        Name = "AppFusion",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 4,
        PlayCost = 5,
        Dp = 6000,
        Mechanics = new[] { Mechanic.AppFusion },
        EvolutionRequirements = new[]
        {
            new EvolutionRequirement
            {
                Mode = EvolutionMode.AppFusion,
                Cost = 2,
                TargetRequirement = new MaterialRequirement
                {
                    Label = "red Digimon",
                    CardKinds = new[] { CardKind.Digimon },
                    CardColors = new[] { CardColor.Red },
                    Zones = new[] { Zone.BattleArea },
                },
                AppFusionLinkRequirement = new MaterialRequirement
                {
                    Label = "linked card",
                    CardKinds = new[] { CardKind.Digimon },
                    Zones = new[] { Zone.LinkedCards },
                },
            },
        },
    };

    state.CardDefinitions["CM-DIGIXROS"] = new CardDefinition
    {
        CardId = "CM-DIGIXROS",
        Name = "DigiXros",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 4,
        PlayCost = 6,
        Dp = 6000,
        Mechanics = new[] { Mechanic.DigiXros },
        PlayRequirements = new[]
        {
            new PlayRequirement
            {
                Mode = PlayMode.DigiXros,
                ReduceCostPerMaterial = 2,
                MaxMaterials = 2,
                Materials = new[]
                {
                    new MaterialRequirement
                    {
                        Label = "xros red",
                        DefinitionIds = new[] { "CM-XROS-A" },
                        AllowHand = true,
                    },
                    new MaterialRequirement
                    {
                        Label = "xros blue",
                        DefinitionIds = new[] { "CM-XROS-B" },
                        AllowTrash = true,
                    },
                },
            },
        },
    };

    state.CardDefinitions["CM-ASSEMBLY"] = new CardDefinition
    {
        CardId = "CM-ASSEMBLY",
        Name = "Assembly",
        CardKinds = new[] { CardKind.Digimon },
        Colors = new[] { CardColor.Red },
        Level = 6,
        PlayCost = 5,
        Dp = 10000,
        Mechanics = new[] { Mechanic.Assembly },
        PlayRequirements = new[]
        {
            new PlayRequirement
            {
                Mode = PlayMode.Assembly,
                ReduceCost = 3,
                Materials = new[]
                {
                    new MaterialRequirement
                    {
                        Label = "assembly red",
                        DefinitionIds = new[] { "CM-ASSEMBLY-A" },
                        Count = 1,
                        AllowTrash = true,
                    },
                    new MaterialRequirement
                    {
                        Label = "assembly blue",
                        DefinitionIds = new[] { "CM-ASSEMBLY-B" },
                        Count = 1,
                        AllowTrash = true,
                    },
                },
            },
        },
    };

    state.CardDefinitions["CM-LINK"] = state.CardDefinitions["CM-LINK"] with
    {
        Mechanics = new[] { Mechanic.Link },
        PlayRequirements = new[]
        {
            new PlayRequirement
            {
                Mode = PlayMode.Normal,
                LinkCost = 2,
                LinkTargetRequirement = new MaterialRequirement
                {
                    Label = "link target",
                    CardKinds = new[] { CardKind.Digimon },
                    Zones = new[] { Zone.BattleArea },
                },
            },
        },
    };

    state.CardDefinitions["CM-DELAY"] = new CardDefinition
    {
        CardId = "CM-DELAY",
        Name = "Delay",
        CardKinds = new[] { CardKind.Option },
        Colors = new[] { CardColor.Red },
        PlayCost = 2,
        Dp = 0,
        Mechanics = new[] { Mechanic.DelayOption },
        PlayRequirements = new[]
        {
            new PlayRequirement
            {
                Mode = PlayMode.DelayOption,
            },
        },
    };

    return state;
}

static void AddComplexDefinition(
    GameState state,
    string cardId,
    CardKind kind,
    int level,
    int playCost,
    int dp,
    IReadOnlyList<CardColor> colors)
{
    state.CardDefinitions[cardId] = new CardDefinition
    {
        CardId = cardId,
        Name = cardId,
        CardKinds = new[] { kind },
        Colors = colors,
        Level = level,
        PlayCost = playCost,
        Dp = dp,
    };
}

static IReadOnlyList<ScriptedScenario> CreateValidationHarnessV1Scenarios()
{
    var deckOutState = CreateMinimalBattleState();
    deckOutState.TurnCount = 2;
    deckOutState.Phase = Phase.Draw;

    var directWinState = CreateMinimalBattleState();
    var directAttacker = AddBattlePermanent(directWinState, 1301, 301, "BT1-STRONG", PlayerId.Player0, 0, enterTurn: 1);

    var lowerDpState = CreateMinimalBattleState();
    var lowerDpAttacker = AddBattlePermanent(lowerDpState, 1311, 311, "BT1-WEAK", PlayerId.Player0, 0, enterTurn: 1);
    var lowerDpDefender = AddBattlePermanent(lowerDpState, 1312, 312, "BT1-STRONG", PlayerId.Player1, 0, enterTurn: 1);

    var equalDpState = CreateMinimalBattleState();
    var equalDpAttacker = AddBattlePermanent(equalDpState, 1321, 321, "BT1-TIE", PlayerId.Player0, 0, enterTurn: 1);
    var equalDpDefender = AddBattlePermanent(equalDpState, 1322, 322, "BT1-TIE", PlayerId.Player1, 0, enterTurn: 1);

    var digivolveState = CreateMinimalBattleState();
    var digivolveTarget = AddBattlePermanent(digivolveState, 1331, 331, "BT1-ROOKIE", PlayerId.Player0, 0, enterTurn: 1);
    var digivolveCard = AddCardToZone(digivolveState, 1332, "BT1-CHAMPION", PlayerId.Player0, Zone.Hand);
    AddCardToZone(digivolveState, 1333, "BT1-OPTION", PlayerId.Player0, Zone.Deck);

    var memoryState = CreateMinimalBattleState();
    memoryState.Memory = 1;
    var memoryCard = AddCardToZone(memoryState, 1341, "BT1-COST3", PlayerId.Player0, Zone.Hand);

    var hatchMoveState = CreateMinimalBattleState();
    hatchMoveState.Phase = Phase.Breeding;
    AddCardToZone(hatchMoveState, 1351, "BT1-EGG", PlayerId.Player0, Zone.DigiEggDeck);

    return new[]
    {
        new ScriptedScenario(
            "deck-out loss",
            deckOutState,
            new ScriptedScenarioStep[] { new DrawPhaseScenarioStep() }),
        new ScriptedScenario(
            "security 0 direct attack win",
            directWinState,
            new[] { new AttackAction(PlayerId.Player0, directAttacker.Id, null) }),
        new ScriptedScenario(
            "lower DP Digimon deleted",
            lowerDpState,
            new[] { new AttackAction(PlayerId.Player0, lowerDpAttacker.Id, lowerDpDefender.Id) }),
        new ScriptedScenario(
            "equal DP battle",
            equalDpState,
            new[] { new AttackAction(PlayerId.Player0, equalDpAttacker.Id, equalDpDefender.Id) }),
        new ScriptedScenario(
            "normal digivolve draw",
            digivolveState,
            new[] { new DigivolveAction(PlayerId.Player0, digivolveCard, digivolveTarget.Id) }),
        new ScriptedScenario(
            "memory crossing turn end",
            memoryState,
            new[] { new PlayCardAction(PlayerId.Player0, memoryCard, 0) }),
        new ScriptedScenario(
            "hatch then move from breeding",
            hatchMoveState,
            new ScriptedScenarioStep[]
            {
                new ActionScenarioStep(new HatchAction(PlayerId.Player0)),
                new ActionScenarioStep(new MoveFromBreedingAction(PlayerId.Player0, new PermanentId(1), 0)),
            }),
    };
}

static void AddBattleDefinition(GameState state, string cardId, CardKind kind, int level, int playCost, int dp)
{
    state.CardDefinitions[cardId] = new CardDefinition
    {
        CardId = cardId,
        Name = cardId,
        CardKinds = new[] { kind },
        Colors = new[] { CardColor.Red },
        Level = level,
        PlayCost = playCost,
        Dp = dp,
    };
}

static CardInstanceId AddCardToZone(
    GameState state,
    int cardInstanceId,
    string definitionId,
    PlayerId owner,
    Zone zone,
    bool isFaceUp = true)
{
    var card = new CardInstanceId(cardInstanceId);
    state.Cards.Add(card, new CardInstance(card, definitionId, owner)
    {
        CurrentZone = zone,
        IsFaceUp = isFaceUp,
    });
    state.GetPlayer(owner).CardsIn(zone).Add(card);
    return card;
}

static PermanentState AddBattlePermanent(
    GameState state,
    int cardInstanceId,
    int permanentId,
    string definitionId,
    PlayerId owner,
    int frame,
    int enterTurn,
    bool isBreeding = false,
    bool isSuspended = false)
{
    var card = new CardInstanceId(cardInstanceId);
    var permanent = new PermanentState(new PermanentId(permanentId), owner, card)
    {
        ControllerPlayerId = owner,
        FrameIndex = frame,
        EnterFieldTurnCount = enterTurn,
        IsBreedingArea = isBreeding,
        IsSuspended = isSuspended,
    };

    state.Cards.Add(card, new CardInstance(card, definitionId, owner)
    {
        CurrentZone = isBreeding ? Zone.BreedingArea : Zone.BattleArea,
        PermanentId = permanent.Id,
    });
    state.GetPlayer(owner).FieldPermanents.Add(permanent);
    return permanent;
}

static CardInstanceId AddLinkedCard(
    GameState state,
    int cardInstanceId,
    string definitionId,
    PlayerId owner,
    PermanentId permanentId)
{
    var permanent = state.GetPlayer(owner).FieldPermanents.First(candidate => candidate.Id == permanentId);
    var card = new CardInstanceId(cardInstanceId);
    permanent.LinkedCards.Add(card);
    state.Cards.Add(card, new CardInstance(card, definitionId, owner)
    {
        CurrentZone = Zone.LinkedCards,
        PermanentId = permanent.Id,
    });
    return card;
}

static CardInstanceId AddEvolutionSource(
    GameState state,
    int cardInstanceId,
    string definitionId,
    PlayerId owner,
    PermanentId permanentId)
{
    var permanent = state.GetPlayer(owner).FieldPermanents.First(candidate => candidate.Id == permanentId);
    var card = new CardInstanceId(cardInstanceId);
    permanent.SourceCardIds.Add(card);
    state.Cards.Add(card, new CardInstance(card, definitionId, owner)
    {
        CurrentZone = Zone.EvolutionSources,
        PermanentId = permanent.Id,
    });
    return card;
}

internal sealed class MutatingSelectionDecisionProvider : IDecisionProvider
{
    private readonly Action<SelectionRequest> _beforeSelection;

    public MutatingSelectionDecisionProvider(Action<SelectionRequest> beforeSelection)
    {
        _beforeSelection = beforeSelection;
    }

    public GameAction ChooseAction(DecisionPoint decisionPoint) =>
        throw new DomainException("MutatingSelectionDecisionProvider only supports selection requests.");

    public SelectionResult ChooseSelection(SelectionRequest request)
    {
        _beforeSelection(request);
        return SelectionResult.ForTargets(request.Id, new[] { request.Candidates[0] });
    }
}

internal sealed class SpyZoneMover : IZoneMover
{
    private readonly ZoneMover _inner = new();
    private readonly List<MoveCardCommand> _moveCardCommands = new();

    public IReadOnlyList<MoveCardCommand> MoveCardCommands => _moveCardCommands;

    public MoveCardResult MoveCard(GameState state, MoveCardCommand command)
    {
        _moveCardCommands.Add(command);
        return _inner.MoveCard(state, command);
    }

    public PermanentZoneMoveResult MovePermanent(GameState state, PermanentZoneMoveCommand command) =>
        _inner.MovePermanent(state, command);

    public DigivolveCardResult DigivolveCard(GameState state, DigivolveCardCommand command) =>
        _inner.DigivolveCard(state, command);
}

internal sealed class TestNoEffectCardScriptRegistry : ICardScriptRegistry
{
    public IReadOnlyCollection<CardEffectPortingRecord> PortingRecords => Array.Empty<CardEffectPortingRecord>();

    public bool TryGetScript(CardDefinition definition, out ICardScript script)
    {
        ArgumentNullException.ThrowIfNull(definition);
        script = new NoEffectCardScript(
            definition.CardId,
            definition.CardEffectClassName,
            "Test-only no-effect registry.");
        return true;
    }

    public ICardScript GetScript(CardDefinition definition)
    {
        TryGetScript(definition, out var script);
        return script;
    }
}
