# Golden Scenarios

최신 기준일: 2026-06-18

Golden scenario는 학습 데이터가 아니라 엔진 검증 데이터다. 현재 ST1 target deck validation은 통과하지만, 더 넓은 DCGO 룰 검증을 위해 scripted/golden scenario를 계속 확장해야 한다.

## 최신 상태 요약 - 2026-06-18

- 현재 golden suite는 ST1/minimal battle 중심이다.
- ST1~ST3 registry snapshot과 구조 guard는 존재하지만, ST1~ST3 전용 expanded golden scenario suite는 아직 future work다.
- 아래 ST1~ST3 후보는 historical inventory 단계의 pass 09 후보이며, 현재 통과된 golden suite라고 해석하면 안 된다.

## Queue 46 Golden Scenario Gap - 2026-06-18

이번 queue 46은 golden scenario 구현이 아니라 gap 계획이다. 현재 검증은 세 층으로 나뉜다.

| 검증 층 | 현재 근거 | Golden coverage 판단 |
| --- | --- | --- |
| minimal battle scripted scenario | `CreateValidationHarnessV1Scenarios()`의 7개 scenario | replay 가능한 현재 golden suite |
| ST1 카드별 regression | `ST1-*` 단위 테스트와 일부 replay determinism | 카드 효과 단위 coverage는 있으나 full scenario suite는 아님 |
| ST2/ST3 카드별 regression | `ST2-*`, `ST3-*` 단위 테스트 | registry/status/file validation과 효과 단위 coverage는 있으나 expanded golden suite는 없음 |

따라서 `Implemented` 상태는 원본 의미를 headless 경계로 보존했다는 뜻이지, Unity 원본 trace와 비교 가능한 `Verified` 상태를 뜻하지 않는다.

## 현재 Golden Suite

| Scenario | 검증 책임 | Replay 대상 |
| --- | --- | --- |
| deck-out loss | draw phase deck-out loss | 일부 |
| security 0 direct attack win | security 0 상태 direct attack win | 예 |
| lower DP Digimon deleted | DP battle loser trash | 일부 |
| equal DP battle | 동률 DP battle 양쪽 trash | 일부 |
| normal digivolve draw | normal digivolve, source 이동, draw 1, memory cost | 예 |
| memory crossing turn end | memory crossing turn/phase 전환 | 일부 |
| hatch then move from breeding | hatch/breeding move invariant | 일부 |

## ST1-12 Regression Coverage

이번 단계에서 추가한 것은 golden end-to-end trace가 아니라 unit/regression coverage다.

- security check에서 ST1-12가 `Executing`으로 이동한 뒤 비용 없이 battle area tamer로 play된다.
- play 성공 시 checked card는 trash로 이동하지 않는다.
- field가 가득 차면 play가 발생하지 않고 checked card는 trash로 이동한다.
- trigger pipeline이 explicit executing source의 `SecuritySkill` descriptor를 실행한다.
- owner turn tamer aura가 effective DP 계산에 반영된다.

## ST1 Golden Gap

ST1 target deck은 completion gate를 통과하지만, 아래 효과는 아직 full scripted golden scenario로 고정되지 않았다. 기존 단위 테스트가 존재해도, 여러 action/phase/security 흐름을 거친 replay 가능한 scenario와는 구분한다.

| 카드 | 현재 coverage | Golden gap |
| --- | --- | --- |
| ST1-06, ST1-09 | memory script와 blocker selection/request 단위 검증 | 실제 block 선택, OnBlockAnyone, attack lifecycle을 묶은 end-to-end scenario |
| ST1-08 | WhenDigivolving selection, DP duration, cleanup 단위 검증 | digivolve action부터 다음 battle 결과 변화까지 이어지는 replay scenario |
| ST1-11 | dynamic SecurityAttack continuous effect 단위 검증 | source 수에 따른 security check 횟수 replay scenario |
| ST1-12 | security play-self tamer와 aura 단위 검증 | security에서 tamer가 play된 뒤 다음 owner turn DP battle 결과가 바뀌는 replay scenario |
| ST1-13, ST1-14 | option/security duration 단위 검증 | security activation 후 turn transition과 실제 security/battle 결과 변화 scenario |
| ST1-15, ST1-16 | main/security Activate Main Option deletion 단위 검증 | security attack 흐름 안에서 selection, deletion, final zone을 replay trace로 고정하는 scenario |

## 다음 Golden Scenario 후보

- ST1-12가 security에서 play된 뒤 다음 owner turn aura가 실제 DP battle 결과를 바꾸는 scripted scenario.
- ST1-11 SecurityAttack continuous effect가 security check 횟수에 반영되는 scripted scenario.
- ST1-08/13 duration cleanup이 실제 turn transition에서 battle 결과를 바꾸는 scripted scenario.
- ST1-15/16 security Activate Main Option이 full security attack 흐름 안에서 target deletion을 수행하는 scenario.
- Unity 원본 trace와 RL.Engine trace의 구조화 비교 scenario.

## ST1-ST3 Golden Scenario 후보

ST1-ST3 inventory 단계에서 새 golden scenario를 실행하지는 않았다. queue 46 기준으로 ST2/ST3 효과는 단위 regression coverage가 존재하지만, 아래 후보들은 아직 golden suite에 들어가지 않았다.

Historical planning note: 2026-06-14 ST1-ST3 inventory/pass plan 문서화 단계에서 아래 후보를 future Implementation Pass 09 대상으로 기록했다. 현재도 새 golden scenario를 실행하거나 통과시킨 기록은 없다.

| Scenario 후보 | 주요 카드 | 현재 coverage | Replay 구분 | Unity trace 필요성 |
| --- | --- | --- | --- | --- |
| Blue memory option/security | ST2-13 | main/security memory 단위 검증 | replay 가능 | headless scripted로 충분 |
| Blue source trash attack | ST2-03, ST2-06, ST2-09 | selection과 bottom source trash 단위 검증 | replay 가능 | source order가 원본과 다른 경우 Unity trace 필요 |
| Blue bounce to hand | ST2-16 | top card hand, source trash, security activation 단위 검증 | replay 가능 | headless scripted로 충분 |
| Blue source-card play | ST2-15 | chained selection과 source play 단위 검증 | replay 가능하나 selection resume 민감 | Unity trace 우선 비교 권장 |
| Blue no-source restriction | ST2-14 | cannot attack/block duration과 cleanup 단위 검증 | replay 가능 | block priority 세부는 Unity trace 필요 |
| Yellow recovery | ST3-09 | deck top recovery와 조건 단위 검증 | replay 가능 | headless scripted로 충분 |
| Yellow DP reduction attack/security | ST3-08, ST3-11, ST3-16 | DP 감소, 0 DP deletion, security activate main 단위 검증 | replay 가능 | DP-zero timing은 Unity trace 비교 권장 |
| Yellow security to hand | ST3-13, ST3-14 | checked card final zone hand 단위 검증 | replay 가능 | headless scripted로 충분 |
| Yellow SecurityAttack reduction | ST3-15 | negative SecurityAttack duration 단위 검증 | replay 가능 | security check 반복 흐름은 scripted 우선 |
| Yellow security DP aura/play | ST3-12 | security DP continuous, security play-self tamer 단위 검증 | replay 가능 | headless scripted로 충분 |
| DP-zero deletion trigger | ST3-01, ST3-04 | DP-zero deletion payload와 inherited trigger 단위 검증 | snapshot-only에서 시작 | Unity trace 비교 권장 |

## Snapshot-only 후보

아래 항목은 현재 action trace만으로는 원본과 완전히 비교하기 어렵다. 먼저 snapshot-only report로 상태 전후를 고정하고, Unity trace harness가 생긴 뒤 replay parity 대상으로 승격한다.

| 후보 | 이유 |
| --- | --- |
| full `MultipleSkills` simultaneous trigger priority | 원본은 turn player/non-turn player와 선택 UI 순서가 섞이며, 현재 FIFO queue만으로는 충분하지 않다. |
| block/counter timing end-to-end | block selection request는 있으나 원본 `AttackProcess`와 `MultipleSkills` priority 흐름 전체가 아직 parity 대상이 아니다. |
| ST2-15 chained source-card play | source card 선택, source 제거, 새 permanent 생성, 후속 trigger 순서가 복합적이다. |
| ST3-01/ST3-04 DP-zero deletion trigger | RuleProcessor payload는 있으나 원본 삭제 원인 분류와 trigger timing 비교가 필요하다. |

## 1차 우선순위 Golden Scenario

다음 5개 이하를 1차 구현 대상으로 둔다. 모두 학습 데이터가 아니라 검증 데이터다.

| 우선순위 | Scenario | 범위 | 선택 이유 |
| ---: | --- | --- | --- |
| 1 | ST1 security tamer aura battle | ST1-12 | 이미 단위 coverage가 있고, security effect가 다음 turn battle 결과까지 이어지는 대표 end-to-end gap이다. |
| 2 | ST1 dynamic SecurityAttack replay | ST1-11 | source 수 기반 derived value가 실제 security check 횟수를 바꾸는지 고정한다. |
| 3 | Blue source trash attack replay | ST2-03, ST2-06, ST2-09 | source order와 selection validation은 원본 parity에서 자주 어긋날 수 있는 zone 이동이다. |
| 4 | Blue bounce/security option replay | ST2-16 | top card hand 이동과 sources trash를 한 scenario에서 검증할 수 있다. |
| 5 | Yellow recovery/security hand replay | ST3-09, ST3-13, ST3-14 | security zone 증감과 final zone이 trash가 아닌 hand인 흐름을 함께 고정한다. |

추후 golden scenario 구현 단계는 고정된 ST1~ST3 target pool count도 함께 확인해야 한다. 기준은 ST1 16장, ST2 16장, ST3 16장, 총 48장, 원본 `CardEffectClassName` 기준 explicit NoEffect 후보 12장, `ST2-07`/`ST3-07`의 shared `ST1_06` mapping이다.

이 scenario들은 검증 데이터로만 유지한다. engine completion checklist가 통과하기 전에는 RL 학습 데이터로 사용하지 않는다.

## Replay 제한

`ReplayRunner`는 현재 action trace 중심으로 replay한다. snapshot-only step이나 외부 instrumentation이 필요한 Unity trace 비교는 별도 adapter/harness 확장이 필요하다. 이 제한은 silent no-op이 아니라 현재 replay capability의 명시 범위다.

## Queue 46 테스트

이번 작업은 문서 gap 계획만 수행했다. 코드와 테스트 코드를 수정하지 않았으므로 당시 전체 테스트는 실행하지 않았다. 최신 기록된 구조 guard 결과는 `All 225 tests passed.`다.
