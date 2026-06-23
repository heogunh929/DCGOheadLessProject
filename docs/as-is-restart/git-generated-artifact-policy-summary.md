# Git Generated Artifact Policy Summary

- 기준 Goal: GIT-001
- 기준 root: `docs/generated/as-is-restart/`
- 상세 정책: `docs/as-is-restart/GIT_001_GENERATED_RAW_INDEX_POLICY.md`
- Manifest: `docs/generated/as-is-restart/git-generated-artifact-manifest.json`

## 결론

100MiB 초과 raw full dump는 Git 일반 추적 대상이 아니다. 현재 HEAD에는 100MiB 초과 generated/raw JSON이 추적되어 있지 않으며, `git rm --cached`가 필요한 파일도 없다.

## 50MiB 이상 파일

| 파일 | 크기(bytes) | 정책 |
| --- | ---: | --- |
| `docs/generated/as-is-restart/asis-headless-requirement-matrix.json` | 80,372,008 | SplitCandidate |
| `docs/generated/as-is-restart/asis-role-reclassification.json` | 63,595,870 | SplitCandidate |
| `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json` | 61,482,179 | SplitCandidate |

## 100MiB 이상 파일

| 파일 | 알려진 크기(bytes) | 상태 | 정책 |
| --- | ---: | --- | --- |
| `docs/generated/as-is-restart/asis-csharp-call-edge-index.json` | 3,870,427,792 | absent, ignored | IgnoreRaw |
| `docs/generated/as-is-restart/asis-csharp-symbol-index.json` | 192,654,059 | absent, ignored | IgnoreRaw |

## 추적 유지

- `Track`: 현재 8개
- `TrackSummaryOnly`: 현재 10개
- `SplitCandidate`: 현재 3개
- `scripts/as-is-restart/**/*.csproj`, `scripts/as-is-restart/**/Program.cs`

`SplitCandidate` 파일은 현재 baseline으로 유지하되, 다음 재생성 때 50MiB 이하 shard/split 정책을 적용한다.

## 추적 제외

- `docs/generated/as-is-restart/asis-csharp-call-edge-index.json`
- `docs/generated/as-is-restart/asis-csharp-symbol-index.json`
- 이후 생성되는 `docs/generated/as-is-restart/*-raw-index.json`
- 이후 생성되는 `docs/generated/as-is-restart/*-raw-dump.json`
- 이후 생성되는 `docs/generated/as-is-restart/*-full-dump.json`

## FND-002 이동 가능 여부

FND-002로 넘어가도 된다. FND-002는 tracked summary, manifest, scripts, `asis-csharp-unresolved-calls.json`을 사용할 수 있고, 100MiB 초과 raw symbol/call-edge dump는 Git-tracked 필수 입력이 아니다.

## 추천 commit message

`docs: define generated raw index git policy`
