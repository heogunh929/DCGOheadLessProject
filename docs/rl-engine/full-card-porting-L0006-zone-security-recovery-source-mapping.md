# Full Card Porting L0006 Zone Security Recovery Source Mapping

## 결정

`L0006_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch는 hand/trash/library/digivolution-source/top-card movement에 걸친 8개 timing을 포함한다. DCGO 원본은 각 timing마다 actual zone 이동 전후 순서, payload, cut-in 여부가 다르며, 현재 RL.Engine은 enum과 기본 `ZoneMover` 이동만으로 이 의미를 증명하지 못한다.

Queue status: needs-review

이 항목의 affected card는 core service, catalog, validator의 `CardId` 분기나 임시 workaround로 처리하지 않는다. 카드별 effect body는 원본 `CardEffect` 경로에 대응되는 파일에서만 구현한다.

## Batch 범위

| Timing | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- |
| `OnAddHand` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardObjectController.cs` `AddHandCards` | enum은 있으나 `Players`, `CardEffect`, `CardSources` payload와 draw/trash/return 구분을 만드는 shared hand-add boundary가 없다. | `needs-review` |
| `OnDigivolutionCardReturnToDeckBottom` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` source-to-bottom flow | enum은 있으나 `Permanent`, `DeckBottomCards`, `CardEffect` payload와 actual bottom return 전 trigger 순서가 공통화되지 않았다. | `needs-review` |
| `OnDiscardHand` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` `IDiscardHands` | enum은 있으나 discard aggregation, `DiscardedCards`, source `CardEffect`, post-discard log/order가 공통 discard primitive에 연결되지 않았다. | `needs-review` |
| `OnPermamemtReturnedToHand` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` hand bounce flow | 원본 typo 그대로 enum은 있으나 would-return cut-in 후 fixed targets를 `OnDeletionHashtable`로 넘기는 actual returned-to-hand timing이 없다. | `needs-review` |
| `OnReturnCardsToHandFromTrash` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardObjectController.cs` `AddHandCards` | enum은 있으나 source가 trash였던 card list만 먼저 감지해 `CardSources` payload로 stack하는 shared trash-to-hand boundary가 없다. | `needs-review` |
| `OnReturnCardsToLibraryFromTrash` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardObjectController.cs` `AddLibraryTopCards`, `AddLibraryBottomCards` | enum은 있으나 trash-to-library top/bottom 양쪽에서 actual move 전 `CardSources` payload를 stack하는 shared boundary가 없다. | `needs-review` |
| `WhenReturntoHandAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, `CardEffectCommons/HashtableSetting.cs` | would-return-to-hand cut-in과 `WhenRemoveField` 동시 collection, `willBeRemoveField`, post-cut-in target refix가 없다. | `needs-review` |
| `WhenTopCardTrashed` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`, `CardEffectCommons/KeyWordEffects/ArmorPurge.cs`, `CardEffectCommons/CanUseEffects/WhenRemoveField.cs` | enum은 있으나 de-digivolve, mass de-digivolve, stack trash, armor purge가 공유하는 `Permanent`/`CardSources` payload와 top-card timestamp handling이 없다. | `needs-review` |

## Source Mapping Notes

`OnAddHand`은 단순 hand zone 진입 이벤트가 아니다. 원본 `CardObjectController.AddHandCards`는 token/egg를 분리하고, ACE overflow를 처리하고, 각 card를 `AddHandCard`로 넣은 뒤, start-game 이후에만 `Players`, `CardEffect`, `CardSources` payload로 `OnAddHand`을 stack한다. draw 여부는 `AddHandCards(cardSources, isDraw, cardEffect)` 인자로 전달되지만 `OnAddHand` payload 자체에는 draw 여부가 직접 들어가지 않는다.

`OnReturnCardsToHandFromTrash`는 actual hand 이동 전에 발생한다. `AddHandCards`는 입력 card 중 trash에 있던 card가 하나라도 있으면 `CardSources` payload로 `OnReturnCardsToHandFromTrash`를 먼저 stack한 뒤 token/egg/ACE overflow/hand insert를 진행한다.

`OnReturnCardsToLibraryFromTrash`는 library top과 bottom return 양쪽에서 발생한다. `CardObjectController.AddLibraryTopCards`와 `AddLibraryBottomCards`는 입력 card가 trash에서 왔는지 먼저 검사하고, actual remove/add 전에 `CardSources` payload로 `OnReturnCardsToLibraryFromTrash`를 stack한다.

`OnDiscardHand`은 여러 `IDiscardHand`를 먼저 모두 실행한 뒤 실제 discarded flag가 선 card들을 모아 `DiscardedCards`, `CardEffect` payload로 한 번 stack한다. 카드별 discard 중간에 하나씩 trigger하지 않는다.

`WhenReturntoHandAnyone`은 actual return 후 trigger가 아니라 would-return cut-in이다. hand bounce flow는 target permanent에 `willBeRemoveField = true`를 표시하고 `WhenReturntoHandAnyone`과 `WhenRemoveField`를 cut-in stack에 넣은 뒤, cut-in 처리 후 `TopCard`와 `willBeRemoveField`가 유지된 target만 다시 고정한다.

`OnPermamemtReturnedToHand`은 원본 enum typo를 그대로 가진 actual returned-to-hand timing이다. hand bounce target이 post-cut-in refix된 뒤 `OnDeletionHashtable` payload로 `OnPermamemtReturnedToHand`가 먼저 stack되고, 이어서 `OnLeaveFieldAnyone`이 stack된다. 따라서 이 timing을 `OnLeaveFieldAnyone`이나 `WhenReturntoHandAnyone`으로 평탄화하면 안 된다.

`OnDigivolutionCardReturnToDeckBottom`은 digivolution source card를 deck bottom으로 보내기 전에 `Permanent`, `DeckBottomCards`, `CardEffect` payload로 stack한다. 그 뒤 `CardObjectController.AddLibraryBottomCards`가 실제 zone 이동을 맡는다.

`WhenTopCardTrashed`는 de-digivolve, mass de-digivolve, stack trash, armor purge 등 여러 flow에서 사용된다. 공통 payload는 `Permanent`, `CardSources`이며, 원본은 새 top card의 timestamp 갱신과 root removal visual/effect를 함께 수행한 뒤 timing을 stack한다. `CardEffectCommons.CanTriggerWhenTopCardTrashed`는 hashtable의 `CardSources` 목록을 기준으로 조건을 검사한다.

## Blocker Policy

- No CardId branch in core service, catalog, or validator.
- zone list를 직접 수정해서 이 timing들을 우회하지 않는다.
- `WhenReturntoHandAnyone`, `OnPermamemtReturnedToHand`, `OnLeaveFieldAnyone`을 하나의 leave-field timing으로 평탄화하지 않는다.
- trash-to-hand/library return timing은 actual move 전 source zone snapshot을 보존해야 한다.
- `OnDiscardHand`은 multi-discard aggregation 단위로 처리해야 한다.
- `WhenTopCardTrashed`는 top-card role 변화, removed top card list, new top timestamp를 함께 검증해야 한다.
- selection이 발생하는 affected card를 구현하기 전에 각 shared zone event boundary가 pause/resume-safe여야 한다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch인 `C0007_zone_security_recovery`다. 이 card-porting batch를 진행하기 전, affected cards가 위 timing에 의존하면 해당 card body를 추측 구현하지 말고 이 L0006 blocker와 연결해 `needs-review`로 유지한다.
