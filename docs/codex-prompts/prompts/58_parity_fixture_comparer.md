AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 58 - Parity Fixture Comparer

## 목표

Unity에서 추출한 rule-visible trace fixture와 RL.Engine trace를 비교하는 deterministic comparer/report를 구현한다.

이번 작업에서도 Unity 원본 파일은 수정하지 않는다.
실제 Unity fixture가 없으면 synthetic fixture만 사용하고 parity 통과를 주장하지 않는다.

## 구현

- fixture loader
- schema validation
- event alignment
- snapshot comparison
- 허용 가능한 비본질적 차이 정규화
- 첫 mismatch 위치
- structured diff:
  - timing
  - zone
  - permanent
  - modifier
  - decision/selection
  - memory/phase
- machine-readable result
- 한국어 markdown report

## 정책

- field ordering처럼 규칙적으로 중요한 값은 무시하지 않는다.
- Unity object instance id 같은 비본질적 값만 normalization 대상으로 허용한다.
- mismatch를 warning만 남기고 pass 처리하지 않는다.
- missing fixture는 `NotRun`, not `Passed`.
- per-scenario expected exclusions를 최소화한다.

## 테스트

- exact match
- phase mismatch
- memory mismatch
- zone order mismatch
- trigger order mismatch
- selection mismatch
- hidden data redaction
- schema mismatch
- missing fixture
- synthetic fixture round trip

## 문서

fixture 저장 위치, 파일 이름 규칙, Unity exporter 실행 절차 초안을 작성한다.
