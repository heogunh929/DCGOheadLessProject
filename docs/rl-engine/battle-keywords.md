# Battle Keywords 이식 계획

## 범위

Battle keyword는 개별 `CardEffect` 포팅이 아니라 attack, battle, security check, active phase에 걸리는 공통 rule hook이다. Complex Play/Evolution Mechanics와도 분리한다. Jogress, Burst Digivolution, App Fusion, DigiXros, Assembly, Link는 play/digivolve pipeline의 문제이고, 이 문서는 전투 중 판정되는 keyword를 다룬다.

## 원본 근거

- `Permanent.CanAttack()`, `Permanent.CanAttackTargetDigimon()`, `Permanent.CanBlock()`은 공격 가능 여부와 blocker 조건을 계산한다.
- `Permanent.HasBlocker`, `HasJamming`, `HasPierce`, `HasReboot`, `HasRush`, `HasRetaliation`, `HasBlitz`, `HasEvade`, `HasBarrier`, `HasAlliance`, `HasCollision`은 battle keyword query 역할을 한다.
- `Permanent.Strike`, `Strike_AllowMinus`, `SecurityAttackChanges`, `InvertSecutiryValue`는 Security Attack 증감 계산을 담당한다.
- `AttackProcess.Attack()`과 blocker selection 구간은 attack 선언, counter timing, block timing, battle/security check 진입을 담당한다.
- `IBattle.Battle()`은 DP battle 결과와 battle destruction을 처리한다.
- `ISecurityCheck.SecurityCheck()`는 security reveal, security effect, security Digimon battle, trash 이동을 처리한다.
- `CardEffectInterfaces.cs`에는 `IBlockerEffect`, `IChangeSAttackEffect`, `IInvertSAttackEffect`, `IRushEffect`, `IRebootEffect`, `ICollisionEffect`, `ICanNotBeDestroyedByBattleEffect`, `ICannotBlockEffect` 등 keyword와 battle hook에 필요한 interface가 있다.

## 우선 keyword

- Blocker: block timing에서 non-turn-player의 blocker 후보를 `SelectionRequest.Permanent`로 노출한다.
- Security Attack +N: `Permanent.Strike`에 대응하는 security check 횟수를 계산한다.
- Piercing: battle 승리 후 security check 여부를 결정하는 hook으로 다룬다.
- Jamming: security Digimon과의 battle destruction 방지를 battle resolver에서 처리한다.
- Rush: 등장 턴 공격 제한 예외를 attack legality에 반영한다.
- Reboot: opponent active phase에도 unsuspend되는 규칙을 active phase에 반영한다.
- Retaliation: battle destruction 결과에 상대 permanent destruction을 추가한다.
- Decoy: 원본 keyword 사용 예시를 확인한 뒤 replacement/selection이 필요하면 별도 `SelectionRequest`로 분리한다. 확인 전에는 deck validation에서 미지원으로 실패시킨다.
- Collision: counter/block timing에서 상대 전체가 blocker처럼 취급될 수 있는 규칙을 attack process hook으로 다룬다.

## 14 단계 구현 범위

이번 단계에서는 `BattleKeyword` capability를 `CardDefinition`과 `PermanentState`에 추가했다. 정적 keyword는 card definition에 선언하고, 이후 CardEffect 단계에서 임시 부여 effect는 permanent state의 keyword/modifier로 연결할 수 있게 했다.

추가한 공통 hook은 다음과 같다.

| Keyword | RL.Engine 처리 |
| --- | --- |
| Blocker | `BattleKeywordService.CreateBlockerSelectionRequest()`가 non-turn player의 blocker 후보를 `SelectionRequest`로 만든다. 후보는 battle area Digimon, unsuspended, 공격자와 다른 controller, blocker 가능 permanent로 제한한다. |
| Security Attack +N | `SecurityAttackModifier`를 합산해 security check 수를 계산한다. `SecurityCheckService`는 같은 공격에서 여러 security를 순서대로 체크하고, 공격자가 사라지면 중단한다. |
| Piercing | `AttackService`가 DP battle 후 공격자가 살아 있고 상대 Digimon이 battle로 삭제된 경우 security check를 이어서 수행한다. |
| Jamming | `BattleResolver.ResolveSecurityBattle()`에서 security Digimon battle로 공격자가 삭제되는 것을 막는다. permanent 대 permanent battle에는 적용하지 않는다. |
| Rush | `BattleRules.CanAttack()`에서 등장 턴 공격 제한 예외로 처리한다. |
| Reboot | `PhaseRunner.RunActivePhase()`가 turn player를 unsuspend한 뒤 non-turn player의 Reboot permanent도 unsuspend한다. |
| Retaliation | DP battle에서 Retaliation을 가진 loser가 battle로 삭제되면 상대 permanent도 삭제 목록에 추가한다. tie에서는 이미 양쪽이 삭제된다. |
| Collision | Blocker request에서 공격자가 Collision을 가지면 상대 battle area Digimon 전체를 blocker 후보로 취급하고 `CanSkip = false`로 만든다. |
| Decoy | replacement/선택 처리 구조가 아직 없으므로 `UnsupportedMechanicException`으로 명시 실패한다. |

## 원본과 다르게 처리한 점

원본 `AttackProcess`는 attack 선언, counter timing, block timing, battle, security check, end attack을 coroutine 상태 머신으로 순차 실행한다. 현재 RL.Engine은 아직 full attack stack을 만들지 않았기 때문에 Blocker/Collision은 즉시 자동 처리하지 않고 selection request 생성 API로 노출한다. 추후 CLI/UnityAdapter/Agent가 `SelectionResult`를 주입하는 attack pipeline 단계에서 defender switch와 blocker suspend를 연결한다.

원본의 keyword는 `ICardEffect` interface와 face-up security/player effect까지 조회한다. 현재 구현은 card definition, permanent state, stack/source/link card definition의 선언형 capability만 읽는다. face-up security와 player continuous effect는 CardEffect foundation 이후 같은 `BattleKeywordService`에 provider를 붙여 확장한다.

Security Attack은 원본의 `Strike`, `Strike_AllowMinus`, invert ordering 전체를 재현하지 않고 additive `SecurityAttackModifier`부터 지원한다. `Security Attack -N`, invert security attack, up-to/down-to constant ordering은 후속 CardEffect 포팅에서 별도 검증한다.

## 구현 원칙

- keyword는 `CardDefinition`, `CardEffect`, 또는 continuous effect가 부여할 수 있는 공통 capability로 모델링한다.
- `AttackService`, `BattleResolver`, `SecurityCheckService`, `EffectQueue` hook을 통해 처리한다.
- keyword 처리 중 zone 이동은 직접 list 조작이 아니라 `ZoneMover`/primitive를 사용한다.
- 선택이 필요한 keyword는 반드시 `SelectionRequest`와 `SelectionResult`를 사용한다.
- 구현되지 않은 keyword 또는 조합은 silent no-op 없이 `UnsupportedMechanicException` 또는 deck validation failure로 실패한다.

## Complex Mechanics와의 경계

Complex Mechanics는 play/digivolve action 자체를 확장한다. 예를 들어 DigiXros material 선택이나 Link card 상태는 card play pipeline에 속한다. Battle Keywords는 이미 field에 존재하는 permanent/card가 공격, 방어, battle, security check 중 받는 공통 규칙이다. 두 범위가 만나는 경우에는 play/digivolve 구조를 먼저 확정하고, 그 결과 생성된 permanent에 keyword capability를 적용한다.

## CardEffect와의 경계

원본에서는 많은 keyword가 `ICardEffect`와 interface로 표현되지만 RL.Engine 포팅 순서에서는 card-specific effect보다 앞선다. keyword가 없으면 Minimal Playable Battle 이후 공격/전투 검증이 card pool 확장과 동시에 흔들리므로, keyword hook은 먼저 만들고 개별 카드효과는 그 hook에 capability를 부여하는 방식으로 포팅한다.

## 테스트 방향

- keyword가 없는 Minimal Playable Battle이 계속 통과해야 한다.
- Blocker 후보 생성과 block 선택 request를 검증한다.
- Piercing, Jamming, Retaliation은 battle 결과와 security check 결과를 scripted scenario로 검증한다.
- Reboot와 Rush는 active phase/attack legality에서 검증한다.
- Collision은 block target 후보 확장을 검증한다.
- Decoy는 원본 사용 예시 확인 전까지 unsupported validation을 검증한다.

## 14 단계 남은 TODO

- `SelectionResult`를 받아 blocker suspend와 defender switch를 실행하는 full attack pipeline
- counter timing, cut-in, attack target changed, on block trigger 연결
- face-up security/player continuous keyword provider
- `Security Attack -N`, invert security attack, constant set ordering
- Decoy replacement selection과 effect-origin 조건
- battle deletion immunity, barrier/evade/armor purge 같은 replacement keyword와의 우선순위
