# C0174_attack_security_timing - attack/security timing card porting 47

AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.

- `DCGO/Assets` 원본은 수정하지 마라.
- Codex는 commit과 push를 하지 마라.
- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.
- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.
- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.
- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.

## Batch Metadata

- BatchId: `C0174_attack_security_timing`
- Kind: `card-porting`
- Category: `attack-security-timing` / attack/security timing
- Dependencies: `L0127_attack_security_timing`
- Card identity count: 20
- Source effect count: 10
- Mechanic blocker count: 0

## Source Effects

| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |
| --- | --- | --- | --- | --- | ---: |
| `P_148` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_148.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, static_or_continuous` | `-` | 3 |
| `P_149` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_149.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectHand, SelectPermanent` | 3 |
| `P_164` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_164.cs` | `OnEndAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectHand` | 4 |
| `P_170` | `DCGO/Assets/Scripts/CardEffect/P/Red/P_170.cs` | `BeforePayCost, None, OnAllyAttack, OnDestroyedAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand, SelectBoolean` | 1 |
| `P_176` | `DCGO/Assets/Scripts/CardEffect/P/Black/P_176.cs` | `OnAllyAttack` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, zone_movement` | `SelectCard, SelectHand` | 4 |
| `P_196` | `DCGO/Assets/Scripts/CardEffect/P/Blue/P_196.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `P_197` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_197.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `SelectPermanent` | 1 |
| `P_198` | `DCGO/Assets/Scripts/CardEffect/P/Purple/P_198.cs` | `None, OnAllyAttack, OnStartMainPhase` | `inherited, max_count_per_turn, optional, static_or_continuous, zone_movement` | `-` | 1 |
| `P_207` | `DCGO/Assets/Scripts/CardEffect/P/Yellow/P_207.cs` | `None, OnAllyAttack, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_on_play, trigger_when_digivolving, zone_movement` | `SelectCard, SelectHand` | 1 |
| `P_208` | `DCGO/Assets/Scripts/CardEffect/P/Green/P_208.cs` | `None, OnAllyAttack, OnDestroyedAnyone, OnEndTurn, OnEnterFieldAnyone` | `inherited, max_count_per_turn, optional, skippable, static_or_continuous, trigger_when_digivolving, zone_movement` | `SelectCard, SelectPermanent` | 1 |

## Card / Variant Identities

| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |
| --- | --- | ---: | --- | --- | --- |
| `P-148#10308@base` | `P-148` | 10308 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_148.asset` |
| `P-148#10309@P1` | `P-148` | 10309 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_148_P1.asset` |
| `P-148#9265@P2` | `P-148` | 9265 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/DigiEgg/P_148_P2.asset` |
| `P-149#10310@base` | `P-149` | 10310 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_149.asset` |
| `P-149#10311@P1` | `P-149` | 10311 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_149_P1.asset` |
| `P-149#9266@P2` | `P-149` | 9266 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/DigiEgg/P_149_P2.asset` |
| `P-164#10439@base` | `P-164` | 10439 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_164.asset` |
| `P-164#10440@P1` | `P-164` | 10440 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_164_P1.asset` |
| `P-164#9275@P2` | `P-164` | 9275 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_164_P2.asset` |
| `P-164#9276@P3` | `P-164` | 9276 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_164_P3.asset` |
| `P-170#5183@base` | `P-170` | 5183 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Red/Digimon/P_170.asset` |
| `P-176#5295@base` | `P-176` | 5295 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_176.asset` |
| `P-176#5296@P1` | `P-176` | 5296 | `P1` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_176_P1.asset` |
| `P-176#9287@P2` | `P-176` | 9287 | `P2` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_176_P2.asset` |
| `P-176#9288@P3` | `P-176` | 9288 | `P3` | `None` | `DCGO/Assets/CardBaseEntity/P/Black/DigiEgg/P_176_P3.asset` |
| `P-196#7467@base` | `P-196` | 7467 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Blue/Digimon/P_196.asset` |
| `P-197#7468@base` | `P-197` | 7468 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_197.asset` |
| `P-198#7469@base` | `P-198` | 7469 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Purple/Digimon/P_198.asset` |
| `P-207#7485@base` | `P-207` | 7485 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Yellow/Digimon/P_207.asset` |
| `P-208#7486@base` | `P-208` | 7486 | `base` | `None` | `DCGO/Assets/CardBaseEntity/P/Green/Digimon/P_208.asset` |

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
