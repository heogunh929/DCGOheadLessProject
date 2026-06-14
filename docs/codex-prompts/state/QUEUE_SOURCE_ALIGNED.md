# QUEUE_SOURCE_ALIGNED

이 queue는 ST1~ST3 확장 전, 현재 GitHub main 기준의 구조/문서/검증 정렬을 위한 작업 목록이다.

## Queue

### 34
- status: done
- prompt: docs/codex-prompts/prompts/34_github_current_state_audit.md
- name: GitHub main 및 로컬 현재 상태 감사

### 35
- status: done
- prompt: docs/codex-prompts/prompts/35_porting_structure_audit.md
- name: 원본 구조 동등성 감사

### 36
- status: done
- prompt: docs/codex-prompts/prompts/36_cardeffect_file_layout_guard.md
- name: CardEffect 파일 구조 guard 강화

### 37
- status: done
- prompt: docs/codex-prompts/prompts/37_common_layer_source_mapping_audit.md
- name: 공통 layer source mapping 감사

### 38
- status: done
- prompt: docs/codex-prompts/prompts/38_document_state_reconciliation.md
- name: 문서 최신 상태/과거 상태 정합성 정리

### 39
- status: skipped-done
- prompt: docs/codex-prompts/prompts/39_source_aligned_checkpoint.md
- name: source-aligned 상태 checkpoint 준비
- skip reason: 38번 완료 후 사용자가 이미 로컬 checkpoint commit `3b993b34 202606142346`을 생성했으므로 39번 checkpoint 목적은 충족됨.

## Support Prompt

### 90
- status: support
- prompt: docs/codex-prompts/prompts/90_handoff_update_source_alignment.md
- name: ChatGPT handoff 갱신
