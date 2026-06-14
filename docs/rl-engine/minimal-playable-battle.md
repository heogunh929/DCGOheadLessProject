# Minimal Playable Battle

## Source of Truth

이번 단계는 Unity 원본의 다음 흐름을 최소 전투 규칙으로 이식했다.

- `TurnStateMachine.BreedingPhase`
  - turn player가 hatch 가능하면 `HatchDigiEggClass.Hatch`를 실행한다.
  - breeding area permanent가 이동 가능하면 `CardObjectController.MovePermanent`로 battle area frame에 이동한다.
- `CardController.HatchDigiEggClass`
  - digi-egg deck top을 breeding area permanent로 만든다.
- `CardController.PlayCardClass`
  - hand/trash/library/security 등 root를 판정하고, 비용 지불 후 permanent play 또는 option use로 분기한다.
  - normal digivolve는 대상 permanent가 있으면 evolution으로 처리하고, 진화 후 draw 1을 수행한다.
- `CardSource.CanPlayCardTargetFrame` / `CanEvolve`
  - 빈 battle frame에는 permanent를 play할 수 있고, 대상 permanent가 있으면 evo cost 조건을 만족해야 한다.
- `AttackProcess.DetermineAttackOutcome`
  - defender가 없고 security가 0이면 direct attack win.
  - defender가 없고 security가 있으면 security check.
  - defender가 있으면 `IBattle`로 DP battle을 처리한다.
- `CardController.ISecurityCheck`
  - security top을 확인하고, security Digimon이면 attacker와 battle한다.
- `CardController.IBattle`
  - attacker DP > defender DP이면 defender가 trash.
  - attacker DP < defender DP이면 attacker가 trash.
  - DP가 같으면 양쪽이 trash.
- `AutoProcessing.EndTurnProcess`
  - pass 시 상대에게 memory 3을 주고 turn을 종료한다.
  - memory가 상대 쪽으로 넘어가면 turn end로 이어진다.

## Engine Mapping

- `TurnRunner`
  - Active, Draw, Breeding, Main까지 진행하는 최소 turn runner.
- `PhaseRunner`
  - Active phase에서 turn count 증가와 unsuspend를 수행한다.
  - Draw phase는 기존 `DrawService`를 사용한다.
  - pass 또는 memory crossing 이후 next turn active phase로 넘긴다.
- `LegalActionGenerator`
  - Breeding phase: hatch, move from breeding.
  - Main phase: play, digivolve, attack security, attack Digimon, pass.
- `ActionExecutor`
  - `HatchAction`, `MoveFromBreedingAction`, `PlayCardAction`, `DigivolveAction`, `AttackAction`, `PassAction`을 dispatch한다.
  - 실행 후 `RuleProcessor`로 invariant와 memory crossing을 처리한다.
- `HatchService`
  - digi-egg deck top을 `ZoneMover.MoveCard`로 `DigiEggDeck -> BreedingArea` 이동한다.
- `MoveFromBreedingService`
  - breeding permanent를 `ZoneMover.MovePermanent`로 `BreedingArea -> BattleArea` 이동한다.
- `PlayCardService`
  - Digimon/Tamer는 `ZoneMover.MoveCard`로 `Hand -> BattleArea` 이동해 `PermanentState`를 만든다.
  - Option은 효과 없는 placeholder로 `Hand -> Trash` 이동한다.
- `DigivolveService`
  - evo cost 조건을 확인하고, `ZoneMover.DigivolveCard`로 top/source를 교체한 뒤 draw 1을 수행한다.
- `AttackService`
  - direct attack win, security check, Digimon battle을 분기한다.
- `SecurityCheckService`
  - security top을 `Security -> Trash`로 이동하고, security Digimon이면 DP battle을 수행한다.
- `BattleResolver`
  - DP 비교 결과에 따라 loser permanent의 sources와 top card를 `ZoneMover`로 trash에 보낸다.
- `WinConditionChecker`
  - deck-out loss와 direct attack win을 기록한다.

## Zone Primitive 확장

10단계에서 다음 common primitive를 `ZoneMover`에 추가했다.

- `MovePermanent`: breeding area와 battle area 사이에서 permanent 전체를 이동한다. top card의 `CurrentZone`과 `PermanentState.IsBreedingArea/FrameIndex`를 함께 갱신한다.
- `DigivolveCard`: hand의 새 card를 permanent top으로 올리고, 기존 top card를 `EvolutionSources`로 이동한다.

서비스 계층은 permanent 이동, digivolve, destroy를 직접 list 수정으로 처리하지 않고 이 primitive를 사용한다.

## Current Tier0 Rules

현재 구현은 CardEffect 없는 최소 battle 규칙이다.

- Hatch
- Move from breeding
- Play Digimon/Tamer as permanent
- Option use-and-trash placeholder
- Normal digivolve with evo cost and draw 1
- Attack security
- Security check
- Security 0 direct attack win
- Attack Digimon
- DP comparison and tie handling
- Memory crossing turn end
- Pass to memory 3

## Explicit Non-Scope

다음은 이번 단계에서 구현하지 않는다.

- Blocker
- Counter timing
- 개별 CardEffect
- Piercing, Jamming, Rush, Reboot, Retaliation 등 keyword
- Jogress, Burst, App Fusion, DigiXros, Assembly 등 complex mechanics
- UnityAdapter

해당 규칙들은 silent no-op으로 처리하지 않고, legal action을 생성하지 않거나 `UnsupportedMechanicException`으로 명시 실패시킨다.

## Tests

현재 테스트는 다음을 검증한다.

- security 0 상대를 직접 공격하면 승리한다.
- draw phase deck-out은 패배한다.
- Digimon vs Digimon 공격에서 낮은 DP가 trash로 이동한다.
- DP 동점 battle에서 양쪽이 trash로 이동한다.
- security attack 후 security count가 감소한다.
- hatch 가능 조건과 실행 결과.
- move from breeding 가능 조건과 실행 결과.
- memory crossing으로 turn이 넘어간다.
- `LegalActionGenerator`가 만든 action을 `ActionExecutor`가 실행할 수 있다.
- invalid action은 명확히 실패한다.
- 같은 seed와 같은 action sequence는 같은 final state hash를 만든다.
