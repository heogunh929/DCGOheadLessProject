# FND-003-M OnDetermineDoSecurityCheck Foundation Scope

## 범위

`OnDetermineDoSecurityCheck`는 전투 종료 결과가 나온 뒤 security check 여부를 최종 결정하기 위한 foundation runtime hook이다. 이번 작업은 카드별 `Pierce` body를 구현하지 않고, Unity 원본과 같은 위치에 headless trigger timing을 연결하는 데 한정한다.

## 원본 근거

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:4731`: `OnEndBattle` stack 이후 `OnDetermineDoSecurityCheck` 후보를 수집하고 첫 번째 active skill만 stack에 올린다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Permanent.cs:2591`: `Permanent.HasPierce`가 `EffectTiming.OnDetermineDoSecurityCheck` effect list를 확인한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\KeyWordEffects\Pierce.cs:78`: temporary `Pierce` grant가 `OnDetermineDoSecurityCheck` timing으로 등록된다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1013`: `EffectTiming.OnDetermineDoSecurityCheck` enum definition.

## Headless 반영

- `AttackService`는 permanent battle resolution과 `OnEndBattle` 이후, `DurationCleanupService.CleanupBattleEnd(...)` 및 piercing security check 이전에 `OnDetermineDoSecurityCheck`를 실행한다.
- `TriggerPipelineOptions.UseFirstActiveOnly`를 추가해 Unity 원본의 `skillInfos_Pierce[0]` behavior를 해당 timing에서만 표현한다.
- payload는 battle-end snapshot에 `SecurityCheckCount`, `ChecksCompleted`, `DoSecurityCheck`, `CanDoSecurityCheck`, `WillDoSecurityCheck`, `PiercingSecurityCheck`를 추가한다.
- `ContinueAfterDetermineDoSecurityCheck` continuation은 selection-bound effect가 pause/resume되어도 battle cleanup과 security check가 이후에 이어지도록 보존한다.

## 검증

- `FND-003-M OnDetermineDoSecurityCheck queues first active battle decision`
- `FND-003-M OnDetermineDoSecurityCheck sees battle durations before cleanup`
- `FND-003-M OnDetermineDoSecurityCheck selection pauses and resumes`
- Full regression: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 626 tests passed.`

## 남은 범위

- card-specific `Pierce` body wiring.
- direct attack `SecurityAttack` count parity.
- strict source `PutStackedSkill` stack object parity.
- broader security-check modifier source parity.
- `OnSecurityCheck` ordering fixture.

## 금지 유지

- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- Foundation Gate 수치 조작 없음.
- generated status 승격 없음.
- 원본 `DCGO` 수정 없음.
