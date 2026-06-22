# RUN_NEXT_FOUNDATION_COMPLETION

Foundation Completion Program만 현재 활성 goal로 사용한다.

## 현재 정책

- 한 번에 foundation 작업 하나만 수행한다.
- 개별 `CardEffect` body 신규 구현과 C0039 이후 card-porting batch 실행은 금지한다.
- `DCGO/Assets` 원본은 읽기 전용 Source of Truth로 취급한다. 현재 git worktree에는 `DCGO/Assets`가 없지만, local read-only 원본은 `E:\headlessDCGO\DCGO\Assets`에서 확인할 수 있다.
- 작업 종료 후 `scripts/calculate_foundation_completion_gate.py --workspace .`로 Foundation Completion Gate를 다시 계산한다.
- commit/push는 수행하지 않고 추천 commit message만 보고한다.

## 완료한 최근 foundation 작업

- 66AE static cannot-move return-to-hand scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `Permanent.CanMove`, `ICanNotMoveEffect`, `CanNotMoveClass`, `EX7_014`, `CanNotReturnToHand`, `CannotReturnToHand` 흐름을 확인했다.
  - `StaticCardRestrictionKind.CannotMove`를 추가하고 `Tier1PrimitiveService.ReturnPermanentToHand(...)` / `ReturnPermanentToHandWithEvents(...)`가 return-to-hand 이동 전에 static restriction gate를 확인하게 했다.
  - `ReturnPermanentToHandWithEvents(...)`는 would-return rule event를 큐잉하기 전에 차단되므로 실패 시 pending event를 남기지 않는다.
  - generated registry를 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66AD static cannot-put-field scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardSource.CanEnterField`, `ICanNotPutFieldEffect`, `CanNotPutFieldClass`, `BT14_017`, `BT20_020` 흐름을 확인했다.
  - `StaticCardRestrictionKind.CannotPutField`와 `StaticCardRestrictionCause`를 추가해 hand permanent play, DigiXros/Assembly/DelayOption field entry, effect-caused primitive field entry가 같은 static restriction gate를 사용하게 했다.
  - generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66AC static cannot-play option scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardSource.CanNotPlayThisOption`, `ICanNotPlayCardEffect`, `CanNotPlayClass`, `CardEffectCommons`, `EX1_072` 흐름을 확인했다.
  - `StaticCardRestrictionDescriptor`와 `StaticCardRestrictionKind.CannotPlay`를 추가해 option play legal action generation과 `PlayCardService` direct execution이 같은 static restriction gate를 사용하게 했다.
  - generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66AB static link cost modifier scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardSource.GetChangedLinkCost`, `IChangeLinkCostEffect`, `ChangeLinkCostClass`, `CardEffectFactory.KeyWordEffects.Link`, `CardController` 흐름을 확인했다.
  - `StaticCostKind.Link` modifier를 `CostResolver.ResolveLink(...)`와 `ComplexMechanicService.ExecuteLink`에 연결해 static link requirement 비용 지급이 shared `StaticEffectService` cost graph를 사용하게 했다.
  - generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66AA static link effective gate scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `AddLinkRequirement`, `CardEffectInterfaces`, `CardSource`, `Permanent` 흐름을 확인했다.
  - `StaticRequirementService` link evaluation과 `ComplexMechanicService` legal action/execution이 같은 `StaticEffectService` effective metadata/level query를 공유하게 했다.
  - generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66Z static level requirement scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardSource.TreatedLevel`, `Permanent.Level`, `ChangeCardLevelClass`, `ChangePermanentLevelClass`, `CardEffectInterfaces` 흐름을 확인했다.
  - `StaticCardLevelDescriptor`, `StaticPermanentLevelDescriptor`를 추가해 normal digivolution 및 static evolution requirement level gate가 effective permanent level을 공유하게 했다.
  - base `CardDefinition.Level` 값은 직접 수정하지 않고, generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66Y static card metadata requirement scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardSource.BaseCardNames`, `CardSource.CardNames`, `CardSource.CardTraits`, `ChangeBaseCardNameClass`, `ChangeCardNamesClass`, `ChangeTraitsClass`, `CardEffectInterfaces` 흐름을 확인했다.
  - `StaticCardNameDescriptor`, `StaticCardTraitDescriptor`, `CardMetadataSnapshot`을 추가해 static cost/restriction/immunity/color/ignore-color criteria와 static evolution requirement criteria가 effective name/trait metadata를 볼 수 있게 했다.
  - base `CardDefinition` name/trait 값은 직접 수정하지 않고, generated registry는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66X static card color requirement scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `ChangeCardColorClass`, `ChangeBaseCardColorClass`, `IgnoreColorConditionClass`, `CardEffectInterfaces`, `CardSource` 흐름을 확인했다.
  - `StaticCardColorDescriptor`와 `IgnoreColorRequirementDescriptor`를 추가해 option play color gate와 digivolution color requirement가 effective color를 공유하게 했다.
  - `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이며, full-card source parity evidence가 `NotRun`이므로 `Verified`로 승격하지 않는다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않는다.
- 66W OnEnterFieldAnyone payload scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`에서 `CardController`, `CardEffectFactory`, `ICardEffect`, `PermanentEnterField`, `AD1_001` 흐름을 확인했다.
  - `OnEnterFieldAnyone`는 `OnPlay`/`WhenDigivolving`으로 전부 평탄화할 수 없으므로, RL.Engine에는 `EnterFieldEventPayload`와 prepared trigger sequence tail을 추가했다.
  - play/digivolve 완료 뒤 self timing group 다음에 global `OnEnterFieldAnyone` group이 이어지고, selection pause/resume 뒤에도 tail group이 실행되는 foundation test를 추가했다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66V EffectTiming.None continuous/static alias scope
  - local read-only source root `E:\headlessDCGO\DCGO\Assets`를 inventory generator가 재현 가능하게 읽도록 source-root fallback과 manifest fingerprint 검증을 추가했다.
  - 원본 `EffectTiming.None`은 모든 generated source record에서 `static_or_continuous=true`와 함께 나타나므로 별도 capability가 아니라 `ContinuousOrStaticEffect` alias로 집계한다.
  - `None`은 `source-required-capabilities`와 unsupported capability blocker에서 제거되고, full mechanic inventory에는 `PartiallyImplemented` timing alias 근거로만 남는다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66U foundation gate blocked empty descriptor scope
  - Foundation Gate의 `blocked empty descriptor` scan이 continuous/static 전용 legacy partial script를 실제 hidden unsupported 후보로 세던 false positive를 분리했다.
  - `blockedEmptyDescriptorCount=0`, `legacyContinuousOnlyEmptyDescriptorCount=13`으로 재계산되며 legacy partial은 status 승격 근거로 쓰지 않는다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66T temporary granted trigger scope
  - 원본 `AddEffectToPermanent` / `AddEffectToPlayer` duration list가 timing 있는 trigger effect factory를 보존하는 흐름을 RL.Engine의 `TemporaryGrantedEffect`로 연결했다.
  - `TemporaryGrantedEffectRegistry`와 `TriggerPipelineService`가 duration-bound granted trigger descriptor/source timing/body hook을 처리한다.
  - state hash, rule-visible snapshot, cleanup, invariant, replay evidence가 granted trigger payload를 공유한다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66S player-wide keyword grant scope
  - 원본 `GainRushPlayerEffect` / `GainBlockerPlayerEffect` 계열 player duration keyword 의미를 RL.Engine의 `TemporaryModifierKind.Keyword` player-target aura로 연결했다.
  - `TemporaryModifier.TargetMetadataCriteria`, `Tier1PrimitiveService.AddTemporaryPlayerKeyword(...)`, `BattleKeywordService.HasKeyword(...)`를 통해 metadata-gated battle-area Digimon keyword grant를 보존한다.
  - state hash, rule-visible snapshot, invariant, replay evidence가 player-wide keyword payload를 공유한다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66R cannot-ignore digivolution restriction scope
  - 원본 `ICannotIgnoreDigivolutionConditionEffect`를 `CannotIgnoreDigivolutionRequirementDescriptor`와 `StaticRequirementService` 평가 경로에 연결했다.
  - `IgnoreDigivolutionRequirement=true` static evolution requirement는 cannot-ignore restriction이 적용되면 legal action과 execution path 모두에서 제외된다.
  - field top source와 `AnyPlayer` 대상 restriction을 지원해 BT8_059 계열 "Players can't ignore digivolution requirements" 의미를 표현할 수 있다.
  - generated registry를 `Verified`로 승격하지 않았고, 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66Q generated/runtime status mismatch closure
  - `docs/generated/capability-truth-audit/status-mismatch-report.json`을 `dcgo.status-mismatch-report.66Q.v1`로 갱신했다.
  - authoritative generated/runtime `statusMismatchCount`는 0으로 닫았다.
  - 기존 92건은 legacy pilot runtime divergence로 분리해 공개하고, `statusPromotionAllowed=false`로 고정했다.
  - generated full-card status registry는 `Implemented` 또는 `Verified`로 승격하지 않았다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66P full-card parity evidence
  - `scripts/generate_full_card_parity_evidence.py`로 generated full-card source scaffold 3918개 source effect를 expected Unity/RL parity fixture 및 comparison report 경로에 매핑했다.
  - 현재 fixture/report가 없으므로 coverage는 Passed 0, Failed 0, NotRun 3918이다.
  - `NotRun`은 parity pass가 아니며, 이 evidence는 card-porting, generated/runtime status 승격, `Verified` 승격을 허용하지 않는다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66O capability truth evidence refresh scope
  - `scripts/generate_capability_truth_audit.py`는 66K~66N 구현 상태를 `ContinuousOrStaticEffect` partial evidence에 반영한다.
  - metadata criteria, static cost/restriction/immunity, ignore-digivolution permission, target permanent temporary keyword grant evidence를 generated `capability-registry.json`에 반영했다.
  - `ContinuousOrStaticEffect`는 `Verified`로 승격하지 않고 `PartiallyImplemented`로 유지한다.
  - generated/runtime status mismatch는 66Q에서 authoritative count 0으로 닫혔지만, C0039 card-porting blocker는 계속 남아 있다.
  - 개별 `CardEffect` body와 C0039 이후 card-porting은 수행하지 않았다.
- 66N temporary keyword grant scope
  - `TemporaryModifierKind.Keyword`와 `TemporaryModifier.Keyword` payload로 target permanent duration-bound battle keyword를 보존한다.
  - `Tier1PrimitiveService.AddTemporaryKeyword(...)`, `BattleKeywordService.HasKeyword(...)`, state hash, rule-visible snapshot, invariant checker가 temporary keyword payload를 공유한다.
  - local source `E:\headlessDCGO\DCGO\Assets`에서 `GainBlocker`, `GainRush`, `GainJamming`, `GainReboot`, `GainCollision`, `GainRetaliation`, `AddEffectToPermanent` 흐름을 확인했다.
- 66M ignore-digivolution-permission semantics
  - `StaticEvolutionRequirementDescriptor.IgnoreDigivolutionRequirement`를 static evolution requirement fallback의 permission semantics로 연결했다.
  - `BattleRules.CanDigivolve`, `LegalActionGenerator`, `DigivolveService`는 기존 static requirement 경로를 공유한다.

## 다음 foundation 작업

현재 gate/scheduler 기준 다음 foundation 후보는 사전에 `ContinuousOrStaticEffect`와 관련된 공통 layer다.
우선순위는 카드별 body 구현이 아니라 공통 API와 검증 기반이다.

1. 2026-06-23 gate 재계산 결과 selected foundation capability는 `ContinuousOrStaticEffect` (`PartiallyImplemented`)다.
2. static cannot-move return-to-hand foundation 후에도 full-card source parity evidence가 아직 `NotRun`이고, unknown common API 27개, Unsupported capability 26개, PartiallyImplemented capability 37개가 남아 `Verified`로 승격하지 않는다.
3. `OpenCodeReady=false`, passed gate 11, failed gate 3이며 C0039 이후 card-porting은 계속 금지한다.

작업 후 `docs/generated/foundation-completion-gate.json`과 `docs/rl-engine/foundation-completion-gate.md`를 확인한다.
