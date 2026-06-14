# 복잡 메커니즘 정리

## 정책

지원하지 않는 메커니즘은 silent no-op 하지 않는다. 구현되지 않은 메커니즘이 decklist나 action sequence에 등장하면 `UnsupportedMechanicException` 또는 deck validation failure로 실패해야 한다.

## Minimal 범위 밖 메커니즘

다음 항목은 원본에 구조가 존재하지만 Minimal Playable Battle 이후 별도 단계에서 지원 여부를 결정한다.

- Jogress/DNA digivolution: `PlayCardAction` payload의 `JogressEvoRootsFrameIDs`, `CardSource.jogressCondition`, `PlayCardClass.SetJogress()`, `PlayPermanentClass.SetJogress()`에 걸쳐 있다.
- Burst digivolution: `BurstTamerFrameID`, `CardSource.burstDigivolutionCondition`, tamer bounce와 turn end 처리까지 포함한다.
- App Fusion: `AppFusionFrameIDs`, linked card source 추가, `Permanent.IsAppFusion` 상태가 필요하다.
- DigiXros: `CardSource.digiXrosCondition`, `SelectDigiXrosClass`, play cost reduction과 source 추가가 필요하다.
- Assembly: `CardSource.assemblyCondition`, `SelectAssemblyClass`, 여러 후보 선택과 cost reduction이 필요하다.
- Link: `CardSource.linkCondition`, `Permanent.LinkedCards`, `LinkedMax`, link condition/count rule process가 필요하다.
- ACE Overflow: field 이탈 또는 특정 zone 이동에서 `AceOverflowClass`가 memory loss를 처리한다.
- face-up security: security zone visibility와 face-up effect timing이 필요하다.
- background/cut-in/counter effect: `AutoProcessing`과 `autoProcessing_CutIn`의 stack 분리가 필요하다.

이 문서의 범위는 play/digivolve/cost/material/source/link 구조를 바꾸는 복합 메커니즘이다. Blocker, Piercing, Jamming, Rush, Reboot, Retaliation, Collision 같은 공격/전투 keyword는 `docs/rl-engine/battle-keywords.md`에서 별도로 다룬다.

## CardEffect보다 먼저 처리하는 이유

Jogress, Burst Digivolution, App Fusion, DigiXros, Assembly, Link는 `ActivateICardEffect.Activate()` 안에서만 실행되는 카드별 효과가 아니다. 원본에서는 main phase action payload, legal target frame 계산, material selection, cost reduction/fixed cost, `Permanent.cardSources`, `Permanent.LinkedCards`, `CardSource.CanPlayCardTargetFrame()`에 직접 관여한다. 이 계층이 없으면 이후 개별 `CardEffect`가 정확한 play/digivolve 상태를 전제로 동작할 수 없다.

## 13 단계 구현 범위

이번 단계에서는 카드별 `CardEffect` 구현 없이 play/evolution pipeline을 바꾸는 공통 메커니즘만 추가했다. 원본 기준점은 `PlayCardAction`, `PlayCardClass.PlayCard`, `PlayPermanentClass.PlayPermanent`, `CardSource.GetPayingCostWithBaseCost`, `SelectDigiXrosClass`, `SelectAssemblyClass`, `SelectBurstDigivolutionEffect`, `SelectAppFusionEffect`, `AddLinkRequirement`, `Link`, `Permanent.LinkedCards`, `AceOverflowClass`이다.

추가한 도메인 모델은 다음과 같다.

- `Mechanic`, `PlayMode`, `EvolutionMode`
- `EvolutionRequirement`, `PlayRequirement`, `MaterialRequirement`
- `EvolutionCandidate`, `PlayCandidate`, `MaterialCandidate`, `CostCandidate`

`CardDefinition`은 복합 메커니즘 선언을 갖고, legal action 생성과 실행은 이 선언을 기준으로만 동작한다. 아직 카드별 C# condition 함수를 포팅하지 않았기 때문에, 선언되지 않은 복합 조건은 추측하지 않는다.

## 13 단계 지원 메커니즘

| 메커니즘 | 현재 처리 |
| --- | --- |
| Jogress | 손패 카드의 `EvolutionRequirement.Mode = Jogress`로 legal action을 만든다. 두 battle area permanent를 source로 검증하고, 비용을 지불한 뒤 새 permanent의 evolution source로 옮기며 진화 드로우를 수행한다. |
| Burst Digivolution | target permanent와 tamer permanent를 검증한다. tamer top card는 hand로 되돌리고, 손패 카드를 target 위에 올린 뒤 `IsBurstDigivolved`를 표시하고 진화 드로우를 수행한다. |
| App Fusion | target permanent와 linked card 후보를 검증한다. linked card를 target의 evolution source로 옮기고 손패 카드를 진화시킨 뒤 `IsAppFusion`을 표시한다. |
| DigiXros Play | material 후보를 hand, battle area, trash, source, linked card 범위에서 requirement로 산출한다. 선택한 material 수에 따라 `ReduceCostPerMaterial`을 적용하고, play 후 material을 evolution source 하단에 넣는다. |
| Assembly Play | trash material 후보를 requirement로 산출한다. 요구 수량을 만족하면 assembly reduction을 적용하고, play 후 material을 evolution source 하단에 넣는다. |
| Link | battle area의 link card 또는 손패 link card를 target permanent에 연결한다. link cost와 `LinkedMax`를 검증하고, 초과 linked card는 rule process에서 trash로 정리한다. |
| Delay Option | delay option을 battle area permanent로 둘 수 있는 action과 상태 플래그를 추가했다. delay effect 자체는 이후 CardEffect 단계에서 처리한다. |
| ACE/Overflow | overflow memory loss를 적용하는 공통 hook을 추가했다. 실제 트리거 지점 확대는 이후 zone 이동/effect timing 통합에서 보강한다. |

모든 실행 경로는 `ZoneMover`를 사용해 zone 이동을 수행한다. 지원하지 않는 mechanic/action 조합은 조용히 성공시키지 않고 `UnsupportedMechanicException` 또는 validation 실패로 남긴다.

## 13 단계 의도적 제한

다음 항목은 구조만 열어 두었고, 카드별 효과나 후속 keyword 단계에서 이어서 다룬다.

- Burst Digivolution의 turn end top source trash 예약 처리
- 원본 `Func` 기반 카드별 jogress/burst/app fusion/DigiXros/assembly/link condition 포팅
- replacement, cut-in, counter timing과 결합된 복합 메커니즘 처리
- ACE Overflow의 모든 field 이탈/zone 이동 트리거 연결
- DigiXros/Assembly의 UI별 상세 선택 순서와 공개/비공개 정보 표시
- Delay Option의 activate timing과 개별 effect body

## Tier1 공통 primitive 목록

Tier1 primitive는 개별 card effect를 나중으로 미루면서도 기본 전투를 구성하기 위한 공통 조작이다.

| Primitive | 필요한 이유 |
| --- | --- |
| `DrawCards` | setup, draw phase, digivolve draw, 여러 card effect가 공유한다. |
| `MoveCardToHand` | draw, add to hand, trash/security/deck에서 hand로 이동하는 효과의 기반이다. |
| `MoveCardToTrash` | discard, destroy, battle, security trash, source trash의 기반이다. |
| `MoveCardToDeckTop/Bottom` | mulligan, deck bounce, recovery 변형, deck 조작의 기반이다. |
| `MoveCardToSecurity` | setup security, recovery, security placement 효과의 기반이다. |
| `MoveCardToExecution` | option use, security check reveal, effect resolving area를 표현한다. |
| `CreatePermanent` | play, hatch, token, delay option permanent의 기반이다. |
| `RemovePermanentFromField` | destroy, bounce, source 이동, rule process 정리의 공통 전처리다. |
| `AddDigivolutionCardsTop/Bottom` | digivolve, DigiXros, inherited effect 구조의 기반이다. |
| `RemoveDigivolutionCards` | Digi-Burst, de-digivolve, source trash 효과의 기반이다. |
| `AddLinkCard/RemoveLinkCard` | Link, AppFusion, link rule process의 기반이다. |
| `Suspend/UnsuspendPermanent` | active phase, attack cost, blocker, suspend effects의 기반이다. |
| `PayMemory/SetMemory` | play cost, digivolve cost, pass memory reset, overflow를 통합한다. |
| `BattleCompareAndDestroy` | DP battle, security Digimon battle, battle destruction trigger의 기반이다. |
| `SecurityCheck` | attack win/loss와 security effect 처리의 중심이다. |
| `ProcessUntilStable` | rule process와 effect queue 반복을 안전하게 종료하기 위해 필요하다. |

모든 primitive는 내부에서 `ZoneMover`를 사용해야 한다. 카드가 여러 zone에 남는 방식의 직접 list 조작은 허용하지 않는다.

## Validation에서 막아야 할 항목

초기 card pool/deck validator는 다음을 탐지해야 한다.

- `CardSource.HasDigiXros`, `HasAssembly`, `jogressCondition`, `burstDigivolutionCondition`, `appFusionCondition`, `linkCondition`이 필요한 카드
- 아직 구현되지 않은 `EffectTiming` 또는 effect interface를 사용하는 카드
- face-up security, token, ACE Overflow, counter, cut-in 같은 지원 전 메커니즘
- UI 선택 helper에만 구현되어 있고 headless `SelectionRequest`로 표현되지 않은 효과

문서화되지 않은 미지원 메커니즘은 실패로 처리한다.
