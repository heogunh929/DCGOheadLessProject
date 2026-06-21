# 59A Engine-Core Milestone Gate

작성일: 2026-06-21

## 판정

| 항목 | 값 |
| --- | --- |
| 대상 | ST1~ST3 engine-core milestone |
| 판정 | `NeedsReview` |
| Core parity blocked | false |
| Full card pool inventory blocked | false |
| RL environment design ready | false |

이 gate는 ST1~ST3를 이용해 공통 engine core가 전체 카드풀 inventory로 확장될 수 있는지 점검하는 중간 milestone이다. 전체 DCGO snapshot completion gate가 아니며, `ObservationEncoder`, `RewardCalculator`, `DatasetExporter`, `Trainer`, RL Environment API, self-play dataset 구현 승인도 아니다.

## Gate Evidence

| Gate | Status | Evidence |
| --- | --- | --- |
| runtime composition graph | Passed | `BattleEngineServices.ValidationReport`, runtime composition tests |
| shared dependency identity | Passed | shared `ZoneMover`, `TriggerPipelineService`, `Tier1PrimitiveService`, `SecurityCheckService` graph validation |
| RuleProcessor/ZoneMover injection | Passed | injected `ZoneMover` link trim tests |
| common decision pause/resume | Passed | option/security/rules/phase/attack selection replay tests |
| option Executing lifecycle | Passed | Hand -> Executing -> OptionSkill -> Trash and rollback tests |
| security timing | Passed | source-aligned multi-check, dynamic `SecurityAttack`, per-card cleanup, snapshot, replay |
| MultipleSkills/AfterEffects | Passed | trigger stack frame, ordering, source revalidation, AfterEffects fingerprint/background state tests |
| counter/block/target timing | Passed | counter source collection, counter resolution, block, switch, OnEndAttack context tests |
| ST1~ST3 structure/source mapping | Passed | card file layout guards, registry-only catalog, source metadata preservation |
| asset registry validator | NeedsReview | `ST3-02` P2 source body unresolved |
| golden scenario batch 1 | Passed | ST1/ST2/ST3 representative golden replay baseline |
| replay determinism | Passed | action/selection/phase/attack/security/rules timing replay tests |
| invariant fuzz | Passed | random legal action invariant smoke and failure-capture test |
| parity trace contract/comparer | Passed for contract/comparer surface | missing Unity fixture is `NotRun`, not parity pass |
| forbidden dependency | Passed | no Unity/Photon dependency in RL.Engine |
| silent skip zero | Passed | unsupported/legacy no-effect silent paths fail explicitly |

## Source Finding

`ST3-02` remains visible and unresolved:

| Variant | CardIndex | CardEffectClassName | 판정 |
| --- | ---: | --- | --- |
| base | 76 | empty | NoEffect candidate |
| P1 | 77 | empty | NoEffect candidate |
| P2 | 4977 | `ST3_02` | source body unconfirmed, needs-review |

이 finding은 full card pool inventory 자체를 막는 성격은 아니지만, `ST3-02` P2 variant implementation과 whole-engine/full snapshot completion, RL environment design을 막는다. source body 없이 효과를 추측 구현하지 않는다.

## 결론

59A의 결론은 `NeedsReview`이다. Core parity evidence는 현재 회귀 테스트와 구조 guard로 충분히 고정되었지만, source data finding이 남아 있으므로 `EngineComplete`, `ReadyForRL`, `FullSnapshotComplete`로 승격하지 않는다.

Machine-readable report: `docs/generated/engine-core-milestone-gate-59A.json`
