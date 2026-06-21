# Full Card Porting C0029 Zone Security Recovery Source Mapping

## 결정

`C0029_zone_security_recovery`는 source body 10개와 asset identity 21개를 DCGO 원본에서 확인했다.
기존 공통 layer로 원본 의미를 보존할 수 있는 일부 효과만 실제 body로 구현했고, 나머지는 공통 layer 미구현이므로 `needs-review`가 아니라 `blocked`로 유지한다.

Queue status: blocked

Implemented reduction:

- `BT1_005`: inherited owner-turn DP +2000 while owner has 6 or more security cards를 `ContinuousEffectDescriptor`로 구현했다.
- `BT1_017`: On Play에서 owner battle-area Digimon 1체를 선택하고 SecurityAttack +1 until turn end를 duration modifier로 구현했다.
- `BT1_018`: field-top owner-turn memory 3 이상 조건의 self SecurityAttack +1을 `ContinuousEffectDescriptor`로 구현했다.
- `BT1_023`: On Play에서 opponent Blocker Digimon 1체를 선택해 삭제하는 target selection body를 구현했다.
- `BT19_084`: Start of Main Phase face-up security 조건 memory +1과 SecuritySkill play-self Tamer를 구현했다.
- `BT19_089`: SecuritySkill add-this-card-to-hand를 `Executing -> Hand` primitive로 구현했다.

No CardId branch. `BT1-010` base/P1/P2/P3/P4와 `P-042` base/P1/P2는 `BT1_010` source effect를 공유하지만, asset identity는 `DefinitionStableId`/`CardIndex`/variant로 분리해 보존한다. Catalog/core service/validator에는 특정 CardId 분기를 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL status |
| --- | --- | --- | --- |
| `BT19_077` | `DCGO/Assets/Scripts/CardEffect/BT19/White/BT19_077.cs` | None: self cannot attack/block. Main: suspend this Digimon, 1 own Digimon may digivolve from hand with cost -2. OnDeletion: place this card on top security. Security: may play 1 Digimon with 2000 DP or less from hand. | `Unsupported`: continuous attack/block restriction, suspend-cost digivolve, deleted-source trigger collection, and hand-to-field free-play with ETB continuation required. |
| `BT19_084` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_084.cs` | StartMainPhase: if owner has face-up security, gain 1 memory. Main: suspend this Tamer, digivolve an own Digimon using face-up security, then optionally place Royal Base Digimon from hand as face-up bottom security. Security: play self Tamer. | `PartiallyImplemented`: memory +1 and security play-self implemented. Main effect remains blocked by suspend-cost digivolve, trait metadata, face-up security digivolve, and hand-to-security placement layers. |
| `BT19_089` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_089.cs` | None: ignore color requirements if opponent has white Digimon/Tamer. OptionSkill: grant option immunity and cannot DP-reduce until opponent turn end. Security: add this card to hand. | `PartiallyImplemented`: Security add-to-hand implemented. ignore-color and effect-immunity/cannot-DP-reduce duration remain blocked. |
| `BT19_092` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_092.cs` | OptionSkill: optionally return own blue Digimon to deck bottom, then return opponent level 4 or level 6-or-lower Digimon to deck bottom depending on that cost. Security: activate main option. | `Unsupported`: return-to-library-bottom cut-in/replacement, chained optional target stages, and security main-option continuation required. |
| `BT19_096` | `DCGO/Assets/Scripts/CardEffect/BT19/Green/BT19_096.cs` | OptionSkill: may place Royal Base Digimon from trash face-up as bottom security, then delete opponent Digimon up to a dynamic total play-cost cap. Security: activate main option. | `Unsupported`: trait metadata, face-up bottom-security placement, multi-target sum constraint selection, deletion continuation, and security main-option continuation required. |
| `BT1_005` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_005.cs` | inherited DP +2000 during owner turn while owner has 6 or more security. | `Implemented`: continuous inherited DP modifier. |
| `BT1_010` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_010.cs` | On Play reveal top 5, add 1 Tamer among them to hand, place remaining cards at deck bottom in any order. | `Unsupported`: reveal/search ordering and bottom-deck ordering common layer required. `P-042` variants share this source effect. |
| `BT1_017` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_017.cs` | On Play select 1 owner Digimon; it gets SecurityAttack +1 for the turn. | `Implemented`: target selection plus temporary SecurityAttack modifier. |
| `BT1_018` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_018.cs` | field-top owner-turn SecurityAttack +1 while owner has 3 or more memory. | `Implemented`: continuous self SecurityAttack modifier. |
| `BT1_023` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_023.cs` | On Play delete 1 opponent Digimon with Blocker. | `Implemented`: target selection plus deletion primitive. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT1-005#139@base` | `BT1_005` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/DigiEgg/BT1_005.asset` | `implemented` |
| `BT1-010#145@base` | `BT1_010` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010.asset` | `blocked` |
| `BT1-010#146@P1` | `BT1_010` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P1.asset` | `blocked` |
| `BT1-010#147@P2` | `BT1_010` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P2.asset` | `blocked` |
| `BT1-010#148@P3` | `BT1_010` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P3.asset` | `blocked` |
| `BT1-010#4260@P4` | `BT1_010` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_010_P4.asset` | `blocked` |
| `BT1-017#156@base` | `BT1_017` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_017.asset` | `implemented` |
| `BT1-018#157@base` | `BT1_018` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_018.asset` | `implemented` |
| `BT1-023#164@base` | `BT1_023` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_023.asset` | `implemented` |
| `BT1-023#165@P1` | `BT1_023` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_023_P1.asset` | `implemented` |
| `BT19-077#5056@base` | `BT19_077` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_077.asset` | `blocked` |
| `BT19-077#8291@P1` | `BT19_077` | `DCGO/Assets/CardBaseEntity/BT19/White/Digimon/BT19_077_P1.asset` | `blocked` |
| `BT19-084#4021@base` | `BT19_084` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084.asset` | `partially-implemented` |
| `BT19-084#4022@P1` | `BT19_084` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084_P1.asset` | `partially-implemented` |
| `BT19-084#8296@P2` | `BT19_084` | `DCGO/Assets/CardBaseEntity/BT19/Green/Tamer/BT19_084_P2.asset` | `partially-implemented` |
| `BT19-089#5069@base` | `BT19_089` | `DCGO/Assets/CardBaseEntity/BT19/Red/Option/BT19_089.asset` | `partially-implemented` |
| `BT19-092#4023@base` | `BT19_092` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Option/BT19_092.asset` | `blocked` |
| `BT19-096#4024@base` | `BT19_096` | `DCGO/Assets/CardBaseEntity/BT19/Green/Option/BT19_096.asset` | `blocked` |
| `P-042#6082@base` | `BT1_010` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042.asset` | `blocked` |
| `P-042#10359@P1` | `BT1_010` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042_P1.asset` | `blocked` |
| `P-042#10360@P2` | `BT1_010` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_042_P2.asset` | `blocked` |

## Remaining Common Layer Blockers

- reveal/search ordering and deck-bottom ordering for `SimplifiedRevealDeckTopCardsAndSelect`.
- return-to-library-bottom cut-in/replacement and `WhenReturntoLibraryAnyone`/`WhenRemoveField` interleaving.
- continuous cannot attack/cannot block static restrictions.
- source-aligned suspend-cost digivolve and face-up security digivolve.
- hand-to-field free play with target frame and ETB continuation for security/hand effects.
- deleted-source trigger collection for self OnDeletion security placement.
- trait metadata for Royal Base and searchable text/trait filters.
- face-up bottom-security placement from hand/trash.
- multi-target sum constraint selection for dynamic play-cost deletion.
- ignore-color requirement and effect-immunity/cannot-DP-reduce duration layers.
- security main-option activation continuation for unsupported option bodies.

## Follow-Up

C0029 remains blocked until the common layers above are implemented. Implemented reductions are covered by focused unit tests, but card-porting completion still requires all source bodies, registry status update, tests, and baseline blocker reduction.
