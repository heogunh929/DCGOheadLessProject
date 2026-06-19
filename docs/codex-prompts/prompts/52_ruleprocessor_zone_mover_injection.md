AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 52 - RuleProcessor / ZoneMover 의존성 정렬

## 목표

`RuleProcessor.TrimExcessLinkedCards` 등 core service가 내부에서 `new ZoneMover()` 같은 concrete dependency를 직접 생성하지 않도록 정렬한다.

## 수행할 것

1. `src/DCGO.RL.Engine`에서 다음 패턴을 감사한다.
   - `new ZoneMover`
   - `new Tier1PrimitiveService`
   - `new TriggerPipelineService`
   - service 내부에서 다른 core service 직접 생성
2. composition root 외부의 직접 생성 위치를 보고한다.
3. `RuleProcessor`는 injected `IZoneMover`를 사용한다.
4. 다른 발견 위치도 원본 책임과 구조를 검토해 주입으로 변경한다.
5. 카드별 파일의 단순 value/helper 생성은 이 작업 범위와 구분한다.
6. dependency cycle이 생기면 interface를 분리한다.
7. behavior는 변경하지 않는다.

## 테스트

- injected fake/spy mover가 사용되는지
- linked overflow 처리
- zone invariant
- production composition
- no direct core-service construction guard
- 전체 regression

## 문서

공통 layer source mapping 문서의 dependency 경계를 갱신한다.
