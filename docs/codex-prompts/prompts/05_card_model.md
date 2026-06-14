# 05 - CEntity_Base / CardSource 카드 모델 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "05_card_model.md 단계만 수행해줘"라고 요청한다.


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
- `DCGO/Assets/Scripts/Script/CEntity_Base.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/DataBase.cs`

목표:
CEntity_Base의 battle-relevant fields를 CardDefinition으로 이식한다.

구현할 것:
- CardDefinition
- EvoCostDefinition
- CardColor enum
- CardKind enum
- CardRarity enum 또는 기존 Rarity 대응 enum
- ICardDatabase
- InMemoryCardDatabase
- CardInstanceFactory

CardDefinition에 포함할 필드:
- CardId
- CardIndex
- CardNameEnglish
- CardNameJapanese
- CardColors
- PlayCost
- EvoCosts
- Level
- CardKinds
- DP
- CardEffectClassName
- MaxCountInDeck
- OverflowMemory
- LinkDP
- OptionCardColorRequirements
- IsAce
- IsDualCard

제외할 것:
- Sprite
- CardSpriteName의 이미지 로딩
- StreamingAssets 접근
- ScriptableObject
- UnityEngine 의존성

테스트:
- CardDefinition 생성 테스트
- 같은 CardDefinition에서 여러 CardInstance를 만들면 InstanceId가 서로 다르다
- CardDatabase가 CardId로 조회된다
- CardDefinition은 테스트 중 변경되지 않는 immutable 모델로 사용된다
- UnityEngine 참조가 없다

완료 후:
- 원본 CEntity_Base에서 어떤 필드를 이식했고 어떤 필드를 제외했는지 한국어로 정리하라.

