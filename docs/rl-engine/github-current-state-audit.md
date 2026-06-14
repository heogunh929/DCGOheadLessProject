# GitHub Current State Audit

## Current Snapshot - 2026-06-14

이 문서는 source-aligned queue 34의 historical audit 기록이다. queue 38 기준 최신 해석은 다음과 같다.

- cached `origin/main`/로컬 기준점은 `a20f045a chore: checkpoint porting structure guards`로 유지한다. `git fetch/pull/push`는 실행하지 않았다.
- queue 34 이후 queue 35~38 문서/구조 guard 변경이 로컬 작업트리에 commit 전 상태로 남아 있다.
- queue 34에서 지적한 문서 충돌은 queue 38에서 각 문서 상단의 `Current Snapshot`과 `Historical` 섹션으로 분리했다.
- `DCGO/Assets/Scripts` 원본 변경은 없다.
- latest recorded structure guard는 `All 212 tests passed.`다.

아래 내용은 queue 34 실행 당시의 관찰 기록이며, 최신 상태로 직접 인용할 때는 위 snapshot과 이후 queue 문서를 함께 확인해야 한다.

작성일: 2026-06-14

## 목적

source-aligned queue 34 작업의 산출물이다. 이번 작업은 구현이 아니라 현재 로컬 작업공간과 GitHub main 비교 가능성을 감사하고, 다음 ST1~ST3 작업을 어떤 기준으로 진행해야 하는지 정리한다.

금지 사항 준수:

- 코드 수정 없음.
- ST2/ST3 신규 구현 없음.
- 기존 `DCGO/` Unity 원본 수정 없음.
- `git fetch`, `git pull`, `git push` 실행 없음.
- commit 생성 없음.

## 시작 Git 상태

작업 시작 시 실행한 명령:

```powershell
git status --short
git diff --stat
git diff --name-only
git remote -v
git log --oneline -3
git status --short -- DCGO
git status --short -- DCGO\Assets\Scripts
```

시작 상태 요약:

- 현재 branch: `main`
- 최신 로컬 commit: `a20f045a chore: checkpoint porting structure guards`
- 이전 commit: `da36ff22 Initial upload without DCGO folder`
- `DCGO/` 변경: 없음
- `DCGO/Assets/Scripts` 변경: 없음
- remote:
  - `origin https://github.com/heogunh929/DCGOheadLessProject.git (fetch)`
  - `origin https://github.com/heogunh929/DCGOheadLessProject.git (push)`

작업 시작 시 uncommitted 변경:

```text
 M README.md
?? README_back.md
?? docs/codex-prompts/ACTIVE/
?? docs/codex-prompts/GOAL_SOURCE_ALIGNED_PORTING.md
?? docs/codex-prompts/prompts/34_github_current_state_audit.md
?? docs/codex-prompts/prompts/35_porting_structure_audit.md
?? docs/codex-prompts/prompts/36_cardeffect_file_layout_guard.md
?? docs/codex-prompts/prompts/37_common_layer_source_mapping_audit.md
?? docs/codex-prompts/prompts/38_document_state_reconciliation.md
?? docs/codex-prompts/prompts/39_source_aligned_checkpoint.md
?? docs/codex-prompts/prompts/90_handoff_update_source_alignment.md
?? docs/codex-prompts/state/
?? docs/codex-prompts/templates/
```

`README.md` diff:

```text
README.md | 86 ++++++++++++++++++++++++++++-----------------------------------
1 file changed, 38 insertions(+), 48 deletions(-)
```

이 변경들은 이번 queue 작업 시작 전에 이미 존재했다. 이번 감사에서는 `docs/rl-engine/github-current-state-audit.md` 생성과 queue 상태 갱신 외에는 기존 변경을 수정하지 않는다.

## GitHub main 비교 가능성

추가로 실행한 로컬-only 명령:

```powershell
git log --oneline -5
git branch -vv
git rev-parse main
git rev-parse --verify origin/main
```

결과:

| 항목 | 값 |
| --- | --- |
| 로컬 `main` | `a20f045a4cd7251e4158b0f02126d458feddb863` |
| cached `origin/main` | `a20f045a4cd7251e4158b0f02126d458feddb863` |
| `git branch -vv` | `main a20f045a [origin/main] chore: checkpoint porting structure guards` |
| 로컬 기준 ahead/behind | 표시 없음 |

판정:

- 로컬에 캐시된 `origin/main` ref는 현재 `main`과 일치한다.
- 하지만 이번 목표와 LOCAL_GIT_GUIDE에 따라 `git fetch`/`pull`을 실행하지 않았으므로, 실제 GitHub remote의 최신 main이 여전히 같은지 네트워크 기준으로 확정할 수는 없다.
- 따라서 이후 작업 기준은 `a20f045a chore: checkpoint porting structure guards` 로컬 commit 및 현재 cached `origin/main` 일치 상태로 둔다.

## CardEffect 파일 존재 감사

확인 명령:

```powershell
Test-Path src\DCGO.RL.Engine\CardEffects\ST1\Red\ST1_08.cs
Test-Path src\DCGO.RL.Engine\CardEffects\ST2\Blue\ST2_01.cs
Test-Path src\DCGO.RL.Engine\CardEffects\ST3\Yellow\ST3_13.cs
Get-ChildItem src\DCGO.RL.Engine\CardEffects\ST1\Red -Filter ST1_*.cs | Measure-Object
Get-ChildItem src\DCGO.RL.Engine\CardEffects\ST2\Blue -Filter ST2_*.cs | Measure-Object
Get-ChildItem src\DCGO.RL.Engine\CardEffects\ST3\Yellow -Filter ST3_*.cs | Measure-Object
```

결과:

| 항목 | 결과 |
| --- | --- |
| `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_08.cs` | 존재 |
| `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_01.cs` | 존재 |
| `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_13.cs` | 존재 |
| ST1 `ST1_*.cs` 파일 수 | 16 |
| ST2 `ST2_*.cs` 파일 수 | 11 |
| ST3 `ST3_*.cs` 파일 수 | 11 |

해석:

- ST1은 `ST1-01`부터 `ST1-16`까지 카드별 파일이 모두 존재한다.
- ST2/ST3는 현재 effect-bearing 카드 중심 파일만 존재한다. NoEffect 카드별 marker 파일은 아직 없다.
- 현재 정책 문서의 "카드별 파일 구조" 원칙을 ST2/ST3 NoEffect까지 강제할지 여부는 다음 구조 작업에서 확정해야 한다.

## Catalog 책임 감사

확인 명령:

```powershell
rg "new EffectDescriptor|SelectionRequest|SelectionResult|Tier1PrimitiveService|context\.Primitives|Trash\(|Delete\(|Destroy\(" `
  src\DCGO.RL.Engine\CardEffects\St1CardScriptCatalog.cs `
  src\DCGO.RL.Engine\CardEffects\St2St3CardScriptCatalog.cs -n
```

결과:

- 매치 없음.

판정:

- `St1CardScriptCatalog`와 `St2St3CardScriptCatalog`는 현재 registry 등록 이상의 effect body를 포함하지 않는 것으로 보인다.
- 이 상태는 `PortingStructure ST1 catalog is registry only` 테스트와도 일치한다.

## 문서 상태 감사

아래 문서들에서 최신 상태와 과거/계획 상태가 섞여 있는 부분이 확인됐다.

| 문서 | 확인된 상태 | 조치 권고 |
| --- | --- | --- |
| `docs/rl-engine/cardeffect-porting-status.md` | ST1은 통과로 기록되어 있고, ST1~ST3 target validation도 통과로 기록되어 있다. 그러나 상단 테스트 수치는 과거 `All 170 tests passed.`이고 하단에는 `All 202 tests passed.`가 추가되어 있다. | Current Snapshot / Historical Notes 분리 필요 |
| `docs/rl-engine/validation-strategy.md` | 상단은 ST1 기준 `All 170 tests passed.`, 후반 구조 검증 섹션은 `All 202 tests passed.`이다. 또한 ST1~ST3 gate design은 "구현을 다음 단계로 미룸/expected fail" 문구를 유지한다. | 현재 기준과 계획 기준을 분리해야 함 |
| `docs/rl-engine/engine-completion-report-st1-st3.md` | ST1~ST3 completion report가 planning-only/expected fail 기준으로 작성되어 있다. 현재 commit에는 ST2/ST3 카드 파일과 테스트가 포함되어 있어 문서와 코드 기준이 어긋날 수 있다. | queue 38 문서 정합성 작업에서 갱신 필요 |
| `docs/rl-engine/effect-system.md` | ST1~ST3 inventory stage, historical local worktree implementation note, planning baseline override가 함께 존재한다. 현재 `a20f045a` commit 이후 어느 섹션이 authoritative인지 불명확하다. | authoritative current section 추가 필요 |
| `docs/rl-engine/porting-structure-audit.md` | "local uncommitted implementation" 표현이 남아 있다. 현재 해당 구조 변경은 `a20f045a`에 commit되었다. | queue 38에서 표현 수정 필요 |

중요 모순:

- `cardeffect-porting-status.md`는 ST1~ST3 target validation 통과를 말한다.
- `engine-completion-report-st1-st3.md`와 `validation-strategy.md` 일부는 ST1~ST3 validation을 "설계만 됨 / expected fail"로 말한다.
- 실제 최신 로컬 commit은 ST1~ST3 파일과 테스트를 포함하며, 마지막 실행 결과는 `All 202 tests passed.`이다.

이번 queue 34에서는 문서 모순을 수정하지 않고 목록화만 한다.

## 구조상 즉시 멈출 위험 여부

즉시 멈춰야 할 정도의 구조 위반은 확인되지 않았다.

근거:

- `DCGO/Assets/Scripts` 변경 없음.
- cached `origin/main`과 로컬 `main` commit 일치.
- ST1 카드별 파일 16개 존재.
- 예시 ST2/ST3 카드 파일 존재.
- Catalog에서 effect body 의심 snippet 없음.
- 구조 guard 테스트가 이전 작업 기준 `All 202 tests passed.`로 기록되어 있다.

주의:

- ST2/ST3 NoEffect 카드별 marker 파일 부재는 정책 범위 확정이 필요하다.
- ST2-07/ST3-07 shared `ST1_06` mapping 문제는 `porting-structure-audit.md`에 이미 위험으로 기록되어 있다.
- 문서 정합성은 다음 queue item 전에 정리해야 할 가능성이 높다.

## 다음 작업 권고

queue 기준 다음 item은 35번이다.

```text
35
prompt: docs/codex-prompts/prompts/35_porting_structure_audit.md
name: 원본 구조 동등성 감사
```

권고:

1. 35번은 이미 존재하는 `porting-structure-audit.md`와 현재 commit 상태를 기준으로 재감사한다.
2. ST2/ST3 기능 구현은 계속 보류한다.
3. 36번에서 ST2/ST3 NoEffect marker 및 shared-effect guard를 강화할지 결정한다.
4. 38번에서 문서의 "계획/과거/현재" 상태를 정리한다.

## 테스트

이번 작업은 감사 문서 생성만 수행했다. 새 코드 변경은 없으므로 테스트는 실행하지 않았다.

직전 구조 검증 작업의 최신 결과는 다음과 같다.

```powershell
.\\.dotnet\\dotnet.exe run --no-restore --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

결과: `All 202 tests passed.`

## 결론

현재 source-aligned 작업 기준점은 로컬 `main` commit `a20f045a chore: checkpoint porting structure guards`다. cached `origin/main`도 같은 commit을 가리킨다. 다만 fetch 금지 때문에 실제 GitHub main 최신 상태를 원격에서 재확인하지는 않았다.

구조상 즉시 중단해야 할 위반은 없지만, ST1~ST3 완료/계획 문서가 서로 충돌한다. 다음 단계는 ST2/ST3 구현이 아니라 35번 구조 감사와 38번 문서 정합성 정리다.
