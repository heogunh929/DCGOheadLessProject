# 전체 Mechanic / Effect Inventory Matrix

- Source snapshot: `local-manifest` / `docs/source/dcgo-source-file-manifest.json`
- Inventory SHA-256: `f10fff14bd37d401b3e4a48ddd6941d767ebd046ac73f9259795cb7e25ffff97`
- CardEffect source files: 3918
- Script source files in scope: 436
- Gameplay card records linked from 62 manifest: 8186

## Status Policy

`Implemented`/`Verified` 판정은 원본 사용량만으로 만들지 않는다. 엔진 enum/service/test 또는 검증 문서 근거가 약하면 `PartiallyImplemented`, `Unsupported`, `NeedsSourceReview`로 남긴다.

## EffectTiming

| Timing | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| None | PartiallyImplemented | 2272 | 2220 | 4326 | 3 | 194 |
| OnEnterFieldAnyone | PartiallyImplemented | 2047 | 2042 | 4141 | 2 | 134 |
| OnAllyAttack | Verified | 950 | 945 | 1936 | 8 | 78 |
| SecuritySkill | Verified | 896 | 894 | 1880 | 24 | 153 |
| OnDestroyedAnyone | Verified | 629 | 623 | 1158 | 5 | 67 |
| OptionSkill | Verified | 524 | 522 | 939 | 14 | 110 |
| OnDeclaration | NeedsSourceReview | 302 | 298 | 578 | 0 | 12 |
| OnEndTurn | Verified | 256 | 249 | 549 | 3 | 24 |
| OnStartMainPhase | Verified | 226 | 222 | 483 | 9 | 38 |
| WhenPermanentWouldBeDeleted | NeedsSourceReview | 210 | 206 | 405 | 0 | 1 |
| WhenRemoveField | NeedsSourceReview | 167 | 164 | 304 | 0 | 15 |
| BeforePayCost | NeedsSourceReview | 143 | 141 | 284 | 0 | 66 |
| OnTappedAnyone | NeedsSourceReview | 140 | 139 | 306 | 0 | 25 |
| OnDetermineDoSecurityCheck | NeedsSourceReview | 122 | 119 | 228 | 0 | 7 |
| OnStartTurn | Verified | 121 | 120 | 322 | 6 | 46 |
| OnCounterTiming | Verified | 115 | 111 | 279 | 3 | 75 |
| OnEndBattle | NeedsSourceReview | 86 | 84 | 160 | 0 | 16 |
| OnEndAttack | Verified | 83 | 80 | 164 | 1 | 33 |
| OnLoseSecurity | Verified | 74 | 73 | 170 | 5 | 78 |
| WhenLinked | NeedsSourceReview | 67 | 64 | 87 | 0 | 0 |
| OnDigivolutionCardDiscarded | NeedsSourceReview | 54 | 53 | 121 | 0 | 18 |
| OnAddDigivolutionCards | NeedsSourceReview | 51 | 50 | 102 | 0 | 22 |
| OnDiscardHand | Verified | 35 | 34 | 59 | 1 | 10 |
| OnAttackTargetChanged | Verified | 32 | 31 | 55 | 1 | 20 |
| OnMove | NeedsSourceReview | 32 | 30 | 79 | 0 | 8 |
| OnUseOption | NeedsSourceReview | 31 | 30 | 65 | 0 | 7 |
| OnUnTappedAnyone | NeedsSourceReview | 30 | 29 | 70 | 0 | 11 |
| OnAddHand | Verified | 22 | 21 | 49 | 1 | 53 |
| OnDiscardLibrary | NeedsSourceReview | 21 | 20 | 51 | 0 | 7 |
| OnAddSecurity | NeedsSourceReview | 15 | 14 | 38 | 0 | 16 |
| OnDiscardSecurity | NeedsSourceReview | 15 | 14 | 23 | 0 | 6 |
| OnSecurityCheck | Verified | 10 | 9 | 20 | 2 | 45 |
| WhenDigisorption | NeedsSourceReview | 10 | 10 | 15 | 0 | 12 |
| WhenReturntoHandAnyone | Verified | 10 | 9 | 25 | 1 | 7 |
| WhenReturntoLibraryAnyone | NeedsSourceReview | 10 | 9 | 25 | 0 | 8 |
| AfterPayCost | NeedsSourceReview | 8 | 7 | 15 | 0 | 13 |
| OnLinkCardDiscarded | NeedsSourceReview | 8 | 7 | 14 | 0 | 0 |
| OnBlockAnyone | Verified | 7 | 6 | 13 | 5 | 24 |
| WhenTopCardTrashed | Verified | 5 | 3 | 6 | 1 | 4 |
| AfterEffectsActivate | Verified | 4 | 2 | 4 | 1 | 64 |
| OnDigivolutionCardReturnToDeckBottom | Verified | 4 | 3 | 4 | 1 | 3 |
| OnLeaveFieldAnyone | Verified | 3 | 1 | 2 | 1 | 10 |
| OnPermamemtReturnedToHand | Verified | 3 | 2 | 5 | 1 | 12 |
| OnRemovedField | NeedsSourceReview | 3 | 2 | 4 | 0 | 3 |
| OnReturnCardsToHandFromTrash | Verified | 3 | 2 | 6 | 1 | 5 |
| WhenWouldLink | NeedsSourceReview | 3 | 2 | 2 | 0 | 0 |
| OnFaceUpSecurityIncreased | NeedsSourceReview | 2 | 1 | 2 | 0 | 0 |
| OnReturnCardsToLibraryFromTrash | Verified | 2 | 1 | 2 | 1 | 3 |
| OnUseDigiburst | NeedsSourceReview | 2 | 1 | 1 | 0 | 0 |
| WhenUntapAnyone | NeedsSourceReview | 2 | 1 | 1 | 0 | 13 |
| WhenWouldDigivolutionCardDiscarded | NeedsSourceReview | 2 | 1 | 1 | 0 | 1 |
| OnDraw | Verified | 1 | 0 | 0 | 1 | 63 |
| OnStartBattle | NeedsSourceReview | 1 | 0 | 0 | 0 | 4 |
| RulesTiming | Verified | 1 | 0 | 0 | 5 | 78 |
| OnEndAttackPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndBlockDesignation | NotReferenced | 0 | 0 | 0 | 0 | 10 |
| OnEndCoinToss | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnEndMainPhase | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnGetDamage | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnKnockOut | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| OnPlay | NotReferenced | 0 | 0 | 0 | 19 | 136 |
| OnUseAttack | NotReferenced | 0 | 0 | 0 | 0 | 0 |
| WhenDigivolving | NotReferenced | 0 | 0 | 0 | 16 | 104 |

## Effect Capability Flags

| Capability | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| static_or_continuous | Verified | 4067 | 3897 | 7867 | 360 | 237 |
| zone_movement | Verified | 3707 | 3505 | 7024 | 244 | 271 |
| max_count_per_turn | Verified | 3661 | 3628 | 7323 | 14 | 21 |
| inherited | Verified | 2417 | 2309 | 4491 | 23 | 1 |
| optional | Verified | 1608 | 1582 | 3207 | 7 | 5 |
| skippable | Verified | 1594 | 1577 | 3079 | 60 | 56 |
| trigger_when_digivolving | Verified | 1199 | 1190 | 2505 | 118 | 308 |
| trigger_on_play | Verified | 1095 | 1085 | 2114 | 188 | 402 |
| security | Verified | 898 | 894 | 1880 | 120 | 127 |
| modifier_duration | Verified | 692 | 648 | 1219 | 259 | 213 |
| replacement_or_cut_in | Verified | 513 | 472 | 993 | 47 | 119 |
| linked | Verified | 120 | 87 | 110 | 67 | 36 |
| counter | Verified | 118 | 111 | 279 | 71 | 133 |
| background | Verified | 28 | 24 | 57 | 62 | 29 |
| trigger_priority | Verified | 24 | 13 | 21 | 100 | 323 |
| declarative | Unsupported | 4 | 0 | 0 | 0 | 9 |
| chain_activation | Verified | 3 | 0 | 0 | 3 | 2 |

## Selection / Root Zone

| Selection | Status | Source files | CardEffect files | Affected cards | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- |
| SelectPermanent | Verified | 2150 | 2128 | 4101 | 50 | 42 |
| SelectJogress | Verified | 1908 | 1856 | 3719 | 3 | 5 |
| SelectCard | Verified | 1794 | 1749 | 3541 | 23 | 20 |
| SelectSecurity | PartiallyImplemented | 1313 | 1282 | 2588 | 3 | 0 |
| SelectHand | Verified | 950 | 943 | 1835 | 43 | 168 |
| SelectBoolean | Verified | 298 | 294 | 604 | 8 | 2 |
| SelectAttackTarget | Verified | 224 | 201 | 367 | 60 | 40 |
| SelectDigiXros | Verified | 125 | 101 | 199 | 3 | 5 |
| SelectInteger | Verified | 84 | 82 | 139 | 10 | 4 |
| SelectOrder | Verified | 78 | 61 | 160 | 53 | 242 |
| SelectBurstDigivolution | Verified | 64 | 45 | 104 | 3 | 5 |
| SelectAssembly | Verified | 29 | 15 | 26 | 3 | 5 |
| SelectAppFusion | Verified | 23 | 10 | 12 | 3 | 5 |
| SelectCount | Verified | 13 | 11 | 26 | 4 | 1 |
| SelectDeck | Verified | 8 | 0 | 0 | 28 | 72 |
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
| Blocker | Verified | 425 | 3172 | True | 240 | 1228 |
| Link | Verified | 177 | 360 | False | 538 | 678 |
| Piercing | Verified | 171 | 1204 | True | 34 | 106 |
| Pierce | Verified | 168 | 0 | True | 17 | 62 |
| Save | Verified | 136 | 628 | False | 34 | 138 |
| Reboot | Verified | 135 | 824 | True | 10 | 86 |
| Rush | Verified | 107 | 824 | True | 6 | 120 |
| Jamming | Verified | 102 | 748 | True | 12 | 128 |
| Retaliation | Verified | 100 | 556 | True | 24 | 128 |
| Raid | Verified | 92 | 604 | False | 4 | 4 |
| Alliance | Unsupported | 88 | 552 | False | 0 | 6 |
| Execute | Verified | 73 | 72 | False | 210 | 364 |
| Barrier | Unsupported | 61 | 388 | False | 0 | 2 |
| ArmorPurge | Unsupported | 48 | 192 | False | 0 | 0 |
| Collision | Verified | 45 | 208 | True | 6 | 52 |
| Blitz | Unsupported | 38 | 244 | False | 0 | 16 |
| Fortitude | Verified | 35 | 152 | False | 8 | 16 |
| Training | Verified | 32 | 152 | False | 2 | 38 |
| Vortex | Verified | 30 | 148 | False | 4 | 16 |
| Evade | Unsupported | 26 | 140 | False | 0 | 2 |
| SecurityAttack | Verified | 26 | 772 | True | 154 | 518 |
| Scapegoat | Unsupported | 23 | 100 | False | 0 | 10 |
| Decode | Verified | 19 | 8 | False | 10 | 18 |
| Decoy | Verified | 18 | 92 | True | 14 | 42 |
| Iceclad | Verified | 18 | 60 | False | 4 | 4 |
| Partition | Unsupported | 18 | 0 | False | 0 | 8 |
| MaterialSave | Unsupported | 15 | 0 | False | 0 | 0 |
| MindLink | Unsupported | 15 | 58 | False | 0 | 0 |
| Overclock | Verified | 15 | 8 | False | 4 | 4 |
| Progress | Unsupported | 13 | 72 | False | 0 | 14 |
| Fragment | Verified | 9 | 0 | False | 2 | 2 |
| Ascension | Unsupported | 8 | 8 | False | 0 | 0 |
| BlastDNADigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
| BlastDigivolution | NotReferenced | 0 | 0 | False | 0 | 0 |
