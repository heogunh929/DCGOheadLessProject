# 35 - 원본 구조 동등성 감사

AGENTS.md 지침, docs/progress/LOCAL_GIT_GUIDE.md 지침, common_constraints.md, 현재 /goal을 따르라.

이번 작업은 구조 감사다. 구현 코드를 수정하지 마라.

## 목표

RL.Engine의 파일/책임 구조가 원본 DCGO와 얼마나 대응되는지 감사한다. 특히 Codex가 임의로 통폐합한 구조가 남아 있는지 확인한다.

## 참조 원본

- DCGO/Assets/Scripts/CardEffect/**
- DCGO/Assets/Scripts/Script/GameContext.cs
- DCGO/Assets/Scripts/Script/Player.cs
- DCGO/Assets/Scripts/Script/TurnStateMachine.cs
- DCGO/Assets/Scripts/Script/CardController.cs
- DCGO/Assets/Scripts/Script/AutoProcessing.cs
- DCGO/Assets/Scripts/Script/AttackProcess.cs
- DCGO/Assets/Scripts/Script/CardEffectCommons.cs
- DCGO/Assets/Scripts/Script/ICardEffect.cs
- DCGO/Assets/Scripts/Script/MainPhaseAction/*.cs

## 감사 대상

- src/DCGO.RL.Engine/
- src/DCGO.RL.Engine.Tests/
- docs/rl-engine/

## 수행할 것

1. CardEffect 구조 비교
   - 원본 CardEffect 파일 구조와 RL.Engine CardEffects 구조를 비교한다.
   - 각 Implemented/NoEffect 카드에 대응 파일이 있는지 확인한다.
   - 원본 CardEffect 파일이 있는 카드가 RL.Engine에서 source mapping을 갖는지 확인한다.

2. Catalog 책임 감사
   - Catalog가 registry만 담당하는지 확인한다.
   - Catalog 안에 SelectionRequest 생성, EffectDescriptor body, primitive 실행, continuation 등 카드별 body가 있으면 문제로 기록한다.

3. 공통 처리 mapping 감사
   - AutoProcessing -> TriggerPipeline/TriggerCollector/EffectQueue
   - CardEffectCommons -> primitive/helper
   - CardController -> Play/UseOption/Security/ZoneMover
   - AttackProcess -> AttackService/BattleResolver/SecurityCheck
   - TurnStateMachine -> PhaseRunner/TurnRunner
   - SelectCard/SelectPermanent -> SelectionRequest/SelectionResult

4. 통폐합 위험 목록 작성
   - 여러 카드 효과가 한 파일에 몰린 곳
   - 원본 공통과 카드별이 섞인 곳
   - helper가 사실상 카드별 body를 숨기는 곳
   - 문서와 실제 코드 구조가 다른 곳

## 산출물

- docs/rl-engine/porting-structure-audit.md
- docs/rl-engine/porting-structure-policy.md

## 금지

- 코드 수정 금지
- 기존 문서의 최신 상태를 섣불리 고치지 말고 감사 문서에만 기록
- commit 금지

## 완료 보고

- 감사 결과 요약
- ST2/ST3 확장 진행 가능 여부
- 우선 수정 필요 항목
