# C0032 zone/security/recovery source mapping

Queue status: blocked

## Source mapping

| SourceEffectClassName | DCGO source path | RL.Engine card file | Status | 구현/차단 근거 |
| --- | --- | --- | --- | --- |
| `BT20_001` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_001.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_001.cs` | `Implemented` | inherited owner-turn DP +2000 while host has 4+ sources and owner deck has at least 1 card를 continuous DP descriptor로 구현했다. |
| `BT20_003` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_003.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_003.cs` | `Unsupported` | owner Tamer with Pulsemon text or SoC/SEEKERS trait를 bottom source로 넣는 효과는 text/trait metadata, permanent-to-source movement, source ordering continuation이 필요하다. |
| `BT20_004` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_004.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_004.cs` | `Unsupported` | ACCEL hand digivolution with reduced cost는 hand digivolve/cost payment/digivolution continuation 공통 layer가 필요하다. |
| `BT20_006` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_006.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_006.cs` | `Unsupported` | optional OnDeletion trash-to-hand Ghost Digimon selection은 trait metadata, deleted-source trigger eligibility, optional trash card selection, L0006 return-to-hand continuation 연결이 필요하다. |
| `BT20_009` | `DCGO/Assets/Scripts/CardEffect/BT20/Red/BT20_009.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Red/BT20_009.cs` | `PartiallyImplemented` | inherited owner-turn DP +2000은 구현했다. purple Digimon play trigger에서 Free trait hand digivolve with cost -1은 hand digivolve/cost continuation blocker로 남긴다. |
| `BT20_034` | `DCGO/Assets/Scripts/CardEffect/BT20/Yellow/BT20_034.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Yellow/BT20_034.cs` | `Unsupported` | alternate digivolution, Fortitude, effect-disable duration, OnAddDigivolutionCards payload, battle-deletion top-security trash가 모두 공통 layer blocker다. |
| `BT20_039` | `DCGO/Assets/Scripts/CardEffect/BT20/Green/BT20_039.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Green/BT20_039.cs` | `PartiallyImplemented` | OnPlay/WhenDigivolving opponent Digimon suspend는 common primitive로 구현했다. inherited Piercing은 `CardDefinition.BattleKeywords` metadata로 검증한다. alternate ACCEL digivolution requirement는 blocker다. |
| `BT20_048` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_048.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_048.cs` | `PartiallyImplemented` | inherited opponent-turn DP +2000은 구현했다. alternate X Antibody digivolution과 reveal top 3/search/bottom-deck ordering은 공통 layer blocker다. |
| `BT20_049` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_049.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_049.cs` | `PartiallyImplemented` | inherited Reboot은 `CardDefinition.BattleKeywords` metadata로 검증한다. OnPlay/WhenDigivolving "cannot attack players"는 generic CannotAttack과 의미가 달라 defender-scoped attack restriction layer 전까지 blocker다. |
| `BT20_055` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_055.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_055.cs` | `Unsupported` | security-zone end-turn play-self, De-Digivolve 2, face-up security flip, deletion by source count, source-to-face-up-bottom-security가 모두 공통 layer blocker다. |

## 구현으로 감소한 baseline blocker

- `BT20_001`은 `Unsupported`가 아니라 실제 continuous DP body로 registry에 등록했다.
- `BT20_009`, `BT20_048`의 inherited DP는 continuous layer로 실제 계산된다.
- `BT20_039`의 OnPlay/WhenDigivolving suspend는 `SelectionResultApplicator`와 `Tier1PrimitiveService.Suspend`를 통해 실행된다.
- `BT20_039` Piercing과 `BT20_049` Reboot은 기존 keyword metadata 정책에 맞춰 fixture와 registry 상태에서 `PartiallyImplemented`로 추적한다.

## 남은 blocker

- hand digivolve와 digivolution cost reduction continuation
- trait/text metadata 기반 candidate 검색
- permanent-to-source movement와 source ordering
- reveal top N, multi-category selection, bottom-deck ordering
- Fortitude
- effect-disable duration과 WhenDigivolving activation suppression
- battle-deletion security trash
- defender-scoped attack restriction
- security-zone end-turn trigger source
- De-Digivolve, face-up security flip, source-to-face-up-bottom-security

## Guardrails

- No CardId branch was added to core services, Catalog, or validators.
- BT20 effect bodies stay in source-aligned per-card files under `src/DCGO.RL.Engine/CardEffects/BT20`.
- `DCGO/Assets/Scripts` remains read-only Source of Truth.
