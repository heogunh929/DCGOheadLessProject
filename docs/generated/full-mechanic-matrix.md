# 전체 Mechanic / Effect Inventory Matrix

- Source snapshot: `local-manifest` / `docs/source/dcgo-source-file-manifest.json`
- Inventory SHA-256: `674811df9b141a5b4ca871176469177c129f55d5f3382d4a90fea073d9cec4fb`
- CardEffect source files: 3918
- Script source files in scope: 436
- Gameplay card records linked from 62 manifest: 8186

## Status Policy

`Implemented`/`Verified` 판정은 원본 사용량만으로 만들지 않는다. 엔진 enum/service/test 또는 검증 문서 근거가 약하면 `PartiallyImplemented`, `Unsupported`, `NeedsSourceReview`로 남긴다.

## EffectTiming

| Timing | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| None | PartiallyImplemented | 2272 | 2220 | 4326 | 8 | 232 |
| OnEnterFieldAnyone | PartiallyImplemented | 2047 | 2042 | 4141 | 2 | 137 |
| OnAllyAttack | Verified | 950 | 945 | 1936 | 8 | 98 |
| SecuritySkill | Verified | 896 | 894 | 1880 | 24 | 153 |
| OnDestroyedAnyone | Verified | 629 | 623 | 1158 | 5 | 68 |
| OptionSkill | Verified | 524 | 522 | 939 | 14 | 133 |
| OnDeclaration | PartiallyImplemented | 302 | 298 | 578 | 1 | 28 |
| OnEndTurn | Verified | 256 | 249 | 549 | 3 | 24 |
| OnStartMainPhase | Verified | 226 | 222 | 483 | 9 | 38 |
| WhenPermanentWouldBeDeleted | PartiallyImplemented | 210 | 206 | 405 | 1 | 3 |
| WhenRemoveField | PartiallyImplemented | 167 | 164 | 304 | 3 | 22 |
| BeforePayCost | PartiallyImplemented | 143 | 141 | 284 | 2 | 95 |
| OnTappedAnyone | PartiallyImplemented | 140 | 139 | 306 | 1 | 41 |
| OnDetermineDoSecurityCheck | PartiallyImplemented | 122 | 119 | 228 | 1 | 26 |
| OnStartTurn | Verified | 121 | 120 | 322 | 6 | 46 |
| OnCounterTiming | Verified | 115 | 111 | 279 | 3 | 75 |
| OnEndBattle | PartiallyImplemented | 86 | 84 | 160 | 1 | 31 |
| OnEndAttack | Verified | 83 | 80 | 164 | 1 | 34 |
| OnLoseSecurity | Verified | 74 | 73 | 170 | 6 | 82 |
| WhenLinked | Unsupported | 67 | 64 | 87 | 0 | 0 |
| OnDigivolutionCardDiscarded | PartiallyImplemented | 54 | 53 | 121 | 2 | 27 |
| OnAddDigivolutionCards | PartiallyImplemented | 51 | 50 | 102 | 1 | 32 |
| OnDiscardHand | Verified | 35 | 34 | 59 | 1 | 11 |
| OnAttackTargetChanged | Verified | 32 | 31 | 55 | 1 | 20 |
| OnMove | PartiallyImplemented | 32 | 30 | 79 | 1 | 21 |
| OnUseOption | PartiallyImplemented | 31 | 30 | 65 | 1 | 28 |
| OnUnTappedAnyone | PartiallyImplemented | 30 | 29 | 70 | 1 | 26 |
| OnAddHand | Verified | 22 | 21 | 49 | 1 | 54 |
| OnDiscardLibrary | PartiallyImplemented | 21 | 20 | 51 | 1 | 19 |
| OnAddSecurity | PartiallyImplemented | 15 | 14 | 38 | 1 | 32 |
| OnDiscardSecurity | PartiallyImplemented | 15 | 14 | 23 | 1 | 16 |
| OnSecurityCheck | Verified | 10 | 9 | 20 | 2 | 47 |
| WhenDigisorption | Unsupported | 10 | 10 | 15 | 0 | 12 |
| WhenReturntoHandAnyone | Verified | 10 | 9 | 25 | 1 | 8 |
| WhenReturntoLibraryAnyone | PartiallyImplemented | 10 | 9 | 25 | 1 | 10 |
| AfterPayCost | PartiallyImplemented | 8 | 7 | 15 | 2 | 39 |
| OnLinkCardDiscarded | Unsupported | 8 | 7 | 14 | 0 | 0 |
| OnBlockAnyone | Verified | 7 | 6 | 13 | 5 | 26 |
| WhenTopCardTrashed | Verified | 5 | 3 | 6 | 1 | 4 |
| AfterEffectsActivate | Verified | 4 | 2 | 4 | 1 | 64 |
| OnDigivolutionCardReturnToDeckBottom | Verified | 4 | 3 | 4 | 1 | 3 |
| OnLeaveFieldAnyone | Verified | 3 | 1 | 2 | 1 | 11 |
| OnPermamemtReturnedToHand | Verified | 3 | 2 | 5 | 1 | 13 |
| OnRemovedField | PartiallyImplemented | 3 | 2 | 4 | 1 | 13 |
| OnReturnCardsToHandFromTrash | Verified | 3 | 2 | 6 | 1 | 6 |
| WhenWouldLink | Unsupported | 3 | 2 | 2 | 0 | 2 |
| OnFaceUpSecurityIncreased | PartiallyImplemented | 2 | 1 | 2 | 1 | 6 |
| OnReturnCardsToLibraryFromTrash | Verified | 2 | 1 | 2 | 1 | 3 |
| OnUseDigiburst | Unsupported | 2 | 1 | 1 | 0 | 0 |
| WhenUntapAnyone | PartiallyImplemented | 2 | 1 | 1 | 2 | 18 |
| WhenWouldDigivolutionCardDiscarded | PartiallyImplemented | 2 | 1 | 1 | 2 | 4 |
| OnDraw | Verified | 1 | 0 | 0 | 1 | 63 |
| OnStartBattle | NotReferenced | 1 | 0 | 0 | 0 | 4 |
| RulesTiming | Verified | 1 | 0 | 0 | 5 | 78 |
| OnEndAttackPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndBlockDesignation | NotReferenced | 0 | 0 | 0 | 0 | 10 |
| OnEndCoinToss | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndMainPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnGetDamage | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnKnockOut | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnPlay | NotReferenced | 0 | 0 | 0 | 19 | 178 |
| OnUseAttack | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| WhenDigivolving | NotReferenced | 0 | 0 | 0 | 16 | 104 |

## Effect Capability Flags

| Capability | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| static_or_continuous | Verified | 4067 | 3897 | 7867 | 388 | 283 |
| zone_movement | Verified | 3707 | 3505 | 7024 | 256 | 322 |
| max_count_per_turn | Verified | 3661 | 3628 | 7323 | 14 | 21 |
| inherited | Verified | 2417 | 2309 | 4491 | 42 | 12 |
| optional | Verified | 1608 | 1582 | 3207 | 7 | 5 |
| skippable | Verified | 1594 | 1577 | 3079 | 68 | 57 |
| trigger_when_digivolving | Verified | 1199 | 1190 | 2505 | 118 | 316 |
| trigger_on_play | Verified | 1095 | 1085 | 2114 | 189 | 492 |
| security | Verified | 898 | 894 | 1880 | 120 | 132 |
| modifier_duration | Verified | 692 | 648 | 1219 | 259 | 258 |
| replacement_or_cut_in | Verified | 513 | 472 | 993 | 86 | 156 |
| linked | Verified | 120 | 87 | 110 | 92 | 51 |
| counter | Verified | 118 | 111 | 279 | 71 | 143 |
| background | Verified | 28 | 24 | 57 | 87 | 43 |
| trigger_priority | Verified | 24 | 13 | 21 | 114 | 341 |
| declarative | Unsupported | 4 | 0 | 0 | 0 | 9 |
| chain_activation | Verified | 3 | 0 | 0 | 3 | 2 |

## Selection / Root Zone

| Selection | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| SelectPermanent | Verified | 2150 | 2128 | 4101 | 54 | 46 |
| SelectJogress | Verified | 1908 | 1856 | 3719 | 3 | 5 |
| SelectCard | Verified | 1794 | 1749 | 3541 | 27 | 22 |
| SelectSecurity | Verified | 1313 | 1282 | 2588 | 6 | 2 |
| SelectHand | Verified | 950 | 943 | 1835 | 69 | 230 |
| SelectBoolean | Verified | 298 | 294 | 604 | 8 | 2 |
| SelectAttackTarget | Verified | 224 | 201 | 367 | 60 | 40 |
| SelectDigiXros | Verified | 125 | 101 | 199 | 3 | 5 |
| SelectInteger | Verified | 84 | 82 | 139 | 11 | 5 |
| SelectOrder | Verified | 78 | 61 | 160 | 57 | 260 |
| SelectBurstDigivolution | Verified | 64 | 45 | 104 | 3 | 5 |
| SelectAssembly | Verified | 29 | 15 | 26 | 3 | 5 |
| SelectAppFusion | Verified | 23 | 10 | 12 | 3 | 5 |
| SelectCount | Verified | 13 | 11 | 26 | 8 | 3 |
| SelectDeck | Verified | 8 | 0 | 0 | 37 | 86 |
| SelectFieldSlot | NotReferenced | 0 | 0 | 0 | 3 | 3 |

## Special Mechanics

| Mechanic | Status | Source files | CardEffect files | Affected cards | Engine enum |
| --- | --- | --- | --- | --- | --- |
| Jogress | Verified | 1908 | 1856 | 3719 | True |
| DelayOption | Verified | 140 | 132 | 281 | True |
| DigiXros | Verified | 125 | 101 | 199 | True |
| Link | Verified | 105 | 82 | 116 | True |
| BurstDigivolution | Verified | 64 | 45 | 104 | True |
| BlastDigivolution | Unsupported | 57 | 52 | 136 | False |
| DigiBurst | PartiallyImplemented | 45 | 39 | 84 | False |
| AceOverflow | Verified | 38 | 31 | 76 | True |
| Assembly | Verified | 29 | 15 | 26 | True |
| Digisorption | Verified | 27 | 19 | 40 | False |
| AppFusion | Verified | 23 | 10 | 12 | True |

## Keywords

| Keyword | Status | Source files | Card text records | Engine enum | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| Blocker | Verified | 425 | 3172 | True | 260 | 1350 |
| Link | Verified | 177 | 360 | False | 646 | 1238 |
| Piercing | Verified | 171 | 1204 | True | 42 | 132 |
| Pierce | Verified | 168 | 0 | True | 21 | 87 |
| Save | Verified | 136 | 628 | False | 34 | 138 |
| Reboot | Verified | 135 | 824 | True | 22 | 136 |
| Rush | Verified | 107 | 824 | True | 18 | 168 |
| Jamming | Verified | 102 | 748 | True | 24 | 184 |
| Retaliation | Verified | 100 | 556 | True | 24 | 134 |
| Raid | Verified | 92 | 604 | False | 4 | 4 |
| Alliance | Unsupported | 88 | 552 | False | 0 | 8 |
| Execute | Verified | 73 | 72 | False | 230 | 406 |
| Barrier | Unsupported | 61 | 388 | False | 0 | 2 |
| ArmorPurge | Unsupported | 48 | 192 | False | 0 | 0 |
| Collision | Verified | 45 | 208 | True | 18 | 98 |
| Blitz | Unsupported | 38 | 244 | False | 0 | 16 |
| Fortitude | Verified | 35 | 152 | False | 8 | 16 |
| Training | Verified | 32 | 152 | False | 2 | 38 |
| Vortex | Verified | 30 | 148 | False | 4 | 18 |
| Evade | Unsupported | 26 | 140 | False | 0 | 2 |
| SecurityAttack | Verified | 26 | 772 | True | 156 | 546 |
| Scapegoat | Unsupported | 23 | 100 | False | 0 | 12 |
| Decode | Verified | 19 | 8 | False | 10 | 18 |
| Decoy | Verified | 18 | 92 | True | 14 | 42 |
| Iceclad | Verified | 18 | 60 | False | 4 | 6 |
| Partition | Unsupported | 18 | 0 | False | 0 | 8 |
| MaterialSave | Unsupported | 15 | 0 | False | 0 | 0 |
| MindLink | Unsupported | 15 | 58 | False | 0 | 0 |
| Overclock | Verified | 15 | 8 | False | 4 | 4 |
| Progress | Unsupported | 13 | 72 | False | 0 | 16 |
| Fragment | Verified | 9 | 0 | False | 2 | 6 |
| Ascension | Unsupported | 8 | 8 | False | 0 | 0 |
| BlastDNADigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
| BlastDigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
