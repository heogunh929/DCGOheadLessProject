# Card Definition Variant Mapping

## 배경

queue 48에서 `ST3-02` Salamon asset variant를 확인했다. 원본 asset은 동일 `CardID: ST3-02`를 공유하지만 `CardIndex`와 `CardEffectClassName`이 다르다.

| Asset | CardIndex | CardID | CardEffectClassName | 판정 |
| --- | ---: | --- | --- | --- |
| `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_02.asset` | 76 | `ST3-02` | 없음 | 명시 NoEffect 후보 |
| `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_02_P1.asset` | 77 | `ST3-02` | 없음 | 명시 NoEffect 후보 |
| `DCGO/Assets/CardBaseEntity/ST3/Yellow/Digimon/ST3_02_P2.asset` | 4977 | `ST3-02` | `ST3_02` | variant source 의미 확인 필요 |

`DCGO/Assets/Scripts/CardEffect/ST3/**` 및 `DCGO/Assets/Scripts/**`에서 `ST3_02` CardEffect source body는 확인되지 않았다. 따라서 `ST3_02_P2`의 effect 의미를 `Implemented`로 승격하지 않는다.

## 현재 엔진 제약

`InMemoryCardDatabase`는 `CardDefinition.CardId`를 단일 키로 사용하고 duplicate `CardId`를 실패시킨다. 이 동작은 variant를 조용히 병합하지 않는 안전장치다. 하지만 장기적으로는 같은 `CardId`를 가진 asset variant를 표현하려면 별도 identity가 필요하다.

필요한 generic 모델:

- `CardId`: decklist와 카드 번호 표시용 공개 id.
- `CardIndex`: Unity asset identity. promo/variant 구분에 사용한다.
- `VariantKey`: `P1`, `P2` 같은 asset suffix나 import pipeline에서 정규화한 variant id.
- card lookup API: `CardId` lookup은 단일 정의일 때만 허용하고, variant가 여러 개인 경우 `CardIndex` 또는 `(CardId, VariantKey)` lookup을 요구한다.

## queue 48 판정

- `ST2-07`/`ST3-07` shared `ST1_06` mapping은 card-id 기반 `Implemented` script로 해결했다.
- `ST3-02`는 현재 `NoEffectCardScript("ST3-02")`를 유지한다.
- `ST3_02_P2.asset`는 같은 `CardId`에 non-empty `CardEffectClassName`을 가진 variant이므로 `needs-review`로 남긴다.
- 다음 단계의 asset registry validator는 `CardId` 단일 키로 variant를 flatten하지 않도록 검사해야 한다.

## queue 49 validator 판정

`AssetRegistryMappingValidator`는 audit/report identity를 `AssetCardDefinitionId(CardId, CardIndex, VariantKey)`로 표현한다. 따라서 동일 `CardId`를 가진 asset variant도 개별 report row와 issue target으로 남으며, `CardId`만으로 variant를 평탄화하지 않는다.

- `ST3_02.asset`은 `CardIndex: 76`, `VariantKey: base`, `CardEffectClassName` 없음이므로 NoEffect 후보로 보고한다.
- `ST3_02_P1.asset`은 `CardIndex: 77`, `VariantKey: P1`, `CardEffectClassName` 없음이므로 NoEffect 후보로 보고한다.
- `ST3_02_P2.asset`은 `CardIndex: 4977`, `VariantKey: P2`, `CardEffectClassName: ST3_02`이지만 `DCGO/Assets/Scripts`에서 source body가 확인되지 않았으므로 `FalseNoEffect` 및 `MissingSourceEffectBody` needs-review로 보고한다.

현재 registry lookup은 여전히 `CardId` 기반이다. 이는 decklist용 card identity가 단일 정의일 때만 안전하다. 향후 실제 gameplay에서 asset variant를 선택해야 하면 `CardIndex` 또는 `(CardId, VariantKey)` 기반 `CardDefinitionId`를 별도 도입해야 한다. 이번 queue 49에서는 source body가 없는 `ST3-02` 효과를 추측 구현하지 않는다.
