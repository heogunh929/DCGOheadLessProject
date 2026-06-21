# Full Card Porting C0037 Zone Security Recovery Source Mapping

## 결정

`C0037_zone_security_recovery`는 `blocked`로 처리한다.

이번 batch는 BT22 source body 10개를 직접 확인했다. `BT22-046`의 inherited `ChangeSelfDPStaticEffect(+1000)`만 현재 continuous modifier layer로 source-aligned하게 이식했다. 나머지는 trait metadata, alternative digivolution requirement, Decode, token identity/body, hand-to-source placement, top-source reorder, temporary keyword/duration, nested WhenDigivolving activation, bottom-deck movement, and battle-deletion payload 같은 공통 layer가 없으면 카드별 workaround가 되므로 silent no-op이나 core CardId branch로 처리하지 않는다.

Queue status: blocked

Source evidence paths:

- `DCGO/Assets/Scripts/CardEffect/BT22/Black/BT22_005.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Purple/BT22_006.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_018.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_021.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Blue/BT22_027.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Yellow/BT22_040.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_043.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_044.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_046.cs`
- `DCGO/Assets/Scripts/CardEffect/BT22/Green/BT22_047.cs`
- No CardId branch.

## Variant Identity

generated manifest와 queue mapping은 `CardId#CardIndex@Variant` identity를 유지한다. `BT22-018`, `BT22-043`, `BT22-044`, `BT22-046`은 복수 asset variant를 가진다. 특히 `BT22-046#7046@base`와 `BT22-046#8432@P1`은 같은 card script/registry entry를 공유하지만, source mapping과 batch test에서는 variant-aware identity를 별도로 검증한다. Catalog는 registry 역할만 하며 variant를 `CardId`만으로 완료 판정하지 않는다.

## Source Mapping

| Source effect | RL.Engine status | 구현/차단 범위 |
| --- | --- | --- |
| `BT22_005` | `Unsupported` | inherited owner-turn once-per-turn draw when an owner Unidentified/CS Digimon is played requires trait metadata and OnEnterFieldAnyone permanent-play payload coverage. |
| `BT22_006` | `Unsupported` | inherited owner-turn once-per-turn draw 1 then trash 1 when effects move this Digimon's top stacked card to bottom source requires source-reorder payload coverage and hand-discard selection continuation. |
| `BT22_018` | `Unsupported` | OnPlay place this Digimon under another owner Aqua/Sea Animal Digimon, grant Blocker and battle-deletion immunity until opponent turn end, plus inherited Jamming require trait metadata, permanent-to-source placement, temporary keyword grant, and battle-immunity duration layers. |
| `BT22_021` | `Unsupported` | Decode, OnPlay/WhenDigivolving optional level 5-or-lower Aqua/Sea Animal hand card placement under an owner Digimon, and inherited Jamming require Decode, trait metadata, two-step optional hand/permanent selection, and hand-to-source placement support. |
| `BT22_027` | `Unsupported` | Decode, OnPlay/WhenDigivolving hand-to-source cost followed by can't-suspend duration, and All Turns once-per-turn bottom-deck level 5-or-lower opponent Digimon require Decode, trait metadata, hand-to-source cost continuation, can't-suspend duration, and bottom-deck movement support. |
| `BT22_040` | `Unsupported` | Overclock, OnPlay/WhenDigivolving Familiar Token play, and All Turns once-per-turn activation of one WhenDigivolving effect after another owner Digimon is deleted require Overclock, token identity/body, deleted-other payload, and nested effect-selection execution support. |
| `BT22_043` | `Unsupported` | Alternative CS level-2 digivolution, owner-turn once-per-turn CS Tamer play on source add, and Main inherited top-card-to-bottom then draw require trait metadata, alternative digivolution requirements, source-add card-condition payload, optional hand play, and top-source reorder support. |
| `BT22_044` | `Unsupported` | Alternative CS level-2 digivolution, owner-turn once-per-turn memory +1 on CS source add, and Main inherited top-card-to-bottom then draw require trait metadata, alternative digivolution requirements, source-add card-condition payload, and top-source reorder support. |
| `BT22_046` | `PartiallyImplemented` | inherited All Turns DP +1000 is implemented through `ContinuousEffectDescriptor`, matching source `ChangeSelfDPStaticEffect`. Alternative CS level-3 digivolution and WhenDigivolving optional CS Tamer hand play remain blocked by CS trait metadata, alternative digivolution requirement, optional hand play continuation, and hand-play ETB continuation. |
| `BT22_047` | `Unsupported` | Alternative CS level-3 digivolution, OnPlay/WhenDigivolving suspend then conditional can't-unsuspend, and inherited battle-deletion memory +1 require CS trait metadata, alternative digivolution requirements, same-level stack predicate, can't-unsuspend duration, and battle-deletion OnEndBattle payload coverage. |

## 구현 파일

- `src/DCGO.RL.Engine/CardEffects/BT22/Black/BT22_005.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Purple/BT22_006.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_018.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_021.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Blue/BT22_027.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Yellow/BT22_040.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_043.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_044.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_046.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Green/BT22_047.cs`
- `src/DCGO.RL.Engine/CardEffects/BT22/Bt22CardScriptCatalog.cs`

## 검증

추가 테스트:

- `FullCardPortingBatch C0037 zone recovery partial implementation`
- `BT22-046 C0037 inherited DP continuous modifier`

전체 regression 결과는 작업 종료 보고에 기록한다.

## 남은 범위

C0037은 L0006 boundary 이후 안전한 subset만 구현했다. 남은 효과는 source body가 확인되었지만 공통 layer가 부족한 `blocked` 상태다. blocker 문서화만으로 card-porting batch를 `done` 처리하지 않는다.
