# RL Queue - Full DCGO Gate 이후 전용

이 queue는 `Full DCGO Snapshot Completion Gate` 통과와 사용자 명시 승인 후에만 활성화한다.

현재 모든 항목은 `blocked` 상태다.
71번이 gate report를 검증하고 사용자가 승인한 뒤 새 RL queue를 생성하거나 이 queue를 갱신한다.

예정 단계:

1. RL environment contract
2. observation과 hidden information
3. action encoding과 legal action mask
4. Reset/Step/Resume 구현
5. vectorized environment와 성능
6. baseline agent와 대량 안정성 검증
7. self-play training harness
8. 첫 학습 및 평가
9. model export와 Unity inference adapter
