# CardEffect File Layout Audit

## 최신 상태 요약 - 2026-06-15

이 문서는 queue 42 `cardeffect_file_layout_audit`와 queue 43 `cardeffect_file_layout_guard_tests`의 결과를 정리한다. 이번 범위에서 새 카드 효과 구현은 하지 않았고, 기존 `DCGO/Assets/Scripts` Unity 원본도 수정하지 않았다. 원격 `origin`은 존재하지만 fetch/pull/push는 수행하지 않았다.

현재 RL.Engine에는 ST1/ST2/ST3 카드별 파일이 `src/DCGO.RL.Engine/CardEffects/{Set}/{Color}/STX_YY.cs` 형태로 48장 모두 존재한다. 원본 DCGO CardEffect 파일이 존재하는 34장은 RL.Engine 카드 파일에 source mapping comment가 있다. 원본 set-local CardEffect 파일이 없는 14장 중 `ST2-07`, `ST3-07`은 shared `ST1_06` 구현 파일이고, 나머지 12장은 명시적 NoEffect marker 파일을 가진다.

## 원본 파일 구조 요약

| Set | Color | 원본 CardEffect `.cs` 수 | 원본 CardEffect 파일이 있는 CardId |
| --- | --- | ---: | --- |
| ST1 | Red | 12 | ST1-01, ST1-03, ST1-06, ST1-07, ST1-08, ST1-09, ST1-11, ST1-12, ST1-13, ST1-14, ST1-15, ST1-16 |
| ST2 | Blue | 11 | ST2-01, ST2-03, ST2-06, ST2-08, ST2-09, ST2-11, ST2-12, ST2-13, ST2-14, ST2-15, ST2-16 |
| ST3 | Yellow | 11 | ST3-01, ST3-04, ST3-05, ST3-08, ST3-09, ST3-11, ST3-12, ST3-13, ST3-14, ST3-15, ST3-16 |
| 합계 | - | 34 | - |

원본 set-local CardEffect 파일이 없는 target CardId는 ST1-02, ST1-04, ST1-05, ST1-10, ST2-02, ST2-04, ST2-05, ST2-07, ST2-10, ST3-02, ST3-03, ST3-06, ST3-07, ST3-10이다. 이 중 `ST2-07`, `ST3-07`은 asset `CardEffectClassName: ST1_06`을 공유 원본 `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_06.cs`로 연결한다. 나머지 12장은 자동 허용이 아니라 명시적 `NoEffectCardScript`와 카드별 marker 파일로 근거를 남겨야 한다.

## RL.Engine 파일 구조 요약

| Set | Color | RL.Engine 카드별 파일 수 | 비고 |
| --- | --- | ---: | --- |
| ST1 | Red | 16 | ST1-01~ST1-16 모두 존재 |
| ST2 | Blue | 16 | ST2-01~ST2-16 모두 존재 |
| ST3 | Yellow | 16 | ST3-01~ST3-16 모두 존재 |
| 합계 | - | 48 | target card pool 전체 |

| 파일 | 현재 책임 | 구조 판단 |
| --- | --- | --- |
| `src/DCGO.RL.Engine/CardEffects/St1CardScriptCatalog.cs` | ST1 script registry 생성 | 적합 |
| `src/DCGO.RL.Engine/CardEffects/St2St3CardScriptCatalog.cs` | ST2/ST3 script registry 생성 및 ST1 결합 registry 생성 | 적합 |
| `src/DCGO.RL.Engine/CardEffects/ST1/Red/St1ScriptSupport.cs` | ST1 shared helper, 일부 공통 body base class | 과도 통합 위험 |
| `src/DCGO.RL.Engine/CardEffects/ST2/Blue/St2ScriptSupport.cs` | ST2 shared helper, 반복 효과 base class | 과도 통합 위험 |
| `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/St3ScriptSupport.cs` | ST3 shared helper, 반복 trigger base class | 과도 통합 위험 |
| `src/DCGO.RL.Engine/CardEffects/StarterScriptSupport.cs` | starter 공통 selection/condition helper | 경계 관리 필요 |

## Catalog 책임 검토 결과

`St1CardScriptCatalog`와 `St2St3CardScriptCatalog`에서 `EffectDescriptor`, `SelectionRequest`, `SelectionResult`, `SelectionContinuation`, `Tier1PrimitiveService`, `ZoneMover`, `TemporaryModifier`, `context.Primitives`, `Resolve`, `Trash(`, `Delete(`, `Destroy(` 패턴을 검색했다. 검색 결과 Catalog 파일에는 위 패턴이 없었고, 현재 Catalog는 script 등록만 수행한다.

## 구조 위반 및 위험 목록

위반 없음:

- 원본 CardEffect 파일이 있는데 RL.Engine 대응 카드 파일이 없는 경우는 발견되지 않았다.
- NoEffect 후보 12장은 모두 RL.Engine 카드별 marker 파일을 가진다.
- 카드별 파일 내 직접 zone list 수정 패턴은 감지되지 않았다.
- ST2/ST3도 `CardEffects/ST2/Blue`, `CardEffects/ST3/Yellow` path를 사용한다.

즉시 주의가 필요한 위험:

- support 파일이 effect body를 숨길 수 있다: `St1OptionDeleteScript`, `St2SourceTrashScript`, `St2NoSourceInheritedContinuousScript`, `St3DpZeroDeletionTriggerScript`, `St3OnAttackDpReductionScript`.
- `StarterScriptSupport`가 카드별 shortcut을 숨기는 helper로 커질 수 있다.
- `ST2-07`, `ST3-07` shared `ST1_06` source-alignment risk는 card-id 기반 `Implemented` script로 해소됐다. `ST3-02` variant risk는 남아 있다.
- `cardeffect-porting-status.md` status table과 실제 registry/file 상태가 drift될 수 있다.

## Queue 43 Guard 결과

queue 43에서 다음 guard를 `src/DCGO.RL.Engine.Tests/Program.cs`에 추가 또는 강화했다.

- `Implemented` script의 concrete class가 대응 카드별 파일에 선언되어 있는지 검증한다.
- `NoEffect` 카드별 파일이 source mapping과 NoEffect 근거만 가진 marker-only 파일인지 검증한다.
- Catalog registry-only 금지 패턴을 `EffectDescriptor`, `CreateSelectionRequest`, `SelectionContinuation`, primitive 호출 계열까지 더 엄격하게 확장했다.

테스트 명령:

```powershell
$env:DOTNET_CLI_HOME='E:\headlessDCGO\.dotnet_home'
$env:NUGET_PACKAGES='E:\headlessDCGO\.nuget\packages'
$env:TEMP='E:\headlessDCGO\.tmp'
$env:TMP='E:\headlessDCGO\.tmp'
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 216 tests passed.` MSBuild `AssemblyReference.cache` 및 `MSBuildTemp` 접근 경고가 있었지만 test runner는 성공 종료했다.

## 다음 Queue 항목

다음 항목은 `44_common_layer_source_mapping_audit.md`다.
