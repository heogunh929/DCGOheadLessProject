# Recommended Next Foundation Task

- 생성 시각: `2026-06-23T05:37:16+09:00`

## 1. 다음 작업명

Script Runtime Foundation 공통 호출 계약 정리

## 2. 목표

카드별 효과 구현을 시작하기 전에, 원본 `CardEffect`들이 기대하는 공통 호출 계약과 headless primitive/service 대응 계약을 상위 참조 계약부터 고정한다.

## 3. 수정 대상 파일

- 문서: `docs/rl-engine/script-runtime-foundation-contract.md`

- generated JSON: `docs/generated/as-is/script_runtime_foundation_contract_candidates.json`

- 필요 시 분석 스크립트: `scripts/` 아래

## 4. 만들 문서/테스트

- 상위 공통 호출 계약별 원본 의미, headless 대응 위치, unsupported 조건, 검증 필요성 표

- 아직 구현 테스트가 아니라 계약 검증 후보와 representative source evidence 정리

## 5. 금지 사항

- CardEffect body 구현 금지

- 신규 카드 효과 구현 금지

- `DCGO/Assets` 수정 금지

- RL Environment/Reward/Dataset/Trainer 구현 금지

## 6. 완료 조건

- 상위 공통 호출 계약 후보가 문서화됨: `CardSource.Owner`, `ContinuousController.instance.StartCoroutine`, `CardEffectCommons.IsExistOnBattleArea`, `Permanent.TopCard`, `ICardEffect.SetUpICardEffect`, `CardEffectCommons.HasMatchConditionPermanent`, `GManager.instance.GetComponent`, `CardEffectCommons.IsExistOnBattleAreaDigimon`

- 각 호출 계약이 `Exists/Partial/Missing/UnityOnly/Ambiguous` 중 하나로 분류됨

- 다음 실제 foundation 구현 단위가 Tier0/Tier1로 분해됨

## 7. Codex에게 줄 다음 프롬프트 초안

```text
다음 작업은 구현이 아니라 `CardEffectCommons`와 state facade의 Script Runtime Foundation / 공통 호출 계약 고정 작업으로 진행한다.

목표:
Unity 원본 `CardEffect`가 가장 많이 참조하는 `CardEffectCommons`, `CardSource`, `Permanent`, `Player`, `ICardEffect`, `EffectTiming` 호출 계약을 기준으로, headless에서 어떤 facade/primitive가 존재해야 하는지 상위 계약부터 unsupported 실패 조건과 함께 문서화하고 최소 검증 범위를 산정한다.

금지:
- 카드별 CardEffect body 구현 금지
- 신규 카드 효과 구현 금지
- 원본 `DCGO/Assets` 수정 금지
- 대규모 RL.Engine 리팩터링 금지

산출물:
- `docs/rl-engine/script-runtime-foundation-contract.md`
- `docs/generated/as-is/script_runtime_foundation_contract_candidates.json`
- 필요한 경우 분석 스크립트만 `scripts/` 아래 추가/보강
```
