# 14 - Battle Keywords 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "14_battle_keywords.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 개별 CardEffect 포팅이 아니라 공통 battle keyword effect 이식이다.
Complex Play/Evolution Mechanics와 분리해서 진행한다.

참조 원본:
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/CardEffect/**/*.cs` 중 keyword 사용 예시

대상 keyword:
- Blocker
- Security Attack +N
- Piercing
- Jamming
- Rush
- Reboot
- Retaliation
- Decoy
- Collision

구현 원칙:
- keyword는 CardDefinition 또는 CardScript에서 부여 가능한 공통 effect로 설계한다.
- 직접 zone list를 수정하지 않는다.
- AttackService, BattleResolver, SecurityCheckService, EffectQueue hook을 사용한다.
- Selection이 필요한 keyword는 SelectionRequest를 사용한다.
- 지원 범위 밖의 keyword 조합은 명시적으로 실패시킨다.

테스트:
- 각 keyword별 단위 테스트
- keyword가 없는 Minimal Playable Battle 테스트가 계속 통과
- Blocker 선택 요청 생성 테스트
- Piercing security 처리 테스트
- Jamming security battle 처리 테스트
- Retaliation DP battle 처리 테스트
- Reboot active phase 처리 테스트
- Rush attack 가능 여부 테스트
- Collision target restriction 테스트

완료 후:
- 구현 keyword
- 원본과 다르게 처리한 점
- 남은 keyword/TODO
- 실행한 테스트
를 한국어로 요약하라.

