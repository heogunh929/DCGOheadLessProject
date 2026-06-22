# 66F Continuous/Static Source Scope

## 결정

66F는 `ContinuousOrStaticEffect` 전체를 완료하지 않고, 원본 static/continuous lookup 범위 중 현재 engine 모델로 보존 가능한 source scope를 확장한 항목이다.

## 원본 Mapping

원본 `DCGO/Assets/Scripts/Script/Permanent.cs`의 stat query는 다음 source를 함께 조회한다.

- `permanent.EffectList(EffectTiming.None)`
- linked card 효과: `EffectList_ForCard` 내부의 `cardEffect.IsLinkedEffect && cardSource.IsLinked`
- face-up security 효과: security card source가 face-up인 경우 `cardSource.EffectList(EffectTiming.None)`
- player runtime effect: `player.EffectList(EffectTiming.None)`

RL.Engine 66F는 이 중 domain에 명시 상태가 있는 다음 source만 구현했다.

- `ContinuousEffectSourceKind.LinkedCard`
- `ContinuousEffectSourceKind.FaceUpSecurity`

face-down security는 원본과 같이 static source로 수집하지 않는다.

## 구현

- `ContinuousEffectSourceCollector`가 battle/breeding permanent의 top, inherited, linked card를 순회한다.
- 각 player의 face-up security card를 continuous source로 순회한다.
- face-up security source는 `SourcePermanent = null`, `Controller = card.Owner`로 descriptor context를 만든다.
- 기존 `FieldTop`/`InheritedSource` script guard는 유지된다.
- core service에는 특정 CardId 분기를 추가하지 않았다.

## 검증

- `Continuous linked source applies from linked zone`
- `Continuous face-up security source applies`
- `Continuous face-down security source is ignored`
- `Continuous` targeted regression: 26 tests passed

## 남은 Blocker

`ContinuousOrStaticEffect`는 아직 `PartiallyImplemented`다. 다음 범위는 별도 queue가 필요하다.

- player-level runtime-added non-stat static effects
- hand/trash/executing static requirement source
- digivolution/link requirement static effects
- trait/name/text metadata 기반 조건
- full-card replay/parity evidence
