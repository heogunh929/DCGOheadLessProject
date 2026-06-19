AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 57 - Unity/RL Parity Trace 계약과 RL Exporter

## 목표

Unity 원본과 RL.Engine이 같은 형식으로 출력할 수 있는 rule-visible trace 계약을 정의하고, RL.Engine exporter를 구현한다.

이번 작업에서는 `DCGO/Assets/Scripts`를 수정하지 않는다.

## Trace 계약

최소 포함:

- schema version
- scenario id
- seed
- step index
- phase/turn player/turn count
- memory
- player zone counts
- ordered public zone card identities
- hidden zone는 count와 허용된 정보만
- permanent id, owner/controller, top/source/link, suspended
- temporary modifiers
- decision/selection request/result
- trigger timing/source/stable id
- zone movement reason
- battle/security outcome
- game result
- canonical state hash

## 구현

- `RuleVisibleSnapshot`
- `ParityTraceEvent`
- canonical ordering
- JSON serializer/deserializer
- schema version validation
- RL.Engine trace exporter
- golden scenario trace 저장
- 비결정적 값 제거
- 개인정보/절대 경로 제외

## 프로젝트 경계

공용 trace DTO는 Unity dependency가 없는 assembly에 둔다.
UnityAdapter는 미래에 같은 DTO를 생성한다.
RL.Engine이 UnityAdapter를 참조하면 안 된다.

## 테스트

- canonical serialization
- round trip
- same state same trace/hash
- hidden information redaction
- schema version mismatch
- golden scenario export
- trace ordering determinism

## 문서

Unity 측 exporter가 채워야 할 mapping table과 예시 fixture를 작성한다.
