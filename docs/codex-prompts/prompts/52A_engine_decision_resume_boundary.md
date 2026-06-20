AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 52A - 공통 Engine Decision Pause/Resume Boundary

## 배경

현재 hand option의 pending selection은 `ActionExecutionResult`로 반환할 수 있다.
그러나 `OnPlay`, `WhenDigivolving`, attack timing, phase timing, rules timing, security timing에서 발생하는 pending decision을 공통적으로 반환하고 재개하는 engine boundary는 아직 없다.

전체 카드풀과 RL `Step()`을 위해 모든 선택은 동일한 pause/resume 경계를 사용해야 한다.

## 목표

다음과 같은 UI 비종속 공통 API를 구현한다.

```text
Start/Step(GameAction) -> EngineStepResult
Resume(DecisionResult) -> EngineStepResult
```

실제 이름은 현재 구조와 맞게 정할 수 있지만 책임은 유지한다.

## 필수 설계

1. `EngineStepResult`
   - pending `DecisionPoint`
   - pending stable continuation identity
   - completed action/timing
   - rules processed 여부
   - terminal/game result
   - trace delta
2. `EngineSession` 또는 동등한 runtime boundary
   - 현재 pending interaction은 최대 하나
   - pending 중 새 action 금지
   - request id와 player 검증
   - selection 적용 후 effect queue drain 재개
   - 후속 selection이 생기면 다시 pause
   - 완전 종료 후에만 rules timing/cleanup 수행
3. 지원 timing
   - OptionSkill
   - OnPlay
   - WhenDigivolving
   - OnAllyAttack
   - OnEndAttack
   - OnStartTurn/OnStartMainPhase/OnEndTurn
   - RulesTiming
   - SecuritySkill 및 security hooks
4. Replay
   - action과 selection result를 모두 기록
   - delegate object나 memory address를 trace identity로 사용하지 않는다
   - stable effect/continuation id를 사용한다
5. Runner
   - Scripted/Random/Replay runner는 pending을 무시하거나 상태 변경 후 예외로 끝내지 않는다
   - decision provider가 있으면 선택을 공급
   - 없으면 `PausedForDecision` 결과 반환

## 금지

- 특정 카드만을 위한 resume 분기
- `PlayCardService.ApplyOptionSelection`만 특례로 유지
- pending 중 rules timing 실행
- raw delegate를 직렬화 계약으로 사용
- UI 호출

## 테스트

- option single selection
- chained selection
- WhenDigivolving selection
- attack trigger selection
- phase trigger selection
- security selection
- invalid player/request/result
- pending 중 새 action 실패
- selection 후 rules timing 정확히 1회
- action+selection replay 동일 hash
- runner pause/resume
- ST1~ST3 regression

## 문서

- `decision-and-selection.md`
- `runtime-composition.md`
- `effect-system.md`
- `validation-strategy.md`
