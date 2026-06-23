# FND-003 Unsupported Capability Remediation

이번 문서는 Foundation Gate/capability matrix에서 `Unsupported`로 남아 있는 referenced capability 26건을 source-aligned remediation task 단위로 분해한 결과다.
구현, `src` 수정, 원본 `DCGO` 수정, generated status 승격, FND-001/TRUST-001 수행은 하지 않았다.

## Scope

- AS-IS Source of Truth: `E:/headlessDCGO/DCGO`
- worktree `DCGO/` 존재 여부: 없음
- local read-only source root: `E:/headlessDCGO/DCGO/Assets`
- FND-002 input: `docs/generated/as-is-restart/fnd-002-unknown-common-api-mapping.json`
- Primary gate input: `docs/generated/foundation-completion-gate.json`
- Capability matrix input: `docs/generated/capability-truth-audit/capability-registry.json`
- Source-required input: `docs/generated/capability-truth-audit/source-required-capabilities.json`

## Count Audit

- Foundation Gate unsupportedCapabilityCount: 26
- Foundation Gate sample unsupportedCapabilities: 20
- Recomputed referenced Unsupported capabilities: 26
- Expected 26 match: yes
- Gate count match: yes
- FND-002 FND-003 handoff count: 26
- FND-002 handoff set match: yes
- Difference note: Gate JSON의 `samples.unsupportedCapabilities`는 20건으로 잘려 있으므로 전체 26건은 `source-required-capabilities.json`과 capability registry 교집합으로 재계산했다.
- Difference note: FND-002의 `OnStartBattle`은 affected card 0/manual review 항목이라 FND-003 referenced Unsupported 26건에는 포함되지 않는다.

## Classification Counts

| Classification | Count |
| --- | ---: |
| CardEffectBodySpecific | 1 |
| DataPolicyRequired | 1 |
| ImplementableFoundationTask | 15 |
| PartialNeedsSubtasks | 9 |

## Full Remediation Table

| Capability | FND-003 classification | Affected cards | Source files | FND-001 | TRUST-001 | Source call evidence |
| --- | --- | ---: | ---: | --- | --- | --- |
| `OnDeclaration` | ImplementableFoundationTask | 578 | 298 | yes | yes | CardSource.cs:1051, Permanent.cs:1623/2927, TurnStateMachine.cs:1472/3061/3063 |
| `WhenPermanentWouldBeDeleted` | PartialNeedsSubtasks | 405 | 206 | yes | yes | CardController.cs:3696, Permanent.cs:2898/2954/3117/3138, Barrier.cs:99, Evade.cs:77 |
| `OnTappedAnyone` | ImplementableFoundationTask | 306 | 139 | yes | yes | CardController.cs:5648 |
| `WhenRemoveField` | PartialNeedsSubtasks | 304 | 164 | yes | yes | CardController.cs:2333/2499/2660/2932/2948/3228/3244/3538/3705, CardObjectController.cs:1048, CanUseEffects/WhenRemoveField.cs:11 |
| `BeforePayCost` | ImplementableFoundationTask | 284 | 141 | yes | yes | CardController.cs:604-621/709-711, CardSource.cs:2641 |
| `OnDetermineDoSecurityCheck` | ImplementableFoundationTask | 228 | 119 | yes | yes | CardController.cs:4731, Permanent.cs:2591, Pierce.cs:78 |
| `OnEndBattle` | ImplementableFoundationTask | 160 | 84 | yes | yes | CardController.cs:4718, CardEffectFactory.cs:452 |
| `OnDigivolutionCardDiscarded` | ImplementableFoundationTask | 121 | 53 | yes | yes | CardController.cs:5181/5215 |
| `OnAddDigivolutionCards` | ImplementableFoundationTask | 102 | 50 | yes | yes | Permanent.cs:1119/1223 |
| `WhenLinked` | PartialNeedsSubtasks | 87 | 64 | yes | yes | Permanent.cs:1290, CardEffectFactory.cs:874, CanUseEffects/WhenLinked.cs:45 |
| `OnMove` | ImplementableFoundationTask | 79 | 30 | yes | yes | CardObjectController.cs:1111, CardEffectFactory.cs:854, CanUseEffects/OnMove.cs:10 |
| `OnUnTappedAnyone` | ImplementableFoundationTask | 70 | 29 | yes | yes | CardController.cs:5694/5754 |
| `OnUseOption` | ImplementableFoundationTask | 65 | 30 | yes | yes | CardController.cs:1765/1767 |
| `OnDiscardLibrary` | ImplementableFoundationTask | 51 | 20 | yes | yes | CardController.cs:5816 |
| `OnAddSecurity` | ImplementableFoundationTask | 38 | 14 | yes | yes | CardController.cs:5489 |
| `WhenReturntoLibraryAnyone` | PartialNeedsSubtasks | 25 | 9 | yes | yes | CardController.cs:2323/2489 |
| `OnDiscardSecurity` | ImplementableFoundationTask | 23 | 14 | yes | yes | CardController.cs:4377 |
| `AfterPayCost` | ImplementableFoundationTask | 15 | 7 | yes | yes | CardController.cs:985 |
| `WhenDigisorption` | PartialNeedsSubtasks | 15 | 10 | yes | yes | ICardEffect.cs:978 plus CardEffect body samples from inventory |
| `OnLinkCardDiscarded` | PartialNeedsSubtasks | 14 | 7 | yes | yes | CardController.cs:5327 |
| `OnRemovedField` | ImplementableFoundationTask | 4 | 2 | yes | yes | CardObjectController.cs:524 |
| `OnFaceUpSecurityIncreased` | DataPolicyRequired | 2 | 1 | yes | yes | CardController.cs:5506/5548 |
| `WhenWouldLink` | PartialNeedsSubtasks | 2 | 2 | yes | yes | CardController.cs:3477, CanUseEffects/WhenWouldLink.cs:11 |
| `OnUseDigiburst` | CardEffectBodySpecific | 1 | 1 | no | yes | CardController.cs:2228 |
| `WhenUntapAnyone` | PartialNeedsSubtasks | 1 | 1 | yes | yes | CardController.cs:5694 |
| `WhenWouldDigivolutionCardDiscarded` | PartialNeedsSubtasks | 1 | 1 | yes | yes | CardController.cs:5181/5293(commented historical occurrence) |

## ImplementableFoundationTask

- `OnDeclaration`
- `OnTappedAnyone`
- `BeforePayCost`
- `OnDetermineDoSecurityCheck`
- `OnEndBattle`
- `OnDigivolutionCardDiscarded`
- `OnAddDigivolutionCards`
- `OnMove`
- `OnUnTappedAnyone`
- `OnUseOption`
- `OnDiscardLibrary`
- `OnAddSecurity`
- `OnDiscardSecurity`
- `AfterPayCost`
- `OnRemovedField`

## BlockedNeedsManualReview

- 없음

## FND-001 ContinuousOrStaticEffect Handoff

- `OnDeclaration`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenPermanentWouldBeDeleted`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnTappedAnyone`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenRemoveField`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `BeforePayCost`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnDetermineDoSecurityCheck`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnEndBattle`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnDigivolutionCardDiscarded`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnAddDigivolutionCards`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenLinked`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnMove`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnUnTappedAnyone`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnUseOption`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnDiscardLibrary`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnAddSecurity`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenReturntoLibraryAnyone`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnDiscardSecurity`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `AfterPayCost`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenDigisorption`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnLinkCardDiscarded`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnRemovedField`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `OnFaceUpSecurityIncreased`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenWouldLink`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenUntapAnyone`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.
- `WhenWouldDigivolutionCardDiscarded`: ContinuousOrStaticEffect/source collection, temporary granted trigger, linked/inherited/face-up security source scope와 맞물려 FND-001 dependency로 표시한다.

## TRUST-001 Handoff

- `OnDeclaration`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenPermanentWouldBeDeleted`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnTappedAnyone`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenRemoveField`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `BeforePayCost`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnDetermineDoSecurityCheck`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnEndBattle`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnDigivolutionCardDiscarded`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnAddDigivolutionCards`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenLinked`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnMove`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnUnTappedAnyone`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnUseOption`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnDiscardLibrary`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnAddSecurity`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenReturntoLibraryAnyone`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnDiscardSecurity`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `AfterPayCost`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenDigisorption`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnLinkCardDiscarded`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnRemovedField`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnFaceUpSecurityIncreased`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenWouldLink`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `OnUseDigiburst`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenUntapAnyone`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.
- `WhenWouldDigivolutionCardDiscarded`: capability matrix status가 Unsupported이고 registry implementationEvidence가 있으므로 기존 src stub/partial claim과 Source of Truth를 TRUST-001에서 대조해야 한다.

## Exclusions

- 제외한 unsupported capability 항목 없음.
- UnityOnlyExcluded, ExternalAdapterRequired, AliasOrDuplicate, ObsoleteOrDead 분류 항목 없음.
- 100MiB 이상 raw JSON 추가 없음.

## Per Capability Evidence And Tasks

### OnDeclaration

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 578, source files 298
- Source class/method area: CardSource/Permanent/TurnStateMachine declaration skill discovery
- Source call evidence: CardSource.cs:1051, Permanent.cs:1623/2927, TurnStateMachine.cs:1472/3061/3063
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:973 - EffectTiming.OnDeclaration
- Card data samples: DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088.asset, DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_088_P1.asset, DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089.asset, DCGO/Assets/CardBaseEntity/BT1/Green/Tamer/BT1_089_P1.asset, DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_025.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Declaration/cost continuation is still unresolved for generated full-card batches.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - declarable effect discovery contract를 CardSource/Permanent source collection 단위로 문서화한다.
  - legal action/DecisionPoint payload에 source card, root, once-per-turn key, activation owner를 포함하는 task로 분리한다.
  - TurnStateMachine command 선택과 headless action generation parity fixture 범위를 정의한다.

### WhenPermanentWouldBeDeleted

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 405, source files 206
- Source class/method area: Permanent delete prevention and keyword-granted replacement
- Source call evidence: CardController.cs:3696, Permanent.cs:2898/2954/3117/3138, Barrier.cs:99, Evade.cs:77
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:980 - EffectTiming.WhenPermanentWouldBeDeleted
- Card data samples: DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset, DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset, DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset, DCGO/Assets/CardBaseEntity/BT10/Red/Digimon/BT10_009.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - delete-prevention/replacement cut-in payload를 deletion reason, source effect, candidate permanents로 분리한다.
  - Barrier/Evade/Scapegoat 계열 granted timing source collection task를 분리한다.
  - actual deletion 전 target refix와 self-effect exclusion fixture task를 별도로 둔다.

### OnTappedAnyone

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 306, source files 139
- Source class/method area: CardController suspend event stack
- Source call evidence: CardController.cs:5648
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1000 - EffectTiming.OnTappedAnyone
- Card data samples: DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset, DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset, DCGO/Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - suspend primitive의 actual suspend 후 event payload를 Permanents/IsBlock/CardEffect로 고정한다.
  - block suspend와 effect-caused suspend를 구분하는 trigger source task를 만든다.
  - OnTappedAnyone queue/replay payload fixture를 추가 task로 정의한다.

### WhenRemoveField

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 304, source files 164
- Source class/method area: CardController/CardObjectController would-remove cut-in
- Source call evidence: CardController.cs:2333/2499/2660/2932/2948/3228/3244/3538/3705, CardObjectController.cs:1048, CanUseEffects/WhenRemoveField.cs:11
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:979 - EffectTiming.WhenRemoveField
- Card data samples: DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Would-remove replacement/cut-in semantics remain unresolved for generated batches.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - would-remove cut-in payload를 willBeRemoveField, reason, battle/digixros flags로 분리한다.
  - return-to-hand/deck/delete/security-like removal별 shared target refix task를 만든다.
  - WhenRemoveField를 OnLeaveFieldAnyone/OnRemovedField alias로 처리하지 않는 negative fixture task를 둔다.

### BeforePayCost

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 284, source files 141
- Source class/method area: CardController/CardSource pre-cost collection and cut-in
- Source call evidence: CardController.cs:604-621/709-711, CardSource.cs:2641
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1015 - EffectTiming.BeforePayCost
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_052.asset, DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087.asset, DCGO/Assets/CardBaseEntity/BT10/Red/Tamer/BT10_087_P1.asset, DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088.asset, DCGO/Assets/CardBaseEntity/BT10/Blue/Tamer/BT10_088_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Cost modifier/replacement layer is documented as a common blocker and is not executable for full-card batches.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - pay-cost state machine의 pre-cost collection, background effects, cut-in 순서를 task로 분리한다.
  - cost modifier source scope와 max payable memory validation task를 분리한다.
  - Digisorption/DigiXros/option play cost와 공유 가능한 CostResolver hook contract를 정의한다.

### OnDetermineDoSecurityCheck

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 228, source files 119
- Source class/method area: CardController/Permanent security-check modifier collection
- Source call evidence: CardController.cs:4731, Permanent.cs:2591, Pierce.cs:78
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1013 - EffectTiming.OnDetermineDoSecurityCheck
- Card data samples: DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset, DCGO/Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - battle security-check 결정 payload에 attacker, defender, battle object, security-check count를 고정한다.
  - Pierce/security check modifier source collection task를 분리한다.
  - security check 변경 후 OnSecurityCheck와의 순서 fixture task를 정의한다.

### OnEndBattle

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 160, source files 84
- Source class/method area: CardController battle end stack
- Source call evidence: CardController.cs:4718, CardEffectFactory.cs:452
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1012 - EffectTiming.OnEndBattle
- Card data samples: DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077.asset, DCGO/Assets/CardBaseEntity/BT1/Green/Digimon/BT1_077_P1.asset, DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_112.asset, DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028.asset, DCGO/Assets/CardBaseEntity/BT10/Blue/Digimon/BT10_028_P0.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Battle-end payload and lifecycle boundary remain unresolved for generated full-card batches.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - battle end payload를 winner/loser/tie/battle snapshot으로 고정한다.
  - attack cleanup 이전/이후 trigger 순서 fixture task를 분리한다.
  - battle object lifetime과 stale permanent handling task를 정의한다.

### OnDigivolutionCardDiscarded

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 121, source files 53
- Source class/method area: CardController source trash actual event stack
- Source call evidence: CardController.cs:5181/5215
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1017 - EffectTiming.OnDigivolutionCardDiscarded
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/DigiEgg/BT10_006_P0.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_072.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_073_P0.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - would-discard cut-in과 actual source trash event를 별도 task로 분리한다.
  - DiscardedCards snapshot payload와 rollback/resume 불가 조건을 정의한다.
  - source trash primitive가 ZoneMover 경유로 event를 발행하는 fixture task를 둔다.

### OnAddDigivolutionCards

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 102, source files 50
- Source class/method area: Permanent source-add event stack
- Source call evidence: Permanent.cs:1119/1223
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1002 - EffectTiming.OnAddDigivolutionCards
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/Tamer/BT10_093_P1.asset, DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088.asset, DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_088_P1.asset, DCGO/Assets/CardBaseEntity/BT17/Yellow/DigiEgg/BT17_003.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - source add primitive payload를 Permanent/CardEffect/CardSources/isFromSameDigimon/isFromDigimon으로 고정한다.
  - face-down/source-bottom add와 inherited source refresh task를 분리한다.
  - source add 후 trigger stack order fixture task를 정의한다.

### WhenLinked

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 87, source files 64
- Source class/method area: Permanent/CardEffectFactory link timing and CanTriggerWhenLinked
- Source call evidence: Permanent.cs:1290, CardEffectFactory.cs:874, CanUseEffects/WhenLinked.cs:45
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1025 - EffectTiming.WhenLinked
- Card data samples: DCGO/Assets/CardBaseEntity/BT21/Green/DigiEgg/BT21_005.asset, DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009.asset, DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_009_P1.asset, DCGO/Assets/CardBaseEntity/BT21/Red/Digimon/BT21_018.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - link source/target permanent 조건을 shared payload로 분리한다.
  - CanTriggerWhenLinked parity fixture와 linked source collection task를 분리한다.
  - link execution completion과 WhenWouldLink/OnLinkCardDiscarded 순서 task를 정의한다.

### OnMove

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 79, source files 30
- Source class/method area: CardObjectController/CardEffectFactory movement event stack
- Source call evidence: CardObjectController.cs:1111, CardEffectFactory.cs:854, CanUseEffects/OnMove.cs:10
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:997 - EffectTiming.OnMove
- Card data samples: DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087.asset, DCGO/Assets/CardBaseEntity/BT11/Purple/Digimon/BT11_087_P0.asset, DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054.asset, DCGO/Assets/CardBaseEntity/BT15/Green/Digimon/BT15_054_P0.asset, DCGO/Assets/CardBaseEntity/BT16/White/Digimon/BT16_082.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Move timing payload and source-zone semantics remain unresolved.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - move event payload에 moved permanent, old/new zone, battle-area survival recheck를 포함한다.
  - CardObjectController/CardEffectFactory CanTriggerOnMove parity fixture task를 분리한다.
  - ZoneMover event bridge와 stale target negative case를 별도 task로 둔다.

### OnUnTappedAnyone

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 70, source files 29
- Source class/method area: CardController unsuspend actual event stack
- Source call evidence: CardController.cs:5694/5754
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1001 - EffectTiming.OnUnTappedAnyone
- Card data samples: DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032.asset, DCGO/Assets/CardBaseEntity/BT11/Blue/Digimon/BT11_032_P1.asset, DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069.asset, DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_069_P0.asset, DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_074.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - WhenUntapAnyone cut-in 뒤 actual unsuspend event를 별도 payload로 고정한다.
  - CanUnsuspend/effect immunity 재검증 후 OnUnTappedAnyone stack task를 정의한다.
  - suspend/unsuspend replay trace payload fixture task를 분리한다.

### OnUseOption

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 65, source files 30
- Source class/method area: CardController option execution boundary
- Source call evidence: CardController.cs:1765/1767
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:972 - EffectTiming.OnUseOption
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032.asset, DCGO/Assets/CardBaseEntity/BT10/Yellow/Digimon/BT10_032_P0.asset, DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044.asset, DCGO/Assets/CardBaseEntity/BT12/Yellow/Digimon/BT12_044_P0.asset, DCGO/Assets/CardBaseEntity/BT17/Yellow/Digimon/BT17_031.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - option card executing zone 이동, Root, Cost, Card payload boundary를 정의한다.
  - OnUseOption/background OnUseOption/OptionSkill 순서 task를 분리한다.
  - executing source cleanup과 interruption/resume negative fixture task를 둔다.

### OnDiscardLibrary

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 51, source files 20
- Source class/method area: CardController deck trash event stack
- Source call evidence: CardController.cs:5816
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:995 - EffectTiming.OnDiscardLibrary
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108.asset, DCGO/Assets/CardBaseEntity/BT10/Purple/Option/BT10_108_P0.asset, DCGO/Assets/CardBaseEntity/BT14/Purple/Digimon/BT14_077.asset, DCGO/Assets/CardBaseEntity/BT19/Purple/Digimon/BT19_071.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - deck trash primitive payload를 DiscardedCards/CardEffect로 고정한다.
  - mill event source collection과 deck exhaustion edge case task를 분리한다.
  - OnDiscardLibrary trigger stack fixture task를 정의한다.

### OnAddSecurity

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 38, source files 14
- Source class/method area: CardController security-add event stack
- Source call evidence: CardController.cs:5489
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:991 - EffectTiming.OnAddSecurity
- Card data samples: DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003.asset, DCGO/Assets/CardBaseEntity/BT14/Yellow/DigiEgg/BT14_003_P1.asset, DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033.asset, DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_033_P1.asset, DCGO/Assets/CardBaseEntity/BT14/Yellow/Digimon/BT14_041.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - security add primitive payload와 face-up/down state를 분리한다.
  - OnAddSecurity와 OnFaceUpSecurityIncreased ordering fixture task를 정의한다.
  - security count/hash/replay payload task를 둔다.

### WhenReturntoLibraryAnyone

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 25, source files 9
- Source class/method area: CardController return-to-library would-remove cut-in
- Source call evidence: CardController.cs:2323/2489
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:981 - EffectTiming.WhenReturntoLibraryAnyone
- Card data samples: DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062.asset, DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_062_P0.asset, DCGO/Assets/CardBaseEntity/BT11/Black/Digimon/BT11_064.asset, DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074.asset, DCGO/Assets/CardBaseEntity/BT20/Purple/Digimon/BT20_074_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Return-to-library cut-in shares unresolved would-remove replacement semantics.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - return-to-library actual mutation 전 cut-in payload와 willBeRemoveField marking task를 분리한다.
  - post-cut-in target refix와 OnLeaveFieldAnyone follow-up task를 별도로 둔다.
  - WhenRemoveField와 shared replacement task dependency를 명시한다.

### OnDiscardSecurity

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 23, source files 14
- Source class/method area: CardController security discard event stack
- Source call evidence: CardController.cs:4377
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:994 - EffectTiming.OnDiscardSecurity
- Card data samples: DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098.asset, DCGO/Assets/CardBaseEntity/BT13/Yellow/Tamer/BT13_098_P1.asset, DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106.asset, DCGO/Assets/CardBaseEntity/BT13/Yellow/Option/BT13_106_P0.asset, DCGO/Assets/CardBaseEntity/BT15/Yellow/Digimon/BT15_037.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - security discard primitive payload를 security card snapshot과 source effect로 고정한다.
  - security trash/recovery flow와 OnLoseSecurity/OnDiscardSecurity ordering task를 분리한다.
  - security discard replay fixture task를 정의한다.

### AfterPayCost

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 15, source files 7
- Source class/method area: CardController post-cost cut-in stack
- Source call evidence: CardController.cs:985
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1016 - EffectTiming.AfterPayCost
- Card data samples: DCGO/Assets/CardBaseEntity/BT1/Green/Option/BT1_109.asset, DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103.asset, DCGO/Assets/CardBaseEntity/BT3/Green/Option/BT3_103_P1.asset, DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109.asset, DCGO/Assets/CardBaseEntity/BT5/White/Option/BT5_109_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: After-pay-cost layer remains a common blocker.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - actual payment 이후 cut-in boundary와 paid cost payload를 정의한다.
  - BeforePayCost와 같은 cost transaction id를 공유하는 task를 분리한다.
  - post-cost trigger order fixture task를 둔다.

### WhenDigisorption

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 15, source files 10
- Source class/method area: BeforePayCost cost-reduction body timing
- Source call evidence: ICardEffect.cs:978 plus CardEffect body samples from inventory
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:978 - EffectTiming.WhenDigisorption
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Green/Digimon/BT10_052.asset, DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_045.asset, DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047.asset, DCGO/Assets/CardBaseEntity/BT2/Green/Digimon/BT2_047_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Digisorption timing remains a documented common-layer blocker.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - BeforePayCost 안에서 선택한 suspend source, reduction amount, cost target을 분리한다.
  - 비용 감소 후 WhenDigisorption trigger stack task를 별도 정의한다.
  - Digisorption special mechanic을 CardEffect body 구현 없이 common cost mechanic prompt로 쪼갠다.

### OnLinkCardDiscarded

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 14, source files 7
- Source class/method area: CardController link source discard event stack
- Source call evidence: CardController.cs:5327
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1029 - EffectTiming.OnLinkCardDiscarded
- Card data samples: DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001.asset, DCGO/Assets/CardBaseEntity/EX10/Green/DigiEgg/EX10_001_P1.asset, DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_030.asset, DCGO/Assets/CardBaseEntity/EX10/Black/Digimon/EX10_030_P1.asset, DCGO/Assets/CardBaseEntity/EX10/Purple/Digimon/EX10_043.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - link source discard payload를 discarded link cards/source permanent로 고정한다.
  - link lifecycle cleanup과 OnLinkCardDiscarded trigger ordering task를 분리한다.
  - linked source removal hash/replay fixture task를 둔다.

### OnRemovedField

- Classification: `ImplementableFoundationTask`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 4, source files 2
- Source class/method area: CardObjectController post-removal event stack
- Source call evidence: CardObjectController.cs:524
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1028 - EffectTiming.OnRemovedField
- Card data samples: DCGO/Assets/CardBaseEntity/BT22/White/DigiEgg/BT22_007.asset, DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010.asset, DCGO/Assets/CardBaseEntity/EX10/Red/Digimon/EX10_010_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - actual field removal 완료 후 payload와 would-remove cut-in과의 관계를 고정한다.
  - CardObjectController OnRemovedField call parity fixture task를 분리한다.
  - OnRemovedField를 WhenRemoveField alias로 처리하지 않는 negative task를 둔다.

### OnFaceUpSecurityIncreased

- Classification: `DataPolicyRequired`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 2, source files 1
- Source class/method area: CardController face-up security state event stack
- Source call evidence: CardController.cs:5506/5548
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1030 - EffectTiming.OnFaceUpSecurityIncreased
- Card data samples: DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004.asset, DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004_P1.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - face-up security state 모델과 count-change event policy를 먼저 확정한다.
  - CardBaseEntity/security card data에서 face-up source identity를 추적하는 data task를 분리한다.
  - OnAddSecurity와 별도 증가 이벤트 ordering fixture를 정의한다.

### WhenWouldLink

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 2, source files 2
- Source class/method area: CardController/CardEffectCommons would-link cut-in
- Source call evidence: CardController.cs:3477, CanUseEffects/WhenWouldLink.cs:11
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1024 - EffectTiming.WhenWouldLink
- Card data samples: DCGO/Assets/CardBaseEntity/BT25/Green/DigiEgg/BT25_004.asset, DCGO/Assets/CardBaseEntity/BT25/Green/Digimon/BT25_045.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - link 실행 직전 candidate card/permanent/root/effect payload를 정의한다.
  - CanTriggerWhenWouldLink parity fixture task를 분리한다.
  - WhenLinked와의 pre/post lifecycle ordering task를 둔다.

### OnUseDigiburst

- Classification: `CardEffectBodySpecific`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 1, source files 1
- Source class/method area: CardController DigiBurst use event stack
- Source call evidence: CardController.cs:2228
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:992 - EffectTiming.OnUseDigiburst
- Card data samples: DCGO/Assets/CardBaseEntity/BT5/Green/Digimon/BT5_056.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: no
- TRUST-001 handoff: yes
- Remediation tasks:
  - DigiBurst source selection/cost/use event를 CardEffect body 구현 없이 mechanic contract로 분리한다.
  - affected source BT5_056과 CardController OnUseDigiburst call만 review fixture 후보로 기록한다.
  - DigiBurst special mechanic partial 상태와 연결되는 후속 prompt로 보낸다.

### WhenUntapAnyone

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 1, source files 1
- Source class/method area: CardController would-untap cut-in
- Source call evidence: CardController.cs:5694
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:983 - EffectTiming.WhenUntapAnyone
- Card data samples: DCGO/Assets/CardBaseEntity/BT7/Green/Digimon/BT7_055.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Untap cut-in pre/post refix semantics remain unresolved.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - actual unsuspend 전 candidate collection, duplicate guard, selection payload를 분리한다.
  - BT7_055형 hand discard 선택과 cannot-unsuspend duration task를 분리한다.
  - post-cut-in CanUnsuspend/effect immunity target refix fixture task를 둔다.

### WhenWouldDigivolutionCardDiscarded

- Classification: `PartialNeedsSubtasks`
- Current capability matrix status: `Unsupported`
- Counts: affected cards 1, source files 1
- Source class/method area: CardController would-source-trash cut-in
- Source call evidence: CardController.cs:5181/5293(commented historical occurrence)
- Source enum evidence: E:/headlessDCGO/DCGO/Assets/Scripts/Script/ICardEffect.cs:1023 - EffectTiming.WhenWouldDigivolutionCardDiscarded
- Card data samples: DCGO/Assets/CardBaseEntity/BT10/Purple/Digimon/BT10_084.asset
- Gate evidence: unsupportedCapabilityCount=26, registry status=Unsupported
- Capability reason: Conservative audit status derived from inventory and local evidence.
- FND-001 handoff: yes
- TRUST-001 handoff: yes
- Remediation tasks:
  - source trash actual mutation 전 would-discard cut-in payload를 정의한다.
  - replacement 처리 후 actual discarded source snapshot task를 분리한다.
  - OnDigivolutionCardDiscarded와 ordering/replay fixture task를 둔다.

## Next Goal Candidate Range

- FND-001: ContinuousOrStaticEffect/source collection dependency가 걸린 25개 항목을 partial closure 범위에 연결한다.
- TRUST-001: 26개 항목의 registry `implementationEvidence`와 실제 `src` 구현/stub 상태를 Source of Truth와 대조한다.
- 이후 FND remediation: ImplementableFoundationTask 15개부터 작은 foundation prompt로 쪼개되, 구현은 별도 goal에서만 수행한다.

## Non-Goals Confirmed

- `src/` C# 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.
- FND-001/TRUST-001 수행 없음.
- Foundation Gate 수치 조작 없음.
- generated status `Implemented`/`Verified` 승격 없음.
- commit/push 없음.
