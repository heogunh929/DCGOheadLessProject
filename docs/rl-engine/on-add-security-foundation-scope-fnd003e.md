# FND-003-E OnAddSecurity Foundation Scope

## 목표

`OnAddSecurity`가 `Unsupported` foundation capability로 남지 않도록, 원본 DCGO의 security 추가 이벤트 경계를 headless primitive 수준에서 닫는다.

이번 작업은 개별 CardEffect body 구현이 아니다. security에 카드를 추가하는 공통 primitive가 source-aligned pending rule event를 생성하도록 하는 foundation 작업이다.

## 원본 근거

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:991`
  - `EffectTiming.OnAddSecurity`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs:1030`
  - `EffectTiming.OnFaceUpSecurityIncreased`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:5489`
  - `IAddSecurity.AddSecurity()`가 `Player`, `CardSources` payload로 `OnAddSecurity`를 stack한다.
  - 추가된 security card가 face-up이면 `OnFaceUpSecurityIncreased`도 이어서 stack한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardObjectController.cs:976`
  - `AddSecurityCard(...)`가 security zone 삽입 후 `new IAddSecurity(cardSource).AddSecurity()`를 호출한다.
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs:2041`
  - `IAddSecurityFromLibrary`가 deck top card를 security로 옮기는 recovery 흐름에서 `AddSecurityCard(...)`를 사용한다.

## Headless 반영

- `src/DCGO.RL.Engine/Primitives/Tier1PrimitiveService.cs`
  - `AddSecurity(...)`가 `Zone.Security` 이동 완료 후 `EffectTiming.OnAddSecurity` pending rule event를 큐에 넣는다.
  - `faceUp=true`인 security 추가는 `OnAddSecurity` 다음에 `EffectTiming.OnFaceUpSecurityIncreased`를 큐에 넣는다.
  - `RecoverFromDeck(...)`는 원본 `AddSecurityCard(...)` 반복 호출에 맞춰 recovery card마다 `OnAddSecurity`를 큐에 넣는다.

## Payload Contract

`OnAddSecurity`와 `OnFaceUpSecurityIncreased` payload는 다음 값을 포함한다.

- `Player`
- `Cards`
- `CardSources`
- `SourceZone`
- `DestinationZone`
- `CardEffect`
- `SourceCard`
- `SourcePermanent`
- `AddedSecurityCards`
- `SecurityCards`
- `SecurityCard`
- `ToTop`
- `FaceUp`
- `MoveReason`

`DestinationZone`은 `Zone.Security`이며, `MoveReason`은 `MoveReason.Effect`다.

## 검증

- 테스트: `FND-003-E OnAddSecurity queues security add events`
- 검증 명령: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj`
- 결과: `All 615 tests passed.`
- Foundation Gate 재계산: `OpenCodeReady=false`, passed gate 11, failed gate 3

## 이번 작업에서 제외한 범위

- `IFlipSecurity.FlipFaceUp()`에 대응하는 별도 face-up flip primitive hook은 구현하지 않았다.
- 개별 `CardEffect` body는 구현하지 않았다.
- 원본 `DCGO/Assets`는 수정하지 않았다.
- C0039 이후 card-porting은 실행하지 않았다.
- Foundation Gate 수치와 generated implementation status는 조작하지 않았다.

## 다음 후보

다음 foundation 작업 후보는 `FND-003-F OnDiscardLibrary`다. deck trash primitive payload, `DiscardedCards`/`CardEffect` source mapping, deck exhaustion edge case, replay fixture 후보를 source-aligned 방식으로 분리해야 한다.
