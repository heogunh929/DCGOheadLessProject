# 66G Continuous/Static Player Runtime Scope

## 결정

66G는 `ContinuousOrStaticEffect` 전체를 완료하지 않고, 원본 `Player.EffectList(EffectTiming.None)` 중 현재 RL.Engine이 실제 상태 모델로 보존하는 player-level runtime stat modifier 범위만 검증한다.

## 원본 Mapping

원본 `DCGO/Assets/Scripts/Script/Player.cs`의 `EffectList(EffectTiming timing)`은 player에 걸린 다음 runtime effect list를 합친다.

- `PermanentEffects`
- `UntilEndBattleEffects`
- `UntilEachTurnEndEffects`
- `UntilOwnerTurnEndEffects`
- `UntilOwnerActivePhaseEffects`
- `UntilSecurityCheckEndEffects`
- `UntilOpponentTurnEndEffects`
- `UntilCalculateFixedCostEffect`

원본 `Permanent.cs`의 DP 계산과 `CardSource.cs`의 비용/조건 계산은 `player.EffectList(EffectTiming.None)`을 permanent/card source effect와 함께 조회한다.

## RL.Engine 대응

현재 RL.Engine에서 source-aligned하게 대응 가능한 player-level runtime stat 범위는 `GameState.TemporaryModifiers`다.

- player-target `TemporaryModifierKind.DP`
- player-target `TemporaryModifierKind.SecurityAttack`
- player-target `TemporaryModifierKind.SecurityDigimonDP`
- duration scope cleanup
- clone/restore/hash/replay 보존

이 상태는 `GameState.Clone`, `GameState.RestoreFrom`, `GameState.ComputeStateHash`에 포함된다. 따라서 같은 seed/action/selection trace에서 replay final state hash가 일치해야 한다.

## 검증

- `Duration player DP modifier affects owner battle area`
- `Duration player SecurityAttack modifier affects owner Digimon`
- `Duration player runtime modifiers clone restore hash`
- `Duration player runtime modifiers replay deterministic`
- targeted `Duration`: `All 19 tests passed.`

## 남은 Blocker

`ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`다.

- hand/trash/executing static requirement source
- digivolution/link requirement static effects
- trait/name/text metadata 기반 조건
- unsupported static effect interfaces such as cost/restriction/immunity variants
- generated full-card parity evidence

이 범위를 구현하기 전에는 C0039 또는 이후 card-porting batch를 실행하지 않는다.
