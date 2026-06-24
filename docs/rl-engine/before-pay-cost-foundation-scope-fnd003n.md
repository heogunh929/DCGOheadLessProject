# FND-003-N BeforePayCost Foundation Scope

## 범위

`BeforePayCost`는 카드 play/digivolution 비용을 실제로 지불하기 전에 발동 후보와 background effect를 처리하는 foundation runtime hook이다. 이번 작업은 개별 `CardEffect` body를 구현하지 않고, 원본 DCGO의 pre-cost trigger 위치와 payload를 headless runtime에 연결하는 데 한정한다.

## 원본 근거

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:604-621`: play/pay-cost flow에서 `BeforePayCost` 후보를 수집하고, hand/trash/security/permanent가 아닌 경우 자기 카드의 후보도 함께 수집한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:609`: foreground cut-in 전에 `ActivateBackgroundEffects(... BeforePayCost)`가 실행된다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:709-711`: 수집한 `BeforePayCost` 후보를 cut-in stack에 올린다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:970-985`: pre-cost 처리가 끝난 뒤 최종 비용을 산정하고 memory를 지불한 다음 `AfterPayCost`로 넘어간다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs:2641`: `HasDigisorption`이 `EffectTiming.BeforePayCost` effect list를 확인한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs:770-1045`: field, inherited, linked, hand, trash, face-up security 등 source sweep와 background activation 근거를 제공한다.

## Headless 반영

- `PlayCardService`는 일반 permanent play와 option play 비용 지불 전에 `BeforePayCost` pipeline을 실행한다.
- `DigivolveService`는 일반 digivolution 비용 지불 전에 `BeforePayCost` pipeline을 실행한다.
- `TriggerPipelineOptions.ExecuteBackgroundEffectsFirst`를 추가해 `BeforePayCost`에서만 background effect가 foreground cut-in보다 먼저 실행되는 순서를 opt-in으로 표현한다.
- `CostPaymentRuleEventPayload.CreateBeforePayCost(...)`는 `PayCost`, `Card`, `Cards`, `CardSources`, `Root`, `SourceZone`, `isEvolution`, `isJogress`, `Permanents`, `Cost`, `BaseCost`, `MemoryBeforeCost`, `CostTransactionId`, `CostKind`를 제공한다.
- pre-cost pipeline 이후 play/digivolution legality와 비용을 다시 확인해, pre-cost effect가 상태를 바꾼 경우 비용 지불 전 검증이 다시 적용되도록 했다.

## 검증

- `FND-003-N BeforePayCost runs before play and option cost payment`
- `FND-003-N BeforePayCost background effects run before cut-in`
- `FND-003-N BeforePayCost runs before digivolution cost payment`
- Full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 629 tests passed.`

## 남은 범위

- `BeforePayCost` 중 selection이 발생한 경우 비용 지불 전 continuation은 아직 explicit unsupported다.
- DigiXros, Assembly, Burst, AppFusion, Jogress, Digisorption 특수 비용 지불 flow는 별도 follow-up이다.
- card-specific `BeforePayCost` body wiring은 이번 범위 밖이다.
- strict `PutStackedSkill` stack object parity와 max-payable memory/material-selection policy는 추가 source mapping이 필요하다.

## 금지 유지

- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- Foundation Gate 수치 조작 없음.
- generated status 승격 없음.
- 원본 `DCGO` 수정 없음.
