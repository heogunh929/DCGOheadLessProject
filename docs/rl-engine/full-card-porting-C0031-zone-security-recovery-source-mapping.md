# Full Card Porting C0031 Zone Security Recovery Source Mapping

Queue status: blocked.

`C0031_zone_security_recovery`는 DCGO 원본 source body 10개와 asset identity 29개를 확인했다. 이번 batch는 실제 effect body 일부를 이식해 baseline blocker를 줄였지만, reveal 후 deck-bottom 임의 정렬, security-stack selection/reveal/shuffle, suspend-cost/source-trash replacement, breeding hatch/move continuation 같은 공통 layer가 아직 남아 있으므로 완료가 아니라 `blocked`로 분류한다.

## Source Mapping

| SourceEffectClassName | DCGO source | RL status | Mapping |
| --- | --- | --- | --- |
| `BT1_062` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_062.cs` | `Implemented` | 원본은 `OnEnterFieldAnyone`에서 `CanTriggerWhenDigivolving`으로 `[When Digivolving]`을 판정한다. RL은 `WhenDigivolving` descriptor로 분리하고, 상대 battle-area Digimon 1체를 선택해 DP -8000 until turn end를 duration modifier로 적용한다. |
| `BT1_063` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_063.cs` | `PartiallyImplemented` | `[When Digivolving] Recovery +1 (Deck)`와 field-top owner-turn security 3장 이상 조건의 SecurityAttack +1 continuous modifier를 구현했다. 원본 `CanAddSecurity` replacement/restriction은 아직 recovery/security-add common layer blocker다. |
| `BT1_067` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_067.cs` | `Unsupported` | `[On Play]` reveal top 3, level 4 Digimon 1장 hand, 나머지 deck bottom any order. reveal selection plus source-aligned deck-bottom ordering continuation이 필요하므로 추측 구현하지 않았다. |
| `BT1_070` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_070.cs` | `PartiallyImplemented` | `[On Play]` 상대 battle-area Digimon 1체 선택 후 suspend primitive를 호출한다. 원본 `SuspendPermanentsClass.Tap`의 replacement/restriction 및 `OnTappedAnyone` timing coverage는 아직 suspend common layer blocker다. |
| `BT1_074` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_074.cs` | `Unsupported` | `[When Digivolving]` reveal top 3, level 5 이상 Digimon 1장 hand, 나머지 deck bottom any order. reveal selection plus source-aligned deck-bottom ordering continuation이 필요하므로 추측 구현하지 않았다. |
| `BT1_085` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_085.cs` | `PartiallyImplemented` | `SetMemoryTo3TamerEffect`, owner-turn red Digimon 4+ source SecurityAttack +1 continuous modifier, `PlaySelfTamerSecurityEffect`를 구현했다. 원본 `CanAddMemory` replacement/restriction은 memory restriction common layer blocker다. |
| `BT1_086` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_086.cs` | `PartiallyImplemented` | `SetMemoryTo3TamerEffect`와 `PlaySelfTamerSecurityEffect`를 구현했다. owner-turn blue Digimon play trigger는 global `OnEnterFieldAnyone` payload, optional suspend-cost lifecycle, bottom digivolution-source trash replacement/continuation이 필요하므로 남겼다. |
| `BT1_087` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_087.cs` | `PartiallyImplemented` | `SetMemoryTo3TamerEffect`와 `PlaySelfTamerSecurityEffect`를 구현했다. `[On Play]` security stack 확인, reveal/add hand, yellow conditional Recovery, security shuffle은 security-stack selection/reveal/shuffle continuation이 필요하므로 남겼다. |
| `BT1_088` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_088.cs` | `PartiallyImplemented` | `PlaySelfTamerSecurityEffect`를 구현했다. `[Main]` suspend-cost reveal top card, Digimon이면 hand, 아니면 deck bottom은 suspend-cost lifecycle과 reveal/deck-bottom continuation이 필요하므로 남겼다. |
| `BT1_089` | `DCGO/Assets/Scripts/CardEffect/BT1/Green/BT1_089.cs` | `PartiallyImplemented` | `SetMemoryTo3TamerEffect`와 `PlaySelfTamerSecurityEffect`를 구현했다. `[Main]` suspend-cost hatch or move from breeding은 decision-capable breeding action continuation이 필요하므로 남겼다. |

## Implemented Reduction

- Added C0031 BT1 card scripts under `src/DCGO.RL.Engine/CardEffects/BT1/**` with per-source files.
- Added `Bt1CardScriptCatalog.CreateC0031Scripts()` and registry coverage.
- Implemented runnable body for `BT1_062`.
- Implemented partial bodies for `BT1_063`, `BT1_070`, `BT1_085`, `BT1_086`, `BT1_087`, `BT1_088`, and `BT1_089`.
- Kept `BT1_067` and `BT1_074` explicit `Unsupported` because reveal/search deck-bottom ordering is not available.
- Added focused regression tests for DP -8000, Recovery/SecurityAttack, suspend, set-memory-to-3, Tamer security play, and BT1-085 continuous SecurityAttack.

## Blockers

- Reveal/search effects that place remaining cards on deck bottom in any order need source-aligned ordering continuation.
- `BT1_087` needs security-stack selection/reveal/shuffle and conditional recovery continuation.
- Suspend effects still need full replacement/restriction and `OnTappedAnyone` event coverage.
- Digivolution-source trash from bottom still needs source-trash protection/replacement and event continuation.
- `CanAddSecurity` and `CanAddMemory` replacement/restriction layers are not implemented.
- `BT1_089` needs a decision-capable hatch-or-move breeding action continuation.

No CardId branch was added to core services, Catalog, or validators. Card effect bodies remain in per-source card files, and DCGO Unity source remains read-only.
