AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- fetch/pull은 사용자 승인 없이 실행하지 마라.
- 작업 종료 시 추천 commit message만 보고하라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 작업 시작/종료 시 Git 상태와 DCGO 원본 변경 여부를 한국어로 보고하라.

# 65 - 전체 카드풀 Validation Baseline

## 목표

고정 snapshot의 전체 카드풀이 validation 대상에 들어오고, 미구현 효과가 silent no-op이 아니라 structured blocker로 보고되게 한다.

## 구현

- `FullCardPoolValidationRequest`
- `FullCardPoolValidator`
- manifest/registry/status/source mapping report
- set/color/mechanic별 counts
- blocking issue 목록
- deck validation API
- JSON/markdown output

## 현재 예상

이 단계에서 completion은 실패가 정상이다.

모든 asset은 다음 중 하나로 명확히 분류돼야 한다.

```text
NoEffect
Implemented
Verified
Unsupported
PartiallyImplemented
StubbedForValidation
NeedsSourceReview
UnknownVariant
```

## Gate 정책

아래는 blocker다.

- registry 누락
- source mapping 누락
- false NoEffect
- missing source body
- unknown variant
- Unsupported/Partial/Stub
- unsupported mechanic
- structure violation

## 테스트

- 전체 manifest count 일치
- 각 status count
- source unavailable
- blocker report
- deck subset validation
- ST1~ST3 regression
- deterministic report
