# C0393_special_digivolution_play - special digivolution/play mechanics card porting 158

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0393_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 28
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_121` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_121.cs` | `OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectJogress` | 3 |
| `P_124` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_124.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_125` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_125.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_126` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_126.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_127` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_127.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_128` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_128.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_129` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_129.cs` | `OnEnterFieldAnyone, OnStartMainPhase, SecuritySkill` | `max_count_per_turn, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectBoolean, SelectSecurity, SelectJogress` | 2 |
| `P_132` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_132.cs` | `None, OnDetermineDoSecurityCheck, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 5 |
| `P_133` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_133.cs` | `OnEnterFieldAnyone, SecuritySkill` | `max_count_per_turn, optional, security, skippable, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard, SelectHand, SelectSecurity, SelectJogress` | 5 |
| `P_135` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_135.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectJogress` | 3 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-121#10417@P1` | `P-121` | 10417 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_121_P1.asset` |
| `P-121#10418@P2` | `P-121` | 10418 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_121_P2.asset` |
| `P-121#6167@base` | `P-121` | 6167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Digimon/P_121.asset` |
| `P-124#10420@P2` | `P-124` | 10420 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_124_P2.asset` |
| `P-124#6170@base` | `P-124` | 6170 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Tamer/P_124.asset` |
| `P-125#10421@P1` | `P-125` | 10421 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_125_P1.asset` |
| `P-125#6171@base` | `P-125` | 6171 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_125.asset` |
| `P-126#10422@P1` | `P-126` | 10422 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_126_P1.asset` |
| `P-126#6172@base` | `P-126` | 6172 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Tamer/P_126.asset` |
| `P-127#10429@P1` | `P-127` | 10429 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_127_P1.asset` |
| `P-127#6173@base` | `P-127` | 6173 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_127.asset` |
| `P-128#10419@P1` | `P-128` | 10419 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_128_P1.asset` |
| `P-128#6174@base` | `P-128` | 6174 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/Tamer/P_128.asset` |
| `P-129#10430@P1` | `P-129` | 10430 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_129_P1.asset` |
| `P-129#6175@base` | `P-129` | 6175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Tamer/P_129.asset` |
| `P-132#10286@base` | `P-132` | 10286 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_132.asset` |
| `P-132#9245@P1` | `P-132` | 9245 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_132_P1.asset` |
| `P-132#9246@P2` | `P-132` | 9246 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_132_P2.asset` |
| `P-132#9247@P3` | `P-132` | 9247 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_132_P3.asset` |
| `P-132#9248@P4` | `P-132` | 9248 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_132_P4.asset` |
| `P-133#10287@base` | `P-133` | 10287 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_133.asset` |
| `P-133#9249@P1` | `P-133` | 9249 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_133_P1.asset` |
| `P-133#9250@P2` | `P-133` | 9250 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_133_P2.asset` |
| `P-133#9251@P3` | `P-133` | 9251 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_133_P3.asset` |
| `P-133#9252@P4` | `P-133` | 9252 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Tamer/P_133_P4.asset` |
| `P-135#10289@base` | `P-135` | 10289 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_135.asset` |
| `P-135#9254@P1` | `P-135` | 9254 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_135_P1.asset` |
| `P-135#9255@P2` | `P-135` | 9255 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_135_P2.asset` |

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
