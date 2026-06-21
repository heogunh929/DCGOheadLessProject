# Full Card Porting C0025 Zone Security Recovery Source Mapping

## 결정

`C0025_zone_security_recovery`는 source body 10개와 asset identity 12개를 확인했다. 현재 공통 layer만으로 runnable card body를 만들면 원본의 비용 변경, 선언 timing, security 조작, security-zone static aura, memory prevention 의미가 쉽게 왜곡된다. 이 batch는 `L0006_zone_security_recovery` 위에 DP-delete max modifier, cannot-add-memory continuous rule, Hybrid/Ten Warriors dual reveal, digivolution trigger payload, BeforePayCost cost-reduction frame, declaration action from hand, trash-to-deck-bottom cost, breeding-area play, face-up bottom security, top security to hand, recovery, security-zone aura, and attack-player restriction layers가 필요하다.

Queue status: needs-review

No CardId branch. 같은 source effect를 여러 variant가 참조해도 `DefinitionStableId`, `CardIndex`, `variantKey`를 각각 보존한다. Catalog나 core service에 특정 `CardId` 분기를 추가하지 않는다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT18_008` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_008.cs` | `OnEnterFieldAnyone`: On Play, opponent Digimon 중 `permanent.DP <= card.Owner.MaxDP_DeleteEffect(2000, activateClass)`인 1장을 `SelectPermanentEffect.Mode.Destroy`로 delete한다. | DP-delete max modifier integration, opponent DP target legality, and destroy target selection are required. |
| `BT18_009` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_009.cs` | `None`: `CannotAddMemoryClass`; this card가 battle area에 있으면 opponent는 non-Tamer effect source의 memory gain을 할 수 없다. `SetUpCannotAddMemoryClass`는 `PlayerCondition`과 `CardEffectCondition`으로 opponent/non-Tamer source만 제한한다. | continuous memory-prevention rule, effect-source type classification, and memory gain replacement/restriction boundary are required. |
| `BT18_010` | `DCGO/Assets/Scripts/CardEffect/BT18/Red/BT18_010.cs` | `OnEnterFieldAnyone`: On Play, top 3 reveal 후 Hybrid/Ten Warriors 1장과 red inherited-effect Tamer 1장을 `SimplifiedRevealDeckTopCardsAndSelect`로 hand에 추가하고 rest bottom deck. 같은 timing의 owner-turn once-per-turn `Memory+1_BT18_010`: own Digimon/Tamer가 Hybrid/Ten Warriors Digimon으로 digivolve하면 `CanTriggerWhenPermanentDigivolving`과 `GetDigivolutionRootsFromEnterFieldHashtable`로 root를 검사해 memory +1. | dual reveal/search, bottom-deck placement, digivolution-to/from payload, root snapshot, duplicate same-timing descriptor identity, and once-per-turn hash are required. |
| `BT18_021` | `DCGO/Assets/Scripts/CardEffect/BT18/Blue/BT18_021.cs` | `BeforePayCost`: owner turn once per turn `ReduceDigivolutionCost-1_BT18_021`; this Digimon or any own Tamer would digivolve into multicolor blue/red Digimon이면 `ChangeCostClass`를 `UntilCalculateFixedCostEffect`에 추가하고 `ShowReducedCost`를 실행해 digivolution cost -1. `None`: inherited `JammingSelfStaticEffect`. | BeforePayCost trigger frame, digivolution cost modifier registration/cleanup, target permanent matching, multicolor condition, and inherited Jamming layer are required. |
| `BT18_031` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_031.cs` | `OnEnterFieldAnyone`: On Play, top 3 reveal 후 Hybrid/Ten Warriors 1장과 yellow inherited-effect Tamer 1장을 add hand, rest bottom deck. 같은 timing의 owner-turn once-per-turn `Memory+1_BT18_031`: inherited-effect Tamer가 played되면 memory +1. | dual reveal/search, inherited-effect Tamer predicate, permanent-play payload, duplicate same-timing descriptor identity, and once-per-turn hash are required. |
| `BT18_033` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_033.cs` | `OnDeclaration`: Main effect from hand. owner turn, breeding empty, hand contains this card, and trash has Three Great Angels Digimon. Optional로 trash card 1장을 `SelectCardEffect.Root.Trash`에서 골라 `CardObjectController.AddLibraryBottomCards`로 bottom deck에 놓고, 성공 시 this hand card를 `PlayPermanentCards(..., root: Hand, activateETB: true, isBreedingArea: true)`로 breeding area에 free play한다. play가 breeding area에 남지 않으면 `CardObjectController.AddTrashCard(card)` fallback을 실행한다. | declaration action from hand, trash-to-deck-bottom cost, breeding-area play, ETB continuation, hand source lifecycle, and fallback trash handling are required. |
| `BT18_038` | `DCGO/Assets/Scripts/CardEffect/BT18/Yellow/BT18_038.cs` | `None`: Angel level 4 `AddSelfDigivolutionRequirementStaticEffect` alternate digivolution cost 3. `OnEnterFieldAnyone` On Play and `OnDestroyedAnyone` On Deletion both let owner put Angel/Archangel/Three Great Angels Digimon from hand to bottom security with `CardObjectController.AddSecurityCard(..., toTop: false)`, then if security count >= 4 add top security to hand through `CardObjectController.AddHandCards` and `IReduceSecurity`. inherited `OnDestroyedAnyone`: `IRecovery(owner, 1)`. | alternate digivolution requirement, hand-to-bottom-security movement, top-security-to-hand continuation, security reduction timing, on-deletion source validation, and Recovery are required. |
| `BT18_043` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_043.cs` | `BeforePayCost`: owner turn once per turn `ReduceDigivolutionCost-1_BT18_043`; this Digimon or own Tamer would digivolve into multicolor green/red Digimon이면 `ChangeCostClass` cost -1 and `ShowReducedCost`. `OnDetermineDoSecurityCheck`: inherited `PierceSelfEffect`. | BeforePayCost trigger frame, temporary cost modifier semantics, multicolor green/red condition, and inherited Piercing timing are required. |
| `BT18_044` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_044.cs` | `None`: Royal Base level 2 `AddSelfDigivolutionRequirementStaticEffect` alternate digivolution cost 0. On Play: optional, Royal Base Digimon from hand is placed face-up as bottom security with `SelectHandEffect.Mode.PutSecurityBottom` and `SetIsFaceup`; then top security is added to hand and `IReduceSecurity` removes it. inherited `None`: self DP +1000 while battle area. Security-zone `None`: if `CardEffectCommons.IsExistInSecurity(card, false)`, all own Royal Base Digimon get +1000 DP via `ChangeDPStaticEffect`. | alternate digivolution requirement, face-up bottom security insertion, top-security-to-hand, security reduction, inherited DP static, and security-zone continuous aura are required. |
| `BT18_046` | `DCGO/Assets/Scripts/CardEffect/BT18/Green/BT18_046.cs` | `None`: Royal Base level 3 `AddSelfDigivolutionRequirementStaticEffect` alternate digivolution cost 2. Opponent turn continuous: while this card is top card on battle area, opponent Digimon with DP <= this Digimon DP cannot attack players through `CanNotAttackStaticEffect` with null defender condition. inherited `None`: self DP +1000. Security-zone `None`: if `CardEffectCommons.IsExistInSecurity(card, false)`, all own Royal Base Digimon get +1000 DP. | alternate digivolution requirement, opponent-turn attack-player restriction, dynamic DP comparison, inherited DP static, and security-zone continuous aura are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT18-008#3854@base` | `BT18_008` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_008.asset` | `needs-review` |
| `BT18-009#3855@base` | `BT18_009` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_009.asset` | `needs-review` |
| `BT18-009#8250@P1` | `BT18_009` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_009_P1.asset` | `needs-review` |
| `BT18-010#3859@base` | `BT18_010` | `DCGO/Assets/CardBaseEntity/BT18/Red/Digimon/BT18_010.asset` | `needs-review` |
| `BT18-021#3879@base` | `BT18_021` | `DCGO/Assets/CardBaseEntity/BT18/Blue/Digimon/BT18_021.asset` | `needs-review` |
| `BT18-031#3888@base` | `BT18_031` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_031.asset` | `needs-review` |
| `BT18-033#3889@base` | `BT18_033` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_033.asset` | `needs-review` |
| `BT18-033#8256@P1` | `BT18_033` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_033_P1.asset` | `needs-review` |
| `BT18-038#3881@base` | `BT18_038` | `DCGO/Assets/CardBaseEntity/BT18/Yellow/Digimon/BT18_038.asset` | `needs-review` |
| `BT18-043#3897@base` | `BT18_043` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_043.asset` | `needs-review` |
| `BT18-044#3893@base` | `BT18_044` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_044.asset` | `needs-review` |
| `BT18-046#3898@base` | `BT18_046` | `DCGO/Assets/CardBaseEntity/BT18/Green/Digimon/BT18_046.asset` | `needs-review` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `needs-review`; C0025 depends on deck, hand, field, security, trash, source, breeding, and cost-calculation payloads.
- `BT18_021` and `BT18_043` require source-aligned `BeforePayCost` continuation and temporary `ChangeCostClass` handling before cost reducers can be runnable.
- `BT18_033` requires OnDeclaration legal action from hand, trash-to-deck-bottom cost, breeding-area play, and fallback trash handling.
- `BT18_038` and `BT18_044` require hand-to-bottom-security, face-up security, top-security-to-hand, and `IReduceSecurity` sequencing.
- `BT18_009`, `BT18_044`, and `BT18_046` require continuous effects that are not ordinary field-only DP modifiers: memory gain restriction and security-zone Royal Base aura.
- `BT18_010` and `BT18_031` require duplicate same-timing descriptor ordering and precise permanent play/digivolve payloads before their once-per-turn memory effects are safe.

## Follow-Up

The next generated queue item is `C0026_zone_security_recovery`. C0025 remains source-reviewed but blocked until shared BeforePayCost, OnDeclaration hand action, breeding play, face-up security, security-zone aura, top-security-to-hand, memory-prevention, and security reduction layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0025 identity to runnable.
