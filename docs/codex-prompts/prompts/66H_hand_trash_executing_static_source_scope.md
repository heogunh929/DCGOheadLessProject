# 66H hand/trash/executing static source scope

Batch id: `66H_hand_trash_executing_static_source_scope`

## 목표

`ContinuousOrStaticEffect`의 source scope 중, 원본 `CardSource`가 hand/trash/executing zone에서도 정적 효과 source로 참조될 수 있는 경계를 RL.Engine에 추가한다.

## 범위

- `ContinuousEffectSourceKind.Hand`
- `ContinuousEffectSourceKind.Trash`
- `ContinuousEffectSourceKind.Executing`
- `ContinuousEffectSourceCollector` player-zone source enumeration
- 기존 field top / inherited / linked / face-up security source scope regression
- capability truth audit evidence 갱신

## 비범위

- C0039 또는 이후 card-porting batch 실행
- CardId 분기
- trait/name/text metadata 구현
- digivolution/link requirement static effect 구현
- cost/restriction/immunity static interfaces 구현
- RL Environment, Observation, Reward, Dataset, Trainer 구현

## 완료 조건

- hand/trash/executing source가 명시적으로 guard된 `IContinuousCardScript`에서만 descriptor를 생성한다.
- 기존 field/inherited continuous scripts가 player zone에서 오작동하지 않는다.
- 실행 테스트와 전체 regression이 통과한다.
- `ContinuousOrStaticEffect`는 `PartiallyImplemented`로 유지한다.
