# 66U Foundation Gate Blocked Empty Descriptor Scope

## 목적

66U는 Foundation Completion Gate의 `blocked empty descriptor count`가 실제 위험 후보와 continuous/static 전용 legacy partial script를 구분하도록 보정한 항목이다.

개별 `CardEffect` body 구현, C0039 이후 card-porting batch 실행, generated registry의 `Verified` 승격은 수행하지 않는다.

## 확인한 현재 상태

`docs/generated/foundation-completion-gate.json`의 기존 13개 `blockedEmptyDescriptors` 샘플은 모두 다음 조건을 만족했다.

- `IContinuousCardScript` 구현체다.
- `CreateEffectDescriptors(...)`는 active trigger descriptor를 만들지 않고 `Array.Empty<EffectDescriptor>()`를 반환한다.
- `CreateContinuousEffectDescriptors(...)`는 `new ContinuousEffectDescriptor(...)`를 반환하는 non-empty continuous/static channel을 가진다.
- `Resolve(...)`는 빈 body가 아니라 explicit exception으로 active body 실행을 막는다.
- generated full-card source scaffold status는 계속 `Unsupported`이며, 이 legacy partial script들은 full-card source truth를 승격하지 않는다.

## RL.Engine 대응

- `scripts/calculate_foundation_completion_gate.py`가 non-empty alternate descriptor channel을 탐지한다.
- continuous/static descriptor channel이 있는 legacy partial의 empty active descriptor는 `blockedEmptyDescriptorCount`에서 제외한다.
- 제외된 항목은 `legacyContinuousOnlyEmptyDescriptorCount`와 `legacyContinuousOnlyEmptyDescriptors` samples로 별도 공개한다.
- 실제 alternate descriptor channel 없이 `Unsupported`/`PartiallyImplemented` 기록 근처에서 빈 descriptor를 반환하는 후보는 계속 `blockedEmptyDescriptorCount`에 남긴다.

## 검증

- `FoundationCompletionGate baseline blocks OpenCode`
- `scripts/calculate_foundation_completion_gate.py --workspace .`

재계산 결과:

- `blockedEmptyDescriptorCount=0`
- `legacyContinuousOnlyEmptyDescriptorCount=13`
- `OpenCodeReady=false`
- passed gate 11, failed gate 3

## 남은 범위

이번 작업은 gate false positive를 분리한 것이며, full-card source scaffold status를 승격하지 않는다. `ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`이고, unknown common API, unsupported capability, partially implemented capability gate가 남아 있다.
