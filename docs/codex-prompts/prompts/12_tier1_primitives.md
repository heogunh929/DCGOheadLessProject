# 12 - Tier1 Common Primitive 이식

> 사용법:
> Codex에서 이 파일을 `/mention`한 뒤, "12_tier1_primitives.md 단계만 수행해줘"라고 요청한다.


중요:
이 작업은 신규 엔진 개발이 아니라 기존 `DCGO/` 하위 DCGO Unity battle 로직을 RL headless engine으로 이식하는 작업이다.
원본 Unity 구현을 Source of Truth로 삼아야 한다.

분석, 진행상황, 작업 계획, 변경 요약, 테스트 결과, TODO는 모두 한국어로 작성한다.
코드 식별자, 파일명, 클래스명, 메서드명은 영어를 사용한다.

`src/DCGO.RL.Engine`에는 UnityEngine, Photon, MonoBehaviour, GameObject, Coroutine, Unity UI 의존성을 추가하지 않는다.
기존 `DCGO/` 하위 Unity battle 파일은 명시 요청 없이 수정하지 않는다.
지원하지 않는 메커니즘은 silent no-op 하지 말고 `UnsupportedMechanicException` 또는 명시적 validation failure로 실패시킨다.

엔진 완성 전 trace/self-play는 학습 데이터가 아니라 검증 데이터다.

이 작업은 개별 CardEffect 포팅이 아니라, CardEffect와 복합 메커니즘이 사용할 공통 primitive와 효과 처리 기반을 이식하는 작업이다.

참조 원본:
- `DCGO/Assets/Scripts/Script/CardController.cs`
- `DCGO/Assets/Scripts/Script/CardEffectCommons.cs`
- `DCGO/Assets/Scripts/Script/AutoProcessing.cs`
- `DCGO/Assets/Scripts/Script/AttackProcess.cs`
- `DCGO/Assets/Scripts/Script/ICardEffect.cs`
- `DCGO/Assets/Scripts/Script/MultipleSkills.cs`
- `DCGO/Assets/Scripts/Script/SkillInfo.cs`

목표:
개별 카드효과를 포팅하기 전에 필요한 Tier1 공통 primitive를 구현한다.

Primitive:
- MoveCard
- Draw
- Shuffle
- Reveal
- Search
- Trash
- AddSecurity
- RemoveSecurity
- DeletePermanent
- DestroyPermanent
- Suspend
- Unsuspend
- ModifyMemory
- ModifyDP
- PlayCard
- Digivolve
- PlayWithoutPayingCost
- DigivolveByEffect
- SecurityCheck
- Battle
- CreateToken

Effect infrastructure:
- EffectTiming
- EffectContext
- EffectQueue
- TriggerCollector
- EffectResolution
- SelectionRequest
- SelectionResult
- OncePerTurnTracker
- BackgroundEffect 처리
- OptionalEffect 처리 경계
- UnsupportedMechanicException

RuleProcessor:
- process-until-stable loop
- max iteration guard
- game end processing
- breeding area 부적합 permanent 처리
- DP zero 이하 처리
- invalid link placeholder 처리
- face-down permanent 처리
- location consistency check

주의:
- 개별 CardEffect 클래스는 포팅하지 않는다.
- keyword 효과도 이 작업에서는 가능하면 primitive hook만 만들고, 실제 keyword 구현은 별도 작업으로 둔다.
- 지원하지 않는 메커니즘은 silent no-op 금지.

테스트:
- 각 primitive 단위 테스트
- RuleProcessor 안정화 테스트
- DP 0 이하 permanent 처리 테스트
- EffectQueue가 timing별 trigger를 수집하는 테스트
- Optional selection request 생성 테스트
- Unsupported mechanic이 명시적으로 실패하는 테스트
- Minimal Playable Battle 테스트가 계속 통과해야 함

문서:
- `docs/rl-engine/tier1-primitives.md` 업데이트
- `docs/rl-engine/effect-system.md` 생성 또는 업데이트

완료 후:
- 원본 AutoProcessing/CardEffectCommons/CardController에서 어떤 책임을 이식했는지
- 아직 미지원인 primitive
- 개별 CardEffect 포팅 전 남은 위험 요소
- 실행한 테스트
를 한국어로 요약하라.

