# FND-002-C Digisorption / DigiBurst Timing Policy

이 문서는 FND-002-B 이후 남은 unknown common API 중 Digisorption/DigiBurst timing 2개를 원본 source에 맞춰 정책 분해한 evidence이다. 구현은 수행하지 않았다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO`
- Source of Truth: `E:/headlessDCGO/DCGO/Assets`
- Target timings: `WhenDigisorption`, `OnUseDigiburst`
- Implementation performed: no
- Generated status promoted: no

## Count Audit

- Target count: 2
- Current status counts: {'Unsupported': 2}
- Moved from NeedsSourceReview to Unsupported: 2
- Foundation Gate unknownCommonApiCount: 2
- Foundation Gate OpenCodeReady: false

## Mapping Table

| API | Original | Current | Source classification | Affected | Source files | Payload keys | Next task |
| --- | --- | --- | --- | --- | --- | --- | --- |
| WhenDigisorption | NeedsSourceReview | Unsupported | DigisorptionCostWindow | 15 | 10 | CardEffect | FND-002-C1 |
| OnUseDigiburst | NeedsSourceReview | Unsupported | DigiBurstLifecycleBoundary | 1 | 2 | Permanent, CardEffect | FND-002-C2 |

## Source Evidence And Policy

| API | Source meaning | Policy decision | Missing headless boundary | Source files |
| --- | --- | --- | --- | --- |
| WhenDigisorption | Digisorption is a BeforePayCost card-effect body pattern that selects a suspend source, installs a temporary cost-reduction provider, then opens a WhenDigisorption cut-in/trigger window for effects that modify which permanent may be suspended. | Do not port individual Digisorption card bodies until a shared cost-window state machine exists. The timing is source-known and should be tracked as a known Unsupported foundation blocker, not unknown. | source-aligned Digisorption cost transaction, selected suspend-source snapshot, reduction amount, UntilCalculateFixedCostEffect equivalent, and post-selection WhenDigisorption trigger queue | DCGO/Assets/Scripts/Script/ICardEffect.cs:978<br>DCGO/Assets/Scripts/Script/CardSource.cs:2640<br>DCGO/Assets/Scripts/Script/CardSource.cs:2641<br>DCGO/Assets/Scripts/Script/CardSource.cs:2645<br>DCGO/Assets/Scripts/Script/Player.cs:1180<br>DCGO/Assets/Scripts/Script/Player.cs:1198<br>DCGO/Assets/Scripts/Script/Player.cs:1204<br>DCGO/Assets/Scripts/Script/Player.cs:1250<br>DCGO/Assets/Scripts/Script/Player.cs:1268<br>DCGO/Assets/Scripts/Script/Player.cs:1274<br>DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs:366<br>DCGO/Assets/Scripts/Script/CardEffectInterfaces.cs:368<br>DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:6<br>DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:8<br>DCGO/Assets/Scripts/Script/CardEffects/CanSuspendByDigisorptionClass.cs:19<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:15<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:18<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:60<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:130<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:136<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:144<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:150<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:165<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:193<br>DCGO/Assets/Scripts/CardEffect/BT2/Green/BT2_045.cs:196<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:279<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:282<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:374<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:377<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:411<br>DCGO/Assets/Scripts/CardEffect/BT3/Green/BT3_056.cs:425 |
| OnUseDigiburst | IDigiBurst selects digivolution cards, emits OnUseDigiburst with Permanent/CardEffect before actual ITrashDigivolutionCards, then self source-trash triggers use the same activating DigiBurst effect as follow-up context. | Treat the common DigiBurst lifecycle as a known Unsupported foundation blocker. Individual DigiBurst card effect bodies remain excluded until the common selection/trash lifecycle exists. | source-aligned DigiBurst primitive, selected source snapshot, OnUseDigiburst trigger ordering before source trash, and self-source-trash follow-up payload | DCGO/Assets/Scripts/Script/ICardEffect.cs:992<br>DCGO/Assets/Scripts/Script/CardSource.cs:2544<br>DCGO/Assets/Scripts/Script/CardSource.cs:2552<br>DCGO/Assets/Scripts/Script/CardController.cs:2114<br>DCGO/Assets/Scripts/Script/CardController.cs:2135<br>DCGO/Assets/Scripts/Script/CardController.cs:2163<br>DCGO/Assets/Scripts/Script/CardController.cs:2173<br>DCGO/Assets/Scripts/Script/CardController.cs:2220<br>DCGO/Assets/Scripts/Script/CardController.cs:2222<br>DCGO/Assets/Scripts/Script/CardController.cs:2223<br>DCGO/Assets/Scripts/Script/CardController.cs:2228<br>DCGO/Assets/Scripts/Script/CardController.cs:2233<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:11<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:13<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/WhenUseDigiBurst.cs:21<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:10<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:18<br>DCGO/Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs:24<br>DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs:59<br>DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_056.cs:89<br>DCGO/Assets/Scripts/CardEffect/BT5/Green/BT5_004.cs:37 |

## Next Foundation Tasks

| Task | API | Boundary |
| --- | --- | --- |
| FND-002-C1 | WhenDigisorption | source-aligned Digisorption cost transaction, selected suspend-source snapshot, reduction amount, UntilCalculateFixedCostEffect equivalent, and post-selection WhenDigisorption trigger queue |
| FND-002-C2 | OnUseDigiburst | source-aligned DigiBurst primitive, selected source snapshot, OnUseDigiburst trigger ordering before source trash, and self-source-trash follow-up payload |

## Remaining FND-002 Range After FND-002-C

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
