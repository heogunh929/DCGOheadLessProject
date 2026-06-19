AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 50 - Option 실행 Lifecycle 원본 정렬

## 목표

hand에서 option card를 사용할 때 원본 `UseOptionClass` 흐름과 동일한 zone lifecycle을 사용한다.

```text
Hand -> Executing -> OptionSkill resolution -> Trash
```

현재 구현이 `Hand -> Trash -> OptionSkill`이라면 source-aligned하게 수정한다.

## 원본 참조

- `CardController.UseOptionClass`
- `PlayCardClass`
- option cost 처리
- `BeforePayCost`, `AfterPayCost`
- `OptionSkill`
- executing card cleanup
- security option execution 경로

## 구현 원칙

1. option play 전 legality와 cost를 검증한다.
2. `ZoneMover`로 Hand에서 Executing으로 이동한다.
3. effect context의 source zone/source card가 Executing을 가리킨다.
4. OptionSkill의 selection/optional boundary를 유지한다.
5. effect 완료 후 card가 Executing에 남아 있으면 Trash로 이동한다.
6. effect가 card를 다른 zone으로 옮겼다면 중복 trash하지 않는다.
7. security `ActivateMainOption` 경로와 main hand option 경로가 body를 공유하되, 시작 zone/cost/final zone은 구분한다.
8. 실패 중간 상태에서 card duplication이 없어야 한다.
9. option body는 직접 zone list를 수정하지 않는다.

## 테스트

- 정상 Hand -> Executing -> Trash
- source context는 Executing
- selection pending 중 Executing 유지
- selection 완료 후 Trash
- effect가 Hand/Field 등 다른 zone으로 이동시킨 경우 후속 Trash 없음
- invalid action rollback/무오염
- cost 처리
- ST1-13/14/15/16 regression
- ST2/ST3 option regression
- StateHash/replay/invariant

## 문서

원본과 RL lifecycle mapping을 `effect-system.md`와 source mapping 문서에 갱신한다.
