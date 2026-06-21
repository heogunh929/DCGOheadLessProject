# 전체 Mechanic / Effect Inventory Matrix

- Source snapshot: `local-manifest` / `docs/source/dcgo-source-file-manifest.json`
- Inventory SHA-256: `e8fd1723d947f14e49cdc1250e0e146092a1e7010cce2833b5dde4f28e836c27`
- CardEffect source files: 3918
- Script source files in scope: 436
- Gameplay card records linked from 62 manifest: 8186

## Status Policy

`Implemented`/`Verified` 판정은 원본 사용량만으로 만들지 않는다. 엔진 enum/service/test 또는 검증 문서 근거가 약하면 `PartiallyImplemented`, `Unsupported`, `NeedsSourceReview`로 남긴다.

## EffectTiming

| Timing | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| None | NeedsSourceReview | 2272 | 2220 | 4326 | 0 | 70 |
| OnEnterFieldAnyone | NeedsSourceReview | 2047 | 2042 | 4141 | 0 | 6 |
| OnAllyAttack | Verified | 950 | 945 | 1936 | 8 | 71 |
| SecuritySkill | Verified | 896 | 894 | 1880 | 15 | 80 |
| OnDestroyedAnyone | Verified | 629 | 623 | 1158 | 3 | 17 |
| OptionSkill | Verified | 524 | 522 | 939 | 13 | 93 |
| OnDeclaration | NeedsSourceReview | 302 | 298 | 578 | 0 | 1 |
| OnEndTurn | Verified | 256 | 249 | 549 | 2 | 10 |
| OnStartMainPhase | Verified | 226 | 222 | 483 | 4 | 17 |
| WhenPermanentWouldBeDeleted | NeedsSourceReview | 210 | 206 | 405 | 0 | 0 |
| WhenRemoveField | NeedsSourceReview | 167 | 164 | 304 | 0 | 0 |
| BeforePayCost | NeedsSourceReview | 143 | 141 | 284 | 0 | 8 |
| OnTappedAnyone | NeedsSourceReview | 140 | 139 | 306 | 0 | 1 |
| OnDetermineDoSecurityCheck | NeedsSourceReview | 122 | 119 | 228 | 0 | 0 |
| OnStartTurn | Verified | 121 | 120 | 322 | 3 | 24 |
| OnCounterTiming | Verified | 115 | 111 | 279 | 3 | 74 |
| OnEndBattle | NeedsSourceReview | 86 | 84 | 160 | 0 | 1 |
| OnEndAttack | Verified | 83 | 80 | 164 | 1 | 31 |
| OnLoseSecurity | Verified | 74 | 73 | 170 | 3 | 34 |
| WhenLinked | NeedsSourceReview | 67 | 64 | 87 | 0 | 0 |
| OnDigivolutionCardDiscarded | NeedsSourceReview | 54 | 53 | 121 | 0 | 0 |
| OnAddDigivolutionCards | NeedsSourceReview | 51 | 50 | 102 | 0 | 0 |
| OnDiscardHand | NeedsSourceReview | 35 | 34 | 59 | 0 | 0 |
| OnAttackTargetChanged | Verified | 32 | 31 | 55 | 1 | 19 |
| OnMove | NeedsSourceReview | 32 | 30 | 79 | 0 | 1 |
| OnUseOption | NeedsSourceReview | 31 | 30 | 65 | 0 | 0 |
| OnUnTappedAnyone | NeedsSourceReview | 30 | 29 | 70 | 0 | 1 |
| OnAddHand | NeedsSourceReview | 22 | 21 | 49 | 0 | 1 |
| OnDiscardLibrary | NeedsSourceReview | 21 | 20 | 51 | 0 | 0 |
| OnAddSecurity | NeedsSourceReview | 15 | 14 | 38 | 0 | 3 |
| OnDiscardSecurity | NeedsSourceReview | 15 | 14 | 23 | 0 | 0 |
| OnSecurityCheck | Verified | 10 | 9 | 20 | 2 | 43 |
| WhenDigisorption | NeedsSourceReview | 10 | 10 | 15 | 0 | 0 |
| WhenReturntoHandAnyone | NeedsSourceReview | 10 | 9 | 25 | 0 | 0 |
| WhenReturntoLibraryAnyone | NeedsSourceReview | 10 | 9 | 25 | 0 | 0 |
| AfterPayCost | NeedsSourceReview | 8 | 7 | 15 | 0 | 9 |
| OnLinkCardDiscarded | NeedsSourceReview | 8 | 7 | 14 | 0 | 0 |
| OnBlockAnyone | Verified | 7 | 6 | 13 | 5 | 24 |
| WhenTopCardTrashed | NeedsSourceReview | 5 | 3 | 6 | 0 | 0 |
| AfterEffectsActivate | Verified | 4 | 2 | 4 | 1 | 53 |
| OnDigivolutionCardReturnToDeckBottom | NeedsSourceReview | 4 | 3 | 4 | 0 | 0 |
| OnLeaveFieldAnyone | NeedsSourceReview | 3 | 1 | 2 | 0 | 1 |
| OnPermamemtReturnedToHand | NeedsSourceReview | 3 | 2 | 5 | 0 | 0 |
| OnRemovedField | NeedsSourceReview | 3 | 2 | 4 | 0 | 0 |
| OnReturnCardsToHandFromTrash | NeedsSourceReview | 3 | 2 | 6 | 0 | 0 |
| WhenWouldLink | NeedsSourceReview | 3 | 2 | 2 | 0 | 0 |
| OnFaceUpSecurityIncreased | NeedsSourceReview | 2 | 1 | 2 | 0 | 0 |
| OnReturnCardsToLibraryFromTrash | NeedsSourceReview | 2 | 1 | 2 | 0 | 0 |
| OnUseDigiburst | NeedsSourceReview | 2 | 1 | 1 | 0 | 0 |
| WhenUntapAnyone | NeedsSourceReview | 2 | 1 | 1 | 0 | 1 |
| WhenWouldDigivolutionCardDiscarded | NeedsSourceReview | 2 | 1 | 1 | 0 | 0 |
| OnDraw | NeedsSourceReview | 1 | 0 | 0 | 0 | 10 |
| OnStartBattle | NeedsSourceReview | 1 | 0 | 0 | 0 | 1 |
| RulesTiming | Verified | 1 | 0 | 0 | 5 | 74 |
| OnEndAttackPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndBlockDesignation | NotReferenced | 0 | 0 | 0 | 0 | 10 |
| OnEndCoinToss | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndMainPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnGetDamage | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnKnockOut | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnPlay | NotReferenced | 0 | 0 | 0 | 1 | 38 |
| OnUseAttack | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| WhenDigivolving | NotReferenced | 0 | 0 | 0 | 5 | 33 |

## Effect Capability Flags

| Capability | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| static_or_continuous | Verified | 4067 | 3897 | 7867 | 74 | 52 |
| zone_movement | Verified | 3707 | 3505 | 7024 | 235 | 233 |
| max_count_per_turn | Verified | 3661 | 3628 | 7323 | 14 | 19 |
| inherited | PartiallyImplemented | 2417 | 2309 | 4491 | 13 | 0 |
| optional | Verified | 1608 | 1582 | 3207 | 7 | 5 |
| skippable | Verified | 1594 | 1577 | 3079 | 46 | 55 |
| trigger_when_digivolving | Verified | 1199 | 1190 | 2505 | 21 | 66 |
| trigger_on_play | Verified | 1095 | 1085 | 2114 | 39 | 78 |
| security | Verified | 898 | 894 | 1880 | 111 | 113 |
| modifier_duration | Verified | 692 | 648 | 1219 | 198 | 156 |
| replacement_or_cut_in | Verified | 513 | 472 | 993 | 37 | 97 |
| linked | Verified | 120 | 87 | 110 | 62 | 27 |
| counter | Verified | 118 | 111 | 279 | 69 | 125 |
| background | Verified | 28 | 24 | 57 | 62 | 26 |
| trigger_priority | Verified | 24 | 13 | 21 | 84 | 179 |
| declarative | Unsupported | 4 | 0 | 0 | 0 | 2 |
| chain_activation | Verified | 3 | 0 | 0 | 3 | 2 |

## Selection / Root Zone

| Selection | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| SelectPermanent | Verified | 2150 | 2128 | 4101 | 31 | 25 |
| SelectJogress | Verified | 1908 | 1856 | 3719 | 3 | 5 |
| SelectCard | Verified | 1794 | 1749 | 3541 | 18 | 20 |
| SelectSecurity | PartiallyImplemented | 1313 | 1282 | 2588 | 3 | 0 |
| SelectHand | Verified | 950 | 943 | 1835 | 30 | 121 |
| SelectBoolean | Verified | 298 | 294 | 604 | 8 | 2 |
| SelectAttackTarget | Verified | 224 | 201 | 367 | 59 | 33 |
| SelectDigiXros | Verified | 125 | 101 | 199 | 3 | 5 |
| SelectInteger | Verified | 84 | 82 | 139 | 10 | 3 |
| SelectOrder | Verified | 78 | 61 | 160 | 37 | 111 |
| SelectBurstDigivolution | Verified | 64 | 45 | 104 | 3 | 5 |
| SelectAssembly | Verified | 29 | 15 | 26 | 3 | 5 |
| SelectAppFusion | Verified | 23 | 10 | 12 | 3 | 5 |
| SelectCount | PartiallyImplemented | 13 | 11 | 26 | 4 | 0 |
| SelectDeck | Verified | 8 | 0 | 0 | 19 | 46 |
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
| Digisorption | PartiallyImplemented | 27 | 19 | 40 | False |
| AppFusion | Verified | 23 | 10 | 12 | True |

## Keywords

| Keyword | Status | Source files | Card text records | Engine enum | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| Blocker | Verified | 425 | 3172 | True | 196 | 398 |
| Link | Verified | 177 | 360 | False | 372 | 282 |
| Piercing | Verified | 171 | 1204 | True | 20 | 28 |
| Pierce | Verified | 168 | 0 | True | 10 | 14 |
| Save | Verified | 136 | 628 | False | 2 | 14 |
| Reboot | Verified | 135 | 824 | True | 4 | 32 |
| Rush | Verified | 107 | 824 | True | 4 | 30 |
| Jamming | Verified | 102 | 748 | True | 4 | 26 |
| Retaliation | Verified | 100 | 556 | True | 10 | 28 |
| Raid | Unsupported | 92 | 604 | False | 0 | 0 |
| Alliance | Unsupported | 88 | 552 | False | 0 | 0 |
| Execute | Verified | 73 | 72 | False | 198 | 164 |
| Barrier | Unsupported | 61 | 388 | False | 0 | 2 |
| ArmorPurge | Unsupported | 48 | 192 | False | 0 | 0 |
| Collision | Verified | 45 | 208 | True | 6 | 32 |
| Blitz | Unsupported | 38 | 244 | False | 0 | 0 |
| Fortitude | Unsupported | 35 | 152 | False | 0 | 0 |
| Training | Unsupported | 32 | 152 | False | 0 | 30 |
| Vortex | Unsupported | 30 | 148 | False | 0 | 0 |
| Evade | Unsupported | 26 | 140 | False | 0 | 2 |
| SecurityAttack | Verified | 26 | 772 | True | 92 | 296 |
| Scapegoat | Unsupported | 23 | 100 | False | 0 | 0 |
| Decode | Unsupported | 19 | 8 | False | 0 | 0 |
| Decoy | Verified | 18 | 92 | True | 10 | 38 |
| Iceclad | Unsupported | 18 | 60 | False | 0 | 0 |
| Partition | Unsupported | 18 | 0 | False | 0 | 8 |
| MaterialSave | Unsupported | 15 | 0 | False | 0 | 0 |
| MindLink | Unsupported | 15 | 58 | False | 0 | 0 |
| Overclock | Unsupported | 15 | 8 | False | 0 | 0 |
| Progress | Unsupported | 13 | 72 | False | 0 | 4 |
| Fragment | Verified | 9 | 0 | False | 2 | 2 |
| Ascension | Unsupported | 8 | 8 | False | 0 | 0 |
| BlastDNADigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
| BlastDigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
