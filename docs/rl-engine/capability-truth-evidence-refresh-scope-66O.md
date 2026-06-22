# 66O capability truth evidence refresh scope

이번 작업은 Foundation Completion Program의 generated capability truth evidence를 최신 foundation 구현 상태에 맞춰 갱신한 범위다. 개별 `CardEffect` body는 구현하지 않았고, C0039 이후 card-porting batch도 실행하지 않았다.

## 입력과 기준

사용한 입력:

- `docs/generated/full-mechanic-inventory.json`
- `docs/generated/full-card-source-scaffold/status-registry.json`
- `docs/generated/full-card-source-scaffold/sources/*.json`
- `docs/generated/full-card-porting-batches-66.json`
- `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`
- `src/DCGO.RL.Engine/CardEffects/**/*.cs`
- `src/DCGO.RL.Engine.Tests/Program.cs`

이 작업은 원본 source body를 새로 해석하거나 카드별 구현 상태를 승격하지 않는다. original source는 기존 generated inventory와 66K~66N source mapping 문서가 가진 Source of Truth 경계를 그대로 사용한다. local source root는 계속 `E:\headlessDCGO\DCGO\Assets`이며 읽기 전용이다.

## 변경 내용

- `scripts/generate_capability_truth_audit.py`의 `ContinuousOrStaticEffect` partial evidence를 66K~66N 구현에 맞게 갱신했다.
- outdated implementation evidence path인 `src/DCGO.RL.Engine/Mechanics/BattleKeywordService.cs`를 실제 경로 `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`로 정리했다.
- complex mechanic verified evidence path를 존재하지 않는 개별 service 파일 대신 `src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs`로 정리했다.
- `ContinuousOrStaticEffect` evidence에 다음 범위를 추가했다.
  - trait/name/text metadata criteria
  - static cost/restriction/immunity descriptor evaluation
  - ignore-digivolution permission semantics
  - target permanent temporary keyword grants
  - temporary keyword replay/invariant evidence
- generated `capability-registry.json`과 `capability-truth-audit-66B.md`를 재생성했다.
- `CapabilityTruthAudit registry is conservative` 테스트가 새 evidence를 직접 검증하도록 보강했다.

## 유지한 제한

- `ContinuousOrStaticEffect`는 계속 `PartiallyImplemented`다.
- `Verified` 승격은 하지 않았다.
- generated full-card status registry와 runtime `CardEffectPortingRecord` mismatch는 92건으로 남아 있다.
- `C0039_zone_security_recovery`는 실행 가능 batch가 아니다.
- generated registry를 수동 조작하지 않았다. 산출물은 `scripts/generate_capability_truth_audit.py --workspace .`로 재생성했다.

## 검증

Targeted:

- `CapabilityTruthAudit`: `All 3 tests passed.`

Full regression:

- `All 560 tests passed.`

Foundation Completion Gate 재계산:

- `openCodeReady=false`
- `passedGateCount=9`
- `failedGateCount=5`
- `unknownCommonApiCount=39`
- `unsupportedCapabilityCount=37`
- `partiallyImplementedCapabilityCount=27`
- `runtimeGeneratedStatusMismatchCount=92`
- `silentNoOpCount=0`
- `blockedEmptyDescriptorCount=13`
- `falseNoEffectCount=0`
- `variantIdentityConflictCount=0`
- `coreCardIdBranchCount=0`
- `directZoneMutationCount=0`
- `sourceLockChangedCount=0`
- `selectedNextFoundationCapability=ContinuousOrStaticEffect`
- `selectedNextFoundationStatus=PartiallyImplemented`
- `localSourceRootAvailable=false`

## 남은 범위

다음 foundation 항목은 여전히 card body 구현이 아니라 공통 foundation과 검증 기반이다.

- generated full-card parity evidence
- generated/runtime status mismatch closure
- `ICannotIgnoreDigivolutionConditionEffect` 대응 restriction layer
- player-wide/granted trigger keyword flow
