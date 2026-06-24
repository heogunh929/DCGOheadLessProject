# FND001-CS-11 Continuous/Static Requirement Descriptor Scope

## 목적

`FND001-CS-11 static evolution/link requirement effective gates`는 원본 DCGO의 static digivolution requirement, static link requirement, ignore-digivolution permission/cannot-ignore restriction을 source-aligned evidence 기준으로 닫는다. 이번 작업은 구현 추가가 아니라, 이미 존재하는 headless descriptor/runtime/test 후보가 원본 factory/class/interface/consumer 근거와 어디까지 연결되는지 검증한다.

## AS-IS Root

- AS-IS root: `E:\headlessDCGO\DCGO\Assets`
- Local worktree `DCGO/`는 사용하지 않았다.

## Source Evidence

| Group | Original source | Original class/interface | Source consumer |
| --- | --- | --- | --- |
| EvolutionRequirement | `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs` | `AddDigivolutionRequirementClass`, `IAddDigivolutionRequirementEffect` | `CardSource.EvoCosts`, `CardSource.CostList` |
| LinkRequirement | `DCGO/Assets/Scripts/Script/CardEffectFactory/AddLinkRequirement.cs` | `AddLinkConditionClass`, `IAddLinkConditionEffect` | `CardSource.linkCondition` |
| IgnorePermission | `DCGO/Assets/Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPlayer/IgnoreDigivolutionRequirement.cs` | `CannotIgnoreDigivolutionConditionClass`, `ICannotIgnoreDigivolutionConditionEffect` | `Player.CanIgnoreDigivolutionRequirement` |

원본 근거는 다음 조건을 포함한다.

- `AddSelfDigivolutionRequirementStaticEffect`는 자기 카드 조건, 비용, 색/레벨 게이트, `costEquation`, `ignoreDigivolutionRequirement`를 `AddDigivolutionRequirementClass.GetEvoCost`로 연결한다.
- `AddSelfLinkConditionStaticEffect`는 자기 카드 조건과 대상 permanent 조건을 `LinkCondition`으로 변환한다.
- `GainIgnoreDigivolutionRequirementPlayerEffect`는 `EffectTiming.None` player effect로 digivolution requirement 무시/비용 변경 효과를 제공한다.
- `CannotIgnoreDigivolutionConditionClass`와 `Player.CanIgnoreDigivolutionRequirement`는 ignore permission을 막는 restriction 경로다.

## Headless Evidence

- `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`
- `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`
- `src/DCGO.RL.Engine/Effects/StaticRequirementService.cs`
- `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs`
- `src/DCGO.RL.Engine/Battle/BattleRules.cs`
- `src/DCGO.RL.Engine/Battle/LegalActionGenerator.cs`
- `src/DCGO.RL.Engine/Battle/DigivolveService.cs`
- `src/DCGO.RL.Engine/Mechanics/ComplexMechanicService.cs`

닫힌 evidence는 다음이다.

- `StaticEvolutionRequirementDescriptor`와 `StaticRequirementService.EvaluateEvolutionRequirements`가 비용, 색, 레벨, 최소/최대 레벨, source/target condition, source/target metadata, `CostEquation`, ignore permission gate를 평가한다.
- `CannotIgnoreDigivolutionRequirementDescriptor`가 player target kind, target/evolving card metadata, condition을 통해 ignore permission 차단을 평가한다.
- `StaticLinkRequirementDescriptor`와 `ComplexMechanicService`가 link legal action 생성 및 실행 경로에서 static link requirement와 static link cost modifier를 사용한다.
- `BattleRules.CanDigivolve`, `LegalActionGenerator`, `DigivolveService`가 static evolution requirement를 legal action과 실행 양쪽에서 공유한다.

## Source Scaffold Counts

- Source sample candidates: 1196
- Factory references: 1257
- Source-required linked samples: 1196
- Full-card parity `NotRun` samples: 1196

| Group | Source samples | Factory references |
| --- | ---: | ---: |
| EvolutionRequirement | 1186 | 1187 |
| LinkRequirement | 70 | 70 |
| IgnorePermission | 0 | 0 |

`IgnorePermission`은 직접 full-card scaffold factory sample이 아니라 공통 helper와 cannot-ignore restriction 경로로 존재하므로, 직접 scaffold 0건은 유지된 boundary로 기록한다.

## Closed Scope

- Static evolution requirement descriptor/runtime evidence는 원본 `AddDigivolutionRequirementClass.GetEvoCost` 흐름과 연결됐다.
- Static link requirement descriptor/runtime evidence는 원본 `AddLinkConditionClass.GetLinkCondition` 흐름과 연결됐다.
- Ignore-digivolution permission과 cannot-ignore restriction은 원본 common helper/player restriction 경로와 연결됐다.
- Existing tests 15개가 requirement descriptor, effective metadata/level/color, cannot-ignore gate, replay determinism 후보로 확인됐다.
- Full-card source scaffold records는 여전히 `NotRun`이며, 이번 작업은 parity pass evidence로 승격하지 않는다.

## Retained Boundaries

- Headless `CardEffectFactory`에는 source-facing `AddSelfLinkConditionStaticEffect`/`AddLinkConditionStaticEffect` wrapper가 아직 없다. CS-11은 link descriptor/runtime effective gate만 닫고, source-facing factory wrapper parity는 `FND001-CS-07` 또는 `PARITY-001`/TRUST rerun에서 다시 본다.
- `IgnorePermission`은 direct scaffold sample이 0건이다. 공통 helper와 runtime restriction evidence는 닫혔지만 full-card parity와 TRUST-001-RERUN의 재사용 판단은 별도다.
- Full-card parity는 여전히 `NotRun 3918`, `Passed 0`이다.

## Verification

- Verifier: `scripts/verify_fnd001_static_requirement_scope.py`
- Evidence JSON: `docs/generated/as-is-restart/fnd-001-cs-11-static-requirement-verification.json`
- Regression evidence test: `FND001 static requirement verification closes sixth task`

Current verifier summary:

- `passed=true`
- Source requirement groups covered: 3 / 3
- Headless requirement groups covered: 3 / 3
- Source sample candidates: 1196
- Factory references: 1257
- Test candidates covered: 15 / 15
- `linkSourceFacingFactoryWrapperPresent=false`
- `ignorePermissionBoundaryRetained=true`
- `fullCardParityReduced=false`

## Policy

- `src/DCGO.RL.Engine` implementation code was not modified for this task.
- Original `DCGO/Assets` was not modified.
- No `CardEffect` body implementation was added.
- C0039 or later card-porting was not run.
- RL components were not implemented.
- Foundation Gate values were not manually changed.
- Generated status was not promoted.
- No commit or push was performed by this task.

## Next Work

All six `CloseableFoundationTask` items in the FND-001 first group are now closed by evidence. Next candidates are `FND001-CS-07` for unsupported trigger/process keyword source mapping, `PARITY-001` for full-card parity fixture reduction, and `TRUST-001-RERUN` for reuse/delete/manual-review trust classification.
