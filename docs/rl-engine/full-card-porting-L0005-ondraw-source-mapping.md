# Full Card Porting L0005 OnDraw Source Mapping

## 결정

`L0005_draw_search_reveal_hidden`는 `done`으로 승격하지 않는다. 이 batch의 mechanic item은 `OnDraw` 하나뿐이고 affected card count는 0이지만, DCGO 원본의 draw primitive 순서와 현재 RL.Engine의 draw service를 대조하면 trigger boundary가 아직 source-aligned로 구현되어 있지 않다.

Queue status: needs-review

이 항목은 카드별 body 구현 대상이 아니다. 공통 draw primitive, `OnAddHand`, `OnDraw`, selection-aware phase continuation이 정렬되기 전에는 full-card completion gate에서 숨기지 않는다.

## Batch 범위

| Timing | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- |
| `OnDraw` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` `DrawClass`, `DCGO/Assets/Scripts/Script/CardObjectController.cs` `AddHandCards` | `EffectTiming.OnDraw` enum과 fixture-level collector tests는 있으나 `src/DCGO.RL.Engine/Setup/DrawService.cs` `DrawService.DrawCards`/draw phase/digivolve draw/effect draw가 source-aligned trigger stack으로 연결되지 않았다. | `needs-review` |

## Source Mapping Notes

DCGO 원본 `DrawClass.Draw` 순서는 다음과 같다.

1. `drawCount <= 0`이면 종료한다.
2. library가 비어 있으면 종료한다.
3. 각 draw마다 library top card를 `CardObjectController.RemoveFromAllArea`로 기존 zone에서 제거하고 `DrawCards` 목록에 추가한다.
4. `CardObjectController.AddHandCards(DrawCards, true, cardEffect)`를 호출한다.
5. draw card 수를 로그에 기록한다.
6. draw card가 1장 이상이면 `Player`, `CardEffect` payload로 `EffectTiming.OnDraw`을 `autoProcessing.StackSkillInfos`에 넣는다.

`CardObjectController.AddHandCards`는 draw 여부를 인자로 받아 hand에 넣고, start-game 이후 hand 증가가 있으면 `Players`, `CardEffect`, `CardSources` payload로 `OnAddHand`을 먼저 stack한다. 따라서 source-aligned 순서는 단순히 "Deck -> Hand 후 OnDraw"가 아니라 `AddHandCards`/`OnAddHand` boundary 이후 `OnDraw`이다.

현재 RL.Engine의 `DrawService.DrawCards`는 `ZoneMover`를 통해 `MoveReason.Draw`로 deck top을 hand로 이동하고 trace move를 남기지만, `OnDraw` trigger pipeline을 호출하지 않는다. `PhaseRunner.RunDrawPhase`도 `DrawPhaseResult`만 반환하고 selection-aware `PhaseExecutionResult`를 반환하지 않기 때문에, draw에서 `OnDraw` 선택이 발생할 경우 `EngineSession.RunToMainPhase`가 pause/resume할 공통 boundary가 없다.

Digivolve draw와 card effect body의 primitive draw도 같은 문제를 공유한다. `DigivolveService`는 digivolve draw 뒤 `WhenDigivolving` trigger를 실행하지만 `OnDraw` trigger를 별도 interleaving하지 않는다. `Tier1PrimitiveService.Draw`는 card script가 직접 호출하는 draw primitive이지만 trigger pipeline과 selection continuation을 받을 수 없다.

## Blocker Policy

- `OnDraw`을 단순 enum 존재 또는 fixture collector coverage만으로 `done` 처리하지 않는다.
- `OnDraw`을 `OnAddHand` 또는 draw phase와 평탄화하지 않는다.
- draw trigger 구현 시 `Player`, source `CardEffect`, drawn card count, drawn card instances, `OnAddHand` 선행 여부를 명시 payload로 보존해야 한다.
- draw phase, digivolve draw, card effect primitive draw가 같은 source-aligned draw boundary를 공유해야 한다.
- selection이 필요한 `OnDraw` 효과가 생기면 `EngineSession`과 runner가 pending `DecisionPoint`를 잃지 않아야 한다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch인 `L0006_zone_security_recovery`다. `L0005_draw_search_reveal_hidden`을 다시 열려면 draw primitive에 source-aligned trigger boundary를 추가하고, `PhaseRunner.RunDrawPhase`/`EngineSession.RunToMainPhase`/direct primitive draw의 pause/resume 정책을 함께 설계해야 한다.
