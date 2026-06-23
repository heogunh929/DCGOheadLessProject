# AS-IS Implementation Priority Rewrite Summary

- 생성 시각(UTC): `2026-06-23T06:23:23.8215416+00:00`
- AS-IS root: `E:\headlessDCGO\DCGO`
- 우선순위 항목 수: 9
- Immediate: 5
- ReviewNext: 1
- AfterFoundation: 1
- Deferred: 2
- 100MB 초과 generated 파일: 2

## 우선순위
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

## 다음 작업
- 다음 실제 작업은 FND-001 ContinuousOrStaticEffect partial closure를 더 작은 source-aligned foundation task로 쪼개는 것이다.
- FND-002/FND-003은 Foundation Gate 실패 수치를 직접 줄이는 작업이며 GOAL 09 기준 최우선이다.
- DATA-001은 구현 전 데이터 정책 감사로, GOAL 06 anomaly와 GOAL 07 Required CardData를 묶는다.
- TRUST-001은 기존 src 구현을 재사용할지 버릴지 결정하기 전에 SourceOfTruth mapping과 generated truth를 맞추는 작업이다.
- GIT-001은 GitHub 업로드 전 별도 처리해야 한다.
