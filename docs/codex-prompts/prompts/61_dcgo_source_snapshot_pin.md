AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 61 - DCGO Source Snapshot Pin

## 목표

전체 카드풀의 움직이는 목표 문제를 막기 위해 학습/포팅 대상 원본 revision을 사용자 승인으로 고정한다.

Upstream:

```text
repository: https://github.com/DCGO2/DCGO
default branch: develop
```

이 패키지 작성 시 확인된 upstream develop 후보는 다음이지만 자동 채택하지 않는다.

```text
3b68b9c49d80002a1e75ce4304dcf2c3295d28af
```

## 수행

1. 로컬 `DCGO`가 Git 저장소인지 확인한다.
2. 가능하면 다음을 수집한다.
   - remote URL
   - branch
   - HEAD SHA
   - dirty status
   - submodule/LFS 상태
3. fetch/pull하지 않는다.
4. 로컬 source가 git metadata 없는 복사본이면:
   - `Assets/CardBaseEntity`
   - `Assets/Scripts/CardEffect`
   - 주요 `Assets/Scripts/Script`
   의 deterministic file hash manifest를 생성한다.
5. upstream 후보와 로컬 revision 차이를 보고한다.
6. 다음을 생성한다.
   - `docs/source/DCGO_SOURCE_REVISION.md`
   - `docs/source/dcgo-source-lock.json`
   - `docs/source/dcgo-source-file-manifest.json`, 필요한 경우
7. source lock에는 repository, commit SHA 또는 content fingerprint, branch, roots, timestamp, dirty 여부를 기록한다.

## 상태 정책

- 사용자가 아직 revision을 승인하지 않았다면 queue 61은 `needs-review`로 종료한다.
- dirty source는 승인하지 않는다.
- relative branch 이름이나 "latest develop"만으로 lock하지 않는다.
- 사용자가 SHA를 승인한 뒤에만 queue 61을 `done` 처리한다.

## 금지

- 자동 fetch/pull/checkout
- Unity source 수정
- 임의 upstream commit 선택
