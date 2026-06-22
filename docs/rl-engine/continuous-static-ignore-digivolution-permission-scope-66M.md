# 66M continuous/static ignore digivolution permission scope

이번 작업은 `ContinuousOrStaticEffect` foundation 중 `IgnoreDigivolutionRequirement` semantics를 공통 static evolution requirement 경로에 연결한 범위다.
개별 `CardEffect` body는 구현하지 않았고, 특정 `CardId` 분기도 추가하지 않았다.

## 확인한 source

현재 git worktree에는 `DCGO/Assets` 원본 폴더가 없지만, local read-only 원본 경로 `E:\headlessDCGO\DCGO\Assets`에서 관련 원본 파일 본문을 확인했다.
Foundation Gate script는 workspace-local `DCGO/Assets`만 확인하므로 `localSourceRootAvailable=false`로 남는다.

- `docs/source/dcgo-source-file-manifest.json`
- `docs/rl-engine/continuous-static-requirement-descriptor-scope-66J.md`
- 원본 참조 경로: `DCGO/Assets/Scripts/Script/CardEffectFactory/AddDigivolutionRequirement.cs`
- 원본 참조 경로: `DCGO/Assets/Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPlayer/IgnoreDigivolutionRequirement.cs`
- local 확인 경로: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\AddDigivolutionRequirement.cs`
- local 확인 경로: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GiveEffect\GiveEffectToPlayer\IgnoreDigivolutionRequirement.cs`
- local 확인 경로: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`
- local 확인 경로: `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs`

원본 확인 결과:

- `GainIgnoreDigivolutionRequirementPlayerEffect`는 `EffectTiming.None` player effect로 `AddDigivolutionRequirementStaticEffect`를 추가한다.
- `DigivolveIntoHandOrTrashCard` 계열은 temporary `UntilCalculateFixedCost` player effect를 만들며, target permanent와 card owner/card condition을 gate로 사용한다.
- `AddDigivolutionRequirement.GetEvoCost`는 `ignoreDigivolutionRequirement`가 true이면 `cardSource.Owner.CanIgnoreDigivolutionRequirement(permanent, cardSource)`를 먼저 확인한다.
- `CanIgnoreDigivolutionRequirement`는 기본 true이며, field/player의 `ICannotIgnoreDigivolutionConditionEffect`가 있으면 false를 반환한다.
- color/level ignore는 `IgnoreRequirement.Color`, `IgnoreRequirement.Level`, `IgnoreRequirement.All`과 `CanIgnoreDigivolutionRequirement`가 함께 맞을 때 허용된다.

## RL.Engine 대응

`StaticEvolutionRequirementDescriptor.IgnoreDigivolutionRequirement`는 이제 unsupported marker가 아니라, 해당 static evolution requirement가 기본 `EvoCosts` 조건을 우회하는 permission임을 나타낸다.
평가는 기존 static requirement와 같은 source, controller, condition, metadata, color/level, cost 흐름을 그대로 사용한다.

- `StaticRequirementService`는 더 이상 `IgnoreDigivolutionRequirement=true` 자체를 unsupported로 처리하지 않는다.
- `StaticEvolutionRequirementEvaluation.IgnoresDigivolutionRequirement`가 평가 결과에 permission 여부를 보존한다.
- `BattleRules.CanDigivolve`, `LegalActionGenerator`, `DigivolveService`는 기존 static requirement fallback 경로를 그대로 공유한다.
- `StaticEffectService`의 digivolution cost modifier 적용 순서는 변경하지 않았다.

## 안전 장치

빈 descriptor로 unsupported를 숨기지 않기 위해 `IgnoreDigivolutionRequirement=true`인 descriptor는 explicit target gate를 요구한다.
다음 중 하나라도 있어야 한다.

- `RequiredColor`
- `RequiredLevel`
- `MinLevel` / `MaxLevel`
- `TargetPermanentCondition`
- `TargetMetadataCriteria`

gate가 없는 ignore descriptor는 `UnsupportedMechanicException`을 던진다.
이 예외는 silent no-op이 아니라, OpenCode/card-effect body 구현 전에 foundation descriptor가 충분히 구체적인지 검증하기 위한 장치다.

## 검증

- `Static evolution requirement ignore permission generates and executes`
- `Static evolution requirement ignore permission requires target gate`
- `Static requirement replay deterministic`

targeted 실행:

```text
All 6 tests passed.
```

전체 회귀:

```text
All 555 tests passed.
```

Foundation Completion Gate 재계산:

```text
openCodeReady=false
passedGateCount=9
failedGateCount=5
unknownCommonApiCount=39
unsupportedCapabilityCount=37
partiallyImplementedCapabilityCount=27
runtimeGeneratedStatusMismatchCount=92
silentNoOpCount=0
blockedEmptyDescriptorCount=13
coreCardIdBranchCount=0
directZoneMutationCount=0
sourceLockChangedCount=0
selectedNextFoundationCapability=ContinuousOrStaticEffect
selectedNextFoundationStatus=PartiallyImplemented
```

## 남은 범위

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다.
남은 공통 foundation 항목:

- trigger/keyword flow
- generated full-card parity evidence
- generated capability truth와 runtime status mismatch 축소
- `ICannotIgnoreDigivolutionConditionEffect` 대응 restriction layer
