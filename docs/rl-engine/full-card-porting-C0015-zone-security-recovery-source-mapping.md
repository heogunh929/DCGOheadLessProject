# Full Card Porting C0015 Zone Security Recovery Source Mapping

## 결정

`C0015_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개와 16개 asset identity는 로컬 DCGO 원본에서 확인됐지만, runnable card body로 옮기려면 `L0006_zone_security_recovery`와 suspend/unsuspend event payload, security trash, reveal-to-source, hand-to-source, play-from-source, played-permanent payload, draw-discard continuation, non-battle deletion snapshot, BecomeDigimon duration, security Tamer play common layer가 먼저 필요하다.

Queue status: blocked

No CardId branch. 이 batch에서는 `CardId` 분기, Catalog 분기, core service 분기, silent no-op 구현을 추가하지 않는다. 같은 source effect를 여러 variant가 참조하더라도 `DefinitionStableId`, `CardIndex`, `variantKey`를 보존한다.

## Source Effects

| SourceEffectClassName | 원본 source | 원본 의미 | RL porting blocker |
| --- | --- | --- | --- |
| `BT13_057` | `DCGO/Assets/Scripts/CardEffect/BT13/Green/BT13_057.cs` | `OnEnterFieldAnyone`: When Digivolving, optional select an unsuspended opponent Digimon/Tamer, suspend it, then if it actually became suspended, unsuspend this Digimon. `OnTappedAnyone`: all turns once per turn when an opponent Digimon/Tamer becomes suspended, suspend 1 opponent Digimon/Tamer. | suspend primitive result, `CanSuspend`/unaffected check, suspend event payload, nested `OnTappedAnyone`, optional target selection, unsuspend continuation이 필요하다. |
| `BT13_061` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_061.cs` | `None`: self Blocker static. `OnDestroyedAnyone`: on deletion during opponent turn, `SimplifiedRevealDeckTopCardsAndSelect` reveals top 3, adds 1 black card, rest deck bottom any order. | Blocker static, deletion snapshot, opponent-turn condition, reveal/search, OnAddHand timing, deck-bottom ordering이 필요하다. |
| `BT13_071` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_071.cs` | `None`: self Blocker static. inherited `OnTappedAnyone`: opponent turn once per turn, when one of your Digimon becomes suspended, `IDestroySecurity` trashes top opponent security. | suspend payload, inherited source role, once-per-turn state, top-security trash and OnLoseSecurity interleaving이 필요하다. |
| `BT13_072` | `DCGO/Assets/Scripts/CardEffect/BT13/Black/BT13_072.cs` | `OnEnterFieldAnyone`: When Digivolving, reveal top 3, place 1 X Antibody card as bottom source, trash rest, then this Digimon gains DP-reduction immunity until opponent turn end if placement succeeded. inherited `OnEndTurn`: owner turn once per turn, optionally place 1 X Antibody Digimon from hand as bottom source. | reveal-to-source, trash rest, source-add success continuation, `GainImmuneFromDPMinus`, hand-to-source bottom, end-turn inherited source snapshot가 필요하다. |
| `BT13_078` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_078.cs` | `OnDestroyedAnyone`: on deletion, draw 1 then trash 1 hand card if possible. inherited `OnEndTurn`: end of opponent turn once per turn, draw 1 then trash 1 hand card. | draw boundary, mandatory discard selection, end-turn timing, inherited source role and draw-discard continuation이 필요하다. |
| `BT13_079` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_079.cs` | `OnEnterFieldAnyone`: on play, select 1 purple Digimon and grant Retaliation until opponent turn end. inherited `OnDestroyedAnyone`: on deletion outside battle, opponent discards 1 hand card. The source body computes `discardCount = Math.Min(1, card.Owner.HandCards.Count)` while selecting `card.Owner.Enemy`; preserve this source observation as review data rather than silently correcting it. | keyword duration grant, non-battle deletion payload, opponent hand discard, source bug/quirk preservation policy가 필요하다. |
| `BT13_081` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_081.cs` | `OnEnterFieldAnyone`: on play, delete 1 opponent level 3 Digimon. `OnDestroyedAnyone`: on deletion while in trash, delete 1 opponent level 3 Digimon. inherited `OnEndTurn`: end of opponent turn once per turn, draw 1 then trash 1 hand card. | delete target selection, on-deletion source zone validation, draw-discard continuation, inherited end-turn timing이 필요하다. |
| `BT13_082` | `DCGO/Assets/Scripts/CardEffect/BT13/Purple/BT13_082.cs` | `None`: self Blocker static. inherited `OnDestroyedAnyone`: on deletion outside battle, opponent discards 1 hand card. Like `BT13_079`, source body uses owner hand count for `discardCount` while selecting enemy hand. | Blocker static, non-battle deletion payload, opponent hand discard, source quirk preservation policy가 필요하다. |
| `BT13_096` | `DCGO/Assets/Scripts/CardEffect/BT13/Blue/BT13_096.cs` | `OnEnterFieldAnyone`: on play, optional select one of your Digimon, then play 1 blue level 3 Digimon from its source for free. `OnEnterFieldAnyone`: all turns when you play a blue Digimon, suspend this Tamer, then optionally put 1 blue level 4 or lower Digimon from hand as bottom source of the played Digimon, using `GetPlayedPermanentsFromEnterFieldHashtable`. `SecuritySkill`: play self Tamer. | play-from-digivolution-cards, source removal/play ETB, played-permanent payload, suspend-cost lifecycle, hand-to-source bottom, security Tamer play가 필요하다. |
| `BT13_099` | `DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_099.cs` | `OnTappedAnyone`: all turns once per turn when one of your yellow Digimon becomes suspended, opponent Digimon gets -1000 DP until opponent turn end. `OnEndTurn`: end of owner turn once per turn, if total security <= 6, this Tamer becomes 3000 DP Digimon, cannot digivolve, and gains Blocker until opponent turn end. `SecuritySkill`: play self Tamer. | suspend payload, DP duration, `BecomeDigimonThatCantDigivolve`, temporary type/DP/Blocker duration, security count condition, security Tamer play가 필요하다. |

## Card / Variant Identities

| DefinitionStableId | SourceEffectClassName | 원본 asset | Status |
| --- | --- | --- | --- |
| `BT13-057#2710@base` | `BT13_057` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057.asset` | `blocked` |
| `BT13-057#4593@P0` | `BT13_057` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057_P0.asset` | `blocked` |
| `BT13-057#8148@P1` | `BT13_057` | `DCGO/Assets/CardBaseEntity/BT13/Green/Digimon/BT13_057_P1.asset` | `blocked` |
| `BT13-061#2718@base` | `BT13_061` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_061.asset` | `blocked` |
| `BT13-071#2728@base` | `BT13_071` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_071.asset` | `blocked` |
| `BT13-071#4596@P0` | `BT13_071` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_071_P0.asset` | `blocked` |
| `BT13-072#2729@base` | `BT13_072` | `DCGO/Assets/CardBaseEntity/BT13/Black/Digimon/BT13_072.asset` | `blocked` |
| `BT13-078#2737@base` | `BT13_078` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_078.asset` | `blocked` |
| `BT13-078#4602@P1` | `BT13_078` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_078_P1.asset` | `blocked` |
| `BT13-079#2738@base` | `BT13_079` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_079.asset` | `blocked` |
| `BT13-081#2740@base` | `BT13_081` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_081.asset` | `blocked` |
| `BT13-082#2741@base` | `BT13_082` | `DCGO/Assets/CardBaseEntity/BT13/Purple/Digimon/BT13_082.asset` | `blocked` |
| `BT13-096#2761@base` | `BT13_096` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_096.asset` | `blocked` |
| `BT13-096#4615@P0` | `BT13_096` | `DCGO/Assets/CardBaseEntity/BT13/Blue/Tamer/BT13_096_P0.asset` | `blocked` |
| `BT13-099#2766@base` | `BT13_099` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_099.asset` | `blocked` |
| `BT13-099#4618@P0` | `BT13_099` | `DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_099_P0.asset` | `blocked` |

## Common Layer Blockers

- `L0006_zone_security_recovery` remains `blocked`; C0015 uses source movement, security trash, hand discard, play-from-source, and security play.
- `BT13_057`, `BT13_071`, and `BT13_099` depend on source-aligned `OnTappedAnyone` payloads. Suspended target identity and successful suspend result must be preserved before nested triggers run.
- `BT13_072` and `BT13_096` require source-add/source-remove/play-from-source operations through common primitives, not direct list mutation.
- `BT13_079` and `BT13_082` expose a source body quirk around opponent discard count. This needs source review before implementing, not silent correction.
- `BT13_096` has two `OnEnterFieldAnyone` descriptors with different payloads and selection chains. Descriptor identity must stay separate for ordering/replay.
- `BT13_099` requires a duration model where a Tamer temporarily becomes a Digimon, cannot digivolve, and gains Blocker until opponent turn end.

## Follow-Up

The next generated queue item is `C0016_zone_security_recovery`. C0015 remains source-reviewed but blocked until shared zone/security/recovery, suspend event, source movement, play-from-source, security trash, draw-discard, and temporary type/keyword duration layers are completed. When those layers exist, port each source body under the planned full-card-pool source path and add deterministic selection/replay tests for each continuation before changing any C0015 identity to runnable.
