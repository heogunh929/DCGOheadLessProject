# Zone Mover

## 06 단계 이식 범위

`06_zone_mover.md` 단계는 원본 Unity 구현의 카드 이동 패턴을 headless 상태 primitive인 `ZoneMover.MoveCard`로 통합한다. 이 단계는 효과 발동, cut-in, animation, log, 선택 UI를 이식하지 않는다. 이동 전 source zone 검증, 이동 후 `CardInstance.CurrentZone` 갱신, permanent stack/source/link 일관성, trace 생성만 담당한다.

## 원본 이동 패턴 대응

| 원본 구현 | 원본 패턴 | RL.Engine primitive |
| --- | --- | --- |
| `CardObjectController.RemoveFromAllArea` | hand/deck/digitama/trash/lost/security/executing 및 permanent source/link에서 카드 제거 | `ZoneMover.RemoveFromZone` |
| `CardObjectController.AddHandCard(s)` | source 제거 후 hand에 추가, draw 여부별 UI 처리 | `MoveCardCommand(..., DestinationZone: Hand)` |
| `CardObjectController.AddSecurityCard` | source 제거 후 security top/bottom에 추가, 기본 face-down | `DestinationZone: Security`, `FaceUp` |
| `CardObjectController.AddTrashCard(s)` | source 제거 후 trash top에 추가 | `DestinationZone: Trash` |
| `CardObjectController.AddLibraryTopCards` | source 제거 후 deck top에 추가 | `DestinationZone: Deck`, `ToTop: true` |
| `CardObjectController.AddLibraryBottomCards` | source 제거 후 deck bottom에 추가 | `DestinationZone: Deck`, `ToTop: false` |
| `CardObjectController.AddExecutingCard` | source 제거 후 executing area에 추가 | `DestinationZone: Executing` |
| `CardObjectController.CreateNewPermanent` | top card를 모든 area에서 제거하고 field frame에 permanent 배치 | `DestinationZone: BattleArea/BreedingArea`, `DestinationPermanent` |
| `CardObjectController.RemoveField` | field frame에서 permanent 제거 후 cardSources 초기화 | top card source 이동 시 `PermanentState` 갱신 |
| `Permanent.AddDigivolutionCardsTop/Bottom` | 모든 area에서 제거 후 source list top/bottom에 추가 | `DestinationZone: EvolutionSources`, `ToTop` |
| `Permanent.AddLinkCard` / `RemoveLinkedCard` | 모든 area에서 제거 후 linked list에 추가 또는 trash | `DestinationZone: LinkedCards` / source `LinkedCards` |
| `ITrashDigivolutionCards` | source list에서 제거 후 trash | `SourceZone: EvolutionSources`, `DestinationZone: Trash` |
| `ITrashLinkCards` | linked list에서 제거 후 trash | `SourceZone: LinkedCards`, `DestinationZone: Trash` |

## Headless 규칙

- 이동 전 `MoveCardCommand.SourceZone`은 `CardInstance.CurrentZone`과 실제 container membership이 모두 일치해야 한다.
- 한 카드는 player public zone과 permanent top/source/link 전체에서 정확히 하나의 membership만 가져야 한다.
- `BattleArea`/`BreedingArea` top card가 이동할 때 source가 있으면 첫 source를 새 top으로 승격하고, 해당 card의 `CurrentZone`을 field zone으로 바꾼다.
- `EvolutionSources`와 `LinkedCards` 이동은 `DestinationPermanent` 또는 `SourcePermanent`를 통해 명확한 permanent에 연결한다.
- 잘못된 source zone, duplicate membership, 존재하지 않는 permanent는 `DomainException`으로 실패한다.
- 현재 primitive가 처리하지 못하는 linked-only permanent 같은 상태는 `UnsupportedMechanicException`으로 실패한다.
- 이동마다 `ZoneMoveTrace`를 만들고, 이동 전후 state hash를 기록한다.

## 의도적으로 제외한 항목

- Unity animation, sound, log object, DOTween, UI card object 생성/삭제
- `GManager`, `ContinuousController`, cut-in auto-processing
- ACE overflow, OnAddHand/OnAddSecurity/OnMove 같은 trigger stacking
- shuffle 난수 처리
- battle rule 판단, legal action 생성, effect condition 검증

이 항목들은 이후 turn flow, primitive, CardEffect 단계에서 원본을 다시 읽고 별도 rule layer로 이식한다. zone container 변경 자체는 계속 `ZoneMover`를 통해서만 수행한다.
