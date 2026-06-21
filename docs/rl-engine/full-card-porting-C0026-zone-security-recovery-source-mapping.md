# Full Card Porting C0026 Zone Security Recovery Source Mapping

## 결정

`C0026_zone_security_recovery`의 source body 10개와 asset identity 11개를 확인했다. 이 batch는 일부 실제 구현 감소분이 있지만, 전체 카드 포팅 완료 조건을 충족하지 않으므로 `blocked`로 유지한다.

Queue status: blocked

Implemented reduction:

- `BT18_080`: On Play / When Digivolving 두 단계 삭제 효과를 카드별 파일에 구현했다. 첫 단계는 상대 레벨 4 이하 red/green/purple/white Digimon 1장을 삭제하고, 이어서 상대 play cost 3 이하 blue/yellow/black/white Tamer 1장을 삭제한다. 두 단계 모두 `SelectionResultApplicator` continuation으로 처리한다.
- `BT18_080`: inherited Retaliation은 `CardDefinition.BattleKeywords`로 표현한다.
- `BT18_085`: battle area top card 상태에서 owner turn 동안 opponent trash의 서로 다른 색 수 / 2 만큼 SecurityAttack을 올리고, 같은 수 * 2000 DP를 continuous descriptor로 적용한다.

No CardId branch. BT18-056 base/P1은 같은 source effect를 공유하지만 asset identity는 `BT18-056#3915@base`, `BT18-056#3916@P1`로 분리해 보존한다. Catalog/core service/validator에 특정 CardId 분기는 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL status |
| --- | --- | --- | --- |
| `BT18_052` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_052.cs` | `None`: Royal Base alternate digivolution. `OnEnterFieldAnyone`: On Play와 When Digivolving 모두 face-up security 수만큼 상대 Digimon을 De-Digivolve 1. `OnEndBattle`: inherited once per turn으로 battle 삭제 시 상대 security top trash. `None`: face-up security static aura로 own Royal Base Digimon에게 Blocker 부여. | `Unsupported`: De-Digivolve, security-zone aura, inherited battle-deletion security trash layer가 필요하다. |
| `BT18_056` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_056.cs` | `None`: Royal Base alternate digivolution, Reboot. `OnDetermineDoSecurityCheck`: Piercing. `OnEnterFieldAnyone`: On Play/When Digivolving으로 face-up security 수만큼 상대 Digimon suspend 후 상대 Digimon 전체 cannot-unsuspend duration. `OnDestroyedAnyone`: All Turns once per turn optional unsuspend when another Digimon is deleted in battle. | `PartiallyImplemented`: Piercing/Reboot는 definition keyword로 표현 가능하지만 suspend/cannot-unsuspend와 battle-deletion payload가 blocker다. |
| `BT18_057` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_057.cs` | `BeforePayCost`: owner turn once per turn, this Digimon 또는 own Tamer가 black/yellow multicolor Digimon으로 digivolve할 때 cost -1. `None`: inherited Blocker. | `PartiallyImplemented`: inherited Blocker는 definition keyword로 표현 가능하지만 `BeforePayCost` cost modifier frame이 blocker다. |
| `BT18_058` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_058.cs` | `OnEnterFieldAnyone`: On Play optional으로 text에 Knightmon이 있는 hand card 1장을 trash하고 draw 2. `None`: inherited DP +1000. | `Unsupported`: RL `CardDefinition`에 searchable source text가 없어 `SelectHandEffect` predicate를 CardId/name으로 추측하지 않는다. |
| `BT18_059` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_059.cs` | `None`: battle area에 있으면 상대가 non-Tamer card effect로 memory를 gain하지 못한다. | `Unsupported`: `CannotAddMemoryClass`에 대응하는 effect-source type 기반 memory-gain prevention layer가 필요하다. |
| `BT18_062` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_062.cs` | `OnEnterFieldAnyone`: On Play/When Digivolving optional으로 text에 Knightmon이 있는 hand card 1장을 trash하고, 성공 시 own Digimon 1장에 opponent effect deletion immunity를 opponent turn end까지 부여. `None`: inherited DP +1000. | `Unsupported`: hand text-search와 effect-deletion immunity duration layer가 필요하다. |
| `BT18_068` | `DCGO/Assets/Scripts/CardEffect/BT18/Black/BT18_068.cs` | `None`: Blocker. `OnEnterFieldAnyone`: On Play/When Digivolving으로 either player deck top 5 reveal 후 top/bottom에 되돌림. | `PartiallyImplemented`: Blocker는 definition keyword로 표현 가능하지만 `RevealDeckTopCardsAndProcessForAll` 및 top/bottom ordering selection이 blocker다. |
| `BT18_075` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_075.cs` | `BeforePayCost`: owner turn once per turn, this Digimon 또는 own Tamer가 purple/yellow multicolor Digimon으로 digivolve할 때 cost -1. `OnDestroyedAnyone`: inherited Retaliation. | `PartiallyImplemented`: inherited Retaliation은 definition keyword로 표현 가능하지만 `BeforePayCost` cost modifier frame이 blocker다. |
| `BT18_080` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_080.cs` | `OnEnterFieldAnyone`: On Play/When Digivolving으로 상대 red/green/purple/white level 4 이하 Digimon 1장 삭제 후 상대 blue/yellow/black/white play cost 3 이하 Tamer 1장 삭제. `OnDestroyedAnyone`: inherited Retaliation. | `Implemented`: 두 단계 target selection + delete body 구현 완료. |
| `BT18_085` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_085.cs` | `None`: not-in-field self digivolution cost reduction by opponent trash distinct color count. `None`: battle area owner turn SecurityAttack +N and DP +2000N where N is opponent trash distinct color count / 2. | `PartiallyImplemented`: battle-area continuous DP/SecurityAttack 구현 완료. not-in-field cost reduction은 cost resolver/BeforePayCost layer blocker다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT18-052#3911@base` | `BT18_052` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_052.asset` | `blocked` |
| `BT18-056#3915@base` | `BT18_056` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_056.asset` | `blocked` |
| `BT18-056#3916@P1` | `BT18_056` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_056_P1.asset` | `blocked` |
| `BT18-057#3906@base` | `BT18_057` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_057.asset` | `blocked` |
| `BT18-058#3907@base` | `BT18_058` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_058.asset` | `blocked` |
| `BT18-059#3908@base` | `BT18_059` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_059.asset` | `blocked` |
| `BT18-062#3919@base` | `BT18_062` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_062.asset` | `blocked` |
| `BT18-068#3928@base` | `BT18_068` | `DCGO/Assets/CardBaseEntity/BT18/Black/Digimon/BT18_068.asset` | `blocked` |
| `BT18-075#3938@base` | `BT18_075` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_075.asset` | `blocked` |
| `BT18-080#3949@base` | `BT18_080` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_080.asset` | `implemented` |
| `BT18-085#3951@base` | `BT18_085` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Digimon/BT18_085.asset` | `partially-implemented` |

## Remaining Common Layer Blockers

- De-Digivolve and security-card trash timing after inherited battle deletion.
- Security-zone static aura from face-up security.
- `BeforePayCost` cost modifier frame and cost resolver integration.
- Hand `SelectHandEffect` by source text, not CardId/name shortcut.
- Memory-gain prevention by effect source type (`CannotAddMemoryClass`).
- Cannot-unsuspend duration and effect-deletion immunity duration.
- Reveal top N from either player's deck with source-aligned top/bottom placement ordering.

## Follow-Up

C0026 remains blocked until the above common layers are implemented and the remaining card bodies are made runnable. `BT18_080` reduces the unsupported baseline by one fully runnable source body, and `BT18_085` reduces the continuous-effect blocker but keeps deck validation blocked because its not-in-field cost reduction is still missing.
