# Full Card Porting C0022 Zone Security Recovery Source Mapping

## 결정

`C0022_zone_security_recovery`는 `done`으로 처리할 수 있는 카드 구현 batch가 아니다. DCGO 원본 source body 10개와 asset identity 20개는 모두 확인했지만, 현재 RL engine의 공통 zone/security/recovery layer만으로는 효과 의미를 안전하게 실행할 수 없다. `L0006_zone_security_recovery`의 tamer security play, option security reuse, reveal/search bottom-deck ordering, hand discard cost continuation, temporary play-cost modifier, inherited/static source role, security stack mutation, duration cleanup, battle-deletion payload가 먼저 source-aligned로 정리되어야 한다.

Queue status: blocked

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT15_082` | `DCGO/Assets/Scripts/CardEffect/BT15/Red/BT15_082.cs` | `OnStartTurn`: `SetMemoryTo3TamerEffect`. `OnReturnCardsToHandFromTrash`: owner red card가 trash에서 hand로 돌아오면 optional로 이 Tamer를 `BouncePeremanentAndProcessAccordingToResult`로 hand에 되돌리고, 성공 시 `SelectHandEffect.Mode.Custom`으로 hand에서 red Digimon 1장을 고른 뒤 `PlayPermanentCards`로 free play한다. playable DP cap은 `13000 - opponent security count * 2000`이고 trait은 Avian/Bird/Beast/Animal/Sovereign 단, Sea Animal 제외 helper에 의존한다. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | return-this-tamer cost, bounce-success continuation, dynamic opponent-security DP cap, trait helper, no-cost hand play with ETB, and Tamer security play are required. |
| `BT15_084` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_084.cs` | `OnDiscardSecurity`: effect로 이 card가 security에서 trash되면 opponent Digimon 1장에게 `ChangeDigimonSAttack(-1, UntilOpponentTurnEnd)`. `OnStartTurn`: `SetMemoryTo3TamerEffect`. `OnLoseSecurity`: once per turn optional, owner security가 effect로 줄어들면 이 Tamer를 `SuspendPermanentsClass`로 suspend한 뒤 opponent Digimon 1장에게 Security Attack -1 until opponent turn end, hash `AllTurns_BT15_084`. `SecuritySkill`: `PlaySelfTamerSecurityEffect`. | OnDiscardSecurity and OnLoseSecurity payloads, effect-source check, suspend cost lifecycle, SecurityAttack duration modifier, once-per-turn hash, and Tamer security play are required. |
| `BT15_097` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_097.cs` | `OptionSkill`: hand에서 Machine/Cyborg/SoC Digimon 1장을 `SelectHandEffect.Mode.Discard`로 trash하면 opponent Digimon/Tamer 중 lowest play cost 1장을 `SelectPermanentEffect.Mode.Destroy`로 delete한다. `SecuritySkill`: `CardEffectCommons.AddActivateMainOptionSecurityEffect`로 main option body를 security에서 활성화한다. | option security reuse, discard-as-cost continuation, lowest-cost Digimon/Tamer target legality, destroy target selection, and security option activation boundary are required. |
| `BT16_006` | `DCGO/Assets/Scripts/CardEffect/BT16/Purple/BT16_006.cs` | inherited `OnDestroyedAnyone`: own deletion source 조건이면 optional로 hand 1장을 `SelectHandEffect.Mode.Discard`하고 성공 시 owner memory +1. | inherited deletion source role, `CanActivateOnDeletion`, optional discard cost, memory gain, and once-per-event source snapshot are required. |
| `BT16_020` | `DCGO/Assets/Scripts/CardEffect/BT16/Blue/BT16_020.cs` | `None`: level 3 Light Fang/Night Claw from field에 대한 `AddSelfDigivolutionRequirementStaticEffect` cost 2와 inherited `JammingSelfStaticEffect`. `OnEnterFieldAnyone`: When Digivolving, both players draw 1 in turn-player order through `DrawClass`; then opponent hand >=8 or this permanent source count >=3이면 memory +1. | alternate digivolution requirement, inherited Jamming, both-player draw ordering, source-count condition, and memory timing are required. |
| `BT16_029` | `DCGO/Assets/Scripts/CardEffect/BT16/Yellow/BT16_029.cs` | `None`: level 2 Light Fang/Night Claw from field에 대한 alternate digivolution cost 0. `OnEnterFieldAnyone`: On Play reveal top 3 through `SimplifiedRevealDeckTopCardsAndSelect`, add 1 Light Fang and 1 Night Claw or 2-color card, rest bottom deck. inherited `None`: owner turn 동안 opponent security Digimon card에게 `ChangeSecurityDigimonCardDPStaticEffect(-3000)`. | reveal dual-pick, bottom-deck placement, alternate digivolution requirement, inherited security-Digimon DP static layer, and owner-turn condition are required. |
| `BT16_037` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_037.cs` | `OnEnterFieldAnyone`: On Play reveal top 4, add 1 Insectoid, rest bottom deck with `SimplifiedRevealDeckTopCardsAndSelect`. inherited `None`: this permanent이 suspended면 `ChangeSelfDPStaticEffect(+1000)`. | reveal/search bottom-deck ordering and inherited suspended-state DP static layer are required. |
| `BT16_042` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_042.cs` | 같은 timing `OnEnterFieldAnyone`에 On Play descriptor와 When Digivolving descriptor가 각각 존재하며, 둘 다 own Digimon 1장에게 `ChangeDigimonDP(+3000, UntilOpponentTurnEnd)`. inherited `None`: suspended면 self DP +1000. | duplicate same-source descriptor identity/ordering, target selection, duration DP modifier, On Play vs When Digivolving trigger split, and inherited suspended-state static layer are required. |
| `BT16_047` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_047.cs` | `None`: Pulsemon level 5 alternate digivolution cost 3. `OnEnterFieldAnyone`: When Digivolving, opponent Digimon 1장을 `SelectPermanentEffect.Mode.Tap`으로 suspend한 뒤 opponent Digimon 1장에게 `GainCantUnsuspendUntilOpponentTurnEnd`. `OnEndBattle`: this Digimon이 opponent Digimon을 battle로 delete하면 owner security >=3이면 `IDestroySecurity`로 opponent top security trash, owner security <=3이면 memory +2. 정확히 3이면 두 branch가 모두 실행된다. | alternate digivolution requirement, suspend target lifecycle, cannot-unsuspend duration, battle-deletion payload, top-security trash, memory gain, and overlapping security-count branch ordering are required. |
| `BT16_048` | `DCGO/Assets/Scripts/CardEffect/BT16/Green/BT16_048.cs` | `None`: level 6 Insectoid cost 13 이하에서 ignore requirement alternate digivolution cost 2. `OnEnterFieldAnyone`: once per turn optional When Digivolving, `ChangeCostClass`를 `UntilCalculateFixedCostEffect`에 임시 추가해 Insectoid/Larva hand Digimon의 play cost를 -8로 계산하고 `SelectHandEffect.Mode.Custom`으로 선택한 뒤 `PlayPermanentCards`로 play하고 modifier를 제거한다. `None`: suspended이면 `CanNotAffectedClass`로 opponent Digimon effects immunity. `OnEndTurn`: once per turn optional, other own unsuspended Digimon 1장을 suspend하고 그 DP 이하 opponent Digimon 1장을 `SelectPermanentEffect.Mode.PutLibraryBottom`으로 bottom deck. | temporary cost modifier registration/cleanup, paid hand play with cost reduction, suspended-state immunity, suspend-cost continuation, DP comparison after suspend, return-to-library-bottom target selection, and once-per-turn hashes `PlayDigimon_BT16_048`/`EOT_BT16-048` are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT15-082#3217@base` | `BT15_082` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082.asset` | `blocked` |
| `BT15-082#3218@P1` | `BT15_082` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P1.asset` | `blocked` |
| `BT15-082#4756@P0` | `BT15_082` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P0.asset` | `blocked` |
| `BT15-082#4757@P2` | `BT15_082` | `DCGO/Assets/CardBaseEntity/BT15/Red/Tamer/BT15_082_P2.asset` | `blocked` |
| `BT15-084#3221@base` | `BT15_084` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084.asset` | `blocked` |
| `BT15-084#3222@P1` | `BT15_084` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P1.asset` | `blocked` |
| `BT15-084#4759@P0` | `BT15_084` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P0.asset` | `blocked` |
| `BT15-084#4760@P2` | `BT15_084` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Tamer/BT15_084_P2.asset` | `blocked` |
| `BT15-097#3238@base` | `BT15_097` | `DCGO/Assets/CardBaseEntity/BT15/Black/Option/BT15_097.asset` | `blocked` |
| `BT16-006#3310@base` | `BT16_006` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006.asset` | `blocked` |
| `BT16-006#3311@P1` | `BT16_006` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006_P1.asset` | `blocked` |
| `BT16-006#4778@P0` | `BT16_006` | `DCGO/Assets/CardBaseEntity/BT16/Purple/DigiEgg/BT16_006_P0.asset` | `blocked` |
| `BT16-020#3328@base` | `BT16_020` | `DCGO/Assets/CardBaseEntity/BT16/Blue/Digimon/BT16_020.asset` | `blocked` |
| `BT16-029#3342@base` | `BT16_029` | `DCGO/Assets/CardBaseEntity/BT16/Yellow/Digimon/BT16_029.asset` | `blocked` |
| `BT16-037#3351@base` | `BT16_037` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_037.asset` | `blocked` |
| `BT16-042#3356@base` | `BT16_042` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_042.asset` | `blocked` |
| `BT16-047#3362@base` | `BT16_047` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047.asset` | `blocked` |
| `BT16-047#4800@P0` | `BT16_047` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_047_P0.asset` | `blocked` |
| `BT16-048#3363@base` | `BT16_048` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_048.asset` | `blocked` |
| `BT16-048#3364@P1` | `BT16_048` | `DCGO/Assets/CardBaseEntity/BT16/Green/Digimon/BT16_048_P1.asset` | `blocked` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0022 depends on hand, security, deck-bottom, field, trash, and bottom-deck movement payloads.
- `BT15_082` and `BT15_084` require `PlaySelfTamerSecurityEffect`, Tamer start-turn memory, and security stack mutation triggers before they can be runnable.
- `BT15_097` requires security option activation to reuse the main option body without duplicating or guessing `OptionSkill`.
- `BT16_029`, `BT16_037`, and earlier reveal helpers require multi-condition reveal/search with source helper bottom-deck behavior.
- `BT16_020`, `BT16_029`, `BT16_037`, `BT16_042`, and `BT16_048` require inherited/static source-role validation and continuous layer support before their `None` effects are safe.
- `BT16_047` requires battle-deletion payload and exact security-count branch semantics; at 3 security both `IDestroySecurity` and memory gain must execute.
- `BT16_048` requires temporary play-cost modifier cleanup around `PlayPermanentCards` and a suspend-cost continuation into return-to-library-bottom. Do not convert this into unconditional play or unconditional bounce.

## Follow-Up

The next generated queue item is `C0023_zone_security_recovery`. C0022 remains source-reviewed but blocked until shared Tamer security, option security, reveal/search, duration modifier, static/inherited source-role, temporary cost, suspend-cost, battle-deletion, and return-to-library-bottom layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0022 identity to runnable.
