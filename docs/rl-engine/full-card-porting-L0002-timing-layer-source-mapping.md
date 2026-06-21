# Full Card Porting L0002 Timing Layer Source Mapping

## 결정

`L0002_existing_layer`는 `done`으로 승격하지 않는다. 이 batch의 모든 항목은 원본 `EffectTiming` enum과 RL.Engine enum에 이름은 존재하지만, source-aligned full-card porting에 필요한 event payload, source-zone collection, declaration boundary, battle/zone-move orchestration이 아직 충분히 증명되지 않았다.

Queue 상태는 `blocked`로 둔다. affected card를 카드별 workaround나 core `CardId` 분기로 처리하지 않는다.

## Batch 범위

| Timing | Batch status | Source evidence | RL.Engine status | Decision |
| --- | --- | --- | --- | --- |
| `None` | `NeedsSourceReview` | `AutoProcessing.EndTurnProcess`, `CardController.UseOptionClass`, `CardSource.CanNotBeAffected`, 다수 `EffectList(EffectTiming.None)` | continuous descriptor는 field/inherited 중심으로 부분 구현. player/hand/trash/executing/option-resolution source 범위 미완성 | `blocked` |
| `OnAddDigivolutionCards` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/Permanent.cs`에서 source 추가 후 `Permanent`, `CardEffect`, `CardSources`, `isFromSameDigimon`, `isFromDigimon` payload로 stack | enum만 존재. source 추가 primitive가 해당 payload/timing을 일반화하지 않음 | `blocked` |
| `OnDeclaration` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`, `Permanent.cs`, `CardSource.cs`의 declarable skill list와 command 선택 | enum만 존재. 선언형 효과 legal action/decision boundary와 once-per-turn 사용 등록이 일반화되지 않음 | `blocked` |
| `OnDigivolutionCardDiscarded` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`의 `TrashDigivolutionCards`가 `WhenWouldDigivolutionCardDiscarded` cut-in 후 실제 trash 대상 snapshot을 `DiscardedCards` payload로 stack | enum만 존재. source trash primitive가 would/actual timing과 rollback/resume을 일반화하지 않음 | `blocked` |
| `OnDiscardLibrary` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`의 library trash flow가 `DiscardedCards`, `CardEffect` payload로 stack | enum만 존재. deck trash primitive와 mill event timing 연결 미완성 | `blocked` |
| `OnEndBattle` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`의 `BattleClass`가 winner/loser snapshot, loser card, tie 여부, battle object payload로 stack | 일부 attack cleanup은 있으나 full original battle payload와 end-battle trigger stack은 일반화되지 않음 | `blocked` |
| `OnEnterFieldAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs`의 `PlayCardClass`가 `OnEnterFieldHashtable`로 permanent, evo roots, old levels, root, DigiXros/Assembly count, source effect를 보존 | ST2/ST3 일부는 `WhenDigivolving` compatibility로 매핑했지만 full on-play/on-enter/on-digivolve source payload는 미완성 | `blocked` |
| `OnLeaveFieldAnyone` | `NeedsSourceReview` | `DCGO/Assets/Scripts/Script/CardController.cs` destroy/bounce/return/remove-field flows와 `CardObjectController`가 leave-field timing을 stack | 일부 deletion flow는 있으나 모든 leave-field reason/top/source/payload를 포괄하지 않음 | `blocked` |

## Source Mapping Notes

`EffectTiming.None`은 단순 "타이밍 없음"이 아니라 continuous/static/declarative/option-resolution effect 조회에 쓰인다. 원본은 player effects, permanent effects, executing card own effects까지 조회한다. RL.Engine의 `ContinuousEffectService`는 현재 주요 field/inherited path를 다루지만, full-card source 범위를 자동 완료로 볼 수 없다.

`OnEnterFieldAnyone`은 원본에서 on-play와 digivolve compatibility가 함께 들어오는 큰 timing이다. `CardController.PlayCardClass`의 `OnEnterFieldHashtable`은 permanent, evo root, root source, old level, DigiXros/Assembly count, source card effect를 payload로 보존한다. 기존 ST2/ST3 일부 매핑처럼 카드별로 `WhenDigivolving`으로 좁혀 처리한 사례는 전체 카드풀 공통 layer 완료 증거가 아니다.

`OnDeclaration`은 `TurnStateMachine`의 command UI, `Permanent.CanDeclareSkillList`, `CardSource.CanDeclareSkillList`에서 선언 가능한 `ActivateICardEffect`를 노출한다. RL.Engine은 이 timing enum만 갖고 있으며, full-card legal action generator가 source-aligned declaration skill을 모두 생성한다는 증거가 없다.

`OnAddDigivolutionCards`, `OnDigivolutionCardDiscarded`, `OnDiscardLibrary`, `OnLeaveFieldAnyone`은 모두 zone/source mutation 직후 또는 직전 cut-in과 연결된다. 직접 zone list 수정 없이 `ZoneMover`/primitive가 event payload를 생성하고 trigger stack으로 이어지는 공통 layer가 필요하다.

`OnEndBattle`은 battle winner/loser snapshot과 battle object payload에 의존한다. attack lifecycle 보정은 일부 진행됐지만, full-card batch affected cards 전체에 대해 원본 payload parity를 보장하려면 golden/replay fixture가 더 필요하다.

## Blocker Policy

- enum 존재 또는 일부 ST1~ST3 compatibility hook만으로 `Implemented`/`Verified`로 승격하지 않는다.
- `None` timing 효과를 card body에서 임의로 즉시 실행하거나 silent no-op 하지 않는다.
- `OnEnterFieldAnyone`을 전부 `WhenDigivolving` 또는 `OnPlay`로 평탄화하지 않는다.
- zone/source mutation timing은 공통 primitive/trigger boundary에서 payload를 보존해야 한다.
- affected card 구현은 이 timing layer가 source-aligned로 설계된 뒤 batch별 카드 파일에서 진행한다.

## Follow-up

다음 작업은 generated queue의 다음 todo batch를 진행한다. `L0002_existing_layer`를 재개하려면 declaration legal action layer, full on-enter payload, continuous source-zone collector, source-add/source-trash/library-trash/leave-field event payload, end-battle payload fixture가 먼저 필요하다.
