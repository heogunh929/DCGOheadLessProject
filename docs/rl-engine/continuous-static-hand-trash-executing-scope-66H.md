# 66H Continuous/Static Hand, Trash, Executing Source Scope

## 결정

66H는 `ContinuousOrStaticEffect` 전체를 완료하지 않는다. 원본 `CardSource` 기반 정적 효과가 field/inherited/linked/face-up security 외에도 hand, trash, executing zone source에서 생성될 수 있는 수집 경계를 추가한 작업이다.

## 원본 Mapping

DCGO 원본의 card effect body는 손패, 트래시, 실행 중인 option/source card에서 `CardEffectFactory` static effect 또는 rule effect를 만들 수 있다. 특히 hand/trash/executing source는 비용 변경, 진화 조건 추가, text/name/trait 조건, option 실행 중 source 조건과 연결된다.

RL.Engine 66H는 이 전체 의미를 구현하지 않고, 다음 source scope를 `ContinuousEffectSourceCollector`가 script context로 전달할 수 있게 한다.

- `ContinuousEffectSourceKind.Hand`
- `ContinuousEffectSourceKind.Trash`
- `ContinuousEffectSourceKind.Executing`

각 카드별 script는 반드시 `context.SourceKind`를 확인해야 한다. 기존 field top, inherited, linked, face-up security script는 guard가 있으므로 hand/trash/executing zone에서 descriptor를 만들지 않는다.

## 구현

- `ContinuousEffectSourceKind`에 `Hand`, `Trash`, `Executing` 추가
- `ContinuousEffectSourceCollector`가 각 player의 `Hand`, `Trash`, `Executing` list를 deterministic order로 순회
- player-zone source는 `SourcePermanent = null`, `Controller = CardInstance.Owner`로 전달
- core service에 CardId 분기 없음

## 검증

- `Continuous hand source applies only from hand`
- `Continuous trash source applies from trash`
- `Continuous executing source applies during execution`
- 기존 `Continuous` targeted regression

## 남은 Blocker

`ContinuousOrStaticEffect`는 여전히 `PartiallyImplemented`다.

- digivolution/link requirement static effects
- trait/name/text metadata 기반 조건
- cost/restriction/immunity static interfaces
- hand/trash/executing source를 사용하는 실제 generated card effect body parity
- generated full-card replay/parity evidence
