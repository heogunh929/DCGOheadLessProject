# Full Card Porting Batches - Queue 66

66번은 62~65 산출물에서 전체 카드풀 porting subqueue를 생성한 작업이다. 이 queue는 전체 카드 구현을 직접 수행하지 않고, mechanic-layer, card-porting, source-review batch를 dependency-aware 순서로 나누는 역할만 한다.

## Summary

- Batch count: 432
- Mechanic-layer batches: 12
- Mechanic-remediation batches: 8
- Card-porting batches: 397
- Source-review batches: 14
- Source scaffolds assigned: 3918 / 3918
- NeedsSourceReview cards assigned: 40 / 40

## Category Counts

- `attack-security-timing`: 56
- `continuous-duration-inherited-linked`: 23
- `draw-search-reveal-hidden`: 1
- `existing-layer`: 4
- `high-risk-source-review`: 14
- `mechanic-remediation`: 9
- `replacement-counter-cut-in`: 50
- `simultaneous-trigger-priority`: 1
- `special-digivolution-play`: 176
- `zone-security-recovery`: 98

## Generated Control Files

- `docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md`
- `docs/codex-prompts/prompts/generated/full-card/*.md`

Machine-readable manifest: `docs/generated/full-card-porting-batches-66.json`

## Queue 66B Capability Truth Audit

66B 이후 full-card-porting은 mechanic-first remediation으로 전환한다. `docs/generated/capability-truth-audit/` 아래에 capability registry, source required capability 목록, card-batch blocker 계산, status mismatch report를 생성했다.

- `Verified`는 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 허용한다.
- 문서 문자열, enum 존재, queue `done` 표기만으로는 `Verified` 처리하지 않는다.
- L0005/L0006 `OnDraw` 충돌은 `PartiallyImplemented`로 보수 분류하며 full-card gate에서 숨기지 않는다.
- 현재 `CardScriptRegistry`의 CardId/effect-class lookup은 `CardId#CardIndex@VariantKey` identity 정책과 맞지 않으므로 blocker로 유지한다.
- `C0039_zone_security_recovery`는 실행 가능 후보가 아니다. `66C`, `66D`, `66E`가 먼저 `todo`로 배치되어 card-porting batch로 건너뛰지 않게 한다.

## Queue 66A Dependency-Aware Scheduler Policy

`scripts/select_next_full_card_porting_batch.py`는 generated subqueue에서 다음 실행 가능 batch를 계산한다. 이 helper는 queue를 수정하지 않고 다음 실행 후보와 skipped 이유만 보고한다.

- `dependencyBatchIds`가 모두 `done`인 `todo` batch만 실행 가능하다.
- dependency가 `blocked` 또는 `needs-review`이면 dependent card-porting batch를 실행하지 않는다.
- 공통 layer 미구현 상태는 `needs-review`가 아니라 `blocked`다.
- `needs-review`는 실제 사용자 판단 또는 source body/source 의미 불명확성에만 사용한다.
- blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
- card-porting 완료 조건은 실제 effect body 구현, registry/status 갱신, 테스트, baseline blocker 감소다.

## Queue 66C Runtime Status Variant Registry

66C는 runtime `CardScriptRegistry`를 `CardId#CardIndex@VariantKey` identity와 연결했다. `CardDefinition`, `CardEffectPortingRecord`, `AssetRegistryMappingValidator`가 같은 stable id 정책을 공유하며, indexed record가 존재하는 카드의 indexed definition은 legacy CardId fallback으로 평탄화하지 않는다.

- `ST3-02#76@base`와 `ST3-02#77@P1`은 explicit `NoEffect` record다.
- `ST3-02#4977@P2`는 source body 미확인 `Unsupported` record로 분리했다.
- `CardScriptRegistry`는 exact definition id를 먼저 보고, safe legacy CardId fallback과 effect-class alias를 그 뒤에 적용한다.
- `status-mismatch-report.json`의 `CardIdOnlyRuntimeRegistry` blocker는 해소되었지만 generated full-card status registry와 code porting record mismatch는 남아 있다.
- 다음 queue는 `66D_card_effect_capability_dependency_graph`이며 C0039는 여전히 실행하지 않는다.

## Queue 66D Capability Dependency Graph

66D는 source effect별 `requiredCapabilities`를 card-porting batch execution gate에 연결했다. `docs/generated/capability-truth-audit/capability-dependency-graph-66D.json`은 capability registry, source required capability, batch blocker, full-card batch manifest를 결합한 machine-readable graph다.

- card-porting batch는 모든 required capability가 `Verified`일 때만 executable이다.
- coarse category dependency 또는 `dependencyBatchIds`가 done이라는 이유만으로 card-porting batch를 실행하지 않는다.
- 현재 397개 card-porting batch는 모두 unresolved capability 때문에 blocked이며, `C0039_zone_security_recovery`도 executable이 아니다.
- selector는 card-porting row의 graph entry가 없거나 `isExecutable=false`이면 `blocked-capability`를 반환한다.
- 다음 queue는 `66E_mechanic_first_goal_scheduler`다.

## Queue 66E Mechanic-First Scheduler

66E는 `docs/generated/capability-truth-audit/mechanic-first-scheduler-66E.json`을 생성하고 selector를 mechanic-first remediation으로 전환한다.

- blocked card batch는 다음 card batch로 넘어가지 않는다.
- selector는 `C0039_zone_security_recovery`를 실행하지 않고 `mechanic-remediation` decision을 반환한다.
- 현재 가장 많은 card definitions를 막는 unresolved capability는 `ContinuousOrStaticEffect`다.
- card-porting batch `done` 조건은 actual effect body, registry/status 갱신, tests, replay, baseline blocker 감소를 모두 요구한다.
- mechanic 구현 후 관련 card batches를 다시 `todo`로 여는 후속 queue가 필요하다.

## Queue 66F Continuous/Static Source Scope

66F는 `ContinuousOrStaticEffect` remediation의 첫 source-scope 구현 항목이다.

- `ContinuousEffectSourceCollector`가 linked card와 face-up security card를 수집한다.
- face-down security는 원본과 같이 continuous/static source로 수집하지 않는다.
- 기존 field top / inherited source 수집은 유지된다.
- `ContinuousOrStaticEffect` 전체는 아직 `PartiallyImplemented`이며 `Verified`가 아니다.
- 남은 범위는 player-level runtime static effects, hand/trash/executing static requirement source, digivolution requirement static effects, trait/name/text metadata, replay/parity evidence다.

## Queue 66G Player Runtime Static Modifier Scope

66G는 원본 `Player.EffectList(EffectTiming.None)` 중 현재 RL.Engine이 `TemporaryModifier` 상태 모델로 대응하는 player-level runtime stat modifier 범위를 검증한 항목이다.

- player-target DP modifier는 owner battle area Digimon에만 적용되고 breeding/opponent에는 적용되지 않는다.
- player-target SecurityAttack modifier는 owner Digimon 전체에 적용된다.
- player-target DP/SecurityAttack/SecurityDigimonDP modifier는 `GameState.Clone`, `RestoreFrom`, `ComputeStateHash`, `ReplayRunner` final state hash에 보존된다.
- `ContinuousOrStaticEffect` 전체는 아직 `PartiallyImplemented`이며 `Verified`가 아니다.
- hand/trash/executing source collection은 66H에서 보강됐지만, 남은 범위는 digivolution/link requirement static effects, trait/name/text metadata, unsupported static effect interfaces, generated full-card parity evidence다.

## Queue 66H Hand/Trash/Executing Static Source Scope

66H는 hand, trash, executing player-zone card가 명시적으로 guard된 continuous/static source로 수집될 수 있는 경계를 추가한 항목이다.

- `ContinuousEffectSourceKind.Hand`, `Trash`, `Executing`을 추가했다.
- `ContinuousEffectSourceCollector`가 각 player의 hand/trash/executing list를 deterministic order로 순회한다.
- player-zone source는 `SourcePermanent = null`, `Controller = CardInstance.Owner`로 전달된다.
- 기존 field top / inherited / linked / face-up security scripts는 `SourceKind` guard를 통해 오작동하지 않는다.
- `ContinuousOrStaticEffect` 전체는 아직 `PartiallyImplemented`이며 `Verified`가 아니다.
- 남은 범위는 digivolution/link requirement static effects, trait/name/text metadata, unsupported static effect interfaces, generated full-card parity evidence다.

## Queue 66I Continuous/Static Keyword Descriptor Scope

66I는 원본 `CardEffectFactory/KeyWordEffects`의 source/condition-aware static keyword grant를 공통 descriptor 경계로 연결한 항목이다.

- `ContinuousKeywordDescriptor`와 `IContinuousKeywordCardScript`를 추가했다.
- `ContinuousEffectSourceCollector`가 stat descriptor와 같은 source scope에서 keyword descriptor를 수집한다.
- `BattleKeywordService.HasKeyword`와 `EnsureSupportedKeywords`가 descriptor 기반 keyword를 반영한다.
- fixture tests는 field top Blocker, inherited source 이동, condition gate, replay determinism을 검증한다.
- `ContinuousOrStaticEffect`는 아직 `PartiallyImplemented`다. digivolution/link requirement static effects, trait/name/text metadata, cost/restriction/immunity static interfaces, trigger형 keyword flow, generated full-card parity evidence가 남아 있다.

## Queue 66J Static Requirement Descriptor Scope

66J는 원본 `AddDigivolutionRequirement`와 `AddLinkRequirement`의 source/condition-aware static requirement를 공통 descriptor 경계로 연결한 항목이다.

- `StaticEvolutionRequirementDescriptor`와 `StaticLinkRequirementDescriptor`를 추가했다.
- `StaticRequirementService`는 `ContinuousEffectSourceCollector`의 source scope를 공유한다.
- `LegalActionGenerator`, `DigivolveService`, `ComplexMechanicService`, `RandomLegalActionRunner`가 production graph에서 같은 requirement service를 사용한다.
- static digivolution/link requirement는 action generation, execution, replay determinism fixture로 검증했다.
- `ContinuousOrStaticEffect`는 아직 `PartiallyImplemented`다. trait/name/text metadata, cost/restriction/immunity static interfaces, ignore-digivolution-permission semantics, trigger형 keyword flow, generated full-card parity evidence가 남아 있다.

## Current Dependency State

`L0006_zone_security_recovery`는 2026-06-22에 `done`으로 처리되었다. 다음 full-card-porting 실행 시에는 selector 결과를 따른다. selector가 `C0026_zone_security_recovery`를 `executable`로 반환하더라도, C0026은 별도 사용자 요청에서 한 queue 항목으로만 진행한다.

Update 2026-06-22: `L0006_zone_security_recovery`는 `done` 처리했다. 이후 `C0026_zone_security_recovery`를 별도 queue 항목으로 진행했고, `BT18_080` runnable body와 `BT18_085` continuous partial body를 추가했다. 나머지 source bodies는 공통 layer blocker가 필요하므로 C0026은 `blocked`로 유지한다.

다음 selector 실행 때 `blocked` 또는 `needs-review` dependency가 있는 dependent card batch는 건너뛴다. card-porting batch는 실제 effect body, registry/status 갱신, 테스트, baseline blocker 감소 없이는 `done` 처리하지 않는다.
