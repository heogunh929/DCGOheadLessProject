# 66J Static Requirement Descriptor Scope

BatchId: `66J_static_requirement_descriptor_scope`

이 queue는 `ContinuousOrStaticEffect` remediation의 일부로, 원본 DCGO static effect factory 중 다음 source-aligned 범위만 구현한다.

- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`

## 목표

1. `AddSelfDigivolutionRequirementStaticEffect`/`AddDigivolutionRequirementStaticEffect`의 source card, target permanent condition, color/level/cost 의미를 RL.Engine descriptor로 표현한다.
2. `AddSelfLinkConditionStaticEffect`/`AddLinkConditionStaticEffect`의 source card, target permanent condition, link cost 의미를 RL.Engine descriptor로 표현한다.
3. descriptor는 `ContinuousEffectSourceCollector`의 deterministic source scope를 공유한다.
4. legal action generation, execution, replay 경로가 같은 `StaticRequirementService`를 사용한다.
5. 원본의 ignore-digivolution-permission, trait/name/text metadata, restriction/immunity static interface는 이번 범위에서 구현하지 않고 blocker로 남긴다.

## 완료 조건

- `StaticRequirementService`가 source/condition-aware static digivolution/link requirement descriptor를 평가한다.
- `LegalActionGenerator`, `DigivolveService`, `ComplexMechanicService`, `BattleEngineServices`, `RandomLegalActionRunner`가 production graph에서 같은 requirement service를 공유한다.
- static digivolution/link action generation, execution, source move invalidation, condition gating, replay determinism 테스트가 통과한다.
- `ContinuousOrStaticEffect`는 아직 `PartiallyImplemented`로 유지한다.
- `C0039_zone_security_recovery`는 실행하지 않는다.
- `DCGO/Assets` 원본은 수정하지 않는다.
