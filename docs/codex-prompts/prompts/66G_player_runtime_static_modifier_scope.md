# 66G player runtime static modifier scope

Batch id: `66G_player_runtime_static_modifier_scope`

## 목표

`ContinuousOrStaticEffect`의 남은 blocker 중 원본 `Player.EffectList(EffectTiming.None)`에 대응되는 player-level runtime stat modifier 범위를 검증한다.

## 범위

- DCGO 원본 `Player.EffectList(EffectTiming.None)`은 player에 걸린 permanent/duration effect list를 반환한다.
- RL.Engine에서 현재 대응 가능한 범위는 `TemporaryModifier`로 상태에 저장되는 player-target DP, SecurityAttack, SecurityDigimonDP runtime stat modifier다.
- 이 항목은 hand/trash/executing static requirement source, trait/name/text metadata, unsupported static effect interface를 구현하지 않는다.
- C0039 또는 다른 card-porting batch를 실행하지 않는다.

## 완료 조건

- player-target DP modifier가 owner battle area Digimon에만 적용되고 breeding/opponent에는 적용되지 않는 테스트
- player-target SecurityAttack modifier가 owner Digimon 전체에 적용되는 테스트
- player-level runtime modifier가 `Clone`, `RestoreFrom`, `ComputeStateHash`에 포함되는 테스트
- player-level runtime modifier가 `ReplayRunner` final state hash와 invariant에서 보존되는 테스트
- capability truth audit partial evidence 갱신
- queue/progress/source mapping 문서 갱신

## 금지

- `DCGO/Assets` 수정 금지
- core service CardId 분기 금지
- `ContinuousOrStaticEffect`를 Verified로 승격 금지
- blocker 문서화만으로 card-porting batch done 처리 금지
