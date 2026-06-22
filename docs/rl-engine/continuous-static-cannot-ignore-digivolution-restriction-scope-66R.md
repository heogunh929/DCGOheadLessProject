# 66R cannot-ignore digivolution restriction scope

## 목적

이번 foundation 작업은 원본 `ICannotIgnoreDigivolutionConditionEffect`를 RL.Engine의 공통 static requirement layer에 연결하는 것이다.
개별 `CardEffect` body를 구현하지 않으며, C0039 이후 card-porting batch도 실행하지 않는다.

## 원본 source

- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\Player.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffects\CannotIgnoreDigivolutionConditionClass.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectInterfaces.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory\AddDigivolutionRequirement.cs`
- `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\BT8\Black\BT8_059.cs`

원본 `Player.CanIgnoreDigivolutionRequirement(...)`는 양쪽 플레이어의 field permanent effect와 player effect를 확인한다.
`ICannotIgnoreDigivolutionConditionEffect.cannotIgnoreDigivolutionCondition(...)`가 true이면 ignore permission을 사용할 수 없다.
`BT8_059`는 field top static effect로 "Players can't ignore digivolution requirements"를 제공한다.

## RL.Engine 대응

- `CannotIgnoreDigivolutionRequirementDescriptor`
- `CannotIgnoreDigivolutionRequirementEvaluationContext`
- `CannotIgnoreDigivolutionRequirementEvaluation`
- `ICannotIgnoreDigivolutionRequirementCardScript`
- `ContinuousEffectSourceCollector.CollectCannotIgnoreDigivolutionRequirements(...)`
- `StaticRequirementService.EvaluateCannotIgnoreDigivolutionRequirements(...)`

`StaticEvolutionRequirementDescriptor.IgnoreDigivolutionRequirement=true`인 requirement는 이제 cannot-ignore restriction이 적용되면 evaluation에서 제외된다.
따라서 legal action generation과 `DigivolveService` execution path가 같은 rule을 공유한다.

## 정책

- restriction descriptor는 source/target/evolving-card metadata criteria와 condition gate를 가진다.
- field top, inherited, linked, face-up security, hand, trash, executing source scope는 기존 `ContinuousEffectSourceCollector` 경계를 공유한다.
- `StaticEffectPlayerTargetKind.AnyPlayer`로 BT8_059 같은 양쪽 플레이어 대상 restriction을 표현한다.
- 빈 descriptor로 unsupported를 숨기지 않는다.
- 개별 카드 body는 구현하지 않는다.

## 검증

- `Static evolution requirement cannot-ignore restriction blocks permission`
- `Static evolution requirement cannot-ignore restriction condition gates`

## 남은 범위

`ContinuousOrStaticEffect`는 이번 작업 이후에도 `Verified`가 아니다.
66S에서 player-wide keyword grant scope는 닫혔고, 현재 남은 foundation queue는 granted trigger effect flow다.
