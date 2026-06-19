AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 53 - Security Trigger Timing 원본 정렬

## 목표

원본 `ISecurityCheck`와 `AutoProcessing`의 security 관련 timing 순서를 headless pipeline에 연결한다.

## 원본에서 확인할 timing

- security card reveal/check 시작
- `OnSecurityCheck`
- `SecuritySkill`
- security Digimon battle
- security 감소 확정
- `OnLoseSecurity`
- security card final zone
- `UntilSecurityCheckEnd` cleanup
- `AfterEffectsActivate`, 원본상 필요한 경우

## 구현 원칙

1. 순서를 먼저 문서화한 뒤 코드에 반영한다.
2. 각 timing은 typed `EffectContext` payload를 갖는다.
3. SecuritySkill이 selection을 요구하면 DecisionPoint에서 멈춘다.
4. 여러 security trigger가 동시에 발생하면 다음 queue의 priority model과 호환되는 group을 만든다.
5. checked card가 Executing을 떠난 경우 중복 trash하지 않는다.
6. cleanup은 모든 effect resolution/selection 완료 후 원본 순서에 맞게 수행한다.
7. 미지원 timing은 silent skip하지 말고 completion report에 노출한다.

## 테스트

- OnSecurityCheck before/after SecuritySkill 순서
- OnLoseSecurity 발생 조건
- security Digimon battle
- option add-to-hand/self-play/final trash regression
- selection pending/resume
- cleanup order
- replay determinism
- ST1~ST3 security cards regression

## 문서

security timing sequence diagram과 source mapping을 갱신한다.
