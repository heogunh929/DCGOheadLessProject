# Engine Parity Queue 47+

상태 값: `todo`, `in-progress`, `done`, `blocked`, `needs-review`, `support`.

| Order | Status | Prompt file | 목적 |
| ---: | --- | --- | --- |
| 47 | done | prompts/47_next_porting_work_plan.md | 현재 기준점 검증, 기존 queue 전환, README/INDEX 정렬 |
| 48 | todo | prompts/48_asset_effect_mapping_reconcile.md | ST2-07/ST3-07 shared effect와 ST3-02 variant 원본 mapping 정리 |
| 49 | todo | prompts/49_asset_registry_mapping_validator.md | 원본 asset ↔ registry/status/file 자동 대조 validator |
| 50 | todo | prompts/50_option_execution_lifecycle_parity.md | Option Hand→Executing→OptionSkill→Trash 원본 lifecycle 정렬 |
| 51 | todo | prompts/51_runtime_composition_guard.md | TriggerPipeline 없는 runtime 구성과 silent trigger skip 금지 |
| 52 | todo | prompts/52_ruleprocessor_zone_mover_injection.md | RuleProcessor와 core service dependency/ZoneMover 주입 정렬 |
| 53 | todo | prompts/53_security_trigger_timing_parity.md | OnSecurityCheck/OnLoseSecurity/SecuritySkill/cleanup 순서 정렬 |
| 54 | todo | prompts/54_after_effects_multiple_skills_priority.md | AfterEffectsActivate와 동시 trigger priority 모델 |
| 55 | todo | prompts/55_counter_block_attack_target_timing.md | counter/block/attack target change timing 보강 |
| 56 | todo | prompts/56_golden_scenarios_batch1.md | 우선순위 golden scenario 5종 구현 |
| 57 | todo | prompts/57_parity_trace_contract_and_exporter.md | Unity/RL 공용 rule-visible trace 계약과 RL exporter |
| 58 | todo | prompts/58_parity_fixture_comparer.md | Unity trace fixture와 RL trace comparer/report |
| 59 | todo | prompts/59_whole_engine_completion_gate_v1.md | ST1~ST3 scope whole-engine completion gate v1 |
| 60 | todo | prompts/60_final_regression_fuzz_readiness_review.md | 전체 regression/fuzz와 RL 단계 진입 전 readiness 검수 |
| 90 | support | prompts/90_handoff_update_engine_parity.md | ChatGPT 공유용 handoff 갱신 |
| 91 | support | prompts/91_checkpoint_engine_parity.md | 사용자 검수 후 로컬 checkpoint commit |
