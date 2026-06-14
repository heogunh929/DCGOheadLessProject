# 13 - Complex Play/Evolution Mechanics 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "13_complex_mechanics.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 개별 CardEffect 포팅이 아니라, DCGO의 복합 플레이/진화 메커니즘을 RL.Engine으로 이식하는 작업이다.

Jogress, Burst Digivolution, App Fusion, DigiXros, Assembly, Link 같은 메커니즘은 카드별 효과보다 먼저 구현되어야 한다.
이들은 단순 효과가 아니라 PlayCard/Digivolve/LegalAction/Selection/Cost pipeline에 영향을 준다.

참조 원본:
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/PlayCardAction.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/**/*.cs`
- `DCGO/Assets/Scripts/CardEffect/**/*.cs` 중 Jogress/Burst/AppFusion/DigiXros/Assembly/Link 사용 예시

대상 메커니즘:
- Jogress
- Burst Digivolution
- App Fusion
- DigiXros
- Assembly
- Link
- Delay Option
- ACE / Overflow 관련 처리

구현할 것:
1. 공통 모델
- EvolutionMode
- PlayMode
- EvolutionRequirement
- PlayRequirement
- EvolutionCandidate
- PlayCandidate
- MaterialCandidate
- CostCandidate
- Mechanic enum

2. LegalActionGenerator 확장
- JogressAction 후보 생성
- BurstDigivolveAction 후보 생성
- AppFusionAction 후보 생성
- DigiXrosPlayAction 후보 생성
- AssemblyPlayAction 후보 생성
- Link 관련 후보 생성

3. SelectionRequest 확장
- select jogress source permanents
- select burst tamer
- select app fusion link card
- select digiXros materials
- select assembly materials
- select link target/source

4. CostResolver 확장
- normal play cost
- normal digivolve cost
- jogress cost
- burst cost
- app fusion cost
- digiXros reduced cost
- assembly cost
- fixed/reduced cost modifier와 충돌하지 않게 설계

5. PlayCardService / DigivolveService 확장
- Jogress 처리
- Burst Digivolution 처리
- App Fusion 처리
- DigiXros material 처리
- Assembly material 처리
- Link card 처리
- PermanentState의 top/source/linked card 일관성 유지

금지:
- 개별 카드별 CardEffect 포팅 금지
- 효과를 임의로 단순화 금지
- UI 직접 호출 금지

테스트:
- Jogress legal action 생성 테스트
- Jogress 실행 후 top/source 상태 테스트
- Burst Digivolution legal action 생성 테스트
- Burst tamer 선택 SelectionRequest 테스트
- App Fusion link card 선택 테스트
- DigiXros material 선택 및 cost reduction 테스트
- Assembly material 선택 테스트
- Link card 상태 일관성 테스트
- unsupported mechanic이 silent no-op 되지 않는 테스트
- 기존 Minimal Playable Battle 테스트가 계속 통과해야 함
- replay determinism 테스트

문서:
- `docs/rl-engine/complex-mechanics.md` 업데이트
- `docs/rl-engine/decision-and-selection.md` 업데이트
- `docs/rl-engine/cardeffect-porting-plan.md`에 이 메커니즘들이 card-specific effect보다 먼저 포팅되는 이유를 기록

완료 후:
- 원본 PlayCardClass/PlayPermanentClass/PlayCardAction에서 어떤 책임을 이식했는지
- 구현한 메커니즘
- 아직 미지원인 조건
- 테스트 결과
를 한국어로 요약하라.

