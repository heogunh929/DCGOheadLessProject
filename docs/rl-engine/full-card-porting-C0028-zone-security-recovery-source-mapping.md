# Full Card Porting C0028 Zone Security Recovery Source Mapping

## 결정

`C0028_zone_security_recovery`의 source body 10개와 asset identity 15개를 원본에서 확인했다.
일부 runnable 감소분은 구현했지만, batch 전체 카드 포팅 완료 조건에는 도달하지 못했다.
남은 항목은 source 불명확성이 아니라 공통 layer 미구현이므로 `needs-review`가 아닌 `blocked`로 둔다.

Queue status: blocked

Implemented reduction:

- `BT19_039`: inherited `[All Turns][Once Per Turn] When your security is reduced, you may unsuspend this Digimon.`을 optional OnLoseSecurity trigger와 `Tier1PrimitiveService.Unsuspend`로 구현했다.
- `BT19_045`: inherited All Turns DP +1000을 `ContinuousEffectDescriptor`로 구현했다.
- `BT19_052`: On Play / When Digivolving에서 owner face-up security 수에 따라 `2 + 2 * face-up security` play-cost threshold를 계산하고, 해당 이하 opponent Digimon 1장을 삭제하는 target selection body를 구현했다.
- `BT19_028`: field-top Blocker와 SecurityAttack +1은 `CardDefinition.BattleKeywords` 및 `CardDefinition.SecurityAttackModifier` metadata로 표현된다.
- `BT19_055`: inherited Reboot은 `CardDefinition.BattleKeywords` metadata로 표현된다.
- `BT19_067`: inherited Retaliation은 `CardDefinition.BattleKeywords` metadata로 표현된다.

No CardId branch. `BT19-027` base/P1/P2와 `BT19-052` base/P1/P2, `BT19-067` base/P1은 같은 source effect를 공유하지만 asset identity는 `DefinitionStableId`/`CardIndex`/variant로 분리해 보존한다. Catalog/core service/validator에는 특정 CardId 분기를 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL status |
| --- | --- | --- | --- |
| `BT19_027` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_027.cs` | WhenRemoveField DecodeSelfEffect, When Digivolving source level 4 이하 blue Digimon play, owner end turn own Digimon bottom-deck cost into opponent Digimon bottom-deck. | `Unsupported`: Decode, source-to-field play with ETB continuation, and permanent bottom-deck bounce layer required. |
| `BT19_028` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_028.cs` | None: this Digimon gets SecurityAttack +1 and Blocker. When Digivolving: unsuspend 1 own Digimon, optionally place Aqua/Sea Animal Digimon under this Digimon and gain 3 memory. | `PartiallyImplemented`: static keyword/security modifier metadata only. trait metadata and permanent-to-source placement remain blocked. |
| `BT19_039` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_039.cs` | OnPlay/WhenDigivolving trash top security, delete opponent level 4 이하 Digimon, gain 1 memory. OnDeletion Recovery +1. inherited OnLoseSecurity optional unsuspend. | `PartiallyImplemented`: inherited OnLoseSecurity unsuspend only. OnDeletion needs deleted-source trigger collection; OnPlay/WhenDigivolving need pre-selection mutation continuation. |
| `BT19_041` | `DCGO/Assets/Scripts/CardEffect/BT19/Yellow/BT19_041.cs` | OnPlay/WhenDigivolving trash top security, give own Digimon Blocker and DP +6000 until opponent turn end. WhenRemoveField Recovery +1 if security 2 이하. | `Unsupported`: pre-selection mutation continuation, temporary keyword grant, and source-aligned WhenRemoveField layer required. |
| `BT19_045` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_045.cs` | Royal Base alternate digivolution, Royal Base digivolution cost -1, inherited DP +1000, security-zone Royal Base DP aura. | `PartiallyImplemented`: inherited DP +1000 only. trait metadata, BeforePayCost cost modifier, and security-zone continuous source layer remain blocked. |
| `BT19_046` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_046.cs` | OnPlay/WhenDigivolving suspend 1 opponent Digimon, then a Data-trait opponent Digimon cannot unsuspend until opponent turn end. | `Unsupported`: trait metadata and cannot-unsuspend duration layer required. |
| `BT19_052` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_052.cs` | Royal Base alternate digivolution. OnPlay/WhenDigivolving delete opponent Digimon by play-cost threshold 2 plus face-up security. inherited OnEndBattle trash opponent top security. security-zone Royal Base Blocker aura. | `PartiallyImplemented`: OnPlay/WhenDigivolving deletion body implemented. OnEndBattle payload, trait metadata, and security-zone continuous Blocker aura remain blocked. |
| `BT19_055` | `DCGO/Assets/Scripts/CardEffect/BT19/Black/BT19_055.cs` | OnDeletion reveal top 3, add Knightmon/Twilight card to hand, place another under Tamer, rest bottom deck. None inherited Reboot. | `PartiallyImplemented`: inherited Reboot metadata only. text/trait search, reveal ordering, and card-to-Tamer-source placement remain blocked. |
| `BT19_067` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_067.cs` | OnPlay optionally play purple Tamer cost 4 이하 from trash if owner has 1 or fewer Tamers. inherited Retaliation. | `PartiallyImplemented`: inherited Retaliation metadata only. trash-to-field Tamer play with target/ETB continuation remains blocked. |
| `BT19_071` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs` | OnPlay/WhenDigivolving trash top 2 deck cards and gain Blocker until opponent turn end. OnDiscardLibrary once per turn delete opponent level 5 이하 Digimon. | `Unsupported`: library-discard event support and temporary keyword grant layer required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT19-027#3985@base` | `BT19_027` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027.asset` | `blocked` |
| `BT19-027#3986@P1` | `BT19_027` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027_P1.asset` | `blocked` |
| `BT19-027#8278@P2` | `BT19_027` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_027_P2.asset` | `blocked` |
| `BT19-028#3987@base` | `BT19_028` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_028.asset` | `partially-implemented` |
| `BT19-039#3991@base` | `BT19_039` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_039.asset` | `partially-implemented` |
| `BT19-041#3996@base` | `BT19_041` | `DCGO/Assets/CardBaseEntity/BT19/Yellow/Digimon/BT19_041.asset` | `blocked` |
| `BT19-045#3995@base` | `BT19_045` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_045.asset` | `partially-implemented` |
| `BT19-046#3993@base` | `BT19_046` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_046.asset` | `blocked` |
| `BT19-052#4006@base` | `BT19_052` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052.asset` | `partially-implemented` |
| `BT19-052#8282@P1` | `BT19_052` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052_P1.asset` | `partially-implemented` |
| `BT19-052#8283@P2` | `BT19_052` | `DCGO/Assets/CardBaseEntity/BT19/Green/Digimon/BT19_052_P2.asset` | `partially-implemented` |
| `BT19-055#4002@base` | `BT19_055` | `DCGO/Assets/CardBaseEntity/BT19/Black/Digimon/BT19_055.asset` | `partially-implemented` |
| `BT19-067#5049@base` | `BT19_067` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_067.asset` | `partially-implemented` |
| `BT19-067#8286@P1` | `BT19_067` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_067_P1.asset` | `partially-implemented` |
| `BT19-071#5051@base` | `BT19_071` | `DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_071.asset` | `blocked` |

## Remaining Common Layer Blockers

- DecodeSelfEffect and permanent bottom-deck bounce.
- Play from digivolution sources/trash with source-aligned target frame selection and ETB continuation.
- Trait/text metadata for Royal Base, Aqua, Sea Animal, Data, Knightmon, Twilight.
- pre-selection mutation continuation for effects that trash security before selecting the target.
- deleted-source trigger collection for self OnDeletion and deleted inherited sources.
- temporary keyword grant such as Blocker until opponent turn end.
- cannot-unsuspend duration support.
- BeforePayCost cost modifier frame.
- security-zone continuous source/aura support.
- OnEndBattle payload for battle-deletion triggers.
- library-discard event support for OnDiscardLibrary.

## Follow-Up

C0028 remains blocked until the common layers above are implemented. Implemented reductions are covered by unit tests, but card-porting completion must wait for actual remaining effect bodies, registry status update, tests, and blocker reduction across the batch.
