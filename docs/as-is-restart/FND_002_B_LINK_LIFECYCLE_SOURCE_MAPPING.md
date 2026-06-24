# FND-002-B Link Lifecycle Source Mapping

이 문서는 FND-002-A 이후 남은 unknown common API 중 link lifecycle timing 3개를 원본 source에 맞춰 분해한 evidence이다. 구현은 수행하지 않았다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO`
- Source of Truth: `E:/headlessDCGO/DCGO/Assets`
- Target timings: `WhenWouldLink`, `WhenLinked`, `OnLinkCardDiscarded`
- Implementation performed: no
- Generated status promoted: no

## Count Audit

- Target count: 3
- Current status counts: {'Unsupported': 3}
- Moved from NeedsSourceReview to Unsupported: 3
- Foundation Gate unknownCommonApiCount: 4
- Foundation Gate OpenCodeReady: false

## Mapping Table

| API | Previous | Current | Source classification | Affected | Source files | Payload keys | Next task |
| --- | --- | --- | --- | --- | --- | --- | --- |
| WhenWouldLink | Unsupported | Unsupported | PreLinkCutIn | 2 | 3 | Card, Root, CardEffect, Permanent | FND-002-B1 |
| WhenLinked | Unsupported | Unsupported | PostLinkTrigger | 87 | 67 | Permanent, CardEffect, Card, isFromDigimon | FND-002-B2 |
| OnLinkCardDiscarded | Unsupported | Unsupported | LinkedCardDiscardEvent | 14 | 8 | CardEffect, Permanent, DiscardedCards | FND-002-B3 |

## Source Evidence

| API | Source meaning | Missing headless boundary | Source files |
| --- | --- | --- | --- |
| WhenWouldLink | link action pre-cut-in before link cost payment and AddLinkCard/IPlacePermanentToLinkCards continuation | pre-link cut-in queue, payload preservation, root-aware continuation, and post-cut-in target revalidation | DCGO/Assets/Scripts/Script/ICardEffect.cs:1024<br>DCGO/Assets/Scripts/Script/CardController.cs:3439<br>DCGO/Assets/Scripts/Script/CardController.cs:3456<br>DCGO/Assets/Scripts/Script/CardController.cs:3476<br>DCGO/Assets/Scripts/Script/CardController.cs:3477<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:198<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenWouldLink.cs:11 |
| WhenLinked | post-link trigger emitted after a card is inserted into LinkedCards and LinkedDP/cardSources are updated | post-link rule event queue, linked-source role snapshot, isFromDigimon payload, and WhenLinking factory wrapper parity | DCGO/Assets/Scripts/Script/ICardEffect.cs:1025<br>DCGO/Assets/Scripts/Script/Permanent.cs:1237<br>DCGO/Assets/Scripts/Script/Permanent.cs:1263<br>DCGO/Assets/Scripts/Script/Permanent.cs:1285<br>DCGO/Assets/Scripts/Script/Permanent.cs:1290<br>DCGO/Assets/Scripts/Script/CardEffectFactory.cs:874<br>DCGO/Assets/Scripts/Script/CardEffectFactory.cs:1111<br>DCGO/Assets/Scripts/Script/CardEffectFactory.cs:1131<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenLinked.cs:8<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenLinked.cs:45 |
| OnLinkCardDiscarded | linked-card discard event emitted for fixed linked cards before RemoveLinkedCard actually removes/trashes them | linked-card discard primitive, willBeRemoveSources equivalent, OnLinkCardDiscarded trigger queue, and Ace overflow ordering | DCGO/Assets/Scripts/Script/ICardEffect.cs:1029<br>DCGO/Assets/Scripts/Script/CardController.cs:5242<br>DCGO/Assets/Scripts/Script/CardController.cs:5263<br>DCGO/Assets/Scripts/Script/CardController.cs:5310<br>DCGO/Assets/Scripts/Script/CardController.cs:5322<br>DCGO/Assets/Scripts/Script/CardController.cs:5327<br>DCGO/Assets/Scripts/Script/CardController.cs:5339<br>DCGO/Assets/Scripts/Script/CardEffectCommons.cs:567 |

## Next Foundation Tasks

| Task | API | Boundary |
| --- | --- | --- |
| FND-002-B1 | WhenWouldLink | pre-link cut-in queue, payload preservation, root-aware continuation, and post-cut-in target revalidation |
| FND-002-B2 | WhenLinked | post-link rule event queue, linked-source role snapshot, isFromDigimon payload, and WhenLinking factory wrapper parity |
| FND-002-B3 | OnLinkCardDiscarded | linked-card discard primitive, willBeRemoveSources equivalent, OnLinkCardDiscarded trigger queue, and Ace overflow ordering |

## Remaining FND-002 Range After FND-002-B

- `FND-002-C`: digisorption/digiburst timing policy (`WhenDigisorption`, `OnUseDigiburst`)
- `FND-002-D`: face-up security data policy (`OnFaceUpSecurityIncreased`)
- `FND-002-E`: `OnStartBattle` manual review

## Guardrails

- `src/DCGO.RL.Engine` implementation code was not modified.
- Original `DCGO/Assets` was not modified.
- No individual `CardEffect` body was implemented.
- No C0039+ card-porting was run.
- No RL component was implemented.
- Generated card status was not promoted.
- Foundation Gate values were recalculated from generated inventory, not edited by hand.
- No commit/push was performed.
