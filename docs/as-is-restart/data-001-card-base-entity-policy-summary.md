# DATA-001 Summary

- AS-IS root: `E:\headlessDCGO\DCGO`
- Source of Truth: `Assets/CardBaseEntity`
- CardBaseEntity file count: 17,753
- Card asset count: 8,186
- Loader asset count: 1
- CardData port file count: 8,187
- Duplicate CardID group count: 2,384
- Unique CardIndex count: 8,186
- Empty EffectClassName asset count: 225
- NoSourceCandidate EffectClassName asset count: 39
- SourceMapped EffectClassName class count: 3,904

## 결정

- CardBaseEntity는 `Port` 대상이다.
- CardID는 unique key로 사용하지 않는다.
- 모든 CardBaseEntity card asset은 asset-level variant로 보존한다.
- empty EffectClassName은 `NoEffectClassDeclaredByCardData`이며 generated status 승격 근거가 아니다.
- source candidate가 없는 EffectClassName은 `NeedsSourceMapping`이다.
- CardEffect body 구현, C0039 이후 card-porting, Foundation Gate 조작은 하지 않았다.

## 다음 작업

1. FND-003-A로 넘어가 15개 ImplementableFoundationTask를 작은 goal로 분해한다.
2. DATA-001-A/B 테스트 후보는 이후 importer/guard 작업에서 사용한다.
3. PARITY-001은 CardID-only가 아니라 asset-level identity를 기준으로 설계한다.

추천 commit message: `docs: close DATA-001 card data policy`
