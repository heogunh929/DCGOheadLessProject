# FND-002-A Replacement/Cut-In Source Mapping

이번 문서는 FND-002-RERUN 이후 남은 unknown common API 중 replacement/cut-in timing 5개를 원본 source에 맞춰 분해한다. 구현은 수행하지 않는다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO`
- Source of Truth: `E:/headlessDCGO/DCGO/Assets`
- Target timings: `WhenPermanentWouldBeDeleted`, `WhenRemoveField`, `WhenReturntoLibraryAnyone`, `WhenUntapAnyone`, `WhenWouldDigivolutionCardDiscarded`
- Implementation performed: no
- Generated status promoted: no

## Count Audit

- Target count: 5
- Current status counts: {'Unsupported': 5}
- Moved from NeedsSourceReview to Unsupported: 5
- Foundation Gate unknownCommonApiCount: 7
- Foundation Gate OpenCodeReady: false

## Mapping Table

| API | Previous | Current | Source classification | Affected | Source files | Payload keys | Next task |
| --- | --- | --- | --- | --- | --- | --- | --- |
| WhenPermanentWouldBeDeleted | Unsupported | Unsupported | ReplacementDeletePrevention | 405 | 210 | Permanents, battle, CardEffect | FND-002-A1 |
| WhenRemoveField | Unsupported | Unsupported | WouldRemoveFieldCutIn | 304 | 167 | Permanents, battle, CardEffect, digixros | FND-002-A2 |
| WhenReturntoLibraryAnyone | Unsupported | Unsupported | WouldReturnToLibraryCutIn | 25 | 10 | Permanents, CardEffect, battle, digixros | FND-002-A3 |
| WhenUntapAnyone | Unsupported | Unsupported | WouldUnsuspendCutIn | 1 | 2 | Permanents, CardEffect | FND-002-A4 |
| WhenWouldDigivolutionCardDiscarded | Unsupported | Unsupported | WouldSourceTrashCutIn | 1 | 2 | Permanent, DiscardedCards, CardEffect | FND-002-A5 |

## Source Evidence

| API | Source meaning | Missing headless boundary | Source files |
| --- | --- | --- | --- |
| WhenPermanentWouldBeDeleted | deletion/destruction replacement window for Evade, Barrier, Partition, Scapegoat, and related prevention effects | delete-prevention replacement queue before actual field removal | DCGO/Assets/Scripts/Script/ICardEffect.cs:980<br>DCGO/Assets/Scripts/Script/Permanent.cs:2898<br>DCGO/Assets/Scripts/Script/Permanent.cs:2954<br>DCGO/Assets/Scripts/Script/Permanent.cs:3117<br>DCGO/Assets/Scripts/Script/Permanent.cs:3138<br>DCGO/Assets/Scripts/Script/CardController.cs:3696<br>DCGO/Assets/Scripts/Script/CardEffectCommons/KeyWordEffects/Barrier.cs:99<br>DCGO/Assets/Scripts/Script/CardEffectCommons/KeyWordEffects/Evade.cs:77 |
| WhenRemoveField | generic would-leave-battle-area cut-in before bounce, placement under cards, link placement, and removal paths | willBeRemoveField target marking, cut-in ordering, target re-fix, and actual movement continuation | DCGO/Assets/Scripts/Script/ICardEffect.cs:979<br>DCGO/Assets/Scripts/Script/CardController.cs:2333<br>DCGO/Assets/Scripts/Script/CardController.cs:2499<br>DCGO/Assets/Scripts/Script/CardController.cs:2932<br>DCGO/Assets/Scripts/Script/CardController.cs:2948<br>DCGO/Assets/Scripts/Script/CardController.cs:3228<br>DCGO/Assets/Scripts/Script/CardController.cs:3244<br>DCGO/Assets/Scripts/Script/CardController.cs:3538<br>DCGO/Assets/Scripts/Script/CardController.cs:3705<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:70<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenRemoveField.cs:11<br>DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/ArmorPurge.cs:21<br>DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Decode.cs:54<br>DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Fragment.cs:52<br>DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/MaterialSave.cs:47 |
| WhenReturntoLibraryAnyone | specific deck-return replacement window that precedes the generic WhenRemoveField window | return-to-library pre-move cut-in before deck bottom/top mutation | DCGO/Assets/Scripts/Script/ICardEffect.cs:981<br>DCGO/Assets/Scripts/Script/CardController.cs:2323<br>DCGO/Assets/Scripts/Script/CardController.cs:2489<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:70 |
| WhenUntapAnyone | would-unsuspend cut-in before actual IsSuspended=false mutation and OnUnTappedAnyone trigger | pre-unsuspend replacement/selection window and target re-fix before primitive unsuspend | DCGO/Assets/Scripts/Script/ICardEffect.cs:983<br>DCGO/Assets/Scripts/Script/CardController.cs:5694 |
| WhenWouldDigivolutionCardDiscarded | would-trash-digivolution-cards cut-in before source cards are removed and OnDigivolutionCardDiscarded fires | source-card willBeRemoveSources marking, cut-in processing, and post-cut-in fixed source trash continuation | DCGO/Assets/Scripts/Script/ICardEffect.cs:1023<br>DCGO/Assets/Scripts/Script/CardController.cs:5181<br>DCGO/Assets/Scripts/Script/CardController.cs:5293<br>DCGO/Assets/Scripts/Script/CardEffectCommons/HashtableSetting.cs:321 |

## Next Foundation Tasks

| Task | API | Boundary |
| --- | --- | --- |
| FND-002-A1 | WhenPermanentWouldBeDeleted | delete-prevention replacement queue before actual field removal |
| FND-002-A2 | WhenRemoveField | willBeRemoveField target marking, cut-in ordering, target re-fix, and actual movement continuation |
| FND-002-A3 | WhenReturntoLibraryAnyone | return-to-library pre-move cut-in before deck bottom/top mutation |
| FND-002-A4 | WhenUntapAnyone | pre-unsuspend replacement/selection window and target re-fix before primitive unsuspend |
| FND-002-A5 | WhenWouldDigivolutionCardDiscarded | source-card willBeRemoveSources marking, cut-in processing, and post-cut-in fixed source trash continuation |

## Remaining FND-002 Range After FND-002-A

- `FND-002-B`: link lifecycle source mapping (`WhenLinked`, `WhenWouldLink`, `OnLinkCardDiscarded`)
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
