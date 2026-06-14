# headlessDCGO

이 폴더는 기존 DCGO Unity 프로젝트를 `DCGO/` 하위 폴더에 두고,
그 battle 로직을 강화학습용 headless RL engine으로 이식하기 위한 작업 공간이다.

## 예상 폴더 구조

```text
headlessDCGO/
├─ AGENTS.md
├─ README.md
├─ DCGO/                         # 여기에 기존 DCGO Git 소스코드를 넣는다
├─ docs/
│  ├─ codex-prompts/              # Codex 단계별 프롬프트
│  └─ rl-engine/                  # 이식 분석/설계/검증 문서
├─ scripts/                       # 보조 스크립트
└─ src/
   ├─ DCGO.RL.Engine/
   ├─ DCGO.RL.Engine.Tests/
   ├─ DCGO.RL.Cli/
   └─ DCGO.RL.UnityAdapter/
```

## DCGO 원본 소스 배치

`DCGO/` 폴더 안에 기존 DCGO Git 소스코드를 넣는다.

예상 예시:

```text
headlessDCGO/
└─ DCGO/
   ├─ .github/
   ├─ Assets/
   ├─ Client/
   ├─ Packages/
   ├─ ProjectSettings/
   ├─ README.md
   └─ ...
```

## Codex 사용 방법

1. `headlessDCGO` 루트에서 Codex를 실행한다.
2. `/goal`은 전체 목표를 짧게 설정한다.
3. 실제 작업은 `docs/codex-prompts/prompts/*.md`를 한 단계씩 `/mention`해서 진행한다.
4. 중간 self-play/trace는 학습 데이터가 아니라 검증 데이터로만 취급한다.
5. 학습용 RL Environment는 엔진 완성 후 별도 단계에서 구현한다.

예시:

```text
/goal docs/codex-prompts/README.md의 단계 순서대로 DCGO battle 로직을 RL headless engine으로 이식한다. 신규 개발이 아니라 원본 이식이며, 한 번에 한 단계만 수행하고 한국어로 보고한다.

/mention docs/codex-prompts/prompts/00_review_agents.md
00_review_agents 단계만 수행해줘.
```

## 가장 중요한 진행 원칙

- 신규 개발이 아니라 이식이다.
- 엔진 완성 전 데이터는 학습 데이터가 아니다.
- `DecisionPoint` / `SelectionRequest`로 CLI와 UnityAdapter의 선택 구조를 분리한다.
- Validation Harness는 초반부터 만든다.
- RL 학습 API는 엔진 완성 후 만든다.
