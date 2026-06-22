# 66Q generated/runtime status mismatch closure

## 목적

이번 foundation 작업은 generated full-card source scaffold status와 runtime `CardEffectPortingRecord` 상태를 같은 truth table로 오해하던 mismatch gate를 닫는 것이다.
개별 `CardEffect` body를 구현하지 않으며, generated status registry를 수동으로 승격하지 않는다.

## 문제

`docs/generated/full-card-source-scaffold/status-registry.json`은 queue 64 scaffold 정책에 따라 source-bearing scaffold를 전부 `Unsupported`로 둔다.
반면 `src/DCGO.RL.Engine/CardEffects/**/*.cs`에는 Foundation Gate 이전 legacy pilot implementation이 보존되어 있고, 이 중 92개가 `Implemented` 또는 `PartiallyImplemented` runtime record를 가진다.

기존 audit은 이 92개 legacy pilot runtime record를 generated scaffold truth와 직접 비교해 `statusMismatchCount=92`로 계산했다.
이 계산은 mismatch를 잘 드러냈지만, 다음 두 정책을 동시에 표현하지 못했다.

- legacy pilot runtime record는 generated full-card status를 자동 승격하지 않는다.
- generated source scaffold truth와 runtime record의 진짜 충돌은 0이어야 한다.

## 변경

`scripts/generate_capability_truth_audit.py`의 status mismatch report를 `dcgo.status-mismatch-report.66Q.v1`로 갱신했다.

- `statusMismatchCount`는 authoritative generated status claim과 runtime record가 실제로 충돌할 때만 증가한다.
- generated scaffold가 `Unsupported`이고 runtime record가 legacy pilot `Implemented`/`PartiallyImplemented`/`Verified`인 경우는 `legacyPilotDivergences`로 분리한다.
- `legacyPilotDivergences`는 `blocksGeneratedStatusTruth=false`, `statusPromotionAllowed=false`를 명시한다.
- `generatedImplementedOrVerifiedCount`는 0으로 유지한다.

## 결과

- Generated implemented or verified count: 0
- Runtime code porting record count: 148
- Runtime/generated status mismatch count: 0
- Legacy pilot runtime divergence count: 92
- Blocker issue count: 0

## 정책

- legacy pilot runtime record는 OpenCode/full-card generated status truth가 아니다.
- generated source scaffold status는 자동 승격하지 않는다.
- `Verified` 승격은 실제 engine implementation, 실행 테스트, replay/invariant evidence가 모두 있을 때만 허용한다.
- 이 작업은 C0039 이후 card-porting batch를 실행하지 않는다.

## 다음 foundation 작업

다음 foundation queue는 `ICannotIgnoreDigivolutionConditionEffect` 대응 restriction layer다.
`ContinuousOrStaticEffect`는 player-wide/granted trigger keyword flow도 남아 있으므로 계속 `PartiallyImplemented`다.
