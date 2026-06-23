# GOAL 09 AS-IS Implementation Priority Rewrite

이번 문서는 GOAL 01-08 기준선을 바탕으로 개발 재시작용 구현 우선순위를 다시 작성한 결과다.
구현, CardEffect body 추가, C0039 이후 card-porting, generated status 승격, RL 학습 구성요소 구현은 수행하지 않았다.

## 입력
- AS-IS root: `E:\headlessDCGO\DCGO`
- GOAL 07 summary: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-headless-requirement-summary.json`
- GOAL 08 trust audit: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\as-is-restart\asis-headless-trust-audit-summary.json`
- Foundation gate: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\foundation-completion-gate.json`
- Mechanic-first scheduler: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\capability-truth-audit\mechanic-first-scheduler-66E.json`
- Source-required capabilities: `C:\Users\HG\.codex\worktrees\793a\headlessDCGO\docs\generated\capability-truth-audit\source-required-capabilities.json`

## 정책
GOAL 09는 구현을 수행하지 않고 우선순위 기준선만 재작성한다. 기존 구현/문서는 historical reference이며 SourceOfTruth, GOAL 07 matrix, GOAL 08 trust audit, Foundation Gate를 우선 증거로 사용한다.
- OpenCodeReady=false 동안 card-porting batch와 RL 학습 구성요소는 Deferred로 둔다.
- Required 파일 수보다 Foundation Gate blocker 해소를 우선한다.
- local src 구현은 reuse candidate이지 completion evidence가 아니다.
- Generated status/registry는 수동 승격하지 않는다.
- 대용량 generated raw index는 push 전에 repository hygiene 항목으로 처리한다.

## 전체 요약
| 항목 | 값 |
| --- | --- |
| Priority items | 9 |
| Immediate | 5 |
| ReviewNext | 1 |
| AfterFoundation | 1 |
| Deferred | 2 |
| Failed gates | 3 |
| Unknown common API | 27 |
| Unsupported capability | 26 |
| PartiallyImplemented capability | 37 |
| Large generated files | 2 |

## 우선순위 항목
| Rank | ID | Lane | Status | Title | Completion evidence |
| --- | --- | --- | --- | --- | --- |
| 1 | FND-001 | FoundationGate | Immediate | ContinuousOrStaticEffect partial closure | 해당 capability의 partial 사유가 구체 하위 작업으로 소거되거나 명시 blocker queue로 분리되고 Foundation Gate 수치가 재계산된다. |
| 2 | FND-002 | FoundationGate | Immediate | Unknown common API 27건 source-aligned mapping | unknown common API가 capability matrix 또는 explicit unsupported queue로 모두 분류된다. |
| 3 | FND-003 | FoundationGate | Immediate | Unsupported capability 26건 remediation 분해 | 각 unsupported capability가 구현 가능 foundation prompt, partial 상태, 또는 명시 blocked 상태로 재분류된다. |
| 4 | TRUST-001 | TrustAudit | Immediate | 기존 src 구현 trust boundary 고정 | 기존 src 구현은 reuse candidate, blocked, stale, verified-by-test 후보로 분리되고 SourceOfTruth mapping evidence를 가진다. |
| 5 | DATA-001 | SourceData | ReviewNext | CardBaseEntity import/variant/data anomaly policy | variant identity, CardIndex, CardID, EffectClassName source candidate 정책이 GOAL 08 trust boundary와 합쳐진다. |
| 6 | PARITY-001 | Validation | AfterFoundation | full-card parity evidence NotRun 해소 계획 | source-locked Unity fixture/RL fixture/comparison report 생성 정책과 실행 기준이 정의된다. |
| 7 | GIT-001 | RepositoryHygiene | Immediate | 100MB 초과 generated raw index 업로드 정책 | 대용량 raw index가 git tracking에서 제거되거나 LFS/외부 보관 정책으로 명시 처리된다. |
| 8 | DEFER-CARDPORTING | Deferred | Deferred | C0039 이후 card-porting batch | OpenCodeReady=true, failed gate 0, 사용자 명시 승인 후에만 재개한다. |
| 9 | DEFER-RL | Deferred | Deferred | RL Environment/Observation/Reward/Dataset/Trainer | 엔진 foundation completion과 parity/invariant evidence 이후 별도 목표로 시작한다. |

## Top capability blockers
| Capability | Status | Affected cards | Source effects | Card batches |
| --- | --- | --- | --- | --- |
| ContinuousOrStaticEffect | PartiallyImplemented | 7,867 | 3,896 | 397 |
| OncePerTurn | PartiallyImplemented | 7,323 | 3,627 | 397 |
| ZoneMovement | PartiallyImplemented | 7,024 | 3,504 | 374 |
| InheritedSource | PartiallyImplemented | 4,491 | 2,308 | 378 |
| OnEnterFieldAnyone | PartiallyImplemented | 4,140 | 2,041 | 352 |
| Selection.SelectPermanent | PartiallyImplemented | 4,101 | 2,128 | 373 |
| Selection.SelectCard | PartiallyImplemented | 3,541 | 1,749 | 371 |
| OptionalDecision | PartiallyImplemented | 3,207 | 1,582 | 349 |
| SkippableEffect | PartiallyImplemented | 3,079 | 1,577 | 354 |
| Selection.SelectSecurity | PartiallyImplemented | 2,588 | 1,282 | 335 |
| WhenDigivolvingTrigger | PartiallyImplemented | 2,505 | 1,190 | 313 |
| OnPlayTrigger | PartiallyImplemented | 2,114 | 1,085 | 316 |
| OnAllyAttack | PartiallyImplemented | 1,936 | 945 | 241 |
| SecuritySkill | PartiallyImplemented | 1,880 | 894 | 226 |
| Selection.SelectHand | PartiallyImplemented | 1,835 | 943 | 323 |
| DurationModifier | PartiallyImplemented | 1,219 | 648 | 294 |
| OnDestroyedAnyone | PartiallyImplemented | 1,158 | 623 | 277 |
| OptionSkill | PartiallyImplemented | 939 | 522 | 130 |
| OnDeclaration | Unsupported | 578 | 298 | 143 |
| OnEndTurn | PartiallyImplemented | 549 | 249 | 163 |
| OnStartMainPhase | PartiallyImplemented | 483 | 222 | 131 |
| WhenPermanentWouldBeDeleted | Unsupported | 405 | 206 | 45 |
| OnStartTurn | PartiallyImplemented | 322 | 120 | 85 |
| OnTappedAnyone | Unsupported | 306 | 139 | 97 |
| WhenRemoveField | Unsupported | 304 | 164 | 87 |

## 100MB 초과 generated 파일
| 경로 | 크기(bytes) | 처리 필요 |
| --- | --- | --- |
| docs/generated/as-is-restart/asis-csharp-call-edge-index.json | 3,967,922,702 | GitHub 일반 git push 제한 초과. raw generated artifact이므로 추적 제거, LFS, 압축/분할, 외부 보관 중 정책 결정 필요. |
| docs/generated/as-is-restart/asis-csharp-symbol-index.json | 198,142,371 | GitHub 일반 git push 제한 초과. raw generated artifact이므로 추적 제거, LFS, 압축/분할, 외부 보관 중 정책 결정 필요. |

## 금지 사항 준수
- `src/` C# 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- generated status 승격 없음.
- RL Environment/Observation/Reward/Dataset/Trainer 구현 없음.
- commit/push 없음.
