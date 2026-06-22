# 66N temporary keyword grant scope

이번 작업은 `ContinuousOrStaticEffect` foundation 중 target permanent에 duration-bound battle keyword를 부여하는 공통 runtime layer를 추가한 범위다. 개별 `CardEffect` body는 구현하지 않았고, C0039 이후 card-porting batch도 실행하지 않았다.

## 원본 source mapping

local read-only source root:

- `E:\headlessDCGO\DCGO\Assets`

확인한 원본:

- `Scripts/Script/CardEffectCommons/KeyWordEffects/Blocker.cs`
- `Scripts/Script/CardEffectCommons/KeyWordEffects/Rush.cs`
- `Scripts/Script/CardEffectCommons/KeyWordEffects/Jamming.cs`
- `Scripts/Script/CardEffectCommons/KeyWordEffects/Reboot.cs`
- `Scripts/Script/CardEffectCommons/KeyWordEffects/Collision.cs`
- `Scripts/Script/CardEffectCommons/KeyWordEffects/Retaliation.cs`
- `Scripts/Script/CardEffectCommons/GiveEffect/GiveEffectToPermanentOrPlayer.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs`
- `Scripts/Script/CardEffectFactory/KeyWordEffects/Retaliation.cs`

원본 `GainBlocker`, `GainRush`, `GainJamming`, `GainReboot`는 target permanent가 battle area에 존재하고 effect immunity에 막히지 않을 때, source card 기반 static keyword effect를 `AddEffectToPermanent(..., EffectTiming.None)`으로 duration 동안 붙인다.

`GainCollision`과 `GainRetaliation`은 원본에서 각각 `EffectTiming.OnCounterTiming`, `EffectTiming.OnDestroyedAnyone` timing으로 붙는다. RL.Engine의 battle resolution은 이미 `BattleKeywordService.HasKeyword` query를 중심으로 `Collision`, `Retaliation` 의미를 처리하지만, 이 작업은 해당 카드별 body나 granted trigger body를 새로 구현하지 않는다.

## RL.Engine 대응

- `TemporaryModifierKind.Keyword`를 추가했다.
- `TemporaryModifier.Keyword` payload를 추가해 duration modifier가 어떤 `BattleKeyword`를 부여하는지 state/hash/snapshot에 보존한다.
- `Tier1PrimitiveService.AddTemporaryKeyword(...)`를 추가했다.
- target permanent는 battle area Digimon이어야 한다.
- `BattleKeyword.Decoy`는 여전히 explicit unsupported다.
- `BattleKeyword.SecurityAttack`은 amount가 필요한 count modifier이므로 기존 `AddTemporarySecurityAttackModifier(...)`를 사용해야 한다.
- `BattleKeywordService.HasKeyword(...)`가 permanent/card metadata, continuous keyword descriptor, temporary keyword modifier를 같은 query 경로로 합산한다.
- `EngineInvariantChecker`는 keyword modifier가 permanent target과 keyword payload를 갖는지 검증한다.
- `GameState.ComputeStateHash()`와 `RuleVisibleSnapshot`은 keyword payload를 포함한다.

## 검증

Targeted:

- `Duration temporary keyword`: `All 4 tests passed.`
- `Duration`: `All 24 tests passed.`
- `BattleKeywords`: `All 9 tests passed.`

Full regression:

- `All 560 tests passed.`

Foundation Completion Gate 재계산:

- `openCodeReady=false`
- `passedGateCount=9`
- `failedGateCount=5`
- `unknownCommonApiCount=39`
- `unsupportedCapabilityCount=37`
- `partiallyImplementedCapabilityCount=27`
- `runtimeGeneratedStatusMismatchCount=92`
- `silentNoOpCount=0`
- `blockedEmptyDescriptorCount=13`
- `falseNoEffectCount=0`
- `variantIdentityConflictCount=0`
- `coreCardIdBranchCount=0`
- `directZoneMutationCount=0`
- `sourceLockChangedCount=0`
- `selectedNextFoundationCapability=ContinuousOrStaticEffect`
- `selectedNextFoundationStatus=PartiallyImplemented`
- `localSourceRootAvailable=false`

추가한 주요 테스트:

- `Duration temporary keyword grants Blocker until cleanup`
- `Duration temporary keyword grants Rush attack legality`
- `Duration temporary keyword hash and replay deterministic`
- `Duration temporary keyword rejects unsupported payloads`
- `Duration invariant detects invalid keyword modifier`

## 남은 범위

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다.

남은 공통 foundation 항목:

- generated full-card parity evidence
- generated capability truth와 runtime status mismatch 축소
- `ICannotIgnoreDigivolutionConditionEffect` 대응 restriction layer
- 66S에서 player-wide keyword grant는 닫혔고, duration-bound granted trigger effect flow는 아직 남아 있다.
- `Retaliation`, `Collision`, `Piercing` 등 원본 timing body와 연결되는 effect activation flow 검증

이 범위가 남아 있으므로 Foundation Gate 전에는 C0039 이후 card-porting batch와 개별 CardEffect body 구현을 시작하지 않는다.
