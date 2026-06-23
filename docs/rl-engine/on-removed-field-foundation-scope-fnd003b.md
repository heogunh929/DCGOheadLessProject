# FND-003-B OnRemovedField Foundation Scope

> 이 문서는 `FND-003-B`에서 처리한 `OnRemovedField` foundation 변경의 source-aligned 범위와 남은 범위를 기록한다. CardEffect body 구현, C0039 이후 card-porting, generated status 승격은 수행하지 않았다.

## Source Evidence

- AS-IS root: `E:/headlessDCGO/DCGO`
- enum evidence: `E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1028` `EffectTiming.OnRemovedField`
- runtime call evidence: `E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardObjectController.cs:524`
- sample source effects:
  - `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_007.cs`
  - `DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_010.cs`

원본 `CardObjectController.RemoveField`는 `WhenPermanentWouldRemoveFieldCheckHashtable(...)` payload를 만들고 `EffectTiming.OnRemovedField`로 stack 처리한다. 따라서 `OnRemovedField`는 `WhenRemoveField` alias가 아니라 별도 timing으로 유지해야 한다.

## Implemented Scope

- `Tier1PrimitiveService.DestroyPermanent(...)`가 제거 대상 permanent의 stack을 trash로 보내기 전에 `EffectTiming.OnRemovedField` pending rule event를 enqueue한다.
- payload는 원본 hashtable 축인 `Permanents`, `CardEffect`, `battle`, `digixros`와 headless 식별자인 `Permanent`, `RemovedPermanent`, `RemovedController`, `RemovedTopCard`, `Cards`, `CardSources`, `SourceZone`, `DestinationZone`, `MoveReason`을 함께 보존한다.
- linked card가 있는 permanent는 기존 unsupported failure를 유지하고, 실패 전에 `OnRemovedField` event를 enqueue하지 않는다.
- `WhenRemoveField`와 `OnDestroyedAnyone` timing은 변경하지 않았다.

## Deferred Scope

- `BattleResolver` 직접 전투 파괴 경로는 이번 scope에서 제외했다. 해당 경로는 action trace가 rule processing 전에 기록되는 구조와 맞물리므로, 별도 trace-aware foundation task에서 다뤄야 한다.
- source card/source permanent가 있는 effect-caused destroy payload 확장은 다음 primitive/source-context goal에서 다룬다.
- generated capability status는 `Unsupported`에서 승격하지 않았다.

## Test Evidence

- 추가 테스트: `Fnd003BOnRemovedFieldQueuesExplicitRemovalEvent`
- 검증 명령: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 612 tests passed.`

## Foundation Gate

- 재계산 명령: `python scripts\\calculate_foundation_completion_gate.py --workspace .`
- 결과: `OpenCodeReady=false`, passed gate 11, failed gate 3
- unsupported capability count 26, partially implemented capability count 37은 직접 조작하지 않았고 그대로 유지됐다.

## Next

1. `FND-003-C AfterPayCost`를 같은 방식으로 source contract, runtime integration, test candidate 순서로 처리한다.
2. `OnRemovedField`의 battle resolver 직접 경로는 trace-aware follow-up으로 분리한다.
3. TRUST-001-RERUN에서 이번 primitive scope를 `VerifiedCandidateNeedsTest` 또는 `ReuseCandidate` 후보로 다시 평가한다.
