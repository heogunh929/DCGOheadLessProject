# Full Per-Card / Source-Effect Scaffold

이 문서는 64번 queue의 생성 결과다. 효과 body를 구현하지 않고, 전체 asset/source identity를 추적 가능한 generated scaffold와 registry-only catalog로 고정한다.

## 입력

- Source snapshot: `local-manifest` / `docs/source/dcgo-source-file-manifest.json`
- Full card pool manifest: `docs/generated/full-card-pool-manifest.json` / `a254765908d21d436dece44216858cd0e70ec14776a4107f9532a4df969ad269`
- Full mechanic inventory: `docs/generated/full-mechanic-inventory.json` / `e8fd1723d947f14e49cdc1250e0e146092a1e7010cce2833b5dde4f28e836c27`

## Coverage

- Card mapping records: 8186 / 8186
- Source effect scaffold records: 3918 / 3918
- Set registry catalogs: 63
- Duplicate CardId groups preserved: True (2384)
- DefinitionStableId duplicate groups: 0
- Source body missing records: 39
- NeedsSourceReview markers: 40

## Status Policy

- Source-bearing scaffold default: `Unsupported`
- Empty CardEffectClassName marker: `NoEffect`
- Missing/ambiguous source body marker: `NeedsSourceReview`
- Implemented/Verified auto-promotion: `False`
- Effect bodies changed by this queue: `False`

카드별 source-bearing scaffold는 이 단계에서 `Unsupported`이며, 원본 source body가 없거나 모호하면 `NeedsSourceReview`다. `NoEffect`는 원본 asset의 `CardEffectClassName`이 비어 있는 경우에만 사용한다.

## Generated Files

- Index: `docs/generated/full-card-source-scaffold/index.json`
- Status registry: `docs/generated/full-card-source-scaffold/status-registry.json`
- Card mapping files: 63
- Source scaffold files: 63
- Set catalog files: 63

Catalog 파일은 `registryOnly=true`이며 effect body, 특정 CardId 분기, zone mutation logic을 포함하지 않는다.
