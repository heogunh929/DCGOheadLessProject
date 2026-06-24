# FND-001-CS-03 Continuous/Static Source Collector Scope

## 목적

`FND001-CS-03`은 `ContinuousOrStaticEffect`가 partial로 남은 이유 중 source collector 범위를 분리한다.
이번 범위는 source kind coverage를 evidence로 고정하는 것이며, full-card Unity/RL parity 또는 `AutoProcessing` strict ordering parity를 닫지 않는다.

## Source Of Truth

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`

원본 `AutoProcessing.GetSkillInfos`는 player effect, field permanent, trash, hand, face-up security source를 수집한다.
원본 `Permanent.EffectList_ForCard`는 top card, inherited source, linked card를 `IsInheritedEffect`, `IsLinkedEffect`, `IsLinked`, face-down 여부로 분리한다.
`Executing` source는 원본의 executing area와 option/security execution flow에서 쓰이며, strict ordering parity는 별도 source mapping으로 남긴다.

## 추가한 검증

- `scripts/verify_fnd001_source_collector_scope.py`
- `docs/generated/as-is-restart/fnd-001-cs-03-source-collector-scope-verification.json`

검증 조건:

- 원본 source surface 5개가 source kind 관련 패턴을 모두 가진다.
- headless `ContinuousEffectSourceKind`가 `FieldTop`, `InheritedSource`, `LinkedCard`, `FaceUpSecurity`, `Hand`, `Trash`, `Executing`을 모두 가진다.
- `ContinuousEffectSourceCollector.EnumerateSources`가 7개 source kind를 모두 열거한다.
- collector가 continuous/statics descriptor 계열 수집 메서드에서 같은 source enumeration을 사용한다.
- script runtime source role validation이 inherited, linked, face-up security, hand, trash, executing, field top을 재검증한다.
- 기존 테스트 후보 10개가 존재한다.
- full-card scaffold fixture 후보가 source kind별로 연결되지만 coverage는 `NotRun`으로 유지된다.
- `OpenCodeReady=false`, `ContinuousOrStaticEffect:PartiallyImplemented`를 유지한다.

## 결과

- `FND001-CS-03`: `ClosedByEvidence`
- source kind coverage: 7 / 7
- source surface coverage: 5 / 5
- test candidate coverage: 10 / 10
- fixture candidate source effects: 4
- full-card parity: `NotRun 3918`, `Passed 0`

## 남긴 경계

- strict Unity source ordering parity는 닫지 않았다.
- `AutoProcessing.GetSkillInfos`, `StackSkillInfos`, `ActivateBackgroundEffects`, cut-in/background ordering은 `FND001-CS-14`에서 다룬다.
- executable full-card Unity/RL fixture/report 생성은 `PARITY-001`에서 다룬다.
- 이 evidence는 generated status를 `Implemented` 또는 `Verified`로 승격하지 않는다.

## 검증

- `python scripts\verify_fnd001_source_collector_scope.py --workspace .`
- `python scripts\verify_fnd001_source_collector_scope.py --workspace . --check`
- `.\.dotnet\dotnet.exe build .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj`
- `.\.dotnet\dotnet.exe run --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj`
- `python scripts\calculate_foundation_completion_gate.py --workspace .`

## 비범위

- `src/DCGO.RL.Engine` 구현 코드 수정 없음
- 원본 `DCGO/Assets` 수정 없음
- 개별 `CardEffect` body 구현 없음
- C0039 이후 card-porting 없음
- Foundation Gate 수치 조작 없음
- generated status 승격 없음

## 다음 후보

다음 CloseableFoundationTask 후보는 `FND001-CS-04 duration bucket cleanup/provider integration parity`다.
