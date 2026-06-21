# Full Card Porting C0020 Zone Security Recovery Source Mapping

## 결정

`C0020_zone_security_recovery`는 `done`으로 승격할 수 없는 카드 포팅 batch다. DCGO 원본 source body 10개와 asset identity 18개는 모두 확인했다. 그러나 runnable card body로 옮기려면 `L0006_zone_security_recovery`와 함께 digivolution-source trash, library-bottom removal, security top/bottom trash selection, suspend-cost success continuation, battle-deletion payload, static/inherited keyword, immunity/restriction duration, and free play layers가 먼저 source-aligned로 정리되어야 한다.

Queue status: blocked

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT15_028` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_028.cs` | `OnEnterFieldAnyone`: On Play select 1 opponent Digimon, trash bottom 3 digivolution cards through `TrashDigivolutionCardsFromTopOrBottom(... trashCount: 3, isFromTop: false)`, then if opponent has no Digimon with digivolution cards, may play 1 blue Tamer from hand for free via `PlayPermanentCards`. | bottom source trash, post-trash board query, optional hand play, free play ETB, and source-trash trigger interleaving are required. |
| `BT15_030` | `DCGO/Assets/Scripts/CardEffect/BT15/Blue/BT15_030.cs` | `None`: `BlockerSelfStaticEffect`. On Play and On Deletion descriptors trash top 2 digivolution cards from all opponent Digimon, respecting `CanNotTrashFromDigivolutionCards` and on deletion `CanNotBeAffected`, then select opponent Digimon with no sources and put it on deck bottom via `SelectPermanentEffect.Mode.PutLibraryBottom`. | mass top source trash, source-trash protection, on-deletion source validation, return-to-library-bottom event ordering, and duplicated descriptor identity are required. |
| `BT15_036` | `DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_036.cs` | `None`: `BlockerSelfStaticEffect`. On Play and On Deletion optional descriptors choose Security Top or Security Bottom through `SelectionElement<bool>`, trash 1 own security with `IDestroySecurity(fromTop)`, then select 1 opponent Digimon and apply DP -6000 with `ChangeDigimonDP` until opponent turn end. | security top/bottom boolean selection, security-trash payload, optional cost/result staging, DP duration, and on-deletion source validation are required. |
| `BT15_043` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_043.cs` | `OnStartMainPhase`: optional owner main phase effect; select any Digimon that can pay suspend cost, suspend it with `SuspendPermanentsClass`, then if suspension succeeded select owner Insectoid Digimon and give DP +3000 until opponent turn end. inherited `OnEndBattle`: all turns once per turn, when this Digimon deletes an opponent Digimon in battle, gain 1 memory with hash `Memory+1_BT15_043`. | suspend-cost legality and success continuation, trait target selection, DP duration, battle-deletion winner/loser payload, inherited source snapshot, and once-per-turn hash are required. |
| `BT15_044` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_044.cs` | `OnDestroyedAnyone`: on deletion, select 1 opponent Digimon and apply `GainCantUnsuspendUntilOpponentTurnEnd`. | on-deletion source validation, cannot-unsuspend duration, target selection, and cleanup timing are required. |
| `BT15_050` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_050.cs` | `OnEnterFieldAnyone`: On Play reveal top 4 through `SimplifiedRevealDeckTopCardsAndSelect`, add up to 2 level 6+ cards, bottom-deck rest. `OnEndTurn`: optional owner end turn effect; select and delete 1 own Digimon, then on successful deletion may play 1 Dark Masters Digimon from hand to empty breeding area for free. inherited `OnDetermineDoSecurityCheck`: `PierceSelfEffect`. | reveal multi-pick, bottom-deck ordering, delete-success continuation, breeding-area free play, inherited Piercing timing, and effect-play ETB are required. |
| `BT15_053` | `DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_053.cs` | `OnStartMainPhase` and When Digivolving descriptors suspend 1 opponent Digimon via `SelectPermanentEffect.Mode.Tap`, then select 1 owner Digimon and grant `GainPierce` until each turn end. `None`: when this source is suspended on field, `CanNotAffectedClass` makes it unaffected by opponent Digimon effects. | separate start-main/when-digivolving descriptor identity, suspend target resolution, temporary Pierce duration, suspended-state continuous immunity, and opponent Digimon-effect filtering are required. |
| `BT15_055` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_055.cs` | `OnEnterFieldAnyone`: On Play reveal top 3 through `SimplifiedRevealDeckTopCardsAndSelect`, add 1 Machine/Cyborg card and 1 black Tamer, bottom-deck rest. `None`: inherited `RebootSelfStaticEffect`. | reveal dual-pick, trait/color filters, bottom-deck ordering, and inherited Reboot static timing are required. |
| `BT15_058` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_058.cs` | `None`: `BlockerSelfStaticEffect` and `AddSelfDigivolutionRequirementStaticEffect` from level 3 DigiPolice/X Antibody for cost 2. inherited `OnTappedAnyone`: once per turn when this Digimon becomes suspended, suspend opponent Digimon/Tamer with play cost <= this Digimon's play cost, hash `suspend_BT15_058_inherited`. On Play/When Digivolving suspend 1 opponent Digimon, then conditionally apply `GainCanNotUnsuspend`; source has a notable asymmetry where On Play checks this card's own DigiPolice Tamer source, while When Digivolving checks selected permanent sources. | alternate digivolution requirement, Blocker, self-suspend trigger payload, play-cost comparison, target Tap, conditional cannot-unsuspend, descriptor identity, and source-condition asymmetry must be preserved. |
| `BT15_061` | `DCGO/Assets/Scripts/CardEffect/BT15/Black/BT15_061.cs` | `None`: `BlockerSelfStaticEffect`, owner turn cannot attack while opponent has no Digimon through `CanNotAttackSelfStaticEffect`, and inherited `RebootSelfStaticEffect`. On Play/When Digivolving optional descriptors may discard 1 Machine/Cyborg Digimon from hand with `SelectHandEffect.Mode.Discard`; if discarded, select 1 owner Digimon and apply `GainCanNotBeDeletedByEffect` against opponent effects until opponent turn end. | optional hand discard cost, discard success continuation, effect-deletion immunity duration, opponent-effect predicate, no-op avoidance when discard skipped, static cannot-attack condition, Blocker, and Reboot are required. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT15-028#3153@base` | `BT15_028` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_028.asset` | `blocked` |
| `BT15-030#3155@base` | `BT15_030` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_030.asset` | `blocked` |
| `BT15-030#4725@P0` | `BT15_030` | `DCGO/Assets/CardBaseEntity/BT15/Blue/Digimon/BT15_030_P0.asset` | `blocked` |
| `BT15-036#3161@base` | `BT15_036` | `DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_036.asset` | `blocked` |
| `BT15-043#3171@base` | `BT15_043` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043.asset` | `blocked` |
| `BT15-043#3172@P1` | `BT15_043` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_043_P1.asset` | `blocked` |
| `BT15-044#3173@base` | `BT15_044` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_044.asset` | `blocked` |
| `BT15-050#3181@base` | `BT15_050` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_050.asset` | `blocked` |
| `BT15-053#3184@base` | `BT15_053` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053.asset` | `blocked` |
| `BT15-053#4735@P0` | `BT15_053` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P0.asset` | `blocked` |
| `BT15-053#4736@P1` | `BT15_053` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P1.asset` | `blocked` |
| `BT15-053#4737@P2` | `BT15_053` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P2.asset` | `blocked` |
| `BT15-053#4738@P3` | `BT15_053` | `DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_053_P3.asset` | `blocked` |
| `BT15-055#3186@base` | `BT15_055` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_055.asset` | `blocked` |
| `BT15-055#4740@P0` | `BT15_055` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_055_P0.asset` | `blocked` |
| `BT15-058#3190@base` | `BT15_058` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_058.asset` | `blocked` |
| `BT15-061#3193@base` | `BT15_061` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_061.asset` | `blocked` |
| `BT15-061#4743@P0` | `BT15_061` | `DCGO/Assets/CardBaseEntity/BT15/Black/Digimon/BT15_061_P0.asset` | `blocked` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0020 depends on hand, source, security, and library-bottom movement payloads.
- `BT15_028`, `BT15_030`, and `BT15_058` require source-trash and protection rules that cannot be approximated by direct source list mutation.
- `BT15_036` requires top/bottom security selection and `IDestroySecurity` semantics, including `OnLoseSecurity` interleaving.
- `BT15_043` and `BT15_061` depend on cost/success continuations: suspend succeeded before DP grant, and discard succeeded before immunity grant.
- `BT15_043` requires battle-deletion payload for `CanTriggerWhenDeleteOpponentDigimonByBattle`, including winner/loser source checks.
- `BT15_050` and `BT15_055` require reveal/search helpers with deterministic bottom-deck ordering and distinct multi-pick categories.
- `BT15_053`, `BT15_058`, and `BT15_061` rely on static/continuous restriction and immunity classes (`CanNotAffectedClass`, `CanNotAttackSelfStaticEffect`, `GainCanNotBeDeletedByEffect`) that must be shared layer behavior, not card-local shortcuts.
- `BT15_058` contains source-condition asymmetry between On Play and When Digivolving; this must be mapped exactly before runnable implementation.

## Follow-Up

The next generated queue item is `C0021_zone_security_recovery`. C0020 remains source-reviewed but blocked until shared source-trash, security top/bottom trash, reveal/search, suspend/discard cost continuation, battle-deletion payload, static/inherited keyword, immunity, and library-bottom layers are completed. After those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests before changing any C0020 identity to runnable.
