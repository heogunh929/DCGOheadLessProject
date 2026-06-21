# C0030_zone_security_recovery - zone/security/recovery card porting 24

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0030_zone_security_recovery`
- Kind: `card-porting`
- Category: `zone-security-recovery` / zone/security/recovery
- Dependencies: `L0006_zone_security_recovery`
- Card identity count: 24
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT1_025` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_025.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `-` | 4 |
| `BT1_029` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_029.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `-` | 6 |
| `BT1_036` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_036.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT1_043` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_043.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent` | 1 |
| `BT1_048` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_048.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectCard` | 3 |
| `BT1_049` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_049.cs` | `OnDestroyedAnyone` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 1 |
| `BT1_053` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_053.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, zone_movement` | `-` | 1 |
| `BT1_055` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_055.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |
| `BT1_060` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_060.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectSecurity` | 5 |
| `BT1_061` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_061.cs` | `OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, zone_movement` | `SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT1-025#167@base` | `BT1-025` | 167 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_025.asset` |
| `BT1-025#168@P1` | `BT1-025` | 168 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_025_P1.asset` |
| `BT1-025#169@P2` | `BT1-025` | 169 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_025_P2.asset` |
| `BT1-025#170@P3` | `BT1-025` | 170 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Red/Digimon/BT1_025_P3.asset` |
| `BT1-029#175@base` | `BT1-029` | 175 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029.asset` |
| `BT1-029#176@P1` | `BT1-029` | 176 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P1.asset` |
| `BT1-029#177@P2` | `BT1-029` | 177 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P2.asset` |
| `BT1-029#178@P3` | `BT1-029` | 178 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P3.asset` |
| `BT1-029#179@P4` | `BT1-029` | 179 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P4.asset` |
| `BT1-029#4265@P5` | `BT1-029` | 4265 | `P5` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_029_P5.asset` |
| `BT1-036#186@base` | `BT1-036` | 186 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_036.asset` |
| `BT1-043#197@base` | `BT1-043` | 197 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Blue/Digimon/BT1_043.asset` |
| `BT1-048#204@base` | `BT1-048` | 204 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_048.asset` |
| `BT1-048#205@P1` | `BT1-048` | 205 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_048_P1.asset` |
| `BT1-048#4268@P2` | `BT1-048` | 4268 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_048_P2.asset` |
| `BT1-049#206@base` | `BT1-049` | 206 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_049.asset` |
| `BT1-053#210@base` | `BT1-053` | 210 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_053.asset` |
| `BT1-055#212@base` | `BT1-055` | 212 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_055.asset` |
| `BT1-060#218@base` | `BT1-060` | 218 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060.asset` |
| `BT1-060#219@P1` | `BT1-060` | 219 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P1.asset` |
| `BT1-060#220@P2` | `BT1-060` | 220 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P2.asset` |
| `BT1-060#4269@P3` | `BT1-060` | 4269 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P3.asset` |
| `BT1-060#4270@P4` | `BT1-060` | 4270 | `P4` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_060_P4.asset` |
| `BT1-061#221@base` | `BT1-061` | 221 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT1/Yellow/Digimon/BT1_061.asset` |

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
