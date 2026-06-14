# 10 - Minimal Playable Battle 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "10_minimal_playable_battle.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

참조 원본:
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`

목표:
Minimal Playable Battle을 구현한다.
이 단계는 단순 phase loop가 아니라 CardEffect 없는 상태에서도 게임 승패가 정상 발생해야 한다.

구현할 것:
- TurnRunner
- PhaseRunner
- LegalActionGenerator
- ActionExecutor
- HatchService
- MoveFromBreedingService
- PlayCardService
- DigivolveService
- AttackService
- BattleResolver
- SecurityCheckService
- WinConditionChecker
- RuleProcessor 최소 버전

필수 action:
- HatchAction
- MoveFromBreedingAction
- PlayCardAction
- DigivolveAction
- AttackAction
- PassAction

필수 규칙:
- Active / Draw / Breeding / Main / End phase
- Hatch
- Move from breeding
- Play Digimon/Tamer as PermanentState
- Option은 효과가 없으면 use-and-trash placeholder
- Normal Digivolve
- 기본 진화 드로우
- Attack security
- Security check
- opponent security가 0인 상태에서 직접 공격 성공 시 승리
- Attack Digimon
- DP 비교
- 낮은 DP trash
- 동점 처리 규칙 반영
- memory crossing turn end
- pass 시 memory 3 처리 여부는 원본 동작을 확인해 반영

금지:
- Blocker 구현 금지
- Counter timing 구현 금지
- 개별 CardEffect 구현 금지
- Piercing/Jamming/Rush/Reboot/Retaliation 등 keyword 구현 금지
- Complex mechanics 구현 금지
- UnityAdapter 구현 금지

테스트:
- scripted scenario로 security 0인 상대에게 공격하면 승리
- draw phase deck-out 패배
- Digimon vs Digimon 공격에서 낮은 DP가 trash로 이동
- DP 동점 battle 처리 테스트
- attack security 시 security count 감소
- hatch 가능 조건 테스트
- move from breeding 가능 조건 테스트
- memory crossing으로 turn이 넘어가는 테스트
- LegalActionGenerator가 생성한 action은 ActionExecutor에서 실패하지 않음
- invalid action은 명확하게 실패
- 같은 seed와 같은 action sequence의 final StateHash가 동일함

완료 후:
- 원본 TurnStateMachine/AttackProcess와의 대응 관계
- 구현한 최소 battle 규칙
- 아직 Tier1으로 남긴 규칙
- 테스트 결과
를 한국어로 요약하라.

