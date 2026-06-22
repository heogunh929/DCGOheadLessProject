# 66S Continuous/Static Player-Wide Keyword Grant Scope

## 목적

66S는 원본 `CardEffectCommons.GainRushPlayerEffect(...)` / `GainBlockerPlayerEffect(...)` 계열처럼 player의 duration effect list에 `EffectTiming.None` keyword effect를 붙이는 공통 foundation 대응이다.

개별 `CardEffect` body 구현, C0039 이후 card-porting batch 실행, generated registry의 `Verified` 승격은 수행하지 않는다.

## 확인한 원본 source

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Rush.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Blocker.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GiveEffect\GiveEffectToPermanentOrPlayer.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\RushClass.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\BlockerClass.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs`

## 원본 의미

- `GainRushPlayerEffect(...)`와 `GainBlockerPlayerEffect(...)`는 `CardEffectFactory.*StaticEffect(...)`로 keyword effect를 만들고 `AddEffectToPlayer(..., EffectTiming.None)`로 카드 소유 player의 duration list에 등록한다.
- 원본 `Permanent` keyword query는 permanent source, linked/source card, player effect list를 모두 순회하고, `IRushEffect` / `IBlockerEffect`가 현재 permanent에 적용되는지 다시 묻는다.
- player-wide keyword는 battle area에 존재하는 target permanent와 `permanentCondition`을 통과한 대상에만 적용된다.

## RL.Engine 대응

- `TemporaryModifierKind.Keyword`가 `TargetPlayerId`를 갖는 player-wide keyword aura를 표현할 수 있게 했다.
- `TemporaryModifier.TargetMetadataCriteria`를 추가해 원본 `permanentCondition` 중 trait/name/text 기반 조건을 deterministic state로 보존한다.
- `Tier1PrimitiveService.AddTemporaryPlayerKeyword(...)`를 추가했다.
- `BattleKeywordService.HasKeyword(...)`는 player-target keyword modifier를 controller의 battle-area Digimon에만 적용하고, `TargetMetadataCriteria`가 있으면 target top card metadata로 gate한다.
- `GameState.ComputeStateHash()`, `RuleVisibleSnapshot`, `EngineInvariantChecker`가 player-wide keyword payload와 target metadata criteria를 공유한다.

## 검증

- `Duration player keyword grants Rush to matching battle area Digimon`
- `Duration player keyword hash and replay deterministic`
- 기존 `Duration temporary keyword ...` permanent-target 테스트는 유지한다.
- `CapabilityTruthAudit registry is conservative`는 새 evidence를 generated capability registry에서 직접 확인한다.

## 남은 범위

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다. 66T에서 duration-bound granted trigger effect flow를 별도 foundation 항목으로 닫았지만, full-card source parity evidence와 남은 continuous/static variant 검증은 계속 필요하다.
