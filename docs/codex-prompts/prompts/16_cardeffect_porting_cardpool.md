# 16 - 학습 대상 카드풀 CardEffect 포팅

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "16_cardeffect_porting_cardpool.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 전체 CardEffect 포팅이 아니다.
학습 대상 card pool 또는 decklist에 포함된 카드만 CardEffect를 포팅한다.

작업 시작 전 사용자에게 다음 중 하나를 확인하라:
- 학습 대상 decklist 파일 경로
- 학습 대상 card pool 목록
- 전체 카드풀 포팅 여부

참조:
- 학습 대상 decklist 또는 card pool
- `DCGO/Assets/Scripts/CardEffect/**/*.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `docs/rl-engine/effect-system.md`
- `docs/rl-engine/tier1-primitives.md`
- `docs/rl-engine/complex-mechanics.md`

작업 순서:
1. 대상 CardId 목록을 추출한다.
2. 각 CardId에 대응하는 원본 CardEffect 클래스를 찾는다.
3. primitive/keyword/complex mechanic으로 표현 가능한 효과와 불가능한 효과를 분류한다.
4. 가능한 효과만 CardScript로 포팅한다.
5. 불가능한 효과는 UnsupportedMechanicException 또는 deck validation 실패로 명시 처리한다.
6. 해당 card pool로 scripted scenario와 smoke test를 추가한다.

금지:
- 대상 card pool에 없는 카드효과 포팅 금지
- 효과를 조용히 무시 금지
- 원본과 다른 효과를 임의로 단순화 금지
- 기존 Unity CardEffect 수정 금지

테스트:
- 각 포팅된 CardScript 단위 테스트
- decklist validation 테스트
- unsupported mechanic 감지 테스트
- 대상 card pool scripted scenario 테스트
- replay determinism 테스트

문서:
- `docs/rl-engine/cardeffect-porting-status.md` 업데이트
- 카드별 포팅 상태 표 작성:
  - CardId
  - 원본 CardEffect class
  - 포팅 상태
  - 사용 primitive
  - 미지원 메커니즘
  - 테스트 여부

완료 후 한국어로 요약하라.

