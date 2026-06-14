# Decision and Selection 구조

최신 기준일: 2026-06-14

RL.Engine은 Unity UI를 구현하지 않는다. 선택이 필요한 rule/effect는 `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult`로 표현하고, CLI/UnityAdapter/RL agent가 같은 구조를 공유한다.

## Current Snapshot

- ST1 selection wiring은 `SelectionRequest`/`SelectionResult` boundary를 유지한다.
- ST2/ST3 source-aligned registry snapshot에서도 selection이 필요한 효과는 카드별 파일과 support helper를 통해 같은 boundary를 사용해야 한다.
- UI 직접 구현은 여전히 금지다.
- full `MultipleSkills` simultaneous trigger priority와 block/counter 세부 selection ordering은 아직 전체 엔진 TODO다.

## 핵심 원칙

- target legality와 후보 산출은 RL.Engine이 담당한다.
- CLI와 UnityAdapter는 표시와 입력 변환만 담당한다.
- UnityAdapter는 battle rule을 재구현하지 않는다.
- invalid selection은 silent no-op 하지 않고 명시적으로 실패한다.
- selection 결과 적용 전 현재 `GameState`에서 target이 여전히 유효한지 다시 검증한다.

## 주요 구조

| 구조 | 책임 |
| --- | --- |
| `DecisionPoint` | 현재 engine이 외부 입력을 기다리는 경계 |
| `LegalAction` | main/breeding/attack 등 player action 후보 |
| `SelectionRequest` | effect resolution 중 필요한 선택 후보와 min/max/skip 규칙 |
| `SelectionResult` | request에 대한 typed 응답 |
| `SelectionValidator` | request/result id, 후보 포함 여부, 개수, skip 규칙 검증 |
| `SelectionResultApplicator` | stale target 재검증 후 continuation 실행 |

## 원본 Unity Mapping

| 원본 Unity 구조 | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `MainPhaseAction` / `QueueMainPhaseAction` | player action 선택 | `LegalAction`, `GameAction` |
| `SelectCardEffect.SetUp` | card 후보 선택 UI | `SelectionRequest` `SelectionKind.SelectCard` |
| `SelectPermanentEffect.SetUp` | permanent 후보 선택 UI | `SelectionRequest` `SelectionKind.SelectPermanent` |
| `SelectCardPanel.OpenSelectCardPanel` | 후보 표시, end/no-select 버튼 | CLI/UnityAdapter rendering |
| `Player.QueuePlayerSelection(CardSelection/PermanentSelection)` | UI 선택 결과 queue | `SelectionResult.SelectedTargets` |
| `Player.QueuePlayerSelection(ValueSelection)` | count/yes-no/option 선택 | `SelectionResult.SelectedCount`, `SelectedBoolean`, `SelectedOption` |

## SelectionResult 적용 방식

`SelectionResultApplicator`는 다음 순서로 effect continuation을 실행한다.

1. `SelectionValidator`로 request id, 후보 포함 여부, MinCount/MaxCount, CanSkip을 검증한다.
2. 선택된 card/permanent가 현재 `GameState`에 여전히 존재하는지 확인한다.
3. 후보 생성 당시 zone snapshot이 있는 경우 현재 zone과 비교해 stale target을 거부한다.
4. 검증된 result만 `EffectResolution.SelectionContinuation`에 전달한다.
5. 적용 후 `EngineInvariantChecker`가 zone/permanent invariant를 확인한다.

잘못된 target, 후보 밖 target, stale target, min/max 위반, required selection skip은 모두 명시 예외로 실패한다.

## 현재 ST1 Selection 사용 현황

| CardId | 선택 사용 | 상태 |
| --- | --- | --- |
| ST1-08 | `WhenDigivolving` owner battle area Digimon 선택 | `Implemented` |
| ST1-13 | main option owner battle area Digimon 선택 | `Implemented` |
| ST1-15 | main/security Activate Main Option target deletion 선택 | `Implemented` |
| ST1-16 | main/security Activate Main Option target deletion 선택 | `Implemented` |

ST1-12 security play-self tamer는 선택이 필요 없는 security effect다. 이번 구현은 `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult` 구조를 변경하지 않았고, UI를 RL.Engine에 추가하지 않았다.

## ST1-12와 Selection Boundary

ST1-12 security effect는 원본 `PlaySelfTamerSecurityEffect`처럼 `Executing` zone의 자기 자신을 비용 없이 play한다.

- selection request를 생성하지 않는다.
- field slot은 engine의 deterministic first-empty-frame 정책으로 결정한다.
- field가 가득 찬 경우 activation 조건이 false가 되어 effect resolution이 실행되지 않는다.
- play 성공 시 card는 `Executing`을 떠나 battle area permanent가 되므로, security check 후속 trash 이동 대상이 아니다.

이 정책은 selection boundary를 우회하는 shortcut이 아니라, 원본 효과 자체가 target/player choice를 요구하지 않는다는 해석에 따른 것이다.

## CLI / UnityAdapter 방향

- CLI는 `DecisionPoint` 또는 `SelectionRequest`를 텍스트 후보 목록으로 표시한다.
- UnityAdapter는 같은 후보를 기존 Unity panel/outline/click target으로 표시한다.
- Bot/RL agent는 UI 없이 `LegalAction` 또는 `SelectionResult`를 직접 선택한다.
- 세 경로 모두 RL.Engine이 만든 후보와 stable id를 사용해야 한다.

## 남은 범위

- full `MultipleSkills` simultaneous trigger priority와 선택 순서
- block/counter 세부 timing의 end-to-end selection result application
- `BeforePayCost` / `AfterPayCost` timing에서의 optional/selection boundary
