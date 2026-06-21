# Full Card Porting C0030 Zone Security Recovery Source Mapping

Queue status: blocked.

`C0030_zone_security_recovery`는 DCGO 원본 source body 10개와 asset identity 24개를 확인했다. 이 batch는 `L0006_zone_security_recovery` 위에서 일부 실제 effect body를 구현해 baseline blocker를 줄였지만, 전체 카드 포팅 완료 조건을 만족하지 않으므로 `blocked`로 유지한다.

## Source Mapping

| SourceEffectClassName | DCGO source | RL status | Mapping |
| --- | --- | --- | --- |
| `BT1_025` | `DCGO/Assets/Scripts/CardEffect/BT1/Red/BT1_025.cs` | `PartiallyImplemented` | `[When Digivolving]` SecurityAttack +1 until turn end is implemented through `WhenDigivolving` and a temporary SecurityAttack modifier. The `None` static owner-turn security option invalidation while attacking requires security-effect cancellation/immunity and remains blocked. |
| `BT1_029` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_029.cs` | `Implemented` | Self `[On Play] Draw 1` is implemented through the draw primitive. L0006 preserves `OnAddHand`/`OnDraw` pending events. |
| `BT1_036` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_036.cs` | `Implemented` | Self `[On Play]` selects 1 owner battle-area Digimon and runs the common unsuspend primitive. The original target predicate is owner battle-area Digimon; it is not narrowed to suspended-only in the card file. |
| `BT1_043` | `DCGO/Assets/Scripts/CardEffect/BT1/Blue/BT1_043.cs` | `Unsupported` | `[When Digivolving]` selects an opponent Digimon and trashes up to 4 digivolution cards from bottom. This requires source-trash ordering, source-trash protection/replacement, and digivolution-card discard continuation. |
| `BT1_048` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_048.cs` | `Unsupported` | `[On Play]` reveal top 4, add all yellow Tamers, and put the rest on deck bottom in any order. This needs reveal/search all-match handling and deck-bottom ordering continuation. |
| `BT1_049` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_049.cs` | `Implemented` | Inherited owner-turn `OnDestroyedAnyone` draw 1 when an opponent Digimon is deleted by DP 0 is implemented from RuleProcessor destruction payload. |
| `BT1_053` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_053.cs` | `Unsupported` | Owner-turn trigger when an owner level 3 yellow Digimon is played while this Digimon is suspended requires the source-aligned `OnEnterFieldAnyone` global enter payload. This is not flattened to this card's own `OnPlay`. |
| `BT1_055` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_055.cs` | `Implemented` | Self `[On Play]` selects 1 opponent Digimon and applies DP -3000 until turn end. |
| `BT1_060` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_060.cs` | `PartiallyImplemented` | Self `[On Play] Recovery +1 (Deck)` is implemented through recovery from deck. Inherited owner-turn DP +1000 per 3 owner security is implemented as a continuous inherited modifier. `CanAddSecurity` replacement/restriction remains blocked by recovery/security-add common layer. |
| `BT1_061` | `DCGO/Assets/Scripts/CardEffect/BT1/Yellow/BT1_061.cs` | `Implemented` | Self `[On Play]` selects exactly `min(2, candidate count)` opponent Digimon and applies DP -3000 until turn end to each selected target. |

## Implemented reduction

- Added BT1 C0030 card scripts under `src/DCGO.RL.Engine/CardEffects/BT1/**` with per-source files.
- Added `Bt1CardScriptCatalog.CreateC0030Scripts()` and registry coverage.
- Implemented runnable bodies for `BT1_029`, `BT1_036`, `BT1_049`, `BT1_055`, and `BT1_061`.
- Implemented partial bodies for `BT1_025` and `BT1_060`.
- Added focused tests for SecurityAttack, draw, unsuspend, inherited DP-zero draw, DP reduction, recovery, and inherited DP.

## Blockers

- `BT1_025` still needs security-option effect invalidation/cancellation while the source Digimon attacks.
- `BT1_043` needs source-aligned digivolution-source trash from bottom, protection/replacement, and event continuation.
- `BT1_048` needs reveal/search all-match handling and deck-bottom ordering.
- `BT1_053` needs the OnEnterFieldAnyone global enter payload for another permanent entering play.
- `BT1_060` still needs `CanAddSecurity` replacement/restriction support around recovery/security-add.

No CardId branch was added to core services, Catalog, or validators. Card effect bodies remain in per-source card files, and DCGO Unity source remains read-only.
