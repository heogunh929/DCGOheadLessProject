# Full Card Porting L0001 Existing Layer Source Mapping

## 결정

`L0001_existing_layer`는 `done`으로 승격하지 않는다. 이 batch는 "기존 layer로 즉시 구현 후보"로 분류되었지만, DCGO 원본과 현재 RL.Engine을 대조하면 카드별 body를 안전하게 포팅하기 전에 공통 layer가 더 필요하다.

Queue 상태는 `needs-review`로 둔다. affected card를 카드별 workaround나 core `CardId` 분기로 처리하지 않는다.

## Batch 범위

| Section | Name | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- | --- |
| `features` | `declarative` | `Unsupported` | `DCGO/Assets/Scripts/Script/ICardEffect.cs`, `AutoProcessing.cs`, `SelectPermanentEffect.cs`, `TurnStateMachine.cs` | declarative activation boundary 없음 | `needs-review` |
| `keywords` | `Scapegoat` | `Unsupported` | `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Scapegoat.cs` | `BattleKeyword` enum에 없음, replacement/delete prevention layer 없음 | `needs-review` |
| `keywords` | `Training` | `Unsupported` | `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Training.cs` | suspend cost + face-down source add 공통 keyword layer 없음 | `needs-review` |
| `keywords` | `Vortex` | `Unsupported` | `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Vortex.cs`, `CardEffectFactory/VortexCanAttackPlayers.cs` | attack target permission, suspend, optional/cost 경계가 공통 keyword로 없음 | `needs-review` |
| `selections` | `SelectCount` | `PartiallyImplemented` | `DCGO/Assets/Scripts/Script/CardController.cs`, `SelectCountEffect` 호출 card sources | `SelectionKind.SelectCount`는 있으나 source-aligned coroutine/resume 적용 범위 미완성 | `needs-review` |
| `specialMechanics` | `Digisorption` | `PartiallyImplemented` | `BeforePayCost` card effect files, `WhenDigisorption` stack calls | `EffectTiming.WhenDigisorption` enum만 있고 pay-cost reduction layer 미완성 | `needs-review` |
| `timings` | `BeforePayCost` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, 141 card effect files | timing enum은 있으나 play/digivolve cost payment에 cut-in stack이 source-aligned로 연결되어 있지 않음 | `needs-review` |
| `timings` | `AfterPayCost` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, 7 card effect files | timing enum은 있으나 cost payment 이후 cut-in stack boundary 미검증 | `needs-review` |

## Source Mapping Notes

`ICardEffect`는 `IsDeclarative`, `IsOptional`, `IsSkippable`, `IsBackgroundProcess`, `IsInheritedEffect`, `IsLinkedEffect`, `IsCounterEffect`, `PermanentWhenTriggered`, `TopCardWhenTriggered` 같은 상태를 효과 객체에 보존한다. RL.Engine의 trigger stack hardening은 일부 source snapshot을 다루지만, declarative effect의 선언 가능 시점과 UI 선택 흐름은 아직 별도 공통 layer로 매핑되지 않았다.

`CardController`의 cost payment 흐름은 비용 선택 `SelectCountEffect`, `BeforePayCost` 후보 수집, background `BeforePayCost`, cut-in 처리, 비용 지불, `AfterPayCost` stack 처리 순서를 갖는다. 현재 RL.Engine은 `EffectTiming.BeforePayCost`와 `AfterPayCost` enum만으로 이 흐름을 완료했다고 볼 수 없다.

`TrainingEffect`는 자신의 permanent를 suspend한 뒤 deck top을 face-down digivolution card bottom으로 추가한다. 이는 suspend replacement/restriction, face-down source identity, source add event timing을 함께 요구한다.

`Scapegoat`는 deletion replacement 성격이다. 원본은 `WhenPermanentWouldBeDeleted` 조건, 자기 효과로 인한 삭제 제외, 다른 Digimon 삭제 선택, inherited/linked/root effect source를 모두 고려한다. 단순 battle keyword 추가로 대체하면 source 의미가 손실된다.

`Vortex`는 자기 Digimon을 suspend해 attack permission을 만들고, 별도의 static effect로 player attack 가능성을 확장할 수 있다. 공격 target timing queue와 suspend primitive, optional activation이 함께 필요하다.

`Digisorption`은 `BeforePayCost`에서 손패에서 digivolve하려는 카드의 비용을 줄이는 cut-in이다. 원본 card effects는 비용 감소 후 `WhenDigisorption` trigger를 별도로 stack한다. 현재 `WhenDigisorption` enum 존재만으로는 비용 지불 전/후 state machine을 증명하지 못한다.

## Blocker Policy

- source body가 있는 affected cards도 이 batch에서는 카드별 구현을 시작하지 않는다.
- `CardId` 또는 set 전용 분기를 core service, catalog, validator에 추가하지 않는다.
- keyword enum에 이름만 추가해서 validator를 통과시키지 않는다.
- 이 batch의 affected cards는 각 mechanic layer가 별도 queue에서 source-aligned로 구현되기 전까지 full-card completion blocker로 남긴다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch를 진행한다. `L0001_existing_layer` 항목을 재개하려면 최소한 pay-cost cut-in state machine, replacement prevention, source-count selection resume, suspend primitive integration, face-down source movement trace가 먼저 설계되어야 한다.
