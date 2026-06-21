# Full Card Porting C0023 Zone Security Recovery Source Mapping

## 결정

`C0023_zone_security_recovery`는 source body 10개와 asset identity 22개를 확인했지만, 현재 공통 layer만으로 runnable card body를 만들면 원본 의미가 변형된다. 이 batch는 `L0006_zone_security_recovery` 위에 deck top/bottom ordering, security stack insertion/trash, source removal, dedigivolve, by-effect play payload, hatch, option security reuse, lowest-DP multi-card bottom-deck ordering, digivolution-card-origin play trigger, and trash digivolve continuation이 필요하다.

Queue status: needs-review

No CardId branch. `BT16_082`처럼 한 source effect가 많은 variant를 갖더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 각각 보존한다. Catalog나 core service에 특정 `CardId` 분기를 넣지 않는다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT16_054` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_054.cs` | On Play와 When Digivolving descriptor가 각각 존재한다. owner trash에서 D-Brigade/DigiPolice 3장을 `SelectCardEffect.Root.Trash` + `SelectCardEffect.Mode.Custom`으로 고르고 reverse한 뒤 `CardObjectController.AddLibraryTopCards`로 deck top에 되돌린다. 성공하면 this Digimon에게 `GainRush`와 `GainCanNotBeBlocked`를 `UntilEachTurnEnd`로 부여한다. `None`: inherited `ChangeDPStaticEffect`로 other own D-Brigade/DigiPolice +1000 DP. | trash-to-deck-top ordering, duplicate same-source descriptor identity, temporary Rush/cannot-be-blocked duration, and inherited team DP static layer are required. |
| `BT16_056` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_056.cs` | On Play와 When Digivolving descriptor는 optional로 opponent Vaccine Digimon 1장의 top card를 `RemoveFromAllArea`, `RemoveDigivolveRootEffect`, `CardObjectController.AddSecurityCard(..., true)`로 opponent security top에 놓는다. `OnAddSecurity`: once per turn `Publimon_BT16_056_AllTurns`; opponent security가 추가되고 3장 이상이면 `SelectionElement<bool>`로 top/bottom을 고른 뒤 `IDestroySecurity`로 trash한다. | moving field top to security top while preserving sources, source removal UI/state cleanup, OnAddSecurity payload, top/bottom boolean choice, security trash, and once-per-turn hash are required. |
| `BT16_060` | `DCGO/Assets/Scripts/CardEffect/BT16/Black/BT16_060.cs` | On Play와 When Digivolving: `RevealDeckTopCardsAndProcessForAll`로 top 3을 reveal하고 D-Brigade/DigiPolice 수만큼 opponent Digimon play cost를 `ChangePlayCostPlayerEffect`로 turn end까지 감소시킨 뒤 deck top/bottom에 돌려놓고 cost 4 이하 opponent Digimon 1장을 `SelectPermanentEffect.Mode.Destroy`로 delete한다. inherited `OnEnterFieldAnyone`: once per turn `De-Digivolve_BT16_060`; other own D-Brigade/DigiPolice가 played되면 opponent Digimon 1장을 `IDegeneration(1)` 한다. | reveal top-or-bottom ordering, play-cost duration layer, destroy target selection, by-play payload, inherited source validation, and dedigivolve primitive are required. |
| `BT16_068` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_068.cs` | On Play와 When Digivolving: own Digimon 1장에게 `GainBlocker` until opponent turn end. inherited `OnEnterFieldAnyone`: owner turn에 effect로 own Digimon이 played되면 once per turn `Draw1_BT16_068`로 draw 1. | temporary keyword duration, target selection, by-effect play payload, inherited source role, and draw timing are required. |
| `BT16_075` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_075.cs` | On Play와 When Digivolving: optional로 trash의 Dark Animal/DarkAnimal/Shaman Digimon 1장을 `SelectCardEffect.Mode.AddHand`로 hand에 되돌린다. inherited `OnEnterFieldAnyone`: owner turn에 effect로 own Digimon이 played되면 once per turn `Rush_BT16_075`, own Digimon 1장에게 Rush for turn. | trash-to-hand selection, trait alias handling, by-effect play payload, inherited source validation, temporary Rush duration, and once-per-turn hash are required. |
| `BT16_078` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_078.cs` | On Play와 When Digivolving: level 4 이하 Digimon 1장을 optional로 고르고 `DeletePeremanentAndProcessAccordingToResult`로 delete한다. `OnDestroyedAnyone`: owner turn에 effect로 another Digimon이 deleted되면 once per turn optional `PlayLevel5_BT16_078`; trash의 level 5 이하 Undead/DarkAnimal/Dark Animal Digimon 1장을 `SelectCardEffect.Root.Custom`에서 고르고 `PlayPermanentCards`로 no-cost play한다. | delete-success payload, effect-deletion trigger, trash free play with ETB, owner-turn gating, trait aliases, and once-per-turn hash are required. |
| `BT16_082` | `DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs` | `OnMove`: owner turn, own Digimon이 breeding에서 battle area로 이동하면 once per turn reveal top 3 with `CardEffectCommons.RevealDeckTopCardsAndSelect`, Digimon/Tamer 1장을 add hand, rest bottom deck. 그 뒤 owner가 `CanHatch`이면 `SelectionElement<bool>`로 hatch 여부를 묻고 true면 `HatchDigiEggClass`를 실행한다. | OnMove breeding-to-battle payload, reveal/search bottom-deck, follow-up optional hatch decision, HatchDigiEgg primitive, and multi-variant identity preservation are required. |
| `BT16_095` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_095.cs` | `OptionSkill`: opponent Digimon 최대 2장을 `SelectPermanentEffect.Mode.Tap`으로 suspend한다. 이어 suspended opponent Digimon 중 lowest DP 전부를 bottom deck으로 돌린다. 1장이면 `DeckBottomBounceClass`, 여러 장이면 `SelectCardEffect.Mode.Custom`/`SetUpSkillInfos`로 bottom-deck order를 정한 뒤 `DeckBottomBounceClass.SetNotShowCards`. 마지막으로 all own Digimon에게 `ChangeDigimonDPPlayerEffect(+3000, UntilOpponentTurnEnd)`. `SecuritySkill`: `CardEffectCommons.AddActivateMainOptionSecurityEffect`. | option body ordering, multi-target suspend lifecycle, lowest-DP group selection, bottom-deck order decision, global DP duration modifier, and security option reuse are required. |
| `BT17_002` | `DCGO/Assets/Scripts/CardEffect/BT17/Blue/BT17_002.cs` | inherited `OnEnterFieldAnyone`: owner turn, own Digimon이 digivolution cards에서 played되면 once per turn draw 1 through `DrawClass`, guarded by `CardEffectCommons.IsFromDigimonDigivolutionCards`. | play-origin payload from digivolution cards, inherited source validation, and draw timing are required. |
| `BT17_006` | `DCGO/Assets/Scripts/CardEffect/BT17/Purple/BT17_006.cs` | inherited `OnAddDigivolutionCards`: owner turn, effect가 this Digimon source에 Tamer card를 넣으면 once per turn optional `DigivolveTrash_BT17_006`; target permanent may digivolve into SoC Digimon in trash through `DigivolveIntoHandOrTrashCard`, paying cost and using frame legality. | OnAddDigivolutionCards payload, added-card/source-effect conditions, trash digivolve selection, frame legality, cost payment, and once-per-turn hash are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT16-054#3371@base` | `BT16_054` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_054.asset` | `needs-review` |
| `BT16-056#3373@base` | `BT16_056` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_056.asset` | `needs-review` |
| `BT16-060#3377@base` | `BT16_060` | `DCGO/Assets/CardBaseEntity/BT16/Black/Digimon/BT16_060.asset` | `needs-review` |
| `BT16-068#3386@base` | `BT16_068` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_068.asset` | `needs-review` |
| `BT16-075#3393@base` | `BT16_075` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_075.asset` | `needs-review` |
| `BT16-075#4807@P0` | `BT16_075` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_075_P0.asset` | `needs-review` |
| `BT16-078#3397@base` | `BT16_078` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_078.asset` | `needs-review` |
| `BT16-078#4810@P0` | `BT16_078` | `DCGO/Assets/CardBaseEntity/BT16/Purple/Digimon/BT16_078_P0.asset` | `needs-review` |
| `BT16-082#3403@base` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082.asset` | `needs-review` |
| `BT16-082#3404@P1` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P1.asset` | `needs-review` |
| `BT16-082#4812@P0` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P0.asset` | `needs-review` |
| `BT16-082#4813@P2` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P2.asset` | `needs-review` |
| `BT16-082#4814@P3` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P3.asset` | `needs-review` |
| `BT16-082#4815@P4` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P4.asset` | `needs-review` |
| `BT16-082#8209@P5` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P5.asset` | `needs-review` |
| `BT16-082#8210@P6` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P6.asset` | `needs-review` |
| `BT16-082#8211@P7` | `BT16_082` | `DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082_P7.asset` | `needs-review` |
| `BT16-095#3423@base` | `BT16_095` | `DCGO/Assets/CardBaseEntity/BT16/Green/Option/BT16_095.asset` | `needs-review` |
| `BT17-002#3542@base` | `BT17_002` | `DCGO/Assets/CardBaseEntity/BT17/Blue/DigiEgg/BT17_002.asset` | `needs-review` |
| `BT17-002#4834@P0` | `BT17_002` | `DCGO/Assets/CardBaseEntity/BT17/Blue/DigiEgg/BT17_002_P0.asset` | `needs-review` |
| `BT17-006#3546@base` | `BT17_006` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006.asset` | `needs-review` |
| `BT17-006#4838@P0` | `BT17_006` | `DCGO/Assets/CardBaseEntity/BT17/Purple/DigiEgg/BT17_006_P0.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0023 depends on field, source, security, trash, deck top, deck bottom, and breeding movement payloads.
- `BT16_054`, `BT16_060`, `BT16_082`, and `BT16_095` need explicit top/bottom deck order decisions before they can be runnable.
- `BT16_056` requires safe movement of a permanent top card to security while preserving/removing digivolution sources and emitting the right `OnAddSecurity` payload.
- `BT16_060` and `BT17_006` require dedigivolve/trash-digivolve primitives and source/trigger payloads, not card-specific shortcuts.
- `BT16_078` requires effect-deletion success and free trash play to keep ETB and replay behavior aligned.
- `BT16_082` requires follow-up hatch selection after reveal/search; a one-step reveal helper cannot silently drop the hatch decision.
- `BT16_095` requires multi-target suspend, lowest-DP group bounce, bottom-deck order selection, and security option reuse.
- `BT17_002` and `BT17_006` require play-from-digivolution-card and add-digivolution-card payloads before inherited effects can be safely implemented.

## Follow-Up

The next generated queue item is `C0024_zone_security_recovery`. C0023 remains source-reviewed but blocked until the shared deck order, security insertion/trash, dedigivolve, hatch, option security, by-effect play, source-add, trash digivolve, and multi-card bottom-deck layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0023 identity to runnable.
