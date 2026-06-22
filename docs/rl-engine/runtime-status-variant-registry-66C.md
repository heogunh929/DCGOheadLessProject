# 66C Runtime Status Variant Registry

## 결정

`66C_runtime_status_variant_registry_integration`은 runtime registry의 CardId-only flattening blocker를 해소한 mechanic-remediation 항목이다. 이 작업은 card-porting batch를 진행하지 않았고, `C0039_zone_security_recovery`를 실행 가능 후보로 만들지 않았다.

## Runtime Registry

- `CardDefinition`은 `VariantKey`, `DefinitionStableId`, `HasDefinitionIdentity`를 가진다.
- indexed definition identity는 `CardId#CardIndex@VariantKey` 형식이다. `VariantKey`가 비어 있으면 `base`로 정규화한다.
- `CardScriptRegistry` lookup 순서는 exact `DefinitionStableId`, safe legacy `CardId`, `CardEffectClassName` alias 순서다.
- 같은 `CardId`에 indexed record가 있으면 indexed definition은 legacy `CardId` fallback으로 평탄화하지 않는다.
- CardIndex가 없는 legacy deck validation path는 기존 target-deck fixture 보존을 위해 legacy `CardId` record를 사용할 수 있다.

## Status Registry

- `CardEffectPortingRecord`는 `CardIndex`, `VariantKey`, `DefinitionStableId`를 보존한다.
- `AssetRegistryMappingValidator`는 실제 `ICardScriptRegistry.TryGetScript(CardDefinition)` 결과로 registry record를 확인한다.
- status snapshot은 indexed record에 대해 exact `DefinitionStableId` key를 요구한다.
- generated full-card source scaffold와 실제 code `CardEffectPortingRecord`의 status mismatch는 아직 blocker로 남아 있다.

## ST3-02 분리

- `ST3-02` legacy record는 기존 ST1~ST3 target-deck fixture를 위한 `NoEffect` fallback이다.
- `ST3-02#76@base`와 `ST3-02#77@P1`은 asset에 `CardEffectClassName`이 없으므로 explicit `NoEffect` record다.
- `ST3-02#4977@P2`는 asset이 `CardEffectClassName: ST3_02`를 참조하지만 source body가 확인되지 않았으므로 `Unsupported` record다.
- P2는 NoEffect로 조용히 처리하지 않는다.

## 남은 차단

- `docs/generated/capability-truth-audit/status-mismatch-report.json`의 runtime registry blocker count는 0이다.
- code/generated status mismatch는 92건으로 남아 있다.
- source required capability graph가 아직 runtime status registry와 연결되지 않았으므로 다음 queue는 `66D_card_effect_capability_dependency_graph`다.
