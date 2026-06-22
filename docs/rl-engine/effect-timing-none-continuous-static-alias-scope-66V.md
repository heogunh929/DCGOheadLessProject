# 66V EffectTiming.None Continuous/Static Alias Scope

## 목적

`EffectTiming.None`을 별도 unsupported capability로 계산하던 foundation audit 중복을 제거한다.
원본 DCGO에서 `None`은 독립 trigger timing이 아니라 `EffectList(EffectTiming.None)`으로 조회되는 static/continuous effect channel이다.

## 확인한 원본 Source

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs`
  - `EffectTiming` enum의 첫 값이 `None`이다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\AutoProcessing.cs`
  - `Player.EffectList(EffectTiming.None)`와 `Permanent.EffectList(EffectTiming.None)`를 turn-end memory rule에 사용한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs`
  - option resolution replacement/static lookup에서 field/player/card `EffectList(EffectTiming.None)`를 모은다.

Generated scaffold 검증 결과 `EffectTiming.None`이 있는 2220개 source record는 모두 `flags.static_or_continuous=true`였다.

## 구현 범위

- `scripts/generate_full_mechanic_inventory.py`
  - worktree 안에 `DCGO/Assets`가 없을 때 local read-only source root `E:\headlessDCGO\DCGO\Assets`를 사용한다.
  - source lock manifest의 상대 경로와 hash를 `E:\headlessDCGO` base에서 검증한다.
  - `EffectTiming.None` mapping status를 `PartiallyImplemented`로 고정하고, `ContinuousOrStaticEffect` foundation coverage로 추적한다는 evidence note를 남긴다.
- `scripts/generate_capability_truth_audit.py`
  - timing capability alias `None -> ContinuousOrStaticEffect`를 적용한다.
  - `source-required-capabilities.json`과 `batch-capability-blockers.json`에는 raw `None` capability를 생성하지 않는다.
  - `capability-registry.json`의 `ContinuousOrStaticEffect` entry는 `inventoryAliases: ["None"]`, `affectedCardCount: 4326`, `sourceFileCount: 2220`를 기록한다.
- `src/DCGO.RL.Engine.Tests/Program.cs`
  - registry에 raw `None` capability가 없음을 검증한다.
  - source-required capability와 non-verified blocker에 raw `None`이 없음을 검증한다.

## Gate 결과

- `OpenCodeReady=false`
- passed gate: 11
- failed gate: 3
- unknown common API count: 28
- Unsupported capability count: 27
- PartiallyImplemented capability count: 36
- blocked empty descriptor count: 0
- legacy continuous-only empty descriptor count: 13
- selected next foundation capability: `ContinuousOrStaticEffect` (`PartiallyImplemented`)

`None` alias 정리는 blocker 중복을 줄였지만 `ContinuousOrStaticEffect`를 `Verified`로 승격하지 않는다.
full-card parity evidence는 여전히 `NotRun`이며, remaining common APIs/capabilities가 OpenCodeReady를 막고 있다.

## 금지 사항 준수

- 개별 `CardEffect` body를 구현하지 않았다.
- C0039 이후 card-porting batch를 실행하지 않았다.
- generated registry를 `Verified`로 수동 승격하지 않았다.
- `DCGO/Assets` 원본을 수정하지 않았다.
