# RL.Engine 이식 개요

## 목적

이 작업은 신규 카드게임 엔진 개발이 아니라 `DCGO/` 원본 Unity battle 구현을 Source of Truth로 삼아 headless `DCGO.RL.Engine`으로 이식하는 작업이다. 원본의 전투 규칙, 처리 순서, zone 이동 규칙, 선택 구조를 먼저 문서화하고, 구현 단계에서는 이 문서를 기준으로 원본과 비교 가능한 작은 단위부터 옮긴다.

## 읽은 원본 파일

- `DCGO/Assets/Scripts/Script/GameContext.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/CEntity_Base.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`

추가로 zone 이동 primitive의 실제 위치를 확인하기 위해 `DCGO/Assets/Scripts/Script/CardObjectController.cs`와 선택 큐 구조 확인을 위해 `DCGO/Assets/Scripts/Script/PlayerSelection/*.cs`를 참고했다.

## 원본 battle 흐름 요약

원본 전투의 외곽 흐름은 `TurnStateMachine.GameStateMachine()`이 담당한다. `StartGame()` 후 루프에서 `SwitchTurnPlayer()`, `ActivePhase()`, `DrawPhase()`, `BreedingPhase()`, `MainPhase()`, `EndPhase()`를 순서대로 실행한다. 각 phase 사이와 주요 액션 후에는 `AutoProcessing.AutoProcessCheck()` 또는 `EndTurnCheck()`가 호출되어 rule process, trigger stack, memory crossing에 따른 턴 종료를 처리한다.

초기 게임은 양 플레이어 5장 드로우, mulligan, security 5장 세팅 순서로 구성된다. `DrawPhase()`에서는 첫 턴을 제외하고 덱이 비어 있으면 상대 승리로 게임을 끝내고, 그렇지 않으면 1장 드로우한다. `BreedingPhase()`는 hatch 또는 move 중 하나를 선택한다. `MainPhase()`는 손패 플레이, field/trash/hand 선언 효과, 공격, pass를 반복하며, 메모리가 상대 기준 종료 임계값 이상이면 `EndTurnProcess()`를 통해 `EndPhase`로 넘어간다.

공격은 `AttackProcess`가 별도 상태 머신으로 처리한다. 공격 선언 시 attacker suspend, `[When Attacking]` 계열 trigger stack, counter timing, blocker selection, battle/security check, end attack cleanup 순서가 이어진다. security가 0인 상대에게 direct attack 가능한 상태에서 공격 결과가 security check로 가면 즉시 승리한다.

## Minimal Playable Battle 정의

Minimal Playable Battle은 개별 `CardEffect` 포팅 없이도 원본 기본 규칙을 검증할 수 있는 최소 전투 루프다.

- 두 플레이어, deterministic seed, 명시 decklist로 게임을 시작한다.
- main deck, digitama deck, hand, security, trash, lost, execution, battle area, breeding area를 구분한다.
- 시작 드로우 5장, mulligan 정책, security 5장 세팅을 재현한다.
- turn cycle은 Active, Draw, Breeding, Main, End 순서를 따른다.
- deck-out loss를 재현한다.
- hatch, breeding area move, normal play, normal digivolve, option use의 기본 비용 지급과 zone 이동을 재현한다.
- memory crossing에 따른 턴 종료를 재현한다.
- 기본 공격, suspend, blocker가 없는 direct security attack, security 0 direct attack win, permanent 간 DP battle과 삭제를 재현한다.
- `LegalActionGenerator`가 만든 action은 `ActionExecutor`에서 성공해야 하며, invalid action은 명시적으로 실패해야 한다.
- 개별 카드효과가 필요한 행동은 지원 상태가 문서화되기 전까지 `UnsupportedMechanicException` 또는 deck validation failure로 실패해야 한다.

## 이식 우선순위

1. 순수 상태 모델과 deterministic RNG를 만든다.
2. 모든 zone 이동을 `ZoneMover` 또는 공통 primitive로 통일한다.
3. `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult`로 선택 구조를 먼저 고정한다.
4. setup, draw, deck-out, security setup을 검증하는 harness v0를 만든다.
5. Minimal Playable Battle 범위의 phase/action/attack을 구현한다.
6. validation harness로 원본 trace와 headless trace를 비교한다.
7. Tier1 primitive를 확장한 뒤 복잡 메커니즘 지원 여부를 deck validation에서 명확히 판정한다.
8. Jogress, Burst Digivolution, App Fusion, DigiXros, Assembly, Link, Delay Option, ACE/Overflow 같은 복합 플레이/진화 메커니즘을 개별 `CardEffect`보다 먼저 이식한다. 이들은 단순 카드 효과가 아니라 play/digivolve legal action, selection, cost, permanent stack 구조에 직접 영향을 준다.
9. Blocker, Security Attack +N, Piercing, Jamming, Rush, Reboot, Retaliation, Decoy, Collision 같은 battle keyword를 복합 플레이/진화 메커니즘과 분리해 이식한다. 이들은 attack/battle/security pipeline의 공통 hook으로 다룬다.
10. 개별 `CardEffect` 포팅은 effect foundation 이후 card pool 단위로 진행한다.

## 엔진 완성 판정 checklist

엔진 완성 판정 기준은 `docs/rl-engine/engine-completion-checklist.md`에 유지한다. `RL Environment`, `Observation`, `Reward`, `Dataset Exporter`, `Trainer`는 이 checklist가 통과되기 전에는 구현하지 않는다. 개발 중 self-play/trace는 학습 데이터가 아니라 검증 데이터로만 취급한다.
