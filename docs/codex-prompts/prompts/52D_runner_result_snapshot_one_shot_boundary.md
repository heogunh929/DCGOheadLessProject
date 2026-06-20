# 52D runner result snapshot and one-shot boundary hardening

## 목적

`ScenarioResult`가 runner session 내부의 live mutable `GameState`, `GameTrace`, invariant report list를 직접 노출하지 않도록 snapshot boundary를 강화한다.

## 범위

- pause 결과와 완료 결과 모두 `FinalState`, `Trace`, `InvariantReports`를 snapshot으로 반환한다.
- providerless one-shot `Run`이 pending decision에서 resumable session을 잃은 결과를 반환하지 못하게 한다.
- external decision 실행은 `StartSession`/`Resume(DecisionResult)`만 사용한다.
- provider가 모든 선택을 처리하는 one-shot `Run`은 유지한다.
- `RunnerSessionHandle`은 diagnostic identity이며, 실제 resume는 runner-owned session 객체로만 가능하다는 점을 문서화한다.

## 검증

- paused result snapshot이 resume 이후에도 변하지 않는다.
- result snapshot을 외부에서 변경해도 runner session에는 영향이 없다.
- providerless one-shot `Run` pause는 `StartSession` 안내와 함께 실패한다.
- provider-driven one-shot `Run`은 계속 통과한다.
- chained decision/RNG/action count/replay regression을 유지한다.
