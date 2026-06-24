# FND-002-E OnStartBattle Manual Review

이번 문서는 `OnStartBattle`이 `NeedsSourceReview`로 남아 있던 이유를 원본 source 근거로 닫는다. 구현은 수행하지 않는다.

## 결론

- AS-IS root: `E:/headlessDCGO/DCGO/Assets`
- 분류: `SourceKnownZeroCardTiming`
- 현재 mapping status: `NotReferenced`
- 현재 CardEffect source reference: 0
- 현재 affected card: 0
- 정책: 현재 AS-IS 카드풀의 full-card completion blocker에서 제외한다.
- 재개 조건: 미래 source snapshot에서 `EffectTiming.OnStartBattle`을 참조하는 CardEffect가 발견되면 battle-start payload foundation task로 다시 연다.

## Source Evidence

- `DCGO/Assets/Scripts/Script/ICardEffect.cs:1011`
- `DCGO/Assets/Scripts/Script/CardController.cs:4527`
- `DCGO/Assets/Scripts/Script/CardController.cs:4542`
- `DCGO/Assets/Scripts/Script/CardController.cs:4549`
- `DCGO/Assets/Scripts/Script/CardController.cs:4553`
- `DCGO/Assets/Scripts/Script/CardController.cs:4554`
- `DCGO/Assets/Scripts/Script/CardController.cs:4555`
- `DCGO/Assets/Scripts/Script/CardController.cs:4557`

## Payload

- `AttackingPermanent`
- `DefendingPermanent`
- `DefendingCard`

## Mapping Audit

| API | Original | Current | Classification | CardEffect refs | Affected | Source refs | Engine refs | Test/doc refs |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| OnStartBattle | NeedsSourceReview | NotReferenced | SourceKnownZeroCardTiming | 0 | 0 | 1 | 0 | 4 |

## Source Meaning

The source dispatch occurs in CardController.Battle() after attacker/defender existence checks and before the battle auto-process/body comparison flow. The source stores snapshot Permanent copies for attacking and defending permanents, plus the defending security/card target when present.

## Policy Decision

Treat OnStartBattle as source-known NotReferenced for the current AS-IS card pool. The runtime dispatch and payload are real, but the current CardEffect corpus has zero EffectTiming.OnStartBattle references and zero affected card records. It should not keep the common API mapping gate open for the current source snapshot.

## Reopen Condition

If a future source snapshot contains any CardEffect file referencing EffectTiming.OnStartBattle, the conditional inventory override no longer applies and the timing must return as a battle-start payload foundation task with Unity/RL fixture coverage.

## Gate Snapshot

- OpenCodeReady: false
- Unknown common API count: 0
- Unsupported capability count: 15
- PartiallyImplemented capability count: 48

## Guardrails

- `src/DCGO.RL.Engine` implementation code was not modified.
- Original `DCGO/Assets` was not modified.
- No individual `CardEffect` body was implemented.
- No C0039+ card-porting was run.
- No RL Environment/Observation/Reward/Dataset/Trainer was implemented.
- Generated card status was not promoted.
- Foundation Gate numbers were recalculated from generated inventory, not edited by hand.
- No commit/push was performed.
