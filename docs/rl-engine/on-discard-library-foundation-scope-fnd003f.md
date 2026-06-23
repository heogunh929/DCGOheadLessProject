# FND-003-F OnDiscardLibrary Foundation Scope

## 목표

`OnDiscardLibrary`가 `Unsupported` foundation capability로 남지 않도록, 원본 DCGO의 deck trash 이벤트 경계를 headless primitive 수준에서 닫는다.

이번 작업은 개별 CardEffect body 구현이 아니다. deck top card를 trash로 보내는 공통 primitive가 source-aligned pending rule event를 생성하도록 하는 foundation 작업이다.

## 원본 근거

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:995`
  - `EffectTiming.OnDiscardLibrary`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:1971`
  - `IAddTrashCardsFromLibraryTop`가 deck top card를 snapshot한다.
  - 요청 수가 deck보다 많으면 가능한 card만 수집한다.
  - 요청 수가 0 이하이거나 deck이 비어 있으면 이벤트 없이 종료한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:5809`
  - `ITrashDeckCards.TrashDeckCards()`가 실제 trash 이동 후 `DiscardedCards`, `CardEffect` payload로 `OnDiscardLibrary`를 stack한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\CanUseEffects\WhenDiscardLibrary.cs:17`
  - `CanTriggerWhenDiscardLibrary`가 `DiscardedCards` payload를 기준으로 trigger 가능 여부를 판단한다.

## Headless 반영

- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`
  - `TrashFromDeckResult`를 추가했다.
  - `TrashTopDeckCardsWithEvents(...)`를 추가했다.
  - deck top snapshot 후 실제 `Zone.Deck -> Zone.Trash` 이동을 수행한다.
  - 이동된 카드가 있을 때만 `EffectTiming.OnDiscardLibrary` pending rule event를 큐에 넣는다.
  - deck 부족은 실패가 아니라 `RequestedMoreThanAvailable=true` 결과로 기록한다.
  - count 0 또는 empty deck은 원본처럼 이벤트 없이 빈 결과를 반환한다.

## Payload Contract

`OnDiscardLibrary` payload는 다음 값을 포함한다.

- `Player`
- `Cards`
- `CardSources`
- `SourceZone`
- `DestinationZone`
- `CardEffect`
- `SourceCard`
- `SourcePermanent`
- `DiscardedCards`
- `RequestedCount`
- `RequestedMoreThanAvailable`
- `MoveReason`

`SourceZone`은 `Zone.Deck`, `DestinationZone`은 `Zone.Trash`, `MoveReason`은 `MoveReason.Effect`다.

## 검증

- 테스트: `FND-003-F OnDiscardLibrary queues deck trash event`
- 검증 명령: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 616 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3

## 이번 작업에서 제외한 범위

- 개별 `CardEffect` body는 구현하지 않았다.
- 기존 `BT19_071` 등 카드별 deck trash 효과를 새 primitive에 연결하지 않았다.
- `Reveal` 상태에서 선택 후 trash되는 흐름은 `OnDiscardLibrary`가 아니라 별도 reveal/select flow 후보로 남겼다.
- 원본 `DCGO/Assets`는 수정하지 않았다.
- C0039 이후 card-porting은 실행하지 않았다.
- Foundation Gate 수치와 generated implementation status는 조작하지 않았다.

## 다음 후보

다음 foundation 작업 후보는 `FND-003-G OnUseOption`이다. option card executing zone 이동, `Root`, `Cost`, `Card` payload boundary, `OnUseOption`/background `OnUseOption`/`OptionSkill` ordering을 source-aligned 방식으로 분리해야 한다.
