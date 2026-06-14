# Common Layer Source Mapping

작성일: 2026-06-14

이 문서는 source-aligned queue 37의 감사 결과다. 목적은 RL.Engine의 공통 레이어가 원본 DCGO Unity battle 코드의 어떤 책임에서 왔는지 추적 가능하게 만들고, 카드별 효과 구현이 공통 helper 안에 숨어 원본 파일 구조와 멀어지는 위험을 식별하는 것이다.

이번 작업은 문서 감사만 수행했다. 카드 효과 구현, ST2/ST3 구현, RL 학습 구성, 기존 `DCGO/Assets/Scripts` 원본 수정은 하지 않았다.

## 요약 판정

- 핵심 공통 service인 `TriggerPipelineService`, `SelectionResultApplicator`, `SecurityEffectExecutionService`, `DurationCleanupService`, `ContinuousEffectService`, `Tier1PrimitiveService`, `ZoneMover`, battle/phase service에는 특정 ST 카드 ID shortcut이 보이지 않는다.
- source-aligned 위험은 core service보다 `StarterScriptSupport`, `St1ScriptSupport`, `St2ScriptSupport`, `St3ScriptSupport` 같은 support helper에 있다. 이 파일들은 반복 구현을 줄이지만 카드별 body를 숨길 수 있다.
- `St1OptionDeleteScript`에는 `Porting.CardId == "ST1-15"` 분기가 남아 있다. 현재 기능 shortcut은 아니고 prompt/debug label 분기지만, 카드별 문구가 support helper에 남아 있는 명확한 통폐합 위험이다.
- `TriggerPipelineService`는 원본 `AutoProcessing`/`MultipleSkills`의 전부가 아니라 headless용 부분 대응이다. full priority, 동시 trigger 선택, cut-in/counter 세부 priority는 아직 partial로 문서화해야 한다.
- `ContinuousEffectService`는 field top/inherited 기반 derived stat 계산에 집중되어 있다. 원본의 모든 player-level/hand/trash/security face-up continuous source를 포괄한다고 보아서는 안 된다.

## 공통 레이어 매핑 표

| RL.Engine layer | 원본 DCGO source mapping | 원본 책임 | 현재 RL.Engine 책임 | 구조 판정 | 남은 gap |
| --- | --- | --- | --- | --- | --- |
| `TriggerPipelineService` | `AutoProcessing.GetSkillInfos`, `AutoProcessing.StackSkillInfos`, `AutoProcessing.TriggeredSkillProcess`, `MultipleSkills.ActivateMultipleSkills`, `SkillInfo` | timing별 effect 수집, stack/background 분리, 복수 skill 처리, activation 후속 timing | source zone에서 descriptor 수집, `TriggerCollector`/`EffectQueue` drain, optional/selection boundary 유지, once-per-turn 등록, invariant hook 호출 | 부분 적합 | 원본 `MultipleSkills`의 turn player/non-turn player priority, 동시 trigger 선택 UI, cut-in/chain priority는 미완성 |
| `SelectionResultApplicator` | `SelectCardEffect`, `SelectPermanentEffect`, `ActivateEffectProcess`의 선택 결과 callback 흐름 | UI 선택 후보 생성, 선택 종료 조건, 선택 결과를 effect continuation에 전달 | `SelectionRequest`와 `SelectionResult` 검증, Min/Max/skip/candidate/stale target 확인, continuation 적용 | 적합 | 원본 UI 표시 metadata 전체는 아직 단순화되어 있음 |
| `TemporaryModifier`, `DurationCleanupService` | `CardEffectCommons`의 일시 DP/SecurityAttack 적용, `TurnStateMachine`/`AttackProcess`의 until-end cleanup list | 일정 timing까지 modifier 유지 후 제거 | 상태에 저장되는 temporary modifier 모델, turn/battle/security cleanup, stale target 정리 | 부분 적합 | 원본 `UntilNextUntapEffects`, counter/cut-in 전용 duration hook은 범위 밖 |
| `ContinuousEffectService`, `ContinuousEffectDescriptor` | `AutoProcessing.GetSkillInfos(EffectTiming.None)`, background/continuous effect, `Permanent`/`Player` effective stat 계산 | field/inherited/tamer 등 지속 효과를 현재 상태에서 계산 | field top/inherited source에서 continuous descriptor 수집, effective DP/SecurityAttack 파생 계산 | 부분 적합 | hand/trash/player-level/face-up security continuous source는 일반화되지 않음 |
| `SecurityEffectExecutionService` | `CardController.ISecurityCheck.SecurityCheck`, security skill 선택, option security의 main effect activation | security card reveal/check 중 security skill 실행 및 후속 zone 처리 | `SecuritySkill` descriptor 실행, `ActivateMainOption`을 `OptionSkill` 경로로 재사용, selection applicator 연결 | 부분 적합 | multiple security skill 선택 priority, security battle의 전체 edge case는 `SecurityCheckService`와 함께 추가 검증 필요 |
| `Tier1PrimitiveService` | `CardEffectCommons`, `CardController`, `CardObjectController`, `Player`, `Permanent`, `AttackProcess`의 공통 조작 | draw/search/reveal/trash/delete/recover/memory/source 이동/play/digivolve/battle 등 카드 효과 공통 primitive | 카드별 body가 직접 zone list를 건드리지 않도록 명시 command와 validation 제공 | 적합하나 넓음 | 메서드가 계속 늘면 원본 mapping 주석과 capability별 테스트가 필요 |
| `ZoneMover` | `CardObjectController.RemoveFromAllArea`, zone add/remove, `Permanent` top/source/link 이동 | 카드가 여러 zone에 중복 존재하지 않게 이동 처리 | 모든 zone 이동의 단일 primitive, `CardInstance.CurrentZone`/permanent membership 일관성 유지 | 적합 | 원본 UI frame/preferred placement와 1:1은 아니며 deterministic headless placement 정책 필요 |
| `AttackService`, `BattleResolver`, `SecurityCheckService` | `AttackProcess`, `IBattle`, `ISecurityCheck`, blocker/security battle 흐름 | 공격 선언, blocker, battle, security check, attack end cleanup | attack/security/battle 흐름을 service로 분리하고 trigger/security service 연결 | 부분 적합 | counter timing, attack target change, blocker priority, security multiple choice는 partial |
| `PhaseRunner`, `RuleProcessor` | `TurnStateMachine`, `AutoProcessing.AutoProcessCheck`, rules timing | phase transition, start/end timing, rule process, DP 0 처리 | start turn/main/end turn hook, rule processing, duration cleanup 연결 | 부분 적합 | breeding/main UI 단계, pay-cost 전후/counter 세부 hook은 아직 완전하지 않음 |
| `StarterScriptSupport` | `CardEffectCommons`, `SelectCardEffect`, `SelectPermanentEffect` 중 starter 카드 공통 후보/선택 helper | 반복되는 target 조건과 선택 request 구성 | ST1~ST3 starter 카드에서 재사용되는 candidate/selection helper 제공 | 과도 통합 위험 | 카드별 body를 숨기지 않도록 helper를 candidate/validation 수준으로 제한해야 함 |
| `St1ScriptSupport` | ST1 카드 효과 파일들의 반복 body 및 `CardEffectCommons` 호출 패턴 | ST1 option delete, inherited continuous, declared capability helper | ST1 카드별 파일에서 호출하는 반복 script 구현 | 과도 통합 위험 | `Porting.CardId == "ST1-15"` 분기 제거 필요. 카드별 prompt/label은 카드 파일 또는 생성자 config로 이동 권장 |
| `St2ScriptSupport`, `St3ScriptSupport` | ST2/ST3 원본 카드 효과의 반복 선택/continuous/trigger body | source trash, no-source inherited continuous, DP zero/on-attack DP 등 반복 body | set별 parameterized script helper | 중간 위험 | 카드 파일이 timing/조건/amount/source mapping을 계속 노출해야 하며 helper가 커지면 card-specific 파일로 분리 필요 |

## 통폐합 위험 목록

| 위치 | 관찰 내용 | 위험 | 권장 조치 |
| --- | --- | --- | --- |
| `src/DCGO.RL.Engine/CardEffects/ST1/Red/St1ScriptSupport.cs` | `St1OptionDeleteScript.CreateSelectionRequest`가 `Porting.CardId == "ST1-15"`로 prompt text를 분기한다. | support helper가 카드별 차이를 직접 알기 시작했다는 신호다. | 기능 변경 없이 후속 refactor에서 카드별 파일/생성자 config로 문구와 source mapping을 넘기도록 이동한다. |
| `St1OptionDeleteScript` | ST1-15/16 option delete body를 하나의 helper로 통합한다. | 원본 카드별 body 차이가 커지면 shortcut으로 변질될 수 있다. | 현재처럼 deletion amount/조건이 동일한 범위만 허용하고, 차이가 생기면 카드별 script로 분리한다. |
| `StarterScriptSupport` | opponent no-source, owner battle area, playable evolution source 등 다양한 선택 후보 helper가 한 파일에 모인다. | 원본 `CardEffectCommons`보다 넓은 starter 전용 helper가 되어 카드 body 가독성을 떨어뜨릴 수 있다. | card-specific body는 카드 파일에 남기고, 이 파일은 candidate/query/request factory 수준으로 제한한다. |
| `St2ScriptSupport`, `St3ScriptSupport` | `EffectDescriptor` 생성과 primitive 호출이 set helper에 들어 있다. | queue 36 guard를 통과해도 실제 card body가 helper에 숨어 원본 파일 대비 추적성이 낮아질 수 있다. | 각 카드 파일에 timing, 조건, amount, 원본 mapping을 남기고 helper에는 중복 없는 generic body만 둔다. |
| `TriggerPipelineService` | `AutoProcessing`, `MultipleSkills`, optional/selection/once-per-turn/invariant가 한 service에 결합되어 있다. | 테스트가 통과해도 원본 priority와 stack behavior가 생략된 사실이 묻힐 수 있다. | docs와 tests에서 `partial MultipleSkills priority`를 명시하고, priority 구현 시 별도 service로 분리 검토한다. |
| `Tier1PrimitiveService` | 많은 primitive가 한 service에 모인다. | CardEffectCommons 대응이지만 커질수록 source mapping이 흐려질 수 있다. | primitive별 원본 mapping과 invariant test를 유지한다. card id 조건은 절대 넣지 않는다. |

## Generic helper와 카드별 body 경계

공통 service에 남겨도 되는 것:

- zone membership, permanent top/source/link 일관성 검증
- `SelectionRequest`/`SelectionResult` 공통 검증
- timing별 descriptor 수집/queue drain
- duration cleanup, continuous stat 계산, state hash/invariant hook
- draw/search/reveal/trash/delete/recover/memory/source 이동 같은 원본 `CardEffectCommons`급 primitive
- 특정 카드 ID를 모르는 candidate query와 target validation

카드별 파일에 남겨야 하는 것:

- 어떤 `EffectTiming`에 descriptor를 제공하는지
- 원본 CardEffect source mapping
- target 조건의 카드별 의미
- amount, duration scope, optional 여부, once-per-turn key
- 원본 효과 문구 또는 card-specific prompt/debug label
- Implemented/Partial/Unsupported 판단 근거

## Deferred Fixes

1. `St1OptionDeleteScript`의 `Porting.CardId == "ST1-15"` 분기를 카드별 configuration으로 이동한다.
2. `StarterScriptSupport`를 candidate/query helper와 effect body helper로 더 명확히 나눌지 검토한다.
3. `St2ScriptSupport`/`St3ScriptSupport`가 커질 경우 카드별 script 파일로 body를 되돌리고 helper에는 primitive wrapper만 남긴다.
4. `TriggerPipelineService`의 `MultipleSkills` priority 미지원 범위를 validation report에 더 직접 노출한다.
5. `Tier1PrimitiveService` primitive별 source mapping comment 또는 별도 표를 확장한다.

## 테스트 상태

이번 queue 37 작업은 문서 감사만 수행했다. 구현 코드와 테스트 코드를 변경하지 않았으므로 전체 테스트는 실행하지 않았다.

직전 queue 36 기준 최신 테스트 기록은 다음과 같다.

```powershell
.\.dotnet\dotnet.exe run --no-restore --project .\src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj
```

결과: `All 212 tests passed.`

