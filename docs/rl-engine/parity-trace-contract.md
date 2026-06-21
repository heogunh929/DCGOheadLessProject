# Parity Trace Contract

이 문서는 Unity 원본 battle trace와 RL.Engine trace를 같은 rule-visible 형식으로 비교하기 위한 계약이다. 현재 구현은 RL.Engine exporter만 포함하며, UnityAdapter는 이후 같은 DTO를 채우는 별도 exporter를 구현한다. `DCGO.RL.Engine`은 UnityAdapter나 UnityEngine을 참조하지 않는다.

## Schema

현재 schema version은 `dcgo.parity.trace.v1`이다. `ParityTraceStore.Load`는 문서와 각 event의 schema version을 모두 검증하며, 다른 version은 `DomainException`으로 실패한다.

| Field | 의미 | 비고 |
| --- | --- | --- |
| `schemaVersion` | trace 계약 version | 문서와 event 양쪽에 기록 |
| `scenarioId` | fixture 또는 scenario 식별자 | 절대 경로를 기록하지 않는다 |
| `seed` | deterministic seed | event seed는 `GameState.Config.Seed`에서 온다 |
| `stepIndex` | trace event 순서 | `GameTrace` index 기준, canonical ordering |
| `kind` / `label` | event 종류와 stable label | delegate, object address, local path 사용 금지 |
| `phase`, `turnPlayer`, `turnCount`, `memory` | event 이후 공개 rule state | `StateAfter` snapshot과 동일 |
| `playerZoneCounts` | player별 zone count | hidden zone도 count만 공개 |
| `stateBefore`, `stateAfter` | rule-visible snapshot | canonical state hash 포함 |
| `action` | replay 가능한 action payload | 지원하지 않는 action은 명시 실패 |
| `move` | zone movement payload | `MoveReason`, source/destination zone 포함 |
| `decision` | pending decision/request | request id, player, token, candidates 포함 |
| `selection` | submitted decision result | player, token, request id, selected targets 포함 |
| `trigger` | trigger stable id 후보 | 현재 RL trace label 기반 최소 식별자, Unity comparer 단계에서 timing/source 확장 예정 |
| `outcome` | zone move/game result/battle/security 요약 | security check와 battle destroy label에서 최소 outcome payload를 만든다 |
| `canonicalStateHash` | event 이후 canonical state hash | replay/state 비교 기준 |

## RuleVisibleSnapshot

`RuleVisibleSnapshot.Capture(GameState)`는 hidden information을 직접 공개하지 않는다.

| 영역 | trace 출력 |
| --- | --- |
| Deck | count만 출력 |
| DigiEggDeck | count만 출력 |
| Hand | count만 출력 |
| face-down Security | count만 출력 |
| face-up Security | card instance/definition/owner/zone 공개 |
| Trash/Lost/Executing/Revealed/OutsideGame | ordered card identity 공개 |
| BattleArea/BreedingArea | permanent id, owner/controller, top/source/link, suspended, frame 공개 |
| TemporaryModifier | stable id, source/target, controller, kind, amount, duration, created/expiry 공개 |
| GameResult | kind, winner, reason 공개 |

공개 card identity는 `InstanceId`, `DefinitionId`, `Owner`, `Zone`, `IsFaceUp`, `PermanentId`만 사용한다. canonical ordering은 player id, permanent frame/id, modifier stable id/source/target, trace event index 순서로 고정한다.

## Exporter Boundary

RL.Engine exporter는 `GameTrace`에 저장된 `TraceEvent.RuleVisibleStateBefore/After`를 사용한다. snapshot이 없는 trace를 parity trace로 내보내려 하면 실패한다. 이는 과거 action replay trace와 parity comparison trace를 섞지 않기 위한 guard다.

Unity exporter는 같은 DTO를 채우되, Unity 객체 참조나 로컬 파일 경로를 trace payload에 넣으면 안 된다. source mapping에는 원본 파일 경로를 문서와 fixture metadata로만 남기고, runtime trace에는 scenario id와 stable source/effect id만 남긴다.

## Example Fixture

현재 RL.Engine fixture는 테스트 `ParityTrace golden scenario export`에서 ST1-11 dynamic SecurityAttack scenario를 사용한다.

검증 항목:

- `schemaVersion == dcgo.parity.trace.v1`
- `scenarioId == ST1 dynamic SecurityAttack replay`
- `Attack` action event가 존재
- security check 과정의 move event가 존재
- serialized JSON에 `C:\` 또는 `E:\` 같은 절대 경로가 없다

후속 `58_parity_fixture_comparer`는 이 JSON 계약을 입력으로 받아 Unity/RL event stream을 비교한다. 이 단계에서는 Unity trace exporter와 comparer를 아직 구현하지 않는다.

## Outcome Mapping

현재 RL.Engine trace는 action result 객체를 직접 저장하지 않으므로, parity exporter는 trace label과 move result에서 최소 outcome을 만든다.

| Trace label | Outcome |
| --- | --- |
| `security-check-execute:{defender}:{card}` | `SecurityOutcome=ExecuteSecuritySkill`, `SecurityDefender`, `SecurityCard`, `FinalZone` |
| `security-check-trash:{defender}:{card}` | `SecurityOutcome=TrashCheckedCard`, `SecurityDefender`, `SecurityCard`, `FinalZone` |
| `destroy-top:{permanent}:{card}` | `BattleOutcome=DestroyTop`, `DestroyedPermanent`, `DestroyedCard`, `FinalZone` |
| `destroy-source:{permanent}:{card}` | `BattleOutcome=DestroySource`, `DestroyedPermanent`, `DestroyedCard`, `FinalZone` |

후속 comparer 단계에서는 Unity trace와 맞춘 battle/security result DTO가 필요하면 이 payload를 확장한다.
