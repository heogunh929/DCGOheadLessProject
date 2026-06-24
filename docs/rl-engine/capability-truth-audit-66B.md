# 66B Capability Truth Audit

## 결정

`66B_capability_truth_audit`는 mechanic-first remediation 전환을 위해 수행한 감사 항목이다. C0039 이후 card-porting batch는 실행 가능 후보로 선택하지 않는다.

## 상태 정책

- 상태 값은 `Unsupported`, `PartiallyImplemented`, `Verified`만 사용한다.
- `Verified`는 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 부여한다.
- 문서 문자열, enum 존재, queue `done` 표기만으로는 `Verified` 처리하지 않는다.
- 공통 layer 미구현은 `needs-review`가 아니라 blocker 성격의 `Unsupported` 또는 `PartiallyImplemented`로 분류한다.

## 핵심 발견

- Capability total: 160
- Status counts: {'PartiallyImplemented': 121, 'Unsupported': 30, 'Verified': 9}
- Documentation conflicts: 12
- Source effects with non-verified capabilities: 3918 / 3918
- Blocked card batches: 397 / 397
- Status mismatches: 0
- Legacy pilot runtime divergences: 92

## OnDraw 충돌

`OnDraw`는 L0005에서 blocked로 남아 있고 L0006에서는 pending rule event coverage로 구현되었다고 기록되어 있다. 66B 최종 분류는 다음과 같다.

- Final status: `PartiallyImplemented`
- Reason: L0005 keeps OnDraw blocked while L0006 documents partial pending-rule coverage; draw primitive selection/replay boundary still needs a focused mechanic gate.
- L0006의 coverage는 실제 구현/테스트 근거로 인정하지만, L0005의 draw primitive selection-aware boundary blocker를 숨기지 않는다.

## OnEnterFieldAnyone Evidence

`OnEnterFieldAnyone`는 self `OnPlay`/`WhenDigivolving` branch와 global enter-field branch가 같은 timing에서 공존하는 원본 흐름을 반영해야 한다. 66W는 payload foundation을 연결했지만 full source ordering parity가 끝난 것은 아니므로 partial로 유지한다.

- Final status: `PartiallyImplemented`
- Reason: PlayCardService and DigivolveService now chain a source-wide OnEnterFieldAnyone payload after existing self OnPlay/WhenDigivolving groups. Source ordering parity, multi-permanent payloads, and all enter-field variants remain incomplete.
- Implementation evidence count: 13
- Test evidence count: 3
- Remaining blockers: multi-permanent enter payload, Jogress/DigiXros/Assembly variants, and complete source ordering parity.

## ContinuousOrStaticEffect Evidence

`ContinuousOrStaticEffect`는 66F~66X foundation evidence를 machine-readable audit에 반영하지만, 아직 `Verified`로 승격하지 않는다.

- Final status: `PartiallyImplemented`
- Reason: Continuous descriptors support field top, inherited, linked, face-up security, hand, trash, and executing source scopes; metadata criteria cover trait/name/text source and target gates; continuous static keyword descriptors cover source/condition-aware supported BattleKeyword grants; StaticRequirementService covers source/condition-aware static digivolution/link requirements, ignore-digivolution permission semantics, cannot-ignore digivolution restriction descriptors, effective card colors and effective permanent levels for digivolution requirements, effective link requirement metadata/level gates through ComplexMechanicService, static link cost modifiers through CostResolver, and effective metadata criteria where a StaticEffectService is available; StaticEffectService covers static cost/restriction/immunity descriptor evaluation, static card play restriction descriptors for option play gates, static card put-field restriction descriptors for permanent field-entry gates, static card move restriction descriptors for return-to-hand gates, effective card/base/current color descriptors, effective card name/trait metadata descriptors, effective card/permanent level descriptors, and ignore color requirements for option play. TemporaryModifier covers supported player-level DP/SecurityAttack/SecurityDigimonDP runtime stat effects, target permanent temporary keyword grants, and player-wide temporary keyword grants with metadata-gated battle-area Digimon targets. TemporaryGrantedEffect covers duration-bound granted trigger source/timing descriptors through the trigger pipeline. Full-card parity evidence is generated conservatively as NotRun, and generated/runtime status mismatch is closed by separating legacy pilot divergence from generated source truth. The capability remains partial until remaining full-card continuous/static variants have source-locked parity evidence.
- Implementation evidence count: 19
- Test evidence count: 48
- Replay/invariant evidence count: 10
- Remaining blockers: full-card continuous/static variants still need source-locked parity evidence before `Verified` promotion.

## C0039 실행 가능성

- C0039 executable: `false`
- Blocking capability count: 20
- C0039는 66B 이후 실행 후보가 아니다. 66C, 66D, 66E가 runtime status/variant registry와 capability dependency graph를 정렬하기 전에는 card-porting batch로 넘어가지 않는다.

## Runtime Registry Status

- `CardScriptRegistry`는 exact `DefinitionStableId` lookup을 우선하고 legacy CardId fallback을 단일 정의 경로로 제한한다.
- full-card identity policy `CardId#CardIndex@VariantKey`와 runtime registry lookup 경계가 연결되었다.
- legacy pilot `CardEffectPortingRecord` divergence는 별도 공개 카운트로 남기며 generated source scaffold status를 자동 승격하지 않는다.

## Machine-Readable Outputs

- `docs/generated/capability-truth-audit/capability-registry.json`
- `docs/generated/capability-truth-audit/source-required-capabilities.json`
- `docs/generated/capability-truth-audit/batch-capability-blockers.json`
- `docs/generated/capability-truth-audit/status-mismatch-report.json`

## 다음 계획

- `66C_runtime_status_variant_registry_integration`: 실제 `ICardScript` `PortingRecord`를 전체 status registry와 validator에 연결하고 variant-aware runtime registry를 도입한다.
- `66D_card_effect_capability_dependency_graph`: 각 source effect의 required capability를 batch scheduler가 직접 사용하게 한다.
- `66E_mechanic_first_goal_scheduler`: blocked card batch를 우회하지 않고 가장 많은 카드를 막는 unresolved mechanic을 다음 구현 대상으로 선택한다.
