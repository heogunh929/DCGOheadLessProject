# Full Card Porting C0034 Zone Security Recovery Source Mapping

## 결정

`C0034_zone_security_recovery`는 `blocked`로 처리한다.

이번 batch는 BT20/BT21 source body 10개를 읽고, source-aligned하게 안전한 일부 effect body만 카드별 파일에 구현했다. 남은 효과는 trait metadata, hand digivolve, source placement, reveal/search ordering, security after-battle play, hidden-hand/security manipulation 같은 공통 layer가 필요하므로 silent no-op이나 CardId branch로 대체하지 않는다.

Queue status: blocked

Source evidence paths:

- `DCGO/Assets/Scripts/CardEffect/BT20/White/BT20_092.cs`
- `DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_096.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_001.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_003.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_007.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_008.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_011.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_012.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_015.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_024.cs`
- No CardId branch.

## Variant Identity

`BT21-008#5314@base`와 `BT21-008#5315@P1`는 같은 source effect class `BT21_008`을 공유하지만, generated manifest와 queue mapping에서는 `CardId#CardIndex@Variant` identity를 유지한다. Catalog는 registry 역할만 하며 variant를 CardId만으로 평탄화하지 않는다.

## Source Mapping

| Source effect | RL.Engine status | 구현/차단 범위 |
| --- | --- | --- |
| `BT20_092` | `PartiallyImplemented` | OnStartTurn memory set to 3와 SecuritySkill play-self Tamer를 구현했다. OnPlay hand level-3 Digimon to this Tamer source plus draw, StartMainPhase source Digimon play then self-delete는 hand-to-source placement, source selection/play continuation, ETB continuation, self-delete sequencing이 필요하다. |
| `BT20_096` | `PartiallyImplemented` | OptionSkill discard 1 hand card then delete opponent level 4-or-lower Digimon, SecuritySkill delete opponent level 6-or-lower Digimon을 구현했다. Trash Main activation은 trash activation/cost payment, return-self-to-deck-bottom continuation, unsuspended target delete가 필요하다. |
| `BT21_001` | `Unsupported` | inherited optional once-per-turn OnLoseSecurity hand digivolve with Reptile/Dragonkin and cost -1은 trait metadata, opponent-security-loss payload, hand digivolve continuation, digivolution cost reduction이 필요하다. |
| `BT21_003` | `Unsupported` | inherited OnEnterFieldAnyone draw on owner WG Digimon play는 global play payload와 trait metadata가 필요하다. |
| `BT21_007` | `PartiallyImplemented` | inherited owner-turn DP +2000 continuous effect를 구현했다. OnPlay optional trash-to-hand Reptile/Dragonkin Digimon은 trait metadata와 optional trash card selection, L0006 return-to-hand continuation coverage가 필요하다. |
| `BT21_008` | `PartiallyImplemented` | inherited owner-turn once-per-turn OnLoseSecurity memory +1을 구현했다. OnPlay reveal top 3, select Reptile/Dragonkin and LIBERATOR, bottom-deck rest는 trait metadata, multi-category reveal selection, deck-bottom ordering이 필요하다. |
| `BT21_011` | `Unsupported` | alternate level-2 digivolution, BeforePayCost cost reduction, Save, inherited conditional Rush는 trait metadata, BeforePayCost frame, Save, conditional inherited keyword support가 필요하다. |
| `BT21_012` | `PartiallyImplemented` | inherited owner-turn DP +2000 continuous effect를 구현했다. Main suspend cost, play red Tamer with inherited effects from hand, place this Digimon under played Tamer는 suspend-cost continuation, inherited-effect metadata, hand play ETB continuation, permanent-to-source placement가 필요하다. |
| `BT21_015` | `PartiallyImplemented` | OnPlay/WhenDigivolving delete opponent Digimon with 4000 DP or less와 inherited owner-turn DP +2000을 구현했다. SecuritySkill play-self Digimon after battle은 source-aligned security after-battle play continuation이 필요하다. |
| `BT21_024` | `PartiallyImplemented` | inherited owner-turn DP +4000 continuous effect를 구현했다. OnPlay/WhenDigivolving opponent hand-to-bottom-security then top-security trash는 hidden-hand selection, hand-to-security-bottom movement, security trash continuation, OnLoseSecurity interleaving이 필요하다. |

## 구현 파일

- `src/DCGO.RL.Engine/CardEffects/BT20/White/BT20_092.cs`
- `src/DCGO.RL.Engine/CardEffects/BT20/Purple/BT20_096.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_001.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_003.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_007.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_008.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_011.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_012.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_015.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_024.cs`
- `src/DCGO.RL.Engine/CardEffects/BT20/Bt20CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21ScriptSupport.cs`

## 검증

추가 테스트:

- `FullCardPortingBatch C0034 zone recovery partial implementation`
- `BT20 C0034 tamer and option effects`
- `BT21 C0034 inherited and security-loss effects`
- `BT21-015 C0034 delete and DP`

전체 regression 결과는 작업 종료 보고에 기록한다.

## 남은 범위

C0034는 shared L0006 boundary 위에서 구현 가능한 safe subset만 완료했다. 남은 효과는 source body가 확인됐지만 공통 layer가 부족한 `blocked` 상태이며, blocker 문서화만으로 card-porting batch를 `done`으로 처리하지 않는다.
