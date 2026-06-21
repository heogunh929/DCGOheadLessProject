# Full Card Porting C0035 Zone Security Recovery Source Mapping

## 결정

`C0035_zone_security_recovery`는 `blocked`로 처리한다.

이번 batch는 BT21 source body 10개를 읽고, source-aligned하게 안전한 부분만 카드별 파일에 구현했다. `BT21-048` OnPlay suspend, `BT21-063` inherited DP, `BT21-080`/`BT21-085` start-main memory, `BT21-080`/`BT21-082`/`BT21-085` SecuritySkill play-self Tamer, 그리고 `BT21-033`/`BT21-048`/`BT21-049` keyword metadata를 반영했다. 나머지는 trait/text metadata, hand digivolve, Save, source placement, deleted-source trigger, optional hand play, suspend-cost continuation 같은 공통 layer가 필요하므로 silent no-op이나 CardId branch로 처리하지 않는다.

Queue status: blocked

Source evidence paths:

- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_029_token.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_033.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_048.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Green/BT21_049.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_055.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_063.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_065.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_080.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_082.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_085.cs`
- No CardId branch.

## Variant Identity

이 batch는 `BT21-055`, `BT21-063`, `BT21-065`, `BT21-080`, `BT21-082`, `BT21-085`가 복수 asset variant를 갖는다. generated manifest와 queue mapping은 `CardId#CardIndex@Variant` identity를 유지한다. Catalog는 registry 역할만 하며 variant를 `CardId`만으로 평탄화하지 않는다.

`BT21_029_token`은 source body가 존재하지만 C0035 generated card identity에는 affected card identity count: 0 이다. token card definition/asset identity가 이 batch에 없으므로 이번 card-porting registry에는 등록하지 않는다. token 생성/토큰 card identity inventory가 별도 batch에서 확인되기 전까지 효과 body를 추측 구현하지 않는다.

Representative tracked identities include `BT21-080#5397@base`, `BT21-080#5398@P1`, `BT21-080#8411@P2`, `BT21-080#8412@P3`, `BT21-085#5407@base`, and `BT21-085#5408@P1`.

## Source Mapping

| Source effect | RL.Engine status | 구현/차단 범위 |
| --- | --- | --- |
| `BT21_029_token` | not registered in C0035 registry | Owner-turn can't suspend static effect와 OnDeletion top-security trash source body를 확인했다. 그러나 affected card identity가 0개라서 token asset identity 없이 registry에 임의 등록하지 않는다. |
| `BT21_033` | `PartiallyImplemented` | inherited Jamming은 `CardDefinition.BattleKeywords`로 반영했다. WG level-2 alternate digivolution과 OnPlay reveal top 3, Avian/Bird 및 WG 선택, bottom-deck rest는 trait metadata, reveal ordering, multi-condition search selection이 필요하다. |
| `BT21_048` | `PartiallyImplemented` | OnPlay suspend 1 Digimon을 공통 suspend primitive와 target selection으로 구현했다. inherited Piercing은 `CardDefinition.BattleKeywords`로 반영했다. WG level-2 alternate digivolution은 trait-aware digivolution requirement layer가 필요하다. source `ActivateClass`는 optional이지만 activation 뒤 `SelectPermanentEffect`는 `canNoSelect=false`라서 이번 port는 후보가 있을 때 target body만 노출하고 pre-activation optional UI 구분은 공통 optional-decision layer 범위로 남긴다. |
| `BT21_049` | `PartiallyImplemented` | inherited Piercing은 `CardDefinition.BattleKeywords`로 반영했다. WG level-3 alternate digivolution, OnPlay/WhenDigivolving optional suspend, All Turns once-per-turn opponent-play suspend는 trait-aware digivolution requirement, optional target selection, OnPermanentPlay payload coverage가 필요하다. |
| `BT21_055` | `Unsupported` | owner-turn Mineral/Rock digivolution cost -1과 inherited OnDigivolutionCardDiscarded deletion은 trait-aware cost modifier, OnDigivolutionCardDiscarded payload/source validation, trash-zone inherited trigger resolution이 필요하다. |
| `BT21_063` | `PartiallyImplemented` | inherited owner-turn DP +2000 continuous effect를 구현했다. level-2 Save/Hero alternate digivolution, OnPlay discard Save/Hero then draw 2, Save on deletion은 text/trait metadata, hand-discard cost continuation, draw sequencing, Save, deleted-source trigger coverage가 필요하다. |
| `BT21_065` | `Unsupported` | BeforePayCost Ghost digivolution cost -1과 inherited OnDeletion memory +1은 BeforePayCost cost-modifier frame과 deleted-source inherited trigger eligibility/activation payload coverage가 필요하다. |
| `BT21_080` | `PartiallyImplemented` | StartMainPhase memory +1 while opponent has a Digimon과 SecuritySkill play-self Tamer를 구현했다. OnAddDigivolutionCards suspend this Tamer, draw 1, memory +1은 OnAddDigivolutionCards payload, Gammamon text/Hero trait matching, suspend-cost continuation, draw+memory sequencing이 필요하다. |
| `BT21_082` | `PartiallyImplemented` | SecuritySkill play-self Tamer를 구현했다. StartMainPhase hand digivolve into Hybrid/Hero with red-Tamer unique-name cost reduction과 inherited OnLoseSecurity optional red Tamer play는 hand digivolution, trait metadata, cost reduction, optional hand play selection, ETB continuation이 필요하다. |
| `BT21_085` | `PartiallyImplemented` | StartMainPhase memory +1 while opponent has a Digimon과 SecuritySkill play-self Tamer를 구현했다. Main suspend-cost, trash top stacked card from Armor Form Digimon, draw, memory +1은 suspend-cost continuation, trait metadata, top-stacked-card trash, ETB/after-effect sequencing이 필요하다. |

## 구현 파일

- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_033.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_085.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_048.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Green/BT21_049.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Black/BT21_055.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_063.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_065.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_080.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_082.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21CardScriptCatalog.cs`

## 검증

추가 테스트:

- `FullCardPortingBatch C0035 zone recovery partial implementation`
- `BT21 C0035 keyword metadata`
- `BT21-048 C0035 suspends any Digimon`
- `BT21-063 C0035 inherited DP`
- `BT21 C0035 tamer memory and security play`

전체 regression 결과는 작업 종료 보고에 기록한다.

## 남은 범위

C0035는 L0006 boundary 이후 안전한 subset만 구현했다. 남은 효과는 source body가 확인되었지만 공통 layer가 부족한 `blocked` 상태다. blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
