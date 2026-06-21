# Full Card Porting C0018 Zone Security Recovery Source Mapping

## 결정

`C0018_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 27개 asset identity는 로컬 DCGO 원본에서 확인했다. 다만 runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 cost-sum multi-selection, reveal/play-from-library, library discard events, security top-to-hand plus security reduction, digivolution-source trash, suspend-cost, hand discard cost, inherited deletion source validation, and security Tamer play 공통 계층이 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT14_068` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_068.cs` | `OnEnterFieldAnyone`: When Digivolving, delete opponent Digimon up to total play cost 7 using `CanEndSelectCondition` and `CanTargetCondition_ByPreSelecetedList`. `None`: opponent turn, D-Brigade owner Digimon gain Blocker. `OnEndTurn`: owner end turn once per turn, reveal top 3, may play D-Brigade/DigiPolice cards up to total play cost 7 from library for free with `PlayPermanentCards`, trash rest. | cost-sum multi-target deletion, static Blocker aura, reveal-to-play from library, empty field frame count, remaining reveal trash, played-permanent payload, once-per-turn hash가 필요하다. |
| `BT14_077` | `DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_077.cs` | On Play and When Digivolving descriptors both trash top 2 cards of both players' decks in turn-player order through `IAddTrashCardsFromLibraryTop`, then show discarded cards. `OnDiscardLibrary`: owner turn once per turn, `CanTriggerWhenDiscardLibrary` sees opponent deck card trashed, then gain 1 memory with hash `Memory+_BT14_077`. | library-discard operation, per-player turn order, revealed discarded-card trace policy, `OnDiscardLibrary` payload, once-per-turn hash가 필요하다. |
| `BT14_082` | `DCGO/Assets/Scripts/CardEffect/BT14/Red/BT14_082.cs` | `OnStartMainPhase`: owner turn, select Vaccine Digimon and give DP +2000 until each turn end. `OnLoseSecurity`: owner turn, when opponent security is removed, suspend this Tamer and gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | start-main selection, DP duration, `OnLoseSecurity` payload, suspend-cost lifecycle, security Tamer play가 필요하다. |
| `BT14_083` | `DCGO/Assets/Scripts/CardEffect/BT14/Blue/BT14_083.cs` | `OnEnterFieldAnyone`: on play, trash any 1 digivolution card of 1 opponent Digimon via `SelectTrashDigivolutionCards`. `OnDigivolutionCardDiscarded`: owner turn, `CanTriggerOnTrashDigivolutionCard` sees opponent Digimon's digivolution card trashed, then suspend this Tamer and gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | digivolution-source trash selection, source trash protection, `OnDigivolutionCardDiscarded` payload, suspend-cost lifecycle, security Tamer play가 필요하다. |
| `BT14_084` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_084.cs` | `OnEnterFieldAnyone`: on play, move owner's top security card to hand with `CardObjectController.AddHandCards`, then run `IReduceSecurity`, then may place 1 yellow Vaccine card from hand to bottom security with `CardObjectController.AddSecurityCard(toTop: false)`. `OnAddSecurity`: owner turn, when own security gains a card, suspend this Tamer and gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | security top-to-hand, security reduction timing, hidden/public hand add trace, optional hand-to-security-bottom, `OnAddSecurity` interleaving, suspend-cost, security Tamer play가 필요하다. |
| `BT14_085` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_085.cs` | `OnEnterFieldAnyone`: on play, reveal top 3, add 1 Digimon with Plant/Fairy traits, rest deck bottom via `SimplifiedRevealDeckTopCardsAndSelect`. `OnTappedAnyone`: owner turn, when an effect suspends any Digimon, suspend this Tamer and gain 1 memory. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | reveal/search, trait mapping, deck-bottom ordering, effect-caused suspend payload, suspend-cost, security Tamer play가 필요하다. |
| `BT15_001` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_001.cs` | inherited `OnDestroyedAnyone`: on deletion, select 1 owner trash Digimon with Avian/Bird/Beast/Animal/Sovereign traits other than Sea Animal and add it to hand via `SelectCardEffect.Root.Trash`. | inherited deletion snapshot, trait helper parity, trash-to-hand movement, `OnAddHand` trigger interleaving이 필요하다. |
| `BT15_002` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_002.cs` | inherited `OnAddHand`: owner turn once per turn, when one of owner's Digimon effects adds cards to hand, this Digimon gets DP +1000 until opponent turn end. Source filters `CanTriggerWhenAddHand` by owner and `cardEffect.IsDigimonEffect`, hash `DP+1000_BT15_002`. | `OnAddHand` payload must include source effect identity/type, inherited source role, DP duration until opponent turn end, once-per-turn hash가 필요하다. |
| `BT15_006` | `DCGO/Assets/Scripts/CardEffect/BT15/Purple/BT15_006.cs` | inherited `OnDestroyedAnyone`: on deletion, optional discard 1 level 5+ Digimon from hand through `SelectHandEffect.Mode.Discard`; if discarded, draw 2 through `DrawClass`. | inherited deletion snapshot, optional hand discard cost, discard success continuation, draw boundary and `OnDraw`/`OnAddHand` interleaving이 필요하다. |
| `BT15_007` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_007.cs` | `OnStartMainPhase`: owner turn, optional discard 1 Avian/Bird/Beast/Sovereign Digimon from hand, then reveal top 4, add 1 red card, rest deck bottom. inherited `OnLoseSecurity`: owner turn once per turn, when opponent security is removed, gain 1 memory with hash `Memory+1_BT15_007`. | hand discard cost, reveal/search, deck-bottom ordering, inherited `OnLoseSecurity` payload, once-per-turn hash가 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT14-068#2994@base` | `BT14_068` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_068.asset` | `needs-review` |
| `BT14-068#2995@P1` | `BT14_068` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_068_P1.asset` | `needs-review` |
| `BT14-077#3005@base` | `BT14_077` | `DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_077.asset` | `needs-review` |
| `BT14-082#3011@base` | `BT14_082` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082.asset` | `needs-review` |
| `BT14-082#3012@P1` | `BT14_082` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082_P1.asset` | `needs-review` |
| `BT14-082#4692@P0` | `BT14_082` | `DCGO/Assets/CardBaseEntity/BT14/Red/Tamer/BT14_082_P0.asset` | `needs-review` |
| `BT14-083#3013@base` | `BT14_083` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083.asset` | `needs-review` |
| `BT14-083#3014@P1` | `BT14_083` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P1.asset` | `needs-review` |
| `BT14-083#4693@P0` | `BT14_083` | `DCGO/Assets/CardBaseEntity/BT14/Blue/Tamer/BT14_083_P0.asset` | `needs-review` |
| `BT14-084#3015@base` | `BT14_084` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084.asset` | `needs-review` |
| `BT14-084#3016@P1` | `BT14_084` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P1.asset` | `needs-review` |
| `BT14-084#4694@P0` | `BT14_084` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Tamer/BT14_084_P0.asset` | `needs-review` |
| `BT14-085#3017@base` | `BT14_085` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085.asset` | `needs-review` |
| `BT14-085#3018@P1` | `BT14_085` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P1.asset` | `needs-review` |
| `BT14-085#4695@P0` | `BT14_085` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P0.asset` | `needs-review` |
| `BT14-085#4696@P2` | `BT14_085` | `DCGO/Assets/CardBaseEntity/BT14/Green/Tamer/BT14_085_P2.asset` | `needs-review` |
| `BT15-001#3114@base` | `BT15_001` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001.asset` | `needs-review` |
| `BT15-001#3115@P1` | `BT15_001` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001_P1.asset` | `needs-review` |
| `BT15-001#4708@P0` | `BT15_001` | `DCGO/Assets/CardBaseEntity/BT15/Red/DigiEgg/BT15_001_P0.asset` | `needs-review` |
| `BT15-002#3116@base` | `BT15_002` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002.asset` | `needs-review` |
| `BT15-002#3117@P1` | `BT15_002` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P1.asset` | `needs-review` |
| `BT15-002#4709@P0` | `BT15_002` | `DCGO/Assets/CardBaseEntity/BT15/Blue/DigiEgg/BT15_002_P0.asset` | `needs-review` |
| `BT15-006#3124@base` | `BT15_006` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006.asset` | `needs-review` |
| `BT15-006#3125@P1` | `BT15_006` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006_P1.asset` | `needs-review` |
| `BT15-006#4713@P0` | `BT15_006` | `DCGO/Assets/CardBaseEntity/BT15/Purple/DigiEgg/BT15_006_P0.asset` | `needs-review` |
| `BT15-007#3126@base` | `BT15_007` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_007.asset` | `needs-review` |
| `BT15-007#3127@P1` | `BT15_007` | `DCGO/Assets/CardBaseEntity/BT15/Red/Digimon/BT15_007_P1.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0018 uses library discard, security add/remove, trash-to-hand, digivolution-source trash, and hand discard movement events.
- `BT14_068` and `BT14_077` have duplicated On Play/When Digivolving or end-turn reveal descriptors whose identity must remain separate for ordering/replay.
- `BT14_068` and `BT14_067`-like cost-sum logic require preselected-list legality, not a simple max-count target selection.
- `BT14_084` moves the top security card to hand before `IReduceSecurity`; this order and the resulting `OnLoseSecurity`/`OnAddHand` interleaving must match source.
- `BT14_083` and `BT15_002` require source-effect payloads for source trash and add-hand triggers; current card files must not infer those from zone deltas alone.
- `BT15_001`, `BT15_006`, and `BT15_007` depend on inherited source snapshot/revalidation after deletion/security loss.
- `BT14_082`, `BT14_083`, `BT14_084`, and `BT14_085` all use `PlaySelfTamerSecurityEffect`; security Tamer play must stay in shared security flow, not a card-specific shortcut.

## Follow-Up

The next generated queue item is `C0019_zone_security_recovery`. C0018 remains source-reviewed but blocked until shared zone/security/recovery, library discard, security top-to-hand, source-trash, add-hand, inherited deletion, suspend-cost, reveal/search, and cost-sum selection layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0018 identity to runnable.
