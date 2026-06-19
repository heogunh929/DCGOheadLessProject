# Engine Parity Queue 47+

이 queue는 GitHub `main`의 `a101acd2 (20260618 local latest)` 이후 상태를 기준으로,
DCGO Unity battle 로직을 headless `RL.Engine`으로 최대한 같은 의미와 구조로 이식하기 위한 후속 작업 묶음이다.

## 현재 기준

- github-current queue 40~46 완료
- 다음 기존 항목은 47번
- ST1~ST3 카드별 파일/marker 구조 존재
- Catalog는 registry 역할만 수행
- ST1 target deck gate 통과
- ST1~ST3 48장 target pool registry/status/file/deck validation 범위 통과
- 전체 엔진 completion은 아직 미실행
- 최신 기록 테스트: `All 216 tests passed.`
- RL 학습 구성은 아직 금지

## 이 queue의 목표

이 queue는 카드를 더 많이 추가하는 작업보다 다음 원본 동등성 문제를 우선 해결한다.

1. 원본 asset의 `CardEffectClassName`과 RL registry/status/file mapping 정합성
2. Option 카드의 `Hand -> Executing -> OptionSkill -> Trash` lifecycle
3. trigger pipeline 필수 구성과 silent skip 제거
4. `RuleProcessor`/`ZoneMover` 의존성 일관성
5. security check 관련 timing 순서
6. `MultipleSkills`, `AfterEffectsActivate`, 동시 trigger priority
7. counter/block/attack-target timing
8. ST1~ST3 end-to-end golden scenarios
9. Unity/RL rule-visible trace 비교 기반
10. ST1~ST3 scope engine-core completion gate

## 적용 위치

압축을 `headlessDCGO` 프로젝트 루트에 풀면 다음 위치에 파일이 추가된다.

```text
docs/codex-prompts/
├─ GOAL_ENGINE_PARITY_47_PLUS.md
├─ README_ENGINE_PARITY_QUEUE.md
├─ ACTIVE/
│  └─ RUN_NEXT_ENGINE_PARITY.md
├─ state/
│  ├─ QUEUE_ENGINE_PARITY.md
│  └─ PROGRESS_ENGINE_PARITY.md
├─ templates/
│  └─ engine_parity_common_constraints.md
└─ prompts/
   ├─ 47_next_porting_work_plan.md
   ├─ 48_asset_effect_mapping_reconcile.md
   ├─ 49_asset_registry_mapping_validator.md
   ├─ 50_option_execution_lifecycle_parity.md
   ├─ 51_runtime_composition_guard.md
   ├─ 52_ruleprocessor_zone_mover_injection.md
   ├─ 53_security_trigger_timing_parity.md
   ├─ 54_after_effects_multiple_skills_priority.md
   ├─ 55_counter_block_attack_target_timing.md
   ├─ 56_golden_scenarios_batch1.md
   ├─ 57_parity_trace_contract_and_exporter.md
   ├─ 58_parity_fixture_comparer.md
   ├─ 59_whole_engine_completion_gate_v1.md
   ├─ 60_final_regression_fuzz_readiness_review.md
   ├─ 90_handoff_update_engine_parity.md
   └─ 91_checkpoint_engine_parity.md
```

## Codex 사용 방법

처음 한 번만:

```text
/mention docs/codex-prompts/GOAL_ENGINE_PARITY_47_PLUS.md
/mention docs/codex-prompts/ACTIVE/RUN_NEXT_ENGINE_PARITY.md
/mention docs/codex-prompts/state/QUEUE_ENGINE_PARITY.md
/mention docs/codex-prompts/templates/engine_parity_common_constraints.md
```

그다음 `GOAL_ENGINE_PARITY_47_PLUS.md` 안의 `/goal` 한 줄을 실행한다.

이후에는 다음 한 줄만 반복한다.

```text
다음 engine-parity queue 작업을 진행해.
```

Codex는 queue에서 첫 번째 `todo` 항목 하나만 수행하고 멈춰야 한다.

## 작업 후 검수

각 항목 완료 후 다음을 확인한다.

- 실제 실행한 prompt 번호
- queue 상태가 `done`, `blocked`, `needs-review` 중 하나로 갱신됐는지
- `git status --short`
- `git diff --stat`
- `git diff --name-only`
- `DCGO/Assets/Scripts` 수정 없음
- 테스트 결과
- silent no-op 또는 임시 shortcut 없음

ChatGPT 공유용 보고가 필요하면:

```text
docs/codex-prompts/prompts/90_handoff_update_engine_parity.md를 실행해.
```

로컬 checkpoint가 필요하면 검수 후에만:

```text
docs/codex-prompts/prompts/91_checkpoint_engine_parity.md를 실행해.
```

## 중요한 범위

이 queue의 completion은 다음 범위를 뜻한다.

```text
DCGO 공통 battle/effect core의 source-aligned completion candidate
+ ST1~ST3 카드풀의 end-to-end 검증
```

전체 DCGO 카드풀의 모든 카드 완료를 뜻하지 않는다.
또한 whole-engine gate가 통과하고 사용자가 승인하기 전까지 RL 학습 API를 구현하지 않는다.
