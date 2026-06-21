# Full Card Porting C0038 Zone Security Recovery Source Mapping

## 결정

`C0038_zone_security_recovery`는 `blocked`로 처리한다.

이번 batch는 BT22 source body 10개를 직접 확인했다. 현재 engine layer로 source-aligned하게 표현 가능한 subset은 `BT22-048` inherited DP +2000, `BT22-056` inherited DP +2000, `BT22-065` OnPlay/WhenDigivolving DP -8000, `BT22-077` top/inherited end-turn optional unsuspend, `BT22-079` OnPlay Draw 1, `BT22-092` OnStartTurn set-memory-to-3 및 SecuritySkill play-self Tamer, `BT22-093` StartMainPhase memory +1 및 SecuritySkill play-self Tamer다. 나머지는 trait metadata, alternative digivolution requirements, Fortitude, Decode-like source movement, reveal multi-bucket selection, temporary keyword grants, cost-reduction BeforePayCost, nested Main effect activation, hand digivolution, and security trash interleaving 같은 공통 layer가 필요하므로 silent no-op이나 core CardId branch로 처리하지 않는다.

Queue status: blocked

Source evidence paths:

- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_048.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_051.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_054.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_056.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_065.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_069.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_077.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_079.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_092.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_093.cs`
- No CardId branch.

## Variant Identity

generated manifest와 queue mapping은 `CardId#CardIndex@Variant` identity를 유지한다. `BT22-048`, `BT22-054`, `BT22-056`, `BT22-069`, `BT22-077`, `BT22-079`, `BT22-092`, `BT22-093`은 복수 asset variant를 가진다. 특히 `BT22-093#7112@base`, `BT22-093#7113@P1`, `BT22-093#7114@P2`를 모두 추적한다. Catalog는 registry 역할만 하며 variant를 `CardId`만으로 완료 판정하지 않는다.

## Source Mapping

| Source effect | RL.Engine status | 구현/차단 범위 |
| --- | --- | --- |
| `BT22_048` | `PartiallyImplemented` | inherited owner-turn `ChangeSelfDPStaticEffect` DP +2000을 `ContinuousEffectDescriptor`로 구현했다. Alternative CS level-3 digivolution and OnPlay/WhenDigivolving DP +3000 followed by conditional temporary Raid/Piercing require CS trait metadata, alternative digivolution requirements, same-level stack predicate coverage, temporary keyword grants, and duration cleanup. |
| `BT22_051` | `Unsupported` | Alternative CS level-4 digivolution, Fortitude, OnPlay/WhenDigivolving suspend then conditional lowest-DP suspended bounce, and inherited battle-deletion top-security trash require alternative digivolution requirements, Fortitude/recovery replacement, same-level stack predicate coverage, source-aligned bounce-to-hand timing, and top-security trash/OnLoseSecurity interleaving. |
| `BT22_054` | `Unsupported` | Alternative CS level-2 digivolution, owner-turn once-per-turn DP -3000 on CS source add, and Main inherited top-card-to-bottom then draw require CS trait metadata, alternative digivolution requirements, OnAddDigivolutionCards card-condition payload, top-source reorder, and declaration-cost continuation. |
| `BT22_056` | `PartiallyImplemented` | inherited opponent-turn `ChangeSelfDPStaticEffect` DP +2000을 `ContinuousEffectDescriptor`로 구현했다. Alternative CS level-3 digivolution and OnPlay/WhenDigivolving DP -3000 followed by conditional De-Digivolve 1 require CS trait metadata, alternative digivolution requirements, same-level stack predicate coverage, and De-Digivolve support. |
| `BT22_065` | `PartiallyImplemented` | OnPlay and WhenDigivolving select 1 opponent Digimon and apply DP -8000 for the turn. Alternative CS level-5 digivolution and owner-turn once-per-turn opponent-deletion hand digivolution require CS trait metadata, alternative digivolution requirements, optional hand digivolution, and free digivolution continuation. |
| `BT22_069` | `Unsupported` | Alternative Light Fang/Night Claw/CS level-2 digivolution, OnPlay reveal top 3 dual-pick search, and Main inherited top-card-to-bottom then draw require trait metadata, alternative digivolution requirements, reveal multi-bucket selection, bottom-deck ordering, top-source reorder, and declaration-cost continuation. |
| `BT22_077` | `PartiallyImplemented` | top and inherited optional end-of-owner-turn unsuspend one owner Digimon are implemented through optional decision plus target selection. Alternative Light Fang/Night Claw/CS level-5 digivolution, Iceclad, Blocker metadata, WhenDigivolving source trash and bottom-deck return require trait metadata, alternative digivolution requirements, Iceclad keyword support, static keyword registry propagation, source-trash selection, and bottom-deck movement. |
| `BT22_079` | `PartiallyImplemented` | OnPlay Draw 1 is implemented through the draw primitive and L0006 draw event boundary. Blocker metadata and inherited breeding-area owner-turn Eater play-cost reduction require static keyword registry propagation, trait metadata, breeding-source trigger collection, BeforePayCost cost modifier, and once-per-turn cost-reduction decision support. |
| `BT22_092` | `PartiallyImplemented` | OnStartTurn set-memory-to-3 and SecuritySkill `PlaySelfTamerSecurityEffect` are implemented. Your-turn suspend-cost activation of a triggered Digimon's Main effect and memory +1 require Flame/CS trait metadata, OnEnterFieldAnyone played/digivolved payload, suspend-cost continuation, nested Main effect selection/execution, and AddUse propagation. |
| `BT22_093` | `PartiallyImplemented` | StartMainPhase memory +1 while opponent has a Digimon and SecuritySkill `PlaySelfTamerSecurityEffect` are implemented. Your-turn suspend-cost hand digivolution into CS Digimon requires CS trait metadata, digivolution payload with same-level source predicate, suspend-cost continuation, and free hand digivolution support. |

## 구현 파일

- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_048.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_051.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_054.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_056.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_065.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_069.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_077.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/White/BT22_079.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_092.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/White/BT22_093.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Bt22CardScriptCatalog.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Bt22ScriptSupport.cs`

## 검증

추가 테스트:

- `FullCardPortingBatch C0038 zone recovery partial implementation`
- `BT22 C0038 inherited DP continuous modifiers`
- `BT22-065 C0038 DP minus target effects`
- `BT22-077 C0038 optional unsuspend`
- `BT22 C0038 tamer and draw effects`

전체 regression 결과는 작업 종료 보고에 기록한다.

## 남은 범위

C0038은 L0006 boundary 이후 안전한 subset만 구현했다. 남은 효과는 source body가 확인되었지만 공통 layer가 부족한 `blocked` 상태다. blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
