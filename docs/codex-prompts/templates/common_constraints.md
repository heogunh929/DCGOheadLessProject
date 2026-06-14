# 공통 제약

- 신규 엔진 개발이 아니라 기존 DCGO Unity battle 로직의 headless RL.Engine 이식이다.
- 기존 `DCGO/Assets/Scripts` 원본 파일은 Source of Truth이며 수정하지 않는다.
- RL.Engine에는 UnityEngine/Photon/MonoBehaviour/GameObject/Coroutine/UI 의존성을 넣지 않는다.
- remote 없는 로컬 Git만 사용한다. `remote add`, `push`, `fetch`, `pull` 금지.
- 사용자 승인 없는 commit 금지.
- 작업 시작 전후 `git status --short`, `git diff --stat`, `git diff --name-only`, `git remote -v` 보고.
- 카드 효과 body는 카드별 파일에 둔다. Catalog는 registry 역할만 한다.
- 원본 의미를 보존할 수 없으면 `Implemented`로 올리지 않는다.
- silent no-op 금지. Unsupported/Partial은 명시 보고.
- 학습용 Observation/Reward/Dataset/Trainer/RL Environment 구현 금지.
