# 66C_runtime_status_variant_registry_integration

66B 이후 계획 항목이다. 이번 66B 작업에서는 실행하지 않는다.

## 목표

- 실제 `ICardScript` `CardEffectPortingRecord`를 전체 generated status registry와 validator에 연결한다.
- `Implemented`, `PartiallyImplemented`, `Unsupported` 상태를 문서가 아니라 코드에서 계산한다.
- `CardId#CardIndex@VariantKey` 기반 variant-aware runtime registry를 도입한다.
- blocked card script의 empty descriptor silent no-op을 금지한다.

## 완료 조건

- runtime registry가 CardId-only flattening을 사용하지 않는다.
- generated full-card status registry와 실제 card script porting record가 같은 계산 경계를 공유한다.
- blocked/unsupported script는 명시적으로 실패한다.
- 테스트와 replay evidence가 갱신된다.
