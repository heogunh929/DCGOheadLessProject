# Full Card Porting L0003 Movement Action Timing Source Mapping

## 결정

`L0003_existing_layer`는 `done`으로 승격하지 않는다. 이 batch는 이름상 기존 layer로 즉시 구현 가능한 후보처럼 보이지만, DCGO 원본을 대조하면 카드별 body를 안전하게 포팅하기 전에 공통 movement/action timing layer가 먼저 필요하다.

Queue status: needs-review

이 항목의 affected card는 core service, catalog, validator의 `CardId` 분기나 임시 workaround로 처리하지 않는다. 카드별 effect body는 원본 `CardEffect` 경로에 대응되는 파일에서만 구현한다.

## Batch 범위

| Timing | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- |
| `OnMove` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnMove.cs`, `CardEffectFactory.cs`, 다수 card effect body | enum은 있으나 moved permanent payload와 battle-area 생존 재검증을 만드는 공통 move primitive가 검증되지 않았다. | `needs-review` |
| `OnRemovedField` | `NeedsSourceReview` | `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_007.cs` 등 실제 card body가 `WhenRemoveField` 조건과 결합해 사용 | field removal 완료 후 timing인지, would-remove cut-in 후 후속 timing인지 source body 단위 검증이 더 필요하다. | `needs-review` |
| `OnStartBattle` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` battle flow가 attacking/defending permanent snapshot과 defending card payload로 stack | attack/battle layer는 일부 보정됐지만 원본 battle start payload와 source snapshot fixture가 아직 없다. | `needs-review` |
| `OnTappedAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` suspend flow가 `Permanents`, `IsBlock`, optional `CardEffect` payload로 stack | suspend primitive는 존재하나 원본 cut-in/restriction/replacement와 block suspend payload parity가 batch 전체에 대해 증명되지 않았다. | `needs-review` |
| `OnUnTappedAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` unsuspend flow가 `WhenUntapAnyone` cut-in 후 actual unsuspend와 `OnUnTappedAnyone` stack을 실행 | unsuspend would/actual 2단계, `CanUnsuspend`, source effect immunity, trace/replay payload가 공통 layer로 고정되지 않았다. | `needs-review` |
| `OnUseOption` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` option execution이 executing zone 이동, cost/root payload, `OnUseOption`, background effects, `OptionSkill` 순서로 실행 | option play boundary와 executing source, background timing, option body resolution을 source-aligned로 연결하는 공통 layer가 아직 없다. | `needs-review` |
| `WhenDigisorption` | `NeedsSourceReview` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_056.cs`, `BT10_052.cs`, `BT8_054.cs` 등 cost-reduction body가 cut-in `WhenDigisorption`을 직접 stack | `BeforePayCost`/cost payment state machine과 digisorption source selection, reduction, subsequent `WhenDigisorption` trigger가 분리되어 검증되어야 한다. | `needs-review` |
| `WhenRemoveField` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenRemoveField.cs`, replacement keyword effects | would-remove cut-in timing이며 return-to-deck/hand, delete, security-like field removal 등 여러 flow에서 `willBeRemoveField`와 target list를 공유한다. 현재 단순 leave-field timing으로 평탄화할 수 없다. | `needs-review` |

## Source Mapping Notes

`OnUseOption`은 option card를 executing zone으로 옮긴 뒤 `Card`, `Root`, `Cost` payload를 만든다. 이후 `OnUseOption`, background `OnUseOption`, 실제 `OptionSkill` body 순서가 이어진다. 따라서 full-card 포팅에서는 option 사용을 단순 play action이나 option body 직접 실행으로 줄이면 안 된다.

`WhenRemoveField`는 actual removal 후 trigger가 아니라 would-remove cut-in에 가깝다. `CardController`의 deck return, hand return, deletion 관련 flow는 대상 permanent에 `willBeRemoveField`를 표시하고 `WhenRemoveField` 후보를 수집한 뒤 cut-in 처리를 거쳐 실제 대상 목록을 다시 고정한다. `CardEffectCommons.CanTriggerWhenRemoveField`는 hashtable의 permanent 목록과 card/permanent 조건을 맞춰 source가 실제 대상에 포함되는지 확인한다.

`OnTappedAnyone`과 `OnUnTappedAnyone`은 직접 bool 변경만으로 끝나는 timing이 아니다. suspend는 실제 suspend 후 `Permanents`, `IsBlock`, `CardEffect` payload를 전달하고, unsuspend는 `WhenUntapAnyone` cut-in, 대상 재고정, actual unsuspend, `OnUnTappedAnyone` 순서로 처리된다. 공통 suspend/unsuspend primitive가 이 순서와 payload를 보존해야 한다.

`OnStartBattle`은 battle 시작 시 live permanent를 그대로 넘기지 않고 attacking/defending permanent snapshot과 defending card를 hashtable에 넣는다. attack lifecycle에서 target이 바뀌거나 삭제되는 경우에도 이 snapshot 정책을 별도 fixture로 검증해야 한다.

`WhenDigisorption`은 `BeforePayCost` 계열 비용 감소 body 안에서 cut-in으로 수집된다. 비용 지불 전 상태, 선택한 suspend 대상, 감소량, 이후 `WhenDigisorption` trigger 순서를 하나의 source-aligned cost state machine으로 다루지 않으면 affected card를 추측 구현하게 된다.

## Blocker Policy

- No CardId branch in core service, catalog, or validator.
- `WhenRemoveField`를 `OnLeaveFieldAnyone` 또는 `OnRemovedField`로 평탄화하지 않는다.
- `OnMove`를 단순 zone 이동 완료 이벤트로 처리하지 않는다. 원본 조건은 moved permanent가 battle area에 남아 있는지 확인한다.
- `OnUseOption`은 executing zone, root, cost, background timing, option body 순서를 보존해야 한다.
- suspend/unsuspend는 direct `IsSuspended` 변경으로 production parity를 주장하지 않는다.
- `WhenDigisorption`은 source body가 cost payment와 결합되어 있으므로 비용 state machine 없이 card body를 먼저 구현하지 않는다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch인 `L0004_existing_layer`다. `L0003_existing_layer`를 다시 열려면 movement event payload, would-remove cut-in, suspend/unsuspend primitive, option execution boundary, battle-start snapshot, digisorption cost timing fixture가 먼저 source-aligned로 설계되어야 한다.
