# FND-003-A Implementable Foundation Task Split

## Progress Update

- `FND-003-B` `OnRemovedField` primitive scope has been completed after this split.
- `FND-003-C` `AfterPayCost` runtime scope has been completed after `FND-003-B`.
- `FND-003-D` `OnDiscardSecurity` primitive event scope has been completed after `FND-003-C`.
- `FND-003-E` `OnAddSecurity` primitive event scope has been completed after `FND-003-D`.
- `FND-003-F` `OnDiscardLibrary` primitive event scope has been completed after `FND-003-E`.
- Queue JSON now marks `FND-003-B` as `CompletedPrimitiveScope`, `FND-003-C` as `CompletedRuntimeScope`, `FND-003-D` as `CompletedPrimitiveScope`, `FND-003-E` as `CompletedPrimitiveScope`, `FND-003-F` as `CompletedPrimitiveScope`, and points `nextImmediateGoal` to `FND-003-G` `OnUseOption`.
- Verification: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 616 tests passed.`
- Foundation Gate remains `OpenCodeReady=false`; no generated status promotion was performed.

> FND-003-A는 FND-003의 `ImplementableFoundationTask` 15개를 다음 구현 goal에서 바로 꺼낼 수 있는 작은 source-aligned goal queue로 분해한 산출물이다. 이번 작업에서는 구현, 테스트 결과 조작, Foundation Gate 수치 변경, generated status 승격을 수행하지 않았다.

## 기준선

- Source root: `E:/headlessDCGO/DCGO`
- FND-003 input: `docs/generated/as-is-restart/fnd-003-unsupported-capability-remediation.json`
- FND-003 next action queue: `docs/generated/as-is-restart/fnd-003-next-action-queue.json`
- DATA-001 policy: `docs/generated/as-is-restart/data-001-card-base-entity-policy.json`
- OpenCodeReady: `False`

## 분해 결과

| 항목 | 수 |
| --- | ---: |
| Parent ImplementableFoundationTask | 15 |
| Split child goals | 45 |
| SourceContract goals | 15 |
| RuntimeIntegration goals | 15 |
| TestAndParityCandidate goals | 15 |
| Low risk parent goals | 4 |
| Medium risk parent goals | 6 |
| High risk parent goals | 5 |

## 분해 정책

- parent goal은 affected card 수와 source file 수가 낮은 순서로 정렬한다.
- 각 parent goal은 `SourceContract`, `RuntimeIntegration`, `TestAndParityCandidate` 3개 child goal로 나눈다.
- 모든 child goal은 FND-003의 source enum/runtime call/sample source path evidence를 그대로 보존한다.
- DATA-001 정책에 따라 CardBaseEntity 샘플은 `CardID` 단독이 아니라 asset-level definition evidence로 취급한다.
- 구현 goal에서는 하나씩 처리하고, 완료 후에만 테스트 및 Foundation Gate 재계산 후보로 기록한다.

## Parent Goal Queue

| Goal | Capability | Risk | Affected cards | Source files | Child goals |
| --- | --- | --- | ---: | ---: | --- |
| FND-003-B | OnRemovedField | Low | 4 | 2 | FND-003-B-S, FND-003-B-R, FND-003-B-T |
| FND-003-C | AfterPayCost | Low | 15 | 7 | FND-003-C-S, FND-003-C-R, FND-003-C-T |
| FND-003-D | OnDiscardSecurity | Low | 23 | 14 | FND-003-D-S, FND-003-D-R, FND-003-D-T |
| FND-003-E | OnAddSecurity | Low | 38 | 14 | FND-003-E-S, FND-003-E-R, FND-003-E-T |
| FND-003-F | OnDiscardLibrary | Medium | 51 | 20 | FND-003-F-S, FND-003-F-R, FND-003-F-T |
| FND-003-G | OnUseOption | Medium | 65 | 30 | FND-003-G-S, FND-003-G-R, FND-003-G-T |
| FND-003-H | OnUnTappedAnyone | Medium | 70 | 29 | FND-003-H-S, FND-003-H-R, FND-003-H-T |
| FND-003-I | OnMove | Medium | 79 | 30 | FND-003-I-S, FND-003-I-R, FND-003-I-T |
| FND-003-J | OnAddDigivolutionCards | Medium | 102 | 50 | FND-003-J-S, FND-003-J-R, FND-003-J-T |
| FND-003-K | OnDigivolutionCardDiscarded | Medium | 121 | 53 | FND-003-K-S, FND-003-K-R, FND-003-K-T |
| FND-003-L | OnEndBattle | High | 160 | 84 | FND-003-L-S, FND-003-L-R, FND-003-L-T |
| FND-003-M | OnDetermineDoSecurityCheck | High | 228 | 119 | FND-003-M-S, FND-003-M-R, FND-003-M-T |
| FND-003-N | BeforePayCost | High | 284 | 141 | FND-003-N-S, FND-003-N-R, FND-003-N-T |
| FND-003-O | OnTappedAnyone | High | 306 | 139 | FND-003-O-S, FND-003-O-R, FND-003-O-T |
| FND-003-P | OnDeclaration | High | 578 | 298 | FND-003-P-S, FND-003-P-R, FND-003-P-T |

## Child Goal Queue

| Goal | Capability | Type | Source-aligned task |
| --- | --- | --- | --- |
| FND-003-B-S | OnRemovedField | SourceContract | actual field removal 완료 후 payload와 would-remove cut-in과의 관계를 고정한다. |
| FND-003-B-R | OnRemovedField | RuntimeIntegration | CardObjectController OnRemovedField call parity fixture task를 분리한다. |
| FND-003-B-T | OnRemovedField | TestAndParityCandidate | OnRemovedField를 WhenRemoveField alias로 처리하지 않는 negative task를 둔다. |
| FND-003-C-S | AfterPayCost | SourceContract | actual payment 이후 cut-in boundary와 paid cost payload를 정의한다. |
| FND-003-C-R | AfterPayCost | RuntimeIntegration | BeforePayCost와 같은 cost transaction id를 공유하는 task를 분리한다. |
| FND-003-C-T | AfterPayCost | TestAndParityCandidate | post-cost trigger order fixture task를 둔다. |
| FND-003-D-S | OnDiscardSecurity | SourceContract | security discard primitive payload를 security card snapshot과 source effect로 고정한다. |
| FND-003-D-R | OnDiscardSecurity | RuntimeIntegration | security trash/recovery flow와 OnLoseSecurity/OnDiscardSecurity ordering task를 분리한다. |
| FND-003-D-T | OnDiscardSecurity | TestAndParityCandidate | security discard replay fixture task를 정의한다. |
| FND-003-E-S | OnAddSecurity | SourceContract | security add primitive payload와 face-up/down state를 분리한다. |
| FND-003-E-R | OnAddSecurity | RuntimeIntegration | OnAddSecurity와 OnFaceUpSecurityIncreased ordering fixture task를 정의한다. |
| FND-003-E-T | OnAddSecurity | TestAndParityCandidate | security count/hash/replay payload task를 둔다. |
| FND-003-F-S | OnDiscardLibrary | SourceContract | deck trash primitive payload를 DiscardedCards/CardEffect로 고정한다. |
| FND-003-F-R | OnDiscardLibrary | RuntimeIntegration | mill event source collection과 deck exhaustion edge case task를 분리한다. |
| FND-003-F-T | OnDiscardLibrary | TestAndParityCandidate | OnDiscardLibrary trigger stack fixture task를 정의한다. |
| FND-003-G-S | OnUseOption | SourceContract | option card executing zone 이동, Root, Cost, Card payload boundary를 정의한다. |
| FND-003-G-R | OnUseOption | RuntimeIntegration | OnUseOption/background OnUseOption/OptionSkill 순서 task를 분리한다. |
| FND-003-G-T | OnUseOption | TestAndParityCandidate | executing source cleanup과 interruption/resume negative fixture task를 둔다. |
| FND-003-H-S | OnUnTappedAnyone | SourceContract | WhenUntapAnyone cut-in 뒤 actual unsuspend event를 별도 payload로 고정한다. |
| FND-003-H-R | OnUnTappedAnyone | RuntimeIntegration | CanUnsuspend/effect immunity 재검증 후 OnUnTappedAnyone stack task를 정의한다. |
| FND-003-H-T | OnUnTappedAnyone | TestAndParityCandidate | suspend/unsuspend replay trace payload fixture task를 분리한다. |
| FND-003-I-S | OnMove | SourceContract | move event payload에 moved permanent, old/new zone, battle-area survival recheck를 포함한다. |
| FND-003-I-R | OnMove | RuntimeIntegration | CardObjectController/CardEffectFactory CanTriggerOnMove parity fixture task를 분리한다. |
| FND-003-I-T | OnMove | TestAndParityCandidate | ZoneMover event bridge와 stale target negative case를 별도 task로 둔다. |
| FND-003-J-S | OnAddDigivolutionCards | SourceContract | source add primitive payload를 Permanent/CardEffect/CardSources/isFromSameDigimon/isFromDigimon으로 고정한다. |
| FND-003-J-R | OnAddDigivolutionCards | RuntimeIntegration | face-down/source-bottom add와 inherited source refresh task를 분리한다. |
| FND-003-J-T | OnAddDigivolutionCards | TestAndParityCandidate | source add 후 trigger stack order fixture task를 정의한다. |
| FND-003-K-S | OnDigivolutionCardDiscarded | SourceContract | would-discard cut-in과 actual source trash event를 별도 task로 분리한다. |
| FND-003-K-R | OnDigivolutionCardDiscarded | RuntimeIntegration | DiscardedCards snapshot payload와 rollback/resume 불가 조건을 정의한다. |
| FND-003-K-T | OnDigivolutionCardDiscarded | TestAndParityCandidate | source trash primitive가 ZoneMover 경유로 event를 발행하는 fixture task를 둔다. |
| FND-003-L-S | OnEndBattle | SourceContract | battle end payload를 winner/loser/tie/battle snapshot으로 고정한다. |
| FND-003-L-R | OnEndBattle | RuntimeIntegration | attack cleanup 이전/이후 trigger 순서 fixture task를 분리한다. |
| FND-003-L-T | OnEndBattle | TestAndParityCandidate | battle object lifetime과 stale permanent handling task를 정의한다. |
| FND-003-M-S | OnDetermineDoSecurityCheck | SourceContract | battle security-check 결정 payload에 attacker, defender, battle object, security-check count를 고정한다. |
| FND-003-M-R | OnDetermineDoSecurityCheck | RuntimeIntegration | Pierce/security check modifier source collection task를 분리한다. |
| FND-003-M-T | OnDetermineDoSecurityCheck | TestAndParityCandidate | security check 변경 후 OnSecurityCheck와의 순서 fixture task를 정의한다. |
| FND-003-N-S | BeforePayCost | SourceContract | pay-cost state machine의 pre-cost collection, background effects, cut-in 순서를 task로 분리한다. |
| FND-003-N-R | BeforePayCost | RuntimeIntegration | cost modifier source scope와 max payable memory validation task를 분리한다. |
| FND-003-N-T | BeforePayCost | TestAndParityCandidate | Digisorption/DigiXros/option play cost와 공유 가능한 CostResolver hook contract를 정의한다. |
| FND-003-O-S | OnTappedAnyone | SourceContract | suspend primitive의 actual suspend 후 event payload를 Permanents/IsBlock/CardEffect로 고정한다. |
| FND-003-O-R | OnTappedAnyone | RuntimeIntegration | block suspend와 effect-caused suspend를 구분하는 trigger source task를 만든다. |
| FND-003-O-T | OnTappedAnyone | TestAndParityCandidate | OnTappedAnyone queue/replay payload fixture를 추가 task로 정의한다. |
| FND-003-P-S | OnDeclaration | SourceContract | declarable effect discovery contract를 CardSource/Permanent source collection 단위로 문서화한다. |
| FND-003-P-R | OnDeclaration | RuntimeIntegration | legal action/DecisionPoint payload에 source card, root, once-per-turn key, activation owner를 포함하는 task로 분리한다. |
| FND-003-P-T | OnDeclaration | TestAndParityCandidate | TurnStateMachine command 선택과 headless action generation parity fixture 범위를 정의한다. |

## 다음 즉시 실행 후보

1. `FND-003-G` `OnUseOption`: affected cards 65, source files 30
2. `FND-003-H` `OnUnTappedAnyone`: affected cards 70, source files 29
3. `FND-003-I` `OnMove`: affected cards 79, source files 30

## 금지 확인

- `src` 구현 코드 수정 없음
- 원본 `DCGO` 수정 없음
- CardEffect body 구현 없음
- C0039 이후 card-porting 실행 없음
- Foundation Gate 수치 조작 없음
- generated status 승격 없음
- commit/push 없음

## 산출물

- `docs/as-is-restart/FND_003_A_IMPLEMENTABLE_FOUNDATION_TASK_SPLIT.md`
- `docs/as-is-restart/fnd-003-a-implementable-foundation-task-split-summary.md`
- `docs/generated/as-is-restart/fnd-003-a-implementable-foundation-task-split.json`
- `docs/generated/as-is-restart/fnd-003-a-foundation-goal-queue.json`
