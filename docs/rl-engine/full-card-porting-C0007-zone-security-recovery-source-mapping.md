# Full Card Porting C0007 Zone Security Recovery Source Mapping

## 결정

`C0007_zone_security_recovery`는 `done`으로 승격하지 않는다. 이 batch의 source body 10개는 모두 로컬 DCGO 원본에서 확인되지만, 안전한 runnable card body 포팅은 `L0006_zone_security_recovery`와 이전 common-layer blocker가 먼저 해결되어야 한다.

Queue status: blocked

이번 작업에서는 카드 효과를 추측 구현하지 않는다. `CardId` 분기, Catalog 동작 삽입, legacy no-op wrapper, source body 통합 파일을 만들지 않는다. 각 source effect는 원본 경로와 variant-aware asset identity에 연결된 추적 가능한 blocker로 유지한다.

## Batch Source Effects

| SourceEffectClassName | Source path | Source 의미 | Blocking reason |
| --- | --- | --- | --- |
| `AD1_019` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_019.cs` | `OnStartMainPhase` memory tamer helper, `OnEnterFieldAnyone` Adventure digivolve trigger, suspend-this-tamer cost, hand selection/play for reduced cost, `SecuritySkill` play-self-tamer. | `OnEnterFieldAnyone` digivolve payload, suspend-cost lifecycle, `SelectHandEffect.Mode.PlayForCost`, tamer color cost reduction, play-from-hand continuation, and security play-self-tamer boundary are not yet source-aligned. |
| `AD1_022` | `DCGO/Assets/Scripts/CardEffect/AD1/Green/AD1_022.cs` | `OnStartMainPhase` memory tamer helper, Adventure permanent play trigger, suspend-this-tamer cost, select own Digimon, digivolve into an Adventure hand card with reduced cost, `SecuritySkill` play-self-tamer. | Needs source-aligned `OnEnterFieldAnyone` play payload, suspend-cost lifecycle, target selection, hand digivolution helper, memory/payment/digivolution continuation, and security play-self-tamer boundary. |
| `BT10_006` | `DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_006.cs` | Inherited `OnDigivolutionCardDiscarded`: opponent turn, when this digivolution card is trashed by an effect, draw 1 from deck after source is in trash. | `OnDigivolutionCardDiscarded` payload/source-role handling and `DrawClass`/`OnDraw` boundary remain blockers; implementation must preserve discarded inherited source identity. |
| `BT10_014` | `DCGO/Assets/Scripts/CardEffect/BT10/Red/BT10_014.cs` | `OnEnterFieldAnyone` when-digivolving Blitz; static self DP +2000 during owner's turn. | Blitz attack-declaration handoff, trigger-when-digivolving payload, and continuous static DP source snapshot/revalidation need common support before card body porting. |
| `BT10_018` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_018.cs` | Optional `OnDestroyedAnyone`: on deletion, select 1 level 4 Blue Flare Digimon in hand and play it suspended without cost with ETB enabled. | On-deletion snapshot, hand selection, play-without-cost from hand, suspended-play lifecycle, ETB interleaving, and `OnAddHand`/zone movement side effects need shared layers. |
| `BT10_020` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_020.cs` | `OnEnterFieldAnyone` on play draw 1 plus draw per opponent Digimon, `OnDestroyedAnyone` Save, inherited static DP +1000 while opponent has 2 or more Digimon. | On-play payload, `DrawClass`/`OnDraw`, Save source movement, inherited continuous DP, and source-role revalidation are not fully source-aligned. |
| `BT10_025` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs` | Hand `OnDeclaration` main effect: pay 3 memory, select own Blue Flare Digimon, place this hand card as bottom digivolution card, then unsuspend that Digimon; inherited static DP +1000 with opponent 2+ Digimon. | Legal declaration from hand, memory payment as cost, add-digivolution-card zone movement, `OnAddDigivolutionCards`, `WhenUntapAnyone`, unsuspend replacement/timing, and continuous inherited DP need common layers. |
| `BT10_028` | `DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs` | Static Blocker, when-digivolving unsuspend self, opponent-turn once-per-turn `OnEndBattle` unsuspend when this Digimon deletes an opponent's Digimon in battle. | Blocker/counter/attack lifecycle, battle result payload, `OnEndBattle`, once-per-turn state, and `WhenUntapAnyone`/unsuspend continuation remain blockers. |
| `BT10_030` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_030.cs` | `OnEnterFieldAnyone` on play select 1 opponent level 5 or lower Digimon; give Security Attack -1 until opponent turn end. | On-play payload, target selection, SecurityAttack modifier duration, and duration cleanup at opponent turn end need source-aligned support. |
| `BT10_042` | `DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_042.cs` | When digivolving, all opponent Digimon gain Security Attack -1 until opponent turn end; static attack restriction against this Digimon; disable opponent WhenAttacking/WhenDigivolving effects from Digimon with SecurityAttack changes. | SecurityAttack continuous modifier, attack restriction query, disable-effect layer, source CanActivate revalidation, and opponent-turn continuous state are not fully implemented for full-card pool. |

## Variant-aware Identity

| DefinitionStableId | SourceEffectClassName | Asset path | Decision |
| --- | --- | --- | --- |
| `AD1-019#7862@base` | `AD1_019` | `DCGO/Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset` | `blocked` |
| `AD1-022#7864@base` | `AD1_022` | `DCGO/Assets/CardBaseEntity/AD1/Green/AD1_022.asset` | `blocked` |
| `BT10-006#2038@base` | `BT10_006` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006.asset` | `blocked` |
| `BT10-006#4287@P0` | `BT10_006` | `DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006_P0.asset` | `blocked` |
| `BT10-014#2048@base` | `BT10_014` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014.asset` | `blocked` |
| `BT10-014#4298@P0` | `BT10_014` | `DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_014_P0.asset` | `blocked` |
| `BT10-018#2053@base` | `BT10_018` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_018.asset` | `blocked` |
| `BT10-018#4301@P1` | `BT10_018` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_018_P1.asset` | `blocked` |
| `BT10-020#2055@base` | `BT10_020` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_020.asset` | `blocked` |
| `BT10-020#4304@P0` | `BT10_020` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_020_P0.asset` | `blocked` |
| `BT10-025#2062@base` | `BT10_025` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_025.asset` | `blocked` |
| `BT10-028#2066@base` | `BT10_028` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028.asset` | `blocked` |
| `BT10-028#4308@P0` | `BT10_028` | `DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028_P0.asset` | `blocked` |
| `BT10-030#2068@base` | `BT10_030` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_030.asset` | `blocked` |
| `BT10-042#2081@base` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042.asset` | `blocked` |
| `BT10-042#2082@P1` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P1.asset` | `blocked` |
| `BT10-042#4313@P2` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P2.asset` | `blocked` |
| `BT10-042#4314@P3` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P3.asset` | `blocked` |
| `BT10-042#4315@P4` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P4.asset` | `blocked` |
| `BT10-042#4316@P5` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P5.asset` | `blocked` |
| `BT10-042#8097@P6` | `BT10_042` | `DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_042_P6.asset` | `blocked` |

## Common-layer Blockers

- `L0006_zone_security_recovery` is still `blocked`; C0007 effects that move cards through hand, trash, security, or digivolution sources cannot be safely implemented yet.
- Earlier timing blockers still apply: `OnEnterFieldAnyone`, `OnDigivolutionCardDiscarded`, `OnDeclaration`, `OnEndBattle`, `OnDestroyedAnyone`, `OnAddDigivolutionCards`, and `WhenUntapAnyone` require source-aligned payloads and source-zone snapshots.
- Draw effects must not call primitive draw and skip `OnAddHand`/`OnDraw` ordering.
- Suspend/unsuspend, blocker, Blitz, Save, SecurityAttack modifiers, and disable-effect logic need common mechanics; do not hide them as partial no-op descriptors.
- AD1 tamer security effects must remain card-file effects after a generic security play-self-tamer helper exists; do not put `AD1_019` or `AD1_022` CardId branches into `SecurityCheckService`.
- BT10 variant cards must remain separate `DefinitionStableId` entries. Do not flatten `BT10-042` variants to a single CardId identity.

## Follow-up

The next generated queue item is `C0008_zone_security_recovery`. C0007 remains a source-reviewed blocker set until the common layers above are implemented and verified. Once those layers exist, port each source body under its planned per-source/per-card location and add targeted replay tests before changing any C0007 identity to runnable.
