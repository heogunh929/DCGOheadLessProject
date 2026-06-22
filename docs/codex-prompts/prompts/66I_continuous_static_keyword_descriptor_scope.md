# 66I continuous/static keyword descriptor scope

Batch id: `66I_continuous_static_keyword_descriptor_scope`

## 목표

`ContinuousOrStaticEffect` remediation의 다음 범위로, 원본 DCGO의 source/condition 기반 static keyword grant를 headless engine의 공통 descriptor 경로에 연결한다.

## Source of Truth

- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs`
- `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs`

## 구현 범위

- `ContinuousKeywordDescriptor`를 추가해 card script가 source identity, source permanent, controller, 적용 대상, 조건, keyword를 선언할 수 있게 한다.
- `ContinuousEffectSourceCollector`가 기존 continuous stat source scope와 같은 순서로 keyword descriptor를 수집한다.
- `BattleKeywordService.HasKeyword`와 `EnsureSupportedKeywords`가 continuous keyword descriptor를 반영한다.
- field top, inherited source, source 이동, 조건 gate, replay determinism을 테스트한다.

## 비범위

- C0039 card-porting batch를 실행하지 않는다.
- CardId 분기나 core service 카드별 예외를 추가하지 않는다.
- Pierce처럼 원본에서 trigger형으로 동작하는 keyword body를 static descriptor에 합치지 않는다.
- Decoy 등 미지원 keyword를 silent no-op 처리하지 않는다.
- digivolution/link requirement static effects, trait/name/text metadata, cost/restriction/immunity static interfaces는 후속 blocker로 유지한다.

## 완료 조건

- 66I 테스트와 full regression이 통과한다.
- `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
- capability truth audit evidence에 static keyword descriptor 테스트와 replay evidence를 추가한다.
