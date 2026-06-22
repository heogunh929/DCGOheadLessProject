# 66D Capability Dependency Graph

## 결정

`66D_card_effect_capability_dependency_graph`는 source effect별 `requiredCapabilities`를 card-porting batch execution gate에 연결한 mechanic-remediation 항목이다. 이 작업은 card effect body를 포팅하지 않았고, `C0039_zone_security_recovery`를 실행 가능 후보로 만들지 않았다.

## Graph

machine-readable graph는 `docs/generated/capability-truth-audit/capability-dependency-graph-66D.json`에 생성한다.

- 입력 capability registry: `docs/generated/capability-truth-audit/capability-registry.json`
- 입력 source capability: `docs/generated/capability-truth-audit/source-required-capabilities.json`
- 입력 batch blocker: `docs/generated/capability-truth-audit/batch-capability-blockers.json`
- 입력 batch manifest: `docs/generated/full-card-porting-batches-66.json`

## Gate 정책

- card-porting batch는 source effect의 모든 required capability가 `Verified`일 때만 executable이다.
- category 또는 `dependencyBatchIds`가 done이라는 이유만으로 card-porting batch를 실행하지 않는다.
- blocker 문서화만으로 card-porting batch를 완료 처리하지 않는다.
- `C0039_zone_security_recovery`는 `L0006_zone_security_recovery` dependency가 done이어도 unresolved capability가 있으므로 blocked다.

## 현재 결과

- source effect count: 3918
- source effects with non-verified capabilities: 3918
- card-porting batch count: 397
- blocked card-porting batch count: 397
- executable card-porting batch count: 0
- C0039 executable: false

## Scheduler 연결

`scripts/select_next_full_card_porting_batch.py`는 `docs/generated/capability-truth-audit/capability-dependency-graph-66D.json`을 읽는다. card-porting row를 만났을 때 graph entry가 없거나 `isExecutable=false`이면 `blocked-capability`로 반환하며, coarse category dependency만으로 실행하지 않는다.

다음 queue는 `66E_mechanic_first_goal_scheduler`다. 66E는 blocked card batch를 다음 card batch로 건너뛰지 않고, blocker capability별 affected card 수를 기준으로 다음 mechanic 구현 대상을 선택해야 한다.
