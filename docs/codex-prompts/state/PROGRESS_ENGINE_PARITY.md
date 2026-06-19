# Engine Parity Progress

| Date | Queue | Result | Baseline commit | Tests | Remaining risk |
| --- | ---: | --- | --- | --- | --- |
| 2026-06-18 | start | GitHub main `a101acd2` 기준 engine-parity queue 생성 | `a101acd2` | latest recorded `All 214 tests passed` | whole-engine completion 미실행 |
| 2026-06-18 | 47 | engine-parity queue 정렬, README/INDEX 갱신, github-current 47 superseded 처리 | `a101acd2` | 문서/queue 갱신만 수행 | 48번 asset effect mapping reconcile 대기 |
| 2026-06-19 | 48 | `ST2-07`/`ST3-07` shared `ST1_06` mapping을 card-id 기반 `Implemented` script로 정리하고 `ST3-02` variant를 문서화 | `8e4739f9` | `All 216 tests passed` | `ST3_02_P2.asset` source effect body/variant identity needs-review |
| 2026-06-19 | 49 | asset registry mapping validator를 추가해 원본 asset, registry, status snapshot, 카드별 파일, source body 존재를 대조 | `69529bc1` + local changes | `All 225 tests passed` | `ST3-02` base/P1은 NoEffect 후보, P2는 source body 미확인 needs-review 유지 |
| 2026-06-19 | 50 | Option hand play lifecycle을 원본 `UseOptionClass` 기준 `Hand -> Executing -> OptionSkill -> Trash`로 정렬 | `15665a95` + local changes | `All 234 tests passed` | `ST3-02` P2 source body 미확인 needs-review/blocking finding 유지, whole-engine gate 미실행 |
