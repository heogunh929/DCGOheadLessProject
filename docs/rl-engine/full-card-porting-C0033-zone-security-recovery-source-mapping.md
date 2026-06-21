# C0033 zone/security/recovery source mapping

Queue status: blocked

## Source mapping

| SourceEffectClassName | DCGO source path | RL.Engine card file | Status | 구현/차단 근거 |
| --- | --- | --- | --- | --- |
| `BT20_062` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_062.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_062.cs` | `PartiallyImplemented` | field-top Retaliation은 `CardDefinition.BattleKeywords` metadata로 검증한다. inherited optional OnDeletion hand discard 뒤 opponent level 4 이하 delete는 deleted-source trigger eligibility, hand-discard cost continuation, target deletion continuation이 필요하다. |
| `BT20_063` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_063.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_063.cs` | `Unsupported` | OnPlay reveal top 3, Ghost/LIBERATOR multi-category select, rest bottom-deck는 trait metadata, reveal selection, bottom-deck ordering이 필요하다. inherited OnDeletion memory +1은 단순 memory body보다 삭제 전 source snapshot/eligibility가 먼저 필요해 blocked로 유지한다. |
| `BT20_065` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_065.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_065.cs` | `PartiallyImplemented` | inherited Retaliation은 metadata로 검증한다. OnPlay hand discard 뒤 opponent Digimon에 UntilOpponentTurnEnd OnDeletion memory -1 부여는 hand-discard cost continuation, granted trigger effect, duration cleanup, immunity check가 필요하다. |
| `BT20_067` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_067.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_067.cs` | `Unsupported` | OnPlay/WhenDigivolving temporary Retaliation grant는 temporary keyword grant와 duration layer가 필요하다. inherited optional OnDeletion hand discard 뒤 delete는 deleted-source trigger와 hand-discard continuation blocker가 남는다. |
| `BT20_069` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_069.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_069.cs` | `PartiallyImplemented` | inherited owner-turn DP +2000은 `ContinuousEffectDescriptor`로 구현했다. Evil trait alternate digivolution과 hand discard 뒤 temporary Blocker/Retaliation grant는 trait metadata, digivolution requirement, hand-discard cost continuation, temporary keyword duration layer가 필요하다. |
| `BT20_072` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_072.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_072.cs` | `Unsupported` | Execute와 OnDeletion/ESS trash-to-field Ghost level 4 이하 play는 Execute timing, trait metadata, play-from-trash continuation, ETB continuation, deleted-source trigger eligibility가 필요하다. |
| `BT20_075` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_075.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_075.cs` | `Unsupported` | inherited SecurityAttack +1은 owner hand count뿐 아니라 all owner Dark Dragon/Evil Dragon Digimon trait filter가 필요하다. CardId fixture로 trait를 대신하지 않고 trait metadata가 생길 때까지 blocked로 둔다. OnPlay/WhenDigivolving hand discard 뒤 Raid/Piercing/DP grant도 temporary keyword/duration layer가 필요하다. |
| `BT20_079` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_079.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_079.cs` | `PartiallyImplemented` | field-top SecurityAttack +1은 `CardDefinition.SecurityAttackModifier` metadata로, OnPlay/WhenDigivolving opponent lowest level Digimon delete는 selection + delete primitive로 구현했다. Execute와 Ghost level 5 이하 trash play OnPlay/OnDeletion은 Execute timing, trait metadata, trash play continuation, ETB continuation, deleted-source trigger eligibility가 필요하다. |
| `BT20_086` | `DCGO/Assets/Scripts/CardEffect/BT20/Black/BT20_086.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Black/BT20_086.cs` | `PartiallyImplemented` | OnStartTurn set memory to 3과 SecuritySkill play-self Tamer는 구현했다. StartMainPhase hand/trash Cyborg/Machine source placement와 opponent security face-up flip은 trait metadata, hand/trash-to-source movement, source ordering, face-up security flip continuation이 필요하다. |
| `BT20_088` | `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_088.cs` | `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_088.cs` | `PartiallyImplemented` | StartMainPhase opponent Digimon 조건 memory +1과 SecuritySkill play-self Tamer는 구현했다. Owner-turn OnDeletion suspend-cost Ghost hand digivolution은 trait metadata, suspend-cost continuation, hand digivolve cost reduction, deleted-source trigger payload coverage가 필요하다. |

## 구현으로 감소한 baseline blocker

- `BT20_069`의 inherited owner-turn DP +2000은 continuous stat 계산에 반영된다.
- `BT20_079`의 field-top SecurityAttack +1과 OnPlay/WhenDigivolving lowest level deletion은 실제 실행된다.
- `BT20_086`의 OnStartTurn memory set to 3과 security play-self Tamer가 실행된다.
- `BT20_088`의 StartMainPhase memory +1과 security play-self Tamer가 실행된다.
- `BT20_062`, `BT20_065`의 Retaliation capability는 기존 keyword metadata 정책으로 추적한다.

## 남은 blocker

- deleted-source trigger snapshot/eligibility
- trait/text metadata 기반 candidate 검색
- reveal top N, multi-category selection, bottom-deck ordering
- hand discard를 비용으로 지불한 뒤 이어지는 stage-aware continuation
- temporary keyword grant와 duration cleanup
- granted trigger effect 부여 및 effect-immunity 재검증
- Execute timing
- trash-to-field play, ETB continuation, hand digivolve/cost reduction
- source placement, source ordering, face-up security flip

## Guardrails

- No CardId branch was added to core services, Catalog, or validators.
- BT20 effect bodies stay in source-aligned per-card files under `src/DCGO.RL.Engine/CardEffects/BT20`.
- `DCGO/Assets/Scripts` remains read-only Source of Truth.
