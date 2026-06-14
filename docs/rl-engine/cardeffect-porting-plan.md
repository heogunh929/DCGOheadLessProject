# CardEffect 포팅 계획

## 원본 구조

`ICardEffect`는 effect source, source permanent, effect name, hash string, max count per turn, optional/declarative/background/counter/security/inherited/link flags, trigger condition, activate condition을 가진다. `ActivateICardEffect`는 `Activate(Hashtable hash)`를 구현하고, extension method가 optional 선택, UI 효과 표시, 실제 실행, after-effects trigger를 연결한다.

`EffectTiming` enum은 trigger 지점을 정의한다. 주요 timing은 `OnStartTurn`, `OnDraw`, `OnStartMainPhase`, `OnDeclaration`, `OnAllyAttack`, `OnCounterTiming`, `OnStartBattle`, `OnEndBattle`, `OnEndAttack`, `OnEndTurn`, `SecuritySkill`, `RulesTiming`, `AfterEffectsActivate` 등이다.

`CardEffectCommons`는 card effect 구현이 직접 호출하는 helper 집합이다. play, option use, token 생성, target suspend/delete/bounce, digivolve from hand/trash/execution, draw and discard 같은 복합 helper가 들어 있다.

## 나중으로 미루기 위한 인터페이스 방향

초기 RL.Engine은 개별 카드효과를 바로 포팅하지 않는다. 대신 다음 인터페이스 경계를 먼저 만든다.

- `IEffectDefinition`: 카드 id, effect id, timing, flags, max count, source kind를 설명한다.
- `IEffectCondition`: typed `EffectContext`를 받아 trigger/use 가능 여부를 계산한다.
- `IEffectOperation`: engine primitive를 호출하는 순수 effect body다.
- `EffectContext`: 원본 `Hashtable`을 대체하는 typed context다. 예: `Cards`, `Permanents`, `AttackingPermanent`, `DefendingPermanent`, `CardEffect`, `Root`.
- `EffectQueue`: timing별 effect 수집, 중복 실행 방지, optional 선택 요청, background effect 실행을 담당한다.
- `UnsupportedEffectDefinition`: 아직 포팅되지 않은 카드효과를 명시 실패로 표현한다.

15 단계에서는 카드별 effect body를 넣기 전에 다음 실행/검증 경계를 추가했다.

- `ICardScript`: 원본 `ICardEffect` 묶음을 headless card script 단위로 포팅하기 위한 인터페이스다.
- `CardScriptRegistry`: card id 또는 `CardEffectClassName`으로 script를 찾는다.
- `NoEffectCardScript`: 명시적으로 효과가 없는 카드만 no-effect로 허용한다.
- `UnsupportedCardScript`: 미등록/미포팅 효과의 descriptor 생성과 실행을 `UnsupportedMechanicException`으로 실패시킨다.
- `CardEffectPortingStatus`: `Unsupported`, `NoEffect`, `StubbedForValidation`, `Implemented`, `Verified` 상태를 표현한다.
- `DeckMechanicValidator`: decklist 안의 mechanic, battle keyword, card script 상태를 검증한다.

`CardScriptExecutionContext`는 public `GameState` 속성을 제공하지 않고 `Tier1PrimitiveService`를 통해 상태 변경을 수행하도록 경계를 둔다. C#에서 악의적인 직접 수정을 완전히 금지하는 sandbox는 아니지만, 포팅되는 card script의 표준 실행 경로와 테스트 fixture는 primitive service 사용을 기준으로 한다.

## CardEffect보다 먼저 포팅할 공통 규칙

Jogress, Burst Digivolution, App Fusion, DigiXros, Assembly, Link, Delay Option, ACE/Overflow는 개별 카드효과로 취급하지 않는다. 원본에서 이 메커니즘들은 `PlayCardAction`, `PlayCardClass`, `PlayPermanentClass`, `CardSource`의 legal/cost query, selection helper, `Permanent`의 source/link 상태에 걸쳐 있어 play/digivolve pipeline 자체를 바꾼다. 따라서 `docs/rl-engine/complex-mechanics.md` 단계에서 먼저 구현하거나 deck validation에서 명시 실패시킨다.

Battle keyword도 개별 `CardEffect` 포팅보다 먼저 별도 단계에서 다룬다. Blocker, Security Attack +N, Piercing, Jamming, Rush, Reboot, Retaliation, Decoy, Collision은 `AttackProcess`, `IBattle`, `ISecurityCheck`, `Permanent` keyword query에 영향을 주므로 `docs/rl-engine/battle-keywords.md`의 공통 hook 설계를 따른다.

13 단계에서 복합 메커니즘은 카드별 effect body가 아니라 선언형 requirement와 cost/material resolver로 분리했다. `CardDefinition`의 `EvolutionRequirement`, `PlayRequirement`, `MaterialRequirement`가 legal action과 execution을 결정하고, `ComplexMechanicService`가 `ZoneMover` primitive를 통해 상태를 변경한다. 이 구조 위에서만 이후 CardEffect가 "이미 플레이/진화/연결된 상태"를 전제로 동작할 수 있다.

14 단계에서 battle keyword는 `BattleKeywordService`와 `BattleKeyword` capability로 분리했다. Blocker, Security Attack +N, Piercing, Jamming, Rush, Reboot, Retaliation, Collision은 attack/battle/security/active phase hook에서 처리한다. Decoy는 replacement selection과 effect-origin 판정이 필요하므로 아직 `UnsupportedMechanicException`으로 실패한다.

CardEffect 단계로 넘긴 항목은 다음과 같다.

- Delay Option의 실제 발동 effect
- Burst Digivolution의 turn end source trash 예약 effect
- ACE Overflow의 모든 timing trigger 연결
- replacement/counter/cut-in과 얽힌 복합 메커니즘 후처리
- Blocker 선택 결과 실행, counter/block timing trigger, attack target changed trigger
- face-up security/player continuous keyword provider
- Decoy replacement selection과 deletion immunity 계열 keyword
- 카드별 condition 함수와 text-specific effect body

## 포팅 순서

1. effect timing enum과 typed context를 먼저 정의한다.
2. no-op이 아니라 실패하는 unsupported effect registry를 만든다.
3. Minimal Playable Battle에 필요한 built-in rule effect만 구현한다.
4. Tier1 primitive를 effect operation에서 호출 가능하게 만든다.
5. 복합 플레이/진화 메커니즘과 battle keyword가 구현 또는 명시 미지원으로 분류된 뒤 card pool을 정한다.
6. 카드별 포팅 상태표를 만든다.
7. 카드별 effect를 Source of Truth 원본 파일과 1:1로 비교하며 포팅한다.
8. 각 card effect는 scripted scenario와 replay trace를 가진다.

15 단계 완료 후 기준으로 1-2번의 기반은 존재한다. 6번의 상태표 문서는 `docs/rl-engine/cardeffect-porting-status.md`에 만들었고, 실제 card pool별 상태 채우기는 다음 단계에서 진행한다.

## Silent no-op 방지

다음 경우는 반드시 실패해야 한다.

- card definition에 `CardEffectClassName`이 있는데 registry에 없다.
- card definition에 `CardEffectClassName`이 없더라도 `NoEffectCardScript`로 명시 등록되지 않았다.
- effect timing은 등록됐지만 operation body가 없다.
- effect가 `SelectionRequest`가 필요한데 adapter가 요청을 처리하지 못한다.
- effect helper가 아직 구현되지 않은 primitive를 요구한다.
- card text상 필요한 keyword/mechanic이 deck validator에 문서화되지 않았다.

## CardEffect 상태표

엔진 완성 판정 전까지 학습 대상 card pool의 모든 effect는 상태표를 가져야 한다.

- `Unsupported`: deck validation에서 실패한다.
- `StubbedForValidation`: 검증용으로 명시 제한된 동작만 한다. 학습 card pool에는 넣지 않는다.
- `Implemented`: 원본 trace와 scripted scenario가 있다.
- `Verified`: replay validation을 통과했다.

이 상태표는 `docs/rl-engine` 아래 별도 문서로 유지하고, `engine-completion-checklist.md`의 “미지원인데 조용히 무시되는 카드 없음” 항목과 연결한다.

현재 상태표 문서: `docs/rl-engine/cardeffect-porting-status.md`.
