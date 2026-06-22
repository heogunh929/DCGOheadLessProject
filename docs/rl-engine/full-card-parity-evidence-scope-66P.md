# 66P full-card parity evidence scope

## 목적

이번 foundation 작업은 generated full-card source scaffold 전체에 대해 Unity/RL parity fixture coverage 증거를 생성하는 것이다.
개별 `CardEffect` body를 구현하지 않으며, C0039 이후 card-porting batch도 실행하지 않는다.

## 입력

- `docs/generated/full-card-source-scaffold/sources/*.json`
- `docs/generated/full-card-source-scaffold/status-registry.json`
- 기대 Unity fixture 위치: `docs/generated/parity-fixtures/unity/full-card-source`
- 기대 RL fixture 위치: `docs/generated/parity-fixtures/rl/full-card-source`
- 기대 comparison report 위치: `docs/generated/parity-fixtures/reports/full-card-source`

원본 Source of Truth는 로컬 `E:\headlessDCGO\DCGO\Assets`에 있으며, 이번 작업에서는 원본 파일을 수정하지 않았다.

## 산출물

- `scripts/generate_full_card_parity_evidence.py`
- `docs/generated/full-card-parity-evidence.json`
- `docs/rl-engine/full-card-parity-evidence-66P.md`

`docs/generated/full-card-parity-evidence.json`은 generated scaffold의 각 source effect를 expected parity scenario로 매핑한다.
fixture나 comparison report가 없으면 coverage status는 `NotRun`이다.

## 66P 결과

- Source effects: 3918
- Affected definitions: 7922
- Passed: 0
- Failed: 0
- NotRun: 3918
- Compared: 0
- Generated implemented or verified count: 0
- All generated source effects have Unity parity: `false`

## 정책

- `NotRun`은 parity pass가 아니다.
- synthetic comparer fixture는 generated full-card Unity parity로 계산하지 않는다.
- 이 report만으로 card-porting, runtime/generated status 승격, `Verified` 승격을 허용하지 않는다.
- generated status registry는 여전히 3918개 source effect를 `Unsupported`로 집계한다.

## 다음 foundation 작업

다음 foundation queue는 generated/runtime status mismatch closure다.
`ContinuousOrStaticEffect`는 66K-66N coverage와 66O evidence refresh 이후에도 `PartiallyImplemented` 상태로 남아 있다.
