# Golden Scenarios

최신 기준일: 2026-06-13

Golden scenario는 학습 데이터가 아니라 엔진 검증 데이터다. 현재 ST1 target deck validation은 통과하지만, 더 넓은 DCGO 룰 검증을 위해 scripted/golden scenario를 계속 확장해야 한다.

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

## 다음 Golden Scenario 후보

- ST1-12가 security에서 play된 뒤 다음 owner turn aura가 실제 DP battle 결과를 바꾸는 scripted scenario.
- ST1-11 SecurityAttack continuous effect가 security check 횟수에 반영되는 scripted scenario.
- ST1-08/13 duration cleanup이 실제 turn transition에서 battle 결과를 바꾸는 scripted scenario.
- ST1-15/16 security Activate Main Option이 full security attack 흐름 안에서 target deletion을 수행하는 scenario.
- Unity 원본 trace와 RL.Engine trace의 구조화 비교 scenario.

## ST1-ST3 Golden Scenario 후보

ST1-ST3 inventory 단계에서 새 golden scenario를 실행하지는 않는다. 다음 후보는 ST2/ST3 common layer 구현 후 검증 데이터로 추가한다.

Planning note: 2026-06-14 현재 요청은 ST1-ST3 inventory/pass plan 문서화 전용이다. 아래 항목은 future Implementation Pass 09에서 추가할 golden scenario 후보이며, 이번 단계에서 새 golden scenario를 실행하거나 통과시키지 않는다.

| Scenario 후보 | 주요 카드 | 검증 책임 |
| --- | --- | --- |
| Blue memory option/security | ST2-13 | main memory +1, security memory +2, checked card zone movement |
| Blue source trash attack | ST2-03, ST2-06, ST2-09 | bottom digivolution source trash, selection validation, source order |
| Blue bounce to hand | ST2-16 | opponent Digimon top card to hand, sources to trash |
| Blue source-card play | ST2-15 | chained selection, source card play as new permanent without cost |
| Yellow recovery | ST3-09 | top deck to top security, security count condition |
| Yellow DP reduction attack | ST3-08, ST3-11, ST3-16 | temporary negative DP, cleanup, security activate main |
| Yellow security to hand | ST3-13, ST3-14 | checked security card final zone is hand rather than trash |
| Yellow SecurityAttack reduction | ST3-15 | negative SecurityAttack duration, cleanup scope |
| DP-zero deletion trigger | ST3-01, ST3-04 | trigger only when opponent Digimon is deleted by 0 DP |

Pass 09 should also verify the fixed ST1-ST3 target pool counts: ST1 16 cards, ST2 16 cards, ST3 16 cards, 48 total cards, 12 explicit NoEffect candidates, and shared `ST1_06` mappings for ST2-07 and ST3-07.

These scenarios remain validation data only. They must not be used as RL training data before the engine completion checklist passes.

## Replay 제한

`ReplayRunner`는 현재 action trace 중심으로 replay한다. snapshot-only step이나 외부 instrumentation이 필요한 Unity trace 비교는 별도 adapter/harness 확장이 필요하다. 이 제한은 silent no-op이 아니라 현재 replay capability의 명시 범위다.
