# 00 - AGENTS.md 검수

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "00_review_agents.md 단계만 수행해줘"라고 요청한다.


AGENTS.md 지침을 따르라.

목표:
현재 루트 `AGENTS.md`가 headlessDCGO 프로젝트 목적에 맞는지 검수하고, 필요한 경우 최소 수정한다.

검수 항목:
- 신규 개발이 아니라 `DCGO/` 원본 battle 로직 이식으로 설명되어 있는가?
- 분석/진행/요약/TODO를 한국어로 작성하라는 규칙이 있는가?
- `src/DCGO.RL.Engine`에 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine 의존성 금지 규칙이 있는가?
- 엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터라고 명시되어 있는가?
- 학습용 RL Environment/Observation/Reward/DatasetExporter는 엔진 완성 후 단계라고 되어 있는가?
- DecisionPoint/SelectionRequest 구조가 CLI와 UnityAdapter 양쪽에 쓰이는 것으로 되어 있는가?
- AGENTS.md가 과도하게 길지 않고, 세부 지침은 docs/rl-engine 또는 docs/codex-prompts에 위임되어 있는가?

금지:
- 구현 코드 생성 금지
- 기존 `DCGO/` 하위 Unity 파일 수정 금지

완료 후:
- 수정 여부
- 핵심 규칙
- 다음 단계 제안
을 한국어로 요약하라.

