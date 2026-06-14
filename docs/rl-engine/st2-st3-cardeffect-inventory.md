# ST2-ST3 CardEffect Inventory Snapshot

Updated: 2026-06-14

This file is a compact companion inventory for `cardeffect-porting-status-st1-st3.md`. It records the source facts used to create the ST1-ST3 pass plan. It is not an implementation-completion report.

## Counts

| Set | Cards | Empty `CardEffectClassName` | Non-empty `CardEffectClassName` | Set-local CardEffect files | Shared CardEffect class references |
| --- | ---: | ---: | ---: | ---: | --- |
| ST2 | 16 | 4 | 12 | 11 | ST2-07 uses `ST1_06` |
| ST3 | 16 | 4 | 12 | 11 | ST3-07 uses `ST1_06` |

## ST2 Source Inventory

| CardId | Name | Effect class | Source file | Planning classification |
| --- | --- | --- | --- | --- |
| ST2-01 | Tsunomon | `ST2_01` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_01.cs` | NeedsCommonLayer |
| ST2-02 | Gomamon | none | none | NoEffect |
| ST2-03 | Gabumon | `ST2_03` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_03.cs` | NeedsCommonLayer |
| ST2-04 | Bearmon | none | none | NoEffect |
| ST2-05 | Ikkakumon | none | none | NoEffect |
| ST2-06 | Garurumon | `ST2_06` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_06.cs` | NeedsCommonLayer |
| ST2-07 | Grizzlymon | `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | ImplementableNow |
| ST2-08 | WereGarurumon | `ST2_08` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_08.cs` | NeedsCommonLayer |
| ST2-09 | Zudomon | `ST2_09` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_09.cs` | NeedsCommonLayer |
| ST2-10 | Plesiomon | none | none | NoEffect |
| ST2-11 | MetalGarurumon | `ST2_11` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_11.cs` | NeedsCommonLayer |
| ST2-12 | Matt Ishida | `ST2_12` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_12.cs` | NeedsCommonLayer |
| ST2-13 | Hammer Spark | `ST2_13` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_13.cs` | ImplementableNow |
| ST2-14 | Sorrow Blue | `ST2_14` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_14.cs` | NeedsCommonLayer |
| ST2-15 | Kaiser Nail | `ST2_15` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_15.cs` | NeedsComplexMechanic |
| ST2-16 | Cocytus Breath | `ST2_16` | `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_16.cs` | NeedsCommonLayer |

## ST3 Source Inventory

| CardId | Name | Effect class | Source file | Planning classification |
| --- | --- | --- | --- | --- |
| ST3-01 | Tokomon | `ST3_01` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_01.cs` | NeedsComplexMechanic |
| ST3-02 | Salamon | none | none | NoEffect |
| ST3-03 | Tapirmon | none | none | NoEffect |
| ST3-04 | Patamon | `ST3_04` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_04.cs` | NeedsComplexMechanic |
| ST3-05 | Angemon | `ST3_05` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_05.cs` | ImplementableNow |
| ST3-06 | Gatomon | none | none | NoEffect |
| ST3-07 | Unimon | `ST1_06` | `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs` | ImplementableNow |
| ST3-08 | MagnaAngemon | `ST3_08` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_08.cs` | ImplementableNow |
| ST3-09 | Angewomon | `ST3_09` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_09.cs` | NeedsCommonLayer |
| ST3-10 | Magnadramon | none | none | NoEffect |
| ST3-11 | Seraphimon | `ST3_11` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_11.cs` | ImplementableNow |
| ST3-12 | T.K. Takaishi | `ST3_12` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_12.cs` | NeedsCommonLayer |
| ST3-13 | Heaven's Gate | `ST3_13` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_13.cs` | NeedsCommonLayer |
| ST3-14 | Heaven's Charm | `ST3_14` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_14.cs` | NeedsCommonLayer |
| ST3-15 | Holy Flame | `ST3_15` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_15.cs` | NeedsCommonLayer |
| ST3-16 | Seven Heavens | `ST3_16` | `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_16.cs` | ImplementableNow |

## Immediate Candidates

The immediate candidate count is 7:

- ST2-07, ST2-13
- ST3-05, ST3-07, ST3-08, ST3-11, ST3-16

These still require tests and explicit catalog/validator registration in a future implementation pass. This inventory does not implement them.
