# 66F_continuous_static_source_scope

66E scheduler가 선택한 `ContinuousOrStaticEffect` remediation의 첫 구현 보강 항목이다.

## 목표

- `ContinuousEffectSourceCollector`가 field top / inherited source 외에 원본 `EffectTiming.None` 조회 범위 일부를 수집하게 한다.
- linked card continuous source를 수집한다.
- face-up security continuous source를 수집한다.
- face-down security는 수집하지 않는다.
- core service에 CardId 분기를 추가하지 않는다.
- `ContinuousOrStaticEffect` 전체를 `Verified`로 승격하지 않는다.

## Source Mapping

- `DCGO/Assets/Scripts/Script/Permanent.cs`는 DP/security/stat query 중 `permanent.EffectList(EffectTiming.None)`, face-up security card source `EffectList(EffectTiming.None)`, `player.EffectList(EffectTiming.None)`를 함께 조회한다.
- `Permanent.EffectList_ForCard`는 linked card의 `IsLinkedEffect`와 inherited effect를 top-card effect와 구분한다.
- RL.Engine 이번 항목은 현재 domain model이 명시적으로 표현하는 `LinkedCards`와 face-up `Security` card만 collector source scope에 추가한다.

## 완료 조건

- linked card descriptor가 host permanent에 적용된다.
- linked card가 zone을 떠나면 descriptor가 더 이상 적용되지 않는다.
- face-up security descriptor가 owner battle area Digimon에 적용된다.
- face-down security descriptor는 적용되지 않는다.
- targeted continuous tests와 full regression이 통과해야 한다.

## 남은 범위

- player-level runtime-added static effects
- hand/trash/executing static requirement source
- digivolution requirement static effects
- trait/name/text metadata 기반 조건
- full-card replay/parity evidence
