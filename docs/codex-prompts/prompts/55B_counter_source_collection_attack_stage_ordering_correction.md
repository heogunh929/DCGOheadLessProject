# 55B Counter Source Collection And Attack Stage Ordering Correction

## 범위

55B는 queue 55A의 attack lifecycle 구현을 source-aligned하게 보정한다. 56번 golden scenarios는 실행하지 않는다.

## 필수 기준

- `CounterEffectHashtable`은 counter source whitelist가 아니라 공격 선언 당시 attacker 정보를 보존하는 trigger-condition payload로만 사용한다.
- counter 후보는 지원되는 cut-in source 전체에서 수집한다: 양 플레이어 field top, inherited, linked, hand, trash, executing, face-up security.
- turn player group을 먼저 처리하고, turn player가 counter를 사용하면 전체 counter window를 종료한다.
- 실제 counter 효과는 양쪽을 합쳐 최대 1개만 실행한다.
- skip은 현재 group의 모든 후보가 skippable일 때만 허용한다.
- counter 선택 자체가 optional activation 동의인 descriptor는 `CounterSelectionConsumesOptional` metadata로 optional yes/no를 중복 질문하지 않는다.
- counter candidate identity는 effect stable id만 사용하지 않고 source card instance, source permanent, candidate index를 포함한다.
- attack declaration은 runtime context와 attacker snapshot을 suspend 전에 생성하고, suspend 실패 시 context를 rollback/clear한다.
- counter 중 기존 defender가 사라져도 block designation 전에는 attack을 끝내지 않는다.
- `OnBlockAnyone` 후 blocker가 사라지면 invalid blocker switch에 대한 `OnAttackTargetChanged`를 발생시키지 않는다.
- target switch는 단일 슬롯이 아니라 ordered queue로 보존한다.
- `OnEndAttack` 실행 전에는 `IsBlocking`/`Blocker`/`Defender` context를 유지하고 cleanup에서만 제거한다.
- `DCGO/Assets/Scripts`는 수정하지 않는다.
- 카드별 effect body는 각 카드 파일에 유지하고 core service에 CardId 분기를 추가하지 않는다.

## 완료 조건

- 55B 전용 counter/block/target-switch/replay 테스트가 통과한다.
- 전체 `DCGO.RL.Engine.Tests` 회귀가 통과한다.
- `QUEUE_DCGO_FULL_CARD_POOL.md`, `PROGRESS_DCGO_FULL_CARD_POOL.md`, `docs/rl-engine/attack-timing-source-mapping.md` 및 관련 runtime/decision/validation 문서를 갱신한다.
- commit/push는 수행하지 않는다.
