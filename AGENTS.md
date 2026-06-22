# AGENTS.md

## Active Development Phase

현재 프로젝트의 활성 목표는 foundation-first completion이다.

작업 전에 반드시 다음 파일을 읽는다.

- `docs/codex-prompts/GOAL_FOUNDATION_COMPLETION.md`
- `docs/codex-prompts/ACTIVE/RUN_NEXT_FOUNDATION_COMPLETION.md`
- foundation queue/progress 문서

Foundation Completion Gate가 `OpenCodeReady=true`가 되기 전까지:

- card-porting batch를 실행하지 않는다.
- C0039 이후를 실행하지 않는다.
- 개별 CardEffect body를 추가 구현하지 않는다.
- OpenCode/local-model task를 생성하지 않는다.
- RL 학습 구성요소를 구현하지 않는다.

## 언어 규칙

- 분석 결과, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 한국어로 작성한다.
- 코드 식별자, 클래스명, 메서드명, 파일명, 네임스페이스는 영어를 사용한다.
- 문서 파일은 한국어로 작성한다.

## 프로젝트 목표

이 프로젝트는 신규 카드게임 엔진 개발이 아니다.

목표는 `DCGO/` 폴더에 위치한 기존 DCGO Unity battle 구현을 Source of Truth로 삼아,
강화학습에 사용할 수 있는 headless RL battle engine으로 이식하는 것이다.

- 원본 Unity source: `DCGO/`
- 이 worktree에 `DCGO/`가 없으면 로컬 read-only 원본 `E:\headlessDCGO\DCGO`를 Source of Truth로 사용한다.
- 신규 headless engine: `src/DCGO.RL.Engine/`
- 테스트: `src/DCGO.RL.Engine.Tests/`
- CLI 검증 도구: `src/DCGO.RL.Cli/`
- 추후 Unity 연결부: `src/DCGO.RL.UnityAdapter/`
- 설계/검증 문서: `docs/rl-engine/`

## 핵심 원칙

- 기존 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
- `DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
- 모든 난수는 deterministic RNG를 통해서만 발생해야 한다.
- 모든 zone 이동은 ZoneMover 또는 공통 primitive를 통해 처리한다.
- 지원하지 않는 메커니즘은 silent no-op 하지 말고 명시적으로 실패시킨다.
- 같은 seed, 같은 decklist, 같은 action sequence는 같은 state sequence와 state hash를 만들어야 한다.
- 개발 중 trace/self-play는 학습 데이터가 아니라 검증 데이터다.
- 학습용 RL Environment, Observation, Reward, Dataset Exporter는 엔진 완성 후에만 구현한다.

## 주요 참조 파일

원본 경로는 모두 `DCGO/` 아래에 있다.

- `DCGO/Assets/Scripts/Script/GameContext.cs`
- `DCGO/Assets/Scripts/Script/Player.cs`
- `DCGO/Assets/Scripts/Script/TurnStateMachine.cs`
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/CEntity_Base.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`
- `DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/CardEffect/**/*.cs`

## 선택지/UI 원칙

- RL.Engine은 그래픽 UI를 만들지 않는다.
- RL.Engine은 `DecisionPoint`, `LegalAction`, `SelectionRequest`, `SelectionResult`를 생성한다.
- CLI는 이를 텍스트 목록으로 표시한다.
- UnityAdapter는 이를 기존 Unity 카드 그래픽, SelectCardPanel, SelectPermanentEffect, outline, 클릭 이벤트로 표시한다.
- Bot/RL Agent는 UI 없이 `SelectionResult` 또는 `GameAction`을 직접 선택할 수 있다.

## 세부 지침

세부 진행 순서와 각 단계 프롬프트는 다음 위치를 따른다.

- `docs/codex-prompts/README.md`
- `docs/codex-prompts/prompts/*.md`
- `docs/rl-engine/`

한 번에 여러 단계를 진행하지 않는다.
각 단계는 테스트와 한국어 요약을 포함해야 한다.
