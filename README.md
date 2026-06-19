# headlessDCGO GitHub 상태 정렬 / 구조 이식 프롬프트 확장

## 현재 Queue - Engine Parity 47+

현재 후속 작업은 `a101acd2 20260618 local latest` 기준의 engine-parity queue를 사용한다. 목적은 ST1~ST3 registry/status/file validation을 전체 엔진 완성으로 오해하지 않고, 원본 DCGO Unity battle 로직과 headless `RL.Engine`의 의미 차이를 줄이는 것이다.

처음 한 번:

```text
/mention docs/codex-prompts/GOAL_ENGINE_PARITY_47_PLUS.md
/mention docs/codex-prompts/ACTIVE/RUN_NEXT_ENGINE_PARITY.md
/mention docs/codex-prompts/state/QUEUE_ENGINE_PARITY.md
/mention docs/codex-prompts/templates/engine_parity_common_constraints.md
```

이후에는 다음 한 줄만 반복한다.

```text
다음 engine-parity queue 작업을 진행해.
```

관련 문서:

- `docs/codex-prompts/README_ENGINE_PARITY_QUEUE.md`
- `docs/codex-prompts/state/QUEUE_ENGINE_PARITY.md`
- `docs/codex-prompts/prompts/INDEX_ENGINE_PARITY.md`

기존 source-aligned/github-current queue는 historical 전환 경로로 유지한다.

이 패키지는 기존 `docs/codex-prompts/prompts/00_...20_...` 구조에 이어서 사용하는 확장 프롬프트입니다.

목적은 ST2/ST3를 무작정 더 구현하는 것이 아니라, 현재 GitHub main 상태를 기준으로 다음을 점검/정렬하는 것입니다.

1. GitHub main의 실제 코드/문서 상태 확인
2. 원본 DCGO와 최대한 유사한 카드별 파일 구조 유지
3. Catalog는 registry 역할만 담당하게 유지
4. 공통 service는 원본 공통 처리에 대응되는 경우만 유지
5. 문서의 과거 상태/최신 상태 혼재 정리
6. ST1~ST3 확장 전에 구조 검증 guard 강화

## 적용 위치

`headlessDCGO` 루트에서 압축을 풀면 다음 경로에 파일이 들어가도록 설계했습니다.

```text
docs/codex-prompts/
├─ GOAL_SOURCE_ALIGNED_PORTING.md
├─ ACTIVE/RUN_NEXT_SOURCE_ALIGNED.md
├─ state/QUEUE_SOURCE_ALIGNED.md
└─ prompts/
   ├─ 34_github_current_state_audit.md
   ├─ 35_porting_structure_audit.md
   ├─ 36_cardeffect_file_layout_guard.md
   ├─ 37_common_layer_source_mapping_audit.md
   ├─ 38_document_state_reconciliation.md
   ├─ 39_source_aligned_checkpoint.md
   └─ 90_handoff_update_source_alignment.md
```

## Codex 사용

처음 한 번:

```text
/mention docs/codex-prompts/GOAL_SOURCE_ALIGNED_PORTING.md
/mention docs/codex-prompts/ACTIVE/RUN_NEXT_SOURCE_ALIGNED.md
/mention docs/codex-prompts/state/QUEUE_SOURCE_ALIGNED.md
```

그 다음 `GOAL_SOURCE_ALIGNED_PORTING.md` 안의 `/goal` 문구를 설정합니다.

이후에는 다음 한 줄만 반복합니다.

```text
다음 source-aligned queue 작업을 진행해.
```

## 주의

- 이 확장은 기존 `QUEUE.md`를 덮어쓰지 않습니다.
- ST1~ST3 구현을 계속하기 전에 구조/문서/검증 guard를 정렬하는 목적입니다.
- GitHub main 상태와 로컬 상태가 다르면 Codex가 먼저 차이를 보고해야 합니다.
