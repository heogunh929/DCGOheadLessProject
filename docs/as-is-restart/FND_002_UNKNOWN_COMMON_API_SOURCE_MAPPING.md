# FND-002 Unknown Common API Source Mapping

이번 문서는 Foundation Gate/capability matrix에서 `unknown common API`로 남아 있는 27건을 current-state 기준으로 다시 찾아 source-aligned 분류만 수행한 결과다.
구현, `src` 수정, 원본 `DCGO` 수정, generated status 승격, FND-001/FND-003 수행은 하지 않았다.

## Scope

- AS-IS Source of Truth: `E:/headlessDCGO/DCGO`
- worktree `DCGO/` 존재 여부: 없음
- local read-only source root: `E:/headlessDCGO/DCGO/Assets`
- Primary gate input: `docs/generated/foundation-completion-gate.json`
- Full source list input: `docs/generated/full-mechanic-inventory.json`
- Capability matrix input: `docs/generated/capability-truth-audit/capability-registry.json`

## Count Audit

- Foundation Gate unknownCommonApiCount: 27
- Foundation Gate sample missingCommonApiCandidates: 20
- full-mechanic-inventory NeedsSourceReview discovered count: 27
- Expected 27 match: yes
- Gate count match: yes
- Difference note: Gate JSON의 `samples.missingCommonApiCandidates`는 20건으로 잘려 있으므로 전체 27건 원천 목록은 `full-mechanic-inventory.json`의 `missingLayerCandidates`에서 확인해야 한다.

## Classification Counts

| Classification | Count |
| --- | ---: |
| BlockedNeedsManualReview | 1 |
| CardEffectBodySpecific | 1 |
| DataDrivenRequired | 1 |
| FoundationRequired | 24 |

## Full Mapping Table

| API | FND-002 classification | Affected cards | Source files | Engine refs | Test/doc refs | Handoff | Source call evidence |
| --- | --- | ---: | ---: | ---: | ---: | --- | --- |
| `OnDeclaration` | FoundationRequired | 578 | 302 | 0 | 12 | FND-001, FND-003 | CardSource.cs:1051, Permanent.cs:1623/2927, TurnStateMachine.cs:1472/3061/3063 |
| `WhenPermanentWouldBeDeleted` | FoundationRequired | 405 | 210 | 0 | 1 | FND-001, FND-003 | CardController.cs:3696, Permanent.cs:2898/2954/3117/3138, Barrier.cs:99, Evade.cs:77 |
| `OnTappedAnyone` | FoundationRequired | 306 | 140 | 0 | 25 | FND-001, FND-003 | CardController.cs:5648 |
| `WhenRemoveField` | FoundationRequired | 304 | 167 | 0 | 15 | FND-001, FND-003 | CardController.cs:2333/2499/2660/2932/2948/3228/3244/3538/3705, CardObjectController.cs:1048, CanUseEffects/WhenRemoveField.cs:11 |
| `BeforePayCost` | FoundationRequired | 284 | 143 | 0 | 66 | FND-001, FND-003 | CardController.cs:604-621/709-711, CardSource.cs:2641 |
| `OnDetermineDoSecurityCheck` | FoundationRequired | 228 | 122 | 0 | 7 | FND-001, FND-003 | CardController.cs:4731, Permanent.cs:2591, Pierce.cs:78 |
| `OnEndBattle` | FoundationRequired | 160 | 86 | 0 | 16 | FND-001, FND-003 | CardController.cs:4718, CardEffectFactory.cs:452 |
| `OnDigivolutionCardDiscarded` | FoundationRequired | 121 | 54 | 0 | 18 | FND-001, FND-003 | CardController.cs:5181/5215 |
| `OnAddDigivolutionCards` | FoundationRequired | 102 | 51 | 0 | 22 | FND-001, FND-003 | Permanent.cs:1119/1223 |
| `WhenLinked` | FoundationRequired | 87 | 67 | 0 | 0 | FND-001, FND-003 | Permanent.cs:1290, CardEffectFactory.cs:874, CanUseEffects/WhenLinked.cs:45 |
| `OnMove` | FoundationRequired | 79 | 32 | 0 | 8 | FND-001, FND-003 | CardObjectController.cs:1111, CardEffectFactory.cs:854, CanUseEffects/OnMove.cs:10 |
| `OnUnTappedAnyone` | FoundationRequired | 70 | 30 | 0 | 11 | FND-001, FND-003 | CardController.cs:5694/5754 |
| `OnUseOption` | FoundationRequired | 65 | 31 | 0 | 7 | FND-001, FND-003 | CardController.cs:1765/1767 |
| `OnDiscardLibrary` | FoundationRequired | 51 | 21 | 0 | 7 | FND-001, FND-003 | CardController.cs:5816 |
| `OnAddSecurity` | FoundationRequired | 38 | 15 | 0 | 16 | FND-001, FND-003 | CardController.cs:5489 |
| `WhenReturntoLibraryAnyone` | FoundationRequired | 25 | 10 | 0 | 8 | FND-001, FND-003 | CardController.cs:2323/2489 |
| `OnDiscardSecurity` | FoundationRequired | 23 | 15 | 0 | 6 | FND-001, FND-003 | CardController.cs:4377 |
| `WhenDigisorption` | FoundationRequired | 15 | 10 | 0 | 12 | FND-001, FND-003 | ICardEffect.cs:978 plus CardEffect body samples from inventory |
| `AfterPayCost` | FoundationRequired | 15 | 8 | 0 | 13 | FND-001, FND-003 | CardController.cs:985 |
| `OnLinkCardDiscarded` | FoundationRequired | 14 | 8 | 0 | 0 | FND-001, FND-003 | CardController.cs:5327 |
| `OnRemovedField` | FoundationRequired | 4 | 3 | 0 | 3 | FND-001, FND-003 | CardObjectController.cs:524 |
| `WhenWouldLink` | FoundationRequired | 2 | 3 | 0 | 0 | FND-001, FND-003 | CardController.cs:3477, CanUseEffects/WhenWouldLink.cs:11 |
| `OnFaceUpSecurityIncreased` | DataDrivenRequired | 2 | 2 | 0 | 0 | FND-001, FND-003 | CardController.cs:5506/5548 |
| `OnUseDigiburst` | CardEffectBodySpecific | 1 | 2 | 0 | 0 | FND-003 | CardController.cs:2228 |
| `WhenUntapAnyone` | FoundationRequired | 1 | 2 | 0 | 13 | FND-001, FND-003 | CardController.cs:5694 |
| `WhenWouldDigivolutionCardDiscarded` | FoundationRequired | 1 | 2 | 0 | 1 | FND-001, FND-003 | CardController.cs:5181/5293(commented historical occurrence) |
| `OnStartBattle` | BlockedNeedsManualReview | 0 | 1 | 0 | 4 | ManualReview | CardController.cs:4557 |

## FoundationRequired

- `OnDeclaration`: 선언형 효과 legal action/decision boundary와 once-per-turn 사용 등록이 필요한 공통 layer다.
- `WhenPermanentWouldBeDeleted`: 삭제 replacement/delete prevention cut-in이며 keyword와 source effect가 모두 참조한다.
- `OnTappedAnyone`: suspend 완료 후 payload를 stack하는 공통 suspend primitive/event layer가 필요하다.
- `WhenRemoveField`: actual removal 이후가 아니라 would-remove cut-in이므로 target refix와 replacement payload가 필요하다.
- `BeforePayCost`: 비용 지불 전 후보 수집, background effect, cut-in, 비용 변경을 묶는 cost state machine이 필요하다.
- `OnDetermineDoSecurityCheck`: Pierce/security check 결정 시점 payload와 security check modifier가 필요한 battle/security layer다.
- `OnEndBattle`: winner/loser/battle object snapshot을 전달하는 battle lifecycle layer가 필요하다.
- `OnDigivolutionCardDiscarded`: source trash actual event이며 would-discard cut-in 이후 snapshot payload가 필요하다.
- `OnAddDigivolutionCards`: source add 이후 Permanent/CardSources/root payload를 전달하는 source movement layer가 필요하다.
- `WhenLinked`: link lifecycle에서 link source와 target permanent 조건을 함께 보는 공통 layer다.
- `OnMove`: 단순 zone 이동 완료가 아니라 이동한 permanent 생존 재검증 payload가 필요한 movement layer다.
- `OnUnTappedAnyone`: WhenUntapAnyone cut-in 뒤 actual unsuspend와 후속 stack이 분리되어야 한다.
- `OnUseOption`: executing zone, root, cost, background timing, OptionSkill 순서를 보존하는 option execution boundary가 필요하다.
- `OnDiscardLibrary`: library/deck trash primitive와 DiscardedCards payload가 필요한 mill event layer다.
- `OnAddSecurity`: security 증가 후 trigger stack과 face-up 상태 경계가 필요한 security movement layer다.
- `WhenReturntoLibraryAnyone`: return-to-library actual mutation 전 replacement cut-in과 willBeRemoveField target refix가 필요하다.
- `OnDiscardSecurity`: security trash/discard primitive와 payload stack이 필요한 security movement layer다.
- `WhenDigisorption`: BeforePayCost 비용 감소 body와 이후 WhenDigisorption trigger가 결합된 cost mechanic layer다.
- `AfterPayCost`: 비용 지불 이후 cut-in stack boundary가 필요한 cost state machine 후단이다.
- `OnLinkCardDiscarded`: link source discard 후속 event이며 link lifecycle/source discard payload가 필요하다.
- `OnRemovedField`: field removal 완료/후속 timing 의미가 source body 단위로 고정되어야 하는 field-removal layer다.
- `WhenWouldLink`: link 실행 직전 cut-in/replacement 성격의 would-link boundary가 필요하다.
- `WhenUntapAnyone`: actual unsuspend 이전 선택/지속효과/재검증을 포함하는 replacement 성격 cut-in이다.
- `WhenWouldDigivolutionCardDiscarded`: source trash actual mutation 전 replacement/cut-in boundary가 필요하다.

## BlockedNeedsManualReview

- `OnStartBattle`: source dispatch는 있지만 affected card가 0이므로 Gate blocker로 유지할지와 fixture 필요 범위를 수동 검토해야 한다.

## FND-001 Handoff

아래 항목은 공통 runtime/event/source payload foundation surface와 맞물린다. 이 문서는 FND-001을 수행하지 않고, 후속 FND-001에서 검토할 dependency 범위만 넘긴다.

- `OnDeclaration`: CardSource/Permanent/TurnStateMachine declaration skill discovery
- `WhenPermanentWouldBeDeleted`: Permanent delete prevention and keyword-granted replacement
- `OnTappedAnyone`: CardController suspend event stack
- `WhenRemoveField`: CardController/CardObjectController would-remove cut-in
- `BeforePayCost`: CardController/CardSource pre-cost collection and cut-in
- `OnDetermineDoSecurityCheck`: CardController/Permanent security-check modifier collection
- `OnEndBattle`: CardController battle end stack
- `OnDigivolutionCardDiscarded`: CardController source trash actual event stack
- `OnAddDigivolutionCards`: Permanent source-add event stack
- `WhenLinked`: Permanent/CardEffectFactory link timing and CanTriggerWhenLinked
- `OnMove`: CardObjectController/CardEffectFactory movement event stack
- `OnUnTappedAnyone`: CardController unsuspend actual event stack
- `OnUseOption`: CardController option execution boundary
- `OnDiscardLibrary`: CardController deck trash event stack
- `OnAddSecurity`: CardController security-add event stack
- `WhenReturntoLibraryAnyone`: CardController return-to-library would-remove cut-in
- `OnDiscardSecurity`: CardController security discard event stack
- `WhenDigisorption`: BeforePayCost cost-reduction body timing
- `AfterPayCost`: CardController post-cost cut-in stack
- `OnLinkCardDiscarded`: CardController link source discard event stack
- `OnRemovedField`: CardObjectController post-removal event stack
- `WhenWouldLink`: CardController/CardEffectCommons would-link cut-in
- `OnFaceUpSecurityIncreased`: CardController face-up security state event stack
- `WhenUntapAnyone`: CardController would-untap cut-in
- `WhenWouldDigivolutionCardDiscarded`: CardController would-source-trash cut-in

## FND-003 Handoff

아래 항목은 current capability matrix에서 `Unsupported`로 확인되었으므로 FND-003에서 remediation prompt 또는 explicit blocked queue로 분해해야 한다. 이 문서는 FND-003을 수행하지 않는다.

- `OnDeclaration`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenPermanentWouldBeDeleted`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnTappedAnyone`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenRemoveField`: current capability status `Unsupported`, classification `FoundationRequired`
- `BeforePayCost`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnDetermineDoSecurityCheck`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnEndBattle`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnDigivolutionCardDiscarded`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnAddDigivolutionCards`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenLinked`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnMove`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnUnTappedAnyone`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnUseOption`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnDiscardLibrary`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnAddSecurity`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenReturntoLibraryAnyone`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnDiscardSecurity`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenDigisorption`: current capability status `Unsupported`, classification `FoundationRequired`
- `AfterPayCost`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnLinkCardDiscarded`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnRemovedField`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenWouldLink`: current capability status `Unsupported`, classification `FoundationRequired`
- `OnFaceUpSecurityIncreased`: current capability status `Unsupported`, classification `DataDrivenRequired`
- `OnUseDigiburst`: current capability status `Unsupported`, classification `CardEffectBodySpecific`
- `WhenUntapAnyone`: current capability status `Unsupported`, classification `FoundationRequired`
- `WhenWouldDigivolutionCardDiscarded`: current capability status `Unsupported`, classification `FoundationRequired`

## Exclusions

- 제외한 unknown common API 항목 없음.
- 100MiB 이상 raw JSON 추가 없음.

## Per API Evidence

### OnDeclaration

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 302, affected cards 578, engine refs 0, test/doc refs 12
- Source class/method/call area: CardSource/Permanent/TurnStateMachine declaration skill discovery
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:973 - EffectTiming.OnDeclaration
- Runtime call evidence: CardSource.cs:1051, Permanent.cs:1623/2927, TurnStateMachine.cs:1472/3061/3063
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_025.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: 선언형 효과 legal action/decision boundary와 once-per-turn 사용 등록이 필요한 공통 layer다.

### WhenPermanentWouldBeDeleted

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 210, affected cards 405, engine refs 0, test/doc refs 1
- Source class/method/call area: Permanent delete prevention and keyword-granted replacement
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:980 - EffectTiming.WhenPermanentWouldBeDeleted
- Runtime call evidence: CardController.cs:3696, Permanent.cs:2898/2954/3117/3138, Barrier.cs:99, Evade.cs:77
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/BT10/Black/BT10_066.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: 삭제 replacement/delete prevention cut-in이며 keyword와 source effect가 모두 참조한다.

### OnTappedAnyone

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 140, affected cards 306, engine refs 0, test/doc refs 25
- Source class/method/call area: CardController suspend event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1000 - EffectTiming.OnTappedAnyone
- Runtime call evidence: CardController.cs:5648
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs, DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: suspend 완료 후 payload를 stack하는 공통 suspend primitive/event layer가 필요하다.

### WhenRemoveField

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 167, affected cards 304, engine refs 0, test/doc refs 15
- Source class/method/call area: CardController/CardObjectController would-remove cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:979 - EffectTiming.WhenRemoveField
- Runtime call evidence: CardController.cs:2333/2499/2660/2932/2948/3228/3244/3538/3705, CardObjectController.cs:1048, CanUseEffects/WhenRemoveField.cs:11
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs, DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: actual removal 이후가 아니라 would-remove cut-in이므로 target refix와 replacement payload가 필요하다.

### BeforePayCost

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 143, affected cards 284, engine refs 0, test/doc refs 66
- Source class/method/call area: CardController/CardSource pre-cost collection and cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1015 - EffectTiming.BeforePayCost
- Runtime call evidence: CardController.cs:604-621/709-711, CardSource.cs:2641
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_088.cs, DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: 비용 지불 전 후보 수집, background effect, cut-in, 비용 변경을 묶는 cost state machine이 필요하다.

### OnDetermineDoSecurityCheck

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 122, affected cards 228, engine refs 0, test/doc refs 7
- Source class/method/call area: CardController/Permanent security-check modifier collection
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1013 - EffectTiming.OnDetermineDoSecurityCheck
- Runtime call evidence: CardController.cs:4731, Permanent.cs:2591, Pierce.cs:78
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs, DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: Pierce/security check 결정 시점 payload와 security check modifier가 필요한 battle/security layer다.

### OnEndBattle

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 86, affected cards 160, engine refs 0, test/doc refs 16
- Source class/method/call area: CardController battle end stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1012 - EffectTiming.OnEndBattle
- Runtime call evidence: CardController.cs:4718, CardEffectFactory.cs:452
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_077.cs, DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_112.cs, DCGO/Assets/Scripts/CardEffect/BT10/Blue/BT10_028.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: winner/loser/battle object snapshot을 전달하는 battle lifecycle layer가 필요하다.

### OnDigivolutionCardDiscarded

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 54, affected cards 121, engine refs 0, test/doc refs 18
- Source class/method/call area: CardController source trash actual event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1017 - EffectTiming.OnDigivolutionCardDiscarded
- Runtime call evidence: CardController.cs:5181/5215
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_006.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_072.cs, DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_073.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: source trash actual event이며 would-discard cut-in 이후 snapshot payload가 필요하다.

### OnAddDigivolutionCards

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 51, affected cards 102, engine refs 0, test/doc refs 22
- Source class/method/call area: Permanent source-add event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1002 - EffectTiming.OnAddDigivolutionCards
- Runtime call evidence: Permanent.cs:1119/1223
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_093.cs, DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_088.cs, DCGO/Assets/Scripts/CardEffect/BT17/Black/BT17_056.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: source add 이후 Permanent/CardSources/root payload를 전달하는 source movement layer가 필요하다.

### WhenLinked

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 67, affected cards 87, engine refs 0, test/doc refs 0
- Source class/method/call area: Permanent/CardEffectFactory link timing and CanTriggerWhenLinked
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1025 - EffectTiming.WhenLinked
- Runtime call evidence: Permanent.cs:1290, CardEffectFactory.cs:874, CanUseEffects/WhenLinked.cs:45
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_053.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_054.cs, DCGO/Assets/Scripts/CardEffect/BT21/Black/BT21_059.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: link lifecycle에서 link source와 target permanent 조건을 함께 보는 공통 layer다.

### OnMove

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 32, affected cards 79, engine refs 0, test/doc refs 8
- Source class/method/call area: CardObjectController/CardEffectFactory movement event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:997 - EffectTiming.OnMove
- Runtime call evidence: CardObjectController.cs:1111, CardEffectFactory.cs:854, CanUseEffects/OnMove.cs:10
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT11/Purple/BT11_087.cs, DCGO/Assets/Scripts/CardEffect/BT15/Green/BT15_054.cs, DCGO/Assets/Scripts/CardEffect/BT16/White/BT16_082.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: 단순 zone 이동 완료가 아니라 이동한 permanent 생존 재검증 payload가 필요한 movement layer다.

### OnUnTappedAnyone

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 30, affected cards 70, engine refs 0, test/doc refs 11
- Source class/method/call area: CardController unsuspend actual event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1001 - EffectTiming.OnUnTappedAnyone
- Runtime call evidence: CardController.cs:5694/5754
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_069.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_074.cs, DCGO/Assets/Scripts/CardEffect/BT11/Blue/BT11_032.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: WhenUntapAnyone cut-in 뒤 actual unsuspend와 후속 stack이 분리되어야 한다.

### OnUseOption

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 31, affected cards 65, engine refs 0, test/doc refs 7
- Source class/method/call area: CardController option execution boundary
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:972 - EffectTiming.OnUseOption
- Runtime call evidence: CardController.cs:1765/1767
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Yellow/BT10_032.cs, DCGO/Assets/Scripts/CardEffect/BT12/Yellow/BT12_044.cs, DCGO/Assets/Scripts/CardEffect/BT17/Yellow/BT17_031.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: executing zone, root, cost, background timing, OptionSkill 순서를 보존하는 option execution boundary가 필요하다.

### OnDiscardLibrary

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 21, affected cards 51, engine refs 0, test/doc refs 7
- Source class/method/call area: CardController deck trash event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:995 - EffectTiming.OnDiscardLibrary
- Runtime call evidence: CardController.cs:5816
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_108.cs, DCGO/Assets/Scripts/CardEffect/BT14/Purple/BT14_077.cs, DCGO/Assets/Scripts/CardEffect/BT19/Purple/BT19_071.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: library/deck trash primitive와 DiscardedCards payload가 필요한 mill event layer다.

### OnAddSecurity

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 15, affected cards 38, engine refs 0, test/doc refs 16
- Source class/method/call area: CardController security-add event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:991 - EffectTiming.OnAddSecurity
- Runtime call evidence: CardController.cs:5489
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_003.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_033.cs, DCGO/Assets/Scripts/CardEffect/BT14/Yellow/BT14_041.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: security 증가 후 trigger stack과 face-up 상태 경계가 필요한 security movement layer다.

### WhenReturntoLibraryAnyone

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 10, affected cards 25, engine refs 0, test/doc refs 8
- Source class/method/call area: CardController return-to-library would-remove cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:981 - EffectTiming.WhenReturntoLibraryAnyone
- Runtime call evidence: CardController.cs:2323/2489
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_062.cs, DCGO/Assets/Scripts/CardEffect/BT11/Black/BT11_064.cs, DCGO/Assets/Scripts/CardEffect/BT20/Purple/BT20_074.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: return-to-library actual mutation 전 replacement cut-in과 willBeRemoveField target refix가 필요하다.

### OnDiscardSecurity

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 15, affected cards 23, engine refs 0, test/doc refs 6
- Source class/method/call area: CardController security discard event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:994 - EffectTiming.OnDiscardSecurity
- Runtime call evidence: CardController.cs:4377
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_098.cs, DCGO/Assets/Scripts/CardEffect/BT13/Yellow/BT13_106.cs, DCGO/Assets/Scripts/CardEffect/BT15/Yellow/BT15_037.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: security trash/discard primitive와 payload stack이 필요한 security movement layer다.

### WhenDigisorption

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 10, affected cards 15, engine refs 0, test/doc refs 12
- Source class/method/call area: BeforePayCost cost-reduction body timing
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:978 - EffectTiming.WhenDigisorption
- Runtime call evidence: ICardEffect.cs:978 plus CardEffect body samples from inventory
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Green/BT10_052.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs, DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_047.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: BeforePayCost 비용 감소 body와 이후 WhenDigisorption trigger가 결합된 cost mechanic layer다.

### AfterPayCost

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 8, affected cards 15, engine refs 0, test/doc refs 13
- Source class/method/call area: CardController post-cost cut-in stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1016 - EffectTiming.AfterPayCost
- Runtime call evidence: CardController.cs:985
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_109.cs, DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_103.cs, DCGO/Assets/Scripts/CardEffect/BT5/White/BT5_109.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: 비용 지불 이후 cut-in stack boundary가 필요한 cost state machine 후단이다.

### OnLinkCardDiscarded

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 8, affected cards 14, engine refs 0, test/doc refs 0
- Source class/method/call area: CardController link source discard event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1029 - EffectTiming.OnLinkCardDiscarded
- Runtime call evidence: CardController.cs:5327
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_030.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_062.cs, DCGO/Assets/Scripts/CardEffect/EX10/Black/EX10_070.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: link source discard 후속 event이며 link lifecycle/source discard payload가 필요하다.

### OnRemovedField

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 3, affected cards 4, engine refs 0, test/doc refs 3
- Source class/method/call area: CardObjectController post-removal event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1028 - EffectTiming.OnRemovedField
- Runtime call evidence: CardObjectController.cs:524
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT22/White/BT22_007.cs, DCGO/Assets/Scripts/CardEffect/EX10/Red/EX10_010.cs, DCGO/Assets/Scripts/Script/CardObjectController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: field removal 완료/후속 timing 의미가 source body 단위로 고정되어야 하는 field-removal layer다.

### WhenWouldLink

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 3, affected cards 2, engine refs 0, test/doc refs 0
- Source class/method/call area: CardController/CardEffectCommons would-link cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1024 - EffectTiming.WhenWouldLink
- Runtime call evidence: CardController.cs:3477, CanUseEffects/WhenWouldLink.cs:11
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_004.cs, DCGO/Assets/Scripts/CardEffect/BT25/Green/BT25_045.cs, DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: link 실행 직전 cut-in/replacement 성격의 would-link boundary가 필요하다.

### OnFaceUpSecurityIncreased

- Classification: `DataDrivenRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 2, affected cards 2, engine refs 0, test/doc refs 0
- Source class/method/call area: CardController face-up security state event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1030 - EffectTiming.OnFaceUpSecurityIncreased
- Runtime call evidence: CardController.cs:5506/5548
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs, DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: face-up security 수와 security card 상태 변화에 의존하므로 source data/state 모델 확정이 필요하다.

### OnUseDigiburst

- Classification: `CardEffectBodySpecific`
- Capability matrix status: `Unsupported`
- Counts: source files 2, affected cards 1, engine refs 0, test/doc refs 0
- Source class/method/call area: CardController DigiBurst use event stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:992 - EffectTiming.OnUseDigiburst
- Runtime call evidence: CardController.cs:2228
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs, DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-003
- Rationale: affected card가 1건이고 DigiBurst special mechanic body와 결합되어 있어 FND-003에서 mechanic 분해가 먼저 필요하다.

### WhenUntapAnyone

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 2, affected cards 1, engine refs 0, test/doc refs 13
- Source class/method/call area: CardController would-untap cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:983 - EffectTiming.WhenUntapAnyone
- Runtime call evidence: CardController.cs:5694
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT7/Green/BT7_055.cs, DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: actual unsuspend 이전 선택/지속효과/재검증을 포함하는 replacement 성격 cut-in이다.

### WhenWouldDigivolutionCardDiscarded

- Classification: `FoundationRequired`
- Capability matrix status: `Unsupported`
- Counts: source files 2, affected cards 1, engine refs 0, test/doc refs 1
- Source class/method/call area: CardController would-source-trash cut-in
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1023 - EffectTiming.WhenWouldDigivolutionCardDiscarded
- Runtime call evidence: CardController.cs:5181/5293(commented historical occurrence)
- Card data/source samples: DCGO/Assets/Scripts/CardEffect/BT10/Purple/BT10_084.cs, DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: FND-001, FND-003
- Rationale: source trash actual mutation 전 replacement/cut-in boundary가 필요하다.

### OnStartBattle

- Classification: `BlockedNeedsManualReview`
- Capability matrix status: `Unsupported`
- Counts: source files 1, affected cards 0, engine refs 0, test/doc refs 4
- Source class/method/call area: CardController battle start stack
- Enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1011 - EffectTiming.OnStartBattle
- Runtime call evidence: CardController.cs:4557
- Card data/source samples: DCGO/Assets/Scripts/Script/CardController.cs
- Gate evidence: full-mechanic-inventory NeedsSourceReview, Foundation Gate unknownCommonApiCount=27
- Handoff: ManualReview
- Rationale: source dispatch는 있지만 affected card가 0이므로 Gate blocker로 유지할지와 fixture 필요 범위를 수동 검토해야 한다.

## Next Goal Candidate Range

- FND-001: 공통 event/source payload layer와 `ContinuousOrStaticEffect` partial dependency를 함께 검토한다.
- FND-003: 26개 Unsupported remediation 항목을 common runtime layer prompt로 분해한다.
- ManualReview: `OnStartBattle`은 affected card 0인데 source dispatch가 있으므로 Gate blocker 유지 여부와 battle-start fixture 범위를 먼저 판단한다.

## Non-Goals Confirmed

- `src/` C# 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.
- Foundation Gate 수치 조작 없음.
- generated status `Verified`/`Implemented` 승격 없음.
- commit/push 없음.
