# 검증 전략

최신 기준일: 2026-06-13

검증의 목적은 Unity 원본 battle 로직과 RL.Engine의 rule-visible state 차이를 찾는 것이다. self-play/trace는 엔진 완성 전 학습 데이터가 아니라 검증 데이터로만 사용한다.

## 최신 ST1 검증 상태

- ST1 target deck validation: 통과
- Unsupported card/effect: 0
- PartiallyImplemented card/effect: 0
- EngineCompletionChecklistRunner failed gate: 0
- 전체 테스트: `All 170 tests passed.`

## 실행 명령

```powershell
$env:DOTNET_CLI_HOME='E:\headlessDCGO\.dotnet_home'
$env:NUGET_PACKAGES='E:\headlessDCGO\.nuget\packages'
$env:TEMP='E:\headlessDCGO\.tmp'
$env:TMP='E:\headlessDCGO\.tmp'
.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj
```

MSBuild cache/temp access denied warning이 있었지만 test runner는 성공 종료했다.

## ST1-12 추가 검증

- security check에서 ST1-12가 `Security -> Executing -> BattleArea`로 이동하는지 확인.
- play-self tamer가 memory cost를 지불하지 않는지 확인.
- play 성공 후 checked card가 trash로 이동하지 않는지 확인.
- ST1-12 field aura가 owner turn 조건에서만 effective DP에 반영되는지 확인.
- `TriggerPipelineService`가 explicit executing source의 `SecuritySkill` descriptor를 실행할 수 있는지 확인.
- field가 가득 찬 경우 ST1-12 security effect가 실행되지 않고 checked card가 trash로 이동하는지 확인.

## 유지되는 주요 검증 축

- forbidden dependency check
- deck setup/draw/security determinism
- deck-out loss
- normal play/digivolve/pass/memory crossing
- DP battle, security attack, direct attack win
- ZoneMover와 `CardInstance.CurrentZone` invariant
- Permanent top/source/linked state invariant
- `TemporaryModifier` cleanup/state hash/replay determinism
- continuous effective stat determinism
- selection request/result validation
- unsupported mechanic explicit failure
- random legal action invariant smoke

## ST1-ST3 validation gate design

2026-06-14 ST1-ST3 inventory 단계에서는 validation 구조만 설계한다. 구현 코드와 ST2/ST3 deck 통과 처리는 다음 단계로 미룬다.

Planning note: 2026-06-14 현재 요청은 문서화 전용 inventory/pass plan 생성이다. 로컬 작업트리에 이전 ST2/ST3 구현 변경이 남아 있을 수 있지만, 이번 단계에서는 ST1-ST3 target pool validation을 새로 통과시키려 하지 않고 아래 gate shape만 고정한다.

Planned ST1-ST3 validation output:

- target card pool count: 48 cards.
- cards with non-empty `CardEffectClassName`: 36 cards.
- unique CardEffect discovery count under ST1/ST2/ST3: 34 files.
- NoEffect candidate count with empty `CardEffectClassName`: 12 cards.
- per-card expected status: `NoEffect`, `ImplementableNow`, `NeedsCommonLayer`, `NeedsComplexMechanic`, or `Unsupported`.
- unsupported/missing layer report grouped by reusable engine capability.
- ST1 baseline regression result kept separate from ST2/ST3 expansion failures.
- explicit no-RL-training check.
- explicit DCGO Unity source unchanged check.

The gate must fail explicitly while ST2/ST3 missing layers remain. A NoEffect card can pass only when the absence of a Unity CardEffect file has been documented. Unsupported ST2/ST3 effects must not be represented as silent no-op CardScripts.

Suggested validation batches are now fixed in `docs/rl-engine/st1-st3-porting-plan.md` as Implementation Pass 01 through Implementation Pass 09. In particular, `ST2-07` and `ST3-07` must be validated as shared `ST1_06` effect-class mappings, not NoEffect cards.

No code test command was required for this document-only inventory task.

## StateHash 기준

StateHash에는 원천 state가 포함된다.

- turn player, phase, memory, turn count
- player zone ordering과 card instance id
- permanent top/source/linked/suspended state
- public security 정보와 hidden placeholder
- pending decision/selection request stable id
- active `TemporaryModifier`

continuous effect는 derived value이므로 별도 저장하지 않는다. 대신 effective stat 계산 determinism을 테스트한다.

## 다음 검증 보강 후보

- ST1-12 security play 후 aura가 실제 DP battle 결과를 바꾸는 golden scenario
- ST1-11 dynamic SecurityAttack이 security check 횟수에 반영되는 golden scenario
- full `MultipleSkills` priority와 동시 trigger 선택 순서 검증
- counter/block 세부 timing end-to-end 검증
- Unity 원본 trace와 RL.Engine trace의 구조화 비교 harness
