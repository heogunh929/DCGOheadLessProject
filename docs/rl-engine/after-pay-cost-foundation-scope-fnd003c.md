# FND-003-C AfterPayCost Foundation Scope

> 이 문서는 `FND-003-C`에서 처리한 `AfterPayCost` foundation 변경 범위와 남은 범위를 기록한다. CardEffect body 구현, C0039 이후 card-porting, generated status 승격은 수행하지 않았다.

## Source Evidence

- AS-IS root: `E:/headlessDCGO/DCGO`
- enum evidence: `E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1016` `EffectTiming.AfterPayCost`
- runtime call evidence: `E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardController.cs:985`
- payload evidence: `E:/headlessDCGO/DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:179` `WouldEnterFieldHashtable(...)`
- sample source effects:
  - `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs`
  - `DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs`
  - `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs`
  - `DCGO/Assets/Scripts/CardEffect/EX1/Green/EX1_033.cs`
  - `DCGO/Assets/Scripts/CardEffect/ST12/Red/ST12_15.cs`

원본 `CardController`는 `BeforePayCost` cut-in과 cost 확정 후 실제 memory를 지불한 다음 `EffectTiming.AfterPayCost` cut-in을 실행한다. 샘플 source effect들은 주로 `BeforePayCost`에서 임시 cost modifier를 등록하고, `AfterPayCost`에서 해당 임시 modifier를 제거하는 구조다.

## Implemented Scope

- `PlayCardService` 일반 permanent play 경로에서 memory 지불 직후 `AfterPayCost` trigger pipeline을 실행한다.
- `PlayCardService` option hand play 경로에서 memory 지불 후 option을 `Executing` zone으로 옮기고, `OptionSkill`보다 먼저 `AfterPayCost` trigger pipeline을 실행한다.
- `DigivolveService` 일반 digivolve 경로에서 memory 지불 직후, digivolve 이동과 draw 전에 `AfterPayCost` trigger pipeline을 실행한다.
- `CostPaymentRuleEventPayload`를 추가해 `PayCost`, `Card`, `CardSources`, `Root`, `SourceZone`, `isEvolution`, `isJogress`, `Permanents`, `PaidCost`, `MemoryBeforeCost`, `MemoryAfterCost`, `CostTransactionId`를 공통 payload로 보존한다.
- `AfterPayCost`가 selection continuation을 요구하면 현재 play/digivolve continuation 경계가 없으므로 명시적으로 `UnsupportedMechanicException`을 발생시킨다.

## Deferred Scope

- `BeforePayCost`와 동일한 cost transaction id를 실제 pre-cost modifier 선택/정산 경로까지 연결하는 작업은 `FND-003-N BeforePayCost`에서 처리한다.
- Jogress, Burst, App Fusion, DigiXros, Assembly, Link, DelayOption 등 `ComplexMechanicService` cost-payment 경로의 `AfterPayCost` parity는 별도 trace-aware follow-up으로 남긴다.
- `AfterPayCost` selection pause/resume을 play/digivolve action 중간 continuation으로 지원하는 작업은 별도 action continuation 설계가 필요하다.
- generated capability status는 `Unsupported`에서 승격하지 않았다.

## Test Evidence

- 추가 테스트: `Fnd003CAfterPayCostRunsAfterCostPayment`
- 검증 범위:
  - 일반 play: 비용 지불 후 `AfterPayCost` 실행, `SourceZone=Hand`
  - option play: 비용 지불 후 `Executing` 이동, `OptionSkill` 전 `AfterPayCost` 실행, `SourceZone=Executing`
  - digivolve: 비용 지불 후 digivolve 이동 전 `AfterPayCost` 실행, target permanent payload 보존
- 검증 명령: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 613 tests passed.`

## Foundation Gate

- 재계산 명령: `python scripts\\calculate_foundation_completion_gate.py --workspace .`
- 결과: `OpenCodeReady=false`, passed gate 11, failed gate 3
- unsupported capability count 26, partially implemented capability count 37은 직접 조작하지 않았고 그대로 유지됐다.

## Next

1. `FND-003-D OnDiscardSecurity`를 같은 방식으로 source contract, runtime integration, test candidate 순서로 처리한다.
2. `ComplexMechanicService` cost-payment 계열의 `AfterPayCost` 적용 범위는 `BeforePayCost`/complex mechanic follow-up에서 분리해 처리한다.
3. TRUST-001-RERUN에서 `AfterPayCost` 공통 boundary를 `VerifiedCandidateNeedsTest` 또는 `ReuseCandidate` 후보로 재평가한다.
