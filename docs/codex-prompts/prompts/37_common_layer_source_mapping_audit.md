# 37 - 공통 layer source mapping 감사

AGENTS.md 지침, LOCAL_GIT_GUIDE.md, common_constraints.md, 현재 /goal을 따르라.

이번 작업은 공통 layer가 원본 DCGO 공통 처리에 대응되는지 감사/정렬하는 작업이다.

## 목표

공통 service가 필요한 이유와 원본 대응을 명확히 하고, 카드별 구현을 숨기는 과도한 통폐합을 제거한다.

## 감사 대상

- TriggerPipelineService
- SelectionResultApplicator
- TemporaryModifier / DurationCleanupService
- ContinuousEffectService
- SecurityEffectExecutionService
- Tier1PrimitiveService
- ZoneMover
- AttackService / BattleResolver / SecurityCheckService
- PhaseRunner / RuleProcessor
- StarterScriptSupport / St1ScriptSupport 등 support helper

## 수행할 것

1. 각 공통 service마다 원본 대응 파일/메서드를 문서화한다.
2. 카드별 특수 로직이 공통 service 안에 섞였는지 확인한다.
3. 카드별 특수 로직은 해당 카드 파일로 옮기는 계획을 세운다.
4. generic helper와 card-specific helper를 분리한다.
5. source mapping이 없는 helper는 역할을 재검토한다.

## 산출물

- docs/rl-engine/common-layer-source-mapping.md 생성 또는 갱신
- docs/rl-engine/porting-structure-audit.md 갱신
- 필요하면 코드의 주석/source mapping 보강

## 허용

- 문서 중심 작업
- 명백한 source mapping 주석 보강
- helper 이름/위치가 오해를 만드는 경우 최소 rename, 단 테스트 유지

## 금지

- 기능 변경
- 새 카드 구현
- 학습 API 구현
- 원본 DCGO 수정
- commit

## 테스트

- 코드 변경이 있으면 전체 테스트 실행
- 문서만 변경하면 테스트 생략 사유 보고

## 완료 보고

- 공통 layer별 원본 mapping 표
- 과도 통폐합 의심 목록
- 수정/보류 항목
