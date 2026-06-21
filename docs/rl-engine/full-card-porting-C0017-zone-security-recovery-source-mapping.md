# Full Card Porting C0017 Zone Security Recovery Source Mapping

## 결정

`C0017_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 18개 asset identity는 로컬 DCGO 원본에서 확인했다. 다만 runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 hand-to-security, play-from-hand, recovery, suspend-cost, reveal/search, top/bottom deck placement, cannot-unsuspend duration, trash-to-deck-top, De-Digivolve, cost-sum deletion selection 공통 계층이 먼저 필요하다.

Queue status: needs-review

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT14_040` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_040.cs` | `OnEnterFieldAnyone`에 3개 descriptor가 있다. On Play와 When Digivolving은 optional로 Tamer 1장을 hand에서 security top에 놓고 `CardObjectController.AddSecurityCard`를 호출한다. All Turns once per turn descriptor는 owner Tamer가 played되면 level 3 Digimon을 hand에서 cost 없이 play한다(`PlayPermanentCards`, hash `Play1Digimon_Bt14_040`). | duplicate descriptor identity, hand-to-security-top, `OnAddSecurity` interleaving, hand play from effect, played-permanent payload, once-per-turn hash가 필요하다. |
| `BT14_041` | `DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_041.cs` | `OnEnterFieldAnyone`: When Digivolving, `IRecovery` +1 deck. `OnAddSecurity`: all turns once per turn, when owner security gains a card, optionally select opponent Digimon for DP -7000 until each turn end, then this Digimon gains Security Attack +1 through `ChangeDigimonSAttack` until each turn end. | recovery operation, source-aligned `OnAddSecurity` payload, DP duration, SecurityAttack duration, target selection optionality, once-per-turn hash가 필요하다. |
| `BT14_042` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_042.cs` | `OnEnterFieldAnyone`: On Play, suspend this Digimon as cost, then `SimplifiedRevealDeckTopCardsAndSelect` reveals top 3, add 1 green card to hand, rest deck bottom. | suspend-cost result, reveal/search, deck-bottom ordering, `OnAddHand`/draw-like trigger boundary가 필요하다. |
| `BT14_043` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_043.cs` | `OnEnterFieldAnyone`: On Play, optional select 1 own Digimon that can suspend, suspend it, and only if it remains suspended then select and suspend 1 opponent Digimon. | staged optional selection, suspend-cost success validation, unaffected/cannot-suspend checks, nested suspend event payload, second mandatory target selection이 필요하다. |
| `BT14_047` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_047.cs` | On Play and When Digivolving descriptors both suspend 1 opponent Digimon if possible, then apply `GainCanNotUnsuspendPlayerEffect` so opponent Digimon with DP <= 5000 do not unsuspend during opponent's next unsuspend phase. | suspend target selection, cannot-unsuspend player-level duration, DP condition snapshot/dynamic policy, owner active phase cleanup이 필요하다. |
| `BT14_050` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_050.cs` | On Play and When Digivolving descriptors both select 1 opponent Digimon and apply `GainCantUnsuspendUntilOpponentTurnEnd`. | target-specific cannot-unsuspend duration, cleanup timing, duplicate descriptor ordering identity가 필요하다. |
| `BT14_051` | `DCGO/Assets/Scripts/CardEffect/BT14/Green/BT14_051.cs` | `OnEndTurn`: end of opponent turn once per turn, optional select 1 own Digimon to suspend. If the selected Digimon actually becomes suspended, reveal top 5, add up to 2 green Digimon, rest deck bottom. Source hash is `Reveal_BT14_051`. | end-turn opponent timing, suspend-cost success continuation, reveal/search max 2, deck-bottom ordering, once-per-turn hash가 필요하다. |
| `BT14_061` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_061.cs` | On Play and When Digivolving descriptors both optionally select 1 Digimon from opponent trash, move it to deck top with `CardObjectController.AddLibraryTopCards`, reveal/log it through `ShowCardEffect`, then gain 1 memory. Source uses `SelectCardEffect.Root.Custom` over enemy trash and `SetNotShowCard`/`SetNotAddLog`. | trash-to-deck-top movement, hidden/public reveal trace policy, opponent trash custom root selection, memory gain continuation, duplicate descriptor identity가 필요하다. |
| `BT14_065` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_065.cs` | On Play and When Digivolving descriptors both make opponent reveal top 3 deck cards via `RevealDeckTopCardsAndProcessForAll`, count revealed Digimon, return revealed cards top or bottom, then if count >= 1 select 1 opponent Digimon and run `IDegeneration` by that count. | opponent-deck reveal, top-or-bottom placement ordering, revealed-card count payload, De-Digivolve level/count semantics, target selection이 필요하다. |
| `BT14_067` | `DCGO/Assets/Scripts/CardEffect/BT14/Black/BT14_067.cs` | On Play and When Digivolving descriptors both reveal opponent top 3, select 1 revealed Digimon, return all revealed cards top or bottom with `isSendAllCardsToSamePlace`, then delete opponent Digimon up to selected card play cost total using `CanEndSelectCondition` and `CanTargetCondition_ByPreSelecetedList`. | opponent-deck reveal, top/bottom same-place ordering, selected revealed-card payload, cost-sum target selection, multi-target deletion, deterministic replay of partial selection legality가 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT14-040#2963@base` | `BT14_040` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_040.asset` | `needs-review` |
| `BT14-040#4656@P0` | `BT14_040` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_040_P0.asset` | `needs-review` |
| `BT14-041#2964@base` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041.asset` | `needs-review` |
| `BT14-041#4657@P0` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P0.asset` | `needs-review` |
| `BT14-041#4658@P1` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P1.asset` | `needs-review` |
| `BT14-041#4659@P2` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P2.asset` | `needs-review` |
| `BT14-041#4660@P3` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P3.asset` | `needs-review` |
| `BT14-041#8174@P4` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P4.asset` | `needs-review` |
| `BT14-041#8175@P5` | `BT14_041` | `DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041_P5.asset` | `needs-review` |
| `BT14-042#2965@base` | `BT14_042` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_042.asset` | `needs-review` |
| `BT14-043#2966@base` | `BT14_043` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_043.asset` | `needs-review` |
| `BT14-047#2971@base` | `BT14_047` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_047.asset` | `needs-review` |
| `BT14-050#2975@base` | `BT14_050` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_050.asset` | `needs-review` |
| `BT14-051#2976@base` | `BT14_051` | `DCGO/Assets/CardBaseEntity/BT14/Green/Digimon/BT14_051.asset` | `needs-review` |
| `BT14-061#2987@base` | `BT14_061` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_061.asset` | `needs-review` |
| `BT14-065#2991@base` | `BT14_065` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_065.asset` | `needs-review` |
| `BT14-067#2993@base` | `BT14_067` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_067.asset` | `needs-review` |
| `BT14-067#4679@P0` | `BT14_067` | `DCGO/Assets/CardBaseEntity/BT14/Black/Digimon/BT14_067_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0017 needs hand-to-security, security add triggers, reveal/search, trash-to-deck-top, and top/bottom deck placement semantics.
- `BT14_040`, `BT14_061`, `BT14_065`, and `BT14_067` each have duplicated On Play/When Digivolving descriptors. Descriptor identity must be stable for ordering, once-per-turn, replay, and source mapping.
- `BT14_041` and `BT14_051` use once-per-turn hash strings and triggered continuations after security add or suspend success; rollback and replay must include those runtime states.
- `BT14_042`, `BT14_043`, and `BT14_051` depend on suspend-cost success. They must not proceed if suspend is blocked or the selected source leaves the field.
- `BT14_047` and `BT14_050` require source-aligned cannot-unsuspend duration and cleanup at opponent active/end-turn boundaries.
- `BT14_065` and `BT14_067` require opponent-deck reveal with top/bottom placement decisions. This cannot be approximated as reveal-to-trash or deterministic bottom-only placement.
- `BT14_067` requires cost-sum multi-target selection using both `CanEndSelectCondition` and `CanTargetCondition_ByPreSelecetedList`; a fixed max-count selector is insufficient.

## Follow-Up

The next generated queue item is `C0018_zone_security_recovery`. C0017 remains source-reviewed but blocked until shared zone/security/recovery, suspend-cost, reveal/search, top/bottom deck placement, cannot-unsuspend duration, trash-to-deck-top, De-Digivolve, and cost-sum deletion selection layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0017 identity to runnable.
