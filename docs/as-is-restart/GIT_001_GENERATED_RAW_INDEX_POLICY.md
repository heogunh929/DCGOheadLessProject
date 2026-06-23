# GIT-001 Generated Raw Index Git Policy

## 목적

GOAL 04/05 이후 생성될 수 있는 대형 generated/raw JSON이 GitHub 일반 Git push를 막지 않도록 `docs/generated/as-is-restart/` 산출물의 Git 추적 정책을 고정한다.

이번 문서는 repository hygiene 정책이며, `src/` 구현 코드, 원본 `DCGO/`, CardEffect body, FND-002/FND-003/FND-001/TRUST-001 작업은 다루지 않는다.

## 기준

- 기준일: `2026-06-23`
- Generated root: `docs/generated/as-is-restart/`
- Script root: `scripts/as-is-restart/`
- GitHub warning 기준: `50MiB` 이상
- GitHub block 기준: `100MiB` 이상
- Manifest: `docs/generated/as-is-restart/git-generated-artifact-manifest.json`

## 정책 정의

| Policy | 의미 | 기본 Git 조치 |
| --- | --- | --- |
| Track | Git에 그대로 보관한다. | 추적 유지 |
| TrackSummaryOnly | summary/manifest/compact governance output만 Git에 보관한다. | 추적 유지 |
| IgnoreRaw | raw full dump이므로 Git 추적에서 제외한다. | `.gitignore` 유지, 필요 시 재생성 |
| SplitCandidate | 100MiB 미만이지만 50MiB 이상 warning 구간이다. | 현재 baseline은 유지하되 다음 재생성 때 50MiB 이하 split |
| LfsCandidate | 정확한 raw payload 보존이 꼭 필요할 때 Git LFS 또는 외부 보관 후보로 둔다. | 일반 Git에는 직접 추가하지 않음 |

## 현재 파일 규모 요약

- 현재 worktree의 `docs/generated/as-is-restart/` 파일 수: 21개
- 현재 HEAD에서 추적 중인 generated/as-is-restart 파일 수: 20개
- 현재 worktree의 100MiB 이상 파일 수: 0개
- 알려진 100MiB 이상 raw index 후보: 2개
- 현재 50MiB 이상 100MiB 미만 파일 수: 3개

## 50MiB 이상 warning 파일

| 파일 | 크기(bytes) | 현재 상태 | 정책 | 조치 |
| --- | ---: | --- | --- | --- |
| `docs/generated/as-is-restart/asis-headless-requirement-matrix.json` | 80,372,008 | tracked | SplitCandidate | 현재 baseline은 유지, 다음 재생성 시 split |
| `docs/generated/as-is-restart/asis-role-reclassification.json` | 63,595,870 | tracked | SplitCandidate | 현재 baseline은 유지, 다음 재생성 시 split |
| `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json` | 61,482,179 | tracked | SplitCandidate | FND-002 입력으로 유지, 다음 재생성 시 split |

## 100MiB 이상 block 파일

현재 worktree와 HEAD에는 아래 두 파일이 없다. 다만 local-only historical branch에 있던 raw dump 크기를 기준으로 block 파일로 기록한다.

| 파일 | 알려진 크기(bytes) | 현재 상태 | 정책 | 조치 |
| --- | ---: | --- | --- | --- |
| `docs/generated/as-is-restart/asis-csharp-call-edge-index.json` | 3,870,427,792 | absent, ignored | IgnoreRaw | 일반 Git 추적 금지 |
| `docs/generated/as-is-restart/asis-csharp-symbol-index.json` | 192,654,059 | absent, ignored | IgnoreRaw | 일반 Git 추적 금지 |

## Git 추적 유지 파일

아래 파일은 현재 baseline 또는 재현용 summary/manifest로 Git 추적을 유지한다.

| 파일 | 정책 |
| --- | --- |
| `docs/generated/as-is-restart/asis-card-data-structure.json` | Track |
| `docs/generated/as-is-restart/asis-cardbaseentity-card-index.json` | Track |
| `docs/generated/as-is-restart/asis-csharp-call-graph.json` | Track |
| `docs/generated/as-is-restart/asis-csharp-file-index.json` | Track |
| `docs/generated/as-is-restart/asis-full-file-inventory.json` | Track |
| `docs/generated/as-is-restart/asis-headless-trust-audit.json` | Track |
| `docs/generated/as-is-restart/asis-implementation-priority-rewrite.json` | Track |
| `docs/generated/as-is-restart/asis-source-of-truth-audit.json` | Track |
| `docs/generated/as-is-restart/asis-card-data-field-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-csharp-call-graph-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-csharp-symbol-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-file-type-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-headless-requirement-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-headless-trust-audit-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-implementation-priority-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-role-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-source-of-truth-audit-summary.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/git-generated-artifact-manifest.json` | TrackSummaryOnly |
| `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json` | SplitCandidate |
| `docs/generated/as-is-restart/asis-headless-requirement-matrix.json` | SplitCandidate |
| `docs/generated/as-is-restart/asis-role-reclassification.json` | SplitCandidate |

## Git 추적 제외 파일

| 파일 | 정책 | 제외 사유 |
| --- | --- | --- |
| `docs/generated/as-is-restart/asis-csharp-call-edge-index.json` | IgnoreRaw | GOAL 05 raw call edge full dump이며 100MiB를 크게 초과한다. |
| `docs/generated/as-is-restart/asis-csharp-symbol-index.json` | IgnoreRaw | GOAL 04 raw symbol full dump이며 100MiB를 초과한다. |

## `.gitignore` 정책

현재 `.gitignore`는 위 두 raw index를 명시적으로 막는다.

```gitignore
docs/generated/as-is-restart/asis-csharp-call-edge-index.json
docs/generated/as-is-restart/asis-csharp-symbol-index.json
```

또한 이후 Goal에서 새 대형 raw dump를 만들 때는 아래 naming/ignore 규칙을 적용한다.

```gitignore
docs/generated/as-is-restart/*-raw-index.json
docs/generated/as-is-restart/*-raw-dump.json
docs/generated/as-is-restart/*-full-dump.json
```

새 generated 파일 작성 규칙:

- 50MiB 미만 summary/manifest/compact index는 `Track` 또는 `TrackSummaryOnly`로 둔다.
- 50MiB 이상 100MiB 미만은 `SplitCandidate`로 기록하고 다음 재생성 때 shard/split 방식을 우선 적용한다.
- 100MiB 이상 raw full dump는 `IgnoreRaw`로 둔다.
- raw full dump가 꼭 필요하면 Git 일반 추적이 아니라 Git LFS 또는 외부 보관을 별도 결정한다.
- 새 raw full dump 파일명은 `*-raw-index.json`, `*-raw-dump.json`, `*-full-dump.json` 중 하나를 사용해 기본 ignore 패턴에 걸리게 한다.
- raw dump와 함께 반드시 summary 또는 manifest를 Git에 남겨 분석 재현 경로를 보존한다.

## `git rm --cached` 필요 여부

현재 HEAD 기준으로 100MiB 초과 generated/raw JSON은 추적 중이지 않다. 따라서 지금 필요한 `git rm --cached` 명령은 없다.

만약 이후 실수로 추적되면 파일 자체를 삭제하지 말고 아래 명령만 실행한다.

```powershell
git rm --cached docs/generated/as-is-restart/asis-csharp-call-edge-index.json
git rm --cached docs/generated/as-is-restart/asis-csharp-symbol-index.json
```

## Script 정책

`scripts/as-is-restart/`의 재현용 script source는 Git에 남긴다.

- `*.csproj`, `Program.cs`: 추적 유지
- `bin/`, `obj/`: 기존 `**/bin/`, `**/obj/` ignore 규칙으로 제외
- script 실행 산출물 중 raw full dump는 `docs/generated/as-is-restart/` naming/ignore 규칙을 따른다.

## FND-002 영향

FND-002는 unknown common API 27개 source-aligned mapping 작업이다. 이 작업에는 Git에 추적된 summary, manifest, script, 그리고 현재 warning 구간이지만 추적 유지 중인 `asis-csharp-unresolved-calls.json`을 입력으로 사용할 수 있다.

`asis-csharp-symbol-index.json`과 `asis-csharp-call-edge-index.json` raw full dump는 FND-002의 Git-tracked 필수 입력이 아니므로, 두 파일을 `IgnoreRaw`로 둬도 다음 작업으로 넘어갈 수 있다.

## 완료 판정

- generated/as-is-restart 파일별 크기와 정책은 `git-generated-artifact-manifest.json`에 기록했다.
- 50MiB 이상 파일과 100MiB 이상 파일을 분리했다.
- 100MiB 초과 raw index는 `.gitignore`로 차단했다.
- summary/manifest/script는 추적 대상으로 유지한다.
- raw full dump는 Git 일반 추적 제외 대상으로 정책화했다.
- 현재 필요한 `git rm --cached`는 없다.
- 코드 구현 변경, DCGO 원본 변경, commit/push는 수행하지 않았다.
