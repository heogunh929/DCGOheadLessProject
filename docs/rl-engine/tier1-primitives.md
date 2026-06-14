# Tier1 Common Primitives

## Source of Truth

12단계는 개별 `CardEffect` 포팅이 아니라, Unity battle 로직에서 카드 효과들이 공통으로 호출하는 저수준 처리를 headless 엔진 primitive로 분리한 단계다.

원본에서 확인한 책임은 다음과 같다.

- `CardEffectCommons`
  - 효과에서 permanent play, option play, token play를 공통 helper로 호출한다.
  - suspend/delete 같은 결과 기반 helper는 실제 처리를 `CardController`의 class로 위임한다.
- `CardController.DrawClass`
  - deck top을 hand로 이동하고 `OnDraw` timing을 stack한다.
- `CardController.DestroyPermanentsClass`
  - 삭제 대상 고정, 삭제 전 cut-in timing, 삭제 후 `OnDestroyedAnyone`/`OnLeaveFieldAnyone`, evolution source trash, top card trash를 처리한다.
- `CardController.ISecurityCheck`
  - security top을 실행 영역으로 이동해 security 감소, security effect, security Digimon battle을 처리한다.
- `CardController.IReduceSecurity` / `IAddSecurity`
  - security 감소/증가 후 `OnLoseSecurity`, `OnAddSecurity`, face-up 증가 timing을 stack한다.
- `CardController.SuspendPermanentsClass` / `IUnsuspendPermanents`
  - suspend/unsuspend 가능 조건을 확인하고 `OnTappedAnyone`, `WhenUntapAnyone`, `OnUnTappedAnyone` timing을 stack한다.
- `CardController.ITrashDeckCards` / `ITrashStack`
  - deck/evolution stack에서 trash로 이동하고 관련 discard/top-card-trashed timing을 stack한다.
- `AutoProcessing.RuleProcess`
  - game end, breeding area 부적합 permanent, DP 부족, link 조건/수량, face-down permanent를 안정화 루프에서 처리한다.

## Engine Mapping

새 구현은 `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`에 모았다. 모든 zone 이동은 기존 `ZoneMover` 또는 기존 battle service를 통해 수행한다.

- `MoveCard`
  - `ZoneMover.MoveCard` wrapper.
- `Draw`
  - 기존 `DrawService.DrawCards` wrapper.
- `Shuffle`
  - `IDeterministicRng` 기반 Fisher-Yates shuffle.
- `Reveal`
  - deck top을 `Zone.Revealed`로 이동한다.
- `Search`
  - UI/agent 선택을 직접 실행하지 않고 `SelectionRequest`를 생성한다.
- `Trash`
  - `MoveCardCommand`의 목적지가 `Zone.Trash`인지 확인한 뒤 `ZoneMover`로 이동한다.
- `AddSecurity`
  - card owner와 security owner가 다르면 `UnsupportedMechanicException`으로 실패한다.
  - 지원되는 경우 source zone에서 `Zone.Security`로 이동한다.
- `RemoveSecurity`
  - 지정 security 또는 top security를 기본 `Zone.Trash`로 이동한다.
- `DeletePermanent` / `DestroyPermanent`
  - 기존 `BattleResolver.DestroyPermanent`를 사용해 evolution source와 top card를 trash로 보낸다.
- `Suspend` / `Unsuspend`
  - `PermanentState.IsSuspended`를 deterministic하게 변경한다.
- `ModifyMemory`
  - 현재 turn player 관점 memory를 actor 기준으로 보정하고 -10..10으로 clamp한다.
- `ModifyDP`
  - `PermanentState.DpModifier`를 변경하며 state hash에 포함된다.
- `PlayCard`
  - 기존 `PlayCardService` wrapper.
- `PlayWithoutPayingCost`
  - permanent card를 비용 없이 battle area에 낸다. option은 아직 명시 예외다.
- `Digivolve`
  - 기존 `DigivolveService` wrapper.
- `DigivolveByEffect`
  - source zone을 인자로 받아 비용 지불 없이 digivolve한다. 필요 시 draw 1을 선택적으로 수행한다.
- `SecurityCheck`
  - 기존 `SecurityCheckService` wrapper.
- `Battle`
  - 기존 `BattleResolver.ResolvePermanentBattle` wrapper.
- `CreateToken`
  - token definition과 card instance를 생성한 뒤 `OutsideGame -> BattleArea` 이동으로 permanent를 만든다.

## RuleProcessor

`RuleProcessor`는 단일 invariant 확인기에서 다음 안정화 루프로 확장했다.

- `ProcessUntilStable`
  - invariant check
  - unsupported linked card 명시 실패
  - breeding area non-Digimon trash
  - DP 0 이하 Digimon trash
  - face-down permanent trash
  - 반복 후 invariant check
  - max iteration guard
- `ProcessAfterAction`
  - 안정화 후 기존 memory crossing turn end를 처리하고, turn이 넘어간 뒤 다시 안정화한다.

현재 link 조건/수량 평가는 구현하지 않았다. linked card가 있는 permanent를 rule processor가 만나면 silent no-op 대신 `UnsupportedMechanicException`으로 실패한다.

## Supported Now

- deterministic common movement primitive
- draw/reveal/search request/shuffle
- security add/remove
- permanent delete/destroy
- suspend/unsuspend
- memory/DP modification
- play without paying cost for permanent cards
- effect digivolve from explicit source zone
- security check/battle wrapper
- token creation
- rule stabilization for invalid breeding, DP zero or below, face-down permanent

## Still Unsupported

- option card effect execution through `PlayWithoutPayingCost`
- linked card legality and link max correction
- delete-prevention cut-in resolution
- individual `CardEffect` classes
- keyword effect bodies
- replacement effects that alter primitive outcomes
- selection result application for search/optional effects

Unsupported items must remain explicit exceptions or validation failures until their prompt stage ports them.

## Tests

12단계에서 추가한 테스트는 다음을 검증한다.

- move/draw/shuffle/reveal/search/trash primitive
- add/remove security, suspend/unsuspend, memory/DP modification
- play without paying cost, effect digivolve, battle, token creation
- `RuleProcessor` invalid breeding/face-down 안정화
- DP 0 permanent trash
- unsupported linked card 명시 실패
- max iteration guard
- timing별 trigger collection과 `EffectQueue`
- optional effect `SelectionRequest`
- once-per-turn duplicate use failure
- unsupported primitive explicit failure

