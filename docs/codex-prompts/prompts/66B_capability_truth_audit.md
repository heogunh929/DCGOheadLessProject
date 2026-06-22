# 66B_capability_truth_audit

현재 `GOAL_FULL_CARD_PORTING_BATCHES` 실행을 중단하고 mechanic-first remediation으로 전환한다.

## 목표

- 전체 mechanic inventory, `L0001`~`L0006`, `C0007`~`C0038` blocker 문서, 실제 engine code와 tests를 대조한다.
- 같은 capability가 문서마다 blocked/implemented로 충돌하는 경우를 찾는다.
- capability 상태는 `Unsupported`, `PartiallyImplemented`, `Verified`만 사용한다.
- `Verified`는 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 허용한다.
- machine-readable capability registry, source required capability 목록, batch blocker 계산, status mismatch report를 생성한다.
- `CardId` 전용 runtime registry와 `CardId#CardIndex@VariantKey` identity 정책 불일치를 blocker로 보고한다.
- `C0039_zone_security_recovery`는 실행 가능 상태로 선택하지 않는다.

## 산출물

- `docs/generated/capability-truth-audit/capability-registry.json`
- `docs/generated/capability-truth-audit/source-required-capabilities.json`
- `docs/generated/capability-truth-audit/batch-capability-blockers.json`
- `docs/generated/capability-truth-audit/status-mismatch-report.json`
- `docs/rl-engine/capability-truth-audit-66B.md`

## 금지

- `DCGO/Assets` 원본을 수정하지 않는다.
- core service에 CardId 분기를 추가하지 않는다.
- RL Environment, Observation, Reward, Dataset, Trainer를 구현하지 않는다.
- C0039 이후 card-porting batch를 실행하지 않는다.
