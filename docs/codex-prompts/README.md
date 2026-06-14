# DCGO RL.Engine 이식용 Codex 프롬프트

이 폴더는 `headlessDCGO` 작업 공간에서 Codex를 단계별로 실행하기 위한 프롬프트 모음이다.

## 전제 폴더 구조

```text
headlessDCGO/
├─ DCGO/                  # 기존 DCGO Git 소스
├─ src/                   # 새 RL.Engine
├─ docs/rl-engine/        # 이식 문서
└─ docs/codex-prompts/    # 이 프롬프트 모음
```

## 사용 순서

Codex에서 `/goal`을 설정한 뒤, `prompts/*.md`를 한 단계씩 `/mention`해서 실행한다.

예시:

```text
/goal docs/codex-prompts/README.md의 단계 순서대로 DCGO Unity battle 로직을 RL headless engine으로 이식한다. 신규 개발이 아니라 원본 이식이며, 한 번에 한 단계만 수행하고 한국어로 보고한다.

/mention docs/codex-prompts/prompts/00_review_agents.md
00_review_agents 단계만 수행해줘.
```

## 단계 목록

1. `00_review_agents.md`
2. `01_create_porting_docs.md`
3. `02_review_porting_docs.md`
4. `03_engine_skeleton.md`
5. `04_state_model.md`
6. `05_card_model.md`
7. `06_zone_mover.md`
8. `07_decision_selection.md`
9. `08_validation_harness_v0.md`
10. `09_setup_draw_deckout.md`
11. `10_minimal_playable_battle.md`
12. `11_validation_harness_v1.md`
13. `12_tier1_primitives.md`
14. `13_complex_mechanics.md`
15. `14_battle_keywords.md`
16. `15_cardeffect_foundation.md`
17. `16_cardeffect_porting_cardpool.md`
18. `17_validation_harness_v2_engine_completion.md`
19. `18_rl_environment_after_engine_complete.md`
20. `19_unity_adapter_design.md`
21. `20_unity_adapter_minimal_impl.md`

## 핵심 원칙

- 한 번에 여러 단계 실행 금지.
- `DCGO/` 하위 원본 Unity 파일은 명시 요청 없이 수정 금지.
- `src/DCGO.RL.Engine`에는 Unity/Photon 의존성 추가 금지.
- 학습용 RL Environment는 엔진 완성 후에만 구현.
- 중간 trace/self-play는 검증용이지 학습용이 아님.
