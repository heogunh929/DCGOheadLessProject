# C0345_special_digivolution_play - special digivolution/play mechanics card porting 110

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0345_special_digivolution_play`
- Kind: `card-porting`
- Category: `special-digivolution-play` / special digivolution/play mechanics
- Dependencies: `L0234_special_digivolution_play`, `L0235_special_digivolution_play`
- Card identity count: 21
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `BT9_040` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_040.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, modifier_duration, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectSecurity, SelectJogress` | 2 |
| `BT9_041` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_041.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand, SelectPermanent, SelectJogress` | 5 |
| `BT9_042` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_042.cs` | `OnAllyAttack, OnDeclaration, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectHand, SelectPermanent, SelectJogress` | 2 |
| `BT9_043` | `DCGO/Assets/Scripts/CardEffect/BT9/Yellow/BT9_043.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `max_count_per_turn, optional, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectSecurity, SelectJogress` | 2 |
| `BT9_046` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_046.cs` | `None, OnEnterFieldAnyone` | `max_count_per_turn, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectOrder, SelectJogress` | 2 |
| `BT9_049` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_049.cs` | `None, OnDetermineDoSecurityCheck` | `inherited, static_or_continuous` | `SelectJogress` | 1 |
| `BT9_052` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_052.cs` | `None, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT9_055` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_055.cs` | `None, OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectPermanent, SelectAttackTarget, SelectJogress` | 2 |
| `BT9_056` | `DCGO/Assets/Scripts/CardEffect/BT9/Green/BT9_056.cs` | `None, OnAllyAttack, OnTappedAnyone` | `max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 2 |
| `BT9_062` | `DCGO/Assets/Scripts/CardEffect/BT9/Black/BT9_062.cs` | `OnEndAttack` | `inherited, max_count_per_turn, static_or_continuous, zone_movement` | `SelectPermanent, SelectJogress` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `BT9-040#1827@base` | `BT9-040` | 1827 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_040.asset` |
| `BT9-040#8968@P0` | `BT9-040` | 8968 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_040_P0.asset` |
| `BT9-041#1828@base` | `BT9-041` | 1828 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_041.asset` |
| `BT9-041#8969@P0` | `BT9-041` | 8969 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_041_P0.asset` |
| `BT9-041#8970@P1` | `BT9-041` | 8970 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_041_P1.asset` |
| `BT9-041#8971@P2` | `BT9-041` | 8971 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_041_P2.asset` |
| `BT9-041#8972@P3` | `BT9-041` | 8972 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_041_P3.asset` |
| `BT9-042#1829@base` | `BT9-042` | 1829 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_042.asset` |
| `BT9-042#8973@P0` | `BT9-042` | 8973 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_042_P0.asset` |
| `BT9-043#1830@base` | `BT9-043` | 1830 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_043.asset` |
| `BT9-043#8974@P0` | `BT9-043` | 8974 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Yellow/Digimon/BT9_043_P0.asset` |
| `BT9-046#1834@base` | `BT9-046` | 1834 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_046.asset` |
| `BT9-046#8975@P0` | `BT9-046` | 8975 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_046_P0.asset` |
| `BT9-049#1837@base` | `BT9-049` | 1837 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_049.asset` |
| `BT9-052#1840@base` | `BT9-052` | 1840 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_052.asset` |
| `BT9-052#8979@P0` | `BT9-052` | 8979 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_052_P0.asset` |
| `BT9-055#1843@base` | `BT9-055` | 1843 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_055.asset` |
| `BT9-055#1844@P1` | `BT9-055` | 1844 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_055_P1.asset` |
| `BT9-056#1845@base` | `BT9-056` | 1845 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_056.asset` |
| `BT9-056#8981@P0` | `BT9-056` | 8981 | `P0` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Green/Digimon/BT9_056_P0.asset` |
| `BT9-062#1852@base` | `BT9-062` | 1852 | `base` | `None` | `DCGO/Assets/CardBaseEntity/BT9/Black/Digimon/BT9_062.asset` |

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
