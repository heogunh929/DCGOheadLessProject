# 66L continuous/static cost, restriction, immunity interface

## 범위

이번 작업은 `ContinuousOrStaticEffect` foundation 중 cost/restriction/immunity의 공통 인터페이스를 여는 작업이다. 개별 `CardEffect` body는 구현하지 않았고, 특정 `CardId` 분기도 추가하지 않았다.

현재 worktree에는 `DCGO/Assets` 원본이 없어서 원본 파일을 직접 재확인하지 못했다. 따라서 이번 mapping은 기존 generated inventory/progress에서 blocker로 남아 있던 `BeforePayCost`, `ChangeCostClass`, `ChangePlayCostPlayerEffect`, static attack/block restriction, static immunity 계열을 foundation API로 받을 수 있게 하는 데 한정한다.

## RL.Engine 대응

- `StaticCostModifierDescriptor`
  - source card/permanent, controller, cost kind, target owner scope, signed amount, source/target metadata criteria를 가진다.
  - `StaticEffectService.ApplyCostModifiers`가 descriptor를 deterministic order로 평가하고 최종 비용을 0 이상으로 clamp한다.
  - 현재 연결된 실행 경로는 normal hand play, option hand play, normal digivolution, static requirement digivolution cost다.

- `StaticRestrictionDescriptor`
  - source card/permanent, controller, permanent target scope, restriction kind, metadata criteria를 가진다.
  - `CannotAttack`은 `BattleRules.CanAttack`, `LegalActionGenerator`, `AttackService` 경로에 연결했다.
  - `CannotBlock`은 `BattleKeywordService.CanBlock`과 blocker selection 경로에 연결했다.

- `StaticImmunityDescriptor`
  - source card/permanent, controller, permanent target scope, immunity kind, metadata criteria를 가진다.
  - 이번 단계에서는 `StaticEffectService.HasImmunity`/`EvaluateImmunities` 평가 API까지만 제공한다.
  - 삭제, bounce, deck return, DP reduction primitive의 실제 replacement/enforcement는 후속 foundation 작업이다.

## Production graph

`BattleEngineServices`는 `StaticEffectService`를 생성하고 다음 runtime 서비스에 같은 인스턴스를 전달한다.

- `PlayCardService`
- `DigivolveService`
- `AttackService`
- `LegalActionGenerator`
- `BattleKeywordService`

`BattleEngineServices.ValidationReport`는 이 shared runtime graph 연결을 검증한다.

## 검증

대상 테스트:

- `Static cost modifier adjusts play and digivolution cost`
- `Static restriction blocks attack and block`
- `Static immunity descriptor evaluates metadata`
- `Runtime composition production graph validates required services`

확인한 것:

- static play cost reduction이 실제 memory payment에 반영된다.
- static digivolution cost reduction이 normal digivolution payment에 반영된다.
- static `CannotAttack`이 legal attack action을 제거한다.
- static `CannotBlock`이 blocker selection 후보를 제거한다.
- source/target metadata criteria가 immunity descriptor 평가를 gate한다.

## 남은 항목

- `IgnoreDigivolutionRequirement`는 66M에서 static evolution requirement fallback의 permission semantics로 연결했다.
- source-aligned `BeforePayCost` event timing, suspend-cost continuation, 선택형 cost reduction은 아직 구현하지 않았다.
- static immunity는 평가 API만 있으며 primitive enforcement는 아직 없다.
- generated full-card status mismatch와 unsupported/partial capability count는 아직 Gate blocker다.
