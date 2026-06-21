# C0091_zone_security_recovery - zone/security/recovery card porting 85

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0091_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 32
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_166` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_166.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 4 |
| `P_167` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_167.cs` | `OnDigivolutionCardDiscarded, OnEnterFieldAnyone, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectBoolean` | 4 |
| `P_168` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_168.cs` | `OnAddDigivolutionCards, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `P_169` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_169.cs` | `OnDigivolutionCardDiscarded, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 4 |
| `P_175` | `DCGO/Assets/Scripts/CardEffect/P/White/P_175.cs` | `OnEnterFieldAnyone, OnStartTurn, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 1 |
| `P_180` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_180.cs` | `None, OnDigivolutionCardDiscarded, OptionSkill, SecuritySkill` | `inherited, max_count_per_turn, security, static_or_continuous, zone_movement` | `SelectPermanent, SelectSecurity` | 4 |
| `P_181` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_181.cs` | `BeforePayCost, OptionSkill, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectSecurity` | 4 |
| `P_188` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_188.cs` | `OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `-` | 3 |
| `P_192` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_192.cs` | `OnDestroyedAnyone, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `P_199` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_199.cs` | `BeforePayCost, None, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, optional, security, static_or_continuous, zone_movement` | `SelectCard, SelectPermanent, SelectSecurity` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-166#10445@base` | `P-166` | 10445 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_166.asset` |
| `P-166#10446@P1` | `P-166` | 10446 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_166_P1.asset` |
| `P-166#9279@P2` | `P-166` | 9279 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_166_P2.asset` |
| `P-166#9280@P3` | `P-166` | 9280 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_166_P3.asset` |
| `P-167#10435@base` | `P-167` | 10435 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_167.asset` |
| `P-167#10436@P1` | `P-167` | 10436 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_167_P1.asset` |
| `P-167#9281@P2` | `P-167` | 9281 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_167_P2.asset` |
| `P-167#9282@P3` | `P-167` | 9282 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_167_P3.asset` |
| `P-168#10442@base` | `P-168` | 10442 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_168.asset` |
| `P-168#10443@P1` | `P-168` | 10443 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_168_P1.asset` |
| `P-168#9283@P2` | `P-168` | 9283 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_168_P2.asset` |
| `P-168#9284@P3` | `P-168` | 9284 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_168_P3.asset` |
| `P-169#10437@base` | `P-169` | 10437 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_169.asset` |
| `P-169#10438@P1` | `P-169` | 10438 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_169_P1.asset` |
| `P-169#9285@P2` | `P-169` | 9285 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_169_P2.asset` |
| `P-169#9286@P3` | `P-169` | 9286 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_169_P3.asset` |
| `P-175#5188@base` | `P-175` | 5188 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/White/Tamer/P_175.asset` |
| `P-180#5303@base` | `P-180` | 5303 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_180.asset` |
| `P-180#5304@P1` | `P-180` | 5304 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_180_P1.asset` |
| `P-180#9295@P2` | `P-180` | 9295 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_180_P2.asset` |
| `P-180#9296@P3` | `P-180` | 9296 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Option/P_180_P3.asset` |
| `P-181#5305@base` | `P-181` | 5305 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_181.asset` |
| `P-181#5306@P1` | `P-181` | 5306 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_181_P1.asset` |
| `P-181#9297@P2` | `P-181` | 9297 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_181_P2.asset` |
| `P-181#9298@P3` | `P-181` | 9298 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Option/P_181_P3.asset` |
| `P-188#6977@base` | `P-188` | 6977 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_188.asset` |
| `P-188#6978@P1` | `P-188` | 6978 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_188_P1.asset` |
| `P-188#9303@P2` | `P-188` | 9303 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_188_P2.asset` |
| `P-192#6985@base` | `P-192` | 6985 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_192.asset` |
| `P-192#6986@P1` | `P-192` | 6986 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_192_P1.asset` |
| `P-192#9307@P2` | `P-192` | 9307 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_192_P2.asset` |
| `P-199#7470@base` | `P-199` | 7470 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_199.asset` |

## Required Work

- Source Effects 표의 원본 파일을 먼저 읽고, 대응 카드별 RL effect 파일 또는 wrapper/mapping을 만든다.
- 같은 source effect를 여러 variant가 참조해도 CardId만으로 평탄화하지 않는다.
- 필요한 공통 helper는 source 의미를 숨기지 않는 범위에서만 추가한다.
- 구현 후 65 baseline에서 이 batch의 `Unsupported` blocker가 줄어드는지 확인한다.

## Tests / Validation

- 대상 카드별 unit/golden/replay 테스트를 추가하거나 갱신한다.
- `FullCardPoolValidator` deck subset 또는 batch-specific validation을 추가한다.
- 전체 regression을 실행한다.
- `DCGO/Assets/Scripts` 변경 없음과 tracked `bin/obj` 없음 확인을 보고한다.

## Blocker Conditions

- source body missing/ambiguous
- 원본 timing/selection/ordering 의미 미확인
- 공통 layer가 없는 mechanic을 카드별 workaround로 구현해야 하는 상황
- core service에 CardId 분기를 넣어야만 통과하는 설계
