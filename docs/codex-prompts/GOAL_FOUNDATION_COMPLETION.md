# Foundation Completion Goal

최종 목표는 `https://github.com/DCGO2/DCGO` 원본의 카드풀, 룰, 카드 효과, 게임 로직을 기반으로 Unity 없이 동작하는 deterministic headless RL battle engine을 만드는 것이다.

현재 단계에서 Codex는 개별 CardEffect body 구현자가 아니다. Codex는 OpenCode/local model이 나중에 개별 `Assets/Scripts/CardEffect/<SET>/<COLOR>/<CARD>.cs` body를 안정적으로 옮길 수 있도록 원본 `Assets/Scripts/Script/**` 공통 카드 효과 foundation을 RL.Engine에 먼저 포팅하고 검증한다.

## 담당 범위

- `Assets/Scripts/Script/ICardEffect.cs` 대응 구조
- `Assets/Scripts/Script/CardEffects/**/*.cs` 대응 구조
- `Assets/Scripts/Script/CardEffectFactory/**/*.cs` 대응 구조
- `Assets/Scripts/Script/CardEffectCommons/**/*.cs` 대응 구조
- `CardSource`, `Permanent`, `Player`, `AttackProcess` 중 카드 효과 기반 API
- `EffectTiming` semantic mapping
- activation, optional, once-per-turn, chain, background effect
- inherited, linked, security, counter effect
- selection, continuation, stale target
- continuous/static effect
- modifier, restriction, immunity, requirement, cost
- zone movement primitive
- runtime status truth
- unsupported/no-op validation gate
- OpenCode용 frozen foundation API와 patch validation 기반

## 금지

- 개별 CardEffect body 신규 구현
- C0039 이후 card-porting batch 실행
- OpenCode가 맡을 카드별 구현 선점
- `DCGO/Assets` 원본 수정
- core service의 특정 CardId 분기 추가
- 카드별 workaround helper 추가
- 직접 zone list 수정
- silent no-op 허용
- 빈 descriptor로 unsupported 숨기기
- generated registry를 수동 조작해 구현된 것처럼 표시
- 테스트 이름만으로 Verified 처리
- Foundation Gate 전 OpenCode task 생성
- Foundation Gate 전 RL Environment, Observation, Reward, Dataset, Trainer 구현
- commit/push 수행

## OpenCodeReady 조건

OpenCodeReady는 `scripts/calculate_foundation_completion_gate.py`가 계산한다.

- 원본 Script/CardEffects inventory 완료
- 원본 CardEffectFactory inventory 완료
- 원본 CardEffectCommons inventory 완료
- 원본 공통 API와 RL.Engine 대응 mapping 완료
- referenced common API unknown count = 0
- referenced Unsupported capability count = 0
- referenced PartiallyImplemented capability count = 0
- runtime/generated status mismatch count = 0
- silent no-op count = 0
- blocked empty descriptor count = 0
- false NoEffect count = 0
- variant identity conflict count = 0
- core CardId branch count = 0
- direct zone mutation count = 0
- full regression 통과
- replay determinism 통과
- invariant suite 통과
- source lock 변경 없음

OpenCodeReady=false 동안 card-porting, local model task, RL 학습 구성요소를 시작하지 않는다.
