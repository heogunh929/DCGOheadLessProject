# 전체 카드풀 Manifest 요약

- Source snapshot: `local-manifest` / `docs/source/dcgo-source-file-manifest.json`
- Source manifest SHA-256: `d0b46c4d82901494e2cfa51c423dc70a7f8f29bbd0bc38eda6c91b7ebd90ef30`
- Manifest SHA-256: `a254765908d21d436dece44216858cd0e70ec14776a4107f9532a4df969ad269`
- Anomalies SHA-256: `5a31d7064894009848da6d9e3feb2e30191a327715585e6d8bcfc26a7f57baeb`
- CardBaseEntity asset files processed: 8187 / 8187
- Gameplay card records: 8186
- Non-gameplay asset files excluded with anomaly: 1
- Unique CardId count: 4201
- Duplicate CardId groups: 2384
- Duplicate CardIndex groups: 0
- Duplicate DefinitionStableId groups: 0
- Missing CardEffect source body records: 39
- Parse/read failures: 0

## Identity Policy

`DefinitionStableId`는 `CardId#CardIndex@VariantKey` 형식이다. 같은 `CardId`의 base/P1/P2 등 variant는 평탄화하지 않고 별도 record로 유지한다.

## Top Sets

- P: 562
- BT7: 280
- BT12: 273
- BT8: 272
- BT5: 263
- BT6: 252
- BT4: 250
- BT13: 246
- BT10: 237
- BT9: 233
- BT17: 229
- BT2: 229
- BT14: 223
- BT16: 222
- BT11: 219
- BT3: 217
- BT1: 214
- BT15: 205
- BT20: 181
- BT21: 181

## Card Kinds

- Digimon: 5877
- Option: 937
- Tamer: 841
- DigiEgg: 547

## Colors

- Red: 1785
- Black: 1629
- Yellow: 1601
- Purple: 1600
- Green: 1483
- Blue: 1446
- White: 439

## Validation Notes

- 모든 `DCGO/Assets/CardBaseEntity/**/*.asset` 파일을 읽고 처리 수를 기록했다.
- `CardEffectClassName`에 대응하는 source body가 없으면 silent no-op으로 숨기지 않고 anomalies에 `MissingCardEffectSourceBody`로 남긴다.
- `CardId` 중복은 variant-aware identity 보존 대상으로 보고하며, `CardIndex`/`DefinitionStableId` 중복은 error anomaly로 보고한다.
- manifest에는 효과 구현 또는 NoEffect 자동 대체가 포함되지 않는다.
