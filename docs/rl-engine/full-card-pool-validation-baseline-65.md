# Full Card Pool Validation Baseline - Queue 65

이 문서는 64번 scaffold를 전체 카드풀 completion gate의 입력으로 검증한 baseline이다. 현재 결과는 `Blocked`가 정상이며, 이는 미구현 카드 효과를 silent no-op으로 숨기지 않는다는 뜻이다.

## Summary

- Decision: `Blocked`
- Card mapping records: 8186 / 8186
- Source scaffold records: 3918
- Set catalogs: 63
- Blocking issues: 11983

## Status Counts

- `NeedsSourceReview`: 40
- `NoEffect`: 225
- `Unsupported`: 7921

## Blocking Issue Counts

- `MissingSourceBody`: 39
- `SourceMappingMissing`: 1
- `UnsupportedMechanic`: 64
- `UnsupportedOrPartial`: 11879

## Deck Validation API Smoke

- `no-effect subset`: `Passed` (1/1)
- `blocked subset`: `Blocked` (2/2)
- `missing subset`: `Blocked` (0/1)

## Policy

- `NoEffect`는 원본 asset의 `CardEffectClassName`이 비어 있는 경우에만 허용한다.
- `Unsupported`, `PartiallyImplemented`, `StubbedForValidation`, `NeedsSourceReview`, `UnknownVariant`는 full snapshot completion blocker다.
- mechanic inventory의 `Unsupported`, `PartiallyImplemented`, `NeedsSourceReview`도 blocker다.
- 이 단계는 effect body를 구현하지 않으며, RL Environment/Observation/Reward/Dataset/Trainer를 만들지 않는다.

Machine-readable report: `docs/generated/full-card-pool-validation-baseline-65.json`
