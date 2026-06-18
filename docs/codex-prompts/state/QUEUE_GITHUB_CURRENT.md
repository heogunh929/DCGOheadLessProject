# GitHub Current Source-Aligned Queue

이 queue는 GitHub main과 로컬 상태를 재정렬하고, 앞으로의 이식을 원본 구조와 최대한 비슷하게 유지하기 위한 queue다.

상태 값: `todo`, `in-progress`, `done`, `blocked`, `needs-review`.

| Order | Status | Prompt file | 목적 |
| ---: | --- | --- | --- |
| 40 | done | prompts/40_github_local_state_reconcile.md | GitHub main과 로컬 상태 비교, 현재 기준점 확정 |
| 41 | done | prompts/41_stale_docs_reconciliation.md | 과거 ST1 실패 문구와 최신 상태 문서 정합성 정리 |
| 42 | done | prompts/42_cardeffect_file_layout_audit.md | 카드별 파일 구조와 Catalog 역할 감사 |
| 43 | done | prompts/43_cardeffect_file_layout_guard_tests.md | 구조 guard 테스트 추가/강화 |
| 44 | done | prompts/44_common_layer_source_mapping_audit.md | 공통 layer가 원본 공통 처리와 대응되는지 감사 |
| 45 | done | prompts/45_st1_st3_completion_gate_reconcile.md | ST1~ST3 validation/report/gate 정합성 검사 |
| 46 | done | prompts/46_golden_scenario_gap_plan.md | 원본 trace/golden scenario 보강 계획 |
| 47 | todo | prompts/47_next_porting_work_plan.md | 이후 ST1~ST3 또는 다음 카드풀 진행 계획 생성 |
| 90 | support | prompts/90_handoff_update_github_current.md | ChatGPT handoff 갱신 |
| 91 | support | prompts/91_checkpoint_github_current.md | 검수 후 로컬 checkpoint commit |
