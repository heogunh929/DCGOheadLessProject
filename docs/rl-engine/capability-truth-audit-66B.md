# 66B Capability Truth Audit

## 결정

`66B_capability_truth_audit`는 mechanic-first remediation 전환을 위해 수행한 감사 항목이다. C0039 이후 card-porting batch는 실행 가능 후보로 선택하지 않는다.

## 상태 정책

- 상태 값은 `Unsupported`, `PartiallyImplemented`, `Verified`만 사용한다.
- `Verified`는 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 부여한다.
- 문서 문자열, enum 존재, queue `done` 표기만으로는 `Verified` 처리하지 않는다.
- 공통 layer 미구현은 `needs-review`가 아니라 blocker 성격의 `Unsupported` 또는 `PartiallyImplemented`로 분류한다.

## 핵심 발견

- Capability total: 161
- Status counts: {'PartiallyImplemented': 83, 'Unsupported': 69, 'Verified': 9}
- Documentation conflicts: 12
- Source effects with non-verified capabilities: 3918 / 3918
- Blocked card batches: 397 / 397
- Status mismatches: 92

## OnDraw 충돌

`OnDraw`는 L0005에서 blocked로 남아 있고 L0006에서는 pending rule event coverage로 구현되었다고 기록되어 있다. 66B 최종 분류는 다음과 같다.

- Final status: `PartiallyImplemented`
- Reason: L0005 keeps OnDraw blocked while L0006 documents partial pending-rule coverage; draw primitive selection/replay boundary still needs a focused mechanic gate.
- L0006의 coverage는 실제 구현/테스트 근거로 인정하지만, L0005의 draw primitive selection-aware boundary blocker를 숨기지 않는다.

## C0039 실행 가능성

- C0039 executable: `false`
- Blocking capability count: 21
- C0039는 66B 이후 실행 후보가 아니다. 66C, 66D, 66E가 runtime status/variant registry와 capability dependency graph를 정렬하기 전에는 card-porting batch로 넘어가지 않는다.

## Runtime Registry Status

- `CardScriptRegistry`는 exact `DefinitionStableId` lookup을 우선하고 legacy CardId fallback을 단일 정의 경로로 제한한다.
- full-card identity policy `CardId#CardIndex@VariantKey`와 runtime registry lookup 경계가 연결되었다.
- generated status registry와 실제 `CardEffectPortingRecord` 불일치는 여전히 남아 있으며 66D/66E의 capability dependency gate에서 계속 차단된다.

## Machine-Readable Outputs

- `docs/generated/capability-truth-audit/capability-registry.json`
- `docs/generated/capability-truth-audit/source-required-capabilities.json`
- `docs/generated/capability-truth-audit/batch-capability-blockers.json`
- `docs/generated/capability-truth-audit/status-mismatch-report.json`

## 다음 계획

- `66C_runtime_status_variant_registry_integration`: 실제 `ICardScript` `PortingRecord`를 전체 status registry와 validator에 연결하고 variant-aware runtime registry를 도입한다.
- `66D_card_effect_capability_dependency_graph`: 각 source effect의 required capability를 batch scheduler가 직접 사용하게 한다.
- `66E_mechanic_first_goal_scheduler`: blocked card batch를 우회하지 않고 가장 많은 카드를 막는 unresolved mechanic을 다음 구현 대상으로 선택한다.
