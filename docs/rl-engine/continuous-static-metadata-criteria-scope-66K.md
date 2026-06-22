# 66K Continuous/Static Metadata Criteria Scope

## Source Mapping

로컬 worktree에는 `DCGO/Assets`가 없으므로 원본 body를 직접 재확인하지 못했다. 이번 범위는 기존 generated inventory와 66F~66J 문서에서 반복적으로 blocker로 남아 있던 trait/name/text metadata 조건을 공통 foundation API로 분리하는 것이다.

참조한 원본 구조:

- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/CardSource.cs`
- `DCGO/Assets/Scripts/Script/Permanent.cs`

이 범위는 개별 `Assets/Scripts/CardEffect/**` body를 구현하지 않는다. 카드별 trait/text 기반 search, alternate digivolution, source placement 등은 이 공통 metadata criteria를 사용해 후속 작업에서 구현해야 한다.

## RL.Engine Mapping

- `CardDefinition`
  - `Traits` / `CardTraits`
  - `CardTextEnglish` / `Text`
  - `CardTextJapanese`
- `CardMetadataCriteria`
  - required trait 조건
  - any trait 조건
  - name substring 조건
  - text substring 조건
  - 조건 비교는 deterministic `OrdinalIgnoreCase`로 수행한다.
- `ContinuousEffectDescriptor`
  - `SourceMetadataCriteria`
  - `TargetMetadataCriteria`
- `ContinuousKeywordDescriptor`
  - `SourceMetadataCriteria`
  - `TargetMetadataCriteria`
- `StaticEvolutionRequirementDescriptor`
  - `SourceMetadataCriteria`
  - `TargetMetadataCriteria`
- `StaticLinkRequirementDescriptor`
  - `SourceMetadataCriteria`
  - `TargetMetadataCriteria`

`ContinuousEffectSourceCollector`는 descriptor를 수집할 때 source card definition metadata를 검사한다. `ContinuousEffectService`와 `StaticRequirementService`는 target permanent top card definition metadata를 검사한다.

## Determinism

`GameState.ComputeStateHash()`는 card definition의 trait와 card text를 포함한다. 따라서 같은 card identity라도 metadata가 다르면 state hash가 달라지고, trait/text 조건의 입력 차이가 replay/invariant 검증에서 숨지 않는다.

## Evidence

- `CardDefinition trait text metadata`
- `CardDefinition metadata changes state hash`
- `Continuous metadata criteria gates target trait and text`
- `Static requirement metadata criteria gates source and target`
- targeted `CardDefinition metadata Continuous Static requirement`: `All 48 tests passed.`

## Remaining Blockers

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다. 남은 공통 foundation 항목:

- cost/restriction/immunity static interface
- trigger body로 처리되는 keyword flow
- generated full-card parity evidence
- runtime/generated status mismatch 해소
