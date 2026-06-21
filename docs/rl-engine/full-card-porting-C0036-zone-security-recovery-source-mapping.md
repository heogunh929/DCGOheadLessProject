# Full Card Porting C0036 Zone Security Recovery Source Mapping

## 결정

`C0036_zone_security_recovery`는 `blocked`로 처리한다.

이번 batch는 BT21/BT22 source body 10개를 읽고, source-aligned하게 안전한 부분만 카드별 파일에 구현했다. `BT21-088` SecuritySkill play-self Tamer와 `BT21-093` SecuritySkill highest-DP deletion은 현재 공통 엔진 경계로 의미를 보존할 수 있어 partial body로 구현했다. 나머지 source effect는 Delay option, hand/trash play, hand digivolve, source placement/transfer, trait/text metadata, face-up security replacement, Vortex, BeforePayCost cost modifier 같은 공통 layer가 필요하므로 silent no-op이나 CardId branch로 처리하지 않는다.

Queue status: blocked

Source evidence paths:

- `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_088.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_090.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_091.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_092.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Red/BT21_093.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Blue/BT21_095.cs`
- `DCGO/Assets/Scripts/CardEffect/BT21/Purple/BT21_099.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_001.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_002.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_004.cs`
- No CardId branch.

## Variant Identity

generated manifest와 queue mapping은 `CardId#CardIndex@Variant` identity를 유지한다. `BT21-088`, `BT21-090`, `BT21-095`는 복수 asset variant를 갖고, 대표 identity로 `BT21-088#5413@base`, `BT21-088#5414@P1`, `BT21-090#5417@base`, `BT21-090#8414@P1`, `BT21-090#8415@P2`, `BT21-095#5422@base`, `BT21-095#8416@P1`을 추적한다. Catalog는 registry 역할만 하며 variant를 `CardId`만으로 평탄화하지 않는다.

## Source Mapping

| Source effect | RL.Engine status | 구현/차단 범위 |
| --- | --- | --- |
| `BT21_088` | `PartiallyImplemented` | SecuritySkill play-self Tamer를 구현했다. StartMainPhase hand Save/Hero card placement under this Tamer, draw 1, memory +1과 BeforePayCost suspend-cost/source-transfer digivolution cost -1은 trait/text metadata, hand-to-source placement, source transfer, suspend-cost continuation, cost-modifier frame이 필요하다. |
| `BT21_090` | `Unsupported` | ignore color requirement, OptionSkill reveal top 3 Gammamon search then Delay option placement, OnAddDigivolutionCards Delay hand digivolve, Security hand/trash Gammamon play then Delay option placement는 text metadata, reveal ordering, Delay option placement, hand/trash play continuation, hand digivolution이 필요하다. |
| `BT21_091` | `Unsupported` | ignore color requirement, OptionSkill Hybrid hand discard then draw 2 and Delay placement, OnEnterField Delay hand digivolve from Tamer, Security optional Tamer play then add this card to hand은 trait/inherited-effect metadata, hand-discard cost continuation, Delay option placement, hand/trash play continuation, hand digivolution이 필요하다. |
| `BT21_092` | `Unsupported` | ignore color requirement, source transfer from Xros Heart Digimon to Tamer, hand play with cost reduction, and Security hand/trash Xros Heart play require trait metadata, source transfer, hand play fixed/reduced cost, and hand/trash play continuation. |
| `BT21_093` | `PartiallyImplemented` | SecuritySkill delete one opponent Digimon with the highest DP를 구현했다. play/use cost -4, OptionSkill delete then Delay option placement, OnLoseSecurity Delay hand digivolve는 cost-modifier frame, Delay option placement, trait metadata, hand digivolution이 필요하다. |
| `BT21_095` | `Unsupported` | ignore color requirement, security-zone Vortex grant to owner WG Digimon, OptionSkill replace bottom security with face-up option, Security hand WG Digimon play require trait metadata, Vortex keyword support, face-up security replacement/option placement, and hand play continuation. |
| `BT21_099` | `Unsupported` | OptionSkill optional hand/trash Save source placement under Tamer then trash digivolve, and Security optional hand/trash Save play then add this card to hand require Save text metadata, hand/trash source placement, trash digivolution, and hand/trash play continuation. |
| `BT22_001` | `Unsupported` | inherited owner-turn once-per-turn OnAddDigivolutionCards draw for Aqua/Sea Animal Digimon sources requires trait metadata and source-aligned OnAddDigivolutionCards card-condition payload coverage. |
| `BT22_002` | `Unsupported` | inherited owner-turn once-per-turn draw when an owner Token or other Puppet Digimon is deleted requires token identity and Puppet trait metadata on the OnDestroyedAnyone payload. |
| `BT22_004` | `Unsupported` | inherited owner-turn once-per-turn OnAddDigivolutionCards optional CS hand digivolution with cost -1 requires CS trait metadata, OnAddDigivolutionCards card-condition payload, optional hand digivolution, and digivolution cost-reduction support. |

## 구현 파일

- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_088.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_090.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_091.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_092.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Red/BT21_093.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Blue/BT21_095.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Purple/BT21_099.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/BT21/Bt21ScriptSupport.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Bt22CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Bt22ScriptSupport.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_001.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Yellow/BT22_002.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_004.cs`

## 검증

추가 테스트:

- `FullCardPortingBatch C0036 zone recovery partial implementation`
- `BT21-088 C0036 security plays self Tamer`
- `BT21-093 C0036 security deletes highest DP`

전체 regression 결과는 작업 종료 보고에 기록한다.

## 남은 범위

C0036은 L0006 boundary 이후 안전한 subset만 구현했다. 남은 효과는 source body가 확인되었지만 공통 layer가 부족한 `blocked` 상태다. blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
