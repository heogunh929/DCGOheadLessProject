# Full Card Porting C0016 Zone Security Recovery Source Mapping

## 결정

`C0016_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 29개 asset identity는 로컬 DCGO 원본에서 확인했지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 suspend cost, bounce/recovery result, security search/digivolve, hand/trash digivolve, play restriction/static modifier, security-after-battle play 공통 계층이 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT13_100` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_100.cs` | `OnStartTurn`: `SetMemoryTo3TamerEffect`. `OnEnterFieldAnyone`: owner turn, one of your Digimon digivolves into a Digimon matching source `HasPlantTraits` or `HasFairyTraits`; by suspending this Tamer, gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | start-turn Tamer memory setter, suspend-cost lifecycle, digivolve payload/trait check, security Tamer play 공통 계층이 필요하다. |
| `BT13_102` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_102.cs` | `OnEnterFieldAnyone`: on play, opponent may trash 1 Tamer or Option from hand through `SelectHandEffect`; if no discard happened, owner draws 1 and gains 1 memory. Second `OnEnterFieldAnyone`: opponent turn, an effect plays a Digimon; suspend this Tamer, gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | opponent hand selection/discard, optional no-select result, draw+memory fallback, by-effect played-permanent payload, suspend-cost, duplicate descriptor identity, security Tamer play이 필요하다. |
| `BT14_001` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_001.cs` | inherited `OnLoseSecurity`: owner turn once per turn, when opponent security is removed, draw 1. Source uses `CanTriggerWhenLoseSecurity` and hash `Draw1_BT14_001`. | source-aligned `OnLoseSecurity` payload, inherited source role, once-per-turn hash, draw trigger continuation이 필요하다. |
| `BT14_003` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_003.cs` | inherited `OnAddSecurity`: owner turn once per turn, when a card is added to owner's security, draw 1. Source uses `CanTriggerWhenAddSecurity` and hash `Draw1_BT14_003`. | source-aligned `OnAddSecurity` payload, inherited source role, once-per-turn hash, draw continuation이 필요하다. |
| `BT14_006` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_006.cs` | inherited `OnDiscardHand`: owner turn, when a `Dark Animal`/`DarkAnimal`/`SoC` Digimon card is trashed from owner's hand, this Digimon may digivolve into that trashed card from trash. Source filters `GetDiscardedCardsFromHashtable`, checks `IsExistOnTrash`, then calls `DigivolveIntoHandOrTrashCard` with `payCost: true` and `isHand: false`. | discarded-card payload, trash existence validation, trash-to-digivolve target selection, cost payment, source role snapshot/rollback이 필요하다. |
| `BT14_017` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_017.cs` | `OnEnterFieldAnyone`: `BlitzSelfEffect` when digivolving. `None`: `CanNotPutFieldClass` prevents opponent playing Digimon with DP 6000 or less while opponent memory is 1 or more and source is not in breeding. Second `None`: self DP +4000 under the same memory condition. | Blitz keyword trigger, play restriction hook, breeding exclusion, dynamic memory-condition static DP, continuous hash/replay 모델이 필요하다. |
| `BT14_027` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_027.cs` | two descriptors on `OnEnterFieldAnyone`: on play and when digivolving, return all level 3 Digimon to hand. Source checks unaffected/`CannotReturnToHand`, gathers both players in turn-player order, then calls `HandBounceClaass`. | mass bounce primitive, return-to-hand replacement/restriction, per-target success/failure events, duplicate descriptor ordering identity가 필요하다. |
| `BT14_030` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_030.cs` | two `OnEnterFieldAnyone` descriptors: on play and when digivolving, optionally select 1 opponent level 3 Digimon or 1 own Digimon, bounce it through `BouncePeremanentAndProcessAccordingToResult`, then if bounce succeeded select and bounce 1 opponent Digimon with level <= `LevelJustBeforeRemoveField`. `OnPermamemtReturnedToHand`: owner turn once per turn, when another Digimon returns to hand, `IRecovery` +1. Source currently gates that timing with `CanTriggerOnPermanentDeleted`. | stage-aware optional selection, bounce success continuation, `LevelJustBeforeRemoveField`, second mandatory selection, return-to-hand event payload, `IRecovery`, typo timing `OnPermamemtReturnedToHand`, duplicate descriptor identity가 필요하다. |
| `BT14_033` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_033.cs` | `OnStartMainPhase`: search own security, may select 1 yellow Vaccine Digimon, shuffle security with `RandomUtility.ShuffledDeckCards`, then play/digivolve it over this Digimon through `PlayCardClass` from `SelectCardEffect.Root.Security` without cost. If `IsDigivolvedByTheEffect`, may place 1 yellow Vaccine card from hand at bottom security with `CardObjectController.AddSecurityCard(toTop: false)`. inherited `OnAddSecurity`: owner turn once per turn, gain 1 memory. | security look/reveal policy, security order shuffle, security-root digivolve/play, `IsSecurityLooking`, digivolved-by-effect verification, optional hand-to-security-bottom, `OnAddSecurity` interleaving이 필요하다. |
| `BT14_034` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_034.cs` | `SecuritySkill`: `PlaySelfDigimonAfterBattleSecurityEffect`. inherited `OnDestroyedAnyone`: on deletion, if `CanActivateOnDeletion` and opponent Digimon exists, select 1 opponent Digimon and apply DP -3000 until each turn end. | security-after-battle play, deletion snapshot, inherited source role, target selection, DP duration `UntilEachTurnEnd`, RuleProcessor interleaving이 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT13-100#2767@base` | `BT13_100` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100.asset` | `needs-review` |
| `BT13-100#2768@P1` | `BT13_100` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P1.asset` | `needs-review` |
| `BT13-100#4619@P0` | `BT13_100` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P0.asset` | `needs-review` |
| `BT13-100#4620@P2` | `BT13_100` | `DCGO/Assets/CardBaseEntity/BT13/Green/Tamer/BT13_100_P2.asset` | `needs-review` |
| `BT13-102#2771@base` | `BT13_102` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102.asset` | `needs-review` |
| `BT13-102#2772@P1` | `BT13_102` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102_P1.asset` | `needs-review` |
| `BT13-102#4622@P0` | `BT13_102` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Tamer/BT13_102_P0.asset` | `needs-review` |
| `BT14-001#2912@base` | `BT14_001` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001.asset` | `needs-review` |
| `BT14-001#2913@P1` | `BT14_001` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P1.asset` | `needs-review` |
| `BT14-001#4632@P0` | `BT14_001` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P0.asset` | `needs-review` |
| `BT14-001#8164@P2` | `BT14_001` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P2.asset` | `needs-review` |
| `BT14-001#8165@P3` | `BT14_001` | `DCGO/Assets/CardBaseEntity/BT14/Red/DigiEgg/BT14_001_P3.asset` | `needs-review` |
| `BT14-003#2916@base` | `BT14_003` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003.asset` | `needs-review` |
| `BT14-003#2917@P1` | `BT14_003` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P1.asset` | `needs-review` |
| `BT14-003#4634@P0` | `BT14_003` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P0.asset` | `needs-review` |
| `BT14-006#2922@base` | `BT14_006` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006.asset` | `needs-review` |
| `BT14-006#2923@P1` | `BT14_006` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P1.asset` | `needs-review` |
| `BT14-006#4637@P0` | `BT14_006` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P0.asset` | `needs-review` |
| `BT14-006#8166@P2` | `BT14_006` | `DCGO/Assets/CardBaseEntity/BT14/Purple/DigiEgg/BT14_006_P2.asset` | `needs-review` |
| `BT14-017#2936@base` | `BT14_017` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017.asset` | `needs-review` |
| `BT14-017#4642@P0` | `BT14_017` | `DCGO/Assets/CardBaseEntity/BT14/Red/Digimon/BT14_017_P0.asset` | `needs-review` |
| `BT14-027#2948@base` | `BT14_027` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_027.asset` | `needs-review` |
| `BT14-030#2951@base` | `BT14_030` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030.asset` | `needs-review` |
| `BT14-030#4649@P0` | `BT14_030` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Digimon/BT14_030_P0.asset` | `needs-review` |
| `BT14-033#2954@base` | `BT14_033` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033.asset` | `needs-review` |
| `BT14-033#2955@P1` | `BT14_033` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P1.asset` | `needs-review` |
| `BT14-033#8173@P2` | `BT14_033` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P2.asset` | `needs-review` |
| `BT14-034#2956@base` | `BT14_034` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_034.asset` | `needs-review` |
| `BT14-034#4650@P0` | `BT14_034` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_034_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0016 uses `OnLoseSecurity`, `OnAddSecurity`, `OnDiscardHand`, `OnPermamemtReturnedToHand`, security search, recovery, and bounce-to-hand events.
- `BT13_100` and `BT13_102` require suspend cost to be represented as a common primitive with trace/replacement/nested-trigger boundaries, not direct `IsSuspended` mutation.
- `BT13_102` needs opponent optional discard semantics where the opponent's no-select result drives the owner's draw/memory fallback.
- `BT14_006` needs discarded-card payload and trash-to-digivolve support. The source-selected card must be the actual discarded card still in trash.
- `BT14_017` depends on static play restriction and dynamic DP continuous effects. `CanNotPutFieldClass` must be modeled as a generic play-permission layer, not a card-specific check.
- `BT14_027` and `BT14_030` require bounce result modeling through `HandBounceClaass` and `BouncePeremanentAndProcessAccordingToResult`, including success/failure and `LevelJustBeforeRemoveField`.
- `BT14_033` needs security-stack inspection, security shuffle, security-root digivolve/play, and hand-to-security-bottom operations to preserve source ordering and hidden information.
- `BT14_034` needs `PlaySelfDigimonAfterBattleSecurityEffect` and inherited deletion DP-duration handling through shared security/deletion/RuleProcessor layers.
- `BT14_027`, `BT14_030`, and `BT13_102` each have multiple descriptors on the same timing. Their descriptor identities must stay separate for ordering, replay, and source mapping.

## Follow-Up

The next generated queue item is `C0017_zone_security_recovery`. C0016 remains source-reviewed but blocked until shared zone/security/recovery, suspend-cost, bounce/recovery, security search/digivolve, play restriction/static modifier, and security-after-battle play layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0016 identity to runnable.
