# 06 - ZoneMover 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "06_zone_mover.md 단계만 수행해줘"라고 요청한다.


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
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`

목표:
모든 카드 zone 이동을 담당하는 공통 primitive를 구현한다.

구현할 것:
- Zone enum
- MoveReason enum
- MoveCardCommand
- MoveCardResult
- IZoneMover
- ZoneMover
- ZoneMoveTrace

Zone enum에는 최소한 포함:
- Deck
- DigiEggDeck
- Hand
- Security
- BattleArea
- BreedingArea
- Trash
- Lost
- EvolutionSources
- LinkedCards
- Executing
- Revealed
- OutsideGame

규칙:
- 이동 전 source zone에 카드가 실제로 존재하는지 검증한다.
- 이동 후 CardInstance.CurrentZone을 갱신한다.
- 한 카드가 동시에 여러 public zone에 중복 존재하면 안 된다.
- BattleArea/BreedingArea의 top card 이동은 PermanentState와 일관성 있게 처리한다.
- EvolutionSources와 LinkedCards 이동은 PermanentState와 일관성 있게 처리한다.
- 잘못된 이동은 예외로 실패한다.
- 조용히 무시하지 않는다.

테스트:
- Deck -> Hand
- Deck -> Security
- Hand -> Trash
- Field top card -> Trash
- Evolution source -> Trash
- 잘못된 source zone 이동 시 예외
- 이동 후 CurrentZone 갱신
- 이동 후 카드가 public zone에 중복 존재하지 않음
- 이동 trace 생성

완료 후:
- 원본 CardController/CardEffectCommons의 어떤 이동 패턴을 ZoneMover로 통합했는지 한국어로 정리하라.

