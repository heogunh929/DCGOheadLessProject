# AS-IS Porting Scope CSV Index

이 문서는 `asis-porting-scope-decision.json`의 파일별 포팅 결정을 CSV로 분리한 색인이다.
CSV는 Windows Excel 호환을 위해 UTF-8 BOM(`utf-8-sig`)으로 생성했다.

## Source

- Source JSON: `docs/generated/as-is-restart/asis-porting-scope-decision.json`
- Source file count: 71180

## CSV Files

| Decision | Rows | Path | SizeBytes |
| --- | --- | --- | --- |
| Port | 12541 | docs/generated/as-is-restart/asis-porting-scope-port.csv | 4664334 |
| ReferenceOnly | 17653 | docs/generated/as-is-restart/asis-porting-scope-reference-only.csv | 6212891 |
| Exclude | 40986 | docs/generated/as-is-restart/asis-porting-scope-exclude.csv | 14297057 |
| ManualReview | 0 | docs/generated/as-is-restart/asis-porting-scope-manual-review.csv | 199 |

## Columns

- `relativePath`, `fileName`, `extension`, `sizeBytes`, `topLevelFolder`
- `portingDecision`, `portingBucket`, `blockedBy`, `nextAction`
- `needLevel`, `headlessCategory`, `sourceScope`, `headlessUse`
- `sourceClassifications`, `portingReason`

## Notes

- `Port`는 headless 엔진에 의미를 이식해야 하는 대상이다.
- `ReferenceOnly`는 직접 구현하지 않지만 근거/검증/해석용으로 보존하는 대상이다.
- `Exclude`는 headless battle engine에 직접 포팅하지 않는 대상이다.
- `ManualReview`는 자동 판정 불가 항목이며, 현재 데이터 row는 0개다.
- 이 작업은 CSV/문서 생성만 수행했으며 `src`, 원본 DCGO, Foundation Gate, generated status는 수정하지 않았다.
