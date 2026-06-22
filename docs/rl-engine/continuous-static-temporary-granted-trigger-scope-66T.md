# 66T Continuous/Static Temporary Granted Trigger Scope

## 목적

66T는 원본 `AddEffectToPermanent` / `AddEffectToPlayer`가 duration list에 `EffectTiming.None` keyword뿐 아니라 trigger timing effect factory도 보존하는 흐름 중, duration-bound granted trigger의 공통 foundation 대응이다.

개별 `CardEffect` body 구현, C0039 이후 card-porting batch 실행, generated registry의 `Verified` 승격은 수행하지 않는다.

## 확인한 원본 source

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\GiveEffect\GiveEffectToPermanentOrPlayer.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Retaliation.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Fortitude.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Alliance.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\KeyWordEffects\Retaliation.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\KeyWordEffects\Fortitude.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\KeyWordEffects\Alliance.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs`

## 원본 의미

- `GainRetaliation`, `GainFortitude`, `GainAlliance` 계열은 target permanent/player의 duration effect list에 timing을 가진 effect factory를 등록한다.
- `Permanent.EffectList(timing)`와 `Player.EffectList(timing)`는 원본 card/source effect와 duration으로 부여된 effect factory를 같은 timing query에서 펼친다.
- 따라서 temporary keyword query만으로는 `OnDestroyedAnyone`, `OnAllyAttack` 같은 granted trigger flow를 표현할 수 없다.

## RL.Engine 대응

- `TemporaryGrantedEffect`를 추가해 source card/permanent, controller, target permanent/player, timing, granted effect key, duration, metadata criteria를 deterministic state로 보존한다.
- `Tier1PrimitiveService.AddTemporaryGrantedTriggerEffect(...)`는 non-None timing, target 단일성, source/target 존재, stable id 중복을 검증한다.
- `TemporaryGrantedEffectRegistry`는 granted effect key를 descriptor 생성과 body resolve handler에 연결한다. handler가 없으면 explicit `UnsupportedMechanicException`으로 실패한다.
- `TriggerPipelineService`는 timing이 맞는 `TemporaryGrantedEffect`를 source 수집 이후 descriptor로 확장하고, target permanent grant는 field-top source snapshot으로 실행한다.
- `DurationCleanupService`, `GameState.ComputeStateHash()`, `RuleVisibleSnapshot`, `EngineInvariantChecker`가 temporary granted trigger payload를 보존하고 검증한다.

## 검증

- `Duration temporary granted trigger runs from target permanent timing`
- `Duration temporary granted trigger hash and replay deterministic`
- `Duration invariant detects invalid granted trigger`
- `CapabilityTruthAudit registry is conservative`

## 남은 범위

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다. 이번 작업은 duration-bound granted trigger source/timing flow를 공통 layer로 닫았지만, full-card source parity fixture가 아직 `NotRun`이고 남은 continuous/static variant들은 source-locked parity evidence가 필요하다.
