# 66I continuous/static keyword descriptor scope

## 원본 mapping

DCGO 원본의 static keyword factory는 keyword 자체를 카드 정의 metadata에 평탄화하지 않고, source가 현재 유효한 위치에 있고 조건을 만족할 때 keyword를 부여한다.

참조한 source:

- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs`

원본 `BlockerSelfStaticEffect`, `JammingSelfStaticEffect`, `RushSelfStaticEffect`, `RebootSelfStaticEffect`, `CollisionSelfStaticEffect`는 공통적으로 `CardEffectCommons.IsExistOnBattleAreaDigimon(card)`와 선택 조건을 확인한 뒤 해당 source permanent에 keyword를 적용한다. RL.Engine의 66I 구현은 이를 `ContinuousKeywordDescriptor`로 대응한다.

## RL.Engine 구조

- `IContinuousKeywordCardScript`는 card script가 keyword descriptor를 생성하는 인터페이스다.
- `ContinuousKeywordDescriptor`는 source card instance, source permanent, controller, target kind, keyword, condition을 보존한다.
- `ContinuousEffectSourceCollector.CollectKeywords`는 field top, inherited source, linked card, face-up security, hand, trash, executing source를 deterministic order로 수집한다.
- `BattleKeywordService.HasKeyword`는 permanent metadata, definition metadata, continuous keyword descriptor를 같은 query 경계에서 합산한다.
- `BattleKeywordService.EnsureSupportedKeywords`는 continuous descriptor가 부여한 unsupported keyword도 명시적으로 실패시킨다.

## 테스트 evidence

- `Continuous static keyword field source grants Blocker`
- `Continuous static keyword inherited source stops after move`
- `Continuous static keyword condition gates keyword`
- `Continuous static keyword replay deterministic`

이 테스트들은 card script fixture가 descriptor를 제공하고, `BattleKeywordService`와 block window/replay 경로가 해당 descriptor를 실제로 소비하는지 확인한다.

## 남은 blocker

`ContinuousOrStaticEffect`는 아직 `Verified`가 아니다. 남은 범위는 다음과 같다.

- digivolution/link requirement static effects
- trait/name/text metadata 기반 condition
- cost/restriction/immunity static interfaces
- Pierce처럼 원본에서 trigger body로 처리되는 keyword 흐름
- generated full-card pool parity evidence

위 범위가 해결되기 전에는 C0039 이후 card-porting batch를 실행하지 않는다.
