# 15 - CardEffect 포팅 기반 구축

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "15_cardeffect_foundation.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 개별 CardEffect 전체 포팅이 아니라, CardEffect 포팅을 안전하게 진행하기 위한 기반 구축이다.

참조 원본:
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/CardEffect/**/*.cs`

목표:
학습 대상 카드풀 또는 전체 카드풀의 CardEffect를 단계적으로 포팅하기 위한 registry, validation, 상태표 구조를 만든다.

구현할 것:
- ICardScript
- ICardScriptRegistry
- CardScriptRegistry
- NoEffectCardScript
- UnsupportedCardScript
- CardEffectPortingStatus model
- DeckMechanicValidator
- UnsupportedMechanicReporter 강화
- CardEffect test fixture helper

규칙:
- 개별 카드효과가 직접 state list를 수정하지 못하게 한다.
- 모든 상태 변경은 primitive service를 통해야 한다.
- 포팅되지 않은 카드효과는 silent no-op 금지.
- 학습 대상 decklist에 unsupported 카드가 있으면 deck validation 실패.

문서:
- `docs/rl-engine/cardeffect-porting-status.md`
- `docs/rl-engine/cardeffect-porting-plan.md` 업데이트

테스트:
- registry lookup 테스트
- unsupported card script 실패 테스트
- deck validation이 unsupported mechanic을 감지하는 테스트
- NoEffectCardScript는 명시적으로 효과 없는 테스트 카드에만 허용
- CardScript가 primitive service만 사용할 수 있는 구조 테스트 가능한 범위 작성

완료 후:
- CardEffect 포팅 기반 구조
- 지원/미지원 카드 처리 방식
- 테스트 결과
를 한국어로 요약하라.

