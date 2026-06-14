# UnityAdapter 계획

## 역할

`DCGO.RL.UnityAdapter`는 나중에 RL.Engine을 Unity 클라이언트와 연결하기 위한 adapter다. adapter는 상태 변환, UI 표시, 사용자 action 주입만 담당한다. battle rule, legal action 계산, effect 처리, zone 이동, memory 처리, attack 처리, deck validation을 재구현하지 않는다.

## 장기 구조

UnityAdapter는 다음 방향으로 원본 Unity battle driver를 대체한다.

- Unity scene은 rendering과 input surface로 남긴다.
- RL.Engine이 authoritative `GameState`와 `DecisionPoint`를 생성한다.
- UnityAdapter는 `GameState`를 기존 card object, field frame, hand/security/trash panel에 반영한다.
- UnityAdapter는 click/drag/command panel 결과를 `GameAction` 또는 `SelectionResult`로 변환한다.
- Bot/RL Agent는 UnityAdapter를 거치지 않고 같은 `DecisionPoint`를 직접 소비한다.

## 기존 UI 재사용 후보

- hand card 표시: 기존 `HandCard`, `Draggable_HandCard`
- field 표시: 기존 `FieldPermanentCard`, `FieldCardFrame`
- card 후보 선택: `SelectCardPanel`, `SelectCardEffect`, `SelectHandEffect`
- permanent 후보 선택: `SelectPermanentEffect`
- count/boolean 선택: `SelectCountEffect`, `UserSelectionManager`, command panel
- attack 표시: target arrow, outline, security break glass

이들은 adapter 계층에서만 사용한다. `DCGO.RL.Engine` 프로젝트는 해당 타입을 참조하지 않는다.

## 선택지 표시

adapter는 `DecisionPoint`를 받아 다음처럼 표시한다.

- `LegalAction.PlayCard`: 손패 카드와 target frame 후보를 표시한다.
- `LegalAction.Attack`: attacker와 가능한 defender/security target을 표시한다.
- `LegalAction.ActivateEffect`: source permanent/card와 effect name을 command panel에 표시한다.
- `SelectionRequest.Card`: card 후보에 outline 또는 panel 목록을 표시한다.
- `SelectionRequest.Permanent`: permanent 후보에 outline과 target marker를 표시한다.
- `SelectionRequest.Count/Bool/Option`: command button으로 표시한다.

사용자가 선택하면 adapter는 원본 `PlayerSelection` 객체가 아니라 headless `SelectionResult`를 만든다.

## Bot 대체

원본 AI 분기는 `TurnStateMachine` 내부에서 random choice와 heuristic을 직접 수행한다. 장기적으로 Bot/RL Agent는 다음 인터페이스만 사용한다.

- 현재 observation 또는 full validation state 읽기
- `DecisionPoint` 읽기
- `LegalAction` 또는 `SelectionResult` 선택
- replay trace 저장

UnityAdapter는 사람이 플레이할 때의 입력 구현일 뿐 Bot 정책을 포함하지 않는다.

## 위험 요소

- 기존 Unity UI는 card object reference 중심이고 RL.Engine은 stable id 중심이어야 한다.
- hidden information을 Unity 화면에 표시할 때 player perspective를 분리해야 한다.
- 원본 coroutine animation이 완료되어야 다음 rule이 진행되는 구조를 adapter에서는 engine step과 rendering animation으로 분리해야 한다.
- 원본 `Photon` action 전파와 engine replay action 전파는 다른 책임이다. network sync는 adapter 또는 상위 app 계층에서 처리한다.

## 완료 조건

UnityAdapter 단계는 engine completion 이후에만 구현한다. 최소 구현의 완료 조건은 기존 Unity 화면에 RL.Engine state를 표시하고, 사람이 `DecisionPoint`를 클릭으로 해결하여 engine에 action을 주입할 수 있는 것이다.
