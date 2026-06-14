# 44 - 공통 layer source mapping 감사

AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, 현재 /goal을 따르라.

이번 작업은 공통 layer 감사다. 새 기능 구현은 하지 마라. 기존 DCGO Unity 원본 파일은 수정하지 마라. remote를 추가하거나 push/fetch/pull 하지 마라. 사용자 승인 없는 commit은 만들지 마라.

## 작업 시작 전 보고

- git status --short
- git diff --stat
- git diff --name-only
- git remote -v
- git log --oneline -5
- git status --short -- DCGO\Assets\Scripts

## 목표

RL.Engine의 공통 service가 원본 DCGO의 공통 처리 구조에 대응되는지, 또는 카드별 구현을 숨기는 통합체가 되었는지 감사한다.

## 감사 대상

- TriggerPipelineService
- SelectionResultApplicator
- TemporaryModifier / DurationCleanupService
- ContinuousEffectService
- SecurityEffectExecutionService
- Tier1PrimitiveService
- ZoneMover
- BattleKeywordService
- RuleProcessor
- PlayCardService / DigivolveService / AttackService / SecurityCheckService

## 원본 대응

- AutoProcessing.cs
- MultipleSkills.cs
- SkillInfo.cs
- ICardEffect.cs
- CardEffectCommons.cs
- CardController.cs
- AttackProcess.cs
- TurnStateMachine.cs
- MainPhaseAction/*.cs

## 수행할 것

각 공통 layer마다 다음 표를 작성한다.

- RL.Engine 파일/타입
- 원본 DCGO 대응 파일/클래스/메서드
- 맡은 책임
- 카드별 로직을 포함하는지 여부
- 적합성: 적합 / 과도 통합 / 누락 / 불명확
- 수정 권고

## 문서 생성/갱신

`docs/rl-engine/common-layer-source-mapping-audit.md`를 생성 또는 갱신한다.

## 금지

- 코드 수정 금지
- 기존 DCGO Unity 원본 수정 금지
- 사용자 승인 없는 commit 금지

## 완료 보고

- 공통 layer 감사 결과
- 과도 통합 위험
- 카드별 파일로 이동해야 할 로직 후보
- 다음 queue 항목
