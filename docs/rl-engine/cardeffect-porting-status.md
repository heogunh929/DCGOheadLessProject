# CardEffect 포팅 상태표

## Current Snapshot - 2026-06-14

- 기준: source-aligned 로컬 작업트리. cached `origin/main`/로컬 기준점은 `a20f045a chore: checkpoint porting structure guards`이며, queue 34~38 변경은 아직 commit 전이다.
- ST1 target deck validation: 통과.
- ST1~ST3 registry snapshot: 48장 모두 `Implemented` 또는 `NoEffect`로 등록되어 있으며, 최신 구조 guard 기록은 `All 212 tests passed.`다.
- 남은 source-alignment risk: `ST2-07`, `ST3-07`은 asset에서 shared `ST1_06`을 참조하지만 registry는 `NoEffect`다. `ST3-02`는 `ST3_02_P2.asset` variant 확인이 필요하다.
- 학습 단계: 아직 진입하지 않는다. `ObservationEncoder`, `RewardCalculator`, `DatasetExporter`, `Trainer`, RL Environment API는 구현 금지 상태다.

## Scope

대상 ST1 decklist: `path/to/ST1_zU4uCF5lBJt.txt`  
ST1 대상 card pool: `ST1-01`부터 `ST1-16`  

이 문서는 `./DCGO` Unity 원본 battle 로직을 Source of Truth로 삼아 RL.Engine card script 포팅 상태를 추적한다. `Implemented`는 ST1 target deck 검증 기준으로 원본 의미를 보존한 구현이 존재한다는 뜻이며, 전체 DCGO 카드풀/룰 완성을 의미하지 않는다.

## 코드 배치 원칙

- ST1 카드별 effect body는 원본 `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_*.cs`에 대응되는 `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_*.cs` 파일에 둔다.
- `St1CardScriptCatalog`는 `ICardScript` registry 등록과 명시적 NoEffect 등록만 담당한다.
- ST1 공통 helper는 원본 `CardEffectCommons` 또는 공통 CardEffect helper에 대응되는 범위만 `src/DCGO.RL.Engine/CardEffects/ST1/Red/St1ScriptSupport.cs`에 둔다.
- ST2/ST3 카드별 effect body도 원본 `DCGO/Assets/Scripts/CardEffect/ST2/Blue/ST2_*.cs`, `DCGO/Assets/Scripts/CardEffect/ST3/Yellow/ST3_*.cs`에 대응되는 `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_*.cs`, `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_*.cs` 파일에 둔다.
- `St2St3CardScriptCatalog`도 registry 등록만 담당한다.

## 상태 값

| Status | 의미 | 검증 deck 허용 |
| --- | --- | --- |
| `Unsupported` | 원본 효과를 아직 포팅하지 않았고 실행 시 명시적으로 실패해야 한다. | 아니오 |
| `NoEffect` | 원본상 명시 효과가 없는 카드다. | 예 |
| `StubbedForValidation` | 검증 보조용 stub이며 학습/대상 deck에는 허용하지 않는다. | 기본 아니오 |
| `PartiallyImplemented` | 일부 timing/body만 보존되어 target deck에는 아직 허용하지 않는다. | 아니오 |
| `Implemented` | typed context와 primitive/service 경계로 구현되어 target deck 검증에 포함 가능하다. | 예 |
| `Verified` | 원본 trace/scripted scenario와 replay 검증까지 통과했다. | 예 |

## 최신 ST1 상태표

| CardId | 원본 class | 최신 상태 | RL.Engine 대응 | 남은 범위 |
| --- | --- | --- | --- | --- |
| ST1-01 | `ST1_01` | `Implemented` | inherited 조건부 continuous DP +1000 | 없음 |
| ST1-02 | 없음 | `NoEffect` | `NoEffectCardScript` | 없음 |
| ST1-03 | `ST1_03` | `Implemented` | inherited owner-turn continuous DP +1000 | 없음 |
| ST1-04 | 없음 | `NoEffect` | `NoEffectCardScript` | 없음 |
| ST1-05 | 없음 | `NoEffect` | `NoEffectCardScript` | 없음 |
| ST1-06 | `ST1_06` | `Implemented` | Blocker keyword, OnPlay memory -2 | full block UX/selection 세부는 별도 룰 레이어 TODO |
| ST1-07 | `ST1_07` | `Implemented` | static SecurityAttack +1 | 없음 |
| ST1-08 | `ST1_08` | `Implemented` | WhenDigivolving selection + DP +3000 `UntilTurnEnd` | 없음 |
| ST1-09 | `ST1_09` | `Implemented` | OnBlockAnyone memory +3 trigger hook | full block end-to-end priority는 별도 TODO |
| ST1-10 | 없음 | `NoEffect` | `NoEffectCardScript` | 없음 |
| ST1-11 | `ST1_11` | `Implemented` | source 수 기반 dynamic SecurityAttack continuous effect | 없음 |
| ST1-12 | `ST1_12` | `Implemented` | field tamer aura DP +1000, security play-self tamer | 없음 |
| ST1-13 | `ST1_13` | `Implemented` | option selection DP duration, security player-wide SecurityAttack duration | 없음 |
| ST1-14 | `ST1_14` | `Implemented` | main/security SecurityDigimonDP duration | 없음 |
| ST1-15 | `ST1_15` | `Implemented` | main/security Activate Main Option target deletion | 없음 |
| ST1-16 | `ST1_16` | `Implemented` | main/security Activate Main Option target deletion | 없음 |

## ST1-12 Source Mapping

| 원본 DCGO | 원본 책임 | RL.Engine 대응 |
| --- | --- | --- |
| `ST1_12.Effect()` | owner turn 동안 내 battle area Digimon DP +1000 | `St1TaiKamiyaScript` continuous descriptor |
| `ST1_12.SecuritySkill()` | `[Security] Play this card without paying its memory cost.` | `St1TaiKamiyaScript` `SecuritySkill` descriptor |
| `CardEffectFactory.PlaySelfTamerSecurityEffect` | executing card를 비용 없이 새 permanent로 play | `Tier1PrimitiveService.PlayWithoutPayingCost` |
| `CardEffectCommons.CanPlayAsNewPermanent` | play 가능 여부와 target frame 조건 확인 | owner/controller/zone/type/field slot 검증 |
| `CardController.ISecurityCheck.SecurityCheck()` | security card를 `Executing`으로 이동하고, effect 후 남아 있으면 trash | `SecurityCheckService` + `SecurityEffectExecutionService` |

## 검증 상태

- `Unsupported`: 0
- `PartiallyImplemented`: 0
- ST1 deck validation: 통과
- EngineCompletionChecklistRunner: ST1 target deck 기준 통과
- 전체 테스트: `All 212 tests passed.`

## 아직 전체 엔진 차원에서 남은 항목

- full `MultipleSkills` simultaneous trigger priority/UI 선택 순서
- `BeforePayCost` / `AfterPayCost` / counter timing
- block selection result application의 end-to-end 통합
- Unity 원본 trace와 RL.Engine trace의 구조화 비교 확대
- RL 학습용 ObservationEncoder/RewardCalculator/DatasetExporter/Trainer는 아직 구현 금지 상태

## Current ST2/ST3 확장 상태

최신 기준일: 2026-06-14  
대상 확장: `ST1` 완료 기준점 이후 `ST1~ST3` target card pool source-aligned registry snapshot

ST2/ST3 확장은 원본 DCGO `DCGO/Assets/Scripts/CardEffect/ST2/**`, `DCGO/Assets/Scripts/CardEffect/ST3/**` inventory를 기준으로 `St2St3CardScriptCatalog`에 명시 등록했다. 현재 source-aligned 로컬 작업트리 기준 ST2/ST3 registry에는 `Unsupported`가 남아 있지 않다. 다만 `ST2-07`, `ST3-07`, `ST3-02`는 원본 asset/source mapping 확인이 필요한 source-alignment risk로 남아 있으며, 이 문서의 통과 상태는 해당 risk가 해소됐다는 뜻이 아니다.

| Set | `NoEffect` | `Unsupported` | `Implemented` |
| --- | --- | --- | --- |
| ST2 | ST2-02, ST2-04, ST2-05, ST2-07, ST2-10 | 없음 | ST2-01, ST2-03, ST2-06, ST2-08, ST2-09, ST2-11, ST2-12, ST2-13, ST2-14, ST2-15, ST2-16 |
| ST3 | ST3-02, ST3-03, ST3-06, ST3-07, ST3-10 | 없음 | ST3-01, ST3-04, ST3-05, ST3-08, ST3-09, ST3-11, ST3-12, ST3-13, ST3-14, ST3-15, ST3-16 |

ST2/ST3 skeleton은 `missing-layer:<id>` 토큰을 사용해 `TargetCardPoolValidator`의 missing layer report가 구체적인 공통 layer 이름을 반환하게 한다. 현재 ST1~ST3 target pool 기준 남은 missing layer는 없다. `security-add-this-card-to-hand`는 ST3-13/14 구현에서 generic `Executing -> Hand` primitive로 해결됐고, `digivolution-source-trash`는 ST2-03/06/09 구현에서 generic bottom-source trash primitive로 해결됐다. `opponent-no-source-condition`은 ST2-01/08/12 구현에서 opponent battle area no-source Digimon helper로 해결됐다. `negative-security-attack-duration`은 ST3-15 구현에서 permanent/player 대상 temporary `SecurityAttack` modifier와 `BattleKeywordService` 하한 계산으로 해결됐다. `st2st3-card-body-wiring`은 ST2-11 구현에서 once-per-turn OnAllyAttack unsuspend body를 `TriggerPipelineService`와 `Tier1PrimitiveService.Unsuspend`에 연결해 해결됐다. `bounce-to-hand`는 ST2-16 구현에서 generic `ReturnPermanentToHand` primitive로 top card hand 이동과 source trash를 분리해 해결됐다. `recovery-from-deck`와 `on-enter-field-when-digivolving-compat`는 ST3-09 구현에서 `Tier1PrimitiveService.RecoverFromDeck`와 `EffectTiming.WhenDigivolving` mapping으로 해결됐다. `continuous-security-digimon-dp`는 ST3-12 구현에서 continuous `SecurityDigimonDP` modifier와 `EffectiveStatService.SecurityDp`로 해결됐다. `cannot-attack-block-duration`은 ST2-14 구현에서 temporary `CannotAttack`/`CannotBlock` restriction modifier와 attack/block legal filtering으로 해결됐다. `evolution-source-card-play`는 ST2-15 구현에서 chained selection과 `Tier1PrimitiveService.PlayEvolutionSourceAsNewPermanent`로 해결됐다. `dp-zero-deletion-trigger`는 ST3-01/04 구현에서 `RuleProcessor` DP-zero deletion payload와 `EffectTiming.OnDestroyedAnyone` hook으로 해결됐다.

Batch A에서 ST2-13, ST3-05, ST3-08, ST3-11, ST3-16은 원본 의미가 기존 generic layer로 보존되어 `Implemented`로 승격했다. 이후 security add-to-hand 갱신에서 ST3-13과 ST3-14가, source-trash 갱신에서 ST2-03, ST2-06, ST2-09가, opponent no-source 갱신에서 ST2-01, ST2-08, ST2-12가, negative SecurityAttack duration 갱신에서 ST3-15가, ST2-11 body wiring 갱신에서 ST2-11이, bounce-to-hand 갱신에서 ST2-16이, recovery 갱신에서 ST3-09가, continuous SecurityDigimonDP 갱신에서 ST3-12가, attack/block restriction 갱신에서 ST2-14가, evolution-source-card-play 갱신에서 ST2-15가, DP-zero deletion trigger 갱신에서 ST3-01과 ST3-04가 generic layer 경계를 통해 `Implemented`로 승격했다. ST1~ST3 target validation은 통과하며, 다음 단계에서도 원본 의미가 보존되는 카드만 generic layer 경계를 통해 개별적으로 승격한다.

## Historical Notes

- 2026-06-13 ST1 완료 직후에는 ST2/ST3가 inventory/pass plan 단계였고, ST1~ST3 validation은 expected fail로 문서화되어 있었다.
- 이후 로컬 source-aligned 작업트리에서 ST2/ST3 registry snapshot과 구조 guard가 추가되었다.
- 과거 `All 170 tests passed.`, `All 194 tests passed.`, `All 202 tests passed.` 기록은 해당 시점의 test count이며, 현재 이 문서의 최신 구조 guard 기준 기록은 `All 212 tests passed.`다.

## Current ST1-ST3 Registry Snapshot - 2026-06-14

이 표는 `St1CardScriptCatalog`와 `St2St3CardScriptCatalog`의 현재 registry 상태를 테스트가 직접 대조하기 위한 최신 snapshot이다. `ST2-07`, `ST3-07`은 asset에서 shared `ST1_06`을 참조하지만 현재 registry는 `NoEffect`로 남아 있어 source-alignment risk로 기록한다. `ST3-02`도 `ST3_02_P2.asset` variant의 `CardEffectClassName: ST3_02` 확인이 필요하다.

| CardId | Registry status | RL.Engine file | Source mapping status |
| --- | --- | --- | --- |
| ST1-01 | `Implemented` | `CardEffects/ST1/Red/ST1_01.cs` | set-local source mapping |
| ST1-02 | `NoEffect` | `CardEffects/ST1/Red/ST1_02.cs` | missing-source marker |
| ST1-03 | `Implemented` | `CardEffects/ST1/Red/ST1_03.cs` | set-local source mapping |
| ST1-04 | `NoEffect` | `CardEffects/ST1/Red/ST1_04.cs` | missing-source marker |
| ST1-05 | `NoEffect` | `CardEffects/ST1/Red/ST1_05.cs` | missing-source marker |
| ST1-06 | `Implemented` | `CardEffects/ST1/Red/ST1_06.cs` | set-local source mapping |
| ST1-07 | `Implemented` | `CardEffects/ST1/Red/ST1_07.cs` | set-local source mapping |
| ST1-08 | `Implemented` | `CardEffects/ST1/Red/ST1_08.cs` | set-local source mapping |
| ST1-09 | `Implemented` | `CardEffects/ST1/Red/ST1_09.cs` | set-local source mapping |
| ST1-10 | `NoEffect` | `CardEffects/ST1/Red/ST1_10.cs` | missing-source marker |
| ST1-11 | `Implemented` | `CardEffects/ST1/Red/ST1_11.cs` | set-local source mapping |
| ST1-12 | `Implemented` | `CardEffects/ST1/Red/ST1_12.cs` | set-local source mapping |
| ST1-13 | `Implemented` | `CardEffects/ST1/Red/ST1_13.cs` | set-local source mapping |
| ST1-14 | `Implemented` | `CardEffects/ST1/Red/ST1_14.cs` | set-local source mapping |
| ST1-15 | `Implemented` | `CardEffects/ST1/Red/ST1_15.cs` | set-local source mapping |
| ST1-16 | `Implemented` | `CardEffects/ST1/Red/ST1_16.cs` | set-local source mapping |
| ST2-01 | `Implemented` | `CardEffects/ST2/Blue/ST2_01.cs` | set-local source mapping |
| ST2-02 | `NoEffect` | `CardEffects/ST2/Blue/ST2_02.cs` | missing-source marker |
| ST2-03 | `Implemented` | `CardEffects/ST2/Blue/ST2_03.cs` | set-local source mapping |
| ST2-04 | `NoEffect` | `CardEffects/ST2/Blue/ST2_04.cs` | missing-source marker |
| ST2-05 | `NoEffect` | `CardEffects/ST2/Blue/ST2_05.cs` | missing-source marker |
| ST2-06 | `Implemented` | `CardEffects/ST2/Blue/ST2_06.cs` | set-local source mapping |
| ST2-07 | `NoEffect` | `CardEffects/ST2/Blue/ST2_07.cs` | no set-local source; asset shared `ST1_06` risk |
| ST2-08 | `Implemented` | `CardEffects/ST2/Blue/ST2_08.cs` | set-local source mapping |
| ST2-09 | `Implemented` | `CardEffects/ST2/Blue/ST2_09.cs` | set-local source mapping |
| ST2-10 | `NoEffect` | `CardEffects/ST2/Blue/ST2_10.cs` | missing-source marker |
| ST2-11 | `Implemented` | `CardEffects/ST2/Blue/ST2_11.cs` | set-local source mapping |
| ST2-12 | `Implemented` | `CardEffects/ST2/Blue/ST2_12.cs` | set-local source mapping |
| ST2-13 | `Implemented` | `CardEffects/ST2/Blue/ST2_13.cs` | set-local source mapping |
| ST2-14 | `Implemented` | `CardEffects/ST2/Blue/ST2_14.cs` | set-local source mapping |
| ST2-15 | `Implemented` | `CardEffects/ST2/Blue/ST2_15.cs` | set-local source mapping |
| ST2-16 | `Implemented` | `CardEffects/ST2/Blue/ST2_16.cs` | set-local source mapping |
| ST3-01 | `Implemented` | `CardEffects/ST3/Yellow/ST3_01.cs` | set-local source mapping |
| ST3-02 | `NoEffect` | `CardEffects/ST3/Yellow/ST3_02.cs` | no set-local source; `ST3_02_P2.asset` variant risk |
| ST3-03 | `NoEffect` | `CardEffects/ST3/Yellow/ST3_03.cs` | missing-source marker |
| ST3-04 | `Implemented` | `CardEffects/ST3/Yellow/ST3_04.cs` | set-local source mapping |
| ST3-05 | `Implemented` | `CardEffects/ST3/Yellow/ST3_05.cs` | set-local source mapping |
| ST3-06 | `NoEffect` | `CardEffects/ST3/Yellow/ST3_06.cs` | missing-source marker |
| ST3-07 | `NoEffect` | `CardEffects/ST3/Yellow/ST3_07.cs` | no set-local source; asset shared `ST1_06` risk |
| ST3-08 | `Implemented` | `CardEffects/ST3/Yellow/ST3_08.cs` | set-local source mapping |
| ST3-09 | `Implemented` | `CardEffects/ST3/Yellow/ST3_09.cs` | set-local source mapping |
| ST3-10 | `NoEffect` | `CardEffects/ST3/Yellow/ST3_10.cs` | missing-source marker |
| ST3-11 | `Implemented` | `CardEffects/ST3/Yellow/ST3_11.cs` | set-local source mapping |
| ST3-12 | `Implemented` | `CardEffects/ST3/Yellow/ST3_12.cs` | set-local source mapping |
| ST3-13 | `Implemented` | `CardEffects/ST3/Yellow/ST3_13.cs` | set-local source mapping |
| ST3-14 | `Implemented` | `CardEffects/ST3/Yellow/ST3_14.cs` | set-local source mapping |
| ST3-15 | `Implemented` | `CardEffects/ST3/Yellow/ST3_15.cs` | set-local source mapping |
| ST3-16 | `Implemented` | `CardEffects/ST3/Yellow/ST3_16.cs` | set-local source mapping |

## 구조 검증 guard

2026-06-14 기준으로 ST1~ST3 구조 검증 테스트를 추가/강화했다.

- ST1 `Implemented`/`NoEffect` 카드는 모두 `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_XX.cs` 파일을 가진다.
- ST2 `Implemented`/`NoEffect` 카드는 모두 `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_XX.cs` 파일을 가진다.
- ST3 `Implemented`/`NoEffect` 카드는 모두 `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_XX.cs` 파일을 가진다.
- 원본 CardEffect가 있는 카드는 파일 상단에 `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_XX.cs` mapping을 기록한다.
- 원본 CardEffect가 없는 NoEffect 카드는 NoEffect marker 파일에 missing-source 근거를 기록한다.
- `St1CardScriptCatalog`와 `St2St3CardScriptCatalog`는 registry-only guard를 통과해야 한다.
- 이 문서의 ST1 status table 및 ST1~ST3 registry snapshot은 실제 registry status와 테스트로 비교된다.
- `ST2-07`, `ST3-07`, `ST3-02`처럼 asset/source mapping 충돌 가능성이 있는 NoEffect registry는 policy/status 문서에 source-alignment risk로 남겨야 한다.
- 실패/수정 내역: 첫 실행에서 기존 ST1 status table parser가 새 ST1~ST3 snapshot의 `ST1-*` 행을 함께 읽어 2개 구조 테스트가 실패했다. parser를 `## 최신 ST1 상태표` 섹션으로 한정한 뒤 재실행했다.
- 최신 실행 결과: `.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 212 tests passed.` MSBuild cache/temp access denied warning은 있었지만 test runner는 성공 종료했다.
