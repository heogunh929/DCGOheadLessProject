# FND-002-D Face-Up Security Data Policy

이 문서는 `OnFaceUpSecurityIncreased`를 원본 근거와 현재 headless 데이터 정책 기준으로 다시 분류한 evidence 산출물이다. 구현은 수행하지 않았다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO/Assets`
- Target timing: `OnFaceUpSecurityIncreased`
- Implementation performed: no
- Generated status promoted: no

## Count Audit

- Target count: 1
- Current status counts: {'PartiallyImplemented': 1}
- Classification counts: {'SourceMappedDataPolicyPartial': 1}
- Moved from NeedsSourceReview to PartiallyImplemented: 1
- Foundation Gate unknownCommonApiCount: 1
- Foundation Gate OpenCodeReady: false

## Mapping Table

| API | Original | Current rerun | Current | Capability | Classification |
| --- | --- | --- | --- | --- | --- |
| OnFaceUpSecurityIncreased | NeedsSourceReview | PartiallyImplemented | PartiallyImplemented | PartiallyImplemented | SourceMappedDataPolicyPartial |

## Source Meaning

The source event is raised when a card is added to security already face-up, or when an existing face-down security card is flipped face-up. The source hashtable carries Player and CardSources, and CanTriggerOnFaceUpSecurityIncreases checks player match plus at least one matching face-up security card.

## Policy Decision

Treat the timing as source-known PartiallyImplemented data-policy foundation. Headless AddSecurity already queues OnAddSecurity followed by OnFaceUpSecurityIncreased for face-up additions, but source-aligned IFlipSecurity conversion and EX11-004 full-card parity are not closed.

## Payload Keys

`Player`, `CardSources`, `AddedSecurityCards`, `SecurityCards`, `SecurityCard`, `SourceZone`, `DestinationZone`, `CardEffect`, `SourceCard`, `SourcePermanent`, `ToTop`, `FaceUp`, `MoveReason`

## Source Evidence

- `DCGO/Assets/Scripts/Script/ICardEffect.cs:1030`
- `DCGO/Assets/Scripts/Script/CardController.cs:5494`
- `DCGO/Assets/Scripts/Script/CardController.cs:5506`
- `DCGO/Assets/Scripts/Script/CardController.cs:5516`
- `DCGO/Assets/Scripts/Script/CardController.cs:5529`
- `DCGO/Assets/Scripts/Script/CardController.cs:5532`
- `DCGO/Assets/Scripts/Script/CardController.cs:5548`
- `DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:11`
- `DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:13`
- `DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs:17`
- `DCGO/Assets/Scripts/Script/CardSource.cs:56`
- `DCGO/Assets/Scripts/Script/CardSource.cs:76`
- `DCGO/Assets/Scripts/Script/CardSource.cs:91`
- `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs:15`
- `DCGO/Assets/Scripts/CardEffect/EX11/Black/EX11_004.cs:30`

## Current Headless Evidence

- `src/DCGO.RL.Engine/Effects/EffectTiming.cs:66`
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:705`
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:821`
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:855`
- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs:858`
- `src/DCGO.RL.Engine.Tests/Program.cs:14968`
- `src/DCGO.RL.Engine.Tests/Program.cs:15043`
- `docs/rl-engine/on-add-security-foundation-scope-fnd003e.md`

## Affected Card Data

| Definition | Card | Variant | Asset |
| --- | --- | --- | --- |
| EX11-004#7663@base | EX11-004 | base | DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004.asset |
| EX11-004#7664@P1 | EX11-004 | P1 | DCGO/Assets/CardBaseEntity/EX11/Black/DigiEgg/EX11_004_P1.asset |

## Missing Boundary

source-aligned FlipSecurity primitive/event, explicit face-up security increase fixture for flip-not-add, EX11-004 inherited draw body parity, and Unity/RL full-card replay comparison

## Next Foundation Tasks

| Task | API | Classification | Title |
| --- | --- | --- | --- |
| FND-002-D1 | OnFaceUpSecurityIncreased | CloseableFoundationTask | Define source-aligned FlipSecurity primitive and event payload |
| FND-002-D2 | OnFaceUpSecurityIncreased | NeedsParityFixture | Add source-locked Unity/RL fixture contract for EX11-004 |
| FND-002-E | OnStartBattle | BlockedNeedsManualReview | Manual review of remaining zero-card OnStartBattle timing |

## Remaining FND-002 Range After FND-002-D

- `FND-002-E`: `OnStartBattle` manual review
- `FND-003`: unsupported capability remediation continuation

## Guardrails

- `src/DCGO.RL.Engine` implementation code was not modified.
- Original `DCGO/Assets` was not modified.
- No individual `CardEffect` body was implemented.
- No C0039+ card-porting was run.
- No RL component was implemented.
- Generated card status was not promoted.
- Foundation Gate values were recalculated from generated inventory, not edited by hand.
- No commit/push was performed.
