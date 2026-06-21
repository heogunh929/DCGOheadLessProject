# Full Card Porting L0004 Cut-In Timing Source Mapping

## 결정

`L0004_existing_layer`는 `done`으로 승격하지 않는다. 이 batch의 두 timing은 모두 이름만 enum에 있는 수준으로는 포팅할 수 없다. DCGO 원본에서는 actual zone/suspend mutation 전에 cut-in stack을 만들고, cut-in 해결 후 대상 목록을 다시 고정한 뒤 실제 mutation을 진행한다.

Queue status: needs-review

이 항목의 affected card는 core service, catalog, validator의 `CardId` 분기나 임시 workaround로 처리하지 않는다. 카드별 effect body는 원본 `CardEffect` 경로에 대응되는 파일에서만 구현한다.

## Batch 범위

| Timing | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- |
| `WhenReturntoLibraryAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs`, `DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_086.cs`, `DCGO/Assets/Scripts/CardEffect/P/Red/P_072.cs` | enum은 있으나 return-to-library would-remove cut-in, replacement target refix, `willBeRemoveField` state, leave-field follow-up을 만드는 공통 layer가 없다. | `needs-review` |
| `WhenUntapAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, `DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_055.cs` | enum은 있으나 actual unsuspend 전 cut-in, `AutoProcessing.HasExecutedSameEffect`, `CanUnsuspend`, post-cut-in target refix, no-unsuspend duration 처리 fixture가 없다. | `needs-review` |

## Source Mapping Notes

`WhenReturntoLibraryAnyone`은 return-to-deck actual mutation 이전에 실행된다. 원본 `CardController`는 return target을 먼저 효과 면역, return restriction, remove 가능 여부로 필터링하고, 대상 permanent에 `willBeRemoveField = true`를 표시한다. 그 다음 `CardEffectCommons.WhenPermanentWouldRemoveFieldCheckHashtable`로 `CardEffect`, `Permanents`, `battle`, `digixros` payload를 만든 뒤 `WhenReturntoLibraryAnyone`과 `WhenRemoveField`를 cut-in stack에 넣는다.

cut-in 처리 뒤에는 기존 target list를 그대로 쓰지 않는다. `TopCard`가 남아 있고 `willBeRemoveField`가 유지된 permanent만 다시 고정한 뒤, show-card, `OnLeaveFieldAnyone`, actual return-to-library flow로 이어진다. 따라서 `WhenReturntoLibraryAnyone`을 단순 `OnLeaveFieldAnyone`이나 `WhenRemoveField` alias로 평탄화하면 원본 replacement 의미가 사라진다.

`WhenUntapAnyone`은 actual unsuspend 이전 timing이다. 원본 `IUnsuspendPermanents.Unsuspend`는 suspended, `CanUnsuspend`, effect immunity 조건으로 후보를 만든 뒤 `CardEffect`, `Permanents` payload로 `WhenUntapAnyone` 후보를 수집한다. 후보가 있으면 cut-in stack을 `TriggeredSkillProcess(false, AutoProcessing.HasExecutedSameEffect)`로 처리하고, 그 뒤 다시 suspended/CanUnsuspend/effect immunity 조건을 재검증해 실제 unsuspend를 실행한다.

`BT7_055`는 `WhenUntapAnyone`에서 hand discard 선택을 제공하고, 선택하지 않으면 해당 Digimon에게 `UntilNextUntap`의 cannot-unsuspend 효과를 부여한다. 따라서 이 timing은 단순 "unsuspend 직전 trigger"가 아니라 선택/지속효과/재검증이 섞인 replacement 성격의 cut-in이다.

## Blocker Policy

- No CardId branch in core service, catalog, or validator.
- `WhenReturntoLibraryAnyone`을 `WhenRemoveField`, `WhenReturntoHandAnyone`, `OnLeaveFieldAnyone`으로 평탄화하지 않는다.
- `WhenUntapAnyone`을 actual `OnUnTappedAnyone` 후속 trigger로 처리하지 않는다.
- return-to-library cut-in은 `willBeRemoveField`와 post-cut-in target refix를 보존해야 한다.
- untap cut-in은 duplicate-effect guard, `CanUnsuspend`, effect immunity, selection pause/resume, cannot-unsuspend duration을 함께 검증해야 한다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch인 `L0005_draw_search_reveal_hidden`이다. `L0004_existing_layer`를 다시 열려면 would-return-to-library replacement layer와 would-untap replacement layer를 공통 primitive/trigger boundary로 먼저 구현해야 한다.
