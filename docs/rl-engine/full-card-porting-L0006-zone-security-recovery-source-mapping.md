# Full Card Porting L0006 Zone Security Recovery Source Mapping

## 결정

`L0006_zone_security_recovery`는 `done`으로 처리한다.

이번 구현은 카드별 effect body를 만들지 않고, DCGO 원본의 hand/trash/library/digivolution-source/top-card 이동 timing을 headless engine의 공통 zone event boundary로 연결했다. 카드별 효과는 이후 C-batch의 각 카드 파일에서 구현해야 하며, core service 또는 Catalog에 CardId 분기를 추가하지 않는다.

Queue status: done

Source evidence paths:

- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/CardObjectController.cs`
- No CardId branch.

## Source Mapping

| Timing | DCGO source 근거 | RL.Engine 구현 |
| --- | --- | --- |
| `OnAddHand` | `DCGO/Assets/Scripts/Script/CardObjectController.cs`의 hand add flow | `Tier1PrimitiveService.Draw`와 `AddCardsToHandWithEvents`가 hand 진입 후 pending rule event로 enqueue한다. payload는 `Cards`, `CardSources`, `Players`, `SourceZone`, `DestinationZone`, `IsDraw`를 보존한다. |
| `OnDraw` | `CardController.DrawClass` 이후 `OnDraw` stack | draw primitive가 `OnAddHand` 뒤에 `OnDraw`를 enqueue한다. payload는 `DrawnCards`, `CardSources`, `SourceZone=Deck`, `DestinationZone=Hand`를 보존한다. |
| `OnDiscardHand` | `CardController`의 `IDiscardHands` 집계 flow | `DiscardHandWithEvents`가 여러 hand card 이동을 먼저 수행한 뒤 `DiscardedCards`/`CardSources`를 집계해 한 번 enqueue한다. |
| `OnReturnCardsToHandFromTrash` | `CardObjectController.AddHandCards`의 trash source 감지 | `AddCardsToHandWithEvents(..., sourceZone: Trash)`가 실제 hand 이동 전에 trash source snapshot payload를 enqueue한다. 이후 `OnAddHand`가 같은 queue tail에 이어진다. |
| `OnReturnCardsToLibraryFromTrash` | `AddLibraryTopCards`/`AddLibraryBottomCards`의 trash-to-library flow | `ReturnTrashCardsToLibraryWithEvents`가 `CardSources`, `ToTop`, `SourceZone=Trash`, `DestinationZone=Deck` payload를 enqueue한 뒤 deck top/bottom 이동을 수행한다. |
| `WhenReturntoHandAnyone` | return-to-hand 전 cut-in 후보 수집 flow | `ReturnPermanentToHandWithEvents`가 실제 bounce 전에 `Permanent`, `ReturnedTopCard`, `WouldReturn` payload를 enqueue한다. |
| `OnPermamemtReturnedToHand` | 원본 typo를 유지한 actual returned-to-hand timing | `ReturnPermanentToHandWithEvents`가 top card hand 이동과 source trash 이동 뒤 `OnPermamemtReturnedToHand`를 enqueue하고, 이어서 기존 `OnLeaveFieldAnyone` interop event를 enqueue한다. |
| `OnDigivolutionCardReturnToDeckBottom` | digivolution source bottom-deck return flow | `ReturnDigivolutionSourcesToDeckBottomWithEvents`가 `Permanent`, `DeckBottomCards`, `CardSources` payload를 enqueue하고 source를 deck bottom으로 이동한다. |
| `WhenTopCardTrashed` | top-card trash/de-digivolve/armor-purge 계열 cut-in | `TrashTopCardWithEvents`가 `Permanent`, `TopCard`, `CardSources` payload를 enqueue한 뒤 top card를 trash로 이동한다. |

## Runtime 구조

`RuntimeRuleState`가 `PendingRuleEvent` queue를 소유한다. 이 queue는 `GameState.Clone`, `GameState.RestoreFrom`, `GameState.ComputeStateHash`에 포함된다. 따라서 pause/resume, rollback, replay, 병렬 session에서 pending zone timing이 누락되거나 공유되지 않는다.

`RuleProcessor.StabilizeStateOnly`는 pending rule event를 `RuleStabilizationEvent`로 반환한다. `TriggerPipelineService.RunAutoProcess`는 이벤트들을 원래 timing의 `PreparedTriggerGroup`으로 변환해 기존 trigger stack frame에서 drain한다. effect body 중 primitive가 새 event를 enqueue하면, effect 1개 해소 후 auto-process boundary에서 state-only stabilization을 거쳐 nested frame으로 처리한다.

`RuleProcessor.ProcessUntilStableWithResult`는 main rule pass 시작 전에 pending rule event를 trigger pipeline으로 drain한다. selection이 발생하면 `ContinueAfterPendingRuleEvent` continuation으로 pause하고, 남은 pending event는 queue에 보존된다.

## 검증

추가 테스트:

- `Runtime rule pending events clone restore hash`
- `Zone events OnAddHand and OnDraw drain through auto process`
- `Zone events discard hand aggregates payload`
- `Zone events trash return fire before hand and library moves`
- `Zone events return permanent and top trash timings`
- `Zone events source return deck bottom timing`

전체 회귀 결과: `All 454 tests passed.`

## 남은 범위

L0006은 shared timing boundary만 완료한다. C0007 이후 card-porting batch는 실제 source effect body, registry/status 갱신, 카드별 테스트, baseline blocker 감소가 모두 있어야 `done`으로 처리할 수 있다. L0006 완료만으로 카드별 effect를 silent no-op 또는 추측 구현으로 처리하지 않는다.
