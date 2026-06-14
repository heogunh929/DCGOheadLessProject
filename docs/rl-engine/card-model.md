# Card Model

## 05 단계 이식 범위

`05_card_model.md` 단계는 원본 `CEntity_Base`의 battle-relevant 정적 카드 데이터를 `CardDefinition`으로 옮기고, 원본 `CardSource.SetBaseData`가 담당하던 런타임 카드 인스턴스 생성을 `CardInstanceFactory`로 분리한다. 원본 `DataBase`의 Unity asset loading, color sprite table, StreamingAssets 접근은 이 단계에서 제외하고, rule/test가 사용할 수 있는 `ICardDatabase` 조회 계약만 만든다.

## 이식한 원본 필드

| 원본 타입 | 원본 필드/프로퍼티 | RL.Engine 타입 |
| --- | --- | --- |
| `CEntity_Base` | `CardID` | `CardDefinition.CardId` |
| `CEntity_Base` | `CardIndex` | `CardDefinition.CardIndex` |
| `CEntity_Base` | `CardName_ENG` | `CardDefinition.CardNameEnglish` |
| `CEntity_Base` | `CardName_JPN` | `CardDefinition.CardNameJapanese` |
| `CEntity_Base` | `cardColors` | `CardDefinition.CardColors` |
| `CEntity_Base` | `PlayCost` | `CardDefinition.PlayCost` |
| `CEntity_Base` | `EvoCosts` | `CardDefinition.EvoCosts`, `EvoCostDefinition` |
| `CEntity_Base` | `Level` | `CardDefinition.Level` |
| `CEntity_Base` | `cardKind` | `CardDefinition.CardKinds` |
| `CEntity_Base` | `DP` | `CardDefinition.DP` |
| `CEntity_Base` | `rarity` | `CardDefinition.Rarity` |
| `CEntity_Base` | `CardEffectClassName` | `CardDefinition.CardEffectClassName` |
| `CEntity_Base` | `MaxCountInDeck` | `CardDefinition.MaxCountInDeck` |
| `CEntity_Base` | `OverflowMemory`, `IsACE` | `CardDefinition.OverflowMemory`, `IsAce` |
| `CEntity_Base` | `LinkDP` | `CardDefinition.LinkDP` |
| `CEntity_Base` | `OptionCardColorRequirements` | `CardDefinition.OptionCardColorRequirements` |
| `CEntity_Base` | `IsDualCard`, `IsPermanent` | `CardDefinition.IsDualCard`, `IsPermanent` |
| `CardSource` | `Owner`, face state, base data reference | `CardInstance.Owner`, `IsFaceUp`, `DefinitionId` |
| `CardSource` | `SetUpCardIndex` runtime identity | `CardInstanceFactory` sequential `CardInstanceId` |

## 의도적으로 제외한 원본 요소

- `Sprite`, `CardSprite`, `CardSpriteName` image loading
- `StreamingAssetsUtility.GetSprite`
- `ScriptableObject`, `[CreateAssetMenu]`, `TextArea`
- `MonoBehaviour`, `PhotonView`, Unity event callbacks
- `DataBase`의 UI 색상표와 `UnityEngine.Color`
- `CardSource`의 play/digivolve 가능 여부, 동적 비용 변경, CardEffect 기반 색/DP/name 변경

동적 rule 계산은 이후 rule primitive와 CardEffect 단계에서 원본 구현을 다시 읽고 옮긴다. 이번 단계의 `CardDefinition`은 정적 원본 데이터를 담는 immutable 모델로만 사용한다.

## Database와 factory

`ICardDatabase`는 CardId 기반 조회만 제공한다. `InMemoryCardDatabase`는 테스트와 이후 검증 harness에서 Unity asset database 없이 카드 정의를 공급하기 위한 구현이다.

`CardInstanceFactory`는 같은 `CardDefinition`에서 여러 `CardInstance`를 만들 때 서로 다른 `CardInstanceId`를 부여한다. 난수를 사용하지 않고 순차 ID만 사용하므로 같은 입력 순서는 같은 instance id sequence를 만든다.
