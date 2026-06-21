# Full Card Porting C0027 Zone Security Recovery Source Mapping

## 결정

`C0027_zone_security_recovery`는 source body 10개와 asset identity 12개를 확인했다. 이 batch는 일부 runnable 구현 감소분이 있지만 전체 카드 포팅 완료 조건은 충족하지 못하므로 `blocked`로 유지한다.

Queue status: blocked

Implemented reduction:

- `BT19_021`: On Play / When Digivolving 모두 opponent level 3 Digimon 1장을 hand로 반환한다. `ReturnPermanentToHandWithEvents`를 사용하므로 top card는 hand, source cards는 trash로 이동하고 return/leave-field pending rule event가 남는다. inherited Jamming은 `CardDefinition.BattleKeywords`로 표현한다.
- `BT18_098`: SecuritySkill 중 opponent Digimon DP 6000 이하 삭제와 owner security 0일 때 Recovery +1을 구현했다. Main option의 top security trash before target selection, bottom-security self placement, OnDiscardSecurity reactivation, ignore-color requirement는 아직 blocker다.
- `BT19_015`: When Digivolving DP 8000 이하 삭제 target body와 owner turn once-per-turn opponent deletion memory +2를 구현했다. 삭제가 실패하거나 target이 없을 때 DP +3000 및 Piercing을 얻는 fallback은 temporary keyword grant layer가 없어 blocker다.
- `BT19_026`: inherited All Turns DP +2000을 continuous descriptor로 구현했다. active body의 De-Digivolve, bounce, Save, source-under-Tamer play는 blocker다.

No CardId branch. BT18-090 base/P1과 BT18-093 base/P1은 같은 source effect를 공유하지만 asset identity를 `CardId`만으로 평탄화하지 않고 `DefinitionStableId`/`CardIndex`/variant로 분리해 보존한다. Catalog/core service/validator에는 특정 CardId 분기를 추가하지 않았다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL status |
| --- | --- | --- | --- |
| `BT18_087` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_087.cs` | SecuritySkill: `PlaySelfTamerSecurityEffect`. OnStartTurn: set memory to 3 Tamer. OnLoseSecurity: opponent security removed, suspend this Tamer as cost, delete opponent Digimon DP 4000 이하. | `Unsupported`: PlaySelfTamerSecurityEffect, set-memory-to-3 Tamer timing, and source-aligned suspend cost/replacement layer remain blockers. |
| `BT18_090` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_090.cs` | SecuritySkill: PlaySelfTamerSecurityEffect. OnStartMainPhase: discard hand card with [Hybrid]/[Ten Warriors] trait, then Draw 1. inherited OnEndBattle: when this Digimon deletes opponent by battle, play Tamer with inherited effects from hand for free. | `Unsupported`: searchable trait metadata, inherited-effect metadata, free Tamer play with ETB continuation, and security Tamer play are blockers. |
| `BT18_093` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_093.cs` | OnStartTurn set memory to 3. OnStartMainPhase discard Option or [Ghost]/[Three Musketeers] from hand, then Draw 1. SecuritySkill PlaySelfTamerSecurityEffect. | `Unsupported`: searchable trait metadata and security Tamer play remain blockers. |
| `BT18_098` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_098.cs` | OnDiscardSecurity activates its SecuritySkill from trash. None ignore color requirement if owner has yellow Data/Witchelny Digimon. OptionSkill trashes own top security first, applies DP -6000 until opponent turn end, then if owner security <=2 places this card bottom security. SecuritySkill deletes opponent Digimon DP 6000 이하, then if owner security 0 Recovery +1. | `PartiallyImplemented`: SecuritySkill delete/recovery body implemented. Main option ordering, bottom-security self placement, OnDiscardSecurity reactivation, and ignore-color requirement remain blocked. |
| `BT19_006` | `DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_006.cs` | inherited OnDeletion: if deleted other than by battle, return 1 level 3 purple Digimon card from trash to hand. | `Unsupported`: trash-to-hand primitive exists, but deleted inherited source collection and by-battle deletion payload are not source-aligned yet. |
| `BT19_015` | `DCGO/Assets/Scripts/CardEffect/BT19/Red/BT19_015.cs` | When Digivolving: delete opponent Digimon DP 8000 이하; if this effect did not delete, this Digimon gains DP +3000 and Piercing until opponent turn end. Your Turn once per turn OnDestroyedAnyone: when opponent Digimon deleted, gain 2 memory. | `PartiallyImplemented`: deletion target body and memory +2 implemented. temporary keyword grants for Piercing fallback remain blocked. |
| `BT19_016` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_016.cs` | OnPlay/OnDeletion: place 1 [Blue Flare] Digimon from hand under any owner Tamer, then Draw 1. | `Unsupported`: trait metadata and hand-to-Tamer-source placement with follow-up draw remain blockers. |
| `BT19_021` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_021.cs` | OnPlay and When Digivolving: return opponent level 3 Digimon to hand. None: inherited Jamming. | `Implemented`: selection + return-to-hand body implemented; inherited Jamming represented by `CardDefinition.BattleKeywords`. |
| `BT19_022` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_022.cs` | None: Blocker. OnDeletion: may place [Blue Flare] Digimon from trash under owner Tamer, then Save. None: inherited Blocker. | `PartiallyImplemented`: Blocker keywords are represented by `CardDefinition.BattleKeywords`. OnDeletion body requires trait metadata, trash-to-Tamer-source placement, SaveProcess, and deleted-source trigger collection. |
| `BT19_026` | `DCGO/Assets/Scripts/CardEffect/BT19/Blue/BT19_026.cs` | OnPlay/When Digivolving: De-Digivolve 2 opponent Digimon, then if opponent has 2 or more Digimon, return level 4 or lower Digimon to hand. OnDeletion: play [Blue Flare]/[Xros Heart] cost 5 이하 from under owner Tamer, then Save. None: inherited All Turns DP +2000. | `PartiallyImplemented`: inherited DP +2000 implemented as continuous effect. De-Digivolve, trait/source-under-Tamer play, and Save remain blockers. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT18-087#3952@base` | `BT18_087` | `DCGO/Assets/CardBaseEntity/BT18/Red/Tamer/BT18_087.asset` | `blocked` |
| `BT18-090#3959@base` | `BT18_090` | `DCGO/Assets/CardBaseEntity/BT18/Green/Tamer/BT18_090.asset` | `blocked` |
| `BT18-090#3960@P1` | `BT18_090` | `DCGO/Assets/CardBaseEntity/BT18/Green/Tamer/BT18_090_P1.asset` | `blocked` |
| `BT18-093#3964@base` | `BT18_093` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_093.asset` | `blocked` |
| `BT18-093#3965@P1` | `BT18_093` | `DCGO/Assets/CardBaseEntity/BT18/Purple/Tamer/BT18_093_P1.asset` | `blocked` |
| `BT18-098#3969@base` | `BT18_098` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Option/BT18_098.asset` | `partially-implemented` |
| `BT19-006#3979@base` | `BT19_006` | `DCGO/Assets/CardBaseEntity/BT19/Purple/DigiEgg/BT19_006.asset` | `blocked` |
| `BT19-015#5018@base` | `BT19_015` | `DCGO/Assets/CardBaseEntity/BT19/Red/Digimon/BT19_015.asset` | `partially-implemented` |
| `BT19-016#5019@base` | `BT19_016` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_016.asset` | `blocked` |
| `BT19-021#3988@base` | `BT19_021` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_021.asset` | `implemented` |
| `BT19-022#5021@base` | `BT19_022` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_022.asset` | `partially-implemented` |
| `BT19-026#5023@base` | `BT19_026` | `DCGO/Assets/CardBaseEntity/BT19/Blue/Digimon/BT19_026.asset` | `partially-implemented` |

## Remaining Common Layer Blockers

- `PlaySelfTamerSecurityEffect` and security Tamer play lifecycle.
- Tamer set-memory-to-3 start-turn boundary as a reusable common effect.
- Source-aligned suspend cost/replacement/restriction lifecycle for Tamer effects.
- Searchable trait metadata such as `[Hybrid]`, `[Ten Warriors]`, `[Ghost]`, `[Three Musketeers]`, `[Blue Flare]`, `[Xros Heart]`.
- Inherited-effect metadata on Tamer cards without CardId/name shortcuts.
- Free play from hand/source with ETB continuation and fallback handling.
- Main option body before target selection continuation for `BT18_098`.
- `OnDiscardSecurity` reactivation from trash and by-battle deletion payload.
- Deleted inherited source trigger collection after zone movement.
- Temporary keyword grants such as Piercing until opponent turn end.
- De-Digivolve, SaveProcess, and Tamer-source placement/play.

## Follow-Up

C0027 remains blocked until the above common layers are implemented and the remaining card bodies become runnable. `BT19_021` is the only fully implemented source body in this batch; `BT18_098`, `BT19_015`, `BT19_022`, and `BT19_026` reduce specific blocker surfaces but do not satisfy full card-porting completion.
