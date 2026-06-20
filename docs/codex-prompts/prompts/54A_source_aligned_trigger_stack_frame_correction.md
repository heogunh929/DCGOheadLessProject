# 54A Source-Aligned Trigger Stack Frame Correction

목표:

- DCGO 원본 `AutoProcessing.TriggeredSkillProcess`와 `MultipleSkills`의 trigger batch, nested trigger, `RuleProcess`, `AfterEffectsActivate` 순서를 `TriggerStackFrame` 모델로 보정한다.
- nested trigger의 remaining effect와 외부 trigger tail을 하나의 list로 평탄화하지 않는다.
- 같은 source card의 복수 descriptor도 `EffectResolution` 단위 ordering 대상으로 보존한다.
- effect 1개 해소 뒤 state-only `RuleProcessor` stabilization을 수행하고, 새 `RulesTiming` trigger는 outer tail보다 먼저 drain한다.
- `AfterEffectsActivate`는 effect마다 실행하지 않고 현재 batch 종료 시 다음 frame으로 수집한다.
- trigger source snapshot에는 role/zone/permanent/top-card/owner/controller를 보존하고 실행 직전 role별로 재검증한다.
- security auto-process는 state stabilization, RulesTiming candidate collection, trigger stack drain, AfterEffectsActivate scheduling을 공통 orchestration으로 수행한다.

금지:

- 카드별 effect body를 통합 파일이나 CardId 분기로 옮기지 않는다.
- `Catalog`, `SecurityCheckService`, core service에 특정 CardId 분기를 추가하지 않는다.
- `DCGO/Assets/Scripts` 원본은 수정하지 않는다.
- commit/push는 수행하지 않는다.

완료 조건:

- 관련 source mapping 문서와 queue/progress 문서를 갱신한다.
- 전체 regression이 통과해야 한다.
- 완료 보고에는 변경 파일, trigger stack frame 구조, MultipleSkills/TriggeredSkillProcess mapping, RuleProcessor interleaving, AfterEffectsActivate scheduling, source snapshot/revalidation, 테스트 결과, git status/diff, DCGO 원본 변경 없음, 남은 55 범위, 추천 commit message를 포함한다.
