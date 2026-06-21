# DCGO Full Card Pool Queue

상태 값: `todo`, `in-progress`, `done`, `blocked`, `needs-review`, `support`.

| Order | Status | Prompt file | 목적 |
| --- | --- | --- | --- |
| 51 | done | prompts/51_runtime_composition_guard.md | production runtime composition 및 silent trigger skip 방지 |
| 52 | done | prompts/52_ruleprocessor_zone_mover_injection.md | core dependency/ZoneMover 주입 정렬 |
| 52A | done | prompts/52A_engine_decision_resume_boundary.md | 모든 timing의 공통 pending decision pause/resume |
| 52B | done | prompts/52B_runtime_state_decision_token_hardening.md | runtime rule state와 DecisionToken replay hardening |
| 52C | done | prompts/52C_resumable_runner_continuation.md | Scripted/Random runner 재개 가능한 continuation/session API |
| 52D | done | prompts/52D_runner_result_snapshot_one_shot_boundary.md | runner result snapshot과 one-shot Run boundary hardening |
| 53 | done | prompts/53_security_trigger_timing_parity.md | security timing 원본 정렬 |
| 53A | done | prompts/53A_source_aligned_security_multi_check_correction.md | source-aligned security multi-check state machine 보정 |
| 54 | done | prompts/54_after_effects_multiple_skills_priority.md | AfterEffectsActivate/MultipleSkills priority |
| 54A | done | prompts/54A_source_aligned_trigger_stack_frame_correction.md | source-aligned trigger stack frame, nested trigger, RuleProcess, AfterEffectsActivate 보정 |
| 54B | done | prompts/54B_trigger_stack_semantic_hardening.md | trigger stack semantic hardening: empty batch AfterEffects 금지, ordering/own decision 분리, RuleProcessor event frame 보존 |
| 55 | done | prompts/55_counter_block_attack_target_timing.md | counter/block/attack target timing |
| 55A | done | prompts/55A_source_aligned_attack_lifecycle_correction.md | source-aligned attack lifecycle: single-counter policy, runtime context, switch/block/suspend, duration cleanup |
| 55B | done | prompts/55B_counter_source_collection_attack_stage_ordering_correction.md | counter source collection and attack stage ordering correction |
| 55C | done | prompts/55C_counter_resolution_target_switch_event_semantic_correction.md | counter resolution 시점, optional decline 진행, source coverage, target-switch event payload 보정 |
| 55D | done | prompts/55D_counter_execution_persistence_target_switch_stage_resume_correction.md | counter 실행 persistence와 target-switch stage resume 보정 |
| 61 | done | prompts/61_dcgo_source_snapshot_pin.md | DCGO2/DCGO 원본 revision 고정. 현재 로컬 manifest snapshot 사용자 승인 완료 |
| 62 | done | prompts/62_full_card_asset_manifest.md | 전체 CardBaseEntity asset manifest 생성 완료. 8,186 gameplay records, manifest SHA-256 `a254765908d21d436dece44216858cd0e70ec14776a4107f9532a4df969ad269` |
| 63 | done | prompts/63_full_mechanic_effect_inventory.md | 전체 mechanic/effect timing inventory 생성 완료. inventory SHA-256 `e8fd1723d947f14e49cdc1250e0e146092a1e7010cce2833b5dde4f28e836c27`, missing/partial candidates 64 |
| 56 | done | prompts/56_golden_scenarios_batch1.md | engine-core golden scenario 1차 완료. ST1/ST2/ST3 대표 5개 scenario를 action/selection trace와 replay final hash baseline으로 고정 |
| 57 | done | prompts/57_parity_trace_contract_and_exporter.md | rule-visible parity trace 계약과 RL.Engine exporter 구현 완료 |
| 58 | done | prompts/58_parity_fixture_comparer.md | Unity/RL parity fixture comparer 구현 완료. missing Unity fixture는 NotRun 처리 |
| 54C | done | prompts/54C_aftereffects_fingerprint_background_state_hardening.md | AfterEffects fingerprint/background state hardening |
| 59A | done | prompts/59A_engine_core_milestone_gate.md | ST1~ST3 engine-core milestone gate |
| 60A | done | prompts/60A_engine_core_expansion_readiness.md | 전체 카드풀 inventory 진입 readiness |
| 64 | done | prompts/64_full_per_card_source_scaffold.md | 전체 카드/source-effect 파일 scaffold |
| 65 | done | prompts/65_full_card_pool_validation_baseline.md | 전체 카드풀 explicit Unsupported baseline |
| 66 | done | prompts/66_generate_full_card_porting_batches.md | 실제 카드 포팅 subqueue 자동 생성 |
| 67 | todo | prompts/67_full_card_porting_batch_completion_audit.md | 생성된 batch queue 완료 감사 |
| 68 | todo | prompts/68_full_card_pool_source_alignment_audit.md | 전체 source/asset/registry/status 정합성 |
| 69 | todo | prompts/69_full_card_pool_golden_parity_coverage.md | 전체 mechanic family golden/parity coverage |
| 70 | todo | prompts/70_full_dcgo_snapshot_completion_gate.md | Full DCGO Snapshot Completion Gate |
| 71 | todo | prompts/71_generate_rl_or_remediation_queue.md | gate 결과에 따른 RL 또는 remediation queue 생성 |
| 90 | support | prompts/90_handoff_update_dcgo_full_card_pool.md | ChatGPT handoff 갱신 |
