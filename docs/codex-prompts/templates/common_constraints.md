# 공통 제약

모든 source-aligned 작업에 적용한다.

- 이 작업은 신규 개발이 아니라 DCGO Unity battle 로직의 headless 이식이다.
- 원본 `DCGO/`는 Source of Truth이며 명시 요청 없이 수정하지 않는다.
- `src/DCGO.RL.Engine`에는 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine/UI 의존성을 추가하지 않는다.
- CardEffect는 원본처럼 카드별 파일 구조를 유지한다.
- Catalog는 registry 등록만 담당한다.
- 공통 service는 원본 공통 처리에 대응될 때만 유지한다.
- 카드별 effect body를 통합 catalog 또는 거대한 helper 파일에 숨기지 않는다.
- 직접 zone list 수정 금지. ZoneMover/primitive를 사용한다.
- unsupported는 silent no-op이 아니라 명시 실패/validation report로 남긴다.
- 학습용 ObservationEncoder/RewardCalculator/DatasetExporter/Trainer/RL Environment API는 구현하지 않는다.
- remote 추가/push/fetch/pull 금지.
- 사용자 승인 없는 commit 금지.
- 모든 보고는 한국어로 한다.
