# 원본과 RL.Engine 매핑

## 핵심 클래스 매핑

| Unity 원본 | 원본 책임 | RL.Engine 후보 | 이식 메모 |
| --- | --- | --- | --- |
| `GameContext` | memory, active card list, 두 `Player`, turn player, phase, player id, turn switch | `GameState`, `TurnContext` | Photon player id 배정은 제거하고 seat/player id를 deterministic 값으로 둔다. |
| `Player` | deck/hand/trash/security/execution/field zone, field frame, effect duration list, memory 관점, hatch/move 가능 여부, action/selection queue | `PlayerState`, `PlayerZones`, `PlayerEffectState` | `MonoBehaviour`, UI object, Transform, Text, animation 의존성을 제거한다. |
| `CEntity_Base` | 카드 정적 데이터, kind/color/level/cost/DP/evo cost/effect class name | `CardDefinition`, `CardDatabase` | `ScriptableObject`, `Sprite`, async image loading은 adapter/data loader 영역으로 분리한다. |
| `CardSource` | 카드 인스턴스, owner, face state, location timestamp, 비용 계산, play/digivolve 가능 여부, trait/name/color/DP/effect 조회 | `CardInstance`, `CardRulesView` | 정적 카드 정의와 mutable instance state를 분리한다. 비용/조건 계산은 rule service로 옮긴다. |
| `Permanent` | field stack, top card, digivolution cards, link cards, DP/keyword 계산, attack/block/remove 가능 여부, duration effects | `PermanentState`, `PermanentRulesView` | top/source/link 일관성 invariant를 validation으로 강제한다. |
| `TurnStateMachine` | start game, phase 루프, main action 대기, action 실행, game end | `RuleProcessor`, `TurnEngine`, `ActionExecutor` | coroutine 대기와 UI 입력은 `DecisionPoint` 반환으로 바꾼다. |
| `MainPhaseAction` 계열 | Photon 직렬화 가능한 main phase command, play/attack/effect/pass/cheat | `GameAction`, `LegalAction` | network serialization은 제거하고 replay 가능한 action id와 payload로 표현한다. |
| `AutoProcessing` | rule process, trigger stack, background effect, end-turn memory check | `EffectQueue`, `RuleProcessor`, `StateBasedActionProcessor` | process-until-stable guard가 반드시 필요하다. |
| `AttackProcess` | attack state machine, counter/block/battle/security/end attack | `AttackProcessor` | selection은 `SelectionRequest`로 외부화한다. |
| `CardController.cs`의 primitive class들 | play, hatch, draw, security, battle, destroy, bounce, digivolution source 조작 등 | `GamePrimitives`, `BattlePrimitives`, `ZoneMover` | Unity effect/animation 호출을 제거하고 순수 state mutation + event log로 바꾼다. |
| `CardObjectController` | 카드 생성, 모든 area 제거, hand/trash/deck/security/execution/field 이동, shuffle, breeding move | `ZoneMover`, `DeckFactory` | 모든 zone 이동의 Source of Truth에 가깝다. 반드시 공통 primitive로 통합한다. |
| `ICardEffect` | effect metadata, trigger/can activate/use, optional/declarative/background flags, timing enum | `CardEffect`, `EffectDescriptor`, `EffectTiming` | UnityAction/UI 표시/Coroutine 실행은 제거하고 command/effect operation으로 표현한다. |
| `CardEffectCommons` | 공통 effect helper, play/digivolve helper, hashtable 생성/조회, selection helper | `EffectHelpers`, `EffectContext`, `PrimitiveInvoker` | `Hashtable` 기반 context는 typed context로 교체한다. |

## 각 주요 원본 책임

`GameContext`는 전역 전투 상태의 루트다. memory는 하나의 정수로 보관하고, 각 플레이어 관점의 memory는 `Player.MemoryForPlayer`에서 해석된다. `TurnPlayer`, `NonTurnPlayer`, `FirstPlayer`, `TurnPhase`, `ActiveCardList`를 포함한다.

`Player`는 owner별 zone 컨테이너다. `LibraryCards`, `DigitamaLibraryCards`, `HandCards`, `TrashCards`, `LostCards`, `SecurityCards`, `ExecutingCards`, `FieldPermanents`를 갖고, main phase action queue와 selection queue도 가진다. 또한 player-level duration effect list와 `CanHatch`, `CanMove`, `CanAddMemory`, `CanReduceSecurity` 같은 rule query를 제공한다.

`CardSource`는 카드 인스턴스와 규칙 조회가 강하게 결합된 객체다. `CanPlayFromHandDuringMainPhase`, `CanPlayCardTargetFrame`, `CanEvolve`, `CanJogressFromTargetPermanents`, `CanBurstDigivolutionFromTargetPermanent`, `CanAppFusionFromTargetPermanent`, `CanLinkToTargetPermanent` 등이 legal action 산출의 근거가 된다.

`Permanent`는 field에 존재하는 stack 단위다. `TopCard`, `DigivolutionCards`, `LinkedCards`, `DP`, `CanAttack`, `CanBlock`, `CanBeDestroyedByBattle`, `CanMove` 같은 battle rule query를 제공한다. field를 떠나기 직전/직후 상태 기록도 여기에서 관리된다.

`TurnStateMachine`은 원본의 최상위 battle driver다. `MainPhase()` 안에서 UI/AI/network action을 기다리고, 선택된 play/effect/attack/pass를 실제 primitive로 연결한다.

`CardController.cs`는 이름과 달리 전투 primitive의 집합이다. `PlayCardClass`, `PlayPermanentClass`, `UseOptionClass`, `DrawClass`, `ISecurityCheck`, `IBattle`, `DestroyPermanentsClass`, `ITrashDigivolutionCards`, `IUnsuspendPermanents` 등 구현 단계에서 가장 중요한 이식 대상이다.

`AutoProcessing`은 state-based action과 trigger 처리의 중심이다. rule process는 반복 실행되며, game end, breeding의 비-Digimon 정리, DP 부족 정리, DP 0 삭제, link 조건/수량 위반, face-down permanent 정리를 처리한다.

`AttackProcess`는 attack lifecycle을 담당한다. `AttackState`는 `None`, `Counter`, `Block`, `Battle`, `End`, `CleanUp` 흐름으로 해석되며, `ActiveAttack()`와 `ProcessNextState()`로 main phase 루프에서 진행된다.

## 제거해야 할 Unity 의존성

- `UnityEngine`, `MonoBehaviour`, `MonoBehaviourPunCallbacks`, `GameObject`, `Transform`, `Sprite`, `Image`, `TextMeshProUGUI`, `EventSystem`
- `Coroutine`, `IEnumerator` 기반 대기, `WaitUntil`, `WaitWhile`, `WaitForSeconds`, `Time.deltaTime`
- `PhotonNetwork`, `[PunRPC]`, `PhotonView`, `ExitGames.Client.Photon.Protocol` 기반 action 전파
- `GManager.instance`, `ContinuousController.instance` singleton 직접 접근
- DOTween animation, sound, outline, command panel, card panel, select panel 호출
- `Hashtable` string key context의 무검증 접근

## 원본과 다르게 처리해야 할 위험

- 원본은 UI 대기와 rule 실행이 coroutine으로 섞여 있어 headless에서는 명확한 `DecisionPoint` 경계가 필요하다.
- 원본은 여러 zone 이동 함수가 먼저 `RemoveFromAllArea()`를 호출하므로 headless도 중복 zone 방지 invariant를 강제해야 한다.
- 원본은 `UnityEngine.Random`과 Photon room state에 의존한다. RL.Engine은 deterministic RNG만 사용해야 한다.
- 원본 selection은 queue에 결과만 들어오며 요청 객체가 명시적이지 않다. RL.Engine은 요청과 응답을 모두 trace에 남겨야 한다.
- 원본의 일부 TODO와 silent return은 headless에서 예외 또는 validation failure로 바꿔야 한다.
