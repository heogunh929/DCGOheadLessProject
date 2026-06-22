# 66Y Continuous/Static Card Metadata Requirement Scope

## 결정

이번 foundation 작업은 원본 Unity의 static 카드명/특성 변경 흐름을 RL.Engine의 공통 effective metadata layer로 연결했다.
개별 `CardEffect` body는 구현하지 않았고 C0039 이후 card-porting batch도 실행하지 않았다.

원본 `CardSource.BaseCardNames`, `CardSource.CardNames`, `CardSource.CardTraits`는 자기 자신과 field permanent가 제공하는 `IChangeBaseCardNameEffect`, `IChangeCardNamesEffect`, `IChangeTraitsEffect`를 적용해 유효 카드명과 특성을 계산한다.
RL.Engine은 이를 `StaticCardNameDescriptor`, `StaticCardTraitDescriptor`, `CardMetadataSnapshot`으로 표현하고, static criteria evaluation에서 base `CardDefinition`을 직접 수정하지 않는다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardSource.cs` | `BaseCardNames`, `CardNames`, `CardTraits`가 self/field static effects를 적용하는 흐름 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeBaseCardNameClass.cs` | base card name list 변경 조건 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeCardNamesClass.cs` | current card name list 변경 조건 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\ChangeTraitsClass.cs` | card trait list 변경 조건 |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs` | `IChangeBaseCardNameEffect`, `IChangeCardNamesEffect`, `IChangeTraitsEffect` interface |

## RL.Engine 추가 범위

- `CardMetadataSnapshot`은 effective card names, traits, text를 criteria evaluation 입력으로 고정한다.
- `StaticCardNameDescriptor`는 base/current name layer를 deterministic transform으로 제공한다.
- `StaticCardTraitDescriptor`는 effective trait list transform을 제공한다.
- `StaticEffectService.EffectiveCardMetadata`, `EffectiveCardNames`, `EffectiveCardTraits`, `MatchesCardMetadata`가 공통 query path를 제공한다.
- `StaticEffectService`의 cost/restriction/immunity/color/ignore-color target metadata criteria는 effective metadata를 사용한다.
- `StaticRequirementService`의 static evolution requirement metadata criteria는 `StaticEffectService`가 주입된 경로에서 effective metadata를 사용한다.

## 검증

추가 foundation test:

- `Static card metadata modifier affects cost criteria`

관련 검증:

- targeted `Static`
- targeted `CapabilityTruthAudit`
- full regression

## 남은 제한

- `ContinuousOrStaticEffect` 전체는 아직 `Verified`로 승격하지 않는다.
- static link requirement path 중 `StaticEffectService`가 없는 legacy helper 경로는 여전히 base metadata를 사용한다.
- 카드 level, DigiXros, Assembly, permanent-level static metadata 변경은 이번 66Y 범위에 포함하지 않았다.
- full-card source parity evidence는 여전히 보수적 `NotRun` 상태다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
