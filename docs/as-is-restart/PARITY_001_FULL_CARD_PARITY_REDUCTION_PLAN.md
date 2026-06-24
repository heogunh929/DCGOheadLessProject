# PARITY-001 Full-Card Parity Reduction Plan

이 문서는 full-card parity evidence의 `NotRun 3918` 상태를 실제 Unity/RL fixture 근거로 줄이기 위한 source-locked 작업 기준선이다.
이번 산출물은 구현이나 fixture 생성을 수행하지 않으며, `NotRun`을 `Passed`로 간주하지 않는다.

## AS-IS Root

- Source of Truth: `E:\headlessDCGO\DCGO\Assets`
- Full-card source effects: 3918
- Affected definitions: 7922

## Current Parity State

- Passed: 0
- Failed: 0
- NotRun: 3918
- Compared: 0
- Candidate queue window: 100 / 100
- First executable reduction target count in this plan: 0
- OpenCodeReady: `false`

## Policy

- Implementation performed: `false`
- DCGO original modified: `false`
- CardEffect body implemented: `false`
- C0039 or later card-porting run: `false`
- Generated status promoted: `false`
- Foundation Gate manipulated: `false`
- NotRun counts as pass: `false`
- Synthetic fixture counts as Unity parity: `false`

## Required Artifact Contract

각 source effect가 `NotRun`에서 벗어나려면 다음 세 파일이 모두 source lock과 동일한 `scenarioId`로 존재해야 한다.

| Artifact | Path Pattern | Producer | Missing Status |
| --- | --- | --- | --- |
| UnityFixture | `docs/generated/parity-fixtures/unity/full-card-source/{scenarioSlug}.parity.json` | Future Unity exporter running the original DCGO scenario | `NotRun` |
| RlFixture | `docs/generated/parity-fixtures/rl/full-card-source/{scenarioSlug}.parity.json` | Future RL.Engine parity trace exporter for the same scenario | `NotRun` |
| ComparisonReport | `docs/generated/parity-fixtures/reports/full-card-source/{scenarioSlug}.comparison.json` | ParityFixtureComparer | `NotRun` |

허용되는 상태 전이는 comparison report의 `Passed` 또는 `Failed` 결과가 존재할 때뿐이다. Unity fixture가 없으면 항상 `NotRun`이다.

## Aggregates

### Missing Artifacts

- ComparisonReport: 3918
- RlFixture: 3918
- UnityFixture: 3918

### Source Set Counts

- AD1: 25
- BT1: 88
- BT10: 100
- BT11: 102
- BT12: 102
- BT13: 109
- BT14: 94
- BT15: 102
- BT16: 103
- BT17: 102
- BT18: 101
- BT19: 103
- BT2: 91
- BT20: 103
- BT21: 103
- BT22: 102
- BT23: 103
- BT24: 102
- BT25: 104
- BT3: 71
- BT4: 88
- BT5: 94
- BT6: 101
- BT7: 104
- BT8: 94
- BT9: 96
- EX1: 73
- EX10: 74
- EX11: 74
- EX2: 73
- EX3: 73
- EX4: 74
- EX5: 75
- EX6: 74
- EX7: 76
- EX8: 75
- EX9: 74
- LM: 62
- P: 233
- RB1: 32
- ST1: 12
- ST10: 12
- ST12: 12
- ST13: 12
- ST14: 11
- ST15: 13
- ST16: 14
- ST17: 13
- ST18: 15
- ST19: 15
- ST2: 11
- ST20: 15
- ST21: 15
- ST22: 14
- ST23: 15
- ST24: 15
- ST3: 11
- ST4: 10
- ST5: 9
- ST6: 10
- ST7: 12
- ST8: 11
- ST9: 12

### Top Non-Verified Capabilities

- ContinuousOrStaticEffect: 3897
- OncePerTurn: 3628
- ZoneMovement: 3505
- InheritedSource: 2309
- Selection.SelectPermanent: 2128
- OnEnterFieldAnyone: 2042
- Selection.SelectCard: 1749
- OptionalDecision: 1582
- SkippableEffect: 1577
- Selection.SelectSecurity: 1282
- WhenDigivolvingTrigger: 1190
- OnPlayTrigger: 1085
- OnAllyAttack: 945
- Selection.SelectHand: 943
- SecuritySkill: 894

## Candidate Queue Sample

| # | Source Effect | Source Path | Affected | Missing | Reduction Status |
| ---: | --- | --- | ---: | --- | --- |
| 1 | `AD1_001` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 2 | `AD1_002` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_002.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 3 | `AD1_003` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 4 | `AD1_004` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 5 | `AD1_005` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 6 | `AD1_006` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_006.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 7 | `AD1_007` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_007.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 8 | `AD1_008` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 9 | `AD1_009` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 10 | `AD1_010` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 11 | `AD1_011` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 12 | `AD1_012` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 13 | `AD1_013` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 14 | `AD1_014` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 15 | `AD1_015` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_015.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 16 | `AD1_016` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 17 | `AD1_017` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_017.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 18 | `AD1_018` | `DCGO/Assets/Scripts/CardEffect/AD1/Purple/AD1_018.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 19 | `AD1_019` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_019.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 20 | `AD1_020` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_020.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 21 | `AD1_021` | `DCGO/Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 22 | `AD1_022` | `DCGO/Assets/Scripts/CardEffect/AD1/Green/AD1_022.cs` | 1 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 23 | `AD1_023` | `DCGO/Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 24 | `AD1_024` | `DCGO/Assets/Scripts/CardEffect/AD1/Blue/AD1_024.cs` | 3 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |
| 25 | `AD1_025` | `DCGO/Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs` | 2 | UnityFixture, RlFixture, ComparisonReport | `BlockedMissingUnityFixture` |

## Boundary And Handoff

- `FND001-CS-07`: unsupported trigger/process keyword static factory mapping은 별도 source mapping 경계로 남긴다.
- `FND001-CS-14`: strict Unity source ordering parity는 별도 source mapping 경계로 남긴다.
- `TRUST-001-RERUN`: fixture 비교 전에 기존 src reuse/delete/manual-review 경계를 foundation evidence 기준으로 갱신한다.

## Next Goal Candidates

- `PARITY-001-A`: Unity full-card fixture exporter scenario contract - ReadyToPlan
- `PARITY-001-B`: RL fixture generator for source-locked full-card scenarios - BlockedUntilUnityContract
- `PARITY-001-C`: Comparison report batch runner and NotRun reduction audit - BlockedUntilFixturesExist
- `FND001-CS-07`: Unsupported trigger/process keyword source mapping - ParallelFoundationPrerequisite
- `TRUST-001-RERUN`: Refresh src reuse boundary after foundation evidence - AfterParityPlan
