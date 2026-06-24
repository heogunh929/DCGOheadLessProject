# FND-001-A EffectTiming.None Alias Verification

## 목적

`FND001-CS-02`는 원본 `EffectTiming.None`을 별도 unsupported capability로 계산하지 않고 `ContinuousOrStaticEffect`의 alias로만 회계 처리하는지 검증한다.
이 작업은 CardEffect body 구현이나 generated status 승격이 아니라, 이미 존재하는 source-aligned alias 정책을 독립 evidence로 고정하는 범위다.

## Source Of Truth

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`

원본에서 `EffectTiming.None`은 "효과 없음"으로 버리는 값이 아니라 `EffectList(EffectTiming.None)`로 조회되는 static/continuous channel이다.

## 추가한 검증

- `scripts/verify_fnd001_none_alias.py`
- `docs/generated/as-is-restart/fnd-001-a-none-alias-verification.json`

검증 조건:

- `capability-registry.json`에 raw `None` capability entry가 없어야 한다.
- `ContinuousOrStaticEffect.inventoryAliases`에만 `None`이 있어야 한다.
- `source-required-capabilities.json`의 `requiredCapabilities`와 `nonVerifiedCapabilities`에 raw `None`이 없어야 한다.
- `batch-capability-blockers.json`의 `blockingCapabilities`에 raw `None`이 없어야 한다.
- Foundation Gate sample의 unsupported/partial capability 목록에 raw `None`이 없어야 한다.
- `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지되어야 한다.
- `OpenCodeReady=false` 상태를 유지해야 한다.

## 결과

- `FND001-CS-02`: `ClosedByEvidence`
- raw `None` capability entry count: 0
- `ContinuousOrStaticEffect` alias owner count: 1
- source-required raw `None` count: 0
- source non-verified raw `None` count: 0
- batch blocker raw `None` count: 0
- gate unsupported/partial raw `None` count: 0

## 검증

- `python scripts\verify_fnd001_none_alias.py --workspace .`
- `.\.dotnet\dotnet.exe build .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj`
- `.\.dotnet\dotnet.exe run --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj` -> `All 634 tests passed.`

## 비범위

- `src/DCGO.RL.Engine` 구현 코드 수정 없음
- 원본 `DCGO/Assets` 수정 없음
- 개별 `CardEffect` body 구현 없음
- C0039 이후 card-porting 없음
- Foundation Gate 수치 조작 없음
- generated status 승격 없음

## 다음 후보

다음 CloseableFoundationTask 후보는 `FND001-CS-03 continuous/static source collector scope parity`다.
