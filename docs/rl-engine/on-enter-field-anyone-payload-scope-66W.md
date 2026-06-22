# 66W OnEnterFieldAnyone Payload Scope

## 결정

이번 foundation 작업은 `OnEnterFieldAnyone`를 개별 카드 body로 이식하지 않는다.
대신 원본 Unity의 enter-field event payload를 RL.Engine trigger pipeline에서 공통으로 전달할 수 있는 최소 기반을 추가한다.

`OnEnterFieldAnyone`는 `OnPlay` 또는 `WhenDigivolving`으로 일괄 평탄화할 수 없다. 원본에는 자기 자신이 등장할 때 실행되는 on-play/when-digivolving branch와, 다른 permanent가 등장할 때 반응하는 전역 branch가 같은 timing 안에 함께 존재한다.

## Source-of-Truth 확인

읽기 전용 원본은 `E:\headlessDCGO\DCGO\Assets`를 사용했다.

확인한 주요 원본 파일은 다음과 같다.

| 파일 | 확인 내용 |
| --- | --- |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardController.cs` | play/digivolve 완료 뒤 `OnEnterFieldHashtable` 기반 trigger가 실행된다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectFactory.cs` | `EffectTiming.OnEnterFieldAnyone` 안에서 `IsOnPlay`와 `IsWhenDigivolving` compatible class를 생성한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\ICardEffect.cs` | `IsOnPlay`와 `IsWhenDigivolving`는 hashtable payload를 검사한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\Script\CardEffectCommons\CanUseEffects\PermanentEnterField\PermanentEnterField.cs` | enter-field payload의 `IsEvolution`와 entered permanent list를 보고 발동 가능성을 판정한다. |
| `E:\headlessDCGO\DCGO\Assets\Scripts\CardEffect\AD1\Red\AD1_001.cs` | 같은 `OnEnterFieldAnyone` timing에 self on-play/when-digivolving branch와 all-turns global branch가 함께 존재한다. |

## RL.Engine 추가 범위

- `EnterFieldEventPayload`를 추가해 `Card`, `Permanent`, `EnteredCards`, `EnteredPermanents`, `IsEvolution`, `Played`, `Digivolved` payload key를 공통화했다.
- `PlayCardService`는 self `OnPlay` group 뒤에 global `OnEnterFieldAnyone` group을 같은 prepared sequence tail로 연결한다.
- `DigivolveService`는 self `WhenDigivolving` group 뒤에 global `OnEnterFieldAnyone` group을 같은 prepared sequence tail로 연결한다.
- `TriggerPipelineService.RunPreparedSequence`를 public API로 열어 selection pause/resume 뒤에도 다음 prepared group이 이어지도록 했다.

## 검증

추가 테스트는 다음 세 가지를 고정한다.

- `TriggerPipeline OnEnterFieldAnyone play payload invokes global descriptor`
- `TriggerPipeline OnEnterFieldAnyone digivolve payload invokes global descriptor`
- `TriggerPipeline OnEnterFieldAnyone tail resumes after OnPlay selection`

이 검증은 payload 전달과 continuation chaining에 대한 foundation evidence다. full-card parity evidence나 개별 source body 구현 완료를 뜻하지 않는다.

## 남은 제한

- 원본의 전체 source ordering parity는 아직 검증하지 않았다.
- multi-permanent enter event, Jogress, DigiXros, Assembly, security/from-effect play variants는 아직 payload variant가 완성되지 않았다.
- generated full-card source scaffold는 여전히 `Verified`로 승격하지 않는다.
- C0039 이후 card-porting batch와 개별 `CardEffect` body 구현은 계속 금지한다.
