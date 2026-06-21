# Full Card Porting C0024 Zone Security Recovery Source Mapping

## 결정

`C0024_zone_security_recovery`는 source body 10개와 asset identity 16개를 확인했다. 다만 현재 공통 layer만으로 runnable card body를 구현하면 원본 의미를 보존하기 어렵다. 이 batch는 `L0006_zone_security_recovery` 위에 dual reveal search, on-deletion free Tamer play, DP-delete max modifier, hand-to-source cost, delete-failure fallback, battle-deletion unsuspend, conditional security play, inherited Tamer/security keyword, face-up bottom security, top security to hand, security reduction, and library-top trash by opponent color count가 필요하다.

Queue status: needs-review

No CardId branch. 같은 source effect를 여러 asset variant가 참조해도 `DefinitionStableId`, `CardIndex`, `variantKey`를 각각 보존한다. Catalog나 core service에 특정 `CardId` 분기를 추가하지 않는다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT17_009` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_009.cs` | `OnEnterFieldAnyone`: On Play, top 3 reveal 후 Hybrid/Ten Warriors 1장과 inherited effect가 있는 Tamer 1장을 `SimplifiedRevealDeckTopCardsAndSelect`로 hand에 추가하고 rest bottom deck. inherited `OnDestroyedAnyone`: On Deletion, hand의 inherited-effect Tamer 1장을 `SelectHandEffect.Mode.Custom`으로 고르고 `PlayPermanentCards`로 free play한다. | dual reveal/search, bottom-deck placement, inherited-effect Tamer predicate, on-deletion source validation, optional hand Tamer play, and ETB play continuation are required. |
| `BT17_010` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_010.cs` | `OnEnterFieldAnyone`: When Digivolving, opponent Digimon 중 `permanent.DP <= owner.MaxDP_DeleteEffect(4000, activateClass)`인 1장을 고르고 `DeletePeremanentAndProcessAccordingToResult`로 delete한다. delete가 실패하면 this Digimon에게 `ChangeDigimonDP(+3000, UntilEachTurnEnd)`. `None`: inherited `ChangeDPDeleteEffectMaxDPClass`; owner memory <= 0이고 source가 같은 permanent에 있으면 source owner의 DP-based deletion max를 +2000. | DP-delete max modifier, source-scoped modifier validation, delete-success/failure continuation, target selection, and duration DP fallback are required. |
| `BT17_024` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_024.cs` | On Play와 When Digivolving: optional로 hand의 level 3 blue Digimon 1장을 `SelectHandEffect.Mode.Custom`으로 선택해 this permanent bottom source로 `AddDigivolutionCardsBottom`한 뒤 own Digimon 1장에게 `GainJamming(UntilEachTurnEnd)`. `None`: inherited `JammingSelfStaticEffect`. | hand-to-bottom-source movement, AddDigivolutionCards payload, follow-up target selection, temporary Jamming duration, and inherited Jamming layer are required. |
| `BT17_047` | `DCGO/Assets/Scripts/CardEffect/BT17/Green/BT17_047.cs` | On Play와 When Digivolving: opponent Digimon 1장을 선택하고 `SuspendPermanentsClass`로 suspend. inherited `OnDestroyedAnyone`: once per turn `Unsuspend_BT17_047`, this Digimon이 battle로 opponent Digimon을 delete하면 optional로 this Digimon을 `IUnsuspendPermanents`로 unsuspend. `SecuritySkill`: owner battle area Digimon이 0장일 때 `PlaySelfDigimonAfterBattleSecurityEffect`. | suspend lifecycle, battle-deletion payload, inherited source validation, unsuspend primitive, once-per-turn hash, and conditional security Digimon play are required. |
| `BT17_061` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_061.cs` | `OnEnterFieldAnyone`: On Play, optional로 other own Digimon 1장을 delete하고 성공 시 opponent level 4 이하 Digimon 1장을 `SelectPermanentEffect.Mode.Destroy`로 delete. inherited `OnDestroyedAnyone`: `RetaliationSelfEffect`. | own-delete cost, delete-success continuation, opponent level target legality, Retaliation inherited layer, and ordered deletion events are required. |
| `BT17_072` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_072.cs` | On Play와 When Digivolving: opponent unsuspended Digimon 1장을 `SelectPermanentEffect.Mode.Destroy`로 delete. `None`: other own level 6 permanent가 있으면 this Digimon gains `ChangeSelfDPStaticEffect(+2000)` and `ChangeSelfSAttackStaticEffect(+1)`. | unsuspended target legality, duplicated descriptor identity, static condition over other own level 6, DP and SecurityAttack continuous layers are required. |
| `BT17_079` | `DCGO/Assets/Scripts/CardEffect/BT17/Red/BT17_079.cs` | `SecuritySkill`: `PlaySelfTamerSecurityEffect`. `OnStartMainPhase`: owner turn and opponent has Digimon, memory +1. `None`: inherited owner-turn self DP +2000. `OnDetermineDoSecurityCheck`: inherited `PierceSelfEffect` while owner turn and this permanent DP >= 10000. | Tamer security play, start-main memory timing, inherited owner-turn DP static, dynamic DP-gated Piercing timing, and security-check determination hook are required. |
| `BT17_083` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_083.cs` | `SecuritySkill`: `PlaySelfTamerSecurityEffect`. `OnStartTurn`: `SetMemoryTo3TamerEffect`. inherited `OnAddHand`: once per turn `Gain1MemoryJamming_BT17_083`; owner turn에 effect가 owner hand에 card를 추가하면 memory +1 then this Digimon gains `GainJamming(UntilEachTurnEnd)`. | Tamer security play, start-turn memory, OnAddHand effect payload, memory gain, temporary Jamming, and once-per-turn hash are required. |
| `BT18_004` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_004.cs` | inherited `OnStartMainPhase`: optional, hand의 Royal Base Digimon 1장을 `SelectHandEffect.Mode.PutSecurityBottom` + `SetIsFaceup`으로 face-up bottom security에 놓고, 성공 시 top security를 `CardObjectController.AddHandCards`로 hand에 더한 뒤 `IReduceSecurity`로 security stack에서 제거한다. | face-up bottom security insertion, hand-to-security payload, top security to hand, security reduction timing, and follow-up continuation are required. |
| `BT18_006` | `DCGO/Assets/Scripts/CardEffect/BT18/Purple/BT18_006.cs` | inherited `OnDestroyedAnyone`: On Deletion, opponent battle area Digimon/Tamer top cards의 distinct color count만큼 `IAddTrashCardsFromLibraryTop`으로 owner deck top을 trash한다. | on-deletion source validation, opponent color aggregation, deterministic library-top trash, and deck-out/loss interaction are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT17-009#3549@base` | `BT17_009` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_009.asset` | `needs-review` |
| `BT17-010#3550@base` | `BT17_010` | `DCGO/Assets/CardBaseEntity/BT17/Red/Digimon/BT17_010.asset` | `needs-review` |
| `BT17-024#3567@base` | `BT17_024` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Digimon/BT17_024.asset` | `needs-review` |
| `BT17-047#3595@base` | `BT17_047` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_047.asset` | `needs-review` |
| `BT17-047#4856@P0` | `BT17_047` | `DCGO/Assets/CardBaseEntity/BT17/Green/Digimon/BT17_047_P0.asset` | `needs-review` |
| `BT17-061#3611@base` | `BT17_061` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_061.asset` | `needs-review` |
| `BT17-072#3622@base` | `BT17_072` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_072.asset` | `needs-review` |
| `BT17-072#4861@P0` | `BT17_072` | `DCGO/Assets/CardBaseEntity/BT17/Purple/Digimon/BT17_072_P0.asset` | `needs-review` |
| `BT17-079#3635@base` | `BT17_079` | `DCGO/Assets/CardBaseEntity/BT17/Red/Tamer/BT17_079.asset` | `needs-review` |
| `BT17-079#3636@P1` | `BT17_079` | `DCGO/Assets/CardBaseEntity/BT17/Red/Tamer/BT17_079_P1.asset` | `needs-review` |
| `BT17-079#4863@P0` | `BT17_079` | `DCGO/Assets/CardBaseEntity/BT17/Red/Tamer/BT17_079_P0.asset` | `needs-review` |
| `BT17-083#3643@base` | `BT17_083` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_083.asset` | `needs-review` |
| `BT17-083#3644@P1` | `BT17_083` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_083_P1.asset` | `needs-review` |
| `BT17-083#4868@P0` | `BT17_083` | `DCGO/Assets/CardBaseEntity/BT17/Blue/Tamer/BT17_083_P0.asset` | `needs-review` |
| `BT18-004#3850@base` | `BT18_004` | `DCGO/Assets/CardBaseEntity/BT18/Green/DigiEgg/BT18_004.asset` | `needs-review` |
| `BT18-006#3851@base` | `BT18_006` | `DCGO/Assets/CardBaseEntity/BT18/Purple/DigiEgg/BT18_006.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0024 depends on deck, hand, field, security, trash, source, and library-top movement payloads.
- `BT17_009` requires dual reveal search and inherited on-deletion free Tamer play with ETB.
- `BT17_010` requires source-scoped DP-delete max modifiers and failure continuation after attempted deletion.
- `BT17_024` and `BT18_004` require hand-to-source/security movement with follow-up effects only after successful placement.
- `BT17_047` requires battle-deletion payload and conditional security Digimon play; this cannot be reduced to always-play security.
- `BT17_079` and `BT17_083` require Tamer security play and Tamer memory timing tied to start-main/start-turn/on-add-hand triggers.
- `BT18_006` requires opponent board color counting before deck top trash and must preserve deterministic order and deck-out interaction.

## Follow-Up

The next generated queue item is `C0025_zone_security_recovery`. C0024 remains source-reviewed but blocked until shared dual reveal, source-add, face-up security, security top-to-hand, security reduction, DP-delete modifier, inherited Tamer/security keyword, on-add-hand, and library-top trash layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0024 identity to runnable.
