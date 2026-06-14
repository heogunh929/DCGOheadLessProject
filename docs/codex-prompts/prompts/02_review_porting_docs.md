# 02 - 이식 분석 문서 검수

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "02_review_porting_docs.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이번 작업은 구현이 아니라 `docs/rl-engine` 문서 검수다.

검수 항목:
- 신규 개발이 아니라 이식으로 설명되어 있는가?
- Minimal Playable Battle이 단순 phase loop가 아니라 승패 가능한 battle로 정의되어 있는가?
- 엔진 완성 전 trace가 학습 데이터가 아니라 검증 데이터로 정의되어 있는가?
- RL Environment/Observation/Reward가 엔진 완성 후 단계로 밀려 있는가?
- Validation Harness가 초반부터 필요한 것으로 정의되어 있는가?
- DecisionPoint/SelectionRequest가 CLI와 UnityAdapter 양쪽에 쓰이는 구조로 정의되어 있는가?
- Complex Mechanics와 Battle Keywords가 분리되어 있는가?
- Jogress/Burst/AppFusion/DigiXros/Assembly/Link가 CardEffect보다 먼저 포팅되는 것으로 정의되어 있는가?
- UnityAdapter가 battle rule 재구현이 아니라 상태 변환/action 주입/UI 매핑만 담당하는 것으로 정의되어 있는가?
- 엔진 완성 판정 checklist가 있는가?

문제점을 찾으면 문서를 수정하라.

금지:
- 구현 코드 생성 금지
- 기존 `DCGO/` 하위 Unity 파일 수정 금지

완료 후:
- 수정한 문서
- 검수 결과
- 남은 위험 요소
를 한국어로 요약하라.

